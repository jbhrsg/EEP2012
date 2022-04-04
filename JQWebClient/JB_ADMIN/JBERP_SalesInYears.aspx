<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesInYears.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSalesInYears.SalesInYears" runat="server" AutoApply="True"
                DataMember="SalesInYears" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="近年銷售客戶排行" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="客戶代號" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="客戶名稱" Format="" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="電話號碼" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1" Editor="text" FieldName="聯絡人1" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1職稱" Editor="text" FieldName="聯絡人1職稱" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1分機" Editor="text" FieldName="聯絡人1分機" Format="" MaxLength="0" Visible="true" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人手機" Editor="text" FieldName="聯絡人手機" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人1信箱" Editor="text" FieldName="聯絡人1信箱" Format="" MaxLength="0" Visible="true" Width="150" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2" Editor="text" FieldName="聯絡人2" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2職稱" Editor="text" FieldName="聯絡人2職稱" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2分機" Editor="text" FieldName="聯絡人2分機" Format="" MaxLength="0" Visible="true" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2手機" Editor="text" FieldName="聯絡人2手機" Format="" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人2信箱" Editor="text" FieldName="聯絡人2信箱" Format="" MaxLength="0" Visible="true" Width="150" />
                    <JQTools:JQGridColumn Alignment="right" Caption="交易金額" Editor="text" FieldName="交易金額" Format="N0" Visible="true" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成交部門" Editor="text" FieldName="成交部門" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="近年銷售客戶排行">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesInYears" HorizontalColumnsCount="2" RemoteName="sSalesInYears.SalesInYears" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="客戶代號" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="客戶名稱" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電話號碼" Editor="text" FieldName="電話號碼" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1" Editor="text" FieldName="聯絡人1" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1職稱" Editor="text" FieldName="聯絡人1職稱" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1分機" Editor="text" FieldName="聯絡人1分機" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人手機" Editor="text" FieldName="聯絡人手機" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人1信箱" Editor="text" FieldName="聯絡人1信箱" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2" Editor="text" FieldName="聯絡人2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2職稱" Editor="text" FieldName="聯絡人2職稱" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2分機" Editor="text" FieldName="聯絡人2分機" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2手機" Editor="text" FieldName="聯絡人2手機" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人2信箱" Editor="text" FieldName="聯絡人2信箱" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易金額" Editor="numberbox" FieldName="交易金額" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成交部門" Editor="text" FieldName="成交部門" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
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
