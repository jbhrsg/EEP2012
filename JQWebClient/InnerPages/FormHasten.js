function formHastenLoaded(selectedRow, dataFormId, winId) {
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }
    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#btnOk_FormHasten').click(function () {
            if ($('#btnOk_FormHasten').attr("href") == "#") {
                $('#btnOk_FormHasten').hide();
                //$('#btnCancel_FormHasten').hide();

                var txtMessage_FormHasten_Value = $('#txtMessage_FormHasten').val();
                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Hasten";
                urlParam.suggest = txtMessage_FormHasten_Value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME =  selectedRow.PROVIDER_NAME;
                urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION);
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
                if (selectedRow.SENDTO_KIND == "2")
                    urlParam.SENDTO_ID = selectedRow.SENDTO_ID + ":UserId;";
                else
                    urlParam.SENDTO_ID = selectedRow.SENDTO_ID;

                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        $('#result_FormHasten')[0].innerHTML = message;
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnCancel_FormHasten').click(function () {
            if ($('#btnCancel_FormHasten').attr("href") == "#") {
                $('#' + winId).dialog('close');
            }
        });

        $('#btnOk_FormHasten')[0].firstChild.firstChild.innerHTML = flowUITexts[5];
        $('#btnCancel_FormHasten')[0].firstChild.firstChild.innerHTML = flowUITexts[6];

        flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText1');
        flowUITexts = flowUIText.split(',');
        $("#label_FormHasten")[0].innerHTML = flowUITexts[0];
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}