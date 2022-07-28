<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form1.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglVoucherMaster.glVoucherMaster" runat="server" AutoApply="True"
                DataMember="glVoucherMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="Form1">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherDate" Editor="datebox" FieldName="VoucherDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherType" Editor="text" FieldName="VoucherType" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" MaxLength="0" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="Form1">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="glVoucherMaster" HorizontalColumnsCount="2" RemoteName="sglVoucherMaster.glVoucherMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VoucherDate" Editor="datebox" FieldName="VoucherDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VoucherType" Editor="text" FieldName="VoucherType" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="glVoucherDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sglVoucherMaster.glVoucherMaster" Title="明細資料" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="Item" Editor="numberbox" FieldName="Item" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="BorrowLendType" Editor="numberbox" FieldName="BorrowLendType" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="DescribeID" Editor="text" FieldName="DescribeID" Format="" Width="120" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="glVoucherDetails" HorizontalColumnsCount="2" RemoteName="sglVoucherMaster.glVoucherMaster" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="Item" Editor="numberbox" FieldName="Item" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="BorrowLendType" Editor="numberbox" FieldName="BorrowLendType" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="DescribeID" Editor="text" FieldName="DescribeID" Format="" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
