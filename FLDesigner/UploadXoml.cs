using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;
using System.Drawing.Imaging;
using System.Workflow.ComponentModel;
using FLTools;
using FLCore;

namespace FLDesigner
{
    public partial class UploadXoml : Form
    {
        //private string _serverPath;
        private LoginForm fFrmLogin = null;

        public UploadXoml()
        {
            InitializeComponent();
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }

        private FLDesignerCore.FLDesigner _flDesigner;
        public FLDesignerCore.FLDesigner FLDesigner
        {
            get
            {
                return _flDesigner;
            }
            set
            {
                _flDesigner = value;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked && string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Update package is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (checkBox2.Checked && string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Update webservice url is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FileInfo file = new FileInfo(_fileName);
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    string package = textBox1.Text;
                    string s = string.Format(@"\Workflow\{0}\{1}", package, file.Name);

                    XmlDocument doc = new XmlDocument();
                    doc.Load(_fileName);
                    string xml = doc.InnerXml;

                    object[] objs = CliUtils.CallMethod("GLModule", "UpdateFLXoml", new object[] { s, xml });
                    if (objs[0] != null && objs[0].ToString() != "0")
                    {
                        MessageBox.Show(objs[1].ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    // 更改Url
                    EEPWebClient.UploadXoml uploadXoml = new EEPWebClient.UploadXoml();
                    uploadXoml.Url = textBox2.Text;

                    string fileName = file.Name.Substring(file.Name.LastIndexOf('\\') + 1, file.Name.LastIndexOf('.') - file.Name.LastIndexOf('\\'));
                    string jpgName = string.Format("{0}jpg", fileName);
                    string xmlName = string.Format("{0}xml", fileName);

                    #region Jpg
                    MemoryStream stream1 = new MemoryStream();
                    _flDesigner.WorkflowView.SaveWorkflowImage(stream1, ImageFormat.Jpeg);
                    Bitmap image1 = new Bitmap(stream1);
                    //image1.Save(@"C:\0001.jpg");
                    Bitmap image2 = new Bitmap(image1.Width - 56, image1.Height - 56);
                    for (int x = 29; x <= image1.Width - 28; x++)
                    {
                        for (int y = 29; y <= image1.Height - 28; y++)
                        {
                            image2.SetPixel(x - 29, y - 29, image1.GetPixel(x, y));
                        }
                    }
                    image1.Dispose();

                    MemoryStream stream2 = new MemoryStream();
                    image2.Save(stream2, ImageFormat.Jpeg);
                    //image2.Save(@"C:\0002.jpg");

                    byte[] bytes = stream2.ToArray();
                    stream1.Close();
                    stream2.Close();
                    #endregion

                    #region Xml

                    string xml = string.Empty;
                    if (this.FLDesigner.Flow.Activities.Count > 0)
                    {
                        Activity startStand = this.FLDesigner.Flow.Activities[0];
                        XmlDocument doc = new XmlDocument();
                        XmlNode root = doc.SelectSingleNode("FlowDetails");
                        if (root == null)
                        {
                            root = doc.CreateElement("FlowDetails");
                            doc.AppendChild(root);
                        }

                        if (startStand is FLStand)
                        {
                            //FormName
                            XmlNode nFormName = root.SelectSingleNode("FormName");
                            if (nFormName == null)
                            {
                                nFormName = doc.CreateElement("FormName");
                                root.AppendChild(nFormName);
                            }
                            if (!(this.FLDesigner.Flow.Activities[0] is FLHyperLink) && !(this.FLDesigner.Flow.Activities[0] is FLQuery))
                            {
                                string webFormName = ((FLStand)startStand).WebFormName;
                                string gloFormName = ((IFLRootActivity)this.FLDesigner.Flow).WebFormName;
                                nFormName.InnerText = string.IsNullOrEmpty(webFormName) ? gloFormName : webFormName;
                            }

                            //FlowNavMode
                            XmlNode nFlowNavMode = root.SelectSingleNode("FlowNavMode");
                            if (nFlowNavMode == null)
                            {
                                nFlowNavMode = doc.CreateElement("FlowNavMode");
                                root.AppendChild(nFlowNavMode);
                            }
                            nFlowNavMode.InnerText = ((FLStand)startStand).FLNavigatorMode.ToString();

                            //NavMode
                            XmlNode nNavMode = root.SelectSingleNode("NavMode");
                            if (nNavMode == null)
                            {
                                nNavMode = doc.CreateElement("NavMode");
                                root.AppendChild(nNavMode);
                            }
                            nNavMode.InnerText = ((FLStand)startStand).NavigatorMode.ToString();

                            //Paramters
                            XmlNode nParameters = root.SelectSingleNode("Parameters");
                            if (nParameters == null)
                            {
                                nParameters = doc.CreateElement("Parameters");
                                root.AppendChild(nParameters);
                            }
                            nParameters.InnerText = ((FLStand)startStand).Parameters;
                        }

                        //FlowFileName
                        XmlNode nFlowFileName = root.SelectSingleNode("FlowFileName");
                        if (nFlowFileName == null)
                        {
                            nFlowFileName = doc.CreateElement("FlowFileName");
                            root.AppendChild(nFlowFileName);
                        }
                        object[] objs = CliUtils.CallMethod("GLModule", "GetServerPath", new object[] { });
                        string f = objs[1].ToString();
                        f += string.Format(@"\Workflow\{0}\{1}", textBox1.Text, xmlName);
                        f = f.ToUpper();
                        nFlowFileName.InnerText = f.Substring(0, f.IndexOf(".XML"));

                        //ImageWidth
                        XmlNode nImageWidth = root.SelectSingleNode("ImageWidth");
                        if (nImageWidth == null)
                        {
                            nImageWidth = doc.CreateElement("ImageWidth");
                            root.AppendChild(nImageWidth);
                        }
                        nImageWidth.InnerText = image2.Width.ToString();

                        //Deleted activities
                        XmlNode nlnk = root.SelectSingleNode("FLHyperLink");
                        if (nlnk != null)
                        {
                            foreach (XmlNode node in nlnk.ChildNodes)
                            {
                                bool nodeExist = false;
                                foreach (Activity a in this.FLDesigner.Flow.Activities)
                                {
                                    if (a is FLHyperLink)
                                    {
                                        if (node.Name == a.Name)
                                        {
                                            nodeExist = true;
                                            break;
                                        }
                                    }
                                }
                                if (!nodeExist)
                                {
                                    nlnk.RemoveChild(node);
                                }
                            }
                        }

                        XmlNode nquery = root.SelectSingleNode("FLQuery");
                        if (nquery != null)
                        {
                            foreach (XmlNode node in nquery.ChildNodes)
                            {
                                bool nodeExist = false;
                                foreach (Activity a in this.FLDesigner.Flow.Activities)
                                {
                                    if (a is FLQuery)
                                    {
                                        if (node.Name == a.Name)
                                        {
                                            nodeExist = true;
                                            break;
                                        }
                                    }
                                }
                                if (!nodeExist)
                                {
                                    nquery.RemoveChild(node);
                                }
                            }
                        }

                        xml = doc.InnerXml;
                    }
                    #endregion

                    jpgName = string.Format(@"image\{0}", jpgName);
                    xmlName = string.Format(@"image\{0}", xmlName);
                    string message = uploadXoml.Upload(jpgName, bytes, xmlName, xml);

                    if (!string.IsNullOrEmpty(message))
                    {
                        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                MessageBox.Show("Update succeed!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox1.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.Enabled = true;
                checkBox1.Checked = true;
                textBox1.Enabled = true;
            }
            else
            {
                textBox2.Enabled = false;
            }
        }

        private void UpdateToEEPNetServerForm_Load(object sender, EventArgs e)
        {
            String s = Application.StartupPath + "\\";
            CliUtils.LoadLoginServiceConfig(s + "FLDesigner.exe.config");

            CliUtils.fClientLang = GetClientLanguage();
            CliUtils.fClientSystem = "Win";

            bool freg = Register(false);
            fFrmLogin = new LoginForm(freg);
            if (fFrmLogin.ShowDialog(this) == DialogResult.OK)
            {
                if (freg == false)
                {
                    if (!Register(true))
                    {
                        this.Close();
                    }
                }
                CheckUser();
            }
            else
            {
                CliUtils.fLoginUser = string.Empty;
                Close();
            }
        }

        private bool Register(bool isShowMessage)
        {
            string message = "";
            bool rtn = CliUtils.Register(ref message);
            if (rtn)
            {
                CliUtils.GetSysXml(Application.StartupPath + @"\sysmsg.xml");
            }
            else
            {
                if (isShowMessage)
                {
                    MessageBox.Show(message);
                }
            }

            return rtn;
        }

        private void CheckUser()
        {
            CheckUser(false);
        }

        private void CheckUser(bool relogin)
        {
            CliUtils.fLoginUser = fFrmLogin.GetUserId();
            CliUtils.fLoginPassword = fFrmLogin.GetPwd();
            CliUtils.fLoginDB = fFrmLogin.GetDB();
            CliUtils.fCurrentProject = fFrmLogin.GetCurProject();
            LoginResult result = LoginResult.Success;
            if (CliUtils.fLoginUser.Contains("'"))
            {
                result = LoginResult.UserNotFound;
            }
            else
            {
                string sParam = CliUtils.fLoginUser + ':' + CliUtils.fLoginPassword + ':' + CliUtils.fLoginDB;
                if (relogin)
                {
                    sParam += ":1";
                }
                else
                {
                    sParam += ":0";
                }

                object[] myRet = CliUtils.CallMethod("GLModule", "CheckManagerRight", new object[] { CliUtils.fLoginDB, CliUtils.fLoginUser });
                if (myRet[1].ToString() != "0")
                {
                    if (myRet[1].ToString() == "1")
                    {
                        MessageBox.Show("No right to use Manager.");
                    }
                    else
                    {
                        MessageBox.Show("User Not Found.");
                    }
                    if (fFrmLogin.ShowDialog(this) == DialogResult.OK)
                    {
                        CheckUser();
                    }
                    else
                    {
                        this.Close();
                    }
                    return;
                } 
                
                myRet = CliUtils.CallMethod("GLModule", "CheckUser", new object[] { (object)sParam });
                result = (LoginResult)myRet[1];
                switch (result)
                {
                    case LoginResult.UserNotFound:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserNotFound");
                            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.PasswordError:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserOrPasswordError");
                            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.UserLogined:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserIsLogined");
                            MessageBox.Show(this, string.Format(message, CliUtils.fLoginUser), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.RequestReLogin:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserReLogined");
                            if (MessageBox.Show(string.Format(message, CliUtils.fLoginUser)
                                , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                CheckUser(true);
                            }
                            else
                            {
                                CliUtils.fLoginUser = string.Empty;
                                this.Close();
                            }
                            return;
                        }
                    default:
                        {
                            CliUtils.fUserName = myRet[2].ToString();
                            CliUtils.fLoginUser = myRet[3].ToString();
                            myRet = CliUtils.CallMethod("GLModule", "GetUserGroup", new object[] { CliUtils.fLoginUser });
                            if (myRet != null && (int)myRet[0] == 0)
                            {
                                CliUtils.fGroupID = myRet[1].ToString();
                            }
                            SaveToClientXML(CliUtils.fLoginUser, CliUtils.fLoginDB, CliUtils.fCurrentProject);
                            break;
                        }
                }
            }
            if (result != LoginResult.Success)
            {
                if (fFrmLogin.ShowDialog(this) == DialogResult.OK)
                {
                    CheckUser();
                }
                else
                {
                    CliUtils.fLoginUser = string.Empty;
                    this.Close();
                }
            }
        }

        private void SaveToClientXML(string sLoginUser, string sLoginDB, string sCurrentProject)
        {
            String sfile = Application.StartupPath + "\\FLDesigner.xml";
            string sUser = sLoginUser;
            string sDB = sLoginDB;
            string sSol = sCurrentProject;
            string stemp = "";
            XmlDocument xml = new XmlDocument();
            if (File.Exists(sfile))
            {
                xml.Load(sfile);
                XmlNode el = xml.DocumentElement;
                foreach (XmlNode xNode in el.ChildNodes)
                {

                    if (xNode.Name.ToUpper().Equals("USER"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sLoginUser))
                                sUser = sUser + "," + s;
                        }
                    }
                    else if (xNode.Name.ToUpper().Equals("DATABASE"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sLoginDB))
                                sDB = sDB + "," + s;
                        }
                    }
                    else if (xNode.Name.ToUpper().Equals("SOLUTION"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sCurrentProject))
                                sSol = sSol + "," + s;
                        }
                    }
                }

                File.Delete(sfile);
            }
            else
            {
                sUser = sLoginUser; sDB = sLoginDB; sSol = sCurrentProject;
            }

            FileStream aFileStream = new FileStream(sfile, FileMode.Create);
            try
            {
                XmlTextWriter w = new XmlTextWriter(aFileStream, new System.Text.ASCIIEncoding());
                w.Formatting = Formatting.Indented;
                w.WriteStartElement("LoginInfo");

                w.WriteStartElement("User");
                w.WriteValue(sUser);
                w.WriteEndElement();

                w.WriteStartElement("DataBase");
                w.WriteValue(sDB);
                w.WriteEndElement();

                w.WriteStartElement("Solution");
                w.WriteValue(sSol);
                w.WriteEndElement();

                w.WriteEndElement();
                w.Close();
            }
            finally
            {
                aFileStream.Close();
            }
        }

        static private SYS_LANGUAGE GetClientLanguage()
        {
            uint dwlang = GetThreadLocale();
            ushort wlang = (ushort)dwlang;
            ushort wprilangid = (ushort)(wlang & 0x3FF);
            ushort wsublangid = (ushort)(wlang >> 10);

            if (0x09 == wprilangid)
                return SYS_LANGUAGE.ENG;
            else if (0x04 == wprilangid)
            {
                if (0x01 == wsublangid)
                    return SYS_LANGUAGE.TRA;
                else if (0x02 == wsublangid)
                    return SYS_LANGUAGE.SIM;
                else if (0x03 == wsublangid)
                    return SYS_LANGUAGE.HKG;
                else
                    return SYS_LANGUAGE.TRA;
            }
            else if (0x11 == wprilangid)
                return SYS_LANGUAGE.JPN;
            else
                return SYS_LANGUAGE.ENG;
        }

        [DllImport("KERNEL32.DLL", EntryPoint = "GetThreadLocale", SetLastError = true,
            CharSet = CharSet.Unicode, ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetThreadLocale();
    }
}
