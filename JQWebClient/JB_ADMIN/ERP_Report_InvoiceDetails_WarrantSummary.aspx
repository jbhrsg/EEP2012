﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_InvoiceDetails_WarrantSummary.aspx.cs" Inherits="Template_JQueryQuery1" %>

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
                alert("請選取「資料內容」");
                return false;
            }

            var CustomerID = $("#CustomerID_Query").refval('getValue');
            var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');
            var SalesDate = $('#SalesDate_Query').datebox('getValue');
            var ARDate = $('#ARDate_Query').datebox('getValue');

            var InsGroupID=arrInsGroupID.join("*");
            var InsGroupName = arrInsGroupName.join(" ");

            //var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails_WarrantSummary_RV.aspx?CustomerID=" + CustomerID +"&InsGroupID=" + InsGroupID + "&SalesDate=" + SalesDate + "&ARDate=" + ARDate + "&ReportType=" + ReportType + "&InsGroupName=" + InsGroupName;// + "&iType=" + iType + "&CompanyText=" + CompanyText;
            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_InvoiceDetails_WarrantSummary_RV.aspx?CustomerID=" + CustomerID + "&InsGroupID=" + InsGroupID + "&SalesDate=" + SalesDate + "&ARDate=" + ARDate + "&ReportType=" + ReportType + "&InsGroupName=" + InsGroupName;// + "&iType=" + iType + "&CompanyText=" + CompanyText;
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_InvoiceDetails_WarrantSummary.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="1px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" MaxLength="0" Width="90" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTotal" Editor="text" FieldName="SalesTotal" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AcceptedAmount" Editor="text" FieldName="AcceptedAmount" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UncollectedAmount" Editor="text" FieldName="UncollectedAmount" MaxLength="0" Width="90" />--%>
                </Columns>
                <QueryColumns>
                    <%--<JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:350,panelHeight:400,remoteName:'sERP_Report_UncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'ID',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />--%>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:550,panelHeight:400,remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[{field:'CustomerID',title:'編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶編號',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Report_UncollectedInvoiceDetails.InsGroup',tableName:'InsGroup',valueFieldCaption:'公司別代碼',textFieldCaption:'公司別簡稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨截止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="SalesDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款截止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="ARDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="資料內容" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'全部',value:'1'},{text:'未收',value:'2'}]" FieldName="ReportType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="125" DefaultValue="1" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>