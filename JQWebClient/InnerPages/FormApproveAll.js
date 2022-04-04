function formApproveAllLoaded(selectedRow, dataFormId, winId) {
    $("#" + winId).attr("isDone", false);

    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FromApproveALL')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        $('#btnCancel_FromApproveALL')[0].firstChild.firstChild.innerHTML = flowUITexts[13];

        $('#lFieldset_FromApproveALL')[0].innerHTML = flowUITexts[0];
        $('#lImportant_FromApproveALL')[0].innerHTML = flowUITexts[1];
        $('#lUrgent_FromApproveALL')[0].innerHTML = flowUITexts[2];
        $('#lSenderRole_FromApproveALL')[0].innerHTML = flowUITexts[5];

        $(".tabs-title", "#formApproveAll")[0].innerHTML = flowUITexts[3];

        $('#ddlRoles_FromApproveALL').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'ddlRoles', LISTID: selectedRow.LISTID },
            onLoadSuccess: function () {
                var data = $('#ddlRoles_FromApproveALL').combobox('getData');
                if (data.length > 0)
                    $('#ddlRoles_FromApproveALL').combobox('select', data[0].GROUPID);
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
                $('#ddlRoles_FromApproveALL').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        //if (selectedRow.FLOWIMPORTANT == "1")
        //    $('#chkImportant_FromApproveALL').attr("checked", "checked");
        //if (selectedRow.FLOWURGENT == "1")
        //    $('#chkUrgent_FromApproveALL').attr("checked", "checked");

        $('#btnOk_FromApproveALL').click(function () {
            if ($('#btnOk_FromApproveALL').attr("href") == "#") {
                if ($("#" + winId).attr("isDone") == "true") return;

                var selectedRows = $('#dgInbox').datagrid("getSelections");

                if (selectedRows.length == 0) {
                    var warningText = $.sysmsg('getValue', 'FLTools/GloFix/SelectData');
                    alert(warningText);
                } else {
                    $('#btnOk_FromApproveALL').hide();
                    //$('#btnCancel_FromApproveALL').hide();

                    var txtSuggest_FromApproveALL_Value = $('#txtSuggest_FromApproveALL').val();
                    var ddlRoles_FromApproveALL_Value = $('#ddlRoles_FromApproveALL').combobox('getValue');
                    var chkImportant_FromApproveALL_value = $('#chkImportant_FromApproveALL').attr("checked");
                    var chkUrgent_FromApproveALL_value = $('#chkUrgent_FromApproveALL').attr("checked");
                    var ulFiles_FromApproveALL_value = "";

                    var urlParam = {};
                    urlParam.Type = 'Workflow';
                    urlParam.Active = 'ApproveAll';
                    //urlParam += "&roles=" + ddlRoles_FromApproveALL_Value;
                    urlParam.important = chkImportant_FromApproveALL_value;
                    urlParam.urgent = chkUrgent_FromApproveALL_value;
                    urlParam.suggest = txtSuggest_FromApproveALL_Value;
                    //urlParam += "&ATTACHMENTS=" + ulFiles_FromApproveALL_value;

                    var LISTIDs = "";
                    var PROVIDER_NAMEs = "";
                    var FORM_KEYSs = "";
                    var FORM_PRESENTATIONs = "";
                    var FLOWPATHs = "";
                    var STATUSs = "";
                    var SENDTO_IDs = "";
                    var ROLEs = ""
                    var ATTACHMENTS = "";
                    for (var i = 0; i < selectedRows.length; i++) {
                        LISTIDs += selectedRows[i].LISTID + "!";
                        PROVIDER_NAMEs += selectedRows[i].PROVIDER_NAME + "!";
                        FORM_KEYSs += selectedRows[i].FORM_KEYS + "!";
                        FORM_PRESENTATIONs += selectedRows[i].FORM_PRESENTATION + "!";
                        FLOWPATHs += selectedRows[i].FLOWPATH + "!";
                        STATUSs += selectedRows[i].STATUS + "!";
                        SENDTO_IDs += selectedRows[i].SENDTO_ID + "!";
                        ATTACHMENTS += selectedRows[i].ATTACHMENTS + "!";

                        var status = decodeURIComponent(selectedRows[i].STATUS);
                        if ((status == "A" || status == "AA" || status == "加签" || status == "加簽" || status == "Plus") && selectedRows[i].SENDTO_KIND == "2") {
                            ROLEs += "!";
                        }
                        else {
                            ROLEs += ddlRoles_FromApproveALL_Value + "!";
                        }
                    }
                    urlParam.LISTID = LISTIDs;
                    urlParam.PROVIDER_NAME = PROVIDER_NAMEs;
                    urlParam.FORM_KEYS = FORM_KEYSs;
                    urlParam.FORM_PRESENTATION = FORM_PRESENTATIONs;
                    urlParam.FLOWPATH = FLOWPATHs;
                    urlParam.STATUS = STATUSs;
                    urlParam.SENDTO_ID = SENDTO_IDs;
                    urlParam.ROLE = ROLEs;
                    urlParam.ATTACHMENT = ATTACHMENTS;

                    //var maxQueryStringLength = 2048;
                    //$.ajax({
                    //    type: "POST",
                    //    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //    data: { Type: getMaxQueryStringLength },
                    //    cache: false,
                    //    async: false,
                    //    success: function (message) {
                    //        maxQueryStringLength = message;
                    //    },
                    //    error: function () {
                    //        return false;
                    //    }
                    //});

                    //if (eval(maxQueryStringLength) < urlParam.length + txtSuggest_FromApproveALL_Value.length + 50) {
                    //    var message = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');;
                    //    alert(message);
                    //    return;
                    //}

                    $.ajax({
                        type: "POST",
                        url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                        data: urlParam,
                        cache: false,
                        async: false,
                        success: function (message) {
                            //$('#result_FromApproveALL')[0].innerHTML = message;
                            $('#result_FromApproveALL').html(message);
                            //isApprove = true;
                            $("#" + winId).attr("isDone", true);
                            if (!urlPrdfix) {
                                RefreshInbox();
                                RefreshOutbox();
                            }
                            else {
                                window.top.FlowRefreshInbox.call();
                                window.top.FlowRefreshOutbox.call();
                            }
                        },
                        error: function () {
                            return false;
                        }
                    });
                }
            }
        });

        $('#btnCancel_FromApproveALL').click(function () {
            if ($('#btnCancel_FromApproveALL').attr("href") == "#") {
                $('#' + winId).dialog('close');
                $(".easyui-dialog").each(function () {
                    $(this).dialog('close');
                });
            }
        });
    }
    catch (ex) { }

}