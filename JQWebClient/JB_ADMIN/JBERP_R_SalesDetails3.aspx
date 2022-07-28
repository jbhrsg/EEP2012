<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_R_SalesDetails3.aspx.cs" Inherits="Template_JQuerySingle1" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="EFClientTools" namespace="EFClientTools" tagprefix="cc1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "#FFFFB5");
             });

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             });
             
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 650 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();
             
             //查詢條件預設值
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#AcceptDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             $('#iday_Query').options('setValue',1);
             //$('input:radio[name=iday_Query_0][value=1]').attr('checked', true);          
             //$('#SalesTypeID_Query').combobox('setValue', 1);
             //$('#iCount_Query').checkbox('setValue', 0);
             $('#Class_Query').options('setValue', 1);//類型=>預設為排行榜
         });
                 
         function queryGrid(dg) {//查詢後添加固定條件
             //var where = $(dg).datagrid('getWhere');
             //if (where.length > 0) {
                 var JQDate1 = $("#CreateDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
                 var JQDate2 = $("#AcceptDate_Query").combo('textbox').val();
                 var SalesEmployeeID = $("#SalesID_Query").combobox('getValue');
                 var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');//交易別
                 var arrStats = $('#sStats_Query').combogrid('getValues');//客戶狀態

                 var CustNO = $("#CustNO_Query").combobox('getValue');                
                 var SalesTypeText = $("#SalesTypeID_Query").combobox('getText');//標題            
                 var iClass = $('#Class_Query').options('getValue');//類型 1排行榜	2數據分析	3業務分類 4月份別

                 var url = "../JB_ADMIN/REPORT/Media/JBERP_R_SalesDetailsReportView3.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&SalesEmployeeID=" + SalesEmployeeID + "&SalesTypeID=" + arrSalesTypeID + "&SalesTypeText=" + SalesTypeText + "&CustNO=" + CustNO + "&iClass=" + iClass + "&sStats=" + arrStats;
            
             var height = $(window).height() - 20;
             var width = $(window).width() - 20;
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


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sR_SalesDetails.ERPSalesDetails2" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails2" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/REPORT/Media/JBERP_R_SalesDetails3.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" AllowAdd="True" AllowDelete="True" AllowUpdate="True" CheckOnSelect="True" ColumnsHibeable="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesName" Editor="numberbox" FieldName="SalesName" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ViewAreaName" Editor="text" FieldName="ViewAreaName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="iCount" Editor="text" FieldName="iCount" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="salesQty" Editor="text" FieldName="salesQty" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustAmt" Editor="text" FieldName="CustAmt" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="AcceptDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sR_SalesDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sR_SalesDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,panelHeight:200,valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sR_SalesDetails.infoSalesType2',tableName:'infoSalesType2',valueFieldCaption:'代號',textFieldCaption:'名稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶狀態" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:200,panelHeight:200,valueField:'sName',textField:'sName',remoteName:'sR_SalesDetails.infoStats',tableName:'infoStats',valueFieldCaption:'客戶狀態',textFieldCaption:'客戶狀態',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="sStats" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="類型" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:180,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'排行榜',value:'1'},{text:'數據分析',value:'2'},{text:'銷貨明細',value:'3'},{text:'各月份營業額',value:'4'}]" FieldName="Class" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="225" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
