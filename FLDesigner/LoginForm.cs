using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.IO;
using Srvtools;
using Microsoft.Win32;

namespace FLDesigner
{
	/// <summary>
	/// Summary description for WinForm1.
	/// </summary>
	public class LoginForm : System.Windows.Forms.Form
    {
        private Panel panel1;
        private ComboBox edtSol;
        private ComboBox edtDB;
        private ComboBox edtUserId;
        private Label label3;
        private Label lblDB;
        private TextBox edtPwd;
        private Button button2;
        private Button button1;
        private Label label2;
        private Label label1;
        private PictureBox pictureBox1;

		public string GetUserId()
		{
			return edtUserId.Text.Trim();
        }
		public string GetPwd()
		{
            return edtPwd.Text.Trim();
		}
		public string GetDB()
		{
            return edtDB.Text.Trim();
		}
        public string GetCurProject()
        {
            return edtSol.Text.Trim();
        }
        private bool register = false;
		public LoginForm(bool reg)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            register = reg;
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose (bool disposing)
		{
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.edtSol = new System.Windows.Forms.ComboBox();
            this.edtDB = new System.Windows.Forms.ComboBox();
            this.edtUserId = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblDB = new System.Windows.Forms.Label();
            this.edtPwd = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(402, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.edtSol);
            this.panel1.Controls.Add(this.edtDB);
            this.panel1.Controls.Add(this.edtUserId);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.lblDB);
            this.panel1.Controls.Add(this.edtPwd);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 178);
            this.panel1.TabIndex = 16;
            // 
            // edtSol
            // 
            this.edtSol.FormattingEnabled = true;
            this.edtSol.Location = new System.Drawing.Point(155, 106);
            this.edtSol.Name = "edtSol";
            this.edtSol.Size = new System.Drawing.Size(185, 21);
            this.edtSol.TabIndex = 22;
            // 
            // edtDB
            // 
            this.edtDB.FormattingEnabled = true;
            this.edtDB.Location = new System.Drawing.Point(155, 78);
            this.edtDB.Name = "edtDB";
            this.edtDB.Size = new System.Drawing.Size(185, 21);
            this.edtDB.TabIndex = 21;
            this.edtDB.SelectedIndexChanged += new System.EventHandler(this.edtDB_SelectedIndexChanged);
            // 
            // edtUserId
            // 
            this.edtUserId.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.edtUserId.FormattingEnabled = true;
            this.edtUserId.Location = new System.Drawing.Point(155, 24);
            this.edtUserId.Name = "edtUserId";
            this.edtUserId.Size = new System.Drawing.Size(185, 21);
            this.edtUserId.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 20);
            this.label3.TabIndex = 19;
            this.label3.Text = "Solution";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDB
            // 
            this.lblDB.BackColor = System.Drawing.Color.Transparent;
            this.lblDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDB.Location = new System.Drawing.Point(51, 78);
            this.lblDB.Name = "lblDB";
            this.lblDB.Size = new System.Drawing.Size(88, 20);
            this.lblDB.TabIndex = 16;
            this.lblDB.Text = "DataBase";
            this.lblDB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // edtPwd
            // 
            this.edtPwd.Location = new System.Drawing.Point(155, 50);
            this.edtPwd.Name = "edtPwd";
            this.edtPwd.PasswordChar = '*';
            this.edtPwd.Size = new System.Drawing.Size(185, 20);
            this.edtPwd.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(269, 133);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(71, 27);
            this.button2.TabIndex = 18;
            this.button2.Text = "Cancel";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(192, 133);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 27);
            this.button1.TabIndex = 17;
            this.button1.Text = "OK";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 22);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "User";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LoginForm
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(402, 266);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.WinForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        private void WinForm1_Load(object sender, System.EventArgs e)
        {
            if (register)
            {
                string[] caption = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "txt_Login").Split(';');
                label1.Text = caption[0];
                label2.Text = caption[1];
                label3.Text = caption[3];
                lblDB.Text = caption[2];
            }
            String s = Application.StartupPath + "\\FLDesigner.xml";
            string sUser = "";
            string sDB = "";
            string sSol = "";
            if (File.Exists(s))
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(s);
                XmlNode el = xml.DocumentElement;
                foreach (XmlNode xNode in el.ChildNodes)
                {

                    if (xNode.Name.ToUpper().Equals("USER"))
                    {
                        sUser = xNode.InnerText.Trim();
                    }
                    else if (xNode.Name.ToUpper().Equals("DATABASE"))
                    {
                        sDB = xNode.InnerText.Trim();
                    }
                    else if (xNode.Name.ToUpper().Equals("SOLUTION"))
                    {
                        sSol = xNode.InnerText.Trim();
                    }
                }
            }

            //Modified by lily 2006/5/16 add another condition(edt.Items.Count==0) for if error login items will be add many times.
            if (edtUserId.Items.Count == 0)
            {
                if (sUser != "")
                {
                    string[] sUsers = sUser.Split(new char[] { ',' });
                    edtUserId.Items.AddRange(sUsers);
                    edtUserId.SelectedIndex = 0;
                }
                else
                {
                 //   edtUserId.Text = "001";
                }
            }

            if (register)
            {
                if (edtDB.Items.Count == 0)
                {
                    object[] myRet1 = CliUtils.CallMethod("GLModule", "GetDB", null);
                    if (myRet1[1] != null && myRet1[1] is ArrayList)
                    {
                        ArrayList dbList = (ArrayList)myRet1[1];
                        foreach (string db in dbList)
                        {
                            this.edtDB.Items.Add(db);
                        }
                    }
                    string[] sDBs = sDB.Split(',');
                    if (sDBs[0] != "" && this.edtDB.Items.Contains(sDBs[0]))
                    {
                        this.edtDB.SelectedItem = sDBs[0];
                    }
                    else if (edtDB.Items.Count > 0)
                    {
                        edtDB.SelectedIndex = 0;
                    }

                    //if (sDB != "")
                    //{
                    //    string[] sDBs = sDB.Split(new char[] { ',' });
                    //    edtDB.Items.AddRange(sDBs);
                    //    edtDB.SelectedIndex = 0;
                    //}
                    //else
                    //    edtDB.Text = "ERPS";
                }
                if (edtDB.SelectedItem != null)
                {
                    loadSysSolutions();
                    string[] sSols = sSol.Split(',');
                    if (sSols[0] != "" && this.edtSol.Items.Contains(sSols[0]))
                    {
                        this.edtSol.SelectedItem = sSols[0];
                    }
                    else if (edtSol.Items.Count > 0)
                    {
                        edtSol.SelectedIndex = 0;
                    }
                }
            }
            //Modified by lily 2006/5/16 add another condition(edt.Items.Count==0) for if error login items will be add many times.
            

            ////new add by ccm
            //List<string> dba = new List<string>();
            //List<string> dbs = new List<string>();
            //string sysdb = "";
            //object[] myRet = CliUtils.CallMethod("GLModule", "GetDB", new object[] { });
            //if (myRet != null && (int)myRet[0] == 0)
            //{
            //    dba = (List<string>)myRet[1];
            //    dbs = (List<string>)myRet[2];
            //    sysdb = (string)myRet[3];
            //    for (int i = 0; i < dba.Count; i++)
            //    {
            //        edtDB.Items.Add((object)dba[i]);
            //    }
            //}
            //edtDB.Text = edtDB.Items[0].ToString();

            //if (dbs[0].Equals("0"))
            //{ 
            //    CliUtils.fLoginDB = dba[0].Trim();
            //}
            //else
            //{
            //    CliUtils.fLoginDB = sysdb.Trim();
            //}
            //DataSet slnDs = new DataSet();
            //myRet = CliUtils.CallMethod("GLModule", "GetSolution", new object[] { });
            //if (myRet != null && (int)myRet[0] == 0)
            //{
            //    slnDs = (DataSet)myRet[1];
            //    int slncount = slnDs.Tables[0].Rows.Count;
            //    for (int i = 0; i < slncount; i++)
            //    {
            //        edtSol.Items.Add((object)slnDs.Tables[0].Rows[i]["itemtype"]);
                
            //    }
            //}
        }

        private void loadSysSolutions()
        {
            edtSol.Items.Clear();
            object[] objParam = new object[1];
            objParam[0] = this.edtDB.SelectedItem.ToString();
            DataSet dsSolution = new DataSet();
            object[] myRet1 = CliUtils.CallMethod("GLModule", "GetSolution", objParam);
            if ((null != myRet1) && (0 == (int)myRet1[0]))
                dsSolution = ((DataSet)myRet1[1]);
            for (int i = 0; i < dsSolution.Tables[0].Rows.Count; i++)
            {
                edtSol.Items.Add(dsSolution.Tables[0].Rows[i]["itemtype"].ToString());
            }
        }

        private void edtDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadSysSolutions();
            //if (edtSol.Items.Count > 0)
            //{
            //    edtSol.SelectedIndex = 0;
            //}
        }

    }
}
