<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ERPOrderFlowM.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPOrderFlowM.ERPOrderFlowM" runat="server" AutoApply="True"
                DataMember="ERPOrderFlowM" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="訂單流程維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="OrderFlowNO" Format="" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="inforefval" FieldName="ORG_NO" Format="" MaxLength="0" Width="180" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPOrderFlowM.Org',tableName:'Org',columns:[],columnMatches:[],whereItems:[],valueField:'ORG_NO',textField:'ORG_DESC',valueFieldCaption:'ORG_NO',textFieldCaption:'ORG_DESC',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQGridColumn Alignment="left" Caption="訂單名稱" Editor="text" FieldName="OrderFlowName" Format="" MaxLength="0" Width="250" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="訂單流程維護" DialogLeft="30px" DialogTop="30px" Width="720px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPOrderFlowM" HorizontalColumnsCount="3" RemoteName="sERPOrderFlowM.ERPOrderFlowM" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="OrderFlowNO" Format="" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="ORG_NO" Format="" maxlength="0" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPOrderFlowM.Org',tableName:'Org',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="流程名稱" Editor="text" FieldName="OrderFlowName" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPOrderFlowD" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPOrderFlowM.ERPOrderFlowM" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="程序" Editor="numberbox" FieldName="SeqNO" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="程序名稱" Editor="text" FieldName="SeqName" Format="" Width="220" />
                        <JQTools:JQGridColumn Alignment="left" Caption="程序產出" Editor="text" FieldName="SeqOutCome" Format="" Width="220" />
                        <JQTools:JQGridColumn Alignment="right" Caption="銷貨佔比(%)" Editor="numberbox" FieldName="SalesRate" Format="" Width="90" />
                        <JQTools:JQGridColumn Alignment="right" Caption="OrderFlowNO" Editor="numberbox" FieldName="OrderFlowNO" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="OrderFlowNO" ParentFieldName="OrderFlowNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPOrderFlowD" HorizontalColumnsCount="1" RemoteName="sERPOrderFlowM.ERPOrderFlowM" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="OrderFlowNO" Format="" ReadOnly="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="程序" Editor="numberbox" FieldName="SeqNO" Format="" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="接單程序名稱" Editor="textarea" EditorOptions="height:30" FieldName="SeqName" Format="" Width="360" />
                            <JQTools:JQFormColumn Alignment="left" Caption="接單產出名稱" Editor="textarea" FieldName="SeqOutCome" Format="" Width="360" EditorOptions="height:30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額佔比" Editor="numberbox" FieldName="SalesRate" Format="" Width="30" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="OrderFlowNO" ParentFieldName="OrderFlowNO" />
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
