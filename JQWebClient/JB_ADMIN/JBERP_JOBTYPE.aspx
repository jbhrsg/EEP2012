<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_JOBTYPE.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function dataGridMasterOnInserted() {
            $('#dataGridMaster').datagrid("reload");;
        }
        function CheckJBType() {
            var jb_type = $("#dataFormMasterjb_type").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJobType.jb_type', 
                    data: "mode=method&method=" + "CheckJobType" + "&parameters=" + jb_type ,
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
                    alert('注意!!此代號已存在');
                    $('#dataFormMasterjb_type').val("");
                    $('#dataFormMasterjb_type').focus();
                    return false;
                }
            }
            else return true;
        }
        function OnBlurJobType() {
            var tt = CheckJBType();
        }
        function GetUserName() {
            var UserName = getClientInfo("USERNAME");
            return UserName;
        }
        function APITest() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJobType.jb_type',
                data: "mode=method&method=" + "procCallAPI" + " &parameters=" + UserID ,
                cache: false,
                async: false,
                success: function (data) {
                    alert(data);
                    //if (data == "True") {
                    //    $('#dataGridCenter1').datagrid("reload");
                    //}
                    //else {
                    //    alert("注意!! 聯絡人加入群組失敗")
                    //}
                }
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJobType.jb_type" runat="server" AutoApply="True"
                DataMember="jb_type" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="行業別維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="680px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="jb_type" Format="" MaxLength="0" Visible="true" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="行業名稱" Editor="text" FieldName="jb_name" Format="" MaxLength="0" Visible="true" Width="240" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApiXml" Editor="text" FieldName="ApiXml" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="key_man" Format="" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="key_date" Format="yyyy/mm/dd HH:MM:SS" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="O_NO" Editor="text" FieldName="O_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="APITest" Text="API測試" />
                   
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="行業別維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="jb_type" HorizontalColumnsCount="2" RemoteName="sJobType.jb_type" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="jb_type" Format="" maxlength="0" Width="60" OnBlur="CheckJBType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行業名稱" Editor="text" FieldName="jb_name" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="key_man" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="key_date" Format="yyyy/mm/dd HH:MM:SS" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserName" DefaultValue="" FieldName="key_man" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="key_date" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="jb_type" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
