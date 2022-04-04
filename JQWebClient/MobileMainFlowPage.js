

function gotoMenu() {
    //var urlPrdfix = "";
    //if (isSubPath)
    //    urlPrdfix = "../";
    $.mobile.changePage("MobileMainPage.aspx", { transition: "slideup", role: "page" })
}

function gotoInbox(flag) {
    var urlPrdfix = "";
    if (flag)
        urlPrdfix = "../";
    $.mobile.changePage(urlPrdfix + "MobileMainFlowPageInbox.aspx", { transition: "slideup", role: "page" })
    //$.fn.flow("initializeInbox");
}

function gotoOutbox() {
    $.mobile.changePage("MobileMainFlowPageOutbox.aspx", { transition: "slideup", role: "page" })
    //$.fn.flow("initializeOutbox");
}

function gotoNotify() {
    $.mobile.changePage("MobileMainFlowPageNotify.aspx", { transition: "slideup", role: "page" })
    //$.fn.flow("initializeNotify");
}

function gotoDelay() {
    $.mobile.changePage("MobileMainFlowPageDelay.aspx", { transition: "slideup", role: "page" })
    //$.fn.flow("initializeDelay");
}


$.fn.flow = function (methodName, value) {
    if (typeof methodName == "string") {
        var method = $.fn.flow.methods[methodName];
        if (method) {
            return method(this, value);
        }
    }
    else if (typeof methodName == "object") {
        this.each(function () {
            $(this).flow('initialize');
            if (!$(this).hasClass($.fn.flow.class)) {
                $(this).addClass($.fn.flow.class)
            }
        });
    }
};

$.fn.flow.class = 'info-flow';

$.fn.flow.defaults = {
    placeholderText: "Search data"
};

$.fn.flow.methods = {
    initialize: function (jq) {

    },
    initializeInbox: function (jq) { initializeInbox(); $(jq).flow('initializeCount', 'Inbox'); },
    initializeOutbox: function (jq) { initializeOutbox(); $(jq).flow('initializeCount', 'Outbox'); },
    initializeNotify: function (jq) {

        //if ($("#aNotify_Notify").attr("selected") == "selected") {
        //    return;
        //}
        //else {
        //    $("#aNotify_Notify").attr("selected", "selected");
        //}
        flowUIText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/ToDoListColumns');
        flowUITexts = flowUIText.split(',');
        var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var titleTexts = titleText.split(';');
        $("#aInbox_Notify").setText(titleTexts[2]);
        $("#aOutbox_Notify").setText(titleTexts[3]);
        $("#aNotify_Notify").setText(titleTexts[16]);
        $("#aDelay_Notify").setText(titleTexts[4]);
        $("#aMenu_Notify").setText($.sysmsg('getValue', 'JQWebClient/info-main/menuText'));
        $("#aWorkFlow_Notify").setText($.sysmsg('getValue', 'JQWebClient/info-main/flowText'));
        $(jq).flow('initializeCount', 'Notify');

        var afterGetDataNotify = function (data) {
            if (data) {
                var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
                var titleTexts = titleText.split(';');

                data = eval('(' + data + ')');
                data = data.rows;
                if (data.length == 0 && filter.page != 1) {
                    filter.page--;
                    return;
                }

                var flowColumnText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/ToDoListColumns');
                var flowColumnTexts = flowColumnText.split(',');
                $("#ulNotify_Notify").empty();
                var titleLi = "<li id=\"ulNotifyTitle\" data-role=\"list-divider\">" + titleTexts[16] + "<span class=\"ui-li-count\">" + data.length + "</span></li>"
                $(titleLi).appendTo($("#ulNotify_Notify"));
                for (var i = 0; i < data.length; i++) {
                    var urlParam = "?IsWorkflow=1";
                    var selectedRow = data[i];
                    for (var field in selectedRow) {
                        urlParam += "&" + field + "=" + selectedRow[field];
                    }
                    var url = selectedRow.WEBFORM_NAME.replace(".", "/") + ".aspx" + urlParam;
                    if (selectedRow.FORM_NAME && selectedRow.FORM_NAME.indexOf('M:') == 0) {
                        url = selectedRow.FORM_NAME.substring(2).replace(".", "/") + ".aspx" + urlParam;
                    }
                    var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
                    var aId = "a" + i;
                    newLi += "<a id=\"" + aId + "\" class=\"popupSource\">";
                    //newLi += "<a href=\"" + url + "\">";
                    newLi += "<h2>";
                    if (data[i].FLOWIMPORTANT == "1")
                        newLi += "<img src=\"Image/WorkflowIcon/Important.png\"></img>";
                    if (data[i].FLOWURGENT == "1")
                        newLi += "<img src=\"Image/WorkflowIcon/urgent.png\"></img>";
                    newLi += "<p class=\"flowtitle\">" + flowColumnTexts[0] + ": " + data[i].FLOW_DESC + "</p>";
                    newLi += "</h2>";
                    newLi += "<table style=\"width: 100%\">";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[1] + ": " + data[i].D_STEP_ID + "</p></td>";
                    newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[3] + ": " + data[i].SENDTO_NAME + "</p></td></tr>";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[6] + ": " + data[i].STATUS + "</p></td>";
                    newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[2] + ": " + data[i].FORM_PRESENT_CT + "</p></td></tr>";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td>";
                    newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[5] + ": " + data[i].UPDATE_WHOLE_TIME + "</p></td></tr>";
                    var link = "";
                    if (data[i].ATTACHMENTS != null && data[i].ATTACHMENTS != "") {
                        var lstAttachments = data[i].ATTACHMENTS.split(';');
                        for (var j = 0; j < lstAttachments.length; j++) {
                            if (lstAttachments[j] != "" && lstAttachments[j] != "null") {
                                var realFileName = lstAttachments[j];
                                var fileName = realFileName.replace(/__/g, "&nbsp;");
                                var href = "WorkflowFiles/" + realFileName;
                                link += "<A id='" + "ATTACHMENTS" + j + "' href='" + href + "' target='_blank' class='" + realFileName + "' >" + fileName + "</A>&nbsp&nbsp";
                            }
                        }
                    }
                    newLi += "<tr><td colspan=\"2\" style=\"width: 100%\"><p class=\"ui-li-desc\">" + flowColumnTexts[11] + ": " + link + "</p></td></tr>";
                    newLi += "</table>";
                    newLi += "</a>";
                    //                    newLi += "<a class=\"popupSource\"  data-mini=\"true\" >";
                    //                    newLi += "</a>";
                    newLi += "</li>";

                    $(newLi).data('row', selectedRow).data('url', url).appendTo($("#ulNotify_Notify"));

                }

                $("#ulNotify_Notify").listview("refresh");
                $("#ulNotify_Notify").find(".flowtitle").click(function () {
                    var url = $(this).closest('li').data('url');
                    window.location.href = url;
                    return false;
                });
                $("#ulNotify_Notify").find(".popupSource").click(function () {
                    $("#popupMenu_Notify").popup('open', { positionTo: $(this) });
                    var selectedRow = $(this).closest('li').data('row');
                    var params = {};
                    params.Type = "Workflow";
                    params.Active = "FlowDelete";
                    params.LISTID = selectedRow.LISTID;
                    params.FLOWPATH = selectedRow.FLOWPATH;
                    params.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                    params.FORM_KEYS = selectedRow.FORM_KEYS;
                    params.FORM_PRESENTATION = selectedRow.FORM_PRESENTATION;
                    params.STATUS = selectedRow.STATUS;
                    params.SENDTO_ID = selectedRow.SENDTO_ID;

                    var url = $(this).closest('li').data('url');
                    $("#popupMenu_Notify").find('a.open').attr('href', url); ;
                    $("#popupMenu_Notify").find('a.delete').unbind('click').bind('click', function () {
                        $("#popupMenu_Notify").popup({
                            afterclose: function (event, ui) {
                                flowDelete(params);
                            }
                        });
                        $("#popupMenu_Notify").popup('close');
                    })
                });

                var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
                var flowUITexts = flowUIText.split(';');
                $("#popupMenu_Notify").find('a.open').html(flowUITexts[9]);
                $("#popupMenu_Notify").find('a.delete').html(flowUITexts[15]);
            }
        }

        var filter = {};
        filter.Type = "Notify";
        filter.rows = 10;
        filter.page = 1;
        getFlowData(filter, afterGetDataNotify);

        $("#divNotify_Notify").find("a.grid-previous").click(function () {
            if (filter.page > 1) {
                filter.page--;
                getFlowData(filter, afterGetDataInbox);
            }
        });
        $("#divNotify_Notify").find("a.grid-next").click(function () {
            filter.page++;
            getFlowData(filter, afterGetDataInbox);
        });
    },
    initializeDelay: function (jq) { initializeDelay(0); $(jq).flow('initializeCount', 'Delay'); },
    initializeCount: function (jq, part) {
        if ($("#aInbox_" + part).length > 0) {
            $.ajax({
                type: "POST",
                url: 'handler/SystemHandle_Flow.ashx',
                data: { Type: "Count" },
                dataType: 'json',
                cache: false,
                async: false,
                success: function (data) {
                    $("#aInbox_" + part).find('.ui-btn-text').html($("#aInbox_" + part).find('.ui-btn-text').html() + '(' + data.Do + ')');
                    $("#aOutbox_" + part).find('.ui-btn-text').html($("#aOutbox_" + part).find('.ui-btn-text').html() + '(' + data.History + ')');
                    $("#aNotify_" + part).find('.ui-btn-text').html($("#aNotify_" + part).find('.ui-btn-text').html() + '(' + data.Notify + ')');
                    $("#aDelay_" + part).find('.ui-btn-text').html($("#aDelay_" + part).find('.ui-btn-text').html() + '(' + data.Delay + ')');
                }
            });
        }
    },
    initializeMobileFormSubmit: function (jq) {

        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $("#btnOk_FromSubmit").setText(flowUITexts[6]);
        $("#btnCancel_FromSubmit").setText(flowUITexts[13]);
        $("#btnPreview_FromSubmit").setText(flowUITexts[14]);
        $("#btnUploadFile_FromSubmit").setText(flowUITexts[12]);

        $("#lImportant_FromSubmit").setText(flowUITexts[1]);
        $("#lUrgent_FromSubmit").setText(flowUITexts[2]);
        $("#lSenderRole_FromSubmit").setText(flowUITexts[5]);
        $("#suggestionView_FromSubmit").setText(flowUITexts[3]);
        $("#lhistoryView_FromSubmit").setText(flowUITexts[16]);

        $("#hHead_MobileFromSubmit").setText($.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText').split(';')[16]);
        var uploadButtonText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText').split(',');
        $("#hUploadHead_MobileFromSubmit").setText(uploadButtonText[12]);
        $("#btnUpload_FromSubmit").setText($.sysmsg('getValue', 'JQWebClient/fileuploadbutton'));
        $("#btnClose_FromSubmit").setText(uploadButtonText[13]);

        var selectedRow = reunionSYS_TODOLIST();
        initFileUpload(selectedRow, "ulFiles_FromSubmit");
        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "ddlRoles", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                $("#ddlRoles_FromSubmit").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#ddlRoles_FromSubmit").append($.createOption(data[i].GROUPNAME, data[i].GROUPID));
                }
                $("#ddlRoles_FromSubmit").selectmenu("refresh");
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                if (data != "") {
                    data = eval('(' + data + ')');
                    flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                    flowCommentTableHeaders = flowCommentTableHeader.split(',');
                    var table = "<table id=\"gdvHis_FromSubmit\" class=\"info-datagrid table-stripe infolight-breakpoint ui-table ui-table-reflow\" data-role=\"table\" infolight-options=\"remoteName:'',tableName:'',allowAdd:true,allowUpdate:true,allowDelete:true,toolItemsPosition:'bottom'\" data-mode=\"reflow\">";
                    var thead = "<thead><tr>";
                    thead += "<th></th>";
                    thead += "<th infolight-options=\"field:'S_STEP_ID',width:120,align:''\">" + flowCommentTableHeaders[0] + "</th>";
                    //thead += "<th infolight-options=\"field:'USER_ID',width:120,align:''\">" + flowCommentTableHeaders[1] + "</th>";
                    thead += "<th infolight-options=\"field:'USERNAME',width:120,align:''\">" + flowCommentTableHeaders[2] + "</th>";
                    thead += "<th infolight-options=\"field:'STATUS',width:120,align:''\">" + flowCommentTableHeaders[3] + "</th>";
                    thead += "<th infolight-options=\"field:'REMARK',width:120,align:''\">" + flowCommentTableHeaders[4] + "</th>";
                    thead += "<th infolight-options=\"field:'UPDATE_WHOLE_TIME',width:120,align:''\">" + flowCommentTableHeaders[6] + "</th>";
                    //thead += "<th infolight-options=\"field:'UPDATE_TIME',width:120,align:''\">" + flowCommentTableHeaders[7] + "</th>";
                    thead += "</tr></thead>";
                    table += thead;
                    //$(thead).appendTo($("#gdvHis_FromSubmit"));
                    var tbody = "<tbody>";
                    for (var i = 0; i < data.length; i++) {
                        tbody += "<tr index=\"" + i + "\">";
                        tbody += "<td></td>";
                        tbody += "<td style=\"width: auto;padding:2px\" field=\"S_STEP_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[0] + "</b>" + data[i].S_STEP_ID + "</td>"
                        //tbody += "<td style=\"width: auto;\" field=\"USER_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[1] + "</b>" + data[i].USER_ID + "</td>"
                        tbody += "<td style=\"width: auto;padding:2px\" field=\"USERNAME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[2] + "</b>" + data[i].USERNAME + "</td>"
                        tbody += "<td style=\"width: auto;padding:2px\" field=\"STATUS\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[3] + "</b>" + data[i].STATUS + "</td>"
                        tbody += "<td style=\"width: auto;padding:2px\" field=\"REMARK\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[4] + "</b>" + decodeURIComponent(data[i].REMARK) + "</td>"
                        tbody += "<td style=\"width: auto;padding:2px\" field=\"UPDATE_WHOLE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[6] + "</b>" + data[i].UPDATE_WHOLE_TIME + "</td>"
                        //tbody += "<td style=\"width: auto;\" field=\"UPDATE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[7] + "</b>" + data[i].UPDATE_TIME + "</td>"
                        tbody += "</tr>"
                    }
                    tbody += "</tbody>";
                    table += tbody;
                    table += "</table>"
                    $(table).appendTo($("#historyView_FromSubmit"));

                    //$(".ui-li-count")[0].innerHTML = data.length;
                    //for (var i = 0; i < data.length; i++) {
                    //    var newLi = "<li class=\"ui-li ui-li-static ui-btn-up-c\">";
                    //    //newLi += "<div data-role=\"controlgroup\" data-type=\"horizontal\" data-mini=\"true\">";
                    //    //newLi += "<a href=\"#\" data-role=\"button\" data-iconpos=\"notext\" data-icon=\"plus\" data-theme=\"b\">Add</a>";
                    //    //newLi += "</div>";
                    //    newLi += "<h2 class=\"ui-li-heading\">" + flowCommentTableHeaders[0] + ": " + data[i].S_STEP_ID + "</h2>";
                    //    newLi += "<table style=\"width: 100%\">";
                    //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[1] + ": " + data[i].USER_ID + "</p></td>";
                    //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[2] + ": " + data[i].USERNAME + "</p></td></tr>";
                    //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[3] + ": " + data[i].STATUS + "</p></td>";
                    //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[4] + ": " + data[i].REMARK + "</p></td></tr>";
                    //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[6] + ": " + data[i].UPDATE_DATE + "</p></td>";
                    //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[7] + ": " + data[i].UPDATE_TIME + "</p></td></tr>";
                    //    newLi += "</table>"
                    //    newLi += "</li>"
                    //    $(newLi).appendTo($("#gdvHis_FromSubmit"));
                    //}
                    //$("#gdvHis_FromSubmit").listview("refresh");
                }
                $("#gdvHis_FromSubmit").datagrid({});
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormApprove: function (jq) {
        $.mobile.ajaxEnabled = true;
        $.mobile.ajaxFormsEnabled = true;

        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');

        $("#btnOk_FromApprove").setText(flowUITexts[6]);
        $("#btnCancel_FromApprove").setText(flowUITexts[13]);
        $("#btnPreview_FromApprove").setText(flowUITexts[14]);
        $("#btnUploadFile_FromApprove").setText(flowUITexts[12]);

        $("#lImportant_FromApprove").setText(flowUITexts[1]);
        $("#lUrgent_FromApprove").setText(flowUITexts[2]);
        $("#lSenderRole_FromApprove").setText(flowUITexts[5]);
        $("#suggestionView_FromApprove").setText(flowUITexts[3]);
        $("#lhistoryView_FromApprove").setText(flowUITexts[16]);

        $("#hHead_MobileFormApprove").setText($.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText').split(';')[17]);
        var uploadButtonText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText').split(',');
        $("#hUploadHead_MobileFormApprove").setText(uploadButtonText[12]);
        $("#btnUpload_FromApprove").setText($.sysmsg('getValue', 'JQWebClient/fileuploadbutton'));
        $("#btnClose_FromApprove").setText(uploadButtonText[13]);
        var selectedRow = reunionSYS_TODOLIST();
        if (selectedRow.FLOWIMPORTANT == "1") $("#chkImportant_FromApprove").prop("checked", true).checkboxradio("refresh");
        if (selectedRow.FLOWURGENT == "1") $("#chkUrgent_FromApprove").prop("checked", true).checkboxradio("refresh");
        initFileUpload(selectedRow, "ulFiles_FromApprove");
        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "ddlRoles", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                $("#ddlRoles_FromApprove").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#ddlRoles_FromApprove").append($.createOption(data[i].GROUPNAME, data[i].GROUPID));
                }
                $("#ddlRoles_FromApprove").selectmenu("refresh");
                if (selectedRow != undefined && selectedRow.SENDTO_KIND == "1") {
                    $('#ddlRoles_FromApprove').val(selectedRow.SENDTO_ID);
                    $('#ddlRoles_FromApprove').selectmenu('disable');
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');

                //$(window).width() > 540

                flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                flowCommentTableHeaders = flowCommentTableHeader.split(',');
                var table = "<table id=\"gdvHis_FromApprove\" class=\"info-datagrid table-stripe infolight-breakpoint ui-table ui-table-reflow\" data-role=\"table\" infolight-options=\"remoteName:'',tableName:'',allowAdd:true,allowUpdate:true,allowDelete:true,toolItemsPosition:'bottom'\" data-mode=\"reflow\">";
                var thead = "<thead><tr>";
                thead += "<th></th>";
                thead += "<th infolight-options=\"field:'S_STEP_ID',width:120,align:''\">" + flowCommentTableHeaders[0] + "</th>";
                //thead += "<th infolight-options=\"field:'USER_ID',width:120,align:''\">" + flowCommentTableHeaders[1] + "</th>";
                thead += "<th infolight-options=\"field:'USERNAME',width:120,align:''\">" + flowCommentTableHeaders[2] + "</th>";
                thead += "<th infolight-options=\"field:'STATUS',width:120,align:''\">" + flowCommentTableHeaders[3] + "</th>";
                thead += "<th infolight-options=\"field:'REMARK',width:120,align:''\">" + flowCommentTableHeaders[4] + "</th>";
                thead += "<th infolight-options=\"field:'UPDATE_WHOLE_TIME',width:120,align:''\">" + flowCommentTableHeaders[6] + "</th>";
                //thead += "<th infolight-options=\"field:'UPDATE_TIME',width:120,align:''\">" + flowCommentTableHeaders[7] + "</th>";
                thead += "</tr></thead>";
                table += thead;
                //$(thead).appendTo($("#gdvHis_FromApprove"));
                var tbody = "<tbody>";
                for (var i = 0; i < data.length; i++) {
                    tbody += "<tr index=\"" + i + "\">";
                    tbody += "<td></td>";
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"S_STEP_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[0] + "</b>" + data[i].S_STEP_ID + "</td>"
                    //tbody += "<td style=\"width: auto;\" field=\"USER_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[1] + "</b>" + data[i].USER_ID + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"USERNAME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[2] + "</b>" + data[i].USERNAME + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"STATUS\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[3] + "</b>" + data[i].STATUS + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"REMARK\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[4] + "</b>" + decodeURIComponent(data[i].REMARK) + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"UPDATE_WHOLE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[6] + "</b>" + data[i].UPDATE_WHOLE_TIME + "</td>"
                    //tbody += "<td style=\"width: auto;\" field=\"UPDATE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[7] + "</b>" + data[i].UPDATE_TIME + "</td>"
                    tbody += "</tr>"
                }
                tbody += "</tbody>";
                table += tbody;
                table += "</table>"
                $(table).appendTo($("#historyView_FromApprove"));
                $("#gdvHis_FromApprove").datagrid({});
                //$("#gdvHis_FromApprove").datalist('loadData', data);

                ////$("#gdvHis_FromApprove").empty();
                ////$(".ui-li-count")[0].innerHTML = data.length;
                //for (var i = 0; i < data.length; i++) {
                //    var newLi = "<li class=\"ui-li ui-li-static ui-btn-up-c\">";
                //    //newLi += "<div data-role=\"controlgroup\" data-type=\"horizontal\" data-mini=\"true\">";
                //    //newLi += "<a href=\"#\" data-role=\"button\" data-iconpos=\"notext\" data-icon=\"plus\" data-theme=\"b\">Add</a>";
                //    //newLi += "</div>";
                //    newLi += "<h2 class=\"ui-li-heading\">" + flowCommentTableHeaders[0] + ": " + data[i].S_STEP_ID + "</h2>";
                //    newLi += "<table style=\"width: 100%\">";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[1] + ": " + data[i].USER_ID + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[2] + ": " + data[i].USERNAME + "</p></td></tr>";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[3] + ": " + data[i].STATUS + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[4] + ": " + data[i].REMARK + "</p></td></tr>";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[6] + ": " + data[i].UPDATE_DATE + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[7] + ": " + data[i].UPDATE_TIME + "</p></td></tr>";
                //    newLi += "</table>"
                //    newLi += "</li>"
                //    $(newLi).appendTo($("#gdvHis_FromApprove"));
                //}
                //$("#gdvHis_FromApprove").listview("refresh");
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormReturn: function (jq) {
        $.mobile.ajaxEnabled = true;
        $.mobile.ajaxFormsEnabled = true;

        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $("#btnOk_FromReturn").setText(flowUITexts[6]);
        $("#btnCancel_FromReturn").setText(flowUITexts[13]);
        $("#btnPreview_FromReturn").setText(flowUITexts[14]);
        $("#btnUploadFile_FromReturn").setText(flowUITexts[12]);

        $("#lImportant_FromReturn").setText(flowUITexts[1]);
        $("#lUrgent_FromReturn").setText(flowUITexts[2]);
        $("#lSenderRole_FromReturn").setText(flowUITexts[5]);
        $("#lReturnTo_FromReturn").setText(flowUITexts[10]);
        $("#suggestionView_FromReturn").setText(flowUITexts[3]);
        $("#lhistoryView_FromReturn").setText(flowUITexts[16]);

        $("#hHead_MobileFromReturn").setText($.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText').split(';')[18]);
        var uploadButtonText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText').split(',');
        $("#hUploadHead_MobileFromReturn").setText(uploadButtonText[12]);
        $("#btnUpload_FromReturn").setText($.sysmsg('getValue', 'JQWebClient/fileuploadbutton'));
        $("#btnClose_FromReturn").setText(uploadButtonText[13]);

        var selectedRow = reunionSYS_TODOLIST();
        if (selectedRow.FLOWIMPORTANT == "1") $("#chkImportant_FromReturn").prop("checked", true).checkboxradio("refresh");
        if (selectedRow.FLOWURGENT == "1") $("#chkUrgent_FromReturn").prop("checked", true).checkboxradio("refresh");
        initFileUpload(selectedRow, "ulFiles_FromReturn");
        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "ddlRoles", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                $("#ddlRoles_FromReturn").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#ddlRoles_FromReturn").append($.createOption(data[i].GROUPNAME, data[i].GROUPID));
                }
                $("#ddlRoles_FromReturn").selectmenu("refresh");
                if (selectedRow != undefined && selectedRow.SENDTO_KIND == "1") {
                    $('#ddlRoles_FromReturn').val(selectedRow.SENDTO_ID);
                    $("#ddlRoles_FromReturn").selectmenu("refresh");
                    $('#ddlRoles_FromReturn').selectmenu('disable');
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "ddlReturnStep", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                $("#ddlReturnStep_FromReturn").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#ddlReturnStep_FromReturn").append($.createOption(data[i].RERURNSTEPNAME, data[i].RERURNSTEPNAME));
                }
                $("#ddlReturnStep_FromReturn").selectmenu("refresh");

                //var data = $('#ddlReturnStep_FromReturn').combobox('getData');
                //if (data.length > 0)
                //    $('#ddlReturnStep_FromReturn').combobox('select', data[0].RERURNSTEPID);
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                flowCommentTableHeaders = flowCommentTableHeader.split(',');
                var table = "<table id=\"gdvHis_FromReturn\" class=\"info-datagrid table-stripe infolight-breakpoint ui-table ui-table-reflow\" data-role=\"table\" infolight-options=\"remoteName:'',tableName:'',allowAdd:true,allowUpdate:true,allowDelete:true,toolItemsPosition:'bottom'\" data-mode=\"reflow\">";
                var thead = "<thead><tr>";
                thead += "<th></th>";
                thead += "<th infolight-options=\"field:'S_STEP_ID',width:120,align:''\">" + flowCommentTableHeaders[0] + "</th>";
                //thead += "<th infolight-options=\"field:'USER_ID',width:120,align:''\">" + flowCommentTableHeaders[1] + "</th>";
                thead += "<th infolight-options=\"field:'USERNAME',width:120,align:''\">" + flowCommentTableHeaders[2] + "</th>";
                thead += "<th infolight-options=\"field:'STATUS',width:120,align:''\">" + flowCommentTableHeaders[3] + "</th>";
                thead += "<th infolight-options=\"field:'REMARK',width:120,align:''\">" + flowCommentTableHeaders[4] + "</th>";
                thead += "<th infolight-options=\"field:'UPDATE_WHOLE_TIME',width:120,align:''\">" + flowCommentTableHeaders[6] + "</th>";
                //thead += "<th infolight-options=\"field:'UPDATE_TIME',width:120,align:''\">" + flowCommentTableHeaders[7] + "</th>";
                thead += "</tr></thead>";
                table += thead;
                //$(thead).appendTo($("#gdvHis_FromReturn"));
                var tbody = "<tbody>";
                for (var i = 0; i < data.length; i++) {
                    tbody += "<tr index=\"" + i + "\">";
                    tbody += "<td></td>";
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"S_STEP_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[0] + "</b>" + data[i].S_STEP_ID + "</td>"
                    //tbody += "<td style=\"width: auto;\" field=\"USER_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[1] + "</b>" + data[i].USER_ID + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"USERNAME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[2] + "</b>" + data[i].USERNAME + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"STATUS\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[3] + "</b>" + data[i].STATUS + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"REMARK\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[4] + "</b>" + decodeURIComponent(data[i].REMARK) + "</td>"
                    tbody += "<td style=\"width: auto;padding:2px\" field=\"UPDATE_WHOLE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[6] + "</b>" + data[i].UPDATE_WHOLE_TIME + "</td>"
                    //tbody += "<td style=\"width: auto;\" field=\"UPDATE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[7] + "</b>" + data[i].UPDATE_TIME + "</td>"
                    tbody += "</tr>"
                }
                tbody += "</tbody>";
                table += tbody;
                table += "</table>"
                $(table).appendTo($("#historyView_FromReturn"));
                $("#gdvHis_FromReturn").datagrid({});

                ////$("#gdvHis_FromReturn").empty();
                ////$(".ui-li-count")[0].innerHTML = data.length;
                //for (var i = 0; i < data.length; i++) {
                //    var newLi = "<li class=\"ui-li ui-li-static ui-btn-up-c\">";
                //    //newLi += "<div data-role=\"controlgroup\" data-type=\"horizontal\" data-mini=\"true\">";
                //    //newLi += "<a href=\"#\" data-role=\"button\" data-iconpos=\"notext\" data-icon=\"plus\" data-theme=\"b\">Add</a>";
                //    //newLi += "</div>";
                //    newLi += "<h2 class=\"ui-li-heading\">" + flowCommentTableHeaders[0] + ": " + data[i].S_STEP_ID + "</h2>";
                //    newLi += "<table style=\"width: 100%\">";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[1] + ": " + data[i].USER_ID + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[2] + ": " + data[i].USERNAME + "</p></td></tr>";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[3] + ": " + data[i].STATUS + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[4] + ": " + data[i].REMARK + "</p></td></tr>";
                //    newLi += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[6] + ": " + data[i].UPDATE_DATE + "</p></td>";
                //    newLi += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[7] + ": " + data[i].UPDATE_TIME + "</p></td></tr>";
                //    newLi += "</table>"
                //    newLi += "</li>"
                //    $(newLi).appendTo($("#gdvHis_FromReturn"));
                //}
                //$("#gdvHis_FromReturn").listview("refresh");
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormNotify: function (jq) {
        $.mobile.ajaxEnabled = true;
        $.mobile.ajaxFormsEnabled = true;

        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $("#h2User_FromNotify").setText($("#h2User_FromNotify").html().replace("Choose a user", flowUITexts[0]));
        $("#h2Group_FromNotify").setText($("#h2Group_FromNotify").html().replace("Choose a role", flowUITexts[1]));
        $('#btnOk_FromNotify').setText(flowUITexts[5]);
        $('#btnCancel_FromNotify').setText(flowUITexts[6]);

        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "lstUsersFrom" },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                if (data.length > 0) {
                    //<li><label><input type="checkbox" id="checkbox-0"><a>0</a></label></li>
                    for (var i = 0; i < data.length; i++) {
                        var newLi = "<li><input class=\"info-checkbox\" type=\"checkbox\" id=\"" + data[i].USERID + "\" /><a>" + data[i].USERID + "(" + data[i].USERNAME + ")</a>";
                        newLi += "</li>"
                        $(newLi).appendTo($("#ulUsers_FormNotify"));
                    }
                    $("#ulUsers_FormNotify").listview("refresh");
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "lstRolesFrom" },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                if (data.length > 0) {
                    //<li><label><input type="checkbox" id="checkbox-0"><a>0</a></label></li>
                    for (var i = 0; i < data.length; i++) {
                        var newLi = "<li><input class=\"info-checkbox\" type=\"checkbox\" id=\"" + data[i].GROUPID + "\" /><a>" + data[i].GROUPID + "(" + data[i].GROUPNAME + ")</a>";
                        newLi += "</li>"
                        $(newLi).appendTo($("#ulRoles_FormNotify"));
                    }
                    $("#ulRoles_FormNotify").listview("refresh");
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormPlusApprove: function (jq) {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $("#h2User_FromPlusApprove").setText($("#h2User_FromPlusApprove").html().replace("Choose a user", flowUITexts[0]));
        $("#h2Group_FromPlusApprove").setText($("#h2Group_FromPlusApprove").html().replace("Choose a role", flowUITexts[1]));
        $('#btnOk_FromPlusApprove').setText(flowUITexts[5]);
        $('#btnCancel_FromPlusApprove').setText(flowUITexts[6]);

        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "lstUsersFrom" },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                if (data.length > 0) {
                    //<li><label><input type="checkbox" id="checkbox-0"><a>0</a></label></li>
                    for (var i = 0; i < data.length; i++) {
                        var newLi = "<li><input class=\"info-checkbox\" type=\"checkbox\" id=\"" + data[i].USERID + "\" /><a>" + data[i].USERID + "(" + data[i].USERNAME + ")</a>";
                        newLi += "</li>"
                        $(newLi).appendTo($("#ulUsers_FormPlusApprove"));
                    }
                    $("#ulUsers_FormPlusApprove").listview("refresh");
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "lstRolesFrom" },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                if (data.length > 0) {
                    //<li><label><input type="checkbox" id="checkbox-0"><a>0</a></label></li>
                    for (var i = 0; i < data.length; i++) {
                        var newLi = "<li><input class=\"info-checkbox\" type=\"checkbox\" id=\"" + data[i].GROUPID + "\" /><a>" + data[i].GROUPID + "(" + data[i].GROUPNAME + ")</a>";
                        newLi += "</li>"
                        $(newLi).appendTo($("#ulRoles_FormPlusApprove"));
                    }
                    $("#ulRoles_FormPlusApprove").listview("refresh");
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormComment: function (jq) {
        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        var selectedRow = reunionSYS_TODOLIST();
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
            cache: false,
            async: false,
            success: function (data) {
                data = eval('(' + data + ')');
                if (data.length > 0) {
                    var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/CommitText');
                    var flowUITexts = flowUIText.split(',');
                    var flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                    var flowCommentTableHeaders = flowCommentTableHeader.split(',');

                    var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
                    var aId = "aa" + 0;
                    newLi += "<a id=\"" + aId + "\" class=\"popupSource\">";
                    newLi += "<table style=\"width: 100%\">";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowUITexts[0] + ": " + data[0].FLOW_DESC + "</p></td>";
                    newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowUITexts[4] + ": " + data[0].FORM_PRESENT_CT + "</p></td></tr>";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowUITexts[2] + ": " + data[0].USER_ID + "(" + data[0].USERNAME + ")</p></td>";
                    newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowUITexts[3] + ": " + data[0].S_ROLE_ID + "</p></td></tr>";
                    newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowUITexts[1] + ": " + data[0].UPDATE_DATE + " " + data[0].UPDATE_TIME + "</p></td></tr>";
                    newLi += "</table>"
                    newLi += "</a>"
                    newLi += "</li>"
                    $(newLi).appendTo($("#ulComment"));

                    //var thead = "<li class=\"ui-li ui-li-static ui-btn-up-c\"><table><thead><tr>";
                    //thead += "<th infolight-options=\"field:'S_STEP_ID',width:120,align:''\">" + flowCommentTableHeaders[0] + "</th>";
                    //thead += "<th infolight-options=\"field:'USER_ID',width:120,align:''\">" + flowCommentTableHeaders[1] + "</th>";
                    //thead += "<th infolight-options=\"field:'USERNAME',width:120,align:''\">" + flowCommentTableHeaders[2] + "</th>";
                    //thead += "<th infolight-options=\"field:'STATUS',width:120,align:''\">" + flowCommentTableHeaders[3] + "</th>";
                    //thead += "<th infolight-options=\"field:'REMARK',width:120,align:''\">" + flowCommentTableHeaders[4] + "</th>";
                    //thead += "<th infolight-options=\"field:'UPDATE_DATE',width:120,align:''\">" + flowCommentTableHeaders[6] + "</th>";
                    //thead += "<th infolight-options=\"field:'UPDATE_TIME',width:120,align:''\">" + flowCommentTableHeaders[7] + "</th>";
                    //thead += "</tr></thead>";
                    //$(thead).appendTo($("#gdvHis_FromApprove"));
                    //var tbody = "<tbody>";
                    //for (var i = 0; i < data.length; i++) {
                    //    tbody += "<tr index=\"" + i + "\">";
                    //    tbody += "<td style=\"width: auto;\" field=\"S_STEP_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[0] + "</b>" + data[i].S_STEP_ID + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"USER_ID\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[1] + "</b>" + data[i].USER_ID + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"USERNAME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[2] + "</b>" + data[i].USERNAME + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"STATUS\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[3] + "</b>" + data[i].STATUS + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"REMARK\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[4] + "</b>" + data[i].REMARK + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"UPDATE_DATE\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[6] + "</b>" + data[i].UPDATE_DATE + "</td>"
                    //    tbody += "<td style=\"width: auto;\" field=\"UPDATE_TIME\"><b class=\"ui-table-cell-label\">" + flowCommentTableHeaders[7] + "</b>" + data[i].UPDATE_TIME + "</td>"
                    //    tbody += "</tr>"
                    //}
                    //tbody += "</tbody></table></li>";
                    //$(tbody).appendTo($("#ulComment"));
                    for (var i = 0; i < data.length; i++) {
                        var newLi2 = "<li  data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
                        //newLi += "<div data-role=\"controlgroup\" data-type=\"horizontal\" data-mini=\"true\">";
                        //newLi += "<a href=\"#\" data-role=\"button\" data-iconpos=\"notext\" data-icon=\"plus\" data-theme=\"b\">Add</a>";
                        //newLi += "</div>";
                        var aId = "a" + i;
                        newLi2 += "<a id=\"" + aId + "\" class=\"popupSource\">";
                        newLi2 += "<h2 class=\"ui-li-heading\">" + flowCommentTableHeaders[0] + ": " + data[i].S_STEP_ID + "</h2>";
                        newLi2 += "<table style=\"width: 100%\">";
                        newLi2 += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[1] + ": " + data[i].USER_ID + "</p></td>";
                        newLi2 += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[2] + ": " + data[i].USERNAME + "</p></td></tr>";
                        newLi2 += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[3] + ": " + data[i].STATUS + "</p></td>";
                        newLi2 += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td></tr>";
                        newLi2 += "<tr><td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[6] + ": " + data[i].UPDATE_DATE + "</p></td>";
                        newLi2 += "<td><p class=\"ui-li-desc\">" + flowCommentTableHeaders[7] + ": " + data[i].UPDATE_TIME + "</p></td></tr>";
                        var link = "";
                        if (data[i].ATTACHMENTS != null && data[i].ATTACHMENTS != "") {
                            var lstAttachments = data[i].ATTACHMENTS.split(';');
                            for (var j = 0; j < lstAttachments.length; j++) {
                                if (lstAttachments[j] != "" && lstAttachments[j] != "null") {
                                    var realFileName = lstAttachments[j];
                                    var fileName = realFileName.replace(/__/g, "&nbsp;");
                                    var href = urlPrdfix + "WorkflowFiles/" + realFileName;
                                    link += "<A id='" + "ATTACHMENTS" + j + "' href='" + href + "' target='_blank' class='" + realFileName + "' >" + fileName + "</A>&nbsp&nbsp";
                                }
                            }
                        }
                        newLi2 += "<tr><td colspan=\"2\" style=\"width: 100%\"><p class=\"ui-li-desc\">" + link + "</p></td></tr>";
                        newLi2 += "</table>"
                        newLi2 += "</a>"
                        newLi2 += "</li>"
                        $(newLi2).appendTo($("#ulComment"));
                    }

                    $("#ulComment").listview("refresh");
                }
            },
            error: function (data) {
                //data.responseText = '';
                //obj = "[{\"" + textField + "\":\"\"}]";
            }
        });
    },
    initializeMobileFormHasten: function (jq) {

        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FormHasten').setText(flowUITexts[5]);
        $('#btnCancel_FormHasten').setText(flowUITexts[6]);

        flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText1');
        flowUITexts = flowUIText.split(',');
        $("#label_FormHasten").setText(flowUITexts[0]);
    }
};

function initializeInbox(selected, queryParam) {
    $.mobile.ajaxEnabled = false;
    $.mobile.ajaxFormsEnabled = false;
    //if ($("#aInbox").attr("selected") == "selected") {
    //    return;
    //}
    //else {
    //    $("#aInbox").attr("selected", "selected");
    //}
    var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
    var titleTexts = titleText.split(';');
    $("#aInbox_Inbox").setText(titleTexts[2]);
    $("#aOutbox_Inbox").setText(titleTexts[3]);
    $("#aNotify_Inbox").setText(titleTexts[16]);
    $("#aDelay_Inbox").setText(titleTexts[4]);
    $("#btnRefresh_Inbox").setText(titleTexts[1]);
    $("#aMenu_Inbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/menuText'));
    $("#aWorkFlow_Inbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/flowText'));

    var afterGetDataInbox = function (data) {
        data = eval('(' + data + ')');
        data = data.rows;
        if (data.length == 0 && filter.page != 1) {
            filter.page--;
            return;
        }

        var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var titleTexts = titleText.split(';');
        var flowColumnText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/ToDoListColumns');
        var flowColumnTexts = flowColumnText.split(',');
        $("#ulInbox_Inbox").empty();
        var titleLi = "<li id=\"ulInboxTitle\" data-role=\"list-divider\">" + titleTexts[2] + "<span class=\"ui-li-count\">" + data.length + "</span></li>"
        $(titleLi).appendTo($("#ulInbox_Inbox"));
        for (var i = 0; i < data.length; i++) {
            var urlParam = "?IsWorkflow=1";
            var selectedRow = data[i];
            for (var field in selectedRow) {
                urlParam += "&" + field + "=" + selectedRow[field];
            }
            var url = selectedRow.WEBFORM_NAME.replace(".", "/") + ".aspx" + urlParam;
            if (selectedRow.FORM_NAME && selectedRow.FORM_NAME.indexOf('M:') == 0) {
                url = selectedRow.FORM_NAME.substring(2).replace(".", "/") + ".aspx" + urlParam;
            }
            var delayStyle = "";
            if (selectedRow.IsDelay == 1) {
                delayStyle = "color:red";
            }

            var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
            var aId = "a" + i;
            newLi += "<a id=\"" + aId + "\" class=\"popupSource\">";
            //newLi += "<a href=\"" + url + "\">";
            newLi += "<h2>";
            if (data[i].FLOWIMPORTANT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/Important.png\"></img>";
            if (data[i].FLOWURGENT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/urgent.png\"></img>";
            newLi += "<p class=\"flowtitle\">" + flowColumnTexts[0] + ": " + data[i].FLOW_DESC + "</p>";
            newLi += "</h2>";
            newLi += "<table style=\"width: 100%\" >";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[1] + ": " + data[i].D_STEP_ID + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[3] + ": " + data[i].USERNAME + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[6] + ": " + data[i].STATUS + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[2] + ": " + data[i].FORM_PRESENT_CT + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[5] + ": " + data[i].UPDATE_WHOLE_TIME + "</p></td></tr>";
            var link = "";
            if (data[i].ATTACHMENTS != null && data[i].ATTACHMENTS != "") {
                var lstAttachments = data[i].ATTACHMENTS.split(';');
                for (var j = 0; j < lstAttachments.length; j++) {
                    if (lstAttachments[j] != "" && lstAttachments[j] != "null") {
                        var realFileName = lstAttachments[j];
                        var fileName = realFileName.replace(/__/g, "&nbsp;");
                        var href = "WorkflowFiles/" + realFileName;
                        link += "<A id='" + "ATTACHMENTS" + j + "' href='" + href + "' target='_blank' class='" + realFileName + "' >" + fileName + "</A>&nbsp&nbsp";
                    }
                }
            }
            newLi += "<tr><td colspan=\"2\" style=\"width: 100%\"><p class=\"ui-li-desc\">" + flowColumnTexts[11] + ": " + link + "</p></td></tr>";
            newLi += "</table>";
            newLi += "</a>";
            //                newLi += "<a class=\"popupSource\"  data-mini=\"true\">";
            //                newLi += "</a>";
            newLi += "</li>";
            $(newLi).data('selectedRow', selectedRow).data('urlParam', urlParam).data('url', url).appendTo($("#ulInbox_Inbox"));
            //$("#a" + i).attr('url', url);
        }
        $("#ulInbox_Inbox").listview("refresh");
        $("#ulInbox_Inbox").find(".flowtitle").click(function () {
            var url = $(this).closest('li').data('url');
            window.location.href = url;
            return false;
        });
        $("#ulInbox_Inbox").find(".popupSource").click(function () {
            $("#popupMenu_Inbox").popup('open', { positionTo: $(this) });
            var selectedRow = $(this).closest('li').data('selectedRow');
            var urlParam = $(this).closest('li').data('urlParam');
            var urlApprove = "InnerPages/MobileFormApprove.aspx?DataFormId=" + urlParam;
            var urlReturn = "InnerPages/MobileFormReturn.aspx?DataFormId=" + urlParam;
            var url = $(this).closest('li').data('url');
            $("#popupMenu_Inbox").find('a.open').attr('href', url);
            $("#popupMenu_Inbox").find('a.approve').attr('href', urlApprove);
            $("#popupMenu_Inbox").find('a.return').attr('href', urlReturn);
            if (selectedRow.PLUSROLES == null || selectedRow.PLUSROLES == "") {
                popUp.find(".approve").css({ "display": "inherit" });
                if (selectedRow.FLNAVIGATOR_MODE != "0" && selectedRow.FLNAVIGATOR_MODE != "5") {
                    popUp.find(".return").css({ "display": "inherit" });
                }
                else {
                    popUp.find(".return").css({ "display": "none" });
                }
            }
            else {
                popUp.find(".approve").css({ "display": "none" });
                popUp.find(".return").css({ "display": "none" });
            }
            //if (popUp.find(".return").length > 0) {
            //    if (selectedRow.FLNAVIGATOR_MODE != "0" && selectedRow.FLNAVIGATOR_MODE != "5") {
            //        popUp.find(".return").css({ "display": "inherit" });
            //    }
            //    else {
            //        popUp.find(".return").css({ "display": "none" });
            //    }
            //}
        });

        var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var flowUITexts = flowUIText.split(';');
        $("#popupMenu_Inbox").find('a.open').html(flowUITexts[9]);
        $("#popupMenu_Inbox").find('a.approve').html(flowUITexts[6]);
        $("#popupMenu_Inbox").find('a.return').html(flowUITexts[8]);
        var popUp = $('#popupMenu_Inbox');
        if ($("#ddlToDoListFilter_Inbox")[0].length == 0) {
            var pleaseSelectText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/SelectOption');
            $("#ddlToDoListFilter_Inbox").append($.createOption("<--" + pleaseSelectText + "-->", "-1"));
            for (var i = 0; i < data.length; i++) {
                var exist = false;
                for (var j = 0; j < $("#ddlToDoListFilter_Inbox")[0].length; j++) {
                    if (data[i].FLOW_DESC == $("#ddlToDoListFilter_Inbox")[0][j].innerHTML) {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    $("#ddlToDoListFilter_Inbox").append($.createOption(data[i].FLOW_DESC, data[i].FLOW_DESC));
            }
            $("#ddlToDoListFilter_Inbox").selectmenu("refresh");
        }
    }

    var filter = {};
    filter.Type = "ToDoList";
    filter.rows = 10;
    filter.page = 1;
    if (selected != undefined && selected != -1)
        filter.Filter = "FLOW_DESC='" + selected + "'";
    if (queryParam != undefined && queryParam != "")
        filter.QueryParam = queryParam;
    getFlowData(filter, afterGetDataInbox);

    $("#divInbox_Inbox").find("a.grid-previous").click(function () {
        if (filter.page > 1) {
            filter.page--;
            getFlowData(filter, afterGetDataInbox);
        }
    });
    $("#divInbox_Inbox").find("a.grid-next").click(function () {
        filter.page++;
        getFlowData(filter, afterGetDataInbox);
    });
}

function getFlowData(filter, afterGetMethod) {
    $.ajax({
        type: "POST",
        url: 'handler/SystemHandle_Flow.ashx',
        data: filter,
        cache: false,
        async: false,
        success: function (data) {
            afterGetMethod(data);
        },
        error: function (data) {
            //data.responseText = '';
            //obj = "[{\"" + textField + "\":\"\"}]";
        }
    });
}

function clickSelect(aId) {
    var url = $(aId).attr("url");
    window.location.href = url;
}

function initializeOutbox(selected, queryParam) {
    $.mobile.ajaxEnabled = false;
    $.mobile.ajaxFormsEnabled = false;
    //if ($("#aOutbox").attr("selected") == "selected") {
    //    return;
    //}
    //else {
    //    $("#aOutbox").attr("selected", "selected");
    //}
    var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
    var titleTexts = titleText.split(';');
    $("#aInbox_Outbox").setText(titleTexts[2]);
    $("#aOutbox_Outbox").setText(titleTexts[3]);
    $("#aNotify_Outbox").setText(titleTexts[16]);
    $("#aDelay_Outbox").setText(titleTexts[4]);
    $("#btnRefresh_Outbox").setText(titleTexts[1]);
    $("#aMenu_Outbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/menuText'));
    $("#aWorkFlow_Outbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/flowText'));

    var afterGetDataOutbox = function (data) {
        var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var titleTexts = titleText.split(';');

        data = eval('(' + data + ')');
        data = data.rows;
        if (data.length == 0 && filter.page != 1) {
            filter.page--;
            return;
        }

        var flowColumnText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/OvertimeColumns');
        var flowColumnTexts = flowColumnText.split(',');
        $("#ulOutbox_Outbox").empty();
        var titleLi = "<li id=\"ulOutboxTitle\" data-role=\"list-divider\">" + titleTexts[3] + "<span class=\"ui-li-count\">" + data.length + "</span></li>"
        $(titleLi).appendTo($("#ulOutbox_Outbox"));
        for (var i = 0; i < data.length; i++) {
            var urlParam = "?IsWorkflow=1";
            var selectedRow = data[i];
            selectedRow.FLNAVMODE = 6;
            selectedRow.NAVMODE = 0;
            for (var field in selectedRow) {
                urlParam += "&" + field + "=" + selectedRow[field];
            }
            var url = selectedRow.WEBFORM_NAME.replace(".", "/") + ".aspx" + urlParam;
            if (selectedRow.FORM_NAME && selectedRow.FORM_NAME.indexOf('M:') == 0) {
                url = selectedRow.FORM_NAME.substring(2).replace(".", "/") + ".aspx" + urlParam;
            }
            var delayStyle = "";
            if (selectedRow.IsDelay == 1) {
                delayStyle = "color:red";
            }

            var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
            var aId = "a" + i;
            newLi += "<a id=\"" + aId + "\" class=\"popupSource\">";
            //newLi += "<a href=\"" + url + "\">";
            newLi += "<h2>";
            if (data[i].FLOWIMPORTANT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/Important.png\"></img>";
            if (data[i].FLOWURGENT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/urgent.png\"></img>";
            newLi += "<p class=\"flowtitle\">" + flowColumnTexts[0] + ": " + data[i].FLOW_DESC + "</p>";
            newLi += "</h2>";
            newLi += "<table style=\"width: 100%\"  >";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[1] + ": " + data[i].D_STEP_ID + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[3] + ": " + data[i].SENDTO_NAME + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[6] + ": " + data[i].STATUS + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[2] + ": " + data[i].FORM_PRESENT_CT + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[5] + ": " + data[i].UPDATE_WHOLE_TIME + "</p></td></tr>";
            var link = "";
            if (data[i].ATTACHMENTS != null && data[i].ATTACHMENTS != "") {
                var lstAttachments = data[i].ATTACHMENTS.split(';');
                for (var j = 0; j < lstAttachments.length; j++) {
                    if (lstAttachments[j] != "" && lstAttachments[j] != "null") {
                        var realFileName = lstAttachments[j];
                        var fileName = realFileName.replace(/__/g, "&nbsp;");
                        var href = "WorkflowFiles/" + realFileName;
                        link += "<A id='" + "ATTACHMENTS" + j + "' href='" + href + "' target='_blank' class='" + realFileName + "' >" + fileName + "</A>&nbsp&nbsp";
                    }
                }
            }
            newLi += "<tr><td colspan=\"2\" style=\"width: 100%\"><p class=\"ui-li-desc\">" + flowColumnTexts[10] + ": " + link + "</p></td></tr>";
            newLi += "</table>";

            newLi += "</a>";
            //                newLi += "<a class=\"popupSource\"  data-mini=\"true\">";
            //                newLi += "</a>";
            newLi += "</li>";
            url = url.replace("NAVIGATOR_MODE=2", "NAVIGATOR_MODE=0");
            $(newLi).data('row', selectedRow).data('url', url).data('urlParam', urlParam).appendTo($("#ulOutbox_Outbox"));
        }

        $("#ulOutbox_Outbox").listview("refresh");
        $("#ulOutbox_Outbox").find(".flowtitle").click(function () {
            var url = $(this).closest('li').data('url');
            window.location.href = url;
            return false;
        });
        $("#ulOutbox_Outbox").find(".popupSource").click(function () {
            $("#popupMenu_Outbox").popup('open', { positionTo: $(this) });
            var urlParam = $(this).closest('li').data('urlParam');
            var urlHasten = "InnerPages/MobileFormHasten.aspx?DataFormId=" + urlParam;
            var selectedRow = $(this).closest('li').data('row');
            var popUp = $('#popupMenu_Outbox');
            if (popUp.find(".return").length > 0) {
                if (selectedRow.FLNAVIGATOR_MODE != "0" && selectedRow.FLNAVIGATOR_MODE != "5") {
                    popUp.find(".return").css({ "display": "inherit" });
                }
                else {
                    popUp.find(".return").css({ "display": "none" });
                }
            }

            if (popUp.find(".retake").length > 0) {
                if (RetakeVisible(selectedRow, false) == false) {
                    popUp.find(".retake").css({ "display": "none" });
                }
                else {
                    popUp.find(".retake").css({ "display": "inherit" });
                }
            }
            var params = "&LISTID=" + selectedRow.LISTID + "&D_STEP_ID=" + selectedRow.D_STEP_ID
            var url = $(this).closest('li').data('url');
            $("#popupMenu_Outbox").find('a.open').attr('href', url);;
            $("#popupMenu_Outbox").find('a.retake').unbind('click').bind('click', function () {
                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Retake";
                urlParam.LISTID = selectedRow.LISTID;
                urlParam.D_STEP_ID = selectedRow.D_STEP_ID;
                flowRetake(urlParam);
            })
            $("#popupMenu_Outbox").find('a.hasten').attr('href', urlHasten);
        });

        var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var flowUITexts = flowUIText.split(';');
        $("#popupMenu_Outbox").find('a.open').html(flowUITexts[9]);
        $("#popupMenu_Outbox").find('a.retake').html(flowUITexts[14]);
        $("#popupMenu_Outbox").find('a.hasten').html(flowUITexts[18]);

        if ($("#ddlToDoHisFilter_Outbox")[0].length == 0) {
            var pleaseSelectText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/SelectOption');
            $("#ddlToDoHisFilter_Outbox").append($.createOption("<--" + pleaseSelectText + "-->", "-1"));
            for (var i = 0; i < data.length; i++) {
                var exist = false;
                for (var j = 0; j < $("#ddlToDoHisFilter_Outbox")[0].length; j++) {
                    if (data[i].FLOW_DESC == $("#ddlToDoHisFilter_Outbox")[0][j].innerHTML) {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    $("#ddlToDoHisFilter_Outbox").append($.createOption(data[i].FLOW_DESC, data[i].FLOW_DESC));
            }
            $("#ddlToDoHisFilter_Outbox").selectmenu("refresh");
        }
    }

    var filter = {};
    filter.Type = "ToDoHis";
    filter.rows = 10;
    filter.page = 1;
    if (selected != undefined && selected != -1)
        filter.Filter = "FLOW_DESC='" + selected + "'";
    if (queryParam != undefined && queryParam != "")
        filter.QueryParam = queryParam;
    getFlowData(filter, afterGetDataOutbox);

    $("#divOutbox_Outbox").find("a.grid-previous").click(function () {
        if (filter.page > 1) {
            filter.page--;
            getFlowData(filter, afterGetDataOutbox);
        }
    });
    $("#divOutbox_Outbox").find("a.grid-next").click(function () {
        filter.page++;
        getFlowData(filter, afterGetDataOutbox);
    });
}

function initializeFlowRunOver(selected, queryParam) {
    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
    var titleTexts = titleText.split(';');
    $("#aInbox_Outbox").setText(titleTexts[2]);
    $("#aOutbox_Outbox").setText(titleTexts[3]);
    $("#aNotify_Outbox").setText(titleTexts[16]);
    $("#aDelay_Outbox").setText(titleTexts[4]);
    $("#btnRefresh_Outbox").setText(titleTexts[1]);
    $("#aMenu_Outbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/menuText'));
    $("#aWorkFlow_Outbox").setText($.sysmsg('getValue', 'JQWebClient/info-main/flowText'));

    var afterGetDataOutbox = function (data) {
        data = eval('(' + data + ')');
        data = data.rows;
        if (data.length == 0 && filter.page != 1) {
            filter.page--;
            return;
        }

        var flowColumnText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/OvertimeColumns');
        var flowColumnTexts = flowColumnText.split(',');
        $("#ulOutbox_Outbox").empty();
        var titleLi = "<li id=\"ulOutboxTitle\" data-role=\"list-divider\">" + titleTexts[3] + "<span class=\"ui-li-count\">" + data.length + "</span></li>"
        $(titleLi).appendTo($("#ulOutbox_Outbox"));
        for (var i = 0; i < data.length; i++) {
            var urlParam = "?IsWorkflow=1";
            var selectedRow = data[i];
            selectedRow.FLNAVMODE = 6;
            selectedRow.NAVMODE = 0;
            for (var field in selectedRow) {
                urlParam += "&" + field + "=" + selectedRow[field];
            }
            var url = selectedRow.WEBFORM_NAME.replace(".", "/") + ".aspx" + urlParam;
            if (selectedRow.FORM_NAME && selectedRow.FORM_NAME.indexOf('M:') == 0) {
                url = selectedRow.FORM_NAME.substring(2).replace(".", "/") + ".aspx" + urlParam;
            }
            var delayStyle = "";
            if (selectedRow.IsDelay == 1) {
                delayStyle = "color:red";
            }

            var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
            var aId = "a" + i;
            newLi += "<a id=\"" + aId + "\"  ";
            //newLi += "<a href=\"" + url + "\">";
            newLi += "<h2>";
            newLi += "<p class=\"flowtitle\">" + flowColumnTexts[0] + ": " + data[i].FLOW_DESC + "</p>";
            newLi += "</h2>";
            newLi += "<table style=\"width: 100%\"  class=\"popupSource\">";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[1] + ": " + data[i].D_STEP_ID + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[3] + ": " + "" /*data[i].USERNAME*/ + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[6] + ": " + data[i].STATUS + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[2] + ": " + data[i].FORM_PRESENT_CT + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[5] + ": " + data[i].UPDATE_WHOLE_TIME + "</p></td></tr>";
            var link = "";
            if (data[i].ATTACHMENTS != null && data[i].ATTACHMENTS != "") {
                var lstAttachments = data[i].ATTACHMENTS.split(';');
                for (var j = 0; j < lstAttachments.length; j++) {
                    if (lstAttachments[j] != "" && lstAttachments[j] != "null") {
                        var realFileName = lstAttachments[j];
                        var fileName = realFileName.replace(/__/g, "&nbsp;");
                        var href = "WorkflowFiles/" + realFileName;
                        link += "<A id='" + "ATTACHMENTS" + j + "' href='" + href + "' target='_blank' class='" + realFileName + "' >" + fileName + "</A>&nbsp&nbsp";
                    }
                }
            }
            newLi += "<tr><td colspan=\"2\" style=\"width: 100%\"><p class=\"ui-li-desc\">" + flowColumnTexts[10] + ": " + link + "</p></td></tr>";
            newLi += "</table>";
            newLi += "</a>";
            //                newLi += "<a class=\"popupSource\"  data-mini=\"true\">";
            //                newLi += "</a>";
            newLi += "</li>";

            $(newLi).data('row', selectedRow).data('url', url).data('urlParam', urlParam).appendTo($("#ulOutbox_Outbox"));
        }

        $("#ulOutbox_Outbox").listview("refresh");
        $("#ulOutbox_Outbox").find(".flowtitle").click(function () {
            var url = $(this).closest('li').data('url');
            window.location.href = url;
        });
        $("#ulOutbox_Outbox").find(".popupSource").click(function () {
            $("#popupMenu_Outbox").popup('open', { positionTo: $(this) });
            var urlParam = $(this).closest('li').data('urlParam');
            var urlHasten = "InnerPages/MobileFormHasten.aspx?DataFormId=" + urlParam;
            var selectedRow = $(this).closest('li').data('row');
            var params = "&LISTID=" + selectedRow.LISTID + "&D_STEP_ID=" + selectedRow.D_STEP_ID
            var url = $(this).closest('li').data('url');
            $("#popupMenu_Outbox").find('a.open').attr('href', url);
        });

        var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var flowUITexts = flowUIText.split(';');
        $("#popupMenu_Outbox").find('a.open').html(flowUITexts[9]);

        if ($("#ddlToDoHisFilter_Outbox")[0].length == 0) {
            var pleaseSelectText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/SelectOption');
            $("#ddlToDoHisFilter_Outbox").append($.createOption("<--" + pleaseSelectText + "-->", "-1"));
            for (var i = 0; i < data.length; i++) {
                var exist = false;
                for (var j = 0; j < $("#ddlToDoHisFilter_Outbox")[0].length; j++) {
                    if (data[i].FLOW_DESC == $("#ddlToDoHisFilter_Outbox")[0][j].innerHTML) {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                    $("#ddlToDoHisFilter_Outbox").append($.createOption(data[i].FLOW_DESC, data[i].FLOW_DESC));
            }
            $("#ddlToDoHisFilter_Outbox").selectmenu("refresh");
        }
    }

    var filter = {};
    filter.Type = "FlowRunOver";
    filter.rows = 10;
    filter.page = 1;
    if (selected != undefined && selected != -1)
        filter.Filter = "FLOW_DESC='" + selected + "'";
    if (queryParam != undefined && queryParam != "")
        filter.QueryParam = queryParam;
    getFlowData(filter, afterGetDataOutbox);

    $("#divOutbox_Outbox").find("a.grid-previous").click(function () {
        if (filter.page > 1) {
            filter.page--;
            getFlowData(filter, afterGetDataOutbox);
        }
    });
    $("#divOutbox_Outbox").find("a.grid-next").click(function () {
        filter.page++;
        getFlowData(filter, afterGetDataOutbox);
    });
}

function cbSubmitted_OutboxChange() {
    if ($("#cbSubmitted_Outbox").attr("checked") == "checked") {
        initializeFlowRunOver();
    }
    else {
        initializeOutbox();
    }
}

function flowRetake(param) {
    $.ajax({
        type: "POST",
        url: 'handler/SystemHandle_Flow.ashx',
        data: param,
        cache: false,
        async: true,
        success: function (message) {
            alert(message);
            location.reload();
        },
        error: function () {
            return false;
        }
    });
}

function flowDelete(param) {
    var DeleteSure = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowDeleteConfirm');
    var NavText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText');
    var NavTexts = NavText.split(";");
    flowDeleteText = String.format(DeleteSure, NavTexts[20]);

    $.messager.confirm('', flowDeleteText, function (r) {
        if (r) {
            var urlPrdfix = "";
            if (isSubPath) {
                urlPrdfix = "../";
            }
            $.ajax({
                type: "POST",
                url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                data: param,
                cache: false,
                async: true,
                success: function (message) {
                    gotoInbox(false, 2);
                    //location.reload();
                },
                error: function (er) {
                    return false;
                }
            });
        }
    });
    return false;
}

function initializeDelay(level) {
    //    $.mobile.ajaxEnabled = false;
    //    $.mobile.ajaxFormsEnabled = false;
    //if ($("#aDelay").attr("selected") == "selected") {
    //    return;
    //}
    //else {
    //    $("#aDelay").attr("selected", "selected");
    //}
    var flowColumnText = $.sysmsg('getValue', 'EEPNetClient/FrmClientMain/OvertimeColumns');
    var flowColumnTexts = flowColumnText.split(',');
    var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
    var titleTexts = titleText.split(';');
    $("#aInbox_Delay").setText(titleTexts[2]);
    $("#aOutbox_Delay").setText(titleTexts[3]);
    $("#aNotify_Delay").setText(titleTexts[16]);
    $("#aDelay_Delay").setText(titleTexts[4]);
    $("#btnRefresh_Delay").setText(titleTexts[1]);
    $("#aMenu_Delay").setText($.sysmsg('getValue', 'JQWebClient/info-main/menuText'));
    $("#aWorkFlow_Delay").setText($.sysmsg('getValue', 'JQWebClient/info-main/flowText'));

    var afterGetDataDelay = function (data) {
        data = eval('(' + data + ')');
        data = data.rows;
        if (data.length == 0 && filter.page != 1) {
            filter.page--;
            return;
        }

        //$(".ui-li-count")[0].innerHTML = data.length;
        $("#ulDelay_Delay").empty();
        var titleLi = "<li id=\"ulDelayTitle\" data-role=\"list-divider\">" + titleTexts[4] + "<span class=\"ui-li-count\">" + data.length + "</span></li>"
        $(titleLi).appendTo($("#ulDelay_Delay"));
        for (var i = 0; i < data.length; i++) {
            var urlParam = "?IsWorkflow=1";
            var selectedRow = data[i];
            selectedRow.FLNAVMODE = 6;
            selectedRow.NAVMODE = 0;
            for (var field in selectedRow) {
                urlParam += "&" + field + "=" + selectedRow[field];
            }
            var url = selectedRow.WEBFORM_NAME.replace(".", "/") + ".aspx" + urlParam;
            if (selectedRow.FORM_NAME && selectedRow.FORM_NAME.indexOf('M:') == 0) {
                url = selectedRow.FORM_NAME.substring(2).replace(".", "/") + ".aspx" + urlParam;
            }
            var newLi = "<li data-role=\"collapsible\" data-iconpos=\"right\" data-shadow=\"false\" data-corners=\"false\">";
            var aId = "a" + i;
            newLi += "<a id=\"" + aId + "\" class=\"popupSource\">";
            //newLi += "<a href=\"" + url + "\">";
            newLi += "<h2>";
            if (data[i].FLOWIMPORTANT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/Important.png\"></img>";
            if (data[i].FLOWURGENT == "1")
                newLi += "<img src=\"Image/WorkflowIcon/urgent.png\"></img>";
            newLi += "<p class=\"flowtitle\">" + flowColumnTexts[0] + ": " + data[i].FLOW_DESC + "</p>";
            newLi += "</h2>";
            newLi += "<table style=\"width: 100%\" class=\"popupSource\">";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[1] + ": " + data[i].D_STEP_ID + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[3] + ": " + data[i].SENDTO_NAME + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[6] + ": " + data[i].STATUS + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[2] + ": " + data[i].FORM_PRESENT_CT + "</p></td></tr>";
            newLi += "<tr><td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[4] + ": " + decodeURIComponent(data[i].REMARK) + "</p></td>";
            newLi += "<td style=\"width: 50%\"><p class=\"ui-li-desc\">" + flowColumnTexts[5] + ": " + data[i].UPDATE_WHOLE_TIME + "</p></td></tr>";
            newLi += "</table>";
            newLi += "</a>"

            newLi += "</li>";

            $(newLi).data('url', url).appendTo($("#ulDelay_Delay"));

        }
        $("#ulDelay_Delay").listview("refresh");
        $("#ulDelay_Delay").find(".flowtitle").click(function () {
            var url = $(this).closest('li').data('url');
            window.location.href = url;
            return false;
        });
        $("#ulDelay_Delay").find(".popupSource").click(function () {
            $("#popupMenu_Delay").popup('open', { positionTo: $(this) });
            var url = $(this).closest('li').data('url');
            $("#popupMenu_Delay").find('a.open').attr('href', url);;
        });
        var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var flowUITexts = flowUIText.split(';');
        $("#popupMenu_Delay").find('a.open').html(flowUITexts[9]);
    }

    var filter = {};
    filter.Type = "Delay";
    filter.Level = level;
    filter.rows = 10;
    filter.page = 1;
    getFlowData(filter, afterGetDataDelay);

    $("#divDelay_Delay").find("a.grid-previous").click(function () {
        if (filter.page > 1) {
            filter.page--;
            getFlowData(filter, afterGetDataDelay);
        }
    });
    $("#divDelay_Delay").find("a.grid-next").click(function () {
        filter.page++;
        getFlowData(filter, afterGetDataDelay);
    });
}

var isSubmit = false;
var isApprove = false;
var isReturn = false;

function btnOk_FromSubmitClick() {
    showLoading();

    $('#btnOk_FromSubmit').hide();
    //$('#btnCancel_FromSubmit').hide();
    $('#btnPreview_FromSubmit').hide();
    $('#btnUploadFile_FromSubmit').hide();
    var selectedRow = reunionSYS_TODOLIST();

    var txtSuggest_FromSubmit_Value = $('#txtSuggest_FromSubmit').val();
    var ddlRoles_FromSubmit_Value = $('#ddlRoles_FromSubmit').val();
    var chkImportant_FromSubmit_value = $('#chkImportant_FromSubmit').attr("checked");
    var chkUrgent_FromSubmit_value = $('#chkUrgent_FromSubmit').attr("checked");
    var ulFiles_FromSubmit_value = "";
    $('#ulFiles_FromSubmit li').each(function (li) {
        ulFiles_FromSubmit_value += $(this).find("a").html() + ";";
    });

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Submit";
    urlParam.roles = ddlRoles_FromSubmit_Value;
    urlParam.important = chkImportant_FromSubmit_value;
    urlParam.urgent = chkUrgent_FromSubmit_value;
    urlParam.suggest = encodeURIComponent(txtSuggest_FromSubmit_Value);
    urlParam.ATTACHMENTS = ulFiles_FromSubmit_value;

    var dataFormId = Request.getQueryStringByName("DataFormId");
    var dataGrid = $(".info-form", "#" + dataFormId).form('options').viewPage;
    var keys = $(".info-datagrid", dataGrid).datagrid('options').keys;
    var keyColumns = "";
    var keyValues = "";
    for (var i = 0; i < keys.length; i++) {
        if (keys[i] != "") {
            var value = $("#" + dataFormId + "_" + keys[i]).val();
            if (value != "") {
                keyValues += keys[i] + "='" + value + "';";
            }
            keyColumns += keys[i] + ";";
        }
    }
    keyValues = keyValues.substring(0, keyValues.lastIndexOf(";"));
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.FLOWFILENAME = Request.getQueryStringByName("FLOWFILENAME");
    urlParam.PROVIDER_NAME = $(".info-form", "#" + dataFormId).form('options').remoteName;
    urlParam.FORM_KEYS = keyColumns;
    urlParam.FORM_PRESENTATION = keyValues;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FromSubmit')[0].innerHTML = message;
            isSubmit = true;
        },
        error: function (message) {
            hideLoading();
            $('#result_FromSubmit')[0].innerHTML = message.responseText;
            return false;
        }
    });
}

function btnOk_FromApproveClick() {
    showLoading();
    $('#btnOk_FromApprove').hide();
    //$('#btnCancel_FromApprove').hide();
    $('#btnPreview_FromApprove').hide();
    $('#btnUploadFile_FromApprove').hide();
    var selectedRow = reunionSYS_TODOLIST();

    var txtSuggest_FromApprove_Value = $('#txtSuggest_FromApprove').val();
    var ddlRoles_FromApprove_Value = $('#ddlRoles_FromApprove').val();
    var chkImportant_FromApprove_value = $('#chkImportant_FromApprove').attr("checked");
    var chkUrgent_FromApprove_value = $('#chkUrgent_FromApprove').attr("checked");
    var ulFiles_FromApprove_value = "";
    $('#ulFiles_FromApprove li').each(function (li) {
        ulFiles_FromApprove_value += $(this).find("a").html() + ";";
    });

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Approve";
    var status = decodeURIComponent(selectedRow.STATUS);
    if ((status == "A" || status == "AA" || status == "加签" || status == "加簽" || status == "Plus") && selectedRow.SENDTO_KIND == "2") {
        urlParam.roles = "";
    }
    else {
        urlParam.roles = ddlRoles_FromApprove_Value;
    }
    urlParam.important = chkImportant_FromApprove_value;
    urlParam.urgent = chkUrgent_FromApprove_value;
    urlParam.suggest = encodeURIComponent(txtSuggest_FromApprove_Value);
    urlParam.ATTACHMENTS = ulFiles_FromApprove_value;

    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
    urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;
    if (selectedRow.FORM_KEYS == undefined || selectedRow.FORM_KEYS == null || selectedRow.FORM_KEYS == "") {
        var dataForm = $('#' + dataFormId);
        var infolightOptions = getInfolightOption(dataForm);
        var keys = "";
        var dialoggrid = dataForm.attr('dialogGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('switchGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('continueGrid');
        var key = $(dialoggrid).attr('keyColumns');
        var keyValues = "";
        var keys = key.split(",");
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] != "") {
                var value = $("#" + dataForm.attr("id") + keys[i]).val();
                if (value != "") {
                    keyValues += keys[i] + "='" + value + "';";
                }
            }
        }
        urlParam.FORM_KEYS = key;
    }
    else {
        urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    }

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FromApprove')[0].innerHTML = message;
            isApprove = true;
        },
        error: function (message) {
            hideLoading();
            $('#result_FromApprove')[0].innerHTML = message.responseText;
            return false;
        }
    });
}

function btnOk_FromReturnClick() {
    showLoading();
    $('#btnOk_FromReturn').hide();
    //$('#btnCancel_FromReturn').hide();
    $('#btnPreview_FromReturn').hide();
    $('#btnUploadFile_FromReturn').hide();
    var selectedRow = reunionSYS_TODOLIST();

    var txtSuggest_FromReturn_Value = $('#txtSuggest_FromReturn').val();
    var ddlRoles_FromReturn_Value = $('#ddlRoles_FromReturn').val();
    var ddlReturnStep_FromReturn_Value = $('#ddlReturnStep_FromReturn').val();
    var ddlReturnStep_FromReturn_Text = $('#ddlReturnStep_FromReturn').val();
    if (ddlReturnStep_FromReturn_Text == "Return previous step" || ddlReturnStep_FromReturn_Text == "退回前一个活动" || ddlReturnStep_FromReturn_Text == "退回前一個活動")
        ddlReturnStep_FromReturn_Value = 0;
    var chkImportant_FromReturn_value = $('#chkImportant_FromReturn').attr("checked");
    var chkUrgent_FromReturn_value = $('#chkUrgent_FromReturn').attr("checked");
    var ulFiles_FromReturn_value = "";
    $('#ulFiles_FromReturn li').each(function (li) {
        ulFiles_FromReturn_value += $(this).find("a").html() + ";";
    });

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Return";
    urlParam.roles = ddlRoles_FromReturn_Value;
    urlParam.returnstep = ddlReturnStep_FromReturn_Value;
    urlParam.returnsteptext = ddlReturnStep_FromReturn_Text;
    urlParam.important = chkImportant_FromReturn_value;
    urlParam.urgent = chkUrgent_FromReturn_value;
    urlParam.suggest = encodeURIComponent(txtSuggest_FromReturn_Value);
    urlParam.ATTACHMENTS = ulFiles_FromReturn_value;

    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;
    urlParam.MULTISTEPRETURN = selectedRow.MULTISTEPRETURN;

    if (selectedRow.FORM_KEYS == undefined || selectedRow.FORM_KEYS == null || selectedRow.FORM_KEYS == "") {
        var dataForm = $('#' + dataFormId);
        var infolightOptions = getInfolightOption(dataForm);
        var keys = "";
        var dialoggrid = dataForm.attr('dialogGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('switchGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('continueGrid');
        var key = $(dialoggrid).attr('keyColumns');
        var keyValues = "";
        var keys = key.split(",");
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] != "") {
                var value = $("#" + dataForm.attr("id") + keys[i]).val();
                if (value != "") {
                    keyValues += keys[i] + "='" + value + "';";
                }
            }
        }
        urlParam.FORM_KEYS = key;
    }
    else {
        urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    }

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FromReturn')[0].innerHTML = message;
            isReturn = true;
            if (!urlPrdfix) {

            }
            else {

            }
        },
        error: function (message) {
            hideLoading();
            $('#result_FromReturn')[0].innerHTML = message.responseText;
            return false;
        }
    });
}

function btnOk_FromNotifyClick() {
    showLoading();
    $('#btnOk_FormNotify').hide();

    var txtMessage_FormNotify_Value = $('#txtMessage_FormNotify').val();
    var dataUsers = $(".info-checkbox", "#ulUsers_FormNotify");
    var dataRoles = $(".info-checkbox", "#ulRoles_FormNotify");
    var users = "";
    for (var i = 0; i < dataUsers.length; i++) {
        if (dataUsers[i].checked)
            users += dataUsers[i].id + ":UserId;";
    }
    var roles = "";
    for (var i = 0; i < dataRoles.length; i++) {
        if (dataRoles[i].checked)
            roles += dataRoles[i].id + ";";
    }

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Notify";
    urlParam.users = users;
    urlParam.roles = roles;
    urlParam.suggest = encodeURIComponent(txtMessage_FormNotify_Value);

    var selectedRow = reunionSYS_TODOLIST();
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    urlParam.FORM_PRESENTATION = selectedRow.FORM_PRESENTATION;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FormNotify')[0].innerHTML = message;
        },
        error: function (message) {
            hideLoading();
            $('#result_FormNotify')[0].innerHTML = message.responseText;
            return false;
        }
    });
}

function btnOk_FromPlusApproveClick() {
    showLoading();
    $('#btnOk_FromPlusApprove').hide();

    var txtMessage_FormPlusApprove_Value = $('#txtMessage_FormPlusApprove').val();
    var dataUsers = $(".info-checkbox", "#ulUsers_FormPlusApprove");
    var dataRoles = $(".info-checkbox", "#ulRoles_FormPlusApprove");
    var users = "";
    var users1 = "";
    for (var i = 0; i < dataUsers.length; i++) {
        if (dataUsers[i].checked) {
            users += "U:" + dataUsers[i].id + ";";
            users1 += dataUsers[i].id + ":UserId;";
        }
    }
    var roles = "";
    for (var i = 0; i < dataRoles.length; i++) {
        if (dataRoles[i].checked)
            roles += dataRoles[i].id + ";";
    }

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Plus";
    urlParam.users = users;
    urlParam.users1 = users1;
    urlParam.roles = roles;
    urlParam.suggest = encodeURIComponent(txtMessage_FormPlusApprove_Value);

    var selectedRow = reunionSYS_TODOLIST();
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    urlParam.FORM_PRESENTATION = selectedRow.FORM_PRESENTATION;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FormPlusApprove')[0].innerHTML = message;
        },
        error: function () {
            $('#btnOk_FromPlusApprove').show();
            hideLoading();
            return false;
        }
    });
}

function btnOk_FormHastenClick() {
    showLoading();
    $('#btnOk_FormHasten').hide();
    //$('#btnCancel_FormHasten').hide();

    var txtMessage_FormHasten_Value = $('#txtMessage_FormHasten').val();
    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Hasten";
    urlParam.suggest = encodeURIComponent(txtMessage_FormHasten_Value);

    var selectedRow = reunionSYS_TODOLIST();
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    urlParam.FORM_PRESENTATION = selectedRow.FORM_PRESENTATION;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            $('#result_FormHasten')[0].innerHTML = message;
        },
        error: function () {
            hideLoading();
            return false;
        }
    });
}

function btnCancel_FromSubmitClick() {
    if (isSubmit == true) {
        gotoInbox(true);
    }
    else {
        //history.back(1);
	commentGoBack();
    }
}

function btnCancel_FromApproveClick() {
    if (isApprove == true) {
        gotoInbox(true);
    }
    else {
        //history.back(1);
	commentGoBack();
    }
}

function btnCancel_FromReturnClick() {
    if (isReturn == true) {
        gotoInbox(true);
    }
    else {
        //history.back(1);
	commentGoBack();
    }
}

function btnPreview_FromSubmitClick() {
    showLoading();

    var txtSuggest_FromSubmit_Value = $('#txtSuggest_FromSubmit').val();
    var ddlRoles_FromSubmit_Value = $('#ddlRoles_FromSubmit').val();
    var chkImportant_FromSubmit_value = $('#chkImportant_FromSubmit').attr("checked");
    var chkUrgent_FromSubmit_value = $('#chkUrgent_FromSubmit').attr("checked");

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Preview";
    urlParam.suggest = encodeURIComponent(txtSuggest_FromSubmit_Value);
    urlParam.roles = ddlRoles_FromSubmit_Value;
    urlParam.important = chkImportant_FromSubmit_value;
    urlParam.urgent = chkUrgent_FromSubmit_value;
    var dataFormId = Request.getQueryStringByName("DataFormId");
    var dataGrid = $(".info-form", "#" + dataFormId).form('options').viewPage;
    var keys = $(".info-datagrid", dataGrid).datagrid('options').keys;
    var keyColumns = "";
    var keyValues = "";
    for (var i = 0; i < keys.length; i++) {
        if (keys[i] != "") {
            var value = $("#" + dataFormId + "_" + keys[i]).val();
            if (value != "") {
                keyValues += keys[i] + "='" + value + "';";
            }
            keyColumns += keys[i] + ";";
        }
    }
    keyValues = keyValues.substring(0, keyValues.lastIndexOf(";"));
    var selectedRow = reunionSYS_TODOLIST();
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.FLOWFILENAME = Request.getQueryStringByName("FLOWFILENAME");
    urlParam.PROVIDER_NAME = $(".info-form", "#" + dataFormId).form('options').remoteName;
    urlParam.FORM_KEYS = keyColumns;
    urlParam.FORM_PRESENTATION = keyValues;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            if (message.toString().indexOf("Activity:") == 0) {
                $('#result_FromSubmit')[0].innerHTML = message;
            }
            else {
                window.open(urlPrdfix + "WorkflowFiles/PreView/" + message, '_blank', 'location=no');
            }
        },
        error: function () {
            hideLoading();
            return false;
        }
    });
}

function btnPreview_FromApproveClick() {
    showLoading();

    var txtSuggest_FromApprove_Value = $('#txtSuggest_FromApprove').val();
    var ddlRoles_FromApprove_Value = $('#ddlRoles_FromApprove').val();
    var chkImportant_FromApprove_value = $('#chkImportant_FromApprove').attr("checked");
    var chkUrgent_FromApprove_value = $('#chkUrgent_FromApprove').attr("checked");
    var selectedRow = reunionSYS_TODOLIST();

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Preview";
    urlParam.suggest = encodeURIComponent(txtSuggest_FromApprove_Value);
    urlParam.roles = ddlRoles_FromApprove_Value;
    urlParam.important = chkImportant_FromApprove_value;
    urlParam.urgent = chkUrgent_FromApprove_value;

    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = decodeURIComponent(selectedRow.PROVIDER_NAME);
    urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
    urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

    if (selectedRow.FORM_KEYS == undefined || selectedRow.FORM_KEYS == null || selectedRow.FORM_KEYS == "") {
        var dataForm = $('#' + dataFormId);
        var infolightOptions = getInfolightOption(dataForm);
        var keys = "";
        var dialoggrid = dataForm.attr('dialogGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('switchGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('continueGrid');
        var key = $(dialoggrid).attr('keyColumns');
        var keyValues = "";
        var keys = key.split(",");
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] != "") {
                var value = $("#" + dataForm.attr("id") + keys[i]).val();
                if (value != "") {
                    keyValues += keys[i] + "='" + value + "';";
                }
            }
        }
        urlParam.FORM_KEYS = key;
    }
    else {
        urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    }

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            if (message.toString().indexOf("Activity:") == 0) {
                $('#result_FromApprove')[0].innerHTML = message;
            }
            else {
                window.open(urlPrdfix + "WorkflowFiles/PreView/" + message);
            }
            //$('#result')[0].innerHTML = message;
        },
        error: function () {
            hideLoading();
            return false;
        }
    });
}

function btnPreview_FromReturnClick() {
    showLoading();

    var txtSuggest_FromReturn_Value = $('#txtSuggest_FromReturn').val();
    var ddlRoles_FromReturn_Value = $('#ddlRoles_FromReturn').val();
    var ddlReturnStep_FromReturn_Value = $('#ddlReturnStep_FromReturn').val();
    var ddlReturnStep_FromReturn_Text = $('#ddlReturnStep_FromReturn').val();
    var chkImportant_FromReturn_value = $('#chkImportant_FromReturn').attr("checked");
    var chkUrgent_FromReturn_value = $('#chkUrgent_FromReturn').attr("checked");

    var urlParam = {};
    urlParam.Type = "Workflow";
    urlParam.Active = "Preview";
    urlParam.suggest = encodeURIComponent(txtSuggest_FromReturn_Value);
    urlParam.roles = ddlRoles_FromReturn_Value;
    urlParam.returnstep = ddlReturnStep_FromReturn_Value;
    urlParam.returnsteptext = ddlReturnStep_FromReturn_Text;
    urlParam.important = chkImportant_FromReturn_value;
    urlParam.urgent = chkUrgent_FromReturn_value;

    var selectedRow = reunionSYS_TODOLIST();
    urlParam.LISTID = selectedRow.LISTID;
    urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
    urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
    urlParam.FLOWPATH = selectedRow.FLOWPATH;
    urlParam.STATUS = selectedRow.STATUS;
    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;
    urlParam.MULTISTEPRETURN = selectedRow.MULTISTEPRETURN;

    if (selectedRow.FORM_KEYS == undefined || selectedRow.FORM_KEYS == null || selectedRow.FORM_KEYS == "") {
        var dataForm = $('#' + dataFormId);
        var infolightOptions = getInfolightOption(dataForm);
        var keys = "";
        var dialoggrid = dataForm.attr('dialogGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('switchGrid');
        if (dialoggrid == undefined) dialoggrid = dataForm.attr('continueGrid');
        var key = $(dialoggrid).attr('keyColumns');
        var keyValues = "";
        var keys = key.split(",");
        for (var i = 0; i < keys.length; i++) {
            if (keys[i] != "") {
                var value = $("#" + dataForm.attr("id") + keys[i]).val();
                if (value != "") {
                    keyValues += keys[i] + "='" + value + "';";
                }
            }
        }
        urlParam.FORM_KEYS = key;
    }
    else {
        urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
    }

    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: urlParam,
        cache: false,
        async: true,
        success: function (message) {
            hideLoading();
            window.open(urlPrdfix + "WorkflowFiles/PreView/" + message);
            //$('#result')[0].innerHTML = message;
        },
        error: function () {
            hideLoading();
            return false;
        }
    });
}

function reunionSYS_TODOLIST() {
    var newSYS_TODOLIST = {};
    newSYS_TODOLIST["LISTID"] = Request.getQueryStringByName("LISTID");
    newSYS_TODOLIST["FLOW_ID"] = Request.getQueryStringByName("FLOW_ID");
    newSYS_TODOLIST["FLOW_DESC"] = Request.getQueryStringByName("FLOW_DESC");
    newSYS_TODOLIST["APPLICANT"] = Request.getQueryStringByName("APPLICANT");
    newSYS_TODOLIST["S_USER_ID"] = Request.getQueryStringByName("S_USER_ID");
    newSYS_TODOLIST["S_STEP_ID"] = Request.getQueryStringByName("S_STEP_ID");
    newSYS_TODOLIST["S_STEP_DESC"] = Request.getQueryStringByName("S_STEP_DESC");
    newSYS_TODOLIST["D_STEP_ID"] = Request.getQueryStringByName("D_STEP_ID");
    newSYS_TODOLIST["D_STEP_DESC"] = Request.getQueryStringByName("D_STEP_DESC");
    newSYS_TODOLIST["EXP_TIME"] = Request.getQueryStringByName("EXP_TIME");
    newSYS_TODOLIST["URGENT_TIME"] = Request.getQueryStringByName("URGENT_TIME");
    newSYS_TODOLIST["TIME_UNIT"] = Request.getQueryStringByName("TIME_UNIT");
    newSYS_TODOLIST["USERNAME"] = Request.getQueryStringByName("USERNAME");
    newSYS_TODOLIST["FORM_NAME"] = Request.getQueryStringByName("FORM_NAME");
    newSYS_TODOLIST["NAVIGATOR_MODE"] = Request.getQueryStringByName("NAVIGATOR_MODE");
    newSYS_TODOLIST["FLNAVIGATOR_MODE"] = Request.getQueryStringByName("FLNAVIGATOR_MODE");
    newSYS_TODOLIST["PARAMETERS"] = Request.getQueryStringByName("PARAMETERS");
    newSYS_TODOLIST["SENDTO_KIND"] = Request.getQueryStringByName("SENDTO_KIND");
    newSYS_TODOLIST["SENDTO_ID"] = Request.getQueryStringByName("SENDTO_ID");
    newSYS_TODOLIST["FLOWIMPORTANT"] = Request.getQueryStringByName("FLOWIMPORTANT");
    newSYS_TODOLIST["FLOWURGENT"] = Request.getQueryStringByName("FLOWURGENT");
    newSYS_TODOLIST["STATUS"] = Request.getQueryStringByName("STATUS");
    newSYS_TODOLIST["FORM_TABLE"] = Request.getQueryStringByName("FORM_TABLE");
    newSYS_TODOLIST["FORM_KEYS"] = Request.getQueryStringByName("FORM_KEYS");
    newSYS_TODOLIST["FORM_PRESENTATION"] = Request.getQueryStringByName("FORM_PRESENTATION");
    newSYS_TODOLIST["FORM_PRESENT_CT"] = Request.getQueryStringByName("FORM_PRESENT_CT");
    newSYS_TODOLIST["REMARK"] = Request.getQueryStringByName("REMARK");
    newSYS_TODOLIST["PROVIDER_NAME"] = Request.getQueryStringByName("PROVIDER_NAME");
    newSYS_TODOLIST["VERSION"] = Request.getQueryStringByName("VERSION");
    newSYS_TODOLIST["EMAIL_ADD"] = Request.getQueryStringByName("EMAIL_ADD");
    newSYS_TODOLIST["EMAIL_STATUS"] = Request.getQueryStringByName("EMAIL_STATUS");
    newSYS_TODOLIST["VDSNAME"] = Request.getQueryStringByName("VDSNAME");
    newSYS_TODOLIST["SENDBACKSTEP"] = Request.getQueryStringByName("SENDBACKSTEP");
    newSYS_TODOLIST["LEVEL_NO"] = Request.getQueryStringByName("LEVEL_NO");
    newSYS_TODOLIST["WEBFORM_NAME"] = Request.getQueryStringByName("WEBFORM_NAME");
    newSYS_TODOLIST["UPDATE_DATE"] = Request.getQueryStringByName("UPDATE_DATE");
    newSYS_TODOLIST["UPDATE_TIME"] = Request.getQueryStringByName("UPDATE_TIME");
    newSYS_TODOLIST["FLOWPATH"] = Request.getQueryStringByName("FLOWPATH");
    newSYS_TODOLIST["PLUSAPPROVE"] = Request.getQueryStringByName("PLUSAPPROVE");
    newSYS_TODOLIST["PLUSROLES"] = Request.getQueryStringByName("PLUSROLES");
    newSYS_TODOLIST["MULTISTEPRETURN"] = Request.getQueryStringByName("MULTISTEPRETURN");
    newSYS_TODOLIST["SENDTO_NAME"] = Request.getQueryStringByName("SENDTO_NAME");
    newSYS_TODOLIST["ATTACHMENTS"] = Request.getQueryStringByName("ATTACHMENTS");
    newSYS_TODOLIST["CREATE_TIME"] = Request.getQueryStringByName("CREATE_TIME");
    return newSYS_TODOLIST;
}

function btnUpload_FromSubmitClick() {
    $(".info-upload-button").each(function () {
        infoFileUploadMethod(this, afterUpload);
    });
}

function btnUpload_FromApproveClick() {
    $(".info-upload-button").each(function () {
        infoFileUploadMethod(this, afterUpload);
    });
}

function btnUpload_FromReturnClick() {
    $(".info-upload-button").each(function () {
        infoFileUploadMethod(this, afterUpload);
    });
}

function afterUpload(fileName) {
    //fileName = fileName.replace(/ /g, "&nbsp;");
    var urlPrdfix = "";
    if (isSubPath)
        urlPrdfix = "../";
    var href = urlPrdfix + "WorkflowFiles/" + fileName;
    var link = "<li><A href='" + href + "' target='_blank ' >" + fileName + "</A></li>"; //<a class='easyui-linkbutton' iconcls='icon-no' plain='true' id='A1'></a>
    $('.ulFiles').each(function () {
        $(link).appendTo(this);
        $("#" + this.id).listview("refresh");
    });
}

function initFileUpload(selectedRow, ulName) {
    if (selectedRow.ATTACHMENTS != "") {
        var urlPrdfix = "";
        if (isSubPath)
            urlPrdfix = "../";
        var ul = $('#' + ulName);
        var lstAttachments = selectedRow.ATTACHMENTS.split(';');
        for (var i = 0; i < lstAttachments.length; i++) {
            if (lstAttachments[i] != "" && lstAttachments[i] != "null") {
                var fileName = lstAttachments[i]; //.replace(/ /g, "&nbsp;");
                var href = urlPrdfix + "WorkflowFiles/" + fileName;
                var link = "<li><A id='" + "ATTACHMENTS" + i + "' href='" + href + "' target='_blank ' >" + fileName + "</A></li>";
                $(link).appendTo(ul);
            }
        }
        ul.listview("refresh");
    }
}

function infoFileUploadMethod(infofileUpload, onAfterUploadSuccess, onAfterUploadError) {
    var fileId = infofileUpload.id;
    var fileexist = true;
    if ($('#' + fileId) == undefined || $('#' + fileId).val() == "") {
        fileexist = false;
        return;
    }
    if (fileexist) {
        var postUrl = "";
        if (isSubPath == undefined || isSubPath == true) {
            postUrl = "../handler/UploadHandler.ashx";
        }
        else {
            postUrl = "handler/UploadHandler.ashx";
        }

        $.ajaxFileUpload({
            url: postUrl, //需要链接到服务器地址   
            secureuri: false,
            data: {
                filter: "",
                isAutoNum: true,
                UpLoadFolder: "WorkflowFiles"
            },
            fileElementId: fileId, //文件选择框的id属性   
            dataType: 'json', //服务器返回的格式，可以是json   
            success: function (data) {
                if (data['result'] == "success") {
                    if (onAfterUploadSuccess != undefined) {
                        onAfterUploadSuccess(data.message);
                    }
                }
                else {
                    if (onAfterUploadError != undefined) {
                        onAfterUploadError(data.message);
                    }
                }
            },
            error: function (data) {
                alert(data);
                if (onAfterUploadError != undefined) {
                    onAfterUploadError(data.message);
                }
            }
        });
    }
};

function btnRefresh_InboxClick() {
    var selected = $("#ddlToDoListFilter_Inbox").val();
    var queryParam = $("#tbQuery_Inbox").val();
    initializeInbox(selected, queryParam);
}

function btnRefresh_OutboxClick() {
    var selected = $("#ddlToDoHisFilter_Outbox").val();
    var queryParam = $("#tbQuery_Outbox").val();
    if ($("#cbSubmitted_Outbox").attr("checked") == "checked") {
        initializeFlowRunOver(selected, queryParam);
    }
    else {
        initializeOutbox(selected, queryParam);
    }
}

function btnRefresh_DelayClick(e, ee, eee) {
    var level = $("#sOvertimeColumn_Delay").val();
    initializeDelay(level);
}

function showLoading() {
    $.mobile.loading('show', { theme: 'd', text: '', textVisible: false });
}

function hideLoading() {
    $.mobile.loading('hide');
}
function RetakeVisible(row, flag) {
    var urlPrdfix = "";
    if (flag)
        urlPrdfix = "../";
    if (row.STATUS != undefined) {
        var status = row.STATUS;
        if (status != null && (status == "NR" || status == "A" || status == "Return" || status == "Plus" || status == "退回" || status == "加簽" || status == "退回" || status == "加签")) {
            return false;
        }
    }
    if (row.PLUSROLES != undefined) {
        var plusroles = row.PLUSROLES;
        if (plusroles != null && plusroles.Length > 0) {
            return false;
        }
    }
    if (row.S_USER_ID != undefined) {
        var user = row.S_USER_ID;
        var fLoginUser = "";
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: urlPrdfix + 'handler/SystemHandle.ashx?type=ClientInfo',
            async: false,
            cache: false,
            success: function (clientInfo) {
                fLoginUser = clientInfo.UserID;
            },
            error: function (err) {

            }
        });
        if (user != null && user == fLoginUser) {
            return true;
        }
    }

    return false;
}