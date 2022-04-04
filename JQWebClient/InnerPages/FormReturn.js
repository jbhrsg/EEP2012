function formReturnLoaded(selectedRow, dataFormId, winId) {
    //var isReturn = false;
    $("#" + winId).attr("isDone", false);
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }


    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FromReturn').linkbutton({ text: flowUITexts[6] });
        $('#btnCancel_FromReturn').linkbutton({ text: flowUITexts[13] });
        $('#btnPreview_FromReturn').linkbutton({ text: flowUITexts[14] });
        $('#btnUploadFile_FromReturn').linkbutton({ text: flowUITexts[12] });
        //        $('#btnOk_FromReturn')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        //        $('#btnCancel_FromReturn')[0].firstChild.firstChild.innerHTML = flowUITexts[13];
        //        $('#btnPreview_FromReturn')[0].firstChild.firstChild.innerHTML = flowUITexts[14];
        //        $('#btnUploadFile_FromReturn')[0].firstChild.firstChild.innerHTML = flowUITexts[12];
        $('#lFieldset_FromReturn').html(flowUITexts[0]);
        $('#lImportant_FromReturn').html(flowUITexts[1]);
        $('#lUrgent_FromReturn').html(flowUITexts[2]);
        $('#lSenderRole_FromReturn').html(flowUITexts[5]);
        $('#lReturnTo_FromReturn').html(flowUITexts[10]);

        //        $('#lFieldset_FromReturn')[0].innerHTML = flowUITexts[0];
        //        $('#lImportant_FromReturn')[0].innerHTML = flowUITexts[1];
        //        $('#lUrgent_FromReturn')[0].innerHTML = flowUITexts[2];
        //        $('#lSenderRole_FromReturn')[0].innerHTML = flowUITexts[5];
        //        $('#lReturnTo_FromReturn')[0].innerHTML = flowUITexts[10];

        if ($(".tabs-title", "#formReturn").length > 0) {
            $($(".tabs-title", "#formReturn")[0]).html(flowUITexts[3]);
            $($(".tabs-title", "#formReturn")[1]).html(flowUITexts[4]);

        }
        //$('#suggestionView_FromReturn').panel("setTitle", flowUITexts[3]);
        //$('#historyView_FromReturn').panel("setTitle", flowUITexts[4]);

        $('#ddlRoles_FromReturn').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'ddlRoles', LISTID: selectedRow.LISTID },
            onLoadSuccess: function () {
                var data = $('#ddlRoles_FromReturn').combobox('getData');
                if (data.length > 0)
                    $('#ddlRoles_FromReturn').combobox('select', data[0].GROUPID);
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
                $('#ddlRoles_FromReturn').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        $('#ddlReturnStep_FromReturn').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'ddlReturnStep', LISTID: selectedRow.LISTID, FLOWPATH: encodeURIComponent(decodeURIComponent(selectedRow.FLOWPATH)) },
            onLoadSuccess: function () {
                var data = $('#ddlReturnStep_FromReturn').combobox('getData');
                if (data.length > 0)
                    $('#ddlReturnStep_FromReturn').combobox('select', data[0].RERURNSTEPID);
                if (selectedRow != undefined && selectedRow.SENDTO_KIND == "1") {
                    $('#ddlRoles_FromReturn').combobox("setValue", selectedRow.SENDTO_ID);
                    $('#ddlRoles_FromReturn').combobox("disable");
                }
            }
        });
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: 'ddlReturnStep', LISTID: selectedRow.LISTID, FLOWPATH: encodeURIComponent(decodeURIComponent(selectedRow.FLOWPATH)) },
            cache: false,
            async: true,
            success: function (data) {
                data = eval(data);
                $('#ddlReturnStep_FromReturn').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        $('#tabsFromReturn').tabs({
            onSelect: function (title, index) {
                if (index == 1) {
                    flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                    flowCommentTableHeaders = flowCommentTableHeader.split(',');
                    $('#gdvHis_FromReturn').datagrid({
                        //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                        //data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
                        columns: [[
                                            { field: 'S_STEP_ID', title: flowCommentTableHeaders[0], sortable: true, width: 80 }, //'作业名称'
                                            { field: 'USER_ID', title: flowCommentTableHeaders[1], sortable: true, width: 80 }, //'用户编号'
                                            { field: 'USERNAME', title: flowCommentTableHeaders[2], sortable: true, width: 80 }, //'寄件者'
                                            { field: 'STATUS', title: flowCommentTableHeaders[3], sortable: true, width: 50 }, //'情况'
                        //{ field: 'FORM_PRESENT_CT', title: '单据号码', sortable: true, width: 150 },
                                            { field: 'REMARK', title: flowCommentTableHeaders[4], sortable: true, width: 120 }, //'讯息'
                                            //{field: 'UPDATE_DATE', title: flowCommentTableHeaders[6], sortable: true, width: 80 }, //'日期'
                                            //{field: 'UPDATE_TIME', title: flowCommentTableHeaders[7], sortable: true, width: 60}//'时间'
                                            { field: 'UPDATE_WHOLE_TIME', title: flowCommentTableHeaders[7], sortable: true, width: 140 }, //'时间'
                        ]],
                        type: 'gdvHis',
                        fitColumns: false,
                        sortName: "UPDATE_WHOLE_TIME",
                        fitColumns: false
                    });
                    $.ajax({
                        type: "POST",
                        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                        data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
                        cache: false,
                        async: true,
                        success: function (data) {
                            data = eval(data);
                            $('#gdvHis_FromReturn').datagrid("loadData", data);
                        },
                        error: function () {
                            return false;
                        }
                    });
                }
            }
        });

        if (selectedRow.FLOWIMPORTANT == "1")
            $('#chkImportant_FromReturn').attr("checked", "checked");
        if (selectedRow.FLOWURGENT == "1")
            $('#chkUrgent_FromReturn').attr("checked", "checked");

        $('#btnOk_FromReturn').click(function () {
            if ($('#btnOk_FromReturn').attr("href") == "#") {
                if ($("#" + winId).attr("isDone") == "true")
                    return;
                else
                    $("#" + winId).attr("isDone", true);

                $('#btnOk_FromReturn').hide();
                //$('#btnCancel_FromReturn').hide();
                $('#btnPreview_FromReturn').hide();
                $('#btnUploadFile_FromReturn').hide();

                var txtSuggest_FromReturn_Value = $('#txtSuggest_FromReturn').val();
                var ddlRoles_FromReturn_Value = $('#ddlRoles_FromReturn').combobox('getValue');
                var ddlReturnStep_FromReturn_Value = $('#ddlReturnStep_FromReturn').combobox('getValue');
                var ddlReturnStep_FromReturn_Text = $('#ddlReturnStep_FromReturn').combobox('getText');
                var chkImportant_FromReturn_value = $('#chkImportant_FromReturn').attr("checked");
                var chkUrgent_FromReturn_value = $('#chkUrgent_FromReturn').attr("checked");
                var ulFiles_FromReturn_value = "";
                $('#ulFiles_FromReturn li').each(function (li) {
                    ulFiles_FromReturn_value += this.firstChild.className + ";";
                });
                //ulFiles_FromReturn_value = ulFiles_FromReturn_value.replace(/&nbsp;/g, " ");

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Return";
                urlParam.roles = ddlRoles_FromReturn_Value;
                urlParam.returnstep = ddlReturnStep_FromReturn_Value;
                urlParam.returnsteptext = ddlReturnStep_FromReturn_Text;
                urlParam.important = chkImportant_FromReturn_value;
                urlParam.urgent = chkUrgent_FromReturn_value;
                urlParam.suggest = txtSuggest_FromReturn_Value;
                urlParam.ATTACHMENTS = ulFiles_FromReturn_value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
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

                $.messager.progress({ title: 'Please waiting', msg: 'Returning...' });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        $('#result_FromReturn').html(message);
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
        $('#btnPreview_FromReturn').click(function () {
            if ($('#btnPreview_FromReturn').attr("href") == "#") {
                var txtSuggest_FromReturn_Value = $('#txtSuggest_FromReturn').val();
                var ddlRoles_FromReturn_Value = $('#ddlRoles_FromReturn').combobox('getValue');
                var ddlReturnStep_FromReturn_Value = $('#ddlReturnStep_FromReturn').combobox('getValue');
                var ddlReturnStep_FromReturn_Text = $('#ddlReturnStep_FromReturn').combobox('getText');
                var chkImportant_FromReturn_value = $('#chkImportant_FromReturn').attr("checked");
                var chkUrgent_FromReturn_value = $('#chkUrgent_FromReturn').attr("checked");

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Preview";
                urlParam.suggest = txtSuggest_FromReturn_Value;
                urlParam.roles = ddlRoles_FromReturn_Value;
                urlParam.returnstep = ddlReturnStep_FromReturn_Value;
                urlParam.returnsteptext = ddlReturnStep_FromReturn_Text;
                urlParam.important = chkImportant_FromReturn_value;
                urlParam.urgent = chkUrgent_FromReturn_value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION).replace(/''/g, "'");;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
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

                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        openPreview(urlPrdfix + "WorkflowFiles/PreView/" + message);
                        //window.open(urlPrdfix + "WorkflowFiles/PreView/" + message);
                        //$('#result')[0].innerHTML = message;
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnUploadFile_FromReturn').click(function () {
            if ($('#btnUploadFile_FromApprove').attr("href") == "#") {
                //var selectedRow = reunionSYS_TODOLIST();
                createAndOpenWorkflowDialog("winFileUpload", flowUITexts[12], 550, 400, "InnerPages/FormFileUpload.aspx", selectedRow, formFileUploadLoaded);
            }
        });

        function initFileUpload() {
            if (selectedRow.ATTACHMENTS != "") {
                var ul = $('#ulFiles_FromReturn');
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

        initFileUpload();

        $('#btnCancel_FromReturn').click(function () {
            if ($('#btnCancel_FromReturn').attr("href") == "#") {
                $('#' + winId).dialog('close');
                //$(".easyui-dialog").each(function () {
                //    $(this).dialog('close');
                //});
                //if (isSubPath == true && isReturn == true) {
                //    var isAutoPageClose = getInfolightOption($("#" + dataFormId)).isAutoPageClose;
                //    if (isAutoPageClose) {
                //        self.parent.closeCurrentTab();
                //    }
                //}
            }
        });
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}