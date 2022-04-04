using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Workflow.ComponentModel;
using System.Drawing.Design;
using System.Workflow.Activities;
using FLDesignerCore;
using System.ComponentModel.Design;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Xml;
using System.IO;
using FLCore;
using FLTools;
using System.Runtime.InteropServices;
using Srvtools;
using FLTools.ComponentModel;

namespace FLDesigner
{
    public partial class MainForm : Form
    {
        private LoginForm fFrmLogin = null;

        private IServiceProvider _serviceProvider;
        private ContextMenu __ContextMenu;
        private MenuItem _MenuItemCutActivity;
        private MenuItem _MenuItemCopyActivity;
        private MenuItem _MenuItemPasteActivity;
        private MenuItem _MenuItemDeleteActivity;
        private MenuItem _MenuItemAddIfElseBranch;
        private MenuItem _MenuItemAddSequence;
        private MenuItem _MenuItemSaveLoacation;
        private MenuItem _MenuItemSyncInstance;
        private MenuItem _MenuItemUpdateToEEPNetServer;
        private string _fileName = string.Empty;
        private bool _isFLLoaded = false;
        private Activity _copyActivity;
        private string _copyActivityString;
        private object _selectedActivity;

        public MainForm()
        {
            InitializeComponent();

            _serviceProvider = flDesigner1._GetService(typeof(IServiceProvider)) as IServiceProvider;
            _copyActivity = null;
            _copyActivityString = string.Empty;

            FLActivityToolbox toolbox = new FLActivityToolbox(_serviceProvider);
            flDesigner1.AddService(typeof(IToolboxService), toolbox);
            InitFLViewContextMenu();
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

        private void InitFLViewContextMenu()
        {
            __ContextMenu = new ContextMenu();

            _MenuItemCutActivity = new MenuItem("Cut Activity", ItemCut_Click);
            _MenuItemCutActivity.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemCutActivity);

            _MenuItemCopyActivity = new MenuItem("Copy Activity", ItemCopy_Click);
            _MenuItemCopyActivity.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemCopyActivity);

            _MenuItemPasteActivity = new MenuItem("Paste Activity", ItemPaste_Click);
            _MenuItemPasteActivity.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemPasteActivity);

            _MenuItemDeleteActivity = new MenuItem("Delete Activity", ItemDelete_Click);
            _MenuItemDeleteActivity.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemDeleteActivity);

            MenuItem item2 = new MenuItem("-");
            __ContextMenu.MenuItems.Add(item2);

            _MenuItemAddIfElseBranch = new MenuItem("Add IfElseBranchActivity", ItemAddIfElseBranch_Click);
            _MenuItemAddIfElseBranch.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemAddIfElseBranch);

            _MenuItemAddSequence = new MenuItem("Add SequenceActivity", ItemAddSequence_Click);
            _MenuItemAddSequence.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemAddSequence);

            _MenuItemSaveLoacation = new MenuItem("SaveActivityLocation", ItemSaveLocation_Click);
            _MenuItemSaveLoacation.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemSaveLoacation);

            MenuItem item3 = new MenuItem("-");
            __ContextMenu.MenuItems.Add(item3);

            _MenuItemSyncInstance = new MenuItem("Modify Instance", ItemSyncInstance_Click);
            _MenuItemSyncInstance.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemSyncInstance);

            _MenuItemUpdateToEEPNetServer = new MenuItem("Update to EEPNetServer", ItemUpdateToEEPNetServer_Click);
            _MenuItemUpdateToEEPNetServer.Enabled = false;
            __ContextMenu.MenuItems.Add(_MenuItemUpdateToEEPNetServer);

            // flDesigner1.SetFLViewContextMenu( _flView_ContextMenu);
            flDesigner1._ContextMenu = __ContextMenu;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            String s = Application.StartupPath + "\\";
            CliUtils.LoadLoginServiceConfig(s + "FLDesigner.exe.config");

            CliUtils.fClientLang = GetClientLanguage();
            CliUtils.fClientSystem = "Win";

            bool freg = Register(false);
            fFrmLogin = new LoginForm(freg);
            fFrmLogin.StartPosition = FormStartPosition.CenterScreen;
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
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                CliUtils.fLoginUser = string.Empty;
                this.Close();
                return;
            }

            this.WindowState = FormWindowState.Maximized;
            btnSave.Enabled = false;
            string path = EEPRegistry.Server + "\\WorkFlow\\FL";
            this.saveFileDialog1.InitialDirectory = path;
            this.openFileDialog1.InitialDirectory = path;

        }

        private void ItemSyncInstance_Click(object sender, EventArgs e)
        {
            if (_fileName == null || _fileName.Length == 0)
            {
                MessageBox.Show("Please save xoml document first.");
                return;
            }

            FLDesigner.ModifyInstance instance = new FLDesigner.ModifyInstance();
            instance.FileName = _fileName;
            string flowDesc = string.Empty;
            if (flDesigner1.Flow != null && flDesigner1.Flow is FLSequentialWorkflow)
            {
                flowDesc = flDesigner1.Flow.Description;
            }
            instance.FlowDesc = flowDesc;
            instance.ShowDialog();
        }

        private void ItemUpdateToEEPNetServer_Click(object sender, EventArgs e)
        {
            if (_fileName == null || _fileName.Length == 0)
            {
                MessageBox.Show("Please save xoml document first.");
                return;
            }

            FLDesigner.UploadXoml updateToEEPNetServer = new FLDesigner.UploadXoml();
            updateToEEPNetServer.FileName = _fileName;
            updateToEEPNetServer.FLDesigner = this.flDesigner1;
            updateToEEPNetServer.ShowDialog();
        }

        private void SerializerCopyActivity()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);
            flDesigner1.SerializeActivity((Activity)_selectedActivity, writer);

            writer.Flush();
            writer.Close();

            _copyActivityString = sb.ToString();
            _copyActivity = (Activity)_selectedActivity;
        }

        private void ItemCut_Click(object sender, EventArgs e)
        {
            if (_selectedActivity != null && _selectedActivity is Activity)
            {
                SerializerCopyActivity();
                flDesigner1.DeleteActivity((Activity)_selectedActivity);
            }
        }

        private void ItemCopy_Click(object sender, EventArgs e)
        {
            if (_selectedActivity != null && _selectedActivity is Activity)
            {
                SerializerCopyActivity();
            }
        }

        private void ItemPaste_Click(object sender, EventArgs e)
        {
            if (_selectedActivity != null && _selectedActivity is Activity && _copyActivity != null && _copyActivityString != string.Empty)
            {
                StringReader reader0 = new StringReader(_copyActivityString.ToString());
                XmlReader reader = XmlReader.Create(reader0);
                Activity pasteActivity = flDesigner1.DeserializeActivity(reader);

                List<Activity> children = new List<Activity>();
                flDesigner1.GetActivityAndChildren(pasteActivity, children);
                foreach (Activity a in children)
                {
                    a.Name = null;
                }
                flDesigner1.AddActivity(pasteActivity);
            }
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            if (_selectedActivity != null && _selectedActivity is Activity)
            {
                flDesigner1.DeleteActivity((Activity)_selectedActivity);
            }
        }

        private void ItemAddIfElseBranch_Click(object sender, EventArgs e)
        {
            flDesigner1.AddActivity(new IfElseBranchActivity());
        }

        private void ItemAddSequence_Click(object sender, EventArgs e)
        {
            flDesigner1.AddActivity(new SequenceActivity());
        }

        private void ItemSaveLocation_Click(object sender, EventArgs e)
        {
            if (this.flDesigner1.WorkflowView != null && this.flDesigner1.WorkflowView is FLView)
            {
                Activity[] selActivities = this.flDesigner1.GetSelectedActivity();
                if (selActivities.Length == 1)
                {
                    Activity a = selActivities[0];
                    if (a is FLHyperLink)
                    {
                        FLHyperLink hlnk = (FLHyperLink)a;
                        string path = EEPRegistry.WebClient;
                        SaveActivityLocation("FLHyperLink", hlnk.Name, ((FLView)this.flDesigner1.WorkflowView).ClickActivityRectangle, hlnk.LinkFlow, hlnk.WebFormName, hlnk.Parameters, path);
                        path = Properties.Settings.Default.EFWebClientPath;
                        SaveActivityLocation("FLHyperLink", hlnk.Name, ((FLView)this.flDesigner1.WorkflowView).ClickActivityRectangle, hlnk.LinkFlow, hlnk.WebFormName, hlnk.Parameters, path);
                    }
                    else if (a is FLQuery)
                    {
                        FLQuery query = (FLQuery)a;
                        string path = EEPRegistry.WebClient;
                        SaveActivityLocation("FLQuery", query.Name, ((FLView)this.flDesigner1.WorkflowView).ClickActivityRectangle, "", query.WebFormName, query.Parameters, path);
                        path = Properties.Settings.Default.EFWebClientPath;
                        SaveActivityLocation("FLQuery", query.Name, ((FLView)this.flDesigner1.WorkflowView).ClickActivityRectangle, "", query.WebFormName, query.Parameters, path);
                    }
                }
            }
        }

        private void flDesinger1_ActivitySelected(object sender, ActivitySelectedEventArgs e)
        {
            _selectedActivity = e.SelectedActivity;

            if (_selectedActivity != null && _selectedActivity is Activity)
            {
                _MenuItemSyncInstance.Enabled = true;
                _MenuItemUpdateToEEPNetServer.Enabled = true;
                propertyGrid1.SelectedObject = _selectedActivity;

                FLActivityToolbox o = new FLActivityToolbox(_serviceProvider);
                splitContainer2.Panel1.Controls.Add(o);

                txtType.Text = _selectedActivity.GetType().ToString();
                txtModule.Text = _selectedActivity.GetType().Module.Name;

                if (!((Activity)_selectedActivity is SequentialWorkflowActivity))
                {
                    _MenuItemDeleteActivity.Enabled = true;
                    _MenuItemCutActivity.Enabled = true;
                    _MenuItemCopyActivity.Enabled = true;


                    if ((Activity)_selectedActivity is IfElseActivity)
                    {
                        _MenuItemAddIfElseBranch.Enabled = true;
                    }
                    else
                    {
                        _MenuItemAddIfElseBranch.Enabled = false;
                    }

                    if ((Activity)_selectedActivity is ParallelActivity)
                    {
                        _MenuItemAddSequence.Enabled = true;
                    }
                    else
                    {
                        _MenuItemAddSequence.Enabled = false;
                    }

                    if ((Activity)_selectedActivity is FLHyperLink || (Activity)_selectedActivity is FLQuery)
                    {
                        _MenuItemSaveLoacation.Enabled = true;
                    }
                    else
                    {
                        _MenuItemSaveLoacation.Enabled = false;
                    }
                }
                else
                {
                    _MenuItemDeleteActivity.Enabled = false;
                    _MenuItemCutActivity.Enabled = false;
                    _MenuItemCopyActivity.Enabled = false;
                }

                if (_copyActivityString != null && _copyActivityString != string.Empty)
                {
                    if (_selectedActivity is SequentialWorkflowActivity)
                    {
                        if (_copyActivity is IfElseBranchActivity || _copyActivity is SequenceActivity)
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                    }
                    else if (_selectedActivity is IfElseActivity)
                    {
                        if (_copyActivity is IfElseBranchActivity)
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                    }
                    else if (_selectedActivity is IfElseBranchActivity)
                    {
                        if (_copyActivity is IfElseBranchActivity)
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                    }
                    else if (_selectedActivity is ParallelActivity)
                    {
                        if (_copyActivity is SequenceActivity && !(_copyActivity is IfElseBranchActivity))
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                    }
                    else if (_selectedActivity is SequenceActivity)
                    {
                        if (_copyActivity is SequenceActivity || _copyActivity is IfElseBranchActivity)
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                    }
                    else if (_selectedActivity is IEventWaiting || _selectedActivity is INonEventWaiting)
                    {
                        if (_copyActivity is IfElseBranchActivity)
                        {
                            _MenuItemPasteActivity.Enabled = false;
                        }
                        else
                        {
                            _MenuItemPasteActivity.Enabled = true;
                        }
                    }
                    else
                    {
                        _MenuItemPasteActivity.Enabled = true;
                    }
                }
                else
                {
                    _MenuItemPasteActivity.Enabled = false;
                }
            }
            else
            {
                _MenuItemDeleteActivity.Enabled = false;
                _MenuItemCutActivity.Enabled = false;
                _MenuItemCopyActivity.Enabled = false;
                _MenuItemPasteActivity.Enabled = false;
                _MenuItemAddIfElseBranch.Enabled = false;
                _MenuItemAddSequence.Enabled = false;
                _MenuItemSaveLoacation.Enabled = false;
                _MenuItemSyncInstance.Enabled = false;
                _MenuItemUpdateToEEPNetServer.Enabled = true;
            }
        }

        private void btnNewSequntial_Click(object sender, EventArgs e)
        {
            _fileName = string.Empty;
            this.Text = "Workflow Designer - Not saved";
            flDesigner1.LoadDefaultWorkflow();
            _isFLLoaded = true;
            btnSave.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (flDesigner1.Flow == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(flDesigner1.Flow.Description))
            {
                string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "FlowDescriptionIsNull");
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_fileName == string.Empty)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
                {
                    _fileName = saveFileDialog1.FileName;
                    this.Text = "Workflow Designer - " + _fileName;
                }
                else
                {
                    return;
                }
            }

            flDesigner1.Save(_fileName);
            SaveWebFlow();
        }

        private void SaveActivityLocation(string activityType, string activityName, Rectangle activityRect, string linkFlow, string webFormName, string paramters, string Path)
        {
            // save image
            string file = _fileName.Substring(_fileName.LastIndexOf('\\') + 1, _fileName.LastIndexOf('.') - _fileName.LastIndexOf('\\'));
            string path = Path + "\\Image";
            //string path = EEPRegistry.WebClient + "\\Image";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path += "\\" + file;
            string imgName = path + "jpg";
            this.flDesigner1.WorkflowView.SaveWorkflowImage(imgName, new ImageFormat(new Guid()));

            if (this.flDesigner1.Flow.Activities.Count > 0)
            {
                //save xml
                string xmlName = path + "xml";
                XmlDocument doc = new XmlDocument();
                FileStream fStream;
                if (!File.Exists(xmlName))
                {
                    fStream = new FileStream(xmlName, FileMode.Create);
                    try
                    {
                        XmlTextWriter writer = new XmlTextWriter(fStream, new ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartElement("FlowDetails");
                        writer.WriteEndElement();
                        writer.Close();
                    }
                    finally
                    {
                        fStream.Close();
                    }
                }
                fStream = new FileStream(xmlName, FileMode.Open);
                doc.Load(fStream);
                XmlNode root = doc.SelectSingleNode("FlowDetails");
                if (root == null)
                {
                    doc.CreateElement("InfoLight");
                    doc.AppendChild(root);
                }

                if (activityType == "FLHyperLink")
                {
                    //FLHyperLink
                    XmlNode nFLHyperLink = root.SelectSingleNode("FLHyperLink");
                    if (nFLHyperLink == null)
                    {
                        nFLHyperLink = doc.CreateElement("FLHyperLink");
                        root.AppendChild(nFLHyperLink);
                    }
                    if (!string.IsNullOrEmpty(linkFlow))
                    {
                        linkFlow = linkFlow.ToUpper();
                        if (linkFlow.IndexOf(".XOML") != -1)
                        {
                            linkFlow = linkFlow.Substring(0, linkFlow.LastIndexOf(".XOML"));
                        }
                    }
                    XmlNode node = nFLHyperLink.SelectSingleNode(activityName);
                    if (node == null)
                    {
                        node = doc.CreateElement(activityName);
                        nFLHyperLink.AppendChild(node);
                        XmlAttribute attTop = doc.CreateAttribute("Top");
                        attTop.Value = activityRect.Top.ToString();
                        node.Attributes.Append(attTop);
                        XmlAttribute attBottom = doc.CreateAttribute("Bottom");
                        attBottom.Value = activityRect.Bottom.ToString();
                        node.Attributes.Append(attBottom);
                        XmlAttribute attLeft = doc.CreateAttribute("Left");
                        attLeft.Value = activityRect.Left.ToString();
                        node.Attributes.Append(attLeft);
                        XmlAttribute attRight = doc.CreateAttribute("Right");
                        attRight.Value = activityRect.Right.ToString();
                        node.Attributes.Append(attRight);
                        XmlAttribute attLinkFlow = doc.CreateAttribute("LinkFlow");
                        attLinkFlow.Value = linkFlow;
                        node.Attributes.Append(attLinkFlow);
                        XmlAttribute attWebFormName = doc.CreateAttribute("WebFormName");
                        attWebFormName.Value = webFormName;
                        node.Attributes.Append(attWebFormName);
                        XmlAttribute attParameters = doc.CreateAttribute("Parameters");
                        attParameters.Value = paramters;
                        node.Attributes.Append(attParameters);
                    }
                    else
                    {
                        node.Attributes["Top"].Value = activityRect.Top.ToString();
                        node.Attributes["Bottom"].Value = activityRect.Bottom.ToString();
                        node.Attributes["Left"].Value = activityRect.Left.ToString();
                        node.Attributes["Right"].Value = activityRect.Right.ToString();
                        if (node.Attributes["LinkFlow"] != null)
                            node.Attributes["LinkFlow"].Value = linkFlow;
                        else
                        {
                            XmlAttribute attLinkFlow = doc.CreateAttribute("LinkFlow");
                            attLinkFlow.Value = linkFlow;
                            node.Attributes.Append(attLinkFlow);
                        }
                        if (node.Attributes["WebFormName"] != null)
                            node.Attributes["WebFormName"].Value = webFormName;
                        else
                        {
                            XmlAttribute attWebFormName = doc.CreateAttribute("WebFormName");
                            attWebFormName.Value = webFormName;
                            node.Attributes.Append(attWebFormName);
                        }
                        if (node.Attributes["Parameters"] != null)
                            node.Attributes["Parameters"].Value = paramters;
                        else
                        {
                            XmlAttribute attParameters = doc.CreateAttribute("Parameters");
                            attParameters.Value = paramters;
                            node.Attributes.Append(attParameters);
                        }
                    }
                }
                else if (activityType == "FLQuery")
                {
                    //FLQuery
                    XmlNode nFLQuery = root.SelectSingleNode("FLQuery");
                    if (nFLQuery == null)
                    {
                        nFLQuery = doc.CreateElement("FLQuery");
                        root.AppendChild(nFLQuery);
                    }
                    XmlNode node = nFLQuery.SelectSingleNode(activityName);
                    if (node == null)
                    {
                        node = doc.CreateElement(activityName);
                        nFLQuery.AppendChild(node);
                        XmlAttribute attTop = doc.CreateAttribute("Top");
                        attTop.Value = activityRect.Top.ToString();
                        node.Attributes.Append(attTop);
                        XmlAttribute attBottom = doc.CreateAttribute("Bottom");
                        attBottom.Value = activityRect.Bottom.ToString();
                        node.Attributes.Append(attBottom);
                        XmlAttribute attLeft = doc.CreateAttribute("Left");
                        attLeft.Value = activityRect.Left.ToString();
                        node.Attributes.Append(attLeft);
                        XmlAttribute attRight = doc.CreateAttribute("Right");
                        attRight.Value = activityRect.Right.ToString();
                        node.Attributes.Append(attRight);
                        XmlAttribute attWebFormName = doc.CreateAttribute("WebFormName");
                        attWebFormName.Value = webFormName;
                        node.Attributes.Append(attWebFormName);
                        XmlAttribute attParameters = doc.CreateAttribute("Parameters");
                        attParameters.Value = paramters;
                        node.Attributes.Append(attParameters);
                    }
                    else
                    {
                        node.Attributes["Top"].Value = activityRect.Top.ToString();
                        node.Attributes["Bottom"].Value = activityRect.Bottom.ToString();
                        node.Attributes["Left"].Value = activityRect.Left.ToString();
                        node.Attributes["Right"].Value = activityRect.Right.ToString();
                        if (node.Attributes["WebFormName"] != null)
                            node.Attributes["WebFormName"].Value = webFormName;
                        else
                        {
                            XmlAttribute attWebFormName = doc.CreateAttribute("WebFormName");
                            attWebFormName.Value = webFormName;
                            node.Attributes.Append(attWebFormName);
                        }
                        if (node.Attributes["Parameters"] != null)
                            node.Attributes["WebFormName"].Value = webFormName;
                        else
                        {
                            XmlAttribute attParamters = doc.CreateAttribute("Parameters");
                            attParamters.Value = paramters;
                            node.Attributes.Append(attParamters);
                        }
                    }
                }

                fStream.Close();
                doc.Save(xmlName);
            }
        }

        public void SaveWebFlow2(object p)
        {
            String path = ((object[])p)[0].ToString();
            String fileName = ((object[])p)[1].ToString();
            String xomlName = ((object[])p)[2].ToString();

            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(SaveWebFlow3));//新建了一个线程
            t.ApartmentState = System.Threading.ApartmentState.STA;//加上这句话！
            t.Start(new object[] { path, fileName, xomlName });//开始线程

            //SaveWebFlow3(p);
        }

        private void SaveWebFlow3(object p)
        {
            try
            {
                String path = ((object[])p)[0].ToString();
                String fileName = ((object[])p)[1].ToString();
                String xomlName = ((object[])p)[2].ToString();
                _fileName = xomlName;
                this.AllowDrop = false;
                this.flDesigner1.AllowDrop = false;
                this.flDesigner1.LoadWorkflow(xomlName);
                SaveWebFlow(Path.Combine(path, fileName));
            }
            catch { }
        }

        private void SaveWebFlow()
        {
            string file = Path.GetFileNameWithoutExtension(_fileName);
            string eepweb = EEPRegistry.WebClient;
            string efweb = Properties.Settings.Default.EFWebClientPath;
            if (!string.IsNullOrEmpty(eepweb))
            {
                SaveWebFlow(string.Format(@"{0}\Image\{1}", eepweb, file));
            }

            if (!string.IsNullOrEmpty(efweb))
            {
                SaveWebFlow(string.Format(@"{0}\Image\{1}", efweb, file));
            }
        }

        private void SaveWebFlow(string path)
        {
            // save image
            //string file = _fileName.Substring(_fileName.LastIndexOf('\\') + 1, _fileName.LastIndexOf('.') - _fileName.LastIndexOf('\\'));
            //string path = EEPRegistry.WebClient + "\\Image";
            //if (!Directory.Exists(path))
            //{
            //    Directory.CreateDirectory(path);
            //}
            //path += "\\" + file;
            string imgName = path + ".jpg";
            this.flDesigner1.WorkflowView.SaveWorkflowImage(imgName, new ImageFormat(new Guid()));
            Bitmap bt1 = new Bitmap(imgName);
            Bitmap bt2 = new Bitmap(bt1.Width - 56, bt1.Height - 56);
            for (int x = 29; x <= bt1.Width - 28; x++)
            {
                for (int y = 29; y <= bt1.Height - 28; y++)
                {
                    bt2.SetPixel(x - 29, y - 29, bt1.GetPixel(x, y));
                }
            }
            bt1.Dispose();
            bt2.Save(imgName);

            if (this.flDesigner1.Flow.Activities.Count > 0)
            {
                Activity startStand = this.flDesigner1.Flow.Activities[0];
                //save xml
                string xmlName = path + ".xml";
                XmlDocument doc = new XmlDocument();
                FileStream fStream;
                if (!File.Exists(xmlName))
                {
                    fStream = new FileStream(xmlName, FileMode.Create);
                    try
                    {
                        XmlTextWriter writer = new XmlTextWriter(fStream, new ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartElement("FlowDetails");
                        writer.WriteEndElement();
                        writer.Close();
                    }
                    finally
                    {
                        fStream.Close();
                    }
                }
                fStream = new FileStream(xmlName, FileMode.Open);
                doc.Load(fStream);
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
                    if (!(this.flDesigner1.Flow.Activities[0] is FLHyperLink) && !(this.flDesigner1.Flow.Activities[0] is FLQuery))
                    {
                        string webFormName = ((FLStand)startStand).WebFormName;
                        string gloFormName = ((IFLRootActivity)this.flDesigner1.Flow).WebFormName;
                        string formName = string.IsNullOrEmpty(webFormName) ? gloFormName : webFormName;
                        if (path.IndexOf("jqwebclient", StringComparison.OrdinalIgnoreCase) < 0)
                        {
                            if (formName.StartsWith("web.", StringComparison.OrdinalIgnoreCase) && formName.Split('.').Length > 2)
                            {
                                formName = formName.Substring(4);
                            }
                        }
                        nFormName.InnerText = formName;
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
                nFlowFileName.InnerText = _fileName.Substring(0, _fileName.ToUpper().IndexOf(".XOML"));

                //ImageWidth
                XmlNode nImageWidth = root.SelectSingleNode("ImageWidth");
                if (nImageWidth == null)
                {
                    nImageWidth = doc.CreateElement("ImageWidth");
                    root.AppendChild(nImageWidth);
                }
                nImageWidth.InnerText = bt2.Width.ToString();

                //Deleted activities
                XmlNode nlnk = root.SelectSingleNode("FLHyperLink");
                if (nlnk != null)
                {
                    foreach (XmlNode node in nlnk.ChildNodes)
                    {
                        bool nodeExist = false;
                        foreach (Activity a in this.flDesigner1.Flow.Activities)
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

                if (startStand is FLHyperLink)
                {
                    XmlNode nFLHyperLink = root.SelectSingleNode("FLHyperLink");
                    if (nFLHyperLink == null)
                    {
                        nFLHyperLink = doc.CreateElement("FLHyperLink");
                        root.AppendChild(nFLHyperLink);
                    }

                    XmlNode nFirstChild = nFLHyperLink.SelectSingleNode("FirstChild");
                    if (nFirstChild == null)
                    {
                        nFirstChild = doc.CreateElement("FirstChild");
                        nFLHyperLink.AppendChild(nFirstChild);
                    }

                    XmlAttribute aWebFormName = doc.CreateAttribute("WebFormName");
                    aWebFormName.Value = (startStand as FLHyperLink).WebFormName;
                    nFirstChild.Attributes.Append(aWebFormName);

                    XmlAttribute aLinkFlow = doc.CreateAttribute("LinkFlow");
                    aLinkFlow.Value = (startStand as FLHyperLink).LinkFlow;
                    nFirstChild.Attributes.Append(aLinkFlow);

                    XmlAttribute aParameters = doc.CreateAttribute("Parameters");
                    aParameters.Value = (startStand as FLHyperLink).Parameters;
                    nFirstChild.Attributes.Append(aParameters);

                    ////WebFormName
                    //XmlNode nWebFormName = nFLHyperLink.SelectSingleNode("WebFormName");
                    //if (nWebFormName == null)
                    //{
                    //    nWebFormName = doc.CreateElement("WebFormName");
                    //    nFLHyperLink.AppendChild(nWebFormName);
                    //}
                    //nWebFormName.InnerText = (startStand as FLHyperLink).WebFormName;

                    ////LinkFlow
                    //XmlNode nLinkFlow = nFLHyperLink.SelectSingleNode("LinkFlow");
                    //if (nLinkFlow == null)
                    //{
                    //    nLinkFlow = doc.CreateElement("LinkFlow");
                    //    nFLHyperLink.AppendChild(nLinkFlow);
                    //}
                    //nLinkFlow.InnerText = (startStand as FLHyperLink).LinkFlow;

                    ////Parameters
                    //XmlNode nParameters = nFLHyperLink.SelectSingleNode("Parameters");
                    //if (nParameters == null)
                    //{
                    //    nParameters = doc.CreateElement("Parameters");
                    //    nFLHyperLink.AppendChild(nParameters);
                    //}
                    //nParameters.InnerText = (startStand as FLHyperLink).Parameters;
                }

                XmlNode nquery = root.SelectSingleNode("FLQuery");
                if (nquery != null)
                {
                    foreach (XmlNode node in nquery.ChildNodes)
                    {
                        bool nodeExist = false;
                        foreach (Activity a in this.flDesigner1.Flow.Activities)
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

                fStream.Close();
                doc.Save(xmlName);
            }
        }


        private List<FLHyperLink> getAllFLHyperLink()
        {
            List<FLHyperLink> hlnks = new List<FLHyperLink>();
            foreach (Activity a in this.flDesigner1.Flow.Activities)
            {
                if (a is FLHyperLink)
                {
                    hlnks.Add(a as FLHyperLink);
                }
            }
            return hlnks;
        }

        private List<FLQuery> getAllFLQuery()
        {
            List<FLQuery> queries = new List<FLQuery>();
            foreach (Activity a in this.flDesigner1.Flow.Activities)
            {
                if (a is FLQuery)
                {
                    queries.Add(a as FLQuery);
                }
            }
            return queries;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (openFileDialog1.ShowDialog() == DialogResult.OK && openFileDialog1.FileName != string.Empty)
            {
                _fileName = openFileDialog1.FileName;
                this.Text = "Workflow Designer - " + _fileName;
                flDesigner1.LoadWorkflow(_fileName);
                _isFLLoaded = true;
                btnSave.Enabled = true;
            }
        }

        private void menuNew_Click(object sender, EventArgs e)
        {
            btnNewSequntial_Click(null, null);
        }

        private void menuOpen_Click(object sender, EventArgs e)
        {
            btnOpen_Click(null, null);
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            btnSave_Click(null, null);
        }

        private void menuSaveAs_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK && saveFileDialog1.FileName != string.Empty)
            {
                _fileName = saveFileDialog1.FileName;
                this.Text = "Workflow Designer - " + _fileName;
                flDesigner1.Save(_fileName);
            }
        }

        private void menuFile_Click(object sender, EventArgs e)
        {
            if (_isFLLoaded)
            {
                menuSaveAs.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                menuSaveAs.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void menuSave_Paint(object sender, PaintEventArgs e)
        {
            if (_isFLLoaded)
            {
                menuSave.Enabled = true;
            }
            else
            {
                menuSave.Enabled = false;
            }
        }

        private void menuSaveAs_Paint(object sender, PaintEventArgs e)
        {
            if (_isFLLoaded)
            {
                menuSaveAs.Enabled = true;
            }
            else
            {
                menuSaveAs.Enabled = false;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVersion form = new FormVersion("About FLDesigner");
            form.ShowDialog(this);

            //Form aboutForm = new AboutForm();
            //aboutForm.ShowDialog();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flDesigner1__KeyDown(object sender, _KeyDownEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //    Activity[] alist = this.flDesigner1.GetSelectedActivity();
            //    foreach (Activity a in alist)
            //    {
            //        MessageBox.Show(a.Name);
            //    }
            //}

        }

        private void flDesigner1__KeyPress(object sender, _KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(3))  // ''
            {
                ItemCopy_Click(null, null);
            }
            else if (e.KeyChar == Convert.ToChar(24))  // ''
            {
                ItemCut_Click(null, null);
            }
            else if (e.KeyChar == Convert.ToChar(22))  // ''
            {
                ItemPaste_Click(null, null);
            }
        }

        private void flDesigner1_ActivityDeleted(object sender, ActivityDeletedEventArgs e)
        {
            string deletedActivity = e.DeletedActivity;

            if (!string.IsNullOrEmpty(deletedActivity))
            {
                string file = _fileName.Substring(_fileName.LastIndexOf('\\') + 1, _fileName.LastIndexOf('.') - _fileName.LastIndexOf('\\'));
                string xmlName = EEPRegistry.WebClient + "\\Image\\" + file + "xml";

                XmlDocument doc = new XmlDocument();
                FileStream fStream;
                if (!File.Exists(xmlName))
                {
                    fStream = new FileStream(xmlName, FileMode.Create);
                    try
                    {
                        XmlTextWriter writer = new XmlTextWriter(fStream, new ASCIIEncoding());
                        writer.Formatting = Formatting.Indented;
                        writer.WriteStartElement("FlowDetails");
                        writer.WriteEndElement();
                        writer.Close();
                    }
                    finally
                    {
                        fStream.Close();
                    }
                }
                fStream = new FileStream(xmlName, FileMode.Open);
                doc.Load(fStream);
                XmlNode nQueryDeleted = doc.SelectSingleNode("FlowDetails/FLQuery/" + deletedActivity);
                XmlNode nLinkDeleted = doc.SelectSingleNode("FlowDetails/FLHyperLink" + deletedActivity);
                if (nQueryDeleted != null)
                {
                    nQueryDeleted.ParentNode.RemoveChild(nQueryDeleted);
                }
                else if (nLinkDeleted != null)
                {
                    nLinkDeleted.ParentNode.RemoveChild(nLinkDeleted);
                }

                fStream.Close();
                doc.Save(xmlName);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CliUtils.fLoginUser.Length > 0)
            {
                if (!CliUtils.closeProtected)
                    CliUtils.CallMethod("GLModule", "LogOut", new object[] { (object)(CliUtils.fLoginUser) });
            }
        }

        private void eFWebClientPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormInitial form = new FormInitial();
            form.ShowDialog();
        }
    }
}