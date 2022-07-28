<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_SecurityDeposit.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            
            $('#dataFormMasterRequisitionNO').after($('#ReqButton'));
            $("#ReqButton").hide();

            //請款單的預定付款日期
            $('#JQDataForm1PlanPayDate').datebox({
                onSelect: function (date) {
                    ChkException();
                }
            }).combo('textbox').blur(function () {
                ChkException();
            });

            //var Link = $("<a>").attr({ 'href': '../JB_ADMIN/BizTravelFeeDetails/費用明細表格式.xlsx', 'target': '_blank' }).text("格式");
            //$('#dataFormMasterFeeDetailsPath').closest('td').append(Link);

        });

        //OnSelect_ContractNO(合約編號)
        function OnSelect_dataFormMasterContractNO(row) {
            $("#dataFormMasterDepositAmount").numberbox('setValue', row.GuarantyAmount);
            $("#dataFormMasterDepositAmount").focus();
            $("#dataFormMasterCusSupplier").refval('setValue', row.ContractB);
            if(row.EntityType=='供應商'){
                $("#dataFormMasterDepositProperty").combobox('setValue', 1);//存出
                $("#ReqButton").show();
            } else if (row.EntityType == '客戶') {
                $("#dataFormMasterDepositProperty").combobox('setValue', 2);//存入
                $("#ReqButton").hide();
            } else {
                $("#ReqButton").hide();
            }

        }

        function OnApply_dataFormMaster() {
            //if ($("#dataFormMasterInOutWay").combobox('getValue') == '2' && $("#dataFormMasterCusSupplierAccName").val() == '') {
                //alert("收支方式選匯款，客戶供應商戶名為必填");
                //$("#dataFormMasterCusSupplierAccName").focus();
                //return false;
            //}

            
        }

        //保證金屬性
        function OnSelect_DepositProperty(row) {
            if (row.PropertyValue == '1') {//存出
                $("#ReqButton").show();
                //過濾客戶供應商
                //$("#dataFormMasterCusSupplier").refval("setWhere", "EntityType = '供應商'");
                //$("#dataFormMasterCusSupplier").refval("setValue", "");
            } else if (row.PropertyValue == '2') {//存入
                $("#ReqButton").hide();
                //$("#dataFormMasterCusSupplier").refval("setWhere", "EntityType = '客戶'");
                //$("#dataFormMasterCusSupplier").refval("setValue", "");
            } else {
                $("#ReqButton").hide();
            }
        }

        //OnClick_ReqButton(請款單按鈕)
        function ReqButtonOnClick() {
            openForm('#JQDialog3', {}, "inserted", 'dialog');
        }

        //(工具)
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }

        //(工具)
        function RedTd(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' });
            });
        }

        //(工具)
        function CheckCombobox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).combobox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }

        //(工具)
        function CheckDatebox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).datebox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).datebox("textbox").focus();
                return false;
            }
        }

        //(工具)
        function CheckVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).val();
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                if (fieldName != 'InsuraceAmt') {
                    alert('注意!!,未填寫 " ' + FieldNameC + ' " ,請填寫!!');
                    $(FormName + fieldName).focus();
                    return false;
                } else if (fieldName == 'InsuraceAmt') {
                    alert('注意!!，投保總金額無金額，請填寫「旅平險申請」的「請款金額」!!');
                    $(FormName + fieldName).focus();
                    return false;
                }
            }
        }

        //(工具)
        function CheckUpload(FormName, fieldName) {
            var infofileUpload = $(FormName + fieldName);
            var FieldNameC = infofileUpload.closest('td').prev('td').text();
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next())
            if (infofileUploadvalue.val() == '' || infofileUploadvalue.val() == undefined) {
                alert('注意!!,未上傳 " ' + FieldNameC + ' " ,請上傳!!');
                infofileUpload.focus();
                return false;
            }
        }

        //(工具)
        function CheckRefVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).refval('selectItem').text;
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }

        function JQDataForm1OnApplied() {
            var ReqNo = $('#JQDataForm1RequisitionNO').val();
            var RoleID = getClientInfo("_GroupID");//_GroupName
            $.ajax({//請款單起單
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_SecurityDeposit.SecurityDeposit', //連接的Server端，command
                data: "mode=method&method=" + "MakeRequisition" + "&parameters=" + ReqNo, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {//data == true會進不去
                        GetSumRACountRA();
                        alert('請款單已成功申請');
                    } else {
                        alert('請款單申請失敗');
                    }
                    closeForm('#JQDialog3');
                }
            });
        }

        //(工具)抓請款單資料
        function GetSumRACountRA() {
            //var DepositNO = $('#dataFormMasterDepositNO').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_SecurityDeposit.SecurityDeposit', //連接的Server端，command
                data: "mode=method&method=" + "SelectRequisition" ,//+ "&parameters=" + DepositNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#dataFormMasterRequisitionNO').val(rows[0].RequisitionNO);
                    }
                    //if (rows[0].CountR > 0) {
                        //$('#ReqButton').linkbutton({ 'text': '請款單- 已申請' + rows[0].CountR + '筆' });
                    //}
                }
            });
        }





//-------------------#region copy from 請款單-------------------
        function JQDataForm1OnLoadSuccess() {
            var UserID = getClientInfo("UserID");
            GetUserOrgNOs();//依登入者來設ApplyOrg_NO申請部門、Org_NOParent直屬主管的部門
            var EmpFlowAgentList = GetEmpFlowAgentList();//登入者的有效代理人人員清單
            var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
            $('#JQDataForm1ApplyEmpID').combobox('setWhere', whereStr);
            //設定緊急付款、非付款日付款 欄位Caption 變顏色
            var flagIDs = ['#JQDataForm1IsUrgentPay', '#JQDataForm1IsNotPayDate'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });

            $('#JQDataForm1SourceBillNO').val($('#dataFormMasterDepositNO').val());

            //把出差人員姓名帶到請款單的請款事由
            //var rowsdata = $('#dataGridDetailTvl1').datagrid('getRows');
            //var nameStr = '';
            //for (var i = 0; i < rowsdata.length; i++) {
            //    if (i == 0) nameStr = '，出差人員：' + rowsdata[i].AccStaffName
            //    else
            //        nameStr = nameStr + '、' + rowsdata[i].AccStaffName
            //}
            //$('#JQDataForm1RequisitionDescr').val($('#dataFormMasterBizTvlGist').val() + '-旅平險' + nameStr);

            //$('#JQDataForm1CostCenterID').combobox('setValue', $('#dataFormMasterCostCenterID').combobox('getValue'));
            //$('#JQDataForm1AccountType').combobox('setValue', $('#dataFormMasterAccountType').combobox('getValue'));
            //$('#JQDataForm1AccountID').combobox('setValue', $('#dataFormMasterAccountID').combobox('getValue'));
            //$('#JQDataForm1CompanyID').combobox('setValue', $('#dataFormMasterCompanyID').combobox('getValue'));
            //$('#JQDataForm1RequisitionNotes').val('出差起訖日期' + $('#dataFormMasterTvlDateS').datebox('getValue') + "~" + $('#dataFormMasterTvlDateE').datebox('getValue'));
            $("#JQDataForm1RequisitAmt").focus();
            $("#JQDataForm1RequisitAmt").numberbox('setValue', $("#dataFormMasterDepositAmount").val());
            $("#JQDataForm1PayTo").refval('setValue', $("#dataFormMasterCusSupplier").refval('getValue'));
            $("#JQDataForm1PayTypeID").combobox('setValue', $("#dataFormMasterInOutWay").combobox('getValue'));

            var DisabledFieldName = [];
            var DisabledComboboxName = ['ApplyEmpID'];
            DisableFields('#JQDataForm1', DisabledFieldName, DisabledComboboxName);

            //var RedFieldName = ['CompanyID', 'CostCenterID', 'AccountType', 'AccountID', 'CostCenterID', 'RequisitionDescr', 'RequisitionTypeID', 'ProofTypeID','RequisitAmt', 'PayTo', 'PayTypeID', 'PayTermID', 'PlanPayDate'];
            //RedTd('#JQDataForm1', RedFieldName);
        }

        //(工具)以預付日期來設 非付款日付款、緊急付款
        function ChkException() {
            var mess = "";
            var bdt = $('#JQDataForm1PlanPayDate').combo('textbox').val();//預付日期
            var dt = new Date(bdt);
            var days = dt.getDate()
            if ((days != 5) && (days != 25)) {
                $('#JQDataForm1IsNotPayDate').checkbox('setValue', 1);//非付款日付款
            }
            else {
                $('#JQDataForm1IsNotPayDate').checkbox('setValue', 0);
            }
            var dd = new Date();
            var dc = (dt - dd) / 86400000;
            if (dc.toPrecision() <= 5) {
                $('#JQDataForm1IsUrgentPay').checkbox('setValue', 1);//緊急付款
            }
            else {
                $('#JQDataForm1IsUrgentPay').checkbox('setValue', 0);
            }
        }
        
        function JQDataForm1OnApply() {

            var FormName = '#JQDataForm1';
            var fieldName = 'CompanyID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'CostCenterID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'AccountType';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'AccountID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'RequisitionDescr';
            if (CheckVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'RequisitionTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'ProofTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'RequisitAmt';
            if (CheckVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTo';
            if (CheckRefVal(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTypeID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            var fieldName = 'PayTermID';
            if (CheckCombobox(FormName, fieldName) == false) { return false; }
            fieldName = 'PlanPayDate';
            if (CheckDatebox(FormName, fieldName) == false) { return false; }

            var pre = confirm("確定起單-請款單?");
            if (pre == true) {
                return true;
            } else {
                return false;
            }
        }

        //OnSelect_PayTo(受款人),來設定付款條件PayTermID與付款方式PayTypeID並由付款方式改變須匯款費、匯款費誰付, 由付款條件改變預付日期
        function GetPayTerm(rowData) {
            $("#JQDataForm1PayTermID").combobox('setValue', rowData.PayTermID);
            $("#JQDataForm1PayTypeID").combobox('setValue', rowData.PayTypeID);
            var pp = $("#JQDataForm1PayTypeID").combobox('getValue'); //combobox 取值
            if (pp == 2) {
                $("#JQDataForm1IsRemit").checkbox('setValue', rowData.IsRemit);//設須匯款費
                $("#JQDataForm1RemitType").combobox('setValue', '廠商付');//設匯款費付款方式(誰付)
                $("#JQDataForm1Remit").val(30); //text給值
            }
            else {
                $("#JQDataForm1IsRemit").checkbox('setValue', 0);
                $("#JQDataForm1RemitType").combobox('setValue', "");
                $("#JQDataForm1Remit").val(0); //text給值
            }
            var PayTerm = $("#JQDataForm1PayTermID").combobox('getText');
            var WorkDays = 5; //作業最短日期
            var tdate = GetPlanPayDate(PayTerm, WorkDays, $("#JQDataForm1ApplyDate").datebox('getValue'));
            $('#JQDataForm1PlanPayDate').datebox('setValue', tdate);
        }

        //OnBlur_IsRemit(需匯款人)
        function CheckRemitType() {//需匯款費Blur時會呼叫
            var kk = $("#JQDataForm1IsRemit").checkbox('getValue')//須匯款費
            if (kk != 1) {
                $("#JQDataForm1RemitType").combobox('setValue', "");//匯款費誰付
                $("#JQDataForm1Remit").val(0); //text給值
            }
            else {
                $("#JQDataForm1RemitType").combobox('setValue', '廠商付');
                $("#JQDataForm1Remit").val(30); //text給值
            }
        }

        //OnSelect_PayTypeID(付款方式),來設定須匯款費與匯款費誰付
        function GetPayType(rowData) {
            var pp = $("#JQDataForm1PayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2) {
                $("#JQDataForm1IsRemit").checkbox('setValue', 0);//須匯款費
                $("#JQDataForm1RemitType").combobox('setValue', "");//匯款費誰付
                $("#JQDataForm1Remit").val(0); //text給值
            }
        }

        //OnSelect_PayTermID(付款條件),來設定預付日期
        function GetPayDateByTerm() {
            var PayTerm = $("#JQDataForm1PayTermID").combobox('getText');
            var tdate = GetPlanPayDate(PayTerm, 5, $("#JQDataForm1ApplyDate").datebox('getValue'));
            $('#JQDataForm1PlanPayDate').datebox('setValue', tdate);
        }

        //(工具)傳入付款條件,作業天數,申請日期→回傳預付日期
        function GetPlanPayDate(PayTerm, WorkDays, PlanPayDate) {
            var now = new Date(PlanPayDate);
            if (PayTerm == "當月") {
                var newDate = DateAdd("d ", WorkDays, now).toLocaleDateString();
            }
            else {
                var days = parseInt(PayTerm, 10);
                var newDate = DateAdd("d ", days, now).toLocaleDateString();
            }
            var ttDate = GetPayDate(newDate);
            return ttDate;
        }

        //(工具)計算付款日(回傳預付日期)
        function GetPayDate(PayDate) {
            var Dt = new Date(PayDate);
            var year = Dt.getFullYear();
            var month = Dt.getMonth() + 1;
            var Bt = Dt.toLocaleDateString();
            var Dt10 = new Date(year + '/' + month + '/' + '10').toLocaleDateString();
            var Dt25 = new Date(year + '/' + month + '/' + '25').toLocaleDateString();
            if (Bt <= Dt10) {
                return Dt10;
            }
            else
                if (Bt <= Dt25) {
                    return Dt25;
                }
                else {
                    return Dt25;
                }
        }

        //(工具)
        function DateAdd(interval, number, date) {
            switch (interval) {
                case "y ": {
                    date.setFullYear(date.getFullYear() + number);
                    return date;
                    break;
                }
                case "q ": {
                    date.setMonth(date.getMonth() + number * 3);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMonth(date.getMonth() + number);
                    return date;
                    break;
                }
                case "w ": {
                    date.setDate(date.getDate() + number * 7);
                    return date;
                    break;
                }
                case "d ": {
                    date.setDate(date.getDate() + number);
                    return date;
                    break;
                }
                case "h ": {
                    date.setHours(date.getHours() + number);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMinutes(date.getMinutes() + number);
                    return date;
                    break;
                }
                case "s ": {
                    date.setSeconds(date.getSeconds() + number);
                    return date;
                    break;
                }
                default: {
                    date.setDate(d.getDate() + number);
                    return date;
                    break;
                }
            }
        }

        //OnSelect_ApplyEmpID(申請員工)→申請部門、直屬主管的部門
        function OnSelectEmployee(rowData) {
            //var whereStr = "EmployeeID =  " + "'" + UserID + "'";
            //$('#dataFormMasteShortTermNO').combobox('setWhere', '1=2');
            //$('#dataFormMasteShortTermNO').combobox('setWhere', whereStr);

            $("#JQDataForm1ApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#JQDataForm1Org_NOParent").val(rowData.OrgNOParent);
        }

        //當點按關閉按鈕時,關閉目前Tab(JQDataForm1的Cancel時)
        //function CloseDataForm() {
        //    //self.parent.closeCurrentTab();
        //    //self.closeCurrentTab();
        //    //return false;
        //}

        //validateMaster檢查憑證號碼
        function CheckProofNO() {
            var ProofType = $("#JQDataForm1ProofTypeID").combobox("getValue");
            var ProofNO = $("#JQDataForm1ProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;
        }

        //(工具)
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "請款單申請";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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

        //(工具)由登入者設定申請部門、直屬主管的部門
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#JQDataForm1ApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        $("#JQDataForm1Org_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }

        //OnSelect_CostCenterID(成本中心)→CostCenterName，清空科目類別,會計科目
        function CostCenterIDOnSelect1() {
            var tt = $('#JQDataForm1CostCenterID').combobox('getText');
            $('#JQDataForm1CostCenterName').val(tt);
            $("#JQDataForm1AccountType").combobox('setValue', "");
            $("#JQDataForm1AccountID").combobox('setValue', "");
        }

        //OnSelect_AccountType(科目類型)→會計科目
        function AccountTypeOnSelect1(rowData) {
            $("#JQDataForm1AccountID").combobox("setValue", "");
            var CostCenterID = $("#JQDataForm1CostCenterID").combobox("getValue");
            var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            $("#JQDataForm1AccountID").combobox('setWhere', FiltStr);
        }

        //=============================檢附憑證=>發票時才須計算(但會計科目=>交際費 除外)=========================================
        //=========================================請款金額 推 進項稅額*1.05(四捨五入), 未稅金額=========================================
        function OnBlurRequisitAmt() {
            var RequisitAmt = 0;
            var RequisitAmtTax = 0;
            var RequisitAmtNoTax = 0;
            var ProofTypeID = $('#JQDataForm1ProofTypeID').combobox('getValue');//檢附憑證=>1 發票
            var AccountText = $("#JQDataForm1AccountID").combobox('getText');//交際費 除外
            if (ProofTypeID == "1" && AccountText != "交際費") {
                var RequisitAmt = $("#JQDataForm1RequisitAmt").val();
                RequisitAmtNoTax = Math.round(RequisitAmt / 1.05);
                RequisitAmtTax = RequisitAmt - RequisitAmtNoTax;
            } else {
                RequisitAmtNoTax = $("#JQDataForm1RequisitAmt").val();
            }

            $("#JQDataForm1RequisitAmtTax").numberbox('setValue', RequisitAmtTax);
            $("#JQDataForm1RequisitAmtTax").val(RequisitAmtTax);
            $("#JQDataForm1RequisitAmtNoTax").numberbox('setValue', RequisitAmtNoTax);
            $("#JQDataForm1RequisitAmtNoTax").val(RequisitAmtNoTax);
        }

        //(工具)
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
        //(工具)
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
        //(工具)
        function GetUserOrgCostCenter() {
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
                        _return = rows[0].OrgCostCenter;
                    }
                }
            })
            return _return;
        }
//-------------------#endregion copy from 請款單-------------------

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sForm_SecurityDeposit.SecurityDeposit" runat="server" AutoApply="True"
                DataMember="SecurityDeposit" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
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
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="保證金單" Width="700px" DialogTop="40px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SecurityDeposit" HorizontalColumnsCount="2" RemoteName="sForm_SecurityDeposit.SecurityDeposit" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApply_dataFormMaster" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金單號" Editor="text" FieldName="DepositNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="inforefval" FieldName="ContractNO" Format="" maxlength="0" Width="180" EditorOptions="title:'合約',panelWidth:600,panelHeight:200,remoteName:'sForm_SecurityDeposit.ContractNO',tableName:'ContractNO',columns:[{field:'ContractNO',title:'合約編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContractB',title:'客戶供應商代號',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContractName',title:'合約名稱',width:130,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'PhysicalContractNO',title:'紙本合約編號',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EntityType',title:'客戶供應商',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'ContractNO',textField:'ContractNO',valueFieldCaption:'ContractNO',textFieldCaption:'ContractNO',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:OnSelect_dataFormMasterContractNO,selectOnly:true,capsLock:'none',fixTextbox:'false'" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金屬性" Editor="infocombobox" FieldName="DepositProperty" Format="" maxlength="0" Width="180" EditorOptions="valueField:'PropertyValue',textField:'PropertyName',remoteName:'sForm_SecurityDeposit.DepositProperty',tableName:'DepositProperty',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelect_DepositProperty,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商" Editor="inforefval" FieldName="CusSupplier" Format="" maxlength="0" Width="180" EditorOptions="title:'客戶供應商',panelWidth:600,remoteName:'sForm_SecurityDeposit.VenderCustomer',tableName:'VenderCustomer',columns:[{field:'ID',title:'編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Name',title:'名稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Tel',title:'電話',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Addr',title:'地址',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EntityType',title:'類型',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'ID',textField:'Name',valueFieldCaption:'名稱',textFieldCaption:'名稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保證金金額" Editor="numberbox" FieldName="DepositAmount" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Notes" Format="" maxlength="0" Width="420" EditorOptions="height:50" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="存出入日期" Editor="datebox" FieldName="InOutDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="收支方式" Editor="infocombobox" FieldName="InOutWay" Format="" maxlength="0" Width="180" EditorOptions="valueField:'InOutWayValue',textField:'InOutWayName',remoteName:'sForm_SecurityDeposit.InOutWay',tableName:'InOutWay',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶供應商戶名" Editor="text" FieldName="CusSupplierAccName" Format="" maxlength="0" Width="420" NewRow="False" Span="2" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票號碼" Editor="text" FieldName="VoucherNO" Format="" maxlength="0" Width="180" NewRow="False" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQButton ID="ReqButton" runat="server" Icon="icon-add" OnClick="ReqButtonOnClick" Text="申請" />
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="DepositNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractNO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DepositProperty" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CusSupplier" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DepositAmount" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog3" runat="server"  BindingObjectID="JQDataForm1" Title="請款單申請" Width="730px" DialogLeft="10px" DialogTop="80px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisition.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" Width="720px" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSuccess" OnApplied="JQDataForm1OnApplied" OnApply="JQDataForm1OnApply">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="來源單號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True" Span="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" ReadOnly="False" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisition.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisition.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="True" Width="130" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="90" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CostCenterIDOnSelect1,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="133" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'AccountType',textField:'AccountType',remoteName:'sForm_SecurityDeposit.AccountType',tableName:'AccountType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AccountTypeOnSelect1,panelHeight:200" FieldName="AccountType" MaxLength="0" ReadOnly="False" Span="1" Width="130" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AccountID',textField:'AccountName',remoteName:'sForm_SecurityDeposit.AccountTitle',tableName:'AccountTitle',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AccountID" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" Span="3" Width="580" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnBlurRequisitAmt,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                        
                   <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="155" OnBlur="" />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" Format="" OnBlur="OnBlurRequisitAmt"/>
                        
                   <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="RequisitAmtNoTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="125" />
                   <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="RequisitAmtTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="130" />  
                   <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sRequisition.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="395" />
                   <JQTools:JQFormColumn Alignment="left" Caption="會辦總務" Editor="checkbox" FieldName="NeedGeneralAffairs" Width="80" NewRow="True" OnBlur="" maxlength="0" Span="1" /> 
                        
                               
                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="580" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'受款人篩選',panelWidth:360,remoteName:'sRequisition.Vendor',tableName:'Vendor',columns:[{field:'VendShortName',title:'供應商簡稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'選取',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:GetPayTerm,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="PayTo" Format="" maxlength="0" span="1" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisition.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayType,panelHeight:200" FieldName="PayTypeID" Format="" Width="133" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisition.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayDateByTerm,panelHeight:200" FieldName="PayTermID" Format="" Width="183" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="133" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" Width="80" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" FieldName="PayToNotes" ReadOnly="False" Span="3" Visible="True" Width="580" EditorOptions="height:40" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Span="1" Width="130" Visible="False" ReadOnly="False" maxlength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Visible="False" Width="130" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需匯款費" Editor="checkbox" FieldName="IsRemit" Visible="True" Width="80" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" EditorOptions="on:1,off:0" OnBlur="CheckRemitType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款費付款" Editor="infocombobox" EditorOptions="items:[{value:'廠商付',text:'廠商付',selected:'false'},{value:'公司付',text:'公司付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RemitType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出差申請單號" Editor="text" FieldName="SourceBillNO" ReadOnly="True" span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="加簽對象" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,panelHeight:200,remoteName:'sRequisition.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'工號',textFieldCaption:'姓名',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="DynamicUser" MaxLength="0" ReadOnly="False" Visible="True" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="宿管人員申請" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEmpGroupID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="解除設定" Editor="checkbox" FieldName="unlock" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="15" />


                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="RequisitionNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" FieldName="CreateBy" DefaultValue="_username" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequistKindID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequisitionTypeID" RemoteMethod="True" />

                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrg" FieldName="ApplyOrg_NO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgNOParent" FieldName="Org_NOParent" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgCostCenter" FieldName="CostCenterID" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <%--<JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="25000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />--%>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="25000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
        </JQTools:JQDialog>



        </div>
    </form>
</body>
</html>
