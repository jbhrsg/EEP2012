<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ViewArea.aspx.cs" Inherits="Template_JQuerySingle1" %>
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
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        });
        function SetWhereDMTypeID(rowindex, rowdata) {
            if (rowdata != null && rowdata != undefined) {
                var DMTypeID = (rowdata.DMTypeID);
                $("#JQDataGrid1").datagrid('setWhere', "DMTypeID=" +"'"+  DMTypeID+"'");
            }
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function GetDMTypeID() {
            var id = $("#dataGridView").datagrid('getSelected').DMTypeID;
            return id;
        }
        function DMTypeIDOnBlur() {
            var DMTypeID = $("#dataFormMasterDMTypeID").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPViewArea.ERPDMType', //連接的Server端，command
                    data: "mode=method&method=" + "CheckDMTypeID" + "&parameters=" + DMTypeID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                    alert('注意!!此版別已存在');
                    $('#dataFormMasterDMTypeID').val("");
                    $('#dataFormMasterDMTypeID').text.focus;
                    return false;
                }
            }
            else return true;
        }
        function ViewAreaIDOnBlur() {
            var ViewAreaID = $("#JQDataForm1ViewAreaID").val();
            if (getEditMode($("#JQDataForm1")) == 'inserted') {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPViewArea.ERPViewArea', //連接的Server端，command
                    data: "mode=method&method=" + "CheckViewAreaID" + "&parameters=" + ViewAreaID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                    alert('注意!!此區域已存在');
                    $('#JQDataForm1ViewAreaID').val("");
                    $('#JQDataForm1ViewAreaID').text.focus;
                    return false;
                }
            }
            else return true;
        }
        function JQDataGrid1OnDelete() {
            var ViewAreaID = $("#JQDataGrid1").datagrid('getSelected').ViewAreaID;//取得當前主檔中選中的那個Data
            alert(ViewAreaID);
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPViewArea.ERPViewArea', //連接的Server端，command
                data: "mode=method&method=" + "CheckDelViewArea" + "&parameters=" + ViewAreaID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                alert('此區域有銷貨資料參考使用,無法刪除!!');

                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPViewArea.ERPDMType" runat="server" AutoApply="True"
                DataMember="ERPDMType" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="版別維護" OnSelect="SetWhereDMTypeID" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="600px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="DMTypeNO" Editor="numberbox" FieldName="DMTypeNO" Format="" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="DMTypeID" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="版別名稱" Editor="text" FieldName="DMTypeName" Format="" MaxLength="0" Width="360" />
                    <JQTools:JQGridColumn Alignment="center" Caption="是否有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpDateDate" Editor="datebox" FieldName="LastUpDateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="版別區域維護" DialogLeft="30px" DialogTop="30px" Width="480px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPDMType" HorizontalColumnsCount="1" RemoteName="sERPViewArea.ERPDMType" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="360px" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="DMTypeNO" Editor="numberbox" FieldName="DMTypeNO" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="DMTypeID" Format="" maxlength="0" Width="30" OnBlur="DMTypeIDOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="版別名稱" Editor="text" FieldName="DMTypeName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是否有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpDateDate" Editor="datebox" FieldName="LastUpDateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DMTypeID" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
        <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ERPViewArea" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPViewArea.ERPViewArea" Title="發刊區域維護" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" Width="600px" OnDelete="JQDataGrid1OnDelete">
            <Columns>
                <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="ViewAreaID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" />
                <JQTools:JQGridColumn Alignment="left" Caption="區域名稱" Editor="text" FieldName="ViewAreaName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="360" />
                <JQTools:JQGridColumn Alignment="left" Caption="DMTypeID" Editor="text" FieldName="DMTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                <JQTools:JQGridColumn Alignment="left" Caption="ViewAreaNO" Editor="text" FieldName="ViewAreaNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
            </Columns>
            <TooItems>
                 <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                 </TooItems>
        </JQTools:JQDataGrid>
        <JQTools:JQDialog ID="JQDialog2" BindingObjectID="JQDataForm1" runat="server" DialogLeft="30px" DialogTop="450px" Title="發刊區域維護" Width="480px">
            <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ERPViewArea" RemoteName="sERPViewArea.ERPViewArea" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="1" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="360px">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="ViewAreaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" OnBlur="ViewAreaIDOnBlur" />
                    <JQTools:JQFormColumn Alignment="left" Caption="區域名稱" Editor="text" FieldName="ViewAreaName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="DMTypeID" Editor="text" FieldName="DMTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    <JQTools:JQFormColumn Alignment="left" Caption="ViewAreaNO" Editor="text" FieldName="ViewAreaNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                </Columns>
                
            </JQTools:JQDataForm>
            <JQTools:JQDefault ID="JQDefault1" BindingObjectID="JQDataForm1" runat="server">
                <Columns>
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetDMTypeID" FieldName="DMTypeID" RemoteMethod="False" />
                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ViewAreaNO" RemoteMethod="True" />
                </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate ID="JQValidate1" BindingObjectID="JQDataForm1" runat="server">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ViewAreaID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="ViewAreaName" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
        </JQTools:JQDialog>
    </form>
</body>
</html>
