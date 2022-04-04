//會用到
//infolight.js
//JQueryEasyUI

//【表單介面修正】
//●jbFormUISet                                  修改表單內Table的UI
; (function ($) {
    $.fn.jbFormUISet = function (_settings) {

        var settings = $.extend({}, _default, _settings);
        var Table = $(this).find('table:eq(0)');

        var IndexOfTypeAll = $.inArray(settings.Type.toLowerCase(), settings.TypeAll);

        if (IndexOfTypeAll == -1) { alert('Type Error'); return; }
        else if (IndexOfTypeAll == 0) $(Table).jbTable2Accordion(settings);
        else if (IndexOfTypeAll == 1) $(Table).jbTable2Tab(settings);

    };

    var _default = {
        TitleList: {},
        ID: '',
        Type: 'accordion',
        TypeAll: ['accordion', 'tab']
    };

})(jQuery);

//【Table介面修正】
//●jbTable2Tab                                  修改Table的UI，欄位做Tab的設定
//●jbTable2Accordion                            修改Table的UI，欄位做Accordion的設定
//○jbTable2UICheck                              修改Table的UI，參數檢查
//○jbTitleListChange                            修改Table的UI，參數轉換
; (function ($) {
    $.fn.jbTable2Tab = function (settings) {
        var Check = $.jbTable2UICheck(settings);
        if (!Check.IsOK) {
            alert(Check.ErrorMsg);
            return;
        }

        var TitleList = Check.TitleList;

        var theTable = $(this);
        var thePlugin = $('#' + settings.ID);
        $(theTable).after(thePlugin);

        var AllRow = $(theTable).find('tbody tr');
        $.each(TitleList, function (key, val) {
            //var aTable = $(document.createElement('table'));
            var aTable = $(theTable).clone().children().remove().end();

            $(aTable).append($(AllRow).slice(val[0] - 1, val[0] - 1 + val[1]));
            var exists = thePlugin.tabs('getTab', key);
            if (exists) {
                //theTab.tabs('update', { tab: exists, options: { content: aTable } });
                exists.append(aTable);
            } else {
                thePlugin.tabs('add', { title: key, content: aTable });
            }
        });
        thePlugin.tabs('select', 0);
    },
    $.fn.jbTable2Accordion = function (settings) {
        var Check = $.jbTable2UICheck(settings);
        if (!Check.IsOK) {
            alert(Check.ErrorMsg);
            return;
        }

        var TitleList = Check.TitleList;

        var theTable = $(this);
        var thePlugin = $('#' + settings.ID);
        $(theTable).after(thePlugin);

        var AllRow = $(theTable).find('tbody tr');
        $.each(TitleList, function (key, val) {
            //var aTable = $(document.createElement('table'));
            var aTable = $(theTable).clone().children().remove().end();

            $(aTable).append($(AllRow).slice(val[0] - 1, val[0] - 1 + val[1]));
            var exists = thePlugin.accordion('getPanel', key);
            if (exists) {
                //exists.panel('refresh', aTable);no methods to update
                exists.append(aTable);
            } else {
                thePlugin.accordion('add', { title: key, content: aTable, selected: false });
            }
        });
        thePlugin.accordion('select', 0);

    },
    $.jbTable2UICheck = function (settings) {
        var IsOK = true;
        var ErrorMsg = '';
        var TitleList = {};

        if (Object.keys(settings.TitleList).length == 0) {
            IsOK = false;
            ErrorMsg = 'TitleList Error';
        }
        else if (!settings.ID) {
            IsOK = false;
            ErrorMsg = 'ID Error';
        }
        else {
            TitleList = $.jbTitleListChange(settings.TitleList);
            if (Object.keys(TitleList).length == 0) {
                IsOK = false;
                ErrorMsg = 'TitleList Error';
            } else IsOK = true;
        }

        return { IsOK: IsOK, ErrorMsg: ErrorMsg, TitleList: TitleList };
    },
    $.jbTitleListChange = function (List) {
        var isOK = true;
        var theType = '';
        $.each(List, function (key, val) {
            if ($.type(key) != 'string') { isOK = false; return false; }
            else if ($.type(val) != 'number' && $.type(val) != 'array') { isOK = false; return false; }
            else if (theType && theType != $.type(val)) { isOK = false; return false; }
            else if ($.type(val) == 'array') {
                var num = 0;
                $.each(val, function (i, val) {
                    if ($.type(val) != 'number') return false;
                    else if (val <= 0) return false;
                    num++;
                });
                if (num < 2) { isOK = false; return false; }
            }
            theType = $.type(val);
        });
        if (!isOK) return {};
        if (theType != 'number') return List;
        else {
            var Index = 1;
            var newList = {};
            $.each(List, function (key, val) {
                newList[key] = [Index, val];
                Index += val;
            });
            return newList;
        }
    }
})(jQuery);

//===============================================以上東西尚未實際測試完全，能不用則不用============
//=================================================================================================
//=                                              擴充方法                                         =
//=================================================================================================

//【DataForm相關】
; (function ($) {
    $.fn.jbDataFormReloadDataGrid = function () {
        $(this).each(function () {
            var form = $(this);
            var dialoggrid = form.attr('dialogGrid');
            if (dialoggrid == undefined) dialoggrid = form.attr('switchGrid');
            if (dialoggrid == undefined) dialoggrid = form.attr('continueGrid');
            $(dialoggrid).datagrid('reload');
        });
    },
    $.fn.jbDataFormGetAFormData = function (isEncode) {
        var fieldEncode = isEncode || false;
        var Form = $(this);
        var reData = new Object();
        $('input,select,textarea', Form).each(function () {
            var field = getInfolightOption($(this)).field;
            var formid = getInfolightOption($(this)).form;
            if (formid != undefined && field != undefined && Form.attr('id') == formid) {
                var theVal = "";
                var controlClass = $(this).attr('class');
                if (controlClass != undefined) {
                    if (controlClass.indexOf('easyui-datebox') == 0) theVal = $(this).datebox('getBindingValue');
                    else if (controlClass.indexOf('easyui-combobox') == 0) theVal = $(this).combobox('getValue');
                    else if (controlClass.indexOf('easyui-datetimebox') == 0) theVal = $(this).datetimebox('getBindingValue');
                    else if (controlClass.indexOf('easyui-combogrid') == 0) theVal = $(this).combogrid('getValue');
                    else if (controlClass.indexOf('info-combobox') == 0) theVal = $(this).combobox('getValue');
                    else if (controlClass.indexOf('info-combogrid') == 0) theVal = $(this).combogrid('getValue');
                    else if (controlClass.indexOf('info-refval') == 0) theVal = $(this).refval('getValue');
                    else if (controlClass.indexOf('info-options') == 0) theVal = $(this).options('getValue');
                    else if (controlClass.indexOf('info-autocomplete') == 0) theVal = $(this).combobox('getValue');
                    else theVal = $(this).val();
                }
                else {
                    if ($(this).attr('type') == "checkbox") theVal = $(this).checkbox('getValue');
                    else theVal = $(this).val();
                }
                if (fieldEncode) reData[field] = encodeURIComponent(theVal).toString().replace(/(%22)/g, "\"").replace(/(%5C)/g, "\\");//双引号和反斜线不需要编码
                else reData[field] = theVal;
            }
        });
        return reData;
    }
    $.fn.jbQueryFormGetAFormData = function (isEncode) {
        var fieldEncode = isEncode || false;
        var Form = $(this);
        var reData = new Object();
        $('input,select,textarea', Form).each(function () {
            var field = getInfolightOption($(this)).field;
            if (field != undefined) {
                var theVal = "";
                var controlClass = $(this).attr('class');
                if (controlClass != undefined) {
                    if (controlClass.indexOf('easyui-datebox') == 0) theVal = $(this).datebox('getBindingValue');
                    else if (controlClass.indexOf('easyui-combobox') == 0) theVal = $(this).combobox('getValue');
                    else if (controlClass.indexOf('easyui-datetimebox') == 0) theVal = $(this).datetimebox('getBindingValue');
                    else if (controlClass.indexOf('easyui-combogrid') == 0) theVal = $(this).combogrid('getValue');
                    else if (controlClass.indexOf('info-combobox') == 0) theVal = $(this).combobox('getValue');
                    else if (controlClass.indexOf('info-combogrid') == 0) theVal = $(this).combogrid('getValue');
                    else if (controlClass.indexOf('info-refval') == 0) theVal = $(this).refval('getValue');
                    else if (controlClass.indexOf('info-options') == 0) theVal = $(this).options('getValue');
                    else if (controlClass.indexOf('info-autocomplete') == 0) theVal = $(this).combobox('getValue');
                    else theVal = $(this).val();
                }
                else {
                    if ($(this).attr('type') == "checkbox") theVal = $(this).checkbox('getValue');
                    else theVal = $(this).val();
                }
                if (fieldEncode) reData[field] = encodeURIComponent(theVal).toString().replace(/(%22)/g, "\"").replace(/(%5C)/g, "\\");//双引号和反斜线不需要编码
                else reData[field] = theVal;
            }
        });
        return reData;
    }
})(jQuery);

//【infofileUpload相關】
; (function ($) {
    $.fn.jbFileUploadWithPhoto = function () {
        /* 隱藏數值欄位部分，並修改同名ID_Img的src
         */
        $(this).each(function () {
            var infofileUpload = $(this);
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next());
            $(infofileUploadvalue).hide();
            var src = '../' + getInfolightOption(infofileUpload).upLoadFolder + '/';
            $('#' + infofileUpload.attr('id') + '_Img').attr('src', src + infofileUploadvalue.val());
        });
    }
})(jQuery);

//【ComboTree】
; (function ($) {

    $.fn.jbCombobox2tree = function (_settings) {
        $(this).each(function () {
            var combobox = $(this);

            var settings = $.extend({}, jbCombobox2tree_default, _settings);

            //把Combobox的Textbox再次轉成ComboTree
            //資料來源為原Combobox的資料去撈

            //var remoteName = getInfolightOption(combobox).remoteName;
            //var tableName = getInfolightOption(combobox).tableName;
            //var rows = getInfolightOption(combobox).pageSize;
            //if (rows == undefined) rows = -1;

            var parentField = settings.parentField;
            if ($.type(parentField) != 'string' || !parentField) return;
            var valueField = getInfolightOption(combobox).valueField;
            var textField = getInfolightOption(combobox).textField;
            var panelHeight = getInfolightOption(combobox).panelHeight;

            var combotree = combobox.combo('textbox');

            combotree.combotree({
                //url: getRemoteUrl(remoteName, tableName, false, rows),
                editable: false,
                panelHeight: panelHeight === undefined ? 'auto' : panelHeight,
                loadFilter: function (data) {
                    var returnData = [], tempHash = {};

                    for (var i = 0; i < data.length; i++) {
                        var aObj = new Object();
                        aObj.id = data[i][valueField];
                        aObj.text = data[i][textField];
                        aObj.children = [];
                        aObj.attributes = data[i];
                        tempHash[data[i][valueField]] = aObj;
                    }

                    for (var i = 0; i < data.length; i++) {
                        var aHash = tempHash[data[i][parentField]];
                        if (aHash) {
                            aHash.state = 'closed';
                            aHash.children.push(tempHash[data[i][valueField]]);
                        }
                        else returnData.push(tempHash[data[i][valueField]]);
                    }
                    return returnData;
                },
                onShowPanel: function () {
                    var theData = combobox.combobox('getData');
                    combotree.combotree('loadData', theData);

                    var value = combobox.combo('getValue');
                    combotree.combotree('clear');
                    var treeObj = combotree.combotree('tree');
                    treeObj.tree('collapseAll');
                    var node = treeObj.tree('find', value);
                    if (node) {
                        combotree.combotree('setValue', value);
                        settings.hasFindValue(treeObj, node);
                    }
                    else settings.notFindValue(treeObj);
                }
            });

            //樣式調整
            combotree.combo('textbox').width(combobox.combo('textbox').width());
            combotree.combo('panel').panel('resize', { width: combobox.combo('panel').width() });
            combotree.data('combo').combo.width(combobox.width()).css('border-width', '0');

        });
    }

    var jbCombobox2tree_default = {
        parentField: 'parentField',
        hasFindValue: function (tree, node) {
            tree.tree('expandTo', node.target).tree('select', node.target);
        },
        notFindValue: function (tree) {
        }
    };

})(jQuery);

//【關於時間的方法】
; (function ($) {
    $.jbIsYearStr = function (Str) {
        /* 2XXX        -> O
         */
        var r = Str.match(/^([1-2]\d{3})$/);
        if (r == null) return false;
        return true;
    },
    $.jbIsDateStr = function (Str) {
        /* 2014-07-14  -> O
         * 2014/07/14  -> O
         * 2014.07.14  -> O
         * 2014 07 14  -> O
         */
        var r = Str.match(/^(\d{4})(-|\/|\.|\s)(0\d|1[0-2])\2(\d{1,2})$/);
        if (r == null) return false;
        var d = new Date(r[1], (r[3] - 1), r[4]);
        return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == r[4]);
    },
    $.jbIsYearMonthStr = function (Str) {
        /* 201401 -> O
         * 20141  -> X   
         */
        var r = Str.match(/^(\d{4})(0[1-9]|1[0-2])$/);
        if (r == null) return false;
        var d = new Date(r[1], (r[2] - 1), 1);
        return (d.getFullYear() == r[1] && d.getMonth() == (r[2] - 1) && d.getDate() == 1);
    },
    $.jbIsTimeFormat = function (Str) {
        /* 0000  -> O
         * 4830  -> O
         * 2459  -> X
         * 2480  -> X
         * 2430  -> O
         */
        var r = Str.match(/^(?:([0-3]\d|4[0-7])(30|00)|4800)$/);
                            
        if (r == null) return false;
        else return true;
    },
    $.jbIsTimeFormat_24 = function (Str) {
        /* 0000  -> O
         * 2359  -> O
         */
        var r = Str.match(/^(?:2[0-3]|[0-1][0-9])[0-5][0-9]$/);
        if (r == null) return false;
        else return true;
    },
    $.jbIsTimeFormat_card = function (Str) {
        /* 0000  -> O
         * 4830  -> O
         * 2459  -> X
         * 2480  -> X
         * 2430  -> O
         */
        var r = Str.match(/^(?:([0-3]\d|4[0-7])([0-5]\d)|4800)$/);
        if (r == null) return false;
        else return true;
    },
    $.jbDateDiff = function (interval, date1, date2) {
        /* interval=["years":"months":"weeks":"days":"hours":"minutes":"seconds"]
         * ex.
         * $.jbDateDiff('seconds','2014/01/01 12:00:00','2014/01/01 13:30:00');
         */
        var second = 1000, minute = second * 60, hour = minute * 60, day = hour * 24, week = day * 7;
        var fromDate = new Date(date1);
        var toDate = new Date(date2);
        var timediff = toDate - fromDate;
        if (isNaN(timediff)) return NaN;
        switch (interval.toLowerCase()) {
            case "years": return toDate.getFullYear() - fromDate.getFullYear();
            case "months": return ((toDate.getFullYear() * 12 + toDate.getMonth()) - (fromDate.getFullYear() * 12 + fromDate.getMonth()));
            case "weeks": return Math.floor(timediff / week);
            case "days": return Math.floor(timediff / day);
            case "hours": return Math.floor(timediff / hour);
            case "minutes": return Math.floor(timediff / minute);
            case "seconds": return Math.floor(timediff / second);
            default: return undefined;
        }
    },
    $.jbDateAdd = function (interval, number, TheDate) {
        /* interval=["years":"months":"days":"hours":"minutes":"seconds"]
         * ex.
         * $.jbDateAdd('seconds', 1, '2014-01-01 12:00:00');
         */
        var date = new Date(TheDate);
        if (isNaN(date)) return NaN;
        if (isNaN(number)) return NaN;
        else number = parseInt(number);
        switch (interval.toLowerCase()) {
            case "years": date.setFullYear(date.getFullYear() + number); break;
            case "months": date.setMonth(date.getMonth() + number); break;
            case "days": date.setDate(date.getDate() + number); break;
            case "hours": date.setHours(date.getHours() + number); break;
            case "minutes": date.setMinutes(date.getMinutes() + number); break;
            case "seconds": date.setSeconds(date.getSeconds() + number); break;
            default: date.setDate(date.getDate() + number); break;
        }
        return date;
    },
    $.jbGetFirstDate = function (aDate) {
        if (isNaN(aDate)) return NaN;
        return new Date(aDate.getFullYear(), aDate.getMonth(), 1);
    },
    $.jbGetLastDate = function (aDate) {
        if (isNaN(aDate)) return NaN;
        return new Date(aDate.getFullYear(), aDate.getMonth() + 1, 0);
    }
})(jQuery);

//=================================================================================================
//=                                              方法擴充                                         =
//=================================================================================================

//validate method
$.extend($.fn.validatebox.defaults.rules, {
    jbjobYYMM: {
        validator: function (value, param) {
            if (value == '' || value == undefined) return true;
            if ($.jbIsYearMonthStr(value)) return true;
            else return false;
        },
        message: "input YYMM is not valid."
    }
});

//【ComboBox】
$.extend($.fn.combobox.methods, {
    selectExistsForText: function (jq, value) {
        return jq.each(function () {
            var textField = $(this).combobox('options').textField;
            var valueField = $(this).combobox('options').valueField;
            var selectValue = "";
            var find = false;
            $.each($(this).combobox('getData'), function (index, object) {
                if (object[textField] == value) {
                    selectValue = object[valueField];
                    find = true;
                }
            });

            if (find) $(this).combobox('select', selectValue);
        });
    },
    selectExistsForValue: function (jq, value) {
        return jq.each(function () {
            var textField = $(this).combobox('options').textField;
            var valueField = $(this).combobox('options').valueField;
            var selectValue = "";
            var find = false;
            $.each($(this).combobox('getData'), function (index, object) {
                if (object[valueField] == value) {
                    selectValue = object[valueField];
                    find = true;
                }
            });

            if (find) $(this).combobox('select', selectValue);
        });
    },
    getSelectItem: function (jq) {
        var aCombobox = $(jq);
        var keyValue = aCombobox.combobox('getValue');
        var keyField = aCombobox.combobox('options').valueField;

        var theResult;
        $.each(aCombobox.combobox('getData'), function (index, rowData) {
            if (rowData[keyField] == keyValue) { theResult = rowData; return false; }
        });
        return theResult;
    }
});

// 建立編制部門 dialog
function initQueryDeptDialog() {
    $("#Dialog_Dept").dialog(
    {
        height: 400,
        width: 400,
        resizable: false,
        modal: true,
        title: "編制部門選項",
        closed: true,
        buttons: [{
            text: '取消',
            handler: function () { $("#Dialog_Dept").dialog("close") }
        },
        {
            text: "確認",
            handler: function () {
                var deptName = "";
                var deptRows = $("#DG_DEPT").datagrid("getRows");
                var checkedItems = $('#DG_DEPT').datagrid('getChecked');
                var flag;

                for (var k = 0; k < deptRows.length; k++) {
                    //判斷有勾選的 update 為 "Y"
                    flag = "N"
                    $.each(checkedItems, function (index, item) {
                        if (deptRows[k].DEPT_ID == item.DEPT_ID) {
                            deptRows[k].IS_SELECTED = "Y";
                            flag = "Y";
                            deptName = deptName + deptRows[k].DEPT_CODE + "-" + deptRows[k].DEPT_CNAME + ",";
                        }
                    });
                    if (flag != "Y")
                        deptRows[k].IS_SELECTED = "N";
                }

                $("#DEPT_CODE_Query").val(deptName);
                $("#Dialog_Dept").dialog("close");
            }
        }]
    });
};

// open編制部門查詢畫面 dialog
function openQueryDeptDialog() {
    var deptList = $("#DEPT_CODE_Query").val();
    $.ajax({
        type: "POST",
        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_System_Share.COLDEF', //連接的Server端，command
        data: "mode=method&method=" + "getDialogData" + "&parameters=" + "Dept" + "," + deptList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
        cache: false,
        async: false,
        success: function (data) {
            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
            if (rows.length > 0) {
                //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                $('#DG_DEPT').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                $('#DG_DEPT').datagrid('uncheckAll');
                for (var j = 0; j < rows.length; j++) {
                    if (rows[j].IS_SELECTED == "Y")
                        $('#DG_DEPT').datagrid('checkRow', j);
                }
            }
        }
    });
    $("#Dialog_Dept").dialog("open");
};

// 建立成本部門 dialog
function initQueryDeptcDialog() {
    $("#Dialog_Dept").dialog(
    {
        height: 400,
        width: 400,
        resizable: false,
        modal: true,
        title: "編制部門選項",
        closed: true,
        buttons: [{
            text: '取消',
            handler: function () { $("#Dialog_Dept").dialog("close") }
        },
        {
            text: "確認",
            handler: function () {
                var deptName = "";
                var deptRows = $("#DG_DEPT").datagrid("getRows");
                var checkedItems = $('#DG_DEPT').datagrid('getChecked');
                var flag;

                for (var k = 0; k < deptRows.length; k++) {
                    //判斷有勾選的 update 為 "Y"
                    flag = "N"
                    $.each(checkedItems, function (index, item) {
                        if (deptRows[k].DEPTC_ID == item.DEPTC_ID) {
                            deptRows[k].IS_SELECTED = "Y";
                            flag = "Y";
                            deptName = deptName + deptRows[k].DEPTC_CODE + "-" + deptRows[k].DEPTC_CNAME + ",";
                        }
                    });
                    if (flag != "Y")
                        deptRows[k].IS_SELECTED = "N";
                }

                $("#DEPT_CODE_Query").val(deptName);
                $("#Dialog_Dept").dialog("close");
            }
        }]
    });
};

// open成本部門查詢畫面 dialog
function openQueryDeptcDialog() {
    var deptList = $("#DEPT_CODE_Query").val();
    $.ajax({
        type: "POST",
        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_System_Share.COLDEF', //連接的Server端，command
        data: "mode=method&method=" + "getDialogData" + "&parameters=" + "Deptc" + "," + deptList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
        cache: false,
        async: false,
        success: function (data) {
            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
            if (rows.length > 0) {
                //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                $('#DG_DEPT').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                $('#DG_DEPT').datagrid('uncheckAll');
                for (var j = 0; j < rows.length; j++) {
                    if (rows[j].IS_SELECTED == "Y")
                        $('#DG_DEPT').datagrid('checkRow', j);
                }
            }
        }
    });
    $("#Dialog_Dept").dialog("open");
};

// 建立員別查詢畫面 dialog
function initQueryIdentityDialog() {
    $("#Dialog_Identity").dialog(
    {
        height: 400,
        width: 400,
        resizable: false,
        modal: true,
        title: "員別選項",
        closed: true,
        buttons: [{
            text: '取消',
            handler: function () { $("#Dialog_Identity").dialog("close") }
        },
        {
            text: "確認",
            handler: function () {
                var identityName = "";
                var identityRows = $("#DG_IDENTITY").datagrid("getRows");
                var checkedItems = $('#DG_IDENTITY').datagrid('getChecked');
                var flag;

                for (var k = 0; k < identityRows.length; k++) {
                    //判斷有勾選的 update 為 "Y"
                    flag = "N"
                    $.each(checkedItems, function (index, item) {
                        if (identityRows[k].IDENTITY_ID == item.IDENTITY_ID) {
                            identityRows[k].IS_SELECTED = "Y";
                            flag = "Y";
                            identityName = identityName + identityRows[k].IDENTITY_CODE + "-" + identityRows[k].IDENTITY_CNAME + ",";
                        }
                    });
                    if (flag != "Y")
                        identityRows[k].IS_SELECTED = "N";
                }

                $("#IDENTITY_CODE_Query").val(identityName);
                $("#Dialog_Identity").dialog("close");
            }
        }]
    });
};

// open員別查詢畫面 dialog
function openQueryIdentityDialog() {
    var identityList = $("#IDENTITY_CODE_Query").val();
    $.ajax({
        type: "POST",
        url: '../handler/jqDataHandle.ashx?RemoteName=_HRM_System_Share.COLDEF', //連接的Server端，command
        data: "mode=method&method=" + "getDialogData" + "&parameters=" + "Identity" + "," + identityList, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
        cache: false,
        async: false,
        success: function (data) {
            var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
            if (rows.length > 0) {
                //$('#DG_EMPLOYEE').datagrid('loadData', { "total": 0, "rows": [] });
                $('#DG_IDENTITY').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                $('#DG_IDENTITY').datagrid('uncheckAll');
                for (var j = 0; j < rows.length; j++) {
                    if (rows[j].IS_SELECTED == "Y")
                        $('#DG_IDENTITY').datagrid('checkRow', j);
                }
            }
        }
    });
    $("#Dialog_Identity").dialog("open");
};

function exportExcel(grid, flag, isBlank, isColumnName, reportName,rows) {
    var userid = getClientInfo("UserID");
    var exportFields = [];
    var fields = grid.datagrid('getColumnFields', true);
    for (var i = 0; i < fields.length; i++) {
        exportFields.push({ field: fields[i], title: grid.datagrid('getColumnOption', fields[i]).title });
    }
    fields = grid.datagrid('getColumnFields');
    for (var i = 0; i < fields.length; i++) {
        exportFields.push({ field: fields[i], title: grid.datagrid('getColumnOption', fields[i]).title });
    }

    //userid 使用者登入帳號;
    //flag == "0" 轉 Excel ; 
    //flag == "1" 轉 txt ; 
    //isBlank == "Y" 欄位與欄位間需要加一個空白字串 ; 
    //isBlank == "N" 欄位與欄位間不需要加一個空白字串; 
    //isColumnName == "Y" 需要加欄位表頭名稱 ; 
    //isColumnName == "N" 不需要加欄位表頭名稱; 
    $.ajax({
        type: "POST",
        url: "../handler/JBHRISHandler.ashx?ExportExcel=" + flag,//連接當前網頁的cs程式
        //data: { mode: 'ExportExcel', userid: userid, flag: flag, isBlank: 'N', isColumnName: 'N', reportName: 'OvertimeAbasentCheckist', fields: $.toJSONString(exportFields), rows: $.toJSONString(rows) },
        data: { mode: 'ExportExcel', userid: userid, flag: flag, isBlank: isBlank, isColumnName: isColumnName, reportName: reportName, fields: $.toJSONString(exportFields), rows: rows },
        cache: false,
        async: false,
        success: function (data) {
            //setTimeout(function () {
            //    $('#dlg').dialog('close'); // 結束顯示訊息, 故意設定結束一秒後才做(不然會看不到)
            //}, 1000);
            window.open('../handler/JqFileHandler.ashx?File=' + data, 'download');
        }
    });
}

function jsonExportExcel(exportFields, flag, isBlank, isColumnName, reportName, rows) {
    var userid = getClientInfo("UserID");
   
    //userid 使用者登入帳號;
    //flag == "0" 轉 Excel ; 
    //flag == "1" 轉 txt ; 
    //isBlank == "Y" 欄位與欄位間需要加一個空白字串 ; 
    //isBlank == "N" 欄位與欄位間不需要加一個空白字串; 
    //isColumnName == "Y" 需要加欄位表頭名稱 ; 
    //isColumnName == "N" 不需要加欄位表頭名稱; 
    $.ajax({
        type: "POST",
        url: "../handler/JBHRISHandler.ashx?ExportExcel=" + flag,//連接當前網頁的cs程式
        //data: { mode: 'ExportExcel', userid: userid, flag: flag, isBlank: 'N', isColumnName: 'N', reportName: 'OvertimeAbasentCheckist', fields: $.toJSONString(exportFields), rows: $.toJSONString(rows) },
        data: { mode: 'ExportExcel', userid: userid, flag: flag, isBlank: isBlank, isColumnName: isColumnName, reportName: reportName, fields: $.toJSONString(exportFields), rows: rows },
        cache: false,
        async: false,
        success: function (data) {
            //setTimeout(function () {
            //    $('#dlg').dialog('close'); // 結束顯示訊息, 故意設定結束一秒後才做(不然會看不到)
            //}, 1000);
            window.open('../handler/JqFileHandler.ashx?File=' + data, 'download');
        }
    });
}

//相片自動縮放計算處理
function ShowImage(ImgD, proMaxWidth, proMaxHeight) {
    var image = new Image();
    image.src = $(ImgD).attr('src');
    if (image.width > 0 && image.height > 0) {
        var rate = ((proMaxWidth / image.width) < (proMaxHeight / image.height)) ? (proMaxWidth / image.width) : (proMaxHeight / image.height);
        if (rate <= 1) {
            $(ImgD).attr('width', image.width * rate);
            $(ImgD).attr('height', image.height * rate);
        }
        else { $(ImgD).attr('width', image.width); $(ImgD).attr('height', image.height); }
    }
}

//=================================================================================================
//=                                              自訂方法                                         =
//=================================================================================================

$.jbjob = {};

//【關於時間的方法】
; (function ($) {

    $.jbjob.Date = {
        IsDateStr: function (Str) {
            /* 2014-07-14  -> O
             * 2014/07/14  -> O
             * 2014.07.14  -> O
             * 2014 07 14  -> O
             */
            var r = Str.match(/^(\d{4})(-|\/|\.|\s)(0\d|1[0-2])\2(\d{1,2})$/);
            if (r == null) return false;
            var d = new Date(r[1], (r[3] - 1), r[4]);
            return (d.getFullYear() == r[1] && d.getMonth() == (r[3] - 1) && d.getDate() == r[4]);
        },
        DateDiff: function (interval, date1, date2) {
            /* interval=["years":"months":"weeks":"days":"hours":"minutes":"seconds"]
             * ex.
             * DateDiff('seconds','2014/01/01 12:00:00','2014/01/01 13:30:00');
             */
            var second = 1000, minute = second * 60, hour = minute * 60, day = hour * 24, week = day * 7;
            var fromDate = new Date(date1);
            var toDate = new Date(date2);
            var timediff = toDate - fromDate;
            if (isNaN(timediff)) return NaN;
            switch (interval.toLowerCase()) {
                case "years": return toDate.getFullYear() - fromDate.getFullYear();
                case "months": return ((toDate.getFullYear() * 12 + toDate.getMonth()) - (fromDate.getFullYear() * 12 + fromDate.getMonth()));
                case "weeks": return Math.floor(timediff / week);
                case "days": return Math.floor(timediff / day);
                case "hours": return Math.floor(timediff / hour);
                case "minutes": return Math.floor(timediff / minute);
                case "seconds": return Math.floor(timediff / second);
                default: return undefined;
            }
        },
        DateAdd: function (interval, number, TheDate) {
            /* interval=["years":"months":"days":"hours":"minutes":"seconds"]
             * ex.
             * DateAdd('seconds', 1, '2014-01-01 12:00:00');
             */
            var date = new Date(TheDate);
            if (isNaN(date)) return NaN;
            if (isNaN(number)) return NaN;
            else number = parseInt(number);
            switch (interval.toLowerCase()) {
                case "years": date.setFullYear(date.getFullYear() + number); break;
                case "months": date.setMonth(date.getMonth() + number); break;
                case "days": date.setDate(date.getDate() + number); break;
                case "hours": date.setHours(date.getHours() + number); break;
                case "minutes": date.setMinutes(date.getMinutes() + number); break;
                case "seconds": date.setSeconds(date.getSeconds() + number); break;
                default: break;
            }
            return date;
        },
        DateFormat: function (interval, fmt) {
            if ($.type(interval) != 'date' || $.type(fmt) != 'string') return ''
            var o = {
                "M+": interval.getMonth() + 1, //月份 
                "d+": interval.getDate(), //日 
                "h+": interval.getHours(), //小时 
                "m+": interval.getMinutes(), //分 
                "s+": interval.getSeconds(), //秒 
                "q+": Math.floor((interval.getMonth() + 3) / 3), //季度 
                "S": interval.getMilliseconds() //毫秒 
            };
            if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (interval.getFullYear() + "").substr(4 - RegExp.$1.length));
            for (var k in o)
                if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
            return fmt;
        }
    }

})(jQuery);

//【關於字串的方法】
; (function ($) {

    $.jbjob.String = {
        StringFormat: function () {
            if (arguments.length == 0) return null;
            var str = arguments[0];
            for (var i = 0; i < arguments.length - 1; i++) {
                var re = new RegExp('\\{' + i + '\\}', 'gm');
                str = str.replace(re, arguments[i + 1]);
            }
            return str;
        }
    }

})(jQuery);

//=================================================================================================
//=                                              自訂plugin                                       =
//=================================================================================================

//修正EasyUI panel window dialogtl 超出範圍的問題
; (function ($) {

    var easyuiPanelOnMove = function (left, top) {
        var parentObj = $(this).panel('panel').parent();
        if (left < 0) {
            $(this).window('move', {
                left: 1
            });
        }
        if (top < 0) {
            $(this).window('move', {
                top: 1
            });
        }
        var width = $(this).panel('options').width;
        var height = $(this).panel('options').height;
        var right = left + width;
        var buttom = top + height;
        var parentWidth = parentObj.width();
        var parentHeight = parentObj.height();
        if (parentObj.css("overflow") == "hidden") {
            if (left > parentWidth - width) {
                $(this).window('move', {
                    "left": parentWidth - width
                });
            }
            if (top > parentHeight - height) {
                $(this).window('move', {
                    "top": parentHeight - height
                });
            }
        }
    };
    //$.fn.panel.defaults.onMove = easyuiPanelOnMove;
    //$.fn.window.defaults.onMove = easyuiPanelOnMove;
    //$.fn.dialog.defaults.onMove = easyuiPanelOnMove;

})(jQuery);



//【JQuery Focus改】                            
(function ($) {
    $.fn.jbSetFocus = function () { return this.each(function () { var dom = this; setTimeout(function () { try { dom.focus(); } catch (e) { } }, 0); }); };
})(jQuery);

//【Input】Input->顏色挑選
; (function ($) {
    $.fn.jbInput2ColorPick = function () {
        $(this).each(function () {
            $(this).spectrum({
                preferredFormat: 'hex',
                allowEmpty: true,
                showPaletteOnly: true,
                showPalette: true,
                hideAfterPaletteSelect: true,
                palette: [
                    ['black'],
                    ['gold', 'orange', 'chocolate'],
                    ['coral', 'red', 'crimson'],
                    ['limegreen', 'green', 'darkgreen'],
                    ['skyblue', 'blue', 'darkblue'],
                    ['violet', 'darkviolet', 'purple']
                ],
                change: function (color) {
                    $(this).val(color);
                }
            });
        });
    }
})(jQuery);

//【ListBox】
; (function ($) {
    var ggFnName = 'jbListbox';
    var inArray = function (target, key) {
        var allData = getData(target);
        for (var index = 0; index < allData.length; index++) {
            if (allData[index].key == key) return index;
        };
        return -1;
    };
    var gridCheck = function (target) {
        var theOptions = options(target);
        if (theOptions) {
            var checkedIndexs = [];
            $(target.datagrid('getChecked')).each(function () {
                var aIndex = target.datagrid('getRowIndex', this);
                checkedIndexs.push(aIndex);
            });

            $.each(target.datagrid('getRows'), function (index) {
                var key = theOptions.getOption(this).key;
                var find = inArray(target, key);
                if (find != -1 && $.inArray(index, checkedIndexs) == -1) target.datagrid('checkRow', index);
                else if (find == -1 && $.inArray(index, checkedIndexs) != -1) target.datagrid('uncheckRow', index);
            });
        }
    };
    var options = function (target) {
        return target.data(ggFnName).options;
    };
    var getTheSelect = function (target) {
        return target.data(ggFnName).theSelect;
    };
    var getData = function (target) {
        var allData = target.data(ggFnName).data;
        if (allData == undefined) return new Array();
        return allData;
    };
    var setData = function (target, allData) {
        target.data(ggFnName).data = allData;
    };
    var reload = function (target) {
        var theSelect = getTheSelect(target);
        if (theSelect) {
            $('option', theSelect).remove();
            var allData = getData(target);
            $.each(allData, function (index, object) {
                $('<option>').data('key', object.key).html(object.value).appendTo(theSelect);
            });
            gridCheck(target);
            var theOptions = options(target);
            if (theOptions) {
                var onSuccess = theOptions.onLoadSuccess;
                if (onSuccess != undefined) onSuccess.call(theSelect, allData);
            }
        }
    };
    var add = function (target, theData) {
        if ($.type(theData) == "array") {
            $.each(theData, function (i, object) {
                add(target, object);
            });
        }
        else if ($.type(theData) == "object") {
            var aObject = { key: theData.key, value: theData.value };
            if (inArray(target, aObject.key) == -1) {
                var allData = getData(target);
                allData.push(aObject);
                setData(target, allData);
                reload(target);
            }
        }
    };
    var remove = function (target, theKey) {
        if ($.type(theKey) == "array") {
            $.each(theKey, function (i, key) {
                remove(target, key);
            });
        }
        else {
            var Index = inArray(target, theKey);
            if (Index != -1) {
                var allData = getData(target);
                var newData = $.grep(allData, function (n, i) { return (i != Index); });
                setData(target, newData);
                reload(target);
            }
        }
    };
    var removeSelect = function (target) {
        var theSelect = getTheSelect(target);
        if (theSelect) {
            var removeData = [];
            $('option:selected', theSelect).each(function () {
                removeData.push($(this).data('key'));
            });
            remove(target, removeData);
        }
    };
    var removeAll = function (target) {
        setData(target, new Array());
        reload(target);
    };
    var getKeys = function (target) {
        var allKey = [];
        $.each(getData(target), function (index, obj) {
            allKey.push(obj.key);
        });
        return allKey;
    };
    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) target.removeData(ggFnName);

        var theOptions = $.extend({}, $.fn[ggFnName].defaults, parameter);

        //標籤產生
        var theGridPanel = target.datagrid('getPanel').parent();
        var theTable = $('<table>', { width: '100%' });
        theGridPanel.after(theTable);
        var theTr = $('<tr>').appendTo(theTable);
        var tdGrid = $('<td>').appendTo(theTr);
        var tdTool = $('<td>', { width: 50 }).appendTo(theTr);
        var tdSelect = $('<td>', { width: theOptions.width }).appendTo(theTr);
        var theSelect = $('<select>', { multiple: 'multiple', width: '100%', height: '100%' }).appendTo(tdSelect);
        theGridPanel.appendTo(tdGrid);
        $(target).datagrid({ width: 100 }).datagrid({ width: 'auto' });

        var btnBack = $('<a>', { href: 'javascript:void(0)' }).appendTo(tdTool).linkbutton({ iconCls: 'icon-back' }).on('click', function () {
            removeSelect(target);
        });

        var btnBackAll = $('<a>', { href: 'javascript:void(0)' }).appendTo(tdTool).linkbutton({ iconCls: 'icon-no' }).on('click', function () {
            removeAll(target);
        });

        //註冊事件
        target.datagrid({
            onCheck: function (rowIndex, rowData) {
                add(target, theOptions.getOption(rowData));
            },
            onCheckAll: function (rows) {
                var addData = [];
                $(rows).each(function (index, rowData) {
                    addData.push(theOptions.getOption(rowData));
                });
                add(target, addData);
            },
            onUncheck: function (rowIndex, rowData) {
                remove(target, theOptions.getOption(rowData).key);
            },
            onUncheckAll: function (rows) {
                var removeKey = [];
                $(rows).each(function (index, rowData) {
                    removeKey.push(theOptions.getOption(rowData).key);
                });
                remove(target, removeKey);
            }
        });


        //設定一些data
        //options,form,dialog,dynamicTable
        target.data(ggFnName, { options: theOptions, theTool: tdTool, theSelect: theSelect });
    };

    $.fn[ggFnName] = function (theOptions, parameter) {
        if (typeof theOptions == "string") {
            return $.fn[ggFnName].methods[theOptions](this, parameter);
        }
        else {
            theOptions = theOptions || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', theOptions);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        options: function (target) {
            return options($(target[0]));
        },
        getTheSelect: function (target) {
            return getTheSelect($(target[0]));
        },
        add: function (target, aOption) {
            target.each(function () {
                add($(this), aOption);
            });
            return target;
        },
        remove: function (target, key) {
            target.each(function () {
                remove($(this), key);
            });
            return target;
        },
        getData: function (target) {
            return getData($(target[0]));
        },
        getKeys: function (target) {
            return getKeys($(target[0]));
        },
        setData: function (target, parameter) {
            target.each(function () {
                setData($(this), parameter);
            });
            return target;
        },
        reload: function (target) {
            target.each(function () {
                reload($(this));
            });
            return target;
        },
        removeSelect: function (target) {
            target.each(function () {
                removeSelect($(this));
            });
            return target;
        },
        removeAll: function (target) {
            target.each(function () {
                removeAll($(this));
            });
            return target;
        },
        gridCheck: function (target) {
            target.each(function () {
                gridCheck($(this));
            });
            return target;
        }
    }

    $.fn[ggFnName].defaults = {
        width: 200,
        getOption: function (rowData) {
            return { key: rowData.key, value: rowData.value };
        },
        onLoadSuccess: function (allData) {
            //this = theSelect
        }
    }
})(jQuery);

//【NcalcCheck】
; (function ($) {
    var ggFnName = 'jbNcalcCheck';

    var Template = function (options) {
        var aTr = $('<tr>');
        $('<td>', { style: 'text-align: right; width:15%;' }).html('參數名稱').appendTo(aTr);
        $('<td>', { style: 'width:30%;' }).append($('<input>', { name: 'Parameter[][Key]', style: 'width: 80%;' })).appendTo(aTr);
        $('<td>', { style: 'text-align: right; width:15%;' }).html('參數數值').appendTo(aTr);
        $('<td>', { style: 'width:30%;' }).append($('<input>', { name: 'Parameter[][Value]', style: 'width: 80%;' })).appendTo(aTr);
        $('<td>', { style: 'width:10%;' }).append($('<a>', { href: 'javascript:void(0)' }).on('click', function () { $(this).closest('tr').remove(); }).linkbutton({ iconCls: 'icon-cancel', plain: true })).appendTo(aTr);
        return aTr;
    };
    var MainTable = function (options) {
        var aTable = $('<table>', { style: 'width:100%; height:100%;' });
        var aTr = $('<tr>').appendTo(aTable);
        $('<td>', { style: 'text-align: right;width: 10%;' }).html('公式').appendTo(aTr);
        $('<td>', { style: 'width: 90%;' }).append($('<textarea>', { name: 'Expression', style: 'width: 90%; height: 90%' })).appendTo(aTr);
        return aTable;
    };
    var Layout = function (options) {
        var theLayoutMain = $('<div>', { 'class': 'easyui-layout' }).width(options.width).height(options.height);
        var theLayoutNorth = $('<div>', { 'data-options': "region:'north',split:false,border:false" }).height(options.formulaHeight).append(MainTable(options)).appendTo(theLayoutMain);
        var theLayoutCenter = $('<div>', { 'data-options': "region:'center',border:false" }).appendTo(theLayoutMain);
        return theLayoutMain;
    };
    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) {
            if (state.dialog) state.dialog.dialog('destroy');
            target.removeData(ggFnName);
        }

        //標籤產生
        var options = $.extend({}, $.fn[ggFnName].defaults, parameter);
        var theMainDialog = $('<div>');
        var theForm = $('<form>').appendTo(theMainDialog);
        var theLayout = Layout(options).appendTo(theForm).layout();
        var theDynamicTable = $('<table>').width(theLayout.width() - 30).appendTo(theLayout.layout('panel', 'center'));

        //設定一些data
        //options,form,dialog,dynamicTable
        target.data(ggFnName, { options: options, form: theForm, dialog: theMainDialog, dynamicTable: theDynamicTable });

        var defaultsToolbar = [
            { text: '新增參數', iconCls: 'icon-add', handler: function () { theDynamicTable.append(Template(options)); } },
            { text: '檢查', iconCls: 'icon-ok', handler: function () { var data = getData(target); options.OnCheckSubmit(data); } }
        ];

        var defaultsButtons = [
            { text: '貼回', iconCls: 'icon-back', handler: function () { var data = getData(target); if (data && data.Expression) { target.val(data.Expression); } } },
            { text: '關閉', iconCls: 'icon-cancel', handler: function () { close(target); } }
        ];

        theMainDialog.dialog({
            title: ' ',
            width: 'auto',
            height: 'auto',
            closed: false,
            modal: true,
            toolbar: defaultsToolbar,
            buttons: defaultsButtons
        }).dialog('close');

    };
    var open = function (target) {
        var state = target.data(ggFnName);
        if (state) {
            if (state.dynamicTable) state.dynamicTable.empty();
            if (state.form) state.form.form('load', { 'Expression': target.val() });
            if (state.dialog) state.dialog.dialog('center').dialog('open');
        }
    };
    var close = function (target) {
        var state = target.data(ggFnName);
        if (state) {
            if (state.dialog) state.dialog.dialog('close');
        }
    };
    var getData = function (target) {
        var form = target.data(ggFnName).form;
        if (form) {
            var data = form.serializeJSON({ useIntKeysAsArrayIndex: true });

            var theParameter = {};
            if (data.Parameter) {
                $.each(data.Parameter, function (index, object) {
                    if (object.Key && object.Value) theParameter[object.Key] = object.Value;
                });
            }
            data.Parameter = theParameter;
            return data;
        } else return {};
    };
    var getElement = function (target, name) {
        if ($.type(name) == 'string' && $.inArray(name, ["options", "form", "dialog", "dynamicTable"]) != -1) {
            return target.data(ggFnName)[name];
        }
    };
    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        open: function (target) {
            target.each(function () {
                open($(this));
            });
            return target;
        },
        close: function (target) {
            target.each(function () {
                close($(this));
            });
            return target;
        },
        getData: function (target) {
            return getData($(target[0]));
        },
        getElement: function (target, name) {
            return getElement($(target[0]), name);
        }
    }

    $.fn[ggFnName].defaults = {
        width: 450,
        height: 300,
        formulaHeight: 95,
        OnCheckSubmit: function (data) {
            $.messager.alert(' ', $.toJSONString(data));
        }
    }
})(jQuery);

//【TheDateTable】
; (function ($) {
    var ggFnName = 'jbTheDateTable';
    var theBoxNum = 42;

    //42個格子的串鍊
    var theNumShift = function (theNum) {
        if (isNaN(theNum)) return 1;
        if (theNum == 0) return theBoxNum;
        else if (theNum > 0) {
            if (theNum < theBoxNum) return theNum;
            else return theNumShift(theNum % theBoxNum);
        }
        else if (theNum < 0) {
            theNum = Math.abs(theNum);
            if (theNum < theBoxNum) return theBoxNum - theNum;
            else return theNumShift(theNum % theBoxNum);
        }
    };

    //設定值
    var getOptions = function (target) {
        return target.data(ggFnName).options;
    };

    //Thead元素
    var getThead = function (target) {
        return target.data(ggFnName).thead;
    };

    //Tbody元素
    var getTbody = function (target) {
        return target.data(ggFnName).tbody;
    };

    //取得日期
    var getDate = function (target) {
        return target.data(ggFnName).options.theDate;
    };

    //設定日期
    var setDate = function (target, newDate) {
        target.data(ggFnName).options.theDate = newDate;
    };

    //載入日期
    var loadDate = function (target, newDate) {
        //舊的日期
        var oldDate = getDate(target);

        //-------------------------------Thead日期----------------------------------------
        var thead = getThead(target);
        $('tr:eq(0) th', thead).html($.jbjob.Date.DateFormat(newDate, 'yyyy-MM'));

        //-------------------------------Tbody內容修正------------------------------------
        var tbody = getTbody(target);
        var theDaysInMonth = new Date(newDate.getFullYear(), newDate.getMonth() + 1, 0).getDate();//本月天數
        var theDayStart = newDate.getDay(); //本月起始星期

        //日期顯示
        $('th', tbody).each(function () {
            var data = $(this).data(ggFnName);
            var dateNum = (data.row * 7) + data.cell - theDayStart;
            $(this).html((1 <= dateNum && dateNum <= theDaysInMonth) ? "00".substring(0, 2 - dateNum.toString().length) + dateNum.toString() : ' ');
        });
        //起始星期幾(0~6)相減，算出位移量
        var numShift = newDate.getDay() - oldDate.getDay();

        //內容移動(先把東西都移出來再放)
        var allElement = {};
        $('td', tbody).each(function () {
            var data = $(this).data(ggFnName);
            var cellNum = (data.row * 7) + data.cell;
            allElement[theNumShift(cellNum + numShift)] = $(this).children();
        });
        $('td', tbody).each(function () {
            var data = $(this).data(ggFnName);
            var cellNum = (data.row * 7) + data.cell;
            $(this).append(allElement[cellNum]);
        });

        //設定成新的日期
        setDate(target, newDate);

        //載入完成的事件
        var event = getOptions(target).onLoad;
        if (event) event.call(target);
    };

    //改變日期
    var changeDate = function (target, newDate) {
        //舊的日期
        var oldDate = getDate(target);

        //日期判斷
        if ($.type(oldDate) != 'date' || isNaN(oldDate)) return;
        else if ($.type(newDate) != 'date' || isNaN(newDate)) return;
        else if (oldDate == newDate) return;
        else loadDate(target, newDate);
    };

    //取得指定日期號碼的Box(如果沒有這個日期一樣會回傳)
    var getDateBox = function (target, dateNum) {
        //日期
        var theDate = getDate(target);

        //本月起始星期 + 上指定日子
        var theNum = theNumShift(theDate.getDay() + dateNum);

        //Tbody
        var tbody = getTbody(target);

        var theTD = [];
        //日期顯示
        $('td', tbody).each(function () {
            var data = $(this).data(ggFnName);
            var dateNum = (data.row * 7) + data.cell;
            if (dateNum == theNum) theTD = $(this);
        });
        return $('div:first', theTD);
    };

    //初始化
    var initialize = function (target, parameter) {
        //設定值載入
        var options = $.extend({}, $.fn[ggFnName].defaults, parameter);

        //日期修正
        if ($.type(options.theDate) != 'date' || isNaN(options.theDate)) options.theDate = new Date();

        //標籤操作
        target.addClass('schedule-table').empty();

        //Thead標籤
        var aTableHead = $('<thead>').appendTo(target);
        $('<tr>', { 'class': 'schedule-header-row' }).appendTo(aTableHead).append($('<th>', { colspan: 7, style: 'text-align:center;' }));
        $('<tr>', { 'class': 'schedule-header-row' }).appendTo(aTableHead).
            append($('<th>', { style: 'text-align:center;' }).html('日')).
            append($('<th>', { style: 'text-align:center;' }).html('一')).
            append($('<th>', { style: 'text-align:center;' }).html('二')).
            append($('<th>', { style: 'text-align:center;' }).html('三')).
            append($('<th>', { style: 'text-align:center;' }).html('四')).
            append($('<th>', { style: 'text-align:center;' }).html('五')).
            append($('<th>', { style: 'text-align:center;' }).html('六'));

        //Tbody標籤
        var aTableBody = $('<tbody>').appendTo(target);
        for (var iRow = 0; iRow < 6; iRow++) {          //寫死創出6個禮拜的格子(最大的可能性)
            var aTrTitle = $('<tr>', { 'class': 'schedule-date-row' }).appendTo(aTableBody);
            var aTrConent = $('<tr>', { 'class': 'schedule-row' }).appendTo(aTableBody);
            for (var iCell = 1; iCell <= 7; iCell++) {
                var aTdTitle = $('<th>', { 'class': iCell == 7 ? 'schedule-cell schedule-last-cell' : 'schedule-cell' }).appendTo(aTrTitle);
                var aTdConent = $('<td>', { 'class': iCell == 7 ? 'schedule-cell schedule-last-cell' : 'schedule-cell' }).appendTo(aTrConent);
                aTdTitle.data(ggFnName, { row: iRow, cell: iCell });
                aTdConent.append($('<div>')).data(ggFnName, { row: iRow, cell: iCell });
            }
        }

        //設定一些data
        target.data(ggFnName, { options: options, thead: aTableHead, tbody: aTableBody });

        //把標題都弄一弄
        loadDate(target, options.theDate);

        //剛創好的事件
        var event = options.onCreate;
        if (event) event.call(target);
    };

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == 'string') {
            return $.fn[ggFnName].methods[options](this, parameter);
        }
        options = options || {};
        return this.each(function () {
            $(this)[ggFnName]('initialize', options);
        });
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        thead: function (target) {
            return getThead($(target[0]));
        },
        options: function (target) {
            return getOptions($(target[0]));
        },
        tbody: function (target) {
            return getTbody($(target[0]));
        },
        getDate: function (target) {
            return getDate($(target[0]));
        },
        getDateBox: function (target, dateNum) {
            return getDateBox($(target[0]), dateNum);
        },
        setDate: function (target, newDate) {
            target.each(function () {
                setDate($(this), newDate);
            });
            return target;
        },
        changeDate: function (target, newDate) {
            target.each(function () {
                changeDate($(this), newDate);
            });
            return target;
        }
    }

    $.fn[ggFnName].defaults = {
        theDate: new Date(),
        onCreate: function () { },
        onLoad: function () { }
    }
})(jQuery);

//【關於jbImportExcel的方法】
; (function ($) {
    var ggFnName = 'jbImportExcel';

    //去dll取得Title的動作
    var getTitle = function (target, fileName) {
        //開始表頭讀檔動作
        $.messager.progress({ msg: 'Loading...' });//進度條開始
        $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_System_Share', {
            mode: 'method', method: 'ExcelGetTitleName', parameters: fileName
        }).done(function (data) {
            var Json = $.parseJSON(data);
            if (Json.IsOK) {
                //執行使用者設定的動作，使用者設定由target拿到
                var state = target.data(ggFnName);
                if (state && state.options && state.options.OnGetTitleSuccess) state.options.OnGetTitleSuccess.call(target, Json.Result, fileName);
            }
            else alert(Json.ErrorMsg);
        }).fail(function (xhr, textStatus, errorThrown) {
            alert('error');
        }).always(function () {
            $.messager.progress('close'); //進度條結束
        });
    };

    //進行上傳動作，回傳內容詳見JBHRISHandler
    var fileUpload = function (target, elementID) {
        $.ajaxFileUpload({
            url: '../handler/JBHRISHandler.ashx?mode=ImportFile',
            secureuri: false,
            data: {
                Filter: 'xls|xlsx'
            },
            fileElementId: elementID,
            dataType: 'json',
            success: function (json) {
                if (!json.IsOK) alert(json.ErrorMsg);
                else getTitle(target, json.Result);     //上傳完就去GetTitle
            },
            error: function (json) {
                alert('error');
            }
        });
    };

    //初始化
    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) target.removeData(ggFnName);

        //target的options設定，寫進data
        var options = $.extend({}, $.fn[ggFnName].defaults, parameter);
        target.data(ggFnName, { options: options });

        target.dialog({
            title: '上傳Excel',
            iconCls: 'icon-importExcel',
            collapsible: false,
            minimizable: false,
            maximizable: false,
            resizable: false,
            closed: true,
            href: '../InnerPages/ImportExcel.aspx',
            width: 500,
            height: 80,
            modal: true,
            onLoad: function () {
                //修改input file標籤
                var fileInput = $('#jbImportExcelFileUpload', this);
                var newID = target.attr('id') + '_' + fileInput.attr('id');
                fileInput.attr({ id: newID, name: newID });

                //觸發上傳動作
                $(".easyui-linkbutton", this).on('click', function () {
                    fileUpload(target, newID);
                });
            }
        });
    };

    var open = function (target) {
        target.window('open');
    };

    var close = function (target) {
        target.window('close');
    };

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        open: function (target) {
            target.each(function () {
                open($(this));
            });
            return target;
        },
        close: function (target) {
            target.each(function () {
                close($(this));
            });
            return target;
        }
    }

    $.fn[ggFnName].defaults = {
        OnGetTitleSuccess: function (dataArray, fileName) {
            alert('Array Length:' + dataArray.length);
        }
    }

})(jQuery);

//【關於jbExcelHandlerFileImport的方法】
; (function ($) {
    var ggFnName = 'jbExcelHandlerFileImport';

    //取得Options
    var getOptions = function (target) {
        return target.data(ggFnName).options;
    };
  
    //取得FilePath
    var getFilePath = function (target) {
        return target.data(ggFnName).filePath;
    };

    //設定FilePath
    var setFilePath = function (target, filePath) {
        target.data(ggFnName).filePath = filePath;
    };

    //進行上傳分析表頭的動作，回傳內容詳見ExcelHandler
    var fileUpload = function (target, newID) {        
        var theOptions = getOptions(target);            //取得Options
        $.ajaxFileUpload({
            url: '../handler/JbExcelHandler.ashx?mode=ExcelFileGetTitle',
            secureuri: false,
            fileElementId: newID,  //ID
            dataType: 'json',
            success: function (json) {
                if (!json.IsOK) alert(json.ErrorMsg);
                else {                    
                    setFilePath(target, json.ErrorMsg); //設定FilePath
                    theOptions.OnGetTitleSuccess(json.Result, json.ErrorMsg);//觸發事件
                }
            },
            error: function (data) {
                //alert(data);
                alert('error');
            }
        });
    };

    //初始化
    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) target.removeData(ggFnName);

        //target的options設定，寫進data
        var options = $.extend({}, $.fn[ggFnName].defaults, parameter);
        target.data(ggFnName, { options: options });
        
        //定義好的方法參數
        var theSystemDialogOption = {
            href: '../InnerPages/ImportExcel.aspx',
            onLoad: function () {
                //修改input file標籤
                var fileInput = $('#jbImportExcelFileUpload', this);
                var newID = target.attr('id') + '_' + fileInput.attr('id');
                fileInput.attr({ id: newID, name: newID });

                //觸發上傳動作
                $(".easyui-linkbutton", this).on('click', function () {
                    fileUpload(target, newID);
                });
            }
        };

        //Dialog
        target.dialog($.extend(options.dialogOptions, theSystemDialogOption));
    };

    //匯入作業
    var importFile = function (target, parameter, fileName) {
        var theFilePath = getFilePath(target);  //取得FilePath        
        fileName = fileName || theFilePath;        

        var theOptions = getOptions(target);    //取得Options
        var ImportParameter = $.extend(theOptions.ImportOptions, parameter);

        $.ajax({
            url: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'ExcelFileImport', remoteName: ImportParameter.remoteName, method: ImportParameter.method, fileName: fileName }),
            data: { handler: $.toJSONString(ImportParameter.handler), parameters: ImportParameter.parameters },
            type: 'POST',
            async: true,
            success: function (jsonStr) { theOptions.OnImportSuccess(jsonStr); },
            beforeSend: function () { $.messager.progress({ msg: '執行中...' }); },
            complete: function () { $.messager.progress('close'); },
            error: function (xhr, ajaxOptions, thrownError) { alert('error'); }
        });
    };    

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        options: function (target) {
            return getOptions($(target[0]));
        },        
        filePathName: function (target) {
            return getFilePath($(target[0]));
        },
        importFile: function (target, parameter, fileName) {
            target.each(function () {
                importFile($(this), parameter, fileName);
            });
            return target;
        }
    }

    $.fn[ggFnName].defaults = {
        dialogOptions: {
            title: 'Excel欄位分析',
            iconCls: 'icon-importExcel',
            width: 500,
            height: 80,
            closed: true,
            modal: true
        },
        ImportOptions: {
            remoteName: '',
            method: '',
            handler: {},
            parameters: ''
        },
        OnGetTitleSuccess: function (dataArray, fileName) {
            alert('Array Length:' + dataArray.length);
        },
        OnImportSuccess: function (jsonStr) {
            alert(jsonStr);
        }
    }

})(jQuery);

//【DialogPlugin】
; (function ($) {
    var ggFnName = 'jbDialogPlugin';

    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) target.removeData(ggFnName);

        //標籤產生
        var theOptions = $.extend({}, $.fn[ggFnName].defaults, parameter);

        var dialogContent = target.hasClass('easyui-dialog') ? target.find('.dialog-content :first') : target;
        var panelContent = dialogContent.find('.panel :first');

        //設定一些data
        //options.....
        target.data(ggFnName, { options: theOptions, dialogContent: dialogContent, panelContent: panelContent });

        var onInit = theOptions.onInit;
        if (onInit != undefined) onInit.call(target);
    };

    var dialogContent = function (target) {
        return target.data(ggFnName).dialogContent;
    };

    var panelContent = function (target) {
        return target.data(ggFnName).panelContent;
    };

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        dialogContent: function (target) {
            return dialogContent($(target[0]));
        },
        panelContent: function (target) {
            return panelContent($(target[0]));
        }
    }

    $.fn[ggFnName].defaults = {
        onInit: function () {
            var target = $(this);
            dialogContent($(target[0])).css('padding', 0);
            panelContent($(target[0])).css('padding', 0);
        }
    }
})(jQuery);

//【DateBoxMultiple】textarea->日期多選
; (function ($) {
    /*將EasyUI Calendar 改一點之後 擴充的功能
     *結合DateBox達到多選的目的
     */

    var ggFnName = 'jbDateBoxMultiple';

    var options = function (target) {
        return target.data(ggFnName).options;
    };

    var select = function (target) {
        return target.data(ggFnName).select;
    };

    var calendar = function (target) {
        return target.data(ggFnName).calendar;
    };

    var getData = function (target) {
        return target.data(ggFnName).data;
    };

    //fun資料存在的判斷
    var dateInArray = function (aDate, allDate) {
        var index = -1;
        $.each(allDate, function (i, o) {
            if (o.valueOf() == aDate.valueOf()) index = i;
        });
        return index;
    };

    //確認日期是否存在
    var isExists = function (target, date) {
        var allDate = getData(target);
        if (dateInArray(date, allDate) == -1) return false;
        else return true;
    };

    //刪除單一日期
    var deleteDate = function (target, date) {
        var allDate = getData(target);

        var index = dateInArray(date, allDate);
        if (index != -1) allDate = $.grep(allDate, function (n, i) { return i != index });

        target.data(ggFnName).data = allDate;
    };

    //加入單一日期
    var addDate = function (target, date) {

        //加入新增前事件
        var theOptions = options(target);
        if (theOptions.onBeforeAdd != undefined) {
            var ans = theOptions.onBeforeAdd.call(target, date);
            if (ans != undefined && ans.toString() == 'false') return;
        }

        //準備新增
        var allDate = getData(target);
        var index = dateInArray(date, allDate);
        if (index == -1) allDate.push(date);
        target.data(ggFnName).data = allDate;
    };

    //select選擇的項目
    var remove = function (target) {
        var aSelect = select(target);

        $.each(aSelect.find('option:selected'), function (i, o) {
            deleteDate(target, $(o).data('date'));
        });
        refresh(target);
    };

    //刷新所有資料顯示
    var refresh = function (target) {

        var theOptions = options(target);
        
        var allDate = getData(target).sort(function (x, y) { //資料sort
            return x.valueOf() - y.valueOf();
        });
        
        var aSelect = select(target).empty();   //select清掉插入        
        target.val('');                         //textarea清掉插入

        $.each(allDate, function (i, o) {
            aSelect.append($('<option>').data('date',o).html(theOptions.dateItemText(o)));
            target.val(target.val() + theOptions.dateItemValue(o) + "\n");            
        });

        var aCalendar = calendar(target);        

        var allDateTD = $(aCalendar).find('td', '.calendar-body tbody');
        $.each(allDateTD, function (i, o) {
            var thisTD = $(o).removeClass('calendar-selected');
            if (isExists(target, new Date(thisTD.attr('abbr')))) thisTD.addClass('calendar-selected');

            //加入顯示事件
            if (theOptions.onShowTD != undefined) theOptions.onShowTD.call(target, o);
        });

        
    };

    //將字串轉存進去
    var setData = function (target) {
        var allDateStr = target.val().split("\n");

        var allDate = [];
        $.each(allDateStr, function (i, o) {
            if ($.jbjob.Date.IsDateStr(o)) {
                allDate.push(new Date(o));
            }
        });

        target.data(ggFnName).data = allDate;

        //日期回到今天
        calendar(target).calendar({
            year: new Date().getFullYear(),
            month: new Date().getMonth() + 1,
            current: new Date()
        });

        refresh(target);
    };

    //觸發時段點選，點到可能是加入可能是刪除
    var onSelect = function (target, date) {

        //加入選擇前事件
        var theOptions = options(target);
        if (theOptions.onBeforeSelect != undefined) {
            var ans = theOptions.onBeforeSelect.call(target, date);
            if (ans != undefined && ans.toString() == 'false') {
                refresh(target);
                return;
            }
        }

        if (isExists(target, date)) deleteDate(target, date);
        else addDate(target, date);

        refresh(target );
    }

    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) {
            state.options = $.extend(state.options, parameter);
            target.data(ggFnName, state);
        }
        else {

            //屬性產生
            var theOptions = $.extend({}, $.fn[ggFnName].defaults, parameter);

            //格局TABLE
            var aTable = $('<table>');
            var aTr = $('<tr>').appendTo(aTable);

            //Select
            var aSelect = $('<select>', { width: theOptions.width, height: theOptions.height, multiple: '' });
            aSelect.appendTo($('<td>').appendTo(aTr));

            var aTd = $('<td>', { style: 'vertical-align: top; text-align: center; ' }).appendTo(aTr);
            var aDateBox = $('<input>').appendTo(aTd);
            aTd.append($('<br>'));
            var aBtnCancel = $('<a>', { href: 'javascript:void(0)' }).appendTo(aTd).linkbutton({ plain: true, iconCls: 'icon-no' }).on('click', function () {
                remove(target);
            });

            target.hide().after(aTable);

            aDateBox.datebox().datebox('textbox').hide();
            aDateBox.datebox('textbox').parent().width('auto');

            //日曆
            var aCalendar = aDateBox.datebox('calendar');

            //設定一些data
            //options.....
            target.data(ggFnName, { options: theOptions, select: aSelect, calendar: aCalendar, data: [] });

            //設定日曆事件
            aCalendar.calendar({
                onSelect: function (date) {
                    onSelect(target, date);
                    return false;
                },
                onShow: function (date) {
                    refresh(target);
                }
            });
        }
    };

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            return this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        options: function (target) {
            return options($(target[0]));
        },
        select: function (target) {
            return select($(target[0]));
        },
        calendar: function (target) {
            return calendar($(target[0]));
        },
        refresh: function (target) {
            target.each(function () {
                refresh($(this));
            });
            return target;
        },
        remove: function (target) {
            target.each(function () {
                remove($(this));
            });
            return target;
        },
        setData: function (target) {
            target.each(function () {
                setData($(this));
            });
            return target;
        },
        getData: function (target) {
            return getData($(target[0]));
        }

    }

    $.fn[ggFnName].defaults = {
        width: 125,
        height: 150,
        dateItemText: function (aDate) {
            return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
        },
        dateItemValue: function (aDate) {
            return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
        },
        onBeforeSelect: function (date) {
            return true;
        },
        onBeforeAdd: function (date) {
            return true;
        },
        onShowTD: function (theTD) {
        }
    }
})(jQuery);

//【DataGridMenuButton】
; (function ($) {
    //綁在Grid之下，需要ID


    var ggFnName = 'jbDataGridMenuButton';

    var initialize = function (target, parameter) {

        var state = target.data(ggFnName);
        if (state) target.removeData(ggFnName);

        //標籤產生
        var theOptions = $.extend({}, $.fn[ggFnName].defaults, parameter);
                
        var gridID = $(target).attr('id');

        theOptions.id = gridID + theOptions.id;

        //設定一些data
        //options.....
        target.data(ggFnName, { options: theOptions });
    };

    var options = function (target) {
        return target.data(ggFnName).options;
    };

    var theMenuButtonID = 'Button';
    var theMenuMenuID = 'Menu';

    var createButton = function (target, Index) {
        var theOptions = options(target);

        var aMenuBtn = $('<a>', { href: 'javascript: void(0)', id: theOptions.id + theMenuButtonID + Index });
        var aMenu = $('<div>', { id: theOptions.id + theMenuMenuID + Index, width: theOptions.width });

        $.each(theOptions.menuItems, function (idx, obj) {
            $(aMenu).append(obj);
        });

        return aMenuBtn[0].outerHTML + aMenu[0].outerHTML;
    };

    var bindButton = function (target, allDataRow) {
        var theOptions = options(target);

        for (Index = 0; Index < allDataRow.length; Index++) {
            //編輯MenuBtn
            var theMenubutton = $('#' + theOptions.id + theMenuButtonID + Index).menubutton({
                menu: '#' + theOptions.id + theMenuMenuID + Index,
                text: theOptions.text,
                iconCls: theOptions.iconCls
            });

            //編輯Menu
            $('#' + theOptions.id + theMenuMenuID + Index).data(ggFnName, theMenubutton).menu({
                onClick: function (item) {                    
                    var rowIndex = parseInt($(this).data(ggFnName).closest('tr').attr('datagrid-row-index'));
                    theOptions.onClick.call(item, allDataRow[rowIndex]);
                }
            });            
        }
    };

    $.fn[ggFnName] = function (options, parameter) {
        if (typeof options == "string") {
            var method = $.fn[ggFnName].methods[options];
            if (method) return method(this, parameter);
        }
        else {
            options = options || {};
            this.each(function () {
                $(this)[ggFnName]('initialize', options);
            });
        }
    };

    $.fn[ggFnName].methods = {
        initialize: function (target, parameter) {
            target.each(function () {
                initialize($(this), parameter);
            });
            return target;
        },
        createButton: function (target,Index) {
            return createButton($(target[0]), Index);
        },
        bindButton: function (target, allDataRow) {
            return bindButton($(target[0]), allDataRow);
        }
    }

    $.fn[ggFnName].defaults = {
        id: 'MenuID',
        text: 'MenuButtonText',
        iconCls: 'icon-tip',
        width: 120,
        menuItems: [
            $('<div>').append('New'),
            $('<div>').append('Open')],
        onClick: function (dataRow) {
            //var theItem = this;
            //alert(theItem.id);
        }
    }
})(jQuery);



