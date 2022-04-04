using System;
using System.Collections.Generic;
using System.Text;
using System.Workflow.ComponentModel.Design;
using System.Workflow.ComponentModel.Compiler;
using System.IO;
using System.ComponentModel.Design.Serialization;
using System.Workflow.ComponentModel;
using System.Reflection;
using System.Collections;
using System.ComponentModel.Design;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using FLCore;

namespace FLDesignerCore
{
    public class FLDesignerLoader : WorkflowDesignerLoader
    {
        private Stream _stream;
        private string _fileName;
        private CompositeActivity _workflow;

        public Activity Temp;

        /// <summary>
        /// 构造FLDesignerLoader
        /// </summary>
        /// <param name="workflow">流程</param>
        public FLDesignerLoader(CompositeActivity workflow) : this("", workflow)
        {
            
        }

        /// <summary>
        /// 构造FLDesignerLoader
        /// </summary>
        /// <param name="fileName">XOML文件</param>
        /// <param name="workflow">流程</param>
        public FLDesignerLoader(string fileName, CompositeActivity workflow)
        {
            _fileName = fileName;
            _workflow = workflow;
        }

        /// <summary>
        /// 构造FLDesignerLoader
        /// </summary>
        /// <param name="stream">XOML文件流</param>
        /// <param name="workflow">流程</param>
        public FLDesignerLoader(Stream stream, CompositeActivity workflow)
        {
            _stream = stream;
            _workflow = workflow;
        }

        public CompositeActivity Workflow
        {
            get
            {
                return _workflow;
            }
        }

        protected override void PerformLoad(IDesignerSerializationManager serializationManager)
        {
            base.PerformLoad(serializationManager);

            if ((string.IsNullOrEmpty(_fileName) || !File.Exists(_fileName)) && _stream == null)
            {
                return;
            }

            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            XmlReader reader = null;
            if (!string.IsNullOrEmpty(_fileName))
            {
                reader = XmlReader.Create(_fileName);
            }
            else
            {
                reader = XmlReader.Create(_stream);
            }

            object deserializedObject = serializer.Deserialize(serializationManager, reader);
            CompositeActivity rootActivity = deserializedObject as CompositeActivity;

            reader.Close();

            _workflow.Description = rootActivity.Description;
            ((IFLRootActivity)_workflow).TableName = ((IFLRootActivity)rootActivity).TableName;
            ((IFLRootActivity)_workflow).EEPAlias = ((IFLRootActivity)rootActivity).EEPAlias;
            ((IFLRootActivity)_workflow).OrgKind = ((IFLRootActivity)rootActivity).OrgKind;
            ((IFLRootActivity)_workflow).Keys = ((IFLRootActivity)rootActivity).Keys;
            ((IFLRootActivity)_workflow).PresentFields = ((IFLRootActivity)rootActivity).PresentFields;
            ((IFLRootActivity)_workflow).FormName = ((IFLRootActivity)rootActivity).FormName;
            ((IFLRootActivity)_workflow).WebFormName = ((IFLRootActivity)rootActivity).WebFormName;
            ((IFLRootActivity)_workflow).ExpTime = ((IFLRootActivity)rootActivity).ExpTime;
            ((IFLRootActivity)_workflow).UrgentTime = ((IFLRootActivity)rootActivity).UrgentTime;
            ((IFLRootActivity)_workflow).ExpTimeField = ((IFLRootActivity)rootActivity).ExpTimeField;
            ((IFLRootActivity)_workflow).TimeUnit = ((IFLRootActivity)rootActivity).TimeUnit;
            ((IFLRootActivity)_workflow).NotifySendMail = ((IFLRootActivity)rootActivity).NotifySendMail;
            ((IFLRootActivity)_workflow).SkipForSameUser = ((IFLRootActivity)rootActivity).SkipForSameUser;
            ((IFLRootActivity)_workflow).RejectProcedure = ((IFLRootActivity)rootActivity).RejectProcedure;
            ((IFLRootActivity)_workflow).BodyField = ((IFLRootActivity)rootActivity).BodyField;
            //((ISupportSetConnectionString)_workflow).EEPAlias = ((ISupportSetConnectionString)rootActivity).EEPAlias;
            //((ISupportSetConnectionString)_workflow).ConnectionType = ((ISupportSetConnectionString)rootActivity).ConnectionType;
            ((ISupportSetClientDll)_workflow).ClientDll = ((ISupportSetClientDll)rootActivity).ClientDll;

            //List<string> names = new List<string>();
            //foreach (Activity acitivity in rootActivity.Activities)
            //{
            //    names.Add(acitivity.QualifiedName);
            //}

            //foreach (string name in names)
            //{
            //    Activity activity = rootActivity.GetActivityByName(name);
            //    rootActivity.Activities.Remove(activity);
            //    _workflow.Activities.Add(activity);
            //    AddActivityToDesigner(activity);
            //}

            List<Activity> activities = new List<Activity>();
            foreach (Activity activity in rootActivity.Activities)
            {
                activities.Add(activity);
            }

            foreach (Activity activity in activities)
            {
                rootActivity.Activities.Remove(activity);
                _workflow.Activities.Add(activity);
                AddActivityToDesigner(activity);
            }
        }

        protected override void Initialize()
        {
            base.Initialize();

            IDesignerLoaderHost host = this.LoaderHost;
            if (!(host == null))
            {
                TypeProvider typeProvider = new TypeProvider(host);
                typeProvider.AddAssemblyReference(typeof(string).Assembly.Location);
            }
        }

        public override void Dispose()
        {
            try
            {
                IDesignerLoaderHost loaderHost = LoaderHost;
                if (!(loaderHost == null))
                {
                    loaderHost.RemoveService(typeof(ITypeProvider), true);
                }
            }
            finally
            {
                base.Dispose();
            }
        }

        public override string FileName
        {
            get
            {
                return _fileName;
            }
        }

        public override System.IO.TextReader GetFileReader(string filePath)
        {
            return new StreamReader(new FileStream(filePath, FileMode.OpenOrCreate));
        }

        public override System.IO.TextWriter GetFileWriter(string filePath)
        {
            return new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate));
        }
    }
}

