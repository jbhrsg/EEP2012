<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_PrintReceipt.aspx.cs" Inherits="Template_JQueryQuery1" %>

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
            //var ReportType = $('#ReportType_Query').options('getValue');
            //if (ReportType == "") {
            //    alert("請選取「報表格式」");
            //    return false;
            //}

            //var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');
            //var arrSalesID = $("#SalesID_Query").combogrid("getValues");//datebox("getBindingValue");//datebox("getValue");
            //var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');

            var InvoiceDateFrom = $('#InvoiceDate_Query').datebox('getValue');
            var InvoiceDateTo = $('#InvoiceDate_Query[infolight-options*="~"] ').datebox('getValue');
            var CustomerID = $("#CustomerID_Query").refval('getValue');
            var InvoiceNO = $("#InvoiceNO_Query").combobox('getValue');
            var SalesID = $("#SalesID_Query").combobox('getValue');
            //var iType = $('#CreateBy_Query').options('getValue');//報表格式	1報告式 2帳戶式
            //var arrInsGroupID = InsGroupID.split(",");
            //var arrSalesTypeID = SalesTypeID.split(",");
            //var arrSalesID = SalesID.split(",");

            //var InsGroupID = arrInsGroupID.join("*");
            var SalesTypeID = arrSalesTypeID.join("*");
            //var SalesID = arrSalesID.join("*");
            //var InsGroupName = arrInsGroupName.join(" ");
            //var CompanyText = $("#CompanyID_Query").combobox('getText');
            //if (CompanyID == "1") {
            //    CompanyText = $("#CompanyID_Query").combobox('getValue');
            //}

            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_PrintReceipt_RV.aspx?SalesTypeID=" + SalesTypeID + "&InvoiceDateFrom=" + InvoiceDateFrom + "&InvoiceDateTo=" + InvoiceDateTo + "&CustomerID=" + CustomerID + "&InvoiceNO=" + InvoiceNO + "&SalesID=" + SalesID;// + "&iType=" + iType + "&CompanyText=" + CompanyText;

            var height = $(window).height() - 20;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: 600,//dialog高度
                //top:0,
                width: 850,
                title: "收據",
                //maximizable: true                              
            });
            //dialog裡有iframe，iframe裡有reportviewer, reportviewer就是有工具列那個
            $('<iframe style="border: 1px;border-style:none;border-color:brown" src="' + url + '" width="99%" height="99%"></iframe>').appendTo(dialog.find('.panel-body'));
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_PrintReceipt.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="true" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceDate" Editor="text" FieldName="InvoiceDate" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="收據號碼" Condition="=" DataType="string" Editor="infocombobox" FieldName="InvoiceNO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" EditorOptions="valueField:'InvoiceNO',textField:'InvoiceNO',remoteName:'sERP_Report_PrintReceipt.InvoiceNO',tableName:'InvoiceNO',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <%--<JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="%" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:350,panelHeight:380,remoteName:'sERP_Report_PrintReceipt.Customer',tableName:'Customer',columns:[{field:'TelNO',title:'電話',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerID',title:'客戶代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'ID',textFieldCaption:'簡稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />--%>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:550,panelHeight:400,remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[{field:'CustomerID',title:'編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶編號',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收據日期起迄" Condition="%" DataType="string" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="%" DataType="string" Editor="datebox" FieldName="InvoiceDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="in" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,panelHeight:400,valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Report_PrintReceipt.SalesType',tableName:'SalesType',valueFieldCaption:'銷貨類別代碼',textFieldCaption:'銷貨類別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Report_PrintReceipt.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
