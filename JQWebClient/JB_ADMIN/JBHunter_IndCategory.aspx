<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_IndCategory.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         function SetWhereIndCategory(rowindex, rowdata) {
             if (rowdata != null && rowdata != undefined) {
                 var ID = rowdata.ID;
                 $("#JQDataGrid1").datagrid('setWhere', "ParentID= " + ID);
               
             }
         }
       //焦點欄位變顏色
         $(function () {
             $("input, select, textarea").focus(function () {
                 $(this).css("background-color", "yellow");
             })

             $("input, select, textarea").blur(function () {
                 $(this).css("background-color", "white");
             })
         })
         function GetCategoryID() {
             var id = $("#dataGridView").datagrid('getSelected').ID;
             return id;
         }
         function CheckMasterDelete() {
             var id = $("#dataGridView").datagrid('getSelected').ID;;//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sIndCategory.HUT_IndCategory', //連接的Server端，command
                 data: "mode=method&method=" + "CheckMasterDelete" + "&parameters=" + id, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = $.parseJSON(data);
                     }
                 }
             });
              
             if ((cnt == "0") || (cnt == "undefined")) {

                 return true;
             }
             else {
                 alert('此產業別大項有細項資料參考使用,無法刪除!!');

                 return false;
             }
         }
         function CheckItemDelete() {
             var row = $('#JQDataGrid1').datagrid('getSelected');//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sIndCategory.HUT_IndCategorys', //連接的Server端，command
                 data: "mode=method&method=" + "CheckDelItem" + "&parameters=" + row.ID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = $.parseJSON(data);
                     }
                 }
             });
             if ((cnt == "0") || (cnt == "undefined")) {

                 return true;
             }
             else {
                 alert('此產業別細項已有客戶資料參考使用,無法刪除!!');

                 return false;
             }
         }

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sIndCategory.HUT_IndCategory" runat="server" AutoApply="True"
                DataMember="HUT_IndCategory" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="產業別-大項" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px" OnSelect="SetWhereIndCategory" OnDelete="CheckMasterDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="ID" Format="" Width="40" />
                    <JQTools:JQGridColumn Alignment="left"  Caption="產業別大項名稱" Editor="text" FieldName="IndCategory" Format="" MaxLength="0" Width="300" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ParentID" Editor="numberbox" FieldName="ParentID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NodeLevel" Editor="text" FieldName="NodeLevel" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
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
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="產業別-大項">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_IndCategory" HorizontalColumnsCount="2" RemoteName="sIndCategory.HUT_IndCategory" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="ID" Format="" Width="40" SPAN="2" ReadOnly="True"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="產業別大項名稱" Editor="text" FieldName="IndCategory" Format="" maxlength="0" Width="180" SPAN="2"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="ParentID" Editor="numberbox" FieldName="ParentID" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="NodeLevel" Editor="text" FieldName="NodeLevel" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ParentID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="NodeLevel" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="HUT_IndCategorys" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sIndCategory.HUT_IndCategorys"
                 EditDialogID="JQDialog2" Title="產業別-細項" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px" OnDelete=" CheckItemDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="text" FieldName="ID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" />
                    <JQTools:JQGridColumn Alignment="left" Caption="產業別細項名稱" Editor="text" FieldName="IndCategory" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ParentID" Editor="text" FieldName="ParentID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="NodeLevel" Editor="text" FieldName="NodeLevel" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
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
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="產業別-細項">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="HUT_IndCategorys" HorizontalColumnsCount="2" RemoteName="sIndCategory.HUT_IndCategorys" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="ID" Width="40" SPAN="2" ReadOnly="True"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="產業別細項名稱" Editor="text" FieldName="IndCategory" maxlength="0" Width="180" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ParentID" Editor="text" FieldName="ParentID" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="NodeLevel" Editor="text" FieldName="NodeLevel" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" maxlength="0" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ID" RemoteMethod="False" DefaultMethod="" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCategoryID" FieldName="ParentID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="NodeLevel" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <br />
        </div>
    </form>
</body>
</html>
