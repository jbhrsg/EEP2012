<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_IssueBelong.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">
         //Detail Grid隨著Master Grid Select改變
         function SetWhereBelongID(rowindex, rowdata) {
             if (rowdata != null && rowdata != undefined) {
                 var IssueBelongID = rowdata.GROUPID;
                 $("#dataGridDetail").datagrid('setWhere', "IssueBelongID= " + IssueBelongID);
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
         //新增明細時,抓取Master 關聯值
         function GetBelongID() {
             var id = $("#dataGridView").datagrid('getSelected').GROUPID;
             return id;
         }
         //刪除Master檢查
         function CheckMasterDelete() {
             var IssueBelongID = $("#dataGridView").datagrid('getSelected').IssueBelongID;;//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sIssueJob.IssueBelong', //連接的Server端，command
                 data: "mode=method&method=" + "CheckMasterDelete" + "&parameters=" + IssueBelongID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                
                 alert('此工作歸屬大項有工作項目參考使用,無法刪除!!');

                 return false;
             }
         }
         
         //刪除Details檢查
         function CheckDetailDelete() {
             var IssueTypeID = $("#dataGridDetail").datagrid('getSelected').IssueTypeID;;//取得當前主檔中選中的那個Data
             var cnt;
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sIssueJob.IssueType', //連接的Server端，command
                 data: "mode=method&method=" + "CheckDetailDelete" + "&parameters=" + IssueTypeID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                 alert('此產業別大項有細項資料參考使用,無法刪除!!');

                 return false;
             }
         }
         
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sIssueJob.GROUPS" runat="server" AutoApply="True"
                DataMember="GROUPS" Pagination="True" QueryTitle="Query" EditDialogID=""
                Title="工作歸屬-大項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnSelect="SetWhereBelongID" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="GROUPID" Format="" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工作歸屬名稱" Editor="text" FieldName="GROUPNAME" Format="" MaxLength="0" Width="150" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="工作歸屬維護" EditMode="Dialog">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="IssueBelong" HorizontalColumnsCount="2" RemoteName="sIssueJob.IssueBelong" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="IssueBelongID" Format="" Width="70" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作歸屬名稱" Editor="text" FieldName="IssueBelongName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IssueBelongID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="True" DataMember="IssueType" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="" RemoteName="sIssueJob.IssueType" Title="工作項目-細項" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="工作項目名稱查詢" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDelete="CheckDetailDelete" BufferView="False" NotInitGrid="False" RowNumbers="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="numberbox" FieldName="IssueTypeID" Format="" Width="70" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="IssueBelongID" Editor="numberbox" FieldName="IssueBelongID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工作項目名稱" Editor="text" FieldName="IssueTypeName" Format="" Width="350" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="IssueBelongID" ParentFieldName="IssueBelongID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="快速查詢" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="工作項目名稱" Condition="%" DataType="string" Editor="text" FieldName="IssueTypeName" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="150" />
                    </QueryColumns>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="工作項目維護">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="" DataMember="IssueType" HorizontalColumnsCount="2" RemoteName="sIssueJob.IssueType" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="IssueTypeID" Format="" Width="70" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IssueBelongID" Editor="numberbox" FieldName="IssueBelongID" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工作項目名稱" Editor="text" FieldName="IssueTypeName" Format="" Width="350" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="IssueBelongID" ParentFieldName="IssueBelongID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IssueTypeID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetBelongID" FieldName="IssueBelongID" RemoteMethod="False" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
