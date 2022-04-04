using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class UploadFilesForm : Form
    {
        public UploadFilesForm()
        {
            InitializeComponent();
            this.dialogFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //this.dialogFile.InitialDirectory = @"C:\Documents and Settings\taoweijia\桌面\wfupload";
        }

        Dictionary<string, string> addedFiles = new Dictionary<string, string>();
        internal List<string> attachments = new List<string>();

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.dialogFile.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < this.dialogFile.FileNames.Length; i++)
                {
                    string safeFileName = this.dialogFile.FileNames[i].Substring(this.dialogFile.FileNames[i].LastIndexOf('\\') + 1);
                    addedFiles.Add(this.dialogFile.FileNames[i], safeFileName);
                    this.lstUploadFiles.Items.Add(safeFileName);
                }
            }
        }

        string getSrvPath()
        {
            string xpath = "{0}appSettings/{0}add[@key='FlowFilesBySolutions']";
            object[] oConfig = CliUtils.CallMethod("GLModule", "GetWebSiteConfig", new object[] { xpath });
            if (oConfig != null && (int)oConfig[0] == 0)
            {
                string srvPath = oConfig[1].ToString();
                if ((bool)oConfig[2])
                {
                    srvPath = string.Format(@"{0}{1}\", oConfig[1].ToString(), CliUtils.fCurrentProject);
                }
                return srvPath;
            }
            return "";
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            string srvPath = getSrvPath();
            srvPath = System.IO.Path.Combine(srvPath, "WorkflowFiles") + @"\";
            if (!string.IsNullOrEmpty(srvPath))
            {
                foreach (KeyValuePair<string, string> addfile in addedFiles)
                {
                    this.UpLoad(addfile.Key, srvPath + addfile.Value);
                }
            }
        }

        private void UpLoad(string clientFile, string serverFile)
        {
            if (File.Exists(clientFile))
            {
                byte[] bfile = File.ReadAllBytes(clientFile);
                object[] oUpload = CliUtils.CallMethod("GLModule", "UpLoadWorkflowFile", new object[] { serverFile, bfile });
                if (oUpload != null && (int)oUpload[0] == 0)
                {
                    attachments.Add(oUpload[1].ToString());
                }
            }
        }

        private void UploadFilesForm_Load(object sender, EventArgs e)
        {
            SetLanguage(CliUtils.fClientLang);
            if (this.attachments.Count > 0)
            {
                int y = 20;
                for (int i = 0; i < this.attachments.Count; i++)
                {
                    CheckBox chk = new CheckBox();
                    chk.Text = this.attachments[i];
                    chk.Width = 185;
                    if (i % 2 == 0)
                    {
                        chk.Location = new Point(5, y);
                    }
                    else
                    {
                        chk.Location = new Point(195, y);
                        y += 25;
                    }
                    this.gbExistFiles.Controls.Add(chk);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string srvPath = getSrvPath();
            if (!string.IsNullOrEmpty(srvPath))
            {
                foreach (Control ctrl in this.gbExistFiles.Controls)
                {
                    if (ctrl is CheckBox)
                    {
                        CheckBox chk = (CheckBox)ctrl;
                        if (chk.Checked)
                        {
                            object[] oDelAttach = CliUtils.CallMethod("GLModule", "DeleteWorkFlowAttachFile", new object[] { chk.Text, CliUtils.fCurrentProject });
                            if (oDelAttach != null && (int)oDelAttach[0] == 0)
                            {
                                this.attachments.Remove(chk.Text);
                            }
                        }
                    }
                }
            }
        }

        private void SetLanguage(SYS_LANGUAGE language)
        {
            string[] UITexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "UploadFilesForm", "UIText").Split(',');
            this.groupBox1.Text = UITexts[0];
            this.btnSelect.Text = UITexts[1];
            this.btnUpload.Text = UITexts[2];
            this.gbExistFiles.Text = UITexts[3];
            this.btnDelete.Text = UITexts[4];
        }
    }
}
