using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using System.Workflow.Runtime;
using System.Reflection;
using System.Workflow.Runtime.Tracking;
using System.Threading;
using System.Workflow.ComponentModel;
using System.Xml;
using System.IO;
using FLTools.ComponentModel;
using System.Data;
using System.Collections;

namespace FLRuntime
{
    public class InstanceManager
    {
        //private string dllName = "Microsoft.Workflow.Extension.dll";
        //private string className = "Microsoft.Workflow.Extension.XomlParser";
        //private string methodName = "GetWorkflowDefinition";

        private void BeginWorkFlowTransaction(object[] clientInfo)
        {
            ((object[])clientInfo[0])[7] = "EEP_WorkFlow";//修改transaction flag
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            remoteModule.CallMethod(clientInfo, "GLModule", "BeginWorkFlowTransaction", null);
        }

        private void ComitWorkFlowTransaction(object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            remoteModule.CallMethod(clientInfo, "GLModule", "ComitWorkFlowTransaction", null);
        }

        private void RollBackWorkFlowTransaction(object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            remoteModule.CallMethod(clientInfo, "GLModule", "RollBackWorkFlowTransaction", null);
        }


        /// <summary>
        /// 流程提交
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Submit(object[] parameters, object[] clientInfo)
        {
            FLInstance flInstance = null;
            Guid flInstanceId = Guid.NewGuid();
            object[] flInstanceParms = (object[])parameters[1];
            object[] keyValues = (object[])parameters[2];


            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                string xomlName = (string)flInstanceParms[0];
                string orgKind = (string)flInstanceParms[8];
                string roleId = ((object[])parameters[1])[5].ToString();
                //XmlReader flDefinitionReader = XmlReader.Create(xomlName);

                Activity temp = FLInstance.GetActivityByXoml(xomlName, string.Empty);
                IFLRootActivity root = (IFLRootActivity)temp;
                string hostTableName = root.TableName;

                if (flInstanceParms[1] != null && (string)flInstanceParms[1] != string.Empty)
                {
                    //XmlReader flRulesReader = XmlReader.Create((string)flInstanceParms[1]);
                    // flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, flDefinitionReader, flRulesReader, clientInfo);
                    flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, xomlName, (string)flInstanceParms[1], clientInfo, roleId, null, orgKind);
                }
                else
                {
                    // flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, flDefinitionReader, clientInfo);
                    flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, xomlName, clientInfo, roleId, null, orgKind);
                    
                }

                // 判断HostTable的记录是否在流程中
                DataSet hostDataSet = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo); //增加eeplias属性后要通过flinstance去取得hosttable
                if (hostDataSet == null || hostDataSet.Tables.Count == 0 || hostDataSet.Tables[0] == null || hostDataSet.Tables[0].Rows.Count == 0)
                {
                    //String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "HostTableNotContainRecord"), ((IFLRootActivity)flInstance.RootFLActivity).TableName, keyValues[1].ToString());
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "HostTableNotContainRecord"), hostTableName, keyValues[1].ToString());
                    throw new FLException(message);
                }
                flInstance._hostDataSet = hostDataSet;

                flInstance.OnCreated(flInstance, new __FLInstanceCreatedEventArgs());

                DataRow hostRow = hostDataSet.Tables[0].Rows[0];

                string tableFlowFlag = hostRow["FlowFlag"].ToString();
                if (tableFlowFlag == "n")
                {
                    //清空flag，可以再重新提交
                    HostTable.ClearFLFlag(flInstance, keyValues, clientInfo);
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "RecordError");
                    throw new FLException(3, message);
                }

                tableFlowFlag = hostRow["FlowFlag"].ToString().ToUpper();
                if (tableFlowFlag == "N" || tableFlowFlag == "P")
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "RecordIsAlreadyInFL");
                    throw new FLException(3, message);
                }

                string webUrl = ((object[])flInstanceParms)[7].ToString();
                flInstance.SetWebUrl(webUrl);

                flInstance.SetKeyValues(keyValues);
                flInstance.AddCacheFLInstanceParm(flInstanceParms);

                string userId = ((object[])clientInfo[0])[1].ToString();
                //string roleId = ((object[])parameters[1])[5].ToString();
                bool isUrgent = Convert.ToBoolean(flInstanceParms[3]);
                flInstance.Submit(userId, roleId, isUrgent, tableFlowFlag);

                if (flInstance.FLFlag != 'Z')
                {
                    flInstance.FLFlag = 'n';//先写n
                    HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    flInstance.FLFlag = 'N';//再写N
                }

                flInstanceParms[0] = null;
                flInstanceParms[1] = flInstance.CurrentFLActivity.Name;

                // 验证不过，Submit的验证throw放在前面，而Approve的放在后面
                if (!flInstance.V)
                {
                    if (string.IsNullOrEmpty(flInstance.VM))
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ValidateFail"), flInstance.VN);
                        throw new FLException(2, message);
                    }
                    else
                    {
                        throw new FLException(2, flInstance.VM);
                    }
                }

                if (flInstance.NextFLActivities.Count != 0 && flInstance.NextFLActivities[0] is IFLRejectActivity)
                {
                    FLNotifyActivity flNotifyActivity = (FLNotifyActivity)((FLRejectActivity)flInstance.NextFLActivities[0]);
                    flInstance.NextFLActivities.Clear();
                    flInstance.NextFLActivities.Add(flNotifyActivity);

                    // Notify
                    Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, string.Empty, keyValues, clientInfo);

                    // Reject
                    flInstance.Reject();
                    Logic.DeleteToDo(flInstanceId, clientInfo);
                }
                else
                {
                    Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, string.Empty, keyValues, clientInfo);
                }

                Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);

                flInstance.OnSubmit(flInstance, new __FLInstanceSubmitEventArgs());

                if (flInstance.FLFlag == 'N' || flInstance.FLFlag == 'C')
                {

                    //if sendids contain userid approve
                    string nextSendToIds = string.Empty;
                    foreach (FLActivity a in flInstance.NextFLActivities)
                    {
                        if (!(a is IEventWaiting))
                        {
                            continue;
                        }
                        bool sendtoself = false;
                        if (((IFLRootActivity)flInstance.RootFLActivity).SkipForSameUser)
                        {
                            if (((IEventWaiting)a).SendToKind == SendToKind.Applicate || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                            {
                                if (userId == ((IEventWaiting)a).SendToId)
                                {
                                    sendtoself = true;
                                }
                            }
                            else
                            {
                                List<string> users = Global.GetUsersIdsByRoleId(((IEventWaiting)a).SendToId, clientInfo);
                                //加入代理人
                                if (users.Count > 0)
                                {
                                    string agent = Global.GetAgent(((IEventWaiting)a).SendToId, users[0], flInstance.RootFLActivity.Description, clientInfo);
                                    if (!string.IsNullOrEmpty(agent))
                                    {
                                        object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                                        if (parAgent != null && Convert.ToBoolean(parAgent)) { }
                                        else
                                        {
                                            users.Add(agent);
                                        }
                                    }
                                }
                                sendtoself = users.Contains(userId);
                                ((object[])parameters[1])[5] = ((IEventWaiting)a).SendToId; //reset role

                            }
                        }
                        if (sendtoself)
                        {
                            HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                            needComit = false;
                            object[] ret = Approve(flInstanceParms[1].ToString(), a.Name, flInstance.FLInstanceId, parameters, clientInfo);
                            if (ret != null && (int)ret[0] == 0)
                            {
                                return new object[] { ret[0], ret[1], flInstanceId, flInstance.CurrentFLActivity.Name };
                            }
                            else
                            {
                                return ret;
                            }
                        }
                        else
                        {
                            if (nextSendToIds != string.Empty)
                            {
                                nextSendToIds += ";";
                            }
                            nextSendToIds += a.Name + "|" + ((IEventWaiting)a).SendToId;
                            if (((IEventWaiting)a).SendToKind == SendToKind.Applicate
                                || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                            {
                                nextSendToIds += ":UserId";
                            }
                        }
                    }
                    HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    return new object[] { 0, nextSendToIds, flInstanceId, flInstance.CurrentFLActivity.Name };
                }
                else if (flInstance.FLFlag == 'Z')
                {
                    HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    // End
                    return new object[] { 0, "60585C77-60E1-4e6f-A2E2-3BBBAD6B4C9E" };
                }
                else if (flInstance.FLFlag == 'X')
                {
                    HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    return new object[] { 0, "512F4277-0D41-441c-BF16-D96B04580C2E" };
                }
                else
                {
                    HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    // Waiting
                    return new object[] { 0, null };
                }
            }
            catch (Exception e)
            {
                if (!(e is FLException && ((FLException)e).Type == 3) && flInstance != null)
                {
                    // 验证没有通过不需要添加ToDoList和ToDoHis。
                    string flPath = Guid.NewGuid().ToString() + ";" + flInstance.RootFLActivity.ChildFLActivities[0].Name;
                    List<FLActivity> list = new List<FLActivity>();
                    list.Add(flInstance.RootFLActivity.ChildFLActivities[0]);
                    flInstance.NextFLActivities = list;
                    //Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);

                    if (!flInstance.V)
                    {
                        flInstance.FLFlag = 'V';

                        // 把HostTable的'N'->'V'
                        HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);
                    }

                    else
                    {
                        HostTable.ClearFLFlag(flInstance, keyValues, clientInfo);
                    }

                }
                else
                {
                    needComit = false;
                    RollBackWorkFlowTransaction(clientInfo);
                }
                //else
                //{
                //    HostTable.ClearFLFlag(flInstance, keyValues, clientInfo);
                //}

                if (e is FLException && (((FLException)e).Type == 2 || ((FLException)e).Type == 3))
                {
                    return new object[] { 2, e.Message };
                }
                else
                {
                    return new object[] { 1, e.Message };
                }
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程签核
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Approve(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                string userId = ((object[])clientInfo[0])[1].ToString();
                string roleId = ((object[])parameters[1])[5].ToString();

                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string currentFLActivityName = (string)flInstanceParms[1];
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }
                if (string.IsNullOrEmpty(previousFLActivityName) && string.IsNullOrEmpty(flInstance.CreateRole))
                {
                    flInstance.SetCreator(userId, roleId);
                }
                // -------------------------------------------------------------
                string flowDescription = flInstance.RootFLActivity.Description;
                bool isAgented = Global.GetIsAgented(roleId, userId, flowDescription, clientInfo);
                if (isAgented)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "RoleIdIsAgented");
                    throw new FLException(2, message);
                }
                // -------------------------------------------------------------

                string plusRoles = Logic.GetPlusRoles(flInstance, previousFLActivityName + ";" + currentFLActivityName, clientInfo);
                if (!string.IsNullOrEmpty(plusRoles))
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "FLInstanceIsPlused");
                    throw new FLException(2, message);
                }

                string webUrl = ((object[])flInstanceParms)[7].ToString();
                flInstance.SetWebUrl(webUrl);

                flInstance.SetKeyValues(keyValues);
                flInstance.AddCacheFLInstanceParm(flInstanceParms);

                bool isUrgent = Convert.ToBoolean(flInstanceParms[3]);
                flInstance.Approve(previousFLActivityName, currentFLActivityName, userId, roleId, isUrgent);

                if (flInstance.NextFLActivities.Count != 0 && flInstance.NextFLActivities[0] is IFLRejectActivity)
                {
                    FLNotifyActivity flNotifyActivity = (FLNotifyActivity)((FLRejectActivity)flInstance.NextFLActivities[0]);
                    flInstance.NextFLActivities.Clear();
                    flInstance.NextFLActivities.Add(flNotifyActivity);

                    // Reject
                    flInstance.Reject();

                    // Notify
                    string flPath = previousFLActivityName + ";" + currentFLActivityName;
                    Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);

                    Logic.DeleteToDo(flInstanceId, clientInfo);
                }
                else if (flInstance.NextFLActivities.Count != 0 && flInstance.NextFLActivities[0] is IFLGotoActivity)
                {
                    var activityName = (flInstance.NextFLActivities[0] as IFLGotoActivity).ActivityName;
                    //return activity

                    ((object[])parameters[1])[0] = activityName;
                    needComit = false;
                    return Return2(parameters, clientInfo);


                }
                else
                {
                    string flPath = previousFLActivityName + ";" + currentFLActivityName;
                    Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);
                }

                // 验证不过
                if (!flInstance.V)
                {
                    if (string.IsNullOrEmpty(flInstance.VM))
                    {
                        String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "FLInstance", "ValidateFail"), flInstance.VN);
                        throw new FLException(2, message);
                    }
                    else
                    {
                        throw new FLException(2, flInstance.VM);
                    }
                }

                Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);

                HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                flInstance.OnApprove(flInstance, new __FLInstanceApproveEventArgs());

                if (flInstance.FLFlag == 'N' || flInstance.FLFlag == 'P')
                {
                    //if sendids contain userid approve
                    string nextSendToIds = string.Empty;
                    foreach (FLActivity a in flInstance.NextFLActivities)
                    {
                        if (!(a is IEventWaiting))
                        {
                            continue;
                        }
                        bool sendtoself = false;
                        if (((IFLRootActivity)flInstance.RootFLActivity).SkipForSameUser)
                        {
                            if (((IEventWaiting)a).SendToKind == SendToKind.Applicate || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                            {
                                if (userId == ((IEventWaiting)a).SendToId)
                                {
                                    sendtoself = true;
                                }
                            }
                            else
                            {
                                List<string> users = Global.GetUsersIdsByRoleId(((IEventWaiting)a).SendToId, clientInfo);
                                //加入代理人
                                if (users.Count > 0)
                                {
                                    string agent = Global.GetAgent(((IEventWaiting)a).SendToId, users[0], flInstance.RootFLActivity.Description, clientInfo);
                                    if (!string.IsNullOrEmpty(agent))
                                    {
                                        object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                                        if (parAgent != null && Convert.ToBoolean(parAgent)) { }
                                        else
                                        {
                                            users.Add(agent);
                                        }
                                    }
                                }

                                sendtoself = users.Contains(userId);
                                ((object[])parameters[1])[5] = ((IEventWaiting)a).SendToId; //reset role
                            }
                        }
                        if (sendtoself)
                        {
                            needComit = false;
                            return Approve(currentFLActivityName, a.Name, Guid.Empty, parameters, clientInfo);
                        }
                        else
                        {
                            if (nextSendToIds != string.Empty)
                            {
                                nextSendToIds += ";";
                            }
                            nextSendToIds += a.Name + "|" + ((IEventWaiting)a).SendToId;
                            if (((IEventWaiting)a).SendToKind == SendToKind.Applicate
                                || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                            {
                                nextSendToIds += ":UserId";
                            }
                        }
                    }
                    return new object[] { 0, nextSendToIds };
                }
                else if (flInstance.FLFlag == 'Z')
                {
                    // End
                    return new object[] { 0, "60585C77-60E1-4e6f-A2E2-3BBBAD6B4C9E" };
                }
                else if (flInstance.FLFlag == 'X')
                {
                    return new object[] { 0, "512F4277-0D41-441c-BF16-D96B04580C2E" };
                }
                else
                {
                    // Waiting
                    return new object[] { 0, "906F766E-3736-403b-BB1D-132ADEC3F2E9" };
                }
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        private object[] Approve(string currentFLActivity, string nextFLActivity, Guid id, object[] parameters, object[] clientInfo)
        {
            //recreate parameters
            if (!id.Equals(Guid.Empty))
            {
                parameters[0] = id;
            }
            ((object[])parameters[1])[0] = currentFLActivity;
            ((object[])parameters[1])[1] = nextFLActivity;
            return Approve(parameters, clientInfo);
        }

        // for scheduling.
        /// <summary>
        /// 流程签核
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Approve2(object[] parameters, object[] clientInfo)
        {
            DataSet dataSet = HostTable.GetOverTimeDataSet4AutoApprove("SYS_TODOLIST", clientInfo);
            if (dataSet == null || dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
            {
                goto Label1;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                // 1
                string id = row["LISTID"].ToString();
                Guid flInstanceId = new Guid(id);


                string path = row["FLOWPATH"] == null ? string.Empty : row["FLOWPATH"].ToString();
                string[] ss = path.Split(';');
                object currentFLActivity = ss.Length == 2 ? ss[0] : string.Empty;
                object nextFLActivity = ss.Length == 2 ? ss[1] : string.Empty;

                object important = row["FLOWIMPORTANT"];
                object urgent = 1;
                object remark = "System Scheduling";
                object role = row["SENDTO_ID"];
                object provider = row["PROVIDER_NAME"];
                object emailurl = "0";
                object orgKid = string.Empty;
                object attachements = row["ATTACHMENTS"];

                // 2
                object[] flInstanceParms = new object[]{
                    currentFLActivity,nextFLActivity,important,urgent,remark,role,provider,emailurl,orgKid,attachements
                };

                // 3
                object key = row["FORM_KEYS"];
                object presentation = row["FORM_PRESENTATION"] == null || row["FORM_PRESENTATION"] == DBNull.Value ? string.Empty : row["FORM_PRESENTATION"].ToString().Replace("'", "''");
                object[] where = new object[] { key, presentation };


                object[] parameters2 = new object[] { flInstanceId, flInstanceParms, where };

                Approve(parameters2, clientInfo);
            }

        Label1:
            return new object[] { 0, null };
        }

        /// <summary>
        /// 流程签核
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Approve3(object[] parameters, object[] clientInfo)
        {
            Guid flInstanceId = (Guid)parameters[0];
            string flPath = parameters[1].ToString();

            DataSet dataSet = HostTable.GetHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, clientInfo);
            if (dataSet == null || dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
            {
                goto Label1;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string[] ss = flPath.Split(';');
                object currentFLActivity = ss.Length == 2 ? ss[0] : string.Empty;
                object nextFLActivity = ss.Length == 2 ? ss[1] : string.Empty;

                object important = row["FLOWIMPORTANT"];
                object urgent = 1;
                object remark = "Admin";
                object role = row["SENDTO_ID"];
                object provider = row["PROVIDER_NAME"];
                object emailurl = "0";
                object orgKid = string.Empty;
                object attachements = row["ATTACHMENTS"];

                // 1
                object[] flInstanceParms = new object[]{
                    currentFLActivity,nextFLActivity,important,urgent,remark,role,provider,emailurl,orgKid,attachements
                };

                // 2
                object key = row["FORM_KEYS"];
                object presentation = row["FORM_PRESENTATION"] == null || row["FORM_PRESENTATION"] == DBNull.Value ? string.Empty : row["FORM_PRESENTATION"].ToString().Replace("'", "''");
                object[] where = new object[] { key, presentation };


                object[] parameters2 = new object[] { flInstanceId, flInstanceParms, where };

                Approve(parameters2, clientInfo);
            }

        Label1:
            return new object[] { 0, null };
        }

        // for scheduling.
        /// <summary>
        /// 过期发送Email
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] DelaySendMail(object[] parameters, object[] clientInfo)
        {
            DataSet dataSet = HostTable.GetOverTimeDataSet4SendMail("SYS_TODOLIST", clientInfo);
            if (dataSet == null || dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
            {
                goto Label1;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                // 1
                string id = row["LISTID"].ToString();
                Guid flInstanceId = new Guid(id);
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);

                string path = row["FLOWPATH"] == null ? string.Empty : row["FLOWPATH"].ToString();
                string[] ss = path.Split(';');
                object currentFLActivity = ss.Length == 2 ? ss[0] : string.Empty;
                object nextFLActivity = ss.Length == 2 ? ss[1] : string.Empty;

                object important = row["FLOWIMPORTANT"];
                object urgent = 1;
                object remark = "System Scheduling";
                object role = row["SENDTO_ID"];
                object provider = row["PROVIDER_NAME"];
                object emailurl = "0";
                object orgKid = string.Empty;
                object attachements = row["ATTACHMENTS"];

                // 2
                object[] flInstanceParms = new object[]{
                    currentFLActivity,nextFLActivity,important,urgent,remark,role,provider,emailurl,orgKid,attachements
                };



                // 3
                object key = row["FORM_KEYS"];
                object presentation = row["FORM_PRESENTATION"] == null || row["FORM_PRESENTATION"] == DBNull.Value ? string.Empty : row["FORM_PRESENTATION"].ToString().Replace("'", "''");
                object[] keyValues = new object[] { key, presentation };

                // 4
                FLActivity flActivity = null;
                if (flInstance != null)
                {
                    flActivity = string.IsNullOrEmpty((string)nextFLActivity) ? null : flInstance.RootFLActivity.GetFLActivityByName(nextFLActivity.ToString());
                }

                if (role != null && role != DBNull.Value)
                {
                    string roleId = role.ToString();
                    //ccm 2012/11/07---------------------------------------------------------------------------------------------------------------
                    if (row["STATUS"].Equals("A") || row["STATUS"].Equals("AA"))
                    {
                        flInstanceParms[8] = row["SENDTO_ID"];
                        string sendtokind = row["SENDTO_KIND"].ToString();
                        if (sendtokind == "2")
                        {
                            flInstanceParms[8] = "U:" + row["SENDTO_ID"].ToString();
                        }

                        
                    }
                    //------------------------------------------------------------------------------------------------------------------
                    // 2
                    // 2
                    flInstance.CurrentFLActivity = flInstance.RootFLActivity.GetFLActivityByName(currentFLActivity.ToString());
                    Email.SendTo2(flInstance, flInstanceParms, keyValues, roleId, flActivity, clientInfo);
                }
            }

        Label1:
            return new object[] { 0, null };
        }

        /// <summary>
        /// 流程退回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Return(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string nextFLActivityName = (string)flInstanceParms[1];

                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "FirstStepNotReturn");
                    throw new FLException(message);
                }

                string plusRoles = Logic.GetPlusRoles(flInstance, previousFLActivityName + ";" + nextFLActivityName, clientInfo);
                if (!string.IsNullOrEmpty(plusRoles))
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "FLInstanceIsPlused");
                    throw new FLException(2, message);
                }

                flInstance.SetKeyValues(keyValues);
                flInstance.AddCacheFLInstanceParm(flInstanceParms);

                string userId = ((object[])clientInfo[0])[1].ToString();
                string roleId = ((object[])parameters[1])[5].ToString();
                bool isUrgent = Convert.ToBoolean(flInstanceParms[3]);
                flInstance.Return(previousFLActivityName, nextFLActivityName, userId, roleId, isUrgent);

                HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                string flPath = previousFLActivityName + ";" + nextFLActivityName;
                Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);

                if (((IFLRootActivity)flInstance.RootFLActivity).NotifySendMail)
                {
                    Email.SendTo4(flInstance, flInstanceParms, keyValues, clientInfo);
                }

                flInstance.OnReturn(flInstance, new __FLInstanceReturnEventArgs());

                if (flInstance.FLFlag != 'Z')
                {
                    string nextSendToIds = string.Empty;
                    foreach (FLActivity a in flInstance.NextFLActivities)
                    {
                        if (!(a is IEventWaiting))
                        {
                            continue;
                        }
                        if (nextSendToIds != string.Empty)
                        {
                            nextSendToIds += ";";
                        }
                        nextSendToIds += a.Name + "|" + ((IEventWaiting)a).SendToId;
                        if (((IEventWaiting)a).SendToKind == SendToKind.Applicate
                            || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                        {
                            nextSendToIds += ":UserId";
                        }
                    }

                    if (nextSendToIds.Length == 0)
                    {
                        string wl = string.Empty;
                        foreach (string w in flInstance.WL)
                        {
                            if (wl.Length > 0)
                                wl += ";";

                            wl += w;
                        }
                        return new object[] { 0, string.Format("C912D847-1825-458a-8CB5-E680FACA42AF:{0}", wl) };
                    }
                    else
                    {
                        return new object[] { 0, nextSendToIds };
                    }
                }
                else if (flInstance.FLFlag == 'Z')
                {
                    // End
                    return new object[] { 0, "B4DAF3A4-AAE8-4b51-A391-B52E46305E9F" };
                }
                else
                {
                    // Waiting
                    return new object[] { 0, "906F766E-3736-403b-BB1D-132ADEC3F2E9" };
                }
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程退回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Return2(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string nextFLActivityName = (string)flInstanceParms[1];

                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "FirstStepNotReturn");
                    throw new FLException(message);
                }

                flInstance.SetKeyValues(keyValues);
                flInstance.AddCacheFLInstanceParm(flInstanceParms);

                string userId = ((object[])clientInfo[0])[1].ToString();
                string roleId = ((object[])parameters[1])[5].ToString();
                bool isUrgent = Convert.ToBoolean(flInstanceParms[3]);
                flInstance.Return2(previousFLActivityName, nextFLActivityName, userId, roleId, isUrgent);

                HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                string flPath = previousFLActivityName + ";" + nextFLActivityName;
                Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);

                if (((IFLRootActivity)flInstance.RootFLActivity).NotifySendMail)
                {
                    Email.SendTo4(flInstance, flInstanceParms, keyValues, clientInfo);
                }

                flInstance.OnReturn(flInstance, new __FLInstanceReturnEventArgs());

                if (flInstance.FLFlag != 'Z')
                {
                    string nextSendToIds = string.Empty;
                    foreach (FLActivity a in flInstance.NextFLActivities)
                    {
                        if (!(a is IEventWaiting))
                        {
                            continue;
                        }
                        if (nextSendToIds != string.Empty)
                        {
                            nextSendToIds += ";";
                        }
                        nextSendToIds += a.Name + "|" + ((IEventWaiting)a).SendToId;
                        if (((IEventWaiting)a).SendToKind == SendToKind.Applicate
                            || ((IEventWaiting)a).SendToKind == SendToKind.RefUser || ((IEventWaiting)a).SendToKind == SendToKind.User)
                        {
                            nextSendToIds += ":UserId";
                        }
                    }

                    return new object[] { 0, nextSendToIds };
                }
                else if (flInstance.FLFlag == 'Z')
                {
                    // End
                    return new object[] { 0, "B4DAF3A4-AAE8-4b51-A391-B52E46305E9F" };
                }
                else
                {
                    // Waiting
                    return new object[] { 0, "906F766E-3736-403b-BB1D-132ADEC3F2E9" };
                }
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程退回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Return3(object[] parameters, object[] clientInfo)
        {
            Guid flInstanceId = (Guid)parameters[0];
            string flPath = parameters[1].ToString();

            DataSet dataSet = HostTable.GetHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, clientInfo);
            if (dataSet == null || dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
            {
                goto Label1;
            }

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string[] ss = flPath.Split(';');
                object currentFLActivity = ss.Length == 2 ? ss[0] : string.Empty;
                object nextFLActivity = ss.Length == 2 ? ss[1] : string.Empty;

                object important = row["FLOWIMPORTANT"];
                object urgent = 1;
                object remark = "Admin";
                object role = row["SENDTO_ID"];
                object provider = row["PROVIDER_NAME"];
                object emailurl = "0";
                object orgKid = string.Empty;
                object attachements = row["ATTACHMENTS"];

                // 1
                object[] flInstanceParms = new object[]{
                    currentFLActivity,nextFLActivity,important,urgent,remark,role,provider,emailurl,orgKid,attachements
                };

                // 2
                object key = row["FORM_KEYS"];
                object presentation = row["FORM_PRESENTATION"] == null || row["FORM_PRESENTATION"] == DBNull.Value ? string.Empty : row["FORM_PRESENTATION"].ToString().Replace("'", "''");
                object[] where = new object[] { key, presentation };


                object[] parameters2 = new object[] { flInstanceId, flInstanceParms, where };

                Return(parameters2, clientInfo);
            }

        Label1:
            return new object[] { 0, null };
        }

        /// <summary>
        /// 流程取回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Retake(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                //if (flInstance.IsSupportRetake)
                //{
                string retakeFLActivityName = flInstance.GetRetakeFLActivityName();
                FLActivity retakeFLActivity = flInstance.RootFLActivity.GetFLActivityByName(retakeFLActivityName);
                if (retakeFLActivity == null)
                {
                    return new object[] { 0, null };
                }

                //if (!flInstance.CanRetake)
                //{
                //    string m = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh");
                //    throw new FLException(2, m);
                //}

                string retakeRoleId = ((IEventWaitingExecute)retakeFLActivity).RoleId;
                string userId = ((object[])clientInfo[0])[1].ToString();
                List<string> roleIds = Global.GetRoleIdsByUserId(userId, clientInfo);
                if (flInstance.Version == "2.0")
                {
                    if (!roleIds.Contains(retakeRoleId))
                    {
                        string m = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh");
                        throw new FLException(2, m);
                    }
                }
                else
                {
                    if (!roleIds.Contains(retakeRoleId))
                    {
                        string m = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh");
                        throw new FLException(2, m);
                    }
                }

                //object temp0 = flInstanceParms[0];
                //if (temp0 == null || temp0.ToString() != retakeFLActivityName)
                //{
                //    string m = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh");
                //    throw new FLException(2, m);
                //}

                //string previousFLActivityName = flInstanceParms[0].ToString();
                //if (retakeFLActivityName != previousFLActivityName)
                //{
                //    string m = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotRetakePleaseRefresh");
                //    throw new FLException(2, m);
                //}

                flInstance.SetKeyValues(keyValues);
                if (flInstance.Version == "2.0")
                {
                    flInstanceParms = new object[] { "", "", "", "", "", retakeRoleId, "", "", "", "", "" };
                    flInstance.AddCacheFLInstanceParm(flInstanceParms);
                    flInstance.Retake();
                    var previousName = flInstance.CurrentFLActivity == null ? Guid.NewGuid().ToString() : flInstance.CurrentFLActivity.Name;
                    string flPath = Guid.NewGuid().ToString() + ";" + retakeFLActivityName;
                    Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);
                }
                else
                {
                    flInstanceParms = flInstance.RemoveCacheFLInstanceParm();

                    if (retakeFLActivityName == flInstance.RootFLActivity.ChildFLActivities[0].Name)
                    {
                        //flInstance.Reject();

                        //HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                        //Logic.DeleteToDo(flInstanceId, clientInfo);

                        //留一笔在ToDoList中。
                        var retakeActvities = flInstance.Retake();
                        flInstance.SetFLDirection(FLDirection.GoToBack);
                        for (int i = retakeActvities.Count - 1; i >= 0; i--)
                        {
                            if (retakeActvities[i] is FLProcedureActivity)
                            {
                                Logic.CallServerMethod(flInstance, flInstanceParms, keyValues, clientInfo, retakeActvities[i] as FLProcedureActivity);
                            }
                        }
                        flInstance.SetFLDirection(FLDirection.GoToNext);
                        string flPath = Guid.NewGuid().ToString() + ";" + retakeFLActivityName;
                        List<FLActivity> list = new List<FLActivity>();
                        list.Add(flInstance.RootFLActivity.ChildFLActivities[0]);
                        flInstance.RootFLActivity.ChildFLActivities[0].InitExecStatus();
                        flInstance.CurrentFLActivity = null;
                        flInstance.NextFLActivities = list;
                        Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);
                    }
                    else
                    {
                        var retakeActvities = flInstance.Retake();
                        flInstance.SetFLDirection(FLDirection.GoToBack);
                        for (int i = retakeActvities.Count - 1; i >= 0; i--)
                        {
                            if (retakeActvities[i] is FLProcedureActivity)
                            {
                                Logic.CallServerMethod(flInstance, flInstanceParms, keyValues, clientInfo, retakeActvities[i] as FLProcedureActivity);
                            }
                        }
                        flInstance.SetFLDirection(FLDirection.GoToNext);
                        string currentFLActivityName = flInstance.CurrentFLActivity.Name;
                        string nextFLActivityName = string.Empty;
                        foreach (FLActivity activity in flInstance.NextFLActivities)
                        {
                            if (activity is IEventWaiting)
                            {
                                nextFLActivityName = activity.Name;
                                break;
                            }
                        }

                        string flPath = currentFLActivityName + ";" + nextFLActivityName;
                        Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);
                    }
                }

                flInstance.OnRetake(flInstance, new __FLInstanceRetakeEventArgs());

                return new object[] { 0, null };
                //}
                //else
                //{
                //    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotSupportRetake");
                //    throw new FLException(2, message);
                //}
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程取消
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Reject(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);

                string previousFLActivityName = flInstanceParms[0].ToString();
                string currentFLActivityName = flInstanceParms[1].ToString();
                bool sendNotifyToAllRole = flInstanceParms[2].ToString() == "1" ? true : false;
                FLActivity currentFLActivity = flInstance.RootFLActivity.GetFLActivityByName(currentFLActivityName);

                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                flInstance.SetKeyValues(keyValues);

                FLNotifyActivity flNotifyActivity = flInstance.Reject(currentFLActivityName, sendNotifyToAllRole);

                HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                string rejectProcedure = ((IFLRootActivity)flInstance.RootFLActivity).RejectProcedure;
                if (!string.IsNullOrEmpty(rejectProcedure))
                {
                    var methods = rejectProcedure.Split('.');
                    if (methods.Length == 2)
                    {
                        FLProcedureActivity procedure = new FLProcedureActivity();
                        procedure.ModuleName = methods[0];
                        procedure.MethodName = methods[1];
                        var procedureParams = new List<object>();
                        procedureParams.AddRange(flInstanceParms);
                        procedureParams.Add("");
                        procedureParams.Add("");//role;

                        Logic.CallServerMethod(flInstance, procedureParams.ToArray(), keyValues, clientInfo, procedure);
                    }
                }


                //ToDoList.Delete(flInstanceId, clientInfo);

                Logic.DeleteToDo(flInstanceId, clientInfo);

                string flPath = previousFLActivityName + ";" + currentFLActivityName;
                var remark = flInstanceParms.Length >= 5 ? flInstanceParms[4] : string.Empty;
                var param = new object[] { previousFLActivityName, currentFLActivityName, 0, 0, remark, string.Empty, flInstanceParms[3].ToString(), string.Empty,  string.Empty, string.Empty };
                flInstance.NextFLActivities = new List<FLActivity>();
                Logic.InserToDoAndCallMethod(flInstance, param, flPath, keyValues, clientInfo);


                if (sendNotifyToAllRole)
                {
                    Logic.InsertToDo4RejectNotify(flInstance, (IEventWaiting)currentFLActivity, flNotifyActivity, flInstanceParms, keyValues, clientInfo);
                    if (((IFLRootActivity)flInstance.RootFLActivity).NotifySendMail)
                    {
                        flInstance.NextFLActivities = new List<FLActivity>();
                        flNotifyActivity.Parameters = "Reject";
                        flNotifyActivity.FLNavigatorMode = FLNavigatorMode.Notify;
                        flNotifyActivity.Name = currentFLActivityName;
                        flInstance.NextFLActivities.Add(flNotifyActivity);


                        Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);
                        flInstance.NextFLActivities.Clear();
                    }
                }

                flInstance.OnReject(flInstance, new __FLInstanceRejectEventArgs());

                return new object[] { 0, "512F4277-0D41-441c-BF16-D96B04580C2E" };
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程取消
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Reject2(object[] parameters, object[] clientInfo)
        {
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                string flPath = parameters[1].ToString();

                DataSet dataSet = HostTable.GetHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, clientInfo);
                if (dataSet == null || dataSet.Tables[0] == null || dataSet.Tables[0].Rows.Count == 0)
                {
                    return new object[] { 0, null };
                }

                foreach (DataRow row in dataSet.Tables[0].Rows)
                {
                    string[] ss = flPath.Split(';');
                    object currentFLActivity = ss.Length == 2 ? ss[0] : string.Empty;
                    object nextFLActivity = ss.Length == 2 ? ss[1] : string.Empty;

                    object important = row["FLOWIMPORTANT"];
                    object urgent = 1;
                    object remark = "Admin";
                    object role = row["SENDTO_ID"];
                    object provider = row["PROVIDER_NAME"];
                    object emailurl = "0";
                    object orgKid = string.Empty;
                    object attachements = row["ATTACHMENTS"];

                    // 1
                    object[] flInstanceParms = new object[]{
                        currentFLActivity,nextFLActivity, "1", "1", "0",provider};

                    // 2
                    object key = row["FORM_KEYS"];
                    object presentation = row["FORM_PRESENTATION"] == null || row["FORM_PRESENTATION"] == DBNull.Value ? string.Empty : row["FORM_PRESENTATION"].ToString().Replace("'", "''");
                    object[] where = new object[] { key, presentation };


                    object[] parameters2 = new object[] { flInstanceId, flInstanceParms, where };

                    Reject(parameters2, clientInfo);

                }



                //object[] keyValues = (object[])parameters[2];

                //if (flInstance == null)
                //{
                //    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                //    throw new FLException(2, message);
                //}

                //flInstance.SetKeyValues(keyValues);

                //flInstance.Reject();

                //HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                ////ToDoList.Delete(flInstanceId, clientInfo);

                //Logic.DeleteToDo(flInstanceId, clientInfo);

                //flInstance.OnReject(flInstance, new __FLInstanceRejectEventArgs());

                return new object[] { 0, "512F4277-0D41-441c-BF16-D96B04580C2E" };
            }
            catch (FLException e)
            {
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                return new object[] { 1, e.Message };
            }
        }

        /// <summary>
        /// 通知
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Notify(object[] parameters, object[] clientInfo)
        {
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string currentFLActivityName = (string)flInstanceParms[1];
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                FLActivity currentFLActivity = flInstance.Notify(currentFLActivityName);

                if (currentFLActivity is IEventWaiting)
                {
                    Logic.InsertToDo4Notify(flInstance, (IEventWaiting)currentFLActivity, flInstanceParms, keyValues, clientInfo);
                }
                else if (currentFLActivity is FLNotifyActivity)
                {
                    FLStandActivity activity = new FLStandActivity();
                    activity.FLNavigatorMode = (currentFLActivity as FLNotifyActivity).FLNavigatorMode;
                    activity.FormName = (currentFLActivity as FLNotifyActivity).FormName;
                    activity.NavigatorMode = (currentFLActivity as FLNotifyActivity).NavigatorMode;
                    activity.Parameters = (currentFLActivity as FLNotifyActivity).Parameters;
                    activity.RoleId = (currentFLActivity as FLNotifyActivity).RoleId;
                    activity.UserId = (currentFLActivity as FLNotifyActivity).UserId;
                    activity.SendEmail = (currentFLActivity as FLNotifyActivity).SendEmail;
                    activity.SendToKind = (currentFLActivity as FLNotifyActivity).SendToKind;
                    activity.SendToField = (currentFLActivity as FLNotifyActivity).SendToField;
                    activity.SendToRole = (currentFLActivity as FLNotifyActivity).SendToRole;
                    activity.SendToUser = (currentFLActivity as FLNotifyActivity).SendToUser;
                    activity.ExpTime = (currentFLActivity as FLNotifyActivity).ExpTime;
                    activity.UrgentTime = (currentFLActivity as FLNotifyActivity).UrgentTime;
                    activity.TimeUnit = (currentFLActivity as FLNotifyActivity).TimeUnit;
                    Logic.InsertToDo4Notify(flInstance, (IEventWaiting)activity, flInstanceParms, keyValues, clientInfo);
                }

                if (((IFLRootActivity)flInstance.RootFLActivity).NotifySendMail)
                {
                    Email.SendTo3(flInstance, flInstanceParms, keyValues, currentFLActivity, clientInfo);
                }

                flInstance.OnNotify(flInstance, new __FLInstanceNotifyEventArgs());

                return new object[] { 0, string.Empty };
            }
            catch (FLException e)
            {
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                return new object[] { 1, e.Message };
            }
        }

        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] PlusApprove(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string currentFLActivityName = (string)flInstanceParms[1];
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                flInstance.PlusApprove(previousFLActivityName, currentFLActivityName);

                Logic.InsertToDo4PlusApprove(flInstance, flInstanceParms, keyValues, clientInfo);

                Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);

                flInstance.OnPlusApprove(flInstance, new __FLInstancePlusApproveEventArgs());

                return new object[] { 0, string.Empty };
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 加签返回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] PlusReturn(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string currentFLActivityName = (string)flInstanceParms[1];//如果是flapprove里面的子activity 插入加签时flowpath有问题
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                flInstance.PlusReturn(previousFLActivityName, currentFLActivityName);

                Logic.InsertToDo4PlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);

                //Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);

                flInstance.OnPlusReturn(flInstance, new __FLInstancePlusReturnEventArgs());//先保存

                /***************判断任意加签是否结束,如果结束,自动跳到下一步**********/
                FLActivity activity = flInstance.RootFLActivity.GetFLActivityByName(currentFLActivityName);
                if (activity is IFLApproveBranchActivity)
                {
                    activity = flInstance.RootFLActivity.GetFLActivityByName(((IFLApproveBranchActivity)activity).ParentActivity);
                }
                if ((activity is IFLStandActivity && !((IFLStandActivity)activity).PlusApproveReturn)
                                    || (activity is IFLApproveActivity && !((IFLApproveActivity)activity).PlusApproveReturn))
                {
                    string plusRoles = Global.GetPlusRoles(flInstanceId.ToString(), currentFLActivityName, clientInfo);
                    if (string.IsNullOrEmpty(plusRoles))
                    {
                        //任意加签结束
                        string[] sendto = Global.GetSendTo(flInstanceId.ToString(), clientInfo);

                        string id = sendto[0];
                        string kind = sendto[1];
                        string user = string.Empty;
                        string role = string.Empty;

                        if (kind == "1")
                        {
                            //role
                            List<string> users = Global.GetUsersIdsByRoleId(id, clientInfo);
                            if (users.Count > 0)
                            {
                                user = users[0];
                                role = id;
                            }
                            else
                            {
                                throw new Exception("找不到用户");
                            }
                        }
                        else
                        {
                            List<string> roles = Global.GetRoleIdsByUserId(id, clientInfo);
                            if (roles.Count > 0)
                            {
                                user = id;
                                role = roles[0];
                            }
                            else
                            {
                                throw new Exception("找不到角色");
                            }
                        }
                        ComitWorkFlowTransaction(clientInfo);
                        needComit = false;
                        user = user.ToLower();

                        SrvGL.LogUser(user, user, "WFPlus", 1);//登录此用户

                        ((object[])clientInfo[0])[1] = user;//修改userid
                        ((object[])parameters[1])[5] = role;//修改roleid
                        ((object[])parameters[1])[0] = sendto[2];//修改前一步
                        ((object[])parameters[1])[1] = sendto[3];//修改后一步


                      
                        object[] ret = Approve(parameters, clientInfo);//审核
                        SrvGL.LogUser(user, user, "WFPlus", -1);//登出此用户
                        Email.SendToForPlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);
                        return ret;
                    }
                }
                /*************************判断任意加签是否结束************************/
                Email.SendToForPlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);




                return new object[] { 0, string.Empty };
            }
            catch (FLException e)
            {
                if (needComit)
                {
                    needComit = false;
                    RollBackWorkFlowTransaction(clientInfo);
                }
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                if (needComit)
                {
                    needComit = false;
                    RollBackWorkFlowTransaction(clientInfo);
                }
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 加签退回
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] PlusReturn2(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                object[] keyValues = (object[])parameters[2];

                string previousFLActivityName = (string)flInstanceParms[0];
                string currentFLActivityName = (string)flInstanceParms[1];//如果是flapprove里面的子activity 插入加签时flowpath有问题
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "NotFoundFLInstance"), flInstanceId);
                    throw new FLException(2, message);
                }

                flInstance.PlusReturn2(previousFLActivityName, currentFLActivityName);
                Logic.InsertToDo4PlusReturn2(flInstance, flInstanceParms, keyValues, clientInfo);
                //Email.SendTo(flInstance, flInstanceParms, keyValues, clientInfo);
                flInstance.OnPlusReturn(flInstance, new __FLInstancePlusReturnEventArgs());//先保存

                Email.SendToForPlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);
                return new object[] { 0, string.Empty };
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 流程暂停
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] Pause(object[] parameters, object[] clientInfo)
        {
            FLInstance flInstance = null;
            Guid flInstanceId = Guid.NewGuid();
            object[] flInstanceParms = (object[])parameters[1];
            object[] keyValues = (object[])parameters[2];
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                string xomlName = (string)flInstanceParms[0];
                string orgKind = (string)flInstanceParms[8];
                //XmlReader flDefinitionReader = XmlReader.Create(xomlName);

                Activity temp = FLInstance.GetActivityByXoml(xomlName, string.Empty);
                IFLRootActivity root = (IFLRootActivity)temp;
                string hostTableName = root.TableName;

             

                if (flInstanceParms[1] != null && (string)flInstanceParms[1] != string.Empty)
                {
                    //XmlReader flRulesReader = XmlReader.Create((string)flInstanceParms[1]);
                    // flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, flDefinitionReader, flRulesReader, clientInfo);
                    flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, xomlName, (string)flInstanceParms[1], clientInfo, string.Empty, null, orgKind);
                }
                else
                {
                    // flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, flDefinitionReader, clientInfo);
                    flInstance = Global.FLRuntime.CreateFLInstance(flInstanceId, xomlName, clientInfo, string.Empty, null, orgKind);
                }

                // 判断HostTable的记录是否在流程中
                DataSet hostDataSet = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo);//增加eeplias属性后要通过flinstance去取得hosttable
                if (hostDataSet == null || hostDataSet.Tables.Count == 0 || hostDataSet.Tables[0] == null || hostDataSet.Tables[0].Rows.Count == 0)
                {
                    String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "HostTableNotContainRecord"), ((IFLRootActivity)flInstance.RootFLActivity).TableName, keyValues[1].ToString());
                    throw new FLException(message);
                }

                DataRow hostRow = hostDataSet.Tables[0].Rows[0];
                if (hostRow["FlowFlag"].ToString().ToUpper() == "B")
                {
                    String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "InstanceManager", "RecordIsPaused");
                    throw new FLException(2, message);
                }
                flInstance._hostDataSet = hostDataSet;

                List<FLActivity> list = new List<FLActivity>();
                list.Add(flInstance.RootFLActivity.ChildFLActivities[0]);
                flInstance.NextFLActivities = list;

                flInstance.Pause();

                HostTable.UpdateFLFlag(flInstance, keyValues, clientInfo);

                //flInstance.FLFlag = 'N';
                string flPath = Guid.NewGuid().ToString() + ";" + flInstance.RootFLActivity.ChildFLActivities[0].Name;
                Logic.InserToDoAndCallMethod(flInstance, flInstanceParms, flPath, keyValues, clientInfo);

                flInstance.OnPause(flInstance, new __FLInstancePauseEventArgs());

                return new object[] { 0, string.Empty, flInstanceId };
            }
            catch (FLException e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                if (e.Type == 2)
                    return new object[] { 2, e.Message };
                else
                    return new object[] { 1, e.Message };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 修改流程定义
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] ModifyFLDefinition(object[] parameters, object[] clientInfo)
        {
            try
            {
                Guid flInstanceId = new Guid(parameters[0].ToString());
                string xomlFileName = ((object[])parameters[1])[0].ToString();

                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);

                flInstance.ModifyFLDefinition(xomlFileName, string.Empty);
                flInstance.InitFLDefinitionId(flInstance, clientInfo);
                Global.ModifyFLInstanceDefinition(flInstance, clientInfo);

                return new object[] { 0, null };
            }
            catch (Exception e)
            {
                return new object[] { 1, e.Message };
            }
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] DeleteNotify(object[] parameters, object[] clientInfo)
        {
            Guid flInstanceId = new Guid(parameters[0].ToString());
            string flPath = parameters[1].ToString();

            var sendToId = string.Empty;
            if (parameters.Length > 2)
            {
                sendToId = parameters[2].ToString();
            }

            try
            {
                Logic.DeleteToDoList4Notify(flInstanceId, flPath, sendToId, clientInfo);

                return new object[] { 0, null };
            }
            catch (Exception e)
            {
                return new object[] { 1, e.Message };
            }
        }

        public object[] GetUserActivities(object[] parameters, object[] clientInfo)
        {

            DataTable userTable = new DataTable("User");
            userTable.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("UserID", typeof(string)),
                    new DataColumn("UserName", typeof(string)),
                    new DataColumn("FlowId", typeof(string)),
                    new DataColumn("FlowDescription", typeof(string)),
                    new DataColumn("ActivityName", typeof(string)),
                }
            );

            DataTable roleTable = new DataTable("Role");
            //RoleId,RoleName,FlowId,FlowDescription,ActivityName
            roleTable.Columns.AddRange(new DataColumn[] 
                {
                    new DataColumn("RoleID", typeof(string)),
                    new DataColumn("RoleName", typeof(string)),
                    new DataColumn("FlowId", typeof(string)),
                    new DataColumn("FlowDescription", typeof(string)),
                    new DataColumn("ActivityName", typeof(string)),
                }
            );

            string flDefinitionFilePath = string.Format("{0}\\WorkFlow\\", EEPRegistry.Server);
            if (Directory.Exists(flDefinitionFilePath))
            {
                string userID = ((object[])clientInfo[0])[1].ToString();
                string userName = Global.GetUserName(userID, clientInfo);
                List<string> roleIDs = new List<string>(((object[])clientInfo[0])[12].ToString().Split(';'));
                DirectoryInfo dir = new DirectoryInfo(flDefinitionFilePath);
                FileInfo[] files = dir.GetFiles("*.xoml", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {
                    try
                    {
                        Activity rootActivity = FLInstance.GetWorkflowDefinition(file.FullName, null);
                        List<IEventWaiting> listActivities = GetActivities(rootActivity);
                        foreach (IEventWaiting activity in listActivities)
                        {
                            if (activity.SendToKind == SendToKind.User)
                            {
                                //add to user table  
                                if (!string.IsNullOrEmpty(activity.SendToUser))
                                {
                                    string uID = activity.SendToUser.Split(';')[0].Trim();
                                    if (uID.Equals(userID))
                                    {
                                        userTable.Rows.Add(new object[] { userID, userName, file.Name, rootActivity.Description, activity.Name });
                                    }
                                }
                            }
                            else if (activity.SendToKind == SendToKind.Role)
                            {
                                //add to role table
                                if (!string.IsNullOrEmpty(activity.SendToRole))
                                {
                                    string rID = activity.SendToRole.Split(';')[0].Trim();

                                    if (roleIDs.Contains(rID))
                                    {
                                        string roleName = Global.GetGroupName(rID, clientInfo).Split('/')[0];
                                        roleTable.Rows.Add(new object[] { rID, roleName, file.Name, rootActivity.Description, activity.Name });
                                    }
                                }
                            }
                            else if (activity is IFLDetailsActivity)
                            {
                                IFLDetailsActivity detailsActivity = activity as IFLDetailsActivity;
                                if (!string.IsNullOrEmpty(detailsActivity.ExtApproveID))
                                {
                                    List<string> roles = Global.GetExtApproveRoles(detailsActivity.ExtApproveID, clientInfo);
                                    foreach (string rID in roles)
                                    {
                                        if (roleIDs.Contains(rID))
                                        {
                                            string roleName = Global.GetGroupName(rID, clientInfo).Split('/')[0];
                                            roleTable.Rows.Add(new object[] { rID, roleName, file.Name, rootActivity.Description, activity.Name });
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return new object[] { 0, userTable, roleTable };
        }

        private List<IEventWaiting> GetActivities(Activity currentActivity)
        {
            List<IEventWaiting> listActivities = new List<IEventWaiting>();
            if (currentActivity is IEventWaiting)
            {
                listActivities.Add(currentActivity as IEventWaiting);
            }
            else if (currentActivity is CompositeActivity)
            {
                foreach (Activity activity in (currentActivity as CompositeActivity).Activities)
                {
                    listActivities.AddRange(GetActivities(activity));
                }
            }
            return listActivities;
        }

        public object[] GetSubFlowFiles(object[] parameters, object[] clientInfo)
        {
            List<string> list = new List<string>();
            DirectoryInfo dir = new DirectoryInfo(string.Format(@"{0}\WorkFlow\FL\SubFlows", EEPRegistry.Server));
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles("*.xoml", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {
                    list.Add(file.Name);
                }
            }
            return new object[] { 0, list.ToArray() };
        }

        /// <summary>
        /// 取得流程描述集合
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] GetFLDescriptions(object[] parameters, object[] clientInfo)
        {
            string flDefinitionFilePath = parameters[0].ToString();
            ArrayList ss = new ArrayList();
            if (Directory.Exists(flDefinitionFilePath))
            {
                DirectoryInfo dir = new DirectoryInfo(flDefinitionFilePath);
                FileInfo[] files = dir.GetFiles("*.xoml", SearchOption.AllDirectories);
                foreach (FileInfo file in files)
                {
                    //if (file.Extension.ToLower() == ".xoml")
                    //{
                    //Assembly assembly = Assembly.LoadFrom(EEPRegistry.Server + @"\" + dllName);
                    //Type type = assembly.GetType(className);
                    //MethodInfo method = type.GetMethod(methodName);

                    //object obj = null;
                    //try
                    //{
                    //    obj = method.Invoke(null, new object[] { file.FullName, null });
                    //}
                    //catch
                    //{
                    //    continue;
                    //}

                    //Activity wfActivity = (Activity)obj;
                    Activity wfActivity = null;
                    try
                    {
                        wfActivity = FLInstance.GetWorkflowDefinition(file.FullName, null);
                    }
                    catch
                    {
                        continue;
                    }
                    string s = wfActivity.Description;

                    if (s != string.Empty)
                    {
                        ss.Add(s);
                    }
                    //}
                }
            }

            return new object[] { 0, ss };
        }

        /// <summary>
        /// 取得流程描述
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] GetFLDescription(object[] parameters, object[] clientInfo)
        {
            string flDefinitionFile = parameters[0].ToString();

            if (File.Exists(flDefinitionFile))
            {
                try
                {
                    //Assembly assembly = Assembly.LoadFrom(dllName);
                    //Type type = assembly.GetType(className);
                    //MethodInfo method = type.GetMethod(methodName);

                    //object obj = method.Invoke(null, new object[] { flDefinitionFile, null });
                    //Activity wfActivity = (Activity)obj;
                    Activity wfActivity = FLInstance.GetWorkflowDefinition(flDefinitionFile, null);

                    return new object[] { 0, wfActivity.Description };
                }
                catch
                {
                    return new object[] { 0, string.Empty };
                }
            }
            return new object[] { 0, string.Empty };
        }

        public object[] Preview(object[] parameters, object[] clientInfo)
        {
            Guid flInstanceId = (Guid)parameters[0];
            object[] flInstanceParms = (object[])parameters[1];
            string xomlName = (string)flInstanceParms[0];
            string ruleName = (string)flInstanceParms[1];
            string currentActivity = (string)flInstanceParms[2];
            DataSet host = (DataSet)flInstanceParms[3];
            string roleID = (string)flInstanceParms[4];
            object[] keyValues = (object[])parameters[2];

            bool isImage = true;
            if (parameters.Length >= 4)
            {
                isImage = (bool)parameters[3];
            }

            FLPreview preview = new FLPreview(xomlName, ruleName, flInstanceId, clientInfo, currentActivity, host, roleID, string.Empty, keyValues);
            try
            {
                if (isImage)
                {
                    System.Drawing.Image image = preview.CreatePreviewImage();
                    if (image != null)
                    {
                        MemoryStream stream = new MemoryStream();
                        image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                        return new object[] { 0, stream.ToArray() };
                    }
                    return new object[] { 0, null };
                }
                else
                {
                    return new object[] { 0, preview.CreatePreviewTable() };
                }
            }
            catch (Exception e)
            {
                return new object[] { 1, e.Message };
            }
        }

        public object[] ChangeSendTo(object[] parameters, object[] clientInfo)
        {
            BeginWorkFlowTransaction(clientInfo);
            bool needComit = true;
            try
            {
                Guid flInstanceId = (Guid)parameters[0];
                object[] flInstanceParms = (object[])parameters[1];
                string flPath = (string)flInstanceParms[0];
                string sendto = (string)flInstanceParms[1];
                DataSet dataSet = HostTable.GetHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, clientInfo);


                object o = Global.FLRuntime.GetService(typeof(Hosting.FLPersistenceService));
               
                if (o != null)
                {
                    var flPersistenceService = (Hosting.FLPersistenceService)o;
                    FLInstance instance = null;
                    var i = flPersistenceService.DepersistenceFL(flInstanceId, clientInfo);
                    if (i != null)
                    {
                        instance = (FLInstance)i;
                    }

                    var currentFLActivityName = flPath.Split(';')[1];
                    var currentFLActivity = instance.RootFLActivity.GetFLActivityByName(currentFLActivityName);
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                    {
                        if (row["SENDTO_KIND"].ToString() == "1")
                        {
                            if (currentFLActivity is IEventWaiting)
                            {
                                if ((currentFLActivity as IEventWaiting).SendToKind == SendToKind.Role)
                                {
                                    (currentFLActivity as IEventWaiting).SendToRole = sendto;
                                }


                                (currentFLActivity as IEventWaiting).SendToId = sendto;
                                HostTable.UpdateHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, sendto, clientInfo);
                            }
                        }
                        else if (row["SENDTO_KIND"].ToString() == "2")
                        {
                            if (currentFLActivity is IEventWaiting)
                            {
                                if ((currentFLActivity as IEventWaiting).SendToKind == SendToKind.User)
                                {
                                    (currentFLActivity as IEventWaiting).SendToUser = sendto;
                                }

                                (currentFLActivity as IEventWaiting).SendToId = sendto;
                                HostTable.UpdateHostDataSetByIdAndFLPath("SYS_TODOLIST", flInstanceId.ToString(), flPath, sendto, clientInfo);
                            }
                        }
                    }
                    flPersistenceService.PersistenceFL(instance, clientInfo);
                }
                return new object[] { 0 };
            }
            catch (Exception e)
            {
                needComit = false;
                RollBackWorkFlowTransaction(clientInfo);
                return new object[] { 1, e.Message };
            }
            finally
            {
                if (needComit)
                {
                    ComitWorkFlowTransaction(clientInfo);
                }
            }
        }

        /// <summary>
        /// 取得流程描述的XML字符串
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] GetFLDefinitionXmlString(object[] parameters, object[] clientInfo)
        {
            string flDefinitionFile = EEPRegistry.Server + "\\WorkFlow\\" + parameters[0].ToString();

            if (File.Exists(flDefinitionFile))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(flDefinitionFile);

                    return new object[] { 0, doc.InnerXml };
                }
                catch
                {
                    return new object[] { 0, string.Empty };
                }
            }
            return new object[] { 0, string.Empty };
        }

        /// <summary>
        /// 取得宿主Server的路径
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] GetFLServerPath(object[] parameters, object[] clientInfo)
        {
            string ffn = EEPRegistry.Server + "\\WorkFlow\\" + parameters[0].ToString().ToLower();
            if (ffn.EndsWith(".xoml"))
            {
                ffn = ffn.Substring(0, ffn.IndexOf(".xoml"));
            }
            return new object[] { 0, ffn };
        }

        /// <summary>
        /// 取得流程路径集合
        /// </summary>
        /// <param name="parameters">Client传入参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public object[] GetFLPathList(object[] parameters, object[] clientInfo)
        {
            Guid flInstanceId = (Guid)parameters[0];
            

            try
            {
                FLInstance flInstance = Global.FLRuntime.GetFLInstance(flInstanceId, clientInfo);
                if (flInstance == null)
                {
                    return new object[] { 0, null };
                }
                else
                {
                    if (flInstance.Version == "2.0")
                    {
                      
                        string nextFLActivityName = string.Empty;
                        if (parameters.Length >= 2)
                        {
                            object[] flInstanceParms = (object[])parameters[1];
                            if (flInstanceParms.Length >= 2)
                            {

                                nextFLActivityName = (string)flInstanceParms[1];
                            }
                        }
                        if (string.IsNullOrEmpty(nextFLActivityName))
                        {
                            var lastActivity = flInstance.RootFLActivity.GetFLActivityByName(flInstance.LastActivity);
                            foreach (var nextActivity in lastActivity.NextActivities)
                            {
                                if (nextActivity is IEventWaiting)
                                {
                                    nextFLActivityName = nextActivity.Name;
                                }
                            }
                        }

                        FLActivity activity = flInstance.RootFLActivity.GetFLActivityByName(nextFLActivityName);
                        var path = new List<string>();
                        var previousActivity = activity.PreviousActivity;
                        while (true)
                        {
                            if (previousActivity == null)
                            {
                                return new object[] { 0, path.ToArray() };
                            }
                            var availableActivity = flInstance.GetAvailableActivity(previousActivity, activity);
                            if (availableActivity == previousActivity && availableActivity.AllowSendBack)
                            {
                                path.Insert(0, previousActivity.Name);
                            }
                            previousActivity = previousActivity.PreviousActivity;
                        }
                    }
                    else
                    {
                        List<string> path = new List<string>();
                        List<object> fullPath = flInstance.P;
                        int i = fullPath.Count;
                        if (i >= 2)
                        {
                            for (int j = i - 2; j >= 0; j--)
                            {
                                object obj = fullPath[j];
                                if (obj is string)
                                {
                                    FLActivity activity = flInstance.RootFLActivity.GetFLActivityByName((string)obj);
                                    if (activity is IFLProcedureActivity)
                                    {
                                        continue;
                                    }
                                    if (activity is IFLNotifyActivity)
                                    {
                                        continue;
                                    }
                                    else if (!activity.AllowSendBack)
                                    {
                                        continue;
                                    }
                                    path.Add(obj.ToString());
                                }
                                else
                                {
                                    List<object> list01 = (List<object>)obj;
                                    foreach (object o1 in list01)
                                    {
                                        List<object> list1 = (List<object>)o1;
                                        if (list1.Count > 0)
                                        {
                                            string name = list1[list1.Count - 1].ToString();
                                            FLActivity activity = flInstance.RootFLActivity.GetFLActivityByName(name);
                                            if (!string.IsNullOrEmpty(activity.Location))
                                            {
                                                path.Add(activity.Name);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }


                            return new object[] { 0, path.ToArray() };
                        }
                        else
                        {
                            return new object[] { 0, null };
                        }
                    }
                }
            }
            catch
            {
                return new object[] { 0, null };
            }
        }
    }
}
