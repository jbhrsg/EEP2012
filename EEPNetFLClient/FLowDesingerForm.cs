using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FLTools;
using Srvtools;
using System.IO;
using FLCore;

namespace EEPNetFLClient
{
    public partial class FLowDesingerForm : InfoForm
    {
        public FLowDesingerForm(IShowForm form, Stream stream)
        {
            InitializeComponent();
            _form = form;
            //_flowFileName = (flowFileName.ToLower().IndexOf(".xoml") == -1) ? flowFileName.ToLower() : flowFileName.ToLower().Substring(0, flowFileName.ToLower().IndexOf(".xoml"));
            _stream = stream;
        }
        
        private void FLowDesingerForm_Load(object sender, EventArgs e)
        {
            doLoad();
        }

        public void doLoad()
        {
            if (_stream != null)
                this.desinger.LoadWorkflow(_stream);
        }

        private IShowForm _form = null;
        public IShowForm Form
        {
            get { return _form; }
            set { _form = value;}
        }

        private string _flowFileName;
        public string FlowFileName
        {
            get { return _flowFileName; }
            set { _flowFileName = value; }
        }

        private Stream _stream;
        public Stream Stream
        {
            get { return _stream; }
            set { _stream = value; }
        }

        private void desinger__DoubleClick(object sender, FLDesignerCore._DoubleClickEventArgs e)
        {
            StringDictionary stringDic = new StringDictionary();
            stringDic.Add("FLOWFILENAME", FlowFileName);
            if (e.SelectedActivity is FLStand && ((FLStand)e.SelectedActivity).Name == e.RootActivity.Activities[0].Name)
            {
                FLStand stand = (FLStand)e.SelectedActivity;
                string openPath = string.IsNullOrEmpty(stand.FormName) ? ((IFLRootActivity)e.RootActivity).FormName : stand.FormName;
                if (openPath != null && openPath != "" && openPath.IndexOf('.') != -1)
                {
                    string packageName = openPath.Substring(0, openPath.IndexOf('.'));
                    string formName = openPath.Substring(openPath.IndexOf('.') + 1);
                    string flMode = ((int)stand.FLNavigatorMode).ToString();
                    string naMode = ((int)stand.NavigatorMode).ToString();
                    stringDic.Add("FLNAVMODE", flMode);
                    stringDic.Add("NAVMODE", naMode);
                    //if (!string.IsNullOrEmpty(stand.Parameters))
                    //{
                    //    string[] lstParams = stand.Parameters.Split(';');
                    //    foreach (string param in lstParams)
                    //    {
                    //        if (param.IndexOf('=') != -1)
                    //        {
                    //            stringDic.Add(param.Substring(0, param.IndexOf('=')), param.Substring(param.IndexOf('=') + 1));
                    //        }
                    //    }
                    //}
                    Form.showForm(packageName, formName, stand.Parameters, stringDic);
                }
            }
            else if (e.SelectedActivity is FLHyperLink)
            {
                FLHyperLink hlnk = (FLHyperLink)e.SelectedActivity;
                string openPath = hlnk.FormName;
                if (openPath != null && openPath != "" && openPath.IndexOf('.') != -1)
                {
                    string packageName = openPath.Substring(0, openPath.IndexOf('.'));
                    string formName = openPath.Substring(openPath.IndexOf('.') + 1);
                    Form.showForm(packageName, formName, hlnk.Parameters, stringDic);
                }
                else if(!string.IsNullOrEmpty(hlnk.LinkFlow))
                {
                    //string packageName = Path.GetDirectoryName(this.FlowFileName);
                    //string formName = openPath;

                    //string path = hlnk.LinkFlow + ".xoml";
                    object[] objs = CliUtils.CallFLMethod("GetFLDefinitionXmlString", new object[] { hlnk.LinkFlow });
                    if (objs[0] != null && (int)objs[0] == 0)
                    {
                        string xml = objs[1] as string;
                        byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml);
                        MemoryStream stream = new MemoryStream(bytes);
                        this.desinger.LoadWorkflow(stream);
                    }
                }
            }
            else if (e.SelectedActivity is FLQuery)
            {
                FLQuery query = (FLQuery)e.SelectedActivity;
                string openPath = query.FormName;
                if (openPath != null && openPath != "" && openPath.IndexOf('.') != -1)
                {
                    string packageName = openPath.Substring(0, openPath.IndexOf('.'));
                    string formName = openPath.Substring(openPath.IndexOf('.') + 1);
                    stringDic.Add("FLNAVMODE", "4");
                    stringDic.Add("NAVMODE", "3");
                    Form.showForm(packageName, formName, query.Parameters, stringDic);
                }

            }
        }
    }
}