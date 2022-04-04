using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Xml;
using System.Xml.Serialization;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLProcedureActivity : FLActivity, IFLProcedureActivity, INonEventWaiting
    {
        private string _moduleName;
        private string _methodName;
        private bool _errorLog;
        private string _errorToRole;

        public FLProcedureActivity()
            : this(string.Empty)
        {
        }

        public FLProcedureActivity(string name)
            : base(name)
        {
        }

        [XmlAttribute("ModuleName")]
        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                _moduleName = value;
            }
        }

        [XmlAttribute("MethodName")]
        public string MethodName
        {
            get
            {
                return _methodName;
            }
            set
            {
                _methodName = value;
            }
        }

        [XmlAttribute("ErrorLog")]
        public bool ErrorLog
        {
            get
            {
                return _errorLog;
            }
            set
            {
                _errorLog = value;
            }
        }

        [XmlAttribute("ErrorToRole")]
        public string ErrorToRole
        {
            get
            {
                return _errorToRole;
            }
            set
            {
                _errorToRole = value;
            }
        }
    }
}
