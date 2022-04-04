<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_PreOrderM.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPPreOrderM.ERPPreOrderM" runat="server" AutoApply="True"
                DataMember="ERPPreOrderM" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="預估訂單登錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="編號" Editor="numberbox" FieldName="PreOrderNO" Format="" Width="30" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPPreOrderM.Org',tableName:'Org',columns:[],columnMatches:[],whereItems:[],valueField:'ORG_NO',textField:'ORG_DESC',valueFieldCaption:'ORG_NO',textFieldCaption:'ORG_DESC',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" FieldName="Org_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="inforefval" FieldName="CustNO" Format="" MaxLength="0" Width="120" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPPreOrderM.Customers',tableName:'Customers',columns:[],columnMatches:[],whereItems:[],valueField:'CustNO',textField:'CustShortName',valueFieldCaption:'CustNO',textFieldCaption:'CustShortName',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:false" />
                    <JQTools:JQGridColumn Alignment="left" Caption="訂單內容" Editor="text" FieldName="PreOrderDescr" Format="" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="下單日期" Editor="datebox" FieldName="PreOrderDate" Format="yyyy/mm/dd" Width="70" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="right" Caption="OrderFlowNO" Editor="numberbox" FieldName="OrderFlowNO" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="numberbox" FieldName="UnitPrice" Format="" Width="60" />
                    <JQTools:JQGridColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="OrderQty" Format="" Width="30" />
                    <JQTools:JQGridColumn Alignment="right" Caption="訂單金額" Editor="numberbox" FieldName="OrderAmt" Format="" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計銷貨日" Editor="datebox" FieldName="PreSalesDate" Format="yyyy/mm/dd" Width="70" FormatScript="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="目前進度" Editor="text" FieldName="NowStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd" Width="70" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="預估訂單登錄" DialogLeft="30px" DialogTop="30px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPPreOrderM" HorizontalColumnsCount="3" RemoteName="sERPPreOrderM.ERPPreOrderM" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="PreOrderNO" Format="" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶" Editor="infocombobox" FieldName="CustNO" Format="" maxlength="0" Width="100" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPPreOrderM.Customers',tableName:'Customers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下單日期" Editor="datebox" FieldName="PreOrderDate" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單流程" Editor="infocombobox" EditorOptions="valueField:'OrderFlowNO',textField:'OrderFlowName',remoteName:'sERPPreOrderM.OrderFlow',tableName:'OrderFlow',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OrderFlowNO" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單內容" Editor="text" FieldName="PreOrderDescr" Format="" maxlength="0" Width="280" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單價" Editor="numberbox" FieldName="UnitPrice" Format="" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="OrderQty" Format="" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總金額" Editor="numberbox" FieldName="OrderAmt" Format="" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計銷貨日" Editor="datebox" FieldName="PreSalesDate" Format="" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPPreOrderD" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPPreOrderM.ERPPreOrderM" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="PreOrderNO" Editor="numberbox" FieldName="PreOrderNO" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="OrderFlowNO" Editor="numberbox" FieldName="OrderFlowNO" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="程序" Editor="numberbox" FieldName="SeqNO" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="程序名稱" Editor="text" FieldName="SEQNAME" Width="180" />
                        <JQTools:JQGridColumn Alignment="right" Caption="銷貨佔比(%)" Editor="numberbox" FieldName="SalesRate" Format="" Width="80" />
                        <JQTools:JQGridColumn Alignment="right" Caption="預估銷貨金額" Editor="numberbox" FieldName="SalesAmt" Format="" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="完成日期" Editor="datebox" FieldName="SeqFinishDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PreOrderNO" ParentFieldName="PreOrderNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="30px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPPreOrderD" HorizontalColumnsCount="2" RemoteName="sERPPreOrderM.ERPPreOrderM" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="OrderFlowNO" Editor="numberbox" FieldName="OrderFlowNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PreOrderNO" Editor="numberbox" FieldName="PreOrderNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="程序" Editor="numberbox" FieldName="SeqNO" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                            <JQTools:JQFormColumn Alignment="left" Caption="流程名稱" Editor="text" FieldName="SEQNAME" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨佔比" Editor="numberbox" FieldName="SalesRate" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額" Editor="numberbox" FieldName="SalesAmt" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="完成日期" Editor="datebox" FieldName="SeqFinishDate" Format="" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PreOrderNO" ParentFieldName="PreOrderNO" />
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
