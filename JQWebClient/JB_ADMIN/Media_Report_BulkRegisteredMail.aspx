<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Media_Report_BulkRegisteredMail.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
    function DfmCustNO_OnSelect(rowdata){
        $("#dataFormMasterCustShortName").val(rowdata.CustShortName);
        $("#dataFormMasterCustAddr").val(rowdata.CustAddr);
    }
    function dataFormMaster_OnLoadSuccess() {
        $("#dataFormMasterCustNO").combobox("setValue", "");
        $('#dataFormMasterCustNO').combobox("textbox").focus();

        
        //$('#ApplyButton').css({ 'visibility': 'visible' });
    }
    function MultiDelete() {
        if (confirm('確定刪除')) {
        var rows = $("#dataGridView").datagrid('getChecked');
        for (var i = rows.length - 1 ; i >= 0 ; i--) {
            var index = $('#dataGridView').datagrid('getRowIndex', rows[i]);
            $('#dataGridView').datagrid('deleteRow', index);
        }
        
        apply('#dataGridView');

        //$('#dataGridView').prev('div#toolbardataGridView').find('.btn').each(function () {
        //    var options = $.parseOptions(this);
        //    if (options.onclick == 'apply') {
        //        $(this).attr("disabled", false);
        //    }
            //    });

        //$("a[name='apply']").attr("disabled", false);
        }
    }
    function dataGridView_OnLoadSuccess() {
        //單選(為了OnUpdate_dataGridDetail來停用結案列的編輯
        if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
            $("#dataGridView").datagrid({
                singleSelect: true,
                selectOnCheck: false,
                checkOnSelect: false
            });
            //$("#JQDialog1").append($("<div style='text-align: center; padding: 5px'>").append($("#ApplyButton")));
        }
        //為了取消預設第一列勾選
        setTimeout(function () {
            $("#dataGridView").datagrid("unselectAll");
        }, 600);

        $('#ApplyButton').css({ 'visibility': 'hidden' });
    }

    //郵件類別視窗，按下確認
    function PrintButton_OnClick() {
        var url = "../JB_ADMIN/REPORT/Media/Media_Report_BulkRegisteredMail_RV.aspx";
        var height = $(window).height() - 50;
        //var width = $(window).width() - 600;
        var width = 960;
        var dialog = $('<div/>')
        .dialog({
            draggable: false,
            modal: true,
            height: height,
            width: width,
            title: "大宗掛號清單(列印提醒:請選擇PDF檔下載後，PDF列印時，紙張格式請選擇Letter)",
            //maximizable: true                              
        });
        $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
        dialog.dialog('open');
    }
    function ApplyButton_OnClick() {
        apply('#dataGridView');
        $('#ApplyButton').css({ 'visibility': 'hidden' });
    }
    //function dataGridView_OnDeleted() {
    //    apply('#dataGridView');
    //}
    function dataFormMaster_OnApply() {
        $('#ApplyButton').css({ 'visibility': 'visible' });
    }
    function InsertModeButton_OnClick() {
        insertItem('#dataGridView');
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sMedia_Report_BulkRegisteredMail.BulkRegisteredMail" runat="server" AutoApply="False"
                DataMember="BulkRegisteredMail" Pagination="False" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="大宗掛號清單" AlwaysClose="False" AllowAdd="True" AllowDelete="True" AllowUpdate="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="dataGridView_OnLoadSuccess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶編號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" MaxLength="0" Visible="true" Width="660" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Enabled="True" ItemType="easyui-linkbutton" Text="多筆刪除" Visible="True" Icon="icon-remove" OnClick="MultiDelete" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="PrintButton_OnClick" Text="列印清單" Visible="True" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="大宗掛號清單" EditMode="Continue">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="BulkRegisteredMail" HorizontalColumnsCount="2" RemoteName="sMedia_Report_BulkRegisteredMail.BulkRegisteredMail" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dataFormMaster_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶編號" Editor="infocombobox" FieldName="CustNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'CustNO',textField:'CustNO',remoteName:'sMedia_Report_BulkRegisteredMail.ERPCustomers2',tableName:'ERPCustomers2',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DfmCustNO_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簡稱" Editor="text" FieldName="CustShortName" Format="" maxlength="0" Width="192" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="CustAddr" Format="" maxlength="0" Width="400" Span="2" ReadOnly="True" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="bElecInvoice" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
                <JQTools:JQButton ID="InsertButton" runat="server" Icon="icon-add" OnClick="InsertModeButton_OnClick" Text="新增" />
                <JQTools:JQButton ID="ApplyButton" runat="server" Icon="icon-save" OnClick="ApplyButton_OnClick" Text="存檔" />
        </div>
    </form>
</body>
</html>
