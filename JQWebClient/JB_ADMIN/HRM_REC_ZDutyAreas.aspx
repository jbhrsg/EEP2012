<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_REC_ZDutyAreas.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

     <script type="text/javascript">
         //$(document).ready(function () {
         //    $("#dataGridView").form({
         //        onLoadSuccess: function () {                    
         //            $("#dataGridDetail").datagrid('setWhere', " IssueBelongID=1");
         //        }

         //    });
         //});
         function LoadClassID() {
             $("#dataGridDetail").datagrid('setWhere', " ClassID=1");
         }
         //Detail Grid隨著Master Grid Select改變
         function SetWhereClassID(rowindex, rowdata) {
             if (rowdata != null && rowdata != undefined) {
                 var ID = rowdata.ID;
                 $("#dataGridDetail").datagrid('setWhere', "ClassID= " + ID);
             }
         }
         ////焦點欄位變顏色
         //$(function () {
         //    $("input, select, textarea").focus(function () {
         //        $(this).css("background-color", "yellow");
         //    })

         //    $("input, select, textarea").blur(function () {
         //        $(this).css("background-color", "white");
         //    })
         //})
         //新增明細時,抓取Master 關聯值
         function GetClassID() {
             var id = $("#dataGridView").datagrid('getSelected').ID;
             return id;
         }
         //Grid genCheckBox
         function GridCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' onclick='return false;'/>";
             else
                 return "<input  type='checkbox' onclick='return false;'/>";
         }
         //刪除Master檢查
         function CheckMasterDelete() {
             var ID = $("#dataGridView").datagrid('getSelected').ID;;//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_ZDutyAreas.REC_ZDutyAreasClass', //連接的Server端，command
                 data: "mode=method&method=" + "CheckMasterDelete" + "&parameters=" + ID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     cnt = data;
                 }
             });
             if ((cnt == "0") || (cnt == "undefined")) {

                 return true;
             }
             else {

                 alert('此縣市底下尚有工作地點,無法刪除!!');

                 return false;
             }
         }

         //刪除Details檢查
         function CheckDetailDelete() {
             var ClassID = $("#dataGridDetail").datagrid('getSelected').ClassID;;//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_REC_ZDutyAreas.REC_ZDutyAreasClass', //連接的Server端，command
                 data: "mode=method&method=" + "CheckDetailDelete" + "&parameters=" + ClassID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     cnt = $.parseJSON(data);
                 }
             });

             if ((cnt == "0") || (cnt == "undefined")) {

                 return true;
             }
             else {
                 alert('此地區有履歷資料參考使用,無法刪除!!');

                 return false;
             }
         }
     </script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div class="easyui-layout" style="height: 522px;">
                <div data-options="region:'west',split:true" style="width: 320px;" >
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="_HRM_REC_ZDutyAreas.REC_ZDutyAreasClass" runat="server" AutoApply="True"
                DataMember="REC_ZDutyAreasClass" Pagination="True" QueryTitle="查詢縣市" EditDialogID=""
                Title="工作縣市" ViewCommandVisible="False" OnLoadSuccess="LoadClassID" OnSelect="SetWhereClassID" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Window" QueryTop="20px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" Width="310px" OnDelete="CheckMasterDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ID" Editor="numberbox" FieldName="ID" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="縣市" Editor="text" FieldName="Contents" Format="" MaxLength="0" Visible="true" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SortID" Editor="numberbox" FieldName="SortID" Format="" Visible="False" Width="120" />
<%--                    <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" FormatScript="GridCheckBox">--%>
<%--                    </JQTools:JQGridColumn>--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增縣市" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="縣市" Condition="%%" DataType="string" Editor="text" FieldName="Contents" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                </QueryColumns>
            </JQTools:JQDataGrid>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataGridView" EnableTheming="True">
                   <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>

                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataGridView" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Contents" RemoteMethod="True" ValidateMessage="請填寫縣市！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoMaster" runat="server" BindingObjectID="dataGridView" FieldName="ID" NumDig="1" />
                </div>
                <div data-options="region:'center'">
                    <div>
                    

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="True" DataMember="REC_ZDutyAreas" EditDialogID="" Pagination="False" ParentObjectID="" RemoteName="_HRM_REC_ZDutyAreas.REC_ZDutyAreas" Title="工作地點" AlwaysClose="False" Width="320px" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60" PageSize="15" QueryAutoColumn="False" QueryLeft="390px" QueryMode="Window" QueryTitle="查詢地點" QueryTop="40px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="ClassID" Editor="numberbox" FieldName="ClassID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="ID" Editor="numberbox" FieldName="ID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="地點" Editor="text" FieldName="Contents" Format="" Width="200" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SortID" Editor="numberbox" FieldName="SortID" Format="" Width="120" Visible="False" />
                    
<%--                        <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="GridCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>--%>
                    
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ClassID" ParentFieldName="ID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增地點" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="地點" Condition="%%" DataType="string" Editor="text" FieldName="Contents" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    </QueryColumns>
                </JQTools:JQDataGrid>

                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                         <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetClassID" FieldName="ClassID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="LastUpdateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                             <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="Contents" RemoteMethod="True" ValidateMessage="請填寫區域！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                    <JQTools:JQAutoSeq ID="JQAutoDetail" runat="server" BindingObjectID="dataGridDetail" FieldName="ID" NumDig="1" />
                </div>
           </div>
        </div>
        </div>
        
    </form>
</body>
</html>
