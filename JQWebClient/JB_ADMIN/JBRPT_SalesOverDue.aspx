<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRPT_SalesOverDue.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var SalesNO = Request.getQueryStringByName("sales");
        var SalesName = Request.getQueryStringByName("name");
        var CDate = Request.getQueryStringByName("date");
        $(document).ready(function () {
            setTimeout(function () {
            var FiltStr = "salesNO='" + SalesNO + "' AND ARDate <= '" + CDate + "'";
            $('#dataGridView').datagrid('setWhere', FiltStr);
            }, 300);
            //$("#dataGridMaster").datagrid('options').rowStyler = function (index, row) {
            //    if (row.IsErrHint == true) {
            //        return 'background-color:pink;color:blue;font-weight:bold;';
            //    }
            //};
        });
        function dataGridViewOnLoadSucess() {
            var CenterName = '逾期帳款 截止日期:' + CDate + '&emsp;&emsp;&emsp;業務人員:' + SalesName;
            $("#dataGridView").datagrid('getPanel').panel('setTitle', CenterName);
            $("#dataGridView").datagrid('options').title = CenterName;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSalesOverDue.SalesDetails" runat="server" AutoApply="True"
                DataMember="SalesDetails" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="逾期帳款明細" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="1080px" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="ShortName" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話" Editor="text" FieldName="TelNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="單據日期" Editor="datebox" FieldName="InvoiceDate" Format="yyyy/mm/dd" Width="75" />
                    <JQTools:JQGridColumn Alignment="center" Caption="發票號碼" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Width="90" Total="count" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrackNote" Editor="text" FieldName="TrackNote" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy/mm/dd" Width="75" />
                    <JQTools:JQGridColumn Alignment="left" Caption="應收款日" Editor="datebox" FieldName="ARDate" Format="" Width="75" />
                    <JQTools:JQGridColumn Alignment="right" Caption="應收金額" Editor="numberbox" FieldName="SalesTotal" Format="N0" Width="75" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已沖金額" Editor="numberbox" FieldName="AcceptedAmount" Format="N0" Width="75" Visible="True" Total="sum" />
                    <JQTools:JQGridColumn Alignment="right" Caption="未收金額" Editor="numberbox" FieldName="UncollectedAmount" Format="N0" Width="75" Total="sum" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"  Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出Excel" />
                  <%--  <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="逾期帳款查詢">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesDetails" HorizontalColumnsCount="2" RemoteName="sSalesOverDue.SalesDetails" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortName" Editor="text" FieldName="ShortName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InvoiceDate" Editor="datebox" FieldName="InvoiceDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesDate" Editor="datebox" FieldName="SalesDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ARDate" Editor="datebox" FieldName="ARDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesTotal" Editor="numberbox" FieldName="SalesTotal" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AcceptedAmount" Editor="numberbox" FieldName="AcceptedAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UncollectedAmount" Editor="numberbox" FieldName="UncollectedAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TrackNote" Editor="text" FieldName="TrackNote" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TelNO" Editor="text" FieldName="TelNO" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
