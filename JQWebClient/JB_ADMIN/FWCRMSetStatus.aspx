<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMSetStatus.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMSetStatus.FWCRMSetStatus" runat="server" AutoApply="True"
                DataMember="FWCRMSetStatus" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="iAutoKey" Editor="numberbox" FieldName="iAutoKey" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="進度狀態" Editor="text" FieldName="StatusName" Format="" MaxLength="0" Visible="True" Width="170" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="排序" Editor="numberbox" FieldName="iSort" Format="" Visible="False" Width="50" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="True" Width="110" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="True" Width="110" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Enabled="True" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Enabled="True" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Enabled="True" Visible="True"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Enabled="True" Visible="True" />
                </TooItems>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="挑工狀態維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="FWCRMSetStatus" HorizontalColumnsCount="2" RemoteName="sFWCRMSetStatus.FWCRMSetStatus" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="iAutoKey" Editor="numberbox" FieldName="iAutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="進度狀態" Editor="text" FieldName="StatusName" Format="" maxlength="0" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="排序" Editor="numberbox" FieldName="iSort" Format="" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="110" NewRow="True" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="110" NewRow="True" ReadOnly="True" Visible="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="iAutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />

                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="StatusName" RemoteMethod="True" ValidateMessage="進度狀態 不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
