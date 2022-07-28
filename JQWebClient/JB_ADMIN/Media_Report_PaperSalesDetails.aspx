<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Media_Report_PaperSalesDetails.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>
         function dataGridMaster_OnLoadSuccess() {
             //panel寬度調整
             var dgid = $('#dataGridMaster');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 600 });
             //Grid隱藏
             $('#dataGridMaster').datagrid('getPanel').hide();
         }

         function queryGrid(dg) {//查詢後添加固定條件
             var ReportType = $('#ReportType_Query').options('getValue');
             if (ReportType == "") {
                 alert("請選取「報表格式」");
                 return false;
             }

             var CustNO = $("#CustNO_Query").refval('getValue');
             var arrSalesID = $("#SalesID_Query").combogrid("getValues");
             var SalesDateFrom = $('#SalesDate_Query').datebox('getValue');
             var SalesDateTo = $('#SalesDate_Query[infolight-options*="銷貨日期迄"] ').datebox('getValue');
             //var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');
             var arrNewsTypeID = $("#NewsTypeID_Query").combogrid("getValues");//報別
             var arrNewsAreaID = $("#NewsAreaID_Query").combogrid("getValues");//版別
             var arrNewsPublishID = $("#NewsPublishID_Query").combogrid("getValues");//發稿處
             var ReportType = $('#ReportType_Query').options('getValue');//報表格式
             var GrantTypeID = $('#GrantTypeID_Query').checkbox('getValue');//報表格式
             

             for (var i = 0; i < arrSalesID.length; i++) {
                 arrSalesID[i] = "'" + arrSalesID[i] + "'";
             }
             var SalesID = arrSalesID.join("*");

             for (var i = 0; i < arrNewsTypeID.length; i++) {
                 arrNewsTypeID[i] = "'" + arrNewsTypeID[i] + "'";
             }
             var NewsTypeID = arrNewsTypeID.join("*");

             for (var i = 0; i < arrNewsAreaID.length; i++) {
                 arrNewsAreaID[i] = "'" + arrNewsAreaID[i] + "'";
             }
             var NewsAreaID = arrNewsAreaID.join("*");

             for (var i = 0; i < arrNewsPublishID.length; i++) {
                 arrNewsPublishID[i] = "'" + arrNewsPublishID[i] + "'";
             }
             var NewsPublishID = arrNewsPublishID.join("*");
             
             var url = "../JB_ADMIN/REPORT/Media/Media_Report_PaperSalesDetails_RV.aspx?CustNO=" + CustNO + "&SalesID=" + SalesID + "&SalesDateFrom=" + SalesDateFrom + "&SalesDateTo=" + SalesDateTo + "&NewsTypeID=" + NewsTypeID + "&NewsAreaID=" + NewsAreaID + "&NewsPublishID=" + NewsPublishID + "&ReportType=" + ReportType + "&GrantTypeID=" + GrantTypeID;// + "&iType=" + iType + "&CompanyText=" + CompanyText;

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

         function clearQuery(dgid) {
             var isQueryAutoColumn = getInfolightOption($(dgid)).queryAutoColumn;
             if (isQueryAutoColumn == true) {
                 var pnid = getInfolightOption($(dgid)).queryDialog;
                 var queryTr = $('#queryTr_' + $(dgid)[0].id);
                 var queryParams = $(dgid).datagrid('options').queryParams;
                 var queryWord = new Object();

                 var where = '';
                 $(":text,select", queryTr).each(function () {
                     var text = $(this);
                     text.val('');
                 });
             }

             var pnid = getInfolightOption($(dgid)).queryDialog;
             if (pnid != undefined) {
                 var queryParams = $(dgid).datagrid('options').queryParams;
                 var queryWord = new Object();

                 var where = '';
                 //jbjob edit by serlina 增加textarea
                 $(":text,select,textarea", pnid).each(function () {
                     var text = $(this);
                     text.val('');
                     var controlClass = $(this).attr('class');
                     if (controlClass != undefined) {
                         if (controlClass.indexOf('easyui-datebox') == 0) {
                             text.datebox('setValue', '');
                         }
                         else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                             text.datetimebox('setValue', '');
                         }
                         else if (controlClass.indexOf('info-combobox') == 0) {
                             text.combobox('setValue', '');
                         }
                         else if (controlClass.indexOf('info-combogrid') == 0) {
                             text.combogrid('setValue', '');
                             text.combogrid('clear');
                             text.combogrid('setWhere', '');
                         }
                         else if (controlClass.indexOf('combo-text') == 0) {
                             value = '';
                         }
                         else if (controlClass.indexOf('info-refval') == 0) {
                             text.refval('setValue', '');
                         }
                         else if (controlClass.indexOf('info-autocomplete') == 0) {
                             text.combobox('setValue', '');
                         }
                     }
                 });
                 $(":radio,:checkbox", pnid).each(function () {
                     $(this).prop("checked", false);
                 });
             }
         }
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sMedia_Report_PaperSalesDetails.ERPSalesDetails" runat="server" AutoApply="True"
                DataMember="ERPSalesDetails" Pagination="True" QueryTitle="查詢"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="true" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NewsTypeID" Editor="text" FieldName="NewsTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NewsAreaID" Editor="text" FieldName="NewsAreaID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ReportType" Editor="text" FieldName="ReportType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NewsPublishID" Editor="text" FieldName="NewsPublishID" Format="" MaxLength="0" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="%" DataType="string" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sMedia_Report_CustomerGroupSalesList.infoCustomers',tableName:'infoCustomers',columns:[{field:'CustNO',title:'代碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustTelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustNO',textField:'CustName',valueFieldCaption:'CustNO',textFieldCaption:'CustName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'SalesID',textField:'SalesName',remoteName:'sMedia_Report_CustomerGroupSalesList.infoSalesMan',tableName:'infoSalesMan',valueFieldCaption:'代碼',textFieldCaption:'業務',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨日期起" Condition="%" DataType="string" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨日期迄" Condition="%" DataType="string" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報別" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'NewsTypeID',textField:'NewsTypeName',remoteName:'sMedia_Report_PaperSalesDetails.infoERPNewsType',tableName:'infoERPNewsType',valueFieldCaption:'代碼',textFieldCaption:'報別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="NewsTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="版別" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'NewsAreaID',textField:'NewsAreaName',remoteName:'sMedia_Report_PaperSalesDetails.infoERPNewsArea',tableName:'infoERPNewsArea',valueFieldCaption:'代碼',textFieldCaption:'版別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="NewsAreaID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="發稿處" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'NewsPublishID',textField:'NewsPublishName',remoteName:'sMedia_Report_PaperSalesDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',valueFieldCaption:'代碼',textFieldCaption:'發稿處',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="NewsPublishID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表格式" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:350,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'日期',value:'1'},{text:'報別',value:'2'},{text:'發稿',value:'3'},{text:'客戶',value:'4'},{text:'業務',value:'5'}]" FieldName="ReportType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="不印贈期" Condition="%" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="GrantTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
