﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_UncollectedInvoiceDetails.aspx.cs" Inherits="Template_JQueryQuery1" %>

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
            //var where = $(dg).datagrid('getWhere');
            //if (where.length > 0) {
            var ReportType = $('#ReportType_Query').options('getValue');
            if (ReportType == "") {
                alert("請選取「報表格式」");
                return false;
            }

            var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');
            var arrSalesID = $("#SalesID_Query").combogrid("getValues");//datebox("getBindingValue");//datebox("getValue");
            var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');

            var SalesDateFrom = $('#SalesDate_Query').datebox('getValue');
            var SalesDateTo = $('#SalesDate_Query[infolight-options*="~"] ').datebox('getValue');
            var CustomerID = $("#CustomerID_Query").refval('getValue');
            var ARDate = $('#ARDate_Query').datebox('getValue');
            var InvoiceDateFrom = $('#InvoiceDate_Query').datebox('getValue');
            var InvoiceDateTo = $('#InvoiceDate_Query[infolight-options*="~"] ').datebox('getValue');
            var MediaSalesDateFrom = $('#MediaSalesDate_Query').datebox('getValue');
            var MediaSalesDateTo = $('#MediaSalesDate_Query[infolight-options*="~"] ').datebox('getValue');
            //var iType = $('#CreateBy_Query').options('getValue');//報表格式	1報告式 2帳戶式
            //var arrInsGroupID = InsGroupID.split(",");
            //var arrSalesTypeID = SalesTypeID.split(",");
            //var arrSalesID = SalesID.split(",");

            for (var i = 0; i < arrInsGroupID.length; i++) {
                arrInsGroupID[i] = "'" + arrInsGroupID[i] + "'";
            }
            var InsGroupID = arrInsGroupID.join("*");

            for (var i = 0; i < arrSalesTypeID.length; i++) {
                arrSalesTypeID[i] = "'" + arrSalesTypeID[i] + "'";
            }
            var SalesTypeID = arrSalesTypeID.join("*");

            for (var i = 0; i < arrSalesID.length; i++) {
                arrSalesID[i] = "'" + arrSalesID[i] + "'";
            }
            var SalesID = arrSalesID.join("*");

            //for (var i = 0; i < arrInsGroupName.length; i++) {
            //    arrInsGroupName[i] = "'" + arrInsGroupName[i] + "'";
            //}
            var InsGroupName = arrInsGroupName.join(" ");
            //var CompanyText = $("#CompanyID_Query").combobox('getText');
            //if (CompanyID == "1") {
            //    CompanyText = $("#CompanyID_Query").combobox('getValue');
            //}

            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_UncollectedInvoiceDetails_ReportView.aspx?InsGroupID=" + InsGroupID + "&SalesTypeID=" + SalesTypeID + "&SalesID=" + SalesID + "&SalesDateFrom=" + SalesDateFrom + "&SalesDateTo=" + SalesDateTo + "&CustomerID=" + CustomerID + "&ARDate=" + ARDate + "&ReportType=" + ReportType + "&InsGroupName=" + InsGroupName + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo + "&MediaSalesDateFrom=" + MediaSalesDateFrom + "&MediaSalesDateTo=" + MediaSalesDateTo;// + "&iType=" + iType + "&CompanyText=" + CompanyText;

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

        function OnSelect_Department(row) {
            $('#SalesID_Query').combogrid('setWhere', "so.ORG_NO='" + row.ORG_NO + "'");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_UncollectedInvoiceDetails.UncollectedInvoiceDetails" runat="server" AutoApply="True"
                DataMember="UncollectedInvoiceDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="1px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="120" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesDate" Editor="datebox" FieldName="SalesDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceDate" Editor="datebox" FieldName="InvoiceDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ARDate" Editor="datebox" FieldName="ARDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Employer" Editor="text" FieldName="Employer" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="120" />--%>
                </Columns>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Report_UncollectedInvoiceDetails.InsGroup',tableName:'InsGroup',valueFieldCaption:'公司別代碼',textFieldCaption:'公司別簡稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Report_UncollectedInvoiceDetails.SalesType',tableName:'SalesType',valueFieldCaption:'銷貨類別代碼',textFieldCaption:'銷貨類別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERP_Report_UncollectedInvoiceDetails.Department',tableName:'Department',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelect_Department,panelHeight:200" FieldName="Department" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" TableName="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Report_UncollectedInvoiceDetails.SalesPerson',tableName:'SalesPerson',valueFieldCaption:'業務員代碼',textFieldCaption:'業務員',selectOnly:false,checkData:false,columns:[{field:'SalesID',title:'工號',width:80,align:'left',sortable:true},{field:'SalesName',title:'姓名',width:80,align:'left',sortable:true},{field:'ORG_DESC',title:'部門',width:80,align:'left',sortable:true}],cacheRelationText:false,multiple:true" FieldName="SalesID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起訖銷貨日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起迄單據日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起訖媒體刊登日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="MediaSalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="MediaSalesDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:550,panelHeight:400,remoteName:'sERP_Report_UncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[{field:'CustomerID',title:'編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶編號',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款截止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="ARDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表格式" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:400,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:4,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'日期',value:'1'},{text:'客戶',value:'2'},{text:'彙總',value:'3'},{text:'媒體',value:'4'}]" FieldName="ReportType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="200" DefaultValue="1" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
