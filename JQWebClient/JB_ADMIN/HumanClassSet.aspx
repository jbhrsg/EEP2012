<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HumanClassSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script>
        
        function OnApplyDF() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var ClassText = $("#dataFormMasterClassText").val();

                var cnt = "";
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sHumanImport.infoHumanClassSetNew', //連接的Server端，command
                    data: "mode=method&method=" + "CheckHumanClassSet" + "&parameters=" + ClassText,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = data;
                        }
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });
                //代辦事項呈現
                if (cnt != 0) {
                    alert('此標籤已存在！');
                    return false;
                }
            }
        }
       
        
   </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHumanImport.infoHumanClassSetNew" runat="server" AutoApply="True"
                DataMember="infoHumanClassSetNew" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="標籤維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="標籤名稱" Editor="text" FieldName="ClassText" Format="" MaxLength="0" Visible="true" Width="130" />
                    <JQTools:JQGridColumn Alignment="left" Caption="標籤代號" Editor="text" FieldName="AutoKey" Format="" Visible="true" Width="90" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="標籤名稱" Condition="%%" DataType="string" Editor="text" FieldName="ClassText" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title=" ">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="infoHumanClassSetNew" HorizontalColumnsCount="2" RemoteName="sHumanImport.infoHumanClassSetNew" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApplyDF" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="標籤代號" Editor="text" FieldName="AutoKey" Format="" Width="70" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="標籤名稱" Editor="text" FieldName="ClassText" Format="" maxlength="0" Width="230" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ClassText" RemoteMethod="True" ValidateMessage="標籤名稱不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
