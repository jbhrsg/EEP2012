using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Data;
using System.Windows.Forms;
using FLTools.Base;
using FLTools;
using FLCore.Base;
using Srvtools;

namespace FLCore
{
    [Serializable]
    public class ApproveRight : InfoSerializableItem, IFLApproveBranchActivity 
    {
        private string _grade;
        private string _expression;
        private string _name;
        private string _parentActivity;

        public ApproveRight()
        {
        }

        [Editor(typeof(PropertyEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value;
                setName();
            }
        }

        private void setName()
        {
            if (_grade != null)
            {
                string[] ss = _grade.Split(";".ToCharArray());
                _name = ss[0].ToString().Trim();
            }
            else
            {
                _name = _grade;
            }
        }

        public string Expression
        {
            get
            {
                return _expression;
            }
            set
            {
                _expression = value;
            }
        }

        [Browsable(false)]
        public string ParentActivity
        {
            get
            {
                return _parentActivity;
            }
            set
            {
                _parentActivity = value;
            }
        }

        [Browsable(false)]
        public override string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public override string ToString()
        {
            return _name;   
        }

        //public string[] GetGradeItems()
        //{
        //    DbConnectionType connectionType = FLSequentialWorkflow.__ConnectionType;
        //    string connectionString = FLSequentialWorkflow.__ConnectionString;
        //    if (connectionString == null || connectionString == string.Empty)
        //    {
        //        MessageBox.Show("RootActivity's connection is null");
        //        return null;
        //    }

        //    IDbConnection connection = Global.AllocateConnection(connectionType, connectionString);

        //    string wherePart = string.Empty;
        //    return Global.GetOrgLevel(connection, wherePart);
        //}

        public string[] GetGradeItems()
        {
            object[] objs = CliUtils.CallMethod("GLModule", "GetOrgLevel", null);
            if (objs[0].ToString() == "0")
            {
                return (string[])objs[1];
            }
            else
            {
                return null;
            }
        }
    }
}
