<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShortTermTest.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermMasterApply.ShortTermMaster" runat="server" AutoApply="True"
                DataMember="ShortTermMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="ShortTermTest">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ShortTermTypeID" Editor="numberbox" FieldName="ShortTermTypeID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermGist" Editor="text" FieldName="ShortTermGist" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TotalAmount" Editor="numberbox" FieldName="TotalAmount" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="ShortTermTest">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTermMaster" HorizontalColumnsCount="2" RemoteName="sShortTermMasterApply.ShortTermMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermTypeID" Editor="numberbox" FieldName="ShortTermTypeID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ShortTermGist" Editor="text" FieldName="ShortTermGist" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TotalAmount" Editor="numberbox" FieldName="TotalAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ShortTermDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sShortTermMasterApply.ShortTermMaster" Title="明細資料" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="ShortTermAmount" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="RequestDate" Editor="datebox" FieldName="RequestDate" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EmployerID" Editor="text" FieldName="EmployerID" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CostNotes" Editor="text" FieldName="CostNotes" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ShortTermDescr" Editor="text" FieldName="ShortTermDescr" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EmployeeIDs" Editor="text" FieldName="EmployeeIDs" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ShortTermNO" ParentFieldName="ShortTermNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ShortTermDetails" HorizontalColumnsCount="2" RemoteName="sShortTermMasterApply.ShortTermMaster" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemNO" Editor="text" FieldName="ItemNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ShortTermAmount" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="RequestDate" Editor="datebox" FieldName="RequestDate" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EmployerID" Editor="text" FieldName="EmployerID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CostNotes" Editor="text" FieldName="CostNotes" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ShortTermDescr" Editor="text" FieldName="ShortTermDescr" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EmployeeIDs" Editor="text" FieldName="EmployeeIDs" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="ShortTermNO" ParentFieldName="ShortTermNO" />
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
