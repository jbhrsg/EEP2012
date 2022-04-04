<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_APDetails.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
    function BeforeOneMonth() {
        var dt = new Date();
        var aDate = new Date($.jbDateAdd('days', -31, dt));
        return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
    }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var FiltStr = '';
                var PlanPayDateS = $('#PlanPayDate_S_Query').datebox('getValue');
                if (PlanPayDateS != '' && PlanPayDateS != undefined) {
                    FiltStr = "PlanPayDate >= " + "'" + PlanPayDateS + "'";
                }
                var PlanPayDateE = $('#PlanPayDate_E_Query').datebox('getValue');
                if (PlanPayDateE != '' && PlanPayDateE != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "PlanPayDate <= " + "'" + PlanPayDateE + "'";
                }
                var InsGroupID = $('#InsGroupID_Query').combobox('getValue');
                if  (InsGroupID != '' && InsGroupID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "InsGroupID = " + "'" + InsGroupID + "'";
                }
                var CostCenterID = $('#CostCenterID_Query').combobox('getValue');
                if (CostCenterID != '' && CostCenterID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "CostCenterID = " + "'" + CostCenterID + "'";
                }
                var PayTo = $('#PayTo_Query').combobox('getValue');
                if (PayTo != '' && PayTo != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "PayTo = " + "'" + PayTo + "'";
                }
                var APTypeID = $('#APTypeID_Query').combobox('getValue');
                if (APTypeID != '' && APTypeID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "APTypeID = " + "'" + APTypeID + "'";
                }
                //付款方式
                var PayWayID = $('#PayWayID_Query').combobox('getValue');
                if (PayWayID != '' && PayWayID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + "PayWayID = " + "'" + PayWayID + "'";
                }
                $(dg).datagrid('setWhere', FiltStr);
            }
        }
        function AmtTaxOnBlur() {
            var amt = $("#dataFormMasterAPAmount").val();
            var tax = $("#dataFormMasterAPTax").val();
            var tamt = Math.round(amt) + Math.round(tax);
            $("#dataFormMasterAPTotalAmt").val(tamt);
        }
        </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAPDetails.APDetails" runat="server" AutoApply="True"
                DataMember="APDetails" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="應付帳款維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="應付單號" Editor="text" FieldName="APNO" Format="" MaxLength="0" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Width="70" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sAPDetails.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="應付來源" Editor="infocombobox" FieldName="APTypeID" Format="" Width="60" EditorOptions="valueField:'APTypeID',textField:'APTypeName',remoteName:'sAPDetails.APType',tableName:'APType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="立帳日期" Editor="datebox" FieldName="APDate" Format="yyyy/mm/dd" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" MaxLength="0" Width="70" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sAPDetails.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="應付內容" Editor="text" FieldName="APDescr" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="center" Caption="數量" Editor="numberbox" FieldName="APQty" Format="" Width="30" />
                    <JQTools:JQGridColumn Alignment="right" Caption="價格" Editor="numberbox" FieldName="APPrice" Format="N0" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="APAmount" Format="N0" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="APTax" Format="N0" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="應付總金額" Editor="numberbox" FieldName="APTotalAmt" Format="N0" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="驗收日期" Editor="datebox" FieldName="APAcceptDate" Format="yyyy/mm/dd" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="採購廠商" Editor="infocombobox" EditorOptions="valueField:'VendID',textField:'VendShortName',remoteName:'sAPDetails.Vendors1',tableName:'Vendors1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" Format="" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="應付款日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" MaxLength="0" Width="70" />
                    <JQTools:JQGridColumn Alignment="left" Caption="帳款支付" Editor="infocombobox" FieldName="PayTo" Format="" MaxLength="0" Width="120" EditorOptions="valueField:'VendID',textField:'VendAccountName',remoteName:'sAPDetails.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="收據發票號碼" Editor="text" FieldName="ProofNO" Format="" Width="90" />
                    <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PAYWAYID',textField:'PAYWAYNAME',remoteName:'sAPDetails.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayWayID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="匯費" Editor="numberbox" FieldName="Remit" Format="" Width="35" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銀行代碼" Editor="text" FieldName="BankNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="帳戶號碼" Editor="text" FieldName="AccountNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="結帳方式" Editor="infocombobox" EditorOptions="valueField:'POPAYTYPEID',textField:'POPAYTYPENAME',remoteName:'sAPDetails.POPayType',tableName:'POPayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="POPayTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="交易單據" Editor="text" FieldName="BillItem" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" MaxLength="0" Width="80" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                   <%-- <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="應付起迄日" Condition="=" DataType="string" DefaultMethod="BeforeOneMonth" Editor="datebox" FieldName="PlanPayDate_S" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="~" Condition="=" DataType="string" DefaultValue="_today" Editor="datebox" FieldName="PlanPayDate_E" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sAPDetails.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sAPDetails.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付給廠商" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'VendID',textField:'VendShortName',remoteName:'sAPDetails.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Condition="%" DataType="string" Editor="infocombobox" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" Caption="付款方式" EditorOptions="valueField:'PAYWAYID',textField:'PAYWAYNAME',remoteName:'sAPDetails.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayWayID" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="應付來源" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'APTypeID',textField:'APTypeName',remoteName:'sAPDetails.APType',tableName:'APType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="APTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="應付帳款維護" DialogLeft="10px" DialogTop="100px" Width="850px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="APDetails" HorizontalColumnsCount="4" RemoteName="sAPDetails.APDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="應付單號" Editor="text" FieldName="APNO" Format="" maxlength="0" ReadOnly="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sAPDetails.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" Format="" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sAPDetails.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Format="" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sAPDetails.BudgetAccount',tableName:'BudgetAccount',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AcSubno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應付內容" Editor="text" FieldName="APDescr" Format="" Span="3" Width="510" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易單據" Editor="text" FieldName="BillItem" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購廠商" Editor="infocombobox" EditorOptions="valueField:'VendID',textField:'VendShortName',remoteName:'sAPDetails.Vendors1',tableName:'Vendors1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應付金額" Editor="numberbox" FieldName="APAmount" Format="" Width="130" OnBlur="AmtTaxOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應付稅額" Editor="numberbox" FieldName="APTax" Format="" Width="130" OnBlur="AmtTaxOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應付總金額" Editor="text" FieldName="APTotalAmt" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費" Editor="numberbox" FieldName="Remit" Format="" maxlength="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="立帳日期" Editor="datebox" FieldName="APDate" Format="yyyy/mm/dd" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收日期" Editor="datebox" FieldName="APAcceptDate" Format="yyyy/mm/dd" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" Format="" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="應付款日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PAYWAYID',textField:'PAYWAYNAME',remoteName:'sAPDetails.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayWayID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳款支付" Editor="infocombobox" EditorOptions="valueField:'VendID',textField:'VendAccountName',remoteName:'sAPDetails.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTo" Format="" Span="1" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="AccountNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票收據" Editor="text" FieldName="ProofNO" Format="" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="APTypeID" Editor="numberbox" FieldName="APTypeID" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
