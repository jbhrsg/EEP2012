using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Srvtools;
using System.Collections;

namespace FLDesigner
{
    public partial class CommentForm : Form
    {
        private static string GET_COMMENT = @"SELECT A.FLOW_DESC,A.S_STEP_ID,A.FORM_PRESENT_CT,B.GROUPNAME,A.REMARK,(A.UPDATE_DATE + ' ' + A.UPDATE_TIME) as DateTime, A.STATUS
                    FROM SYS_TODOHIS  A,GROUPS B WHERE A.LISTID='{0}' AND B.GROUPID=A.S_ROLE_ID AND B.ISROLE='Y' ORDER BY A.UPDATE_DATE,A.UPDATE_TIME";
        private string _instanceId;

        public CommentForm(string instanceId)
        {
            InitializeComponent();
            _instanceId = instanceId;
        }

        private void CommentForm_Load(object sender, EventArgs e)
        {
            gvComment.AutoGenerateColumns = false;

            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item2");
            string[] items = message.Split(",".ToCharArray());
            int i = 0;
            foreach (string item in items)
            {
                if (gvComment.Columns.Count < i)
                    break;

                gvComment.Columns[i].HeaderText = item;
                i++;
            }

            string project = CliUtils.fCurrentProject;
            string sql = string.Format(GET_COMMENT, _instanceId);
            DataSet dataSet = CliUtils.ExecuteSql("GLModule", "cmdWorkflow", sql, true, project);

            Hashtable t = new Hashtable();
            string message2 = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item3");
            string[] items2 = message2.Split(",".ToCharArray());
            foreach (string item2 in items2)
            {
                string[] ss = item2.Split(":".ToCharArray());
                t.Add(ss[0].Trim().ToUpper(), ss[1].Trim());
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                object o = row["STATUS"];
                if (o == null || o == DBNull.Value)
                    continue;

                row["STATUS"] = t.ContainsKey(o.ToString().ToUpper()) ? t[o.ToString().ToUpper()] : string.Empty;
            }

            gvComment.DataSource = dataSet.Tables[0];
        }
    }
}
