<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_SYS_Variable.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSYS_Variable.SYS_Variable" runat="server" AutoApply="True"
                DataMember="SYS_Variable" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="系統變數設定" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="編號" Editor="numberbox" FieldName="iAutokey" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分類代號" Editor="text" FieldName="Category" Format="" MaxLength="0" Width="200" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="變數名稱" Editor="text" FieldName="Title" Format="" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="變數值" Editor="text" FieldName="CategoryValue" Format="" MaxLength="0" Width="520" />
                </Columns>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="系統變數設定">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SYS_Variable" HorizontalColumnsCount="1" RemoteName="sSYS_Variable.SYS_Variable" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="分類代號" Editor="text" FieldName="Category" Format="" maxlength="0" Width="200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="變數名稱" Editor="text" FieldName="Title" Format="" maxlength="0" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="變數值" Editor="text" FieldName="CategoryValue" Format="" maxlength="0" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
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
