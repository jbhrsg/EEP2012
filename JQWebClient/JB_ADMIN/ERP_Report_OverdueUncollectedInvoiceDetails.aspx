<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Report_OverdueUncollectedInvoiceDetails.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        var UserID = getClientInfo("UserID");
        function dataGridMaster_OnLoadSuccess() {
            //panel寬度調整
            setTimeout(function () {
                $('#SalesTypeID_Query').combogrid('setWhere', "A.SalesID = '" + UserID + "'");
            }, 2000);
            var dgid = $('#dataGridMaster');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 600 });
            //Grid隱藏
            $('#dataGridMaster').datagrid('getPanel').hide();
        }
        function queryGrid(dg) {//查詢後添加固定條件
            var strSalesTypeID = GetUserSalesTypesStr(UserID);
            if (strSalesTypeID.length <= 0) {
                alert('注意!!你無查詢權限,請洽會計室或管理室...');
                return false;
            }
            var arrInsGroupID = $("#InsGroupID_Query").combogrid('getValues');
            var arrSalesTypeID = $('#SalesTypeID_Query').combogrid('getValues');
            var arrSalesID = $("#SalesID_Query").combogrid("getValues");//datebox("getBindingValue");//datebox("getValue");
            var arrInsGroupName = $("#InsGroupID_Query").combogrid('getText').split(',');
            var CustomerID = $("#CustomerID_Query").refval('getValue');
            var ARDate = $('#ARDate_Query').datebox('getValue');
            //var arrSalesTypeID = ['01','06'];
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
            if (arrSalesTypeID == '') {
                strSalesTypeID = GetUserSalesTypesStr(UserID);
                arrSalesTypeID = strSalesTypeID.split(",");
            }
            var SalesTypeID = arrSalesTypeID.join("*");
            for (var i = 0; i < arrSalesID.length; i++) {
                arrSalesID[i] = "'" + arrSalesID[i] + "'";
            }
            var SalesID = arrSalesID.join("*");
            var InsGroupName = arrInsGroupName.join(" ");
            var url = "../JB_ADMIN/REPORT/ERP/ERP_Report_OverdueUncollectedInvoiceDetails_RV.aspx?InsGroupID=" + InsGroupID + "&SalesTypeID=" + SalesTypeID + "&SalesID=" + SalesID + "&CustomerID=" + CustomerID + "&ARDate=" + ARDate + "&InsGroupName=" + InsGroupName;// + "&iType=" + iType + "&CompanyText=" + CompanyText;
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
        //取得使用者銷貨類別權限字串
        function GetUserSalesTypesStr(UserID) {
            var UserSalesTypesStr = "";
             $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Report_OverdueUncollectedInvoiceDetails.InvoiceDetails',
                data: "mode=method&method=" + "GetUserSalesTypesStr" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        UserSalesTypesStr = rows[0].UserSalesTypesStr;
                    }
                }
            }
            );
             return UserSalesTypesStr;
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
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERP_Report_OverdueUncollectedInvoiceDetails.InvoiceDetails" runat="server" AutoApply="True"
                DataMember="InvoiceDetails" Pagination="True" QueryTitle="查詢條件"
                Title="" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="False" AllowAdd="False" ViewCommandVisible="False" OnLoadSuccess="dataGridMaster_OnLoadSuccess" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="1px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" MaxLength="0" Width="90" />
                    <%--<JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceDate" Editor="text" FieldName="InvoiceDate" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesDate" Editor="text" FieldName="SalesDate" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ARDate" Editor="text" FieldName="ARDate" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTotal" Editor="text" FieldName="SalesTotal" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AcceptedAmount" Editor="text" FieldName="AcceptedAmount" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UncollectedAmount" Editor="text" FieldName="UncollectedAmount" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="InsGroupID" Editor="text" FieldName="InsGroupID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="TrackNote" Editor="text" FieldName="TrackNote" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>--%>
                </Columns>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="number" Editor="infocombogrid" EditorOptions="panelWidth:350,panelHeight:350,valueField:'InsGroupID',textField:'ShortName',remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.InsGroup',tableName:'InsGroup',valueFieldCaption:'公司別代碼',textFieldCaption:'公司別簡稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="InsGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'客戶',panelWidth:550,panelHeight:400,remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.Customer',tableName:'Customer',columns:[{field:'CustomerID',title:'編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ShortName',title:'簡稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustomerName',title:'名稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TelNO',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxNO',title:'統編',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CustomerID',textField:'ShortName',valueFieldCaption:'客戶編號',textFieldCaption:'簡稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="銷貨類別" Condition="=" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,panelHeight:350,valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Report_OverdueUncollectedInvoiceDetails.SalesType',tableName:'SalesType',valueFieldCaption:'銷貨類別代碼',textFieldCaption:'銷貨類別',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesTypeID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="業務" Condition="=" DataType="string" Editor="infocombogrid" EditorOptions="panelWidth:350,panelHeight:350,valueField:'SalesID',textField:'SalesName',remoteName:'sERP_Report_UncollectedInvoiceDetails.SalesPerson',tableName:'SalesPerson',valueFieldCaption:'業務員代碼',textFieldCaption:'業務員',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="SalesID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="收款截止日" Condition="=" DataType="datetime" Editor="datebox" FieldName="ARDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
