<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_AttendOverTimeFlow.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="../js/jquery.jbjob.js"></script>
    <title></title>
     <script>
         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
             //明細Hours加總至Grid欄位
             $("#dataGridDetail").datagrid({
                 onAfterEdit: function (rowIndex, rowData, changes) {
                     rowData.TotalHours = parseFloat(rowData.OverTimeHours) + parseFloat(rowData.RestHours);
                     $(this).datagrid('refreshRow', rowIndex);
                 }
             });
         });
         
         function OnLoadFormMaster() {
             var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
             var CreateDate = $('#dataFormMasterCreateDate').datebox('getValue');
             if (EmployeeID != "") {

                 //取得申請時的部門名稱,班別
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                     data: "mode=method&method=" + "getDeptInfo" + "&parameters=" + EmployeeID + "," + CreateDate,
                     cache: false,
                     async: false,
                     success: function (data) {
                         var rows = $.parseJSON(data);
                         if (rows.length > 0) {
                             $('#dataFormMasterOverTimeDeptID').val(rows[0].DEPT_ID);
                             $('#dataFormMasterDEPT_Name').val(rows[0].DEPT_CNAME);
                             $('#dataFormMasterOverTimeRoteID').val(rows[0].ROTE_ID);
                             $('#dataFormMasterROTE_Name').val(rows[0].ROTE_CNAME);
                         }
                     }
                 });
             }             
         }
         //check 時間格式如 : 0800 或 0830
         function checkTimeFormat(val) {
             return $.jbIsTimeFormat(val);            
         }
         //check 加班日期=>卡一個月,5/14 <= 4/14 都不行
         function checkTimeDate(val) {
             var dt = new Date();
             var aDate = new Date($.jbDateAdd('months', -1, dt));//小一個月
             var bDate = $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd')
             if (val <= bDate) return false;
             else return true;
         }

         function dataFormMasterOnApply() {
             var data = $("#dataGridDetail").datagrid('getData');
             if (data.total == 0) {             
                 alert('注意!!,未新增加班紀錄,無法存檔!');
                 return false;
             }
             //明細Hours加總至Grid欄位
            endEdit($('#dataGridDetail'));
         }
         //加總至Master欄位。DataGridDetail Amount欄位Total屬性設定sum、OnTotal屬性定義此方法
         function HoursTotal(rowData) {
             if (getEditMode($("#dataFormMaster")) == 'inserted') {                 
                 //Grid加總時數
                 var datagrid = $('#dataGridDetail');
                 var rows = datagrid.datagrid('getRows');
                 var result = 0;
                 for (var i = 0; i < rows.length; i++) {
                     var RestHours = rows[i].RestHours;
                     var OverTimeHours = rows[i].OverTimeHours;
                     if (RestHours != undefined && OverTimeHours != undefined && RestHours != "" && OverTimeHours != "") {
                         value = parseFloat(RestHours) + parseFloat(OverTimeHours);

                         if (!isNaN(value)) {
                             result = eval(result + value);
                         }
                     }
                 }
                 $('#dataFormMasterMasterTotalHours').numberbox('setValue', result);
                 return result;
             }
         }        
         
         //存檔前        
         //1. 判斷加班起始日期不可大於截止日期
         //2. 判斷加班時數        
         //3. 判斷加班資料(EMPLOYEE_ID/OVERTIME_DATE_TIME_BEGIN/OVERTIME_DATE_TIME_END)申請的時段內是否已有存在的加班資料
         function checkOvertimeData() {            

             //1. 判斷加班起始時間不可大於截止時間
             var beginTime = $("#dataFormDetailBeginTime").val();
             var endTime = $("#dataFormDetailEndTime").val();
             if (parseInt(beginTime) >= parseInt(endTime)) {
                 alert('加班起始時間 : ' + beginTime + ' 需小於加班截止時間 : ' + endTime);
                 return false;
             }

             if (getEditMode($("#dataFormMaster")) == 'updated')
                 OverTimeNO = $('#dataFormMasterOverTimeNO').val();
             else
                 OverTimeNO = "0";

             var EmployeeID = $("#dataFormMasterEmployeeID").refval('getValue');
             var overtimeDate = $('#dataFormDetailOverTimeDate').datebox('getValue');
             var beginTime = $('#dataFormDetailBeginTime').val();
             var endTime = $('#dataFormDetailEndTime').val();
             var restHours = $('#dataFormDetailRestHours').val();

             //2. 判斷加班時數
             var rows;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                 data: "mode=method&method=" + "checkOvertimeHours" + "&parameters=" + OverTimeNO + "," + EmployeeID + "," + overtimeDate + "," + beginTime + "," + endTime,
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false)
                         rows = $.parseJSON(data);
                 }
             });
             if (rows.length > 0) {
                 if (rows[0].rejectCode != "") {
                     switch (rows[0].rejectCode) {                       
                         case "4": alert("申請日期查無出勤資料"); break;
                         case "5": alert("加班起始時間未在合理時間範圍內"); $('#dataFormDetailBeginTime').focus(); break;
                         case "6": alert("加班截止時間未在合理時間範圍內"); $('#dataFormDetailEndTime').focus(); break;
                     }
                     return false;
                 }
                 else {
                     if (rows[0].hours == 0) {
                         alert("申請的時段為上班時間");
                         $('#dataFormDetailOverTimeHours').numberbox('setValue', rows[0].hours);
                         return false;
                     }
                     if (rows[0].hours != parseFloat($('#dataFormDetailOverTimeHours').numberbox('getValue')) + parseFloat($('#dataFormDetailRestHours').numberbox('getValue'))) {
                         alert("加班總時數不正確(加班總時數" + rows[0].hours + "小時)");
                         $('#dataFormDetailOverTimeHours').focus();
                         //$('#dataFormMasterOVERTIME_HOURS').numberbox('setValue', rows[0].hours);
                         return false;
                     }
                 }
             }
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sHRMAttendOverTime.HRMAttendOverTimeApplyMaster', //連接的Server端，command
                 data: "mode=method&method=" + "checkOvertimeData" + "&parameters=" + OverTimeNO + "," + EmployeeID + "," + overtimeDate + "," + beginTime + "," + endTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = $.parseJSON(data);
                     }
                 }
             });
             if (cnt != "0" && cnt != "undefined") {
                 alert("申請的時段內已有存在加班資料！");
                 return false;
             }
             //4. 加班時數及補休時數只能擇一申請
             //var overtimeHours = $("#dataFormDetailOverTimeHours").numberbox('getValue');
             //var restHours = $("#dataFormDetailRestHours").numberbox('getValue');
             //if (parseInt(overtimeHours) > 0 && parseInt(restHours) > 0) {
             //    alert("加班時數及補休時數只能擇一申請");
             //    return false;
             //}
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" runat="server" AutoApply="True"
                DataMember="HRMAttendOverTimeApplyMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="HRM_AttendOverTime" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeNO" Editor="numberbox" FieldName="OverTimeNO" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeDeptID" Editor="numberbox" FieldName="OverTimeDeptID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="OverTimeRoteID" Editor="numberbox" FieldName="OverTimeRoteID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TotalHours" Editor="numberbox" FieldName="TotalHours" Format="" Visible="true" Width="120" EditorOptions="precision:1" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="加班單" DialogLeft="50px" DialogTop="30px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HRMAttendOverTimeApplyMaster" HorizontalColumnsCount="2" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadFormMaster" OnApply="dataFormMasterOnApply" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="加班單號" Editor="text" FieldName="OverTimeNO" Format="" Width="180" ReadOnly="True" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請姓名" Editor="inforefval" FieldName="EmployeeID" Format="" Width="90" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sHRMAttendOverTime.infoHRM_BASE_BASE',tableName:'infoHRM_BASE_BASE',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_ID',textField:'NAME_C',valueFieldCaption:'EMPLOYEE_ID',textFieldCaption:'NAME_C',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none'" OnBlur="" NewRow="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="CreateDate" Format="" ReadOnly="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="DEPT_Name" NewRow="True" ReadOnly="True" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="班別名稱" Editor="text" FieldName="ROTE_Name" Width="130" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDeptID" Editor="numberbox" FieldName="OverTimeDeptID" Format="" Width="130" ReadOnly="True" MaxLength="0" NewRow="True" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OverTimeRoteID" Editor="numberbox" FieldName="OverTimeRoteID" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加班備註" Editor="textarea" FieldName="Memo" MaxLength="125" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="450" EditorOptions="height:50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總時數" Editor="numberbox" EditorOptions="precision:1" FieldName="MasterTotalHours" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建檔日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="HRMAttendOverTimeApplyDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" Title="加班紀錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OverTimeDate" Format="" Width="70" />
                        <JQTools:JQGridColumn Alignment="center" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="52" />
                        <JQTools:JQGridColumn Alignment="center" Caption="終止時間" Editor="text" FieldName="EndTime" Format="" Width="52" />
                        <JQTools:JQGridColumn Alignment="right" Caption="加班時數" Editor="numberbox" FieldName="OverTimeHours" Format="" Width="52" EditorOptions="precision:1" />
                        <JQTools:JQGridColumn Alignment="right" Caption="補休時數" Editor="numberbox" FieldName="RestHours" Format="" Width="52" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" EditorOptions="precision:1" />
                        <JQTools:JQGridColumn Alignment="right" Caption="合計時數" Editor="numberbox" FieldName="TotalHours" OnTotal="HoursTotal" Total="sum" Visible="True" Width="52" EditorOptions="precision:1">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="加班原因" Editor="infocombobox" FieldName="OverTimeCauseID" Format="" Width="140" EditorOptions="valueField:'OVERTIME_CAUSE_ID',textField:'OVERTIME_CAUSE_CNAME',remoteName:'sHRMAttendOverTime.infoHRM_ATTEND_OVERTIME_CAUSE',tableName:'infoHRM_ATTEND_OVERTIME_CAUSE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeBegin" Editor="datebox" FieldName="OverTimeDateTimeBegin" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="OverTimeDateTimeEnd" Editor="datebox" FieldName="OverTimeDateTimeEnd" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="新增加班資料">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="HRMAttendOverTimeApplyDetails" HorizontalColumnsCount="2" RemoteName="sHRMAttendOverTime.HRMAttendOverTimeApplyMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="checkOvertimeData" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeNO" Editor="text" FieldName="OverTimeNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班日期" Editor="datebox" FieldName="OverTimeDate" Format="" Width="100" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="120" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="終止時間" Editor="text" FieldName="EndTime" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班時數" Editor="numberbox" FieldName="OverTimeHours" Format="" Width="120" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="補休時數" Editor="numberbox" FieldName="RestHours" Format="" Width="120" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="加班原因" Editor="inforefval" FieldName="OverTimeCauseID" Format="" Width="180" EditorOptions="title:'加班原因',panelWidth:350,remoteName:'sHRMAttendOverTime.infoHRM_ATTEND_OVERTIME_CAUSE',tableName:'infoHRM_ATTEND_OVERTIME_CAUSE',columns:[{field:'OVERTIME_CAUSE_CODE',title:'加班原因代碼',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'OVERTIME_CAUSE_CNAME',title:'加班原因中文',width:230,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'OVERTIME_CAUSE_ID',textField:'OVERTIME_CAUSE_CNAME',valueFieldCaption:'加班原因代碼',textFieldCaption:'加班原因中文',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none'" maxlength="0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDateTimeBegin" Editor="datebox" FieldName="OverTimeDateTimeBegin" Format="" MaxLength="0" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="OverTimeDateTimeEnd" Editor="datebox" FieldName="OverTimeDateTimeEnd" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OverTimeNO" ParentFieldName="OverTimeNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="自動編號" FieldName="OverTimeNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="EmployeeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OverTimeDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OverTimeHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RestHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OverTimeDate" RemoteMethod="False" ValidateMessage="加班日期不可小於一個月前" ValidateType="None" CheckMethod="checkTimeDate" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BeginTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemSeq" NumDig="2" />
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
