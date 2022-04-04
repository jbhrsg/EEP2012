<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRM_Salary_Report_Salary.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="x-ua-compatible" content="IE=edge" />
    <link href="../css/JBHRIS/Dialog.css" rel="stylesheet" />
    <link href="../css/JBHRIS/HRM_Salary_Report_Salary.css" rel="stylesheet" />
    <title>發放薪資</title>
    <script>
        $(document).ready(function () {
            var date = new Date();
            var beginYear = date.getFullYear().toString();
            var beginMonth = (date.getMonth() + 1).toString();
            var beginDay = date.getDate().toString();
            date = new Date(beginYear, date.getMonth() + 1, 0);
            ////將月份移至下個月份
            //date.setMonth(date.getMonth() + 1);
            ////設定為下個月份的第一天
            //date.setDate(1);
            ////將日期-1為當月的最後一天
            //date.setDate(date.getDate() - 1);
            var endYear = date.getFullYear().toString();
            var endMonth = (date.getMonth() + 1).toString();
            var endDay = date.getDate().toString();

            var firstDay = beginYear + '/' + (beginMonth[1] ? beginMonth : "0" + beginMonth[0]) + '/01';
            var lastDay = endYear + '/' + (endMonth[1] ? endMonth : "0" + endMonth[0]) + '/' + (endDay[1] ? endDay : "01");
            $('#Input_salaryYYMM').val(beginYear + (beginMonth[1] ? beginMonth : "0" + beginMonth[0]));
            $('#Input_salarySeq').val("2");
            $('#JQAttendDateBegin').datebox('setValue', firstDay);
            $('#JQAttendDateEnd').datebox('setValue', lastDay);
            $('#JQEffectDate').datebox('setValue', lastDay);
            $('#JQReportType').combobox('setValue', "1");
            $('#DEPT_CODE_Query').closest("td").attr('colspan', 4);
            $('#DEPTC_CODE_Query').closest("td").attr('colspan', 4);       
            $('#Input_employeeCode').closest("td").attr('colspan', 4);
            $('#Input_employeeName').closest("td").attr('colspan', 4);
            $("#Input_salaryYYMM").blur();
            $('#JQDeptType').combobox('setValue', "1");//部門總類
            //預設關閉銀行選項
            $('#JQBANK_TRANSFER_ID').closest('td').hide();
            //預設關閉語言選項
            $('#JQLANGUAGE').closest('td').hide();


            //預設關閉員工資料選項
            $('#EmployeeData').closest('td').hide();
            $('#h9').closest('td').hide();
            
            //$('#ExportExcel').prop("checked", true);
            //$('#ExportExcel').attr("disabled", true);

            $('#JQTextAreaMemo').val("請同仁遵循薪資保密之公司政策，若有任何對薪資方面的疑問，請逕洽人事單位。");

            $('<a>', { id: 'BT_DEPT', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#DEPT_CODE_Query').closest("td")).linkbutton();
            $('<a>', { id: 'BT_DEPTC', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#DEPTC_CODE_Query').closest("td")).linkbutton();
            $('<a>', { id: 'BT_WorkCode', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#JQTextArea_WorkCode').closest("td")).linkbutton();
            $('<a>', { id: 'BT_IDENTITY', class: 'easyui-linkbutton', 'data-options': "iconCls:'icon-search',plain:true" }).appendTo($('#JQIDENTITY').closest("td")).linkbutton();


            $("#Dialog_TransferBankDetail").hide();     //轉帳明細表
            $("#Dialog_CashDetail").hide();     //現金表

            $("#Input_salaryYYMM").blur(function () {
                var companyCode = $('#JQCompanyCode').combobox('getValue');
                var salaryYYMM = $('#Input_salaryYYMM').val();
                var salarySeq = $('#Input_salarySeq').val();

                if ($.jbIsYearMonthStr(salaryYYMM)) {
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Attend_Report_Share.HRM_ATTEND_CARD_DATA', //連接的Server端，command
                        data: "mode=method&method=" + "getAttendDate" + "&parameters=" + salaryYYMM.toString().substr(0, 4) + "," + salaryYYMM.toString().substr(4, 2) + "," + salaryYYMM.toString().substr(0, 4) + "," + salaryYYMM.toString().substr(4, 2) + "," + companyCode, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                            if (rows.length > 0) {
                                $('#JQAttendDateBegin').datebox('setValue', rows[0].attendBeginDate);
                                $('#JQAttendDateEnd').datebox('setValue', rows[0].attendEndDate);
                                $('#JQEffectDate').datebox('setValue', rows[0].attendEndDate);
                            }
                        }
                    });

                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_COMPANY', //連接的Server端，command
                        data: "mode=method&method=" + "getSalaryTransferDate" + "&parameters=" + salaryYYMM + "," + salarySeq, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                            if (rows.length > 0)
                                $('#JQTransferDate').datebox('setValue', rows[0].TRANSFER_DATE);
                        }
                    });
                }
            });

            $("#Input_salarySeq").blur(function () {
                var salaryYYMM = $('#Input_salaryYYMM').val();
                var salarySeq = $('#Input_salarySeq').val();
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_COMPANY', //連接的Server端，command
                    data: "mode=method&method=" + "getSalaryTransferDate" + "&parameters=" + salaryYYMM + "," + salarySeq, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 0)
                            $('#JQTransferDate').datebox('setValue', rows[0].TRANSFER_DATE);
                    }
                });
            });

            //$("#DEPT_CODE_Query").text('resize', { width: 200 });

            // 建立編制部門 dialog
            initQueryDeptDialog();

            //open 編制部門 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_DEPT').bind('click', function () {
                    openQueryDeptDialog();
                });
            });
            // 建立成本部門 dialog
            initQueryDeptcDialog();

            //open 成本部門 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_DEPTC').bind('click', function () {
                    openQueryDeptcDialog();
                });
            });
            $("#btnReport").click(function () {
                var attendDateBegin = $('#JQAttendDateBegin').datebox('getValue');
                var attendDateEnd = $('#JQAttendDateEnd').datebox('getValue');
                var effectDate = $('#JQEffectDate').datebox('getValue');
                var salaryYYMM = $('#Input_salaryYYMM').val();
                var salarySeq = $('#Input_salarySeq').val();
                //var transferDate = $('#JQTransferDate').datebox('getValue');
                var memo = $('#JQTextAreaMemo').val();
                var transferDate = $('#JQTransferDate').datebox('getValue');
                var reportType = $('#JQReportType').combobox('getValue');
                var identityType = $('#JQIDENTITY').val();//員別
                var workCode = $('#JQTextArea_WorkCode').val();//公司地點
                var deptType = $('#JQDeptType').combobox('getValue');//部門總類
                var BankTranserId = $('#JQBANK_TRANSFER_ID').combobox('getValue');//銀行

                if (!$.jbIsYearMonthStr(salaryYYMM)) {
                    flag = false;
                    if (salaryYYMM == "" || salaryYYMM == undefined)
                        alert("請輸入計薪年月");
                    else
                        alert("起始計薪年月格式錯誤, 請重新輸入");
                    $("#Input_salaryYYMM").focus();
                }
                else if (salarySeq == "" || salarySeq == undefined) {
                    flag = false;
                    alert("請輸入計薪期別");
                    $("#Input_salarySeq").focus();
                }
                else if (!$.jbIsDateStr(attendDateBegin)) {
                    flag = false;
                    if (attendDateBegin == "" || attendDateBegin == undefined)
                        alert("請輸入起始出勤日期");
                    else
                        alert("起始出勤日期格式錯誤, 請重新輸入");
                    $("#JQOvertimeDateBegin").datebox('textbox').focus();
                }
                else if (!$.jbIsDateStr(attendDateEnd)) {
                    flag = false;
                    if (attendDateEnd == "" || attendDateEnd == undefined)
                        alert("請輸入截止出勤日期");
                    else
                        alert("截止出勤日期格式錯誤, 請重新輸入");
                    $("#JQAttendDateEnd").datebox('textbox').focus();
                }
                else if (!$.jbIsDateStr(effectDate)) {
                    flag = false;
                    if (effectDate == "" || effectDate == undefined)
                        alert("請輸入異動截止日");
                    else
                        alert("異動截止日日期格式錯誤, 請重新輸入");
                    $("#JQEffectDate").datebox('textbox').focus();
                }
                else if (reportType == "7" && !$.jbIsDateStr(transferDate)) {
                    flag = false;
                    if (transferDate == "" || transferDate == undefined)
                        alert("請輸入轉帳日期");
                    else
                        alert("轉帳日期格式錯誤, 請重新輸入");
                    $("#JQTransferDate").datebox('textbox').focus();
                }
                else if (reportType == "10") {
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_COMPANY', //連接的Server端，command
                        data: "mode=method&method=" + "getMailParamerterData" + "&parameters=" + companyCode + "," + companyName + "," + employeeCode + "," + employeeName + "," + deptCode + "," + salaryYYMM + "," + salarySeq + "," + transferDate + "," + attendDateBegin + "," + attendDateEnd + "," + effectDate + "," + dataType + "," + reportType + "," + memo + "," + exportToExcel + "," + isdispalycompany, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                            if (rows.length == 0) {
                                flag = false;
                                alert("請先設定郵件相關參數");
                            }
                            else
                                flag = true;
                        }
                    });
                }
                else
                    flag = true;

                if (flag) {
                    var WhereString = "";
                    var employeeCode = $('#Input_employeeCode').val();
                    var employeeName = $('#Input_employeeName').val();
                    var checkedItems = $('#DG_DEPT').datagrid('getChecked');
                    var costcheckeItems = $('#DG_DEPTC').datagrid('getChecked');
                    var workCheckedItems = $('#DG_WorkCode').datagrid('getChecked');
                    var identityCheckedItems = $('#DG_identity').datagrid('getChecked');
                    
                    var deptCode = "";
                    if ($('#DEPT_CODE_Query').val().length > 0) {
                        $.each(checkedItems, function (index, item) {
                            item.DEPT_ID
                            if (index + 1 == checkedItems.length)
                                deptCode = deptCode + item.DEPT_ID;
                            else
                                deptCode = deptCode + item.DEPT_ID + ";";
                        });
                    }
                    //成本
                    var deptCost = "";
                    if ($('#DEPTC_CODE_Query').val().length > 0) {
                        $.each(costcheckeItems, function (index, item) {
                            item.DEPTC_ID
                            if (index + 1 == costcheckeItems.length)
                                deptCost = deptCost + item.DEPTC_ID;
                            else
                                deptCost = deptCost + item.DEPTC_ID + ";";
                        });
                    }
                    employeeCode = employeeCode.replace(/\,/g, ";");

                    //公司
                    var companyCode = $('#JQCompanyCode').combobox('getValue');
                    var companyName = $('#JQCompanyCode').combobox('getText');


                    //工作地點
                    //var workCode = $('#JQWorkCode').combobox('getValue');
                    var workCode = "";
                    if ($('#JQTextArea_WorkCode').val().length > 0) {
                        $.each(workCheckedItems, function (index, item) {
                            item.WORK_ID
                            if (index + 1 == workCheckedItems.length)
                                workCode = workCode + item.WORK_ID;
                            else
                                workCode = workCode + item.WORK_ID + ";";
                        });
                    }
                    //員別
                    var identityType = "";
                    if ($('#JQIDENTITY').val().length > 0) {
                        $.each(identityCheckedItems, function (index, item) {
                            item.IDENTITY_ID
                            if (index + 1 == identityCheckedItems.length)
                                identityType = identityType + item.IDENTITY_ID;
                            else
                                identityType = identityType + item.IDENTITY_ID + ";";
                        });
                    }

                    //資料內容
                    var dataType = $("input:radio[name='dataType']:checked").val();

                    //部門種類
                    var deptType = $('#JQDeptType').combobox('getValue');

                    //綁定報表是否顯示公司別
                    var isdispalycompany = document.getElementById("DisplayCompany").checked == true ? "Y" : "N";
                    var isdispalydept = document.getElementById("DisplayDept").checked == true ? "Y" : "N";

                    //綁定報表是否顯示表頭
                    var isdispalyTitle = document.getElementById("DisplayTitle").checked == true ? "Y" : "N";

                    //綁定報表是否顯示員工資料
                    var isEmployeeData = document.getElementById("EmployeeData").checked == true ? "Y" : "N";

                    var Language = "";
                    if (reportType == "9" || reportType == "10")
                        Language = $('#JQLANGUAGE').combobox('getValue');

                    if (reportType == "11")
                        var mailtitle = salaryYYMM.substr(0, 4) + "年" + salaryYYMM.substr(4, 2) + "月部門發放薪資明細表";
                    else
                        var mailtitle = salaryYYMM.substr(0, 4) + "年" + salaryYYMM.substr(4, 2) + "月薪資單";

                    var body = "本月薪資如附件請參考，謝謝。請使用PDF軟體閱讀，薪資檔案密碼是您的身分證號後四碼";
                    if (Language == "CN")
                        body = "本月薪資如附件請參考，謝謝。請使用PDF軟體閱讀，薪資檔案密碼是您的身分證號後四碼";
                    else if (Language == "US")
                        body = "Please refer to this month's salary as attached, thank you. The salary file password is the last four digits of your residence permit number.";
                    else if (Language == "CNUS") {
                        body = "本月薪資如附件請參考，謝謝。請使用PDF軟體閱讀，薪資檔案密碼是您的身分證號後四碼" + "\r\n";
                        body = body + "Please refer to this month's salary as attached, thank you. The salary file password is the last four digits of your residence permit number.";
                    }
                    else if (Language == "VN")
                        body = "Vui lòng tham khảo bảng lương tháng này như file đính kèm, cảm ơn bạn. Mật khẩu mở tập lương là bốn chữ số cuối của số giấy phép cư trú của bạn.";
                    else if (Language == "VN")
                        body = "Gaji bulan ini seperti lampiran berikut, terimakasih. Password file gaji adalah 4 angka terakhir ARC Anda."


                    //匯出excel
                    if ($('#ExportExcel').is(":checked") == true) {
                        var exportToExcel = "Y";
                        var colStruct = [];
                        var colItems = [];
                        $.messager.progress({ title: '發薪資料', msg: '發薪資料匯出 Excel, 請稍後 ...' });//進度條開始

                        $.ajax({
                            type: "POST",
                            url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_COMPANY', //連接的Server端，command
                            data: "mode=method&method=" + "getSalaryData" + "&parameters=" + companyCode + "," + companyName + "," + employeeCode + "," + employeeName + "," + deptCode + "," + salaryYYMM + "," + salarySeq + "," + transferDate + "," + attendDateBegin + "," + attendDateEnd + "," + effectDate + "," + dataType + "," + reportType + "," + memo + "," + exportToExcel + "," + isdispalycompany + "," + isdispalydept + "," + isdispalyTitle + "," + Language + "," + deptCost + "," + identityType + "," + workCode + "," + deptType + "," + isEmployeeData + "," + BankTranserId, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                            cache: false,
                            async: false,
                            success: function (data) {
                                var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                                if (rows.length == 0) {
                                    alert("無此區間範圍資料");
                                }
                                else {
                                    //This loop will extract the label from 1st index of on array
                                    var exportFields = [];
                                    for (var index in rows[0]) {
                                        exportFields.push({ field: index.trim(), title: index.trim() });
                                    }
                                    jsonExportExcel(exportFields, "0", "N", "N", "SalaryData", data)
                                }
                                $.messager.progress('close'); //進度條結束
                            }
                        });
                    }
                        //轉帳磁片(轉帳格式設定)
                    else if (reportType == "7") {
                        //var aData = $('#DataFormOutput').jbDataFormGetAFormData();
                        //var aDiv = $('#DataFormOutputMessageBox');
                        //var BankTranserId = $('#JQBANK_TRANSFER_ID').combobox('getValue');
                        $.ajax({
                            url: '../handler/JBHRISUseCaseHandler.ashx?' + $.param({ mode: 'BankMedia', remoteName: '_HRM_Salary_Normal_BankTransfer', method: 'Test' }),
                            //data: aData,
                            data: { "BankTranserId": BankTranserId, "CompanyCode": companyCode, "SalaryYYMM": salaryYYMM, "SalarySEQ": salarySeq, "EffectDate": effectDate, "TransferDate": transferDate },
                            type: 'POST',
                            async: true,
                            success: function (data) {
                                var Json = $.parseJSON(data);
                                //aDiv.empty();
                                if (Json.IsOK) {
                                    window.open('../handler/JqFileHandler.ashx?File=' + Json.ErrorMsg, 'download');
                                }
                                    //else $(aDiv).append(Json.ErrorMsg);
                                else alert(Json.ErrorMsg);
                            },
                            beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
                            complete: function () { $.messager.progress('close'); },
                            error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
                        });
                    }
                    else {
                        var url = "../JB_HRIS_Page/REPORT/SALARY/HRM_Salary_Report_SalaryReportView.aspx?companyCode=" + companyCode + "&companyName=" + companyName + "&employeeCode=" + employeeCode + "&deptCode=" + deptCode + "&employeeName=" + employeeName + "&salaryYYMM=" + salaryYYMM + "&salarySeq=" + salarySeq + "&transferDate=" + transferDate + "&attendDateBegin=" + attendDateBegin + "&attendDateEnd=" + attendDateEnd + "&effectDate=" + effectDate + "&dataType=" + dataType + "&reportType=" + reportType + "&memo=" + memo + "&exportToExcel=" + exportToExcel + "&isdispalycompany=" + isdispalycompany + "&mailtitle=" + mailtitle + "&body=" + body + "&isdispalydept=" + isdispalydept + "&isdispalyTitle=" + isdispalyTitle + "&Language=" + Language + "&deptCost=" + deptCost + "&identityType=" + identityType + "&workCode=" + workCode + "&deptType=" + deptType + "&isEmployeeData=" + isEmployeeData + "&BankTranserId=" + BankTranserId;

                        var height = $(window).height() - 20;
                        var width = $(window).width() - 20;
                        var dialog = $('<div/>')
                        .dialog({
                            draggable: false,
                            modal: true,
                            height: height,
                            width: width,
                            title: "Report"//,
                            //maximizable: true
                        });
                        $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));

                        if (reportType == "10" || reportType == "11") {
                            alert("檔案產生已完成");
                            dialog.dialog('close');
                        }
                        else {
                            dialog.dialog('open');

                        }
                    }
                }   //if (flag)
            }); //$("#btnReport").click

            //結繫預設報表內容是否顯示公司別
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_System_Share', //連接的Server端，command
                data: "mode=method&method=" + "getIsDisplayCompany" + "&parameters=" + 'test', //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.Table.length > 0) {
                        if (rows.Table[0].IS_DISPLAY_COMPANY == "Y")
                            $('#DisplayCompany').prop("checked", true);
                        if (rows.Table[0].IS_DISPLAY_DEPT == "Y")
                            $('#DisplayDept').prop("checked", true);
                    }
                }
            });

            $('#DisplayTitle').prop("checked", true);

            // 建立班別 dialog
            $("#Dialog_WorkCode").dialog(
                {
                    height: 400,
                    width: 400,
                    resizable: false,
                    modal: true,
                    title: "工作地點選項",
                    closed: true,
                    buttons: [{
                        text: '取消',
                        handler: function () { $("#Dialog_WorkCode").dialog("close") }
                    },
                    {
                        text: "確認",
                        handler: function () {
                            var workAddr = "";
                            var workCodeRows = $("#DG_WorkCode").datagrid("getRows");
                            var checkedItems = $('#DG_WorkCode').datagrid('getChecked');
                            var flag;

                            for (var k = 0; k < workCodeRows.length; k++) {
                                //判斷有勾選的 update 為 "Y"
                                flag = "N"
                                $.each(checkedItems, function (index, item) {
                                    if (workCodeRows[k].WORK_ID == item.WORK_ID) {
                                        workCodeRows[k].IS_SELECTED = "Y";
                                        flag = "Y";
                                        workAddr = workAddr + workCodeRows[k].WORK_CODE + "-" + workCodeRows[k].WORK_ADDR + ",";
                                    }
                                });
                                if (flag != "Y")
                                    workCodeRows[k].IS_SELECTED = "N";
                            }

                            $("#JQTextArea_WorkCode").val(workAddr);
                            $("#Dialog_WorkCode").dialog("close");
                        }
                    }]
                });


            //open 班別 dialog --使用 jQuery 绑定 easyui-linkbutton
            $(function () {
                $('#BT_WorkCode').bind('click', function () {
                    //if (getEditMode($('#dataFormMaster')) == "inserted") {
                    var workCodeList = $("#JQTextArea_WorkCode").val();
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_COMPANY', //連接的Server端，command
                        data: "mode=method&method=" + "getWorkCodeDialogData" + "&parameters=" + workCodeList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                            if (rows.length > 0) {
                                //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                                $('#DG_WorkCode').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                                $('#DG_WorkCode').datagrid('uncheckAll');
                                for (var j = 0; j < rows.length; j++) {
                                    if (rows[j].IS_SELECTED == "Y")
                                        $('#DG_WorkCode').datagrid('checkRow', j);
                                }
                            }
                        }
                    });
                    $("#Dialog_WorkCode").dialog("open");
                });
            });

            // 建立員別 identity dialog
            initQueryidentityDialog();


            // //open 員別 dialog --使用 jQuery 绑定 easyui-linkbutton

            $(function () {
                $('#BT_IDENTITY').bind('click', function () {
                    openQueryidentityDialog();
                });
            });

        }); //$(document).ready

        function genCheckBox(val) {
            if (val == "Y")
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }

        function getComanyCode() {
            //將combobox loaddata 的第一筆資料作為 default value
            var list = $('#JQCompanyCode').combobox('getData');
            $('#JQCompanyCode').combobox('setValue', list[0].COMPANY_CODE);
        }

        function checkExportExcel(row) {
            if (row.value == "7" || row.value == "9" || row.value == "10" || row.value == "11") //轉帳磁片||薪資單
            {
                $('#ExportExcel').prop("checked", false);
                $('#ExportExcel').attr("disabled", true);
            }
            else {
                $('#ExportExcel').prop("checked", false);
                $('#ExportExcel').attr("disabled", false);
            }
            //開關銀行選項
            if (row.value == "7" || row.value == "5") {
                $('#JQBANK_TRANSFER_ID').closest('td').show();
                //將combobox loaddata 的第一筆資料作為 default value
                var list = $('#JQBANK_TRANSFER_ID').combobox('getData');
                $('#JQBANK_TRANSFER_ID').combobox('setValue', list[0].BANK_TRANSFER_ID);
                //$('#JQTransferDate').datebox('disable');
            }
            else {
                $('#JQBANK_TRANSFER_ID').closest('td').hide();
                //$('#JQTransferDate').datebox('enable');
            }
            //開關語言選項
            if (row.value == "9" || row.value == "10") {
                $('#JQLANGUAGE').closest('td').show();
                //將combobox loaddata 的第一筆資料作為 default value
                var list = $('#JQLANGUAGE').combobox('getData');
            }
            else {
                $('#JQLANGUAGE').closest('td').hide();
            }

            //開關員工選項
            if (row.value == "5") {
                $('#EmployeeData').closest('td').show();
                $('#h9').closest('td').show();
              
            }
            else {
                $('#EmployeeData').closest('td').hide();
                $('#h9').closest('td').hide();
            }
        }
        function initQueryidentityDialog() {
            $("#Dialog_identity").dialog(
                {
                    height: 400,
                    width: 400,
                    resizable: false,
                    modal: true,
                    title: "員別選項",
                    closed: true,
                    buttons: [{
                        text: '取消',
                        handler: function () { $("#Dialog_identity").dialog("close") }
                    },
                    {
                        text: "確認",
                        handler: function () {
                            var entityName = "";
                            var entityRows = $("#DG_identity").datagrid("getRows");
                            var checkedItems = $('#DG_identity').datagrid('getChecked');
                            var flag;

                            for (var k = 0; k < entityRows.length; k++) {
                                //判斷有勾選的 update 為 "Y"
                                flag = "N"
                                $.each(checkedItems, function (index, item) {

                                    if (entityRows[k].IDENTITY_ID == item.IDENTITY_ID) {
                                        entityRows[k].IS_SELECTED = "Y";
                                        flag = "Y";
                                        entityName = entityName + entityRows[k].IDENTITY_CODE + "-" + entityRows[k].IDENTITY_CNAME + ",";
                                    }
                                });
                                if (flag != "Y")
                                    entityRows[k].IS_SELECTED = "N";
                            }

                            $("#JQIDENTITY").val(entityName);
                            $("#Dialog_identity").dialog("close");
                        }
                    }]
                });
        }
        function openQueryidentityDialog() {

            // $('#BT_IDENTITY').bind('click', function () {
            //if (getEditMode($('#dataFormMaster')) == "inserted") {
            var entityCodeList = $("#JQIDENTITY").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_Salary_Report_Share.HRM_IDENTITY', //連接的Server端，command
                data: "mode=method&method=" + "getidentityCodeDialogData" + "&parameters=" + entityCodeList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                    if (rows.length > 0) {
                        //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                        $('#DG_identity').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                        $('#DG_identity').datagrid('uncheckAll');
                        for (var j = 0; j < rows.length; j++) {
                            if (rows[j].IS_SELECTED == "Y")
                                $('#DG_identity').datagrid('checkRow', j);
                        }
                    }
                }
            });
            $("#Dialog_identity").dialog("open");
            //});

        }
    </script>

    <style type="text/css">
        #textarea {
            height: 48px;
            width: 478px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <div id="Dialog_createCardData">
            <div class="div_RelativeLayout">
                <div id="Div_Area">
                    <table>
                        <tr>
                            <td>
                                <h5 id="companyCode" class="h3_Caption">公司</h5>
                            </td>
                            <td>
                                <JQTools:JQComboBox ID="JQCompanyCode" runat="server" CheckData="True" DisplayMember="COMPANY_NAME" RemoteName="_HRM_Salary_Report_Share.HRM_COMPANY" ValueMember="COMPANY_CODE" PanelHeight="50">
                                </JQTools:JQComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="employeeCode" class="h3_Caption">員工工號</h5>
                            </td>
                            <td>
                                <JQTools:JQTextArea ID="Input_employeeCode" runat="server" Width="450px" Height="20px" />
                                <%--<input id="Input_employeeCode" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="employeeName" class="h3_Caption">員工姓名</h5>
                            </td>
                            <td>
                                <JQTools:JQTextArea ID="Input_employeeName" runat="server" Width="450px" Height="20px" />
                                <%--<input id="Input_employeeName" type="text" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="dept" class="h3_Caption">編制部門</h5>
                            </td>
                            <td>
                                <JQTools:JQTextArea ID="DEPT_CODE_Query" runat="server" Width="450px" Height="40px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="H6" class="h3_Caption">成本部門</h5>
                            </td>
                            <td>
                                <JQTools:JQTextArea ID="DEPTC_CODE_Query" runat="server" Width="450px" Height="40px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="workCode" class="h3_Caption">工作地點</h5>
                            </td>
                            <td >
                                <JQTools:JQTextArea ID="JQTextArea_WorkCode" runat="server" Width="150px" />
                            </td>
                             <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H7" class="h3_Caption">員別</h5>
                            </td>
                              <td >
                                <JQTools:JQTextArea ID="JQIDENTITY" runat="server" Width="150px" />
                            </td>
                            <%-- <td>
                               <JQTools:JQComboBox ID="JQIDENTITY" runat="server" DisplayMember="IDENTITY_CNAME" RemoteName="_HRM_Employee_Report_Share.HRM_IDENTITY" ValueMember="IDENTITY_ID" PanelHeight="200">
                                </JQTools:JQComboBox>
                            </td>--%>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="salaryYYMM" class="h3_Caption">計薪年月</h5>
                            </td>
                            <td>
                                <input id="Input_salaryYYMM" type="text" />
                            </td>
                            <td>
                                <h5 id="salarySeq" style="text-align: right; padding: 0px; margin: 0px; width: 30px">期別</h5>
                            </td>
                            <td>
                                <input id="Input_salarySeq" type="text" style="width: 20px" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="TransferDate" class="h3_Caption">轉帳日期</h5>
                            </td>
                            <td>
                                <JQTools:JQDateBox ID="JQTransferDate" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="EffectDate" class="h3_Caption">異動截止日</h5>
                            </td>
                            <td>
                                <JQTools:JQDateBox ID="JQEffectDate" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <h5 id="attendDateBegin" class="h3_Caption">出勤日期</h5>
                            </td>
                            <td>
                                <JQTools:JQDateBox ID="JQAttendDateBegin" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                            <td>
                                <h5 id="attendDateEnd" style="text-align: center; padding: 0px; margin: 0px; width: 20px">至</h5>
                            </td>
                            <td>
                                <JQTools:JQDateBox ID="JQAttendDateEnd" Format="DateTime" runat="server" Width="200px" ShowSeconds="False" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H1" class="h3_Caption">資料內容</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <input id="Radio1" checked="checked" name="dataType" type="radio" value="all" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <h5 id="h5_Radio1">全部</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <input id="Radio2" name="dataType" type="radio" value="direct" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <h5 id="h5_Radio2">直接</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <input id="Radio3" name="dataType" type="radio" value="indirect" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <h5 id="h5_Radio3">間接</h5>
                            </td>
                            <td>
                                <input id="Radio5" name="dataType" type="radio" value="taiwan" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <h5 id="h2">本勞</h5>
                            </td>
                            <td>
                                <input id="Radio4" name="dataType" type="radio" value="foreigner" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px; width: 30px">
                                <h5 id="h5_Radio4">外勞</h5>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H4" class="h3_Caption">顯示方式</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <h5 id="h5_DisplayCompany" class="h3_Caption">顯示公司別</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <input id="DisplayCompany" type="checkbox" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <h5 id="h5_DisplayDept" class="h3_Caption">顯示部門別</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <input id="DisplayDept" type="checkbox" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <h5 id="h5_Checkbox1" class="h3_Caption">匯出Excel</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <input id="ExportExcel" name="exportExcel" type="checkbox" />
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <h5 id="h5_DisplayTitle" class="h3_Caption">顯示表頭</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <input id="DisplayTitle" name="displayTitle" type="checkbox" />
                             </td>

                           <td style="text-align: center; padding: 0px; margin: 0px;">
                                <h5 id="h9" class="h3_Caption">顯示員工資料</h5>
                            </td>
                            <td style="text-align: center; padding: 0px; margin: 0px;">
                                <input id="EmployeeData" name="EmployeeData" type="checkbox" />
                            </td>

                        </tr>
                    </table>
                    <table>
                         <tr>
                            <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H8" class="h3_Caption">部門種類</h5>
                            </td>
                            <td colspan="8">
                                <JQTools:JQComboBox ID="JQDeptType" runat="server" CheckData="True" DisplayMember="" RemoteName="" ValueMember="" PanelHeight="200" OnSelect="">
                                    <Items>
                                        <JQTools:JQComboItem Selected="False" Text="成本部門" Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text="編制部門" Value="2" />
                                    </Items>
                                </JQTools:JQComboBox>
                            </td>
                             
                        </tr>
                        <tr>
                            <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H3" class="h3_Caption">報表種類</h5>
                            </td>
                            <td>
                                <JQTools:JQComboBox ID="JQReportType" runat="server" CheckData="True" DisplayMember="" RemoteName="" ValueMember="" PanelHeight="200" OnSelect="checkExportExcel">
                                    <Items>
                                        <%-- <JQTools:JQComboItem Selected="False" Text="薪資明細表--編制" Value="1" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資明細表--成本" Value="2" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資明細表--編制合併" Value="21" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資明細表--成本合併" Value="22" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資彙總表--編制" Value="3" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資彙總表--成本" Value="4" />--%>
                                         <JQTools:JQComboItem Selected="False" Text="薪資明細表" Value="1" />
                                         <JQTools:JQComboItem Selected="False" Text="薪資明細表--合併" Value="21" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資彙總表" Value="3" />
                                        <JQTools:JQComboItem Selected="False" Text="轉帳明細表" Value="5" />
                                        <JQTools:JQComboItem Selected="False" Text="現金表" Value="6" />
                                        <JQTools:JQComboItem Selected="False" Text="轉帳磁片" Value="7" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資人數" Value="8" />
                                        <%--<JQTools:JQComboItem Selected="False" Text="公司職工所得表" Value="12" />
                                        <JQTools:JQComboItem Selected="False" Text="部門職工所得表" Value="13" />--%>
                                        <JQTools:JQComboItem Selected="False" Text="薪資單" Value="9" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資單-Mail員工" Value="10" />
                                        <JQTools:JQComboItem Selected="False" Text="薪資單-Mail主管" Value="11" />
                                    </Items>
                                </JQTools:JQComboBox>
                            </td>
                            <td>
                                <JQTools:JQComboBox ID="JQBANK_TRANSFER_ID" runat="server" CheckData="True" DisplayMember="BANK_TRANSFER_NAME" RemoteName="_HRM_Salary_Report_Share.cb_HRM_SALARY_BANK_TRANSFER" ValueMember="BANK_TRANSFER_ID" PanelHeight="200">
                                </JQTools:JQComboBox>
                            </td>
                            <td>
                                <JQTools:JQComboBox ID="JQLANGUAGE" runat="server" CheckData="True" DisplayMember="" RemoteName="" ValueMember="" PanelHeight="200">
                                    <Items>
                                        <JQTools:JQComboItem Selected="true" Text="中文" Value="CN" />
                                        <JQTools:JQComboItem Selected="False" Text="英文" Value="US" />
                                        <JQTools:JQComboItem Selected="False" Text="中英對照" Value="CNUS" />
                                        <JQTools:JQComboItem Selected="False" Text="越南文" Value="VN" />
                                        <JQTools:JQComboItem Selected="False" Text="印尼文" Value="IN" />
                                    </Items>
                                </JQTools:JQComboBox>
                            </td>

                             
                            
                        </tr>
                        <tr>
                            <td style="text-align: right; padding: 0px; margin: 0px;">
                                <h5 id="H5" class="h3_Caption">提示文字</h5>
                            </td>
                            <td colspan="12">
                                <JQTools:JQTextArea ID="JQTextAreaMemo" runat="server" Height="50px" Width="400px" />
                            </td>
                        </tr>
                    </table>
                </div>
                <h5 id="btnReport" class="Btn_Decide">報表列印</h5>
            </div>
        </div>

        <!-- DEPT dialog對話框內容的 DIV -->
        <div id="Dialog_Dept">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_DEPT" runat="server" AutoApply="False" DataMember="HRM_CURRENTLY_DEPT" Pagination="False" ParentObjectID="" RemoteName="_HRM_Attend_Share.HRM_CURRENTLY_DEPT" Title="編制部門選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="DEPT_ID" Editor="text" FieldName="EMPLOYEE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="編制部門代碼" Editor="text" FieldName="DEPT_CODE" Width="90" Visible="True" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="編制部門名稱" Editor="text" FieldName="DEPT_CNAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>

         <!-- DEPTC dialog對話框內容的 DIV -->
        <div id="Dialog_Deptc">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_DEPTC" runat="server" AutoApply="False" DataMember="HRM_CURRENTLY_DEPTC" Pagination="False" ParentObjectID="" RemoteName="_HRM_Attend_Share.HRM_CURRENTLY_DEPTC" Title="成本部門選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None" OnLoadSuccess="">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="DEPTC_ID" Editor="text" FieldName="EMPLOYEE_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="成本部門代碼" Editor="text" FieldName="DEPTC_CODE" Width="90" Visible="True" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="成本部門名稱" Editor="text" FieldName="DEPTC_CNAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
        <!-- WorkCode dialog對話框內容的 DIV -->
        <div id="Dialog_WorkCode">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_WorkCode" runat="server" AutoApply="False" DataMember="HRM_WORKPLACE" Pagination="False" ParentObjectID="" RemoteName="_HRM_Employee_Report_Share.HRM_WORKPLACE" Title="工作地點選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="WORK_ID" Editor="text" FieldName="WORK_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工作地點代碼" Editor="text" FieldName="WORK_CODE" Width="90" Visible="True" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工作地點名稱" Editor="text" FieldName="WORK_ADDR" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>

         <!-- IDENTITY dialog對話框內容的 DIV -->
        <div id="Dialog_identity">
            <div class="div_RelativeLayout">
                <JQTools:JQDataGrid ID="DG_identity" runat="server" AutoApply="False" DataMember="HRM_IDENTITY" Pagination="False" ParentObjectID="" RemoteName="_HRM_Employee_Report_Share.HRM_IDENTITY" Title="員別選項" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="50" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="360px" CheckOnSelect="True" RecordLock="False" ColumnsHibeable="False" RecordLockMode="None">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="IDENTITY_ID" Editor="text" FieldName="IDENTITY_ID" Width="90" Visible="False" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員別代碼" Editor="text" FieldName="IDENTITY_CODE" Width="90" Visible="True" Frozen="False" MaxLength="4" QueryCondition="" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員別名稱" Editor="text" FieldName="IDENTITY_CNAME" Width="150" ReadOnly="True" Frozen="False" MaxLength="4" QueryCondition="" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="已選擇" Editor="checkbox" FieldName="IS_SELECTED" Width="40" Frozen="False" MaxLength="60" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" EditorOptions="on:'Y',off:'N'" FormatScript="genCheckBox" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
    </form>
</body>
</html>
