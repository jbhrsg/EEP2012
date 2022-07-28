<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form_ISODocument.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">

        var WhoCanDownload_BeforeManagerModify = "";
        var OrgCanDownload_BeforeManagerModify = "";
        var IsAllCanDownload_BeforeManagerModify = "";
        var WhoCanDownload1_BeforeManagerModify = "";
        var OrgCanDownload1_BeforeManagerModify = "";
        var IsAllCanDownload1_BeforeManagerModify = "";

        var JQDialog4TextBox = "";//0下載，1取號
        var JQDialog5TextBox = "";

        $(function () {
            //var DocPaperNOButton = $("<button type='button'>").attr({ 'id': 'DocPaperNOButton', 'href': '#', 'onclick': 'DocPaperNOButton_OnClick()' }).text("預覽編號");
            //$('#dataFormMasterDocPaperNO').closest('td').append('&nbsp;').append(DocPaperNOButton);

            RedFields('#dataFormMaster', ['WordFile', 'PdfFile']);

            //全公司下載_click處理函式
            $('#dataFormMasterIsAllCanDownload').click(function () {
                if ($(this).is(":checked") == true) {
                    $("#OrgCanDownloadButton").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').addClass('l-btn-disabled');
                } else if ($(this).is(":checked") == false) {
                    $("#OrgCanDownloadButton").attr("onclick", "OpenJQDialog4(0)").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "OpenJQDialog5(0)").unbind("click");
                    $('#OrgCanDownloadButton').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').removeClass('l-btn-disabled');
                }
            });

            //全公司取號_click處理函式
            $('#dataFormMasterIsAllCanDownload1').click(function () {
                if ($(this).is(":checked") == true) {
                    $("#OrgCanDownloadButton1").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton1').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').addClass('l-btn-disabled');
                } else if ($(this).is(":checked") == false) {
                    $("#OrgCanDownloadButton1").attr("onclick", "OpenJQDialog4(1)").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "OpenJQDialog5(1)").unbind("click");
                    $('#OrgCanDownloadButton1').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').removeClass('l-btn-disabled');
                }
            });


            $('#dataFormMasterOrgCanDownloadC').after($('#OrgCanDownloadButton'));
            $('#dataFormMasterWhoCanDownloadC').after($('#WhoCanDownloadButton'));

            $('#dataFormMasterOrgCanDownload1C').after($('#OrgCanDownloadButton1'));
            $('#dataFormMasterWhoCanDownload1C').after($('#WhoCanDownloadButton1'));
        });
        function FirstNO_OnSelect(row) {
            $("#dataFormMasterSecondNO").combobox("setValue", "");
            $("#dataFormMasterSecondNO").combobox("setWhere", "FirstNO='" + row.FirstNO + "'");
            setTimeout(function () {
                var data = $("#dataFormMasterSecondNO").combobox("getData");
                if (data.length > 0) {
                    RedFields('#dataFormMaster', ['SecondNO']);
                } else {
                    BlackFields('#dataFormMaster', ['SecondNO']);
                }
            }, 1000);
        }
        //沒用到(預覽編號)
        function DocPaperNOButton_OnClick() {
            var FirstNO = $("#dataFormMasterFirstNO").combobox("getValue");
            var SecondNO = $("#dataFormMasterSecondNO").combobox("getValue");
            var DocPropertyNO = $("#dataFormMasterDocPropertyNO").combobox("getValue");
            var DocName = $("#dataFormMasterDocName").val();
            if (FirstNO !== '' && DocPropertyNO != '' && DocName != '') {
                if (SecondNO == "") {
                    SecondNO = FirstNO;
                }

                $("#dataFormMasterDocPaperNO").val(FirstNO + SecondNO + '-' + DocPropertyNO + GetISOCode(FirstNO + SecondNO + '-' + DocPropertyNO) + '-' + DocName);

            } else {
                alert("請先填「主編碼」、「文件屬性」、「文件名稱」");
            }
        }
        //沒用到(預覽編號)
        function GetISOCode(ISOCodePrefix) {
            var returnValue = "有誤";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                data: "mode=method&method=" + "GetISOCode&parameters=" + ISOCodePrefix, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "0") {
                        returnValue = data;
                    } else {
                        alert("產生編號有誤，請洽管理室");
                    }
                }
            });

            return returnValue;
        }

        function dataFormMaster_Onload() {
            if ($("#dataFormMasterIsModify").val() == 'n') {
                $("#JQDialog1").panel('setTitle', '文件首次申請單');
                $("#dataFormMasterReason").closest("td").prev("td").html("文件制訂申請說明");
            } else if ($("#dataFormMasterIsModify").val() == 'y') {
                $("#JQDialog1").panel('setTitle', '文件修訂申請單');
                $("#dataFormMasterReason").closest("td").prev("td").html("文件修訂申請說明");
            }

            var parameter = Request.getQueryStringByName2("p1");//沒加密
            //mode = Request.getQueryStringByName2("NAVIGATOR_MODE");
            if (parameter == "") {
                parameter = Request.getQueryStringByName("p1");//有加密
                //mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            }

            //只有申請時才會顯示
            $("#DocPaperNOButton").hide();

            //隱藏欄位顯示
            var ShowMasterFields = ['PdfFile','WordFile','WhoCanDownload','OrgCanDownload','IsAllCanDownload'];//
            ShowFields('#dataFormMaster', ShowMasterFields);

            //停用欄位恢復
            var EnabledFieldArr = ["DocName","Reason"];
            var EnabledComboboxArr = ["FirstNO", "SecondNO", "DocPropertyNO"];
            var EnabledRefvalArr = [];
            EnableFields("#dataFormMaster", EnabledFieldArr, EnabledComboboxArr, EnabledRefvalArr);

            //掃描檔、電子檔停用，只有上傳才啟用
            $('.info-fileUpload-file[name="PdfFilefile"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="PdfFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('input[name="PdfFile"]').attr('disabled', true);
            $('.info-fileUpload-file[name="WordFilefile"]').attr('disabled', true);//選擇檔案
            $('.info-fileUpload-file[name="WordFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            $('input[name="WordFile"]').attr('disabled', true);

            //alert(parameter);
            
            if (parameter == "apply" && getEditMode($("#dataFormMaster")) != 'viewed') {
                var HiddenMasterFields = ['PdfFile', 'WordFile', 'WhoCanDownloadC', 'OrgCanDownloadC', 'IsAllCanDownload', 'WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                HideFields('#dataFormMaster', HiddenMasterFields);
                if ($("#dataFormMasterIsModify").val() == 'n') {
                    $("#DocPaperNOButton").show();
                } else if ($("#dataFormMasterIsModify").val() == 'y') {
                    var DisabledFieldArr = ['DocName'];
                    var DisabledComboboxArr = ["FirstNO", "SecondNO", "DocPropertyNO"];
                    var DisabledRefvalArr = [];
                    DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                }

                //$('#dataFormMasterDocNO').closest('td').append('&nbsp;').append('註：「ISO文件編號」以存檔後的為準');

            } else if (parameter == "a1") {
                var HiddenMasterFields = ['PdfFile', 'WordFile', 'WhoCanDownloadC', 'OrgCanDownloadC', 'IsAllCanDownload', 'WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                HideFields('#dataFormMaster', HiddenMasterFields);

                var DisabledFieldArr = ['DocName','Reason'];
                var DisabledComboboxArr = ["FirstNO", "SecondNO", "DocPropertyNO"];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            }else if(parameter=="upload"){
                $('.info-fileUpload-file[name="PdfFilefile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="PdfFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="PdfFile"]').attr('disabled', false);
                $('.info-fileUpload-file[name="WordFilefile"]').attr('disabled', false);//選擇檔案
                $('.info-fileUpload-file[name="WordFilefile"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-enabled');//上傳
                $('input[name="WordFile"]').attr('disabled', false);

                var DisabledFieldArr = ['DocName', "Reason"];
                var DisabledComboboxArr = ["FirstNO", "SecondNO", "DocPropertyNO"];
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);

                //選全公司下載要停用部門下載和個人下載(考慮到退回的呈現)
                if ($("#dataFormMasterIsAllCanDownload").is(":checked") == true) {
                    $("#OrgCanDownloadButton").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').addClass('l-btn-disabled');
                } else if ($("#dataFormMasterIsAllCanDownload").is(":checked") == false) {
                    $("#OrgCanDownloadButton").attr("onclick", "OpenJQDialog4(0)").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "OpenJQDialog5(0)").unbind("click");
                    $('#OrgCanDownloadButton').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').removeClass('l-btn-disabled');
                }
                //選全公司取號
                if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == true) {
                    $("#OrgCanDownloadButton1").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton1').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').addClass('l-btn-disabled');
                } else if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == false) {
                    $("#OrgCanDownloadButton1").attr("onclick", "OpenJQDialog4(1)").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "OpenJQDialog5(1)").unbind("click");
                    $('#OrgCanDownloadButton1').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').removeClass('l-btn-disabled');
                }

                //文件屬性是表單
                if ($("#dataFormMasterDocPropertyNO").combobox("getValue") == "F") {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    ShowFields('#dataFormMaster', HiddenMasterFields);
                } else {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    HideFields('#dataFormMaster', HiddenMasterFields);
                }
                //顯示部門組或個人代碼的中文
                DisplayChineseName();
            } else if (parameter == "a2") {
                WhoCanDownload_BeforeManagerModify = $("#dataFormMasterWhoCanDownload").val().split(',');
                OrgCanDownload_BeforeManagerModify = $("#dataFormMasterOrgCanDownload").val().split(',');
                IsAllCanDownload_BeforeManagerModify = $("#dataFormMasterIsAllCanDownload").checkbox("getValue");
                WhoCanDownload1_BeforeManagerModify = $("#dataFormMasterWhoCanDownload1").val().split(',');
                OrgCanDownload1_BeforeManagerModify = $("#dataFormMasterOrgCanDownload1").val().split(',');
                IsAllCanDownload1_BeforeManagerModify = $("#dataFormMasterIsAllCanDownload1").checkbox("getValue");

                var DisabledFieldArr = ['DocName', 'Reason'];//, 'ISAllCanDownload'
                var DisabledComboboxArr = ["FirstNO", "SecondNO", "DocPropertyNO"];//, 'WhoCanDownload', 'OrgCanDownload'
                var DisabledRefvalArr = [];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
                //$("#dataFormMasterIsAllCanDownload").attr("disabled", true); //唯獨

                if ($("#dataFormMasterIsAllCanDownload").is(":checked") == true) {
                    $("#OrgCanDownloadButton").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').addClass('l-btn-disabled');
                } else if ($("#dataFormMasterIsAllCanDownload").is(":checked") == false) {
                    $("#OrgCanDownloadButton").attr("onclick", "OpenJQDialog4(0)").unbind("click");
                    $("#WhoCanDownloadButton").attr("onclick", "OpenJQDialog5(0)").unbind("click");
                    $('#OrgCanDownloadButton').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton').removeClass('l-btn-disabled');
                }

                if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == true) {
                    $("#OrgCanDownloadButton1").attr("onclick", "").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "").unbind("click");
                    $('#OrgCanDownloadButton1').addClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').addClass('l-btn-disabled');
                } else if ($("#dataFormMasterIsAllCanDownload1").is(":checked") == false) {
                    $("#OrgCanDownloadButton1").attr("onclick", "OpenJQDialog4(1)").unbind("click");
                    $("#WhoCanDownloadButton1").attr("onclick", "OpenJQDialog5(1)").unbind("click");
                    $('#OrgCanDownloadButton1').removeClass('l-btn-disabled');
                    $('#WhoCanDownloadButton1').removeClass('l-btn-disabled');
                }

                //文件屬性是表單
                if ($("#dataFormMasterDocPropertyNO").combobox("getValue") == "F") {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    ShowFields('#dataFormMaster', HiddenMasterFields);
                } else {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    HideFields('#dataFormMaster', HiddenMasterFields);
                }

                //顯示部門組或個人代碼的中文
                DisplayChineseName();
            }else if(getEditMode($("#dataFormMaster")) == 'viewed'){
                //顯示部門組或個人代碼的中文
                DisplayChineseName();

                //文件屬性是表單
                if ($("#dataFormMasterDocPropertyNO").combobox("getValue") == "F") {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    ShowFields('#dataFormMaster', HiddenMasterFields);
                } else {
                    var HiddenMasterFields = ['WhoCanDownload1C', 'OrgCanDownload1C', 'IsAllCanDownload1'];
                    HideFields('#dataFormMaster', HiddenMasterFields);
                }
            }


            $("#downloadPdfFile").remove();
            var PdfFile = $('.info-fileUpload-value', $("#dataFormMasterPdfFile").next()).val();
            if (PdfFile != '') {
                var link = $("<a download>").attr({ 'id': 'downloadPdfFile', 'href': '../JB_ADMIN/Form_ISODocument/PdfFile/' + PdfFile }).html('下載');
                $('#dataFormMasterPdfFile').closest('td').append(link);
            }

            $("#downloadWordFile").remove();
            var WordFile = $('.info-fileUpload-value', $("#dataFormMasterWordFile").next()).val();
            if (WordFile != '') {
                var link = $("<a download>").attr({ 'id': 'downloadWordFile', 'href': '../JB_ADMIN/Form_ISODocument/WordFile/' + WordFile }).html('下載');
                $('#dataFormMasterWordFile').closest('td').append(link);
            }

            //var DisabledFieldArr = ["WhoCanDownload", "OrgCanDownload"];
            //    var DisabledComboboxArr = [];
            //    var DisabledRefvalArr = [];
            //    if ($("#dataFormMasterIsAllCanDownload").checkbox("getValue") == "1") {
            //        DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            //    } else{
            //        EnableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            //    }
        }

        function DisplayChineseName() {
            //顯示中文名稱
            if ($("#dataFormMasterOrgCanDownload").val() != "") {
                //轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnOrgNames&parameters=" + $("#dataFormMasterOrgCanDownload").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            $("#dataFormMasterOrgCanDownloadC").val(data);
                        }
                    }
                });
            }

            //顯示中文名稱
            if ($("#dataFormMasterOrgCanDownload1").val() != "") {
                //轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnOrgNames&parameters=" + $("#dataFormMasterOrgCanDownload1").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            $("#dataFormMasterOrgCanDownload1C").val(data);
                        }
                    }
                });
            }

            //顯示中文名稱
            if ($("#dataFormMasterWhoCanDownload").val() != "") {
                //轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnUserNames&parameters=" + $("#dataFormMasterWhoCanDownload").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            $("#dataFormMasterWhoCanDownloadC").val(data);
                        }
                    }
                });
            }

            //顯示中文名稱
            if ($("#dataFormMasterWhoCanDownload1").val() != "") {
                //轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnUserNames&parameters=" + $("#dataFormMasterWhoCanDownload1").val(), //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            $("#dataFormMasterWhoCanDownload1C").val(data);
                        }
                    }
                });
            }
        }

        function dataFormMaster_OnApply() {
            var parameter = Request.getQueryStringByName2("p1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("p1");//有加密
            }

            if(parameter =="apply" && getEditMode($("#dataFormMaster")) != 'viewed'){
                var data = $("#dataFormMasterSecondNO").combobox("getData");
                if (data.length > 0 && $("#dataFormMasterSecondNO").combobox("getValue")=="") {
                    alert("次編碼為必填");
                    return false;
                }
            } else if (parameter == "upload") {
                var arr = [];
                if ($('.info-fileUpload-value', $("#dataFormMasterPdfFile").next()).val() == '') {
                    arr.push("「掃描檔」");
                }
                if ($('.info-fileUpload-value', $("#dataFormMasterWordFile").next()).val() == '') {
                    arr.push("「電子檔」");
                }
                if (arr.length > 1) {
                    alert(arr.join("、") + "為必填");
                    return false;
                } else if (arr.length == 1) {
                    alert(arr.join("") + "為必填");
                    return false;
                }

                if ($("#dataFormMasterIsAllCanDownload").checkbox("getValue") == "1") {
                    $("#dataFormMasterWhoCanDownload").val("");
                    $("#dataFormMasterOrgCanDownload").val("");
                    $("#dataFormMasterWhoCanDownloadC").val("");
                    $("#dataFormMasterOrgCanDownloadC").val("");
                }
                if ($("#dataFormMasterIsAllCanDownload1").checkbox("getValue") == "1") {
                    $("#dataFormMasterWhoCanDownload1").val("");
                    $("#dataFormMasterOrgCanDownload1").val("");
                    $("#dataFormMasterWhoCanDownload1C").val("");
                    $("#dataFormMasterOrgCanDownload1C").val("");
                }
                //alert($("#dataFormMasterIsAllCanDownload").checkbox("getValue"));
                //下載三欄位沒填，就提醒
                if ($("#dataFormMasterIsAllCanDownload").checkbox("getValue") == "" && $("#dataFormMasterWhoCanDownload").val() == "" && $("#dataFormMasterOrgCanDownload").val() == "") {
                    alert("「全公司下載」、「部門組下載」、「個人下載」皆沒填，會導致「掃描文件」、「電子原稿」無法下載");
                }
                //屬性表單，取號三欄位沒填，就提醒
                if ($("#dataFormMasterDocPropertyNO").combobox("getValue") == "F" && $("#dataFormMasterIsAllCanDownload1").checkbox("getValue") == "" && $("#dataFormMasterWhoCanDownload1").val() == "" && $("#dataFormMasterOrgCanDownload1").val() == "") {
                    alert("「全公司取號」、「部門組取號」、「個人取號」皆沒填，會導致此表單無法取號");
                }
            } else if (parameter == "a2") {

                if ($("#dataFormMasterIsAllCanDownload").checkbox("getValue") == "1") {
                    $("#dataFormMasterWhoCanDownload").val("");
                    $("#dataFormMasterOrgCanDownload").val("");
                    $("#dataFormMasterWhoCanDownloadC").val("");
                    $("#dataFormMasterOrgCanDownloadC").val("");
                }
                if ($("#dataFormMasterIsAllCanDownload1").checkbox("getValue") == "1") {
                    $("#dataFormMasterWhoCanDownload1").val("");
                    $("#dataFormMasterOrgCanDownload1").val("");
                    $("#dataFormMasterWhoCanDownload1C").val("");
                    $("#dataFormMasterOrgCanDownload1C").val("");
                }
                //設定ManagerIsModify
                var WhoCanDownload = $("#dataFormMasterWhoCanDownload").val().split(',');//click之前array只有一個元素，之後就變成看選多少個
                var OrgCanDownload = $("#dataFormMasterOrgCanDownload").val().split(',');
                var IsAllCanDownload = $("#dataFormMasterIsAllCanDownload").checkbox("getValue");
                var WhoCanDownload1 = $("#dataFormMasterWhoCanDownload1").val().split(',');//click之前array只有一個元素，之後就變成看選多少個
                var OrgCanDownload1 = $("#dataFormMasterOrgCanDownload1").val().split(',');
                var IsAllCanDownload1 = $("#dataFormMasterIsAllCanDownload1").checkbox("getValue");

                
                if (!arraysEqual(WhoCanDownload_BeforeManagerModify, WhoCanDownload) || !arraysEqual(OrgCanDownload_BeforeManagerModify, OrgCanDownload) || IsAllCanDownload_BeforeManagerModify != IsAllCanDownload || !arraysEqual(WhoCanDownload1_BeforeManagerModify, WhoCanDownload1) || !arraysEqual(OrgCanDownload1_BeforeManagerModify, OrgCanDownload1) || IsAllCanDownload1_BeforeManagerModify != IsAllCanDownload1) {
                    //alert("有異動");
                    $("#dataFormMasterManagerIsModify").val(1);
                } else {
                    //alert("無異動");
                    $("#dataFormMasterManagerIsModify").val(0);
                }
            }
        }

        //沒用到(設定ManagerIsModify(改到去cs設定))
        function arraysEqual(a, b) {
            if (a === b) return true;
            if (a == null || b == null) return false;
            if (a.length !== b.length) return false;

            for (var i = 0; i < a.length; ++i) {
                if (a[i] !== b[i]) return false;
            }
            return true;
        }

        function dataFormMaster_OnApplied() {
            var DocNO = $("#dataFormMasterDocNO").val();
            
            var returnValue = "錯誤";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                data: "mode=method&method=" + "GetDocPaperNO&parameters=" + DocNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data != "0") {
                        returnValue = data;
                    } else {
                        alert("產生編號有誤，請洽管理室");
                    }
                }
            });
            $("#dataFormMasterDocPaperNO").val(returnValue);
        }

        function OpenJQDialog4(i) {
            var nodes = $('#TreeOrgCanDownload').tree('getChecked');
            $.each(nodes, function (index, node) {
                $('#TreeOrgCanDownload').tree('uncheck', node.target);
                $(node.target).removeClass('.tree-node-clicked');
            });
            var root = $('#TreeOrgCanDownload').tree('getRoot');
            $('#TreeOrgCanDownload').tree('uncheck', root.target);

            var OrgCanDownload = "";
            if (i == 0) {
                OrgCanDownload = $("#dataFormMasterOrgCanDownload").val();
                JQDialog4TextBox = 0;
            } else if (i == 1) {
                OrgCanDownload = $("#dataFormMasterOrgCanDownload1").val();
                JQDialog4TextBox=1;
            }
            TreeOrgCanDownloadSetChecked(OrgCanDownload);

            openForm('#JQDialog4', {}, "", 'dialog');

            //
            //$("#CBUser").combobox('setValue', '');
            //$("#CBOrgL2").combobox('setValue', '');
        }
        function TreeOrgCanDownloadSetChecked(IDstr) {
            if (IDstr != '') {
                IDstrArr = IDstr.split(",");
                $.each(IDstrArr, function (i, v) {
                    //alert(i + ',' + v);
                    var node = $('#TreeOrgCanDownload').tree('find', v);
                    if (node != null) {
                        $(node.target).addClass('tree-node-clicked');
                        $('#TreeOrgCanDownload').tree('check', node.target);
                    }
                });
            }
        }

        function OpenJQDialog5(i) {
            //清除查詢並查詢 
            //ClearBtn_OnClick();
            //$("#TreeWhoCanDownload").tree("setWhere", "1=1");

            //清除勾選
            var nodes = $('#TreeWhoCanDownload').tree('getChecked');
            $.each(nodes, function (index, node) {
                $('#TreeWhoCanDownload').tree('uncheck', node.target);
                $(node.target).removeClass('.tree-node-clicked');
            });
            var root = $('#TreeWhoCanDownload').tree('getRoot');
            $('#TreeWhoCanDownload').tree('uncheck', root.target);

            var WhoCanDownload = "";
            if (i == 0) {
                WhoCanDownload = $("#dataFormMasterWhoCanDownload").val();
                JQDialog5TextBox=0;
            } else if (i == 1) {
                WhoCanDownload = $("#dataFormMasterWhoCanDownload1").val();
                JQDialog5TextBox=1;
            }

            //樹勾選
            TreeWhoCanDownloadSetChecked(WhoCanDownload);
            
            //開啟
            openForm('#JQDialog5', {}, "", 'dialog');

        }

        function TreeWhoCanDownloadSetChecked(IDstr) {
            if (IDstr != '' && IDstr!=undefined) {
                IDstrArr = IDstr.split(",");
                $.each(IDstrArr, function (i, v) {
                    //alert(i + ',' + v);
                    var node = $('#TreeWhoCanDownload').tree('find', v);
                    if (node != null) {
                        $(node.target).addClass('tree-node-clicked');
                        $('#TreeWhoCanDownload').tree('check', node.target);
                    }
                });
            }
        }

        function JQDialog4OnSubmited() {
            var nodes = $('#TreeOrgCanDownload').tree('getChecked');
            var SalesTypeIDs = '';
            var i = 1;
            $.each(nodes, function (index, node) {
                if (i > 1) {
                    SalesTypeIDs = SalesTypeIDs + ',' + node.id;
                }
                else {
                    SalesTypeIDs = SalesTypeIDs + node.id;
                }
                i = i + 1;
            });
            //SaveSalesSaleType(SalesTypeIDs);
            //$("#dataGrid1").datagrid('reload');
            //return true;

            var OrgNames = "";
            if (SalesTypeIDs != "") {
                //SalesTypeIDs轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnOrgNames&parameters=" + SalesTypeIDs, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            OrgNames = data;
                        } else {
                            alert("錯誤，請洽管理室");
                        }
                    }
                });
            }

            if (JQDialog4TextBox == 0) {
                $("#dataFormMasterOrgCanDownload").val(SalesTypeIDs);
                $("#dataFormMasterOrgCanDownloadC").val(OrgNames);
            } else if (JQDialog4TextBox == 1) {
                $("#dataFormMasterOrgCanDownload1").val(SalesTypeIDs);
                $("#dataFormMasterOrgCanDownload1C").val(OrgNames);
            }
            closeForm('#JQDialog4');
        }
            
        function JQDialog5OnSubmited() {
            var nodes = $('#TreeWhoCanDownload').tree('getChecked');
            var SalesTypeIDs = '';
            var i = 1;
            $.each(nodes, function (index, node) {
                if (i > 1) {
                    SalesTypeIDs = SalesTypeIDs + ',' + node.id;
                }
                else {
                    SalesTypeIDs = SalesTypeIDs + node.id;
                }
                i = i + 1;
            });
            
            var UserNames = "";
            if (SalesTypeIDs != "") {
                //SalesTypeIDs轉中文名稱
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sForm_ISODocument.ISODocument',
                    data: "mode=method&method=" + "ReturnUserNames&parameters=" + SalesTypeIDs, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data.length > 0) {
                            UserNames = data;
                        } else {
                            alert("錯誤，請洽管理室");
                        }
                    }
                });
            }

            if (JQDialog5TextBox == 0) {
                $("#dataFormMasterWhoCanDownload").val(SalesTypeIDs);
                $("#dataFormMasterWhoCanDownloadC").val(UserNames);
            } else if (JQDialog5TextBox == 1) {
                $("#dataFormMasterWhoCanDownload1").val(SalesTypeIDs);
                $("#dataFormMasterWhoCanDownload1C").val(UserNames);
            }
            closeForm('#JQDialog5');
        }
        function QueryBtn_OnClick() {
            var userid = $("#CBUser").combobox('getValue');
            var org = $("#CBOrgL2").combobox('getValue');

            if (userid == '' && org=='') {
                $("#TreeWhoCanDownload").tree("setWhere", "1=1");
            } else {
                var arr = [];
                if (userid != '') {
                    arr.push("USERID='" + userid + "'");
                }
                if (org != '') {
                    arr.push("ORGNOL2='" + org + "'");
                }
                $("#TreeWhoCanDownload").tree("setWhere", arr.join(" and "));


            }
        }
        function ClearBtn_OnClick() {
            $("#CBUser").combobox('setValue', '');
            $("#CBOrgL2").combobox('setValue', '');
        }

        //----------工具-------------
        function HideFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
            });
        }
        function ShowFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames, DisabledRefvalNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
            $.each(DisabledRefvalNames, function (index, value) {
                $(FormName + value).refval('disable');
            });
        }
        function EnableFields(FormName, EnabledFieldNames, EnabledComboboxNames, EnabledRefvalNames) {
            $.each(EnabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', false);
            });
            $.each(EnabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('enable');
            });
            $.each(EnabledRefvalNames, function (index, value) {
                $(FormName + value).refval('enable');
            });
        }
        function RedFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' })
            });
        }
        function BlackFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'black' })
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sForm_ISODocument.ISODocument" runat="server" AutoApply="True"
                DataMember="ISODocument" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AlwaysClose="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="DocNO" Editor="text" FieldName="DocNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DocPaperNO" Editor="text" FieldName="DocPaperNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FirstNO" Editor="text" FieldName="FirstNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SecondNO" Editor="text" FieldName="SecondNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DocName" Editor="text" FieldName="DocName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PdfFile" Editor="text" FieldName="PdfFile" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WordFile" Editor="text" FieldName="WordFile" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WhoCanDownload" Editor="text" FieldName="WhoCanDownload" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="登錄文件申請單" Width="880px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ISODocument" HorizontalColumnsCount="2" RemoteName="sForm_ISODocument.ISODocument" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_Onload" OnApply="dataFormMaster_OnApply" OnApplied="dataFormMaster_OnApplied" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請文號" Editor="text" FieldName="DocNO" Format="" maxlength="0" Width="180" ReadOnly="True" Span="2" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ISO文編" Editor="text" FieldName="DocPaperNO" Format="" maxlength="0" Width="500" ReadOnly="True" NewRow="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主編碼" Editor="infocombobox" FieldName="FirstNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'FirstNO',textField:'FirstName',remoteName:'sForm_ISODocument.ISOFirstNO',tableName:'ISOFirstNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:FirstNO_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="次編碼" Editor="infocombobox" FieldName="SecondNO" Format="" maxlength="0" Width="180" EditorOptions="valueField:'SecondNO',textField:'SecondName',remoteName:'sForm_ISODocument.ISOSecondNO',tableName:'ISOSecondNO',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件屬性" Editor="infocombobox" FieldName="DocPropertyNO" maxlength="0" Width="180" EditorOptions="valueField:'DocPropertyNO',textField:'DocPropertyName',remoteName:'sForm_ISODocument.ISODocProperty',tableName:'ISODocProperty',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件名稱" Editor="text" FieldName="DocName" Format="" maxlength="0" Width="180" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件制(修)訂申請說明" Editor="textarea" FieldName="Reason" MaxLength="0" Width="500" ReadOnly="False" EditorOptions="height:50" NewRow="False" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預定發行日期" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="110" ReadOnly="False" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="掃描文件" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/PdfFile',showButton:true,showLocalFile:false,fileSizeLimited:'100000'" FieldName="PdfFile" Format="" maxlength="0" Width="300" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="電子原稿" Editor="infofileupload" FieldName="WordFile" MaxLength="0" Width="300" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/Form_ISODocument/WordFile',showButton:true,showLocalFile:false,fileSizeLimited:'100000'" Format="" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司下載" Editor="checkbox" FieldName="IsAllCanDownload" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組下載" Editor="text" EditorOptions="panelWidth:350,valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sForm_ISODocument.OrgCanDownload',tableName:'OrgCanDownload',valueFieldCaption:'號碼',textFieldCaption:'名稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="OrgCanDownload" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="False" Width="400" PlaceHolder="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人下載" Editor="text" FieldName="WhoCanDownload" MaxLength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="2" Visible="False" Width="400" EditorOptions="panelWidth:600,valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocument.WhoCanDownload',tableName:'WhoCanDownload',valueFieldCaption:'工號',textFieldCaption:'姓名',selectOnly:false,checkData:false,columns:[{field:'USERID',title:'工號',width:80,align:'left',sortable:true},{field:'USERNAME',title:'姓名',width:80,align:'left',sortable:true}],cacheRelationText:false,multiple:true" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組下載" Editor="text" FieldName="OrgCanDownloadC" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="400" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人下載" Editor="text" FieldName="WhoCanDownloadC" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="400" />
                        <JQTools:JQFormColumn Alignment="left" Caption="全公司取號" Editor="checkbox" FieldName="IsAllCanDownload1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1" maxlength="0" ReadOnly="True" Span="2" Visible="False" Width="400" EditorOptions="panelWidth:350,valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sForm_ISODocument.OrgCanDownload',tableName:'OrgCanDownload',valueFieldCaption:'號碼',textFieldCaption:'名稱',selectOnly:false,checkData:false,columns:[],cacheRelationText:false,multiple:true" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人取號" Editor="text" EditorOptions="panelWidth:600,valueField:'USERID',textField:'USERNAME',remoteName:'sForm_ISODocument.WhoCanDownload',tableName:'WhoCanDownload',valueFieldCaption:'工號',textFieldCaption:'姓名',selectOnly:false,checkData:false,columns:[{field:'USERID',title:'工號',width:80,align:'left',sortable:true},{field:'USERNAME',title:'姓名',width:80,align:'left',sortable:true}],cacheRelationText:false,multiple:true" FieldName="WhoCanDownload1" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="False" Width="400" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門組取號" Editor="text" FieldName="OrgCanDownload1C" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="400" />
                        <JQTools:JQFormColumn Alignment="left" Caption="個人取號" Editor="text" FieldName="WhoCanDownload1C" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="400" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsModify" Editor="text" FieldName="IsModify" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ManagerIsModify" Editor="text" FieldName="ManagerIsModify" maxlength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQButton ID="OrgCanDownloadButton" runat="server" Icon="icon-view" OnClick="OpenJQDialog4(0)" Text="下載權限" />
                <JQTools:JQButton ID="WhoCanDownloadButton" runat="server" Icon="icon-view" OnClick="OpenJQDialog5(0)" Text="下載權限" />
                <JQTools:JQButton ID="OrgCanDownloadButton1" runat="server" Icon="icon-view" OnClick="OpenJQDialog4(1)" Text="取號權限" />
                <JQTools:JQButton ID="WhoCanDownloadButton1" runat="server" Icon="icon-view" OnClick="OpenJQDialog5(1)" Text="取號權限" />
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="DocNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="n" FieldName="IsModify" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="DocPaperNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="FirstNO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DocPropertyNO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DocName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CreateDate" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="260px" DialogTop="0px" Title="部門組別" Width="450px" OnSubmited="JQDialog4OnSubmited" ><%--OnSubmited="SalesTypeOnSubmited"--%>
                  <div class="easyui-layout" style="height: 480px;">
                      
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="TreeOrgCanDownload" runat="server" DataMember="OrgCanDownload" idField="ORG_NO" parentField="UPPER_ORG" RemoteName="sForm_ISODocumentQuery.OrgCanDownload" textField="ORG_DESC" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>

            <JQTools:JQDialog ID="JQDialog5" runat="server" DialogLeft="260px" DialogTop="0px" Title="個人" Width="450px" OnSubmited="JQDialog5OnSubmited" ><%--OnSubmited="SalesTypeOnSubmited"--%>
                  <div class="easyui-layout" style="height: 480px;">
                      
                    <div data-options="region:'center',title:'',border:false" title="">
                        <JQTools:JQComboBox ID="CBOrgL2" runat="server" DisplayMember="ORG_DESC" RemoteName="sForm_ISODocument.SYS_ORGL2" ValueMember="ORG_NO" Width="80px">
                        </JQTools:JQComboBox>
                        <JQTools:JQComboBox ID="CBUser" runat="server" RemoteName="sForm_ISODocument.USERS" DisplayMember="USERNAME" ValueMember="USERID" Width="50px">
                        </JQTools:JQComboBox>
                        <JQTools:JQButton ID="QueryBtn" runat="server" Text="查詢" Icon="icon-search" OnClick="QueryBtn_OnClick" />
                        <JQTools:JQButton ID="ClearBtn" runat="server" Text="清除" Icon="icon-remove" OnClick="ClearBtn_OnClick" />
                 <JQTools:JQTreeView ID="TreeWhoCanDownload" runat="server" DataMember="USERS" idField="USERID" parentField="ParentID" RemoteName="sForm_ISODocument.USERS" textField="USERNAME" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
       </JQTools:JQDialog>
        </div>


       


    </form>
</body>
</html>
