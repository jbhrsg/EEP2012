function formFlowQueryLoaded(controlId, dataFormId, winId) {

    try {
        var flowUIText = $.sysmsg('getValue', 'Web/webClientMainFlow/QueryCaption');
        var flowUITexts = flowUIText.split(';');
        $('#btnSearch_FromFlowQuery')[0].firstChild.firstChild.innerHTML = flowUITexts[9];
        $('#btnClear_FromFlowQuery')[0].firstChild.firstChild.innerHTML = flowUITexts[10];

        $('#lSearch_FLOW_DESC')[0].innerHTML = flowUITexts[0];
        $('#lSearch_D_STEP_ID')[0].innerHTML = flowUITexts[1];

        if (controlId == "dgInbox")
            $('#lSearch_USERNAME')[0].innerHTML = flowUITexts[2];
        else if (controlId == "dgOutbox" || controlId == "dgFlowRunOver")
            $('#lSearch_USERNAME')[0].innerHTML = flowUITexts[7];
        $('#lSearch_FORM_PRESENT_CT')[0].innerHTML = flowUITexts[3];
        $('#lSearch_UPDATE_WHOLE_TIME_Form')[0].innerHTML = flowUITexts[5];
        $('#lSearch_UPDATE_WHOLE_TIME_To')[0].innerHTML = flowUITexts[6];
        $('#lSearch_REMARK')[0].innerHTML = flowUITexts[4];

        $('#btnClear_FromFlowQuery').click(function () {
            if ($('#btnClear_FromFlowQuery').attr("href") == "#") {
                $('#formFlowQuery').form("clear");
            }
        });

        $('#btnSearch_FromFlowQuery').click(function () {
            if ($('#btnSearch_FromFlowQuery').attr("href") == "#") {
                var filter = "";
                var Search_FLOW_DESC = $('#Search_FLOW_DESC').val();
                if (Search_FLOW_DESC != "") {
                    filter += "FLOW_DESC like '%" + Search_FLOW_DESC + "%' and ";
                }
                var Search_D_STEP_ID = $('#Search_D_STEP_ID').val();
                if (Search_D_STEP_ID != "") {
                    filter += "D_STEP_ID like '%" + Search_D_STEP_ID + "%' and ";
                }
                var Search_USERNAME = $('#Search_USERNAME').val();
                if (Search_USERNAME != "") {
                    if (controlId == "dgInbox")
                        filter += "USERNAME like '%" + Search_USERNAME + "%' and ";
                    else if (controlId == "dgOutbox")
                        filter += "SENDTO_NAME like '%" + Search_USERNAME + "%' and ";
                }
                var Search_FORM_PRESENT_CT = $('#Search_FORM_PRESENT_CT').val();
                if (Search_FORM_PRESENT_CT != "") {
                    filter += "FORM_PRESENT_CT like '%" + Search_FORM_PRESENT_CT.replace(/'/g, "''") + "%' and ";
                }
                var Search_UPDATE_WHOLE_TIME_Form = $('#Search_UPDATE_WHOLE_TIME_Form').datebox("getValue");
                if (Search_UPDATE_WHOLE_TIME_Form != "") {
                    if (controlId == "dgFlowRunOver") {
                        var date = new Date(Search_UPDATE_WHOLE_TIME_Form);
                        var m = date.getMonth() + 1;
                        if (m.toString().length == 1)
                            m = "0" + m.toString();
                        var d = date.getDate();
                        if (d.toString().length == 1)
                            d = "0" + d.toString();
                        var sDate = date.getFullYear() + "-" + m + "-" + d;
                        filter += "UPDATE_DATE >= '" + sDate + "' and ";
                    }
                    else
                        filter += "UPDATE_DATE >= CONVERT('" + Search_UPDATE_WHOLE_TIME_Form + "','System.DateTime') and ";
                }
                var Search_UPDATE_WHOLE_TIME_To = $('#Search_UPDATE_WHOLE_TIME_To').datebox("getValue");
                if (Search_UPDATE_WHOLE_TIME_To != "") {
                    if (controlId == "dgFlowRunOver") {
                        var date = new Date(Search_UPDATE_WHOLE_TIME_To);
                        var m = date.getMonth() + 1;
                        if (m.toString().length == 1)
                            m = "0" + m.toString();
                        var d = date.getDate();
                        if (d.toString().length == 1)
                            d = "0" + d.toString();
                        var sDate = date.getFullYear() + "-" + m + "-" + d;
                        filter += "UPDATE_DATE <= '" + sDate + "' and ";
                    }
                    else
                        filter += "UPDATE_DATE <= CONVERT('" + Search_UPDATE_WHOLE_TIME_To + "','System.DateTime') and ";
                }
                var Search_REMARK = $('#Search_REMARK').val();
                if (Search_REMARK != "") {
                    filter += "REMARK like '%" + Search_REMARK + "%' and ";
                }
                filter = filter.substring(0, filter.lastIndexOf("and"));

                var type = "";
                switch (controlId) {
                    case "dgInbox": type = "ToDoList"; break;
                    case "dgOutbox": type = "ToDoHis"; break;
                    case "dgFlowRunOver": type = "FlowRunOver"; break;
                }

                $('#' + winId).dialog('close');
                $('#' + controlId).datagrid('options').queryParams.Filter = encodeURI(filter);
                $('#' + controlId).datagrid('load');
            }
        });

        $('#btnClear_FromFlowQuery').click(function () {
            if ($('#btnClear_FromFlowQuery').attr("href") == "#") {
                $('#' + winId).dialog('close');
                //$("#formFlowQuery").form("clear");
            }
        });
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}