<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Bank.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
                    $(this).css("background-color", "lightyellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //設定 Grid QueryColunm Windows width=320px
            //var dgid = $('#dataGridView');
            //var queryPanel = getInfolightOption(dgid).queryDialog;
            //if (queryPanel)
            //    $(queryPanel).panel('resize', { width: 280 });
            //var mac = networkInfo();
            //alert(mac);
        });
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function queryGrid(dg) {
            var BankName = $('#BankName_Query').val();
            var where = "BankName like '%" + BankName + "%'";
            $("#dataGridView").datagrid('setWhere', where);

        }
        function DataFormMasterOnApply() {
            var BankRootName = $("#dataFormMasterBankRootName").val();
            var BankName = $("#dataFormMasterBankName").val();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#dataFormMasterBankName").val(BankRootName + ' ' + BankName);
            }
        }
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sReferencesADM.Bank" runat="server" AutoApply="True"
                DataMember="Bank" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="交易銀行" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="50px" QueryMode="Panel" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="720px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="BankID" Format="" Width="50" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行代號" Editor="text" FieldName="BankNO" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行名稱" Editor="text" FieldName="BankRootName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80"></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="分行代號" Editor="text" FieldName="BankBranchNO" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分行名稱" Editor="text" FieldName="BankName" Format="" MaxLength="0" Width="320" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="手續費" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRemit" FormatScript="genCheckBox" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"  OnClick="insertItem" Text="新增" />
                  <%--  <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="" Caption="銀行名稱" Condition="%%" DataType="string" Editor="text" FieldName="BankName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="交易銀行">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Bank" HorizontalColumnsCount="2" RemoteName="sReferencesADM.Bank" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="DataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="代號" Editor="numberbox" FieldName="BankID" Format="" Width="80" Span="2" ReadOnly="True" maxlength="0"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="銀行代號" Editor="text" FieldName="BankNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銀行名稱" Editor="text" FieldName="BankRootName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分行代號" Editor="text" FieldName="BankBranchNO" ReadOnly="False" Span="1" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分行名稱" Editor="text" FieldName="BankName" Format="" maxlength="0" Width="180" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手續費" Editor="checkbox" FieldName="IsRemit" Format="genCheckBox" MaxLength="0" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BankID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsRemitFee" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
