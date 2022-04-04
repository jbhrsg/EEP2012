using Srvtools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JBTool
{
    public class IndentityLogInfoPatch_Event
    {
        public delegate void SetDataRowHandler(DataRow aRow);
    }

    public class IndentityLogInfoPatch : IndentityLogInfoPatch_Event
    {
        public UpdateComponent UpdateComponent { get; set; }

        public LogInfo LogInfo { get; set; }

        public SetDataRowHandler EventSetRowData;

        public IndentityLogInfoPatch(UpdateComponent aUpdateComponent, LogInfo aLogInfo)
        {
            UpdateComponent = aUpdateComponent;
            LogInfo = aLogInfo;
            EventSetRowData = null;
        }       

        public void Execute()
        {                      
            var dataset = (DataSet)UpdateComponent.GetType().GetField("_updateDataSet", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(UpdateComponent);
            var table = (string)UpdateComponent.GetType().GetField("_updateTable", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(UpdateComponent);
            DataTable dt = UpdateComponent.GetSchema();            

            foreach (DataRow aRow in dataset.Tables[table].Rows)
            {
                if (aRow.RowState == DataRowState.Unchanged) continue;
                if (EventSetRowData != null && aRow.RowState == DataRowState.Added) EventSetRowData(aRow);
                LogInfo.Log(aRow, dt, UpdateComponent.conn, UpdateComponent.trans, UpdateComponent.SelectCmd.KeyFields);
            }            
        }

        /// <summary> UpdateComponent寫入LogInfo修正</summary>
        /// <param name="aUpdateComponent">UpdateComponent</param>
        /// <param name="aLogInfo">LogInfo</param>
        /// <param name="KeyFieldName">IndentityFieldName</param>
        /// <summary>
        /// 此功能『只能』用在單一新增時的狀況下，即當有Master-Detail狀況下並不適用。
        /// 因為Detail一次性將資料寫入後才會輪到AfterInsert事件，此方法主要利用SCOPE_IDENTITY()來運作，
        /// 當資料一次性進來的時候，SCOPE_IDENTITY()只能取得最後的資料，故在新增兩筆以上的資料時會產生錯誤，
        /// 設定的PK值重複的問題。
        /// </summary>
        public static void Execute(UpdateComponent aUpdateComponent, LogInfo aLogInfo, string KeyFieldName)
        {
            aUpdateComponent.AfterInsert += delegate(object sender, UpdateComponentAfterInsertEventArgs e)
            {
                var command = aUpdateComponent.conn.CreateCommand();
                command.CommandText = "SELECT SCOPE_IDENTITY();";
                command.Transaction = aUpdateComponent.trans;
                int newID = Convert.ToInt32(command.ExecuteScalar());

                IndentityLogInfoPatch aPatch = new IndentityLogInfoPatch(aUpdateComponent, aLogInfo);
                aPatch.EventSetRowData += delegate(DataRow aRow) {
                    aRow[KeyFieldName] = newID; 
                };
                aPatch.Execute();
            };

            aUpdateComponent.AfterModify += delegate(object sender, UpdateComponentAfterModifyEventArgs e)
            {
                new IndentityLogInfoPatch(aUpdateComponent, aLogInfo).Execute();                
            };

            aUpdateComponent.AfterDelete += delegate(object sender, UpdateComponentAfterDeleteEventArgs e)
            {
                new IndentityLogInfoPatch(aUpdateComponent, aLogInfo).Execute();
            };
        }

        /// <summary> UpdateComponent新增前先將FieldAttr(只有系統提供的)塞入</summary> 
        /// <example>_username就可以用，而自訂的就不行</example>
        public static void SysFieldAttr(UpdateComponent aUpdateComponent)
        {
            aUpdateComponent.BeforeInsert += delegate(object sender, UpdateComponentBeforeInsertEventArgs e)
            {
                foreach (FieldAttr aFieldAttr in aUpdateComponent.FieldAttrs)
                {
                    if (aFieldAttr.UpdateEnable && 
                        (aFieldAttr.DefaultMode == DefaultModeType.Insert || aFieldAttr.DefaultMode == DefaultModeType.InsertAndUpdate) &&
                        !string.IsNullOrEmpty(aFieldAttr.DefaultValue))
                    {
                        object[] sysResultt = SrvUtils.GetValue(aFieldAttr.DefaultValue, (DataModule)aUpdateComponent.OwnerComp);
                        if (sysResultt != null && (int)sysResultt[0] == 0) aUpdateComponent.SetFieldValue(aFieldAttr.DataField, sysResultt[1]);
                    }
                }

            };
        }
    }
}
