using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections;
using FLRuntime.Hosting;
using System.Workflow.Runtime.Tracking;
using System.IO;
using System.Reflection;
using System.Workflow.ComponentModel;
using System.Data;

namespace FLRuntime
{
    [Serializable]
    public class FLRuntime //: IFLRuntime
    {
        private Hashtable _services;

        public FLRuntime(object service)
        {
            _services = new Hashtable();

            if (service != null)
            {
                AddService(service);
            }

            Global.FLRuntime = this;
        }

        public FLRuntime()
            : this(null)
        {
        }

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="service">服务实体</param>
        public void AddService(object service)
        {
            Type type = service.GetType();
            if (!_services.ContainsKey(type))
            {
                _services.Add(type, service);
            }
            else
            {
                throw new Exception("");
            }
        }

        /// <summary>
        /// 取得服务
        /// </summary>
        /// <param name="servieType">服务类型</param>
        /// <returns></returns>
        public object GetService(Type servieType)
        {
            return _services[servieType];
        }

        /// <summary>
        /// 开始运行时
        /// </summary>
        public void Start()
        {

        }

        /// <summary>
        /// 停止运行时
        /// </summary>
        public void Stop()
        {

        }

        /// <summary>
        /// 载入流程
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns>流程</returns>
        public FLInstance LoadFLInstance(Guid flInstanceId, object[] clientInfo)
        {
            FLInstance flInstance = null;
            object o = GetService(typeof(FLPersistenceService));

            if (o != null)
            {
                FLPersistenceService flPersistenceService = (FLPersistenceService)o;
                flInstance = flPersistenceService.DepersistenceFL(flInstanceId, clientInfo);
            }

            return flInstance;
        }

        /// <summary>
        /// 卸载流程
        /// </summary>
        /// <param name="flInstance">流程</param>
        private void UnloadFLInstance(FLInstance flInstance)
        {
            UnloadFLInstance(flInstance, false);
        }

        /// <summary>
        /// 卸载流程
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="isFirstUnload">是否第一次卸载</param>
        private void UnloadFLInstance(FLInstance flInstance, bool isFirstUnload)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            object[] clientInfo = flInstance.GetClientInfo();
            if (clientInfo == null)
            {
                throw new Exception("FLRuntime.UnloadFLInstance");
            }

            object o = GetService(typeof(FLPersistenceService));
            if (o != null)
            {
                FLPersistenceService flPersistenceService = (FLPersistenceService)o;
                if (isFirstUnload)
                {
                    Guid flDefiniationId = flInstance.FLDefinitionId;
                    flPersistenceService.PersistenceFLDefinition(flDefiniationId, flInstance.FLDefinition.Name, flInstance.GetFLDefinitionXml().InnerXml, clientInfo);
                }
                flPersistenceService.PersistenceFL(flInstance, clientInfo);
            }

            flInstance = null;
        }

        /// <summary>
        /// 取消流程
        /// </summary>
        /// <param name="flInstance">流程</param>
        private void RejectFLInstance(FLInstance flInstance)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            object[] clientInfo = flInstance.GetClientInfo();
            if (clientInfo == null)
            {
                throw new Exception("FLRuntime.UnloadFLInstance");
            }

            object o = GetService(typeof(FLPersistenceService));
            if (o != null)
            {
                FLPersistenceService flPersistenceService = (FLPersistenceService)o;
                flPersistenceService.DeleteFL(flInstance.FLInstanceId, clientInfo);
            }

            flInstance = null;
        }

        /// <summary>
        /// 取得流程
        /// </summary>
        /// <param name="instanceId">流程Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public FLInstance GetFLInstance(Guid instanceId, object[] clientInfo)
        {
            FLInstance flInstance = LoadFLInstance(instanceId, clientInfo);

            if (flInstance != null)
            {
                flInstance.SetFLRuntime(this);
                flInstance.SetClientInfo(clientInfo);
            }

            return flInstance;
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="flDefinitionFile">流程定义XOML文件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="hostDataSet">宿主表</param>
        /// <param name="orgKind">用户OrgKind</param>
        /// <returns></returns>
        public FLInstance CreateFLInstance(Guid flInstanceId, string flDefinitionFile, /* XmlReader flDefinitionReader,*/ object[] clientInfo, string roleID, DataSet hostDataSet, string orgKind)
        {
            return CreateFLInstance(flInstanceId, flDefinitionFile, null, clientInfo, roleID,  hostDataSet, orgKind);
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="flDefinitionFile">流程定义XOML文件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="hostDataSet">宿主表</param>
        /// <returns></returns>
        public FLInstance CreateFLInstance(string flDefinitionFile, /* XmlReader flDefinitionReader,*/ object[] clientInfo, string roleID, DataSet hostDataSet, string orgKind)
        {
            Guid instanceId = Guid.NewGuid();

            return CreateFLInstance(instanceId, flDefinitionFile, null, clientInfo, roleID, hostDataSet, orgKind);
        }

        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="flInstanceId">流程Id</param>
        /// <param name="flDefinitionFile">流程定义XOML文件</param>
        /// <param name="flRulesFile">流程规则文件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="hostDataSet">宿主表</param>
        /// <param name="orgKind">用户OrgKind</param>
        /// <returns></returns>
        public FLInstance CreateFLInstance(Guid flInstanceId, string flDefinitionFile, string flRulesFile,/* XmlReader flDefinitionReader, XmlReader flRulesReader, */ object[] clientInfo, string roleID, DataSet hostDataSet, string orgKind)
        {
            //if (_flInstances.ContainsKey(flInstanceId))
            //{
            //    throw new Exception("FLRuntime is existed " + flInstanceId.ToString());
            //}

            //FLInstance instance = new FLInstance(flInstanceId, this, flDefinitionReader, flRulesReader);
            FLInstance instance = new FLInstance(flInstanceId, this, flDefinitionFile, flRulesFile, clientInfo, hostDataSet, orgKind);
            instance.SetFLRuntime(this);
            instance.SetClientInfo(clientInfo);
            instance.SetCreator(((object[])clientInfo[0])[1].ToString(), roleID);   // 设置创建者

            // 根据启动的服务添加相应的事件
            // 添加__Start事件
            instance.__Created += new FLInstance.__CreatedEventHandler(Instance___Created);
            instance.__Submit += new FLInstance.__SubmitEventHandler(Instance___Submit);
            instance.__Approve += new FLInstance.__ApproveEventHandler(instance___Approve);
            instance.__Return += new FLInstance.__ReturnEventHandler(instance___Return);
            instance.__Reject += new FLInstance.__RejectEventHandler(instance___Reject);
            instance.__Retake += new FLInstance.__RetakeEventHandler(instance___Retake);
            instance.__Pause += new FLInstance.__PauseEventHandler(instance___Pause);
            instance.__PlusApprove += new FLInstance.__PlusApproveEventHandler(instance___PlusApprove);
            instance.__PlusReturn += new FLInstance.__PlusReturnEventHandler(instance___PlusReturn);

            return instance;
        }

        void instance___PlusReturn(object sender, __FLInstancePlusReturnEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {
                UnloadFLInstance((FLInstance)sender);
            }
        }

        private void instance___PlusApprove(object sender, __FLInstancePlusApproveEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {
                UnloadFLInstance((FLInstance)sender);
            }
        }

        // 向Db中写Definition等信息。
        private void Instance___Created(object sender, __FLInstanceCreatedEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {
                UnloadFLInstance((FLInstance)sender, true);
            }
        }

        private void Instance___Submit(object sender, __FLInstanceSubmitEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {
                if (((FLInstance)sender).FLFlag == 'Z')
                {
                    RejectFLInstance((FLInstance)sender);
                }
                else
                {
                    UnloadFLInstance((FLInstance)sender);
                }
            }
        }

        private void instance___Approve(object sender, __FLInstanceApproveEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {

                if (((FLInstance)sender).FLFlag == 'Z')   
                {
                    RejectFLInstance((FLInstance)sender);
                }
                else
                {
                    UnloadFLInstance((FLInstance)sender);
                }
            }
        }

        private void instance___Retake(object sender, __FLInstanceRetakeEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            if (s != null)
            {
                UnloadFLInstance((FLInstance)sender);
            }
        }

        private void instance___Return(object sender, __FLInstanceReturnEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            if (s != null)
            {
                if (((FLInstance)sender).FLFlag == 'Z')    // FLInstance已经退到取消
                {
                    RejectFLInstance((FLInstance)sender);
                }
                else
                {
                    UnloadFLInstance((FLInstance)sender);
                }
            }
        }

        private void instance___Reject(object sender, __FLInstanceRejectEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            if (s != null)
            {
                RejectFLInstance((FLInstance)sender);
            }
        }

        private void instance___Pause(object sender, __FLInstancePauseEventArgs e)
        {
            object s = GetService(typeof(FLPersistenceService));
            bool v = ((FLInstance)sender).V;
            if (s != null && v)
            {
                UnloadFLInstance((FLInstance)sender);
            }
        }
    }
}
