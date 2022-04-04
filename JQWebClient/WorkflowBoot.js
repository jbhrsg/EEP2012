var scripts = [
    { url: "js/jquery.infolight-wf.js" },
    { url: "InnerPages/FormApprove.js" },
    { url: "InnerPages/FormApproveAll.js" },
    { url: "InnerPages/FormNotify.js" },
    { url: "InnerPages/FormPlusApprove.js" },
    { url: "InnerPages/FormHasten.js" },
    { url: "InnerPages/FormReturn.js" },
    { url: "InnerPages/FormReturnAll.js" },
    { url: "InnerPages/FormSubmit.js" },
    { url: "InnerPages/FormComment.js" },
    { url: "InnerPages/FormFileUpload.js" },
    { url: "InnerPages/FormFlowQuery.js" },
    { url: "InnerPages/OvertimeQuery.js" }
];

var styles = [
    //{ url: "Libs/Sysmex/themes/shared.css" }
];

var path = window.location.pathname;

path = path.replace(/\/jqwebclient/i, "");
path = path.substring(1, path.length);
if (path.indexOf("/") != -1) {
    isSubPath = true;
}
else {
    path = "/";
    isSubPath = false;
}

function loadCss(url, pos, async) {
    if (isSubPath) {
        url = "../" + url;
    }
    document.write(['<link rel="stylesheet" href="', url, '" type="text/css">', '</link>'].join(""));
}

for (var idx = 0; styles[idx]; idx++) {
    styles[idx].url && loadCss(styles[idx].url, "head", false);
}

function loadJs(url, pos, async) {
    if (isSubPath) {
        url = "../" + url;
    }
    document.write(['<script src="', url, '" type="text/javascript">', '</script>'].join(""));
}

for (var idx = 0; scripts[idx]; idx++) {
    scripts[idx].url && loadJs(scripts[idx].url, "head", false);
}
$(function () {
    $("<div id=\"winSubmit\" />").appendTo(document.body);
    $("<div id=\"winApprove\" />").appendTo(document.body);
    $("<div id=\"winReturn\" />").appendTo(document.body);
    $("<div id=\"winPlusApprove\" />").appendTo(document.body);
    $("<div id=\"winComment\" />").appendTo(document.body);
    $("<div id=\"winNotify\" />").appendTo(document.body);
    $("<div id=\"winPlus\" />").appendTo(document.body);
    $("<div id=\"winHasten\" />").appendTo(document.body);
    $("<div id=\"winFileUpload\" />").appendTo(document.body);
    $("<div id=\"winFlowQuery\" />").appendTo(document.body);

    $.sysmsg('getValues', [
        'Srvtools/AnyQuery/DeleteSure'
        , 'FLClientControls/FLNavigator/NavText'
        , 'FLClientControls/FLNavigator/FlowRejectConfirm'
        , 'FLClientControls/FLNavigator/FlowPauseConfirm'
    ]);
    DeleteSure = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowDeleteConfirm');
    var NavText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/NavText');
    var NavTexts = '';
    if (NavText) {
        NavTexts = NavText.split(";");
    }
    flowDeleteText = '';
    if (DeleteSure) {
        flowDeleteText = String.format(DeleteSure, NavTexts[20]);
    }
    flowRejectText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowRejectConfirm');
    flowPauseText = $.sysmsg('getValue', 'FLClientControls/FLNavigator/FlowPauseConfirm');
});

var DeleteSure = '';
var flowDeleteText = '';
var flowRejectText = '';
var flowPauseText = ''; 