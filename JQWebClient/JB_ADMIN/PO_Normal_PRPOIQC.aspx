<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PO_Normal_PRPOIQC.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        var sPONO = "";
        var sCompanyID = "";
        var sVoucherID = "";
        var iVoucherCount = 0;
        var sCount = 0;
        $(function () {
            $("#VirtualColumn_Query").attr("placeholder", "請購單號、申請者工號/姓名、申請事由...");
            //alert(getEditMode($("#dataFormMaster")) == 'inserted');
            //if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //開啟查詢物品類別DIV
                //var textBox1 = $("<input/>").attr({ 'id': 'textBox1', 'type': 'textBox', 'size': '23px', 'placeholder': '輸入物品關鍵字,移動滑鼠查詢', 'onblur': 'onblurfunc(this.value)' });
                //$('#dataFormMasterItemTypeID').closest('td').append('&nbsp;').append('&nbsp;').append('查詢物品類別').append('&nbsp;').append(textBox1);
            //}
            //報價檔 清除button
            var clearBtn1 = $("<button type='button'>").attr({ 'id': 'clearBtn1', 'href': '#', 'onclick': 'ClearPurDocVen1()' }).text("清除");
            var clearBtn2 = $("<button type='button'>").attr({ 'id': 'clearBtn2', 'href': '#', 'onclick': 'ClearPurDocVen2()' }).text("清除");
            var clearBtn3 = $("<button type='button'>").attr({ 'id': 'clearBtn3', 'href': '#', 'onclick': 'ClearPurDocVen3()' }).text("清除");
            //報價檔 帶入採購checkbox
            var checkBox1 = $("<input/>").attr({ 'id': 'checkBox1', 'type': 'checkbox', 'onclick': 'OnClick_checkBox(1)' });
            var checkBox2 = $("<input/>").attr({ 'id': 'checkBox2', 'type': 'checkbox', 'onclick': 'OnClick_checkBox(2)' });
            var checkBox3 = $("<input/>").attr({ 'id': 'checkBox3', 'type': 'checkbox', 'onclick': 'OnClick_checkBox(3)' });
 
            $('#dataFormMasterPurDocVen1').closest('td').append('&nbsp;').append(clearBtn1).append('&nbsp;').append('帶入採購').append(checkBox1);//.append('&nbsp;&nbsp;')
            $('#dataFormMasterPurDocVen2').closest('td').append('&nbsp;').append(clearBtn2).append('&nbsp;').append('帶入採購').append(checkBox2);
            $('#dataFormMasterPurDocVen3').closest('td').append('&nbsp;').append(clearBtn3).append('&nbsp;').append('帶入採購').append(checkBox3);
            //分期期數的確定按鈕
            var installmentsBtn = $("<button type='button'>").attr({ 'id': 'installmentsBtn', 'href': '#', 'onclick': 'installmentsBtn_OnClick()' }).text("確定");
            $('#dataFormMasterInstallments').closest('td').append('&nbsp;').append(installmentsBtn)

            //紅字
            var redfields2 = ['PurVendor1', 'POPayTypeID', 'Installments', 'PurDocVen1','IsCatalogue','unlock'];
            RedFields('#dataFormMaster', redfields2);
            var redfields1 = ['PurQty', 'PurPrice', 'PurVendor', 'PurPriceVen1', 'FirstDeliveryDate', 'FirstDeliveryQty'];
            RedFields('#dataFormDetail', redfields1);
                            //交貨數量        //交貨日期    //物品單價  //總價        //付款方式  //驗收日期      //驗收數量      //驗收人員  //帳款天數      //檢附憑據          //存放區域
            var redfields0 = ['DeliveryQty', 'DeliveryDate', 'PurPrice', 'TotalPrice', 'PayWayID', 'AcceptanceDate', 'AcceptanceQty', 'Surveyors', 'DebtorDays', 'ProofTypeID', 'AssetLocaID', 'AcceptancePic'];
            RedFields('#dataFormDelivery', redfields0);

            //單選
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $("#dataGridView").datagrid({
                    singleSelect: true,
                });
            }
            //取消預設第一列勾選
            setTimeout(function () {
                $("#dataGridView").datagrid('unselectAll');//unselectAll
            }, 600);
            //[設值]查詢的成本中心，預設使用者的成本中心
            var myArr = GetUserOrgNOs(getClientInfo("UserID"));//得組織編號和上層組織編號
            var myCostCenterID;
            if (myArr.length > 0) {
                myCostCenterID = GetOrgNO_CostCenterID(myArr[0]);//用所在組織編號得成本中心編號
                $("#CostCenterID_Query").combobox('setValue', myCostCenterID);
            }        
            //[設值]查詢的申請起日，預設今日前十天
            $('#ApplyDate_Query').datebox('setValue', getShiftDay(-10));
            //[設值]查詢的申請迄日，預設今日
            $('#ApplyDate_Query[infolight-options*="~"] ').datebox('setValue', getToday());
            //查詢窗
            $("#querydataGridView").show();

            //請購單價 旁增加 採購歷史單價
            var historyPOPrice = $("<button type='button'>").attr({ 'id': 'historyPOPrice', 'href': '#', 'onclick': 'Open_historyPOPrice()' }).text("歷史單價");
            $('#dataFormDetailRegPrice').closest('td').append('&nbsp;&nbsp;').append(historyPOPrice);
            initJQDialog4();

            //-------Rebecca Add-------//
            //----------------修改jqDataForm的Caption為Hypelink-----------------------------------
            //呼叫科目明細
            $("#dataFormDetail0").form({
                onLoadSuccess: function (data) {
                    $("td", "#dataFormDetail0").each(function (index) {
                        if ($(this).children().length == 0) {
                            if ($(this).html() == "呼叫明細") {
                                $(this).html("");
                                //$('<a>aa</a>').attr({
                                //    'href': 'bOrders1.aspx'
                                //}).appendTo(this);
                                $('<input type="image" img  src="img/clock_red.png" onclick="OpenSubAcnoToolTip()">').attr({
                                }).appendTo(this);

                                $("#dataFormDetail0OpenToolTip").closest('td').hide();
                            }

                        }
                    });
                }
            });

        });
        function onblurfunc(para) {
            if (para == "" || para == undefined) {
                return true;
            }
            var filtstr = "ItemName LIKE '%" + para + "%'";
            $("#JQDataGrid1").datagrid('setWhere', filtstr);
            openForm('#JQDialog6', {}, "", 'dialog');
        }
       //初始化 JQDialog4 採購歷史單價清單
        function initJQDialog4() {
            $("#JQDialog4").dialog(
            {
                height: 350,
                width: 800,
                left: 300,
                top: 200,
                resizable: false,
                modal: true,
                title: "歷史採購單價",
                closed: true
            });
        };
        //開啟 採購歷史單價清單
        function Open_historyPOPrice() {
            var itemID = $("#dataFormDetailItemID").refval('getValue');
            if (itemID != null && itemID !="") {
                $("#dataGridPODetailsHistory").datagrid('setWhere', "d.[ItemID]='" + itemID + "'");
                $("#JQDialog4").dialog("open");
            } else {
                alert("請先選「品名」");
            }
        }

        //帶入報價廠商
        function OnClick_checkBox(int) {
            
            if (int == 1) {
                if ($("#checkBox1").attr('checked') == 'checked') {//勾
                    $("#checkBox2").attr({ 'checked': false });
                    $("#checkBox3").attr({ 'checked': false });
                    var dialoggrid = "#dataGridDetail";
                    var detailLength = $(dialoggrid).datagrid('getRows').length;
                    var purVendor = $("#dataFormMasterPurVendor1").refval('getValue');
                    for (var i = 0; i < detailLength; i++) {
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor);//設值
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                } else {//反勾
                    var detailLength = $('#dataGridDetail').datagrid('getRows').length;
                    var purVendor = '';
                    for (var i = 0; i < detailLength; i++) {
                        var dialoggrid = "#dataGridDetail";
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor);//設值
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                }
            } else if (int == 2) {
                if ($("#checkBox2").attr('checked') == 'checked') {
                    $("#checkBox1").attr({ 'checked': false });
                    $("#checkBox3").attr({ 'checked': false });

                    var dialoggrid = "#dataGridDetail";
                    var detailLength = $(dialoggrid).datagrid('getRows').length;
                    var purVendor = $("#dataFormMasterPurVendor2").refval('getValue');
                    for (var i = 0; i < detailLength; i++) {
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor);
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                } else {
                    var detailLength = $('#dataGridDetail').datagrid('getRows').length;
                    var purVendor = '';
                    for (var i = 0; i < detailLength; i++) {
                        var dialoggrid = "#dataGridDetail";
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor);
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                }
            } else if (int == 3) {
                if ($("#checkBox3").attr('checked') == 'checked') {
                    $("#checkBox1").attr({ 'checked': false });
                    $("#checkBox2").attr({ 'checked': false });
                    var dialoggrid = "#dataGridDetail";
                    var detailLength = $(dialoggrid).datagrid('getRows').length;
                    var purVendor = $("#dataFormMasterPurVendor3").refval('getValue');
                    for (var i = 0; i < detailLength; i++) {
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor);//設值
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                } else {
                    var detailLength = $('#dataGridDetail').datagrid('getRows').length;
                    var purVendor1 = '';
                    for (var i = 0; i < detailLength; i++) {
                        var dialoggrid = "#dataGridDetail";
                        $(dialoggrid).datagrid('beginEdit', i);
                        var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'PurVendor' });
                        if (editor) {
                            editor.actions.setValue(editor.target, purVendor1);//設值
                        }
                        $(dialoggrid).datagrid('endEdit', i);
                    }
                }
            }
        }
        
        //採購單價、採購數量 的OnBlur(為了設定採購稅額)
        function dataFormDetail_OnBlur() {
            var price = $.trim($("#dataFormDetailPurPrice").val());
            var qty = $.trim($("#dataFormDetailPurQty").val());
            //var otherFee = $.trim($("#dataFormDetailOtherFee").val());
            var otherFee = 0;
            var tax = GetTax($("#dataFormDetailItemID").refval('getValue'), price, qty, otherFee);
            if (price > 0 && qty > 0) {
                $("#dataFormDetailPurTax").focus();
                $("#dataFormDetailPurTax").val(tax);
                $("#dataFormDetailPurTax").blur();
            }
            
        }

        //呼叫dll指定的infoCommand
        function GetInfoCommandValue(controller, where, getWhat) {
            var remoteName = getInfolightOption(controller).remoteName;
            var tableName = getInfolightOption(controller).tableName;
            // var valueField = getInfolightOption(infoRefval).valueField;
            var returnValue = "";
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: getRemoteUrl(remoteName, tableName, false) + "&whereString=" + encodeURIComponent(where),
                data: { rows: 1 },
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        if (getWhat == "TaxRate") {//稅率
                            var value = data[0]["TaxRate"];
                        } else if (getWhat == "LeadTime") {//採購前置天數
                            var value = data[0]["LeadTime"];
                        } else if (getWhat == "IsAsset") {//是否資產
                            var value = data[0]["IsAsset"];
                        }
                        //$(infoRefval).refval('setValue', value);
                        returnValue = value;
                    }
                },
                error: function (data) { }
            });
            return returnValue;
        }

        function GetPODeliveryLength(where) {
            var returnValue = "";
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: getRemoteUrl("sPO_Normal_PRPOIQC.PODelivery", "PODelivery", false) + "&whereString=" + encodeURIComponent(where),
                data: {rows: 1000 },
                cache: false,
                async: false,
                success: function (data) {
                    returnValue = data.length;//PODelivery列數
                },
                error: function (data) { }
            });
            return returnValue;
        }

        function dataGridView_OnLoad() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                var whereArr = [];

                var costCenterID_query = $("#CostCenterID_Query").combobox('getValue');
                if (costCenterID_query != "") {
                    whereArr.push("POMaster.CostCenterID='" + costCenterID_query + "'");
                }
                var applyDate_query_s = $('#ApplyDate_Query').datebox('getValue');
                if (applyDate_query_s != "") {
                    whereArr.push("ApplyDate >='" + applyDate_query_s + "'");
                }
                //var applyDate_query_e = $('#ApplyDate_Query[infolight-options*="~"] ').datebox('getValue');
                //if (applyDate_query_e != "") {
                //    whereArr.push("ApplyDate <='" + applyDate_query_e + "'");
                //}
                var d_STEP_ID = $('#D_STEP_ID_Query').combobox('getValue');
                if (d_STEP_ID != "") {
                    whereArr.push("D_STEP_ID ='" + d_STEP_ID + "'");
                }
                setTimeout(
                    function () {
                        $("#dataGridView").datagrid('setWhere', whereArr.join(" and "))
                    }
                , 1200);
            }
        }

        //送出存檔→自動起單(沒用到)
        function dataFormMaster_OnApplied() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新增狀態
                var userid = getClientInfo("userid");
                //自動起單
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster',
                    data: "mode=method&method=" + "FlowStartUp" + "&parameters=" + userid, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            alert('請購單申請成功');
                        } else {
                            alert('請購單申請失敗');
                        }
                    }
                });
                $("#dataGridView").datagrid("load");
            }
        }


        //手動起單
        function FlowStartUpByHand() {
            //起單前檢查勾選及確認意願
            if ($("#dataGridView").datagrid('getChecked').length == 0) {
                alert('請勾選');
            } else {
                
                var row = $('#dataGridView').datagrid('getChecked');
                var userid = getClientInfo("userid");

                //檢查起單者是否為申請者
                if (row[0].ApplyUserID != userid) {
                    alert('您非申請者，無法起單');
                    return false;
                }

                //起單
                var pre = confirm("確定起單?");
                if (pre == true) {
                    var PONO = row[0].PONO;
                    var Flowflag = row[0].Flowflag;
                    if (userid != undefined && PONO != undefined && (Flowflag == null || Flowflag == '' || Flowflag == 'X')) {
                        //起單
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster',
                            data: "mode=method&method=" + "FlowStartUpByHand" + "&parameters=" + userid + "," + PONO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data != "False") {
                                    alert('請購單申請成功');
                                } else {
                                    alert('請購單申請失敗');
                                }
                            }
                        });
                        $("#dataGridView").datagrid("load");
                    } else if (Flowflag != null && Flowflag != '' && Flowflag != 'X') {
                        alert("已起單過");
                        return false;
                    }
                }
            }
        }

        //表單onload時(flow各關卡的欄位 顯示或隱藏 及 停用或啟用)
        function dataFormMaster_OnLoadSuccess() {
            //取flow的parameters
            var parameter = Request.getQueryStringByName2("P1");//沒加密
                //mode = Request.getQueryStringByName2("NAVIGATOR_MODE");
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
                //mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            }

            //跑流程時，把query隱藏
            if (parameter != "") {
                $("#querydataGridView").hide();
            } else {
                $("#querydataGridView").show();
            }
            //設定dialog的Title
            $("#JQDialog1").panel('setTitle', $("#dataFormMasterD_STEP_ID").val());
            //設定系統變數
            $("#dataFormMastersysVariable").val(sysVariable_Default());
     
            //交貨明細grid顯示
            //全部欄位顯示
            $("#dataGridDeliveryDiv").show();
            var ShowyFields = ['PurQty', 'PurPrice', 'PurTax', 'PurVendor', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'FirstDeliveryDate', 'FirstDeliveryQty', 'PurVendor10', 'PurVendor20', 'PurVendor30'];
            ShowFields('#dataFormDetail', ShowyFields);
            var ShowMasterFields = ['ResponsibleGROUPID', 'FlagDeliveryEnough', 'D_STEP_ID', 'sysVariable', 'Acno', 'SubAcno', 'ApplyOrg_NO', 'OtherFee', 'OtherComment', 'POPayTypeID', 'PurVendor1', 'PurVendor2', 'PurVendor3', 'PurDocVen2', 'PurDocVen3', 'PurDocVen10', 'PurDocVen20', 'PurDocVen30', 'PurComment', 'PurDocVen1','RequestNotes','OtherFeeTax','PurCommentFile','DeliveryTotalAmount','PurTotalAmount'];//
            ShowFields('#dataFormMaster', ShowMasterFields);
            var ShowDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'AcceptanceQty', 'PayWayID', 'AcceptanceDate', 'Surveyors', 'ReturnQty', 'AcceptanceTax', 'AccountNO', 'PayTo', 'PlanPayDate', 'AcceptancePic'];
            ShowFields('#dataFormDelivery', ShowDeliveryFields);

            //全部欄位啟用
            var EnabledFieldArr = ['Description', 'OtherFee', 'OtherComment', 'PurComment','RequestNotes','OtherFeeTax'];
            var EnabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno', 'POPayTypeID','VoucherYear'];
            var EnabledRefvalArr = ['PurVendor1', 'PurVendor2', 'PurVendor3'];
            EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);
            var EnabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit','FirstDeliveryQty'];
            var EnabledComboboxArr1 = ['RegDate','FirstDeliveryDate'];
            var EnabledRefvalArr1 = [];//'RecVendor'
            EnableFields("#dataFormDetail", EnabledFieldArr1, EnabledComboboxArr1, EnabledRefvalArr1);
            var EnabledFieldArr2 = ['DeliveryQty', 'AcceptanceQty', 'ReturnQty', 'PurPrice','OtherFee','AcceptanceTax','TotalPrice','DebtorDays','InvoiceNO','AccountNO','PayTo','PlanPayDate'];
            var EnabledComboboxArr2 = ['DeliveryDate', 'AcceptanceDate', 'PayWayID'];
            var EnabledRefvalArr2 = [];
            EnableFields("#dataFormDelivery", EnabledFieldArr2, EnabledComboboxArr2, EnabledRefvalArr2);
            //Master停用採購作業要填的欄位(採購作業S4啟用)
            var DisabledFieldArr = ['OtherFee', 'OtherComment', 'PurComment','OtherFeeTax'];
            var DisabledComboboxArr = ['POPayTypeID'];
            var DisabledRefvalArr = ['PurVendor1', 'PurVendor2', 'PurVendor3'];
            DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            $('.info-fileUpload-file[name="PurDocVen1file"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PurDocVen1file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('.info-fileUpload-file[name="PurDocVen2file"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PurDocVen2file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('.info-fileUpload-file[name="PurDocVen3file"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PurDocVen3file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('input[name="PurDocVen1"]').attr('disabled', true);
            $('input[name="PurDocVen2"]').attr('disabled', true);
            $('input[name="PurDocVen3"]').attr('disabled', true);
            $("#clearBtn1").attr('disabled', true);
            $("#clearBtn2").attr('disabled', true);
            $("#clearBtn3").attr('disabled', true);
            $("#checkBox1").attr('disabled', true);
            $("#checkBox2").attr('disabled', true);
            $("#checkBox3").attr('disabled', true);
            $('.info-fileUpload-file[name="PurCommentFilefile"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PurCommentFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('input[name="PurCommentFile"]').attr('disabled', true);

            //Master停用請購申請的樣品照片、估價單
            $('.info-fileUpload-file[name="PrPicfile"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PrPicfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('.info-fileUpload-file[name="PrDocfile"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PrDocfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('input[name="PrPic"]').attr('disabled', true);
            $('input[name="PrDoc"]').attr('disabled', true);

            //Master停用建議廠商、預算年度(請購申請啟用)、暫借款單(請購申請啟用)
            var DisabledFieldArr = ['IsAdd'];
            var DisabledComboboxArr = ['VoucherYear','ShortTermNO'];
            var DisabledRefvalArr = ['RecVendor'];
            DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            
            //停用欄位(交貨明細)  (停用'AssetLocaID'，到驗收再啟用)
            var DisabledFieldArr00 = ['Surveyors'];
            var DisabledComboboxArr00 = ['AssetLocaID'];
            var DisabledRefvalArr00 = [];
            DisableFields("#dataFormDelivery", DisabledFieldArr00, DisabledComboboxArr00, DisabledRefvalArr00);

            //隱藏 採購職稱、安排交貨數量完成、未收數量、作業名稱、系統變數、分期期數
            var HiddenMasterFields = ['ResponsibleGROUPID', 'FlagDeliveryEnough', 'D_STEP_ID', 'sysVariable', 'Acno', 'SubAcno', 'ApplyOrg_NO', 'Flowflag', 'Installments', 'IsCatalogue','OtherFee','OtherFeeTax','unlock'];
            HideFields('#dataFormMaster', HiddenMasterFields);
            $("#installmentsBtn").hide();//分期期數的確定按鈕(採購方式選到分期付款，再顯示)
            //隱藏 首批交貨日期、首批交貨數量
            var HiddenFields = ['FirstDeliveryDate', 'FirstDeliveryQty'];
            HideFields('#dataFormDetail', HiddenFields);
            //隱藏 IsAssetCompleted(資產處理完成)
            var HiddenFields = ['IsAssetCompleted'];
            HideFields('#dataFormDelivery', HiddenFields);
            //隱藏 資產異動單才有的欄位
            HideFields('#dataFormDelivery', ['AssetLocaID']);
            //隱藏 驗收照片
            HideFields('#dataFormDelivery', ['AcceptancePic']);

            //grid欄位顯示
            var gridDetailColumns = ['ItemNO', 'ItemID', 'ItemSpec', 'RegQty', 'Unit', 'RegDate', 'PurQty', 'PurPrice', 'PurTax', 'TotalAmount', 'PurVendor', 'sumDeliveryQty', 'sumAcceptanceQty'];//, 'OtherFee'
            ShowGridColumns("#dataGridDetail", gridDetailColumns);
            var gridDeliveryColumns = ['DeliveryNO','ItemNO', 'ItemID', 'DeliveryDate', 'DeliveryQty', 'AcceptanceQty', 'ReturnQty', 'Surveyors', 'InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice'];
            ShowGridColumns("#dataGridDelivery", gridDeliveryColumns);

            //for (i = 1; i <= 3; i++) {
            //    var infofileUpload = $('#dataFormMasterPurDocVen'+i);
            //    var folder = getInfolightOption(infofileUpload).upLoadFolder;
            //    var fileName = "";
            //    if ($("#href1").length == 0) {
            //        fileName = $('.info-fileUpload-value', infofileUpload.next()).val();
            //        if (fileName != "") {
            //            var aLink = $("<a id='href'"+i+" download>").attr({ 'href': "../" + folder + "/" + fileName }).text("下載");
            //            $('#dataFormMasterPurDocVen'+i).closest('td').append('&nbsp;').append(aLink);
            //        }
            //    }
            //}

            //顯示 檢視、編輯、刪除按鈕
            var dgView = $("#dataGridDetail");
            var infolightOptions = dgView.attr("infolight-options");
            if (infolightOptions.indexOf("commandButtons:'v'")>=0) {
                infolightOptions.replace("commandButtons:'v'", "commandButtons:'vud'");
            } else if (infolightOptions.indexOf("commandButtons:'vu'") >= 0) {
                infolightOptions.replace("commandButtons:'vu'", "commandButtons:'vud'");
            }
            dgView.attr("infolight-options", infolightOptions);

            dgView = $("#dataGridDelivery");
            infolightOptions = dgView.attr("infolight-options");
            if (infolightOptions.indexOf("commandButtons:'v'") >= 0) {
                infolightOptions.replace("commandButtons:'v'", "commandButtons:'vud'");
            } else if (infolightOptions.indexOf("commandButtons:'vu'") >= 0) {
                infolightOptions.replace("commandButtons:'vu'", "commandButtons:'vud'");
            }
            dgView.attr("infolight-options", infolightOptions);
            //某些欄位隱藏或停用
            //申請新增或退回到申請的修改
            if (parameter == "" && (getEditMode($("#dataFormMaster")) == 'inserted' || getEditMode($("#dataFormMaster")) == 'updated' || (getEditMode($("#dataFormMaster")) == 'viewed' && $("#dataFormMasterFlowflag").val()==""))) {
                //設值ApplyOrg_NO、Org_NOParent
                if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    //開啟查詢物品類別DIV
                    if (sCount == 0) {
                        var textBox1 = $("<input/>").attr({ 'id': 'textBox1', 'type': 'textBox', 'size': '23px', 'placeholder': '輸入物品關鍵字,移動滑鼠查詢', 'onblur': 'onblurfunc(this.value)' });
                        $('#dataFormMasterItemTypeID').closest('td').append('&nbsp;').append('&nbsp;').append('查詢物品類別').append('&nbsp;').append(textBox1);
                        sCount = sCount + 1;
                    }
                    var myArr = GetUserOrgNOs(getClientInfo("UserID"));//得組織編號和上層組織編號
                    var myCostCenterID;
                    if (myArr.length > 0) {
                        $("#dataFormMasterApplyOrg_NO").combobox('setValue', myArr[0]);
                        $("#dataFormMasterOrg_NOParent").val(myArr[1]);
                        myCostCenterID = GetOrgNO_CostCenterID(myArr[0]);//用所在組織編號得成本中心編號
                        $("#dataFormMasterCostCenterID").combobox('setValue', myCostCenterID);
                    }
                }
                
                //成本中心篩選
                //var userid = ($("#dataFormMasterApplyUserID").val() != '') ? $("#dataFormMasterApplyUserID").val() : getClientInfo("_usercode");
                //$("#dataFormMasterCostCenterID").combobox('setWhere', "AuthorUsers like '%" + userid + "%'");
                //交貨明細隱藏
                $("#dataGridDeliveryDiv").hide();
                //採購人員填的欄位隱藏
                var HiddenFields = ['PurQty', 'PurPrice', 'PurTax', 'PurVendor', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurVendor10', 'PurVendor20', 'PurVendor30'];
                HideFields('#dataFormDetail', HiddenFields);
                //隱藏 採購總金額、採購作業要填的欄位、補單
                var HiddenMasterFields = ['PurTotalAmount', 'DeliveryTotalAmount', 'OtherFee', 'OtherComment', 'POPayTypeID', 'PurVendor1', 'PurVendor2', 'PurVendor3', 'PurDocVen2', 'PurDocVen3', 'PurDocVen10', 'PurDocVen20', 'PurDocVen30', 'PurComment', 'PurDocVen1', 'IsAdd', 'RequestNotes', 'OtherFeeTax', 'PurCommentFile', 'DeliveryTotalAmount'];//
                HideFields('#dataFormMaster', HiddenMasterFields);
                
                //物品類別可用
                $("#dataFormMasterItemTypeID").combobox('enable', true);
                //隱藏作業名稱
                //HideFields('#dataFormMaster', ['D_STEP_ID']);

                //隱藏dataGridDetail欄位//, 'OtherFee'
                gridDetailColumns = ['PurQty', 'PurPrice', 'PurTax', 'TotalAmount', 'PurVendor', 'sumDeliveryQty', 'sumAcceptanceQty'];
                HideGridColumns("#dataGridDetail", gridDetailColumns);

                //物品類別停用
                //if ($("#dataFormMasterItemTypeID").combobox('getValue') != "") {
                    //$("#dataFormMasterItemTypeID").combobox('disable', true);
                //}

                //Master啟用 建議廠商、預算年度、暫借款單
                var EnabledFieldArr = [];
                var EnabledComboboxArr = ['VoucherYear','ShortTermNO'];
                var EnabledRefvalArr = ['RecVendor'];
                EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);

                //Master啟用樣品照片、估價單
                $('.info-fileUpload-file[name="PrPicfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrPicfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PrDocfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrDocfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PrPic"]').attr('disabled', false);
                $('input[name="PrDoc"]').attr('disabled', false);

                //顯示 檢視、編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                if (infolightOptions.indexOf("commandButtons:'v'") >= 0) {
                    infolightOptions.replace("commandButtons:'v'", "commandButtons:'vud'");
                } else if (infolightOptions.indexOf("commandButtons:'vu'") >= 0) {
                    infolightOptions.replace("commandButtons:'vu'", "commandButtons:'vud'");
                }
                dgView.attr("infolight-options", infolightOptions);
            } else if (parameter == "S2" ) {//資料與備品確認 
                //交貨明細隱藏
                $("#dataGridDeliveryDiv").hide();
                //採購人員填的欄位隱藏
                var HiddenFields = ['PurQty', 'PurPrice', 'PurTax', 'PurVendor', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurVendor10', 'PurVendor20', 'PurVendor30'];
                HideFields('#dataFormDetail', HiddenFields);
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //隱藏安排交貨數量完成、採購作業要填的欄位
                var HiddenMasterFields = ['PurTotalAmount','DeliveryTotalAmount', 'OtherFee', 'OtherComment', 'POPayTypeID', 'PurVendor1', 'PurVendor2', 'PurVendor3', 'PurDocVen2', 'PurDocVen3', 'PurDocVen10', 'PurDocVen20', 'PurDocVen30', 'PurComment', 'PurDocVen1', 'RequestNotes','OtherFeeTax','PurCommentFile'];//
                HideFields('#dataFormMaster', HiddenMasterFields);

                //隱藏dataGridDetail欄位, 'OtherFee'
                gridDetailColumns = ['PurQty', 'PurPrice', 'PurTax', 'TotalAmount', 'PurVendor', 'sumDeliveryQty', 'sumAcceptanceQty'];
                HideGridColumns("#dataGridDetail", gridDetailColumns);
                //啟用補單
                EnableFields("#dataFormMaster", ['IsAdd'], [], []);
                //停用欄位(請購明細)
                DisableFields("#dataFormDetail", ['ItemSpec', 'RegQty', 'Unit','RegPrice'], ['RegDate'], []);

                //[隱藏]請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
            } else if (parameter == "S3") {//請購者主管審核
                //交貨明細隱藏
                $("#dataGridDeliveryDiv").hide();
                //採購人員填的欄位隱藏
                var HiddenFields = ['PurQty', 'PurPrice', 'PurTax', 'PurVendor', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurVendor10', 'PurVendor20', 'PurVendor30'];
                HideFields('#dataFormDetail', HiddenFields);
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //隱藏安排交貨數量完成、採購作業要填的欄位
                var HiddenMasterFields = ['PurTotalAmount','DeliveryTotalAmount', 'OtherFee', 'OtherComment', 'POPayTypeID', 'PurVendor1', 'PurVendor2', 'PurVendor3', 'PurDocVen2', 'PurDocVen3', 'PurDocVen10', 'PurDocVen20', 'PurDocVen30', 'PurComment', 'PurDocVen1', 'RequestNotes', 'OtherFeeTax', 'PurCommentFile'];//
                HideFields('#dataFormMaster', HiddenMasterFields);

                //隱藏dataGridDetail欄位, 'OtherFee'
                gridDetailColumns = ['PurQty', 'PurPrice', 'PurTax', 'TotalAmount', 'PurVendor', 'sumDeliveryQty', 'sumAcceptanceQty'];
                HideGridColumns("#dataGridDetail", gridDetailColumns);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
            } else if (parameter == "S4") {//採購作業
                //[隱藏]請購明細和交貨明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);

                //啟用 採購作業要填的欄位
                var EnabledFieldArr = ['OtherFee', 'OtherComment', 'PurComment','OtherFeeTax'];
                var EnabledComboboxArr = ['POPayTypeID', 'ShortTermNO'];
                var EnabledRefvalArr = ['PurVendor1', 'PurVendor2', 'PurVendor3'];
                EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);
                $('.info-fileUpload-file[name="PurDocVen1file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen1file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen2file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen2file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen3file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen3file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurDocVen1"]').attr('disabled', false);
                $('input[name="PurDocVen2"]').attr('disabled', false);
                $('input[name="PurDocVen3"]').attr('disabled', false);
                $("#clearBtn1").attr('disabled', false);
                $("#clearBtn2").attr('disabled', false);
                $("#clearBtn3").attr('disabled', false);
                $("#checkBox1").attr('disabled', false);
                $("#checkBox2").attr('disabled', false);
                $("#checkBox3").attr('disabled', false);
                $('.info-fileUpload-file[name="PurCommentFilefile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurCommentFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurCommentFile"]').attr('disabled', false);
                //停用欄位(請購明細)
                //var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit'];
                //開放ItemSpec給採購填入
                var DisabledFieldArr1 = ['RegQty', 'RegPrice', 'Unit'];
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID'];//, 'RecVendor'
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1); 
                //隱藏 採購總金額
                var HiddenMasterFields = ['RequestNotes','DeliveryTotalAmount'];//'FlagDeliveryEnough'安排交貨數量完成
                HideFields('#dataFormMaster', HiddenMasterFields);
                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'AcceptanceQty'];//'DeliveryQty'
                var DisabledComboboxArr2 = ['PayWayID', 'AcceptanceDate'];
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);
                //隱藏交貨明細的欄位
                var HiddenDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'AcceptanceQty', 'PayWayID', 'AcceptanceDate', 'Surveyors', 'ReturnQty', 'AcceptanceTax','AccountNO','ProofTypeID','PayTo','PlanPayDate'];
                HideFields('#dataFormDelivery', HiddenDeliveryFields);
                //顯示 首批交貨日期、首批交貨數量(只有"採購作業"這關才會顯示)
                var ShowyFields = ['FirstDeliveryDate', 'FirstDeliveryQty'];
                ShowFields('#dataFormDetail', ShowyFields);
                //交貨明細隱藏，當沒資料(要新增交貨明細只能填首批交貨日期和首批交貨數量
                var pONO = $("#dataFormMasterPONO").val();
                var pODeliveryLength = GetPODeliveryLength("dl.PONO='" + pONO + "'");
                if (pODeliveryLength == '0') {
                    $("#dataGridDeliveryDiv").hide();
                }
                //隱藏dataGridDelivery欄位
                gridDeliveryColumns = ['AcceptanceQty', 'ReturnQty', 'Surveyors', 'InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice'];
                HideGridColumns("#dataGridDelivery", gridDeliveryColumns);
                //setWhere廠商(依據物品類別)
                var ItemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
                if ($("#dataFormMasterPurVendor1").refval('getValue') == '') { 
                $("#dataFormMasterPurVendor1").refval("setWhere", "ItemTypeID='" + ItemTypeID + "'");
                }
                if ($("#dataFormMasterPurVendor2").refval('getValue') == '') {
                $("#dataFormMasterPurVendor2").refval("setWhere", "ItemTypeID='" + ItemTypeID + "'");
                }
                if ($("#dataFormMasterPurVendor3").refval('getValue') == '') {
                $("#dataFormMasterPurVendor3").refval("setWhere", "ItemTypeID='" + ItemTypeID + "'");
                }

                //啟用補單
                EnableFields("#dataFormMaster", ['IsAdd'], [], []);

                //設值 採購者的隸屬部門
                var myArr = GetUserOrgNOs(getClientInfo("UserID"));//得組織編號和上層組織編號
                if (myArr.length > 0) {
                    $("#dataFormMasterOrg_NOParent1").val(myArr[1]);
                }

                //清除，因沒送出或被退回，infofileUpload的value還是會在，但infofileUpload的file不在
                //for (var i = 1; i <= 3; i++) {
                //    var infofileUpload = $('#dataFormMasterPurDocVen' + i);
                //    var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
                //    infofileUploadvalue.val('');
                //}

            } else if (parameter == "S5") {//採購主管審核
                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //隱藏交貨明細的欄位
                var HiddenDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'AcceptanceQty', 'PayWayID', 'AcceptanceDate', 'Surveyors', 'ReturnQty', 'AcceptanceTax', 'AccountNO', 'ProofTypeID', 'PayTo', 'PlanPayDate'];
                HideFields('#dataFormDelivery', HiddenDeliveryFields);
                //隱藏dataGridDelivery欄位
                gridDeliveryColumns = ['AcceptanceQty', 'ReturnQty', 'Surveyors', 'InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice'];
                HideGridColumns("#dataGridDelivery", gridDeliveryColumns);
                //隱藏
                HideFields('#dataFormMaster', ['RequestNotes', 'DeliveryTotalAmount']);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
            } else if (parameter == "S6") {//請購交期安排

                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //停用欄位(請購明細)
                var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit', 'PurQty', 'PurPrice', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3'];//, 'OtherFee'
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID', 'PurVendor'];//, 'RecVendor', 'PurVendor1', 'PurVendor2', 'PurVendor3'
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1);
                
                //可新增交期
                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['PurPrice', 'OtherFee', 'TotalPrice', 'InvoiceNO', 'AcceptanceQty', 'DebtorDays'];//'DeliveryQty'
                var DisabledComboboxArr2 = ['PayWayID','AcceptanceDate'];
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);
                //刪除請採購明細form的按鈕
                $('#JQDialog2').find('.infosysbutton-s').remove();
                $('#JQDialog2').find('.infosysbutton-c').remove();
                //隱藏交貨明細的欄位
                var HiddenDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'InvoiceNO', 'AcceptanceQty', 'PayWayID', 'AcceptanceDate', 'Surveyors', 'ReturnQty', 'AcceptanceTax', 'DebtorDays', 'AccountNO', 'ProofTypeID', 'PayTo', 'PlanPayDate', 'PurVendor'];
                HideFields('#dataFormDelivery', HiddenDeliveryFields);

                //隱藏dataGridDelivery欄位
                gridDeliveryColumns = ['AcceptanceQty', 'ReturnQty', 'Surveyors', 'InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice', 'AcceptancePic', 'AcceptanceTax', 'TotalAmount'];
                HideGridColumns("#dataGridDelivery", gridDeliveryColumns);

                if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == '3') {//分期付款不用新增交貨明細
                    //交貨明細的新增按鈕隱藏
                    $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });

                    //隱藏 編輯、刪除按鈕(分期付款不能編輯或刪除)
                    var dgView = $("#dataGridDelivery");
                    var infolightOptions = dgView.attr("infolight-options");
                    infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                    dgView.attr("infolight-options", infolightOptions);
                }

                //隱藏
                HideFields('#dataFormMaster', ['RequestNotes','DeliveryTotalAmount']);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
            } else if (parameter == "S7") {//請購者驗收

                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);

                //停用欄位(請購明細)
                var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit', 'PurQty', 'PurPrice', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurTax'];//, 'OtherFee'
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID', 'PurVendor'];
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1);

                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['DeliveryQty'];
                var DisabledComboboxArr2 = ['DeliveryDate'];
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);

                //刪除請採購明細form的按鈕
                $('#JQDialog2').find('.infosysbutton-s').remove();
                $('#JQDialog2').find('.infosysbutton-c').remove();

                //隱藏交貨明細的欄位
                var HiddenDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'PayWayID', 'AcceptanceTax', 'AccountNO', 'ProofTypeID', 'PayTo', 'PlanPayDate', 'PurVendor'];
                HideFields('#dataFormDelivery', HiddenDeliveryFields);

                //隱藏dataGridDelivery欄位
                gridDeliveryColumns = ['InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice','AcceptanceTax','TotalAmount','IsAPCompleted'];
                HideGridColumns("#dataGridDelivery", gridDeliveryColumns);

                //隱藏
                HideFields('#dataFormMaster', ['RequestNotes','DeliveryTotalAmount']);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'vu'");
                dgView.attr("infolight-options", infolightOptions);

                //暫借款 採購說明啟用
                EnableFields("#dataFormMaster", ['PurComment'], ['ShortTermNO'], []);

                //驗收照片
                ShowFields('#dataFormDelivery', ['AcceptancePic']);

                if ($("#dataFormMasterPurComment").val() == '') {
                    $("#dataFormMasterPurComment").val('無');
                }

            } else if (parameter == "S8") {//請購者直屬主管驗收

                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //停用欄位(請購明細)
                var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit', 'PurQty', 'PurPrice', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurTax'];//, 'OtherFee'
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID', 'PurVendor'];
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1);

                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['DeliveryQty', 'PurPrice', 'OtherFee', 'TotalPrice', 'InvoiceNO', 'DebtorDays', 'AcceptanceQty','ReturnQty'];
                var DisabledComboboxArr2 = ['DeliveryDate', 'PayWayID', 'AcceptanceDate'];
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);

                //刪除請採購明細form的按鈕
                $('#JQDialog2').find('.infosysbutton-s').remove();
                $('#JQDialog2').find('.infosysbutton-c').remove();

                //隱藏交貨明細的欄位
                var HiddenDeliveryFields = ['PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays', 'InvoiceNO', 'PayWayID', 'AcceptanceTax', 'AccountNO', 'ProofTypeID', 'PayTo', 'PlanPayDate'];
                HideFields('#dataFormDelivery', HiddenDeliveryFields);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);

                //隱藏dataGridDelivery欄位
                gridDeliveryColumns = ['InvoiceNO', 'PurPrice', 'OtherFee', 'TotalPrice', 'AcceptanceTax', 'TotalAmount', 'IsAPCompleted'];
                HideGridColumns("#dataGridDelivery", gridDeliveryColumns);

                //隱藏
                HideFields('#dataFormMaster', ['RequestNotes','DeliveryTotalAmount']);
            } else if (parameter == "S9") {//採購結帳

                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                //var DisabledFieldArr = ['Description'];
                //var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                //var DisabledRefvalArr = [];
                //DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //停用欄位(請購明細)
                var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit', 'PurQty', 'PurPrice', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurTax'];//, 'OtherFee'
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID', 'PurVendor'];
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1);

                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['DeliveryQty', 'AcceptanceQty', 'ReturnQty'];//'PurPrice', 'OtherFee', 'TotalPrice', 'DebtorDays',
                var DisabledComboboxArr2 = ['DeliveryDate', 'AcceptanceDate'];//, 'PayWayID'
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);

                //刪除請採購明細form的按鈕
                $('#JQDialog2').find('.infosysbutton-s').remove();
                $('#JQDialog2').find('.infosysbutton-c').remove();
                

                //啟用
                var EnabledFieldArr = ['OtherFee', 'OtherComment', 'PurComment','OtherFeeTax'];
                var EnabledComboboxArr = ['POPayTypeID', 'VoucherYear', 'ShortTermNO'];
                var EnabledRefvalArr = ['PurVendor1', 'PurVendor2', 'PurVendor3', 'RecVendor'];
                EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);
                $('.info-fileUpload-file[name="PurDocVen1file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen1file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen2file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen2file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen3file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen3file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurDocVen1"]').attr('disabled', false);
                $('input[name="PurDocVen2"]').attr('disabled', false);
                $('input[name="PurDocVen3"]').attr('disabled', false);
                $("#clearBtn1").attr('disabled', false);
                $("#clearBtn2").attr('disabled', false);
                $("#clearBtn3").attr('disabled', false);
                $("#checkBox1").attr('disabled', false);
                $("#checkBox2").attr('disabled', false);
                $("#checkBox3").attr('disabled', false);
                $('.info-fileUpload-file[name="PurCommentFilefile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurCommentFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurCommentFile"]').attr('disabled', false);

                //Master啟用樣品照片、估價單
                $('.info-fileUpload-file[name="PrPicfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrPicfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PrDocfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrDocfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PrPic"]').attr('disabled', false);
                $('input[name="PrDoc"]').attr('disabled', false);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'vu'");
                dgView.attr("infolight-options", infolightOptions);
                //篩選會計科目
                var voucherYear = $("#dataFormMasterVoucherYear").combobox('getValue');
                var itemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
                var costCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
                if (voucherYear != "" && itemTypeID != "" && costCenterID != "") {
                    $("#dataFormMasterAcSubno").combobox('setWhere', "i.CostCenterID='" + costCenterID + "' and i.ItemTypeID='" + itemTypeID + "' and VoucherYear='" + voucherYear + "'");
                }
            } else if (parameter == "S11") {//會計審核
                //請購明細的新增按鈕隱藏
                $("#dataGridDetailInsertBtn").css({ 'visibility': 'hidden' });
                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(請購主檔)
                //var DisabledFieldArr = ['Description'];
                //var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                //var DisabledRefvalArr = [];
                //DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //停用欄位(請購明細)
                var DisabledFieldArr1 = ['ItemSpec', 'RegQty', 'RegPrice', 'Unit', 'PurQty', 'PurPrice', 'PurPriceVen1', 'PurPriceVen2', 'PurPriceVen3', 'PurTax'];//, 'OtherFee'
                var DisabledComboboxArr1 = ['RegDate'];
                var DisabledRefvalArr1 = ['ItemID', 'PurVendor'];
                DisableFields("#dataFormDetail", DisabledFieldArr1, DisabledComboboxArr1, DisabledRefvalArr1);

                //交貨明細的新增按鈕隱藏
                $("#dataGridDeliveryInsertBtn").css({ 'visibility': 'hidden' });
                //停用欄位(交貨明細)
                var DisabledFieldArr2 = ['DeliveryQty', 'PurPrice', 'OtherFee', 'TotalPrice', 'InvoiceNO', 'DebtorDays', 'AcceptanceQty', 'AccountNO'];
                var DisabledComboboxArr2 = ['DeliveryDate', 'PayWayID', 'AcceptanceDate'];
                var DisabledRefvalArr2 = [];
                DisableFields("#dataFormDelivery", DisabledFieldArr2, DisabledComboboxArr2, DisabledRefvalArr2);

                //刪除請採購明細form的按鈕
                $('#JQDialog2').find('.infosysbutton-s').remove();
                $('#JQDialog2').find('.infosysbutton-c').remove();

                //啟用 採購作業要填的欄位
                var EnabledFieldArr = ['OtherFee', 'OtherComment', 'PurComment','OtherFeeTax'];
                var EnabledComboboxArr = ['POPayTypeID'];
                var EnabledRefvalArr = ['PurVendor1', 'PurVendor2', 'PurVendor3'];
                EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);
                $('.info-fileUpload-file[name="PurDocVen1file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen1file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen2file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen2file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PurDocVen3file"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurDocVen3file"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurDocVen1"]').attr('disabled', false);
                $('input[name="PurDocVen2"]').attr('disabled', false);
                $('input[name="PurDocVen3"]').attr('disabled', false);
                $("#clearBtn1").attr('disabled', false);
                $("#clearBtn2").attr('disabled', false);
                $("#clearBtn3").attr('disabled', false);
                $("#checkBox1").attr('disabled', false);
                $("#checkBox2").attr('disabled', false);
                $("#checkBox3").attr('disabled', false);
                $('.info-fileUpload-file[name="PurCommentFilefile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PurCommentFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PurCommentFile"]').attr('disabled', false);

                //Master啟用 建議廠商、預算年度
                var EnabledFieldArr = [];
                var EnabledComboboxArr = ['VoucherYear'];
                var EnabledRefvalArr = ['RecVendor'];
                EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);

                //Master啟用樣品照片、估價單
                $('.info-fileUpload-file[name="PrPicfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrPicfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('.info-fileUpload-file[name="PrDocfile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PrDocfile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PrPic"]').attr('disabled', false);
                $('input[name="PrDoc"]').attr('disabled', false);
                //財產目錄& 解除傳票設定
                //-------Rebecca Add-------//
                ShowFields('#dataFormMaster', ['IsCatalogue', 'unlock']);
                var glVoucherLink = $('<a>', { href: 'javascript:void(0)', name: 'glVoucher', onclick: 'OpeneglVoucher.call(this)' }).linkbutton({ plain: false, text: '傳票設定' })[0].outerHTML
                if (getEditMode($("#dataFormMaster")) != 'viewed') {
                    var daglVoucher = $('#dataFormMasterunlock').closest('td');
                    daglVoucher.append("&nbsp;&nbsp;" + glVoucherLink);
                }
												   
                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'vu'");
                dgView.attr("infolight-options", infolightOptions);
                //篩選會計科目
                var voucherYear = $("#dataFormMasterVoucherYear").combobox('getValue');
                var itemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
                var costCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
                if (voucherYear != "" && itemTypeID != "" && costCenterID != "") {
                    $("#dataFormMasterAcSubno").combobox('setWhere', "i.CostCenterID='" + costCenterID + "' and i.ItemTypeID='" + itemTypeID + "' and VoucherYear='" + voucherYear + "'");
                }
            } else if (getEditMode($("#dataFormMaster")) == 'viewed') {
                //停用欄位(請購主檔)
                var DisabledFieldArr = ['Description','RequestNotes'];
                var DisabledComboboxArr = ['CostCenterID', 'ItemTypeID', 'POTypeID', 'RequisitKindID', 'InsGroupID', 'AcSubno'];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);

                //隱藏 編輯、刪除按鈕
                var dgView = $("#dataGridDetail");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
                var dgView = $("#dataGridDelivery");
                var infolightOptions = dgView.attr("infolight-options");
                infolightOptions = infolightOptions.replace("commandButtons:'vud'", "commandButtons:'v'");
                dgView.attr("infolight-options", infolightOptions);
            }


            //加下載連結
            for (i = 1; i <= 3; i++) {
                $("#download" + i).remove();
                var file = $('.info-fileUpload-value', $("#dataFormMasterPurDocVen" + i).next()).val();
                if (file != '') {
                    var link = $("<a download>").attr({ 'id': 'download' + i, 'href': '../JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen' + i + '/' + file }).html('下載');
                    $('#dataFormMasterPurDocVen' + i).closest('td').append(link);
                }
            }

            //加下載連結
            $("#downloadPrPic").remove();
            $("#downloadPrDoc").remove();
            var filePic=$('.info-fileUpload-value', $("#dataFormMasterPrPic").next()).val();
            if (filePic != '') {
               var link = $("<a download>").attr({ 'id': 'downloadPrPic', 'href': '../JB_ADMIN/PO_Normal_PRPOIQC/PrPic/' + filePic }).html('下載');
               $('#dataFormMasterPrPic').closest('td').append(link);
            }
            var fileDoc = $('.info-fileUpload-value', $("#dataFormMasterPrDoc").next()).val();
            if (fileDoc != '') {
                var link = $("<a download>").attr({ 'id': 'downloadPrDoc', 'href': '../JB_ADMIN/PO_Normal_PRPOIQC/PrDoc/' + fileDoc }).html('下載');
                $('#dataFormMasterPrDoc').closest('td').append(link);
            }
            
            $("#downloadPurCommentFile").remove();
            var PurCommentFile = $('.info-fileUpload-value', $("#dataFormMasterPurCommentFile").next()).val();
            if (PurCommentFile != '') {
                var link = $("<a download>").attr({ 'id': 'downloadPurCommentFile', 'href': '../JB_ADMIN/PO_Normal_PRPOIQC/PurCommentFile/' + PurCommentFile }).html('下載');
                $('#dataFormMasterPurCommentFile').closest('td').append(link);
            }
        }

        //請採購明細OnLoad
        function dataFormDetail_OnLoadSuccess() {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            //申請
            if (getEditMode($("#dataFormDetail")) == "inserted" || (getEditMode($("#dataFormDetail")) == "updated" && parameter == "")) {
                //物品類別連動物品combobox(請購單只能有一種物品類別)
                var ItemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
                $("#dataFormDetailItemID").refval("setWhere", "ItemTypeID in('" + ItemTypeID + "')");//,'IT9999'
                //setWhere建議廠商(依據物品類別)
                //$("#dataFormDetailRecVendor").refval("setWhere","ItemTypeID='" + ItemTypeID + "'");
            } else if (getEditMode($("#dataFormDetail")) == "updated" && parameter=="S4") {//採購作業
                //[停用]品名 
                $("#dataFormDetailItemID").refval("disable", true);

                //[篩選]setWhere xx廠商(依據物品類別)
                var ItemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
                $("#dataFormDetailPurVendor").refval("setWhere", "ItemTypeID='" + ItemTypeID + "'");

                

                //[設值]採購數量 預設值
                if ($("#dataFormDetailPurQty").val() == '') {
                    $("#dataFormDetailPurQty").focus();
                    $("#dataFormDetailPurQty").val($("#dataFormDetailRegQty").val());
                    $("#dataFormDetailPurQty").blur();
                }
                //[設值]首批交貨日期 預設值
                if ($("#dataFormDetailFirstDeliveryDate").datebox('getValue') == '') {
                    $("#dataFormDetailFirstDeliveryDate").datebox('setValue', $("#dataFormDetailRegDate").datebox('getValue'));
                }

                //[啟用,停用]交貨日期FirstDeliveryDate、交貨數量FirstDeliveryQty、報價1、報價2、報價3
                if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == "3") {//分期付款
                    if ($("#dataFormDetailItemID").refval('getValue') != 'I99999') {//請採購項目非I99999
                        //停用
                        $("#dataFormDetailFirstDeliveryDate").combobox('disable');
                        $("#dataFormDetailFirstDeliveryQty").attr('disabled', true);
                        BlackFields('#dataFormDetail', ['FirstDeliveryDate', 'FirstDeliveryQty']);
                        $("#dataFormDetailFirstDeliveryQty").val(0);
                        //啟用
                        $("#dataFormDetailPurPriceVen1").attr('disabled', false);
                        $("#dataFormDetailPurPriceVen2").attr('disabled', false);
                        $("#dataFormDetailPurPriceVen3").attr('disabled', false);
                        RedFields('#dataFormDetail', ['PurPriceVen1']);
                    } else if ($("#dataFormDetailItemID").refval('getValue') == 'I99999') {//請採購項目I99999
                        //啟用
                        $("#dataFormDetailFirstDeliveryDate").combobox('enable');
                        $("#dataFormDetailFirstDeliveryQty").attr('disabled', false);
                        RedFields('#dataFormDetail', ['FirstDeliveryDate', 'FirstDeliveryQty']);
                        //停用
                        $("#dataFormDetailPurPriceVen1").attr('disabled', true);
                        $("#dataFormDetailPurPriceVen2").attr('disabled', true);
                        $("#dataFormDetailPurPriceVen3").attr('disabled', true);
                        BlackFields('#dataFormDetail', ['PurPriceVen1']);
                    }
                } else {//非分期付款
                    if ($("#dataFormDetailItemID").refval('getValue') != 'I99999') {//請採購項目非I99999
                        //啟用
                        $("#dataFormDetailFirstDeliveryDate").combobox('enable');
                        $("#dataFormDetailFirstDeliveryQty").attr('disabled', false);
                        RedFields('#dataFormDetail', ['FirstDeliveryDate', 'FirstDeliveryQty']);
                        //啟用
                        $("#dataFormDetailPurPriceVen1").attr('disabled', false);
                        $("#dataFormDetailPurPriceVen2").attr('disabled', false);
                        $("#dataFormDetailPurPriceVen3").attr('disabled', false);
                        RedFields('#dataFormDetail', ['PurPriceVen1']);
                    }
                }

                //[停用]首批交貨日期或首批交貨數量有值則停用 且 交貨資料有資料，要改交貨日期或交貨數量就要到交貨明細去修改(採購作業重填首批交貨數量或首批交或日期)
                if ($("#dataFormDetailFirstDeliveryDate").datebox('getValue') != '' && $("#dataGridDelivery").datagrid('getRows').length != 0) {
                    DisableFields("#dataFormDetail", [], ['FirstDeliveryDate'], []);
                }
                if ($("#dataFormDetailFirstDeliveryQty").val() != '' && $("#dataGridDelivery").datagrid('getRows').length != 0) {
                    DisableFields("#dataFormDetail", ['FirstDeliveryQty'], [], []);
                }
            }

            //設定請購金額
            dataFormDetailPR_OnBlur();

            //[設值]報價廠商1、報價廠商2、報價廠商3
            $("#dataFormDetailPurVendor10").val($("#dataFormMasterPurVendor1").refval('selectItem').text);
            $("#dataFormDetailPurVendor20").val($("#dataFormMasterPurVendor2").refval('selectItem').text);
            $("#dataFormDetailPurVendor30").val($("#dataFormMasterPurVendor3").refval('selectItem').text);
        }

        //主檔_物品類別連動物品類別負責職稱
        function DFM_ItemTypeID_OnSelect(rowdata) {
            var rows = $("#dataGridDetail").datagrid('getRows');
            if (rows.length > 0) {
                alert("請注意!!!物品類別須跟請購項目的類別一樣");
            }

            $("#dataFormMasterResponsibleGROUPID").combobox('setValue', rowdata.ResponsibleGROUPID);

            //setWhere會計科目和選第一個(依 成本中心、物品類別)
            dataFormMasterCostCenterID_OnSelect();

            //setWhere建議廠商(依據物品類別)
            var ItemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
            $("#dataFormMasterRecVendor").refval("setWhere", "ItemTypeID='" + ItemTypeID + "'");
        }

        function dataFormMasterCostCenterID_OnSelect() {
            //setWhere會計科目(依 預算年度、成本中心、物品類別)
            //if (getEditMode($("#dataFormMaster")) == 'inserted') {
            //    var textBox1 = $("<input/>").attr({ 'id': 'textBox1', 'type': 'textBox','size':'23px', 'placeholder': '輸入物品關鍵字,移動滑鼠查詢', 'onblur': 'onblurfunc(this.value)' });
            //    $('#dataFormMasterItemTypeID').closest('td').append('&nbsp;').append('&nbsp;').append('查詢物品類別').append('&nbsp;').append(textBox1);
            //}
            var voucherYear = $("#dataFormMasterVoucherYear").combobox('getValue');
            var itemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
            var costCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
            if (voucherYear !="" && itemTypeID != "" && costCenterID != "") {
                $("#dataFormMasterAcSubno").combobox('setWhere', "i.CostCenterID='" + costCenterID + "' and i.ItemTypeID='" + itemTypeID + "' and VoucherYear='" + voucherYear + "'");

                //選第一個
                setTimeout(function () {//因為data要抓setWhere後的data，setWhere需要時間
                    var data = $('#dataFormMasterAcSubno').combobox('getData');
                    if (data.length > 0) {
                        $("#dataFormMasterAcSubno").combobox('select', data[0].AcSubno);
                    } else {
                        $("#dataFormMasterAcSubno").combobox('select', '');
                    }
                }, 1000);
            }
        }

        //請採購明細OnApply(為了請購單只能有一種物品類別)
        function dataFormDetail_OnApply() {
            //為了請購單只能有一種物品類別
            var rowsDetail = $("#dataGridDetail").datagrid('getRows');
            if (rowsDetail.length == '0') $("#dataFormMasterItemTypeID").combobox('disable', true);

            var sysVariable = $("#dataFormMastersysVariable").val();
            var PurQty = $("#dataFormDetailPurQty").val();
            var PurPrice = $("#dataFormDetailPurPrice").val();
            //var OtherFee = $("#dataFormDetailOtherFee").val();
            var OtherFee=0
            var PurTax = $("#dataFormDetailPurTax").val();
            var TotalPrice = (Number(PurQty) * Number(PurPrice)) + Number(OtherFee) + Number(PurTax);

            //若報價單沒湊到3個，採購說明為必填
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            if (parameter == "S4") {//採購作業

                //檢查必填
                if ($("#dataFormDetailPurQty").val() == '') {
                    alert("採購數量為必填");
                    return false;
                } else if ($("#dataFormDetailPurPrice").val() == '') {
                    alert("採購單價為必填");
                    return false;
                } else if ($("#dataFormDetailPurVendor").refval('getValue') == '') {
                    alert("採購廠商為必填");
                    return false;
                } else if ($("#dataFormDetailPurPriceVen1").val() == '') {
                    alert("報價單價1為必填");
                    return false;
                } else if ($("#dataFormDetailFirstDeliveryDate").datebox('getValue') == '') {
                    alert("交貨日期為必填");
                    return false;
                } else if ($("#dataFormDetailFirstDeliveryQty").val() == '') {
                    alert("交貨數量為必填");
                    return false;
                } else if (Number($("#dataFormDetailFirstDeliveryQty").val()) > Number($("#dataFormDetailPurQty").val())) {
                    alert("交貨數量不得大於採購數量");
                    $("#dataFormDetailFirstDeliveryQty").focus();
                    return false;
                } else if ($('.info-fileUpload-value', $("#dataFormMasterPurDocVen2").next()).val() != "" && $.trim($("#dataFormDetailPurPriceVen2").val()) == "" && $("#dataFormDetailItemID").refval('getValue') != "I99999") {
                    alert("報價單價2為必填");//主檔報價檔2有填，就須填報價單價2
                    $("#dataFormDetailPurPriceVen2").focus();
                    return false;
                } else if ($('.info-fileUpload-value', $("#dataFormMasterPurDocVen3").next()).val() != "" && $.trim($("#dataFormDetailPurPriceVen3").val()) == "" && $("#dataFormDetailItemID").refval('getValue') != "I99999") {
                    alert("報價單價3為必填");
                    $("#dataFormDetailPurPriceVen3").focus();
                    return false;
                } else {
                    //第一列的首次交貨日期填到以下列的首次交貨日期
                    var itemNO = $("#dataFormDetailItemNO").val();
                    if (itemNO = "001") {
                        var firstDeliveryDate = $("#dataFormDetailFirstDeliveryDate").datebox('getValue');

                        var dialoggrid = "#dataGridDetail";
                        var rows = $(dialoggrid).datagrid('getRows');
                        var length = rows.length;
                        for (var i = 1; i < length; i++) {
                            $(dialoggrid).datagrid('beginEdit', i);
                            var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'FirstDeliveryDate' });
                            if (editor) {
                                editor.actions.setValue(editor.target, firstDeliveryDate);//設值 (rows[i]['purqty'] * row[i]['purprice']) + row[i]['purtax']
                            }
                            $(dialoggrid).datagrid('endEdit', i);
                        }
                    }
                    //設值 採購總金額
                    SetdataFormMasterPurTotalAmount();

                    //檢查都過
                    return true;
                }

            }
            else if (getEditMode($("#dataFormMaster")) == 'inserted' || (getEditMode($("#dataFormMaster")) == 'updated' && parameter == "")) {//第一關
                if ($("#dataFormMasterRequisitKindID").combobox('getValue') == '2' && $.trim($("#dataFormDetailRegPrice").val())=='') {//非例行性
                    alert("請購性質為非例行姓，請購單價為必填");
                    $("#dataFormDetailRegPrice").focus();
                    return false;
                }

                //需求日-申請日 < 前置採購天數 ，則提示
                dataFormDetailRegDate_OnBlur();
            }
        }

        //請採購明細OnDeleted(為了請購單只能有一種物品類別)
        function dataGridDetail_OnDeleted() {
            //為了請購單只能有一種物品類別
            var rowsDetail = $("#dataGridDetail").datagrid('getRows');
            if (rowsDetail.length == '0') $("#dataFormMasterItemTypeID").combobox('enable');
        }

        //主檔的物品類別須先填，才能新增請採購明細檔
        function dataGridDetail_OnInsert() {
            if ($("#dataFormMasterItemTypeID").combobox('getValue') == '') {
                alert("請先選擇「物品類別」");
                return false;
            } else if ($("#dataFormMasterRequisitKindID").combobox('getValue') == '') {
                alert("請先選擇「請購性質」");
                return false;
            } else {
                return true;
            }
        }

        //請採購明細的報價單1
        function DFDPurDocVen1_OnSuccess() {
            $('input[name="PurDocVen1"]').attr('disabled', true);
        }

        //請採購明細的報價單1
        function DFDPurDocVen2_OnSuccess() {
            $('input[name="PurDocVen2"]').attr('disabled', true);
        }

        //請採購明細的報價單1
        function DFDPurDocVen3_OnSuccess() {
            $('input[name="PurDocVen3"]').attr('disabled', true);
        }

        //請採購明細的報價單1
        function dataFormMasterPurCommentFile_OnSuccess() {
            $('input[name="PurCommentFile"]').attr('disabled', true);
        }
        
        function dataFormDelivery_OnLoadSuccess() {
            //清文字
            $("#fontDeliveryQty").remove();

            //用flow的parameters來判斷關卡，來安排交貨 或 驗收人員設值
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            //交貨明細form新增，須先選"請採購明細"任一筆(for採購作業&請購交期安排)
            if (parameter == 'S6' || parameter == 'S4') {//交期安排
                //啟用 交貨數量、交貨日期
                $("#dataFormDeliveryDeliveryQty").attr('disabled', false);
                $("#dataFormDeliveryDeliveryDate").combobox('enable');

                //新增
                if (getEditMode($("#dataFormDelivery")) == 'inserted') {
                    var rows = $("#dataGridDetail").datagrid("getSelected");
                    if (rows != null) {
                        $("#dataFormDeliveryItemNO").val(rows.ItemNO);//設定請採購項次
                        $("#dataFormDeliveryItemID").combobox('setValue', rows.ItemID);//設定品名
                        $("#dataFormDeliveryPurPrice").val(rows.PurPrice);//設定單價
                        $("#dataFormDeliveryOtherFee").val(0);//設定其他費用
                        $("#dataFormDeliveryDebtorDays").val(rows.PayTermName);//帳款天數
                        $("#dataFormDeliveryAccountNO").val(rows.VendAccount);//匯款帳號
                    }

                    //顯示 該採購物品的 交貨數量 / 採購數量(for採購作業S4 和 請購者交貨安排S6 )
                    var selectedRow = $("#dataGridDetail").datagrid("getSelected");
                    var data1 = $("#dataGridDelivery").datagrid("getData");
                    //所選的請採購明細
                    var ItemNO = selectedRow.ItemNO;
                    var PurQty = selectedRow.PurQty;//採購數量
                    var SumDeliveryQty = 0;
                    $(data1.rows).each(function () {//交貨明細
                        if (ItemNO == this.ItemNO) {
                            SumDeliveryQty = Number(SumDeliveryQty) + Number(this.DeliveryQty);//已安排交貨總數量
                        }
                    });
                    $('#dataFormDeliveryDeliveryQty').closest('td').append("<font id='fontDeliveryQty' color='blue'>交/採:" + SumDeliveryQty + "/" + PurQty + "</font>");

                //編輯
                } else if (getEditMode($("#dataFormDelivery")) == 'updated') {
                    //完成IsAssetCompleted，就停用交貨數量、交貨日期
                    if ($("#dataFormDeliveryIsAssetCompleted").checkbox('getValue') == true) {
                        $("#dataFormDeliveryDeliveryQty").attr('disabled', true);
                        $("#dataFormDeliveryDeliveryDate").combobox('disable');
                    } 
                }


                

            } else if (parameter == 'S7' && (getEditMode($("#dataFormDelivery")) == 'updated')) {//請購者驗收
                //啟用
                $("#dataFormDeliveryAcceptanceDate").combobox('enable');
                $("#dataFormDeliveryAcceptanceQty").attr('disabled', false);
                $("#dataFormDeliveryReturnQty").attr('disabled', false);

                //完成IsAssetCompleted，就停用
                if ($("#dataFormDeliveryIsAssetCompleted").checkbox('getValue') == true) {
                    $("#dataFormDeliveryAcceptanceDate").combobox('disable');
                    $("#dataFormDeliveryAcceptanceQty").attr('disabled', true);
                    $("#dataFormDeliveryReturnQty").attr('disabled', true);
                } else {
                    //設定驗收人員
                    $("#dataFormDeliverySurveyors").val(getClientInfo('_username'));

                    //設定驗收日期
                    if ($("#dataFormDeliveryAcceptanceDate").datebox("getValue") == "") {
                        var date = new Date();
                        var year = date.getYear() + 1900;
                        var month = date.getMonth() + 1;
                        var day = date.getDate();
                        var now = year + "/" + month + "/" + day;
                        $("#dataFormDeliveryAcceptanceDate").datebox("setValue", now);
                    }
                    //設定退貨數量
                    if ($("#dataFormDeliveryReturnQty").val() == "") {
                        $("#dataFormDeliveryReturnQty").val(0);
                    }
                }

                //根據物品去呼叫infoCommand來取IsAsset，若=1則顯示資產異動單才有的欄位(onapply也是要取IssAsset，若=1則必填)
                //var itemID = $("#dataFormDeliveryItemID").combobox('getValue');
                //var isAsset = GetInfoCommandValue($("#dataFormDeliveryItemID"), "ItemID='" + itemID + "'", "IsAsset");
                var ItemNO = $("#dataFormDeliveryItemNO").val();
                var IsAsset;

                var data = $("#dataGridDetail").datagrid("getData"); //讀取 Grid 資料到 data
                var rowData;
                $(data.rows).each(function () {
                    if (this.ItemNO == ItemNO) {
                        IsAsset = this.IsAsset;
                        return false;
                    }
                });

                var fields = ['AssetLocaID'];
                if (IsAsset == "1" || IsAsset == true) {
                    ShowFields('#dataFormDelivery', fields);
                    //啟用
                    EnableFields("#dataFormDelivery", [], ['AssetLocaID'], []);
                } else {
                    HideFields('#dataFormDelivery', fields);
                }
                

            } else if (parameter == 'S8' && (getEditMode($("#dataFormDelivery")) == 'updated')) {//請購者主管驗收
                var ori = $("#dataFormDeliverySurveyors").val();
                if (ori.indexOf(getClientInfo('_username')) < 0) {
                    $("#dataFormDeliverySurveyors").val(ori + ',' + getClientInfo('_username'));//驗收人員設值
                }
            } else if (parameter == 'S9') {//採購結帳
                //啟用
                $("#dataFormDeliveryPurPrice").attr('disabled', false);
                $("#dataFormDeliveryOtherFee").attr('disabled', false);
                $("#dataFormDeliveryAcceptanceTax").attr('disabled', false);
                $("#dataFormDeliveryTotalPrice").attr('disabled', false);
                $("#dataFormDeliveryPayWayID").combobox('enable');
                $("#dataFormDeliveryDebtorDays").attr('disabled', false);
                $("#dataFormDeliveryInvoiceNO").attr('disabled', false);
                $("#dataFormDeliveryAccountNO").attr('disabled', false);
                $("#dataFormDeliveryProofTypeID").combobox('enable');

                if ($("#dataFormDeliveryIsAssetCompleted").checkbox('getValue') == true) {
                    //停用
                    //$("#dataFormDeliveryPurPrice").attr('disabled', true);
                    //$("#dataFormDeliveryOtherFee").attr('disabled', true);
                    //$("#dataFormDeliveryAcceptanceTax").attr('disabled', true);
                    //$("#dataFormDeliveryTotalPrice").attr('disabled', true);
                    //$("#dataFormDeliveryPayWayID").combobox('disable');
                    //$("#dataFormDeliveryDebtorDays").attr('disabled', true);
                    //$("#dataFormDeliveryInvoiceNO").attr('disabled', true);
                    //$("#dataFormDeliveryAccountNO").attr('disabled', true);
                    //$("#dataFormDeliveryProofTypeID").combobox('disable');
                } else {
                    if ($("#dataFormDeliveryAcceptanceTax").val() == '') {
                        //設定 其他費用
                        if ($("#dataFormDeliveryOtherFee").val() == '') {
                            $("#dataFormDeliveryOtherFee").focus();
                            $("#dataFormDeliveryOtherFee").val(0);
                            $("#dataFormDeliveryOtherFee").blur();
                        }
                        //設定 稅額
                        dataFormDelivery_OnBlur();
                    }
                    
                    if ($("#dataFormDeliveryPayTo").refval('getValue') == '') {
                        //設定PayTo付款對象
                        $("#dataFormDeliveryPayTo").refval('setValue', $("#dataFormDeliveryPurVendor").refval('getValue'));
                    }
                    if($("#dataFormDeliveryPlanPayDate").datebox('getValue')==''){
                        //設定PlanPayDate應付日
                        var EndDay=25;
                        var DebtorDays=$("#dataFormDeliveryDebtorDays").val();
                        var AcceptanceDate=$("#dataFormDeliveryAcceptanceDate").datebox('getValue');
                        var PlanPayDate=GetPlanPayDate(EndDay,DebtorDays,AcceptanceDate);
                        $("#dataFormDeliveryPlanPayDate").datebox('setValue', PlanPayDate);
                    }
                    
                }
            }
            //會計可修改預付日期
            if (parameter == 'S9') {
                $("#dataFormDeliveryPlanPayDate").attr('disabled', false);

            }

            //其他關卡要看的
            if (parameter != 'S7') {
                if ($("#dataFormDeliveryAssetLocaID").combobox('getValue') != "") {
                    ShowFields('#dataFormDelivery', ['AssetLocaID']);
                } else {
                    HideFields('#dataFormDelivery', ['AssetLocaID']);
                }
            }

            //加下載連結
            //$("#downloadAcceptancePic").remove();
            //var AcceptancePic = $('.info-fileUpload-value', $("#dataFormDeliveryAcceptancePic").next()).val();
            //if (AcceptancePic != '') {
            //    var link = $("<a download>").attr({ 'id': 'downloadAcceptancePic', 'href': '../JB_ADMIN/PO_Normal_PRPOIQC/AcceptancePic/' + AcceptancePic }).html('下載');
            //    $('#downloadAcceptancePic').closest('td').append(link);
            //}
        }

        //設定稅額及物品總價(驗收單價、其他費用的OnBlur)
        function dataFormDelivery_OnBlur() {
            //設定稅額
            var price = $.trim($("#dataFormDeliveryPurPrice").val());
            var qty = $.trim($("#dataFormDeliveryAcceptanceQty").val());
            var otherFee = $.trim($("#dataFormDeliveryOtherFee").val());
            var tax = GetTax($("#dataFormDeliveryItemID").combobox('getValue'), price, qty, otherFee);
            $("#dataFormDeliveryAcceptanceTax").focus();
            $("#dataFormDeliveryAcceptanceTax").val(tax);
            $("#dataFormDeliveryAcceptanceTax").blur();
            

            //設定物品總價
            var totalPrice = (Number(qty) * Number(price));// + Number(otherFee) + Number(tax);
            $("#dataFormDeliveryTotalPrice").focus();
            $("#dataFormDeliveryTotalPrice").val(totalPrice);//Math.round(Number(totalPrice.toFixed(2)))
            $("#dataFormDeliveryTotalPrice").blur();
        }

        //設定稅額(工程金額的OnBlur)
        function dataFormDeliveryOtherFee_OnBlur() {
            //設定稅額
            var price = $.trim($("#dataFormDeliveryPurPrice").val());
            var qty = $.trim($("#dataFormDeliveryAcceptanceQty").val());
            var otherFee = $.trim($("#dataFormDeliveryOtherFee").val());
            var tax = GetTax($("#dataFormDeliveryItemID").combobox('getValue'), price, qty, otherFee);
            $("#dataFormDeliveryAcceptanceTax").focus();
            $("#dataFormDeliveryAcceptanceTax").val(tax);
            $("#dataFormDeliveryAcceptanceTax").blur();
        }

        //(不用，form裡沒有總價)設定總價(稅額的OnBlur)
        function dataFormDeliveryTax_OnBlur() {
            
            var price = $.trim($("#dataFormDeliveryPurPrice").val());
            var qty = $.trim($("#dataFormDeliveryAcceptanceQty").val());
            var otherFee = $.trim($("#dataFormDeliveryOtherFee").val());

            var tax = $.trim($("#dataFormDeliveryAcceptanceTax").val());

            //設定物品總價
            var totalPrice = (Number(qty) * Number(price));// + Number(otherFee) + Number(tax);
            $("#dataFormDeliveryTotalPrice").focus();
            $("#dataFormDeliveryTotalPrice").val(Number(totalPrice));
            $("#dataFormDeliveryTotalPrice").blur();
        }

        //設定總價
        function dataFormDetailPR_OnBlur() {
            var price = $.trim($("#dataFormDetailRegPrice").val());
            var qty = $.trim($("#dataFormDetailRegQty").val());
            //設定總價
            var regTotalAmount = (Number(qty) * Number(price));
            $("#dataFormDetailRegTotalAmount").focus();
            $("#dataFormDetailRegTotalAmount").val(regTotalAmount);
            $("#dataFormDetailRegTotalAmount").blur();
        }

        //交易明細新增前，須先選請採購明細任一筆
        function dataGridDelivery_OnInsert() {
            var rows = $("#dataGridDetail").datagrid("getSelected");
            if (rows == null) {
                alert("請先選擇請採購明細一筆");
                return false;
            } else {
                if (rows.PurQty==null) {
                    alert("請採購明細的採購欄位未填");
                    return false;
                }
            }
        }

        //請購者驗收，自動填未收數量
        function dataFormMaster_OnApply() {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            
            //請購者交期安排、採購作業
            if ((parameter == 'S6' || parameter == 'S4') && (getEditMode($("#dataFormMaster")) == 'updated')) {

                if (parameter == 'S4') {//採購作業
                    //[檢查]主檔必填
                    if ($("#dataFormMasterPurVendor1").refval('getValue') == '' && $("#dataFormMasterIsAdd").val()!='on') {
                        alert("報價廠商1為必填");
                        return false;
                    } else if ($('.info-fileUpload-value', $("#dataFormMasterPurDocVen1").next()).val() == '' && $("#dataFormMasterIsAdd").val() != 'on') {
                        alert("報價檔1為必填");
                        return false;
                    } else if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == '') {
                        alert("結帳方式為必填");
                        return false;
                    } else if ($('.info-fileUpload-value', $('#dataFormMasterPurDocVen2').next()).val()!="") {
                        var data = $("#dataGridDetail").datagrid("getData");
                        var alertFlag0 = true;
                        $(data.rows).each(function () {
                            if ((this.PurPriceVen2 == null || this.PurPriceVen2 == '') && (this.ItemID != 'I99999')) {//採購報價2沒填//
                                alertFlag0 = false;
                            }
                        });
                        if (alertFlag0 == false) { alert("請採購明細的「報價單價2」未填"); return false; }
                    } else if ($('.info-fileUpload-value', $('#dataFormMasterPurDocVen3').next()).val()!="") {
                        var data = $("#dataGridDetail").datagrid("getData");
                        var alertFlag0 = true;
                        $(data.rows).each(function () {
                            if ((this.PurPriceVen3 == null || this.PurPriceVen3 == '') && (this.ItemID != 'I99999')) {//採購報價3沒填
                                alertFlag0 = false;
                            }
                        });
                        if (alertFlag0 == false) { alert("請採購明細的「報價單價3」未填"); return false; }
                    }

                    //[檢查]採購作業未填採購明細
                    var data = $("#dataGridDetail").datagrid("getData");
                    var alertFlag = false;
                    $(data.rows).each(function () {
                        if (this.PurQty == null || this.PurQty == 'null' || this.PurQty == '') {//採購單價沒填
                            alertFlag = true;
                        }
                    });
                    if (alertFlag == true) { alert("請採購明細的採購欄位未填"); return false; }

                    //[檢查]報價檔三個的門檻
                    var rows = $("#dataGridDetail").datagrid('getRows');
                    var PurTotalAmount = 0;
                    for (var i = 0; i < rows.length; i++) {
                        if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == '3' && rows[i].ItemID == 'I99999') {//分期付款
                            PurTotalAmount = Number(PurTotalAmount) + ((Number(rows[i].PurQty) * Number(rows[i].PurPrice)) + Number(rows[i].PurTax));
                        } else if ($("#dataFormMasterPOPayTypeID").combobox('getValue') != '3' && rows[i].ItemID != 'I99999') {
                            PurTotalAmount = Number(PurTotalAmount) + ((Number(rows[i].PurQty) * Number(rows[i].PurPrice)) + Number(rows[i].PurTax));
                        }
                    }
                    var otherFee = 0; //$("#dataFormMasterOtherFee").val();
                    var TotalAmount = Number(PurTotalAmount) + Number(otherFee);
                    var sysVariable = $("#dataFormMastersysVariable").val();
                    var PurDocVen1 = $('.info-fileUpload-value', $('#dataFormMasterPurDocVen1').next()).val();
                    var PurDocVen2 = $('.info-fileUpload-value', $('#dataFormMasterPurDocVen2').next()).val();
                    var PurDocVen3 = $('.info-fileUpload-value', $('#dataFormMasterPurDocVen3').next()).val();
                    if ($.trim($("#dataFormMasterPurComment").val()) == '' && !(PurDocVen1 != '' && PurDocVen2 != '' && PurDocVen3 != '') && Number(TotalAmount) > Number(sysVariable) && $("#dataFormMasterIsAdd").val() != 'on') {
                        alert("採購總價" + TotalAmount + "大於" + sysVariable + "，請填寫「採購說明」或附3個報價檔");
                        return false;
                        $("#dataFormMasterPurComment").focus();
                    }


                    //[設值]若為分期付款，原請購項目的交貨日期和交貨數量設為空白
                    if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == '3') {
                        var dialoggrid = "#dataGridDetail";
                        var rows = $("#dataGridDetail").datagrid("getRows");
                        $(rows).each(function (rowindex, row) {
                            if (this.ItemID != "I99999") {//請採購項目是分期
                                $(dialoggrid).datagrid('beginEdit', rowindex);
                                var editor = $(dialoggrid).datagrid('getEditor', { index: rowindex, field: 'FirstDeliveryDate' });
                                if (editor) {
                                    editor.actions.setValue(editor.target, '');//設值
                                }
                                editor = $(dialoggrid).datagrid('getEditor', { index: rowindex, field: 'FirstDeliveryQty' });
                                if (editor) {
                                    editor.actions.setValue(editor.target, '');//設值
                                }
                                $(dialoggrid).datagrid('endEdit', rowindex);
                            }
                        });


                        
                    }

                    //[檢查]請採購明細無分期項目(選分期付款)
                    if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == '3') {
                        var rows = $("#dataGridDetail").datagrid("getRows");
                        var alertFlag = false;
                        $(rows).each(function () {
                            if (this.ItemID == 'I99999') {//有分期項目
                                alertFlag = true;
                            }
                        });
                        if (alertFlag == false) { alert("「分期期數」須填及按「確認」"); $("#dataFormMasterInstallments").focus(); return false; }
                    }
                }
                //[檢查](請購者交期安排 且 安排交貨未完成)卡至少新增一筆交貨明細
                else if (parameter == 'S6' && IsDeliveryEnough()!=true) {
                    //若dataGridDelivery都是結賬完成，就卡至少新增一筆
                    var dataDelivery = $("#dataGridDelivery").datagrid("getData");
                    var flag = true;
                    $(dataDelivery.rows).each(function () {
                            flag = flag && this.IsAssetCompleted;
                    });
                    
                    if (flag == true) {
                        //取得PODelivery列數(已存檔 且 結帳未完成 的交貨明細 )
                        var pONO = $("#dataFormMasterPONO").val();
                        var pODeliveryLength = GetPODeliveryLength("dl.PONO='" + pONO + "'");
                        //取得目前dataGridDelivery的列數(含未存檔的交貨明細)

                        var dataDeliveryLength = dataDelivery.rows.length;
                        //若相等(代表沒新增)，alert(至少安排一筆交貨明細，才能送出")
                        if (Number(pODeliveryLength) == Number(dataDeliveryLength)) {
                            alert("至少安排一筆新的交貨明細，才能送出");
                            return false;
                        }
                    }
                }

                
                //[檢查]請採購項次的交貨數量超過採購數量(S4採購作業、S6交期安排都會檢查)
                if ($("#dataFormMasterPOPayTypeID").combobox('getValue') != '3') {//非分期付款
                    $("#dataGridDetail").datagrid('selectRow', -1);
                    $("#dataGridDelivery").datagrid('selectRow', -1);
                    var data = $("#dataGridDetail").datagrid("getData");//請採購明細
                    var data1 = $("#dataGridDelivery").datagrid("getData");//交貨明細

                    var flagDeliveryEnough = true;
                    var arrOver = [];
                    $(data.rows).each(function () {//請採購明細
                        var ItemNO = this.ItemNO;
                        var PurQty = this.PurQty;
                        var SumDeliveryQty = 0;
                        $(data1.rows).each(function () {//交貨明細
                            if (ItemNO == this.ItemNO) {
                                SumDeliveryQty = Number(SumDeliveryQty) + Number(this.DeliveryQty);
                            }
                        });
                        if (Number(PurQty) == Number(SumDeliveryQty)) {//採購數量==交貨安排數量，則交貨數量安排完成
                            flagDeliveryEnough = (flagDeliveryEnough && true);
                        } else {
                            flagDeliveryEnough = (flagDeliveryEnough && false);
                            if (Number(PurQty) < Number(SumDeliveryQty)) {
                                arrOver.push(ItemNO);
                            }
                        }
                    });
                    if (flagDeliveryEnough == true) {//安排交貨數量完成是否
                        //$("#dataFormMasterFlagDeliveryEnough").checkbox('setValue', true);
                    } else {
                        //$("#dataFormMasterFlagDeliveryEnough").checkbox('setValue', false);
                        if (arrOver.length > 0) {
                            alert("請採購項次:" + arrOver.join(",") + "的交貨數量超過採購數量");
                            return false;
                        }
                    }
                }
            } else if (parameter == 'S7' && (getEditMode($("#dataFormMaster")) == 'updated')) {//請購者驗收
                
                $("#dataGridDetail").datagrid('selectRow', -1);
                $("#dataGridDelivery").datagrid('selectRow', -1);

                //檢查 驗收未完成的交貨明細至少要驗收一筆
                var data1 = $("#dataGridDelivery").datagrid("getData");
                var rowsLength0 = 0;//未完成採購結賬的筆數
                var rowsLength1 = 0;//未完成採購結賬 且 沒填驗收人員 的筆數
                $(data1.rows).each(function () {//交貨明細
                    //未完成採購結賬
                    if (this.IsAssetCompleted!=true) {
                        rowsLength0 = Number(rowsLength0) + 1;
                    }
                    //未完成採購結賬 且 沒填驗收人員
                    if ((this.Surveyors == null || this.Surveyors == "") && this.IsAssetCompleted != true) {
                        rowsLength1 = Number(rowsLength1) + 1;
                    }
                });
                if ((rowsLength0 == rowsLength1) && (rowsLength0!=0)) {
                    alert("至少驗收一筆，才能送出");
                    return false;
                }

                if ($("#dataFormMasterPurComment").val() == '') {
                    alert("採購說明暨驗收說明至少要填'無'");
                    return false;
                }

            } else if (parameter == 'S8' && (getEditMode($("#dataFormMaster")) == 'updated')) {//請購者主管驗收，卡填驗收人員(沒按確定)
                var data1 = $("#dataGridDelivery").datagrid("getData");
                var flag = true;
                var arrAlert = [];
                $(data1.rows).each(function () {
                    if ((this.AcceptanceQty !=null && $.trim(this.AcceptanceQty) !='') && this.Surveyors.indexOf(getClientInfo('_username')) < 0) {
                        flag = false;
                        arrAlert.push(this.DeliveryNO);
                    }
                });
                if (flag == false) { alert('交貨項次:'+arrAlert.join(',')+"還沒驗收(請點「驗收人員」欄位的按鈕)");return false;}
            } else if (parameter == 'S9' && (getEditMode($("#dataFormMaster")) == 'updated')) {//卡採購結帳(沒填發票號碼)
                var data1 = $("#dataGridDelivery").datagrid("getData");
                var flag = true;
                var arrAlert = [];
                $(data1.rows).each(function () {
                    if (((this.AcceptanceQty != null && $.trim(this.AcceptanceQty) != '') ) && ($.trim(this.PayWayID) == '')) {//|| $.trim(this.PurPrice) == '' || $.trim(this.TotalPrice) == ''
                        flag = false;
                        arrAlert.push(this.DeliveryNO);
                    }
                });
                if (flag == false) { alert('提醒您，交貨項次:' + arrAlert.join(',') + "已驗收，但未填結帳資料"); return false;}
				if (flag == true) {
                    alert("請記得夾檔發票或收據");
                }				   
            }
            //(請購單申請)新增時，卡明細至少一筆
            else if (getEditMode($("#dataFormMaster")) == 'inserted' || (parameter == "" && (getEditMode($("#dataFormMaster")) == 'updated'))) {
                var rowsDetail = $("#dataGridDetail").datagrid('getRows');
                if (rowsDetail.length == '0') {
                    alert("請採購明細至少一筆");
                    return false;
                }else if($("#dataFormMasterAcSubno").combobox('getValue')==""){
                    alert("會計科目為必填");
                    return false;
                }
            }

            //-------Rebecca Add-------//
            if (parameter == "S11") {//會計審核
                var dataFormMasterCompanyID = $("#dataFormMasterInsGroupID").combobox('getValue');
                //公司別=>3傑報資訊,2傑報人力,6傑信管理
                if (dataFormMasterCompanyID == "2" || dataFormMasterCompanyID == "3" || dataFormMasterCompanyID == "6") {
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


        }

        function dataFormDelivery_OnApply() {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            //請購者交期安排，新增時檢查
            if ((parameter == 'S6' || parameter == 'S4') && (getEditMode($("#dataFormDelivery")) == 'inserted')) {
                
                //if ($("#dataFormMasterPOPayTypeID").combobox('getValue') != '3') {//非分期付款要檢查
                    //必填
                    var deliveryDate = $("#dataFormDeliveryDeliveryDate").datebox('getValue');
                    var deliveryQty = $("#dataFormDeliveryDeliveryQty").val();
                    if (deliveryDate == '') {
                        alert("交貨日期為必填");
                        return false;
                    } else if (deliveryQty == '') {
                        alert("交貨數量為必填");
                        return false;
                    }

                    //檢查 交貨數量有無超過採購數量
                    var selectedRow = $("#dataGridDetail").datagrid("getSelected");//採購
                    var data1 = $("#dataGridDelivery").datagrid("getData");//交貨
                    var flagOverDelivery = false;//超過flag

                    var ItemNO = selectedRow.ItemNO;
                    var PurQty = selectedRow.PurQty;
                    var SumDeliveryQty = 0;
                    $(data1.rows).each(function () {//交貨明細
                        if (ItemNO == this.ItemNO) {
                            SumDeliveryQty = Number(SumDeliveryQty) + Number(this.DeliveryQty);
                        }
                    });
                    if (Number(PurQty) < Number(SumDeliveryQty) + Number(deliveryQty)) {//採購數量==交貨安排數量，則交貨數量安排完成
                        flagOverDelivery = true;
                    } else {
                        flagOverDelivery = false;
                    }

                    if (flagOverDelivery == true) {
                        alert("目前安排的交貨數量超過採購數量");
                        return false;
                    } else {
                        var notDeliveryQty = Number(PurQty) - Number(SumDeliveryQty) - Number(deliveryQty);
                        if (notDeliveryQty == 0) {
                            alert("此請採購項次的交貨數量已安排完全");
                        } else {
                            alert("未安排交貨數量還有" + notDeliveryQty.toString() + "單位");
                        }
                        return true;
                    }
                //} else {//分期付款不能新增
                    //alert("分期付款無法新增交貨明細");
                    //return false;
                //}
            } else if (parameter == 'S7') {//請購者驗收
                //必填
                var infofileUpload = $('#dataFormDeliveryAcceptancePic');//驗收照片
                var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());//驗收照片

                if ($("#dataFormDeliveryAcceptanceDate").datebox('getValue') == '') {//驗收日期
                    alert("驗收日期為必填");
                    return false;
                } else if ($("#dataFormDeliveryAcceptanceQty").val() == '') {//驗收數量
                    alert("驗收數量為必填");
                    return false;
                }
                else if (infofileUploadvalue.val() == '') {//驗收照片
                    alert("驗收照片為必填");
                    return false;
                }
                //(驗收數量+退貨數量)不能大於交貨數量
                if (Number($("#dataFormDeliveryAcceptanceQty").val()) + Number($("#dataFormDeliveryReturnQty").val()) != Number($("#dataFormDeliveryDeliveryQty").val())) {
                    alert("(驗收數量+退貨數量)必須等於交貨數量");
                    return false;
                } 

                
                //根據物品去呼叫infoCommand來取IsAsset，若=11則必填資產異動單才有的欄位
                //var itemID = $("#dataFormDeliveryItemID").combobox('getValue');
                //var isAsset = GetInfoCommandValue($("#dataFormDeliveryItemID"), "ItemID='" + itemID + "'", "IsAsset");
                var ItemNO = $("#dataFormDeliveryItemNO").val();
                var IsAsset;

                var data = $("#dataGridDetail").datagrid("getData"); //讀取 Grid 資料到 data
                var rowData;
                $(data.rows).each(function () {
                    if (this.ItemNO == ItemNO) {
                        IsAsset = this.IsAsset;
                        return false;
                    }
                });

                if ((IsAsset == "1" || IsAsset == true) && $("#dataFormDeliveryAssetLocaID").combobox('getValue') == "") {
                    alert("存放區域為必填");
                    return false;
                    $("#dataFormDeliveryAssetLocaID").focus();
                }

            } else if (parameter == 'S9') {//採購結帳
                //必填
                if ($.trim($("#dataFormDeliveryInvoiceNO").val()) == '' && ($("#dataFormDeliveryProofTypeID").combobox('getValue') == '1')) {//憑據號碼
                    alert("憑據號碼為必填");
                    return false;
                }else if ($.trim($("#dataFormDeliveryPurPrice").val()) == '') {//物品單價
                    alert("物品單價為必填");
                    return false;
                } else if ($.trim($("#dataFormDeliveryTotalPrice").val()) == '') {//物品總價
                    alert("物品總價為必填");
                    return false;
                    //付款方式
                } else if ($("#dataFormDeliveryPayWayID").combobox('getValue') == '' || $("#dataFormDeliveryPayWayID").combobox('getValue') == '---請選擇---') {
                    alert("付款方式為必填");
                    return false;
                } else if ($.trim($("#dataFormDeliveryDebtorDays").val()) == '') {//帳款天數
                    alert("帳款天數為必填");
                    return false;
                } else if ($("#dataFormDeliveryPayWayID").combobox('getValue')=='2' && $("#dataFormDeliveryAccountNO").val() == '') {//匯款帳號
                    alert("付款方式選匯款，匯款帳號為必填");
                    return false;
                } else if ($("#dataFormDeliveryProofTypeID").combobox('getValue') == '' || $("#dataFormDeliveryProofTypeID").combobox('getValue') == '---請選擇---') {
                    alert("檢附憑據為必填");
                    return false;
                } else if ($.trim($("#dataFormDeliveryInvoiceNO").val()) != '' && $("#dataFormDeliveryProofTypeID").combobox('getValue')=='1') {//憑據號碼
                    if ($.trim($("#dataFormDeliveryInvoiceNO").val()).length != 10) {
                        alert("發票號碼不正確");
                        return false;
                    }
                    
                }
                //現金就去除匯款帳號
                if ($("#dataFormDeliveryPayWayID").combobox('getValue') == '1') {
                    $("#dataFormDeliveryAccountNO").val('');
                }

                //設定交貨總金額
                SetdataFormMasterDeliveryTotalAmount();

                if ($.trim($("#dataFormDeliveryAcceptanceTax").val()) == "") {
                    $("#dataFormDeliveryAcceptanceTax").val(0);
                }
                if ($.trim($("#dataFormDeliveryOtherFee").val()) == "") {
                    $("#dataFormDeliveryOtherFee").val(0);
                }

            }
            
        }

        //請購主檔，申請者必填
        function DFM_CheckMethod_POTypeID(val) {
            if (val != '' && val != '---請選擇---') { 
                return true;
            }
            return false;
        }

        //請購明細檔，採購者必填
        function DFD_CheckMethod_PurQty(val) {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            if ((val != '' && val != '---請選擇---' && parameter == 'S4') || parameter != 'S4') {
                return true;
            }
            return false;
        }

        //無法刪除交貨明細
        function DGDelivery_OnDelete(row) {
            //取flow的parameters
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            if (parameter == "S7" || parameter == "S8" || parameter == "S9" || parameter == "S11") {//請購者驗收、請購者主管驗收、採購結帳、會計審核
                alert("無法刪除");
                return false;
            } else if (parameter == "S6" && (row.Surveyors != '' && row.Surveyors != null)) {//(交期安排)已驗收不能刪除
                alert("已驗收完成，不能刪除");
                return false;
            }
        }

        //無法刪除請採購明細
        function DGDetail_OnDelete() {
            //取flow的parameters
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            if (parameter == "S2" || parameter == "S3" || parameter == "S5" || parameter == "S6" || parameter == "S7" || parameter == "S8" || parameter == "S9" || parameter == "S11") {//請購者驗收、請購者主管驗收、採購結帳、會計審核
                alert("無法刪除");
                return false;
            }
        }

        //無法編輯請採購明細
        function DGDetail_OnUpdate() {
            //取flow的parameters
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            //請購者交期安排~會計審核
            if (parameter == "S6" || parameter == "S7" || parameter == "S8" || parameter == "S9" || parameter == "S11") {
                alert("無法編輯");
                return false;
            } else if (parameter == "S4") {//採購作業
                if ($("#dataFormMasterPOPayTypeID").combobox('getValue') == "") {
                    alert("請先填「結帳方式」");
                    return false;
                }
            }
        }
        
        function DGDelivery_OnUpdate(row) {
            //取flow的parameters
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            //請購者主管驗收
            if (parameter == "S8" && (row.Surveyors == '' || row.Surveyors == null)) {//未驗收的交貨明細不能編輯
                alert("請購者還未驗收");
                return false;
            //採購結帳
            } else if (parameter == "S9" && (row.Surveyors == '' || row.Surveyors == null)) {//未驗收的交貨明細不能編輯
                alert("還未驗收，勿填結帳欄位");
                return false;
            } else if (parameter == "S6" && (row.Surveyors != '' && row.Surveyors != null)) {//已驗收不能編輯
                alert("已驗收完成，不能編輯");
                return false;
            }
        }

        //沒用到，因跑流程仍要編輯
        function DGV_OnUplate(row) {
            var UserID = getClientInfo("UserID");
            if (row.Flowflag == null && row.ApplyUserID == UserID) {
                return true;
            } else if (row.ApplyUserID != UserID) {
                alert('您非申請者，無法編輯');
                return false;
            }else {
                alert("已起單，無法編輯");
                return false;
            }
        }

        //修改按鈕
        function DGV_UpdateBtnClick() {
            var UserID = getClientInfo("UserID");
            if ($("#dataGridView").datagrid('getChecked').length == 0) {
                alert('請勾選');
            } else {
                var rows = $("#dataGridView").datagrid('getChecked');
                var row=rows[0];
                if ((row.Flowflag == null || row.Flowflag == '' || row.Flowflag == 'X') && row.ApplyUserID == UserID) {
                    openForm('#JQDialog1', row, "updated", 'dialog');
                    //return true;
                } else if (row.ApplyUserID != UserID) {
                    alert('您非申請者，無法編輯');
                    //return false;
                }else {
                    alert("已起單，無法編輯");
                    //return false;
                }
            }
        }
        //搭配上面的DGV_UpdateBtnClick，改完存檔要refresh
        function DFM_OnApplied() {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            //採購結帳
            //if (parameter == "S9") {
            //    alert("請記得夾檔發票或收據");
            //}
            $("#dataGridView").datagrid('reload');
        }

        //刪除
        function DGV_DeleteBtnClick() {
            var UserID = getClientInfo("UserID");
            if ($("#dataGridView").datagrid('getChecked').length == 0) {
                alert('請勾選');
            } else {
                var pre = confirm("確定刪除?");
                if (pre == true) {
                    var rows = $("#dataGridView").datagrid('getChecked');
                    var row = rows[0];
                    if ((row.Flowflag == null || row.Flowflag == '') && row.ApplyUserID == UserID) {
                        var index = $('#dataGridView').datagrid('getRowIndex', row);
                        var PONO = row.PONO;
                        $('#dataGridView').datagrid('deleteRow', index);//removeRow
                        apply("#dataGridView");
                        alert("請購單" + PONO + "刪除成功");
                    } else if (row.ApplyUserID != UserID) {
                        alert('您非申請者，無法刪除');                                  
                        return false;
                    } else {
                        alert("已起單，無法刪除");
                        return false;
                    }
                }
            }
        }

        //沒用到
        function OnDelete_dataGridView(row) {
                var UserID = getClientInfo("UserID");
                if ((row.Flowflag == null || row.Flowflag == 'X') && row.ApplyUserID == UserID) {
                } else if (row.ApplyUserID != UserID) {
                    alert('您非申請者，無法刪除');
                    return false;
                } else {
                    alert("已起單，無法刪除");
                    return false;
                }
        }
        
        function dataFormMaster_OnCancel() {
            $("#querydataGridView").show();
        }

        //設值採購總金額
        function dataGridDetail_OnLoad() {
            var rows = $("#dataGridDetail").datagrid('getRows');
            var PurTotalAmount = 0;
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].ItemID != 'I99999') {
                    PurTotalAmount = Number(PurTotalAmount) + Number(rows[i].TotalAmount);//用採購金額來算
                }
            }
            var otherFee = 0;//$("#dataFormMasterOtherFee").val();
            var otherFeeTax = 0;//$("#dataFormMasterOtherFeeTax").val();
            $("#dataFormMasterPurTotalAmount").val(Number(PurTotalAmount) + Number(otherFee) + Number(otherFeeTax));
        }
        //設值交貨總金額
        function dataGridDelivery_OnLoad(){
            var rows = $("#dataGridDelivery").datagrid('getRows');
            var DeliveryTotalAmount = 0;
            for (var i = 0; i < rows.length; i++) {
                DeliveryTotalAmount = Number(DeliveryTotalAmount) + Number(rows[i].TotalAmount);
            }
            $("#dataFormMasterDeliveryTotalAmount").val(Number(DeliveryTotalAmount));
        }

        //設值採購總金額(dataFormDetail_OnApply,工程金額_OnBlur)
        function SetdataFormMasterPurTotalAmount() {
            var ItemNO = $("#dataFormDetailItemNO").val();
            var PurPrice = $("#dataFormDetailPurPrice").val();
            var PurQty = $("#dataFormDetailPurQty").val();
            var PurTax = $("#dataFormDetailPurTax").val();

            var rows = $("#dataGridDetail").datagrid('getRows');
            var PurTotalAmount = 0;
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].ItemID != 'I99999') {
                    if (rows[i].ItemNO != ItemNO) {
                        PurTotalAmount = Number(PurTotalAmount) + Number((Number(rows[i].PurPrice) * Number(rows[i].PurQty)) + Number(rows[i].PurTax));
                    } else if (rows[i].ItemNO == ItemNO) {
                        PurTotalAmount = Number(PurTotalAmount) + Number((Number(PurPrice) * Number(PurQty)) + Number(PurTax));
                    }
                }
            }
            var otherFee = 0;//$("#dataFormMasterOtherFee").val();
            var otherFeeTax = 0;//$("#dataFormMasterOtherFeeTax").val();
            //if (otherFeeTax == '' || otherFeeTax == '0') {
            //    otherFeeTax = Math.round(Number(otherFee) * 0.05);
            //    $("#dataFormMasterOtherFeeTax").numberbox('setValue', otherFeeTax);
            //}
            $("#dataFormMasterPurTotalAmount").val(Math.round(Number(PurTotalAmount) + Number(otherFee)+Number(otherFeeTax)));
        }

        //設值交貨總金額
        function SetdataFormMasterDeliveryTotalAmount() {
            var DeliveryNO = $("#dataFormDeliveryDeliveryNO").val();
            var TotalPrice = $("#dataFormDeliveryTotalPrice").val();
            var OtherFee = $("#dataFormDeliveryOtherFee").val();
            var AcceptanceTax = $("#dataFormDeliveryAcceptanceTax").val();

            var rows = $("#dataGridDelivery").datagrid('getRows');
            var DeliveryTotalAmount = 0;
            for (var i = 0; i < rows.length; i++) {
                
                if (rows[i].DeliveryNO != DeliveryNO) {
                    DeliveryTotalAmount = Number(DeliveryTotalAmount) + Number(rows[i].TotalPrice) + Number(rows[i].OtherFee) + Number(rows[i].AcceptanceTax);
                } else if (rows[i].DeliveryNO == DeliveryNO) {
                    DeliveryTotalAmount = Number(DeliveryTotalAmount) + Number(TotalPrice) + Number(OtherFee) + Number(AcceptanceTax);
                }
                
            }

            $("#dataFormMasterDeliveryTotalAmount").val(DeliveryTotalAmount);//DeliveryTotalAmount
        }

        function FormatScriptSurveyors(val, row, index) {
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            
            if (val != null && val != "" && val.indexOf(getClientInfo('_username')) < 0 && parameter == 'S8') {//採購主管驗收 !val.includes(',')
                return $('<a>', { href: 'javascript:void(0)', onclick: 'OnClickPaperButton(' + index + ')', style: '{ color: red }', color: 'red', fontcolor: 'red' }).linkbutton({ width: '200', text: '請點此驗收', color: 'red' })[0].outerHTML;
            }
            else {
                return val;
            }
        }
        //主管驗收
        function OnClickPaperButton(rowindex) {
            //取舊值
            $('#dataGridDelivery').datagrid('selectRow', rowindex);
            var row = $('#dataGridDelivery').datagrid('getSelected');
            var ori = row["Surveyors"];
            //若沒簽，設新值
            if (ori.indexOf(getClientInfo('_username')) < 0) {
                ori = ori + ',' + getClientInfo('_username');

                var dialoggrid = "#dataGridDelivery";
                $(dialoggrid).datagrid('beginEdit', rowindex);
                var editor = $(dialoggrid).datagrid('getEditor', { index: rowindex, field: 'Surveyors' });
                if (editor) {
                    editor.actions.setValue(editor.target, ori);//設值
                }
                $(dialoggrid).datagrid('endEdit', rowindex);
                //applyUpdates($(dialoggrid));
            }
        }


        //需求日-申請日 < 前置採購天數 ，則提示
        function dataFormDetailRegDate_OnBlur() {
            var itemID = $("#dataFormDetailItemID").refval('getValue');
            if (itemID != "") {
                //此物品的採購前置天數
                var leadTime = GetInfoCommandValue($("#dataFormDetailItemID"), "ItemID='" + itemID + "'", "LeadTime");
                var applyDate = new Date($("#dataFormMasterApplyDate").datebox('getValue'));
                var regDate = new Date($("#dataFormDetailRegDate").datebox('getValue'));
                var difference = Math.floor((Date.UTC(regDate.getFullYear(), regDate.getMonth(), regDate.getDate()) - Date.UTC(applyDate.getFullYear(), applyDate.getMonth(), applyDate.getDate())) / (1000 * 60 * 60 * 24));
                if (Number(difference) < Number(leadTime)) {//前置天數不夠處理
                    alert("請注意，採購前置作業天數為" + leadTime + "天");
                }
            } else {
                alert("請先選擇品名");
            }
        }

        function sysVariable_Default() {
            var returnValue="";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster',
                data: "mode=method&method=" + "sysVariable_Default", //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "0") {
                        returnValue= data;
                    }
                }
            });

            return returnValue;
        }

        function dataFormMasterAcSubno_OnSelect(row) {
            $("#dataFormMasterAcno").val(row.Acno);
            $("#dataFormMasterSubAcno").val(row.SubAcno);
        }
        //結帳方式OnSelect
        function DFM_POPayTypeID_OnSelect(row) {

            if (row.POPayTypeID == '3') {
                ShowFields('#dataFormMaster', ['Installments']);
                $("#installmentsBtn").show();//採購方式選到分期付款，再顯示
                //刪除dataGridDelivery非分期
                var rows0 = $("#dataGridDelivery").datagrid('getRows');
                for (var i = rows0.length - 1 ; i >= 0 ; i--) {
                    var index = $('#dataGridDelivery').datagrid('getRowIndex', rows0[i]);
                    $('#dataGridDelivery').datagrid('selectRow', index);
                    var rowSelected = $('#dataGridDelivery').datagrid('getSelected');
                    if (rowSelected.ItemID != "I99999") {//請採購項目是分期
                        $('#dataGridDelivery').datagrid('deleteRow', index);
                    }
                }
            } else {
                HideFields('#dataFormMaster', ['Installments']);
                $("#installmentsBtn").hide();
                //刪除dataGridDetail分期
                var rows = $("#dataGridDetail").datagrid('getRows');
                for (var i = rows.length - 1 ; i >= 0 ; i--) {
                    var index = $('#dataGridDetail').datagrid('getRowIndex', rows[i]);
                    $('#dataGridDetail').datagrid('selectRow', index);
                    var rowSelected = $('#dataGridDetail').datagrid('getSelected');
                    if (rowSelected.ItemID == "I99999") {//請採購項目是分期
                        $('#dataGridDetail').datagrid('deleteRow', index);
                    }
                }
                //刪除dataGridDelivery分期
                var rows0 = $("#dataGridDelivery").datagrid('getRows');
                for (var i = rows0.length - 1 ; i >= 0 ; i--) {
                    var index = $('#dataGridDelivery').datagrid('getRowIndex', rows0[i]);
                    $('#dataGridDelivery').datagrid('selectRow', index);
                    var rowSelected = $('#dataGridDelivery').datagrid('getSelected');
                    if (rowSelected.ItemID == "I99999") {//請採購項目是分期
                        $('#dataGridDelivery').datagrid('deleteRow', index);
                    }
                }
            }
        }

        function installmentsBtn_OnClick() {
            //先刪除dataGridDelivery
            var rows = $("#dataGridDelivery").datagrid('getRows');
            for (var i = rows.length - 1 ; i >= 0 ; i--) {
                var index = $('#dataGridDelivery').datagrid('getRowIndex', rows[i]);
                $('#dataGridDelivery').datagrid('deleteRow', index);
            }

            //新增到dataGridDelivery
            var installments=$("#dataFormMasterInstallments").val();
            var rows =$("#dataGridDetail").datagrid('getData').rows;
            var rowsLength=rows.length;

            if (installments > 0) {
                for (var i = 1; i <= installments; i++) {
                    var rowData = new Object();
                    rowData['PONO'] = rows[0].PONO;
                    rowData['ItemNO'] = ('000' + (Number(rowsLength) + Number(i))).slice(-3);
                    rowData['ItemID'] = 'I99999';
                    rowData['RegDate'] = rows[0].RegDate;
                    rowData['RegQty'] = '0';
                    rowData['PurPriceVen1'] = '0';
                    rowData['PurQty'] = '1';
                    rowData['FirstDeliveryQty'] = '1';
                    rowData['FirstDeliveryDate'] = rows[0].RegDate;
                    rowData['PurVendor'] = rows[0].PurVendor;
                    rowData['PurPrice'] = (rows[0].PurPrice * rows[0].PurQty) / installments;
                    rowData['PurTax'] = rows[0].PurTax / installments;
                    
                    $("#dataGridDetail").datagrid("appendRow", rowData);
                    //$("#dataGridDetail").datagrid('changeState', 'editing');
                }
            } else {
                alert("請填「分期期數」");
                return false;
            }
        }

        function InsGroupID_OnSelect(rowdata) {
            $("#dataFormMasterAccountantRoleID").val(rowdata.AccountantRoleID);
        }
        //沒用到(因觸發不了)
        function dataFormDetail_OnApplied() {
            //var dialoggrid = "#dataGridDetail";
            //var rows = $(dialoggrid).datagrid('getRows');
            //var length = rows.length;
            //alert(length);
            //alert(rows);
            //if(rows) {
            //    alert(rows[0].FirstDeliveryDate); //取出自動編號值
            //}

            //var purVendor = $("#dataFormMasterPurVendor1").refval('getValue');
            //for (var i = 0; i < length; i++) {
            //    $(dialoggrid).datagrid('beginEdit', i);
            //    var editor = $(dialoggrid).datagrid('getEditor', { index: i, field: 'TotalAmount' });
            //    if (editor) {
            //        editor.actions.setValue(editor.target, '123');//設值 (rows[i]['PurQty'] * row[i]['PurPrice']) + row[i]['PurTax']
            //    }
            //    $(dialoggrid).datagrid('endEdit', i);
            //}
        }

        function PayTo_OnSelect(rowdata) {
            $("#dataFormDeliveryDebtorDays").focus(); 
            $("#dataFormDeliveryDebtorDays").val(rowdata.PayTermName);//帳款天數
            $("#dataFormDeliveryDebtorDays").blur();

            $("#dataFormDeliveryAccountNO").focus(); 
            //匯款帳號
            if ($("#dataFormDeliveryPayWayID").combobox('getValue') == '1') {//現金
                $("#dataFormDeliveryAccountNO").val('');
            } else {
                $("#dataFormDeliveryAccountNO").val(rowdata.VendAccount);
            }
            $("#dataFormDeliveryAccountNO").blur();

            //設定PlanPayDate應付日(因帳款天數改變)
            var EndDay = 25;
            var DebtorDays = $("#dataFormDeliveryDebtorDays").val();
            var AcceptanceDate = $("#dataFormDeliveryAcceptanceDate").datebox('getValue');
            var PlanPayDate = GetPlanPayDate(EndDay, DebtorDays, AcceptanceDate);
            $("#dataFormDeliveryPlanPayDate").focus();
            $("#dataFormDeliveryPlanPayDate").datebox('setValue', PlanPayDate);
            $("#dataFormDeliveryPlanPayDate").blur();
        }
        //付款方式選現金，則帳號空白
        function PayWayID_OnSelect(rowdata) {
            if (rowdata.PayWayID == '1') {
                $("#dataFormDeliveryAccountNO").val('');
            }
        }
        function ProofTypeID_OnSelect(row) {
            if (row.ProofTypeName == "收據") {
                $("#dataFormDeliveryAcceptanceTax").val(0);
            }
        }

        function ItemID_OnSelect(rowdata) {
            if (rowdata.IsAsset == '1' || rowdata.IsAsset == 'true') {
                $("#dataFormDetailIsAsset").checkbox('setValue', '1')
            } else {
                $("#dataFormDetailIsAsset").checkbox('setValue', '0')
            }
        }

        //沒用到，因為dataFormDetail存檔後，返回dataFormMaster會消失(改用format)
        function IsAsset_FormatScript(val) {
                if (val == true || val =="1")
                    return "<input  type='checkbox' checked='true' onclick='return  false;'/>";
                else
                return "<input  type='checkbox' onclick='return false;'/>";
        }

        function DFD_PurPriceVen1_OnBlur() {
            var PurPriceVen1 = $("#dataFormDetailPurPriceVen1").val();
            
            $("#dataFormDetailPurPrice").focus();
            $("#dataFormDetailPurPrice").val(PurPriceVen1);
            $("#dataFormDetailPurPrice").blur();
        }

        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var result = [];
                var result1=[];
                var aVal = '';

                aVal = $('#CostCenterID_Query').combobox('getValue');
                if (aVal != '') {
                    result.push("POMaster.CostCenterID = '" + aVal + "'");
                }

                aVal = $("#ApplyDate_Query").datebox('getValue');
                if (aVal != '') {
                    result.push("ApplyDate >= '" + aVal + "'");
                }

                aVal = $("#D_STEP_ID_Query").combobox('getValue');
                if (aVal != '') {
                    result.push("t1.D_STEP_ID = '" + aVal + "'");
                }

                aVal = $("#InsGroupID_Query").combobox('getValue');
                if (aVal != '') {
                    result.push("InsGroupID = '" + aVal + "'");
                }

                //請購單號、申請者工號、請購說明
                aVal = $('#VirtualColumn_Query').val();
                if (aVal != '') {
                    result.push("(PONO like '%" + aVal + "%' or ApplyUserID like '%" + aVal + "%' or Description like '%" + aVal + "%' or CreateBy like '%"+aVal+"%' )");
                }

                //物品名稱
                aVal = $('#VirtualColumn1_Query').val();
                if (aVal != '') {
                    result1.push("i.ItemName like '%" + aVal + "%'");
                }

                //補單
                aVal = $('#IsAdd_Query').combobox('getValue');
                if (aVal != '') {
                    result.push("IsAdd = '" + aVal + "'");
                }

                var filtstr = result.join(' and ');
                var filtstr1 = result1.join(' and ');
                $(dg).datagrid('setWhere', filtstr + ';' + filtstr1);
            }
           
            //-------Rebecca Add-------//
            if ($(dg).attr('id') == 'dataGridDetail0') {
                //查詢條件
                var result = [];
                if (sPONO != '') result.push("g.PONO = '" + sPONO + "'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }

        }
//--------------工具--------------------------------------------
        function GetUserOrgNOs(UserID) {
            //var UserID = getClientInfo("UserID");
            var myArr = [];
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        myArr[0] = rows[0].OrgNO;
                        myArr[1] = rows[0].OrgNOParent;
                    }
                }
            }
            );
            return myArr;
        }
        function GetOrgNO_CostCenterID(OrgNO) {
            var returnValue;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster', //連接的Server端，command
                data: "mode=method&method=" + "GetOrgNO_CostCenterID" + "&parameters=" + OrgNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        returnValue = rows[0].CostCenterID;
                    }
                }
            }
            );
            return returnValue;
        }
        function HideFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
            });
        }
        function ShowFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames, DisabledRefvalNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
            $.each(DisabledRefvalNames, function (index, value) {
                $(FormName + value).refval('disable');
            });
        }
        function EnableFields(FormName, EnabledFieldNames, EnabledComboboxNames, EnabledRefvalNames) {
            $.each(EnabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', false);
            });
            $.each(EnabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('enable');
            });
            $.each(EnabledRefvalNames, function (index, value) {
                $(FormName + value).refval('enable');
            });
        }
        function RedFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' })
            });
        }
        function BlackFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'black' })
            });
        }
        function ClearPurDocVen1() {
            var infofileUpload = $('#dataFormMasterPurDocVen1');
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
            infofileUploadvalue.val('');
        }
        function ClearPurDocVen2() {
            var infofileUpload = $('#dataFormMasterPurDocVen2');
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
            infofileUploadvalue.val('');
        }
        function ClearPurDocVen3() {
            var infofileUpload = $('#dataFormMasterPurDocVen3');
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
            infofileUploadvalue.val('');
        }
        //今天日期，格式yyyy/mm/dd
        function getToday() {
            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();
            var todayFormat = yyyy + '/' + mm + '/' + dd;
            return todayFormat;
        }
        //今天前後幾天，格式yyyy/mm/dd
        function getShiftDay(shiftDay) {
            var startDate = new Date();
            var numberOfDaysToAdd = shiftDay;
            startDate.setDate(startDate.getDate() + numberOfDaysToAdd);
            var dd1 = String(startDate.getDate()).padStart(2, '0');
            var mm1 = String(startDate.getMonth() + 1).padStart(2, '0');
            var yyyy1 = startDate.getFullYear();
            var startDateFormat = yyyy1 + '/' + mm1 + '/' + dd1;
            return startDateFormat;
        }

        //取ItemID稅額
        function GetTax(itemID, price, qty, otherFee) {
            var tax = 0;
            if (price != '' && qty != '') {
                var taxRate = GetInfoCommandValue($("#dataFormDetailItemID"), "ItemID='" + itemID + "'","TaxRate");
                tax = Math.round(((Number(price) * Number(qty)) * Number(taxRate)) + (Number(otherFee) * Number(0.05)));//四捨五入
            }
            return tax;
        }

        //datagrid 隱藏欄位
        function HideGridColumns(GridName, ColumnNames) {
            $.each(ColumnNames, function (index, value) {
                $(GridName).datagrid('hideColumn',value);//closest上一層selector,prev下一個selector
            });
        }

        function ShowGridColumns(GridName, ColumnNames) {
            $.each(ColumnNames, function (index, value) {
                $(GridName).datagrid('showColumn', value);//closest上一層selector,prev下一個selector
            });
        }

        function GetPlanPayDate(EndDay, DebtorDays, AcceptanceDate) {
            var returnValue;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPO_Normal_PRPOIQC.POMaster', //連接的Server端，command
                data: "mode=method&method=" + "GetPlanPayDate" + "&parameters=" + EndDay + "," + DebtorDays + "," + AcceptanceDate, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        returnValue = rows[0].Column1.substring(0, rows[0].Column1.indexOf('T'));
                    }
                }
            }
            );
            return returnValue;
        }

        //安排交貨數量是否完成
        function IsDeliveryEnough() {
            var data = $("#dataGridDetail").datagrid("getData");//請採購明細
            var data1 = $("#dataGridDelivery").datagrid("getData");//交貨明細
            var flagDeliveryEnough = true;

            $(data.rows).each(function () {//請採購明細
                var ItemNO = this.ItemNO;
                var PurQty = this.PurQty;
                var SumDeliveryQty = 0;
                $(data1.rows).each(function () {//交貨明細
                    if (ItemNO == this.ItemNO) {
                        SumDeliveryQty = Number(SumDeliveryQty) + Number(this.DeliveryQty);
                    }
                });
                if (Number(PurQty) == Number(SumDeliveryQty)) {//採購數量==交貨安排數量，則交貨數量安排完成
                    flagDeliveryEnough = (flagDeliveryEnough && true);
                } else {
                    flagDeliveryEnough = (flagDeliveryEnough && false);
                }
            });

            return flagDeliveryEnough;
        }
        
        //-------Rebecca Add-------//
        //=========================================開啟傳票設定內容====================================================================================      
        function OpeneglVoucher() {
            //openForm('#JQDialogVoucherOpen', {}, 'inserted', 'dialog');

            openForm('#JQDialogVoucherOpen', $('#dataGridDetail0').datagrid('getSelected'), "updated", 'dialog');
            setTimeout(function () {
                SetglVoucher();
                queryGrid($('#dataGridDetail0'));
            }, 500);

        }

        //由請款單傳回目前的公司別、傳票內容資訊
        function SetglVoucher() {
            //sPONO = 'PAY1081512';

            //var sPONO = 'PAY1081516';//$("#dataFormMasterPONO").val();

            sPONO = $("#dataFormMasterPONO").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.POMasterVoucherM', //連接的Server端，command
                data: "mode=method&method=" + "getPOMasterVoucher" + "&parameters=" + sPONO,
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
                        $("#dataFormVoucherPONO").val(sPONO);
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
        //----------------求得目前是否有設定傳票內容--------------------
        function getglVoucher() {
            //sPONO = 'PAY1081512';
            //var sPONO = 'PAY1081516';//$("#dataFormMasterRequisitionNO").val();

            sPONO = $("#dataFormMasterPONO").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.POMasterVoucherM', //連接的Server端，command
                data: "mode=method&method=" + "getPOMasterVoucher" + "&parameters=" + sPONO,
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
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherSet.POMasterVoucherM', //連接的Server端，command
                data: "mode=method&method=" + "InsertPOMasterVoucher" + "&parameters=" + sPONO + "," + VoucherMVoucherDate, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    $('#dataGridDetail0').datagrid('reload');
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
           
        }

        //呼叫科目明細Tooltip
        function OpenSubAcnoToolTip() {
            openForm('#JQDialogToolTip', {}, 'viewed', 'dialog');

            var result = [];
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            if (CompanyID.trim() != '') result.push("CompanyID = " + CompanyID);

            var Acno = $("#dataFormDetail0Acno").combobox('getValue');

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
                $("#dataFormDetail0Acno").combobox('loadData', CodeList);//Detail
            }
        }
        function RunGetSubAcno() {
            //若DataFormDetails不為viewed狀態,則重跑
            if (getEditMode($("#dataFormDetail0")) != 'viewed') {
                var Acno = $("#dataFormDetail0Acno").combobox('getValue');
                GetSubAcno(Acno, "");
            }
        }
        //得到明細資料
        var GetSubAcno = function (Acno, SubAcno) {
            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetSubAcno', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (CodeList != null) $("#dataFormDetail0SubAcno").combobox('loadData', CodeList);
        }
        //========================================= 科目 1.明細 2.摘碼代號 連動Combobox ====================================================================================   

        //---------------------------------------選取科目觸發---------------------------------
        var Acno_OnSelect = function (rowdata) {
            //$("#dataFormDetail0Describe").val("");
            //ClearAcnoCombo();明細 清空
            //1.明細
            GetSubAcno(rowdata.value, "");
            //2.摘碼代號
            GetDescribeID(rowdata.value, "");
        }

        function ClearAcnoCombo() {
            //1.明細 清空
            $("#dataFormDetail0SubAcno").combobox('loadData', []).combobox('clear');
            //2.摘碼代號 清空
            $("#dataFormDetail0DescribeID").combobox('loadData', []).combobox('clear');
        }

        //得到摘碼代號
        var GetDescribeID = function (Acno, DescribeID) {
            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetDescribeID', { Company_ID: CompanyID, Ac_no: Acno, Describe_ID: DescribeID });
            if (CodeList != null) $("#dataFormDetail0DescribeID").combobox('loadData', CodeList);
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
                $('#dataFormDetail0').hide();
            } else $('#dataFormDetail0').show();

            if (getEditMode($("#dataFormVoucher")) == 'inserted') {

                //傳票日期disable属性删除
                $("#dataFormVoucherVoucherDate").combo('textbox').removeAttr("disabled");
                $("#dataFormVoucherVoucherDate").datebox().removeAttr("disabled");

                //預設傳票日期
                var dt = new Date();
                var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                $("#dataFormVoucherVoucherDate").datebox('setValue', today);

                //設定傳回目前的公司別、傳票類別   
                $("#dataFormVoucherPONO").val(sPONO);
                $("#dataFormVoucherCompanyID").combobox('setValue', sCompanyID);
                $("#dataFormVoucherVoucherID").options('setValue', sVoucherID);

                //Detail dataform
                GetAcno("");
                GetSubAcno("0", "");//新增時預設

                //分錄科目預設清空
                $("#dataFormVoucherOftenUsedAcno").combobox('setValue', "");

            }
           
            ControlGrid();//控制Grid key in 顯示方式的顯示或隱藏
            

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
                        $('#dataFormDetail0Describe').val(rows[0].Describe);
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
                        $('#dataFormDetail0SubAcnoText').val(rows[0].AcnoName);
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
            if (getEditMode($("#dataFormDetail0")) == 'updated') {
                GetAcno(rowdata.Acno);
                GetSubAcno(rowdata.Acno, rowdata.SubAcno);
                GetDescribeID(rowdata.Acno, rowdata.DescribeID);
            }
            if (getEditMode($("#dataFormDetail0")) == 'inserted') {
                $("#dataFormDetail0BorrowLendType").combo('textbox').focus();//焦點
                $("#dataFormDetail0BorrowLendType").combobox('setValue', "");
            }
            //================================== combo blur 事件 ====================================       

            //combo blur 事件  =>   科目
            $("#dataFormDetail0Acno").combo('textbox').blur(function () {
                var DetailAcno = $("#dataFormDetail0Acno").combobox('getValue');//科目
                //1.得到明細
                //GetSubAcno(DetailAcno, "");
                $("#dataFormDetail0SubAcno").combobox('setValue', "");//明細清空
                //2.摘碼代號
                GetDescribeID(DetailAcno, "");
            });



            //combo blur 事件  =>   明細
            $("#dataFormDetail0SubAcno").combo('textbox').blur(function () {
                //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用      
                var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetail0Acno").combobox('getValue');//科目
                var SubAcno = $("#dataFormDetail0SubAcno").combobox('getValue');//明細
                //得到內容
                GetAcnoNameText(CompanyID, DetailAcno, SubAcno);

            });


            //combo blur 事件  =>   摘碼代號
            $("#dataFormDetail0DescribeID").combo('textbox').blur(function () {

                var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetail0Acno").combobox('getValue');//科目
                var DescribeID = $("#dataFormDetail0DescribeID").combobox('getValue');//摘碼代號

                //得到內容
                GetDescribeText(CompanyID, DetailAcno, DescribeID);
            });
        }

        //將摘碼代號所選帶入摘碼內容
        function OnSelectDescribeID(rowData) {
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');//公司別
            var DetailAcno = $("#dataFormDetail0Acno").combobox('getValue');//科目
            var DescribeID = $("#dataFormDetail0DescribeID").combobox('getValue');//摘碼代號
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
            var SubAcno = $("#dataFormDetail0SubAcno").combobox('getText');
            if (SubAcno == "" || SubAcno == "---請選擇---") {
                alert('請選擇明細!');
                return false;
            } else return true;//通過
        }
        function OnSelectSubAcno(rowData) {
            //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用            
            $("#dataFormDetail0SubAcnoText").val(rowData.text);
        }

        //DataFormDetails存檔前檢查
        function OnApplyDFDetail() {

            //公司別
            var CompanyID = $("#dataFormVoucherCompanyID").combobox('getValue');
            $("#dataFormDetail0CompanyID").val(CompanyID);
            //傳票類別	
            var VoucherID = $("#dataFormVoucherVoucherID").options('getCheckedValue');
            $("#dataFormDetail0VoucherID").val(VoucherID);

            var Acno = $("#dataFormDetail0Acno").combobox('getValue');
            var SubAcno = $("#dataFormDetail0SubAcno").combobox('getValue');
            var BorrowLendType = $("#dataFormDetail0BorrowLendType").combobox('getValue');
            var Describe = $("#dataFormDetail0Describe").val();
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
            var CostCenterID = $("#dataFormDetail0CostCenterID").combobox('getValue');
            if (bCostCenterID == "True" && CostCenterID == "") {
                alert('此科目需成本中心-請選擇成本中心!');
                return false;
            }

        }
      
        //========================================= 存檔前檢查 ====================================================================================              
        //存檔前檢查 OnSubmited
        function OnApplyDFMaster() {

            var GridName = '#dataGridDetail0';

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
            var GridName = '#dataGridDetail0';

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
            $("#dataFormDetail0SubAcno").combobox('setValue', SubAcno);
            closeForm('#JQDialogToolTip');
        }
        function JQDialog6OnSubmited() {
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            $("#dataFormMasterItemTypeID").combobox('setValue', RowData.ItemTypeID);
            $("#dataFormMasterResponsibleGROUPID").combobox('setValue', RowData.ResponsibleGROUPID);
            //setWhere會計科目和選第一個(依 成本中心、物品類別)
            dataFormMasterCostCenterID_OnSelect();
            $("#dataFormMasterRecVendor").refval("setWhere", "ItemTypeID='" + RowData.ItemTypeID + "'");
           
            return true;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPO_Normal_PRPOIQC.POMaster" runat="server" AutoApply="True"
                DataMember="POMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" DeleteCommandVisible="False" UpdateCommandVisible="False" DuplicateCheck="False" MultiSelect="True" CheckOnSelect="True" QueryMode="Panel" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" ColumnsHibeable="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" ViewCommandVisible="True" OnLoadSuccess="dataGridView_OnLoad">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="請購單編號" Editor="text" FieldName="PONO" Format="" MaxLength="20" Visible="true" Width="80" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="text" FieldName="ApplyDate" Format="yyyy-mm-dd" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyUserID" Format="" MaxLength="20" Visible="true" Width="50" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sPO_Normal_PRPOIQC.UsersAll',tableName:'UsersAll',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程作業" Editor="text" FieldName="D_STEP_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="經辦者" Editor="text" FieldName="SENDTO_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="Flowflag" Format="" MaxLength="10" Visible="True" Width="60" EditorOptions="items:[{value:'',text:'未起單',selected:'false'},{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'結案',selected:'false'},{value:'X',text:'作廢',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請購說明" Editor="text" FieldName="Description" Format="" MaxLength="256" Visible="true" Width="280" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Format="" Visible="true" Width="80" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sPO_Normal_PRPOIQC.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="請購類別" Editor="infocombobox" FieldName="POTypeID" Frozen="False" IsNvarChar="False" MaxLength="10" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" EditorOptions="valueField:'POTypeID',textField:'POTypeName',remoteName:'sPO_Normal_PRPOIQC.POType',tableName:'POType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="請購性質" Editor="infocombobox" FieldName="RequisitKindID" Format="" MaxLength="0" Visible="True" Width="60" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sPO_Normal_PRPOIQC.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="text" FieldName="CostCenterName" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" FieldName="CostCenterID" Format="" MaxLength="20" Visible="False" Width="120" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPO_Normal_PRPOIQC.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" MaxLength="20" Visible="true" Width="80" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPO_Normal_PRPOIQC.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="20" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="物品類別" Editor="infocombobox" FieldName="ItemTypeID" Format="" Visible="true" Width="120" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sPO_Normal_PRPOIQC.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="採購總金額" Editor="numberbox" FieldName="PurTotalAmount1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="50" MaxLength="20" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy-mm-dd" MaxLength="0" Visible="False" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="120" MaxLength="20" />
                    <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ResponsibleGROUPID" Editor="text" FieldName="ResponsibleGROUPID" Width="120" MaxLength="0" Visible="False" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="採購說明" Editor="text" FieldName="PurComment" Frozen="False" IsNvarChar="False" MaxLength="512" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="DGV_UpdateBtnClick" Text="修改" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="DGV_DeleteBtnClick" Text="刪除" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportXlsx" Text="匯出xlsx" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-next" ItemType="easyui-linkbutton" Text="起單(送出審核)" Visible="True" OnClick="FlowStartUpByHand" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="成本中心" Condition="=" DataType="string" Editor="infocombobox" FieldName="CostCenterID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="135" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPO_Normal_PRPOIQC.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" TableName="POMaster" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請日期起始" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="ApplyDate" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="95" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程作業名稱" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'請購者申請',text:'請購者申請',selected:'false'},{value:'資料與備品確認',text:'資料與備品確認',selected:'false'},{value:'請購者主管審核',text:'請購者主管審核',selected:'false'},{value:'請購者主管審核_',text:'請購者主管審核_',selected:'false'},{value:'採購作業',text:'採購作業',selected:'false'},{value:'採購主管審核',text:'採購主管審核',selected:'false'},{value:'採購主管審核_',text:'採購主管審核_',selected:'false'},{value:'請購者交期安排',text:'請購者交期安排',selected:'false'},{value:'請購者驗收',text:'請購者驗收',selected:'false'},{value:'請購者主管驗收',text:'請購者主管驗收',selected:'false'},{value:'採購結帳',text:'採購結帳',selected:'false'},{value:'會計審核',text:'會計審核',selected:'false'},{value:'結案通知',text:'結案通知',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="D_STEP_ID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="134" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sPO_Normal_PRPOIQC.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="請購單搜尋" Condition="%" DataType="string" Editor="text" FieldName="VirtualColumn" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="298" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="物品名稱" Condition="%" DataType="string" Editor="text" FieldName="VirtualColumn1" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="補單" Condition="=" DataType="number" Editor="infocombobox" FieldName="IsAdd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="125" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'1',text:'有',selected:'false'},{value:'0',text:'無',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title=" " Width="1140px" DialogTop="10px" DialogLeft="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="POMaster" HorizontalColumnsCount="3" RemoteName="sPO_Normal_PRPOIQC.POMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" OnLoadSuccess="dataFormMaster_OnLoadSuccess" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMaster_OnApply" OnApplied="DFM_OnApplied" OnCancel="dataFormMaster_OnCancel" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="作業名稱" Editor="text" FieldName="D_STEP_ID" maxlength="0" NewRow="True" ReadOnly="True" Width="180" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購單編號" Editor="text" FieldName="PONO" Format="" maxlength="20" Width="180" ReadOnly="True" NewRow="True" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="180" ReadOnly="True" maxlength="0" NewRow="False" Visible="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyUserID" Format="" maxlength="20" Width="180" ReadOnly="True" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sPO_Normal_PRPOIQC.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="InsGroupID" Width="180" NewRow="True" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sPO_Normal_PRPOIQC.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:InsGroupID_OnSelect,panelHeight:200" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購類別" Editor="infocombobox" FieldName="POTypeID" Format="" Width="180" EditorOptions="valueField:'POTypeID',textField:'POTypeName',remoteName:'sPO_Normal_PRPOIQC.POType',tableName:'POType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="False" Span="1" ReadOnly="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購性質" Editor="infocombobox" FieldName="RequisitKindID" Format="" Width="180" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sPO_Normal_PRPOIQC.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" NewRow="False" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購說明" Editor="textarea" FieldName="Description" Format="" NewRow="True" Span="3" Width="653" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預算年度" Editor="infocombobox" EditorOptions="valueField:'VoucherYear',textField:'VoucherYear',remoteName:'sPO_Normal_PRPOIQC.VoucherYear',tableName:'VoucherYear',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterCostCenterID_OnSelect,panelHeight:200" FieldName="VoucherYear" NewRow="False" Width="180" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sPO_Normal_PRPOIQC.glCostCenter',tableName:'glCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:dataFormMasterCostCenterID_OnSelect,panelHeight:200" FieldName="CostCenterID" Format="" Width="180" NewRow="False" Span="1" ReadOnly="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="物品類別" Editor="infocombobox" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sPO_Normal_PRPOIQC.ItemType',tableName:'ItemType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:DFM_ItemTypeID_OnSelect,panelHeight:200" FieldName="ItemTypeID" Format="" NewRow="False" Span="1" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="會計科目" Editor="infocombobox" EditorOptions="valueField:'AcSubno',textField:'AcnoName',remoteName:'sPO_Normal_PRPOIQC.ItemTypeAcno',tableName:'ItemTypeAcno',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:dataFormMasterAcSubno_OnSelect,panelHeight:200" FieldName="AcSubno" NewRow="False" OnBlur="dataFormMasterAcSubno_OnSelect" Span="1" Width="180" PlaceHolder="" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建議廠商" Editor="inforefval" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'VendID',textFieldCaption:'VendName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="RecVendor" Format="" NewRow="False" Width="180" MaxLength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="樣本照片" Editor="infofileupload" FieldName="PrPic" Width="160" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PrPic',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" NewRow="True" ReadOnly="False" MaxLength="0" Visible="True" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="估價單" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PrDoc',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" FieldName="PrDoc" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sPO_Normal_PRPOIQC.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="425" />
                        <JQTools:JQFormColumn Alignment="left" Caption="補單" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAdd" NewRow="False" Span="1" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價廠商1" Editor="inforefval" FieldName="PurVendor1" Format="" Width="180" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'廠商ID',textFieldCaption:'廠商名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" NewRow="True" Span="1" MaxLength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價檔1" Editor="infofileupload" FieldName="PurDocVen1" Format="" Width="120" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen1',showButton:true,showLocalFile:false,onSuccess:DFDPurDocVen1_OnSuccess,fileSizeLimited:'20000'" Span="2" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價廠商2" Editor="inforefval" FieldName="PurVendor2" Format="" NewRow="True" Span="1" Width="180" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'廠商ID',textFieldCaption:'廠商名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" maxlength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價檔2" Editor="infofileupload" FieldName="PurDocVen2" Format="" Width="120" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen2',showButton:true,showLocalFile:false,onSuccess:DFDPurDocVen2_OnSuccess,fileSizeLimited:'20000'" NewRow="False" Span="2" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價廠商3" Editor="inforefval" FieldName="PurVendor3" Format="" Width="180" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'廠商ID',textFieldCaption:'廠商名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Span="1" NewRow="True" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報價檔3" Editor="infofileupload" FieldName="PurDocVen3" Format="" Span="2" Width="120" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen3',showButton:true,showLocalFile:false,onSuccess:DFDPurDocVen3_OnSuccess,fileSizeLimited:'20000'" NewRow="False" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購說明暨驗收說明" Editor="textarea" FieldName="PurComment" Format="" Width="658" EditorOptions="height:40" Span="3" NewRow="True" maxlength="0" ReadOnly="False" RowSpan="1" Visible="True" />    
                        <JQTools:JQFormColumn Alignment="left" Caption="採購說明檔" Editor="infofileupload" FieldName="PurCommentFile" Width="658" maxlength="0" ReadOnly="False" NewRow="False" Span="3" RowSpan="1" Visible="True" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/PurCommentFile',showButton:true,showLocalFile:false,onSuccess:dataFormMasterPurCommentFile_OnSuccess,fileSizeLimited:'20000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程金額" Editor="numberbox" FieldName="OtherFee" MaxLength="0" ReadOnly="False" Width="180" NewRow="False" Span="1" RowSpan="1" Visible="False" Format="" OnBlur="SetdataFormMasterPurTotalAmount" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程稅額" Editor="numberbox" FieldName="OtherFeeTax" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" OnBlur="SetdataFormMasterPurTotalAmount" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結帳方式" Editor="infocombobox" FieldName="POPayTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'POPayTypeID',textField:'POPayTypeName',remoteName:'sPO_Normal_PRPOIQC.POPayType',tableName:'POPayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DFM_POPayTypeID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分期期數" Editor="text" FieldName="Installments" maxlength="0" ReadOnly="False" Width="146" NewRow="False" Span="1" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程說明" Editor="textarea" FieldName="OtherComment" maxlength="0" ReadOnly="False" Visible="True" Width="658" NewRow="False" RowSpan="1" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購總金額" Editor="text" FieldName="PurTotalAmount" Width="180" ReadOnly="True" maxlength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" Format="N0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交貨總金額" Editor="numberbox" FieldName="DeliveryTotalAmount" MaxLength="0" NewRow="False" ReadOnly="True" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" FieldName="RequestNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="658" />
                        <JQTools:JQFormColumn Alignment="left" Caption="財產目錄" Editor="checkbox" FieldName="IsCatalogue" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
						<JQTools:JQFormColumn Alignment="left" Caption="解除傳票設定" Editor="checkbox" FieldName="unlock" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />																																																		
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" MaxLength="20" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" Format="" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPO_Normal_PRPOIQC.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上一層部門" Editor="text" FieldName="Org_NOParent" MaxLength="20" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購職稱" Editor="infocombobox" FieldName="ResponsibleGROUPID" maxlength="0" NewRow="False" ReadOnly="False" Span="1" Width="180" RowSpan="1" Visible="True" EditorOptions="valueField:'ResponsibleGROUPID',textField:'GROUPNAME',remoteName:'sPO_Normal_PRPOIQC.ResponsibleGROUPID',tableName:'ResponsibleGROUPID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="安排交貨數量完成" Editor="checkbox" FieldName="FlagDeliveryEnough" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="sysVariable" Editor="text" FieldName="sysVariable" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Acno" Editor="text" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AccountantRoleID" Editor="text" FieldName="AccountantRoleID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購者隸屬部門" Editor="text" FieldName="Org_NOParent1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PODetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sPO_Normal_PRPOIQC.POMaster" Title="請採購明細" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="dataGridDetail_OnInsert" OnDeleted="dataGridDetail_OnDeleted" OnDelete="DGDetail_OnDelete" OnLoadSuccess="dataGridDetail_OnLoad" OnUpdate="DGDetail_OnUpdate" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="請購單編號" Editor="text" FieldName="PONO" Width="75" MaxLength="20" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" Width="30" MaxLength="10" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="infocombobox" FieldName="ItemID" Width="100" MaxLength="10" EditorOptions="valueField:'ItemID',textField:'ItemName',remoteName:'sPO_Normal_PRPOIQC.Item',tableName:'Item',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="物品規格" Editor="text" FieldName="ItemSpec" Width="100" MaxLength="256" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="資產" Editor="text" FieldName="IsAsset" MaxLength="0" Visible="True" Width="35" EditorOptions="" Format="L,V," FormatScript="" />
                        <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="Unit" Width="30" MaxLength="4" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請購量" Editor="text" FieldName="RegQty" MaxLength="4" Width="45" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請購單價" Editor="text" FieldName="RegPrice" Width="60" MaxLength="4" Visible="True" Format="N1" EditorOptions="" />
                        <JQTools:JQGridColumn Alignment="right" Caption="請購金額" Editor="text" FieldName="RegTotalAmount" Width="60" MaxLength="0" Visible="True" Format="N1" EditorOptions="" />
                        <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="text" FieldName="RegDate" Width="60" MaxLength="8" Visible="True" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="採購量" Editor="text" FieldName="PurQty" MaxLength="4" Visible="True" Width="45" Format="N0" />
                        <JQTools:JQGridColumn Alignment="right" Caption="採購單價" Editor="text" FieldName="PurPrice" Width="60" MaxLength="4" Visible="True" Format="N1" EditorOptions="" />
                        <JQTools:JQGridColumn Alignment="right" Caption="採購稅額" Editor="text" FieldName="PurTax" Width="50" MaxLength="4" Visible="True" Format="N0" />
                        <JQTools:JQGridColumn Alignment="right" Caption="採購金額" Editor="text" FieldName="TotalAmount" Width="55" MaxLength="0" Visible="True" Format="N0" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="採購廠商" Editor="infocombobox" FieldName="PurVendor" Frozen="False" IsNvarChar="False" MaxLength="10" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" EditorOptions="valueField:'VendID',textField:'VendName',remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="採購廠商1報價" Editor="text" FieldName="PurPriceVen1" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="50" EditorOptions="" Format="N1">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="採購廠商2報價" Editor="text" FieldName="PurPriceVen2" MaxLength="4" Visible="False" Width="60" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" EditorOptions="" Format="N1">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="採購廠商3報價" Editor="text" FieldName="PurPriceVen3" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60" EditorOptions="" Format="N1">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" MaxLength="20" Visible="False" Width="60" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" MaxLength="8" Visible="False" Width="60" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Format="yyyy-mm-dd HH:MM:SS">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" MaxLength="20" Visible="False" Width="60" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" MaxLength="8" Visible="False" Width="60" Format="yyyy-mm-dd HH:MM:SS">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemTypeIDTemp" Editor="text" FieldName="ItemTypeIDTemp" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="FirstDeliveryDate" Editor="text" FieldName="FirstDeliveryDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="FirstDeliveryQty" Editor="text" FieldName="FirstDeliveryQty" MaxLength="0" Visible="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="交貨量" Editor="text" FieldName="sumDeliveryQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" Format="N0">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="驗收量" Editor="text" FieldName="sumAcceptanceQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="40" Format="N0">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="帳款天數" Editor="text" FieldName="PayTermName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="VendAccount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PurVendor10" Editor="text" FieldName="PurVendor10" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PurVendor20" Editor="text" FieldName="PurVendor20" MaxLength="0" Visible="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PurVendor30" Editor="text" FieldName="PurVendor30" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" ID="dataGridDetailInsertBtn" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="請採購明細" Width="980px" DialogLeft="47px" DialogTop="145px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="PODetails" HorizontalColumnsCount="3" RemoteName="sPO_Normal_PRPOIQC.POMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormDetail_OnApply" OnLoadSuccess="dataFormDetail_OnLoadSuccess" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="請購單編號" Editor="text" FieldName="PONO" Format="" Width="120" ReadOnly="True" Visible="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="50" ReadOnly="True" Span="2" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="inforefval" FieldName="ItemID" Format="" Width="325" EditorOptions="title:'物品',panelWidth:510,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Item',tableName:'Item',columns:[{field:'ItemID',title:'物品編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ItemName',title:'品名',width:220,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'TaxRate',title:'稅率',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Unit',title:'單位',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'Unit',value:'Unit'}],whereItems:[],valueField:'ItemID',textField:'ItemName',valueFieldCaption:'ItemID',textFieldCaption:'ItemName',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,onSelect:ItemID_OnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" Span="2" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="center" Caption="資產" Editor="checkbox" FieldName="IsAsset" Width="35" NewRow="False" Span="1" MaxLength="0" ReadOnly="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="物品規格" Editor="textarea" FieldName="ItemSpec" Format="" Width="540" NewRow="True" Span="3" EditorOptions="height:70" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請購數量" Editor="numberbox" FieldName="RegQty" Format="" Width="120" OnBlur="dataFormDetailPR_OnBlur" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請購單價" Editor="numberbox" FieldName="RegPrice" Format="" Width="120" OnBlur="dataFormDetailPR_OnBlur" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請購單位" Editor="text" FieldName="Unit" Format="" Width="105" NewRow="False" />
                            
                            <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RegDate" NewRow="True" Width="125" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" Format="" OnBlur="dataFormDetailRegDate_OnBlur" />
                            
                            <JQTools:JQFormColumn Alignment="left" Caption="請購金額" Editor="numberbox" FieldName="RegTotalAmount" Width="120" ReadOnly="True" Visible="True" EditorOptions="precision:1" Format="N1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價廠商1" Editor="text" FieldName="PurVendor10" NewRow="True" Width="120" MaxLength="0" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價廠商2" Editor="text" FieldName="PurVendor20" NewRow="False" Width="120" MaxLength="0" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            
                            <JQTools:JQFormColumn Alignment="left" Caption="報價廠商3" Editor="text" FieldName="PurVendor30" Width="105" NewRow="False" MaxLength="0" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價單價1" Editor="numberbox" FieldName="PurPriceVen1" Format="" Width="120" NewRow="True" OnBlur="DFD_PurPriceVen1_OnBlur" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價單價2" Editor="numberbox" FieldName="PurPriceVen2" Format="" Width="120" NewRow="False" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價單價3" Editor="numberbox" FieldName="PurPriceVen3" Format="" Width="105" NewRow="False" EditorOptions="precision:1" />
                            
                            <JQTools:JQFormColumn Alignment="left" Caption="採購數量" Editor="numberbox" FieldName="PurQty" Format="" Width="120" OnBlur="dataFormDetail_OnBlur" NewRow="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="採購單價" Editor="numberbox" FieldName="PurPrice" Format="" Width="120" NewRow="False" EditorOptions="precision:1" OnBlur="dataFormDetail_OnBlur" />
                            <JQTools:JQFormColumn Alignment="left" Caption="採購稅額" Editor="numberbox" FieldName="PurTax" Format="" Width="120" NewRow="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="採購廠商" Editor="inforefval" FieldName="PurVendor" Format="" Width="330" Visible="True" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'廠商ID',textFieldCaption:'廠商名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Span="2" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Visible="False" Width="120" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Visible="False" Width="120" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemTypeIDTemp" Editor="text" FieldName="ItemTypeIDTemp" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="交貨日期" Editor="datebox" FieldName="FirstDeliveryDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="交貨數量" Editor="text" FieldName="FirstDeliveryQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="autoSeqDetail" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemNO" />
                    
                </JQTools:JQDialog>
                <div id="dataGridDeliveryDiv">
                <JQTools:JQDataGrid ID="dataGridDelivery" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="PODelivery" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog3" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPO_Normal_PRPOIQC.POMaster" RowNumbers="True" Title="交貨明細(新增前，請先點選請採購明細任一筆)" TotalCaption="合計" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="dataGridDelivery_OnInsert" OnDelete="DGDelivery_OnDelete" OnUpdate="DGDelivery_OnUpdate" OnLoadSuccess="dataGridDelivery_OnLoad">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="4" Width="90" Visible="False" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="請購單編號" Editor="text" FieldName="PONO" MaxLength="20" Width="75" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="請項" Editor="text" FieldName="ItemNO" MaxLength="10" Width="40" />
                        <JQTools:JQGridColumn Alignment="center" Caption="交項" Editor="text" FieldName="DeliveryNO" MaxLength="10" Width="40" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="infocombobox" FieldName="ItemID" MaxLength="0" Width="120" EditorOptions="valueField:'ItemID',textField:'ItemName',remoteName:'sPO_Normal_PRPOIQC.Item',tableName:'Item',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="交貨日期" Editor="text" FieldName="DeliveryDate" MaxLength="8" Width="60" Format="yyyy/mm/dd" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="交貨數" Editor="text" FieldName="DeliveryQty" MaxLength="4" Width="40" />
                        <JQTools:JQGridColumn Alignment="right" Caption="驗收數" Editor="numberbox" FieldName="AcceptanceQty" MaxLength="0" Width="40" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="退貨數" Editor="text" FieldName="ReturnQty" MaxLength="0" Width="40" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="驗收照片" Editor="text" FieldName="AcceptancePic" MaxLength="0" Width="50" Visible="True" Format="download,folder:JB_ADMIN/PO_Normal_PRPOIQC/AcceptancePic" />
                        <JQTools:JQGridColumn Alignment="left" Caption="驗收人員" Editor="text" FieldName="Surveyors" MaxLength="20" Width="90" Visible="True" FormatScript="FormatScriptSurveyors" />
                        <JQTools:JQGridColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="InvoiceNO" MaxLength="20" Width="80" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="物品單價" Editor="text" FieldName="PurPrice" MaxLength="4" Width="60" Visible="True" Format="N1" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="物品總價" Editor="text" FieldName="TotalPrice" MaxLength="4" Width="60" Visible="True" Format="N1" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="工程金額" Editor="text" FieldName="OtherFee" MaxLength="4" Width="60" Visible="True" Format="N0" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="text" FieldName="AcceptanceTax" MaxLength="0" Width="40" Visible="True" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" Format="N0" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="總價" Editor="text" FieldName="TotalAmount" MaxLength="0" Width="60" Visible="True" Total="sum" OnTotal="" Format="N0" />
                        <JQTools:JQGridColumn Alignment="left" Caption="付款方式" Editor="text" FieldName="PayWayID" MaxLength="10" Width="50" Visible="False" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="帳款天數" Editor="text" FieldName="DebtorDays" MaxLength="4" Visible="False" Width="90" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" MaxLength="20" Visible="False" Width="40" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="text" FieldName="CreateDate" MaxLength="8" Visible="False" Width="90" Format="yyyy-mm-dd HH:MM:SS" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" MaxLength="20" Visible="False" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="text" FieldName="LastUpdateDate" MaxLength="8" Visible="False" Width="90" Format="yyyy-mm-dd HH:MM:SS" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AcceptanceDate" Editor="text" FieldName="AcceptanceDate" MaxLength="0" Visible="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsAssetCompleted" Editor="text" FieldName="IsAssetCompleted" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AccountNO" Editor="text" FieldName="AccountNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ProofTypeID" Editor="text" FieldName="ProofTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetLocaID" Editor="text" FieldName="AssetLocaID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PurVendor" Editor="text" FieldName="PurVendor" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PlanPayDate" Editor="text" FieldName="PlanPayDate" MaxLength="0" Visible="False" Width="80" Frozen="False" IsNvarChar="False" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="會計已審" Editor="text" FieldName="IsAPCompleted" Format="L-V-" MaxLength="0" Visible="True" Width="55" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" ID="dataGridDeliveryInsertBtn" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormDelivery" Title="交貨明細" DialogTop="290px" Width="600px">
                    <JQTools:JQDataForm ID="dataFormDelivery" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="PODelivery" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sPO_Normal_PRPOIQC.POMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormDelivery_OnLoadSuccess" OnApply="dataFormDelivery_OnApply">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="請購單號" Editor="text" FieldName="PONO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="請採購項次" Editor="text" FieldName="ItemNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="品名" Editor="infocombobox" FieldName="ItemID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'ItemID',textField:'ItemName',remoteName:'sPO_Normal_PRPOIQC.Item',tableName:'Item',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                <JQTools:JQFormColumn Alignment="left" Caption="交貨項次" Editor="text" FieldName="DeliveryNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="交貨日期" Editor="datebox" FieldName="DeliveryDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="交貨數量" Editor="numberbox" FieldName="DeliveryQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="驗收人員" Editor="text" FieldName="Surveyors" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="驗收日期" Editor="datebox" FieldName="AcceptanceDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="驗收數量" Editor="numberbox" FieldName="AcceptanceQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="退貨數量" Editor="numberbox" FieldName="ReturnQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="存放區域" Editor="infocombobox" FieldName="AssetLocaID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sPO_Normal_PRPOIQC.AssetLocation',tableName:'AssetLocation',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="驗收照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PO_Normal_PRPOIQC/AcceptancePic',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" FieldName="AcceptancePic" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="420" />
                            <JQTools:JQFormColumn Alignment="left" Caption="物品單價" Editor="numberbox" FieldName="PurPrice" MaxLength="0" NewRow="False" OnBlur="dataFormDelivery_OnBlur" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="precision:2" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="物品總價" Editor="numberbox" FieldName="TotalPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="precision:1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工程金額" Editor="numberbox" FieldName="OtherFee" MaxLength="0" NewRow="False" OnBlur="dataFormDeliveryOtherFee_OnBlur" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="稅額" Editor="numberbox" FieldName="AcceptanceTax" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" OnBlur="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="採購廠商" Editor="inforefval" FieldName="PurVendor" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="title:'廠商',panelWidth:560,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors',tableName:'Vendors',columns:[{field:'VendName',title:'名稱',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'聯絡人手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendName',valueFieldCaption:'廠商ID',textFieldCaption:'廠商名稱',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                            <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" FieldName="PayWayID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'PayWayID',textField:'PayWayName',remoteName:'sPO_Normal_PRPOIQC.PayWay',tableName:'PayWay',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:PayWayID_OnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="檢附憑據" Editor="infocombobox" FieldName="ProofTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sPO_Normal_PRPOIQC.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:ProofTypeID_OnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="InvoiceNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="付款對象" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:570,panelHeight:200,remoteName:'sPO_Normal_PRPOIQC.Vendors1',tableName:'Vendors1',columns:[{field:'VendName',title:'名稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelArea',title:'區碼',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTel',title:'電話',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactTelExt',title:'分機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactMobile',title:'手機',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendAccountName',valueFieldCaption:'VendID',textFieldCaption:'VendAccountName',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:PayTo_OnSelect,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="PayTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="AccountNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                                <JQTools:JQFormColumn Alignment="left" Caption="帳款天數" Editor="numberbox" FieldName="DebtorDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="應付款日" Editor="datebox" FieldName="PlanPayDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IsAssetCompleted" Editor="checkbox" EditorOptions="on:true,off:false" FieldName="IsAssetCompleted" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="autoSeqDelivery" runat="server" BindingObjectID="dataFormDelivery" FieldName="DeliveryNO" NumDig="2" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="PONO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyUserID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequisitKindID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="VoucherYear" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CostCenterID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ItemTypeID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="POTypeID" RemoteMethod="False" ValidateType="None" CheckMethod="DFM_CheckMethod_POTypeID" ValidateMessage="該輸入項為必輸項" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitKindID" RemoteMethod="False" ValidateType="None" CheckMethod="DFM_CheckMethod_POTypeID" ValidateMessage="該輸入項為必輸項" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InsGroupID" RemoteMethod="False" ValidateType="None" CheckMethod="DFM_CheckMethod_POTypeID" ValidateMessage="該輸入項為必輸項" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyOrg_NO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AcSubno" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherYear" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RegQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RegDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <br />
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ItemID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RegQty" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RegDate" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDelivery" runat="server" BindingObjectID="dataFormDelivery" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ReturnQty" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                

            </JQTools:JQDialog>
            
        
        </div>
        <%--                            <JQTools:JQFormColumn Alignment="left" Caption="報價檔1" Editor="text" FieldName="PurDocVen10" Format="download,folder:JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen1" Width="180" NewRow="False" Span="1" MaxLength="0" ReadOnly="True" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價檔2" Editor="text" FieldName="PurDocVen20" Format="download,folder:JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen2" Width="180" maxlength="0" ReadOnly="False" Visible="True" NewRow="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="報價檔3" Editor="text" FieldName="PurDocVen30" Format="download,folder:JB_ADMIN/PO_Normal_PRPOIQC/PurDocVen3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />--%>
        <div id="JQDialog4">
                        <JQTools:JQDataGrid ID="dataGridPODetailsHistory" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="PODetailsHistory" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPO_Normal_PRPOIQC.PODetailsHistory" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="ItemID" Frozen="False" IsNvarChar="False" MaxLength="10" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="品名" Editor="text" FieldName="ItemName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="物品規格" Editor="text" FieldName="ItemSpec" Frozen="False" IsNvarChar="False" MaxLength="256" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="採購單價" Editor="text" FieldName="PurPrice" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="採購數量" Editor="text" FieldName="PurQty" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="其他費用" Editor="text" FieldName="OtherFee" Frozen="False" IsNvarChar="False" MaxLength="4" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="採購廠商" Editor="text" FieldName="PurVendor" Frozen="False" IsNvarChar="False" MaxLength="10" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="採購廠商" Editor="text" FieldName="VendShortName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="請購日期" Editor="text" FieldName="ApplyDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" Format="yyyy-mm-dd">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Enabled="True" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                                <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                    </div>
			<JQTools:JQDialog ID="JQDialogVoucherOpen" runat="server" BindingObjectID="dataFormVoucher" Title="傳票維護" DialogLeft="20px" DialogTop="5px" Width="910px">
                <JQTools:JQDataForm ID="dataFormVoucher" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="POMasterVoucherM" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherSet.POMasterVoucherM" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFMaster" OnApply="OnApplyDFMaster" OnApplied="OnAppliedDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" MaxLength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購單號" Editor="text" FieldName="PONO" Format="" Width="100" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" Format="" Width="160" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infooptions" FieldName="VoucherID" Format="" Width="180" EditorOptions="title:'JQOptions',panelWidth:260,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" MaxLength="0" ReadOnly="True" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>

                <a id="lbVoucher" class="easyui-linkbutton" data-options="" href="#" onclick="InsertVoucherData()">載入項目</a><JQTools:JQDataGrid ID="dataGridDetail0" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="POMasterVoucherD" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog5" EditMode="Dialog" EditOnEnter="True" Height="240px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnInsert="OnInsertDetail" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormVoucher" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglVoucherSet.POMasterVoucherD" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="PONO" Editor="text" FieldName="PONO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" EditorOptions="" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="text" EditorOptions="" FieldName="SubAcnoText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" EditorOptions="" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="text" EditorOptions="" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="270">
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
                        <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="OpenToolTip" Editor="text" FieldName="OpenToolTip" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyDetail" Text="複製" Visible="True" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog5" runat="server" BindingObjectID="dataFormDetail0" EditMode="Continue" Width="850px" Closed="True" Title="" >
                    <JQTools:JQDataForm ID="dataFormDetail0" runat="server" ParentObjectID="dataFormVoucher" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="POMasterVoucherD" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherSet.POMasterVoucherD" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnLoadSuccess="OnLoadSuccessDFDetail"  >
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
                            <JQTools:JQFormColumn Alignment="left" Caption="PONO" Editor="text" FieldName="PONO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PONO" ParentFieldName="PONO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail0" FieldName="Item" NumDig="3" />
                    <JQTools:JQAutoSeq ID="JQAutoSeq11" runat="server" BindingObjectID="dataFormDetail0" FieldName="AutoKey" NumDig="1" />
                </JQTools:JQDialog>
                <JQTools:JQValidate ID="validateMaster0" runat="server" BindingObjectID="dataFormVoucher" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateMessage="請選擇傳票類別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherDate" RemoteMethod="True" ValidateMessage="傳票日期	不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail0" runat="server" BindingObjectID="dataFormDetail0" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="BorrowLendType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail0" runat="server" BindingObjectID="dataFormDetail0" EnableTheming="True">
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
         <JQTools:JQDialog ID="JQDialog6" runat="server" DialogLeft="600px" DialogTop="180px" Title="物品類別選單" Width="480px" Closed="True" ShowSubmitDiv="True" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical" OnSubmited="JQDialog6OnSubmited" Direction="LeftToRight" ShowModal="True">
            <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="ItemTypeItems" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPO_Normal_PRPOIQC.ItemTypeItems" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="420px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品名稱" Editor="text" FieldName="ItemName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品類別名稱" Editor="text" FieldName="ItemTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="240">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品類別代號" Editor="text" FieldName="ItemTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="ResponsibleGROUPID" Editor="text" FieldName="ResponsibleGROUPID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <TooItems>
                   <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                 </TooItems>
                </JQTools:JQDataGrid>
             </JQTools:JQDialog> 
        																																																																																																																																																					   	   
    </form>
</body>
</html>
