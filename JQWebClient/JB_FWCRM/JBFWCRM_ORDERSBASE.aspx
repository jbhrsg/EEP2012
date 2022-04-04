<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBFWCRM_ORDERSBASE.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#dataFormMaster').form({
                onLoadSuccess: function (data) {
                    $('#dataFormMasterEffectDate').combo('textbox').blur(function () {
                        var theNum = $('#dataFormMasterDueDays').val();
                        var theDate = $('#dataFormMasterEffectDate').datebox('getBindingValue');
                        var date = new Date($.jbDateAdd('days', theNum, theDate));
                        if (!isNaN(date)) {
                            var theGoal = $('#dataFormMasterDueDate');
                            var format = getInfolightOption(theGoal).format;
                            theGoal.datebox('setValue', getFormatValue(date.toJSON(), format));
                        }
                    });
                 }              
            });                                 
        });

        function SetBillNum(row) {
            var Value = row.ApplyBillID;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sOrdersBase.APPLYTYPE', //連接的Server端，command
                    data: "mode=method&method=" + "SetBillType" + "&parameters=" + Value, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            theNum = $.parseJSON(data);
                            $("#dataFormMasterDueDays").val(theNum);
                            var theDate = $('#dataFormMasterEffectDate').datebox('getBindingValue');
                            var date = new Date($.jbDateAdd('days', theNum, theDate));
                            if (!isNaN(date)) {
                                var theGoal = $('#dataFormMasterDueDate');
                                var format = getInfolightOption(theGoal).format;
                                theGoal.datebox('setValue', getFormatValue(date.toJSON(), format));
                            }
                        }
                    }
                });            
        }
        function GridReload() {
            $('#dataGridView').datagrid('reload');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sOrdersBase.OrdersBase" runat="server" AutoApply="True"
                DataMember="OrdersBase" Pagination="True" QueryTitle="快速查詢" EditDialogID="JQDialog1"
                Title="訂單基本庫維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="OrdersBaseID" Editor="numberbox" FieldName="OrdersBaseID" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="EMPLOYERNAME" MaxLength="0" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶" Editor="text" FieldName="EmployerID" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="函別" Editor="text" FieldName="APPLYTYPENAME" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="ApplyBillID" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="函號" Editor="text" FieldName="NO" MaxLength="50" Width="240" />
                    <JQTools:JQGridColumn Alignment="right" Caption="人數" Editor="numberbox" FieldName="Nums" Width="100" />
                    <JQTools:JQGridColumn Alignment="right" Caption="待確認人數" Editor="numberbox" FieldName="OnWayNums" Width="100" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已確認人數" Editor="numberbox" FieldName="CommitNums" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Width="100" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="DueDate" Width="100" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DueDays" Editor="text" FieldName="DueDays" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="客戶名稱" Condition="%%" DataType="string" Editor="text" FieldName="EMPLOYERNAME" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn Caption="函別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ApplyBillID',textField:'Name',remoteName:'sOrdersBase.APPLYTYPE',tableName:'APPLYTYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyBillID" NewLine="True" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="訂單基本庫維護">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="OrdersBase" HorizontalColumnsCount="2" RemoteName="sOrdersBase.OrdersBase" Closed="False" ContinueAdd="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="GridReload" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="OrdersBaseID" Width="180"  ReadOnly="True" Visible="true"  Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" FieldName="EmployerID" maxlength="20" Width="180" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sOrdersBase.EMPLOYER',tableName:'EMPLOYER',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="函別" Editor="infocombobox" FieldName="ApplyBillID" maxlength="0" Width="180" EditorOptions="valueField:'ApplyBillID',textField:'Name',remoteName:'sOrdersBase.APPLYTYPE',tableName:'APPLYTYPE',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SetBillNum,panelHeight:200" Span="1" Visible="True"/>

                        <JQTools:JQFormColumn Alignment="left" Caption="函號" Editor="text" FieldName="NO" Width="180" MaxLength="50"  />
                        <JQTools:JQFormColumn Alignment="left" Caption="人數" Editor="numberbox" FieldName="Nums" maxlength="0" Width="177" Span="2"  />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Width="180" Span="1" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="DueDate" Width="180" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Width="180" MaxLength="50" Visible="False"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="有效天數" Editor="text" FieldName="DueDays" Width="80" Visible="True" ReadOnly="True" Span="2" />
                       
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn DefaultValue="0" FieldName="OrdersBaseID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="1" FieldName="Nums" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="EffectDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" RemoteMethod="False" ValidateType="None" FieldName="EmployerID" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyBillID" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Nums" RangeFrom="1" RangeTo="300" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EffectDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DueDate" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
