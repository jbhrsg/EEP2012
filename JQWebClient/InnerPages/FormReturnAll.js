function formReturnAllLoaded(selectedRow, dataFormId, winId) {
    $("#" + winId).attr("isDone", false);
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FromReturnALL')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        $('#btnCancel_FromReturnALL')[0].firstChild.firstChild.innerHTML = flowUITexts[13];

        $('#lFieldset_FromReturnALL')[0].innerHTML = flowUITexts[0];
        $('#lImportant_FromReturnALL')[0].innerHTML = flowUITexts[1];
        $('#lUrgent_FromReturnALL')[0].innerHTML = flowUITexts[2];
        $('#lSenderRole_FromReturnALL')[0].innerHTML = flowUITexts[5];

        $(".tabs-title", "#formApproveAll")[0].innerHTML = flowUITexts[3];

        $('#ddlRoles_FromReturnALL').combobox({
            editable: false,
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'ddlRoles', LISTID: selectedRow.LISTID },
            onLoadSuccess: function () {
                var data = $('#ddlRoles_FromReturnALL').combobox('getData');
                if (data.length > 0)
                    $('#ddlRoles_FromReturnALL').combobox('select', data[0].GROUPID);
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
                $('#ddlRoles_FromReturnALL').combobox("loadData", data);
            },
            error: function () {
                return false;
            }
        });

        //if (selectedRow.FLOWIMPORTANT == "1")
        //    $('#chkImportant_FromReturnALL').attr("checked", "checked");
        //if (selectedRow.FLOWURGENT == "1")
        //    $('#chkUrgent_FromReturnALL').attr("checked", "checked");

        $('#btnOk_FromReturnALL').click(function () {
            if ($('#btnOk_FromReturnALL').attr("href") == "#") {
                if ($("#" + winId).attr("isDone") == "true") return;

                var selectedRows = $('#dgInbox').datagrid("getSelections");

                if (selectedRows.length == 0) {
                    alert("Please select one row first.");
                } else {
                    $('#btnOk_FromReturnALL').hide();
                    //$('#btnCancel_FromReturnALL').hide();

                    var txtSuggest_FromReturnALL_Value = $('#txtSuggest_FromReturnALL').val();
                    var ddlRoles_FromReturnALL_Value = $('#ddlRoles_FromReturnALL').combobox('getValue');
                    var chkImportant_FromReturnALL_value = $('#chkImportant_FromReturnALL').attr("checked");
                    var chkUrgent_FromReturnALL_value = $('#chkUrgent_FromReturnALL').attr("checked");
                    var ulFiles_FromReturnALL_value = "";

                    var urlParam = {};
                    urlParam.Type = "Workflow";
                    urlParam.Active = "ReturnAll";
                    urlParam.roles = ddlRoles_FromReturnALL_Value;
                    urlParam.important = chkImportant_FromReturnALL_value;
                    urlParam.urgent = chkUrgent_FromReturnALL_value;
                    urlParam.suggest = txtSuggest_FromReturnALL_Value;
                    urlParam.returnstep = "0";
                    urlParam.ATTACHMENTS = ulFiles_FromReturnALL_value;

                    var LISTIDs = "";
                    var PROVIDER_NAMEs = "";
                    var FORM_KEYSs = "";
                    var FORM_PRESENTATIONs = "";
                    var FLOWPATHs = "";
                    var STATUSs = "";
                    var SENDTO_IDs = "";
                    for (var i = 0; i < selectedRows.length; i++) {
                        LISTIDs += selectedRows[i].LISTID + "!";
                        PROVIDER_NAMEs += selectedRows[i].PROVIDER_NAME + "!";
                        FORM_KEYSs += selectedRows[i].FORM_KEYS + "!";
                        FORM_PRESENTATIONs += selectedRows[i].FORM_PRESENTATION + "!";
                        FLOWPATHs += selectedRows[i].FLOWPATH + "!";
                        STATUSs += selectedRows[i].STATUS + "!";
                        SENDTO_IDs += selectedRows[i].SENDTO_ID + "!";
                    }
                    urlParam.LISTID = LISTIDs;
                    urlParam.PROVIDER_NAME = PROVIDER_NAMEs;
                    urlParam.FORM_KEYS =  FORM_KEYSs;
                    urlParam.FORM_PRESENTATION = FORM_PRESENTATIONs;
                    urlParam.FLOWPATH =  FLOWPATHs;
                    urlParam.STATUS = STATUSs;
                    urlParam.SENDTO_ID = SENDTO_IDs;
                    urlParam.returnstep = "0";

                    //var maxQueryStringLength = 2048;
                    //$.ajax({
                    //    type: "POST",
                    //    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //    data: { Type: 'getMaxQueryStringLength' },
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
                            //$('#result_FromReturnALL')[0].innerHTML = message;
                            $('#result_FromReturnALL').html(message);
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

        $('#btnCancel_FromReturnALL').click(function () {
            if ($('#btnCancel_FromReturnALL').attr("href") == "#") {
                $('#' + winId).dialog('close');
                $(".easyui-dialog").each(function () {
                    $(this).dialog('close');
                });
            }
        });
    }
    catch (ex) { }
}