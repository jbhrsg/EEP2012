<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_UserCareerCategory.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //焦點欄位變顏色
        $(function () {
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });

            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
        });

        function CheckMasterDelete() {
            var id = $("#dataGridView").datagrid('getSelected').CategoryID;;//取得當前主檔中選中的那個Data
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sHUTUser.HUT_UserCareerCategory', //連接的Server端，command
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
                alert('此產業類別有工作經驗參考使用,無法刪除!!');

                return false;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHUTUser.HUT_UserCareerCategory" runat="server" AutoApply="True"
                DataMember="HUT_UserCareerCategory" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="產業類別" Width="700px" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDelete="CheckMasterDelete">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="CategoryID" Width="30" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="產業類別" Editor="text" FieldName="CategoryName" MaxLength="50" Width="628" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   <%--     <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                            Text="存檔" />
                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                            Text="取消"  />--%>
                  <%--  <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="關鍵字" Condition="%%" DataType="string" Editor="text" FieldName="CategoryName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="產業類別">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="HUT_UserCareerCategory" HorizontalColumnsCount="2" RemoteName="sHUTUser.HUT_UserCareerCategory" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="text" FieldName="CategoryID" Width="30" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="產業類別" Editor="text" FieldName="CategoryName" maxlength="50" Width="280" Span="2"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateby" Editor="text" FieldName="LastUpdateby" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="CategoryID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="LastUpdateby" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CategoryName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
