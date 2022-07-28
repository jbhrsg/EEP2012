<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_CheckWarrantDetails.aspx.cs" Inherits="Template_JQueryQuery1" %>

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
            
            var CustomerID = $("#CustomerID_Query").refval('getValue');
            var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var WarrantDateFrom = $('#WarrantDate_Query').datebox('getValue');
            var WarrantDateTo = $('#WarrantDate_Query[infolight-options*="~"] ').datebox('getValue');
            var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');
            var arrAccountID = $("#AccountID_Query").combogrid('getValues');

            
            for (var i = 0; i < arrInsGroupID.length; i++) {
                arrInsGroupID[i] = "'" + arrInsGroupID[i] + "'";
            }
            var InsGroupID = arrInsGroupID.join("*");

            for (var i = 0; i < arrAccountID.length; i++) {
                arrAccountID[i] = "'" + arrAccountID[i] + "'";
            }
            var AccountID = arrAccountID.join("*");

            var InsGroupName = arrInsGroupName.join(" ");
            
            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_CheckWarrantDetails_RV.aspx?CustomerID=" + CustomerID + "&InsGroupID=" + InsGroupID + "&WarrantDateFrom=" + WarrantDateFrom + "&WarrantDateTo=" + WarrantDateTo + "&InsGroupName=" + InsGroupName + "&AccountID=" + AccountID;// + "&iType=" + iType + "&CompanyText=" + CompanyText;
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_CheckWarrantDetails.CheckDetails" runat="server" AutoApply="True"
                DataMember="CheckDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="1px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CompanyCustomerID" Editor="text" FieldName="CompanyCustomerID" MaxLength="0" Width="90" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WarrantDate" Editor="text" FieldName="WarrantDate" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Amount" Editor="text" FieldName="Amount" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Width="90" MaxLength="0" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceDate" Editor="text" FieldName="InvoiceDate" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RecAmount" Editor="text" FieldName="RecAmount" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OthAmount" Editor="text" FieldName="OthAmount" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDueDate" Editor="text" FieldName="CheckDueDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckNO" Editor="text" FieldName="CheckNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BankBranchName" Editor="text" FieldName="BankBranchName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>--%>
                </Columns>
                <QueryColumns>
                    <%--<JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:350,panelHeight:400,remoteName:'sERP_Report_CheckWarrantDetails.Customer',tableName:'Customer',columns:[],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'ID',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />--%>
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:550,panelHeight:400,remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[{field:'CustomerID',title:'編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶編號',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="in" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Report_CheckWarrantDetails.InsGroup',tableName:'InsGroup',valueFieldCaption:'公司別代碼',textFieldCaption:'公司別簡稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款日起訖" Condition="=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="支票銀行帳戶" Condition="in" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Report_CheckWarrantDetails.CheckAccount',tableName:'CheckAccount',valueFieldCaption:'代碼',textFieldCaption:'支票銀行帳戶',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="AccountID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
