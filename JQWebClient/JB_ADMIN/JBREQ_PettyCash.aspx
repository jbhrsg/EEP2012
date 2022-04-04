<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_PettyCash.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var backcolor = "#F2FFF2"
        $(":input").css("background", backcolor);
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterIsUrgentPay', '#dataFormMasterIsNotPayDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", '#FFFFE8');
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });
            var PettyCashTax = $('#dataFormMasterPettyCashTax').closest('td');
            //PettyCashTax.append('   ←可修改為發票上實際稅額')
        })
        function dataFormMaster_OnLoadSuccess() {
           var UserID = getClientInfo("UserID");
           var parameters = Request.getQueryStringByName("P1");
           var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
           if (getEditMode($("#dataFormMaster")) == 'inserted') {
               $("#dataFormMasterPettyCashAmt").attr('disabled', true);
               $("#dataFormMasterPettyCashTax").attr('disabled', true);
               var EmpFlowAgentList = GetEmpFlowAgentList();
               var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
               $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
               //取得部門對應的成本中心-----------------------------------------------------
               var rowData = $("#dataFormMasterApplyOrg_NO").combobox('getSelectItem');
               var filter = "CostCenterID in (" + rowData.CostCenterID + ")";
               $("#dataFormMasterCostCenterID").combobox("setWhere", filter);
               //取得部門對應的成本中心-----------------------------------------------------
               $("#dataFormMasterAcSubno").combobox("setValue", "");
               $("#dataFormMasterAcSubno").combobox('setWhere', "1=2");
               var UserOrgNO = GetUserOrgNO(UserID);
               $("#dataFormMasterOrg_NOParent").val(UserOrgNO);
           }
       }
       function GetAccountID(rowData) {
            //$("#dataFormMasterPayTypeID").combobox('setValue', rowData.PayTypeID);
        };
        //當點按關閉按鈕時,關閉目前Tab
       function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
       }
        //檢查憑證號碼
       function CheckProofNO() {
            var ProofType = $("#dataFormMasterProofTypeID").combobox("getValue");
            var ProofNO = $("#dataFormMasterProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;
        }
        //選取科目類別,舊科目
        function OnSelectAccountType(rowData) {
            $("#dataFormMasterAccountID").combobox("setValue","");
            var CostCenterID = $("#dataFormMasterCostCenterID").combobox("getValue");
            var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            $("#dataFormMasterAccountID").combobox('setWhere', FiltStr);
        }
        //選取科目類別,新科目
        function OnSelectBudgetType(rowData) {
            $("#dataFormMasterAcSubNO").combobox("setValue", "");
            var CostCenterID = $("#dataFormMasterCostCenterID").combobox("getValue");
            var FiltStr = "BudgetType=" + "'" + rowData.BudgetType + "'" + " AND CostCenterID = " + "'" + CostCenterID + "'";
            $("#dataFormMasterAcSubNO").combobox('setWhere', FiltStr);
        }
        function OnApplydataFormMaster() {
            var dataFormMasterApplyOrg_NO = $("#dataFormMasterApplyOrg_NO").combobox('getValue');
            if (dataFormMasterApplyOrg_NO == "" || dataFormMasterApplyOrg_NO == undefined) {
                alert('注意!!,未選取申請部門,請選取');
                $("#dataFormMasterApplyOrg_NO").focus();
                return false;
            }
            var dataFormMasterInsGroupID = $("#dataFormMasterInsGroupID").combobox('getValue');
            if (dataFormMasterInsGroupID == "" || dataFormMasterInsGroupID == undefined) {
                alert('注意!!,未選取公司別,請選取');
                $("#dataFormMasterInsGroupID").focus();
                return false;
            }
            var dataFormMasterCostCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
            if (dataFormMasterCostCenterID == "" || dataFormMasterCostCenterID == undefined) {
                alert('注意!!,成本中心未設定,請洽管理室');
                $("#dataFormMasterCostCenterID").focus();
                return false;
            }
            var dataFormMasterBudgetType = $("#dataFormMasterBudgetType").combobox('getValue');
            if (dataFormMasterBudgetType == "" || dataFormMasterBudgetType == undefined) {
                alert('注意!!,未選取科目類別,請選取');
                $("#dataFormMasterBudgetType").focus();
                return false;
            }
            var dataFormMasterAcSubno = $("#dataFormMasterAcSubNO").combobox('getValue');
            if (dataFormMasterAcSubno == "" || dataFormMasterAcSubno == undefined) {
                alert('注意!!,未選取會計科目,請選取');
                $("#dataFormMasterAcSubno").focus();
                return false;
            }
            var dataFormMasterInvoiceYM = $("#dataFormMasterInvoiceYM").combobox('getValue');
            if (dataFormMasterInvoiceYM == "" || dataFormMasterInvoiceYM == undefined) {
                alert('注意!!,未選取單據(發票/收據/支出證明單)年月,請選取');
                $("#dataFormMasterInvoiceYM").focus();
                return false;
            }
            var dataFormMasterProofTypeID = $("#dataFormMasterProofTypeID").combobox('getValue');
            if (dataFormMasterProofTypeID == "" || dataFormMasterProofTypeID == undefined) {
                alert('注意!!,未選取支出憑據,請選取');
                $("#dataFormMasterProofTypeID").focus();
                return false;
            }
            var dataFormMasterProofNO = $("#dataFormMasterProofNO").val();
            if (dataFormMasterProofTypeID == 1 && (dataFormMasterProofNO == "" || dataFormMasterProofNO == undefined)) {
                alert('注意!!,請輸入憑據單號(發票號碼)');
                return false;
            }
           };
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "零用金申請單";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash', 
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        function PettyCashTotalOnBlur() {
            var payproof = $("#dataFormMasterProofTypeID").combobox('getValue');
            var PettyCashTotal = $("#dataFormMasterPettyCashTotal").val();
            if (payproof == 1) {
                var PettyCashAmt = Math.round(PettyCashTotal / 1.05);
                var PettyCashTax = Math.round((PettyCashAmt * 0.05));
                var PettyCashAmt = PettyCashTotal - PettyCashTax;
                $("#dataFormMasterPettyCashAmt").numberbox('setValue', PettyCashAmt);
                $("#dataFormMasterPettyCashTax").numberbox('setValue', PettyCashTax);
            }
            else {
                $("#dataFormMasterPettyCashAmt").numberbox('setValue', PettyCashTotal);
                $("#dataFormMasterPettyCashTax").numberbox('setValue', 0);
            }
        }
        //給值
        function OnSelectAcSubno(rowData) {
                $("#dataFormMasterAcno").val(rowData.Acno_S);
                $("#dataFormMasterSubAcno").val(rowData.SubAcno_S);
        }
        function OnSelectOrg_NO(rowData) {
            //自2022/01 開始不預設成本中心 joe 2022/01/29
            //$("#dataFormMasterCostCenterID").combobox('setValue', rowData.CostCenterID);
            //var whereStr = "CostCenterID='" + rowData.CostCenterID + "'";
            //$("#dataFormMasterCostCenterID").combobox('setWhere', whereStr);
            var filter = "CostCenterID in (" + rowData.CostCenterID + ")";
            $("#dataFormMasterCostCenterID").combobox("setWhere", filter);
            //var UserOrgParent = GetUserOrgNOParent();
            //alert(UserOrg);
            //$("#dataFormMasterOrg_NOParent").val(UserOrgParent);
            return true;
        }
        function PettyCashAmtOnBlur() {
            return true;
        }
        function PettyCashTaxOnBlur() {
            return true;
        }
        function GetUserOrgNO(UserID) {
            //var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
                data: "mode=method&method=" + "GetUserOrgNO" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].UserOrgNO;
                    }
                }
            })
            return _return;
        }
        function GetUserOrg() {
            var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash', 
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].OrgNO;
                    }
                }
            })
            return _return;
        }
        //取得所在部門
        function GetUserOrgNOParent() {
            var UserID = getClientInfo("UserID");
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].OrgNOParent;
                    }
                }
            })
            return _return;
        }
        function GetUserOrgCostCenter() {
            //var UserID = getClientInfo("UserID");
            var _return = "";
            //$.ajax({
            //    type: "POST",
            //    url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash',
            //    data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
            //    cache: false,
            //    async: false,
            //    success: function (data) {
            //        var rows = $.parseJSON(data);
            //        if (rows.length == 1) {
            //            _return = rows[0].OrgCostCenter;
            //        }
            //    }
            //})
            return _return;
        }
        function ProofTypeIDOnSelect() {
            PettyCashTotalOnBlur();
        }
        //function GetUserOrgNOs() {
        //    var UserID = getClientInfo("UserID");
        //    $.ajax({
        //        type: "POST",
        //        url: '../handler/jqDataHandle.ashx?RemoteName=sPettyCash.PettyCash', 
        //        data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
        //        cache: false,
        //        async: false,
        //        success: function (data) {
        //            var rows = $.parseJSON(data);
        //            if (rows.length = 1) {
        //                $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
        //                var rowData = $("#dataFormMasterApplyOrg_NO").combobox('getSelectItem');
        //                setTimeout(function () {
        //                var whereStr = "CostCenterID='" + rowData.CostCenterID + "'";
        //                $("#dataFormMasterCostCenterID").combobox('setWhere', whereStr);
        //                }, 400);
        //                $("#dataFormMasterCostCenterID").combobox('setValue', rowData.CostCenterID);
        //                $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
        //            }
        //         }
        //    }
        //    );
        //}
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPettyCash.PettyCash" runat="server" AutoApply="True"
                DataMember="PettyCash" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="零用金單號" Editor="text" FieldName="PettyCashID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProofNO" Editor="text" FieldName="ProofNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="摘要" Editor="text" FieldName="AccountNotes" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="申請金額" Editor="numberbox" FieldName="PettyCashAmt" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="申請稅額" Editor="numberbox" FieldName="PettyCashTax" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountYM" Editor="text" FieldName="AccountYM" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AccountID" Editor="text" FieldName="AccountID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
              
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="零用金申請" Width="655px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PettyCash" HorizontalColumnsCount="4" RemoteName="sPettyCash.PettyCash" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" Width="900px" OnCancel="CloseDataForm" IsAutoPause="False" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="OnApplydataFormMaster" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="零用金單號" Editor="text" FieldName="PettyCashID" Format="" Width="110" ReadOnly="True" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" maxlength="0" Span="3" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" FieldName="ApplyEmpID" Width="120" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sPettyCash.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="130" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPettyCash.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectOrg_NO,panelHeight:200" Span="1" NewRow="False" ReadOnly="True" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sPettyCash.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="132" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="textarea" FieldName="PettyCashGist" maxlength="512" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="487" EditorOptions="height:37" />
                        <JQTools:JQFormColumn Alignment="left" Caption="摘要" Editor="textarea" FieldName="AccountNotes" Format="" Width="487" maxlength="500" Span="4" EditorOptions="height:75" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預算年度" Editor="infocombobox" EditorOptions="valueField:'VoucherYear',textField:'VoucherYear',remoteName:'sPettyCash.VoucherYear',tableName:'VoucherYear',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VoucherYear" MaxLength="0" ReadOnly="False" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" Width="130" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPettyCash.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" MaxLength="0" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" FieldName="AccountType" Width="117" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sPettyCash.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectAccountType,panelHeight:200" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'BudgetType',textField:'BudgetTypeName',remoteName:'sPettyCash.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectBudgetType,panelHeight:200" FieldName="BudgetType" Visible="True" Width="132" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sPettyCash.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:GetAccountID,panelHeight:200" FieldName="AccountID" Format="" Span="1" Width="215" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sPettyCash.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectAcSubno,panelHeight:200" FieldName="AcSubNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="306" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據年月" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sPettyCash.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" MaxLength="0" ReadOnly="False" Span="2" Visible="True" Width="132" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支出憑據" Editor="infocombobox" FieldName="ProofTypeID" Format="" Width="120" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sPettyCash.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:ProofTypeIDOnSelect,panelHeight:200" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據單號" Editor="text" FieldName="ProofNO" Format="" Width="125" Span="1" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sPettyCash.PayType',tableName:'PayType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Format="" ReadOnly="True" Span="2" Visible="True" Width="132" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支付總額" Editor="text" FieldName="PettyCashTotal" ReadOnly="False" Visible="True" Width="115" OnBlur="PettyCashTotalOnBlur" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="PettyCashAmt" Format="" Width="125" ReadOnly="False" Visible="True" OnBlur="PettyCashAmtOnBlur" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="PettyCashTax" Format="" Width="125" ReadOnly="False" Visible="True" OnBlur="PettyCashTaxOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="給付年月" Editor="text" FieldName="AccountYM" Format="" Width="100" Visible="False" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsSettleAccount" Editor="text" FieldName="IsSettleAccount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" ReadOnly="False" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PettyCashAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PettyCashTax" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsSettleAccount" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PettyCashTotal" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="ApplyOrg_NO" RemoteMethod="False" DefaultMethod="GetUserOrg" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgCostCenter" FieldName="CostCenterID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgNOParent" FieldName="Org_NOParent" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="PayTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="PettyCashID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PettyCashGist" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PettyCashTotal" RangeFrom="1" RangeTo="300000" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <br />
   
            </JQTools:JQDialog>
            </div>
        <%--<script type="text/javascript">
            $(":input").css("background", backcolor);
         </script>--%>
    </form>
</body>
</html>
