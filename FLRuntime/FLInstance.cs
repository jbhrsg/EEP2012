using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Workflow.ComponentModel;
using FLCore;
using System.Workflow.Activities;
using System.Workflow.ComponentModel.Serialization;
using System.ComponentModel.Design.Serialization;
using FLTools;
using System.Xml.Serialization;
using System.Workflow.Runtime;
using System.Collections;

using System.Data.SqlClient;
using System.Data;
using System.Reflection;

using System.Text.RegularExpressions;
using FLTools.ComponentModel;
using System.IO;
using Microsoft.Win32;
using FLRuntime.Hosting;
using System.Linq;

namespace FLRuntime
{
    [Serializable]
    public class FLInstance //: IFLInstance
    {

        private string _version;

        private Guid _flInstanceId;
        private Guid _flDefinitionId;

        private string _creator;
        private string _createRole;
        private DateTime _createdTime;

        [NonSerialized]
        private FLRuntime _flRuntime;
        //[NonSerialized]
        //private XmlReader _flDefinitionReader;
        //[NonSerialized]
        //private XmlReader _flRulesReader;

        internal string _flDefinitionFile;
        private string _flRulesFile;

        [NonSerialized]
        private object[] _clientInfo;
        [NonSerialized]
        internal DataSet _hostDataSet;
        [NonSerialized]
        private object[] _keyValues;

        [NonSerialized]
        private FLDirection _flDirection;

        private List<object> _p;

        private FLActivity _flDefinition;
        private FLRootActivity _rootFLActivity;
        private string _flDefinitionXmlString;

        [NonSerialized]
        private FLActivity _currentFLActivity;
        [NonSerialized]
        private FLActivity _previousFLActivity;
        [NonSerialized]
        private List<FLActivity> _nextFLActivities;

        private List<string> _setUpperParallels;
        private List<string> _setLocations;

        [NonSerialized]
        private FLActivity _sendFromFLActivity;

        [NonSerialized]
        private Hashtable _flDefinitionXmlNodes;

        [NonSerialized]
        private bool _v = true;

        [NonSerialized]
        private string _vN = string.Empty;

        [NonSerialized]
        internal string VM = string.Empty;

        [NonSerialized]
        private string _r = string.Empty;

        private List<string> _rl;

        //[NonSerialized]
        private string _webUrl = string.Empty;

        [NonSerialized]
        private List<string> _wl;

        private char _flflag;

        private string _orgKind;

        // 记录每一布的FLPathList、执行过的FLActivity、FLDirection等信息，以便Retake的时候使用。
        private bool _isRetake;
        private bool _isReturn;
        private bool _isPause;
        private bool _isPlusApprove;
        private Hashtable _records;
        private List<string> _nameRecords;
        //private bool _isSupportRetake;
        private List<string> _cacheFLInstanceParms;

        private Hashtable _executedActivities;
        //private static string dllName = "Microsoft.Workflow.Extension.dll";
        //private static string className = "Microsoft.Workflow.Extension.XomlParser";
        //private static string methodName = "GetWorkflowDefinition";

        //test use
        public FLInstance(object[] clientInfo) { _clientInfo = clientInfo; }

        // 第一次会向Db中写Definition。
        /// <summary>
        /// 初始化流程
        /// </summary>
        /// <param name="instanceId">流程Id</param>
        /// <param name="runtime">流程运行时</param>
        /// <param name="flDefinitionFile">流程XOML文件</param>
        /// <param name="flRulesFile">流程规则文件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="hostDataSet">宿主表</param>
        /// <param name="orgKind">角色的OrgKind</param>
        public FLInstance(Guid instanceId, FLRuntime runtime, string flDefinitionFile, string flRulesFile, object[] clientInfo, DataSet hostDataSet, string orgKind)// XmlReader flDefinitionReader, XmlReader flRulesReader)
        {
            _createdTime = DateTime.Now;

            _clientInfo = clientInfo;
            _hostDataSet = hostDataSet;
            _flflag = ' ';
            _creator = string.Empty;
            _flDefinitionId = Guid.NewGuid();
            _flRuntime = runtime;
            _flInstanceId = instanceId;
            //_flDefinitionReader = flDefinitionReader;
            //_flRulesReader = flRulesReader;

            _flDefinitionFile = flDefinitionFile;
            _flRulesFile = flRulesFile;
            _orgKind = orgKind;

            InitFLDefinition();
            InitFLDefinitionId();
            InitFLDefinitionXmlString();

            _currentFLActivity = null;
            _previousFLActivity = null;
            _nextFLActivities = new List<FLActivity>();
            _setUpperParallels = new List<string>();
            _setLocations = new List<string>();
            _executedActivities = new Hashtable();

            solution = ((object[])clientInfo[0])[6] == null ? string.Empty : ((object[])clientInfo[0])[6].ToString();

            _flDirection = FLDirection.Waiting;
            _p = new List<object>();
            _rl = new List<string>();

            _records = new Hashtable();
            _nameRecords = new List<string>();
            _isFirstInParallel = false;
            _cacheFLInstanceParms = new List<string>();

            _version = "2.0";
        }

        public List<string> WL
        {
            get
            {
                return _wl;
            }
        }

        /// <summary>
        /// 流程创建时间
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                return _createdTime;
            }
        }

        public List<object> P
        {
            get
            {
                return _p;
            }
        }

        public List<string> NameRecords
        {
            get
            {
                return _nameRecords;
            }
            set
            {
                _nameRecords = value;
            }
        }

        public string VN
        {
            get
            {
                return _vN;
            }
            set
            {
                _vN = value;
            }
        }

        public string R
        {
            get
            {
                return _r;
            }
            set
            {
                _r = value;
            }
        }

        public List<string> RL
        {
            get
            {
                return _rl;
            }
            set
            {
                _rl = value;
            }
        }

        public bool V
        {
            get
            {
                return _v;
            }
            set
            {
                _v = value;
            }
        }

        public string FLDefinitionFile
        {
            get
            {
                var fileInfo = new FileInfo(_flDefinitionFile);
                if (fileInfo.Exists)
                {
                    return _flDefinitionFile;
                }
                else
                {
                    return Path.Combine(EEPRegistry.Server, "WorkFlow", fileInfo.Directory.Name, fileInfo.Name);
                }
            }
        }


        /// <summary>
        /// 流程创建者
        /// </summary>
        public string Creator
        {
            get
            {
                return _creator;
            }
        }

        /// <summary>
        /// 流程创建者角色
        /// </summary>
        public string CreateRole
        {
            get
            {
                return _createRole;
            }
        }

        /// <summary>
        /// 流程流转方向
        /// </summary>
        public FLDirection FLDirection
        {
            get
            {
                return _flDirection;
            }
        }

        /// <summary>
        /// 是否取回
        /// </summary>
        public bool IsRetake
        {
            get
            {
                return _isRetake;
            }
        }

        /// <summary>
        /// 是否加签
        /// </summary>
        public bool IsPlusApprove
        {
            get
            {
                return _isPlusApprove;
            }
        }

        /// <summary>
        /// 是否退回
        /// </summary>
        public bool IsReturn
        {
            get
            {
                return _isReturn;
            }
        }

        /// <summary>
        /// 是否支持加签
        /// </summary>
        public bool IsPause
        {
            get
            {
                return _isPause;
            }
        }

        public string Version
        {
            get
            {
                return _version;
            }
        }

        /// <summary>
        /// 根Activity
        /// </summary>
        public FLActivity RootFLActivity
        {
            get
            {
                return _rootFLActivity;
            }
        }

        /// <summary>
        /// 流程Id
        /// </summary>
        public Guid FLInstanceId
        {
            get
            {
                return _flInstanceId;
            }
        }

        /// <summary>
        /// 流程定义Id
        /// </summary>
        public Guid FLDefinitionId
        {
            get
            {
                return _flDefinitionId;
            }
            set
            {
                _flDefinitionId = value;
            }
        }

        private string solution;

        public string Solution
        {
            get
            {
                if (string.IsNullOrEmpty(solution))
                {
                    return ((object[])_clientInfo[0])[6] == null ? string.Empty : ((object[])_clientInfo[0])[6].ToString();
                }
                return solution;
            }
            set { solution = value; }
        }


        /// <summary>
        /// 流程运行时
        /// </summary>
        public FLRuntime FLRuntime
        {
            get
            {
                return _flRuntime;
            }
        }

        /// <summary>
        /// 流程标记
        /// </summary>
        public char FLFlag
        {
            get
            {
                return _flflag;
            }
            set
            {
                _flflag = value;
            }
        }

        /// <summary>
        /// 上一Activity
        /// </summary>
        public FLActivity PreviousFLActivity
        {
            get
            {
                return _previousFLActivity;
            }
        }

        /// <summary>
        /// 当前Activity
        /// </summary>
        public FLActivity CurrentFLActivity
        {
            get
            {
                return _currentFLActivity;
            }
            set
            {
                _currentFLActivity = value;
            }
        }

        /// <summary>
        /// 下一Activity的集合
        /// </summary>
        public List<FLActivity> NextFLActivities
        {
            get
            {
                return _nextFLActivities;
            }
            set
            {
                _nextFLActivities = value;
            }
        }

        /// <summary>
        /// 流程流转方向
        /// </summary>
        public FLActivity FLDefinition
        {
            get
            {
                return _flDefinition;
            }
            set
            {
                _flDefinition = value;
            }
        }

        //public bool IsSupportRetake
        //{
        //    get
        //    {
        //        return _isSupportRetake;
        //    }
        //}

        private object[] _flInstanceParms = null;
        /// <summary>
        /// 添加流程参数到缓存中
        /// </summary>
        /// <param name="parm">流程参数</param>
        public void AddCacheFLInstanceParm(object[] parm)
        {
            if (Version == "2.0")
            {
                _flInstanceParms = parm;
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (object o in parm)
                    sb.Append(o == null ? string.Empty : o.ToString() + ((char)234).ToString());

                _cacheFLInstanceParms.Add(sb.ToString());
            }
        }

        private object[] FLInstanceParms
        {
            get
            {
                if (Version == "2.0")
                {
                    return _flInstanceParms;
                }
                else
                {
                    string s = _cacheFLInstanceParms[_cacheFLInstanceParms.Count - 1];
                    string[] ss = s.Split(((char)234).ToString().ToCharArray());
                    List<object> parms = new List<object>(ss);
                    return parms.ToArray();
                }
            }
        }

        /// <summary>
        /// 移除流程参数
        /// </summary>
        /// <returns></returns>
        public object[] RemoveCacheFLInstanceParm()
        {
            if (Version == "2.0")
            {
                return null;
            }
            else
            {
                string s = _cacheFLInstanceParms[_cacheFLInstanceParms.Count - 1];
                string[] ss = s.Split(((char)234).ToString().ToCharArray());
                List<object> parms = new List<object>();
                foreach (string o in ss)
                    parms.Add(o);

                _cacheFLInstanceParms.RemoveAt(0);

                return parms.ToArray();
            }
        }

        private static void SetIfElseAcitivityDisable(XmlNode node)
        {
            if (node.Name == "IfElseActivity")
            {
                XmlAttribute att = node.Attributes["Enabled"];
                if (att == null)
                {
                    att = node.OwnerDocument.CreateAttribute("Enabled");
                    node.Attributes.Append(att);
                }
                att.Value = "False";
            }
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                SetIfElseAcitivityDisable(node.ChildNodes[i]);
            }
        }

        public static Activity GetWorkflowDefinition(string flDefinitionFile, string flRulesFile)
        {
            string tempfileName = string.Empty;
            do
            {
                tempfileName = Path.GetTempPath() + Path.GetRandomFileName();
            }
            while (File.Exists(tempfileName));
            File.Copy(flDefinitionFile, tempfileName, true);
            File.SetAttributes(tempfileName, FileAttributes.Normal);

            XmlDocument xml = new XmlDocument();
            xml.Load(tempfileName);
            SetIfElseAcitivityDisable(xml.DocumentElement); //将所有的IfElseActivityDisable掉
            xml.Save(tempfileName);
            flDefinitionFile = tempfileName;

            XmlReader reader = null;
            XmlReader ruleReader = null;
            try
            {
                reader = XmlReader.Create(flDefinitionFile);
                if (!string.IsNullOrEmpty(flRulesFile))
                {
                    ruleReader = XmlReader.Create(flDefinitionFile);
                }

                Activity activity = null;
                using (WorkflowRuntime workflowRuntime = new WorkflowRuntime())
                {
                    WorkflowInstance instance = workflowRuntime.CreateWorkflow(reader, ruleReader, null);
                    activity = instance.GetWorkflowDefinition();
                    workflowRuntime.StopRuntime();
                }
                return activity;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }

                if (ruleReader != null)
                {
                    ruleReader.Close();
                }

                if (File.Exists(tempfileName))
                {
                    File.Delete(tempfileName);
                }

            }
        }

        /// <summary>
        /// 修改流程定义
        /// </summary>
        /// <param name="flDefinitionFile">流程XOML文件</param>
        /// <param name="flRulesFile">流程规则文件</param>
        public void ModifyFLDefinition(string flDefinitionFile, string flRulesFile)
        {
            //Assembly assembly = Assembly.LoadFrom(EEPRegistry.Server + "\\" + dllName);
            //Type type = assembly.GetType(className);
            //MethodInfo method = type.GetMethod(methodName);

            //object obj = method.Invoke(null, new object[] { _flDefinitionFile, _flRulesFile });
            //Activity wfActivity = (Activity)obj;

            Activity wfActivity = FLInstance.GetWorkflowDefinition(flDefinitionFile, flRulesFile);

            IFLRootActivity tempRootActivity = null;
            // FLActivity
            if (wfActivity is IFLRootActivity)
            {
                tempRootActivity = new FLRootActivity();
                InitFLActivities((IFLActivity)tempRootActivity, wfActivity);
            }
            else
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ErrorInXoml");
                throw new FLException(message);
            }

            ICollection keys = _rootFLActivity.GetAllChildFLActivities().Keys;
            foreach (object obj1 in keys)
            {
                FLActivity a0 = _rootFLActivity.GetFLActivityByName(obj1.ToString());
                FLActivity a1 = ((FLRootActivity)tempRootActivity).GetFLActivityByName(obj1.ToString());

                if (a0 != null && a1 != null && a0.GetType() == a1.GetType())
                {
                    InitFLActivities2(a0, a1);
                }
            }

            _flDefinition = _rootFLActivity;

            CheckFL();

            //if (_rootFLActivity.GetFLActivitiesByType(typeof(FLParallelActivity)).Count != 0)
            //{
            //    _isSupportRetake = false;
            //}
            //else
            //{
            //    _isSupportRetake = true;
            //}

            InitFLDefinitionXmlString();
        }

        /// <summary>
        /// 初始化流程定义Id
        /// </summary>
        public void InitFLDefinitionId()
        {
            InitFLDefinitionId(this, _clientInfo);
        }

        /// <summary>
        /// 初始化流程定义Id
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="clientInfo">ClientInfo</param>
        public void InitFLDefinitionId(FLInstance flInstance, object[] clientInfo)
        {
            string flTypeName = flInstance.RootFLActivity.Name;
            string flDefinition = flInstance.GetFLDefinitionXoml().InnerXml;
            string flTypeId = Global.GetFLTypeId(flTypeName, flDefinition, clientInfo);

            _flDefinitionId = new Guid(flTypeId);
        }

        /// <summary>
        /// 通过流程XOML得到Activity
        /// </summary>
        /// <param name="definitionFile">流程XOML文件</param>
        /// <param name="ruleRulesFile">流程规则文件</param>
        /// <returns></returns>
        public static Activity GetActivityByXoml(string definitionFile, string ruleRulesFile)
        {
            //Assembly assembly = Assembly.LoadFrom(EEPRegistry.Server + "\\" + dllName);
            //Type type = assembly.GetType(className);
            //MethodInfo method = type.GetMethod(methodName);

            //object obj = method.Invoke(null, new object[] { definitionFile, ruleRulesFile });

            //return (Activity)obj;
            return FLInstance.GetWorkflowDefinition(definitionFile, ruleRulesFile);
        }

        /// <summary>
        /// 初始化流程定义
        /// </summary>
        private void InitFLDefinition()
        {
            Activity wfActivity = GetActivityByXoml(FLDefinitionFile, _flRulesFile);

            IFLRootActivity rootActivity = null;
            // FLActivity
            if (wfActivity is IFLRootActivity)
            {
                rootActivity = new FLRootActivity();
                InitFLActivities((IFLActivity)rootActivity, wfActivity);
            }
            else
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ErrorInXoml");
                throw new FLException(message);
            }

            _rootFLActivity = (FLRootActivity)rootActivity;
            _flDefinition = (FLActivity)rootActivity;

            CheckFL();

            //if (_rootFLActivity.GetFLActivitiesByType(typeof(FLParallelActivity)).Count != 0)
            //{
            //    _isSupportRetake = false;
            //}
            //else
            //{
            //    _isSupportRetake = true;
            //}
        }

        [NonSerialized]
        private static Hashtable XmlSerializerTable = new Hashtable();

        private XmlSerializer CreateXmlSerializer(Type activityType)
        {
            if (!XmlSerializerTable.Contains(activityType))
            {
                XmlSerializer serializer = new XmlSerializer(activityType);
                XmlSerializerTable[activityType] = serializer;
            }
            return (XmlSerializer)XmlSerializerTable[activityType];
        }

        /// <summary>
        /// 初始化流程定义的XML字符串
        /// </summary>
        private void InitFLDefinitionXmlString()
        {
            if (_flDefinition == null)
            {
                InitFLDefinition();
            }

            XmlSerializer serializer = CreateXmlSerializer(typeof(FLRootActivity));

            StringBuilder builder = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(builder);
            serializer.Serialize(writer, _flDefinition);

            _flDefinitionXmlString = builder.ToString();
        }

        /// <summary>
        /// 检查流程
        /// </summary>
        private void CheckFL()
        {
            FLActivity firstFLActivity = _rootFLActivity.ChildFLActivities[0];
            if (!(firstFLActivity is IEventWaiting))
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FirstFLActivityError");
                throw new FLException(message);
            }

            if (((IEventWaiting)firstFLActivity).SendToKind != SendToKind.Applicate)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FirstFLActivityError2");
                throw new FLException(2, message);
            }

            List<FLIfElseActivity> ifElseFLActivities = new List<FLIfElseActivity>();
            List<FLParallelActivity> parallelFLActivities = new List<FLParallelActivity>();

            Hashtable allFLActivities = _rootFLActivity.GetAllChildFLActivities();
            ICollection keys = allFLActivities.Keys;
            foreach (object key in keys)
            {
                object a = allFLActivities[key];
                if (a is IFLIfElseActivity)
                {
                    ifElseFLActivities.Add((FLIfElseActivity)a);
                }

                if (a is IFLParallelActivity)
                {
                    parallelFLActivities.Add((FLParallelActivity)a);
                }
            }

            foreach (FLIfElseActivity a in ifElseFLActivities)
            {
                int i = 0;
                foreach (FLActivity b in a.ChildFLActivities)
                {
                    if (((FLIfElseBranchActivity)b).Condition != string.Empty)
                    {
                        i++;
                    }
                }

                if (i < a.ChildFLActivities.Count - 1)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "IfElseBranchConditionNotSet"), a.Name, (a.ChildFLActivities.Count - i).ToString());
                    throw new FLException(message);
                }
            }

            foreach (FLParallelActivity a in parallelFLActivities)
            {
                string desc = a.Description;
                if (!string.IsNullOrEmpty(desc) && !string.IsNullOrEmpty(desc.Trim()) && desc.Trim().ToLower() != "or" && desc.Trim().ToLower() != "and")
                {
                    string s = desc.Trim().ToLower();
                    string[] ss = s.Split(":".ToCharArray());
                    if (ss.Length != 2)
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ParallelActivityDescriptError"), a.Name);
                        throw new FLException(2, message);
                    }

                    if (ss[0].Trim() != "rate")
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ParallelActivityDescriptError"), a.Name);
                        throw new FLException(2, message);
                    }

                    decimal d = 0;
                    if (!decimal.TryParse(ss[1].Trim(), out d))
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ParallelActivityDescriptError"), a.Name);
                        throw new FLException(2, message);
                    }

                    if (d < 0 || d > 100)
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ParallelActivityDescriptError"), a.Name);
                        throw new FLException(2, message);
                    }
                }

                ICollection aa = a.GetAllChildFLActivities().Values;
                foreach (object b in aa)
                {
                    if (b is IFLParallelActivity && a.Name != ((FLActivity)b).Name)
                    {
                        String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ParallelNotSupportNest");
                        //throw new FLException(message);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化流程FLActivity
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="tempActivity"></param>
        private void InitFLActivities2(IFLActivity activity, object tempActivity)
        {
            //if (tempActivity is IEventWaiting)
            //{
            //    IEventWaiting m = (IEventWaiting)activity;
            //    IEventWaiting n = (IEventWaiting)tempActivity;
            //    m.AllowSendBack = n.AllowSendBack;
            //}

            if (tempActivity is IFLRootActivity)
            {
                IFLRootActivity m = (IFLRootActivity)activity;
                IFLRootActivity n = (IFLRootActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.EEPAlias = n.EEPAlias;
                m.TableName = n.TableName;
                m.Keys = n.Keys;
                m.PresentFields = n.PresentFields;
                m.OrgKind = n.OrgKind;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.ExpTime = n.ExpTime;
                m.ExpTimeField = n.ExpTimeField;
                m.UrgentTime = n.UrgentTime;
                m.TimeUnit = n.TimeUnit;
                m.SkipForSameUser = n.SkipForSameUser;
                m.RejectProcedure = n.RejectProcedure;
                m.BodyField = n.BodyField;
                m.MailApproveLevel = n.MailApproveLevel;
            }
            else if (tempActivity is IFLDetailsActivity)
            {
                IFLDetailsActivity m = (IFLDetailsActivity)activity;
                IFLDetailsActivity n = (IFLDetailsActivity)tempActivity;


                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.DetailsTableName = n.DetailsTableName;
                m.ParallelField = n.ParallelField;
                m.SendToMasterField = n.SendToMasterField;
                m.ParallelMode = n.ParallelMode;
                m.ParallelRate = n.ParallelRate;
                m.RelationKeys = n.RelationKeys;
                m.SendToField = n.SendToField;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FLNavigatorField = n.FLNavigatorField;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;

                m.ExtApproveID = n.ExtApproveID;
                m.ExtGroupField = n.ExtGroupField;
                m.ExtValueField = n.ExtValueField;

                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;

                m.AllowSendBack = n.AllowSendBack;

                m.PlusApprove = n.PlusApprove;
                m.PlusApproveReturn = n.PlusApproveReturn;
            }
            else if (tempActivity is IFLSubFlowActivity)
            {
                IFLSubFlowActivity m = (IFLSubFlowActivity)activity;
                IFLSubFlowActivity n = (IFLSubFlowActivity)tempActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.XomlName = n.XomlName;
            }
            else if (tempActivity is IEventWaiting)
            {
                IEventWaiting m = (IEventWaiting)activity;
                IEventWaiting n = (IEventWaiting)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;

                m.AllowSendBack = n.AllowSendBack;
              
                if (tempActivity is IFLStandActivity)
                {
                    ((IFLStandActivity)m).PlusApprove = ((IFLStandActivity)n).PlusApprove;
                    ((IFLStandActivity)m).PlusApproveReturn = ((IFLStandActivity)n).PlusApproveReturn;
                    ((IFLStandActivity)m).DelayAutoApprove = ((IFLStandActivity)n).DelayAutoApprove;
                }
                else if (tempActivity is IFLApproveActivity)
                {
                    IFLApproveActivity p = (IFLApproveActivity)activity;
                    IFLApproveActivity q = (IFLApproveActivity)tempActivity;
                    ((IFLApproveActivity)q).PlusApprove = ((IFLApproveActivity)p).PlusApprove;
                    ((IFLApproveActivity)q).PlusApproveReturn = ((IFLApproveActivity)p).PlusApproveReturn;
                    ((IFLApproveActivity)q).DelayAutoApprove = ((IFLApproveActivity)p).DelayAutoApprove;

                    p.Description = q.Description;
                }
                else if (tempActivity is FLApproveBranchActivity)
                {
                    IFLApproveBranchActivity p = (IFLApproveBranchActivity)activity;
                    IFLApproveBranchActivity q = (IFLApproveBranchActivity)tempActivity;

                    p.Expression = q.Expression;
                    p.Grade = q.Grade;
                }
            }
            else if (tempActivity is IFLIfElseActivity)
            {
                IFLIfElseActivity m = (IFLIfElseActivity)activity;
                IFLIfElseActivity n = (IFLIfElseActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (tempActivity is IFLIfElseBranchActivity)
            {
                IFLIfElseBranchActivity m = (IFLIfElseBranchActivity)activity;
                IFLIfElseBranchActivity n = (IFLIfElseBranchActivity)tempActivity;

                m.Enabled = n.Enabled;
                m.Condition = n.Description == null ? string.Empty : n.Description.ToString();
            }
            else if (tempActivity is IFLParallelActivity)
            {
                IFLParallelActivity m = (IFLParallelActivity)activity;
                IFLParallelActivity n = (IFLParallelActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (tempActivity is IFLSequenceActivity)
            {
                IFLSequenceActivity m = (IFLSequenceActivity)activity;
                IFLSequenceActivity n = (IFLSequenceActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (tempActivity is IFLNotifyActivity)
            {
                IFLNotifyActivity m = (IFLNotifyActivity)activity;
                IFLNotifyActivity n = (IFLNotifyActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;
            }
            else if (tempActivity is IFLProcedureActivity)
            {
                IFLProcedureActivity m = (IFLProcedureActivity)activity;
                IFLProcedureActivity n = (IFLProcedureActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ErrorLog = n.ErrorLog;
                m.ErrorToRole = n.ErrorToRole;
                m.MethodName = n.MethodName;
                m.ModuleName = n.ModuleName;
            }
            else if (tempActivity is IFLValidateActivity)
            {
                IFLValidateActivity m = (IFLValidateActivity)activity;
                IFLValidateActivity n = (IFLValidateActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Expression = n.Expression;
                m.Message = n.Message;
            }
            else if (tempActivity is IFLHyperLinkActivity)
            {
                IFLHyperLinkActivity m = (IFLHyperLinkActivity)activity;
                IFLHyperLinkActivity n = (IFLHyperLinkActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Parameters = n.Parameters;
            }
            else if (tempActivity is IFLQueryActivity)
            {
                IFLQueryActivity m = (IFLQueryActivity)activity;
                IFLQueryActivity n = (IFLQueryActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Parameters = n.Parameters;
            }
            else if (tempActivity is IFLRejectActivity)
            {
                IFLRejectActivity m = (IFLRejectActivity)activity;
                IFLRejectActivity n = (IFLRejectActivity)tempActivity;

                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
            }
            else if (tempActivity is IFLGotoActivity)
            {
                IFLGotoActivity m = (IFLGotoActivity)activity;
                IFLGotoActivity n = (IFLGotoActivity)tempActivity;
                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;
                m.ActivityName = n.ActivityName;
            }
            else
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "NotSupportActivityType"), tempActivity.GetType().Name);
                throw new FLException(message);
            }
        }

        /// <summary>
        /// 初始化流程FLActivity
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="wfActivity"></param>
        private void InitFLActivities(IFLActivity activity, object wfActivity)
        {
            //if (wfActivity is IEventWaiting)
            //{
            //    IEventWaiting m = (IEventWaiting)activity;
            //    IEventWaiting n = (IEventWaiting)wfActivity;
            //    m.AllowSendBack = n.AllowSendBack;
            //}

            if (wfActivity is IFLRootActivity)
            {
                IFLRootActivity m = (IFLRootActivity)activity;
                IFLRootActivity n = (IFLRootActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.EEPAlias = n.EEPAlias;
                m.TableName = n.TableName;
                m.Keys = n.Keys;
                m.PresentFields = n.PresentFields;
                m.OrgKind = (_orgKind == null || _orgKind == string.Empty) ? n.OrgKind : _orgKind;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.ExpTime = n.ExpTime;
                m.ExpTimeField = n.ExpTimeField;
                m.UrgentTime = n.UrgentTime;
                m.TimeUnit = n.TimeUnit;
                m.NotifySendMail = n.NotifySendMail;
                m.SkipForSameUser = n.SkipForSameUser;
                m.RejectProcedure = n.RejectProcedure;
                m.BodyField = n.BodyField;
                m.MailApproveLevel = n.MailApproveLevel;
            }
            else if (wfActivity is IFLDetailsActivity)
            {
                IFLDetailsActivity m = (IFLDetailsActivity)activity;
                IFLDetailsActivity n = (IFLDetailsActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.DetailsTableName = n.DetailsTableName;
                m.ParallelField = n.ParallelField;
                m.SendToMasterField = n.SendToMasterField;
                m.ParallelMode = n.ParallelMode;
                m.ParallelRate = n.ParallelRate;
                m.RelationKeys = n.RelationKeys;
                m.SendToField = n.SendToField;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FLNavigatorField = n.FLNavigatorField;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;

                m.ExtApproveID = n.ExtApproveID;
                m.ExtGroupField = n.ExtGroupField;
                m.ExtValueField = n.ExtValueField;

                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;

                m.AllowSendBack = n.AllowSendBack;

                m.PlusApprove = n.PlusApprove;
                m.PlusApproveReturn = n.PlusApproveReturn;


                #region  --- 修改为动态---

                //if (_hostDataSet == null)
                //{
                //    _hostDataSet = HostTable.GetHostDataSet(this, _keyValues, _clientInfo);
                //}

                //Activity temp = FLInstance.GetActivityByXoml(_flDefinitionFile, string.Empty);
                //IFLRootActivity rootActivity = (IFLRootActivity)temp;

                //DataSet detailsDataSet = HostTable.GetDetailsDataSet(_hostDataSet, rootActivity.Keys, n.DetailsTableName, n.RelationKeys, _clientInfo);
                //string parallelField = n.ParallelField;
                //string sendToField = n.SendToField;

                //if (detailsDataSet != null && detailsDataSet.Tables.Count != 0 && detailsDataSet.Tables[0].Rows.Count != 0)
                //{
                //    int i0 = 1;
                //    int i1 = 1;
                //    int i2 = 1;

                //    FLParallelActivity parallelActivity = null;
                //    foreach (DataRow row in detailsDataSet.Tables[0].Rows)
                //    {
                //        FLStandActivity stand = new FLStandActivity();

                //        //stand.Name = n.Name + "_s" + i0.ToString();
                //        stand.Name = n.Name + "_" + i0.ToString();
                //        i0++;

                //        stand.Description = ((FLDetails)n).Description;
                //        stand.Enabled = ((FLDetails)n).Enabled;

                //        stand.ExpTime = ((FLDetails)n).ExpTime;
                //        stand.FLNavigatorMode = ((FLDetails)n).FLNavigatorMode;
                //        stand.FormName = ((FLDetails)n).FormName;
                //        stand.WebFormName = ((FLDetails)n).WebFormName;
                //        stand.NavigatorMode = ((FLDetails)n).NavigatorMode;
                //        stand.Parameters = ((FLDetails)n).Parameters;
                //        stand.SendToField = ((FLDetails)n).SendToField;
                //        stand.SendToKind = ((FLDetails)n).SendToKind;
                //        stand.SendToRole = ((FLDetails)n).SendToRole;
                //        stand.TimeUnit = ((FLDetails)n).TimeUnit;
                //        stand.UrgentTime = ((FLDetails)n).UrgentTime;
                //        stand.SendEmail = ((FLDetails)n).SendEmail;
                //        stand.PlusApprove = false;

                //        object sendToId2 = row[sendToField];
                //        if (sendToId2 == null || sendToId2 == DBNull.Value || sendToId2.ToString() == string.Empty)
                //        {
                //            continue;
                //            //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "SendToFieldValueIsNull"), n.DetailsTableName);
                //            //throw new FLException(message);
                //        }
                //        ((ISupportFLDetailsActivity)stand).SendToId2 = sendToId2.ToString();

                //        if (!string.IsNullOrEmpty(parallelField))
                //        {
                //            object isParallel = row[parallelField];
                //            if ((isParallel != null && isParallel != DBNull.Value) &&
                //                (isParallel.ToString().Trim().ToLower() == "y" || isParallel.ToString().Trim().ToLower() == "and"))
                //            {
                //                if (parallelActivity == null)
                //                {
                //                    parallelActivity = new FLParallelActivity();
                //                    parallelActivity.Description = n.ParallelRate > 0 ? string.Format("rate:{0}", n.ParallelRate) : "and";
                //                    parallelActivity.Name = n.Name + "_p" + i2.ToString();
                //                    i2++;

                //                    ((FLActivity)m).ChildFLActivities.Add(parallelActivity);
                //                }

                //                FLSequenceActivity sequenceActivity = new FLSequenceActivity();
                //                sequenceActivity.Name = n.Name + "_se" + i1.ToString();
                //                i1++;
                //                ((FLActivity)parallelActivity).ChildFLActivities.Add(sequenceActivity);


                //                ((FLActivity)sequenceActivity).ChildFLActivities.Add(stand);
                //            }
                //            else
                //            {
                //                parallelActivity = null;
                //                ((FLActivity)m).ChildFLActivities.Add(stand);
                //            }
                //        }
                //        else
                //        {
                //            parallelActivity = null;
                //            ((FLActivity)m).ChildFLActivities.Add(stand);
                //        }
                //    }
                //}

                #endregion
            }
            else if (wfActivity is IFLSubFlowActivity)
            {
                IFLSubFlowActivity m = (IFLSubFlowActivity)activity;
                IFLSubFlowActivity n = (IFLSubFlowActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.IncludeFirstActivity = n.IncludeFirstActivity;
                m.XomlName = n.XomlName;
                m.XomlField = n.XomlField;
                //    m.Name = n.Name;
                //    m.Description = n.Description;
                //    m.Enabled = n.Enabled;

                //    string xomlName = n.XomlName;

                //    IFLActivity temp1 = new FLRootActivity();

                //    FileInfo fileInfo = new FileInfo(_flDefinitionFile);
                //    string file = fileInfo.Directory + @"\" + xomlName;
                //    Activity temp2 = GetActivityByXoml(file, string.Empty);

                //    InitFLActivities(temp1, temp2);

                //    List<string> temp3 = new List<string>();
                //    foreach (FLActivity a in ((FLActivity)temp1).ChildFLActivities)
                //    {
                //        temp3.Add(a.Name);
                //    }

                //    bool isFirst = true;
                //    foreach (string k in temp3)
                //    {
                //        if (isFirst && !n.IncludeFirstActivity)
                //        {
                //            isFirst = false; continue;
                //        }

                //        FLActivity temp4 = ((FLActivity)temp1).GetFLActivityByName(k);
                //        temp4.Enabled = n.Enabled;

                //        ((FLActivity)temp1).ChildFLActivities.Remove(temp4);
                //        ((FLActivity)m).ChildFLActivities.Add(temp4);
                //    }
            }
            else if (wfActivity is IEventWaiting)
            {
                IEventWaiting m = (IEventWaiting)activity;
                IEventWaiting n = (IEventWaiting)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;

                m.AllowSendBack = n.AllowSendBack;

                if (wfActivity is IFLStandActivity)
                {
                    ((IFLStandActivity)m).PlusApprove = ((IFLStandActivity)n).PlusApprove;
                    ((IFLStandActivity)m).PlusApproveReturn = ((IFLStandActivity)n).PlusApproveReturn;
                    ((IFLStandActivity)m).DelayAutoApprove = ((IFLStandActivity)n).DelayAutoApprove;
                }
                else if (wfActivity is IFLApproveActivity)
                {
                    IFLApproveActivity q = (IFLApproveActivity)activity;
                    IFLApproveActivity p = (IFLApproveActivity)wfActivity;

                    ((IFLApproveActivity)q).PlusApprove = ((IFLApproveActivity)p).PlusApprove;
                    ((IFLApproveActivity)q).PlusApproveReturn = ((IFLApproveActivity)p).PlusApproveReturn;
                    ((IFLApproveActivity)q).DelayAutoApprove = ((IFLApproveActivity)p).DelayAutoApprove;

                    List<IFLApproveBranchActivity> list = p.GetApproveRights();
                    foreach (IFLApproveBranchActivity a in list)
                    {
                        FLApproveBranchActivity approveBranch = new FLApproveBranchActivity();
                        approveBranch.Grade = a.Grade;
                        approveBranch.Expression = a.Expression;
                        approveBranch.Name = activity.Name + "-" + a.Name;

                        approveBranch.Description = m.Description;
                        approveBranch.ExpTime = m.ExpTime;
                        approveBranch.FLNavigatorMode = m.FLNavigatorMode;
                        approveBranch.FormName = m.FormName;
                        approveBranch.WebFormName = m.WebFormName;
                        approveBranch.NavigatorMode = m.NavigatorMode;
                        approveBranch.Parameters = m.Parameters;
                        approveBranch.SendToField = m.SendToField;
                        approveBranch.SendToKind = m.SendToKind;
                        approveBranch.SendToRole = m.SendToRole;
                        approveBranch.SendToUser = m.SendToUser;
                        approveBranch.TimeUnit = m.TimeUnit;
                        approveBranch.UrgentTime = m.UrgentTime;
                        approveBranch.SendEmail = m.SendEmail;
                        approveBranch.AllowSendBack = m.AllowSendBack;

                        approveBranch.ParentActivity = activity.Name;
                        activity.AddFLActivity(approveBranch);
                    }
                }
            }
            else if (wfActivity is IfElseActivity)
            {
                IFLIfElseActivity m = (IFLIfElseActivity)activity;
                IfElseActivity n = (IfElseActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (wfActivity is IfElseBranchActivity)
            {
                IFLIfElseBranchActivity m = (IFLIfElseBranchActivity)activity;
                IfElseBranchActivity n = (IfElseBranchActivity)wfActivity;

                m.Name = n.Name;
                m.Condition = n.Description == null ? string.Empty : n.Description.ToString();
                m.Description = n.Description;
                m.Enabled = n.Enabled;
                //m.Condition = n.Condition == null ? string.Empty : n.Condition.ToString();
            }
            else if (wfActivity is ParallelActivity)
            {
                IFLParallelActivity m = (IFLParallelActivity)activity;
                ParallelActivity n = (ParallelActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (wfActivity is SequenceActivity)
            {
                IFLSequenceActivity m = (IFLSequenceActivity)activity;
                SequenceActivity n = (SequenceActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;
            }
            else if (wfActivity is IFLNotifyActivity)
            {
                IFLNotifyActivity m = (IFLNotifyActivity)activity;
                IFLNotifyActivity n = (IFLNotifyActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;
            }
            else if (wfActivity is IFLProcedureActivity)
            {
                IFLProcedureActivity m = (IFLProcedureActivity)activity;
                IFLProcedureActivity n = (IFLProcedureActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ErrorLog = n.ErrorLog;
                m.ErrorToRole = n.ErrorToRole;
                m.MethodName = n.MethodName;
                m.ModuleName = n.ModuleName;
            }
            else if (wfActivity is IFLValidateActivity)
            {
                IFLValidateActivity m = (IFLValidateActivity)activity;
                IFLValidateActivity n = (IFLValidateActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Expression = n.Expression;
                m.Message = n.Message;
            }
            else if (wfActivity is IFLHyperLinkActivity)
            {
                IFLHyperLinkActivity m = (IFLHyperLinkActivity)activity;
                IFLHyperLinkActivity n = (IFLHyperLinkActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Parameters = n.Parameters;
            }
            else if (wfActivity is IFLQueryActivity)
            {
                IFLQueryActivity m = (IFLQueryActivity)activity;
                IFLQueryActivity n = (IFLQueryActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.Parameters = n.Parameters;
            }
            else if (wfActivity is IFLRejectActivity)
            {
                IFLRejectActivity m = (IFLRejectActivity)activity;
                IFLRejectActivity n = (IFLRejectActivity)wfActivity;

                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;

                m.ExpTime = n.ExpTime;
                m.FLNavigatorMode = n.FLNavigatorMode;
                m.FormName = n.FormName;
                m.WebFormName = n.WebFormName;
                m.NavigatorMode = n.NavigatorMode;
                m.Parameters = n.Parameters;
                m.SendToField = n.SendToField;
                m.SendToKind = n.SendToKind;
                m.SendToRole = n.SendToRole;
                m.SendToUser = n.SendToUser;
                m.TimeUnit = n.TimeUnit;
                m.UrgentTime = n.UrgentTime;
                m.SendEmail = n.SendEmail;
            }
            else if (wfActivity is IFLGotoActivity)
            {
                IFLGotoActivity m = (IFLGotoActivity)activity;
                IFLGotoActivity n = (IFLGotoActivity)wfActivity;
                m.Name = n.Name;
                m.Description = n.Description;
                m.Enabled = n.Enabled;
                m.ActivityName = n.ActivityName;
            }
            else
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "NotSupportActivityType"), wfActivity.GetType().Name);
                throw new FLException(message);
            }

            if (wfActivity is CompositeActivity)
            {
                CompositeActivity compositeActivity = (CompositeActivity)wfActivity;
                foreach (Activity child in compositeActivity.Activities)
                {
                    IFLActivity a = null;
                    if (child is IFLSubFlowActivity)
                    {
                        //a = new FLSequenceActivity();
                        a = new FLSubFlowActivity();
                    }
                    else if (child is IFLDetailsActivity)
                    {
                        //a = new FLSequenceActivity();
                        a = new FLDetailsActivity();
                    }
                    else if (child is IFLStandActivity)
                    {
                        a = new FLStandActivity();
                    }
                    else if (child is IFLApproveActivity)
                    {
                        a = new FLApproveActivity();
                    }
                    else if (child is IfElseActivity)
                    {
                        a = new FLIfElseActivity();
                    }
                    else if (child is IfElseBranchActivity)
                    {
                        a = new FLIfElseBranchActivity();
                    }
                    else if (child is ParallelActivity)
                    {
                        a = new FLParallelActivity();
                    }
                    else if (child is SequenceActivity)
                    {
                        a = new FLSequenceActivity();
                    }
                    else if (child is IFLNotifyActivity)
                    {
                        a = new FLNotifyActivity();
                    }
                    else if (child is IFLProcedureActivity)
                    {
                        a = new FLProcedureActivity();
                    }
                    else if (child is IFLValidateActivity)
                    {
                        a = new FLValidateActivity();
                    }
                    else if (child is IFLHyperLinkActivity)
                    {
                        a = new FLHyperLinkActivity();
                    }
                    else if (child is IFLQueryActivity)
                    {
                        a = new FLQueryActivity();
                    }
                    else if (child is IFLRejectActivity)
                    {
                        a = new FLRejectActivity();
                    }
                    else if (child is IFLGotoActivity)
                    {
                        a = new FLGotoActivity();
                    }
                    else
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "NotSupportActivityType"), wfActivity.GetType().Name);
                        throw new FLException(message);
                    }

                    activity.AddFLActivity(a);
                    InitFLActivities(a, child);
                }
            }
        }

        private List<object> GetCurrentPathList(FLActivity currentFLActivity, List<object> path)
        {
            if (currentFLActivity == null)
            {
                return path;
            }
            foreach (var p in path)
            {
                if (p is string && p.ToString() == currentFLActivity.Name)
                {
                    return path;
                }
                else if (p is List<object>)
                {
                    return GetCurrentPathList(currentFLActivity, p as List<object>);        
                }
            }
            return path;
        }

        private bool IsSubParallel(FLActivity nextFLActivity, FLActivity currentFLActivity)
        {
            if (currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.UpperParallel))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(nextFLActivity.UpperParallel))
            {
                if (nextFLActivity.UpperParallel == currentFLActivity.UpperParallel)
                {
                    return true;
                }
                else
                {
                    var parallelActivity = this.RootFLActivity.GetFLActivityByName(nextFLActivity.UpperParallel);
                    if (parallelActivity != null)
                    {
                        return IsSubParallel(parallelActivity, currentFLActivity);
                    }
                }
            }
            return false;
        }

       

        /// <summary>
        /// 添加已执行的节点到Path中
        /// </summary>
        /// <param name="nextFLActivities">下一节点集合</param>
        /// <param name="currentFLActivity">当前节点</param>
        [Obsolete]
        private void AddToPathList(List<FLActivity> nextFLActivities, FLActivity currentFLActivity)
        {
            if (Version == "2.0")
            {
            }
            else
            {
                bool b = false;
                foreach (FLActivity nextFLActivity in nextFLActivities)
                {
                    if (nextFLActivity is IFLValidateActivity)
                    {
                        continue;
                    }
                    if ((currentFLActivity == null) || (nextFLActivity.UpperParallel == string.Empty && currentFLActivity.UpperParallel == string.Empty)
                        || (nextFLActivity.UpperParallel == string.Empty && currentFLActivity.UpperParallel != string.Empty))
                    {
                        #region

                        if (_p.Count >= 1)
                        {
                            if (_p[_p.Count - 1].ToString() != nextFLActivity.Name)
                                _p.Add(nextFLActivity.Name);
                        }
                        else
                        {
                            _p.Add(nextFLActivity.Name);
                        }

                        #endregion
                    }
                    else if (nextFLActivity.UpperParallel != string.Empty && currentFLActivity.UpperParallel != string.Empty
                        && nextFLActivity.UpperParallel == currentFLActivity.UpperParallel)
                    {
                        #region

                        List<object> list = (List<object>)_p[_p.Count - 1];
                        foreach (object o in list)
                        {
                            List<object> list1 = (List<object>)o;
                            if (list1.Exists(
                                     delegate(object s)
                                     {
                                         if (currentFLActivity.Name == s.ToString())
                                             return true;
                                         else
                                             return false;
                                     }
                                     ))
                            {
                                if (list1.Count >= 1)
                                {
                                    if (list1[list1.Count - 1].ToString() != nextFLActivity.Name)
                                        list1.Add(nextFLActivity.Name); break;
                                }
                                else
                                    list1.Add(nextFLActivity.Name); break;
                            }
                        }


                        #endregion
                    }
                    else          // (activity.UpperParallel != string.Empty && previousFLActivity.UpperParallel == string.Empty)
                    {
                        #region

                        List<object> list = null;
                        if (!b)
                        {
                            list = new List<object>();
                            _p.Add(list);
                            b = true;
                        }
                        else
                        {
                            list = (List<object>)_p[_p.Count - 1];
                        }

                        List<object> list1 = new List<object>();
                        list1.Add(nextFLActivity.Name);

                        list.Add(list1);

                        #endregion
                    }
                }
            }
        }

        /// <summary>
        /// 取得流程定义XOML
        /// </summary>
        /// <returns></returns>
        public XmlDocument GetFLDefinitionXml()
        {
            if (_flDefinitionXmlString == null || _flDefinitionXmlString == string.Empty)
            {
                InitFLDefinitionXmlString();
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_flDefinitionXmlString);

            return doc;
        }

        public XmlDocument GetFLDefinitionXoml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(_flDefinitionFile);
            return doc;
        }

        /// <summary>
        /// 初始化流程定义的XML文档的节点
        /// </summary>
        private void InitFLDefinitionXmlNodes()
        {
            XmlDocument doc = GetFLDefinitionXml();
            XmlNode root = doc.ChildNodes[1];

            InitFLDefinitionXmlNodes(root);
        }

        /// <summary>
        /// 初始化流程定义的XML文档的节点
        /// </summary>
        /// <param name="rootNode">XML文档的根节点</param>
        private void InitFLDefinitionXmlNodes(XmlNode rootNode)
        {
            if (_flDefinitionXmlNodes.ContainsKey(rootNode.Attributes["Name"].Value))
            {
                _flDefinitionXmlNodes[rootNode.Attributes["Name"].Value] = rootNode;
            }
            else
            {
                _flDefinitionXmlNodes.Add(rootNode.Attributes["Name"].Value, rootNode);
            }

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                InitFLDefinitionXmlNodes(node);
            }
        }

        /// <summary>
        /// 计算运算符的优先级
        /// </summary>
        /// <param name="opr">String of operator</param>
        /// <returns>Integer of priority</returns>
        private int PriOp(string opr)
        {
            switch (opr)
            {
                case "||": return 1;
                case "&&": return 2;
            }
            return 0;
        }


        /// <summary>
        /// 判断条件
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>条件是否成立</returns>
        private bool JudgeCondition(string condition)
        {
            if (string.IsNullOrEmpty(condition))
            {
                return false;
            }

            #region 分割
            string methodPattern = @"(?<=&&\s*|\|\|\s*|\(\s*|^\s*)\w+\.\w+\(\s*\)";
            string operatorPattern = @"&&|\|\||\(|\)";
            string[] list = Regex.Split(condition, methodPattern);//匹配函数
            MatchCollection matches = Regex.Matches(condition, methodPattern);
            List<string> listConditon = new List<string>();
            string subString = string.Empty;
            for (int i = 0; i < list.Length; i++) //要确保引号成对
            {
                subString += list[i];
                if (subString.Split('"').Length % 2 == 0)
                {
                    if (i < matches.Count)
                    {
                        subString += matches[i].Value;
                        continue;
                    }
                    else
                    {
                        ThrowBadConditionException(condition);//引号不匹配
                    }
                }
                else
                {
                    string[] subList = Regex.Split(subString, operatorPattern);//匹配括号与非
                    MatchCollection subMatches = Regex.Matches(subString, operatorPattern);
                    string subValue = string.Empty;
                    for (int j = 0; j < subList.Length; j++) //要确保引号成对
                    {
                        subValue += subList[j];
                        if (subValue.Split('"').Length % 2 == 0)
                        {
                            if (j < subMatches.Count)
                            {
                                subValue += subMatches[j].Value;
                                continue;
                            }
                            else
                            {
                                ThrowBadConditionException(condition);//引号不匹配
                            }
                        }
                        else
                        {
                            if (subValue.Trim().Length > 0)
                            {
                                listConditon.Add(subValue.Trim());
                            }
                            if (j < subMatches.Count)
                            {
                                listConditon.Add(subMatches[j].Value);
                            }
                            subValue = string.Empty;
                        }
                    }

                    if (i < matches.Count)
                    {
                        listConditon.Add(matches[i].Value);
                    }
                    subString = string.Empty;
                }
            }
            listConditon.Add("#");//加入结尾符号
            #endregion

            #region 转换后缀表达式(结束符还没加)
            List<string> RPNList = new List<string>();
            Stack<string> stackOperator = new Stack<string>();
            foreach (string str in listConditon)
            {
                if (str == "(")
                {
                    stackOperator.Push(str);
                }
                else if (str == ")")
                {
                    while (true)
                    {
                        if (stackOperator.Count == 0)
                        {
                            ThrowBadConditionException(condition);//括号不匹配
                        }
                        string value = stackOperator.Pop();
                        if (value == "(")
                        {
                            break;
                        }
                        else
                        {
                            RPNList.Add(value);
                        }
                    }
                }
                else if (str == "&&" || str == "||")
                {
                    while (stackOperator.Count > 0 && PriOp(stackOperator.Peek()) >= PriOp(str))
                    {
                        RPNList.Add(stackOperator.Pop());
                    }
                    //if (stackOperator.Count > 0 && stackOperator.Peek() != "(")
                    //{
                    //    RPNList.Add(stackOperator.Pop());
                    //}
                    stackOperator.Push(str);
                }
                else if (str == "#")
                {
                    while (true)
                    {
                        if (stackOperator.Count == 0)
                        {
                            break;
                        }
                        string value = stackOperator.Pop();
                        if (value == "(")
                        {
                            ThrowBadConditionException(condition);//括号不匹配
                        }
                        else
                        {
                            RPNList.Add(value);
                        }
                    }
                }
                else
                {
                    RPNList.Add(str);
                }
            }
            #endregion

            #region 计算
            Stack<string> stackValue = new Stack<string>();
            foreach (string str in RPNList)
            {
                if (str == "&&" || str == "||")
                {
                    if (stackValue.Count < 2)
                    {
                        ThrowBadConditionException(condition);//右括号不匹配
                    }
                    else
                    {
                        string value2 = stackValue.Pop();
                        string value1 = stackValue.Pop();
                        if (str == "&&")
                        {
                            bool result = JudgeSubCondition(value1) && JudgeSubCondition(value2);
                            stackValue.Push(result.ToString());
                        }
                        else if (str == "||")
                        {
                            bool result = JudgeSubCondition(value1) || JudgeSubCondition(value2);
                            stackValue.Push(result.ToString());
                        }
                    }
                }
                else
                {
                    stackValue.Push(str);
                }
            }
            #endregion

            if (stackValue.Count != 1)
            {
                ThrowBadConditionException(condition);//操作符不匹配
            }
            else
            {
                return JudgeSubCondition(stackValue.Pop());
            }
            return false;
        }

        private string validMessage = string.Empty;

        private bool JudgeSubCondition(string condition)
        {
            string operatorPattern = @">(?=[^=])|<(?=[^=])|=(?=[^=])|!=|>=|<=|==";
            string[] list = Regex.Split(condition, operatorPattern);//匹配比较符
            MatchCollection matches = Regex.Matches(condition, operatorPattern);
            List<string> listConditon = new List<string>();
            string subString = string.Empty;
            string strOperater = string.Empty;
            for (int i = 0; i < list.Length; i++) //要确保引号成对
            {
                subString += list[i];
                if (subString.Split('"').Length % 2 == 0)
                {
                    if (i < matches.Count)
                    {
                        subString += matches[i].Value;
                        continue;
                    }
                    else
                    {
                        ThrowBadConditionException(condition);//引号不匹配
                    }
                }
                else
                {
                    listConditon.Add(subString.Trim());
                    if (i < matches.Count && string.IsNullOrEmpty(strOperater))
                    {
                        strOperater = matches[i].Value;
                    }
                    subString = string.Empty;
                }
            }

            //if (_hostDataSet == null)
            //{
                _hostDataSet = HostTable.GetHostDataSet(this, _keyValues, _clientInfo);
            //}
            DataRow row = _hostDataSet.Tables[0].Rows[0];
            if (listConditon.Count == 1)
            {
                string value = listConditon[0];
                if (string.IsNullOrEmpty(value) || string.Compare(value, "false", true) == 0)
                {
                    return false;
                }
                else if (string.Compare(value, "true", true) == 0)
                {
                    return true;
                }
                else
                {
                    string methodPattern = @"\w+\.\w+(?=\(\s*\))";
                    Match match = Regex.Match(value, methodPattern);//匹配函数名
                    if (match.Success)
                    {
                        object returnValue = CallServerMethod(match.Value, new object[] { row, (int)_flDirection });
                        if (returnValue is object[] && ((object[])returnValue)[1] is Boolean)
                        {
                            if (((object[])returnValue).Length > 2)
                            {
                                validMessage = ((object[])returnValue)[2].ToString();
                            }

                            return (bool)(((object[])returnValue)[1]);
                        }
                        else
                        {
                            string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ServerMethodNotReturnBoolean"), match.Value);
                            throw new FLException(message);
                        }
                    }
                    else
                    {
                        ThrowBadConditionException(condition);
                    }
                }
            }
            else if (listConditon.Count == 2)
            {
                string column = listConditon[0];
                if (column.Length == 0)
                {
                    ThrowBadConditionException(condition);
                }
                if (!row.Table.Columns.Contains(column))
                {
                    string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "TableNotContainsColumn"), row.Table.TableName, column);
                    throw new FLException(message);
                }
                else
                {
                    object value = row[column];
                    Type columnType = row.Table.Columns[column].DataType;

                    string compareValue = listConditon[1];
                    object objValue = null;
                    if ((columnType == typeof(string) || columnType == typeof(DateTime)))//去掉compareValue中的引号
                    {
                        if ((compareValue.StartsWith("\"") && compareValue.EndsWith("\"")) || (compareValue.StartsWith("'") && compareValue.EndsWith("'")))
                        {
                            if (compareValue.Length >= 2)
                            {
                                objValue = compareValue.Substring(1, compareValue.Length - 2);
                                if (columnType == typeof(DateTime))
                                {
                                    objValue = Convert.ToDateTime(objValue);
                                }
                            }
                            else
                            {
                                objValue = string.Empty;
                                if (columnType == typeof(DateTime))
                                {
                                    ThrowBadConditionException(condition);
                                }
                            }
                        }
                        else if (compareValue.StartsWith("\"") || compareValue.EndsWith("\"")|| compareValue.StartsWith("'") || compareValue.EndsWith("'"))
                        {
                            ThrowBadConditionException(condition);
                        }
                        else
                        {
                            if (row.Table.Columns.Contains(compareValue))
                            {
                                objValue = row[compareValue];
                            }
                            else
                            {
                                string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "TableNotContainsColumn"), row.Table.TableName, compareValue);
                                throw new FLException(message);
                            }
                        }
                    }
                    else
                    {
                        if (compareValue.Length == 0)
                        {
                            ThrowBadConditionException(condition);
                        }
                        try
                        {
                            objValue = Convert.ChangeType(compareValue, columnType);
                        }
                        catch
                        {
                            if (row.Table.Columns.Contains(compareValue))
                            {
                                objValue = row[compareValue];
                            }
                            else
                            {
                                string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "TableNotContainsColumn"), row.Table.TableName, compareValue);
                                throw new FLException(message);
                            }
                        }
                    }
                    if (value == null || value.Equals(DBNull.Value))
                    {
                        if (columnType == typeof(string))
                        {
                            value = string.Empty;
                        }
                        else if (objValue.Equals(DBNull.Value) && strOperater == "==")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    if (strOperater != "=")
                    {
                        int result = ((IComparable)value).CompareTo(objValue);
                        switch (strOperater)
                        {
                            case ">": return result > 0;
                            case "<": return result < 0;
                            case "!=": return result != 0;
                            case "==": return result == 0;
                            case ">=": return result >= 0;
                            case "<=": return result <= 0;
                            default: return false;
                        }
                    }
                    else if (columnType == typeof(string))
                    {
                        return ((string)value).Contains(objValue.ToString());
                    }
                    return false;
                }
            }
            else
            {
                ThrowBadConditionException(condition);//比较符过多
            }
            return false;
        }

        private void ThrowBadConditionException(string condition)
        {
            string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ConditionBad"), condition);
            throw new FLException(message);
        }

        /// <summary>
        /// 调用ServerMethod
        /// </summary>
        /// <param name="methodName">ServerMethod的名称</param>
        /// <param name="parameters">ServerMethod的参数</param>
        /// <returns>ServerMethod的返回值</returns>
        public object CallServerMethod(string methodName, object[] parameters)
        {
            string[] ss = methodName.Split(".".ToCharArray());
            if (ss.Length != 2)
            {
                string message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "WrongExpression"), methodName);
                throw new FLException(message);
            }

            string a = ss[0].Trim();
            string b = ss[1].Trim();

            EEPRemoteModule remoteModule = new EEPRemoteModule();

            return remoteModule.CallMethod(_clientInfo, a, b, parameters);
        }

        /// <summary>
        /// 设置ClientInfo
        /// </summary>
        /// <param name="clientInfo">ClientInfo</param>
        public void SetClientInfo(object[] clientInfo)
        {
            _clientInfo = clientInfo;
        }

        /// <summary>
        /// 设置流程流转方向
        /// </summary>
        /// <param name="flDirection"></param>
        public void SetFLDirection(FLDirection flDirection)
        {
            _flDirection = flDirection;
        }

        /// <summary>
        /// 设置宿主表的筛选条件
        /// </summary>
        /// <param name="keyValues">筛选条件</param>
        public void SetKeyValues(object[] keyValues)
        {
            _keyValues = keyValues;
        }

        /// <summary>
        /// 取得ClientInfo
        /// </summary>
        /// <returns></returns>
        public object[] GetClientInfo()
        {
            return _clientInfo;
        }

        /// <summary>
        /// 取得宿主表的筛选条件
        /// </summary>
        /// <returns></returns>
        public object[] GetKeyValues()
        {
            return _keyValues;
        }

        /// <summary>
        /// 取得Web路径
        /// </summary>
        /// <returns></returns>
        public string GetWebUrl()
        {
            return _webUrl;
        }

        /// <summary>
        /// 设置流程运行时
        /// </summary>
        /// <param name="runtime">流程运行时</param>
        public void SetFLRuntime(FLRuntime runtime)
        {
            _flRuntime = runtime;
        }

        /// <summary>
        /// 设置Web的路径
        /// </summary>
        /// <param name="url">Web路径</param>
        public void SetWebUrl(string url)
        {
            _webUrl = url;
        }

        /// <summary>
        /// 设置创建者
        /// </summary>
        /// <param name="creator"></param>
        public void SetCreator(string creator, string role)
        {
            _creator = creator;
            _createRole = role;
        }

        // ---------------------------------------------------------------------------
        // Event

        public delegate void __CreatedEventHandler(object sender, __FLInstanceCreatedEventArgs e);
        public delegate void __SubmitEventHandler(object sender, __FLInstanceSubmitEventArgs e);
        public delegate void __ApproveEventHandler(object sender, __FLInstanceApproveEventArgs e);
        public delegate void __ReturnEventHandler(object sender, __FLInstanceReturnEventArgs e);
        public delegate void __RejectEventHandler(object sender, __FLInstanceRejectEventArgs e);
        public delegate void __NotifyEventHandler(object sender, __FLInstanceNotifyEventArgs e);
        public delegate void __RetakeEventHandler(object sender, __FLInstanceRetakeEventArgs e);
        public delegate void __PlusApproveEventHandler(object sender, __FLInstancePlusApproveEventArgs e);
        public delegate void __PlusReturnEventHandler(object sender, __FLInstancePlusReturnEventArgs e);
        public delegate void __PauseEventHandler(object sender, __FLInstancePauseEventArgs e);

        public void OnCreated(object sender, __FLInstanceCreatedEventArgs value)
        {
            if (__Created != null)
            {
                __Created(sender, value);
            }
        }

        public void OnSubmit(object sender, __FLInstanceSubmitEventArgs value)
        {
            if (__Submit != null)
            {
                __Submit(sender, value);
            }
        }

        public void OnApprove(object sender, __FLInstanceApproveEventArgs value)
        {
            if (__Approve != null)
            {
                __Approve(sender, value);
            }
        }

        public void OnReturn(object sender, __FLInstanceReturnEventArgs value)
        {
            if (__Return != null)
            {
                __Return(sender, value);
            }
        }

        public void OnReject(object sender, __FLInstanceRejectEventArgs value)
        {
            if (__Reject != null)
            {
                __Reject(sender, value);
            }
        }

        public void OnNotify(object sender, __FLInstanceNotifyEventArgs value)
        {
            if (__Notify != null)
            {
                __Notify(sender, value);
            }
        }

        public void OnRetake(object sender, __FLInstanceRetakeEventArgs value)
        {
            if (__Retake != null)
            {
                __Retake(sender, value);
            }
        }

        public void OnPlusApprove(object sender, __FLInstancePlusApproveEventArgs value)
        {
            if (__PlusApprove != null)
            {
                __PlusApprove(sender, value);
            }
        }

        public void OnPlusReturn(object sender, __FLInstancePlusReturnEventArgs value)
        {
            if (__PlusReturn != null)
            {
                __PlusReturn(sender, value);
            }
        }

        public void OnPause(object sender, __FLInstancePauseEventArgs value)
        {
            if (__Pause != null)
            {
                __Pause(sender, value);
            }
        }

        public event __CreatedEventHandler __Created;
        public event __SubmitEventHandler __Submit;
        public event __ApproveEventHandler __Approve;
        public event __ReturnEventHandler __Return;
        public event __RejectEventHandler __Reject;
        public event __NotifyEventHandler __Notify;
        public event __RetakeEventHandler __Retake;
        public event __PlusApproveEventHandler __PlusApprove;
        public event __PlusReturnEventHandler __PlusReturn;
        public event __PauseEventHandler __Pause;


        private List<FLActivity> GetExecutedActivities(FLActivity activity)
        {
            return GetExecutedActivities(activity, null);
        }

        private List<FLActivity> GetExecutedActivities(FLActivity activity, List<FLActivity> previousExecutedActivities)
        {
            List<FLActivity> executedActivities = new List<FLActivity>();
            foreach (var childActivity in activity.ChildFLActivities)
            {
                if (childActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)
                {
                    if (previousExecutedActivities != null && previousExecutedActivities.Contains(childActivity))
                    { }
                    else
                    {
                        executedActivities.Add(childActivity);
                    }
                }
                executedActivities.AddRange(GetExecutedActivities(childActivity, previousExecutedActivities));
            }
            return executedActivities;
        }


        // ---------------------------------------------------------------------------

        /// <summary>
        /// 提交流程
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">权限Id</param>
        /// <param name="isUrgent">是否紧急</param>
        /// <param name="tableFlowFlag">宿主表中的FlowFlag字段值</param>
        public void Submit(string userId, string roleId, bool isUrgent, string tableFlowFlag)
        {
            _isPlusApprove = false;
            _isPause = false;
            _isRetake = false;
            _isReturn = false;
            if (_rootFLActivity.ExecutionStatus != FLActivityExecutionStatus.Initialized)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLInstanceIsSummited");
                throw new FLException(2, message);
            }

            _flflag = tableFlowFlag == "Z" ? 'C' : 'N';
            _v = true;
            _flDirection = FLDirection.GoToNext;
            _currentFLActivity = _rootFLActivity.ChildFLActivities[0];
            _nextFLActivities = new List<FLActivity>();

            var previousExecutedActivities = new List<FLActivity>();
            if (Version == "2.0")
            {
                previousExecutedActivities = GetExecutedActivities(this.RootFLActivity);
            }
            ((IEventWaitingExecute)_currentFLActivity).Execute(userId, roleId, isUrgent);
            _sendFromFLActivity = _currentFLActivity;

            //---------------------------------------------------------
            if (_flDefinitionXmlNodes == null)
            {
                _flDefinitionXmlNodes = new Hashtable();
                InitFLDefinitionXmlNodes();
            }
          
            GetNextFLActivities(_currentFLActivity, _nextFLActivities);
            foreach (FLActivity activity in _nextFLActivities)
            {
                activity.InitExecStatus();

                if (Version == "2.0")
                {
                    activity.PreviousActivity = _currentFLActivity;
                }
            }
            if (Version == "2.0")
            {
                _currentFLActivity.NextActivities = _nextFLActivities;
                _currentFLActivity.ExecutedActivities = GetExecutedActivities(this.RootFLActivity, previousExecutedActivities);
                LastActivity = _currentFLActivity.Name;
            }
            //---------------------------------------------------------
            List<FLActivity> list = new List<FLActivity>();
            list.Add(_currentFLActivity);
            AddToPathList(list, null);
            if (!_v)
            {
                //roll back current
                _currentFLActivity.InitExecStatus();
                //
                Record();
                object o = this.FLRuntime.GetService(typeof(FLPersistenceService));

                if (o != null)
                {
                    FLPersistenceService flPersistenceService = (FLPersistenceService)o;
                    flPersistenceService.PersistenceFL(this, _clientInfo);
                }

            }
            else
            {

                AddToPathList(_nextFLActivities, _currentFLActivity);
                Record();
            }

            // OnSubmit(this, new __FLInstanceSubmitEventArgs());
        }

        /// <summary>
        /// 提交流程
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">权限Id</param>
        /// <param name="isUrgent">是否紧急</param>
        public void Submit(string userId, string roleId, bool isUrgent)
        {
            Submit(userId, roleId, isUrgent, string.Empty);
        }

        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="previousFLActivityName">上一节点</param>
        /// <param name="currentFLActivityName">当前节点</param>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">权限Id</param>
        /// <param name="isUrgent">是否紧急</param>
        public void Approve(string previousFLActivityName, string currentFLActivityName, string userId, string roleId, bool isUrgent)
        {
            _isPlusApprove = false;
            _isPause = false;
            _isRetake = false;
            _isReturn = false;
            _flflag = 'P';
            _v = true;
            _flDirection = FLDirection.GoToNext;
            _previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _nextFLActivities = new List<FLActivity>();

            if (_currentFLActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLSetpIsApprovedOrReturned");
                throw new FLException(2, message);
            }

            if (_previousFLActivity != null && _previousFLActivity.ExecutionStatus != FLActivityExecutionStatus.Executed && previousFLActivityName != currentFLActivityName)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLSetpIsApprovedOrReturned");
                throw new FLException(2, message);
            }
            var previousExecutedActivities = new List<FLActivity>();
            if (Version == "2.0")
            {
                previousExecutedActivities = GetExecutedActivities(this.RootFLActivity);
            }
            ((IEventWaitingExecute)_currentFLActivity).Execute(userId, roleId, isUrgent);

            _sendFromFLActivity = _currentFLActivity;

            //---------------------------------------------------------
            if (_flDefinitionXmlNodes == null)
            {
                _flDefinitionXmlNodes = new Hashtable();
                InitFLDefinitionXmlNodes();
            }
            GetNextFLActivities(_currentFLActivity, _nextFLActivities);
            foreach (FLActivity activity in _nextFLActivities)
            {
                activity.InitExecStatus();
                if (Version == "2.0")
                {
                    activity.PreviousActivity = _currentFLActivity;
                }
            }
            if (Version == "2.0")
            {
                _currentFLActivity.NextActivities = _nextFLActivities;
                _currentFLActivity.ExecutedActivities = GetExecutedActivities(this.RootFLActivity, previousExecutedActivities);
                LastActivity = _currentFLActivity.Name;
            }
            //---------------------------------------------------------

            AddToPathList(_nextFLActivities, _currentFLActivity);
            Record();

            // OnApprove(this, new __FLInstanceApproveEventArgs());
        }

        /// <summary>
        /// 退回流程
        /// </summary>
        /// <param name="currentFLActivityName">当前节点</param>
        /// <param name="nextFLActivityName">下一节点</param>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">权限Id</param>
        /// <param name="isUrgent">是否紧急</param>
        public void Return(string currentFLActivityName, string nextFLActivityName, string userId, string roleId, bool isUrgent)
        {
            _isPlusApprove = false;
            _isPause = false;
            _isRetake = false;
            _isReturn = true;
            _v = true;
            _flDirection = FLDirection.GoToBack;
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(nextFLActivityName);
            _nextFLActivities = new List<FLActivity>();
            _wl = new List<string>();

            if (_currentFLActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLSetpIsApprovedOrReturned");
                throw new FLException(2, message);
            }

            ((IEventWaitingExecute)_currentFLActivity).Return(userId, roleId, isUrgent);

            _previousFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _sendFromFLActivity = _currentFLActivity;

            GetPreviousFLActivites(_currentFLActivity, _nextFLActivities);//这个方法有问题
            foreach (FLActivity activity in _nextFLActivities)
            {
                activity.InitExecStatus();
            }

            ((IEventWaitingExecute)_currentFLActivity).Execute(userId, roleId, isUrgent);
            Record();

            // OnReturn(this, new __FLInstanceReturnEventArgs());
        }

        /// <summary>
        /// 退回流程
        /// </summary>
        /// <param name="currentFLActivityName">当前节点</param>
        /// <param name="nextFLActivityName">下一节点</param>
        /// <param name="userId">用户Id</param>
        /// <param name="roleId">权限Id</param>
        /// <param name="isUrgent">是否紧急</param>
        public void Return2(string currentFLActivityName, string nextFLActivityName, string userId, string roleId, bool isUrgent)
        {
            _isPause = false;
            _isRetake = false;
            _isReturn = true;
            _v = true;
            _flDirection = FLDirection.GoToBack;
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(nextFLActivityName);
            _nextFLActivities = new List<FLActivity>();
            _wl = new List<string>();

            if (_currentFLActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLSetpIsApprovedOrReturned");
                throw new FLException(2, message);
            }

            ((IEventWaitingExecute)_currentFLActivity).Return(userId, roleId, isUrgent);

            _previousFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _sendFromFLActivity = _currentFLActivity;

            GetPreviousFLActivites2(_currentFLActivity, _previousFLActivity, _nextFLActivities);
            foreach (FLActivity activity in _nextFLActivities)
            {
                activity.InitExecStatus();
            }

            ((IEventWaitingExecute)_currentFLActivity).Execute(userId, roleId, isUrgent);
            Record();

            // OnReturn(this, new __FLInstanceReturnEventArgs());
        }

        /// <summary>
        /// 取回流程
        /// </summary>
        public List<FLActivity> Retake()
        {
            _isPlusApprove = false;
            _isPause = false;
            _isRetake = true;
            _isReturn = false;
            _v = true;
            if (Version == "2.0")
            {
                string retakeFLActivityName = this.GetRetakeFLActivityName();
                FLActivity retakeFLActivity = this.RootFLActivity.GetFLActivityByName(retakeFLActivityName);
                ReturnActivity(retakeFLActivity);
                _currentFLActivity = retakeFLActivity.PreviousActivity;
                LastActivity = _currentFLActivity == null ? string.Empty : _currentFLActivity.Name;
                _nextFLActivities = new List<FLActivity>();
                _nextFLActivities.Add(retakeFLActivity);
                return null;
            }
            else
            {
                int i = _nameRecords.Count;
                List<FLActivity> retakeActivities = new List<FLActivity>();
                // ArrayList keys = new ArrayList(_records.Keys);
                if (i > 1)
                {
                    string s = _nameRecords[i - 1].ToString();
                    string currentFLActivityName = s.Split(':')[0];

                    object o = _records[_nameRecords[i - 2]];
                    List<object> record = (List<object>)o;

                    _p = new List<object>((List<object>)record[0]);
                    _currentFLActivity = (FLActivity)record[1];
                    _previousFLActivity = (FLActivity)record[2];
                    _nextFLActivities = (List<FLActivity>)record[3];
                    _setUpperParallels = (List<string>)record[4];
                    _setLocations = (List<string>)record[5];
                    _flDirection = (FLDirection)record[6];
                    _flflag = (char)record[7];

                    if (_flDirection == FLCore.FLDirection.GoToBack)
                    {
                        var activitityName = _previousFLActivity.Name;
                        while (true)
                        {
                            if (_nameRecords.Count == 0)
                            {
                                break;
                            }
                            string n = _nameRecords[_nameRecords.Count - 1].ToString();
                            record = (List<object>)_records[n];
                            _p = new List<object>((List<object>)record[0]);
                            _currentFLActivity = (FLActivity)record[1];
                            _previousFLActivity = (FLActivity)record[2];
                            _nextFLActivities = (List<FLActivity>)record[3];
                            _setUpperParallels = (List<string>)record[4];
                            _setLocations = (List<string>)record[5];
                            _flDirection = (FLDirection)record[6];
                            _flflag = (char)record[7];
                            bool find = false;
                            if (_flDirection == FLCore.FLDirection.GoToNext)
                            {
                                for (int j = 0; j < _nextFLActivities.Count; j++)
                                {
                                    if (_nextFLActivities[j].Name == activitityName)
                                    {
                                        find = true;
                                    }
                                }
                            }
                            if (find)
                            {
                                break;
                            }
                            else
                            {
                                retakeActivities = _nextFLActivities;
                            }

                            _nameRecords.Remove(n);
                            _records.Remove(n);
                        }
                        Hashtable activitiesStutas = (Hashtable)record[8];
                        foreach (object key in activitiesStutas.Keys)
                        {
                            FLActivity activity = _rootFLActivity.GetFLActivityByName(key.ToString());
                            if (activity != null)
                            {
                                FLActivityExecutionStatus status = (FLActivityExecutionStatus)activitiesStutas[key];
                                if (status == FLActivityExecutionStatus.Initialized)
                                    activity.InitExecStatus();
                                else
                                    activity.ExecutionStatus = status;
                            }
                        }
                    }
                    else
                    {
                        retakeActivities = (List<FLActivity>)((List<object>)_records[s])[3];
                        Hashtable activitiesStutas = (Hashtable)record[8];
                        foreach (object key in activitiesStutas.Keys)
                        {
                            FLActivity activity = _rootFLActivity.GetFLActivityByName(key.ToString());
                            if (activity != null)
                            {
                                FLActivityExecutionStatus status = (FLActivityExecutionStatus)activitiesStutas[key];
                                if (status == FLActivityExecutionStatus.Initialized)
                                    activity.InitExecStatus();
                                else
                                    activity.ExecutionStatus = status;
                            }
                        }

                        _nameRecords.Remove(s);
                        _records.Remove(s);
                    }

                    // OnRetake(this, new __FLInstanceRetakeEventArgs());
                }
                else
                {
                    string s = _nameRecords[i - 1].ToString();
                    retakeActivities = (List<FLActivity>)((List<object>)_records[s])[3];

                    Hashtable table = _rootFLActivity.GetAllChildFLActivities();
                    foreach (object value in table.Values)
                    {
                        FLActivity activity = (FLActivity)value;
                        activity.InitExecStatus();
                        //activity.ExecutionStatus = FLActivityExecutionStatus.Initialized;
                    }

                    _nameRecords.Clear();
                    _records.Clear();
                    _p.RemoveAt(_p.Count - 1);
                }
                return retakeActivities;
            }
        }

        private List<string> GetAllPath(List<object> p)
        {
            var path = new List<string>();
            if (p != null)
            {
                foreach (var obj in p)
                {
                    if (obj is string)
                    {
                        path.Add((string)obj);
                    }
                    else
                    {
                        path.AddRange(GetAllPath((List<object>)obj));
                    }
                }
            }
            return path;
        }

        //private void RetakeProcedure(List<object> previousP)
        //{
        //    var path = GetAllPath(_p);
        //    var previousPath = GetAllPath(previousP);
        //    for (int i = path.Count - 1; i >= 0; i--)
        //    {
        //        var p = path[i];
        //        if (!previousPath.Contains(p))
        //        {
        //            var activity = this.RootFLActivity.GetFLActivityByName(p);
        //            if (activity != null && activity is FLProcedureActivity)
        //            {
        //                thi
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 取消流程
        /// </summary>
        public void Reject()
        {
            Reject(string.Empty, false);
        }

        /// <summary>
        /// 取消流程
        /// </summary>
        /// <param name="currentFLActivityName">当前节点</param>
        /// <param name="sendNotifyToAll">是否发送通知</param>
        /// <returns>通知Activity</returns>
        public FLNotifyActivity Reject(string currentFLActivityName, bool sendNotifyToAll)
        {
            _flflag = 'X';
            _v = true;

            if (!sendNotifyToAll)
            {
                return null;
            }

            FLActivity currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            FLNotifyActivity notifyActivity = new FLNotifyActivity();
            notifyActivity.Name = "Notify_" + currentFLActivity.Name;
            notifyActivity.SendToKind = SendToKind.AllRoles;
            notifyActivity.FormName = ((IEventWaiting)currentFLActivity).FormName;
            notifyActivity.WebFormName = ((IEventWaiting)currentFLActivity).WebFormName;
            notifyActivity.UrgentTime = (int)((IEventWaiting)currentFLActivity).UrgentTime;
            notifyActivity.TimeUnit = ((IEventWaiting)currentFLActivity).TimeUnit;
            notifyActivity.NavigatorMode = ((IEventWaiting)currentFLActivity).NavigatorMode;
            notifyActivity.FLNavigatorMode = ((IEventWaiting)currentFLActivity).FLNavigatorMode;
            notifyActivity.UserId = ((IEventWaitingExecute)currentFLActivity).UserId;
            notifyActivity.RoleId = ((IEventWaitingExecute)currentFLActivity).RoleId;
            notifyActivity.SendEmail = ((IFLRootActivity)this.RootFLActivity).NotifySendMail;

            return notifyActivity;

            // OnReject(this, new __FLInstanceRejectEventArgs());
        }

        /// <summary>
        /// 流程通知
        /// </summary>
        /// <param name="currentFLActivityName">当前节点</param>
        /// <returns></returns>
        public FLActivity Notify(string currentFLActivityName)
        {
            _v = true;
            FLActivity currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);

            // OnNotify(this, new __FLInstanceNotifyEventArgs());

            return currentFLActivity;
        }

        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="previousFLActivityName">上一节点</param>
        /// <param name="currentFLActivityName">当前节点</param>
        public void PlusApprove(string previousFLActivityName, string currentFLActivityName)
        {
            _isPlusApprove = true;
            _isReturn = false;
            _v = true;
            _previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _nextFLActivities = new List<FLActivity>();
            _nextFLActivities.Add(_currentFLActivity);

            //任意加签跳过检查
            if ((_previousFLActivity is IFLStandActivity && !((IFLStandActivity)_previousFLActivity).PlusApproveReturn)
                                   || (_previousFLActivity is IFLApproveActivity && !((IFLApproveActivity)_previousFLActivity).PlusApproveReturn))
            {

            }
            else
            {
                if (_previousFLActivity != null && _previousFLActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "FLSetpIsApprovedOrReturned");
                    throw new FLException(2, message);
                }
            }

            // OnNotify(this, new __FLInstanceNotifyEventArgs());
        }

        /// <summary>
        /// 流程加签返回
        /// </summary>
        /// <param name="previousFLActivityName">上一节点</param>
        /// <param name="currentFLActivityName">当前节点</param>
        public void PlusReturn(string previousFLActivityName, string currentFLActivityName)
        {
            _isPlusApprove = false;
            _isReturn = false;
            _v = true;
            _previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _nextFLActivities = new List<FLActivity>();
            _nextFLActivities.Add(_currentFLActivity);

            // OnNotify(this, new __FLInstanceNotifyEventArgs());
        }

        /// <summary>
        /// 流程加签退回
        /// </summary>
        /// <param name="previousFLActivityName">上一节点</param>
        /// <param name="currentFLActivityName">当前节点</param>
        public void PlusReturn2(string previousFLActivityName, string currentFLActivityName)
        {
            _isPlusApprove = false;
            _isReturn = true;
            _v = true;
            _previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
            _currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);
            _nextFLActivities = new List<FLActivity>();
            _nextFLActivities.Add(_currentFLActivity);

            // OnNotify(this, new __FLInstanceNotifyEventArgs());
        }

        /// <summary>
        /// 流程暂停
        /// </summary>
        public void Pause()
        {
            _flflag = 'B';

            _isPlusApprove = false;
            _isPause = true;
            _isRetake = false;
            _isReturn = false;

            _v = true;

            AddToPathList(_nextFLActivities, _currentFLActivity);
        }

        public string LastActivity;

        /// <summary>
        /// 取得取回的Activity
        /// </summary>
        /// <returns></returns>
        public string GetRetakeFLActivityName()
        {
            if (Version == "2.0")
            {
                return LastActivity;
            }
            else
            {
                int i = _nameRecords.Count;
                if (i > 0)
                {
                    string s = _nameRecords[i - 1];
                    return s.Split(':')[0];
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public bool CanRetake
        {
            get
            {
                return true;
                //get last activity direction
                if (_records.Count > 0)
                {
                    List<object> record = (List<object>)_records[_records.Count - 1];
                    FLDirection flDirection = (FLDirection)record[6];


                    if (flDirection == FLDirection.GoToNext)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 记录当前流程信息
        /// </summary>
        [Obsolete]
        private void Record()
        {

            if (Version == "2.0")
            {

            }
            else
            {
                //List<object> _flPathList;
                //FLActivity _currentFLActivity;
                //FLActivity _previousFLActivity;
                //List<FLActivity> _nextFLActivities;
                //List<string> _setUpperParallels;
                //List<string> _setLocations;
                //FLDirection _flDirection;
                //char _flflag;

                object[] obj1 = new object[_p.Count];
                _p.CopyTo(obj1);
                List<object> flPathList = new List<object>(obj1);

                string[] obj2 = new string[_setUpperParallels.Count];
                _setUpperParallels.CopyTo(obj2);
                List<string> setUpperParallels = new List<string>(obj2);

                string[] obj3 = new string[_setLocations.Count];
                _setLocations.CopyTo(obj3);
                List<string> setLocations = new List<string>(obj3);

                List<object> list = new List<object>();

                list.Add(flPathList);                                 //
                list.Add(_currentFLActivity);
                list.Add(_previousFLActivity);
                list.Add(_nextFLActivities);
                list.Add(setUpperParallels);                           //
                list.Add(setLocations);                                //
                list.Add(_flDirection);
                list.Add(_flflag);
                list.Add(GetActivitiesStatus());

                string s = _currentFLActivity.Name + ":" + Guid.NewGuid().ToString();
                _nameRecords.Add(s);
                _records.Add(s, list);
            }
        }

        /// <summary>
        /// 取得Activity的状态
        /// </summary>
        /// <returns></returns>
        private Hashtable GetActivitiesStatus()
        {
            Hashtable activitiesStatus = new Hashtable();
            Hashtable activities = _rootFLActivity.GetAllChildFLActivities();
            foreach (object key in activities.Keys)
            {
                activitiesStatus.Add(key, ((FLActivity)activities[key]).ExecutionStatus);
            }
            return activitiesStatus;
        }

        /// <summary>
        /// 取得下一Activity
        /// </summary>
        /// <param name="flActivity">当前Activity</param>
        /// <returns></returns>
        private FLActivity GetRealNextFLActivity(FLActivity flActivity)
        {
            XmlNode currentNode = (XmlNode)_flDefinitionXmlNodes[flActivity.Name];
            XmlNode nextNode = currentNode.NextSibling;
            if (nextNode != null)
            {
                return _rootFLActivity.GetFLActivityByName(nextNode.Attributes["Name"].Value);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 取得当前Activity的父Activity
        /// </summary>
        /// <param name="flActivity"></param>
        /// <returns></returns>
        private FLActivity GetRealParentFLActivity(FLActivity flActivity)
        {
            XmlNode currentNode = (XmlNode)_flDefinitionXmlNodes[flActivity.Name];
            XmlNode parentNode = currentNode.ParentNode;
            if (parentNode != null)
            {
                return _rootFLActivity.GetFLActivityByName(parentNode.Attributes["Name"].Value);
            }
            else
            {
                return null;
            }
        }

        /* // 此方法支持Parallel的嵌套
        private void SetUpperParallel(FLActivity activity)
        {
            if (!_setUpperParallelFLActivities.Exists(
                    delegate(string s)
                    {
                        if (activity.Name == s)
                            return true;
                        else
                            return false;
                    }   
                ))
            {
                FLActivity parentActivity = GetRealParentFLActivity(activity);
                if (parentActivity != null && !(parentActivity is IFLRootActivity))
                {
                    if (parentActivity is IFLParallelActivity)
                    {
                        if (parentActivity.UpperParallel != null && parentActivity.UpperParallel != string.Empty)
                        {
                            activity.UpperParallel = parentActivity.UpperParallel + "->";
                        }
                        activity.UpperParallel += parentActivity.Name;
                        if (((IFLParallelActivity)parentActivity).Description.ToLower() == "true")
                        {
                            activity.IsUpperParallelAnd = true;
                        }
                        activity.UpperParallelBranch = activity.Name;
                    }
                    else
                    {
                        activity.UpperParallel = parentActivity.UpperParallel;
                        activity.IsUpperParallelAnd = parentActivity.IsUpperParallelAnd;
                        activity.UpperParallelBranch = parentActivity.UpperParallelBranch;
                    }
                }

                _setUpperParallelFLActivities.Add(activity.Name);
            }
        }
        */

        // 此方法不支持Parallel的嵌套
        /// <summary>
        /// 设置当前Activity的上一级的Parallel
        /// </summary>
        /// <param name="activity">当前的Activity</param>
        private void SetUpperParallel(FLActivity activity)
        {
            if (!_setUpperParallels.Exists(
                    delegate(string s)
                    {
                        if (activity.Name == s)
                            return true;
                        else
                            return false;
                    }
                ))
            {
                FLActivity parentFLActivity = GetRealParentFLActivity(activity);
                if (parentFLActivity != null && !(parentFLActivity is IFLRootActivity))
                {
                    if (parentFLActivity is IFLParallelActivity)
                    {
                        activity.UpperParallel = parentFLActivity.Name;
                        if (((IFLParallelActivity)parentFLActivity).Description.ToLower() == "and" || string.IsNullOrEmpty(((IFLParallelActivity)parentFLActivity).Description))
                        {
                            activity.IsUpperParallelAnd = true;
                        }
                        activity.UpperParallelBranch = activity.Name;
                    }
                    else
                    {
                        activity.UpperParallel = parentFLActivity.UpperParallel;
                        activity.IsUpperParallelAnd = parentFLActivity.IsUpperParallelAnd;
                        activity.UpperParallelBranch = parentFLActivity.UpperParallelBranch;
                    }
                }

                _setUpperParallels.Add(activity.Name);
            }
        }

        /// <summary>
        /// 设置当前Activity的路径
        /// </summary>
        /// <param name="activity">当前的Activity</param>
        private void SetLocation(FLActivity activity)
        {
            if (!_setLocations.Exists(
                    delegate(string s)
                    {
                        if (activity.Name == s)
                            return true;
                        else
                            return false;
                    }
                ))
            {
                FLActivity parentFLActivity = GetRealParentFLActivity(activity);
                if (parentFLActivity != null && !(parentFLActivity is IFLRootActivity))
                {
                    if (parentFLActivity is IFLParallelActivity)
                    {
                        activity.Location = parentFLActivity.Location;
                    }
                    else
                    {
                        FLActivity grandparentFLActivity = GetRealParentFLActivity(parentFLActivity);
                        if (grandparentFLActivity != null && !(grandparentFLActivity is IFLRootActivity) && grandparentFLActivity is IFLParallelActivity)
                        {
                            activity.Location = parentFLActivity.Location;
                        }
                        else
                        {
                            if (parentFLActivity.Location != null && parentFLActivity.Location != string.Empty)
                            {
                                activity.Location = parentFLActivity.Location + "->" + parentFLActivity.Name;
                            }
                            else
                            {
                                activity.Location = parentFLActivity.Name;
                            }
                        }
                    }
                }

                _setLocations.Add(activity.Name);
            }
        }

        internal List<FLActivity> GetNextFLActivities(string currentFLActivityName, string userId, string roleId)
        {
            FLActivity currentFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivityName);

            if (currentFLActivity is IEventWaitingExecute)
            {
                ((IEventWaitingExecute)currentFLActivity).Execute(userId, roleId, false);
            }
            _sendFromFLActivity = currentFLActivity;
            _flDirection = FLDirection.GoToNext;
            if (_flDefinitionXmlNodes == null)
            {
                _flDefinitionXmlNodes = new Hashtable();
                InitFLDefinitionXmlNodes();
            }


            List<FLActivity> nextFLActivities = new List<FLActivity>();
            preview = true;
            GetNextFLActivities(currentFLActivity, nextFLActivities);
            preview = false;
            return nextFLActivities;
        }

        private bool preview = false;


        [NonSerialized]
        private bool _isFirstInParallel = false;
        [NonSerialized]
        private string _tempParallel = string.Empty;
        [NonSerialized]
        private string _tempParallelBranch = string.Empty;
        /// <summary>
        /// 取得下一Activity的集合
        /// </summary>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="nextFLActivities">下一Activity的集合</param>
        private void GetNextFLActivities(FLActivity currentFLActivity, List<FLActivity> nextFLActivities)
        {
            SetUpperParallel(currentFLActivity);
            SetLocation(currentFLActivity);

            if (_isFirstInParallel && ((_tempParallel != currentFLActivity.UpperParallel) || (_tempParallelBranch != currentFLActivity.UpperParallelBranch)))
            {
                return;
            }

            if (currentFLActivity is IFLNotifyActivity || currentFLActivity is IFLProcedureActivity)
            {
                if (!nextFLActivities.Exists(
                    delegate(FLActivity flActivity)
                    {
                        if (currentFLActivity.Name == flActivity.Name)
                            return true;
                        else
                            return false;
                    }
                ) && currentFLActivity.Enabled)
                {

                    if (currentFLActivity is IFLProcedureActivity && !preview)
                    {
                        Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, currentFLActivity as IFLProcedureActivity);
                    }

                    nextFLActivities.Add(currentFLActivity);
                }
            }

            if (currentFLActivity is IFLRejectActivity && currentFLActivity.Enabled)
            {
                nextFLActivities.Clear();
                nextFLActivities.Add(currentFLActivity);

                return;
            }

            if (currentFLActivity is IFLGotoActivity && currentFLActivity.Enabled)
            {
                nextFLActivities.Clear();
                nextFLActivities.Add(currentFLActivity);

                return;
            }

            if (currentFLActivity is IFLValidateActivity)
            {
                if (!nextFLActivities.Exists(
                    delegate(FLActivity flActivity)
                    {
                        if (currentFLActivity.Name == flActivity.Name)
                            return true;
                        else
                            return false;
                    }
                ) && currentFLActivity.Enabled && !preview)
                {
                    nextFLActivities.Add(currentFLActivity);

                    if (!JudgeCondition(((IFLValidateActivity)currentFLActivity).Expression))
                    {
                        _v = false;
                        _vN = currentFLActivity.Name;
                        VM = ((IFLValidateActivity)currentFLActivity).Message;
                        if (!string.IsNullOrEmpty(validMessage))
                        {
                            VM = validMessage;
                            validMessage = string.Empty;
                        }
                        return;

                        //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ValidateFail"), currentFLActivity.Name);
                        //throw new FLException(2, message);
                    }
                }
            }

            FLActivity activity = null;
            if ((currentFLActivity is IFLApproveActivity && currentFLActivity.ExecutionStatus != FLActivityExecutionStatus.Executed) || currentFLActivity is IFLApproveBranchActivity)
            {
                #region

                List<FLActivity> approveRights;
                FLActivity approveActivity = null;
                if (currentFLActivity is IFLApproveActivity)
                {
                    approveRights = currentFLActivity.ChildFLActivities;
                    approveActivity = currentFLActivity;
                }
                else
                {
                    approveActivity = _rootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)currentFLActivity).ParentActivity);
                    approveRights = approveActivity.ChildFLActivities;
                }

                string roleId = string.Empty;
                string orgKind = ((IFLRootActivity)_rootFLActivity).OrgKind;
                if (((IEventWaiting)approveActivity).SendToKind == SendToKind.Manager || ((FLApproveActivity)approveActivity).I > 0)           // Manager或者RefManager第一次进
                {
                    roleId = ((IEventWaitingExecute)_sendFromFLActivity).RoleId;
                }
                else if (((IEventWaiting)approveActivity).SendToKind == SendToKind.RefManager)      // RefManager第二次进
                {
                    string sendToField = ((IEventWaiting)approveActivity).SendToField;
                    string values = _keyValues[1].ToString();

                    string tableName = _rootFLActivity.TableName;
                    string qq = Global.GetRoleIdByRefRole(this, sendToField, tableName, values, _clientInfo);
                    roleId = qq;//Global.GetManagerRoleId(qq.ToString(), orgKind, _clientInfo);
                }
                else if (((IEventWaiting)approveActivity).SendToKind == SendToKind.ApplicateManager)
                {
                    if (!string.IsNullOrEmpty(this.CreateRole))
                    {
                        roleId = this.CreateRole;
                    }
                    else
                    {
                        string user = this.Creator;
                        List<string> roles = Global.GetRoleIdsByUserId(user, _clientInfo);
                        if (roles.Count > 0)
                        {
                            roleId = roles[0];
                        }
                    }
                }

                if (((IEventWaiting)approveActivity).SendToKind == SendToKind.Manager && string.IsNullOrEmpty(roleId))
                {
                    if (preview)
                    {
                        return;
                    }
                    else
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "UserNotHaveRole"), ((IEventWaitingExecute)_sendFromFLActivity).UserId);
                        throw new FLException(2, message);
                    }
                }

                bool b = false;
                string levelNo = Global.GetLevelNo(roleId, orgKind, _clientInfo);

                if (!string.IsNullOrEmpty(levelNo))
                {
                    List<string> gRoleIds = new List<string>();
                    foreach (FLActivity a in approveRights)
                    {
                        gRoleIds.Clear();
                        if (((FLApproveActivity)approveActivity).I > 0 || a.Name != approveActivity.Name + "-0")
                        {
                            if (levelNo != "900cae2f-6266-4b7f-ba8c-483a94e01e93")
                            {
                                if (string.Compare(approveActivity.Name + "-" + levelNo, a.Name) >= 0)
                                {
                                    continue;
                                }
                            }
                        }

                        if (JudgeCondition(((IFLApproveBranchActivity)a).Expression))
                        {
                            string grade = string.Empty;
                            string name = string.Empty;
                            string roleId2 = roleId;
                            string roleId3 = string.Empty;
                            bool bb = true;
                            do
                            {
                                roleId3 = roleId2;
                                grade = Global.GetManagerLevelNo(roleId3, orgKind, _clientInfo);
                                if (string.IsNullOrEmpty(grade)) { bb = false; break; }
                                name = approveActivity.Name + "-" + grade;
                                roleId2 = Global.GetManagerRoleId(roleId3, orgKind, _clientInfo);
                                if (gRoleIds.Contains(roleId2))
                                {
                                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "UserManagerIsCircle"), roleId2);
                                    throw new FLException(2, message);
                                }
                                if (roleId2 == roleId3)
                                {
                                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "UserManagerCanNotBeYouself"), roleId2);
                                    throw new FLException(2, message);
                                }
                                gRoleIds.Add(roleId2);
                            }
                            while (!(name == a.Name || (((FLApproveActivity)approveActivity).I == 0 && a.Name == approveActivity.Name + "-0" && roleId2 != "")));

                            if (!bb)
                            {
                                continue;
                            }

                            _r = roleId3;

                            if (approveRights.IndexOf(a) == approveRights.Count - 1 && currentFLActivity.Name == name)  // add by andy
                            {
                                break;
                            }

                            SetUpperParallel(a);
                            SetLocation(a);
                            nextFLActivities.Add(a);
                            b = true;
                            approveActivity.Execute();
                            ((FLApproveActivity)approveActivity).I++;
                            break;
                        }
                    }
                }

                if (!b)
                {
                    approveActivity.Execute();

                    GetNextFLActivities(approveActivity, nextFLActivities);
                }

                #endregion
            }
            else if (currentFLActivity is IFLDetailsActivity && currentFLActivity.ExecutionStatus != FLActivityExecutionStatus.Executed)
            {
                #region

                IFLDetailsActivity detailsActivity = (IFLDetailsActivity)currentFLActivity;
                //if (_hostDataSet == null)
                //{
                    _hostDataSet = HostTable.GetHostDataSet(this, _keyValues, _clientInfo);
                //}

                currentFLActivity.ClearActivities();
                FLActivity childActivity = null;
                if (!string.IsNullOrEmpty(detailsActivity.ExtApproveID))
                {
                    childActivity = new FLSequenceActivity();
                    DataRow hostRow = _hostDataSet.Tables[0].Rows[0];
                    string groupID = hostRow[detailsActivity.ExtGroupField].ToString();
                    object value = hostRow[detailsActivity.ExtValueField];
                    List<string> roles = Global.GetExtApproveRoles(detailsActivity.ExtApproveID, groupID, value, _clientInfo);
                    for (int i = 0; i < roles.Count; i++)
                    {
                        FLStandActivity stand = new FLStandActivity();
                        stand.Name = detailsActivity.Name + "_" + i.ToString();
                        childActivity.AddFLActivity(stand);
                        stand.Description = ((IFLDetailsActivity)detailsActivity).Description;
                        stand.Enabled = ((IFLDetailsActivity)detailsActivity).Enabled;
                        stand.ExpTime = ((IFLDetailsActivity)detailsActivity).ExpTime;
                        stand.FLNavigatorMode = ((IFLDetailsActivity)detailsActivity).FLNavigatorMode;
                        stand.FormName = ((IFLDetailsActivity)detailsActivity).FormName;
                        stand.WebFormName = ((IFLDetailsActivity)detailsActivity).WebFormName;
                        stand.NavigatorMode = ((IFLDetailsActivity)detailsActivity).NavigatorMode;
                        stand.Parameters = ((IFLDetailsActivity)detailsActivity).Parameters;
                        stand.SendToField = ((IFLDetailsActivity)detailsActivity).SendToField;
                        stand.SendToKind = ((IFLDetailsActivity)detailsActivity).SendToKind;
                        stand.SendToRole = ((IFLDetailsActivity)detailsActivity).SendToRole;
                        stand.SendToUser = ((IFLDetailsActivity)detailsActivity).SendToUser;
                        stand.TimeUnit = ((IFLDetailsActivity)detailsActivity).TimeUnit;
                        stand.UrgentTime = ((IFLDetailsActivity)detailsActivity).UrgentTime;
                        stand.SendEmail = ((IFLDetailsActivity)detailsActivity).SendEmail;
                        stand.PlusApprove = ((IFLDetailsActivity)detailsActivity).PlusApprove;
                        stand.PlusApproveReturn = ((IFLDetailsActivity)detailsActivity).PlusApproveReturn;
                        stand.AllowSendBack = ((IFLDetailsActivity)detailsActivity).AllowSendBack;

                        ((ISupportFLDetailsActivity)stand).SendToId2 = roles[i];
                    }


                    if (childActivity.ChildFLActivities.Count > 0) //当有子activity时才加入
                    {
                        ((FLActivity)detailsActivity).AddFLActivity(childActivity);
                    }
                }
                else if (!string.IsNullOrEmpty(detailsActivity.SendToMasterField))
                {
                    #region

                    DataRow hostRow = _hostDataSet.Tables[0].Rows[0];
                    string ids = string.Empty;

                    if (hostRow.Table.Columns.Contains(detailsActivity.SendToMasterField))
                    {
                        //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "TableNotExistColumn"), _rootFLActivity.TableName, detailsActivity.SendToMasterField);
                        //throw new FLException(2, message);    

                        object obj = hostRow[detailsActivity.SendToMasterField];
                        if (obj != null && obj != DBNull.Value && !string.IsNullOrEmpty(obj.ToString()))
                        {
                            //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "SendToFieldValueIsNull"), _rootFLActivity.TableName, detailsActivity.SendToMasterField);
                            //throw new FLException(2, message);
                            ids = obj.ToString();
                        }
                    }

                    if (detailsActivity.ParallelMode == ParallelMode.And || detailsActivity.ParallelMode == ParallelMode.Or)
                    {
                        childActivity = new FLParallelActivity();
                        childActivity.Description = (detailsActivity.ParallelMode == ParallelMode.And && detailsActivity.ParallelRate > 0) ?
                            string.Format("rate:{0}", detailsActivity.ParallelRate) : detailsActivity.ParallelMode.ToString();
                        childActivity.Name = detailsActivity.Name + "_p1";
                    }
                    else
                    {
                        childActivity = new FLSequenceActivity();
                        childActivity.Name = detailsActivity.Name + "_se1";
                    }

                    int i0 = 0;
                    string[] sendToIds = ids.ToString().Split(",".ToCharArray());
                    foreach (string sendToId in sendToIds)
                    {
                        if (string.IsNullOrEmpty(sendToId))
                        {
                            continue;
                        }

                        FLStandActivity stand = new FLStandActivity();
                        stand.Name = detailsActivity.Name + "_" + i0.ToString();
                        i0++;
                        if (detailsActivity.ParallelMode == ParallelMode.And || detailsActivity.ParallelMode == ParallelMode.Or)
                        {
                            FLSequenceActivity sequenceActivity = new FLSequenceActivity();
                            sequenceActivity.Name = detailsActivity.Name + "_se" + i0.ToString();
                            sequenceActivity.AddFLActivity(stand);
                            childActivity.AddFLActivity(sequenceActivity);
                        }
                        else
                        {
                            childActivity.AddFLActivity(stand);
                        }

                        stand.Description = ((IFLDetailsActivity)detailsActivity).Description;
                        stand.Enabled = ((IFLDetailsActivity)detailsActivity).Enabled;

                        stand.ExpTime = ((IFLDetailsActivity)detailsActivity).ExpTime;
                        stand.FLNavigatorMode = ((IFLDetailsActivity)detailsActivity).FLNavigatorMode;
                        stand.FormName = ((IFLDetailsActivity)detailsActivity).FormName;
                        stand.WebFormName = ((IFLDetailsActivity)detailsActivity).WebFormName;
                        stand.NavigatorMode = ((IFLDetailsActivity)detailsActivity).NavigatorMode;
                        stand.Parameters = ((IFLDetailsActivity)detailsActivity).Parameters;
                        stand.SendToField = ((IFLDetailsActivity)detailsActivity).SendToField;
                        stand.SendToKind = ((IFLDetailsActivity)detailsActivity).SendToKind;
                        stand.SendToRole = ((IFLDetailsActivity)detailsActivity).SendToRole;
                        stand.SendToUser = ((IFLDetailsActivity)detailsActivity).SendToUser;
                        stand.TimeUnit = ((IFLDetailsActivity)detailsActivity).TimeUnit;
                        stand.UrgentTime = ((IFLDetailsActivity)detailsActivity).UrgentTime;
                        stand.SendEmail = ((IFLDetailsActivity)detailsActivity).SendEmail;
                        stand.PlusApprove = ((IFLDetailsActivity)detailsActivity).PlusApprove;
                        stand.PlusApproveReturn = ((IFLDetailsActivity)detailsActivity).PlusApproveReturn;
                        stand.AllowSendBack = ((IFLDetailsActivity)detailsActivity).AllowSendBack;

                       // ((ISupportFLDetailsActivity)stand).SendToId2 = sendToId;
                        if (((IFLDetailsActivity)detailsActivity).SendToKind == SendToKind.RefRole)
                        {
                            stand.SendToKind = SendToKind.Role;
                            stand.SendToRole = sendToId;
                        }
                        else if (((IFLDetailsActivity)detailsActivity).SendToKind == SendToKind.RefUser)
                        {
                            stand.SendToKind = SendToKind.User;
                            stand.SendToUser = sendToId;
                        }
                    }
                    if (childActivity.ChildFLActivities.Count > 0) //当有子activity时才加入
                    {
                        ((FLActivity)detailsActivity).AddFLActivity(childActivity);
                    }

                    #endregion
                }
                else
                {
                    #region

                    Activity temp = FLInstance.GetActivityByXoml(this.FLDefinitionFile, string.Empty);
                    IFLRootActivity rootActivity = (IFLRootActivity)temp;

                    DataSet detailsDataSet = HostTable.GetDetailsDataSet(this, _hostDataSet, rootActivity.Keys, detailsActivity.DetailsTableName, detailsActivity.RelationKeys, _clientInfo);
                    string parallelField = detailsActivity.ParallelField;
                    string sendToField = detailsActivity.SendToField;


                    Dictionary<string, FLSequenceActivity> sequenceActivities = new Dictionary<string, FLSequenceActivity>(); // for y1,y2,y3
                    if (detailsDataSet != null && detailsDataSet.Tables.Count != 0 && detailsDataSet.Tables[0].Rows.Count != 0)
                    {
                        int i0 = 1;
                        int i1 = 1;
                        int i2 = 1;

                        foreach (DataRow row in detailsDataSet.Tables[0].Rows)
                        {
                            FLStandActivity stand = new FLStandActivity();

                            //stand.Name = n.Name + "_s" + i0.ToString();
                            stand.Name = detailsActivity.Name + "_" + i0.ToString();
                            i0++;

                            stand.Description = ((IFLDetailsActivity)detailsActivity).Description;
                            stand.Enabled = ((IFLDetailsActivity)detailsActivity).Enabled;

                            stand.ExpTime = ((IFLDetailsActivity)detailsActivity).ExpTime;
                            stand.FLNavigatorMode = ((IFLDetailsActivity)detailsActivity).FLNavigatorMode;
                            stand.FormName = ((IFLDetailsActivity)detailsActivity).FormName;
                            stand.WebFormName = ((IFLDetailsActivity)detailsActivity).WebFormName;
                            stand.NavigatorMode = ((IFLDetailsActivity)detailsActivity).NavigatorMode;
                            stand.Parameters = ((IFLDetailsActivity)detailsActivity).Parameters;
                            stand.SendToField = ((IFLDetailsActivity)detailsActivity).SendToField;
                            stand.SendToKind = ((IFLDetailsActivity)detailsActivity).SendToKind;
                            stand.SendToRole = ((IFLDetailsActivity)detailsActivity).SendToRole;
                            stand.SendToUser = ((IFLDetailsActivity)detailsActivity).SendToUser;
                            stand.TimeUnit = ((IFLDetailsActivity)detailsActivity).TimeUnit;
                            stand.UrgentTime = ((IFLDetailsActivity)detailsActivity).UrgentTime;
                            stand.SendEmail = ((IFLDetailsActivity)detailsActivity).SendEmail;
                            stand.PlusApprove = ((IFLDetailsActivity)detailsActivity).PlusApprove;
                            stand.PlusApproveReturn = ((IFLDetailsActivity)detailsActivity).PlusApproveReturn;
                            stand.AllowSendBack = ((IFLDetailsActivity)detailsActivity).AllowSendBack;


                            if (!string.IsNullOrEmpty(detailsActivity.FLNavigatorField))
                            {
                                object flNavigatorMode = row[detailsActivity.FLNavigatorField];
                                if (flNavigatorMode != null)
                                {
                                    if (string.Compare(flNavigatorMode.ToString(), "Continue", true) == 0)
                                    {
                                        stand.FLNavigatorMode = FLNavigatorMode.Continue;
                                    }
                                }
                            }

                            object sendToId2 = row[sendToField];
                            if (sendToId2 == null || sendToId2 == DBNull.Value || sendToId2.ToString() == string.Empty)
                            {
                                continue;
                                //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "SendToFieldValueIsNull"), n.DetailsTableName);
                                //throw new FLException(message);
                            }
                            //((ISupportFLDetailsActivity)stand).SendToId2 = sendToId2.ToString();
                            if (((IFLDetailsActivity)detailsActivity).SendToKind == SendToKind.RefRole)
                            {
                                stand.SendToKind = SendToKind.Role;
                                stand.SendToRole = sendToId2.ToString();
                            }
                            else if (((IFLDetailsActivity)detailsActivity).SendToKind == SendToKind.RefUser)
                            {
                                stand.SendToKind = SendToKind.User;
                                stand.SendToUser = sendToId2.ToString();
                            }

                            if (!string.IsNullOrEmpty(parallelField))
                            {
                                object isParallel = row[parallelField];
                                if ((isParallel != null && isParallel != DBNull.Value) &&
                                    (isParallel.ToString().Trim().ToLower() == "y" || isParallel.ToString().Trim().ToLower() == "and"))
                                {
                                    if (childActivity == null)
                                    {
                                        childActivity = new FLParallelActivity();
                                        childActivity.Description = detailsActivity.ParallelRate > 0 ? string.Format("rate:{0}", detailsActivity.ParallelRate) : "and";
                                        childActivity.Name = detailsActivity.Name + "_p" + i2.ToString();
                                        i2++;

                                        ((FLActivity)detailsActivity).AddFLActivity(childActivity);
                                    }

                                    FLSequenceActivity sequenceActivity = new FLSequenceActivity();
                                    sequenceActivity.Name = detailsActivity.Name + "_se" + i1.ToString();
                                    i1++;
                                    ((FLActivity)childActivity).AddFLActivity(sequenceActivity);

                                    ((FLActivity)sequenceActivity).AddFLActivity(stand);
                                }
                                else if ((isParallel != null && isParallel != DBNull.Value) && (isParallel.ToString().Trim().ToLower().StartsWith("y")))
                                {
                                    if (childActivity == null)
                                    {
                                        childActivity = new FLParallelActivity();
                                        childActivity.Description = "and";
                                        childActivity.Name = detailsActivity.Name + "_p" + i2.ToString();
                                        i2++;

                                        ((FLActivity)detailsActivity).AddFLActivity(childActivity);
                                    }
                                    FLSequenceActivity sequenceActivity = null;
                                    if(sequenceActivities.ContainsKey(isParallel.ToString()))
                                    {
                                        sequenceActivity = sequenceActivities[isParallel.ToString()];
                                    }
                                    if (sequenceActivity == null)
                                    {
                                        sequenceActivity = new FLSequenceActivity();
                                        sequenceActivity.Name = detailsActivity.Name + "_se" + i1.ToString();
                                        i1++;
                                        ((FLActivity)childActivity).AddFLActivity(sequenceActivity);
                                        sequenceActivities[isParallel.ToString()] = sequenceActivity;
                                    }
                                    sequenceActivity.AddFLActivity(stand);
                                }
                                //修改n值时会串签
                                //else if ((isParallel != null && isParallel != DBNull.Value) &&
                                //    (isParallel.ToString().Trim().ToLower() == "n" || isParallel.ToString().Trim().ToLower() == "or"))
                                //{
                                //    if (childActivity == null)
                                //    {
                                //        childActivity = new FLParallelActivity();
                                //        childActivity.Description = "Or";
                                //        childActivity.Name = detailsActivity.Name + "_p" + i2.ToString();
                                //        i2++;

                            //        ((FLActivity)detailsActivity).AddFLActivity(childActivity);
                                //    }
                                //    FLSequenceActivity sequenceActivity = new FLSequenceActivity();
                                //    sequenceActivity.Name = detailsActivity.Name + "_se" + i1.ToString();
                                //    i1++;
                                //    ((FLActivity)childActivity).AddFLActivity(sequenceActivity);

                            //    ((FLActivity)sequenceActivity).AddFLActivity(stand);
                                //}
                                else
                                {
                                    childActivity = null;
                                    ((FLActivity)detailsActivity).AddFLActivity(stand);
                                }
                            }
                            else
                            {
                                childActivity = null;
                                ((FLActivity)detailsActivity).AddFLActivity(stand);
                            }
                        }
                    }

                    #endregion
                }

                XmlSerializer serializer = CreateXmlSerializer(typeof(FLDetailsActivity));

                StringBuilder builder = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(builder);
                serializer.Serialize(writer, detailsActivity);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(builder.ToString());
                XmlNode nodeDetails = doc.SelectSingleNode(string.Format("FLDetailsActivity[@Name='{0}']", detailsActivity.Name));

                XmlNode nodeOldDetails = (XmlNode)_flDefinitionXmlNodes[detailsActivity.Name];
                nodeOldDetails.InnerXml = nodeDetails.InnerXml;

                InitFLDefinitionXmlNodes(nodeOldDetails);
                _flDefinitionXmlString = nodeOldDetails.OwnerDocument.InnerXml;

                ((FLActivity)detailsActivity).Execute();

                Hashtable childAll = ((FLActivity)detailsActivity).GetAllChildFLActivities();
                foreach (string key in childAll.Keys)
                {
                    _setUpperParallels.Remove(key);
                }
                foreach (string key in childAll.Keys)
                {
                    _setLocations.Remove(key);
                }
                SetUpperParallel((FLActivity)detailsActivity);
                SetLocation((FLActivity)detailsActivity);

                if (((FLActivity)detailsActivity).ChildFLActivities.Count == 0)
                {
                    GetNextFLActivities((FLActivity)detailsActivity, nextFLActivities);
                    //当detailsActivity没有任何人需要签核时, 撤消detailsActivity状态,不然退回后再也无法进入detailsActivity
                    ((FLActivity)detailsActivity).InitExecStatus();
                }
                else
                {
                    if (((FLActivity)detailsActivity).ChildFLActivities[0] is FLStandActivity)
                    {

                        SetUpperParallel(((FLActivity)detailsActivity).ChildFLActivities[0]);
                        SetLocation(((FLActivity)detailsActivity).ChildFLActivities[0]);


                        nextFLActivities.Add(((FLActivity)detailsActivity).ChildFLActivities[0]);
                    }
                    else
                    {
                        GetNextFLActivities(((FLActivity)detailsActivity).ChildFLActivities[0], nextFLActivities);
                    }
                }

                #endregion
            }
            else if (currentFLActivity is IFLSubFlowActivity && currentFLActivity.ExecutionStatus != FLActivityExecutionStatus.Executed)
            {

                IFLSubFlowActivity subflow = currentFLActivity as IFLSubFlowActivity;
                ((FLActivity)subflow).ChildFLActivities.Clear();
                string xomlName = subflow.XomlName;
                if (_hostDataSet == null)
                {
                    _hostDataSet = HostTable.GetHostDataSet(this, _keyValues, _clientInfo);
                }
                DataRow hostRow = _hostDataSet.Tables[0].Rows[0];
                if (!string.IsNullOrEmpty(subflow.XomlField) && hostRow.Table.Columns.Contains(subflow.XomlField))
                {
                    object obj = hostRow[subflow.XomlField];
                    if (obj != null && obj != DBNull.Value && !string.IsNullOrEmpty(obj.ToString()))
                    {
                        xomlName = obj.ToString();
                    }
                }

                if (!string.IsNullOrEmpty(xomlName))
                {
                    FLRootActivity subflowRoot = new FLRootActivity();
                    FileInfo fileInfo = new FileInfo(this.FLDefinitionFile);
                    string file = fileInfo.Directory + @"\" + xomlName;

                    if (!File.Exists(file))
                    {
                        file = fileInfo.Directory + @"\SubFlows\" + xomlName;
                    }
                    Activity subflowDefniation = GetActivityByXoml(file, string.Empty);


                    InitFLActivities(subflowRoot, subflowDefniation);

                    for (int i = 0; i < subflowRoot.ChildFLActivities.Count; i++)
                    {
                        if (i == 0 && !subflow.IncludeFirstActivity)
                        {
                            continue;
                        }

                        FLActivity child = subflowRoot.ChildFLActivities[i];

                        //((FLActivity)subflowRoot).ChildFLActivities.Remove(child);
                      
                        if (this.RootFLActivity.GetFLActivityByName(child.Name) != null)
                        {
                            if (this.RootFLActivity.GetFLActivityByName(currentFLActivity.Name).GetFLActivityByName(child.Name) == null)
                            {
                                throw new FLException(string.Format("Activity:'{0}' exists in main flow and sub flow.", child.Name));
                            }
                        }

                        ((FLActivity)subflow).AddFLActivity(child);
                    }
                }

                XmlSerializer serializer = CreateXmlSerializer(typeof(FLSubFlowActivity));

                StringBuilder builder = new StringBuilder();
                XmlWriter writer = XmlWriter.Create(builder);
                serializer.Serialize(writer, subflow);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(builder.ToString());
                XmlNode nodeSubs = doc.SelectSingleNode(string.Format("FLSubFlowActivity[@Name='{0}']", subflow.Name));

                XmlNode nodeOldSubs = (XmlNode)_flDefinitionXmlNodes[subflow.Name];
                nodeOldSubs.InnerXml = nodeSubs.InnerXml;

                InitFLDefinitionXmlNodes(nodeOldSubs);
                _flDefinitionXmlString = nodeOldSubs.OwnerDocument.InnerXml;
                ((FLActivity)subflow).Execute();


                Hashtable childAll = ((FLActivity)subflow).GetAllChildFLActivities();
                foreach (string key in childAll.Keys)
                {
                    _setUpperParallels.Remove(key);
                    _setLocations.Remove(key);
                }

                SetUpperParallel((FLActivity)subflow);
                SetLocation((FLActivity)subflow);

                if (((FLActivity)subflow).ChildFLActivities.Count == 0)
                {
                    GetNextFLActivities((FLActivity)subflow, nextFLActivities);
                    //当subflow没有任何人需要签核时, 撤消subflow状态,不然退回后再也无法进入subflow
                    ((FLActivity)subflow).InitExecStatus();
                }
                else
                {
                    if (((FLActivity)subflow).ChildFLActivities[0] is FLStandActivity)
                    {
                        SetUpperParallel(((FLActivity)subflow).ChildFLActivities[0]);
                        SetLocation(((FLActivity)subflow).ChildFLActivities[0]);
                        nextFLActivities.Add(((FLActivity)subflow).ChildFLActivities[0]);
                    }
                    else
                    {
                        GetNextFLActivities(((FLActivity)subflow).ChildFLActivities[0], nextFLActivities);
                    }
                }
            }
            else
            {
                #region

                if ((currentFLActivity is IEventWaiting) || (currentFLActivity is IFLValidateActivity) || ((currentFLActivity is IControlFL) && (currentFLActivity.ExecutionStatus == FLActivityExecutionStatus.Executed)))
                {
                    if (currentFLActivity is IFLParallelActivity && (string.IsNullOrEmpty(((IFLParallelActivity)currentFLActivity).Description) || string.IsNullOrEmpty(((IFLParallelActivity)currentFLActivity).Description.Trim()) || ((IFLParallelActivity)currentFLActivity).Description.ToLower() == "and"))
                    {
                        bool b = true;
                        foreach (FLActivity a in currentFLActivity.ChildFLActivities)
                        {
                            if (!((IFLParallelActivity)currentFLActivity).ExecutedBranches.Exists(
                                delegate(string s)
                                {
                                    if (a.Name == s)
                                        return true;
                                    else
                                        return false;
                                }
                                ))
                            {
                                b = false;
                                break;
                            }
                        }

                        if (!b)
                        {
                            return;
                        }
                    }
                    else if (currentFLActivity is IFLParallelActivity && (!string.IsNullOrEmpty(((IFLParallelActivity)currentFLActivity).Description) && !string.IsNullOrEmpty(((IFLParallelActivity)currentFLActivity).Description.Trim()) && ((IFLParallelActivity)currentFLActivity).Description.ToLower().Contains("rate")))
                    {
                        decimal rate = 0;
                        decimal x = currentFLActivity.ChildFLActivities.Count;
                        if (x != 0)
                        {
                            decimal y = ((IFLParallelActivity)currentFLActivity).ExecutedBranches.Count;
                            decimal j = y / x;

                            string q = ((IFLParallelActivity)currentFLActivity).Description.Trim();
                            string[] qq = q.Split(":".ToCharArray());
                            string p = qq[1].Trim();
                            rate = decimal.Parse(p);

                            rate = rate / 100;
                            if (j < rate)
                            {
                                return;
                            }
                        }
                    }

                    activity = GetRealNextFLActivity(currentFLActivity);
                    if (activity == null)
                    {
                        activity = GetRealParentFLActivity(currentFLActivity);
                        if (activity == null || activity is IFLRootActivity)
                        {
                            _flflag = 'Z';
                            return;
                        }

                        FLActivity parentActivity = GetRealParentFLActivity(activity);
                        if (parentActivity is IFLParallelActivity && ((string.IsNullOrEmpty(((IFLParallelActivity)parentActivity).Description) || string.IsNullOrEmpty(((IFLParallelActivity)parentActivity).Description.Trim()) || ((IFLParallelActivity)parentActivity).Description.Trim().ToLower() == "and")
                            || (!string.IsNullOrEmpty(((IFLParallelActivity)parentActivity).Description) && !string.IsNullOrEmpty(((IFLParallelActivity)parentActivity).Description.Trim()) && ((IFLParallelActivity)parentActivity).Description.Trim().ToLower().Contains("rate"))))
                        {
                            if (!((IFLParallelActivity)parentActivity).ExecutedBranches.Exists(
                                 delegate(string s)
                                 {
                                     if (currentFLActivity.Name == s)
                                         return true;
                                     else
                                         return false;
                                 }
                                 ))
                            {
                                ((IFLParallelActivity)parentActivity).ExecutedBranches.Add(activity.Name);
                                foreach (FLActivity a in parentActivity.ChildFLActivities)
                                {
                                    FLDirection direction = ((IFLSequenceActivity)a).FLDirection;
                                    if (a.Name != activity.Name && direction != FLDirection.Waiting)
                                    {
                                        //if (direction != _flDirection)
                                        //{
                                        //    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "DirectionError"), _flDirection.ToString(), a.Name, direction.ToString());
                                        //    throw new FLException(2, message);
                                        //}
                                    }
                                }
                                ((IFLSequenceActivity)activity).SetFLDirection(_flDirection);
                            }
                        }
                    }

                    if (activity is IEventWaiting && !(activity is IFLApproveActivity || activity is IFLApproveBranchActivity) && activity.Enabled)
                    {
                        SetUpperParallel(activity);
                        SetLocation(activity);
                        nextFLActivities.Add(activity);
                    }
                    else
                    {
                        GetNextFLActivities(activity, nextFLActivities);
                    }
                }
                else
                {
                    if (currentFLActivity is IControlFL && !(currentFLActivity is IFLRootActivity))
                    {
                        currentFLActivity.Execute();
                    }

                    FLActivity parentFLActivity = GetRealParentFLActivity(currentFLActivity);
                    if (parentFLActivity != null && parentFLActivity is IFLIfElseActivity)
                    {
                        foreach (FLActivity flActivity in parentFLActivity.ChildFLActivities)
                        {
                            flActivity.Execute();
                        }
                    }

                    if (currentFLActivity is IFLIfElseActivity)
                    {
                        List<FLActivity> branchActivities = currentFLActivity.ChildFLActivities;
                        List<FLActivity> runBranchActivities = new List<FLActivity>();
                        FLActivity elseBranchActivity = null;

                        foreach (FLActivity branchActivity in branchActivities)
                        {
                            IFLIfElseBranchActivity a = (IFLIfElseBranchActivity)branchActivity;
                            string condition = a.Condition;
                            if (condition != string.Empty)
                            {
                                if (JudgeCondition(condition))
                                {
                                    runBranchActivities.Add(branchActivity);
                                    break;
                                }
                            }
                            else
                            {
                                elseBranchActivity = branchActivity;
                            }
                        }

                        foreach (FLActivity branchActivity in runBranchActivities)
                        {
                            GetNextFLActivities(branchActivity, nextFLActivities);
                        }

                        if (runBranchActivities.Count == 0)
                        {
                            GetNextFLActivities(elseBranchActivity, nextFLActivities);
                        }
                    }
                    else if (currentFLActivity is IFLParallelActivity)
                    {
                        _isFirstInParallel = true;
                        _tempParallel = currentFLActivity.Name;
                        foreach (FLActivity a in currentFLActivity.ChildFLActivities)
                        {
                            _tempParallelBranch = a.Name;
                            List<FLActivity> tempNextFLActivities = new List<FLActivity>();

                            ((IFLSequenceActivity)a).SetFLDirection(FLDirection.Waiting);
                            GetNextFLActivities(a, tempNextFLActivities);

                            int j = 0;
                            foreach (FLActivity tempFLActivity in tempNextFLActivities)
                            {
                                if (tempFLActivity is IEventWaiting)
                                {
                                    j++;
                                }
                            }

                            if (j == 0)
                            {
                                ((IFLParallelActivity)currentFLActivity).ExecutedBranches.Add(a.Name);
                            }

                            nextFLActivities.AddRange(tempNextFLActivities);
                        }
                        _isFirstInParallel = false;
                        _tempParallel = string.Empty;
                        _tempParallelBranch = string.Empty;

                        if (nextFLActivities.Count == 0)
                        {
                            GetNextFLActivities(currentFLActivity, nextFLActivities);
                        }
                    }
                    else
                    {
                        List<FLActivity> activities = currentFLActivity.ChildFLActivities;
                        if (activities.Count == 0)
                        {
                            activity = GetRealNextFLActivity(currentFLActivity);
                            if (activity == null)
                            {
                                activity = GetRealParentFLActivity(currentFLActivity);
                                if (activity == null || activity is IFLRootActivity)
                                {
                                    _flflag = 'Z';
                                    return;
                                }
                            }

                            if (activity is IEventWaiting && !(activity is IFLApproveActivity || activity is IFLApproveBranchActivity) && activity.Enabled)
                            {
                                SetUpperParallel(activity);
                                SetLocation(activity);
                                nextFLActivities.Add(activity);
                            }
                            else
                            {
                                GetNextFLActivities(activity, nextFLActivities);
                            }
                        }
                        else
                        {
                            activity = activities[0];
                            if (activity is IEventWaiting && !(activity is IFLApproveActivity || activity is IFLApproveBranchActivity) && activity.Enabled)
                            {
                                SetUpperParallel(activity);
                                SetLocation(activity);
                                nextFLActivities.Add(activity);
                            }
                            else
                            {
                                GetNextFLActivities(activity, nextFLActivities);
                            }
                        }
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// 设置Activity为UnExecute状态
        /// </summary>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="previousFLActivity">上一Activity</param>
        private void UnExecuteIFLControls(FLActivity currentFLActivity, FLActivity previousFLActivity)
        {
            List<FLActivity> unExecuteIFLControls = new List<FLActivity>();

            string currentLocation = currentFLActivity.Location;
            string previousLocation = previousFLActivity.Location;

            if (currentLocation != previousLocation && currentLocation != string.Empty)
            {
                List<string> list1 = new List<string>();
                List<string> list2 = new List<string>();

                string[] ss = currentLocation.Split("->".ToCharArray());
                foreach (string s in ss)
                {
                    if (s != null && s != string.Empty)
                        list1.Add(s);
                }

                ss = previousLocation.Split("->".ToCharArray());
                foreach (string s in ss)
                {
                    if (s != null && s != string.Empty)
                        list2.Add(s);
                }

                if (previousLocation != string.Empty)
                {
                    int i = 0;
                    int count1 = list1.Count;
                    int count2 = list2.Count;
                    foreach (string s in list1)
                    {
                        if (!(i <= count2 - 1 && list1[i] == list2[i]))
                        {
                            break;
                        }
                        i++;
                    }

                    for (int j = i; j <= count1 - 1; j++)
                    {
                        unExecuteIFLControls.Add(_rootFLActivity.GetFLActivityByName(list1[j]));
                    }
                }
                else
                {
                    foreach (string s in list1)
                    {
                        unExecuteIFLControls.Add(_rootFLActivity.GetFLActivityByName(s));
                    }
                }
            }

            foreach (FLActivity flControl in unExecuteIFLControls)
            {
                flControl.InitExecStatus();
                //retunr all child of ifelse acitivity
                if (flControl is IFLIfElseActivity)
                {
                    foreach (FLActivity flActivity in flControl.ChildFLActivities)
                    {
                        flActivity.InitExecStatus();
                    }
                }
            }
        }

        //private void GetPreviousFLActivites(FLActivity currentFLActivity, List<FLActivity> nextFLActivities)
        //{
        //    if (_flPathList.Count <= 1)
        //    {
        //        _flflag = 'Z';
        //    }

        //    currentFLActivity.UnExecute();

        //    if (currentFLActivity.UpperParallel == string.Empty)
        //    {
        //        object o0 = _flPathList[_flPathList.Count - 2];
        //        // A ——> B ——> C 
        //        if (o0 is string)
        //        {
        //            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(o0.ToString());
        //            nextFLActivities.Add(previousFLActivity);

        //            UnExecuteIFLControls(currentFLActivity, previousFLActivity);

        //            _flPathList.Remove(currentFLActivity.Name);
        //        }
        //        // B ——>
        //        //         A    
        //        // C ——>
        //        else
        //        {
        //            List<object> list01 = (List<object>)_flPathList[_flPathList.Count - 2];
        //            List<string> branches = new List<string>();
        //            foreach (object o1 in list01)
        //            {
        //                List<object> list1 = (List<object>)o1;
        //                if (list1.Count > 0)
        //                {
        //                    string previousFLActivityName = list1[list1.Count - 1].ToString();
        //                    FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
        //                    nextFLActivities.Add(previousFLActivity);
        //                    branches.Add(previousFLActivity.UpperParallelBranch);

        //                    UnExecuteIFLControls(currentFLActivity, previousFLActivity);
        //                }
        //            }

        //            FLActivity tempFLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
        //            string upperParallel = tempFLActivity.UpperParallel;
        //            FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
        //            foreach (string branch in branches)
        //            {
        //                ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
        //                ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
        //            }

        //            _flPathList.Remove(currentFLActivity.Name);
        //        }
        //    }
        //    else
        //    {
        //        List<object> list0 = (List<object>)_flPathList[_flPathList.Count - 1];
        //        List<object> list1 = null;
        //        foreach (object o0 in list0)
        //        {
        //            List<object> temp = (List<object>)o0;
        //            if (temp.Exists(
        //                         delegate(object s)
        //                         {
        //                             if (currentFLActivity.Name == s.ToString())
        //                                 return true;
        //                             else
        //                                 return false;
        //                         }
        //                ))
        //            {
        //                list1 = temp;
        //                break;
        //            }
        //        }

        //        //   ——> B ——> D 
        //        // A               
        //        //   ——> C ——> E
        //        if (list1.Count > 1)
        //        {
        //            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(list1[list1.Count - 2].ToString());
        //            nextFLActivities.Add(previousFLActivity);

        //            UnExecuteIFLControls(currentFLActivity, previousFLActivity);

        //            list1.Remove(currentFLActivity.Name);
        //        }
        //        else
        //        {
        //            FLActivity upperParallelFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivity.UpperParallel);
        //            FLActivity upperParallelBranchFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivity.UpperParallelBranch);
        //            foreach (FLActivity a in upperParallelFLActivity.ChildFLActivities)
        //            {
        //                FLDirection direction = ((IFLSequenceActivity)a).FLDirection;
        //                if (a.Name != upperParallelBranchFLActivity.Name && direction != FLDirection.Waiting)
        //                {
        //                    if (direction != _flDirection)
        //                    {
        //                        throw new Exception("You don't " + _flDirection.ToString() + ",because " + a.Name + " had " + direction.ToString() + ".");
        //                    }
        //                }
        //            }

        //            ((IFLSequenceActivity)upperParallelBranchFLActivity).SetFLDirection(_flDirection);

        //            object o01 = _flPathList[_flPathList.Count - 2];
        //            //   ——> B  
        //            // A          
        //            //   ——> C
        //            if (o01 is string)
        //            {
        //                FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(o01.ToString());
        //                if (list0.Count <= 1)
        //                {
        //                    nextFLActivities.Add(previousFLActivity);

        //                    upperParallelBranchFLActivity.UnExecute();

        //                    upperParallelFLActivity.UnExecute();

        //                    UnExecuteIFLControls(currentFLActivity, previousFLActivity);

        //                    _flPathList.Remove(list0);
        //                }
        //                else
        //                {
        //                    upperParallelBranchFLActivity.UnExecute();

        //                    UnExecuteIFLControls(currentFLActivity, previousFLActivity);

        //                    list0.Remove(list1);
        //                }
        //            }
        //            // A ——>      ——> C(有问题)
        //            //         None
        //            // B ——>      ——> D
        //            else
        //            {
        //                List<object> list01 = (List<object>)o01;
        //                List<string> branches = new List<string>();

        //                if (list0.Count <= 1)
        //                {
        //                    foreach (object o1 in list01)
        //                    {
        //                        list1 = (List<object>)o1;
        //                        if (list1.Count > 0)
        //                        {
        //                            string previousFLActivityName = list1[list1.Count - 1].ToString();
        //                            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
        //                            nextFLActivities.Add(previousFLActivity);
        //                            branches.Add(previousFLActivity.UpperParallelBranch);

        //                            UnExecuteIFLControls(currentFLActivity, previousFLActivity);
        //                        }
        //                    }

        //                    // --------------------------------------------------------------------------------------------------
        //                    upperParallelBranchFLActivity.UnExecute();

        //                    upperParallelFLActivity.UnExecute();

        //                    FLActivity tempFLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
        //                    string upperParallel = tempFLActivity.UpperParallel;

        //                    FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
        //                    foreach (string branch in branches)
        //                    {
        //                        ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
        //                        ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
        //                    }
        //                    // --------------------------------------------------------------------------------------------------

        //                    _flPathList.Remove(list0);
        //                }
        //                else
        //                {
        //                    foreach (object o1 in list01)
        //                    {
        //                        list1 = (List<object>)o1;
        //                        if (list1.Count > 0)
        //                        {
        //                            string previousFLActivityName = list1[list1.Count - 1].ToString();
        //                            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
        //                            UnExecuteIFLControls(currentFLActivity, previousFLActivity);
        //                        }
        //                    }

        //                    upperParallelBranchFLActivity.UnExecute();

        //                    list0.Remove(list1);
        //                }
        //            }
        //        }
        //    }

        //    _flflag = 'P';
        //}

        /// <summary>
        /// 取得上一Activity的集合
        /// </summary>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="nextFLActivities">下一Activity的集合</param>
        private void GetPreviousFLActivites(FLActivity currentFLActivity, List<FLActivity> nextFLActivities)
        {
            if (Version == "2.0")
            {
                var previousFLActivity = currentFLActivity.PreviousActivity;
                GetPreviousFLActivites2(currentFLActivity, previousFLActivity, nextFLActivities);
            }
            else
            {

                if (_p.Count <= 1)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLTools", "FLWebNavigator", "ReturnToEnd");
                    throw new FLException(message);
                    return;
                }

                ////同时要退回当前activity的上级
                //object current = _p[_p.Count - 1];
                //if (current is string)
                //{
                //    ReturnUpperParallel((string)current, currentFLActivity);
                //}
                //else
                //{
                //    List<object> list = (List<object>)current;
                //    foreach (object obj in list)
                //    {
                //        List<object> branch = obj as List<object>;
                //        foreach (object str in branch)
                //        {
                //            ReturnUpperParallel(str.ToString(), currentFLActivity);
                //        }
                //    }
                //}

                if (currentFLActivity.UpperParallel == string.Empty)
                {
                    object o0 = _p[_p.Count - 2];
                    // A ——> B ——> C 
                    if (o0 is string)
                    {
                        FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(o0.ToString());

                        UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                        _p.Remove(currentFLActivity.Name);

                        if (previousFLActivity is IEventWaiting)
                        {
                            nextFLActivities.Add(previousFLActivity);
                            //Hashtable table = _rootFLActivity.GetAllChildFLActivities();
                            //foreach (object key in table.Keys)
                            //{
                            //    object o3 = table[key];
                            //    if (o3 == null)
                            //    {
                            //        continue;
                            //    }

                            //    FLActivity temp2FLActivity = (FLActivity)o3;
                            //    if (temp2FLActivity is FLIfElseActivity)
                            //    {
                            //        ((FLIfElseActivity)temp2FLActivity).InitExecStatus();
                            //        foreach (FLActivity child in ((FLIfElseActivity)temp2FLActivity).ChildFLActivities)
                            //        {
                            //            child.InitExecStatus();
                            //        }
                            //    }
                            //}
                        }
                        else if (previousFLActivity is IFLProcedureActivity)
                        {
                            if (previousFLActivity is IFLProcedureActivity)
                            {
                                Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                            }
                            nextFLActivities.Add(previousFLActivity);

                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                        }
                        else
                        {
                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                        }
                    }
                    // B ——>
                    //         A    
                    // C ——>
                    else
                    {
                        List<object> list01 = (List<object>)_p[_p.Count - 2];
                        List<string> branches = new List<string>();
                        foreach (object o1 in list01)
                        {
                            List<object> list1 = (List<object>)o1;
                            if (list1.Count > 0)
                            {
                                string previousFLActivityName = list1[list1.Count - 1].ToString();
                                FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);

                                branches.Add(previousFLActivity.UpperParallelBranch);

                                UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                                if (previousFLActivity is IEventWaiting)
                                {
                                    nextFLActivities.Add(previousFLActivity);
                                }
                                else if (previousFLActivity is IFLProcedureActivity)
                                {
                                    if (previousFLActivity is IFLProcedureActivity)
                                    {
                                        Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                                    }
                                    nextFLActivities.Add(previousFLActivity);

                                    GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                }
                                else
                                {
                                    GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                }
                            }
                        }

                        FLActivity tempFLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
                        string upperParallel = tempFLActivity.UpperParallel;
                        FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
                        foreach (string branch in branches)
                        {
                            ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
                            ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
                        }

                        _p.Remove(currentFLActivity.Name);
                    }
                }
                else
                {
                    List<object> list0 = _p[_p.Count - 1] is List<object> ? (List<object>)_p[_p.Count - 1] : (List<object>)_p[_p.Count - 2];
                    List<object> list1 = null;
                    foreach (object o0 in list0)
                    {
                        List<object> temp = (List<object>)o0;
                        if (temp.Exists(
                                     delegate(object s)
                                     {
                                         if (currentFLActivity.Name == s.ToString())
                                             return true;
                                         else
                                             return false;
                                     }
                            ))
                        {
                            list1 = temp;
                            break;
                        }
                    }

                    //   ——> B ——> D 
                    // A               
                    //   ——> C ——> E
                    if (list1.Count > 1)
                    {
                        FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(list1[list1.Count - 2].ToString());

                        UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                        list1.Remove(currentFLActivity.Name);

                        if (previousFLActivity is IEventWaiting)
                        {
                            nextFLActivities.Add(previousFLActivity);
                        }
                        else if (previousFLActivity is IFLProcedureActivity)
                        {
                            if (previousFLActivity is IFLProcedureActivity)
                            {
                                Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                            }
                            nextFLActivities.Add(previousFLActivity);

                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                        }
                        else
                        {
                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                        }
                    }
                    else
                    {
                        FLActivity upperParallelFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivity.UpperParallel);
                        FLActivity upperParallelBranchFLActivity = _rootFLActivity.GetFLActivityByName(currentFLActivity.UpperParallelBranch);
                        foreach (FLActivity a in upperParallelFLActivity.ChildFLActivities)
                        {
                            FLDirection direction = ((IFLSequenceActivity)a).FLDirection;
                            if (a.Name != upperParallelBranchFLActivity.Name && direction != FLDirection.Waiting)
                            {
                                //有退回时不考虑其他的方向
                                //if (direction != _flDirection)
                                //{
                                //    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLRuntime", "FLInstance", "DirectionError"), a.Name);
                                //    throw new FLException(2, message);
                                //}

                            }
                        }

                        ((IFLSequenceActivity)upperParallelBranchFLActivity).SetFLDirection(_flDirection);

                        object o01 = _p[_p.Count - 2];
                        //   ——> B  
                        // A          
                        //   ——> C
                        if (o01 is string)
                        {
                            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(o01.ToString());
                            bool w = false;
                            if (upperParallelFLActivity.Description.Length == 0 || upperParallelFLActivity.Description.ToLower() == "and")
                            {
                                //w= true; 平行的直接退回
                                w = false;
                            }
                            if (upperParallelFLActivity.Description.ToLower().Trim().StartsWith("rate"))
                            {
                                string q = upperParallelFLActivity.Description.Trim();
                                string[] qq = q.Split(":".ToCharArray());
                                string p = qq[1].Trim();
                                decimal rate = decimal.Parse(p);
                                rate = rate / 100;

                                int c = upperParallelFLActivity.ChildFLActivities.Count;
                                decimal rate2 = new decimal((c - list0.Count + 1)) / c;
                                if (rate2 < rate)
                                {
                                    w = true;
                                }
                            }

                            if ((w) && list0.Count > 1)
                            {
                                upperParallelBranchFLActivity.InitExecStatus();

                                UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                                list0.Remove(list1);

                                GetWL(list0, _wl);
                            }
                            else
                            {
                                //改了平行加签直接退回后，需要重置整个平行系统
                                ReturnUpperParallel(currentFLActivity.Name, previousFLActivity);
                                // upperParallelBranchFLActivity.InitExecStatus();
                                foreach (FLActivity a in upperParallelFLActivity.ChildFLActivities)
                                {
                                    a.InitExecStatus();
                                }
                                if (upperParallelFLActivity is FLTools.ComponentModel.FLParallelActivity)
                                {
                                    (upperParallelFLActivity as FLTools.ComponentModel.FLParallelActivity).ExecutedBranches.Clear(); //退回時重置這個屬性
                                }

                                upperParallelFLActivity.InitExecStatus();

                                UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                                _p.Remove(list0);

                                if (previousFLActivity is IEventWaiting)
                                {
                                    nextFLActivities.Add(previousFLActivity);
                                }
                                else if (previousFLActivity is IFLProcedureActivity)
                                {
                                    if (previousFLActivity is IFLProcedureActivity)
                                    {
                                        Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                                    }
                                    nextFLActivities.Add(previousFLActivity);

                                    GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                }
                                else
                                {

                                    GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                }
                            }

                            //if (upperParallelFLActivity.Description.ToLower() != "and" || list0.Count <= 1)
                            //{  
                            //    upperParallelBranchFLActivity.InitExecStatus();

                            //    upperParallelFLActivity.InitExecStatus();

                            //    UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                            //    _flPathList.Remove(list0);

                            //    if (previousFLActivity is IEventWaiting)
                            //    {
                            //        nextFLActivities.Add(previousFLActivity);
                            //    }
                            //    else if (previousFLActivity is IFLProcedureActivity)
                            //    {
                            //        nextFLActivities.Add(previousFLActivity);

                            //        GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                            //    }
                            //    else
                            //    {

                            //        GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                            //    }
                            //}
                            //else
                            //{
                            //    upperParallelBranchFLActivity.InitExecStatus();

                            //    UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                            //    list0.Remove(list1);
                            //}
                        }
                        // A ——>      ——> C(有问题)
                        //         None
                        // B ——>      ——> D
                        else
                        {
                            List<object> list01 = (List<object>)o01;
                            List<string> branches = new List<string>();

                            if (true)//直接退回
                            {
                                foreach (object o1 in list01)
                                {
                                    list1 = (List<object>)o1;
                                    if (list1.Count > 0)
                                    {
                                        string previousFLActivityName = list1[list1.Count - 1].ToString();
                                        FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);

                                        branches.Add(previousFLActivity.UpperParallelBranch);

                                        UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                                        if (previousFLActivity is IEventWaiting)
                                        {
                                            nextFLActivities.Add(previousFLActivity);
                                        }
                                        else if (previousFLActivity is IFLProcedureActivity)
                                        {
                                            if (previousFLActivity is IFLProcedureActivity)
                                            {
                                                Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                                            }
                                            nextFLActivities.Add(previousFLActivity);

                                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                        }
                                        else
                                        {
                                            GetPreviousFLActivites(previousFLActivity, nextFLActivities);
                                        }
                                    }
                                }

                                // --------------------------------------------------------------------------------------------------
                                upperParallelBranchFLActivity.InitExecStatus();

                                upperParallelFLActivity.InitExecStatus();

                                FLActivity tempFLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
                                string upperParallel = tempFLActivity.UpperParallel;

                                FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
                                foreach (string branch in branches)
                                {
                                    ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
                                    ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
                                }
                                // --------------------------------------------------------------------------------------------------

                                _p.Remove(list0);
                            }
                            else
                            {
                                foreach (object o1 in list01)
                                {
                                    list1 = (List<object>)o1;
                                    if (list1.Count > 0)
                                    {
                                        string previousFLActivityName = list1[list1.Count - 1].ToString();
                                        FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
                                        UnExecuteIFLControls(currentFLActivity, previousFLActivity);
                                    }
                                }

                                upperParallelBranchFLActivity.InitExecStatus();

                                list0.Remove(list1);
                            }
                        }
                    }
                }

                _flflag = 'P';
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list1"></param>
        /// <param name="list2"></param>
        private void GetWL(List<object> list1, List<string> list2)
        {
            foreach (object obj in list1)
            {
                if (obj is string)
                {
                    IEventWaiting activity = (IEventWaiting)_rootFLActivity.GetFLActivityByName(obj.ToString());
                    if (!string.IsNullOrEmpty(activity.SendToId))
                    {
                        list2.Add(activity.SendToId);
                    }
                }
                else if (obj is List<object>)
                {
                    GetWL((List<object>)obj, list2);
                }
            }
        }

        private void ReturnUpperParallel(string activityName, FLActivity previousActivity)
        {
            FLActivity activity = _rootFLActivity.GetFLActivityByName(activityName);
            if (activity != null)
            {
                if (!string.IsNullOrEmpty(activity.Location))
                {
                    //
                    UnExecuteIFLControls(activity, previousActivity);
                }
                if (!string.IsNullOrEmpty(activity.UpperParallel))
                {
                    FLActivity parent = _rootFLActivity.GetFLActivityByName(activity.UpperParallel);
                    if (parent != null)
                    {
                        parent.InitExecStatus();
                    }
                    if (activity.UpperParallel != activityName)
                    {
                        ReturnUpperParallel(activity.UpperParallel, previousActivity);
                    }

                }
                if (!string.IsNullOrEmpty(activity.UpperParallelBranch))
                {
                    FLActivity parent = _rootFLActivity.GetFLActivityByName(activity.UpperParallelBranch);
                    if (parent != null)
                    {
                        parent.InitExecStatus();
                    }
                    if (activity.UpperParallelBranch != activityName)
                    {
                        ReturnUpperParallel(activity.UpperParallel, previousActivity);
                    }

                }
            }

        }

        private List<object> GetParallelPath(string previous, string current, List<object> list)
        {
            if (list.Contains(previous) && list.Contains(current))
            {
                return list;
            }
            else
            {
                foreach (var obj in list)
                {
                    if (obj is List<object>)
                    {
                        var path = GetParallelPath(previous, current, obj as List<object>);
                        if (path != null)
                        {
                            return path;
                        }
                    }
                }
                return null;
            }
        }

        private void ReturnActivity(FLActivity activity)
        {
            foreach (var executedActivity in activity.ExecutedActivities)
            {
                executedActivity.InitExecStatus();
                if (executedActivity is FLParallelActivity)
                {
                    (executedActivity as FLParallelActivity).ExecutedBranches.Clear();
                }
               
            }
            foreach (var nextActivity in activity.NextActivities)
            {
                ReturnActivity(nextActivity);
                if (nextActivity is FLProcedureActivity)
                {
                    _flDirection = FLCore.FLDirection.GoToBack;
                    Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, nextActivity as IFLProcedureActivity);
                    //_flDirection = FLCore.FLDirection.GoToNext;
                }
            }
        }

        public List<string> GetParallelPath(FLActivity activity)
        {
            var parentAcivity = GetRealParentFLActivity(activity);
            if (parentAcivity is FLRootActivity)
            {
                return new List<string>(new string[] { parentAcivity.Name });
            }
            else
            {
                var parentPath = GetParallelPath(parentAcivity);
                if (parentAcivity is FLParallelActivity)
                {
                    parentPath.Add(parentAcivity.Name);
                }
                return parentPath;
            }
        }

        public FLActivity GetAvailableActivity(FLActivity previousFLActivity2, FLActivity currentFLActivity)
        {
            if (_flDefinitionXmlNodes == null)
            {
                _flDefinitionXmlNodes = new Hashtable();
                InitFLDefinitionXmlNodes();
            }
            var availableActivity = previousFLActivity2;
            var previousParallelPath = string.Join("->", GetParallelPath(previousFLActivity2));
            var currentParallelPath = string.Join("->", GetParallelPath(currentFLActivity));
            while (true)
            {
                if (availableActivity == null)
                {
                    throw new Exception(string.Format("Return from:{0} to {1} failed.", currentParallelPath, previousParallelPath));
                }
                var availableParallelPath = string.Join("->", GetParallelPath(availableActivity)); 
                //                       - C1
                //  P(A) - C or P(A) <
                //                       - C2
                if (currentParallelPath.Contains(availableParallelPath))
                {
                    return availableActivity;
                }
                //      - P1 -            - P1 -     - C1       
                //  A <        > C or A <        > <
                //      - P2 -            - P2 -     - C2
                else if (availableParallelPath.Contains(currentParallelPath))
                {
                    availableActivity = availableActivity.PreviousActivity;  // 继续退
                }
                //       - P1 -     - C1     P3(A) -    - P1 -     - C1 
                //   A <        > <       or         ><        > <  
                //       - P2 -     - C2        P4 -    - P2 -     - C2
                else
                {
                    if (availableParallelPath.Contains(previousParallelPath) || previousParallelPath.Contains(availableParallelPath)) //直到退出previous的平行
                    {
                        availableActivity = availableActivity.PreviousActivity;  // 继续退
                    }
                    else
                    {
                        return availableActivity;   
                    }
                }
            }
        }

        /// <summary>
        /// 取得上一Activity的集合
        /// </summary>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="previousFLActivity2">上一Activity</param>
        /// <param name="nextFLActivities">下一Activity的集合</param>
        private void GetPreviousFLActivites2(FLActivity currentFLActivity, FLActivity previousFLActivity2, List<FLActivity> nextFLActivities)
        {
            if (Version == "2.0")
            {
                //比对2个activity的位置
                var availableActivity = GetAvailableActivity(previousFLActivity2, currentFLActivity);
                if (availableActivity == previousFLActivity2) //直接退回
                {
                    ReturnActivity(previousFLActivity2);
                    nextFLActivities.Add(previousFLActivity2);

                   // LastActivity = availableActivity == null ? string.Empty : availableActivity.Name;
                    LastActivity = availableActivity == null ? string.Empty : (availableActivity.PreviousActivity == null ? string.Empty : availableActivity.PreviousActivity.Name);
                }
                else
                {
                    //var currentParallelPath = string.Join("->", GetParallelPath(currentFLActivity));
                    foreach (var nextActivity in availableActivity.NextActivities) //退进平行的话(只有退回上一步才会), 退回到平行关卡的上一关
                    {
                        //var nextParallelPath = string.Join("->", GetParallelPath(nextActivity));
                        //if (nextParallelPath.Contains(currentParallelPath))
                        //{
                        ReturnActivity(nextActivity);
                        if (nextActivity is IEventWaiting)
                        {
                            nextFLActivities.Add(nextActivity);
                        }
                        //}
                    }
                    foreach (var executedActivity in availableActivity.ExecutedActivities)
                    {
                        if (executedActivity is FLParallelActivity)
                        {
                            (executedActivity as FLParallelActivity).ExecutedBranches.Clear(); //清除ExecutedBranches
                        }
                    }
                    LastActivity = availableActivity.Name;
                }
            }
            else
            {
                if (_p.Count <= 1)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(_clientInfo[0]))[0]), "FLTools", "FLWebNavigator", "ReturnToEnd");
                    throw new FLException(message);
                    return;
                }
                //同时要退回当前activity的上级
                object current = _p[_p.Count - 1];
                if (current is string)
                {
                    ReturnUpperParallel((string)current, previousFLActivity2);
                }
                else
                {
                    if (previousFLActivity2.UpperParallel != string.Empty && previousFLActivity2.UpperParallel == currentFLActivity.UpperParallel)
                    {
                        List<object> list = (List<object>)current;
                        // 特别的在平行签核同一边的退回
                        var path = GetParallelPath(previousFLActivity2.Name, currentFLActivity.Name, list);
                        if (path != null)
                        {
                            for (int i = path.Count - 1; i >= 0; i--)
                            {
                                var obj = path[i];
                                if (obj is string)
                                {
                                    var activity = _rootFLActivity.GetFLActivityByName(obj.ToString());
                                    if (activity != null)
                                    {
                                        if (obj.ToString() == previousFLActivity2.Name)
                                        {
                                            nextFLActivities.Add(activity);
                                            break;
                                        }
                                        else
                                        {
                                            activity.InitExecStatus();
                                        }
                                    }

                                }
                            }
                            return;
                        }
                    }
                    else
                    {
                        List<object> list = (List<object>)current;
                        foreach (object obj in list)
                        {
                            List<object> branch = obj as List<object>;
                            foreach (object str in branch)
                            {
                                ReturnUpperParallel(str.ToString(), previousFLActivity2);
                            }
                        }
                    }
                }
                object o0 = _p[_p.Count - 2];
                if (o0 is string)
                {
                    FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(o0.ToString());

                    UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                    //当前步是会签时。这里会移除不掉
                    if (current is string)
                    {
                        _p.Remove(currentFLActivity.Name);
                    }
                    else
                    {
                        List<object> list = (List<object>)current;
                        foreach (object item in list)
                        {
                            if (item is string && item.Equals(currentFLActivity.Name))
                            {
                                _p.Remove(current);
                                break;
                            }
                            else
                            {
                                List<object> list1 = (List<object>)item;
                                if (list1.Contains(currentFLActivity.Name))
                                {
                                    _p.Remove(current);
                                    break;
                                }
                            }
                        }
                    }
                    if (previousFLActivity is IEventWaiting && previousFLActivity.Name.ToLower() == previousFLActivity2.Name.ToLower())
                    {
                        nextFLActivities.Add(previousFLActivity);

                        //Hashtable table = _rootFLActivity.GetAllChildFLActivities();
                        //foreach (object key in table.Keys)
                        //{
                        //    object o3 = table[key];
                        //    if (o3 == null)
                        //    {
                        //        continue;
                        //    }

                        //    FLActivity temp2FLActivity = (FLActivity)o3;
                        //    if (temp2FLActivity is FLIfElseActivity)
                        //    {
                        //        ((FLIfElseActivity)temp2FLActivity).InitExecStatus();
                        //        foreach (FLActivity child in ((FLIfElseActivity)temp2FLActivity).ChildFLActivities)
                        //        {
                        //            child.InitExecStatus();
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        if (previousFLActivity is IEventWaiting && !string.IsNullOrEmpty(((IEventWaiting)previousFLActivity).SendToId))
                        {
                            FLNotifyActivity notifyActivity = new FLNotifyActivity();
                            notifyActivity.Name = "Notify_" + previousFLActivity.Name;
                            // -------------------------------------------------------------------
                            //notifyActivity.SendToField = ((IEventWaiting)previousFLActivity).SendToField;
                            //notifyActivity.SendToKind = ((IEventWaiting)previousFLActivity).SendToKind;
                            //notifyActivity.SendToRole = ((IEventWaiting)previousFLActivity).SendToRole;
                            notifyActivity.SendToKind = SendToKind.Role;
                            notifyActivity.SendToRole = ((IEventWaiting)previousFLActivity).SendToId;
                            // -------------------------------------------------------------------
                            notifyActivity.FormName = ((IEventWaiting)previousFLActivity).FormName;
                            notifyActivity.WebFormName = ((IEventWaiting)previousFLActivity).WebFormName;
                            notifyActivity.UrgentTime = (int)((IEventWaiting)previousFLActivity).UrgentTime;
                            notifyActivity.TimeUnit = ((IEventWaiting)previousFLActivity).TimeUnit;
                            notifyActivity.NavigatorMode = ((IEventWaiting)previousFLActivity).NavigatorMode;
                            notifyActivity.FLNavigatorMode = ((IEventWaiting)previousFLActivity).FLNavigatorMode;
                            notifyActivity.UserId = ((IEventWaitingExecute)previousFLActivity).UserId;
                            notifyActivity.RoleId = ((IEventWaitingExecute)previousFLActivity).RoleId;

                            nextFLActivities.Add(notifyActivity);
                        }
                        else if (previousFLActivity is IFLProcedureActivity)
                        {
                            if (previousFLActivity is IFLProcedureActivity)
                            {
                                Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                            }
                            nextFLActivities.Add(previousFLActivity);
                        }
                        GetPreviousFLActivites2(previousFLActivity, previousFLActivity2, nextFLActivities);
                    }
                }
                else
                {
                    List<object> list01 = (List<object>)_p[_p.Count - 2];
                    bool isDetailReturn = false;
                    List<string> branches = new List<string>();
                    foreach (object o1 in list01)
                    {
                        List<object> list1 = (List<object>)o1;
                        if (list1.Count > 0)
                        {
                            string previousFLActivityName = list1[list1.Count - 1].ToString();
                            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);
                            if (previousFLActivity.Location == previousFLActivity2.Location)
                            {
                                isDetailReturn = true;
                                branches.Add(previousFLActivity.UpperParallelBranch);

                                UnExecuteIFLControls(currentFLActivity, previousFLActivity);

                                if (previousFLActivity is IEventWaiting)
                                {
                                    nextFLActivities.Add(previousFLActivity);
                                }
                            }
                        }
                    }

                    if (isDetailReturn)
                    {
                        FLActivity tempFLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
                        string upperParallel = tempFLActivity.UpperParallel;
                        FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
                        foreach (string branch in branches)
                        {
                            ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
                            ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
                        }

                        _p.Remove(currentFLActivity.Name);
                    }
                    else
                    {
                        branches = new List<string>();
                        foreach (object o1 in list01)
                        {
                            List<object> list1 = (List<object>)o1;
                            if (list1.Count > 0)
                            {
                                string previousFLActivityName = list1[list1.Count - 1].ToString();
                                FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(previousFLActivityName);

                                branches.Add(previousFLActivity.UpperParallelBranch);
                            }
                        }

                        FLActivity temp1FLActivity = _rootFLActivity.GetFLActivityByName(((List<object>)list01[0])[0].ToString());
                        string upperParallel = temp1FLActivity.UpperParallel;
                        FLActivity parallelFLActivity = _rootFLActivity.GetFLActivityByName(upperParallel);
                        parallelFLActivity.InitExecStatus();
                        if (!string.IsNullOrEmpty(parallelFLActivity.Location))//退回时要初始化FlDetail
                        {
                            UnExecuteIFLControls(parallelFLActivity, previousFLActivity2);
                            //FLActivity detailActivity = _rootFLActivity.GetFLActivityByName(parallelFLActivity.Location);
                            //if (detailActivity != null)
                            //{
                            //    detailActivity.InitExecStatus();
                            //}
                        }
                        foreach (string branch in branches)
                        {
                            ((IFLParallelActivity)parallelFLActivity).ExecutedBranches.Remove(branch);
                            FLActivity previousFLActivity = _rootFLActivity.GetFLActivityByName(branch);
                            //_rootFLActivity.GetFLActivityByName(branch).InitExecStatus();//多步退回时触发procedure
                            previousFLActivity.InitExecStatus();
                            if (previousFLActivity is IFLProcedureActivity)
                            {
                                if (previousFLActivity is IFLProcedureActivity)
                                {
                                    Logic.CallServerMethod(this, FLInstanceParms, this._keyValues, this._clientInfo, previousFLActivity as IFLProcedureActivity);
                                }
                                nextFLActivities.Add(previousFLActivity);
                            }
                            ((IFLSequenceActivity)_rootFLActivity.GetFLActivityByName(branch)).SetFLDirection(FLDirection.Waiting);
                        }

                        //Hashtable table = _rootFLActivity.GetAllChildFLActivities();
                        //foreach (object key in table.Keys)
                        //{
                        //    object o3 = table[key];
                        //    if (o3 == null)
                        //    {
                        //        continue;
                        //    }

                        //    FLActivity temp2FLActivity = (FLActivity)o3;
                        //    if (temp2FLActivity is IEventWaiting)
                        //    {
                        //        ((IEventWaitingExecute)(temp2FLActivity)).InitExecStatus();
                        //    }
                        //    else if (temp2FLActivity is FLIfElseActivity)
                        //    {
                        //        ((FLIfElseActivity)temp2FLActivity).InitExecStatus();
                        //        foreach (FLActivity child in ((FLIfElseActivity)temp2FLActivity).ChildFLActivities)
                        //        {
                        //            child.InitExecStatus();
                        //        }
                        //    }
                        //}

                        _p.Remove(list01);
                        GetPreviousFLActivites2(currentFLActivity, previousFLActivity2, nextFLActivities);
                    }

                }

                _flflag = 'P';
            }
        }

        /// <summary>
        /// 取得是否支持多步退回
        /// </summary>
        /// <param name="nextFLActivity"></param>
        /// <returns></returns>
        public bool GetMultiStepReturn(FLActivity nextFLActivity)
        {
            if (Version == "2.0")
            {
                return true;
            }
            else
            {
                string upperParallel = nextFLActivity.UpperParallel;
                if (string.IsNullOrEmpty(upperParallel))
                {
                    return true;
                }
                else
                {
                    IFLParallelActivity parallel = (IFLParallelActivity)_rootFLActivity.GetFLActivityByName(upperParallel);
                    if (string.IsNullOrEmpty(parallel.Description) || parallel.Description.ToLower() == "and")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }
    }

    [Serializable]
    public class __FLInstanceCreatedEventArgs : EventArgs
    {
        public __FLInstanceCreatedEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceSubmitEventArgs : EventArgs
    {
        public __FLInstanceSubmitEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceApproveEventArgs : EventArgs
    {
        public __FLInstanceApproveEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceReturnEventArgs : EventArgs
    {
        public __FLInstanceReturnEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceRejectEventArgs : EventArgs
    {
        public __FLInstanceRejectEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceNotifyEventArgs : EventArgs
    {
        public __FLInstanceNotifyEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstanceRetakeEventArgs : EventArgs
    {
        public __FLInstanceRetakeEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstancePlusApproveEventArgs : EventArgs
    {
        public __FLInstancePlusApproveEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstancePlusReturnEventArgs : EventArgs
    {
        public __FLInstancePlusReturnEventArgs()
            : base()
        {

        }
    }

    [Serializable]
    public class __FLInstancePauseEventArgs : EventArgs
    {
        public __FLInstancePauseEventArgs()
            : base()
        {

        }
    }
}