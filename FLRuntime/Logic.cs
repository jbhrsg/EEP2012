using System;
using System.Collections.Generic;
using System.Text;
using FLCore;
using FLTools.ComponentModel;
using System.Data;
using System.Linq;

namespace FLRuntime
{
    public class Logic
    {
#if !CreateTime
        private static string INSERT_TODOLIST = @"INSERT INTO SYS_TODOLIST
                   (LISTID,FLOW_ID,FLOW_DESC,S_USER_ID,S_STEP_ID,S_STEP_DESC,D_STEP_ID,D_STEP_DESC,EXP_TIME,URGENT_TIME,TIME_UNIT
                    ,USERNAME,FORM_NAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,SENDTO_KIND,SENDTO_ID,FLOWIMPORTANT,FLOWURGENT,STATUS
                    ,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,FORM_PRESENT_CT,REMARK,PROVIDER_NAME,VERSION,EMAIL_ADD,EMAIL_STATUS,VDSNAME
                    ,SENDBACKSTEP,LEVEL_NO,WEBFORM_NAME,APPLICANT,FLOWPATH,UPDATE_DATE,UPDATE_TIME,PLUSAPPROVE,PLUSROLES,MULTISTEPRETURN
                    ,SENDTO_NAME,ATTACHMENTS)
                VALUES
                   ('{0}','{1}',{43}'{2}','{3}','{4}',{43}'{5}','{6}',{43}'{7}',{8},{9},'{10}',{43}'{11}','{12}',
                    '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{43}'{24}'
                    ,{43}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',{43}'{35}','{36}'
                    ,'{37}','{38}','{39}','{40}',{43}'{41}',{43}'{42}')";
#else
        private static string INSERT_TODOLIST = @"INSERT INTO SYS_TODOLIST
                   (LISTID,FLOW_ID,FLOW_DESC,S_USER_ID,S_STEP_ID,S_STEP_DESC,D_STEP_ID,D_STEP_DESC,EXP_TIME,URGENT_TIME,TIME_UNIT
                    ,USERNAME,FORM_NAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,SENDTO_KIND,SENDTO_ID,FLOWIMPORTANT,FLOWURGENT,STATUS
                    ,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,FORM_PRESENT_CT,REMARK,PROVIDER_NAME,VERSION,EMAIL_ADD,EMAIL_STATUS,VDSNAME
                    ,SENDBACKSTEP,LEVEL_NO,WEBFORM_NAME,APPLICANT,FLOWPATH,UPDATE_DATE,UPDATE_TIME,PLUSAPPROVE,PLUSROLES,MULTISTEPRETURN
                    ,SENDTO_NAME,ATTACHMENTS, CREATE_TIME)
                VALUES
                   ('{0}','{1}',{43}'{2}','{3}','{4}',{43}'{5}','{6}',{43}'{7}',{8},{9},'{10}',{43}'{11}','{12}',
                    '{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}',{43}'{24}'
                    ,{43}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}',{43}'{35}','{36}'
                    ,'{37}','{38}','{39}','{40}',{43}'{41}',{43}'{42}', '{44}')";
#endif




#if !CreateTime && !DUSERNAME
        private static string INSERT_TODOHIS = @"INSERT INTO SYS_TODOHIS
            (LISTID,FLOW_ID,FLOW_DESC,ROLE_ID,S_ROLE_ID,S_STEP_ID,S_STEP_DESC,S_USER_ID,USER_ID,USERNAME,FORM_NAME,WEBFORM_NAME
            ,S_USERNAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,STATUS,PROC_TIME,EXP_TIME,TIME_UNIT,FLOWIMPORTANT
            ,FLOWURGENT,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,REMARK,VERSION,VDSNAME,SENDBACKSTEP,LEVEL_NO,UPDATE_DATE
            ,UPDATE_TIME,D_STEP_ID,FORM_PRESENT_CT,ATTACHMENTS)
         VALUES
            ('{0}','{1}',{35}'{2}','{3}','{4}','{5}',{35}'{6}','{7}','{8}',{35}'{9}','{10}','{11}',{35}'{12}'
            ,'{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}','{21}'
            ,'{22}','{23}','{24}',{35}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}'
            ,{35}'{33}',{35}'{34}')";

        private static string INSERT_TODOHIS2 = @"INSERT INTO SYS_TODOHIS
            (LISTID,FLOW_ID,FLOW_DESC,ROLE_ID,S_ROLE_ID,S_STEP_ID,S_STEP_DESC,S_USER_ID,USER_ID,USERNAME,FORM_NAME,WEBFORM_NAME
            ,S_USERNAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,STATUS,PROC_TIME,EXP_TIME,TIME_UNIT,FLOWIMPORTANT
            ,FLOWURGENT,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,REMARK,VERSION,VDSNAME,SENDBACKSTEP,LEVEL_NO,UPDATE_DATE
            ,UPDATE_TIME,D_STEP_ID,FORM_PRESENT_CT,ATTACHMENTS, PLUSROLES)
         VALUES
            ('{0}','{1}',{35}'{2}','{3}','{4}','{5}',{35}'{6}','{7}','{8}',{35}'{9}','{10}','{11}',{35}'{12}'
            ,'{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}','{21}'
            ,'{22}','{23}','{24}',{35}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}'
            ,{35}'{33}',{35}'{34}', '{36}')";
#endif

#if CreateTime
        private static string INSERT_TODOHIS = @"INSERT INTO SYS_TODOHIS
            (LISTID,FLOW_ID,FLOW_DESC,ROLE_ID,S_ROLE_ID,S_STEP_ID,S_STEP_DESC,S_USER_ID,USER_ID,USERNAME,FORM_NAME,WEBFORM_NAME
            ,S_USERNAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,STATUS,PROC_TIME,EXP_TIME,TIME_UNIT,FLOWIMPORTANT
            ,FLOWURGENT,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,REMARK,VERSION,VDSNAME,SENDBACKSTEP,LEVEL_NO,UPDATE_DATE
            ,UPDATE_TIME,D_STEP_ID,FORM_PRESENT_CT,ATTACHMENTS, CREATE_TIME)
         VALUES
            ('{0}','{1}',{35}'{2}','{3}','{4}','{5}',{35}'{6}','{7}','{8}',{35}'{9}','{10}','{11}',{35}'{12}'
            ,'{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}','{21}'
            ,'{22}','{23}','{24}',{35}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}'
            ,{35}'{33}',{35}'{34}', '{36}')";
#endif


#if DUSERNAME
        private static string INSERT_TODOHIS = @"INSERT INTO SYS_TODOHIS
            (LISTID,FLOW_ID,FLOW_DESC,ROLE_ID,S_ROLE_ID,S_STEP_ID,S_STEP_DESC,S_USER_ID,USER_ID,USERNAME,FORM_NAME,WEBFORM_NAME
            ,S_USERNAME,NAVIGATOR_MODE,FLNAVIGATOR_MODE,PARAMETERS,STATUS,PROC_TIME,EXP_TIME,TIME_UNIT,FLOWIMPORTANT
            ,FLOWURGENT,FORM_TABLE,FORM_KEYS,FORM_PRESENTATION,REMARK,VERSION,VDSNAME,SENDBACKSTEP,LEVEL_NO,UPDATE_DATE
            ,UPDATE_TIME,D_STEP_ID,FORM_PRESENT_CT,ATTACHMENTS, D_USERNAME)
         VALUES
            ('{0}','{1}',{35}'{2}','{3}','{4}','{5}',{35}'{6}','{7}','{8}',{35}'{9}','{10}','{11}',{35}'{12}'
            ,'{13}','{14}','{15}','{16}',{17},{18},'{19}','{20}','{21}'
            ,'{22}','{23}','{24}',{35}'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}'
            ,{35}'{33}',{35}'{34}','{36}')";
#endif

        private static string DELETE_TODOLIST = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND STATUS <> 'F'";
        private static string DELETE_TODOLIST_3 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND FLOWPATH='{1}' AND STATUS <> 'F'";
        private static string DELETE_TODOLIST_6 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND ({1}) AND STATUS <> 'F'";
        private static string DELETE_TODOLIST_6_1 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND FLOWPATH LIKE '%;{1}' AND STATUS <> 'F'";

        private static string DELETE_TODOLIST_2 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND STATUS = 'F' AND FLOWPATH='{1}'";

        private static string DELETE_TODOLIST_4 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND (STATUS = 'A' OR STATUS = 'AA') AND SENDTO_ID='{1}' AND SENDTO_KIND='{2}'";
        private static string DELETE_TODOLIST_5 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND (STATUS = 'A' OR STATUS = 'AA')";
        private static string DELETE_TODOLIST_2_1 = "DELETE FROM SYS_TODOLIST WHERE LISTID='{0}' AND STATUS = 'F' AND FLOWPATH='{1}' AND SENDTO_ID='{2}'";

        private static string SELECT_TODOLIST_4 = "SELECT * FROM SYS_TODOLIST WHERE LISTID='{0}' AND (STATUS = 'A' OR STATUS = 'AA') AND SENDTO_ID='{1}' AND SENDTO_KIND='{2}'";

        private static string UPDATE_TODOLIST = "UPDATE SYS_TODOLIST SET PLUSROLES='{0}' WHERE LISTID='{1}' AND D_STEP_ID = '{2}' AND STATUS NOT IN ('F','A', 'AA')";
        private static string UPDATE_TODOLIST2 = "UPDATE SYS_TODOLIST SET ATTACHMENTS={2}'{0}' WHERE LISTID='{1}'";

        private static string GET_TODOLIST = @"SELECT * FROM SYS_TODOLIST WHERE LISTID='{0}' AND FLOWPATH='{1}'";
        private static string COL_PLUS_ROLES = "PLUSROLES";

        /// <summary>
        /// 取得加签角色
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flowPath">流程路径</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        public static string GetPlusRoles(FLInstance flInstance, string flowPath, object[] clientInfo)
        {
            string plusRoles = string.Empty;

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = string.Format(GET_TODOLIST, flInstance.FLInstanceId, flowPath);
            clientInfo[3] = sql;
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
            // object[] objs = remoteModule.GetSqlCommand(clientInfo, "GLModule", "cmdWorkflow", "", false);

            if (objs[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objs[1];
                if (dataSet != null && dataSet.Tables.Count != 0 && dataSet.Tables[0].Rows.Count != 0)
                {
                    object obj = dataSet.Tables[0].Rows[0][COL_PLUS_ROLES];
                    if (obj != null && obj != DBNull.Value)
                    {
                        plusRoles = obj.ToString().Trim();
                    }
                }
            }

            return plusRoles;
        }

        private static string GetNvarcharMark(object[] clientInfo)
        {
            string dbType = GetDBType(clientInfo);
            return dbType == "1" ? "N" : string.Empty;
        }

        public static object MarkValue(object value)
        {
            if (value is string)
            {
                var values = ((string)value).Split('\'');
                var list = new List<string>();
                for (int i = 0; i < values.Length; i++)
                {
                    if (i == 0 || i == values.Length - 1)
                    {
                        list.Add(values[i]);
                    }
                    else if (!string.IsNullOrEmpty(values[i]))
                    {
                        list.Add(values[i]);
                    }
                }
                return string.Join("''", list); 
            }
            return value;
        }

        private static string GetFormPresentCT(FLInstance flInstance, string keys, string values, string presentFields, object[] clientInfo)
        {
            return Global.GetFormPresentCT(flInstance, keys, values, presentFields, clientInfo, true);
        }

        private static string GetInsertToDoHisSQL(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            return GetInsertToDoHisSQL(flInstance, flInstanceParms, keyValues, clientInfo, false);
        }

        /// <summary>
        /// 取得要插入ToDoHis的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoHisSQL(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo, bool plusreturn)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            // 由于S_STEP_ID记录了上一个，是错误的，所以在FLInstance.GetNextFLActivities去取上一个FLActivity是多余的。
            // 有时间就修正上面的多余。
            IEventWaiting previousFLActivity = flInstance.PreviousFLActivity == null ? null : (IEventWaiting)flInstance.PreviousFLActivity;
            IEventWaiting currentFLActivity = flInstance.CurrentFLActivity == null ? null : (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            // todolist
            List<IEventWaiting> nextFLActivities = new List<IEventWaiting>();
            if (s != null && s.Count != 0)
            {
                foreach (FLActivity a in s)
                {
                    if (a is IEventWaiting)
                    {
                        nextFLActivities.Add((IEventWaiting)a);
                    }
                }
            }

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;

            string userName = Global.GetUserName(((object[])clientInfo[0])[1].ToString(), clientInfo, flInstanceParms[5].ToString());
            string sUserName = Global.GetUserName(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId, clientInfo);

            string sql = string.Empty;
            IEventWaiting nextFLActivity = nextFLActivities != null && nextFLActivities.Count != 0 ? nextFLActivities[0] : null;
            if (nextFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                nextFLActivity = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            string status = "N";
            if (!plusreturn)
            {
                if (flInstance.FLFlag == 'Z')
                {
                    status = "Z";
                }
                else if (nextFLActivity is IFLNotifyActivity)
                {
                    status = "F";
                }
                else if (flInstance.IsRetake)
                {
                    status = "NF";
                }
                else if (flInstance.IsPause)
                {
                    status = "NP";
                }
                else if (flInstance.IsReturn)
                {
                    status = "NR";
                }
            }
            else
            {
                if (flInstance.IsReturn)
                {
                    status = "NR";
                }
            }

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;

            DateTime now = DateTime.Now;
            string s_step_id = currentFLActivity == null ? null : currentFLActivity.Name;
            if (flInstance.IsRetake && nextFLActivity != null)
            {
                s_step_id = nextFLActivity.Name;
            }

            List<object> list = new List<object>();
            list.Add(flInstanceId.ToString());                                                                              //LISTID
            list.Add(flDefinitionId);                                                                                       //FLOW_ID
            list.Add(flowDesc);                                                                                             //FLOW_DESC
            list.Add(flInstanceParms[5].ToString());                                                                        //ROLE_ID
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).RoleId);                  //S_ROLE_ID
            list.Add(s_step_id);                                            //S_STEP_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                                     //S_STEP_DESC
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId);                  //S_USER_ID
            list.Add(((object[])clientInfo[0])[1].ToString());                                                              //USER_ID
            list.Add(userName);                                                                                             //USERNAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.FormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : currentFLActivity.FormName);                      //FORM_NAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.WebFormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : currentFLActivity.WebFormName);                //WEBFORM_NAME
            list.Add(sUserName);                                                                                            //S_USERNAME
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.NavigatorMode);                                       //NAVIGATOR_MODE
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.FLNavigatorMode);                                     //FLNAVIGATOR_MODE
            list.Add(nextFLActivity == null ? null : nextFLActivity.Parameters);                                            //PARAMETERS
            list.Add(status);                                                                                               //STATUS
            list.Add(previousFLActivity == null || previousFLActivity.ExecutedTime == new DateTime(1, 1, 1)
                 || (now - previousFLActivity.ExecutedTime).TotalHours < 0.001
                 ? "0" : (now - previousFLActivity.ExecutedTime).TotalHours.ToString("f2"));                                                  //PROC_TIME
            list.Add(currentFLActivity == null
                ? 0 : Math.Round((currentFLActivity.IsUrgent ? currentFLActivity.UrgentTime : currentFLActivity.ExpTime), 2));             //EXP_TIME
            list.Add(previousFLActivity == null ? TimeUnit.Day.ToString() : previousFLActivity.TimeUnit.ToString());        //TIME_UNIT
            list.Add(flInstanceParms[2]);                                                                                   //FLOWIMPORTANT
            list.Add(flInstanceParms[3]);                                                                                   //FLOWURGENT
            list.Add(tableName);                                                                                            //FORM_TABLE
            list.Add(keyValues[0]);                                                                                         //FORM_KEYS
            list.Add(keyValues[1]);                                                                                         //FORM_PRESENTATION
            list.Add(flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''"));         //REMARK
            list.Add(null);                                                                                                 //VERSION
            list.Add(flInstance.Solution);                                                                                  // VDSNAME;
            list.Add(null);                                                                                                 //SENDBACKSTEP
            list.Add(delayAutoApprove);                                                                                     //LEVEL_NO
            list.Add(now.ToString("yyyy-MM-dd"));                                                                           //UPDATE_DATE
            list.Add(now.ToString("HH:mm:ss"));                                                                             //UPDATE_TIME
            list.Add(nextFLActivity == null ? null : nextFLActivity.Name);                                                  //D_STEP_ID
            list.Add(GetFormPresentCT(flInstance, keyValues[0].ToString(),
                keyValues[1].ToString(), presentFields, clientInfo));                                                       //FORM_PRESENT_CT
            list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                        //ATTACHMENTFILES
            list.Add(GetNvarcharMark(clientInfo));

#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

#if DUSERNAME
            list.Add(GetDUSERNAME(flInstance, flInstanceParms, keyValues, clientInfo));
#endif

            sql = sql + string.Format(INSERT_TODOHIS, list.Select(c=> MarkValue(c)).ToArray());
            return sql;
        }

        /// <summary>
        /// 取得取消时要插入ToDoHis的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoHisSQL4Reject(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            // 由于S_STEP_ID记录了上一个，是错误的，所以在FLInstance.GetNextFLActivities去取上一个FLActivity是多余的。
            // 有时间就修正上面的多余。
            IEventWaiting previousFLActivity = flInstance.PreviousFLActivity == null ? null : (IEventWaiting)flInstance.PreviousFLActivity;
            IEventWaiting currentFLActivity = flInstance.CurrentFLActivity == null ? null : (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            // todolist
            List<IFLNotifyActivity> nextFLActivities = new List<IFLNotifyActivity>();
            if (s != null && s.Count != 0)
            {
                foreach (FLActivity a in s)
                {
                    if (a is IFLNotifyActivity)
                    {
                        nextFLActivities.Add((IFLNotifyActivity)a);
                    }
                }
            }

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;

            string userName = Global.GetUserName(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sUserName = Global.GetUserName(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId, clientInfo);

            string sql = string.Empty;
            IFLNotifyActivity nextFLActivity = nextFLActivities != null && nextFLActivities.Count != 0 ? nextFLActivities[0] : null;

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            string status = "X";
            if (flInstance.FLFlag == 'Z')
            {
                status = "Z";
            }

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;

            DateTime now = DateTime.Now;

            List<object> list = new List<object>();
            list.Add(flInstanceId.ToString());                                                                              //LISTID
            list.Add(flDefinitionId);                                                                                       //FLOW_ID
            list.Add(flowDesc);                                                                                             //FLOW_DESC
            list.Add(flInstanceParms[5].ToString());                                                                        //ROLE_ID
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).RoleId);                  //S_ROLE_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                            //S_STEP_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                                     //S_STEP_DESC
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId);                  //S_USER_ID
            list.Add(((object[])clientInfo[0])[1].ToString());                                                              //USER_ID
            list.Add(userName);                                                                                             //USERNAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.FormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : currentFLActivity.FormName);                      //FORM_NAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.WebFormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : currentFLActivity.WebFormName);                //WEBFORM_NAME
            list.Add(sUserName);                                                                                            //S_USERNAME
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.NavigatorMode);                                       //NAVIGATOR_MODE
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.FLNavigatorMode);                                     //FLNAVIGATOR_MODE
            list.Add(nextFLActivity == null ? null : nextFLActivity.Parameters);                                            //PARAMETERS
            list.Add(status);                                                                                               //STATUS
            list.Add(previousFLActivity == null || previousFLActivity.ExecutedTime == new DateTime(1, 1, 1)
                 || (now - previousFLActivity.ExecutedTime).TotalHours < 0.001
                 ? "0" : (now - previousFLActivity.ExecutedTime).TotalHours.ToString("f2"));                                                  //PROC_TIME
            list.Add(previousFLActivity == null
                ? 0 : (previousFLActivity.IsUrgent ? previousFLActivity.UrgentTime : previousFLActivity.ExpTime));          //EXP_TIME
            list.Add(previousFLActivity == null ? TimeUnit.Day.ToString() : previousFLActivity.TimeUnit.ToString());        //TIME_UNIT
            list.Add(flInstanceParms[2]);                                                                                   //FLOWIMPORTANT
            list.Add(flInstanceParms[3]);                                                                                   //FLOWURGENT
            list.Add(tableName);                                                                                            //FORM_TABLE
            list.Add(keyValues[0]);                                                                                         //FORM_KEYS
            list.Add(keyValues[1]);                                                                                         //FORM_PRESENTATION
            list.Add(flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''"));         //REMARK
            list.Add(null);                                                                                                 //VERSION
            list.Add(flInstance.Solution);                                                                                  // VDSNAME;
            list.Add(null);                                                                                                 //SENDBACKSTEP
            list.Add(delayAutoApprove);                                                                                     //LEVEL_NO
            list.Add(now.ToString("yyyy-MM-dd"));                                                                           //UPDATE_DATE
            list.Add(now.ToString("HH:mm:ss"));                                                                             //UPDATE_TIME
            list.Add(nextFLActivity == null ? null : nextFLActivity.Name);                                                  //D_STEP_ID
            list.Add(GetFormPresentCT(flInstance, keyValues[0].ToString(),
                keyValues[1].ToString(), presentFields, clientInfo));                                                       //FORM_PRESENT_CT
            list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                        //ATTACHMENTFILES
            list.Add(GetNvarcharMark(clientInfo));
#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

#if DUSERNAME
            list.Add(GetDUSERNAME(flInstance, flInstanceParms, keyValues, clientInfo));
#endif

            sql = sql + string.Format(INSERT_TODOHIS, list.Select(c=> MarkValue(c)).ToArray());
            return sql;
        }

        /// <summary>
        /// 取得检查时要插入ToDoHis的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoHisSQL4Validate(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            string remark = "Pass";
            if (!flInstance.V)
            {
                remark = "No Pass";
            }

            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            // 由于S_STEP_ID记录了上一个，是错误的，所以在FLInstance.GetNextFLActivities去取上一个FLActivity是多余的。
            // 有时间就修正上面的多余。
            IEventWaiting previousFLActivity = flInstance.PreviousFLActivity == null ? null : (IEventWaiting)flInstance.PreviousFLActivity;
            IEventWaiting currentFLActivity = flInstance.CurrentFLActivity == null ? null : (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            // todolist
            List<IEventWaiting> nextFLActivities = new List<IEventWaiting>();
            foreach (FLActivity a in s)
            {
                if (a is IEventWaiting)
                {
                    nextFLActivities.Add((IEventWaiting)a);
                }
            }

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;

            string userName = Global.GetUserName(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sUserName = Global.GetUserName(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId, clientInfo);

            string sql = string.Empty;
            IEventWaiting nextFLActivity = nextFLActivities != null && nextFLActivities.Count != 0 ? nextFLActivities[0] : null;
            if (nextFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                nextFLActivity = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            string status = "V";
            //string status = "N";
            //if (flInstance.FLFlag == 'Z')
            //{
            //    status = "Z";
            //}
            //else if (nextFLActivity is IFLNotifyActivity)
            //{
            //    status = "F";
            //}
            //else if (flInstance.IsRetake || flInstance.IsPause)
            //{
            //    status = "NF";
            //}
            //else if (flInstance.IsReturn)
            //{
            //    status = "NR";
            //}

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;

            DateTime now = DateTime.Now;

            List<object> list = new List<object>();
            list.Add(flInstanceId.ToString());                                                                              //LISTID
            list.Add(flDefinitionId);                                                                                       //FLOW_ID
            list.Add(flowDesc);                                                                                             //FLOW_DESC
            list.Add(flInstanceParms[5].ToString());                                                                        //ROLE_ID
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).RoleId);                  //S_ROLE_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                            //S_STEP_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                                     //S_STEP_DESC
            list.Add(currentFLActivity == null ? null : ((IEventWaitingExecute)currentFLActivity).UserId);                  //S_USER_ID
            list.Add("SYS");                                                                                                //USER_ID
            list.Add("System Inspector");                                                                                   //USERNAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.FormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : currentFLActivity.FormName);                      //FORM_NAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.WebFormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : currentFLActivity.WebFormName);                //WEBFORM_NAME
            list.Add(sUserName);                                                                                            //S_USERNAME
            list.Add(null);                                                                                                 //NAVIGATOR_MODE
            list.Add(null);                                                                                                 //FLNAVIGATOR_MODE
            list.Add(null);                                                                                                 //PARAMETERS
            list.Add(status);                                                                                               //STATUS
            list.Add(previousFLActivity == null || previousFLActivity.ExecutedTime == new DateTime(1, 1, 1)
                || (now - previousFLActivity.ExecutedTime).TotalHours < 0.001
                ? "0" : (now - previousFLActivity.ExecutedTime).TotalHours.ToString("f2"));                                                  //PROC_TIME
            list.Add(previousFLActivity == null ? 0 : (previousFLActivity.IsUrgent ? previousFLActivity.UrgentTime : previousFLActivity.ExpTime));   //EXP_TIME
            list.Add(previousFLActivity == null ? TimeUnit.Day.ToString() : previousFLActivity.TimeUnit.ToString());        //TIME_UNIT
            list.Add(flInstanceParms[2]);                                                                                   //FLOWIMPORTANT
            list.Add(flInstanceParms[3]);                                                                                   //FLOWURGENT
            list.Add(tableName);                                                                                            //FORM_TABLE
            list.Add(keyValues[0]);                                                                                         //FORM_KEYS
            list.Add(keyValues[1]);                                                                                         //FORM_PRESENTATION
            list.Add(remark);                                                                                               //REMARK
            list.Add(null);                                                                                                 //VERSION
            list.Add(flInstance.Solution);                                                                                  // VDSNAME;
            list.Add(null);                                                                                                 //SENDBACKSTEP
            list.Add(delayAutoApprove);                                                                                     //LEVEL_NO
            list.Add(now.ToString("yyyy-MM-dd"));                                                                           //UPDATE_DATE
            list.Add(now.ToString("HH:mm:ss"));                                                                             //UPDATE_TIME
            list.Add(nextFLActivity == null ? null : nextFLActivity.Name);                                                  //D_STEP_ID
            list.Add(GetFormPresentCT(flInstance, keyValues[0].ToString(),
                keyValues[1].ToString(), presentFields, clientInfo));                                                       //FORM_PRESENT_CT
            list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                        //ATTACHMENTFILES
            list.Add(GetNvarcharMark(clientInfo));
#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

#if DUSERNAME
            list.Add(GetDUSERNAME(flInstance, flInstanceParms, keyValues, clientInfo));
#endif

            sql = sql + string.Format(INSERT_TODOHIS, list.Select(c=> MarkValue(c)).ToArray());
            return sql;
        }

        /// <summary>
        /// 取得加签时要插入ToDoHis的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoHisSQL4PlusApprove(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo, bool plus)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            // 由于S_STEP_ID记录了上一个，是错误的，所以在FLInstance.GetNextFLActivities去取上一个FLActivity是多余的。
            // 有时间就修正上面的多余。
            IEventWaiting previousFLActivity = flInstance.PreviousFLActivity == null ? null : (IEventWaiting)flInstance.PreviousFLActivity;
            IEventWaiting currentFLActivity = flInstance.CurrentFLActivity == null ? null : (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            // todolist
            List<IEventWaiting> nextFLActivities = new List<IEventWaiting>();
            if (s != null && s.Count != 0)
            {
                foreach (FLActivity a in s)
                {
                    if (a is IEventWaiting)
                    {
                        nextFLActivities.Add((IEventWaiting)a);
                    }
                }
            }

            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;

            string userName = Global.GetUserName(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sUserName = userName;

            string sql = string.Empty;
            IEventWaiting nextFLActivity = nextFLActivities != null && nextFLActivities.Count != 0 ? nextFLActivities[0] : null;
            if (nextFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                nextFLActivity = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            string status = "A";

            if ((nextFLActivity is IFLStandActivity && !((IFLStandActivity)nextFLActivity).PlusApproveReturn)
                || (nextFLActivity is IFLApproveActivity && !((IFLApproveActivity)nextFLActivity).PlusApproveReturn))
            {
                status = "AA";//任意加签
            }

            if (flInstance.FLFlag == 'Z')
            {
                status = "Z";
            }

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;

            DateTime now = DateTime.Now;

            List<object> list = new List<object>();
            list.Add(flInstanceId.ToString());                                                                              //LISTID
            list.Add(flDefinitionId);                                                                                       //FLOW_ID
            list.Add(flowDesc);                                                                                             //FLOW_DESC
            list.Add(flInstanceParms[5].ToString());                                                                        //ROLE_ID
            list.Add(flInstanceParms[5].ToString());                                                                        //S_ROLE_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                            //S_STEP_ID
            list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                                     //S_STEP_DESC
            list.Add(((object[])clientInfo[0])[1].ToString());                                                              //S_USER_ID
            list.Add(((object[])clientInfo[0])[1].ToString());                                                              //USER_ID
            list.Add(userName);                                                                                             //USERNAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.FormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : currentFLActivity.FormName);                      //FORM_NAME
            list.Add(currentFLActivity == null || string.IsNullOrEmpty(currentFLActivity.WebFormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : currentFLActivity.WebFormName);                //WEBFORM_NAME
            list.Add(sUserName);                                                                                            //S_USERNAME
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.NavigatorMode);                                       //NAVIGATOR_MODE
            list.Add(nextFLActivity == null ? 0 : (int)nextFLActivity.FLNavigatorMode);                                     //FLNAVIGATOR_MODE
            list.Add(nextFLActivity == null ? null : nextFLActivity.Parameters);                                            //PARAMETERS
            list.Add(status);                                                                                               //STATUS
            list.Add(previousFLActivity == null || previousFLActivity.ExecutedTime == new DateTime(1, 1, 1)
                    || (now - previousFLActivity.ExecutedTime).TotalHours < 0.001
                    ? "0" : (now - previousFLActivity.ExecutedTime).TotalHours.ToString("f2"));                                                  //PROC_TIME
            list.Add(previousFLActivity == null ? 0 : (previousFLActivity.IsUrgent ? previousFLActivity.UrgentTime : previousFLActivity.ExpTime));   //EXP_TIME
            list.Add(previousFLActivity == null ? TimeUnit.Day.ToString() : previousFLActivity.TimeUnit.ToString());        //TIME_UNIT
            list.Add(flInstanceParms[2]);                                                                                   //FLOWIMPORTANT
            list.Add(flInstanceParms[3]);                                                                                   //FLOWURGENT
            list.Add(tableName);                                                                                            //FORM_TABLE
            list.Add(keyValues[0]);                                                                                         //FORM_KEYS
            list.Add(keyValues[1]);                                                                                         //FORM_PRESENTATION
            list.Add(flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''"));         //REMARK
            list.Add(null);                                                                                                 //VERSION
            list.Add(flInstance.Solution);                                                                                  // VDSNAME;
            list.Add(null);                                                                                                 //SENDBACKSTEP
            list.Add(delayAutoApprove);                                                                                     //LEVEL_NO
            list.Add(now.ToString("yyyy-MM-dd"));                                                                           //UPDATE_DATE
            list.Add(now.ToString("HH:mm:ss"));                                                                             //UPDATE_TIME
            list.Add(nextFLActivity == null ? null : nextFLActivity.Name);                                                  //D_STEP_ID
            list.Add(GetFormPresentCT(flInstance, keyValues[0].ToString(),
                keyValues[1].ToString(), presentFields, clientInfo));                                                       //FORM_PRESENT_CT
            list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                        //ATTACHMENTFILES
            list.Add(GetNvarcharMark(clientInfo));
            if (plus)
            {
                var plusroles = new List<string>();
                if (!string.IsNullOrEmpty(flInstanceParms[8].ToString()))
                {
                    string[] ss = flInstanceParms[8].ToString().Split(';');
                    foreach (string id in ss)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            if (id.StartsWith("U:"))
                            {
                                plusroles.Add(Global.GetUserName(id.Substring(2), clientInfo));
                            }
                            else
                            {
                                plusroles.Add(Global.GetGroupName(id, clientInfo));
                            }
                        }
                    }
                }
                list.Add(string.Join(",", plusroles));
            }
#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif
#if DUSERNAME
            list.Add(GetDUSERNAME(flInstance, flInstanceParms, keyValues, clientInfo));
#endif
            if (plus)
            {
                sql = sql + string.Format(INSERT_TODOHIS2, list.Select(c=> MarkValue(c)).ToArray());
            }
            else
            {
                sql = sql + string.Format(INSERT_TODOHIS, list.Select(c=> MarkValue(c)).ToArray());
            }
            return sql;
        }

        /// <summary>
        /// 取得加签返回时要插入ToDoHis的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoHisSQL4PlusReturn(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            return GetInsertToDoHisSQL(flInstance, flInstanceParms, keyValues, clientInfo, true);
        }

        /// <summary>
        /// 取得插入ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="nextFLActivity">下一Activity</param>
        /// <returns></returns>
        private static string GetInsertToDoListSQL(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo, IEventWaiting nextFLActivity)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            IEventWaiting currentFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);
            string email = Global.GetUserEmail(((object[])clientInfo[0])[1].ToString(), clientInfo);

            string flowPath = (currentFLActivity == null ? string.Empty : currentFLActivity.Name) + ";" + nextFLActivity.Name;

            IEventWaiting currentFLActivity2 = null;
            if (currentFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)currentFLActivity).ParentActivity;
                currentFLActivity2 = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
            }
            else
            {
                currentFLActivity2 = currentFLActivity;
            }

            IEventWaiting nextFLActivity2 = null;
            if (nextFLActivity is IFLApproveBranchActivity)
            {
                string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                nextFLActivity2 = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                ((IEventWaitingExecute)nextFLActivity2).UserId = ((IEventWaitingExecute)nextFLActivity).UserId;
                ((IEventWaitingExecute)nextFLActivity2).RoleId = ((IEventWaitingExecute)nextFLActivity).RoleId;
            }
            else
            {
                nextFLActivity2 = nextFLActivity;
            }

            int plusApprove = 0;
            if (nextFLActivity2 is IFLStandActivity)
            {
                plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity2).PlusApprove);
            }
            else if (nextFLActivity2 is IFLApproveActivity)
            {
                plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity2).PlusApprove);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity2 is IFLStandActivity && ((IFLStandActivity)nextFLActivity2).DelayAutoApprove)
                || (nextFLActivity2 is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity2).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            string sendToId = string.Empty;
            string sendToName = string.Empty;
            string sendToKind = "1";
            if (nextFLActivity2.SendToKind == SendToKind.Role)
            {
                string q = nextFLActivity2.SendToRole;
                if (string.IsNullOrEmpty(q))
                {
                    string message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotSet");
                    throw new FLException(2, message);
                }
                string[] qq = q.Split(";".ToCharArray());

                sendToId = qq[0].Trim();
                flInstance.RL.Add(string.Format("R:{0}", sendToId));
            }
            else if (nextFLActivity2.SendToKind == SendToKind.RefRole)
            {
                string sendToField = nextFLActivity2.SendToField;
                string values = keyValues[1].ToString();

                if (nextFLActivity is FLStandActivity && ((ISupportFLDetailsActivity)nextFLActivity).SendToId2 != string.Empty)
                {
                    sendToId = ((ISupportFLDetailsActivity)nextFLActivity).SendToId2;
                }
                else
                {
                    sendToId = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                }
                flInstance.RL.Add(string.Format("R:{0}", sendToId));
            }
            else if (nextFLActivity2.SendToKind == SendToKind.Applicate)
            {
                sendToId = flInstance.Creator;
                sendToKind = "2";
                flInstance.RL.Add(string.Format("U:{0}", sendToId));
            }
            else if (nextFLActivity2.SendToKind == SendToKind.User)
            {
                string q = nextFLActivity2.SendToUser;
                if (string.IsNullOrEmpty(q))
                {
                    string message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotSet");
                    throw new FLException(2, message);
                }
                sendToId = q.Split(';')[0];
                sendToKind = "2";
                flInstance.RL.Add(string.Format("U:{0}", sendToId));
            }
            else if (nextFLActivity2.SendToKind == SendToKind.RefUser)
            {
                string sendToField = nextFLActivity2.SendToField;
                string values = keyValues[1].ToString();
                sendToId = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo, true);
                sendToKind = "2";
                flInstance.RL.Add(string.Format("U:{0}", sendToId));
            }

            else
            {
                if (flInstance.IsRetake)
                {
                    sendToId = flInstanceParms[5].ToString();
                }
                else
                {
                    if (flInstance.FLDirection == FLDirection.GoToBack)
                    {
                        sendToId = ((IEventWaitingExecute)nextFLActivity2).RoleId;
                    }
                    else
                    {
                        if (nextFLActivity2.SendToKind == SendToKind.Manager)
                        {
                            if (string.IsNullOrEmpty(flInstance.R))
                            {
                                sendToId = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                            }
                            else
                            {
                                sendToId = Global.GetManagerRoleId(flInstance.R.ToString(), orgKind, clientInfo);
                            }
                        }
                        else if (nextFLActivity2.SendToKind == SendToKind.RefManager)
                        {
                            if (nextFLActivity2 is IFLApproveActivity && !string.IsNullOrEmpty(flInstance.R))
                            {
                                sendToId = Global.GetManagerRoleId(flInstance.R.ToString(), orgKind, clientInfo);
                            }
                            else
                            {
                                string sendToField = nextFLActivity2.SendToField;
                                string values = keyValues[1].ToString();

                                string roleId = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                                sendToId = Global.GetManagerRoleId(roleId.ToString(), orgKind, clientInfo);
                            }
                        }
                        else if (nextFLActivity2.SendToKind == SendToKind.ApplicateManager)
                        {
                            if (nextFLActivity2 is IFLApproveActivity && !string.IsNullOrEmpty(flInstance.R))
                            {
                                sendToId = Global.GetManagerRoleId(flInstance.R.ToString(), orgKind, clientInfo);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(flInstance.CreateRole))
                                {
                                    sendToId = Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo);
                                    flInstance.RL.Add(string.Format("R:{0}", sendToId));
                                }
                                else
                                {
                                    List<string> roleIds = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                                    if (roleIds.Count > 0)
                                    {
                                        sendToId = Global.GetManagerRoleId(roleIds[0], orgKind, clientInfo);
                                        flInstance.RL.Add(string.Format("R:{0}", sendToId));
                                    }
                                }
                            }

                        }
                        flInstance.RL.Add(string.Format("R:{0}", sendToId));
                    }
                }
            }

            if (sendToKind == "1")
            {
                sendToName = Global.GetGroupName(sendToId, clientInfo);
                var users = Global.GetUsersIdsByRoleId(sendToId, clientInfo);
                if (users.Count == 0)
                {
                    string message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "NoUsersInSendToId");
                    throw new FLException(2, string.Format(message, sendToId));
                }
            }
            else
            {
                sendToName = Global.GetUserName(sendToId, clientInfo);
            }

            if (sendToId == null || sendToId == string.Empty)
            {
                String message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdIsNull");
                throw new FLException(message);
            }

            // 判断代理 移到InsertToDoAndCallMethod中
            bool parAgentNotify = false;
            //string sSendToId = currentFLActivity == null ? string.Empty : currentFLActivity.SendToId;
            //SendToKind sSendToKind = currentFLActivity2 == null ? SendToKind.Role : currentFLActivity2.SendToKind;
            //if ((sSendToId != null && sSendToId != string.Empty) && sSendToKind != SendToKind.Applicate && sSendToKind != SendToKind.User && sSendToKind != SendToKind.RefUser)
            //{
            //    List<string> userRoleIds = Global.GetRoleIdsByUserId(sUserId, clientInfo);
            //    if (!userRoleIds.Contains(sSendToId))
            //    {
            //        object parAgent = Global.GetPARAGENT(flowDesc, sUserId, clientInfo);
            //        if (parAgent != null && Convert.ToBoolean(parAgent))
            //        {
            //            // 添加一个Notify
            //            parAgentNotify = true;
            //        }
            //    }
            //}

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
            string keys = keyValues[0].ToString();
            string presenation = keyValues[1].ToString();
            string presenationCT = GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

            nextFLActivity.SendToId = sendToId;

            string status = "N";
            if (nextFLActivity2 is IFLNotifyActivity)
            {
                status = "F";
            }
            //else if (flInstance.RootFLActivity.ChildFLActivities[0].Name == nextFLActivity2.Name)
            //{

            if (flInstance.IsRetake )
            {
                status = "NF";
            }
            else if (flInstance.IsPause)
            {
                status = "NP";
            }
            else if (flInstance.IsReturn)  // 把Return都归为此类
            {
                status = "NR";
            }
            //}

            int navigatorMode = 0;
            if ((flInstance.IsRetake || flInstance.IsReturn || flInstance.IsPause || !flInstance.V))
            {
                navigatorMode = (flInstance.RootFLActivity.ChildFLActivities[0].Name == nextFLActivity.Name)/*取得是否取回或退回到第一个activity*/ ? 2 : (int)nextFLActivity.NavigatorMode;
            }
            else
            {
                navigatorMode = (int)nextFLActivity2.NavigatorMode;
            }
            //if (status == "NR")
            //{
            //    navigatorMode = 2;
            //}

            bool isUrgent = (flInstanceParms[3] != null && flInstanceParms[3].ToString() == "1") ? true : false;
            IFLRootActivity rootActivity = ((IFLRootActivity)flInstance.RootFLActivity);
            DateTime now = DateTime.Now;

            decimal expTime = -1;
            decimal urgentTime = -1;
            TimeUnit timeUnit = TimeUnit.Hour;

            decimal rExpTime = rootActivity.ExpTime;
            decimal rUrgentTime = rootActivity.UrgentTime;

            if (!string.IsNullOrEmpty(rootActivity.ExpTimeField))
            {
                DataSet dataset = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo);
                if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                {
                    object value = dataset.Tables[0].Rows[0][rootActivity.ExpTimeField];
                    if (!value.Equals(DBNull.Value))
                    {
                        decimal.TryParse(value.ToString(), out rExpTime);
                        decimal.TryParse(value.ToString(), out rUrgentTime);
                    }
                }
            }

            decimal nExpTime = nextFLActivity2.ExpTime;
            decimal nUrgentTime = nextFLActivity2.UrgentTime;
            if (rootActivity.TimeUnit == TimeUnit.Day)
            {
                rExpTime *= 8;
                rUrgentTime *= 8;
            }
            if (nextFLActivity2.TimeUnit == TimeUnit.Day)
            {
                nExpTime *= 8;
                nUrgentTime *= 8;
            }

            decimal timeSpanr = isUrgent ? rUrgentTime : rExpTime;
            decimal timeSpann = isUrgent ? nUrgentTime : nExpTime;

            if (timeSpanr <= 0)//如果没有设置root取next
            {
                expTime = nExpTime;
                urgentTime = nUrgentTime;
            }
            else
            {
                //decimal usedHours = new decimal((now - flInstance.CreatedTime).TotalHours);
                decimal usedDays = WorkTimeSpan(now.Date, flInstance.CreatedTime.Date, true, null).Days;
                decimal usedHours = usedDays * 8 + now.Hour - flInstance.CreatedTime.Hour;

                if (timeSpanr - usedHours > 0)
                {
                    rExpTime -= usedHours;
                    rUrgentTime -= usedHours;
                    if (timeSpann <= 0)
                    {
                        expTime = rExpTime;
                        urgentTime = rUrgentTime;
                    }
                    else
                    {
                        expTime = Math.Min(rExpTime, nExpTime);
                        urgentTime = Math.Min(rUrgentTime, nUrgentTime);
                    }
                }
            }

            List<object> list = new List<object>();
            list.Add(flInstanceId.ToString());                                                                      // LISTID;    0
            list.Add(flDefinitionId.ToString());                                                                    // FLOW_ID;         1
            list.Add(flowDesc);                                                                                     // FLOW_DESC;     2
            list.Add(sUserId);                                                                                      // S_USER_ID;      3
            list.Add(currentFLActivity2 == null ? null : currentFLActivity2.Name);                                  // S_STEP_ID;     4
            list.Add(currentFLActivity2 == null ? null : currentFLActivity2.Description);                           // S_STEP_DESC;   5
            list.Add(nextFLActivity2.Name);                                                                         // D_STEP_ID;      6
            list.Add(nextFLActivity2.Description);                                                                  // D_STEP_DESC;     7
            list.Add(expTime);                                                                                      // EXP_TIME;       8
            list.Add(urgentTime);                                                                                   // URGENT_TIME;     9 
            list.Add(timeUnit);                                                                                     // TIME_UNIT;     10
            list.Add(sUserName);                                                                                    // USERNAME
            list.Add(nextFLActivity2 == null || string.IsNullOrEmpty(nextFLActivity2.FormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : nextFLActivity2.FormName);                // FORM_NAME
            list.Add(navigatorMode);                                                                                // NAVIGATOR_MODE;
            list.Add((int)nextFLActivity2.FLNavigatorMode);                                                         // FLNAVIGATOR_MODE;
            list.Add(nextFLActivity2.Parameters);                                                                   // PARAMETERS;
            list.Add(sendToKind);                                                                                   // SENDTO_KIND;
            list.Add(sendToId);                                                                                     // SENDTO_ID;
            list.Add(flInstanceParms[2]);                                                                           // FLOWIMPORTANT;
            list.Add(flInstanceParms[3]);                                                                           // FLOWURGENT;
            list.Add(status);                                                                                       // STATUS;              // 先不管
            list.Add(tableName);                                                                                    // FORM_TABLE;
            list.Add(keys);                                                                                         // FORM_KEYS
            list.Add(presenation);                                                                                  // FORM_PRESENTATION;
            list.Add(presenationCT);                                                                                // FORM_PRESENT_CT;
            list.Add(flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''")); // REMARK;
            list.Add(flInstanceParms[6].ToString());                                                                // PROVIDER_NAME;       // 先不管
            list.Add(null);                                                                                         // VERSION;             // 先不管
            list.Add(email);                                                                                        // EMAIL_ADD;
            list.Add(null);                                                                                         // EMAIL_STATUS;
            list.Add(flInstance.Solution);                                                                          // VDSNAME;            
            list.Add(null);                                                                                         // SENDBACKSTEP;        // 先不管
            list.Add(delayAutoApprove);                                                                             // LEVEL_NO
            list.Add(nextFLActivity2 == null || string.IsNullOrEmpty(nextFLActivity2.WebFormName)
                ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : nextFLActivity2.WebFormName);          // WEBFORM_NAME            
            list.Add(flInstance.Creator);                                                                           // APPLICANT
            list.Add(flowPath);                                                                                     // FLOWPATH
            list.Add(now.ToString("yyyy-MM-dd"));                                                                   // UPDATE_DATE
            list.Add(now.ToString("HH:mm:ss"));                                                                     // UPDATE_TIME
            list.Add(plusApprove);                                                                                  // PLUSAPPROVE
            list.Add(null);                                                                                         // PLUSROLES
            list.Add(flInstance.GetMultiStepReturn((FLActivity)nextFLActivity2) ? "1" : "0");                       // MULTISTEPRETURN
            list.Add(sendToName);                                                                                   // SENDTO_NAME
            list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                // ATTACHMENTFILES
            list.Add(GetNvarcharMark(clientInfo));

#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

            //if (!parAgentNotify)
            //{
            return string.Format(INSERT_TODOLIST, list.Select(c=> MarkValue(c)).ToArray()); ;
            //}
            //else
            //{
            //    List<object> flInstanceParms2 = new List<object>(flInstanceParms);
            //    if (flInstanceParms2.Count >= 9)
            //    {
            //        flInstanceParms2[8] = sSendToId;
            //    }
            //    else
            //    {
            //        int count = flInstanceParms2.Count;
            //        for (int i = 0; i < 8 - count; i++)
            //        {
            //            flInstanceParms2.Add(null);
            //        }
            //        flInstanceParms2.Add(sSendToId);
            //    }

            //    string parAgentNotifySQL = GetInsertToDoListSQL4Notify(flInstance, currentFLActivity2, null, flInstanceParms2.ToArray(), keyValues, clientInfo);
            //    return string.Format(INSERT_TODOLIST, list.Select(c=> MarkValue(c)).ToArray()) + ";" + parAgentNotifySQL;
            //}
        }


        private static TimeSpan WorkTimeSpan(DateTime nowTime, DateTime updateTime, bool weekendSensible, List<string> extDates)
        {
            TimeSpan span = new TimeSpan();
            if (weekendSensible)
            {
                if (nowTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    nowTime = nowTime.Date.AddSeconds(-1);
                }
                else if (nowTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    nowTime = nowTime.Date.AddDays(-1).AddSeconds(-1);
                }

                if (updateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    updateTime = updateTime.Date.AddDays(2);
                }
                else if (updateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    updateTime = updateTime.Date.AddDays(1);
                }
            }
            span = nowTime - updateTime;
            if (weekendSensible)
            {
                int weekends = span.Days / 7;
                int i = nowTime.DayOfWeek - updateTime.DayOfWeek;
                if (i < 0)
                    weekends++;
                span = span.Subtract(new TimeSpan(2 * weekends, 0, 0, 0));
            }
            int extDays = 0;
            if (extDates == null) return span;
            foreach (string extDate in extDates)
            {
                if (Convert.ToDateTime(extDate).CompareTo(nowTime) < 0
                    && Convert.ToDateTime(extDate).CompareTo(updateTime) > 0)
                {
                    if (weekendSensible)
                    {
                        if (Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Saturday
                            && Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Sunday)
                        {
                            extDays++;
                        }
                    }
                    else
                    {
                        extDays++;
                    }
                }
            }
            span = span.Subtract(new TimeSpan(extDays, 0, 0, 0));
            return span;
        }

        private static List<string> GetDeletePaths(FLActivity activity)
        {
            var deletePaths = new List<string>();
            foreach (var nextActivity in activity.NextActivities)
            {
                deletePaths.Add(nextActivity.Name);
                deletePaths.AddRange(GetDeletePaths(nextActivity));
            }
            activity.NextActivities.Clear();
            return deletePaths;
        }


        /// <summary>
        /// 取得删除ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flPath">流程路径</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetDeleteToDoListSQL(FLInstance flInstance, string flPath, object[] clientInfo)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;

            IEventWaiting currentFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
            List<FLActivity> s = flInstance.NextFLActivities;

            string deleteSql = string.Empty;
            if (flInstance.Version == "2.0")
            {
                if (flInstance.IsReturn || flInstance.IsRetake)
                {
                    if (currentFLActivity != null)
                    {
                        var deletePaths = new List<string>();
                        foreach (FLActivity activity in flInstance.NextFLActivities)
                        {
                            //删除所有activity
                            deletePaths.AddRange(GetDeletePaths(activity));
                        }
                        if (deletePaths.Count > 0)
                        {
                            var wheres = new List<string>();
                            foreach (var deletePath in deletePaths)
                            {
                                wheres.Add(string.Format("FLOWPATH LIKE '%;{0}'", deletePath));
                            }

                            deleteSql = string.Format(DELETE_TODOLIST_6, flInstanceId.ToString(), string.Join(" OR ", wheres));
                        }
                        else
                        {
                            deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                        }
                    }
                    else
                    {
                        deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                    }
                }
                else
                {
                    if (currentFLActivity != null && ((FLActivity)currentFLActivity).UpperParallel != string.Empty && flPath != string.Empty && flInstance.FLFlag != 'Z')
                    {
                        bool b = true;
                        if (s.Count != 0)
                        {
                            foreach (FLActivity activity in s)
                            {
                                if (activity is IEventWaiting && activity.UpperParallel != ((FLActivity)currentFLActivity).UpperParallel)
                                {
                                    b = false;
                                    break;
                                }
                            }
                        }


                        if ((((FLActivity)currentFLActivity).IsUpperParallelAnd) || (!((FLActivity)currentFLActivity).IsUpperParallelAnd && b))
                        {
                            deleteSql = string.Format(DELETE_TODOLIST_3, flInstanceId.ToString(), flPath);
                        }
                        else
                        {

                            var parallelActivity = flInstance.RootFLActivity.GetFLActivityByName(((FLActivity)currentFLActivity).UpperParallel);
                            if (!((FLActivity)currentFLActivity).IsUpperParallelAnd || (!string.IsNullOrEmpty(parallelActivity.Description.Trim()) && parallelActivity.Description.ToLower().Contains("rate")))
                            {
                                //for 平行嵌套，只退出子平行时不要删除父平行的内容
                               
                                var childActivities = parallelActivity.GetAllChildFLActivities();
                                var sqls = new List<string>();
                                foreach (var activity in childActivities.Values)
                                {
                                    var previousName = flPath.Split(';')[0];
                                    if (activity is IEventWaiting)
                                    {
                                        var flowPath = string.Format("{0};{1}", previousName, (activity as FLActivity).Name);
                                        sqls.Add(string.Format(DELETE_TODOLIST_3, flInstanceId.ToString(), flowPath));
                                    }
                                }
                                deleteSql = string.Join(";", sqls);
                            }
                            else
                            {
                                deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                            }
                        }

                    }
                    else
                    {
                        deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                    }
                }
            }
            else
            {
                if (currentFLActivity != null && ((FLActivity)currentFLActivity).UpperParallel != string.Empty && flPath != string.Empty && flInstance.FLFlag != 'Z')
                {
                    bool b = true;
                    if (s.Count != 0)
                    {
                        foreach (FLActivity activity in s)
                        {
                            if (activity is IEventWaiting && activity.UpperParallel != ((FLActivity)currentFLActivity).UpperParallel)
                            {
                                b = false;
                                break;
                            }
                        }
                    }

                    if (flInstance.IsReturn)
                    {
                        if (b)
                        {
                            //平行的关卡里多步退回 用like
                            deleteSql = string.Format(DELETE_TODOLIST_6_1, flInstanceId.ToString(), ((FLActivity)currentFLActivity).Name);
                        }
                        else
                        {
                            //没有判断没有退出平行的情况
                            deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());//平行的直接退回
                        }
                    }
                    else
                    {
                        if ((((FLActivity)currentFLActivity).IsUpperParallelAnd) || (!((FLActivity)currentFLActivity).IsUpperParallelAnd && b))
                        {
                            deleteSql = string.Format(DELETE_TODOLIST_3, flInstanceId.ToString(), flPath);
                        }
                        else
                        {
                            deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                        }
                    }
                }
                else
                {
                    deleteSql = string.Format(DELETE_TODOLIST, flInstanceId.ToString());
                }
            }
            return deleteSql;
        }

        /// <summary>
        /// 取得通知时要插入ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="notifyActivity">通知Activity</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoListSQL4Notify(FLInstance flInstance, IEventWaiting currentFLActivity, IFLNotifyActivity notifyActivity, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;

            List<string> sendToIds = new List<string>();
            if (notifyActivity != null)
            {
                string id = string.Empty;
                if (notifyActivity.SendToKind == SendToKind.Role)
                {
                    string q = notifyActivity.SendToRole;
                    if (string.IsNullOrEmpty(q))
                    {
                        string message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotSet");
                        throw new FLException(2, message);
                    }
                    string[] qq = q.Split(";".ToCharArray());
                    id = qq[0].Trim();
                    sendToIds.AddRange(Global.GetUsersIdsByRoleId(id, clientInfo));
                }
                else if (notifyActivity.SendToKind == SendToKind.RefRole)
                {
                    string values = keyValues[1].ToString();
                    string sendToField = notifyActivity.SendToField;
                    //id = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);

                    id = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo, true);

                    if (!string.IsNullOrEmpty(id))
                    {
                        string[] roles = id.Split(';');
                        foreach (var role in roles)
                        {
                            sendToIds.AddRange(Global.GetUsersIdsByRoleId(role, clientInfo));
                        }
                    }
                }

                else if (notifyActivity.SendToKind == SendToKind.User)
                {
                    string q = notifyActivity.SendToUser;
                    if (string.IsNullOrEmpty(q))
                    {
                        string message = SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "SendToIdNotSet");
                        throw new FLException(2, message);
                    }
                    string[] users = q.Split(';');
                    foreach (string user in users)
                    {
                        if (user.Trim().Length > 0)
                        {
                            sendToIds.Add(user.Trim());
                        }
                    }
                }
                else if (notifyActivity.SendToKind == SendToKind.RefUser)
                {
                    string values = keyValues[1].ToString();
                    string sendToField = notifyActivity.SendToField;
                    id = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo, true);//取出RefUser 方法一样就不重写了
                    if (!string.IsNullOrEmpty(id))
                    {
                        string[] users = id.Split(';');
                        foreach (string user in users)
                        {
                            if (user.Trim().Length > 0)
                            {
                                sendToIds.Add(user.Trim());
                            }
                        }
                    }
                }
                else if (notifyActivity.SendToKind == SendToKind.RefManager)
                {
                    string values = keyValues[1].ToString();
                    string sendToField = notifyActivity.SendToField;
                    id = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                    id = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                    sendToIds.AddRange(Global.GetUsersIdsByRoleId(id, clientInfo));
                }
                else if (notifyActivity.SendToKind == SendToKind.Manager)
                {
                    if (flInstance.IsReturn)
                    {
                        id = notifyActivity.RoleId;
                    }
                    {
                        id = Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo);
                    }
                    sendToIds.AddRange(Global.GetUsersIdsByRoleId(id, clientInfo));
                }
                else if (notifyActivity.SendToKind == SendToKind.Applicate)
                {
                    sendToIds.Add(flInstance.Creator);
                }
                else if (notifyActivity.SendToKind == SendToKind.ApplicateManager)
                {

                    if (!string.IsNullOrEmpty(flInstance.CreateRole))
                    {
                        id = Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo);
                        sendToIds.AddRange(Global.GetUsersIdsByRoleId(id, clientInfo));
                    }
                    else
                    {
                        List<string> roleIds = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                        if (roleIds.Count > 0)
                        {
                            id = Global.GetManagerRoleId(roleIds[0], orgKind, clientInfo);
                            sendToIds.AddRange(Global.GetUsersIdsByRoleId(id, clientInfo));
                        }
                    }
                }


                else if (notifyActivity.SendToKind == SendToKind.AllRoles)
                {
                    List<string> list = new List<string>();
                    foreach (string q in flInstance.RL)
                    {
                        if (string.IsNullOrEmpty(q)) continue;
                        string[] qq = q.Split(":".ToCharArray());
                        if (qq[0] == "R")
                        {
                            list.AddRange(Global.GetUsersIdsByRoleId(qq[1].Trim(), clientInfo));
                        }
                        else
                        {
                            list.Add(qq[1].Trim());
                        }
                    }
                    list.Add(flInstance.Creator);

                    foreach (string u in list)
                    {
                        if (sendToIds.Contains(u))
                        {
                            continue;
                        }

                        sendToIds.Add(u);
                    }
                }
                else if (notifyActivity.SendToKind == SendToKind.LastUser)
                {
                    List<string> list = new List<string>();
                    if (flInstance.RL.Count > 0)
                    {
                        for (int i = flInstance.RL.Count - 1; i >= 0; i--)
                        {
                            string q = flInstance.RL[i];
                            if (string.IsNullOrEmpty(q)) continue;
                            string[] qq = q.Split(":".ToCharArray());
                            if (qq[0] == "R")
                            {
                                list.AddRange(Global.GetUsersIdsByRoleId(qq[1].Trim(), clientInfo));
                            }
                            else
                            {
                                list.Add(qq[1].Trim());
                            }
                            break;
                        }
                    }
                    else
                    {
                        list.Add(flInstance.Creator);
                    }

                    foreach (string u in list)
                    {
                        if (sendToIds.Contains(u))
                        {
                            continue;
                        }

                        sendToIds.Add(u);
                    }
                }
            }
            else
            {
                string s = flInstanceParms[8].ToString();
                string[] ss = s.Split(';');
                foreach (string id in ss)
                {
                    if (id != null && id != string.Empty)
                    {
                        string[] ss1 = id.Split(':');
                        if (ss1.Length > 1)
                        {
                            if (ss1[ss1.Length - 1].Trim().ToLower() == "userid")
                            {
                                sendToIds.Add(ss1[0]);
                            }
                            else
                            {

                                sendToIds.AddRange(Global.GetUsersIdsByRoleId(ss1[0], clientInfo));
                            }
                        }
                        else
                        {
                            List<string> userofrole = Global.GetUsersIdsByRoleId(id, clientInfo);
                            if (userofrole.Count > 0)
                            {
                                string agent = Global.GetAgent(id, userofrole[0], flInstance.RootFLActivity.Description, clientInfo);
                                if (!string.IsNullOrEmpty(agent))
                                {
                                    object parAgent = Global.GetPARAGENT(flInstance.RootFLActivity.Description, agent, clientInfo);
                                    if (parAgent != null && Convert.ToBoolean(parAgent))
                                    {
                                        sendToIds.AddRange(userofrole);
                                    }
                                    sendToIds.Add(agent);
                                }
                                else
                                {
                                    sendToIds.AddRange(userofrole);
                                }
                            }
                        }
                    }
                }
            }

            var flowPath = notifyActivity == null ? Guid.NewGuid().ToString() + ";" + currentFLActivity.Name : currentFLActivity.Name + ";" + notifyActivity.Name;

            IEventWaiting nextFLActivity = currentFLActivity;

            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);
            string email = Global.GetUserEmail(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sql = string.Empty;

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
            string keys = keyValues[0].ToString();
            string presenation = keyValues[1].ToString();
            string presenationCT = GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

            int plusApprove = 0;
            if (nextFLActivity is IFLStandActivity)
            {
                plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
            }
            else if (nextFLActivity is IFLApproveActivity)
            {
                plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            foreach (string id in sendToIds)
            {
                if (currentFLActivity is IFLApproveBranchActivity)
                {
                    string parentName = ((IFLApproveBranchActivity)currentFLActivity).ParentActivity;
                    currentFLActivity = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                }

                IEventWaiting nextFLActivity2 = null;
                if (nextFLActivity is IFLApproveBranchActivity)
                {
                    string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                    nextFLActivity2 = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                }
                else
                {
                    nextFLActivity2 = nextFLActivity;
                }

                string sendToKind = "2";
                string status = "F";
                string sendToName = Global.GetUserName(id, clientInfo);

                DateTime now = DateTime.Now;
                string remark = flInstance.IsReturn == true ? "Return(system)" : (flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''"));

                List<object> list = new List<object>();
                list.Add(flInstanceId.ToString());                                                                      // LISTID;    0
                list.Add(flDefinitionId.ToString());                                                                    // FLOW_ID;         1
                list.Add(flowDesc);                                                                                     // FLOW_DESC;     2
                list.Add(sUserId);                                                                                      // S_USER_ID;      3
                list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                    // S_STEP_ID;     4
                list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                             // S_STEP_DESC;   5
                list.Add(notifyActivity != null ? notifyActivity.Name : nextFLActivity2.Name);                          // D_STEP_ID;      6
                list.Add(notifyActivity != null ? notifyActivity.Description : nextFLActivity2.Description);            // D_STEP_DESC;     7
                list.Add(notifyActivity != null ? notifyActivity.ExpTime : nextFLActivity2.ExpTime);                    // EXP_TIME;       8
                list.Add(notifyActivity != null ? notifyActivity.UrgentTime : nextFLActivity2.UrgentTime);              // URGENT_TIME;     9 
                list.Add(notifyActivity != null ? notifyActivity.TimeUnit : nextFLActivity2.TimeUnit);                  // TIME_UNIT;     10
                list.Add(sUserName);                                                                                    // USERNAME
                list.Add(notifyActivity != null && !string.IsNullOrEmpty(notifyActivity.FormName)
                    ? notifyActivity.FormName
                    : (
                        nextFLActivity2 != null && !string.IsNullOrEmpty(nextFLActivity2.FormName)
                        ? nextFLActivity2.FormName : ((IFLRootActivity)flInstance.RootFLActivity).FormName)
                    );                                                                                                  // FORM_NAME;
                list.Add(0);                                                                                            // NAVIGATOR_MODE;
                list.Add(3);                                                                                            // FLNAVIGATOR_MODE;
                list.Add(notifyActivity != null ? notifyActivity.Parameters : nextFLActivity2.Parameters);              // PARAMETERS;
                list.Add(sendToKind);                                                                                   // SENDTO_KIND;
                list.Add(id);                                                                                           // SENDTO_ID;
                list.Add(flInstanceParms[2]);                                                                           // FLOWIMPORTANT;
                list.Add(flInstanceParms[3]);                                                                           // FLOWURGENT;
                list.Add(status);                                                                                       // STATUS;              // 先不管
                list.Add(tableName);                                                                                    // FORM_TABLE;
                list.Add(keys);                                                                                         // FORM_KEYS
                list.Add(presenation);                                                                                  // FORM_PRESENTATION;
                list.Add(presenationCT);                                                                                // FORM_PRESENT_CT;
                list.Add(remark);                                                                                       // REMARK;
                list.Add(flInstanceParms[6].ToString());                                                                // PROVIDER_NAME;       // 先不管
                list.Add(null);                                                                                         // VERSION;             // 先不管
                list.Add(email);                                                                                        // EMAIL_ADD;
                list.Add(null);                                                                                         // EMAIL_STATUS;
                list.Add(flInstance.Solution);                                                                          // VDSNAME;
                list.Add(null);                                                                                         // SENDBACKSTEP;        // 先不管
                list.Add(delayAutoApprove);                                                                             // LEVEL_NO
                list.Add(notifyActivity != null && !string.IsNullOrEmpty(notifyActivity.WebFormName)
                    ? notifyActivity.WebFormName
                    : (
                        nextFLActivity2 != null && !string.IsNullOrEmpty(nextFLActivity2.WebFormName)
                        ? nextFLActivity2.WebFormName : ((IFLRootActivity)flInstance.RootFLActivity).WebFormName)
                    );                                                                                                  // WEBFORM_NAME;
                list.Add(flInstance.Creator);                                                                           // APPLICANT
                list.Add(flowPath);                                     // FLOWPATH
                list.Add(now.ToString("yyyy-MM-dd"));                                                                   // UPDATE_DATE
                list.Add(now.ToString("HH:mm:ss"));                                                                     // UPDATE_TIME
                list.Add(plusApprove);                                                                                  // PLUSAPPROVE
                list.Add(null);                                                                                         // PLUSROLES
                list.Add(flInstance.GetMultiStepReturn((FLActivity)nextFLActivity2) ? "1" : "0");                       // MULTISTEPRETURN
                list.Add(sendToName);                                                                                   // SENDTO_NAME
                list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                // ATTACHMENTFILES
                list.Add(GetNvarcharMark(clientInfo));

#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

                string insertSql = string.Format(INSERT_TODOLIST, list.Select(c=> MarkValue(c)).ToArray());
                sql = sql + insertSql + ";";
            }

            return sql;
        }

        /// <summary>
        /// 取得取消通知时要插入ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="notifyActivity">通知Activity</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoListSQL4RejectNotify(FLInstance flInstance, IEventWaiting currentFLActivity, IFLNotifyActivity notifyActivity, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;

            List<string> sendToIds = new List<string>();
            if (notifyActivity != null)
            {
                if (notifyActivity.SendToKind == SendToKind.AllRoles)
                {
                    List<string> list = new List<string>();
                    foreach (string q in flInstance.RL)
                    {
                        if (string.IsNullOrEmpty(q)) continue;
                        string[] qq = q.Split(":".ToCharArray());
                        if (qq[0] == "R")
                        {
                            list.AddRange(Global.GetUsersIdsByRoleId(qq[1].Trim(), clientInfo));
                        }
                        else
                        {
                            list.Add(qq[1].Trim());
                        }
                    }
                    list.Add(flInstance.Creator);

                    foreach (string u in list)
                    {
                        if (sendToIds.Contains(u))
                        {
                            continue;
                        }

                        sendToIds.Add(u);
                    }
                }
            }

            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);
            string email = Global.GetUserEmail(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sql = string.Empty;

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
            string keys = keyValues[0].ToString();
            string presenation = keyValues[1].ToString();
            string presenationCT = GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

            foreach (string id in sendToIds)
            {
                string sendToKind = "2";
                string status = "F";
                string sendToName = Global.GetUserName(id, clientInfo);

                DateTime now = DateTime.Now;
                string remark = "Reject(system)";

                List<object> list = new List<object>();
                list.Add(flInstanceId.ToString());                                                                      // LISTID;    0
                list.Add(flDefinitionId.ToString());                                                                    // FLOW_ID;         1
                list.Add(flowDesc);                                                                                     // FLOW_DESC;     2
                list.Add(sUserId);                                                                                      // S_USER_ID;      3
                list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                    // S_STEP_ID;     4
                list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                             // S_STEP_DESC;   5
                list.Add(notifyActivity.Name);                                                                          // D_STEP_ID;      6
                list.Add(notifyActivity.Description);                                                                   // D_STEP_DESC;     7
                list.Add(notifyActivity.ExpTime);                                                                       // EXP_TIME;       8
                list.Add(notifyActivity.UrgentTime);                                                                    // URGENT_TIME;     9 
                list.Add(notifyActivity.TimeUnit);                                                                      // TIME_UNIT;     10
                list.Add(sUserName);                                                                                    // USERNAME
                list.Add(string.IsNullOrEmpty(notifyActivity.FormName)
                    ? ((IFLRootActivity)flInstance.RootFLActivity).FormName
                    : notifyActivity.FormName);                                                                         // FORM_NAME;
                list.Add(0);                                                                                            // NAVIGATOR_MODE;
                list.Add(3);                                                                                            // FLNAVIGATOR_MODE;
                list.Add(notifyActivity.Parameters);                                                                    // PARAMETERS;
                list.Add(sendToKind);                                                                                   // SENDTO_KIND;
                list.Add(id);                                                                                           // SENDTO_ID;
                list.Add(0);                                                                                            // FLOWIMPORTANT;
                list.Add(0);                                                                                            // FLOWURGENT;
                list.Add(status);                                                                                       // STATUS;              // 先不管
                list.Add(tableName);                                                                                    // FORM_TABLE;
                list.Add(keys);                                                                                         // FORM_KEYS
                list.Add(presenation);                                                                                  // FORM_PRESENTATION;
                list.Add(presenationCT);                                                                                // FORM_PRESENT_CT;
                list.Add(remark);                                                                                       // REMARK;
                list.Add(flInstanceParms[3].ToString());                                                                // PROVIDER_NAME;       // 先不管
                list.Add(null);                                                                                         // VERSION;             // 先不管
                list.Add(email);                                                                                        // EMAIL_ADD;
                list.Add(null);                                                                                         // EMAIL_STATUS;
                list.Add(flInstance.Solution);                                                                          // VDSNAME;             
                list.Add(null);                                                                                         // SENDBACKSTEP;        // 先不管
                list.Add(null);                                                                                         // LEVEL_NO
                list.Add(string.IsNullOrEmpty(notifyActivity.WebFormName)
                    ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName
                    : notifyActivity.WebFormName);                                                                      // WEBFORM_NAME;
                list.Add(flInstance.Creator);                                                                           // APPLICANT
                list.Add(Guid.NewGuid().ToString() + ";" + currentFLActivity.Name);                                     // FLOWPATH
                list.Add(now.ToString("yyyy-MM-dd"));                                                                   // UPDATE_DATE
                list.Add(now.ToString("HH:mm:ss"));                                                                     // UPDATE_TIME
                list.Add(null);                                                                                         // PLUSAPPROVE
                list.Add(null);                                                                                         // PLUSROLES
                list.Add("0");                                                                                          // MULTISTEPRETURN
                list.Add(sendToName);                                                                                   // SENDTO_NAME
                list.Add(null);                                                                                         // ATTACHMENTFILES
                list.Add(GetNvarcharMark(clientInfo));
#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

                string insertSql = string.Format(INSERT_TODOLIST, list.Select(c=> MarkValue(c)).ToArray());
                sql = sql + insertSql + ";";
            }

            return sql;
        }
        /// <summary>
        /// 取得加签时要插入ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetInsertToDoListSQL4PlusApprove(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            Guid flInstanceId = flInstance.FLInstanceId;
            Guid flDefinitionId = flInstance.FLDefinitionId;
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;

            List<string> sendToIds = new List<string>();

            string s = flInstanceParms[8].ToString();
            string[] ss = s.Split(';');
            foreach (string id in ss)
            {
                if (id != null && id != string.Empty)
                {
                    sendToIds.Add(id);
                    if (id.StartsWith("U:"))
                    {
                        flInstance.RL.Add(id);
                    }
                    else
                    {
                        flInstance.RL.Add(string.Format("R:{0}", id));
                    }
                }
            }

            IEventWaiting nextFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
            IEventWaiting currentFLActivity = nextFLActivity;

            string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;
            string sUserId = ((object[])clientInfo[0])[1].ToString();
            string sUserName = Global.GetUserName(sUserId, clientInfo);
            string email = Global.GetUserEmail(((object[])clientInfo[0])[1].ToString(), clientInfo);
            string sql = string.Empty;

            string presentFields = ((IFLRootActivity)flInstance.RootFLActivity).PresentFields;
            string keys = keyValues[0].ToString();
            string presenation = keyValues[1].ToString();
            string presenationCT = GetFormPresentCT(flInstance, keys, presenation, presentFields, clientInfo);

            int plusApprove = 0;
            if (nextFLActivity is IFLStandActivity)
            {
                plusApprove = Convert.ToInt32(((IFLStandActivity)nextFLActivity).PlusApprove);
            }
            else if (nextFLActivity is IFLApproveActivity)
            {
                plusApprove = Convert.ToInt32(((IFLApproveActivity)nextFLActivity).PlusApprove);
            }

            // scheduling
            string delayAutoApprove = null;
            if ((nextFLActivity is IFLStandActivity && ((IFLStandActivity)nextFLActivity).DelayAutoApprove)
                || (nextFLActivity is IFLApproveActivity && ((IFLApproveActivity)nextFLActivity).DelayAutoApprove))
                delayAutoApprove = "AUTO";

            foreach (string id in sendToIds)
            {
                if (currentFLActivity is IFLApproveBranchActivity)
                {
                    string parentName = ((IFLApproveBranchActivity)currentFLActivity).ParentActivity;
                    currentFLActivity = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                }

                IEventWaiting nextFLActivity2 = null;
                if (nextFLActivity is IFLApproveBranchActivity)
                {
                    string parentName = ((IFLApproveBranchActivity)nextFLActivity).ParentActivity;
                    nextFLActivity2 = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                }
                else
                {
                    nextFLActivity2 = nextFLActivity;
                }


                string sendToID = string.Empty;
                string sendToKind = string.Empty;
                string sendToName = string.Empty;

                if (id.StartsWith("U:"))
                {
                    sendToKind = "2";
                    sendToID = id.Substring(2);
                    sendToName = Global.GetUserName(sendToID, clientInfo);
                }
                else
                {
                    sendToKind = "1";
                    sendToID = id;
                    sendToName = Global.GetGroupName(sendToID, clientInfo);

                }

                string status = "A";
                if ((nextFLActivity2 is IFLStandActivity && !((IFLStandActivity)nextFLActivity2).PlusApproveReturn)
                    || (nextFLActivity2 is IFLApproveActivity && !((IFLApproveActivity)nextFLActivity2).PlusApproveReturn))
                {
                    status = "AA";//任意加签
                }

                DateTime now = DateTime.Now;


                IFLRootActivity rootActivity = ((IFLRootActivity)flInstance.RootFLActivity);
                bool isUrgent = (flInstanceParms[3] != null && flInstanceParms[3].ToString() == "1") ? true : false;
                decimal expTime = -1;
                decimal urgentTime = -1;
                TimeUnit timeUnit = TimeUnit.Hour;

                decimal rExpTime = rootActivity.ExpTime;
                decimal rUrgentTime = rootActivity.UrgentTime;

                if (!string.IsNullOrEmpty(rootActivity.ExpTimeField))
                {
                    DataSet dataset = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo);
                    if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
                    {
                        object value = dataset.Tables[0].Rows[0][rootActivity.ExpTimeField];
                        if (!value.Equals(DBNull.Value))
                        {
                            decimal.TryParse(value.ToString(), out rExpTime);
                            decimal.TryParse(value.ToString(), out rUrgentTime);
                        }
                    }
                }
                decimal nExpTime = nextFLActivity2.ExpTime;
                decimal nUrgentTime = nextFLActivity2.UrgentTime;
                if (rootActivity.TimeUnit == TimeUnit.Day)
                {
                    rExpTime *= 8;
                    rUrgentTime *= 8;
                }
                if (nextFLActivity2.TimeUnit == TimeUnit.Day)
                {
                    nExpTime *= 8;
                    nUrgentTime *= 8;
                }

                decimal timeSpanr = isUrgent ? rUrgentTime : rExpTime;
                decimal timeSpann = isUrgent ? nUrgentTime : nExpTime;

                if (timeSpanr <= 0)//如果没有设置root取next
                {
                    expTime = nExpTime;
                    urgentTime = nUrgentTime;
                }
                else
                {
                    //decimal usedHours = new decimal((now - flInstance.CreatedTime).TotalHours);
                    decimal usedDays = WorkTimeSpan(now.Date, flInstance.CreatedTime.Date, true, null).Days;
                    decimal usedHours = usedDays * 8 + now.Hour - flInstance.CreatedTime.Hour;

                    if (timeSpanr - usedHours > 0)
                    {
                        rExpTime -= usedHours;
                        rUrgentTime -= usedHours;
                        if (timeSpann <= 0)
                        {
                            expTime = rExpTime;
                            urgentTime = rUrgentTime;
                        }
                        else
                        {
                            expTime = Math.Min(rExpTime, nExpTime);
                            urgentTime = Math.Min(rUrgentTime, nUrgentTime);
                        }
                    }
                }

                List<object> list = new List<object>();
                list.Add(flInstanceId.ToString());                                                                      // LISTID;    0
                list.Add(flDefinitionId.ToString());                                                                    // FLOW_ID;         1
                list.Add(flowDesc);                                                                                     // FLOW_DESC;     2
                list.Add(sUserId);                                                                                      // S_USER_ID;      3
                list.Add(currentFLActivity == null ? null : currentFLActivity.Name);                                    // S_STEP_ID;     4
                list.Add(currentFLActivity == null ? null : currentFLActivity.Description);                             // S_STEP_DESC;   5
                list.Add(nextFLActivity2.Name);                                                                         // D_STEP_ID;      6
                list.Add(nextFLActivity2.Description);                                                                  // D_STEP_DESC;     7
                list.Add(expTime);                                                                      // EXP_TIME;       8
                list.Add(urgentTime);                                                                   // URGENT_TIME;     9 
                list.Add(timeUnit);                                                                     // TIME_UNIT;     10
                list.Add(sUserName);                                                                                    // USERNAME
                list.Add(nextFLActivity2 == null || string.IsNullOrEmpty(nextFLActivity2.FormName)
                    ? ((IFLRootActivity)flInstance.RootFLActivity).FormName : nextFLActivity2.FormName);                // FORM_NAME
                list.Add(0);                                                                                            // NAVIGATOR_MODE;
                list.Add(7);                                                                                            // FLNAVIGATOR_MODE;
                list.Add(nextFLActivity2.Parameters);                                                                   // PARAMETERS;
                list.Add(sendToKind);                                                                                   // SENDTO_KIND;
                list.Add(sendToID);                                                                                           // SENDTO_ID;
                list.Add(flInstanceParms[2]);                                                                           // FLOWIMPORTANT;
                list.Add(flInstanceParms[3]);                                                                           // FLOWURGENT;
                list.Add(status);                                                                                       // STATUS;              // 先不管
                list.Add(tableName);                                                                                    // FORM_TABLE;
                list.Add(keys);                                                                                         // FORM_KEYS
                list.Add(presenation);                                                                                  // FORM_PRESENTATION;
                list.Add(presenationCT);                                                                                // FORM_PRESENT_CT;
                list.Add(flInstanceParms[4] == null ? string.Empty : flInstanceParms[4].ToString().Replace("'", "''")); // REMARK;
                list.Add(flInstanceParms[6].ToString());                                                                // PROVIDER_NAME;       // 先不管
                list.Add(null);                                                                                         // VERSION;             // 先不管
                list.Add(email);                                                                                        // EMAIL_ADD;
                list.Add(null);                                                                                         // EMAIL_STATUS;
                list.Add(flInstance.Solution);                                                                          // VDSNAME;             
                list.Add(null);                                                                                         // SENDBACKSTEP;        // 先不管
                list.Add(delayAutoApprove);                                                                             // LEVEL_NO
                list.Add(nextFLActivity2 == null || string.IsNullOrEmpty(nextFLActivity2.WebFormName)
                    ? ((IFLRootActivity)flInstance.RootFLActivity).WebFormName : nextFLActivity2.WebFormName);          // WEBFORM_NAME
                list.Add(flInstance.Creator);                                                                           // APPLICANT
                list.Add(Guid.NewGuid().ToString() + ";" + currentFLActivity.Name);                                     // FLOWPATH    
                list.Add(now.ToString("yyyy-MM-dd"));                                                                   // UPDATE_DATE
                list.Add(now.ToString("HH:mm:ss"));                                                                     // UPDATE_TIME
                list.Add(plusApprove);                                                                                  // PLUSAPPROVE
                list.Add(null);                                                                                         // PLUSROLES
                list.Add(flInstance.GetMultiStepReturn((FLActivity)nextFLActivity2) ? "1" : "0");                       // MULTISTEPRETURN
                list.Add(sendToName);                                                                                   // SENDTO_NAME
                list.Add(flInstanceParms[9] == null ? null : flInstanceParms[9].ToString());                                                                //ATTACHMENTFILES
                list.Add(GetNvarcharMark(clientInfo));

#if CreateTime
            list.Add(flInstance.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss"));
#endif

                string insertSql = string.Format(INSERT_TODOLIST, list.Select(c=> MarkValue(c)).ToArray());
                sql = sql + insertSql + ";";
            }

            return sql;
        }

        /// <summary>
        /// 取得加签时要更新ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetUpdateToDoListSQL4PlusApprove(FLInstance flInstance, object[] flInstanceParms, object[] clientInfo)
        {
            string applyRole = flInstanceParms[5].ToString();//当前角色
            if (string.IsNullOrEmpty(applyRole))
            {
                applyRole = string.Format("U:{0}", ((object[])clientInfo[0])[1].ToString());
            }
            string newPlusRoles = flInstanceParms[8].ToString();//要加签的角色

            FLActivity activity = flInstance.CurrentFLActivity;
            string step = activity.Name;

            if (activity is IFLApproveBranchActivity)
            {
                step = ((IFLApproveBranchActivity)activity).ParentActivity;
            }

            string dbPlusRoles = Global.GetPlusRoles(flInstance.FLInstanceId.ToString(), step, clientInfo);//取出资料库里还没加签的角色
            List<string> list = new List<string>();
            if (!string.IsNullOrEmpty(dbPlusRoles))
            {
                string[] roles = dbPlusRoles.Split(';');
                foreach (string role in roles)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        if (!list.Contains(role))
                        {
                            list.Add(role);
                        }
                    }
                }


            }
            list.Remove(applyRole);//去掉当前角色
            string[] newRoles = newPlusRoles.Split(';');//加上新的角色
            foreach (string role in newRoles)
            {
                if (!string.IsNullOrEmpty(role))
                {
                    if (!list.Contains(role))
                    {
                        list.Add(role);
                    }
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (string str in list)
            {
                if (builder.Length > 0)
                {
                    builder.Append(";");
                }
                builder.Append(str);
            }

            //string roles = flInstanceParms[8].ToString();
            //FLActivity activity = flInstance.CurrentFLActivity;
            //string step = activity.Name;

            if (activity is IFLApproveBranchActivity)
            {
                step = ((IFLApproveBranchActivity)activity).ParentActivity;
            }

            string sql = string.Format(UPDATE_TODOLIST, builder.ToString(), flInstance.FLInstanceId.ToString(), step);
            return sql;
        }

        /// <summary>
        /// 取得加签返回时要更新ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetUpdateToDoListSQL4PlusReturn(FLInstance flInstance, object[] flInstanceParms, object[] clientInfo)
        {
            //加签返回不变
            string step = flInstance.CurrentFLActivity is IFLApproveBranchActivity
                ? (flInstance.CurrentFLActivity as IFLApproveBranchActivity).ParentActivity : flInstance.CurrentFLActivity.Name;//FLApprove中使用加签返回的问题
            string plusRoles = Global.GetPlusRoles(flInstance.FLInstanceId.ToString(), step, clientInfo);
            string role = flInstanceParms[5].ToString();
            if (string.IsNullOrEmpty(role))
            {
                //用户加签
                role = string.Format("U:{0}", ((object[])clientInfo[0])[1].ToString());
            }

            string sql = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (plusRoles != null && plusRoles != string.Empty)
            {
                string[] ss = plusRoles.Split(';');
                foreach (string s in ss)
                {
                    if (s != null && s.Length > 1 && s != role)
                    {
                        if (sb.Length > 1)
                        {
                            sb.Append(";");
                        }
                        sb.Append(s);
                    }
                }

                sql = string.Format(UPDATE_TODOLIST, sb.ToString(), flInstance.FLInstanceId.ToString(), step);
            }
            return sql;
        }

        /// <summary>
        /// 取得添加附件时要更新ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <returns></returns>
        private static string GetUpdateToDoListSQL4Attachments(FLInstance flInstance, object[] flInstanceParms, object[] clientInfo)
        {
            string attachMents = flInstanceParms[9] == null ? null : flInstanceParms[9].ToString();
            string sql = string.Format(UPDATE_TODOLIST2, attachMents, flInstance.FLInstanceId, GetNvarcharMark(clientInfo));

            return sql;
        }

        private static String GetDBType(object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            String dbAlias = (clientInfo[0] as object[])[2].ToString();
            String dbType = String.Empty;
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            if (xx[0].ToString() == "0")
                dbAlias = xx[1].ToString();
            object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { dbAlias });
            if (myRet != null && myRet[0].ToString() == "0")
                dbType = myRet[1].ToString();

            return dbType;
        }

        // IEventWaiting   IFLNotifyActivity  IFLProcedureActivity
        /// <summary>
        /// 取得CallMethod时要插入ToDoList的SQL语句
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="flPath">流程路径</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void InserToDoAndCallMethod(FLInstance flInstance, object[] flInstanceParms, string flPath, object[] keyValues, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            String dbType = GetDBType(clientInfo);
            String sqlOdbc = String.Empty;

            string sql = string.Empty;
            if (!flInstance.V)
            {
                if (dbType == "4" || dbType == "6")
                {
                    sqlOdbc = GetInsertToDoHisSQL4Validate(flInstance, flInstanceParms, keyValues, clientInfo);
                    object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                    if (executeOdbc[0].ToString() == "1")
                    {
                        throw new FLException(executeOdbc[1].ToString());
                    }
                }
                else
                {
                    sql = GetInsertToDoHisSQL4Validate(flInstance, flInstanceParms, keyValues, clientInfo) + ";";
                }

                if (flInstance.FLFlag == 'N')
                {
                    if (dbType == "4" || dbType == "6")
                    {
                        sqlOdbc = GetDeleteToDoListSQL(flInstance, flPath, clientInfo);
                        object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                        if (executeOdbc[0].ToString() == "1")
                        {
                            throw new FLException(executeOdbc[1].ToString());
                        }
                    }
                    else
                    {
                        sql += GetDeleteToDoListSQL(flInstance, flPath, clientInfo) + ";";
                    }
                    if (flInstanceParms != null && flInstanceParms.Length > 5)
                    {
                        flInstanceParms[4] = "No Pass";
                    }

                    if (dbType == "4" || dbType == "6")
                    {
                        sqlOdbc = GetInsertToDoListSQL(flInstance, flInstanceParms, keyValues, clientInfo, (IEventWaiting)flInstance.NextFLActivities[0]);
                        object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                        if (executeOdbc[0].ToString() == "1")
                        {
                            throw new FLException(executeOdbc[1].ToString());
                        }
                    }
                    else
                    {
                        sql += GetInsertToDoListSQL(flInstance, flInstanceParms, keyValues, clientInfo, (IEventWaiting)flInstance.NextFLActivities[0]) + ";";
                    }
                }
            }
            else
            {
                if (flInstance.FLFlag == 'X')
                {
                    if (dbType == "4" || dbType == "6")
                    {
                        sqlOdbc = GetInsertToDoHisSQL4Reject(flInstance, flInstanceParms, keyValues, clientInfo);
                        object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                        if (executeOdbc[0].ToString() == "1")
                        {
                            throw new FLException(executeOdbc[1].ToString());
                        }
                    }
                    else
                    {
                        sql = GetInsertToDoHisSQL4Reject(flInstance, flInstanceParms, keyValues, clientInfo) + ";";
                    }
                }
                else
                {
                    if (dbType == "4" || dbType == "6")
                    {
                        sqlOdbc = GetInsertToDoHisSQL(flInstance, flInstanceParms, keyValues, clientInfo);
                        object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                        if (executeOdbc[0].ToString() == "1")
                        {
                            throw new FLException(executeOdbc[1].ToString());
                        }
                    }
                    else
                    {
                        sql = GetInsertToDoHisSQL(flInstance, flInstanceParms, keyValues, clientInfo) + ";";
                    }
                }

                if (dbType == "4" || dbType == "6")
                {
                    sqlOdbc = GetDeleteToDoListSQL(flInstance, flPath, clientInfo);
                    object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                    if (executeOdbc[0].ToString() == "1")
                    {
                        throw new FLException(executeOdbc[1].ToString());
                    }
                }
                else
                {
                    sql += GetDeleteToDoListSQL(flInstance, flPath, clientInfo) + ";";
                }

                foreach (FLActivity activity in flInstance.NextFLActivities)
                {
                    string s = "";
                    if (activity is IEventWaiting)
                    {
                        s = GetInsertToDoListSQL(flInstance, flInstanceParms, keyValues, clientInfo, (IEventWaiting)activity);
                    }
                    else if (activity is IFLNotifyActivity)
                    {
                        string param = flInstanceParms[4].ToString();
                        flInstanceParms[4] = null;
                        s = GetInsertToDoListSQL4Notify(flInstance, (IEventWaiting)flInstance.CurrentFLActivity, (IFLNotifyActivity)activity, flInstanceParms, keyValues, clientInfo);
                        flInstanceParms[4] = param;
                    }
                    else if (activity is IFLProcedureActivity)
                    {
                        //CallServerMethod(flInstance, flInstanceParms, keyValues, clientInfo, (IFLProcedureActivity)activity);
                    }
                    else if (activity is IFLValidateActivity)
                    {
                        s = GetInsertToDoHisSQL4Validate(flInstance, flInstanceParms, keyValues, clientInfo);
                    }
                    if (s != "")
                    {
                        if (dbType == "4" || dbType == "6")
                        {
                            if (!s.EndsWith(";"))
                            {
                                sqlOdbc = s;
                            }
                            else
                            {
                                sqlOdbc = s;
                            }
                            object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                            if (executeOdbc[0].ToString() == "1")
                            {
                                throw new FLException(executeOdbc[1].ToString());
                            }
                        }
                        else
                        {
                            if (!s.EndsWith(";"))
                            {
                                sql += s + ";";
                            }
                            else
                            {
                                sql += s;
                            }
                        }
                    }
                }


                // agent notify
                #region Agent notify
                string sUserId = ((object[])clientInfo[0])[1].ToString();
                string flowDesc = ((IFLRootActivity)flInstance.RootFLActivity).Description;

                IEventWaiting currentFLActivity = flInstance.CurrentFLActivity as IEventWaiting;
                IEventWaiting currentFLActivity2 = null;
                if (currentFLActivity is IFLApproveBranchActivity)
                {
                    string parentName = ((IFLApproveBranchActivity)currentFLActivity).ParentActivity;
                    currentFLActivity2 = (IEventWaiting)flInstance.RootFLActivity.GetFLActivityByName(parentName);
                }
                else
                {
                    currentFLActivity2 = currentFLActivity;
                }

                string sSendToId = currentFLActivity == null ? string.Empty : currentFLActivity.SendToId;
                SendToKind sSendToKind = currentFLActivity2 == null ? SendToKind.Role : currentFLActivity2.SendToKind;
                if ((sSendToId != null && sSendToId != string.Empty) && sSendToKind != SendToKind.Applicate && sSendToKind != SendToKind.User && sSendToKind != SendToKind.RefUser)
                {
                    object parAgent = Global.GetPARAGENT(flowDesc, sUserId, clientInfo);
                    if (parAgent != null && Convert.ToBoolean(parAgent))
                    {
                        List<string> userRoleIds = Global.GetRoleIdsByUserId(sUserId, clientInfo);
                        if (!userRoleIds.Contains(sSendToId))
                        {
                            List<object> flInstanceParms2 = new List<object>(flInstanceParms);
                            if (flInstanceParms2.Count >= 9)
                            {
                                flInstanceParms2[8] = sSendToId;
                            }
                            else
                            {
                                int count = flInstanceParms2.Count;
                                for (int i = 0; i < 8 - count; i++)
                                {
                                    flInstanceParms2.Add(null);
                                }
                                flInstanceParms2.Add(sSendToId);
                            }

                            string s = GetInsertToDoListSQL4Notify(flInstance, currentFLActivity2, null, flInstanceParms2.ToArray(), keyValues, clientInfo);
                            if (!s.EndsWith(";"))
                            {
                                sql += s + ";";
                            }
                            else
                            {
                                sql += s;
                            }

                        }
                    }
                }

                #endregion

            }

            if (string.IsNullOrEmpty(sql) && string.IsNullOrEmpty(sqlOdbc))
            {
                return;
            }

            if (sql != "")
            {
                if (!sql.EndsWith(";"))
                {
                    sql += ";";
                }
            }

            if (dbType == "4" || dbType == "6")
            {
                sqlOdbc = GetUpdateToDoListSQL4Attachments(flInstance, flInstanceParms, clientInfo);
                object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
            }
            else
            {
                sql += GetUpdateToDoListSQL4Attachments(flInstance, flInstanceParms, clientInfo) + ";";
                if (dbType == "3" && sql.EndsWith(";"))  //Oracle中如果要多句语句一起执行，需在之前加上BEGIN，之后加上END;
                    sql = "BEGIN " + sql + "END;";
                object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
                if (objs[0].ToString() == "1")
                {
                    throw new FLException(objs[1].ToString());
                }
            }
        }

        //public static void InserToDoList41stFLActivity(FLInstance flInstance, object[] flInstanceParms, string flPath, object[] keyValues, object[] clientInfo)
        //{
        //    string sql = GetInsertToDoListSQL(flInstance, flInstanceParms, keyValues, clientInfo, (IEventWaiting)flInstance.NextFLActivities[0]) + ";";

        //    sql = "BEGIN TRAN;" + sql + "COMMIT TRAN;";

        //    EEPRemoteModule remoteModule = new EEPRemoteModule();

        //    object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
        //    if (objs[0].ToString() == "1")
        //    {
        //        throw new Exception(objs[1].ToString());
        //    }
        //}

        /// <summary>
        /// 删除ToDoList和ToDoHis
        /// </summary>
        /// <param name="instanceId">流程Id</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void DeleteToDo(Guid instanceId, object[] clientInfo)
        {
            string deleteSql = string.Format(DELETE_TODOLIST, instanceId.ToString());

            string sql = deleteSql;
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
        }

        /// <summary>
        /// 删除通知
        /// </summary>
        /// <param name="instanceId">流程Id</param>
        /// <param name="flPath">流程路径</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void DeleteToDoList4Notify(Guid instanceId, string flPath, string sendToId, object[] clientInfo)
        {
            string deleteSql = string.Format(DELETE_TODOLIST_2, instanceId.ToString(), flPath);
            if (!string.IsNullOrEmpty(sendToId))
            {
                deleteSql = string.Format(DELETE_TODOLIST_2_1, instanceId.ToString(), flPath, sendToId);
            }
            string sql = deleteSql;
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            String dbType = GetDBType(clientInfo);

            //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
        }

        // 要传入currentFLActivity，就是为了一个消息可以多次传递。
        /// <summary>
        /// 发送通知
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void InsertToDo4Notify(FLInstance flInstance, IEventWaiting currentFLActivity, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            string insertToDoListSQL4Notify = GetInsertToDoListSQL4Notify(flInstance, currentFLActivity, null, flInstanceParms, keyValues, clientInfo);

            string sql = insertToDoListSQL4Notify;
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            String dbType = GetDBType(clientInfo);
            if (dbType == "3" && sql.EndsWith(";"))
                sql = "BEGIN " + sql + "END;";

            //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);

            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
        }

        /// <summary>
        /// 取消通知
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="currentFLActivity">当前Activity</param>
        /// <param name="flNotifyActivity">通知Activity</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void InsertToDo4RejectNotify(FLInstance flInstance, IEventWaiting currentFLActivity, FLNotifyActivity flNotifyActivity, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            string insertToDoListSQL4Notify = GetInsertToDoListSQL4RejectNotify(flInstance, currentFLActivity, flNotifyActivity, flInstanceParms, keyValues, clientInfo);

            string sql = insertToDoListSQL4Notify;
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            String dbType = GetDBType(clientInfo);
            if (dbType == "3" && sql.EndsWith(";"))
                sql = "BEGIN " + sql + "END;";

            //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
            object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
            if (objs[0].ToString() == "1")
            {
                throw new FLException(objs[1].ToString());
            }
        }

        /// <summary>
        /// 加签
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <summary>
        public static void InsertToDo4PlusApprove(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            string roleId = flInstanceParms[5].ToString();
            string kind = "1";
            if (string.IsNullOrEmpty(roleId))
            {
                //如果角色为空代表用user回复的加签
                roleId = ((object[])clientInfo[0])[1].ToString();
                kind = "2";
            }

            EEPRemoteModule remoteModule = new EEPRemoteModule();
            var plus = false;
            object[] objp = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { "SELECT * FROM SYS_TODOHIS WHERE 1=0" });
            if (objp[0].ToString() == "0")
            {
                DataSet dataSet = (DataSet)objp[1];
                if (dataSet.Tables[0].Columns.Contains("PLUSROLES"))
                {
                    plus = true;
                }
            }

            string deleteToDoListSql4PlusReturn = string.Format(DELETE_TODOLIST_4, flInstance.FLInstanceId.ToString(), roleId, kind);//删除自己的
            AddSemicolonToSQL(ref deleteToDoListSql4PlusReturn);
            string insertToDoListSQL4PlusApprove = GetInsertToDoListSQL4PlusApprove(flInstance, flInstanceParms, keyValues, clientInfo);
            AddSemicolonToSQL(ref insertToDoListSQL4PlusApprove);
            string insertToDoHisSQL4PlusApprove = GetInsertToDoHisSQL4PlusApprove(flInstance, flInstanceParms, keyValues, clientInfo, plus);
            AddSemicolonToSQL(ref insertToDoHisSQL4PlusApprove);
            string updateToDoListSQL4PlusApprove = GetUpdateToDoListSQL4PlusApprove(flInstance, flInstanceParms, clientInfo);
            AddSemicolonToSQL(ref updateToDoListSQL4PlusApprove);

            string sql = deleteToDoListSql4PlusReturn + insertToDoListSQL4PlusApprove + insertToDoHisSQL4PlusApprove + updateToDoListSQL4PlusApprove;
          

            String dbAlias = (clientInfo[0] as object[])[2].ToString();
            String dbType = String.Empty;
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            if (xx[0].ToString() == "0")
                dbAlias = xx[1].ToString();
            object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { dbAlias });
            if (myRet != null && myRet[0].ToString() == "0")
                dbType = myRet[1].ToString();
            String sqlOdbc = String.Empty;

            if (dbType == "4" || dbType == "6")
            {
                sqlOdbc = insertToDoListSQL4PlusApprove;
                object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = insertToDoHisSQL4PlusApprove;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = updateToDoListSQL4PlusApprove;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
            }
            else
            {
                //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
                if (dbType == "3" && sql.EndsWith(";"))
                    sql = "BEGIN " + sql + "END;";
                object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
                if (objs[0].ToString() == "1")
                {
                    throw new FLException(objs[1].ToString());
                }
            }
        }

        private static void AddSemicolonToSQL(ref String strSql)
        {
            if (!strSql.EndsWith(";"))
                strSql = strSql + ";";
        }

        /// <summary>
        /// 加签返回
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void InsertToDo4PlusReturn(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            String dbAlias = (clientInfo[0] as object[])[2].ToString();
            String dbType = String.Empty;
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            if (xx[0].ToString() == "0")
                dbAlias = xx[1].ToString();
            object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { dbAlias });
            if (myRet != null && myRet[0].ToString() == "0")
                dbType = myRet[1].ToString();
            String sqlOdbc = String.Empty;

            string roleId = flInstanceParms[5].ToString();
            string kind = "1";
            if (string.IsNullOrEmpty(roleId))
            {
                //如果角色为空代表用user回复的加签
                roleId = ((object[])clientInfo[0])[1].ToString();
                kind = "2";
            }

            string selectToDoListSql4PlusReturn = string.Format(SELECT_TODOLIST_4, flInstance.FLInstanceId.ToString(), roleId, kind);
            object[] objs0 = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { selectToDoListSql4PlusReturn });
            if (objs0[0].ToString() == "1")
            {
                throw new FLException(objs0[1].ToString());
            }

            DataSet ds = (DataSet)objs0[1];
            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                String message = string.Format(SysMsg.GetSystemMessage((SYS_LANGUAGE)(((object[])(clientInfo[0]))[0]), "FLRuntime", "Logic", "CannotPlusReturn"), roleId);
                throw new FLException(2, message);
            }

            string deleteToDoListSql4PlusReturn = string.Format(DELETE_TODOLIST_4, flInstance.FLInstanceId.ToString(), roleId, kind);
            AddSemicolonToSQL(ref deleteToDoListSql4PlusReturn);
            string insertToDoHisSQL4PlusReturn = GetInsertToDoHisSQL4PlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);
            AddSemicolonToSQL(ref insertToDoHisSQL4PlusReturn);
            string updateToDoListSQL4PlusReturn = GetUpdateToDoListSQL4PlusReturn(flInstance, flInstanceParms, clientInfo);
            AddSemicolonToSQL(ref updateToDoListSQL4PlusReturn);
            string updateToDoListSQL4Attachments = GetUpdateToDoListSQL4Attachments(flInstance, flInstanceParms, clientInfo);
            AddSemicolonToSQL(ref updateToDoListSQL4Attachments);
            if (dbType == "4" || dbType == "6")
            {
                sqlOdbc = deleteToDoListSql4PlusReturn;
                object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = insertToDoHisSQL4PlusReturn;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = updateToDoListSQL4PlusReturn;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = updateToDoListSQL4Attachments;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
            }
            else
            {
                string sql = deleteToDoListSql4PlusReturn + insertToDoHisSQL4PlusReturn + updateToDoListSQL4PlusReturn + updateToDoListSQL4Attachments;

                if (dbType == "3" && sql.EndsWith(";"))
                    sql = "BEGIN " + sql + "END;";
                //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
                object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
                if (objs[0].ToString() == "1")
                {
                    throw new FLException(objs[1].ToString());
                }
            }
        }



        /// <summary>
        /// 加签返回
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        public static void InsertToDo4PlusReturn2(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            String dbAlias = (clientInfo[0] as object[])[2].ToString();
            String dbType = String.Empty;
            object[] xx = remoteModule.CallMethod(clientInfo, "GLModule", "GetSplitSysDB2", new object[] { dbAlias });
            if (xx[0].ToString() == "0")
                dbAlias = xx[1].ToString();
            object[] myRet = remoteModule.CallMethod(clientInfo, "GLModule", "GetDataBaseType", new object[] { dbAlias });
            if (myRet != null && myRet[0].ToString() == "0")
                dbType = myRet[1].ToString();
            String sqlOdbc = String.Empty;

            //string roleId = flInstanceParms[5].ToString();

            string deleteToDoListSql4PlusReturn = string.Format(DELETE_TODOLIST_5, flInstance.FLInstanceId.ToString());
            AddSemicolonToSQL(ref deleteToDoListSql4PlusReturn);
            string insertToDoHisSQL4PlusReturn = GetInsertToDoHisSQL4PlusReturn(flInstance, flInstanceParms, keyValues, clientInfo);
            AddSemicolonToSQL(ref insertToDoHisSQL4PlusReturn);

            //加签返回不变
            string step = flInstance.CurrentFLActivity is IFLApproveBranchActivity
                ? (flInstance.CurrentFLActivity as IFLApproveBranchActivity).ParentActivity : flInstance.CurrentFLActivity.Name;//FLApprove中使用加签返回的问题

            string updateToDoListSQL4PlusReturn = string.Format(UPDATE_TODOLIST, "", flInstance.FLInstanceId.ToString(), step);
            AddSemicolonToSQL(ref updateToDoListSQL4PlusReturn);
            string updateToDoListSQL4Attachments = GetUpdateToDoListSQL4Attachments(flInstance, flInstanceParms, clientInfo);
            AddSemicolonToSQL(ref updateToDoListSQL4Attachments);

            if (dbType == "4" || dbType == "6")
            {
                sqlOdbc = deleteToDoListSql4PlusReturn;
                object[] executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = insertToDoHisSQL4PlusReturn;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = updateToDoListSQL4PlusReturn;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
                sqlOdbc = updateToDoListSQL4Attachments;
                executeOdbc = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sqlOdbc });
                if (executeOdbc[0].ToString() == "1")
                {
                    throw new FLException(executeOdbc[1].ToString());
                }
            }
            else
            {
                string sql = deleteToDoListSql4PlusReturn + insertToDoHisSQL4PlusReturn + updateToDoListSQL4PlusReturn + updateToDoListSQL4Attachments;

                if (dbType == "3" && sql.EndsWith(";"))
                    sql = "BEGIN " + sql + "END;";
                //object[] objs = remoteModule.ExecuteSql(clientInfo, "GLModule", "cmdWorkflow", sql, false);
                object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "UpdateWorkFlow", new object[] { sql });
                if (objs[0].ToString() == "1")
                {
                    throw new FLException(objs[1].ToString());
                }
            }
        }


        /// <summary>
        /// CallServerMethod
        /// </summary>
        /// <param name="flInstance">流程</param>
        /// <param name="flInstanceParms">流程参数</param>
        /// <param name="keyValues">宿主表的筛选条件</param>
        /// <param name="clientInfo">ClientInfo</param>
        /// <param name="flProcedureActivity">调用ServerMethod的Activity</param>
        public static void CallServerMethod(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo, IFLProcedureActivity flProcedureActivity)
        {
            EEPRemoteModule remoteModule = new EEPRemoteModule();

            string moduleName = flProcedureActivity.ModuleName;
            string methodName = flProcedureActivity.MethodName;

            DataSet hostDataSet = HostTable.GetHostDataSet(flInstance, keyValues, clientInfo);
            DataRow row = (hostDataSet == null || hostDataSet.Tables.Count == 0 || hostDataSet.Tables[0].Rows.Count == 0) ? null : hostDataSet.Tables[0].Rows[0];
            string roleId = flInstanceParms[5].ToString();

            object[] parameters = new object[] { row, (int)flInstance.FLDirection, roleId, clientInfo };
            object[] ClientInfo = clientInfo[0] as object[];
            object[] objs = null;
            if (ClientInfo.Length > 17 && ClientInfo[17] != null)
                objs = CallSDMethod(moduleName, methodName, parameters, clientInfo) as object[];
            else
                objs = remoteModule.CallMethod(clientInfo, moduleName, methodName, parameters);
            if (objs[0].ToString() == "1")
            {
                // 先做Error的处理。
                if (flProcedureActivity.ErrorLog && flProcedureActivity.ErrorToRole != null)
                {
                    IEventWaiting currentFLActivity = (IEventWaiting)flInstance.CurrentFLActivity;
                    List<object> list = new List<object>(flInstanceParms);
                    list.Add(flProcedureActivity.ErrorToRole);
                    list[4] = flProcedureActivity.Name + " has error";
                    InsertToDo4Notify(flInstance, currentFLActivity, list.Select(c=> MarkValue(c)).ToArray(), keyValues, clientInfo);
                }

                throw new FLException(objs[1].ToString());
            }
        }

        private static object CreateModuleInstance(string assemblyName, object[] clientInfo)
        {
            object[] ClientInfo = clientInfo[0] as object[];
            EEPRemoteModule remoteModule = new EEPRemoteModule();
            string sql = String.Format("SELECT * FROM SYS_WEBPAGES WHERE PageType='{0}' and PageName='{1}' and UserID='{2}' and SolutionID='{3}'", "S", assemblyName, ClientInfo[17], ClientInfo[6]);
            //object[] objs = remoteModule.CallMethod(clientInfo, "GLModule", "ExcuteWorkFlow", new object[] { sql });
            string dbName = Srvtools.DbConnectionSet.GetSystemDatabase(null);
            using (IDbConnection conn = Srvtools.DbConnectionSet.GetDbConn(dbName).CreateConnection())
            {
                IDbCommand comm = conn.CreateCommand();
                comm.CommandText = sql;
                if (conn.State != ConnectionState.Open) conn.Open();
                IDataReader idr = comm.ExecuteReader();
                if (!idr.Read())
                {
                    throw new Exception(string.Format("Package: {0} not active", assemblyName));
                }
                var content = (byte[])idr["SERVERDLL"];
                var assembly = System.Reflection.Assembly.Load(content);
                var componentType = assembly.GetType(string.Format("{0}.Component", assemblyName));
                return Activator.CreateInstance(componentType);
            }
        }

        /// <summary>
        /// Excute server method
        /// </summary>
        /// <param name="assemblyName">Name of assembly</param>
        /// <param name="methodName">Name of method</param>
        /// <param name="parameters">Parameters of method</param>
        /// <returns>Result of excuting server method</returns>
        public static object CallSDMethod(string assemblyName, string methodName, object[] parameters, object[] ClientInfo)
        {
            if (string.IsNullOrEmpty(assemblyName))
            {
                throw new ArgumentNullException("assemblyName");
            }
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException("methodName");
            }
            var dataModule = CreateModuleInstance(assemblyName, ClientInfo) as Srvtools.DataModule;
            dataModule.SetClientInfo(ClientInfo);
            dataModule.SetOwnerComponent();
            var method = dataModule.GetType().GetMethod(methodName);
            if (method != null)
            {
                return method.Invoke(dataModule, new object[] { parameters });
            }
            else
            {
                return null;
            }
        }

        private static string GetDUSERNAME(FLInstance flInstance, object[] flInstanceParms, object[] keyValues, object[] clientInfo)
        {
            List<string> duserNames = new List<string>();
            List<FLActivity> nextFLActivities = flInstance.NextFLActivities;
            List<string> roleIds = new List<string>();
            List<string> userIds = new List<string>();
            string orgKind = ((IFLRootActivity)flInstance.RootFLActivity).OrgKind;
            string tableName = ((IFLRootActivity)flInstance.RootFLActivity).TableName;
            foreach (var flActivity in nextFLActivities)
            {
                if (flActivity is IEventWaiting)
                {
                    SendToKind sk = ((IEventWaiting)flActivity).SendToKind;
                    string sr = ((IEventWaiting)flActivity).SendToRole;
                    string su = ((IEventWaiting)flActivity).SendToUser;
                    string sf = ((IEventWaiting)flActivity).SendToField;
                    #region getroles
                    if (flInstance.IsPlusApprove)
                    {
                        string q = flInstanceParms[8].ToString();
                        string[] qq = q.Split(";".ToCharArray());
                        foreach (string r in qq)
                        {
                            if (!string.IsNullOrEmpty(r))
                            {
                                if (r.StartsWith("U:"))
                                {
                                    userIds.Add(r.Substring(2));
                                }
                                else
                                {
                                    roleIds.Add(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (sk == SendToKind.Applicate)
                        {
                            userIds.Add(flInstance.Creator);
                        }
                        else if (sk == SendToKind.Role)
                        {
                            string q = sr;
                            string[] qq = q.Split(";".ToCharArray());

                            roleIds.Add(qq[0].Trim());
                        }
                        else if (sk == SendToKind.ApplicateManager)
                        {
                            if (flActivity is IFLApproveBranchActivity && !string.IsNullOrEmpty(flInstance.R))
                            {
                                roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(flInstance.CreateRole))
                                {
                                    roleIds.Add(Global.GetManagerRoleId(flInstance.CreateRole, orgKind, clientInfo));
                                }
                                else
                                {
                                    List<string> roles = Global.GetRoleIdsByUserId(flInstance.Creator, clientInfo);
                                    if (roles.Count > 0)
                                    {
                                        roleIds.Add(Global.GetManagerRoleId(roles[0], orgKind, clientInfo));
                                    }
                                }
                            }
                        }
                        else if (sk == SendToKind.Manager)
                        {
                            if (flInstance.FLDirection == FLDirection.GoToBack)
                            {
                                roleIds.Add(((IEventWaitingExecute)flActivity).RoleId);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(flInstance.R))
                                {
                                    roleIds.Add(Global.GetManagerRoleId(flInstanceParms[5].ToString(), orgKind, clientInfo));
                                }
                                else
                                {
                                    roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                                }
                            }
                        }
                        else if (sk == SendToKind.RefRole)
                        {
                            if (flActivity is FLStandActivity && ((ISupportFLDetailsActivity)flActivity).SendToId2 != string.Empty)
                            {
                                roleIds.Add(((ISupportFLDetailsActivity)flActivity).SendToId2);
                            }
                            else
                            {
                                roleIds.Add(Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo));
                            }
                        }
                        else if (sk == SendToKind.RefManager)
                        {
                            if (flInstance.FLDirection == FLDirection.GoToBack)
                            {
                                roleIds.Add(((IEventWaitingExecute)flActivity).RoleId);
                            }
                            else
                            {
                                if (flActivity is IFLApproveBranchActivity && !string.IsNullOrEmpty(flInstance.R))
                                {
                                    roleIds.Add(Global.GetManagerRoleId(flInstance.R, orgKind, clientInfo));
                                }
                                else
                                {
                                    string sendToField = sf;
                                    string values = keyValues[1].ToString();

                                    string s = Global.GetRoleIdByRefRole(flInstance, sendToField, tableName, values, clientInfo);
                                    roleIds.Add(Global.GetManagerRoleId(s.ToString(), orgKind, clientInfo));
                                }
                            }
                        }
                        else if (sk == SendToKind.RefUser)
                        {
                            string id = Global.GetRoleIdByRefRole(flInstance, sf, tableName, keyValues[1].ToString(), clientInfo, true);

                            if (!string.IsNullOrEmpty(id))
                            {
                                string[] listusers = id.Split(';');
                                foreach (string user in listusers)
                                {
                                    if (user.Trim().Length > 0)
                                    {
                                        userIds.Add(user);
                                    }
                                }
                            }
                        }
                        else if (sk == SendToKind.User)
                        {
                            string[] listusers = su.Split(';');
                            foreach (string user in listusers)
                            {
                                if (user.Trim().Length > 0)
                                {
                                    userIds.Add(user);
                                }
                            }
                        }
                        else
                        {
                            roleIds.Add(flInstanceParms[5].ToString());
                        }
                    }
                    #endregion
                }
            }
            List<string> tempIds = new List<string>();
            foreach (string r in roleIds)
            {
                string[] rr = r.Split(":".ToCharArray());
                if (rr.Length > 1)
                {
                    tempIds.Add(rr[1]);
                }

                List<string> userofrole = Global.GetUsersIdsByRoleId(r, clientInfo);
                if (userofrole.Count > 0)
                {
                    tempIds.AddRange(userofrole);
                }
            }
            var dusers = new List<string>();
            foreach (string u in tempIds)
            {
                if (!dusers.Contains(u))
                {
                    dusers.Add(u);
                }
            }
            foreach (string u in userIds)
            {
                if (!dusers.Contains(u))
                {
                    dusers.Add(u);
                }
            }
            foreach (string u in dusers)
            {
                duserNames.Add(string.Format("{0}({1})", u, Global.GetUserName(u, clientInfo)));
            }

            return string.Join(";", duserNames);
        }
    }
}