<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RglInternal.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>     
         //========================================= ready ====================================================================================

         var sCompanyID = "";
         var sVoucherID = "";

         $(document).ready(function () {

             //傳回登入者可用的成本中心
             setTimeout(function () {
                 GetCostCenterID();
             }, 1000);

            //變色        
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
             //查詢條件合併
             var Date1 = $('#CreateDate_Query').closest('td');
             var Date2 = $('#VoucherDate_Query').closest('td').children();
             Date1.append(' ~ ').append(Date2);           

         });
         
         //---------------------------------------呼叫Method---------------------------------------
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = null;
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sglCompany',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };

         //得到成本中心
         var GetCostCenterID = function () {
             var UserID = getClientInfo("UserID");
             var CodeList = GetDataFromMethod('GetCostCenter', { User_ID: UserID });
             if (CodeList != null) {
                 $("#CostCenterID_Query").combobox('loadData', CodeList);//成本中心
             }
         }

         function OnLoadSuccessGV() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 600 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();

             //查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#VoucherDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));

             ////設定傳回目前的公司別、傳票類別               
             //$("#CompanyID_Query").combobox('setValue', sCompanyID);
             //$("#VoucherID_Query").options('setValue', 'A');
           

         }

         function queryGrid(dg) {//查詢後添加固定條件
             var CompanyID = "1";//資訊+人力+傑信
             var VoucherID = "A";
             var JQDate1 = $("#CreateDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
             var JQDate2 = $("#VoucherDate_Query").datebox("getValue");
             var ckDiff = $("#Diff_Query").combobox('getValue');//0 損益表 ,1 損益平均數差異分析 ,2 損益累積差異分析 ,3 分類帳
             //報表用參數
             var CompanyText = "1";

             var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
             if (CostCenterID == "") {
                 alert('請選擇成本中心');
                 return false;
             }

             //---------------------日期檢查---------------------------------------------------------------
             var beginDateValidate = $.fn.datebox('parseDate', JQDate1.replace(/\//g, '-'));
             var endDateValidate = $.fn.datebox('parseDate', JQDate2.replace(/\//g, '-'));
             //判斷起始日期不可大於結束日期
             if (beginDateValidate == "Invalid Date" || !$.jbIsDateStr(JQDate1)) {
                 alert('起始日期:' + JQDate1 + '格式錯誤');
                 $("#dataFormMasterBeginDate").datebox('textbox').focus();
                 return false;
             }

             if (endDateValidate == "Invalid Date" || !$.jbIsDateStr(JQDate2)) {
                 alert('結束日期:' + JQDate2 + '格式錯誤');
                 $("#dataFormMasterEndDate").datebox('textbox').focus();
                 return false;
             }

             if (JQDate1 > JQDate2) {
                 alert('起始日期 : ' + JQDate1 + ' 需小於結束日期 : ' + JQDate2);
                 $("#VoucherDate_Query").datebox('textbox').focus();
                 return false;
             }
             //---------------------鎖檔檢查---------------------------------------------------------------
             var cnt = "";
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglCompany.glCostCenter', //連接的Server端，command
                 data: "mode=method&method=" + "checkLockYM" + "&parameters=" + CompanyID + "," + JQDate2,
                 cache: false,
                 async: false,
                 success: function (data) {
                         cnt = $.parseJSON(data);
                 }
             });
             if ((cnt == "0")) {
                 alert("此日期期間的損益資料尚未開放查詢。");
                 return false;
             }


             //-----------------------------------------------損益表-----------------------------------------------
             if (ckDiff != "3") {
                 var CostCenterText = "";
                 if (CostCenterID != "") {
                     CostCenterText = $("#CostCenterID_Query").combobox('getText');
                 }
                 var IsEng = "0";
                 var url = "../JB_ADMIN/REPORT/JBGL/RglProfitListReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID + "&IsDiff=" + ckDiff + "&IsEng=" + IsEng +
                     "&CostCenterID=" + CostCenterID + "&CostCenterText=" + CostCenterText + "&CompanyText=" + CompanyText;

                 var height = $(window).height() - 40;
                 var height2 = $(window).height() - 110;
                 var width = $(window).width() - 60;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "Report",
                     maximizable: true
                 });
                 $('<iframe style="border: 0px;" src="' + url + '" width="99%" height="98%" ></iframe>').appendTo(dialog.find('.panel-body'));
                 dialog.dialog('open');
             } else {//-----------------------------------------------分類帳------------------------------------------------

                 var Acno1 = "";
                 var Acno2 = "";
                 var SubAcno1 = "";
                 var SubAcno2 = "";
                 var VoucherNo = "";
                 var iType = "4";// 1傳票清單 2日記帳 3分類帳  4分類帳-不分頁 5分類帳-不分群組

                 //報表用參數
                 var TypeText = "分類帳";

                 var url = "../JB_ADMIN/REPORT/JBGL/RglVoucherListReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
                     "&VoucherNo=" + VoucherNo + "&CostCenterID=" + CostCenterID + "&Acno1=" + Acno1 + "&Acno2=" + Acno2 + "&SubAcno1=" + SubAcno1 + "&SubAcno2=" + SubAcno2 +
                     "&iType=" + iType + "&TypeText=" + TypeText + "&CompanyText=" + CompanyText;

                 var height = $(window).height() - 20;
                 var height2 = $(window).height() - 90;
                 var width = $(window).width() - 20;
                 var dialog = $('<div/>')
                 .dialog({
                     draggable: false,
                     modal: true,
                     height: height,
                     //top:0,
                     width: width,
                     title: "Report",
                     //maximizable: true                              
                 });
                 $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
                 dialog.dialog('open');
             }

         }
         
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sRglVoucherList.glVoucherList" runat="server" AutoApply="True"
                DataMember="glVoucherList" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/JBGL/glVoucherList.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="95" RowSpan="0" Span="2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="datetime" Editor="datebox" FieldName="VoucherDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="95" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表種類" Condition="%" DataType="string" Editor="infocombobox" FieldName="Diff" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="3" Width="200" EditorOptions="items:[{value:'0',text:'損益表',selected:'true'},{value:'1',text:'損益平均數差異分析',selected:'false'},{value:'2',text:'損益累積差異分析',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
