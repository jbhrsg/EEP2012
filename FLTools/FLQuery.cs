using System.Workflow.ComponentModel;
using FLCore;
using FLTools.Base;
using System;
using System.ComponentModel;
using System.Drawing;

namespace FLTools
{
    [Serializable]
    [ToolboxBitmap(typeof(FLQuery), "Resources.FLQuery.png")]
    public class FLQuery : Activity, INonEventWaiting, IFLQueryActivity
    {
        #region properites
        private string _formName;
        [Editor(typeof(FormNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string FormName
        {
            get
            {
                return _formName;
            }
            set
            {
                _formName = value;
            }
        }

        private string _parameters;
        public string Parameters
        {
            get
            {
                return _parameters;
            }
            set
            {
                _parameters = value;
            }
        }

        private string _webFormName;
        [Editor(typeof(WebFormNameEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string WebFormName
        {
            get
            {
                return _webFormName;
            }
            set
            {
                _webFormName = value;
            }
        }
        #endregion

    }
}
