function formSubmitLoaded(selectedRow, dataFormId, winId) {
    $("#" + winId).attr("isDone", false);
    //var isSubmit = false;
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FromSubmit').linkbutton({ text: flowUITexts[6] });
        $('#btnCancel_FromSubmit').linkbutton({ text: flowUITexts[13] });
        $('#btnPreview_FromSubmit').linkbutton({ text: flowUITexts[14] });
        $('#btnUploadFile_FromSubmit').linkbutton({ text: flowUITexts[12] });
        //$('#btnOk_FromSubmit')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        //$('#btnCancel_FromSubmit')[0].firstChild.firstChild.innerHTML = flowUITexts[13];
        //$('#btnPreview_FromSubmit')[0].firstChild.firstChild.innerHTML = flowUITexts[14];
        //$('#btnUploadFile_FromSubmit')[0].firstChild.firstChild.innerHTML = flowUITexts[12];
        $('#lFieldset_FromSubmit').html(flowUITexts[0]);
        $('#lImportant_FromSubmit').html(flowUITexts[1]);
        $('#lUrgent_FromSubmit').html(flowUITexts[2]);
        $('#lSenderRole_FromSubmit').html(flowUITexts[5]);
        //        $('#lFieldset_FromSubmit')[0].innerHTML = flowUITexts[0];
        //        $('#lImportant_FromSubmit')[0].innerHTML = flowUITexts[1];
        //        $('#lUrgent_FromSubmit')[0].innerHTML = flowUITexts[2];
        //        $('#lSenderRole_FromSubmit')[0].innerHTML = flowUITexts[5];

        if ($(".tabs-title", "#formSubmit").length > 0) {
            $($(".tabs-title", "#formSubmit")[0]).html(flowUITexts[3]);
            $($(".tabs-title", "#formSubmit")[1]).html(flowUITexts[4]);
        }
        //$('#suggestionView_FromSubmit').panel("setTitle", flowUITexts[3]);
        //$('#historyView_FromSubmit').panel("setTitle", flowUITexts[4]);

        $('#ddlRoles_FromSubmit').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: "ddlRoles", LISTID: selectedRow.LISTID },
            onLoadSuccess: function () {
                var data = $('#ddlRoles_FromSubmit').combobox('getData');
                if (data.length > 0)
                    $('#ddlRoles_FromSubmit').combobox('select', data[0].GROUPID);
            }
        });
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: "ddlRoles", LISTID: selectedRow.LISTID },
            cache: false,
            async: true,
            success: function (data) {
                data = eval(data);
                $('#ddlRoles_FromSubmit').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        $('#tabsFromSubmit').tabs({
            onSelect: function (title, index) {
                if (index == 1) {
                    flowCommentTableHeader = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
                    flowCommentTableHeaders = flowCommentTableHeader.split(',');
                    $('#gdvHis_FromSubmit').datagrid({
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
                                            { field: 'UPDATE_WHOLE_TIME', title: flowUITexts[7], sortable: true, width: 140 }, //'时间'
                        ]],
                        type: 'gdvHis',
                        fitColumns: false,
                        sortName: "UPDATE_WHOLE_TIME",
                        fitColumns: false,
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
                        data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
                        cache: false,
                        async: true,
                        success: function (data) {
                            data = eval(data);
                            $('#gdvHis_FromSubmit').datagrid("loadData", data);
                        },
                        error: function () {
                            return false;
                        }
                    });
                }
            }
        })

        if (selectedRow.FLOWIMPORTANT == "1")
            $('#chkImportant_FromSubmit').attr("checked", "checked");
        if (selectedRow.FLOWURGENT == "1")
            $('#chkUrgent_FromSubmit').attr("checked", "checked");

        $('#btnOk_FromSubmit').click(function () {
            if ($('#btnOk_FromSubmit').attr("href") == "#") {
                if ($("#" + winId).attr("isDone") == "true")
                    return;
                else
                    $("#" + winId).attr("isDone", true);

                $('#btnOk_FromSubmit').hide();
                //$('#btnCancel_FromSubmit').hide();
                $('#btnPreview_FromSubmit').hide();
                $('#btnUploadFile_FromSubmit').hide();

                var txtSuggest_FromSubmit_Value = $('#txtSuggest_FromSubmit').val();
                var ddlRoles_FromSubmit_Value = $('#ddlRoles_FromSubmit').combobox('getValue');
                var chkImportant_FromSubmit_value = $('#chkImportant_FromSubmit').attr("checked");
                var chkUrgent_FromSubmit_value = $('#chkUrgent_FromSubmit').attr("checked");
                var ulFiles_FromSubmit_value = "";
                $('#ulFiles_FromSubmit li').each(function (li) {
                    ulFiles_FromSubmit_value += this.firstChild.className + ";";
                });
                //ulFiles_FromSubmit_value = ulFiles_FromSubmit_value.replace(/&nbsp;/g, " ");

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Submit";
                urlParam.roles = ddlRoles_FromSubmit_Value;
                urlParam.important = chkImportant_FromSubmit_value;
                urlParam.urgent = chkUrgent_FromSubmit_value;
                urlParam.suggest = txtSuggest_FromSubmit_Value;
                urlParam.ATTACHMENTS = ulFiles_FromSubmit_value;

                var dataForm = $('#' + dataFormId);
                var infolightOptions = getInfolightOption(dataForm);
                var keys = "";
                var dialoggrid = dataForm.attr('dialogGrid');
                if (dialoggrid == undefined) dialoggrid = dataForm.attr('switchGrid');
                if (dialoggrid == undefined) dialoggrid = dataForm.attr('continueGrid');
                //var tableName = $(dialoggrid).attr('tableName');
                var key = $(dialoggrid).attr('keyColumns');
                var keyValues = "";
                var keys = key.split(",");
                for (var i = 0; i < keys.length; i++) {
                    if (keys[i] != "") {
                        var control = $("#" + dataForm.attr("id") + keys[i]);
                        var controlClass = control.attr('class');
                        if (controlClass != undefined) {
                            if (controlClass.indexOf('easyui-datebox') == 0) {
                                value = control.datebox('getBindingValue');
                            }
                            else if (controlClass.indexOf('easyui-combobox') == 0) {
                                value = control.combobox('getValue');
                            }
                            else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                                value = control.datetimebox('getBindingValue');
                            }
                            else if (controlClass.indexOf('easyui-combogrid') == 0) {
                                value = control.combogrid('getValue');
                            }
                            else if (controlClass.indexOf('info-combobox') == 0) {
                                value = control.combobox('getValue');
                            }
                            else if (controlClass.indexOf('info-combogrid') == 0) {
                                value = control.combogrid('getValue');
                            }
                            else if (controlClass.indexOf('info-refval') == 0) {
                                value = control.refval('getValue');
                            }
                            else {
                                value = control.val();
                            }
                        }
                        else {
                            value = control.val();
                        }

                        if (value != "") {
                            //keyValues += tableName + "." + keys[i] + "='" + value + "';";
                            keyValues += keys[i] + "='" + value + "';";
                        }
                    }
                }
                keyValues = keyValues.substring(0, keyValues.lastIndexOf(";"));
                urlParam.LISTID = selectedRow.LISTID;
                urlParam.FLOWFILENAME = Request.getQueryStringByName("FLOWFILENAME");
                urlParam.PROVIDER_NAME = infolightOptions.remoteName;
                urlParam.FORM_KEYS = key;
                urlParam.FORM_PRESENTATION = keyValues;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
                urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

                $.messager.progress({ title: 'Please waiting', msg: 'Submiting...' });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        $('#result_FromSubmit')[0].innerHTML = message;
                        window.top.FlowRefreshInbox.call();
                        window.top.FlowRefreshOutbox.call();
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

        $('#btnPreview_FromSubmit').click(function () {
            if ($('#btnPreview_FromSubmit').attr("href") == "#") {
                var txtSuggest_FromSubmit_Value = $('#txtSuggest_FromSubmit').val();
                var ddlRoles_FromSubmit_Value = $('#ddlRoles_FromSubmit').combobox('getValue');
                var chkImportant_FromSubmit_value = $('#chkImportant_FromSubmit').attr("checked");
                var chkUrgent_FromSubmit_value = $('#chkUrgent_FromSubmit').attr("checked");

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Preview";
                urlParam.suggest = txtSuggest_FromSubmit_Value;
                urlParam.roles = ddlRoles_FromSubmit_Value;
                urlParam.important = chkImportant_FromSubmit_value;
                urlParam.urgent = chkUrgent_FromSubmit_value;

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
                        var control = $("#" + dataForm.attr("id") + keys[i]);
                        var controlClass = control.attr('class');
                        if (controlClass != undefined) {
                            if (controlClass.indexOf('easyui-datebox') == 0) {
                                value = control.datebox('getBindingValue');
                            }
                            else if (controlClass.indexOf('easyui-combobox') == 0) {
                                value = control.combobox('getValue');
                            }
                            else if (controlClass.indexOf('easyui-datetimebox') == 0) {
                                value = control.datetimebox('getBindingValue');
                            }
                            else if (controlClass.indexOf('easyui-combogrid') == 0) {
                                value = control.combogrid('getValue');
                            }
                            else if (controlClass.indexOf('info-combobox') == 0) {
                                value = control.combobox('getValue');
                            }
                            else if (controlClass.indexOf('info-combogrid') == 0) {
                                value = control.combogrid('getValue');
                            }
                            else if (controlClass.indexOf('info-refval') == 0) {
                                value = control.refval('getValue');
                            }
                            else {
                                value = control.val();
                            }
                        }
                        else {
                            value = control.val();
                        }
                        if (value != "") {
                            keyValues += keys[i] + "='" + value + "';";
                        }
                    }
                }
                keyValues = keyValues.substring(0, keyValues.lastIndexOf(";"));
                urlParam.LISTID = selectedRow.LISTID;
                urlParam.FLOWFILENAME = Request.getQueryStringByName("FLOWFILENAME");
                urlParam.PROVIDER_NAME = infolightOptions.remoteName;
                urlParam.FORM_KEYS = key;
                urlParam.FORM_PRESENTATION = keyValues;
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
                urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        if (message.toString().indexOf("Activity:") == 0) {
                            $('#result_FromSubmit')[0].innerHTML = message;
                        }
                        else {
                            openPreview(urlPrdfix + "WorkflowFiles/PreView/" + message);
                            //window.open(urlPrdfix + "WorkflowFiles/PreView/" + message);
                        }
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnUploadFile_FromSubmit').click(function () {
            if ($('#btnUploadFile_FromSubmit').attr("href") == "#") {
                //var selectedRow = reunionSYS_TODOLIST();
                createAndOpenWorkflowDialog("winFileUpload", flowUITexts[12], 550, 400, "InnerPages/FormFileUpload.aspx", selectedRow, formFileUploadLoaded);
            }
        });

        function initFileUpload() {
            if (selectedRow.ATTACHMENTS != "") {
                var ul = $('#ulFiles_FromSubmit');
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

        $('#btnCancel_FromSubmit').click(function () {
            if ($('#btnCancel_FromSubmit').attr("href") == "#") {
                $('#' + winId).dialog('close');
                //$(".easyui-dialog").each(function () {
                //    $(this).dialog('close');
                //});
                //if (isSubPath == true && isSubmit == true) {
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