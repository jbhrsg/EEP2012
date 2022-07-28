<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_R_SalesDetails.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             $("#AcceptDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             $('#iday_Query').options('setValue', 1);
             $('#ViewAreaName_Query').options('setValue', 1);
             //$('input:radio[name=iday_Query_0][value=1]').attr('checked', true);      
             $('#SalesTypeID_Query').combobox('setValue', 1);

         }

         function queryGrid(dg) {//查詢後添加固定條件
             //var where = $(dg).datagrid('getWhere');
             //if (where.length > 0) {
                 var JQDate1 = $("#CreateDate_Query").datebox("getValue");//datebox("getBindingValue");//datebox("getValue");                
                 var JQDate2 = $("#AcceptDate_Query").combo('textbox').val();
                 var SalesEmployeeID = $("#SalesID_Query").combobox('getValue');                 
                 var SalesTypeID = $("#SalesTypeID_Query").combobox('getValue');//交易別
                 var NewsTypeID = $("#NewsTypeID_Query").combobox('getValue');//報別
                 var CustNO = $("#CustNO_Query").combobox('getValue');
                 var Sort = $('#iday_Query').options('getValue');
                 var iType = $('#ViewAreaName_Query').options('getValue');//呈現種類	1訂單 2見刊
                 var SalesTypeText = $("#SalesTypeID_Query").combobox('getText');//標題
                 var IsAcceptePaper = $("#Amt_Query").combobox('getValue');//挑報 0不拘	1紙本 2電子檔

                 if (SalesTypeID == "") {
                     SalesTypeText = " ";
                 }
                 var url = "../JB_ADMIN/REPORT/Media/JBERP_R_SalesDetailsReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&SalesEmployeeID=" + SalesEmployeeID + "&SalesTypeID=" + SalesTypeID + "&SalesTypeText=" + SalesTypeText + "&NewsTypeID=" + NewsTypeID + "&CustNO=" + CustNO + "&Sort=" + Sort + "&iType=" + iType + "&IsAcceptePaper=" + IsAcceptePaper;
            
                 var height = $(window).height() - 40;
                 var height2 = $(window).height() - 80;
                 var width = $(window).width() - 60;
                 var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 //top:0,
                 width: width,
                 title: "統計表",
                 //maximizable: true                              
             });
             $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
             dialog.dialog('open');
        
         }
         
    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />


&nbsp;<div id="Dialog_dataGridMasterData">
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sR_SalesDetails.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" ReportFileName="~/JB_ADMIN/JBERP_R_SalesDetails.rdlc" QueryMode="Panel" DeleteCommandVisible="False" UpdateCommandVisible="False" ViewCommandVisible="False" AlwaysClose="True" OnLoadSuccess="OnLoadSuccessGV" >
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustShortName" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DMTypeID" Editor="text" FieldName="DMTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesQty" Editor="numberbox" FieldName="SalesQty" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AcceptDate" Editor="datebox" FieldName="AcceptDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="iday" Editor="text" FieldName="iday" Format="" MaxLength="0" Width="120" />
                </Columns>                 
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="AcceptDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sR_SalesDetails.infoSalesMan',tableName:'infoSalesMan',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶代號" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sR_SalesDetails.infoCustomersAll',tableName:'infoCustomersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CustNO" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="交易別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sR_SalesDetails.infoSalesTypeAll',tableName:'infoSalesTypeAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'NewsTypeID',textField:'NewsTypeName',remoteName:'sR_SalesDetails.infoERPNewsType',tableName:'infoERPNewsType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="NewsTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="排序方式" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'客戶代號',value:'1'},{text:'建立日期',value:'2'}]" FieldName="iday" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="挑報" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'9',text:'==不拘==',selected:'true'},{value:'2',text:'紙本',selected:'false'},{value:'1',text:'電子檔',selected:'false'},{value:'3',text:'Line',selected:'false'},{value:'0',text:'未確認',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Amt" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="呈現種類" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:110,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'訂單',value:'1'},{text:'見刊',value:'2'}]" FieldName="ViewAreaName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>
        </div>
</form>
</body>
</html>
