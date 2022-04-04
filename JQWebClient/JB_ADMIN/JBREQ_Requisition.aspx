<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_Requisition.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8 ; noindex" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var sRequisitionNO = "";
        var sCompanyID = "";
        var sVoucherID = "";
        var iVoucherCount = 0;
        var backcolor = "#cbf1de"
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
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });
            var Notes = $('#dataFormMasterNeedGeneralAffairs').closest('td');
            Notes.append('＊若選取時，須填寫「請款備註」：總務要配合的相關資訊')
            //$("#dataFormMasterNeedGeneralAffairs").closest('td').after("<td colspan='4' style=':blue'>若選color取時，須填寫「請款備註」：總務要配合的相關資訊</td>");//.css("color", "blue");
            //$("#dataFormMasterNeedGeneralAffairs").closest('td').after("<td style=':blue'>若選color取時，須填寫「請款備註」：總務要配合的相關資訊</td>");//.css("color", "blue");
            //$("#dataFormMasterNeedGeneralAffairs").closest('td').append("須填請款備註").css("color", "blue");
            $('#dataFormMasterDynamicUser').closest('td').append("  ").append($("#dataFormMasterClear"));
            $("#dataFormMasterClear").bind("click", function () {
                //$('#dataFormMasterDynamicUser').val("");
                //$('#dataFormMasterDynamicUser').next('span').children('input').val("");
                $('#dataFormMasterDynamicUser').refval("setValue", "")
            });
            //$("#dataFormMasterPlanPayDate").datetimepicker().on('changeDate', function (ev) {
            //    alert(ev);
            //});
            //預付日期選取後動作
            //$('#dataFormMasterPlanPayDate').datebox({
            //    onchange: function (date) {
            //      ChkException();
            //    }
            //}).combo('textbox').blur(function () {
            //    ChkException();
            //});
            //在客戶負責人加入經濟部商業司超連結
            var RequisitKindLink = $("<a>").attr({ 'href': '../JB_ADMIN/img/委任權限表.pdf' }).attr({ 'target': '_blank' }).text("     請款性質說明");
            var RequistKind = $('#dataFormMasterRequistKindID').closest('td');
            RequistKind.append(RequisitKindLink);

            //在匯費金額 後 加入 傳票設定連結   
            var parameters = Request.getQueryStringByName("P1");
            if (parameters == "SERVICE") {
                //$('<a>', { id: 'lbglVoucher', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#dataFormMasterIsEmpGroupID').closest("td")).linkbutton();
            var glVoucherLink = $('<a>', { href: 'javascript:void(0)', name: 'glVoucher', onclick: 'OpeneglVoucher.call(this)' }).linkbutton({ plain: false, text: '傳票設定' })[0].outerHTML
                var daglVoucher = $('#dataFormMasterRemit').closest('td');
                daglVoucher.append("&nbsp;&nbsp;" + glVoucherLink);
                
                //未稅金額 與 稅額 開放給會計修改
                $('#dataFormMasterRequisitAmtNoTax').removeAttr("disabled");
                $('#dataFormMasterRequisitAmtTax').removeAttr("disabled");
            }

            //----------------修改jqDataForm的Caption為Hypelink-----------------------------------
            //呼叫科目明細
            $("#dataFormDetail").form({
                onLoadSuccess: function (data) {
                    $("td", "#dataFormDetail").each(function (index) {
                        if ($(this).children().length == 0) {
                            if ($(this).html() == "呼叫明細") {
                                $(this).html("");
                                //$('<a>aa</a>').attr({
                                //    'href': 'bOrders1.aspx'
                                //}).appendTo(this);
                                $('<input type="image" img  src="img/clock_red.png" onclick="OpenSubAcnoToolTip()">').attr({
                                }).appendTo(this);

                                $("#dataFormDetailOpenToolTip").closest('td').hide();
                            }

                        }
                    });
                }
            });
        });       
        function OnSelectOrg_NO(rowData) {
            //$("#dataFormMasterCostCenterID").combobox('setValue', rowData.CostCenterID);
            var whereStr = "CostCenterID in (" + rowData.CostCenterID + ")";
            $("#dataFormMasterCostCenterID").combobox('setWhere', whereStr);
            var OrgParent = GetUserOrgNOParent();
            $("#dataFormMasterOrg_NOParent").val(OrgParent);
            return true;
        }
        function dataFormMasterOnApply() {
            var dataFormMasterApplyOrg_NO = $("#dataFormMasterApplyOrg_NO").combobox('getValue');
            if (dataFormMasterApplyOrg_NO == "" || dataFormMasterApplyOrg_NO == undefined) {
                alert('注意!!,未選取申請部門,請選取');
                $("#dataFormMasterApplyOrg_NO").focus();
                return false;
            }
            var dataFormMasterRequistKindID = $("#dataFormMasterRequistKindID").combobox('getValue');
            if (dataFormMasterRequistKindID == "" || dataFormMasterRequistKindID == undefined) {
                alert('注意!!,未選取請款性質,請選取');
                $("#dataFormMasterRequisitKindID").focus();
                return false;
            }
            var dataFormMasterCompanyID = $("#dataFormMasterCompanyID").combobox('getValue');

            if (dataFormMasterCompanyID == "" || dataFormMasterCompanyID == undefined) {
                alert('注意!!,未選取公司別,請選取');
                $("#dataFormMasterCompanyID").focus();
                return false;
            }
            //檢查預算年月
            var dataFormMasterVoucherYear = $("#dataFormMasterVoucherYear").combobox('getValue');
            if (dataFormMasterVoucherYear == "" || dataFormMasterVoucherYear == undefined) {
                alert('注意!!,未選取預算年月,請選取');
                $("#dataFormMasterVoucherYear").focus();
                return false;
            }
            var dataFormMasterCostCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
            if (dataFormMasterCostCenterID == "" || dataFormMasterCostCenterID == undefined) {
                alert('注意!!,未選取成本中心,請選取');
                $("#dataFormMasterCostCenterID").focus();
                return false;
            }
            var dataFormMasterBudgetType = $("#dataFormMasterBudgetType").combobox('getValue');
            if (dataFormMasterBudgetType == "" || dataFormMasterBudgetType == undefined) {
                   alert('注意!!,未選取科目類別,請選取');
                   $("#dataFormMasterBudgetType").focus();
                   return false;
             }
            var dataFormMasterAccountID = $("#dataFormMasterAccountID").combobox('getValue');
            if ((dataFormMasterAccountID.substring(0, 1)) != "0") {
                alert('注意!!,輸入的會計科目不正確,請選取');
                var CostCenterID = $("#dataFormMasterCostCenterID").combobox("getValue");
                var BudgetType = $("#dataFormMasterBudgetType").combobox("getValue");
                var FiltStr = "BudgetType = " + "'" + BudgetType + "'" + " and CostCenterID = " + "'" + CostCenterID + "'";
                $("#dataFormMasterAccountID").combobox('setWhere', FiltStr);
                
                $("#dataFormMasterAccountID").combobox('setValue', '');
                $("#dataFormMasterAccountID").focus();
                return false;

            }
            if (dataFormMasterAccountID == "" || dataFormMasterAccountID == undefined) {
                alert('注意!!,未選取會計科目,請選取');
                $("#dataFormMasterAccountID").focus();
                return false;
            }
            var dataFormMasterRequisitionTypeID = $("#dataFormMasterRequisitionTypeID").combobox('getValue');
            if (dataFormMasterRequisitionTypeID == "" || dataFormMasterRequisitionTypeID == undefined) {
                alert('注意!!,未選取請款依據,請選取');
                $("#dataFormMasterRequisitionTypeID").focus();
                return false;
            }
      
            //檢查發票年月
            var dataFormMasterInvoiceYM = $("#dataFormMasterInvoiceYM").combobox('getValue');
            if (dataFormMasterInvoiceYM == "" || dataFormMasterInvoiceYM == undefined) {
                alert('注意!!,未選取發票年月,請選取');
                $("#dataFormMasterInvoiceYM").focus();
                return false;
            }
            var dataFormMasterProofTypeID = $("#dataFormMasterProofTypeID").combobox('getValue');
            if (dataFormMasterProofTypeID == "" || dataFormMasterProofTypeID == undefined) {
                alert('注意!!,未選取檢附憑證,請選取');
                $("#dataFormMasterProofTypeID").focus();
                return false;
            }
            var dataFormMasterPayTermID = $("#dataFormMasterPayTermID").combobox('getValue');
            if (dataFormMasterPayTermID == "" || dataFormMasterPayTermID == undefined) {
                alert('注意!!,未選取付款條件,請選取');
                $("#dataFormMasterPayTermID").focus();
                return false;
            }
            //檢查預付日期必填,且與申請日期不可大於90天
            var dataFormMasterPlanPayDate = $("#dataFormMasterPlanPayDate").datebox('getValue')
            if (dataFormMasterPlanPayDate == "" || dataFormMasterPlanPayDate == undefined) {
                alert('注意!!,未選取或輸入預付日期,請選取');
                $("#dataFormMasterPlanPayDate").focus();
                return false;
            }
            var dataFormMasterApplyDate = $("#dataFormMasterApplyDate").datebox('getValue');
            var diffdays = datediffindays(dataFormMasterApplyDate, dataFormMasterPlanPayDate);
            if (diffdays > 90) {
                alert('注意!!,您可能選錯預付日期了,請重選');
                $("#dataFormMasterPlanPayDate").focus();
                return false;
            }

            var dataFormMasterPayTypeID = $("#dataFormMasterPayTypeID").combobox('getValue');
            if (dataFormMasterPayTypeID == "" || dataFormMasterPayTypeID == undefined) {
                alert('注意!!,未選取付款方式,請選取');
                $("#dataFormMasterPayTypeID").focus();
                return false;
            }
            if ($("#dataFormMasterNeedGeneralAffairs").checkbox('getValue') == true && $.trim($("#dataFormMasterRequisitionNotes").val()) == "") {
                alert('注意!!,未填寫「請款備註」,請填寫');
                $("#dataFormMasterRequisitionNotes").focus();
                return false;
            }            
            var parameters = Request.getQueryStringByName("P1");
            if (parameters == "SERVICE") {
                //檢查請款金額=未稅金額+稅額
                var RequisitAmt = parseInt($("#dataFormMasterRequisitAmt").val());
                var RequisitAmtTax = parseInt($("#dataFormMasterRequisitAmtTax").val());
                var RequisitAmtNoTax = parseInt($("#dataFormMasterRequisitAmtNoTax").val());
                if (RequisitAmt != RequisitAmtTax + RequisitAmtNoTax) {
                    alert('未稅金額、稅額不正確。');
                    return false;
                }
                //公司別=>1傑報資訊,2傑報人力,3傑誠人力,4傑信人力,5傑報健康,6海青專案
                //1,2,4才檢查
                if (dataFormMasterCompanyID == "1" || dataFormMasterCompanyID == "2" || dataFormMasterCompanyID == "4") {
                    //解除設訂有勾選則不檢查
                    var unlock = $("#dataFormMasterunlock").checkbox('getValue')
                    if (unlock != 1) {
                        if (getglVoucher() == 0) {
                            alert('傳票尚未設定。');
                            return false;
                        }
                    }
                }
            }
        };
        //計算傳入日期相差天數,date1:前日期,date2:近日期
        var datediffindays = function (date1, date2) {
            dt1 = new Date(date1);
            dt2 = new Date(date2);
            return Math.floor((Date.UTC(dt2.getFullYear(), dt2.getMonth(), dt2.getDate()) - Date.UTC(dt1.getFullYear(), dt1.getMonth(), dt1.getDate())) / (1000 * 60 * 60 * 24));
        }
        //----------------求得目前是否有設定傳票內容--------------------
        function getglVoucher() {
            //sRequisitionNO = 'PAY1081512';
            //var sRequisitionNO = 'PAY1081516';//$("#dataFormMasterRequisitionNO").val();
            sRequisitionNO = $("#dataFormMasterRequisitionNO").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "getRequisitionVoucher" + "&parameters=" + sRequisitionNO,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows != null && rows.length > 0) {
                        
                        iVoucherCount = rows[0].iVoucherCount;
                        
                    }

                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

            return iVoucherCount;
        }


        //檢查是否非期限內付款,緊急付款
        function dataFormMaster_OnLoadSuccess() {
            var UserID = getClientInfo("UserName");
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var sGroupID = '1071061'; //高專/外勞客服組
                $('#dataFormMasterIsEmpGroupID').checkbox('setValue',CheckApplyEmpIsGroupID(sGroupID));
                var cd  = getClientInfo("UserID");
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
                $("#dataFormMasterAccountID").combobox("setValue", "");
                $("#dataFormMasterAccountID").combobox('setWhere', "1=2");
                //setTimeout(function () {
                    var ApplyOrg_NO = GetUserOrg();
                    $("#dataFormMasterApplyOrg_NO").combobox('setValue', ApplyOrg_NO);
                //}, 100);
                //取得部門對應的成本中心-----------------------------------------
                //alert(filter);
                //alert('IN');
                setTimeout(function () {
                    var rowData = $("#dataFormMasterApplyOrg_NO").combobox('getSelectItem');
                    //alert(rowData.CostCenterID);
                    var filter = "CostCenterID in (" + rowData.CostCenterID + ")";
                    $("#dataFormMasterCostCenterID").combobox("setWhere", filter);
                }, 500);
                
                //------------------------------------------------------------
               
            } else {
                $('#dataFormMasterDynamicUser').closest('td').prev('td').hide();
                $('#dataFormMasterDynamicUser').closest('td').hide();
                //$('#dataFormMasterDynamicUser').next('span').children('span').hide();
            }

            if (parameters == "SERVICE" && mode == "0") {
                $('#dataFormMasterIsRemit').removeAttr("disabled");
                $("#dataFormMasterRemitType").combobox('enable');  //combobox 設為可用
                $('#dataFormMasterRemit').removeAttr("disabled");
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#dataFormMasterCompanyID").combobox().next('span').find('input').focus();
            }
            var SyncStatus = SyncVendorsFromJBHR_EEP(UserID);
            if (SyncStatus == "False") {
                alert("訊息通知!!員工資訊同步廠商資訊失敗,請通知管理室IT人員 分機-209/505,你仍可繼續作業")
                return;
            }
        }
        //同步更新廠商資訊
        function SyncVendorsFromJBHR_EEP(UserID) {
            var status = 'False'
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',
                data: "mode=method&method=" + "procSyncEmployeeVendor" + " &parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    status = data
                    //if (data == "True") {
                    //    $('#dataGridCenter1').datagrid("reload");
                    //}
                    //else {
                    //    alert("注意!! 聯絡人加入群組失敗")
                    //}
                }
            });
            return status;
        }

        //得到核示資訊
        function GetSignNotesData() {
            //var AbsentMinusID = $('#dataFormMasterAbsentMinusID').val();
            var no = $("#dataFormMasterRequisitionNO").val();
            //if (AbsentMinusID != "") {
            if (no != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',  //連接的Server端，command
                    data: "mode=method&method=" + "GetSignNotesData" + "&parameters=" + no,
                    cache: false,
                    async: true,
                    success: function (data) {
                        var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                           
                        $('#GridSignNotesData').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        if (rows.length == 0) {
                            $('#divSignNotesData').hide();
                        } else $('#divSignNotesData').show();
                    }
                });
            }
        }
        //取得有簽核內容簽核數
        function GetSignCount() {
            var RequisitionNO = $("#dataFormMasterRequisitionNO").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',
                data: "mode=method&method=" + "GetSignCount" + "&parameters=" + RequisitionNO,
                cache: false,
                async: false,
                success: function (data) {
                    cnt = $.parseJSON(data);
                }
            });
            return cnt;
        }
        function CheckApplyEmpIsGroupID(GroupID) {
            var ApplyEmpID = $("#dataFormMasterApplyEmpID").combobox('getValue');
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',
                data: "mode=method&method=" + "CheckApplyEmpIsGroupID" + "&parameters=" + ApplyEmpID + "," +  GroupID, 
                cache: false,
                async: false,
                success: function (data) {
                    cnt = data;
                }
            });
            if ((cnt == "Y")) {
                return true;
            }
            else {
                return false;
            }
        }

        function ChkException() {
            var mess = "";
            var bdt = $("#dataFormMasterPlanPayDate").datebox('getValue');
            //var bdt = $('#dataFormMasterPlanPayDate').combo('textbox').val();
            var dt = new Date(bdt);
            var days = dt.getDate()
            if ((days != 5) && (days != 25)) {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 1);
            }
            else {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 0);
            }
            var dd = new Date();
            var dc = (dt - dd) / 86400000;
            if (dc.toPrecision() <= 5) {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 1);
               }
            else {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 0);
            }
        }
        //當選取客戶時,重新設定付款條件與付款方式並改變預付日期
        function GetPayTerm(rowData) {
            $("#dataFormMasterPayTermID").combobox('setValue', rowData.PayTermID);
            $("#dataFormMasterPayTypeID").combobox('setValue', rowData.PayTypeID);
            if (rowData.PayTypeID == 2) {
                $("#dataFormMasterIsRemit").checkbox('setValue', rowData.IsRemit);
                $("#dataFormMasterRemit").val(rowData.Remit); //text給值
               }
              else
               {
                    $("#dataFormMasterIsRemit").checkbox('setValue',0);
                    $("#dataFormMasterRemitType").combobox('setValue', "");
                    $("#dataFormMasterRemit").val(0); //text給值
            }
            return true;
        }
        function CheckRemitType() {
            var kk = $("#dataFormMasterIsRemit").checkbox('getValue')
            if (kk != 1) {
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
            }
            else {
                $("#dataFormMasterRemitType").combobox('setValue', '廠商付');
                $("#dataFormMasterRemit").val(30); //text給值
            }
        }
        //當選取付款方式時,重新設定付款條件與付款方式並改變預付日期
        function GetPayType(rowData) {
            var pp = $("#dataFormMasterPayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2)
             {
                $("#dataFormMasterIsRemit").checkbox('setValue',0);
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
              }
        }
        //當選取付款條件時,依付款條件改變預付日期
        function GetPayDateByTerm() {
            //var PayTerm = $("#dataFormMasterPayTermID").combobox('getText');
            //var tdate = GetPlanPayDate(PayTerm, 5, $("#dataFormMasterApplyDate").datebox('getValue'));
            //$('#dataFormMasterPlanPayDate').datebox('setValue', tdate);
        }
       //傳入付款條件,作業天數,申請日期求得預付日期
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
        //計算付款日
        function GetPayDate(PayDate) {
            var Dt = new Date(PayDate);
            var year = Dt.getFullYear();
            var month = Dt.getMonth()+1;
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
        function OnSelectAccountType(rowData) {
            $("#dataFormMasterAccountID").combobox("setValue","");
            var CostCenterID = $("#dataFormMasterCostCenterID").combobox("getValue");
            //var FiltStr = "AccountType=" + "'" + rowData.AccountType + "'" + " and (LimitCostCenters='' or LimitCostCenters like '%" + CostCenterID + "%' or LimitCostCenters is null)";
            var FiltStr = "BudgetType = " + "'" + rowData.BudgetType + "'" + " and CostCenterID = " + "'" + CostCenterID + "'";
            $("#dataFormMasterAccountID").combobox('setWhere', FiltStr);
        }
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        //當點按關閉按鈕時,關閉目前Tab
        function OnSelectShortTermNO(rowData) {
            //var ReqAmt = parseInt($("#dataFormMasterRequisitAmt").val());
            //if ((ReqAmt + parseInt(rowData.VoidAmount)) > rowData.ShortTermAmount) {
            //    alert('注意!!請款金額大於選取的暫借單餘額:' + (rowData.ShortTermAmount - rowData.VoidAmount) + ',請確認後再選取');
            //    $("#dataFormMasterShortTermNO").combobox("setValue", "");
            //    $("#dataFormMasterShortTermNO").focus();
            //    return false;
            //}
        }
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
        //選取成本中心時,先清空
        function OnSelectCostCenterID() {
            $("#dataFormMasterBudgetType").combobox('setValue', "");
            $("#dataFormMasterAccountID").combobox('setValue', "");
            $("#dataFormMasterAccountID").combobox('setWhere', "1=2");
        }
        //取得此表單設登入者為有效代理人人員清單
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
        //function GetUserOrgNOs() {
        //   var UserID = getClientInfo("UserID");
        //    $.ajax({
        //        type: "POST",
        //        url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', 
        //        data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, 
        //        cache: false,
        //        async: false,
        //        success: function (data) {
        //            var rows = $.parseJSON(data);
        //            if (rows.length > 0) {
        //                $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
        //                var rowData = $("#dataFormMasterApplyOrg_NO").combobox('getSelectItem');
        //                $("#dataFormMasterCostCenterID").combobox('setValue', rowData.CostCenterID);
        //                $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
        //            }
        //        }
        //    }
        //    );
        //}
        //=============================檢附憑證=>發票時才須計算(但會計科目=>交際費 除外)=========================================
        //=========================================請款金額 推 進項稅額*1.05(四捨五入), 未稅金額=========================================
        function OnBlurRequisitAmt(rowData) {
            var RequisitAmt = 0;
            var RequisitAmtTax = 0;
            var RequisitAmtNoTax = 0;
            var ProofTypeID = $('#dataFormMasterProofTypeID').combobox('getValue');//檢附憑證=>1 發票
            var AccountText = $("#dataFormMasterAccountID").combobox('getText');//交際費 除外
            if (ProofTypeID == "1" && AccountText != "交際費") {
                var RequisitAmt = $("#dataFormMasterRequisitAmt").val();
                RequisitAmtNoTax = Math.round(RequisitAmt / 1.05);
                RequisitAmtTax = RequisitAmt - RequisitAmtNoTax;
            } else {
                RequisitAmtNoTax = $("#dataFormMasterRequisitAmt").val();
            }
            $("#dataFormMasterRequisitAmtTax").numberbox('setValue', RequisitAmtTax);     
            $("#dataFormMasterRequisitAmtTax").val(RequisitAmtTax);
            $("#dataFormMasterRequisitAmtNoTax").numberbox('setValue', RequisitAmtNoTax);
            $("#dataFormMasterRequisitAmtNoTax").val(RequisitAmtNoTax);
            //將會計科目主目,會計科目子目帶入
 
            //$("#dataFormMasterAcno").val(rowData.Acno_S);
            //$("#dataFormMasterSubAcno").val(rowData.SubAcno_S);
        }
        //=========================================開啟傳票設定內容=========================================
        function OpeneglVoucher() {

            openForm('#JQDialogVoucherOpen', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
            setTimeout(function () {
                SetglVoucher();
                queryGrid($('#dataGridDetail'));
            }, 500);
            

        }
       
        //由請款單傳回目前的公司別、傳票內容資訊
        function SetglVoucher() {
            //sRequisitionNO = 'PAY1081512';
                //var sRequisitionNO = 'PAY1081516';//$("#dataFormMasterRequisitionNO").val();

                sRequisitionNO = $("#dataFormMasterRequisitionNO").val();
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                    data: "mode=method&method=" + "getRequisitionVoucher" + "&parameters=" + sRequisitionNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows != null && rows.length > 0) {
                            sCompanyID = rows[0].glCompanyID;
                            sVoucherID = rows[0].VoucherID;
                            iVoucherCount = rows[0].iVoucherCount;
                            var VoucherDate = rows[0].VoucherDate;

                            //設定dataForm
                            $("#dataFormVoucherRequisitionNO").val(sRequisitionNO);
                            $("#dataFormVoucherCompanyID").combobox('setValue', sCompanyID);
                            $("#dataFormVoucherVoucherID").options('setValue', sVoucherID);

                            //是否Grid已有資料
                            if (iVoucherCount > 0) {
                                $("#dataFormVoucherVoucherDate").datebox('setValue', VoucherDate);
                                $("#dataFormVoucherVoucherDate").datebox("disable");
                                $('#lbVoucher').hide();
                            } else {
                                //$("#dataFormVoucherVoucherDate").removeAttr("disable");
                                $('#lbVoucher').show();
                            }
                        }

                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert(xhr.status);
                        alert(thrownError);
                    }
                });

        }
        //========================================= queryGrid ====================================================================================

        function queryGrid(dg) {//查詢後添加固定條件

            if ($(dg).attr('id') == 'dataGridDetail') {
                //查詢條件
                var result = [];
                if (sRequisitionNO != '') result.push("g.RequisitionNO = '" + sRequisitionNO + "'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        //========================================= 寫入傳票內容 ====================================================================================        
        function InsertVoucherData() {

            var VoucherMVoucherDate = $("#dataFormVoucherVoucherDate").datebox('getValue');
            if (VoucherMVoucherDate == "" || VoucherMVoucherDate == undefined) {
                alert('請選取傳票日期');
                $("#dataFormVoucherVoucherDate").focus();
                return false;
            }

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition', //連接的Server端，command
                data: "mode=method&method=" + "InsertRequisitionVoucher" + "&parameters=" + sRequisitionNO + "," + VoucherMVoucherDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    $('#dataGridDetail').datagrid('reload');
                    $('#lbVoucher').hide();
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }
        //========================================= Grid key in 顯示方式的顯示或隱藏 ====================================================================================   
        function ControlGrid() {

            //if (getEditMode($("#dataFormVoucher")) == 'inserted') {
            //    //Detail dataform 設定為 新增模式
            //    //setTimeout(function () {
            //    //    insertItem('#dataGridDetail');
            //    //}, 500);
            //    //Grid隱藏
            //    $('#dataGridDetail').datagrid('getPanel').show();
            //    $('#JQDialog2').show();
            //    $('#dataGridDetail2').datagrid('getPanel').hide();
            //} else {
            //    $('#dataGridDetail').datagrid('getPanel').hide();
            //    $('#JQDialog2').hide();
            //    $('#dataGridDetail2').datagrid('getPanel').show();
            //}
        }        

        //呼叫科目明細Tooltip
        function OpenSubAcnoToolTip() {
            openForm('#JQDialogToolTip', {}, 'viewed', 'dialog');

            var result = [];
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            if (CompanyID.trim() != '') result.push("CompanyID = " + CompanyID);

            var Acno = $("#dataFormDetailAcno").combobox('getValue');

            if (Acno.trim() != '') result.push("Acno = " + Acno);

            $('#ToolTip').datagrid('setWhere', result.join(' and '));
        }
        
        //========================================= dataFormVoucher ====================================================================================

        //新增前的檢查
        function OnInsertDetail() {
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            if (CompanyID == "") {
                alert('請選擇公司別!');
                return false;
            }
            var VoucherID = $("#dataFormVoucherVoucherID").options('getCheckedValue');
            if (VoucherID == "") {
                alert('請選擇傳票類別!');
                return false;
            }
        }
        //========================================= 公司別 & 科目 連動Combobox ====================================================================================   
        //主檔的 公司別 有變動時      
        //---------------------------------------選公司別觸發---------------------------------
        var CompanyID_OnSelect = function (rowdata) {
            //影響
            GetAcno("");//科目
            //RunGetSubAcno();//明細           
        }
        //得到科目資料
        var GetAcno = function (Acno) {
            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetAcno', { Company_ID: CompanyID, Ac_no: Acno });
            if (CodeList != null) {
                $("#dataFormDetailAcno").combobox('loadData', CodeList);//Detail
            }
        }
        function RunGetSubAcno() {
            //若DataFormDetails不為viewed狀態,則重跑
            if (getEditMode($("#dataFormDetail")) != 'viewed') {
                var Acno = $("#dataFormDetailAcno").combobox('getValue');
                GetSubAcno(Acno, "");
            }
        }
        //得到明細資料
        var GetSubAcno = function (Acno, SubAcno) {
            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetSubAcno', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (CodeList != null) $("#dataFormDetailSubAcno").combobox('loadData', CodeList);
        }
        //========================================= 科目 1.明細 2.摘碼代號 連動Combobox ====================================================================================   

        //---------------------------------------選取科目觸發---------------------------------
        var Acno_OnSelect = function (rowdata) {
            //$("#dataFormDetailDescribe").val("");
            //ClearAcnoCombo();明細 清空
            //1.明細
            GetSubAcno(rowdata.value, "");
            //2.摘碼代號
            GetDescribeID(rowdata.value, "");
        }

        function ClearAcnoCombo() {
            //1.明細 清空
            $("#dataFormDetailSubAcno").combobox('loadData', []).combobox('clear');
            //2.摘碼代號 清空
            $("#dataFormDetailDescribeID").combobox('loadData', []).combobox('clear');
        }

        //得到摘碼代號
        var GetDescribeID = function (Acno, DescribeID) {
            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetDescribeID', { Company_ID: CompanyID, Ac_no: Acno, Describe_ID: DescribeID });
            if (CodeList != null) $("#dataFormDetailDescribeID").combobox('loadData', CodeList);
        }
        //---------------------------------------呼叫Method---------------------------------------
        var GetDataFromMethod = function (methodName, data) {
            var returnValue = null;
            $.ajax({
                url: '../handler/JqDataHandle.ashx?RemoteName=sglVoucherMaster',
                data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                type: 'POST',
                async: false,
                success: function (data) { returnValue = $.parseJSON(data); },
                error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
            });
            return returnValue;
        };

       

        //========================================= dataFormVoucher ====================================================================================        
        function OnLoadSuccessDFMaster() {

            if (getEditMode($("#dataFormVoucher")) == 'viewed') {
                $('#dataFormDetail').hide();
            } else $('#dataFormDetail').show();

            if (getEditMode($("#dataFormVoucher")) == 'inserted') {
                //傳票日期disable属性删除
                $("#dataFormVoucherVoucherDate").combo('textbox').removeAttr("disabled");
                $("#dataFormVoucherVoucherDate").datebox().removeAttr("disabled");
                //預設傳票日期
                var dt = new Date();
                var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                $("#dataFormVoucherVoucherDate").datebox('setValue', today);
                //設定傳回目前的公司別、傳票類別   
                $("#dataFormVoucherRequisitionNO").val(sRequisitionNO);
                $("#dataFormVoucherCompanyID").combobox('setValue', sCompanyID);
                $("#dataFormVoucherVoucherID").options('setValue', sVoucherID);
                //Detail dataform
                GetAcno("");
                GetSubAcno("0", "");//新增時預設
                //分錄科目預設清空
                $("#dataFormVoucherOftenUsedAcno").combobox('setValue', "");
            }
            //else {
            //    //傳票日期不可編輯
            //    $("#dataFormVoucherVoucherDate").datebox("disable");

            //}

            ControlGrid();//控制Grid key in 顯示方式的顯示或隱藏

            //顯示Grid
            //queryGrid($("#dataGridDetail"));

        }

        //主檔的 公司別 或 傳票類別 有變動時
        function OnSelectCompanyID(rowdata) {
            RunGetSubAcno();//科目            

        }

        //摘碼代號 => 得到內容
        function GetDescribeText(CompanyID, DetailAcno, DescribeID) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherDetails', //連接的Server端，command
                data: "mode=method&method=" + "GetDescribeText" + "&parameters=" + CompanyID + "," + DetailAcno + "," + DescribeID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#dataFormDetailDescribe').val(rows[0].Describe);
                    }
                }
            });
        }

        //明細代號 => 得到內容 (顯示在Grid中)
        function GetAcnoNameText(CompanyID, Acno, SubAcno) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherDetails', //連接的Server端，command
                data: "mode=method&method=" + "GetAcnoNameText" + "&parameters=" + CompanyID + "," + Acno + "," + SubAcno,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用            
                        $('#dataFormDetailSubAcnoText').val(rows[0].AcnoName);
                    }
                }
            });
        }

        function OnAppliedDFMaster() {
            $("#dataGridView").datagrid("reload");
        }
       

        //========================================= DataFormDetails ====================================================================================              
        var OnLoadSuccessDFDetail = function (rowdata) {
            //DataFormDetails 資料編輯時
            if (getEditMode($("#dataFormDetail")) == 'updated') {
                GetAcno(rowdata.Acno);
                GetSubAcno(rowdata.Acno, rowdata.SubAcno);
                GetDescribeID(rowdata.Acno, rowdata.DescribeID);
            }
            if (getEditMode($("#dataFormDetail")) == 'inserted') {
                $("#dataFormDetailBorrowLendType").combo('textbox').focus();//焦點
                $("#dataFormDetailBorrowLendType").combobox('setValue', "");
            }
            //================================== combo blur 事件 ====================================       

            //combo blur 事件  =>   科目
            $("#dataFormDetailAcno").combo('textbox').blur(function () {
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                //1.得到明細
                //GetSubAcno(DetailAcno, "");
                $("#dataFormDetailSubAcno").combobox('setValue', "");//明細清空
                //2.摘碼代號
                GetDescribeID(DetailAcno, "");
            });
            //combo blur 事件  =>   明細
            $("#dataFormDetailSubAcno").combo('textbox').blur(function () {
                //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用      
                var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');//明細
                //得到內容
                GetAcnoNameText(CompanyID, DetailAcno, SubAcno);

            });


            //combo blur 事件  =>   摘碼代號
            $("#dataFormDetailDescribeID").combo('textbox').blur(function () {

                var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                var DescribeID = $("#dataFormDetailDescribeID").combobox('getValue');//摘碼代號

                //得到內容
                GetDescribeText(CompanyID, DetailAcno, DescribeID);
            });
        }

        //將摘碼代號所選帶入摘碼內容
        function OnSelectDescribeID(rowData) {
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
            var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
            var DescribeID = $("#dataFormDetailDescribeID").combobox('getValue');//摘碼代號
            //得到內容
            GetDescribeText(CompanyID, DetailAcno, DescribeID);
        }
        //明細	驗證 => 金額需>0
        function CheckMethodAmt(val) {
            if (val <= 0) {
                return false;
            } else return true;//通過
        }
        //明細	驗證 =>可能selected Value 為空白=> 判斷文字
        function CheckSubAcno() {
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getText');
            if (SubAcno == "" || SubAcno == "---請選擇---") {
                alert('請選擇明細!');
                return false;
            } else return true;//通過
        }
        function OnSelectSubAcno(rowData) {
            //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用            
            $("#dataFormDetailSubAcnoText").val(rowData.text);
        }

        //DataFormDetails存檔前檢查
        function OnApplyDFDetail() {

            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            $("#dataFormDetailCompanyID").val(CompanyID);
            //傳票類別	
            var VoucherID = $("#dataFormVoucherVoucherID").options('getCheckedValue');
            $("#dataFormDetailVoucherID").val(VoucherID);

            var Acno = $("#dataFormDetailAcno").combobox('getValue');
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');
            var BorrowLendType = $("#dataFormDetailBorrowLendType").combobox('getValue');
            var Describe = $("#dataFormDetailDescribe").val();
            //1.必選檢查
            if (BorrowLendType == "") {
                alert('請選擇借貸!');
                return false;
            }
            if (SubAcno == "---請選擇---") {
                alert('請選擇明細!');
                return false;
            }
            if (Describe == "") {
                alert('內容不可為空白!');
                return false;
            }
            //2.傳票日期必選檢查            
            var VoucherDate = $("#dataFormVoucherVoucherDate").datebox('getValue');
            if (VoucherDate == "") {
                alert('請選擇傳票日期!');
                return false;
            }
            //3.新增明細時檢查  => 科目+明細檢查       
            //公司別
            var iCount = GetDataFromMethod('GetDetailData', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (iCount == 0) {
                alert("科目或明細資料不存在！");
                return false;
            }
            //4.是否要成本中心=>由Acno,SubAcno推 
            var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            var CostCenterID = $("#dataFormDetailCostCenterID").combobox('getValue');
            if (bCostCenterID == "True" && CostCenterID == "") {
                alert('此科目需成本中心-請選擇成本中心!');
                return false;
            }

        }

        //========================================= 存檔前檢查 ====================================================================================              
        //存檔前檢查 OnSubmited
        function OnApplyDFMaster() {

            var GridName = '#dataGridDetail';                       

            //1.檢查 dataGridDetail 的 公司別 是否有跑掉
            //公司別 
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            //2.傳票內容檢查
            var rows = $(GridName).datagrid('getRows');

            if (rows.length == 0) {
                alert("無傳票內容！");
                return false;
            }
            //3.借貸方金額要平衡 借+貸=0 BorrowLendType 1=>借 , 2=>貸           
            var borrow = 0;//借金額
            var lend = 0;//貸金額

            for (var i = 0; i < rows.length; i++) {
                if (rows[i].CompanyID != CompanyID) {
                    alert("傳票資料有誤:公司別不一致！");
                    return false;
                }
                //3.1是否要成本中心=>由Acno,SubAcno推 
                var Acno = rows[i].Acno;
                //檢查科目
                if (Acno == null) {
                    alert('科目為必填!');
                    return false;
                }

                var SubAcno = rows[i].SubAcno;
                var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
                var CostCenterID = rows[i].CostCenterID;
                if (bCostCenterID == "True" && CostCenterID == "") {
                    alert('此科目' + Acno + ' ' + SubAcno + '需成本中心-請選擇成本中心!');
                    return false;
                }

                if (rows[i].BorrowLendType == 1) {
                    borrow = parseInt(borrow) + parseInt(rows[i].AmtShow);
                } else {
                    lend = parseInt(lend) + parseInt(rows[i].AmtShow);
                }
            }
            //if (rows.length> 0 && borrow == 0) {
            //    alert("借:" + borrow + ",貸:" + lend + " 借貸有問題！");
            //    return false;
            //}
            if (borrow != lend) {
                alert("借:" + borrow + ",貸:" + lend + " 總金額不平衡！");
                return false;
            }

            //3.傳票日期檢查=>是否已鎖檔
            var VoucherDate = $('#dataFormVoucherVoucherDate').datebox('getValue')
            //檢查若沒有glVoucherDetails,則可以刪除
            var cnt = GetDataFromMethod('CheckDeleteglVoucherMaster', { Company_ID: CompanyID, Voucher_Date: VoucherDate });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                alert('此年月已鎖檔,無法新增!!');
                return false;
            }


            ////4.傳票日期檢查 => 不同年份提醒檢查
            //var dt = new Date();
            //var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
            //return confirm("提醒您,傳票日期。");

            ////更新
            //$('#dataGridView').datagrid('reload');
            //queryGrid('#dataGridView');//按查詢

        }

        //複制分錄
        function CopyDetail() {
            var GridName = '#dataGridDetail';            

            var rowcount = $(GridName).datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有可選取明細資料,本功能無法使用');
                return false;
            }
            var aNewObj = {};
            var row = $(GridName).datagrid('getSelected');
            $.extend(aNewObj, row);//複製結構與資料

            //取目前編號
            var Item = 0;
            var AutoKey = 0;
            var rows = $(GridName).datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                if (parseInt(rows[i].Item) > Item) {
                    Item = rows[i].Item;
                }
                if (parseInt(rows[i].AutoKey) > AutoKey) {
                    AutoKey = rows[i].AutoKey;
                }
            }
            aNewObj.AutoKey = parseInt(AutoKey) + 1;
            aNewObj.Item = padLeft(parseInt(Item) + 1, 3);

            $(GridName).datagrid('appendRow', aNewObj);

        }
        //補左邊字串
        function padLeft(str, len) {
            str = '' + str;
            return str.length >= len ? str : new Array(len - str.length + 1).join("0") + str;
        }
        //========================================Grid2中新增資料=>取得公司別================================================================================
        function GetGrid2CompanyID() {
            return $("#dataFormVoucherCompanyID").combobox('getValue');
        }
        //========================================Grid2中新增資料=>取得公司別================================================================================
        function GetGrid2VoucherID() {
            return $("#dataFormVoucherVoucherID").options('getCheckedValue');
        }
        function SetToolTipValue() {
            var row = $('#ToolTip').datagrid('getSelected');
            var SubAcno = row.SubAcno;
            $("#dataFormDetailSubAcno").combobox('setValue', SubAcno);
            closeForm('#JQDialogToolTip');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sRequisition.Requisition" runat="server" AutoApply="True"
                DataMember="Requisition" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionDescr" Editor="text" FieldName="RequisitionDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RequisitionTypeID" Editor="numberbox" FieldName="RequisitionTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTermID" Editor="numberbox" FieldName="PayTermID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNotes" Editor="text" FieldName="RequisitionNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayToNotes" Editor="text" FieldName="PayToNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請款單申請" Width="720px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisition.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" Width="720px" OnApply="dataFormMasterOnApply" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnCancel="CloseDataForm" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="96"  ReadOnly="True" Span="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sRequisition.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" FieldName="ApplyEmpID" MaxLength="0" ReadOnly="False" Width="103" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisition.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectOrg_NO,panelHeight:250" FieldName="ApplyOrg_NO" Format="" ReadOnly="False" Width="130" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" Width="95" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預算年度" Editor="infocombobox" FieldName="VoucherYear" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="103" EditorOptions="valueField:'VoucherYear',textField:'VoucherYear',remoteName:'sRequisition.VoucherYear',tableName:'VoucherYear',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectCostCenterID,panelHeight:250" FieldName="CostCenterID" Format="" Span="1" Width="130" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目類別" Editor="infocombobox" EditorOptions="valueField:'BudgetType',textField:'BudgetTypeName',remoteName:'sRequisition.BudgetType',tableName:'BudgetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectAccountType,panelHeight:200" FieldName="BudgetType" MaxLength="0" ReadOnly="False" Span="1" Width="95" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sRequisition.BudgetBase',tableName:'BudgetBase',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnBlurRequisitAmt,panelHeight:200" FieldName="AccountID" Span="3" Width="338" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" Span="3" Width="483" maxlength="254" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="103" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnBlurRequisitAmt,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="89" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sRequisition.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="103" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" Format="" Width="96" NewRow="False" OnBlur="OnBlurRequisitAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="未稅金額" Editor="numberbox" FieldName="RequisitAmtNoTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="125" />
                        <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="RequisitAmtTax" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="89" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sRequisition.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectShortTermNO,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="489" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會辦總務" Editor="checkbox" FieldName="NeedGeneralAffairs" Width="80" NewRow="True" OnBlur="" maxlength="0" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" Format="" Width="483" maxlength="512" NewRow="False" ReadOnly="False" RowSpan="1" span="3" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'廠商與員工搜尋',panelWidth:540,panelHeight:240,remoteName:'sRequisition.Vendor',tableName:'Vendor',columns:[{field:'Employee_ID',title:'員工代號',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendName',title:'廠商/員工姓名',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'廠商簡稱',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'VendShortName',cacheRelationText:true,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:GetPayTerm,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="PayTo" Format="" Width="130" maxlength="0" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisition.PayType',tableName:'PayType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:GetPayType,panelHeight:200" FieldName="PayTypeID" Format="" Width="103" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" FieldName="PayTermID" Width="95" Format="" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisition.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:GetPayDateByTerm,panelHeight:200" Visible="True" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" FieldName="PlanPayDate" ReadOnly="False" Span="1" Visible="True" Width="133" EditorOptions="" Format="yyyy/mm/dd" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" span="2" Visible="True" Width="96" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" Span="1" Width="80" Visible="False" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" FieldName="PayToNotes" Format="" Visible="True" Width="483" ReadOnly="False" Span="3" EditorOptions="height:40" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="130" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="130" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需匯款費" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRemit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" OnBlur="CheckRemitType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款費付款" Editor="infocombobox" FieldName="RemitType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" EditorOptions="items:[{value:'廠商付',text:'廠商付',selected:'false'},{value:'公司付',text:'公司付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加簽對象" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,panelHeight:200,remoteName:'sRequisition.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeName',valueFieldCaption:'工號',textFieldCaption:'姓名',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="DynamicUser" MaxLength="0" ReadOnly="False" Visible="True" Width="80" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="宿管人員申請" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEmpGroupID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="解除設定" Editor="checkbox" FieldName="unlock" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="15" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="RequisitionNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" FieldName="CreateBy" DefaultValue="_username" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrg" FieldName="ApplyOrg_NO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgNOParent" FieldName="Org_NOParent" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetUserOrgCostCenter" FieldName="CostCenterID" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="25000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <br />
                <%--<div id="divSignNotesData">
                <div class="panel-header" style="width: 628px;"><div class="panel-title">批示意見</div></div>
                <table ID="GridSignNotesData" class="easyui-datagrid" style="width:640px;" data-options="fitColumns:true,singleSelect:true">
                    <thead>
                        <tr>
                            <th data-options="field:'S_STEP_ID',width:100">流程</th>
                            <th data-options="field:'USERNAME',width:50">簽核者</th>
                            <th data-options="field:'REMARK',width:350,align:'right'">簽核內容</th>
                            <th data-options="field:'UPDATEDATE',width:128,align:'right'">簽核日期</th>
                        </tr>
                    </thead>
                </table>
                </div>--%>
                <input ID='dataFormMasterClear' type="button" style="background-image:url(../image/Ajax/close.gif);border:0;width:14px;height:14px;">
            </JQTools:JQDialog>
            
            <JQTools:JQDialog ID="JQDialogVoucherOpen" runat="server" BindingObjectID="dataFormVoucher" Title="傳票維護" DialogLeft="20px" DialogTop="5px" Width="870px">
                <JQTools:JQDataForm ID="dataFormVoucher" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="RequisitionglVoucherM" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sRequisition.RequisitionglVoucherM" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFMaster" OnApply="OnApplyDFMaster" OnApplied="OnAppliedDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" MaxLength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單號" Editor="text" FieldName="RequisitionNO" Format="" Width="100" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" Format="" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infooptions" FieldName="VoucherID" Format="" Width="180" EditorOptions="title:'JQOptions',panelWidth:260,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" MaxLength="0" ReadOnly="False" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>

                <a id="lbVoucher" class="easyui-linkbutton" data-options="" href="#" onclick="InsertVoucherData()">載入項目</a><JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="RequisitionglVoucherD" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" Height="240px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnInsert="OnInsertDetail" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormVoucher" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sRequisition.RequisitionglVoucherD" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" EditorOptions="" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="text" EditorOptions="" FieldName="SubAcnoText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="text" EditorOptions="" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="197">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="AmtShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="OpenToolTip" Editor="text" FieldName="OpenToolTip" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="RequisitionNO" ParentFieldName="RequisitionNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyDetail" Text="複製" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Continue" Width="780px" Closed="True" Title="">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormVoucher" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="RequisitionglVoucherD" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sRequisition.RequisitionglVoucherD" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnLoadSuccess="OnLoadSuccessDFDetail" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListID',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="BorrowLendType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Acno_OnSelect,panelHeight:150" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectSubAcno,panelHeight:150" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" OnBlur="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="呼叫明細" Editor="text" FieldName="OpenToolTip" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="20" Format="" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SubAcnoText" Editor="text" FieldName="SubAcnoText" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="105" />
                            <JQTools:JQFormColumn Alignment="left" Caption="摘碼代號" Editor="infocombobox" FieldName="DescribeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDescribeID,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="內容" Editor="text" FieldName="Describe" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="290" />
                            <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="AmtShow" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="RequisitionNO" ParentFieldName="RequisitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="3" />
                    <JQTools:JQAutoSeq ID="JQAutoSeq11" runat="server" BindingObjectID="dataFormDetail" FieldName="AutoKey" NumDig="1" />
                </JQTools:JQDialog>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormVoucher" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateMessage="請選擇傳票類別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherDate" RemoteMethod="True" ValidateMessage="傳票日期	不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="BorrowLendType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowLendType" RemoteMethod="True" ValidateMessage="請選擇借貸！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="SubAcno" RemoteMethod="False" ValidateMessage="請選擇明細！" ValidateType="None" CheckMethod="CheckSubAcno" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AmtShow" RemoteMethod="True" ValidateMessage="請填寫金額！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>


            <JQTools:JQDialog ID="JQDialogToolTip" runat="server" DialogLeft="130px" DialogTop="80px" Title="明細查詢" ShowSubmitDiv="False" Width="420px" Height="370px">
                <JQTools:JQDataGrid ID="ToolTip" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoSubAcno" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglVoucherMaster.infoSubAcno" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" BufferView="False" NotInitGrid="False" RowNumbers="True" Width="100%">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="明細代碼" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="明細文字" Editor="text" FieldName="AcnoName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="220" />
                    </Columns>
                </JQTools:JQDataGrid>
                <JQTools:JQButton ID="JQButton1" runat="server" OnClick="SetToolTipValue()" Text="傳回科目" />
        </JQTools:JQDialog>


        </div>
         <script type="text/javascript">
             $(":input").css("background", backcolor);
         </script>
    </form>
</body>
</html>
