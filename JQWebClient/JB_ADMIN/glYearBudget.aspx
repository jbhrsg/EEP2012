<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glYearBudget.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         var DetailGridTitle = '';
         $(document).ready(function () {
             $(function () {
                 $("input, select, textarea").focus(function () {
                     $(this).css("background-color", "yellow");
                 });
                 $("input, select, textarea").blur(function () {
                     $(this).css("background-color", "white");
                 });
       
             });
              $(function () {
                 $('#CostCenterID_Query').combobox({
                 }
                 ).combo('textbox').blur(function () {
                     QCostCenterOnSelect($('#CostCenterID_Query').combobox('getSelectItem'));
                 });
              });
             //合計列背景淺灰
              $("#dataGridView").datagrid('options').rowStyler = function (index, row) {
                  if (row.RecordType == 2) {
                      return 'background-color:#D3D3D3;color:blue;font-weight:bold;';
                  }
              }
          }
         );
         var BookedAmt_FormatScript = function (value, row, index) {
             if (value > 0) {
                 var fieldName = this.field;
                 return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "BookedAmt_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
             }
         }
         var BookedAmt_CommandTrigger = function (command) {
             var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
             var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
             var Acno_S = rowData.Acno_S + rowData.SubAcno_S;
             var Acno_E = rowData.Acno_E + rowData.SubAcno_E;
             var CostCenterID = rowData.CostCenterID;
             var BudgetYear = $('#VoucherYear_Query').combobox('getValue');
             var EndDate = $("#EndDate_Query").datebox('getValue');
             DetailGridTitle = ' 成本中心:'+rowData.CostCenterName + ' 會計科目:' + Acno_S + '-' + rowData.AcnoName + ' ' + '入帳明細'
             GetGridDataBookedAmt(BudgetYear, EndDate, CostCenterID, Acno_S, Acno_E);
             return true;
         }
         function GetGridDataBookedAmt(BudgetYear, EndDate, CostCenterID, Acno_S,Acno_E) {
             var FiltStr = '';
             FiltStr = "VoucherYear = '" + BudgetYear + "'";
             FiltStr = FiltStr + " and VoucherDate <='" + EndDate + "'";
             FiltStr = FiltStr + " and CostCenterID = '" + CostCenterID + "'";
             FiltStr = FiltStr + " and ACNO >='" + Acno_S + "' and ACNO <='" + Acno_E + "'";
             $("#JQDataGridBookDetails").datagrid('setWhere', FiltStr);
             openForm('#JQDialog3', {}, "", 'dialog');
         }

         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "<input  type='checkbox' />";
         }
         function dataGridViewOnLoadSucess() {
             var CostCenterID = $('#CostCenterID_Query').combobox('getValue');
             if ((CostCenterID) == '000' || CostCenterID == undefined) {
                 GetAllAccItemM();
             }
             var tt= $('#VoucherYear_Query').combobox('getValue');
         }
         function queryGrid(dg) {
             var UserID = getClientInfo("UserID");
             if ($(dg).attr('id') == 'dataGridView') {
                 var BudgetYear = $('#VoucherYear_Query').combobox('getValue');
                 var EndDate = $("#EndDate_Query").datebox('getValue');
                 var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
                 var Acno = $("#Acno_S_Query").combobox('getValue');
                 var AcnoS = $("#AcnoS_Query").val();
                 var AcnoE = $("#AcnoE_Query").val();
                 if ((AcnoS == '' && AcnoE == '') && CostCenterID == '') {
                     alert('提示!!為加快資料搜尋速度,請選取成本中心');
                     $("#CostCenterID_Query").focus();
                     return false;
                 }
                 //var SubAcno = $("#SubAcno_S_Query").combobox('getValue');
                 GetGridDataDynamic(BudgetYear, EndDate, CostCenterID, Acno, UserID, AcnoS, AcnoE);
                 //$("#CostCenterID_Query").combobox('setValue', CostCenterID);
             }
         }
         //依條件取得Grid資料
         function GetGridDataDynamic(BudgetYear, EndDate, CostCenterID, Acno, UserID, AcnoS, AcnoE) {
             $.ajax({
                 type: "POST",

                 url: '../handler/jqDataHandle.ashx?RemoteName=sglYearBudget.glYearBudget',
                 data: "mode=method&method=" + "GetGridDataDynamic" + "&parameters=" + BudgetYear + "," + EndDate + "," + CostCenterID + "," + Acno + "," + AcnoS + ","+ AcnoE + "," + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     $.messager.progress({ msg: '資料下載中...', interval: '1000' });//進度條開始
                     if (data == "false") {
                         $("#dataGridView").datagrid('setWhere', "1=0");
                     }
                     else {
                         $("#dataGridView").datagrid('setWhere', "UserID='" + UserID + "'");
                     }
                     setTimeout(function () {
                       $.messager.progress('close'); //進度條結束
                     }, 1000);
                  
                 }
             }
          );
         }
         function GetCostCenter() {
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglYearBudget.glYearBudget',
                 data: "mode=method&method=" + "GetCostCenter" + "&parameters=" + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         $('#CostCenterID_Query').combobox("loadData", rows);
                         //$('#CostCenterID_Query').combobox('setValue', rows[0].CostCenterID);
                     }
                 }
             }
          );
         }
         function GetAllAccItemM() {
             var UserID = getClientInfo("UserID");
             var CostCenterID = '000';
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglYearBudget.glYearBudget',
                 data: "mode=method&method=" + "GetAccitemM" + "&parameters=" + CostCenterID + "," + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         $('#Acno_S_Query').combobox("loadData", rows);
                     }
                    
                 }
             }

          );
         }
         function Acno_SOnBlur() {
             $("#dataFormMasterAcno_E").val($("#dataFormMasterAcno_S").val());
         }
         function SubAcno_SOnBlur() {

             $("#dataFormMasterSubAcno_E").val($("#dataFormMasterSubAcno_S").val());
         }
         function GetYear() {
             var today = new Date();
             var CurrentYear = today.getFullYear();
             return CurrentYear;
         }
         function TodayDate() {
             var dt = new Date();
             var currentDate = dt.getFullYear() + "/" + (dt.getMonth() + 1) + "/" + dt.getDate();
             return currentDate;
         }
         function AccItemMOnSelect() {
             var CostCenterID = $('#CostCenterID_Query').combobox('getValue');
             var ACNO_S = $('#Acno_S_Query').combobox('getValue');
             var sStr = "";
             if (CostCenterID != "" || CostCenterID != undefined) {
                 sStr = "AND CostCenterID = " + CostCenterID;
             }
             $('#SubAcno_S_Query').combobox('setWhere', "ACNO_S = " + ACNO_S + sStr);
         }
         function QCostCenterOnSelect() {
             var VoucherYear = $('#VoucherYear_Query').combobox('getValue');
             var UserID = getClientInfo("UserID");
             var CostCenterID = $('#CostCenterID_Query').combobox('getValue');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglYearBudget.glYearBudget', 
                 data: "mode=method&method=" + "GetAccitemM" + "&parameters=" + VoucherYear +"," + CostCenterID + "," + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         $('#Acno_S_Query').combobox("loadData", rows);
                         $('#Acno_S_Query').combobox('setValue','');
                     }
                 }
             }
          );
         }
         //匯出EXECL ByCoding
         function ManualExcel() {
             var BudgetYear = $('#VoucherYear_Query').combobox('getValue');
             var EndDate = $("#EndDate_Query").datebox('getValue');
             var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
             var CostCenterName = $("#CostCenterID_Query").combobox('getText');
             var Acno = $("#Acno_S_Query").combobox('getValue');
             var AcnoS = $("#AcnoS_Query").val();
             var AcnoE = $("#AcnoE_Query").val();
             var UserID = getClientInfo("UserID");
             $.ajax({
                 url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sglYearBudget', method: 'GetGridDataDynamicExcel' }),
                 data: "&parameters=" + BudgetYear + "," + EndDate + "," + CostCenterID + "," + Acno + "," + AcnoS + "," + AcnoE + "," + UserID,
                 type: 'POST',
                 async: true,
                 success: function (data) {
                     //Json.FileName
                     var Json = $.parseJSON(data);
                     if (Json.IsOK) {
                         var Url = $('<a>', {
                             href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: BudgetYear + '預算控管.xls', FilePathName: Json.FileStreamOrFileName }),
                             target: '_blank'
                         }).html(BudgetYear+'_'+CostCenterName+'_'+'預算使用明細.xls')[0].outerHTML;
                         $.messager.alert('下載', Url, '');
                     }
                     else $.messager.alert('錯誤', Json.Msg, 'error');
                 },
                 beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
                 complete: function () { $.messager.progress('close'); },
                 error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
             });
         }
         function dataFormMasterOnApply() {
             var lv = $("#dataFormMasterVoucherYear").val();
             if (lv == "" || lv == "undefined") {
                 alert('注意!!請輸入預算年度');
                 $("#dataFormMasterVoucherYear").focus();
                 return false;
             }
             var lv = $("#dataFormMasterBudgetType").combobox('getValue');
             if (lv == "" || lv == "undefined") {
                 alert('注意!!請選取預算類別');
                 $('#dataFormMasterBudgetType').combobox().next('span').find('input').focus();
                 return false;
             }
             //var lv = $("#dataFormMasterCostCenterID").combobox('getValue');
             //if (lv == "" || lv == "undefined") {
             //    alert('注意!!請選取成本中心');
             //    $('#dataFormMasterCostCenterID').combobox().next('span').find('input').focus();
             //    return false;
             //}
             var lv = $("#dataFormMasterAcno_S").val();
             if (lv == "" || lv == "undefined") {
                 alert('注意!!請選取會計科目主目');
                 $('#dataFormMasterAcno_S').focus();
                 return false;
             }
             var lv = $("#dataFormMasterAcno_E").val();
             if (lv == "" || lv == "undefined") {
                 alert('注意!!請選取會計科目子目');
                 $('#dataFormMasterAcno_E').focus();
                 return false;
             }
             var lv = $("#dataFormMasterAcnoName").val();
             if (lv == "" || lv == "undefined") {
                 alert('注意!!請輸入會計科目名稱');
                 $("#dataFormMasterAcnoName").focus();
                 return false;
             }
             var lv = $("#dataFormMasterBudgetAmt").val();
             if (lv == "" || lv == "undefined") {
                 alert('注意!!預算金額不得為0');
                 $("#dataFormMasterBudgetAmt").focus();
                 return false;
             }
             var CostCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
             var Acno_S = $("#dataFormMasterAcno_S").val();
             var SubAcno_S = $("#dataFormMasterSubAcno_S").val();
             $("#dataFormMasterAcSubno").val(CostCenterID + Acno_S + SubAcno_S);
             //var where = $("#dataGridView").datagrid('getWhere');
             //alert(where);
             //$("#dataGridView").datagrid('setWhere', '1=1');
         }
         function dataGridViewOnInsert() {
             var UserID = getClientInfo("UserID");
             var AuditorList = GetAuditorList();
             var UserRight = AuditorList.indexOf(UserID);
             if (UserRight == -1) {
                 alert('注意!!,你無權限新增預算資料');
                 return false;
             }
             return true;
         }
         function dataGridViewOnUpdate() {
             var UserID = getClientInfo("UserID");
             var AuditorList = GetAuditorList();
             var UserRight = AuditorList.indexOf(UserID);
             if (UserRight == -1) {
                 alert('注意!!,你無權限修改預算資料');
                 return false;
             }
             return true;
         }
         function GetAuditorList() {
             //var UserID = getClientInfo("UserID");
             var Category = 'YearBudgetAuditors';
             var ReturnStr = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglYearBudget.glYearBudget',
                 data: "mode=method&method=" + "GetAuditorList" + "&parameters=" + Category,
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         ReturnStr = data;
                     }
                 }
             });
             return ReturnStr;
         }
         function OnLoadSucessJQDBD() {
             $('#JQDialog3').dialog('setTitle', DetailGridTitle);
         }
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglYearBudget.glYearBudget" runat="server" AutoApply="True"
                DataMember="glYearBudget" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="年度預算編輯檢視" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="" UpdateCommandVisible="False" ViewCommandVisible="True" OnLoadSuccess="dataGridViewOnLoadSucess" OnInsert="dataGridViewOnInsert" OnUpdate="dataGridViewOnUpdate" Width="1160px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="BudgetType" Editor="numberbox" FieldName="BudgetType" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預算類別" Editor="text" FieldName="BudgetTypeName" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" EditorOptions="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AcnoName" Format="" MaxLength="0" Width="150" />
                    <JQTools:JQGridColumn Alignment="center" Caption="加總" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSummary" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="全年預算金額" Editor="numberbox" FieldName="BudgetAmt" Format="N0" Width="90" Total="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="期間預算金額" Editor="numberbox" FieldName="PBudgetAmt" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Total="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="期間入帳金額" Editor="numberbox" FieldName="BookedAmt" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Total="" FormatScript="BookedAmt_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="最近入帳日" Editor="datebox" FieldName="VocherDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="待入帳金額" Editor="numberbox" FieldName="PrepareAmt" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="期間使用比率(%)" Editor="numberbox" FieldName="PS" MaxLength="0" Width="100" Visible="True" Format="N1" />
                    <JQTools:JQGridColumn Alignment="right" Caption="全年使用比率(%)" Editor="numberbox" FieldName="YS" Format="N1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="會計科目" Editor="text" FieldName="Acno_S" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SubAcno_S" Editor="text" FieldName="SubAcno_S" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Acno_E" Editor="text" FieldName="Acno_E" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SubAcno_E" Editor="text" FieldName="SubAcno_E" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RecordType" Editor="text" FieldName="RecordType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
 <%--               <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="ManualExcel" Text="匯出Excel" Visible="True" />--%>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportXlsx" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="預算年度" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'VoucherYear',textField:'VoucherYear',remoteName:'sglYearBudget.VoucherYear',tableName:'VoucherYear',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VoucherYear" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" DefaultMethod="GetYear" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結止日期" Condition="=" DataType="datetime" DefaultValue="" Editor="datebox" FieldName="EndDate" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" DefaultMethod="TodayDate" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglYearBudget.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QCostCenterOnSelect,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="130" DefaultValue="000" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="會計主科目" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ACNO_S',textField:'AcnoName',remoteName:'sglYearBudget.AccItemM',tableName:'AccItemM',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Acno_S" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起迄主科目" Condition="%" DataType="string" Editor="text" FieldName="AcnoS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="-" Condition="%" DataType="string" Editor="text" FieldName="AcnoE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="50" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="年度預算編輯" Width="500px" DialogLeft="50px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="glYearBudget" HorizontalColumnsCount="2" RemoteName="sglYearBudget.glYearBudget" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Span="1" Visible="False" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預算年度" Editor="text" FieldName="VoucherYear" Format="" Width="90" Span="2" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預算類別" Editor="infocombobox" FieldName="BudgetType" Format="" maxlength="0" Width="92" EditorOptions="valueField:'BudgetType',textField:'BudgetTypeName',remoteName:'sglYearBudget.glBudgetType',tableName:'glBudgetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'COSTCENTERID',textField:'COSTCENTERNAME',remoteName:'sglYearBudget.CostCenterList',tableName:'CostCenterList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目主" Editor="text" FieldName="Acno_S" Format="" maxlength="0" Width="92" OnBlur="Acno_SOnBlur" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目次" Editor="text" FieldName="SubAcno_S" Format="" maxlength="0" Width="92" Span="1" OnBlur="SubAcno_SOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目名稱" Editor="text" FieldName="AcnoName" Format="" Width="260" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目主" Editor="text" FieldName="Acno_E" Format="" Width="92" OnBlur="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目次" Editor="text" FieldName="SubAcno_E" maxlength="0" Width="80" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AcSubno" Editor="text" FieldName="AcSubno" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="right" Caption="預算金額" Editor="numberbox" FieldName="BudgetAmt" Format="" Width="92" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主科目加總" Editor="checkbox" FieldName="IsSummary" Width="80" EditorOptions="on:1,off:0" Visible="True" Format="genCheckBox" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetYear" FieldName="VoucherYear" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BudgetTypeName" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsSummary" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BudgetAmt" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
             <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="30px" DialogTop="60px" Title="期間入帳明細" Width="1080px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridBookDetails" runat="server" AlwaysClose="True" DataMember="BookedAmtDetails" RemoteName="sglYearBudget.BookedAmtDetails" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="15,30,60" PageSize="15" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Width="1010px" BufferView="False" NotInitGrid="False" RowNumbers="True" ToolTip="測試" OnLoadSuccess="OnLoadSucessJQDBD">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="入帳日期" Editor="datebox" FieldName="VoucherDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="會科代號" Editor="text" FieldName="ACNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="分錄說明" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="760">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="right" Caption="入帳金額" Editor="text" FieldName="Amtshow" Frozen="False" IsNvarChar="True" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="N0" Total="sum">
                         </JQTools:JQGridColumn>
                     </Columns>
                      <TooItems>
                      <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                      </TooItems>
                </JQTools:JQDataGrid>
         </JQTools:JQDialog>
            
        </div>
   
    </form>
</body>
</html>
