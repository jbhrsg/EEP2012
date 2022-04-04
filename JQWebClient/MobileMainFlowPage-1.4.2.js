function gotoMenu() {
    window.location.href = "MobileMainPage.aspx";
}

function gotoInbox(flag, openFlowTabIndex) {
    var urlPrdfix = "";
    if (flag)
        urlPrdfix = "../";
    if (openFlowTabIndex != undefined)
        window.location.href = urlPrdfix + "MobileMainFlowPage.aspx?OpenFlowTabIndex=" + openFlowTabIndex;
    else
        window.location.href = urlPrdfix + "MobileMainFlowPage.aspx?OpenFlowTabIndex=0";
}

//$.fn.flow = function (methodName, value) {
//    if (typeof methodName == "string") {
//        var method = $.fn.flow.methods[methodName];
//        if (method) {
//            return method(this, value);
//        }
//    }
//    else if (typeof methodName == "object") {
//        this.each(function () {
//            $(this).flow('initialize');
//            if (!$(this).hasClass($.fn.flow.class)) {
//                $(this).addClass($.fn.flow.class)
//            }
//        });
//    }
//};

//$.fn.flow.class = 'info-flow';

$.extend($.fn.flow.methods, {
    initialize: function (jq) {
        //load flow
        $(jq).flow('initializeInbox');
        $(jq).flow('initializeOutbox');
        $(jq).flow('initializeNotify');
        $(jq).flow('initializeDelay');
        var titleText = $.sysmsg('getValue', 'Web/webClientMainFlow/UIText');
        var titleTexts = titleText.split(';');
        $('#titleInbox_Inbox').html(titleTexts[2]);
        $('#titleOutbox_Outbox').html(titleTexts[3]);
        $('#titleNotify_Notify').html(titleTexts[16]);
        $('#titleDelay_Delay').html(titleTexts[4]);

        $('#tbQuery_Inbox').attr('placeholder', $.fn.flow.defaults.placeholderText);

        $.ajax({
            type: "POST",
            url: 'handler/SystemHandle_Flow.ashx',
            data: { Type: "Count" },
            dataType: 'json',
            cache: false,
            async: false,
            success: function (data) {
                //$('<span>1</span>').insertAfter($('#titleInbox_Inbox'));
                $('#titleInbox_Inbox').html('<span>' + $('#titleInbox_Inbox').html() + '</span><span class="ui-li-count ui-body-inherit">' + data.Do + '</span>');
                $('#titleOutbox_Outbox').html('<span>' + $('#titleOutbox_Outbox').html() + '</span><span class="ui-li-count ui-body-inherit">' + data.History + '</span>');
                $('#titleNotify_Notify').html('<span>' + $('#titleNotify_Notify').html() + '</span><span class="ui-li-count ui-body-inherit">' + data.Notify + '</span>');
                $('#titleDelay_Delay').html('<span>' + $('#titleDelay_Delay').html() + '</span><span class="ui-li-count ui-body-inherit">' + data.Delay + '</span>');
            }
        });


        $("a.refresh").attr("title", $.fn.main.defaults.refreshText);
        $("a.logout").attr("title", $.fn.main.defaults.logoutText);
        $("a.menu .ui-btn-text").html($.fn.main.defaults.menuText);
        $("a.flow .ui-btn-text").html($.fn.main.defaults.flowText);
        if ($("a.menu .ui-btn-text").length == 0) {
            $("a.menu").html($.fn.main.defaults.menuText);
        }
        if ($("a.flow .ui-btn-text").length == 0) {
            $("a.flow").html($.fn.main.defaults.flowText);
        }

        var openFlowTabIndex = Request.getQueryStringByName("OpenFlowTabIndex");
        var index = 0;
        if (openFlowTabIndex != undefined) index = openFlowTabIndex;
        $("#tabs").tabs({
            active: index
        });
    }
});

