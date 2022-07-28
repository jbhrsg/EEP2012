<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RglVoucherList.aspx.cs" Inherits="Template_JQuerySingle1" %>

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

             var Acno1 = $('#Acno_Query').closest('td');
             var Acno2 = $('#Acno2_Query').closest('td').children();
             Acno1.append(' ~ ').append(Acno2);

             var SubAcno1 = $('#SubAcno_Query').closest('td');
             var SubAcno2 = $('#SubAcno2_Query').closest('td').children();
             SubAcno1.append(' ~ ').append(SubAcno2);

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
             var JQDate2 = $("#VoucherDate_Query").combo('textbox').val();

             var Acno1 = $('#Acno_Query').val();
             var Acno2 = $('#Acno2_Query').val();

             var SubAcno1 = $('#SubAcno_Query').val();
             var SubAcno2 = $('#SubAcno_Query').val();

             var VoucherNo = $('#VoucherNo_Query').val();
             var CostCenterID = $("#CostCenterID_Query").combobox('getValue');
             var iType = $('#CreateBy_Query').options('getValue');//呈現種類	1傳票清單 2日記帳

             //報表用參數
             var TypeText = $('#CreateBy_Query').options('getText');//呈現種類	1傳票清單 
             var CompanyText = $("#CompanyID_Query").combobox('getText');
             if (CompanyID == "1") {
                 CompanyText = $("#CompanyID_Query").combobox('getValue');
             }

             var url = "../JBGL/REPORT/RglVoucherListReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
                 "&VoucherNo=" + VoucherNo + "&CostCenterID=" + CostCenterID + "&Acno1=" + Acno1 + "&Acno2=" + Acno2 + "&SubAcno1=" + SubAcno1 + "&SubAcno2=" + SubAcno2+
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="會計科目" Condition="=" DataType="string" Editor="text" FieldName="Acno" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="string" Editor="text" FieldName="Acno2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="子科目" Condition="=" DataType="string" Editor="text" FieldName="SubAcno" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="string" Editor="text" FieldName="SubAcno2" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="60" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票編號" Condition="=" DataType="string" Editor="text" EditorOptions="" FieldName="VoucherNo" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" Span="2" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表類型" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:200,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:6,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'傳票清單',value:'1'},{text:'日記帳',value:'2'},{text:'分類帳',value:'3'}]" FieldName="CreateBy" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="3" Width="200" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
