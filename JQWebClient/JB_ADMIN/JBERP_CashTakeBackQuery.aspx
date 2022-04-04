<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CashTakeBackQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        $(function () {
            $('#SetAccountDate_Query').closest('td').next('td').next('td').append("&nbsp;&nbsp;&nbsp;&nbsp;");
            $('#SetAccountDate_Query').closest('td').next('td').next('td').append($('.infosysbutton-q', '#querydataGridMaster').closest('td').contents());
        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sCashTakeBackQuery.CashTakeBackDetails" runat="server" AutoApply="True"
                DataMember="CashTakeBackDetails" Pagination="True" QueryTitle="查詢條件"
                Title="現金匯款繳回查詢" AllowDelete="False" AllowInsert="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="False" AllowAdd="False" ViewCommandVisible="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="現金繳回單號" Editor="text" FieldName="CashTakeBackNO" Format="" MaxLength="20" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請員工姓名" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="ApplyNotes" Format="" MaxLength="256" Width="700" />
                    <JQTools:JQGridColumn Alignment="right" Caption="申請金額" Editor="numberbox" FieldName="TotalAmount" Format="" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="繳回入帳日期" Editor="datebox" FieldName="SetAccountDate" Format="" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="EXCEL輸出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="繳回入帳日期" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="SetAccountDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="SetAccountDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

    </form>
</body>
</html>
