<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RglAssetDebt.aspx.cs" Inherits="Template_JQuerySingle1" %>

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

             //傳回登入者目前設定的公司別、傳票類別
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherMaster', //連接的Server端，command
                 data: "mode=method&method=" + "getglVoucherSet" + "&parameters=" + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         sCompanyID = rows[0].CompanyID;
                         sVoucherID = rows[0].VoucherID;
                     }
                 }
             });
            //變色        
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });            

         });
                
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
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));

             $("#CreateBy_Query").options('setValue', "1");

             //設定傳回目前的公司別、傳票類別               
             $("#CompanyID_Query").combobox('setValue', sCompanyID);
             $("#VoucherID_Query").options('setValue', 'A');
           

         }

         function queryGrid(dg) {//查詢後添加固定條件
             //var where = $(dg).datagrid('getWhere');
             //if (where.length > 0) {
             var CompanyID = $("#CompanyID_Query").combobox('getValue');
             var VoucherID = $('#VoucherID_Query').options('getValue');
             var JQDate1 = $("#CreateDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                

             var iType = $('#CreateBy_Query').options('getValue');//報表格式	1報告式 2帳戶式            

             var IsEng = "0";
             var ckEng = $("#Eng_Query").checkbox('getValue');
             if (ckEng == true) {
                 IsEng = "1";
             }

             var CompanyText = $("#CompanyID_Query").combobox('getText');
             if (CompanyID == "1") {
                 CompanyText = $("#CompanyID_Query").combobox('getValue');
             }
             //傑誠英文版
             if (CompanyID == "4" && IsEng == "1") {
                 CompanyText = "Jye-Cheng Human Resource Management Consulting Co., Ltd.";
             }

             var url = "../JB_ADMIN/REPORT/JBGL/RglAssetDebtReportView.aspx?EDate=" + JQDate1 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
                 "&iType=" + iType + "&CompanyText=" + CompanyText + "&IsEng=" + IsEng;

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
         
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sRglVoucherList.glVoucherList" runat="server" AutoApply="True"
                DataMember="glVoucherList" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JBGL/REPORT/glVoucherList.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" >
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="200" Span="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票類別" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:80,remoteName:'sRVoucherList.infoglVoucherType2',tableName:'infoglVoucherType2',valueField:'VoucherType',textField:'VoucherType',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="VoucherID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="截止日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" RowSpan="0" Span="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表格式" Condition="=" DataType="string" Editor="infooptions" FieldName="CreateBy" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" EditorOptions="title:'JQOptions',panelWidth:170,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:6,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'報告式',value:'1'},{text:'帳戶式',value:'2'}]" Span="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="英文版" Condition="%" DataType="string" Editor="checkbox" FieldName="Eng" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="45" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
