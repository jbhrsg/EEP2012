<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBAST_AssetLocation.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
     $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "lightyellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
     });
     function genCheckBox(val) {
         if (val)
             return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
         else
             return "<input  type='checkbox' onclick='return false;' />";
     }
     function dataGridViewOnDelete() {
         var rowdata = $("#dataGridView").datagrid('getSelected');
         if (rowdata.AssetCou > 0) {
             alert('注意!!此位置已有資產項目,無法刪除')
             return false;
         }
     }
 </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAssetLocation.AssetLocation" runat="server" AutoApply="True"
                DataMember="AssetLocation" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="資產所在位置" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Panel" QueryTop="40px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="1020px" OnDelete="dataGridViewOnDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="AssetLocaID" Format="" Width="40" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="位置名稱" Editor="text" FieldName="AssetLocaName" Format="" MaxLength="0" Width="120" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="資產項目" Editor="text" FieldName="AssetCou" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="位置地址" Editor="text" FieldName="AssetLocaAddr" Format="" MaxLength="0" Width="240" />
                    <JQTools:JQGridColumn Alignment="left" Caption="位置備註" Editor="text" FieldName="AssetLocaNotes" Format="" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="設備保管人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sAssetLocation.OwnerList',tableName:'OwnerList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetOwnerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="順序" Editor="text" FieldName="Seq" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="有效" Editor="checkbox" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建置日期" Editor="datebox" FieldName="AssetLocaEffectDate" Format="yyyy/mm/dd" Width="85" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"  OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                   
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="位置名稱" Condition="%%" DataType="string" Editor="text" FieldName="AssetLocaName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="200" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="資產所在位置" DialogLeft="50px" DialogTop="50px" Width="800px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AssetLocation" HorizontalColumnsCount="1" RemoteName="sAssetLocation.AssetLocation" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="AssetLocaID" Format="" Width="40" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="位置名稱" Editor="text" FieldName="AssetLocaName" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="位置地址" Editor="text" FieldName="AssetLocaAddr" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="位置備註" Editor="text" FieldName="AssetLocaNotes" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備保管人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sAssetLocation.OwnerList',tableName:'OwnerList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetOwnerID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="88" />
                        <JQTools:JQFormColumn Alignment="left" Caption="順序" Editor="text" FieldName="Seq" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="AssetLocaEffectDate" Format="yyyy/mm/dd" Width="90" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="AssetLocaContDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sAssetLocation.AssetLocation" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="AssetLocaContractsNO" Editor="numberbox" FieldName="AssetLocaContractsNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="AssetLocaID" Editor="numberbox" FieldName="AssetLocaID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="LocaContID" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetLocaEffectDate" Editor="datebox" FieldName="AssetLocaEffectDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="AssetLocaID" ParentFieldName="AssetLocaID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                       
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="AssetLocaContDetails" HorizontalColumnsCount="2" RemoteName="sAssetLocation.AssetLocation" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AssetLocaContractsNO" Editor="numberbox" FieldName="AssetLocaContractsNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AssetLocaID" Editor="numberbox" FieldName="AssetLocaID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LocaContID" Editor="text" FieldName="LocaContID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AssetLocaEffectDate" Editor="datebox" FieldName="AssetLocaEffectDate" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="AssetLocaID" ParentFieldName="AssetLocaID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AssetLocaID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="AssetLocaEffectDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetLocaName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
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
