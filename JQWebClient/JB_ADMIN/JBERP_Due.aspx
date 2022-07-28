<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Due.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#dataGridDetail").datagrid({
                rowStyler: function (index, row) {
                    if (row.FlowFlag == "Z" && (row.DeleteFlag == null || row.DeleteFlag == "")) {
                        //回傳底色
                        return 'background-color:#6293BB';
                    }
                }
            });
            //按X設firstLoad為false，為了OnLoad_dataGridDetail變單選，checkbox多選
            $('#JQDialog1').dialog('options').onBeforeClose = function () {
                $("#dataGridDetail").data('firstLoad', false);
                return true;
            }
        });
        //新增
        function OnClickGridButton1(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var row = $("#dataGridView").datagrid('getSelected');
            var DueNO = row.DueNO;
            var DueYM = row.DueYM;
            var DetailCounts = row.DetailCounts;
            //檢查期滿人數是否已有，有就不塞外勞資料
            if(DetailCounts>0){return false;}

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                data: "mode=method&method=" + "SelectLab" + "&parameters=" + DueYM + "," + DueNO,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        if (data == "True") {
                            alert("新增成功");
                            $("#dataGridView").datagrid('load');
                        } else { alert("已有期滿月份勞工"); }
                    } else {
                        alert('新增失敗');
                    }
                }
            });
        }
        //補增
        function OnClickGridButton2(index) {
            $("#dataGridView").datagrid('selectRow', index);
            var row = $("#dataGridView").datagrid('getSelected');
            var DueNO = row.DueNO;
            var DueYM = row.DueYM;
            var LaborDeadline = row.LaborDeadline;
            var EmployerDeadline = row.EmployerDeadline;
            var DetailCounts = row.DetailCounts;

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                data: "mode=method&method=" + "SelectLab1" + "&parameters=" + DueYM + "," + DueNO,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        alert("新增成功");
                        $("#dataGridView").datagrid('load');
                    } else {
                        alert('新增失敗');
                    }
                }
            });
        }
        //沒用到
        function SelectERPDueFormDetail(DueNO) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                data: "mode=method&method=SelectERPDueFormDetail&parameters=" +DueNO,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        var rows = $.parseJSON(data);
                        var row = new Object();
                        if (rows.length > 0) {
                            for (var i = 0; i < rows.length; i++) {
                                row['DueNO'] = rows[i].DueNO;
                                row['AutoKey'] = i;
                                row['LaborName'] = rows[i].LaborName;
                                row['eLaborName'] = rows[i].eLaborName; row['Employer'] = rows[i].Employer;
                                row['eEmployer'] = rows[i].eEmployer; row['Contact'] = rows[i].Contact;
                                row['Gender'] = rows[i].Gender; row['Country'] = rows[i].Country;
                                row['ImmigrationDate'] =parseInt(rows[i].ImmigrationDate.substring(0, 3)) + 1911 + '/' + rows[i].ImmigrationDate.substring(3, 5) + '/' + rows[i].ImmigrationDate.substring(5, 7);
                                row['DueDate'] = parseInt(rows[i].DueDate.substring(0, 3)) + 1911 + '/' + rows[i].DueDate.substring(3, 5) + '/' + rows[i].DueDate.substring(5, 7);
                                row['IsRecontract'] = rows[i].IsRecontract; row['CEConfirmNO'] = rows[i].CEConfirmNO;
                                row['Transfer'] = rows[i].Transfer; row['ReturnHome'] = rows[i].ReturnHome;
                                $('#dataGridDetail').datagrid('appendRow', row);
                            }
                        }
                    } else {
                        alert('失敗');
                    }
                }
            });
        }
        //通知單申請按鈕
        function OnClickGridButton() {
            var DueNO = $('#dataFormMasterDueNO').val();
            if ($("#dataGridDetail").datagrid('getChecked').length == 0) {
                alert('請先勾選');
            } else {
                var pre = confirm("確定送出「通知單總單」的申請?");
                if (pre == true) {
                    var rows = $('#dataGridDetail').datagrid('getChecked');
                    //檢查是否多勾選
                    var x1, x2, x3;
                    for (var i = 0; i < rows.length; i++) {
                        x1 = 0; x2 = 0; x3 = 0; x4 = 0; x5 = 0;
                        if (rows[i].IsRecontract == true) { x1 = 1; }
                        if (rows[i].Transfer == true) { x2 = 1; }
                        if (rows[i].ReturnHome == true) { x3 = 1; }
                        if (rows[i].BackPot == true) { x4 = 1; }
                        if (rows[i].TransferAg == true) { x5 = 1; }
                        if ((x1 + x2 + x3 + x4 + x5) > 1) {
                            alert('確認書編號' + rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + " 意願多勾選");
                            return false;
                        }
                    }

                    //檢查已申請過通知單的
                    var temp = 0;
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].FlowFlag == "N" || rows[i].FlowFlag == "P" || (rows[i].FlowFlag == "Z" && rows[i].DeleteFlag != true)) {
                            temp = temp + 1;
                            alert('確認書編號'+rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + "已申請通知單，請勿勾選");
                        }
                        if ((rows[i].IsRecontract == null || rows[i].IsRecontract == false) && (rows[i].Transfer == null || rows[i].Transfer == false) && (rows[i].ReturnHome == null || rows[i].ReturnHome == false) && (rows[i].BackPot == null || rows[i].BackPot == false) && (rows[i].TransferAg == null || rows[i].TransferAg == false)) {
                            temp = temp + 1;
                            alert('確認書編號' + rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + " 的續聘意願未勾選");
                        }
                        //if ((rows[i].IsRecontract == true) && (rows[i].Transfer == true ) && (rows[i].ReturnHome == true)||
                        //    (rows[i].IsRecontract == true) && (rows[i].Transfer == false) && (rows[i].ReturnHome == true)||
                        //    (rows[i].IsRecontract == false) && (rows[i].Transfer == true) && (rows[i].ReturnHome == true)||
                        //    (rows[i].IsRecontract == true) && (rows[i].Transfer == true) && (rows[i].ReturnHome == false)) {
                        //    temp = temp + 1;
                        //    alert('確認書編號' + rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + " 續聘意願重複勾選");
                        //}
                    }
                    if (temp > 0) {return false; }
                    
                    var aAutoKey = [];
                    var aLaborName = [];
                    var aGender = [];
                    var aEmployer = [];
                    var aCountry = [];
                    var aImmigrationDate = [];
                    var aDueDate = [];
                    var aIsRecontract = [];
                    var aCEConfirmNO = [];
                    var aTransfer = [];
                    var aReturnHome = [];
                    var aBackPot = [];
                    var aSalesID = [];
                    var aTransferAg = [];

                    var sAutoKey, sLaborName, sGender, sEmployer, sCountry, sImmigrationDate, sDueDate, sIsRecontract, sCEConfirmNO, sTransfer, sReturnHome, sBackPot, sSalesID, sTransferAg;

                    for (var i = 0; i < rows.length; i++) {
                        if (i == 0) {
                            sAutoKey = rows[i].AutoKey;
                            sLaborName = rows[i].LaborName.trim();
                            sGender = rows[i].Gender.trim();
                            sEmployer = rows[i].Employer.trim();
                            sCountry = rows[i].Country.trim();
                            sImmigrationDate = rows[i].ImmigrationDate;
                            sDueDate = rows[i].DueDate;
                            sIsRecontract = (rows[i].IsRecontract == null) ? "0" : rows[i].IsRecontract;
                            sCEConfirmNO = rows[i].CEConfirmNO;
                            sTransfer = (rows[i].Transfer == null) ? "0" : rows[i].Transfer;
                            sReturnHome = (rows[i].ReturnHome == null) ? "0" : rows[i].ReturnHome;
                            sBackPot = (rows[i].BackPot == null) ? "0" : rows[i].BackPot;
                            sSalesID = rows[i].SalesID.trim();
                            sTransferAg = (rows[i].TransferAg == null)?"0":rows[i].TransferAg;
                        } else if (rows[i].Employer.trim() != rows[i - 1].Employer.trim()) {
                            sAutoKey = sAutoKey + '$' + rows[i].AutoKey;
                            sLaborName = sLaborName + '$' + rows[i].LaborName.trim();
                            sGender = sGender + '$' + rows[i].Gender;
                            sEmployer = sEmployer + '$' + rows[i].Employer.trim();
                            sCountry = sCountry + '$' + rows[i].Country.trim();
                            sImmigrationDate = sImmigrationDate + '$' + rows[i].ImmigrationDate;
                            sDueDate = sDueDate + '$' + rows[i].DueDate;
                            sIsRecontract = sIsRecontract + '$' + ((rows[i].IsRecontract == null) ? "0" : rows[i].IsRecontract);
                            sCEConfirmNO = sCEConfirmNO + '$' + rows[i].CEConfirmNO;
                            sTransfer = sTransfer + '$' + ((rows[i].Transfer == null) ? "0" : rows[i].Transfer);
                            sReturnHome = sReturnHome + '$' + ((rows[i].ReturnHome == null) ? "0" : rows[i].ReturnHome);
                            sBackPot = sBackPot + '$' + ((rows[i].BackPot == null) ? "0" : rows[i].BackPot);
                            sSalesID = sSalesID + '$' + rows[i].SalesID.trim();
                            sTransferAg = sTransferAg + '$' + ((rows[i].TransferAg == null) ? "0" : rows[i].TransferAg);
                        } else if (rows[i].Employer.trim() == rows[i - 1].Employer.trim()) {
                            sAutoKey = sAutoKey + '*' + rows[i].AutoKey;
                            sLaborName = sLaborName + '*' + rows[i].LaborName.trim();
                            sGender = sGender + '*' + rows[i].Gender;
                            sEmployer = sEmployer + '*' + rows[i].Employer.trim();
                            sCountry = sCountry + '*' + rows[i].Country.trim();
                            sImmigrationDate = sImmigrationDate + '*' + rows[i].ImmigrationDate;
                            sDueDate = sDueDate + '*' + rows[i].DueDate;
                            sIsRecontract = sIsRecontract + '*' + ((rows[i].IsRecontract == null) ? "0" : rows[i].IsRecontract);
                            sCEConfirmNO = sCEConfirmNO + '*' + rows[i].CEConfirmNO;
                            sTransfer = sTransfer + '*' + ((rows[i].Transfer == null) ? "0" : rows[i].Transfer);
                            sReturnHome = sReturnHome + '*' + ((rows[i].ReturnHome == null) ? "0" : rows[i].ReturnHome);
                            sBackPot = sBackPot + '*' + ((rows[i].BackPot == null) ? "0" : rows[i].BackPot);
                            sSalesID = sSalesID + '*' + rows[i].SalesID.trim();
                            sTransferAg = sTransferAg + '*' + ((rows[i].TransferAg == null) ? "0" : rows[i].TransferAg);
                        }
                    }

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                        data: "mode=method&method=" + "JBERPContinueEmployStartUp" + "&parameters=" + DueNO + "," + sAutoKey + "," + sLaborName + "," + sGender + "," + sEmployer + "," + sCountry + "," + sImmigrationDate + "," + sDueDate + "," + sIsRecontract + "," + sCEConfirmNO + "," + sTransfer + "," + sReturnHome + "," + sBackPot + "," + sSalesID + "," + sTransferAg, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != "False") {
                                alert('續聘通知單總單已成功申請');
                                $("#dataGridDetail").datagrid("load");
                            } else {
                                alert('申請失敗');
                            }
                        }
                    });
                }
            }
        }

        //確認書按鈕
        function OpeneReportOrders() {
            var DueNO = $('#dataFormMasterDueNO').val();
            var rows = $('#dataGridDetail').datagrid('getChecked');
            if (rows.length == 0) {
                alert('請先勾選');
                return false;
            }
            var aCEConfirmNO = [];
            for (var i = 0; i < rows.length; i++) {
                aCEConfirmNO.push("'" + rows[i].CEConfirmNO + "'");
            }
            var sCEConfirmNO = aCEConfirmNO.join(',');
            var url = "../JB_ADMIN/REPORT/FWCRM/CEConfirmReportView.aspx?DueNO=" + DueNO + "&CEConfirmNO=" + sCEConfirmNO;// + "&OrderType=" + OrderType;

            var height = $(window).height() - 50;
            //var width = $(window).width() - 600;
            var width = 900;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "外籍勞工期滿續聘確認書",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }
        //內聯單按鈕
        function OpeneReportOrders1() {
            var DueNO = $('#dataFormMasterDueNO').val();
            var rows = $('#dataGridDetail').datagrid('getChecked');
            if (rows.length == 0) {
                alert('請先勾選');
                return false;
            }
            var aCEConfirmNO = [];
            for (var i = 0; i < rows.length; i++) {
                aCEConfirmNO.push("'" + rows[i].CEConfirmNO + "'");
            }
            var sCEConfirmNO = aCEConfirmNO.join(',');
            var url = "../JB_ADMIN/REPORT/FWCRM/CEConfirmTrackReportView.aspx?DueNO=" + DueNO + "&CEConfirmNO=" + sCEConfirmNO;// + "&OrderType=" + OrderType;

            var height = $(window).height() - 150;
            //var width = $(window).width() - 600;
            var width = 840;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: "續聘確認書內聯單",                            
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }

        //新增按鈕
        function FormatScript_InsertDetail(val, row, index) {
            if (val > 0) {
                return val;//$('<a>', { href: 'javascript:void(0)', id: 'GridButton1' + index, onclick: 'OnClickGridButton1(' + index + ')' }).linkbutton({ plain: false, text: '&nbsp;&nbsp;' + val + '&nbsp;&nbsp;', disabled: 'disabled' })[0].outerHTML
            } else {
                return $('<a>', { href: 'javascript:void(0)', id: 'GridButton1' + index, onclick: 'OnClickGridButton1(' + index + ')' }).linkbutton({ plain: false, text: '新增' })[0].outerHTML
            }
        }

        //新增按鈕(宏燦系統有新增這個期滿月份的外勞)
        function FormatScript_DiffeButton(val, row, index) {
            if (row.DetailCounts != null && row.Diffe > 0) {//已有期滿人數且差異人數>0
                return $('<a>', { href: 'javascript:void(0)', id: 'GridButton2' + index, onclick: 'OnClickGridButton2(' + index + ')' }).linkbutton({ plain: false, text: '新增' + row.Diffe+'人' })[0].outerHTML
            }
        }
        function FormatScript_CheckBox(val) {
            return (val == true) ? "V" : "";
        }
        function FormatScript_WillCounts(val, row, index) {
            if ((row.RecontractCounts != null && row.RecontractCounts != "") || (row.TransferCounts != null && row.TransferCounts != "") || (row.ReturnHomeCounts != null && row.ReturnHomeCounts != "") || (row.BackPotCounts != null && row.BackPotCounts != "") || (row.TransferAgCounts != null && row.TransferAgCounts != "")) {
                return (parseInt((row.RecontractCounts == null) ? "0" : row.RecontractCounts) + parseInt((row.TransferCounts == null) ? "0" : row.TransferCounts) + parseInt((row.ReturnHomeCounts == null) ? "0" : row.ReturnHomeCounts) + parseInt((row.BackPotCounts == null) ? "0" : row.BackPotCounts) + parseInt((row.TransferAgCounts == null) ? "0" : row.TransferAgCounts));
            }
            else { return '' }
        }
        
        function OnLoad_dataFormMaster() {
            var DueNO = $("#dataFormMasterDueNO").val();
            //增加按鈕
            if ($("#GridButton").size() == 0 && $("#PrintLink").size() == 0) {
                var FlowLink = $('<a>', { href: 'javascript:void(0)', id: 'GridButton', onclick: 'OnClickGridButton()' }).linkbutton({ plain: false, text: '通知單總單申請' })[0].outerHTML
                var PrintLink = $('<a>', { href: 'javascript:void(0)', id: 'PrintLink', onclick: 'OpeneReportOrders()' }).linkbutton({ plain: false, text: '確認書列印' })[0].outerHTML
                var PrintLink1 = $('<a>', { href: 'javascript:void(0)', id: 'PrintLink1', onclick: 'OpeneReportOrders1()' }).linkbutton({ plain: false, text: '調查內聯單列印' })[0].outerHTML
                $("#toolbardataGridDetail").append(PrintLink);
                $("#toolbardataGridDetail").append("&nbsp;");
                $("#toolbardataGridDetail").append(PrintLink1);
                $("#toolbardataGridDetail").append("&nbsp;");
                $("#toolbardataGridDetail").append(FlowLink);
            }
        }
        //沒用
        function OnClick_SearchButton() {
            var e = $("#dataFormMasterEmployer").combobox('getValue');
            var l = $("#dataFormMasterLaborName").combobox('getValue');
            var c = $("#dataFormMasterCountry").combobox('getValue');
            alert(e + '/' + l + '/' + c);
            var where = $("#dataGridDetail").datagrid('getWhere');

            if (e != '' || l == '' || c == '') {
                $("#dataGridDetail").datagrid('setWhere', "Employer ='" + e + "'");
            } else if (e == '' || l != '' || c == '') {
                $("#dataGridDetail").datagrid('setWhere', "LaborName ='" + l + "'");
            } else if (e == '' || l == '' || c != '') {
                $("#dataGridDetail").datagrid('setWhere', "Country ='" + c + "'");
            } else if (e != '' || l != '' || c == '') {
                $("#dataGridDetail").datagrid('setWhere', "Employer ='" + e + "' and LaborName ='" + l + "'");
            } else if (e != '' || l == '' || c != '') {
                $("#dataGridDetail").datagrid('setWhere', "Employer ='" + e + "' and Country ='" + c + "'");
            } else if (e == '' || l != '' || c != '') {
                $("#dataGridDetail").datagrid('setWhere', "LaborName ='" + l + "' and Country ='" + c + "'");
            } else if (e != '' || l != '' || c != '') {
                $("#dataGridDetail").datagrid('setWhere', "Employer ='" + e + "' and LaborName ='" + l + "' and Country ='" + c + "'");
            } else if (e == '' || l == '' || c == '') {
            }
        }
        //明細檔 外勞姓名
        function OnSelectLaborName(rowdata) {
            $("#dataFormDetaileLaborName").val(rowdata.lab_name);
            $("#dataFormDetailEmployer").combobox("setValue",rowdata.title);
            $("#dataFormDetaileEmployer").val(rowdata.ename);
            $("#dataFormDetailContact").val(rowdata.contact);
            if (rowdata.sex == "M") {
                $("#dataFormDetailGender").combobox("setValue", "男");
            } else if (rowdata.sex == "F") {
                $("#dataFormDetailGender").combobox("setValue", "女");
            }
            $("#dataFormDetailCountry").val(rowdata.nat_name);
            $("#dataFormDetailImmigrationDate").datebox('setValue', parseInt(rowdata.lab_idate.substring(0, 3)) + 1911 + '/' + rowdata.lab_idate.substring(3, 5) + '/' + rowdata.lab_idate.substring(5, 7));
            $("#dataFormDetailDueDate").datebox('setValue', parseInt(rowdata.lab_edate.substring(0, 3)) + 1911 + '/' + rowdata.lab_edate.substring(3, 5) + '/' + rowdata.lab_edate.substring(5, 7));
        }
        //主檔 勞工填寫截止日期
        function OnBlur_LaborDeadline() {
            var LaborDeadline = $("#dataFormMasterLaborDeadline").datebox('getValue');
        }
        //明細檔 雇主名稱
        function OnSelectEmployer(rowdata) {
            $("#dataFormDetaileEmployer").val(rowdata.ename);
            $("#dataFormDetailContact").val(rowdata.contact);
        }
        //點"明細"
        function OnClickEditDetail() {
            if ($("#dataGridView").datagrid('getSelected')) {
                openForm('#JQDialog1', $("#dataGridView").datagrid('getSelected'), "updated", 'dialog');
            }
        }
        function OnInserted_dataGridView() {
            $("#dataGridView").datagrid('load');
        }
        //修改時，停用期滿年月編輯
        function OnUpdate_dataGridView() {
            var row = $("#dataGridView").datagrid('getSelected');
            var index = $("#dataGridView").datagrid('getRowIndex', row);
            var editIndex = undefined;
            if (editIndex != index) {
                $("#dataGridView").datagrid('selectRow', index).datagrid('beginEdit', index);
                var cellEdit = $("#dataGridView").datagrid('getEditor', { index: index, field: 'DueYM' });
                $(cellEdit.target).attr("disabled", "disabled"); //text
                //$("#dataGridView").datagrid('endEdit', index);
            }
        }
        //dataGridView存檔前的檢查
        function OnClick_dataGridViewApply() {
            var row = $("#dataGridView").datagrid('getSelected');
            if (row) {
                var index = $("#dataGridView").datagrid('getRowIndex', row);
                var editIndex = undefined;
                if (editIndex != index) {
                    $("#dataGridView").datagrid('selectRow', index).datagrid('beginEdit', index);
                    var cellEdit = $("#dataGridView").datagrid('getEditor', { index: index, field: 'DueYM' });
                    var cellEdit1 = $("#dataGridView").datagrid('getEditor', { index: index, field: 'DueNO' });
                    var cellEdit2 = $("#dataGridView").datagrid('getEditor', { index: index, field: 'LaborDeadline' });
                    var cellEdit3 = $("#dataGridView").datagrid('getEditor', { index: index, field: 'EmployerDeadline' });
                    var DueYM = $(cellEdit.target).val();
                    var DueNO = $(cellEdit1.target).val();
                    var LaborDeadline = $(cellEdit2.target).datebox('getValue');
                    var EmployerDeadline = $(cellEdit3.target).datebox('getValue');
                    $("#dataGridView").datagrid('selectRow', index).datagrid('endEdit', index);
                    if (LaborDeadline == "") {
                        alert("外勞填單截止日須填");
                        return false;
                    }
                    if (EmployerDeadline == "") {
                        alert("雇主填單截止日須填");
                        return false;
                    }
                    //新增時，檢查期滿月份格式，檢查不過則不存檔
                    if (DueNO == "") {
                        var re = new RegExp(/[1][0-9]{2}[0-9]{2}/);
                        var returnValue = re.exec(DueYM);
                        if (returnValue == null) {
                            alert("格式須YYYMM，例如:10609");
                            return false;
                        }
                    }
                    //新增時，檢查期滿月份是否重複，不重覆則存檔
                    if (DueNO == "") {
                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                            data: "mode=method&method=" + "SelectDueYM" + "&parameters=" + DueYM,
                            cache: false,
                            async: false,
                            success: function (data) {
                                if (data != "False") {
                                    if (data == "True") {
                                        apply($("#dataGridView"));
                                        alert("存檔成功");
                                    } else { alert("期滿年月有重複"); }
                                } else {
                                    alert('檢查失敗');
                                }
                            }
                        });
                    } else {
                        apply($("#dataGridView"));
                    }
                }
            }
        }
        function OnLoad_dataGridView() {
            //$("#dataGridView").datagrid("selectRow",-1);
        }
        //刪除某確認書總單前之檢查
        function OnDeleting_dataGridView(row) {
            deleteRow(row)
            return false;
        }
        //若某確認書總單的確認書裡有通知單流程狀態，就不能刪
        function deleteRow(row) {
            var DueNO = row.DueNO;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPDue.ERPDueFormMaster', //連接的Server端，command
                data: "mode=method&method=" + "OnDelete_dataGridView" + "&parameters=" + DueNO,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "False") {
                        if (data == "True") {
                            if (confirm("確定刪除?")) {
                                var index = $("#dataGridView").datagrid('getRowIndex', row);
                                $("#dataGridView").datagrid('beginEdit', index);
                                var editor = $("#dataGridView").datagrid('getEditor', { index: index, field: 'DeleteFlag' });
                                if (editor) {
                                    editor.actions.setValue(editor.target, "1");
                                }
                                $("#dataGridView").datagrid('endEdit', index);
                                apply($("#dataGridView"));
                            }
                        } else { alert("已跑通知單總單流程，無法刪除"); return false; }
                    } else {
                        alert('檢查失敗'); return false;
                    }
                }
            });
            $("#dataGridView").datagrid('load');
        }
        function FormatScript_dataGridDetailFlowFlag(val) {
            if (val == 'N') {
                return '新申請';
            } else if (val == 'P') {
                return '流程中';
            } else if (val == 'Z') {
                return '結案';
            } else if (val == 'X') {
                return '作廢';
            }
        }

        function OnCancel_dataFormMaster() {
            //$("#dataGridView").datagrid('load');
            $("#dataGridDetail").data('firstLoad', false);//為了dataGridDetail單選，checkbox多選
        }
        //dataFormMaster存檔
        function submitForm() {
            var rows = $("#dataGridDetail").datagrid('getRows');
            var x1, x2, x3, x4, x5;
            var index;
            var cellEdit1, cellEdit2, cellEdit3, cellEdit4, cellEdit5;
            //檔每列的意願多選
            for (var i = 0; i < rows.length; i++) {
                x1 = 0; x2 = 0; x3 = 0; x4 = 0; x5 = 0;
                cellEdit1 = $("#dataGridDetail").datagrid('getEditor', { index: i, field: 'IsRecontract' });
                if (cellEdit1 != null) {//編輯狀態
                    cellEdit2 = $("#dataGridDetail").datagrid('getEditor', { index: i, field: 'Transfer' });
                    cellEdit3 = $("#dataGridDetail").datagrid('getEditor', { index: i, field: 'ReturnHome' });
                    cellEdit4 = $("#dataGridDetail").datagrid('getEditor', { index: i, field: 'BackPot' });
                    cellEdit5 = $("#dataGridDetail").datagrid('getEditor', { index: i, field: 'TransferAg' });

                    if ($(cellEdit1.target).checkbox('getValue') == true) { x1 = 1; }
                    if ($(cellEdit2.target).checkbox('getValue') == true) { x2 = 1; }
                    if ($(cellEdit3.target).checkbox('getValue') == true) { x3 = 1; }
                    if ($(cellEdit4.target).checkbox('getValue') == true) { x4 = 1; }
                    if ($(cellEdit5.target).checkbox('getValue') == true) { x5 = 1; }
                    if ((x1 + x2 + x3 + x4 + x5) > 1) {
                        alert('確認書編號' + rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + " 意願只能勾選一個");
                        return false;
                    }
                } else {//非編輯狀態
                    if (rows[i].IsRecontract == true) { x1 = 1; }
                    if (rows[i].Transfer == true) { x2 = 1; }
                    if (rows[i].ReturnHome == true) { x3 = 1; }
                    if (rows[i].BackPot == true) { x4 = 1; }
                    if (rows[i].TransferAg == true) { x5 = 1; }
                    if ((x1 + x2 + x3 + x4 + x5) > 1) {
                        alert('確認書編號' + rows[i].CEConfirmNO.trim() + ':' + rows[i].LaborName.trim() + " 意願只能勾選一個");
                        return false;
                    }
                }
            }
            apply("#dataGridDetail");//存到DB
            $("#dataGridDetail").datagrid('load');
        }

        function OnLoad_dataGridDetail() {
            //單選(為了OnUpdate_dataGridDetail來停用結案列的編輯
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                $("#dataGridDetail").datagrid({
                    singleSelect: true,
                    selectOnCheck: false,
                    checkOnSelect: false
                });
            }
            
            //為了取消預設第一列勾選
            setTimeout(function () {
                $("#dataGridDetail").datagrid("unselectAll");
            }, 600);
            
        }
        //若為流程狀態，則停用列編輯
        function OnUpdate_dataGridDetail() {

            var cellEdit1, cellEdit2, cellEdit3, cellEdit4, cellEdit5;
            var rows = $("#dataGridDetail").datagrid('getSelected');
            var index = $("#dataGridDetail").datagrid('getRowIndex', rows);
            if ((rows.FlowFlag == "Z" && rows.DeleteFlag == null) || (rows.FlowFlag == "Z" && rows.DeleteFlag == "") || (rows.FlowFlag == "N") || (rows.FlowFlag == "P")) {
                $("#dataGridDetail").datagrid('selectRow', index).datagrid('beginEdit', index);
                cellEdit1 = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'IsRecontract' });
                cellEdit2 = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'Transfer' });
                cellEdit3 = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'ReturnHome' });
                cellEdit4 = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'BackPot' });
                cellEdit5 = $("#dataGridDetail").datagrid('getEditor', { index: index, field: 'TransferAg' });
                $(cellEdit1.target).attr("disabled", "disabled"); 
                $(cellEdit2.target).attr("disabled", "disabled"); 
                $(cellEdit3.target).attr("disabled", "disabled"); 
                $(cellEdit4.target).attr("disabled", "disabled");
                $(cellEdit5.target).attr("disabled", "disabled");
            } 
        }
        function FormatScript_GridDetailEmployer(val){
            return(val.substring(0, 4));
        }
        function FormatScript_DeleteFlag(val) {
            return (val == "true") ? "V" : "";
        }
        
        function queryGrid(dg) {
            var where = $(dg).datagrid('getWhere');
            var e = $('#DueYM_Query').combobox('getValue');
            if (e != '') {
                $(dg).datagrid('setWhere', "(convert(datetime,substring(DueYM,4,2)+ '-01'+'-'+convert(nvarchar(4),(convert(int,substring(DueYM,1,3))+1911))) <=  dateadd(day ,-1,dateadd(MONTH,1,convert(varchar,convert(int,substring('" + e + "',1,3))+1911)+'-'+substring('" + e + "',4,2)+ '-01')) and  convert(datetime,substring(DueYM,4,2)+ '-01'+'-'+convert(nvarchar(4),(convert(int,substring(DueYM,1,3))+1911))) >= DATEADD (YEAR , -1 ,dateadd(day ,-1,dateadd(MONTH,1,convert(varchar,convert(int,substring('" + e + "',1,3))+1911)+'-'+substring('" + e + "',4,2)+ '-01'))))");
            } else {
                $(dg).datagrid('setWhere', '');
            }
        }
        //沒用
        function genCheckBox(val) {
            if (val != false)
                return "<input  type='checkbox' checked='true' onclick='return false;'/>";
            else
                return "<input  type='checkbox' onclick='return false;'/>";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPDue.ERPDueFormMaster" runat="server" AutoApply="False"
                DataMember="ERPDueFormMaster" Pagination="True" QueryTitle="" EditDialogID=""
                Title="續聘確認書總單列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,80,100" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnInserted="OnInserted_dataGridView" OnLoadSuccess="OnLoad_dataGridView" OnDelete="OnDeleting_dataGridView" OnUpdate="OnUpdate_dataGridView">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="確認書總單編號" Editor="text" FieldName="DueNO" Format="" MaxLength="0" Visible="true" Width="100" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="期滿年月" Editor="text" FieldName="DueYM" Format="" MaxLength="0" Visible="true" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="外勞填單截止日" Editor="datebox" FieldName="LaborDeadline" Format="" MaxLength="0" Visible="true" Width="100" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主填單截止日" Editor="datebox" FieldName="EmployerDeadline" Format="" MaxLength="0" Visible="true" Width="100" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="期滿人數" Editor="text" FieldName="DetailCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="64" FormatScript="FormatScript_InsertDetail">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="text" FieldName="RecontractCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="text" FieldName="TransferCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="text" FieldName="ReturnHomeCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="text" FieldName="BackPotCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="text" FieldName="TransferAgCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="已勾" Editor="text" FieldName="WillCounts" FormatScript="FormatScript_WillCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="結案" Editor="text" FieldName="FlowEndings" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="送通知單" Editor="text" FieldName="FlowCounts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="變更意願" Editor="text" FieldName="DeleteFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="補增" Editor="text" FieldName="Diffe" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="補增" Editor="text" FieldName="DiffeButton" FormatScript="FormatScript_DiffeButton" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="OnClickEditDetail" Visible="True" Text="明細" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="OnClick_dataGridViewApply" Text="存檔" Visible="True" />
                    <JQTools:JQToolItem Enabled="True" Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="期滿年月" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'DueYM',textField:'DueYM',remoteName:'sERPDue.DueYM',tableName:'DueYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DueYM" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="續聘確認書總單" Width="1160px" DialogTop="5px" Closed="True" DialogLeft="5px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPDueFormMaster" HorizontalColumnsCount="2" RemoteName="sERPDue.ERPDueFormMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoad_dataFormMaster" OnCancel="OnCancel_dataFormMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="確認書總單編號 " Editor="text" FieldName="DueNO" Format="" maxlength="0" Width="180" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="期滿年月" Editor="text" FieldName="DueYM" Format="" maxlength="0" Width="180" EditorOptions="" ReadOnly="True" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="外勞填單截止日" Editor="datebox" FieldName="LaborDeadline" Format="" maxlength="0" Width="180" OnBlur="" ReadOnly="True" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主填單截止日" Editor="datebox" FieldName="EmployerDeadline" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="雇主" Caption="Employer" Editor="infocombobox" EditorOptions="valueField:'Employer',textField:'Employer',remoteName:'sERPDue.Search_Employer',tableName:'Search_Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Employer" MaxLength="0" ReadOnly="False" Width="200" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LaborName" Editor="infocombobox" EditorOptions="valueField:'LaborName',textField:'LaborName',remoteName:'sERPDue.Search_LaborName',tableName:'Search_LaborName',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LaborName" maxlength="0" ReadOnly="False" Width="80" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Country" Editor="infocombobox" EditorOptions="valueField:'Country',textField:'Country',remoteName:'sERPDue.Search_Country',tableName:'Search_Country',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Country" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPDueFormDetail" EditDialogID="" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPDue.ERPDueFormMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoad_dataGridDetail" Height="450px" OnUpdate="OnUpdate_dataGridDetail" >
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="確認書編號" Editor="text" FieldName="CEConfirmNO" Width="140" Visible="True" ReadOnly="False" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="內聯單號" Editor="text" FieldName="CEConfirmTrackNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="確認書總單編號" Editor="text" FieldName="DueNO" Format="" ReadOnly="True" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" ReadOnly="True" Visible="False" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="勞工姓名" Editor="text" FieldName="LaborName" Format="" ReadOnly="True" Visible="True" Width="52" />
                        <JQTools:JQGridColumn Alignment="left" Caption="勞工英名" Editor="text" FieldName="eLaborName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" Format="" FormatScript="FormatScript_GridDetailEmployer" ReadOnly="True" Visible="True" Width="55" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主英稱" Editor="text" FieldName="eEmployer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主聯絡人" Editor="text" FieldName="Contact" ReadOnly="True" Visible="True" Width="65">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="入境日" Editor="text" FieldName="ImmigrationDate" Format="yyyy-mm-dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="期滿日" Editor="text" FieldName="DueDate" Format="yyyy-mm-dd" ReadOnly="True" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRecontract" Format="" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="Transfer" Format="" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="ReturnHome" Format="" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="BackPot" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="TransferAg" FormatScript="FormatScript_CheckBox" ReadOnly="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="送單狀態" Editor="text" EditorOptions="" FieldName="FlowFlag" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="送單狀態" Editor="text" FieldName="CFlowFlag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="負責業務" Editor="text" FieldName="SalesName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" ReadOnly="False" Visible="False" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="變意願" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="DeleteFlag" Format="" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起單數" Editor="text" FieldName="counts" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="居留證號碼" Editor="text" FieldName="ResidenceID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="欠款" Editor="text" FieldName="FeeAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="DueNO" ParentFieldName="DueNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPDueFormDetail" HorizontalColumnsCount="2" RemoteName="sERPDue.ERPDueFormMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="DueNO" Format="" Width="120" Visible="False" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" ReadOnly="False" Visible="True" Width="80" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="確認書編號" Editor="text" FieldName="CEConfirmNO" Visible="True" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="勞工姓名" Editor="inforefval" FieldName="LaborName" Width="120" ReadOnly="False" Visible="True" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sERPDue.lab',tableName:'lab',columns:[{field:'lab_no',title:'lab_no',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'lab_cname',title:'lab_cname',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'lab_name',title:'lab_name',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'lab_edate',title:'lab_edate',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'lab_cname',textField:'lab_cname',valueFieldCaption:'lab_cname',textFieldCaption:'lab_cname',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:OnSelectLaborName,selectOnly:false,capsLock:'none',fixTextbox:'false'" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="外勞英名" Editor="text" FieldName="eLaborName" Visible="True" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" FieldName="Employer" Format="" Width="120" EditorOptions="valueField:'title',textField:'title',remoteName:'sERPDue.cus',tableName:'cus',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主英名" Editor="text" FieldName="eEmployer" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主聯絡人" Editor="text" FieldName="Contact" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="infocombobox" FieldName="Gender" Format="" Width="120" EditorOptions="items:[{value:'男',text:'男',selected:'false'},{value:'女',text:'女',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="yyyy/mm/dd" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="yyyy/mm/dd" Width="120" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘與否" Editor="checkbox" FieldName="IsRecontract" Width="120" Format="" EditorOptions="on:1,off:0" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="轉換雇主" Editor="checkbox" FieldName="Transfer" Format="" Width="120" EditorOptions="on:1,off:0" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="返國" Editor="checkbox" FieldName="ReturnHome" Format="" Width="120" EditorOptions="on:1,off:0" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="TransferAg" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="TransferAg" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="DueNO" ParentFieldName="DueNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataGridView" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="DueNO" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataGridView" EnableTheming="True">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="CEConfirmNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
    <%--<script type="text/javascript">
        $(document).ready(
    //$(function () {
    function () {
        $("#JQDialog1").dialog({ onClose: function () { alert('close'); } });
        //});
    }
        );
    </script>--%>
</html>
