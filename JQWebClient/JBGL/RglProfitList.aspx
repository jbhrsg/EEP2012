<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RglProfitList.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             //查詢條件合併
             var Date1 = $('#CreateDate_Query').closest('td');
             var Date2 = $('#VoucherDate_Query').closest('td').children();
             Date1.append(' ~ ').append(Date2);           

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
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#VoucherDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));

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
             var JQDate2 = $("#VoucherDate_Query").combo('textbox').val();
          
             var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
             var CostCenterText = "";
             if (CostCenterID != "") {
                 CostCenterText=$("#CostCenterID_Query").combobox('getText');
             } 

             //報表用參數
             var CompanyText = $("#CompanyID_Query").combobox('getText');
             if (CompanyID == "1") {
                 CompanyText = $("#CompanyID_Query").combobox('getValue');
             }

             var url = "../JBGL/REPORT/RglProfitListReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" Span="2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票類別" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:80,remoteName:'sRVoucherList.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherType',textField:'VoucherType',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="VoucherID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" RowSpan="0" Span="3" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="datetime" Editor="datebox" FieldName="VoucherDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="110" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRVoucherList.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
