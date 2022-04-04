using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;

namespace FLTools
{
    public partial class Comment : Form
    {
        public Comment()
        {
            InitializeComponent();
            this.dialogSave.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            //this.dialogSave.InitialDirectory = @"C:\Documents and Settings\taoweijia\×ÀÃæ\wfdownload";

        }

        internal string listId = "";
        string[] lstStatus = null;
        internal List<string> attachments = new List<string>();

        private void Comment_Load(object sender, EventArgs e)
        {
            lstStatus = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item3").Split(',');
            SetLanguage(CliUtils.fClientLang);
            string sql = "SELECT S_STEP_ID,USER_ID,USERNAME,STATUS,UPDATE_DATE,UPDATE_TIME,REMARK FROM SYS_TODOHIS Where (LISTID = '" + listId + "') ORDER BY UPDATE_DATE,UPDATE_TIME";
            DataTable tabHis = null;

            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { sql });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                tabHis = ((DataSet)ret1[1]).Tables[0];
            }
            this.dgvHis.AutoGenerateColumns = false;
            this.dgvHis.DataSource = tabHis;
            Binding binding = new Binding("Text", tabHis, "REMARK");
            this.txtRemark.DataBindings.Add(binding);

            if (this.attachments.Count > 0)
            {
                this.groupBox1.Controls.Clear();
                int y = 15;
                for (int i = 0; i < this.attachments.Count; i++)
                {
                    LinkLabel lbl = new LinkLabel();
                    lbl.Text = this.attachments[i];
                    lbl.Width = 200;
                    lbl.Height = 15;
                    if (i % 3 == 0)
                    {
                        lbl.Location = new Point(10, y);
                    }
                    else if (i % 3 == 1)
                    {
                        lbl.Location = new Point(240, y);
                    }
                    else
                    {
                        lbl.Location = new Point(470, y);
                        y += 15;
                    }
                    lbl.LinkClicked += new LinkLabelLinkClickedEventHandler(lbl_LinkClicked);
                    this.groupBox1.Controls.Add(lbl);
                }
            }
        }

        void lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lbl = sender as LinkLabel;
            this.dialogSave.FileName = lbl.Text;
            if (this.dialogSave.ShowDialog() == DialogResult.OK)
            {
                string xpath = "{0}appSettings/{0}add[@key='FlowFilesBySolutions']";
                object[] oConfig = CliUtils.CallMethod("GLModule", "GetWebSiteConfig", new object[] { xpath });
                if (oConfig != null && (int)oConfig[0] == 0)
                {
                    string srv = string.Format(@"{0}WorkflowFiles\{1}", oConfig[1].ToString(), lbl.Text); // omit workflowfiles since added in server part
                    if ((bool)oConfig[2])
                    {
                        srv = string.Format(@"{0}WorkflowFiles\{1}\{2}", oConfig[1].ToString(), CliUtils.fCurrentProject, lbl.Text);
                    }

                    string fileName = dialogSave.FileName;//string.Format(@"{0}\Temp\{2}", Application.StartupPath, lbl.Text);
                    CliUtils.DownLoad(srv, fileName);
                    System.Diagnostics.Process.Start(fileName);

                    //string clt = this.dialogSave.FileName;
                    //CliUtils.DownLoad(srv, clt);
                }
            }
        }

        private void SetLanguage(SYS_LANGUAGE language)
        {
            string[] UIHisTexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "SubmitConfirm", "HisUIText").Split(',');
            this.columnUserId.HeaderText = UIHisTexts[0];
            this.columnUserName.HeaderText = UIHisTexts[1];
            this.columnUpdateDate.HeaderText = UIHisTexts[2];
            this.columnUpdateTime.HeaderText = UIHisTexts[3];
            this.columnStepId.HeaderText = UIHisTexts[5];
            this.label1.Text = UIHisTexts[4];
            this.columnStatus.HeaderText = UIHisTexts[6];
            string[] UITexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLClientControls", "Comment", "UIText").Split(',');
            this.groupBox1.Text = UITexts[0];
        }

        private void dgvHis_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                Rectangle newRect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
                using (Brush gridBrush = new SolidBrush(dgvHis.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor), selectionBackColorBrush = new SolidBrush(e.CellStyle.SelectionBackColor))
                {
                    using (Pen gridLinePen = new Pen(gridBrush))
                    {
                        // Erase the cell.
                        if (dgvHis.Rows[e.RowIndex].Selected)
                        {
                            e.Graphics.FillRectangle(selectionBackColorBrush, e.CellBounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                        }

                        // Draw the grid lines (only the right and bottom lines;
                        // DataGridView takes care of the others).
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                        e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                    }
                }
                using (Brush foreColorBrush = new SolidBrush(e.CellStyle.ForeColor), selectionForeColorBrush = new SolidBrush(e.CellStyle.SelectionForeColor))
                {
                    string formatValue = "";

                    switch (e.Value.ToString())
                    {
                        case "Z":
                            formatValue = lstStatus[0];
                            break;
                        case "N":
                            formatValue = lstStatus[1];
                            break;
                        case "NR":
                            formatValue = lstStatus[2];
                            break;
                        case "NF":
                            formatValue = lstStatus[3];
                            break;
                        case "X":
                            formatValue = lstStatus[4];
                            break;
                        case "A":
                            formatValue = lstStatus[5];
                            break;
                    }

                    if (dgvHis.Rows[e.RowIndex].Selected)
                    {
                        e.Graphics.DrawString(formatValue, e.CellStyle.Font, selectionForeColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                    }
                    else
                    {
                        e.Graphics.DrawString(formatValue, e.CellStyle.Font, foreColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                    }
                    e.Handled = true;
                }
            }
        }
    }
}