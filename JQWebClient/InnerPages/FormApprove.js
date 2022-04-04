function formApproveLoaded(selectedRow, dataFormId, winId) {
    $("#" + winId).attr("isDone", false);
    //var isApprove = false;
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FromApprove').linkbutton({ text: flowUITexts[6] });
        $('#btnCancel_FromApprove').linkbutton({ text: flowUITexts[13] });
        $('#btnPreview_FromApprove').linkbutton({ text: flowUITexts[14] });
        $('#btnUploadFile_FromApprove').linkbutton({ text: flowUITexts[12] });
        //$('#btnOk_FromApprove')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        //$('#btnCancel_FromApprove')[0].firstChild.firstChild.innerHTML = flowUITexts[13];
        //$('#btnPreview_FromApprove')[0].firstChild.firstChild.innerHTML = flowUITexts[14];
        //$('#btnUploadFile_FromApprove')[0].firstChild.firstChild.innerHTML = flowUITexts[12];
        $('#lFieldset_FromApprove').html(flowUITexts[0]);
        $('#lImportant_FromApprove').html(flowUITexts[1]);
        $('#lUrgent_FromApprove').html(flowUITexts[2]);
        $('#lSenderRole_FromApprove').html(flowUITexts[5]);
        //        $('#lFieldset_FromApprove')[0].innerHTML = flowUITexts[0];
        //        $('#lImportant_FromApprove')[0].innerHTML = flowUITexts[1];
        //        $('#lUrgent_FromApprove')[0].innerHTML = flowUITexts[2];
        //        $('#lSenderRole_FromApprove')[0].innerHTML = flowUITexts[5];

        if ($(".tabs-title", "#formApprove").length > 0) {
            $($(".tabs-title", "#formApprove")[0]).html(flowUITexts[3]);
            $($(".tabs-title", "#formApprove")[1]).html(flowUITexts[4]);
        }
        //$('#suggestionView_FromApprove').panel("setTitle", flowUITexts[3]);
        //$('#historyView_FromApprove').panel("setTitle", flowUITexts[4]);

        $('#ddlRoles_FromApprove').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'ddlRoles', LISTID: selectedRow.LISTID },
            onLoadSuccess: function () {
                var data = $('#ddlRoles_FromApprove').combobox('getData');
                if (data.length > 0)
                    $('#ddlRoles_FromApprove').combobox('select', data[0].GROUPID);
                if (selectedRow != undefined && selectedRow.SENDTO_KIND == "1") {
                    $('#ddlRoles_FromApprove').combobox("setValue", selectedRow.SENDTO_ID);
                    $('#ddlRoles_FromApprove').combobox("disable");
                }
            }
        });

        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: 'ddlRoles', LISTID: selectedRow.LISTID },
            cache: false,
            async: true,
            success: function (data) {
                data = eval(data);
                $('#ddlRoles_FromApprove').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        $('#tabsFromApprove').tabs({
            onSelect: function (title, index) {
                if (index == 1) {
                    var flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                    var flowCommentTableHeaders = flowCommentTableHeader.split(',');
                    $('#gdvHis_FromApprove').datagrid({
                        //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                        //data: { type: 'gdvHis', LISTID: selectedRow.LISTID },
                        columns: [[
                                            { field: 'S_STEP_ID', title: flowCommentTableHeaders[0], sortable: true, width: 80 }, //'作业名称'
                                            { field: 'USER_ID', title: flowCommentTableHeaders[1], sortable: true, width: 80 }, //'用户编号'
                                            { field: 'USERNAME', title: flowCommentTableHeaders[2], sortable: true, width: 80 }, //'寄件者'
                                            { field: 'STATUS', title: flowCommentTableHeaders[3], sortable: true, width: 50 }, //'情况'
                        //{ field: 'FORM_PRESENT_CT', title: '单据号码', sortable: true, width: 150 },
                                            { field: 'REMARK', title: flowCommentTableHeaders[4], sortable: true, width: 120 }, //'讯息'
                                            //{field: 'UPDATE_DATE', title: flowCommentTableHeaders[6], sortable: true, width: 80 }, //'日期'
                                            //{field: 'UPDATE_TIME', title: flowCommentTableHeaders[7], sortable: true, width: 60}//'时间'
                                            { field: 'UPDATE_WHOLE_TIME', title: flowCommentTableHeaders[7], sortable: true, width: 100 }, //'时间'
                        ]],
                        type: 'gdvHis',
                        fitColumns: false,
                        sortName: "UPDATE_WHOLE_TIME",
                        sortOrder: "asc",
                        onSelect: function (rowIndex, rowData) {

                        },
                        onSortColumn: function (sort, order) {
                            if (self.filter != undefined) {

                            }
                        },
                        onDblClickRow: function (rowIndex, rowData) {

                        },
                        onClickCell: function (rowIndex, field, value) {

                        }
                    });

                    $.ajax({
                        type: "POST",
                        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                        data: { type: 'gdvHis', LISTID: selectedRow.LISTID },
                        cache: false,
                        async: true,
                        success: function (data) {
                            data = eval(data);
                            $('#gdvHis_FromApprove').datagrid("loadData", data);
                        },
                        error: function () {
                            return false;
                        }
                    });
                }
            }
        });

        if (selectedRow.FLOWIMPORTANT == "1")
            $('#chkImportant_FromApprove').attr("checked", "checked");
        if (selectedRow.FLOWURGENT == "1")
            $('#chkUrgent_FromApprove').attr("checked", "checked");

        $('#btnOk_FromApprove').click(function () {
            if ($('#btnOk_FromApprove').attr("href") == "#") {
                if ($("#" + winId).attr("isDone") == "true")
                    return;
                else
                    $("#" + winId).attr("isDone", true);
                $('#btnOk_FromApprove').hide();
                //$('#btnCancel_FromApprove').hide();
                $('#btnPreview_FromApprove').hide();
                $('#btnUploadFile_FromApprove').hide();

                var txtSuggest_FromApprove_Value = $('#txtSuggest_FromApprove').val();
                var ddlRoles_FromApprove_Value = $('#ddlRoles_FromApprove').combobox('getValue');
                var chkImportant_FromApprove_value = $('#chkImportant_FromApprove').attr("checked");
                var chkUrgent_FromApprove_value = $('#chkUrgent_FromApprove').attr("checked");
                var ulFiles_FromApprove_value = "";
                $('#ulFiles_FromApprove li').each(function (li) {
                    ulFiles_FromApprove_value += this.firstChild.className + ";";
                });
                //ulFiles_FromApprove_value = ulFiles_FromApprove_value.replace(/&nbsp;/g, " ");

                var urlParam = {};
                urlParam.Type = 'Workflow';
                urlParam.Active = 'Approve';
                var status = decodeURIComponent(selectedRow.STATUS);
                if ((status == "A" || status == "AA" || status == "加签" || status == "加簽" || status == "Plus") && selectedRow.SENDTO_KIND == "2") {
                    urlParam.roles = "";
                }
                else {
                    urlParam.roles = ddlRoles_FromApprove_Value;
                }
                urlParam.important = chkImportant_FromApprove_value;
                urlParam.urgent = chkUrgent_FromApprove_value;
                urlParam.suggest = txtSuggest_FromApprove_Value;
                urlParam.ATTACHMENTS = ulFiles_FromApprove_value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = status;
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

                $.messager.progress({ title: 'Please waiting', msg: 'Approving...' });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        $('#result_FromApprove').html(message);
                        if (winId) {
                            if (!urlPrdfix) {
                                RefreshInbox();
                                RefreshOutbox();
                            }
                            else {
                                window.top.FlowRefreshInbox.call();
                                window.top.FlowRefreshOutbox.call();
                            }
                        }
                    },
                    error: function () {
                        $("#" + winId).attr("isDone", false);
                        return false;
                    },
                    complete: function () {
                        $.messager.progress('close');
                    }
                });
            }
        });

        $('#btnPreview_FromApprove').click(function () {
            if ($('#btnPreview_FromApprove').attr("href") == "#") {
                var txtSuggest_FromApprove_Value = $('#txtSuggest_FromApprove').val();
                var ddlRoles_FromApprove_Value = $('#ddlRoles_FromApprove').combobox('getValue');
                var chkImportant_FromApprove_value = $('#chkImportant_FromApprove').attr("checked");
                var chkUrgent_FromApprove_value = $('#chkUrgent_FromApprove').attr("checked");

                var urlParam = {};
                urlParam.Type = 'Workflow';
                urlParam.Active = "Preview";
                urlParam.suggest = txtSuggest_FromApprove_Value;
                urlParam.roles = ddlRoles_FromApprove_Value;
                urlParam.important = chkImportant_FromApprove_value;
                urlParam.urgent = chkUrgent_FromApprove_value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = decodeURIComponent(selectedRow.PROVIDER_NAME);
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
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

                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        if (message.toString().indexOf("Activity:") == 0) {
                            $('#result_FromApprove').html(message);
                        }
                        else {
                            openPreview(urlPrdfix + "WorkflowFiles/PreView/" + message);
                            //window.open(urlPrdfix + "WorkflowFiles/PreView/" + message);
                        }
                        //$('#result')[0].innerHTML = message;
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnUploadFile_FromApprove').click(function () {
            if ($('#btnUploadFile_FromApprove').attr("href") == "#") {
                //var selectedRow = reunionSYS_TODOLIST();
                createAndOpenWorkflowDialog("winFileUpload", flowUITexts[12], 450, 250, "InnerPages/FormFileUpload.aspx", selectedRow, formFileUploadLoaded);
            }
        });

        function initFileUpload() {
            if (selectedRow.ATTACHMENTS != "") {
                var ul = $('#ulFiles_FromApprove');
                ul.empty();
                var lstAttachments = selectedRow.ATTACHMENTS.split(';');
                for (var i = 0; i < lstAttachments.length; i++) {
                    if (lstAttachments[i] != "" && lstAttachments[i] != "null") {
                        var realFileName = lstAttachments[i];
                        var fileName = realFileName.replace(/__/g, "&nbsp;");
                        var href = urlPrdfix + "WorkflowFiles/" + realFileName;
                        var link = "<li><A id='" + "ATTACHMENTS" + i + "' href='" + href + "' target='_blank ' class=" + realFileName + " >" + fileName + "</A></li>";
                        $(link).appendTo(ul);
                    }
                }
            }
        }

        $('#btnCancel_FromApprove').click(function () {
            if ($('#btnCancel_FromApprove').attr("href") == "#") {
                $('#' + winId).dialog('close');
                //$(".easyui-dialog").each(function () {
                //    $(this).dialog('close');
                //});
                //if (isSubPath == true && isApprove == true) {
                //    var isAutoPageClose = getInfolightOption($("#" + dataFormId)).isAutoPageClose;
                //    if (isAutoPageClose) {
                //        self.parent.closeCurrentTab();
                //    }
                //}
            }
        });

        initFileUpload();

    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}