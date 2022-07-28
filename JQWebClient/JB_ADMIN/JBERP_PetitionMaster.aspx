<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_PetitionMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js">    </script>   
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        })

        //期限日期依申請日自動加三年
        function getAddYear(AddDate, AddYear) {
            //alert("AddDate:" + AddDate);
            //var d1 = new Date();
            var d2 = new Date(AddDate);
            d2.setFullYear(d2.getFullYear() + AddYear);
            d2.setDate(d2.getDate() - 1);
            var year = d2.getFullYear();
            var month = d2.getMonth() + 1;
            if (month < 10)
                month = "0" + month;

            var days = d2.getDate();
            if (days < 10)
                days = "0" + days;
            return (year + "/" + month + "/" + days);
        }

        var AddDetailform = 0;//明細新增筆數AutoKey欄位預設值       
        var _LogUserId = "";//登入使用者
        var UserNmae = "";//登入使用者姓名
        var FlomParameters = "";//表單參數
        var DispPlusApprove = true;
        var dispcnt = 0;
        var DetailAttachment = true;

        function DataformLoadSucess() {            
            //表頭設定名稱
            $('#JQDialog1').dialog('setTitle', '簽呈申請');
            _LogUserId = getClientInfo("UserID");
            FlomParameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            UserNmae = getClientInfo('_username');
            var mode1 = getEditMode($("#dataFormMaster"));
            //alert("FlomParameters:" + FlomParameters + " mode1:" + mode1);
            //期限日期依申請日自動加一1年
            if (mode1 == "inserted") {
                var dt = new Date();
                var expdate = getAddYear(dt, 1);                
                $("#dataFormMasterExpirationDate").datebox('setValue', $.jbjob.Date.DateFormat(new Date(expdate), 'yyyy/MM/dd'));
            }
            else {
                $("#dataFormMasterExpirationDate").datebox('disable', true);
            }

            //顯示加簽者意見
            var FlowListid = $("#dataFormMasterFlowListid").val();
            if (FlowListid == '' || mode1 == "inserted" || FlomParameters == "Mang" || (FlomParameters == "Apply" && mode1 == "updated")) {
                $("#dgpanel").remove();
            }

            //退回原內容
            msterDescription = $("#dataFormMasterDescription").val();
            msterSummary = $("#dataFormMasterSummary").val();
            //取得表單編號Listid存入主檔
            if (FlowListid == "") {//&& FlomParameters == "Applicant"
                GetFlowListID();
            }

            //限閱及密件讀取名單預設申請人
            if (FlomParameters == "" && mode == 0) {
                $("#dataFormMasterReadDataEmpID").combogrid('setValue', _LogUserId + '-' + UserNmae);
            }

            //顯示結案狀態及文件查詢名單
            if ((FlomParameters != "Close" && mode1 != 'viewed') || (FlomParameters != "" && mode1 == 'viewed')) {                
                $("#dataFormMasterReadDataEmpID").closest("td").prev("td").hide()
                $('#dataFormMasterReadDataEmpID').closest('td').hide();
                $("#dataFormMasterClosed").closest("td").prev("td").hide()
                $('#dataFormMasterClosed').closest('td').hide();
            }

            //簽呈表新增時關閉綜合意見、會簽回覆、總經理決行
            if (mode1 == "inserted" || (FlomParameters == "Apply" && mode1 == "updated")) {
                $("#dataFormMasterMangAdditional").closest("td").prev("td").hide()
                $('#dataFormMasterMangAdditional').closest('td').hide();
                $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterCarryOut').closest('td').hide();
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormDetailMangReply").closest("td").prev("td").hide()
                $('#dataFormDetailMangReply').closest('td').hide();
                $("#dataFormMasterCreateBy").val(UserNmae);
                GetUserOrgNOs();
                WelfareChoose();
                if (FlomParameters == "Apply" && mode1 == "updated") {
                    $('#dataFormMasterTempAdditional').focus().css({ "background-color": "yellow" });
                }
            }

            if (mode1 == "updated") {                
                //不含申請人
                if (FlomParameters != "Apply") {
                    $('#dataFormMasterFileLevel').combobox('disable', true);                    
                    //隱藏明細新增功能
                    $('#DetailAdd').remove();
                }

                //結案不顯示限閲名單
                if (FlomParameters != "Close") {
                    $("#dataFormMasterReadDataEmpID").closest("td").prev("td").hide()
                    $('#dataFormMasterReadDataEmpID').closest('td').hide();                    
                }

                $('#dataFormMasterFileLevel').combobox('disable', true);
                $('#dataFormMasterAttachment').attr('readonly', true);
                $('#dataFormMasterAttachment').attr('disabled', true);
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $('#dataFormMasterSubject').attr('disabled', true);
                $('#dataFormMasterSubject').attr('readonly', true);
                $('#dataFormMasterDescription').attr('disabled', true);
                $('#dataFormMasterDescription').attr('readonly', true);
                $('#dataFormMasterActionPlan').attr('disabled', true);
                $('#dataFormMasterActionPlan').attr('readonly', true);
                var _empid = $('#dataFormMasterApplyEmpID').combobox('getValue');
                if (FlomParameters == "Countersigns" || FlomParameters == "Applicant") {
                    $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterCarryOut').closest('td').hide();
                    $('#dataFormDetailDescription').attr('disabled', true);
                    $('#dataFormDetailDescription').attr('readonly', true);
                }
                if (FlomParameters == "Countersigns")//隱藏彙總欄位
                {
                    //表頭更動名稱
                    $('#JQDialog1').dialog('setTitle', '簽呈申請-會簽人員');
                    $("#dataFormMasterSummary").closest("td").prev("td").hide()
                    $('#dataFormMasterSummary').closest('td').hide();
                    $('#dataFormMasterMangAdditional').attr('disabled', true);
                    $('#dataFormMasterMangAdditional').attr('readonly', true);                    
                }
                if (FlomParameters == "Applicant") {
                    //表頭更動名稱
                    $('#JQDialog1').dialog('setTitle', '簽呈申請-彙總承辦');
                    $('#dataFormMasterMangAdditional').attr('disabled', true);
                    $('#dataFormMasterMangAdditional').attr('readonly', true);
                    $('#dataFormDetailMangReply').attr('disabled', true);
                    $('#dataFormDetailMangReply').attr('readonly', true);                    
                    $('#dataFormMasterSummary').focus().css({ "background-color": "yellow" });
                    var _summary = $('#dataFormMasterSummary').val(); 
                    if (_summary == "" ) {
                        $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                        $('#dataFormMasterTempAdditional').closest('td').hide();
                    }
                    else {
                        $('#dataFormMasterSummary').attr('disabled', true);
                        $('#dataFormMasterSummary').attr('readonly', true);
                        $('#dataFormMasterTempAdditional').focus().css({ "background-color": "yellow" });
                    }
                }
                if (FlomParameters == "CarryOut") {
                    //表頭更動名稱                   
                    $('#JQDialog1').dialog('setTitle', '簽呈申請-總經理決行');
                    $('#dataFormMasterMangAdditional').attr('disabled', true);
                    $('#dataFormMasterMangAdditional').attr('readonly', true);
                    $('#dataFormMasterSummary').attr('disabled', true);
                    $('#dataFormMasterSummary').attr('readonly', true);
                    $('#dataFormMasterCarryOut').focus().css({ "background-color": "yellow" });
                }
            }

            //退回補充說明欄位不顯示
            if (mode1 == "inserted" || FlomParameters == "" || FlomParameters == "Mang" || FlomParameters == "CarryOut" || FlomParameters == "Countersigns" || FlomParameters == "Close") {
                $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                $('#dataFormMasterTempAdditional').closest('td').hide();
            }

            //會簽加簽人員
            if (mode1 == "viewed") {
                if (FlomParameters == "Countersigns") {
                    $("#dataFormMasterCarryOut").closest("td").prev("td").hide();
                    $('#dataFormMasterCarryOut').closest('td').hide();
                    $("#dataFormMasterSummary").closest("td").prev("td").hide();
                    $('#dataFormMasterSummary').closest('td').hide();
                    $("#dataFormDetailMangReply").closest("td").prev("td").hide();
                    $('#dataFormDetailMangReply').closest('td').hide();
                    $("#dataFormMasterClosed").closest("td").prev("td").hide();
                    $('#dataFormMasterClosed').data("infooptions").panel.closest("td").hide();
                }
                //承辦彙總加簽人員
                if (FlomParameters == "Applicant") {
                    $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                    $('#dataFormMasterTempAdditional').closest('td').hide();
                    $("#dataFormMasterCarryOut").closest("td").prev("td").hide();
                    $('#dataFormMasterCarryOut').closest('td').hide();
                }
            }
            if (FlomParameters == "Mang") {
                $("#JQDialog1").dialog('setTitle', '簽呈申請-主管審核');
                $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterCarryOut').closest('td').hide();
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormDetailMangReply").closest("td").prev("td").hide()
                $('#dataFormDetailMangReply').closest('td').hide();
                $('#dataFormMasterMangAdditional').focus().css({ "background-color": "yellow" });
            }

            if (FlomParameters == "Close") {
                $("#JQDialog1").dialog('setTitle', '簽呈申請-簽呈結案');
                $('#dataFormMasterMangAdditional').attr('disabled', true);
                $('#dataFormMasterMangAdditional').attr('readonly', true);
                $('#dataFormMasterSummary').attr('disabled', true);
                $('#dataFormMasterSummary').attr('readonly', true);
                $('#dataFormMasterCarryOut').attr('disabled', true);
                $('#dataFormMasterCarryOut').attr('readonly', true);
            }

            var RawExcel = $('.info-fileUpload-value', $("#dataFormMasterAttachment").next()).val();
            if (RawExcel != '') {
                var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/Petition/' + RawExcel }).html('[檔案下載]');
                $('#dataFormMasterAttachment').closest('td').append(link);
            }
        }

        function mygetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return '';
        }

        function OnSelectEmployeeID(rowData) {
            alert('empid編號:' + rowdata.applyempid);
            $("#dataFormMasterApplyEmpID").combobox('setValue', rowData.ApplyEmpID);
        }

        function CkCountersignRole() {
            var ApplyEmpID = $("#dataFormDetailCountersignRole").val();
            alert("職稱值:" + ApplyEmpID);
            if (ApplyEmpID == "") {
                $("#dataFormDetailCountersignRole").focus();
            }
        }

        //取得USER的部門代號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + _LogUserId,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {                        
                        $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        $("#dataFormMasterOrgNOParent").val(rows[0].OrgNOParent);
                        ReturnStr = data;
                    }
                }
            }
            );
        }

        //只有福委主委可以選擇呈送部門20220628新增此功能
        function WelfareChoose() {
            var ReturnStr = "";
            var _Welfare = false;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',
                data: "mode=method&method=" + "GetPetitionList" + "&parameters=" + _LogUserId,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);                    
                    for (i = 0; i < rows.length; i++)
                    {
                        if(rows[i].GroupID=="2001"){
                            _Welfare=true;
                        }
                    }
                }
            }
            );
            if (_Welfare) {
                $("#dataFormMasterApplyOrg_NO").combobox('enable', true);
            }
        }

        //福委會直接簽核至總經理20220628新增此功能
        function ResetApplyOrg_NO() {
            var _OrgNO = $("#dataFormMasterApplyOrg_NO").combobox("getValue");
            if (_OrgNO == "99999") {
                $("#dataFormMasterOrgNOParent").val("10000");
            }
        }

            //取得表單編號Listid存入主檔
            function GetFlowListID() {
                var _PetitionNO = $("#dataFormMasterPetitionNO").val();
                var ReturnStr = "";

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',
                    data: "mode=method&method=" + "GetListID" + "&parameters=" + _PetitionNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);
                        if (rows.length > 0) {
                            $("#dataFormMasterFlowListid").val(rows[0].LISTID);
                            ReturnStr = data;
                        }
                    }
                }
                );
            }

            function DetailformLoadSucess() {
                var RawExcel = $('.info-fileUpload-value', $("#dataFormDetailAttachment").next()).val();
                if (RawExcel != '' && DetailAttachment) {
                    var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/Petition/' + RawExcel }).html('[檔案下載]');
                    $('#dataFormDetailAttachment').closest('td').append(link);
                    DetailAttachment = false;
                }

                var mode1 = getEditMode($("#dataFormMaster"));
                if (mode1 == "inserted" || FlomParameters == "Mang" || (FlomParameters == "Apply" && mode1 == "updated")) {
                    $('#dataFormDetailAttachment').closest("td").prev("td").hide();
                    $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').hide(); //前面
                    $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').hide(); //後面瀏覽鈕
                    $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                }

                //會簽人員已填寫不可再修改
                var _MangReply = $('#dataFormDetailMangReply').val();            
                if (_MangReply != "") {
                    $('#dataFormDetailMangReply').attr('disabled', true);
                    $('#dataFormDetailMangReply').attr('readonly', true);
                    $('#dataFormDetailAttachment').siblings('.info-fileUpload-span').find('.info-fileUpload-value').prop('disabled', true);
                    //$('#dataFormDetailAttachment').siblings('.info-fileUpload-span').find('file').linkbutton('disable');
                    $('#dataFormDetailAttachment').siblings('.info-fileUpload-span').find('a').linkbutton('disable');
                    $('#dataFormDetailCountersignRole').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                    $('#dataFormDetailCountersignEmp').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                }
                else {
                    $('#dataFormDetailMangReply').focus().css({ "background-color": "yellow" });
                }
            }
            //明細AutoKey欄位預設值
            function dataFormDetail_OnApply() {
                if (getEditMode($("#dataFormDetail")) == "inserted") {
                    $("#dataFormDetailAutoKey").val(AddDetailform);
                }
                AddDetailform += 1;
            }

            function GridDetail_OnDelete() {
                //會簽、承辦會總、總經理決行不可刪除
                if (FlomParameters == "Countersigns" || FlomParameters == "Applicant" || FlomParameters == "CarryOut" || FlomParameters == "Mang" || FlomParameters == "Close") {
                    alert('會簽明細不可刪除!');
                    return false;
                }
            }

            //明細修改資料判斷
            function GridDetail_OnUpdate() {
                if (FlomParameters == "Countersigns") {
                    var _selectrow = $('#dataGridDetail').datagrid('getSelected');
                    var rowIndex = $('#dataGridDetail').datagrid('getRowIndex', _selectrow)
                    var _UpdateRows = $('#dataGridDetail').datagrid('getRows');
                    var up_value = _UpdateRows[rowIndex].CountersignEmp;
                    if (_LogUserId != up_value) {
                        alert('非此會簽人員無法編輯!');
                        return false;
                    }
                    else {
                        $('#dataFormDetailCountersignRole').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                        $('#dataFormDetailCountersignEmp').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                        $('#dataFormDetailDescription').attr('readonly', true);
                        $('#dataFormDetailDescription').attr('disabled', true);
                    }
                }
                else {
                    if (FlomParameters == "Applicant" || FlomParameters == "CarryOut" || FlomParameters == "Mang" || FlomParameters == "Close") {
                        alert('非會簽人員無法編輯,請點選瀏覽!');
                        return false;

                    }
                }
            }

            //彙總會簽人員名單至主檔
            function MasterOnApply() {            
                var mode1 = getEditMode($("#dataFormMaster"));            
                if (getEditMode($("#dataFormMaster")) == 'inserted' || (FlomParameters == "Apply" && mode1 == "updated")) {
                    var rows = $('#dataGridDetail').datagrid('getRows');
                    var _emps = "";
                    var RepeatEmmp = "";
                    for (var i = 0; i < rows.length; i++) {
                        var chkemp = _emps.indexOf(rows[i]["CountersignEmp"]);                    
                        if (rows.length - 1 == i)
                            _emps = _emps + rows[i]["CountersignEmp"]; //人員rows[i]["CountersignEmp"]
                        else
                            _emps = _emps + rows[i]["CountersignEmp"] + ",";
                        if (i > 0 && Number(chkemp) >= 0) {
                            RepeatEmmp = RepeatEmmp + rows[i]["CountersignEmp"] + "\n";
                        }
                    }
                
                    if (RepeatEmmp != "") {
                        alert('會簽名單重覆人員編號如下:\n' + RepeatEmmp);
                        return false;
                    }
                    $("#dataFormMasterCountersignEmps").val(_emps);
                
                }
            
                //退回填寫補充說明至累加欄位
                var _temp = $("#dataFormMasterTempAdditional").val();
                if (_temp != "") {
                    var appenddata = "";
                    var Returndate = $.jbjob.Date.DateFormat(new Date(), 'MM/dd');
                    if (FlomParameters == "Apply" && msterDescription != "") {
                        appenddata = Returndate + "補充:" + _temp + "\n" + $("#dataFormMasterDescription").val();
                        $("#dataFormMasterDescription").val(appenddata);
                        appenddata = "";
                        $("#dataFormMasterTempAdditional").val(appenddata);
                    }

                    if (FlomParameters == "Applicant" && msterSummary != "") {
                        appenddata = Returndate + "補充:" + _temp + "\n" + $("#dataFormMasterSummary").val();
                        $("#dataFormMasterSummary").val(appenddata);
                        appenddata = "";
                        $("#dataFormMasterTempAdditional").val(appenddata);
                    }
                }
                //MasterOnApplied();
                if (FlomParameters == "Close") {
                    var _close = $("#dataFormMasterClosed").options('getValue');
                    if (_close == "") {
                        alert("請填寫結案狀態!");
                        return false;
                    }
                    ////文件讀取名單為空白無法正常存儲,先設定預設值在結案清空,在儲檔重新再寫入
                    var _ReadDataEmpID = $("#dataFormMasterReadDataEmpID").combogrid('getValues');                
                    if (_ReadDataEmpID == "" || _ReadDataEmpID == "undefined") {
                        $("#dataFormMasterReadDataEmpID").combogrid('setValue', _LogUserId + '-' + UserNmae);
                        alert("讀取名單預設為申請人");
                    }
                }

                //判斷Textarea欄位有特殊字元
                $('textarea', this).each(function () {
                    var value1 = $(this).val();
                    value = value1.replace(/\v/g, '');//replace \v
                    $(this).val(value);
                })
            }

            function MasterOnApplied() {
                var _ReadDataEmpID = $("#dataFormMasterReadDataEmpID").combogrid('getValues');
                if (_ReadDataEmpID == "") {
                    $("#dataFormMasterReadDataEmpID").combogrid('setValue', _LogUserId + '-' + UserNmae);
                }
                return alert("空白");
            }

            function ReadDataEmpIBFLoad() {
                var _ReadDataEmpID = $("#dataFormMasterReadDataEmpID").combogrid('getValues');
                return alert("_ReadDataEmpID" + _ReadDataEmpID);
            }

            function ReadDataEmpIDonSelect()
            {
                var _ReadDataEmpID = $("#dataFormMasterReadDataEmpID").combogrid('getValues');
                if (_ReadDataEmpID == "") {
                    $("#dataFormMasterReadDataEmpID").combogrid('setValue', _LogUserId + '-' + UserNmae);
                }
            }

            //Grid資料自動換行
            function ShowDetailGrid(value) {
                return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
            }
            function GriddownloadScript(val, rowData, index) {
                if (rowData.Attachment != undefined) {//表示不是最後一筆加總的row
                    return '<a href="../JB_ADMIN/Petition/' + val + '">' + val + '</a>';
                }
            }

            //加簽意見附檔可下戴
            function FlowdownloadScript(val, rowData, index) {
                var link = "";
                var lstAttachments = rowData.ATTACHMENTS.split(';');//

                for (var i = 0; i < lstAttachments.length; i++) {
                    if (lstAttachments[i] != "" && lstAttachments[i] != "null") {
                        var realFileName = lstAttachments[i];
                        var fileName = realFileName.replace(/__/g, "&nbsp;");
                        var href = "../WorkflowFiles/" + realFileName;
                        link += "<A id='" + "ATTACHMENTS" + i + "' href='" + href + "' target='_blank' class=" + realFileName + " download >" + fileName + "</A>&nbsp&nbsp";
                    }
                }
                return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + link + "</p>";
            }
        

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div style="display: none;"> <%--display: none;--%>
                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPetitionMaster.PetitionMaster" runat="server" AutoApply="True"
                DataMember="PetitionMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False"  EditOnEnter="True"  MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True"  EditMode="Dialog" InsertCommandVisible="True" Width="600px">
                <Columns>
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="呈核人員編號" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="呈核人員" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="保密等級" Editor="numberbox" FieldName="FileLevel" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="主旨" Editor="text" FieldName="Subject" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Description" Editor="text" FieldName="Description" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ActionPlan" Editor="text" FieldName="ActionPlan" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment" Editor="text" FieldName="Attachment" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Summary" Editor="text" FieldName="Summary" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CarryOut" Editor="text" FieldName="CarryOut" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="True" />
                </Columns>
               <%-- <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>--%>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="簽呈申請" Width="930px"  DialogTop="10px" DialogLeft="10px">               
                <div id="titledes"><asp:Label ID="LabGrid0" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="文字敍述顯示不完整時,請點此欄位最右邊捲軸" BorderStyle="Solid" Font-Size="Small" Height="16px"></asp:Label></div>
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PetitionMaster" HorizontalColumnsCount="12" RemoteName="sPetitionMaster.PetitionMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" 
                     OnLoadSuccess="DataformLoadSucess" OnApply="MasterOnApply" ParentObjectID="">                     
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" ReadOnly="True" Span="2" Width="100" maxlength="0" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" maxlength="0" ReadOnly="True" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployeeID,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" ReadOnly="True" Span="2" Width="120" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPetitionMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:ResetApplyOrg_NO,panelHeight:200" FieldName="ApplyOrg_NO" Format="" maxlength="0" ReadOnly="True" Span="4" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保密等級" Editor="infocombobox" EditorOptions="valueField:'Code',textField:'CodeNmae',remoteName:'sPetitionMaster.GradeCode',tableName:'GradeCode',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FileLevel" Format="" maxlength="0" Span="2" Width="100" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主旨" Editor="textarea" EditorOptions="height:20" FieldName="Subject" maxlength="256" PlaceHolder="最多可填寫256字元!" Span="12" Width="800" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="說明" Editor="textarea" EditorOptions="height:60" FieldName="Description" Format="" MaxLength="2048" PlaceHolder="最多可填寫2048字元!" ReadOnly="False" Span="12" Visible="True" Width="800" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建議" Editor="textarea" EditorOptions="height:40" FieldName="ActionPlan" Format="" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" Span="12" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保留期限" Editor="datebox" FieldName="ExpirationDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment" Format="" maxlength="0" Span="10" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核主管補充說明" Editor="textarea" EditorOptions="height:25" FieldName="MangAdditional" maxlength="256" NewRow="False" PlaceHolder="最多可填寫256字元!" ReadOnly="False" RowSpan="1" Span="12" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="彙總承報" Editor="textarea" FieldName="Summary" Format="" MaxLength="2048" Span="12" Visible="True" Width="800" EditorOptions="height:45" PlaceHolder="最多可填寫2048字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總經理決行" Editor="textarea" EditorOptions="height:30" FieldName="CarryOut" Format="" maxlength="1024" PlaceHolder="最多可填寫1024字元!" Span="12" Width="800" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" maxlength="0" Span="6" Visible="False" Width="450" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Span="1" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員姓名" Editor="text" FieldName="ApplyEmpName" Format="" ReadOnly="True" Span="2" Visible="False" Width="120" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" ReadOnly="False" Span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="退回補充說明" Editor="textarea" EditorOptions="height:30" FieldName="TempAdditional" maxlength="0" ReadOnly="False" Span="12" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案狀態" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'未結案',value:'0'},{text:'結案',value:'1'}]" FieldName="Closed" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="12" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件讀取名單" Editor="infocombogrid" EditorOptions="panelWidth:350,valueField:'USERID',textField:'USERNAME',remoteName:'sPetitionMaster.ReaderList',tableName:'ReaderList',valueFieldCaption:'人員編號',textFieldCaption:'人員姓名',selectOnly:true,checkData:false,columns:[],cacheRelationText:false,multiple:true" FieldName="ReadDataEmpID" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="12" Visible="True" Width="800" OnBlur="" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PetitionCountersign" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sPetitionMaster.PetitionMaster" Title="會簽明細" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="Reload" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="GridDetail_OnUpdate" OnDelete="GridDetail_OnDelete" >                   
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽職稱" Editor="infocombobox" FieldName="CountersignRole" Format="" Width="120" EditorOptions="valueField:'GroupID',textField:'GROUPNAME',remoteName:'sPetitionMaster.PetitionPosition',tableName:'PetitionPosition',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽人員" Editor="infocombobox" FieldName="CountersignEmp" Format="" Width="60" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="承辦詢問建議" Editor="text" FieldName="Description" Visible="True" Width="270" Format="" FormatScript="ShowDetailGrid">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽者回覆" Editor="text" FieldName="MangReply" Width="270" Visible="True" FormatScript="ShowDetailGrid" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽日期" Editor="datebox" FieldName="CountersignDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="附件檔案" Editor="text" FieldName="Attachment" Format="" Width="80" Visible="True" FormatScript="GriddownloadScript" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="80" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem ID="DetailAdd" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="930px" EditMode="Dialog" DialogLeft="10px" DialogTop="150px" Title="會簽明細">
                    
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="PetitionCountersign" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="dataFormDetail_OnApply" ParentObjectID="dataFormMaster" RemoteName="sPetitionMaster.PetitionMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="DetailformLoadSucess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽職稱" Editor="inforefval" EditorOptions="title:'選取職稱',panelWidth:350,remoteName:'sPetitionMaster.PetitionPosition',tableName:'PetitionPosition',columns:[{field:'GroupID',title:'職稱編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'GROUPNAME',title:'職稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'GroupID',textField:'GROUPNAME',valueFieldCaption:'職稱編號',textFieldCaption:'職稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignRole" Span="2" Width="200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽人員" Editor="inforefval" EditorOptions="title:'選取會簽人員',panelWidth:350,remoteName:'sPetitionMaster.PetitionList',tableName:'PetitionList',columns:[{field:'USERID',title:'人員編號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'USERNAME',title:'會簽人員',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[{field:'GroupID',value:'row[CountersignRole]'}],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'會簽人員編號',textFieldCaption:'會簽人員',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignEmp" Format="" Span="2" Width="200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="承辦詢問建議" Editor="textarea" EditorOptions="height:40" FieldName="Description" Format="" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" Span="4" Width="780" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽者回覆" Editor="textarea" EditorOptions="height:60" FieldName="MangReply" MaxLength="1024" OnBlur="" PlaceHolder="最多可填寫1024字元!" Span="4" Visible="True" Width="780" />
                            <JQTools:JQFormColumn Alignment="left" Caption="附件檔名" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽日期" Editor="text" FieldName="CountersignDate" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>                     

                </JQTools:JQDialog>                
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">                    
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PetitionDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="PetitionNO" RemoteMethod="True" DefaultValue=" 自動編號" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="ApplyEmpName" RemoteMethod="True" DefaultValue="_username" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                    </Columns>                    
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="FileLevel" RemoteMethod="True" ValidateMessage="請選擇密件等級!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Subject" RemoteMethod="True" ValidateMessage="主旨不可空白!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Description" RemoteMethod="True" ValidateMessage="說明不可空白!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignRole" RemoteMethod="True" ValidateType="None" ValidateMessage="會簽職稱不可空白!" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignEmp" RemoteMethod="True" ValidateMessage="會簽人員不可空白!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <asp:Panel ID="dgpanel" runat="server">
                    <%--<asp:Label ID="LabGrid" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="顯示加簽意見請點選右邊▼展開資料" BorderStyle="Solid"></asp:Label>--%>
                    <br />
                    <JQTools:JQDataGrid ID="PlusAppoveList" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="PlusApproveList" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPetitionMaster.PetitionMaster" RowNumbers="True" Title="加簽意見" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="left" Caption="LISTID" Editor="text" FieldName="LISTID" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="left" Caption="加簽者" Editor="text" FieldName="USERNAME" Format="" FormatScript="" Width="80" />
                            <JQTools:JQGridColumn Alignment="left" Caption="批示意見" Editor="text" FieldName="REMARK" Format="" FormatScript="ShowDetailGrid" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="680" />
                            <JQTools:JQGridColumn Alignment="left" Caption="附檔" Editor="text" FieldName="ATTACHMENTS" FormatScript="FlowdownloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataGrid>
                    <br />
                </asp:Panel>                
            </JQTools:JQDialog>
        </div>
    </form>
</body>
   
</html>
