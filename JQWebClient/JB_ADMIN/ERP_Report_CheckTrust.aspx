<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_CheckTrust.aspx.cs" Inherits="Template_JQueryQuery1" %>

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

            var arrAccountID = $("#AccountID_Query").combogrid('getValues');
            var WarrantDateFrom = $('#WarrantDate_Query').datebox('getValue');
            var WarrantDateTo = $('#WarrantDate_Query[infolight-options*="~"] ').datebox('getValue');
            var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');
            //var CheckDueDateYM = $("CheckDueDateYM_Query").val();
            var CheckDueDateFrom = $('#CheckDueDate_Query').datebox('getValue');
            var CheckDueDateTo = $('#CheckDueDate_Query[infolight-options*="~"] ').datebox('getValue');
            var PrintTrustDate = $("#PrintTrustDate_Query").datebox('getValue');
            var arrActionCode = $("#ActionCode_Query").combogrid('getValues');
            var TrustDateFrom = $('#TrustDate_Query').datebox('getValue');
            var TrustDateTo = $('#TrustDate_Query[infolight-options*="~"] ').datebox('getValue');

            var AccountID = arrAccountID.join("*");
            var InsGroupID = arrInsGroupID.join("*");
            var InsGroupName = arrInsGroupName.join(" ");
            var ActionCode = arrActionCode.join("*");
            
            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_CheckTrust_RV.aspx?AccountID=" + AccountID + "&InsGroupID=" + InsGroupID + "&WarrantDateFrom=" + WarrantDateFrom + "&WarrantDateTo=" + WarrantDateTo + "&InsGroupName=" + InsGroupName + "&CheckDueDateFrom=" + CheckDueDateFrom + "&CheckDueDateTo=" + CheckDueDateTo + "&PrintTrustDate=" + PrintTrustDate + "&ActionCode=" + ActionCode + "&TrustDateFrom=" + TrustDateFrom + "&TrustDateTo=" + TrustDateTo;// + "&iType=" + iType + "&CompanyText=" + CompanyText;
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_CheckTrust.CheckDetails" runat="server" AutoApply="True"
                DataMember="CheckDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="1px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountID" Editor="text" FieldName="AccountID" MaxLength="0" Width="90" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Width="90" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountName" Editor="text" FieldName="AccountName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckAccount" Editor="text" FieldName="CheckAccount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckNO" Editor="text" FieldName="CheckNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BankRootID" Editor="text" FieldName="BankRootID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BankBranchID" Editor="text" FieldName="BankBranchID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BankRootName" Editor="text" FieldName="BankRootName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="BankBranchName" Editor="text" FieldName="BankBranchName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDueDate" Editor="text" FieldName="CheckDueDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="Amount" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="WarrantDate" Editor="text" FieldName="WarrantDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDueDateYM" Editor="text" FieldName="CheckDueDateYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TrustDate" Editor="text" FieldName="TrustDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="支票銀行帳戶" Condition="%" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:340,valueField:'CheckAccountID',textField:'CheckAccountName',remoteName:'sERP_Report_CheckTrust.CheckAccount',tableName:'CheckAccount',valueFieldCaption:'支票銀行帳戶代號',textFieldCaption:'支票銀行帳戶',selectOnly:false,checkData:false,columns:[{field:'CheckAccountID',title:'支票銀行帳戶代號',width:110,align:'left',sortable:false},{field:'CheckAccountName',title:'支票銀行帳戶',width:200,align:'left',sortable:false}],cacheRelationText:false,multiple:true" FieldName="AccountID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款日起訖" Condition="=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="WarrantDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="in" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:330,valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Report_CheckTrust.InsGroup',tableName:'InsGroup',valueFieldCaption:'公司別代碼',textFieldCaption:'公司別簡稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                   <%-- <JQTools:JQQueryColumn AndOr="and" Caption="到期年月" Condition="%" DataType="string" Editor="text" FieldName="CheckDueDateYM" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />--%>
                    <JQTools:JQQueryColumn AndOr="and" Caption="到期日起訖" Condition="=" DataType="datetime" Editor="datebox" FieldName="CheckDueDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="CheckDueDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="印表託收日期" Condition="%" DataType="string" Editor="datebox" FieldName="PrintTrustDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="票據狀態" Condition="in" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:330,valueField:'ActionCode',textField:'ActionName',remoteName:'sERP_Report_CheckTrust.Action',tableName:'Action',valueFieldCaption:'狀態代碼',textFieldCaption:'狀態',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="ActionCode" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="託收日起訖" Condition="=" DataType="datetime" Editor="datebox" FieldName="TrustDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="datetime" Editor="datebox" FieldName="TrustDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
