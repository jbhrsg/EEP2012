<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TEST.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sOrdersBase.ORDERSBASE" runat="server" AutoApply="True"
                DataMember="ORDERSBASE" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="TEST">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="OrdersBaseID" Editor="text" FieldName="OrdersBaseID" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="EmployerID" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="ApplyBillID" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="DueDate" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="NO" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="人數" Editor="numberbox" FieldName="Nums" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="APPLYTYPENAME" Editor="text" FieldName="APPLYTYPENAME" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EMPLOYERNAME" Editor="text" FieldName="EMPLOYERNAME" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="待確認人數" Editor="numberbox" FieldName="OnWayNums" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已確認人數" Editor="numberbox" FieldName="CommitNums" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="TEST">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ORDERSBASE" HorizontalColumnsCount="2" RemoteName="sOrdersBase.ORDERSBASE" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="OrdersBaseID" Editor="text" FieldName="OrdersBaseID" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="EmployerID" maxlength="20" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="ApplyBillID" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="DueDate" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="函號" Editor="text" FieldName="NO" maxlength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="人數" Editor="numberbox" FieldName="Nums" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" maxlength="50" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="APPLYTYPENAME" Editor="text" FieldName="APPLYTYPENAME" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EMPLOYERNAME" Editor="text" FieldName="EMPLOYERNAME" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="待確認人數" Editor="numberbox" FieldName="OnWayNums" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已確認人數" Editor="numberbox" FieldName="CommitNums" Width="180" />
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
