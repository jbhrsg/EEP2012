<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBAST_AssetQueryByID.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
         var AssetID = Request.getQueryStringByName("AssetID");
         //alert(AssetID);
         //var AssetID = 'A000001'

         $(document).ready(function () {
             //$('#dataFormMasterAssetID').closest('td').prev('td').css({ 'font-size': '40' });
             //$('#dataFormMasterAssetID').css({ 'height': '40' });
             //$('#dataFormMasterAssetID').css({ 'font-size': '40' });
             setTimeout(function () {
                 GetAssetDataByID();
             }, 500);
             $('#div1').hide();
             openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
             //var Filtstr = "CardTimeLastIn>=" + "'" + ControlDateTime + "'"
             //alert(Filtstr);
             //$("#dataGridMaster").datagrid('setWhere', Filtstr);
             //$("#dataGridMaster").datagrid('options').rowStyler = function (index, row) {
             //    if ((row.CardTimeLastIn == '' || row.CardTimeLastIn == undefined || row.CardTimeLastIn >= ControlDateTime))
             //        return 'background-color:pink;color:blue;font-weight:bold;';
             //};
         });
         function dataGridViewOnLoad() {
             openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "viewed", 'dialog');
             //alert('ok');
             //$('#dataGridView').find('.info-datagrid').datagrid('insertRow');
         }
         function GetAssetDataByID() {
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sAssetQueryByID',
                 data: "mode=method&method=" + "GetAssetDataByID" + "&parameters=" + AssetID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                  
                         $('#dataGridView').datagrid('loadData', rows);
                
                     }
                 }
             }
             );
         }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="div1">
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAssetQueryByID.AssetMaster" runat="server" AutoApply="True"
                DataMember="AssetMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="資產物品資訊" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridViewOnLoad">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AssetMasterNO" Editor="numberbox" FieldName="AssetMasterNO" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetID" Editor="text" FieldName="AssetID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetName" Editor="text" FieldName="AssetName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemTypeID" Editor="text" FieldName="ItemTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetUnit" Editor="text" FieldName="AssetUnit" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetGetType" Editor="text" FieldName="AssetGetType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetSpecs" Editor="text" FieldName="AssetSpecs" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetGetDate" Editor="datebox" FieldName="AssetGetDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetGetYM" Editor="text" FieldName="AssetGetYM" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="UsefulYears" Editor="numberbox" FieldName="UsefulYears" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AssetQty" Editor="numberbox" FieldName="AssetQty" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetPlace" Editor="text" FieldName="AssetPlace" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetPhotoPath" Editor="text" FieldName="AssetPhotoPath" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PONO" Editor="text" FieldName="PONO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TranTypeID" Editor="text" FieldName="TranTypeID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CompID" Editor="numberbox" FieldName="CompID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OwnerID" Editor="text" FieldName="OwnerID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LocalID" Editor="text" FieldName="LocalID" Format="" MaxLength="0" Width="120" />
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
            </div>
       
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="資產物品資訊" DialogLeft="10px" DialogTop="10px" Width="390px">
            
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AssetMaster" HorizontalColumnsCount="1" RemoteName="sAssetMaster.AssetMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                  
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備名稱" Editor="text" FieldName="AssetName" Format="" maxlength="0" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備規格" Editor="text" FieldName="AssetSpecs" Format="" maxlength="0" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備類別" Editor="infocombobox" FieldName="ItemTypeID" Format="" maxlength="0" Width="185" Visible="False" EditorOptions="valueField:'ITEMTYPEID',textField:'ITEMTYPENAME',remoteName:'sAssetQueryByID.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="text" FieldName="AssetUnit" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="取得方式" Editor="infocombobox" FieldName="AssetGetType" Format="" maxlength="0" Width="183" EditorOptions="valueField:'AssetGetType',textField:'AssetGetType',remoteName:'sAssetQueryByID.AssetGetType',tableName:'AssetGetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Format="yyyy/mm/dd" maxlength="0" Width="183" />
                        <JQTools:JQFormColumn Alignment="left" Caption="取得年月" Editor="text" FieldName="AssetGetYM" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="使用年限" Editor="numberbox" FieldName="UsefulYears" Format="" maxlength="0" Width="180" EditorOptions="precision:1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="AssetQty" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="存放位置" Editor="text" FieldName="AssetPlace" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetPhotoPath" Editor="text" FieldName="AssetPhotoPath" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購單號" Editor="text" FieldName="PONO" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區位位置" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetQueryByID.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LocalID" Format="" maxlength="0" Width="183" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管人員" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetQueryByID.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OwnerID" Format="" Width="183" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務歸屬" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetQueryByID.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompID" Format="" maxlength="0" Width="183" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資產狀態" Editor="infocombobox" FieldName="TranTypeID" Format="" maxlength="0" Width="183" Visible="True" EditorOptions="valueField:'TranTypeID',textField:'AssetStatusName',remoteName:'sAssetQueryByID.AssetTranType',tableName:'AssetTranType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="StatusID" Editor="numberbox" FieldName="StatusID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RecNO" Editor="numberbox" FieldName="RecNO" Format="" Width="180" Visible="False" />
                    </Columns>
                  
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
       
     
    </form>
</body>
</html>
