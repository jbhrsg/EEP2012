function formFileUploadLoaded(selectedRow, dataFormId, winId) {
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    var jqFileUpload1 = $('#JQFileUpload1');
    initInfoFileUpload(jqFileUpload1);
    var jqFileUpload2 = $('#JQFileUpload2');
    initInfoFileUpload(jqFileUpload2);
    var jqFileUpload3 = $('#JQFileUpload3');
    initInfoFileUpload(jqFileUpload3);
    var jqFileUpload4 = $('#JQFileUpload4');
    initInfoFileUpload(jqFileUpload4);
    var jqFileUpload5 = $('#JQFileUpload5');
    initInfoFileUpload(jqFileUpload5);

    $('#btnUpload_FormFileUpload').click(function () {
        if ($('#btnUpload_FormFileUpload').attr("href") == "#") {
            $('#btnUpload_FormFileUpload').hide();
            if ($('#infoFileUploadJQFileUpload1') != undefined && $('#infoFileUploadJQFileUpload1').val() != "") {
                var fileName1 = $('#infoFileUploadJQFileUpload1').val();
                //infoFileUploadMethod(jqFileUpload2, false, 'WorkflowFiles', '', 'infoFileUploadJQFileUpload2', undefined, undefined);
                infoFileUploadMethod(jqFileUpload1, afterUpload, undefined);
            }
            if ($('#infoFileUploadJQFileUpload2') != undefined && $('#infoFileUploadJQFileUpload2').val() != "") {
                var fileName2 = $('#infoFileUploadJQFileUpload2').val();
                infoFileUploadMethod(jqFileUpload2, afterUpload, undefined);
            }
            if ($('#infoFileUploadJQFileUpload3') != undefined && $('#infoFileUploadJQFileUpload3').val() != "") {
                var fileName3 = $('#infoFileUploadJQFileUpload3').val();
                infoFileUploadMethod(jqFileUpload3, afterUpload, undefined);
            }
            if ($('#infoFileUploadJQFileUpload4') != undefined && $('#infoFileUploadJQFileUpload4').val() != "") {
                var fileName4 = $('#infoFileUploadJQFileUpload4').val();
                infoFileUploadMethod(jqFileUpload4, afterUpload, undefined);
            }
            if ($('#infoFileUploadJQFileUpload5') != undefined && $('#infoFileUploadJQFileUpload5').val() != "") {
                var fileName5 = $('#infoFileUploadJQFileUpload5').val();
                infoFileUploadMethod(jqFileUpload5, afterUpload, undefined);
            }
        }
    });

    function afterUpload(fileName) {
        var realFileName = fileName;
        fileName = fileName.replace(/__/g, "&nbsp;");
        var ul = $('#ulFiles_FormFileUpload');
        var href = urlPrdfix + "WorkflowFiles/" + realFileName;
        var link = "<li><A href='" + href + "' target='_blank ' class=" + realFileName + " >" + fileName + "</A></li>"; //<a class='easyui-linkbutton' iconcls='icon-no' plain='true' id='A1'></a>
        $(link).appendTo(ul);
        $('.ulFiles').each(function () {
            $(link).appendTo(this);
        });
    }

    $('#btnCancel_FileUpload').click(function () {
        if ($('#btnCancel_FileUpload').attr("href") == "#") {
            $('#' + winId).dialog('close');
        }
    });


    if ($('#btnCancel_FileUpload').length > 0) {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/SubmitConfirm/UIText');
        var flowUITexts = flowUIText.split(',');
        var uploadButtonText = $.sysmsg('getValue', 'JQWebClient/fileuploadbutton');
        $('#btnUpload_FormFileUpload')[0].firstChild.firstChild.innerHTML = uploadButtonText;
        $('#btnCancel_FileUpload')[0].firstChild.firstChild.innerHTML = flowUITexts[13];
    }
}