<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Setting_Item.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        $(function () {
            $("#querydataGridView").find(".infosysbutton-q").closest('td').attr({ 'align': 'middle' });
            $('.infosysbutton-q', '#querydataGridView0').closest('td').attr({ 'align': 'middle' });
        });

        function dataGridView_OnLoadSuccess() {
            $("#dataGridView").datagrid("selectRow", -1);
        }

        
        function dataGridView0_OnLoadSuccess() {
            //物品類別刪除後，要重load物品的查詢的物品類別(刪除物品類別，會觸發dataGridView0_OnLoadSuccess)
            $("#ItemTypeID_Query").combobox('reload');
        }

        function dataFormMaster0_OnLoadSuccess() {
            if(getEditMode($('#dataFormMaster0'))=="inserted"){
                //為了新增完物品類別資料，就要新增物品，
                $("#dataFormMaster0ItemTypeID").combobox('reload');
                
                //新增物品，類別抓類別資料選的資料列
                var selectdata = $("#dataGridView").datagrid('getSelected');
                if (selectdata != null) {
                    $("#dataFormMaster0ItemTypeID").combobox('setValue', selectdata.ItemTypeID);
                } 
                //else {
                //    alert("先選取物品類別");
                //    closeForm('#JQDialog2');
                //}
            }
        }
        function dataGridView_OnSelect(index,rowdata) {
            $("#dataGridView0").datagrid("setWhere", "ItemTypeID='" + rowdata.ItemTypeID + "'");
        }
        function dataFormMaster0_OnApplied() {
            //物品新增完後，物品重load資料
            var ItemTypeID = $("#dataFormMaster0ItemTypeID").combobox('getValue');
            $("#dataGridView0").datagrid("setWhere", "ItemTypeID='" + ItemTypeID + "'");
        }
        function dataFormMaster_OnApply() {
            //類別名稱重複檢查
            if (getEditMode($('#dataFormMaster')) == 'inserted') {//新增時檢查是否重複
                var flag1 = true;
                var ItemTypeName = $.trim($("#dataFormMasterItemTypeName").val());
                if (ItemTypeName != '') {//檢查有無重複
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Setting_Item.ItemType', //連接的Server端，command
                        data: "mode=method&method=" + "CheckDuplicate_ItemTypeName" + "&parameters=" + ItemTypeName, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != 'False') {
                                var rows = $.parseJSON(data);
                                if (rows.length != 0) {
                                    alert("此類別名稱已存在");
                                    flag1 = false;
                                }
                            } else {
                                alert("檢查類別名稱錯誤");
                                flag1 = false;
                            }
                        }
                    });
                }
                if (flag1 == false) { return false }
            }
        }
        function dataFormMaster0_OnApply() {
            //物品名稱重複檢查
            if (getEditMode($('#dataFormMaster')) == 'inserted') {//新增時檢查是否重複
                var flag1 = true;
                var ItemName = $.trim($("#dataFormMaster0ItemName").val());
                if (ItemName != '') {//檢查有無重複
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Setting_Item.ItemType', //連接的Server端，command
                        data: "mode=method&method=" + "CheckDuplicate_ItemName" + "&parameters=" + ItemName, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != 'False') {
                                var rows = $.parseJSON(data);
                                if (rows.length != 0) {
                                    alert("此物品名稱已存在");
                                    flag1 = false;
                                }
                            } else {
                                alert("檢查物品名稱錯誤");
                                flag1 = false;
                            }
                        }
                    });
                }
                if (flag1 == false) { return false }
            }
        }

        function dataGridView_OnDelete(rowdata) {
            //物品名稱有無資料檢查
                var flag1 = true;
                if (rowdata.ItemTypeID != '') {//檢查有無資料
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Setting_Item.ItemType', //連接的Server端，command
                        data: "mode=method&method=" + "CheckItems" + "&parameters=" + rowdata.ItemTypeID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != 'False') {
                                var rows = $.parseJSON(data);
                                if (rows.length != 0) {
                                    alert("此類別的物品已存在，故無法刪除");
                                    flag1 = false;
                                }
                            } else {
                                alert("檢查該類的物品有無資料錯誤");
                                flag1 = false;
                            }
                        }
                    });
                }
                if (flag1 == false) { return false }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Setting_Item.ItemType" runat="server" AutoApply="True"
                DataMember="ItemType" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="物品類別" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="60px" QueryMode="Window" QueryTop="30px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnSelect="dataGridView_OnSelect" OnLoadSuccess="dataGridView_OnLoadSuccess" OnDelete="dataGridView_OnDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="類別編號" Editor="text" FieldName="ItemTypeID" Format="" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="類別名稱" Editor="text" FieldName="ItemTypeName" Format="" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="負責職稱" Editor="text" FieldName="GROUPNAME" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="true" Width="70" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="類別名稱" Condition="%%" DataType="string" Editor="text" FieldName="ItemTypeName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" TableName="it" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="負責職稱" Condition="%%" DataType="string" Editor="text" FieldName="GROUPNAME" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" TableName="g" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="物品類別">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ItemType" HorizontalColumnsCount="2" RemoteName="sERP_Setting_Item.ItemType" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMaster_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="類別編號" Editor="text" FieldName="ItemTypeID" maxlength="0" Width="80" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="類別名稱" Editor="text" FieldName="ItemTypeName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責職稱" Editor="infocombobox" FieldName="ResponsibleGROUPID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="183" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sERP_Setting_Item.ResponsibleGROUPID',tableName:'ResponsibleGROUPID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ItemTypeID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ItemTypeName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <JQTools:JQDataGrid ID="dataGridView0" data-options="pagination:true,view:commandview" RemoteName="sERP_Setting_Item.Item" runat="server" AutoApply="True"
                DataMember="Item" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog2"
                Title="物品" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="60px" QueryMode="Window" QueryTop="300px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="dataGridView0_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="物品編號" Editor="text" FieldName="ItemID" MaxLength="0" Visible="true" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="ItemName" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="物品類別" Editor="infocombobox" FieldName="ItemTypeID" MaxLength="0" Visible="True" Width="180" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sERP_Setting_Item.ItemTypeForDG0',tableName:'ItemTypeForDG0',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem"
                        Text="Update" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem"
                        Text="Delete" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton"
                        OnClick="apply" Text="Apply" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="品名" Condition="%%" DataType="string" Editor="text" FieldName="ItemName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="物品類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sERP_Setting_Item.ItemTypeForDG0',tableName:'ItemTypeForDG0',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ItemTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMaster0" Title="物品">
                <JQTools:JQDataForm ID="dataFormMaster0" runat="server" DataMember="Item" HorizontalColumnsCount="2" RemoteName="sERP_Setting_Item.Item" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="dataFormMaster0_OnLoadSuccess" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="dataFormMaster0_OnApplied" OnApply="dataFormMaster0_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="物品編號" Editor="text" FieldName="ItemID" maxlength="0" Width="80" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="text" FieldName="ItemName" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="物品類別" Editor="infocombobox" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sERP_Setting_Item.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ItemTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="183" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster0" runat="server" BindingObjectID="dataFormMaster0" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ItemID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormMaster0" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ItemName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ItemTypeID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
