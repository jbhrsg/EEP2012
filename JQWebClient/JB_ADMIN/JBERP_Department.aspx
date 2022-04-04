<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Department.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sDepartment.SYS_ORG" runat="server" AutoApply="True"
                DataMember="SYS_ORG" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="成本中心部門維護" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="部門代號" Editor="text" FieldName="ORG_NO" Format="" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="ORG_DESC" Format="" MaxLength="0" Width="240" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenter" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" MaxLength="0" Width="240" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sDepartment.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ORG_KIND" Editor="text" FieldName="ORG_KIND" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UPPER_ORG" Editor="text" FieldName="UPPER_ORG" Format="" MaxLength="0" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                  <%--  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />--%>
                  <%--    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />--%>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="成本中心部門維護" DialogLeft="50px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SYS_ORG" HorizontalColumnsCount="1" RemoteName="sDepartment.SYS_ORG" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="部門代號" Editor="text" FieldName="ORG_NO" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="ORG_DESC" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" maxlength="0" Width="180" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sDepartment.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ORG_KIND" Editor="text" FieldName="ORG_KIND" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UPPER_ORG" Editor="text" FieldName="UPPER_ORG" Format="" maxlength="0" Width="180" Visible="False" />
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
