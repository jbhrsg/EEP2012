<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DispatchArea.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPDispatchArea.ERPDispatchAreaID" runat="server" AutoApply="True"
                DataMember="ERPDispatchAreaID" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="派遣區域" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="600px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="DispatchAreaNO" Editor="numberbox" FieldName="DispatchAreaNO" Format="" Width="40" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="DispatchAreaID" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="區域名稱" Editor="text" FieldName="DispatchAreaName" Format="" MaxLength="0" Width="240" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽核主管" Editor="infocombobox" FieldName="DispatchAreaManager" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPDispatchArea.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="派遣區域" DialogLeft="30px" DialogTop="50px" Width="450px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPDispatchAreaID" HorizontalColumnsCount="1" RemoteName="sERPDispatchArea.ERPDispatchAreaID" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="450px" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="DispatchAreaNO" Editor="numberbox" FieldName="DispatchAreaNO" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區域代號" Editor="numberbox" FieldName="DispatchAreaID" Format="" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區域名稱" Editor="text" FieldName="DispatchAreaName" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核主管" Editor="infocombobox" FieldName="DispatchAreaManager" Format="" maxlength="0" Width="123" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPDispatchArea.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
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
