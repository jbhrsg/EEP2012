using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;
using FLCore.Base;
using System.Data.SqlClient;
using System.Data.Odbc;
using System.Data.OracleClient;
using System.Data.OleDb;
using System.Xml;
using Microsoft.Win32;
using FLRuntime;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using FLTools.ComponentModel;
using FLCore;
using System.Runtime.Remoting;
using System.Runtime.InteropServices;

namespace FLDesigner
{
    public partial class ModifyInstance : Form
    {
        private const string _sql = "LISTID='{0}'";
        private const string GET_FLINSTANCESTATE = "SELECT * FROM SYS_FLINSTANCESTATE WHERE FLINSTANCEID='{0}'";
        private const string GET_FLINSTANCES = "SELECT SYS_TODOLIST.*,(UPDATE_DATE {1} ' ' {1} UPDATE_TIME) DateTime FROM SYS_TODOLIST WHERE FLOW_DESC LIKE '%{0}%' AND STATUS <> 'F' ORDER BY FORM_PRESENTATION";
        private const string GET_FLINSTANCE_DESC = "SELECT DISTINCT(FLOW_DESC) FROM SYS_TODOLIST";

        public ModifyInstance()
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

        private string _flowDesc;
        public string FlowDesc
        {
            get
            {
                return _flowDesc;
            }
            set
            {
                _flowDesc = value;
            }
        }

        private void Instance_Load(object sender, EventArgs e)
        {
            gvInstances.AutoGenerateColumns = false;

            string project = CliUtils.fCurrentProject;
            DataSet dataSet = CliUtils.ExecuteSql("GLModule", "cmdWorkflow", GET_FLINSTANCE_DESC, true, project);
            if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0] != null)
            {
                DataTable table = dataSet.Tables[0];
                bool b = false;
                List<object> list = new List<object>();
                foreach (DataRow row in table.Rows)
                {
                    object v = row["FLOW_DESC"];
                    list.Add(v);
                    if (!b && v != null && v != DBNull.Value && v.ToString() == _flowDesc)
                        b = true;
                }

                if (b)
                    cmbDescription.SelectedText = _flowDesc;
            }

            txtXomlFile.Text = _fileName;

            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item1");
            string[] items = message.Split(",".ToCharArray());
            int i = 1;
            foreach (string item in items)
            {
                if (gvInstances.Columns.Count < i)
                    break;

                gvInstances.Columns[i].HeaderText = item;
                i++;
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            foreach (DataGridViewRow row in gvInstances.Rows)
            {
                if (row.Cells["Modify"].Value != null && Convert.ToBoolean(row.Cells["Modify"].Value) && row.Cells["LISTID"] != null && row.Cells["LISTID"].Value != DBNull.Value)
                {
                    string id = row.Cells["LISTID"].Value.ToString();
                    Guid flInstanceId = new Guid(id);
                    string flPath = row.Cells["FLOWPATH"].Value.ToString();

                    object[] objs = CliUtils.CallFLMethod("ModifyFLDefinition", new object[] { flInstanceId, new object[] { txtXomlFile.Text } });
                    if (objs[0].ToString() != "0")
                    {
                        string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                        s += presentation + " modify fail.\r\n";
                    }
                }
            }

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Modify succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            foreach (DataGridViewRow row in gvInstances.Rows)
            {
                if (row.Cells["Modify"].Value != null && Convert.ToBoolean(row.Cells["Modify"].Value) && row.Cells["LISTID"] != null && row.Cells["LISTID"].Value != DBNull.Value)
                {
                    string id = row.Cells["LISTID"].Value.ToString();
                    Guid flInstanceId = new Guid(id);
                    string flPath = row.Cells["FLOWPATH"].Value.ToString();

                    object[] objs = CliUtils.CallFLMethod("Approve3", new object[] { flInstanceId, flPath });
                    if (objs[0].ToString() != "0")
                    {
                        string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                        s += presentation + " forward fail.\r\n";
                    }
                }
            }

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Forward succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            foreach (DataGridViewRow row in gvInstances.Rows)
            {
                if (row.Cells["Modify"].Value != null && Convert.ToBoolean(row.Cells["Modify"].Value) && row.Cells["LISTID"] != null && row.Cells["LISTID"].Value != DBNull.Value)
                {
                    string id = row.Cells["LISTID"].Value.ToString();
                    Guid flInstanceId = new Guid(id);
                    string flPath = row.Cells["FLOWPATH"].Value.ToString();

                    object[] objs = CliUtils.CallFLMethod("Return3", new object[] { flInstanceId, flPath });
                    if (objs[0].ToString() != "0")
                    {
                        string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                        s += presentation + " back fail.\r\n";
                    }
                }
            }           

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Back succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            foreach (DataGridViewRow row in gvInstances.Rows)
            {
                if (row.Cells["Modify"].Value != null && Convert.ToBoolean(row.Cells["Modify"].Value) && row.Cells["LISTID"] != null && row.Cells["LISTID"].Value != DBNull.Value)
                {
                    string id = row.Cells["LISTID"].Value.ToString();
                    Guid flInstanceId = new Guid(id);

                    if (row.Cells["STATUS"].Value != null && row.Cells["STATUS"].Value != DBNull.Value && row.Cells["STATUS"].Value.ToString() == "F")
                    {
                        string flPath = row.Cells["FLOWPATH"].Value.ToString();
                        object[] objs = CliUtils.CallFLMethod("DeleteNotify", new object[] { flInstanceId, flPath });
                        if (objs[0].ToString() != "0")
                        {
                            string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                            s += presentation + " reject fail.\r\n";
                        }
                    }
                    else
                    {
                        string keys = row.Cells["FORM_KEYS"].Value.ToString();
                        string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                        string flPath = row.Cells["FLOWPATH"].Value.ToString();


                        object[] objs = CliUtils.CallFLMethod("Reject2", new object[] { flInstanceId, flPath, new object[] { keys, presentation } });
                        if (objs[0].ToString() != "0")
                        {
                            s += presentation + " reject fail.\r\n";
                        }
                    }
                }
            }

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Reject succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private DataTable _dataTable;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ClientType type = CliUtils.GetDataBaseType();
            string connMark = (type == ClientType.ctMsSql || type == ClientType.ctOleDB) ? "+" : "||";

            string project = CliUtils.fCurrentProject;
            DataSet dataSet = CliUtils.ExecuteSql("GLModule", "cmdWorkflow", string.Format(GET_FLINSTANCES, cmbDescription.Text, connMark), true, project);

            _dataTable = dataSet.Tables[0];
            gvInstances.DataSource = _dataTable;
        }

        private void menuItemComment_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = gvInstances.CurrentRow;
            if (row == null)
                return;

            string id = row.Cells["LISTID"].Value.ToString();

            CommentForm commentForm = new CommentForm(id);
            commentForm.ShowDialog();
        }

        private void menuItemForward_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            DataGridViewRow row = gvInstances.CurrentRow;
            if (row == null)
                return;

            string id = row.Cells["LISTID"].Value.ToString();
            Guid flInstanceId = new Guid(id);
            string flPath = row.Cells["FLOWPATH"].Value.ToString();

            object[] objs = CliUtils.CallFLMethod("Approve3", new object[] { flInstanceId, flPath });
            if (objs[0].ToString() != "0")
            {
                string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                s += presentation + " forward fail.\r\n";
            }

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Forward succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private void menuItemBack_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            DataGridViewRow row = gvInstances.CurrentRow;
            if (row == null)
                return;

            string id = row.Cells["LISTID"].Value.ToString();
            Guid flInstanceId = new Guid(id);
            string flPath = row.Cells["FLOWPATH"].Value.ToString();
            object[] objs = CliUtils.CallFLMethod("Return3", new object[] { flInstanceId, flPath });
            if (objs[0].ToString() != "0")
            {
                string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                s += presentation + " back fail.\r\n";
            }

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Back succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }

        private void menuItemReject_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            DataGridViewRow row = gvInstances.CurrentRow;
            if (row == null)
                return;

            string id = row.Cells["LISTID"].Value.ToString();
            Guid flInstanceId = new Guid(id);

            if (row.Cells["STATUS"].Value != null && row.Cells["STATUS"].Value != DBNull.Value && row.Cells["STATUS"].Value.ToString() == "F")
            {
                string flPath = row.Cells["FLOWPATH"].Value.ToString();
                object[] objs = CliUtils.CallFLMethod("DeleteNotify", new object[] { flInstanceId, flPath });
                if (objs[0].ToString() != "0")
                {
                    string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
                    s += presentation + " reject fail.\r\n";
                }
            }
            else
            {
                string keys = row.Cells["FORM_KEYS"].Value.ToString();
                string presentation = row.Cells["FORM_PRESENTATION"].Value.ToString();
               object[] objs =  CliUtils.CallFLMethod("Reject", new object[] { flInstanceId, new object[] { keys, presentation } });
                if (objs[0].ToString() != "0")
                {
                    s += presentation + " reject fail.\r\n";
                }
            }

            btnSearch_Click(null, null);

            if (string.IsNullOrEmpty(s))
            {
                MessageBox.Show("Reject succee!");
            }
            else
            {
                MessageBox.Show(s);
            }
        }
    }
}