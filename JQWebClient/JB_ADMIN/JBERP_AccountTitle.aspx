<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_AccountTitle.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 640 });
            $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function CheckAccountIDDul() {
            var dataFormMasterAccountID = $("#dataFormMasterAccountID").refval('getValue');
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var AccountID = $('#dataFormMasterAccountID').val();
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sReferencesADM.AccountTitle', //連接的Server端，command
                    data: "mode=method&method=" + "CheckAccountIDDul" + "&parameters=" + AccountID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
                    alert('注意!!此會計科目代號已存在');
                    $('#dataFormMasterAccountID').val('');
                    $('#dataFormMasterAccountID').focus;
                    return false;
                }
            }
           
        }
        function dataFormMasterOnApplied() {
            $("#dataGridView").datagrid("reload");
        }
        function dataFormMasterOnApply() {
            var dataFormMasterPayTypeID = $("#dataFormMasterPayTypeID").combobox('getValue');
            if (dataFormMasterPayTypeID == "" || dataFormMasterPayTypeID == undefined) {
                alert('注意!!,未選付款方式,請選取!!');
                $("#dataFormMasterPayTypeID").focus();
                return false;
            }

        }
        </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sReferencesADM.AccountTitle" runat="server" AutoApply="True"
                DataMember="AccountTitle" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="會計科目-JBERP_AccountTitle" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="1080px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="科目代號" Editor="text" FieldName="AccountID" Format="" MaxLength="0" Width="55" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AccountName" Format="" MaxLength="0" Width="150" Frozen="True" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="科目類別" Editor="text" FieldName="AccountType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayTypeID" Format="" Width="55" Visible="True" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sReferencesADM.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn FieldName="LimitCostName" Caption="使用成本中心" IsNvarChar="False" Alignment="left" Width="320" Editor="text" MaxLength="0" Sortable="False" Frozen="False" ReadOnly="False" Visible="True" QueryCondition=""></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="請款單" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRequisition" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="零用金" Editor="checkbox" EditorOptions="on: 1,off:0" FieldName="IsPettyCash" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="採購單" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPurchase" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="center" Caption="出差單" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsBizTravel" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="說明" Editor="text" FieldName="AccountDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="科目代號" Condition="%%" DataType="string" Editor="text" FieldName="AccountID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="科目名稱" Condition="%%" DataType="string" Editor="text" FieldName="AccountName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="科目類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sReferencesADM.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountType" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="請款單" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRequisition" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="零用金" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPettyCash" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="請購單" Condition="=" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPurchase" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="會計科目" DialogLeft="30px" DialogTop="50px" Width="630px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AccountTitle" HorizontalColumnsCount="2" RemoteName="sReferencesADM.AccountTitle" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" IsAutoPause="False" OnApply="dataFormMasterOnApply" OnApplied="dataFormMasterOnApplied" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="科目代號" Editor="text" FieldName="AccountID" Format="" maxlength="0" Width="100" span="1" OnBlur="CheckAccountIDDul"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="AccountName" Format="" Width="300" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目說明" Editor="text" FieldName="AccountDescr" maxlength="0" span="2" Width="460" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sReferencesADM.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支付方式" Editor="infocombobox" FieldName="PayTypeID" Format="" Width="90" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sReferencesADM.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" span="2" maxlength="0" Visible="True"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單項目" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRequisition" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="零用金項目" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPettyCash" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購單項目" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPurchase" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差單項目" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsBizTravel" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="限制成本中心" Editor="text" FieldName="LimitCostCenters" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="460" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="AccountID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AccountName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
