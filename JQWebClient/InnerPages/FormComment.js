function formCommentLoaded(selectedRow, dataFormId) {
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }


    try {
        var flowUIText = $.sysmsg('getValue', 'FLRuntime/InstanceManager/CommentTableHeader');
        var flowUITexts = flowUIText.split(',');
        $('#gdvHis_FromComment').datagrid({
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx?type=gdvHis&LISTID=' + selectedRow.LISTID,
            columns: [[
                                { field: 'S_STEP_ID', title: flowUITexts[0], sortable: true, width: 80 }, //'作业名称'
                                { field: 'USER_ID', title: flowUITexts[1], sortable: true, width: 80 }, //'用户编号'
                                { field: 'USERNAME', title: flowUITexts[2], sortable: true, width: 80 }, //'寄件者'
                                { field: 'STATUS', title: flowUITexts[3], sortable: true, width: 50 }, //'情况'
            //{ field: 'FORM_PRESENT_CT', title: flowUITexts[2], sortable: true, width: 150 },//'单据号码'
                                {
                                    field: 'REMARK', title: flowUITexts[4], sortable: true, width: 120, formatter: function (value, row, index) {
                                        return decodeURIComponent(value);
                                    }
                                }, //'讯息'
                                //{ field: 'UPDATE_DATE', title: flowUITexts[6], sortable: true, width: 80 }, //'日期'
                                //{ field: 'UPDATE_TIME', title: flowUITexts[7], sortable: true, width: 60 }, //'时间'
                                { field: 'UPDATE_WHOLE_TIME', title: flowUITexts[7], sortable: true, width: 140 }, //'时间'
                                {
                                    field: 'ATTACHMENTS', title: flowUITexts[8], sortable: true, width: 120, formatter: function (value, row, index) {
                                        var link = "";
                                        if (value != null && value != "") {
                                            var lstAttachments = value.split(';');
                                            for (var i = 0; i < lstAttachments.length; i++) {
                                                if (lstAttachments[i] != "" && lstAttachments[i] != "null") {
                                                    var realFileName = lstAttachments[i];
                                                    var fileName = realFileName.replace(/__/g, "&nbsp;");
                                                    var href = urlPrdfix + "WorkflowFiles/" + realFileName;
                                                    link += "<A id='" + "ATTACHMENTS" + i + "' href='" + href + "' target='_blank' class=" + realFileName + " >" + fileName + "</A>&nbsp&nbsp";
                                                }
                                            }
                                        }
                                        return link;
                                    }
                                }//'相关文件'
            ]],
            type: 'gdvHis',
            fitColumns: false,
            sortName: "UPDATE_WHOLE_TIME",
            sortOrder: "asc",
            remoteSort: false,
            onLoadSuccess: function (data) {
                if (data.rows.length > 0) {
                    var index = data.rows.length - 1;
                    $('#FLOW_DESC_FormComment')[0].innerHTML = data.rows[index].FLOW_DESC;
                    $('#FORM_PRESENT_CT_FormComment')[0].innerHTML = data.rows[index].FORM_PRESENT_CT;
                    $('#USER_FormComment')[0].innerHTML = data.rows[index].USER_ID + "(" + data.rows[index].USERNAME + ")";
                    $('#ROLE_FormComment')[0].innerHTML = data.rows[index].S_ROLE_ID;
                    $('#WHOLEDATETIME_FormComment')[0].innerHTML = data.rows[index].UPDATE_DATE + " " + data.rows[index].UPDATE_TIME;
                }
                for (var i = data.rows.length - 1; i > 0; i--) {
                    if (data.rows[i].STATUS == 'Z' || data.rows[i].STATUS == '结案' || data.rows[i].STATUS == '結案'
                        || data.rows[i].STATUS == 'X' || data.rows[i].STATUS == '作废' || data.rows[i].STATUS == '作廢') {
                        $('#btnPreview_FormComment').hide();
                        return;
                    }
                }
            }
        });

        flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/CommitText');
        flowUITexts = flowUIText.split(',');
        $('#lFLOW_DESC_FormComment')[0].innerHTML = flowUITexts[0];
        $('#lFORM_PRESENT_CT_FormComment')[0].innerHTML = flowUITexts[4];
        $('#lUSER_FormComment')[0].innerHTML = flowUITexts[2];
        $('#lROLE_FormComment')[0].innerHTML = flowUITexts[3];
        $('#lWHOLEDATETIME_FormComment')[0].innerHTML = flowUITexts[1];

        flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        flowUITexts = flowUIText.split(',');

        $('#btnPreview_FormComment').linkbutton({ text: flowUITexts[14] });
        if (selectedRow.STATUS == 'Z' || selectedRow.STATUS == '结案' || selectedRow.STATUS == '結案'
            || selectedRow.STATUS == 'X' || selectedRow.STATUS == '作废' || selectedRow.STATUS == '作廢') {
            $('#btnPreview_FormComment').hide();
        }
        $('#btnPreview_FormComment').click(function () {
            if ($('#btnPreview_FormComment').attr("href") == "#") {
                var txtSuggest_FromApprove_Value = '';
                var ddlRoles_FromApprove_Value = '';
                var chkImportant_FromApprove_value = false;
                var chkUrgent_FromApprove_value = false;

                var urlParam = {};
                urlParam.Type = 'Workflow';
                urlParam.active = 'Preview';
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
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }

    $.ajax({
        type: "POST",
        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
        data: { Type: "gdvHis", LISTID: selectedRow.LISTID },
        cache: false,
        async: false,
        success: function (data) {
            data = eval('(' + data + ')');
            $('#gdvHis_FromComment').datagrid("loadData", []);
            $('#gdvHis_FromComment').datagrid("loadData", data);
        },
        error: function (data) {
        }
    });
}