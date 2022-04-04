function formPlusLoaded(selectedRow, dataFormId, winId) {
    $("#" + winId).attr("isDone", false);
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }
    var dataform = $("#" + dataFormId);


    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#lstUsersFrom_FormPlus').datagrid({
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'lstUsersFrom' },
            columns: [[
                                { field: 'USERID', title: flowUITexts[11], sortable: true, width: 80, sortable: true }, //'用户编号'
                                { field: 'USERNAME', title: flowUITexts[12], sortable: true, width: 87, sortable: true }//'用户姓名'
            ]],
            remoteSort: false,
            fitColumns: false,
            title: flowUITexts[3]
        });
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: 'lstUsersFrom' },
            cache: false,
            async: true,
            success: function (data) {
                data = eval(data);
                $('#lstUsersFrom_FormPlus').datagrid("loadData", data);
            },
            error: function () {
                return false;
            }
        });
        $('#lstUsersTo_FormPlus').datagrid({
            columns: [[
                                { field: 'USERID', title: flowUITexts[11], sortable: true, width: 80, sortable: true }, //'角色编号'
                                { field: 'USERNAME', title: flowUITexts[12], sortable: true, width: 87, sortable: true }//'角色名称'
            ]],
            remoteSort: false,
            fitColumns: false,
            title: flowUITexts[4]
        });
        $('#btnUsersTo_FormPlus').click(function () {
            if ($('#btnUsersTo_FormPlus').attr("href") == "#") {
                fromTo("lstUsersFrom_FormPlus", "lstUsersTo_FormPlus");
            }
        });
        $('#btnUsersBack_FormPlus').click(function () {
            if ($('#btnUsersBack_FormPlus').attr("href") == "#") {
                fromTo("lstUsersTo_FormPlus", "lstUsersFrom_FormPlus");
            }
        });
        $('#btnUsersSeach_FormPlus').click(function () {
            if ($('#btnUsersSeach_FormPlus').attr("href") == "#") {
                var txtSearchUserId_FormPlus_value = $('#txtSearchUserId_FormPlus').val();
                var txtSearchUserName_FormPlus_value = $('#txtSearchUserName_FormPlus').val();
                $('#lstUsersFrom_FormPlus').datagrid({
                    //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //data: { Type: 'lstUsersFrom', USERID: encodeURI(txtSearchUserId_FormPlus_value), USERNAME: encodeURI(txtSearchUserName_FormPlus_value) }
                });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: { Type: 'lstUsersFrom', USERID: encodeURI(txtSearchUserId_FormPlus_value), USERNAME: encodeURI(txtSearchUserName_FormPlus_value) },
                    cache: false,
                    async: true,
                    success: function (data) {
                        data = eval(data);
                        $('#lstUsersFrom_FormPlus').datagrid("loadData", data);
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#lstRolesFrom_FormPlus').datagrid({
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'lstRolesFrom' },
            columns: [[
                                { field: 'GROUPID', title: flowUITexts[8], sortable: true, width: 80, sortable: true },
                                { field: 'GROUPNAME', title: flowUITexts[9], sortable: true, width: 87, sortable: true }
            ]],
            remoteSort: false,
            fitColumns: false,
            title: flowUITexts[3]
        });
        $.ajax({
            type: "POST",
            url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            data: { Type: 'lstRolesFrom' },
            cache: false,
            async: true,
            success: function (data) {
                data = eval(data);
                $('#lstRolesFrom_FormPlus').datagrid("loadData", data);
            },
            error: function () {
                return false;
            }
        });
        $('#lstRolesTo_FormPlus').datagrid({
            columns: [[
                                { field: 'GROUPID', title: flowUITexts[8], sortable: true, width: 80, sortable: true },
                                { field: 'GROUPNAME', title: flowUITexts[9], sortable: true, width: 87, sortable: true }
            ]],
            remoteSort: false,
            fitColumns: false,
            title: flowUITexts[4]
        });
        $('#btnRolesTo_FormPlus').click(function () {
            if ($('#btnRolesTo_FormPlus').attr("href") == "#") {
                fromTo("lstRolesFrom_FormPlus", "lstRolesTo_FormPlus");
            }
        });
        $('#btnRolesBack_FormPlus').click(function () {
            if ($('#btnRolesBack_FormPlus').attr("href") == "#") {
                fromTo("lstRolesTo_FormPlus", "lstRolesFrom_FormPlus");
            }
        });
        $('#btnRolesSeach_FormPlus').click(function () {
            if ($('#btnRolesSeach_FormPlus').attr("href") == "#") {
                var txtSearchRoleId_FormPlus_value = $('#txtSearchRoleId_FormPlus').val();
                var txtSearchRoleName_FormPlus_value = $('#txtSearchRoleName_FormPlus').val();
                $('#lstRolesFrom_FormPlus').datagrid({
                    //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //data: { Type: 'lstRolesFrom', GROUPID: encodeURI(txtSearchRoleId_FormPlus_value), GROUPNAME: encodeURI(txtSearchRoleName_FormPlus_value) }
                });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: { Type: 'lstRolesFrom', GROUPID: encodeURI(txtSearchRoleId_FormPlus_value), GROUPNAME: encodeURI(txtSearchRoleName_FormPlus_value) },
                    cache: false,
                    async: true,
                    success: function (data) {
                        data = eval(data);
                        $('#lstRolesFrom_FormPlus').datagrid("loadData", data);
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });


        $('#btnOk_FormPlus').click(function () {
            if ($('#btnOk_FormPlus').attr("href") == "#") {
                $('#btnOk_FormPlus').hide();

                var txtMessage_FormPlus_Value = $('#txtMessage_FormPlus').val();
                var dataUsers = $('#lstUsersTo_FormPlus').datagrid('getData');
                var users = "";
                var users1 = "";
                for (var i = 0; i < dataUsers.rows.length; i++) {
                    users += "U:" + dataUsers.rows[i].USERID + ";";
                    users1 += dataUsers.rows[i].USERID + ":UserId;";
                }
                var dataRoles = $('#lstRolesTo_FormPlus').datagrid('getData');
                var roles = "";
                for (var i = 0; i < dataRoles.rows.length; i++) {
                    roles += dataRoles.rows[i].GROUPID + ";";
                }

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Plus";
                urlParam.users = users;
                urlParam.users1 = users1;
                urlParam.roles = roles;
                urlParam.suggest = txtMessage_FormPlus_Value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION);
                urlParam.FLOWPATH = decodeURIComponent(selectedRow.FLOWPATH);
                urlParam.STATUS = decodeURIComponent(selectedRow.STATUS);
                urlParam.SENDTO_ID = selectedRow.SENDTO_ID;
                urlParam.ATTACHMENTS = selectedRow.ATTACHMENTS;

                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: urlParam,
                    cache: false,
                    async: true,
                    success: function (message) {
                        var dataform = $("#" + dataFormId);
                        $("#FlowApprove,#FlowPlus,#FlowReturn", dataform).hide();
                        window.top.FlowRefreshInbox.call();
                        $('#result_FormPlus')[0].innerHTML = message;
                        $("#" + winId).attr("isDone", true);
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnCancel_FormPlus').click(function () {
            if ($('#btnCancel_FormPlus').attr("href") == "#") {
                $('#' + winId).dialog('close');
            }
        });

        $('#btnRolesSeach_FormPlus')[0].firstChild.firstChild.innerHTML = flowUITexts[10];
        $('#btnOk_FormPlus')[0].firstChild.firstChild.innerHTML = flowUITexts[5];
        $('#btnCancel_FormPlus')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        $('#lSearchUserId_FormPlus')[0].innerHTML = flowUITexts[11];
        $('#lSearchUserName_FormPlus')[0].innerHTML = flowUITexts[12];
        $('#lSearchRoleId_FormPlus')[0].innerHTML = flowUITexts[8];
        $('#lSearchRoleName_FormPlus')[0].innerHTML = flowUITexts[9];
        $("#lMessage_FormPlus")[0].innerHTML = flowUITexts[2];
        //$(".tabs-title")[1].innerHTML = flowUITexts[0];
        //$(".tabs-title")[0].innerHTML = flowUITexts[1];

        $('#tabsMain_FormPlus').tabs('update', {
            tab: $("#tabsMain_FormPlus").tabs("tabs")[0],
            options: {
                title: flowUITexts[1]
            }
        });
        $('#tabsMain_FormPlus').tabs('update', {
            tab: $("#tabsMain_FormPlus").tabs("tabs")[1],
            options: {
                title: flowUITexts[0]
            }
        });
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}

function fromTo(fromDataGrid, toDataGrid) {
    var to_Data = $('#' + toDataGrid).datagrid('getData');
    var from_Data = $('#' + fromDataGrid).datagrid('getData');
    var from_Selected = $('#' + fromDataGrid).datagrid('getSelections');
    for (var i = 0; i < from_Selected.length; i++) {
        to_Data.rows.push(from_Selected[i]);
        var index = $('#' + fromDataGrid).datagrid('getRowIndex', from_Selected[i]);
        $('#' + fromDataGrid).datagrid('deleteRow', index);
    }
    $('#' + toDataGrid).datagrid('loadData', to_Data.rows);
}