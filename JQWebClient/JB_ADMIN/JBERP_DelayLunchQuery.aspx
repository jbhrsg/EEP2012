<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DelayLunchQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
       
         $(document).ready(function () {
             //設定匯款日期寬度
             $('#JQDate').datebox({
                 width: 90                 
             })
             var dt = new Date();
             $("#JQDate").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));

             //設定 誤餐申請紀錄 dialog
             initDelayLunchDialog();
            
         });

         //設JQDate定未結案的最小請領年月
         function SetYearMonth() {            
             var YearMonth=getYearMonth();
             $('#YearMonth_Query').val(YearMonth);
             $('#dataGridView').datagrid('setWhere', " a.YearMonth='" + YearMonth + "'");
         }
         //抓取未結案的最小請領年月
         function getYearMonth() {
             var YearMonth = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply', //連接的Server端，command
                 data: "mode=method&method=" + "GetLunchApplyYM",
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         YearMonth = $.parseJSON(data);
                     }
                 }
             });
             return YearMonth;
         }

         //是否已結案
         function genCheckBox(val) {
             if (val == undefined) ""
             else if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }

         //產生設定誤餐資料
         //1.檢查請領年月是否已完成結案
         //2.檢查請領年月是否是未結案的最小請領年月getYearMonth()
         function InsertDelayLunch() {
             
             //檢查請領年月是否已完成結案
                 var YearMonth = $('#YearMonth_Query').val();
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "checkLunchYMClose" + "&parameters=" + YearMonth,
                     cache: false,
                     async: false,
                     success: function (data) {
                         if (data != false) {
                             IsClose = $.parseJSON(data);
                         }
                     }
                 });
                 if ((IsClose == "True")) {
                     alert("此請領年月已完成結案！故不可產生。");
                     return false;
                 }
                 //2.檢查請領年月是否是未結案的最小請領年月getYearMonth()
                 if ($('#YearMonth_Query').val() > getYearMonth()) {
                     alert("上個月尚未完成結案！故不可產生。");
                     return false;
                 }

                 var pre = confirm("確定產生?");
                 if (pre == true) {
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply', //連接的Server端，command
                         data: "mode=method&method=" + "InsertDelayLunch" + "&parameters=" + YearMonth, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                         cache: false,
                         async: false,
                         success: function (data) {
                             alert('誤餐資料產生完成！');
                             $('#dataGridView').datagrid("reload");
                         },
                         error: function (xhr, ajaxOptions, thrownError) {
                             alert(xhr.status);
                             alert(thrownError);
                         }
                     });
                 }
         }

         //結案
         //檢查1.檢查請領年月是否是未結案的最小請領年月getYearMonth()
         //    2.檢查設定500名單內的人又去訂便當
         function UpdateDelayLunch() {
             var YearMonth = $('#YearMonth_Query').val();
             //1.檢查請領年月是否是未結案的最小請領年月getYearMonth()
             if (YearMonth > getYearMonth()) {
                 alert("上個月尚未完成結帳！故此月不可結案。");
                 return false;
             }
             //2.檢查設定500名單內的人又去訂便當   
             var sNameC;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply', //連接的Server端，command
                 data: "mode=method&method=" + "checkLunchEmpOrder" + "&parameters=" + YearMonth,
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         sNameC = $.parseJSON(data);
                     }
                 }
             });
             if ((sNameC != "")) {
                 alert("整月不訂人員卻有訂便當紀錄！--> " + sNameC);
                 return false;
             }


             var pre = confirm("確定結案?");
             if (pre == true) {
                 
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply', //連接的Server端，command
                     data: "mode=method&method=" + "UpdateDelayLunch" + "&parameters=" + YearMonth, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         alert('結案完成！');
                         $('#dataGridView').datagrid("reload");
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(thrownError);
                     }
                 });
             }
         }

         //隱藏顯示按鈕
         function OnLoadSuccessGV() {
             //控制顯示
             var data = $("#dataGridView").datagrid('getData');
             if (data.total > 0) {
                 var rowdata = $('#dataGridView').datagrid('getSelected');
                 if (rowdata.IsClose == true) {
                     $("#toolItemdataGridView產生設定誤餐資料").hide();
                     $("#toolItemdataGridView結案").hide();
                 } else {
                     $("#toolItemdataGridView產生設定誤餐資料").show();
                     $("#toolItemdataGridView結案").show();
                 }
             } else {
                 $("#toolItemdataGridView產生設定誤餐資料").hide();
                 $("#toolItemdataGridView結案").hide();
             }
         }

         // 誤餐申請紀錄 dialog
         function initDelayLunchDialog() {
             $("#Dialog_DelayLunch").dialog(
             {
                 height: 400,
                 width: 850,
                 left: 100,
                 top: 80,
                 resizable: false,
                 modal: true,
                 title: "誤餐申請紀錄",
                 closed: true
             });
         };

         //誤餐申請紀錄
         function DelayLunchLink(value, row, index) {
             return $('<a>', { href: 'javascript:void(0)', name: 'DelayLunchLink', onclick: 'LinkDelayLunch.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '申請紀錄' })[0].outerHTML
         }

         // open誤餐申請紀錄畫面 dialog
         function LinkDelayLunch() {
             //alert(index)
             var index = $(this).attr('rowIndex');
             $("#dataGridView").datagrid('selectRow', index);
             var rows = $("#dataGridView").datagrid('getSelected');
             var YearMonth = rows.YearMonth;
             var UserID = rows.UserID;
             $("#dataGrid_DelayLunch").datagrid('setWhere', "a.YearMonth = '" + YearMonth + "' and a.UserID='"+UserID+"'");
             $("#Dialog_DelayLunch").dialog("open");
         }        

         //計算最後誤餐金額
         function OnBlur_Check() {
             CheckTotal = $('#dataFormMasterCheckTotal').val();
             ApplyDate = $('#dataFormMasterApplyDate').val();
             CheckEat = $('#dataFormMasterCheckEat').val();
             CheckAbsent = $('#dataFormMasterCheckAbsent').val();
             CheckOther = $('#dataFormMasterCheckOther').val();
             var Amt = CheckTotal - ApplyDate - CheckEat - CheckAbsent - CheckOther;//修改期間金額-扣除未訂餐金額-修改訂餐扣款-修改請假扣款-其他扣款
             $('#dataFormMasterCheckAmt').numberbox('setValue', Amt);
         }
         //修改完成
         function OnAppliedDF() {
             $('#dataGrid_DelayLunch').datagrid("reload");
             $('#dataGridView').datagrid("reload");
         }
         //-----------------------------------------------------------------------------------------------------------------------------------------
         
         //期間總金額 iType=1
         function CheckTotalLink(value, row, index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkDelayDate(" + index + ",1);'>" + value + "</a>";
             else return value;
         }
         //訂餐總扣款 iType=2
         function CheckEatLink(value, row, index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkDelayDate(" + index + ",2);'>" + value + "</a>";
             else return value;
         }
         //請假總扣款 iType=3
         function CheckAbsentLink(value, row, index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkDelayDate(" + index + ",3);'>" + value + "</a>";
             else return value;
         }
         //區間排除總金額 iType=4
         function ApplyDateLink(value, row, index) {
             if (value != 0)
                 return "<a href='javascript: void(0)' onclick='LinkDelayDate(" + index + ",4);'>" + value + "</a>";
             else return value;
         }
         
         // open誤餐日期紀錄畫面 dialog
         function LinkDelayDate(index,iType) {
             $("#dataGridView").datagrid('selectRow', index);
             var rows = $("#dataGridView").datagrid('getSelected');
             var YearMonth = rows.YearMonth;
             var UserID = rows.UserID;
             var IsSys = rows.IsSys;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchQuery.ERPDelayLunchApply',  //連接的Server端，command
                 data: "mode=method&method=" + "DelayDateList" + "&parameters=" + YearMonth + "," + UserID + "," + iType + "," + IsSys,  //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入搜尋字串
                 cache: false,
                 async: true,
                 success: function (data) {
                     var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示

                     $('#dataGrid_DelayDate').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                 }
             });
             openForm('#Dialog_DelayDate', {},'viewed','dialog');

         }


         //呼叫Report視窗
         function OpenexportGrid() {

             var YearMonth = $('#YearMonth_Query').val();
             var ExportDate = $("#JQDate").combo('textbox').val();
             var url = "../JB_ADMIN/REPORT/DelayLunch/BankRemitReportView.aspx?YearMonth=" + YearMonth + "&ExportDate=" + ExportDate;

             var height = $(window).height() - 100;
             var width = $(window).width() - 400;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 width: width,
                 title: "Report",
                 //maximizable: true                              
             });
             $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
             dialog.dialog('open');
         }       
  

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchQuery.ERPDelayLunchApply" runat="server" AutoApply="True"
                DataMember="ERPDelayLunchApply" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="誤餐費作業" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="DelayLunchID" Editor="numberbox" FieldName="DelayLunchID" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="請領年月" Editor="text" FieldName="YearMonth" Format="" MaxLength="0" Visible="true" Width="53"  />
                    <JQTools:JQGridColumn Alignment="center" Caption="工號" Editor="text" FieldName="UserID" Format="" MaxLength="0" Visible="true" Width="45"  />
                    <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Visible="true" Width="60"  />
                    <JQTools:JQGridColumn Alignment="center" Caption=" " Editor="text" FieldName="DelayLunchLink" FormatScript="DelayLunchLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95"></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="誤餐總金額" Editor="numberbox" FieldName="CheckAmt" Format="" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="系統產生" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSys" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="已結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsClose" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="期間總金額" Editor="numberbox" FieldName="CheckTotal" Format="" Visible="true" Width="80" FormatScript="CheckTotalLink" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未訂餐金額" Editor="numberbox" FieldName="ApplyDate" FormatScript="ApplyDateLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="訂餐總扣款" Editor="numberbox" FieldName="CheckEat" Format="" Visible="true" Width="80" FormatScript="CheckEatLink" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假總扣款" Editor="numberbox" FieldName="CheckAbsent" Format="" Visible="true" Width="80" FormatScript="CheckAbsentLink" />
                    <JQTools:JQGridColumn Alignment="right" Caption="其它總扣款" Editor="numberbox" FieldName="CheckOther" Format="" Visible="true" Width="80" />
                    
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-back" ItemType="easyui-linkbutton" OnClick="InsertDelayLunch" Text="產生設定誤餐資料" />
                    <JQTools:JQToolItem Icon="icon-ok" ItemType="easyui-linkbutton" OnClick="UpdateDelayLunch" Text="結案" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="OpenexportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="請領年月" Condition="=" DataType="string" Editor="text" FieldName="YearMonth" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="75" DefaultMethod="SetYearMonth" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工工號" Condition="%" DataType="string" Editor="text" FieldName="UserID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工姓名" Condition="%" DataType="string" Editor="text" FieldName="NAME_C" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="65" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="BeginDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="結束日期" Condition="&lt;=" DataType="datetime" Editor="datebox" FieldName="EndDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="75" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="Dialog_DelayLunch" runat="server" BindingObjectID="" Title="誤餐費明細" ShowSubmitDiv="False">
                        <JQTools:JQDataGrid ID="dataGrid_DelayLunch" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchQuery.ERPDelayLunchList" runat="server" AutoApply="True"
                DataMember="ERPDelayLunchList" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessGV" Width="100%">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="DelayLunchID" Editor="numberbox" FieldName="DelayLunchID" Format="" Visible="False" Width="120" MaxLength="0" />
                    <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="text" FieldName="NAME_C" Format="" Visible="true" Width="60" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="起始日期" Editor="datebox" FieldName="BeginDate" Visible="True" Width="70" ReadOnly="True">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="結束日期" Editor="datebox" FieldName="EndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="誤餐總金額" Editor="numberbox" FieldName="CheckAmt" Format="" Total="sum" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="期間金額(後)" Editor="numberbox" FieldName="CheckTotal" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未訂餐金額" Editor="numberbox" FieldName="ApplyDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="75" />
                    <JQTools:JQGridColumn Alignment="right" Caption="訂餐扣款(後)" Editor="numberbox" FieldName="CheckEat" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假扣款(後)" Editor="numberbox" FieldName="CheckAbsent" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="其它扣款" Editor="numberbox" FieldName="CheckOther" Format="" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="扣款備註" Editor="text" FieldName="CheckOtherMemo" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="誤餐金額(前)" Editor="numberbox" FieldName="ApplyAmt" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="期間金額(前)" Editor="numberbox" FieldName="ApplyTotal" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="訂餐扣款(前)" Editor="numberbox" FieldName="ApplyEat" Format="" Visible="true" Width="75" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="請假扣款(前)" Editor="numberbox" FieldName="ApplyAbsent" Format="" Visible="True" Width="75" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="系統產生" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSys" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="已結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsClose" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="45" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>                  
                </Columns>                             
            </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" DialogTop="70px" Title="誤餐費修改" Width="600px">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="ERPDelayLunchList" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ParentObjectID="dataGrid_DelayLunch" RemoteName="sERPDelayLunchQuery.ERPDelayLunchList" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedDF">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="DelayLunchID" Editor="numberbox" FieldName="DelayLunchID" Format="" NewRow="False" ReadOnly="False" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="BeginDate" Format="" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="結束日期" Editor="datebox" FieldName="EndDate" Format="" maxlength="0" NewRow="False" ReadOnly="True" Span="1" Visible="True" Width="100" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="請領年月" Editor="text" FieldName="YearMonth" NewRow="True" ReadOnly="True" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修改期間金額" Editor="numberbox" FieldName="CheckTotal" NewRow="False" OnBlur="" ReadOnly="True" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="扣除未訂餐金額" Editor="numberbox" FieldName="ApplyDate" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修改訂餐扣款" Editor="numberbox" FieldName="CheckEat" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="修改請假扣款" Editor="numberbox" FieldName="CheckAbsent" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="其他扣款" Editor="numberbox" FieldName="CheckOther" MaxLength="0" NewRow="False" OnBlur="OnBlur_Check" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="最後誤餐金額" Editor="numberbox" FieldName="CheckAmt" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="扣款備註" Editor="textarea" EditorOptions="height:80" FieldName="CheckOtherMemo" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="350" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                </Columns>
                            </JQTools:JQDataForm>
                        </JQTools:JQDialog>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="Dialog_DelayDate" runat="server" BindingObjectID="" Title="誤餐費明細" ShowSubmitDiv="False" DialogLeft="300px" DialogTop="100px" Width="300px">
                        <JQTools:JQDataGrid ID="dataGrid_DelayDate" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchQuery.ERPDelayLunchList" runat="server" AutoApply="True"
                DataMember="ERPDelayLunchList" Pagination="False" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="80px" QueryMode="Panel" QueryTop="80px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="Adate" Visible="True" Width="99">
                    </JQTools:JQGridColumn>   
                    <JQTools:JQGridColumn Alignment="center" Caption="星期" Editor="text" FieldName="Dateweek" Visible="True" Width="90">
                    </JQTools:JQGridColumn>                  
                </Columns>                
            </JQTools:JQDataGrid>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialogExport" runat="server" DialogLeft="150px" DialogTop="80px" Title="匯出Excel" ShowSubmitDiv="False" Width="700px" Height="450px">
                <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="匯款日期:"></asp:Label>
                <JQTools:JQDateBox ID="JQDate" runat="server" />
                &nbsp;<a class="easyui-linkbutton" href="#" onclick="ExportQuery()">查詢</a><JQTools:JQDataGrid ID="JQGridExport" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPDelayLunchApply" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPDelayLunchQuery.ERPDelayLunchApply" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="匯款日" Editor="text" FieldName="ExportDate" Format="" MaxLength="0" Visible="True" Width="65" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="NAME_C" Format=""  MaxLength="0" Visible="True" Width="60" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="收款戶名" Editor="text" FieldName="NAME_C" Format="" MaxLength="0" Visible="True" Width="60" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="收款銀行" Editor="text" FieldName="BANK_CODE" Format="" MaxLength="0" Visible="True" Width="65" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="收款人帳號" Editor="text" FieldName="ACCOUNT_NO" Format="" MaxLength="0" Visible="True" Width="70" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="帳款金額" Editor="numberbox" FieldName="CheckAmt" Format=""  Visible="True" Width="60" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="投保單位" Editor="text" FieldName="Unit" Format="" MaxLength="0" Visible="True" Width="55" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="身份證字號" Editor="text" FieldName="IDNO" Format="" MaxLength="0" Visible="True" Width="78" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="電子郵件" Editor="text" FieldName="Email" Format="" MaxLength="0" Visible="True" Width="55" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" Enabled="True" />
                    </TooItems>
                </JQTools:JQDataGrid>
        </JQTools:JQDialog>


        </div>
    </form>
</body>
</html>
