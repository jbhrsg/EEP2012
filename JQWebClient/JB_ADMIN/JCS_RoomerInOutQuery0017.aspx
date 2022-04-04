<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCS_RoomerInOutQuery0017.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
         var CustNO = Request.getQueryStringByName("cn");
         var CurrentDate = Request.getQueryStringByName("cdt");
         var ControlTime = Request.getQueryStringByName("ct");
         var DoomID = Request.getQueryStringByName("jt");
         var ControlDateTime = CurrentDate + ' ' + ControlTime + ':00';
         $(document).ready(function () {
             setTimeout(function () {
                 GetRoomerInOutData();
             }, 500);
             //var Filtstr = "CardTimeLastIn>=" + "'" + ControlDateTime + "'"
             //alert(Filtstr);
             //$("#dataGridMaster").datagrid('setWhere', Filtstr);
             //$("#dataGridMaster").datagrid('options').rowStyler = function (index, row) {
             //    if ((row.CardTimeLastIn == '' || row.CardTimeLastIn == undefined || row.CardTimeLastIn >= ControlDateTime))
             //        return 'background-color:pink;color:blue;font-weight:bold;';
             //};
             //$("#dataGridMaster").datagrid('options').rowStyler = function (index, row) {
             //    if (row.IsErrHint == true) {
             //        return 'background-color:pink;color:blue;font-weight:bold;';
             //    }
             //};
         });
         function dataGridMasterOnLoadSucess() {
             var CenterName = '進出日期:' + CurrentDate + '&emsp;&emsp;&emsp;管控時間:' + ControlTime;
             $("#dataGridMaster").datagrid('getPanel').panel('setTitle', CenterName);
             $("#dataGridMaster").datagrid('options').title = CenterName;
         }
         function GetRoomerInOutData() {
             var UserID = getClientInfo("UserID");
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sJCSOutQuery',
                 data: "mode=method&method=" + "GetRoomerInOutData" + "&parameters=" + CustNO + "," + CurrentDate + ","+ ControlTime + "," + DoomID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         if (rows.length > 10) {
                             $('#dataGridMaster').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                         }
                          else{
                             $('#dataGridMaster').datagrid('loadData', rows);
                         }
                    }
                 }
             });
         }
         function genCheckBox(val) {
             if (val)
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "<input  type='checkbox' />";
         }
         function AutoExcel() {             

             $.ajax({
                 url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'CallServerMethodReturnFile', remoteName: 'sJCSOutQuery', method: 'RoomerInOutAutoExcel' }),
                 //data: { parameters: $.toJSONString(data) },

                 data: "&parameters=" + CustNO + "," + CurrentDate + "," + ControlTime + "," + DoomID,

                 type: 'POST',
                 async: true,
                 success: function (data) {
                     //Json.FileName
                     var Json = $.parseJSON(data);
                     if (Json.IsOK) {
                         var Url = $('<a>', {
                             href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '不在宿名單.xls', FilePathName: Json.FileStreamOrFileName }),
                             target: '_blank'

                         }).html('檔案下載')[0].outerHTML;

                         $.messager.alert('下載', Url, '');

                     }

                     else $.messager.alert('錯誤', Json.Msg, 'error');

                 },

                 beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },

                 complete: function () { $.messager.progress('close'); },

                 error: function (xhr, ajaxOptions, thrownError) { alert('error'); }

             });
         }

     </script>
      
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" AgentDatabase="JBADMIN" AgentSolution="JBADMIN" AgentUser="A0123" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJCSOutQuery.RoomerInOutQuery" runat="server" AutoApply="True"
                DataMember="RoomerInOutQuery" Pagination="True" QueryTitle="Query"
                Title="房客進出資訊" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="720px" OnLoadSuccess="dataGridMasterOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerShortName" Format="" MaxLength="0" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="房客姓名" Editor="text" FieldName="RoomerNameC" Format="" MaxLength="0" Width="90" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="性別" Editor="text" FieldName="Gender" Format="" MaxLength="0" Width="30" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="房號" Editor="text" FieldName="Room" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="班別" Editor="text" FieldName="Class" Format="" MaxLength="0" Width="60" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="當日最早刷出" Editor="text" FieldName="CardTimeLastOut" Format="" MaxLength="0" Width="130" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="當日最後刷進" Editor="text" FieldName="CardTimeLastIn" Format="" MaxLength="0" Width="130" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RoomerID" Editor="text" FieldName="RoomerID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="外出時數" Editor="numberbox" FieldName="HoursLastOut" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="在宿時數" Editor="numberbox" FieldName="HoursLastIN" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="異常" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsErrHint" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45" FormatScript="genCheckBox">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="AutoExcel" Text="匯出Excel" />
                    <%--<JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
</form>
</body>
</html>
