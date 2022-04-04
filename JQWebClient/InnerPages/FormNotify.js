function formNotifyLoaded(selectedRow, dataFormId, winId) {
    var urlPrdfix = "";
    if (isSubPath) {
        urlPrdfix = "../";
    }

    try {
        var flowUIText = $.sysmsg('getValue', 'FLClientControls/NotifyForm/UIText');
        var flowUITexts = flowUIText.split(',');
        $('#lstUsersFrom_FormNotify').datagrid({
            //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
            //data: { Type: 'lstUsersFrom' },
            columns: [[
                                { field: 'USERID', title: flowUITexts[11], sortable: true, width: 80, sortable: true }, //'用户编号'
                                { field: 'USERNAME', title: flowUITexts[12], sortable: true, width: 87, sortable: true }//'用户姓名'
            ]],
            fitColumns: false,
            remoteSort: false,
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
                $('#lstUsersFrom_FormNotify').datagrid("loadData", data);
            },
            error: function () {
                return false;
            }
        });
        $('#lstUsersTo_FormNotify').datagrid({
            columns: [[
                                { field: 'USERID', title: flowUITexts[11], sortable: true, width: 80, sortable: true }, //'角色编号'
                                { field: 'USERNAME', title: flowUITexts[12], sortable: true, width: 87, sortable: true }//'角色名称'
            ]],
            fitColumns: false,
            remoteSort: false,
            title: flowUITexts[4]
        });
        $('#btnUsersTo_FormNotify').click(function () {
            if ($('#btnUsersTo_FormNotify').attr("href") == "#") {
                fromTo("lstUsersFrom_FormNotify", "lstUsersTo_FormNotify");
            }
        });
        $('#btnUsersBack_FormNotify').click(function () {
            if ($('#btnUsersBack_FormNotify').attr("href") == "#") {
                fromTo("lstUsersTo_FormNotify", "lstUsersFrom_FormNotify");
            }
        });
        $('#btnUsersSeach_FormNotify').click(function () {
            if ($('#btnUsersSeach_FormNotify').attr("href") == "#") {
                var txtSearchUserId_FormNotify_value = $('#txtSearchUserId_FormNotify').val();
                var txtSearchUserName_FormNotify_value = $('#txtSearchUserName_FormNotify').val();
                $('#lstUsersFrom_FormNotify').datagrid({
                    //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //data: { Type: 'lstUsersFrom', USERID: encodeURI(txtSearchUserId_FormNotify_value), USERNAME: encodeURI(txtSearchUserName_FormNotify_value) }
                });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: { Type: 'lstUsersFrom', USERID: encodeURI(txtSearchUserId_FormNotify_value), USERNAME: encodeURI(txtSearchUserName_FormNotify_value) },
                    cache: false,
                    async: true,
                    success: function (data) {
                        data = eval(data);
                        $('#lstUsersFrom_FormNotify').datagrid("loadData", data);
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#lstRolesFrom_FormNotify').datagrid({
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
                $('#lstRolesFrom_FormNotify').datagrid("loadData", data);
            },
            error: function () {
                return false;
            }
        });
        $('#lstRolesTo_FormNotify').datagrid({
            columns: [[
                                { field: 'GROUPID', title: flowUITexts[8], sortable: true, width: 80, sortable: true },
                                { field: 'GROUPNAME', title: flowUITexts[9], sortable: true, width: 87, sortable: true }
            ]],
            remoteSort: false,
            fitColumns: false,
            title: flowUITexts[4]
        });
        $('#btnRolesTo_FormNotify').click(function () {
            if ($('#btnRolesTo_FormNotify').attr("href") == "#") {
                fromTo("lstRolesFrom_FormNotify", "lstRolesTo_FormNotify");
            }
        });
        $('#btnRolesBack_FormNotify').click(function () {
            if ($('#btnRolesBack_FormNotify').attr("href") == "#") {
                fromTo("lstRolesTo_FormNotify", "lstRolesFrom_FormNotify");
            }
        });
        $('#btnRolesSeach_FormNotify').click(function () {
            if ($('#btnRolesSeach_FormNotify').attr("href") == "#") {
                var txtSearchRoleId_FormNotify_value = $('#txtSearchRoleId_FormNotify').val();
                var txtSearchRoleName_FormNotify_value = $('#txtSearchRoleName_FormNotify').val();
                $('#lstRolesFrom_FormNotify').datagrid({
                    //url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    //data: { Type: 'lstRolesFrom' & GROUPID, GROUPID: encodeURI(txtSearchRoleId_FormNotify_value), GROUPNAME: encodeURI(txtSearchRoleName_FormNotify_value) }
                });
                $.ajax({
                    type: "POST",
                    url: urlPrdfix + 'handler/SystemHandle_Flow.ashx',
                    data: { Type: 'lstRolesFrom', GROUPID: encodeURI(txtSearchRoleId_FormNotify_value), GROUPNAME: encodeURI(txtSearchRoleName_FormNotify_value) },
                    cache: false,
                    async: true,
                    success: function (data) {
                        data = eval(data);
                        $('#lstRolesFrom_FormNotify').datagrid("loadData", data);
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

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

        $('#btnOk_FormNotify').click(function () {
            if ($('#btnOk_FormNotify').attr("href") == "#") {
                $('#btnOk_FormNotify').hide();
                //$('#btnCancel_FormNotify').hide();

                var txtMessage_FormNotify_Value = $('#txtMessage_FormNotify').val();
                var dataUsers = $('#lstUsersTo_FormNotify').datagrid('getData');
                var dataRoles = $('#lstRolesTo_FormNotify').datagrid('getData');
                var users = "";
                for (var i = 0; i < dataUsers.rows.length; i++) {
                    users += dataUsers.rows[i].USERID + ":UserId;";
                }
                var roles = "";
                for (var i = 0; i < dataRoles.rows.length; i++) {
                    roles += dataRoles.rows[i].GROUPID + ";";
                }

                var urlParam = {};
                urlParam.Type = "Workflow";
                urlParam.Active = "Notify";
                urlParam.users = users;
                urlParam.roles = roles;
                urlParam.suggest = txtMessage_FormNotify_Value;

                urlParam.LISTID = selectedRow.LISTID;
                urlParam.PROVIDER_NAME = selectedRow.PROVIDER_NAME;
                urlParam.FORM_KEYS = selectedRow.FORM_KEYS;
                urlParam.FORM_PRESENTATION = decodeURIComponent(selectedRow.FORM_PRESENTATION);
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
                        $('#result_FormNotify')[0].innerHTML = message;
                    },
                    error: function () {
                        return false;
                    }
                });
            }
        });

        $('#btnCancel_FormNotify').click(function () {
            if ($('#btnCancel_FormNotify').attr("href") == "#") {
                $('#' + winId).dialog('close');
            }
        });

        $('#btnUsersSeach_FormNotify')[0].firstChild.firstChild.innerHTML = flowUITexts[10];
        $('#btnRolesSeach_FormNotify')[0].firstChild.firstChild.innerHTML = flowUITexts[10];
        $('#btnOk_FormNotify')[0].firstChild.firstChild.innerHTML = flowUITexts[5];
        $('#btnCancel_FormNotify')[0].firstChild.firstChild.innerHTML = flowUITexts[6];
        $('#lSearchUserId_FormNotify')[0].innerHTML = flowUITexts[11];
        $('#lSearchUserName_FormNotify')[0].innerHTML = flowUITexts[12];
        $('#lSearchRoleId_FormNotify')[0].innerHTML = flowUITexts[8];
        $('#lSearchRoleName_FormNotify')[0].innerHTML = flowUITexts[9];
        $("#lMessage_FormNotify")[0].innerHTML = flowUITexts[2];

        $('#tabsMain_FormNotify').tabs('update', {
            tab: $("#tabsMain_FormNotify").tabs("tabs")[0],
            options: {
                title: flowUITexts[0]
            }
        });
        $('#tabsMain_FormNotify').tabs('update', {
            tab: $("#tabsMain_FormNotify").tabs("tabs")[1],
            options: {
                title: flowUITexts[1]
            }
        });
    }
    catch (err) {
        //alert("System.XML Version Too Old");
    }
}