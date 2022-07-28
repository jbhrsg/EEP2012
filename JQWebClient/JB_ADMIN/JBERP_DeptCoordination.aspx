<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DeptCoordination.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js">    </script>   
    <script type="text/javascript">
        $(document).ready(function () {
            //將focus 欄位背景顏色改為黃色,移到物事上的hover事件
            $(function () {
                $("textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        })

        var AddDetailform = 0;//明細新增筆數AutoKey欄位預設值
        var DetailAttachment = true;
        var FlomParameters = "";       

        function DataformLoadSucess() {
            var _LogUserId = getClientInfo("UserID");
            var mode1 = getEditMode($("#dataFormMaster"));
            FlomParameters = Request.getQueryStringByName("P1");
            //alert("FlomParameters:" + FlomParameters + " mode1:" + mode1);
            //alert("mode1:" + mode1);

            //文件保密等級
            $("#dataFormMasterFileLevel").val(0);

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
            var tempdiv = $('#dataFormMasterClosed').closest('tr');
            //var Goal = $('#dataFormMasterDescription').closest('/tr');
            //var label1 = $('<br/><span><tr><td><label>主管補充</label></td><br/><td><input id=TempAdditionala type=\"text\" value =1></input></td></tr></span>').appendTo(tempdiv);
            //var columnIndext = $("<input type=\"text\" value ='1'></input>").appendTo(div1).css('width', '30px');
            //$("<input type=\"text\" value ='1'></input>").appendTo(dataFormMasterDescription).css('width', '800px');
            //var labela = $('<tr><td align style=text-align:right><label>主管補充</label></td><td span=10 colspan=19><textarea id=dataFormMasterMangAdditional name=MangAdditional type=text placeholder=最多可填寫256字元! maxlength=256 style=width:800px;height:30px </textarea></td> </tr>').appendTo(tempdiv);
            //顯示結案狀態
            if ((FlomParameters != "Close" && mode1 != 'viewed') || (FlomParameters != "" && mode1 == 'viewed')) {
                $("#dataFormMasterClosed").closest("td").prev("td").hide()
                $('#dataFormMasterClosed').closest('td').hide();
            }

            //顯示增補會簽
            var _Supplement = $("#dataFormMasterSupplement").options('getValue');
            if (mode1 == "inserted" || (FlomParameters == "Apply" && mode1 == 'updated') || (FlomParameters == "Mang") || (FlomParameters == "Countersigns" && _Supplement=="")) {
                $("#dataFormMasterSupplement").closest("td").prev("td").hide()
                $('#dataFormMasterSupplement').closest('td').hide();
            }
            else {
                if (_Supplement != "") {
                    $('#dataFormMasterSupplement').next('span').find('input').attr('disabled', true);
                }
            }
            if ((FlomParameters == "Countersigns" && _Supplement != "")) {
                $('#dataFormMasterSupplement').next('span').find('input').attr('disabled', true);
            }

            if (mode1 == "inserted") {                
                GetUserOrgNOs();                
            }
            //簽呈表新增時關閉綜合意見、會簽回覆
            if (mode1 == "inserted" || FlomParameters == "AddCountersigns" || (FlomParameters == "Apply" && mode1 == "updated")) {
                $("#dataFormMasterMangAdditional").closest("td").prev("td").hide();
                $('#dataFormMasterMangAdditional').closest('td').hide();
                $("#dataFormMasterSummary").closest("td").prev("td").hide();
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterMangCarryOut').closest('td').hide();
                $("#dataFormMasterClosed").closest("td").prev("td").hide();
                $('#dataFormMasterClosed').closest('td').hide();                
                $("#dataFormDetailMangReply").closest("td").prev("td").hide(); 
                $('#dataFormDetailMangReply').closest('td').hide(); 
                if (FlomParameters == "Apply" && mode1 == "updated") {
                    $('#dataFormMasterTempAdditional').focus().css({ "background-color": "yellow" });
                }
            }
            
            //會簽人員關閉功能及欄位不可修改
            if (mode1 == "updated") {
                //不含申請人
                if (FlomParameters != "Apply") {
                    //$('#dataFormMasterFileLevel').combobox('disable', true);
                }
                
                var _Supplement1 = false;
                if (_Supplement != "") {
                    _Supplement1 = Boolean(_Supplement);
                }

                if (FlomParameters != "Apply" && (FlomParameters != "AddCountersigns") || (FlomParameters == "AddCountersigns" && !_Supplement1 && _Supplement != "")) {
                    //隱藏明細新增功能
                    //$('#DetailAdd').hide();
                    $('#DetailAdd').remove();
                }
                
                $('#dataFormMasterSubject').attr('disabled', true);
                $('#dataFormMasterSubject').attr('readonly', true);
                $('#dataFormMasterDescription').attr('disabled', true);
                $('#dataFormMasterDescription').attr('readonly', true);
                $('#dataFormMasterActionPlan').attr('disabled', true);
                $('#dataFormMasterActionPlan').attr('readonly', true);
                
                if (FlomParameters == "Countersigns" || FlomParameters == "Applicant") {//|| FlomParameters == "AddCountersigns"
                    $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterMangCarryOut').closest('td').hide();
                    $('#dataFormDetailDescription').attr('disabled', true);
                    $('#dataFormDetailDescription').attr('readonly', true);                    
                }
                if (FlomParameters == "Countersigns" || FlomParameters == "AddCountersigns") { //隱藏彙總欄位
                    //表頭更動名稱
                    if (FlomParameters == "Countersigns") {
                        $('#JQDialog1').dialog('setTitle', '跨部門聯絡單-會簽人員');
                    }
                    else
                        $('#JQDialog1').dialog('setTitle', '跨部門聯絡單-增補會簽人員');
                    $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterMangCarryOut').closest('td').hide();
                    $("#dataFormMasterSummary").closest("td").prev("td").hide()
                    $('#dataFormMasterSummary').closest('td').hide();
                    $('#dataFormMasterMangAdditional').attr('disabled', true);
                    $('#dataFormMasterMangAdditional').attr('readonly', true);        
                }
                if (FlomParameters == "Applicant") {                    
                    //表頭更動名稱
                    $('#JQDialog1').dialog('setTitle', '跨部門聯絡單-意見綜合');
                    $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterMangCarryOut').closest('td').hide();
                    $('#dataFormMasterMangAdditional').attr('disabled', true);
                    $('#dataFormMasterMangAdditional').attr('readonly', true);
                    $('#dataFormDetailMangReply').attr('disabled', true);
                    $('#dataFormDetailMangReply').attr('readonly', true);                    
                    $('#dataFormMasterSummary').focus().css({ "background-color": "yellow" });
                    var _summary = $('#dataFormMasterSummary').val();
                    if (_summary == "") {
                        $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                        $('#dataFormMasterTempAdditional').closest('td').hide();
                    }
                    else {
                        $('#dataFormMasterSummary').attr('disabled', true);
                        $('#dataFormMasterSummary').attr('readonly', true);
                        $('#dataFormMasterTempAdditional').focus().css({ "background-color": "yellow" });
                    }
                }
            }

            //退回補充說明欄位不顯示
            if (mode1 == "inserted" || FlomParameters == "" || FlomParameters == "Mang"  || FlomParameters == "Countersigns" || FlomParameters == "AddCountersigns" || FlomParameters == "MangCarryOut" || FlomParameters == "Close") {
                $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                $('#dataFormMasterTempAdditional').closest('td').hide();
            }

            if (FlomParameters == "Mang") {
                $("#JQDialog1").dialog('setTitle', '跨部門聯絡單-主管審核');
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                $('#dataFormMasterTempAdditional').closest('td').hide();
                $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterMangCarryOut').closest('td').hide();
                $("#dataFormMasterClosed").closest("td").prev("td").hide()
                $('#dataFormMasterClosed').closest('td').hide();               
                $('#dataFormMasterMangAdditional').focus().css({ "background-color": "yellow" });
            }
            if (FlomParameters == "MangAddCheck") {
                $("#JQDialog1").dialog('setTitle', '跨部門聯絡單-增補會簽主管審核');
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterMangCarryOut').closest('td').hide();
                $("#dataFormMasterTempAdditional").closest("td").prev("td").hide();
                $('#dataFormMasterTempAdditional').closest('td').hide();
                $("#dataFormMasterClosed").closest("td").prev("td").hide()
                $('#dataFormMasterClosed').closest('td').hide();
               
                //$("#dataFormMasterMangAdditional").attr('readonly', true);
                //$('#dataFormMasterMangAdditional').attr('disabled', true);
            }
            if (mode1 == "viewed") {
                if (FlomParameters == "Countersigns" || FlomParameters == "AddCountersigns") {
                    $('#dataFormMasterSubject').attr('readonly', true);
                    $('#dataFormMasterSubject').attr('disabled', true);
                    $('#dataFormMasterDescription').attr('readonly', true);
                    $('#dataFormMasterDescription').attr('disabled', true);
                    $("#dataFormMasterSummary").closest("td").prev("td").hide();
                    $('#dataFormMasterSummary').closest('td').hide();
                    $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterMangCarryOut').closest('td').hide();
                    //$('#dataFormMasterSummary').attr('readonly', true);
                    //$('#dataFormMasterSummary').attr('disabled', true);
                }

                if (FlomParameters == "Applicant") {
                    $('#dataFormMasterSubject').attr('readonly', true);
                    $('#dataFormMasterSubject').attr('disabled', true);
                    $('#dataFormMasterDescription').attr('readonly', true);
                    $('#dataFormMasterDescription').attr('disabled', true);
                    $('#dataFormDetailDescription').attr('readonly', true);
                    $('#dataFormDetailDescription').attr('disabled', true);
                    $("#dataFormMasterMangCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterMangCarryOut').closest('td').hide();
                }
            }
            
            if (FlomParameters == "MangCarryOut") {
                $("#JQDialog1").dialog('setTitle', '跨部門聯絡單-主管決行');
                $("#dataFormMasterMangAdditional").attr('disabled', true);
                $('#dataFormMasterMangAdditional').attr('disabled', true);
                $('#dataFormMasterSummary').attr('disabled', true);
                $('#dataFormMasterSummary').attr('readonly', true);
                $('#dataFormDetailMangReply').attr('disabled', true);
                $('#dataFormDetailMangReply').attr('readonly', true);
                $('#dataFormMasterMangCarryOut').focus().css({ "background-color": "yellow" });
            }
            if (FlomParameters == "Close") {
                $("#JQDialog1").dialog('setTitle', '跨部門聯絡單-結案');
                $('#dataFormMasterMangAdditional').attr('disabled', true);
                $('#dataFormMasterMangAdditional').attr('readonly', true);
                $('#dataFormMasterSummary').attr('disabled', true);
                $('#dataFormMasterSummary').attr('readonly', true);
                $("#dataFormMasterMangCarryOut").attr('disabled', true);
                $('#dataFormMasterMangCarryOut').attr('readonly', true);
            }
            
            var FormName = '#dataFormMasterAttachment';
            //檔案下載
            for (var i = 0; i < 3; i++) {
                var RawExcel = $('.info-fileUpload-value', $(FormName + String(i + 1)).next()).val();
                if (RawExcel != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/DeptCoordination/' + RawExcel }).html('[檔案下載]');
                    $(FormName + String(i + 1)).closest('td').append(link);
                }
            }
            if (FlomParameters == "Mang" || FlomParameters == "Countersigns" || FlomParameters == "AddCountersigns" || FlomParameters == "Applicant" || FlomParameters == "MangCarryOut" || FlomParameters == "Close") {
                for (var i = 0; i < 3; i++) {
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                }
            }
            
        }

        //取得USER的部門代號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sDeptCoordinationMaster.DeptCoordinationMaster',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
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

        //取得表單編號Listid存入主檔
        function GetFlowListID() {
            var _PetitionNO = $("#dataFormMasterDeptCoordinationNO").val();
            var ReturnStr = "";            
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sDeptCoordinationMaster.DeptCoordinationMaster',
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

        function OnSelectEmployeeID(rowData) {
            alert('empid編號:' + rowData.ApplyEmpID);
            $("#dataFormMasterApplyEmpID").combobox('setValue', rowData.ApplyEmpID);
        }

        function DetailformLoadSucess() {            
            //var FlomParameters = Request.getQueryStringByName("P1");
            var mode1 = getEditMode($("#dataFormMaster"));
            var mode2 = getEditMode($("#dataFormDetail"));
            var RawExcel = $('.info-fileUpload-value', $("#dataFormDetailAttachment").next()).val();
            if (RawExcel != '' && DetailAttachment) {
                var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/DeptCoordination/' + RawExcel }).html('[檔案下載]');
                $('#dataFormDetailAttachment').closest('td').append(link);
                DetailAttachment = false;
            }

            if (mode1 == "inserted" || FlomParameters == "Mang" || FlomParameters == "AddCountersigns" || FlomParameters == "Applicant" || (FlomParameters == "Apply" && mode1 == "updated")) {
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
            }

            //會簽人員已填寫不可再修改
            var _MangReply = $('#dataFormDetailMangReply').val();            
            if (_MangReply != "" ) {
                $('#dataFormDetailMangReply').attr('disabled', true);
                $('#dataFormDetailMangReply').attr('readonly', true);
                $('#dataFormDetailAttachment').siblings('.info-fileUpload-span').find('.info-fileUpload-value').prop('disabled', true);
                $('#dataFormDetailAttachment').siblings('.info-fileUpload-span').find('a').linkbutton('disable');
            }
            else {
                if (FlomParameters != "AddCountersigns" && mode2 != "viewed") {
                    $('#dataFormDetailMangReply').focus().css({ "background-color": "yellow" });
                }

            }
            if (FlomParameters != "" && FlomParameters != "Apply" && FlomParameters != "AddCountersigns") {
                $('#dataFormDetailCountersignRole').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                $('#dataFormDetailCountersignEmp').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
            }
            ////會簽人員增補及非增補欄位寫入
            //if (getEditMode($("#dataFormMaster")) == 'inserted' || (FlomParameters == "Apply" && mode1 == "updated")) {
            //    //$("#dataFormDetailflowflag").val("Y");//第一次會簽人員由主檔執行會簽流程,第二次由明細執行會簽流程
            //    $("#dataFormDetailAddCountersigns").val(0);//不是增補人員
            //}
        }

        //明細AutoKey欄位預設值
        function dataFormDetail_OnApply() {
            var mode1 = getEditMode($("#dataFormDetail"));
            if (getEditMode($("#dataFormDetail")) == "inserted") {
                $("#dataFormDetailAutoKey").val(AddDetailform);
            }
            AddDetailform += 1;

            //會簽人員增補及非增補欄位寫入
            if (getEditMode($("#dataFormMaster")) == 'inserted' || FlomParameters == "AddCountersigns" || (FlomParameters == "Apply" && mode1 == "updated")) {
                if (FlomParameters == "AddCountersigns") {
                    $("#dataFormDetailAddCountersigns").val(1);//是增補人員
                }
                else
                    $("#dataFormDetailAddCountersigns").val(0);//非增補人員
            }
        }

        //彙總會簽人員名單至主檔
        function MasterOnApply() {            
            //var FlomParameters = Request.getQueryStringByName("P1");
            var mode1 = getEditMode($("#dataFormMaster"));
            if (getEditMode($("#dataFormMaster")) == 'inserted' || (FlomParameters == "Apply" && mode1 == "updated") || FlomParameters == "AddCountersigns") {
                //檢查重覆會簽人員名單
                var rows = $('#dataGridDetail').datagrid('getRows');
                var _emps = "";
                var _addemps = "";
                var RepeatEmmp = "";
                var _Add = 0;
                for (var i = 0; i < rows.length; i++) {                    
                    var chkemp = _emps.indexOf(rows[i]["CountersignEmp"]);
                    _Add = Boolean(rows[i]["AddCountersigns"]);                    
                    if (rows.length - 1 == i) {
                        if (FlomParameters == "AddCountersigns" && _Add)
                            _addemps = _addemps + rows[i]["CountersignEmp"];
                        else
                            _emps = _emps + rows[i]["CountersignEmp"];//人員rows[i]["CountersignEmp"]
                    }
                    else {
                        if (FlomParameters == "AddCountersigns" && _Add)
                            _addemps = _addemps + rows[i]["CountersignEmp"] + ",";
                        else
                            _emps = _emps + rows[i]["CountersignEmp"] + ",";
                    }
                    if (i > 0 && Number(chkemp) >= 0) {
                        RepeatEmmp = RepeatEmmp + rows[i]["CountersignEmp"] + "\n";
                    }
                }

                if (RepeatEmmp != "") {
                    alert('會簽名單重覆人員編號如下:\n' + RepeatEmmp);
                    return false;
                }
                if (FlomParameters == "AddCountersigns") {
                    $("#dataFormMasterAddCountersignEmps").val(_addemps);//申請者第二次補增會簽人員
                }
                else {
                    $("#dataFormMasterCountersignEmps").val(_emps);//申請者第一次新增會簽人員
                }
                
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

            if (FlomParameters == "Close") {
                var _close = $("#dataFormMasterClosed").options('getValue');
                if (_close == "") {
                    alert("請填寫結案狀態!");
                    return false;
                }                
            }
            if (FlomParameters == "AddCountersigns") {
                var _close = $("#dataFormMasterSupplement").options('getValue');
                if (_close == "") {
                    alert("請填寫增補會簽狀態!");
                    return false;
                }
            }            
        }

        function GridDetail_OnInsert() {
            var _Supplement = $("#dataFormMasterSupplement").options('getValue');
            
            if (FlomParameters == "AddCountersigns") {
                if (_Supplement == "") {
                    alert("請填寫增補會簽狀態!");
                    return false;
                }
                if (_Supplement == "false") {
                    alert("增補會簽填寫為[是]才可新增增補會簽人員!");
                    return false;
                }
            }       
        }

        function GridDetail_OnDelete() {
            //會簽不可刪除
            //var FlomParameters = Request.getQueryStringByName("P1");
            var _selectrow = $('#dataGridDetail').datagrid('getSelected');
            var rowIndex = $('#dataGridDetail').datagrid('getRowIndex', _selectrow)
            var _UpdateRows = $('#dataGridDetail').datagrid('getRows');
            var up_value = false;
            up_value = Boolean(_UpdateRows[rowIndex].AddCountersigns);
            
            if (FlomParameters == "Countersigns" || (FlomParameters == "AddCountersigns" && !up_value) || FlomParameters == "Applicant" || FlomParameters == "Mang" || FlomParameters == "MangCarryOut" || FlomParameters == "Close") {
                alert('會簽明細不可刪除!');
                return false;
            }
        }

        function GridDetail_OnUpdate() {
            var _LogUserId = getClientInfo("UserID");
            //var FlomParameters = Request.getQueryStringByName("P1");
            if (FlomParameters == "Countersigns" || FlomParameters == "AddCountersigns") {
                var _selectrow = $('#dataGridDetail').datagrid('getSelected');
                var rowIndex = $('#dataGridDetail').datagrid('getRowIndex', _selectrow)
                var _UpdateRows = $('#dataGridDetail').datagrid('getRows');
                var up_value = _UpdateRows[rowIndex].CountersignEmp;
                if (_LogUserId != up_value) {
                    alert('非此會簽人員無法編輯!');
                    return false;
                } else {
                    $('#dataFormDetailCountersignRole').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                    $('#dataFormDetailCountersignEmp').data("inforefval").refval.find("span.icon-view").attr('disabled', true);
                    $('#dataFormDetailDescription').attr('readonly', true);
                    $('#dataFormDetailDescription').attr('disabled', true);
                }
            }
            else {
                if (FlomParameters == "Applicant" || FlomParameters == "Mang" || FlomParameters == "MangCarryOut" || FlomParameters == "Close") {
                    alert('非會簽人員無法編輯,請點選瀏覽!');
                    return false;

                }
            }
        }

        //完整顯示明細Grid資料
        function ShowDetailGrid(value) {
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }

        function GriddownloadScript(val, rowData, index) {
            if (rowData.Attachment != undefined) {//表示不是最後一筆加總的row
                return '<a href="../JB_ADMIN/DeptCoordination/' + val + '">' + val + '</a>';
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
                    var href = "../WorkflowFiles/" + realFileName ;
                    link += "<A id='" + "ATTACHMENTS" + i.toString() + "' href='" + href + "' target='_blank' class=" + realFileName + " download >" + fileName + "</A>&nbsp&nbsp";
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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sDeptCoordinationMaster.DeptCoordinationMaster" runat="server" AutoApply="True"
                DataMember="DeptCoordinationMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡單編號" Editor="text" FieldName="DeptCoordinationNO" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者編號" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="100" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="FileLevel" Editor="numberbox" FieldName="FileLevel" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="主旨" Editor="text" FieldName="Subject" Format="" MaxLength="256" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="Description" Format="" MaxLength="2048" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ActionPlan" Editor="text" FieldName="ActionPlan" Format="" MaxLength="1024" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Summary" Editor="text" FieldName="Summary" Format="" MaxLength="1024" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment1" Editor="text" FieldName="Attachment1" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment2" Editor="text" FieldName="Attachment2" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment3" Editor="text" FieldName="Attachment3" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment4" Editor="text" FieldName="Attachment4" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment5" Editor="text" FieldName="Attachment5" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" MaxLength="100" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="10" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工" Editor="text" FieldName="CreateBy" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="跨部門聯絡單" Width="930px" DialogLeft="10px" DialogTop="10px">
                <div id="titledes"><asp:Label ID="LabGrid0" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="文字敍述顯示不完整時,請點此欄位最右邊捲軸" BorderStyle="Solid" Font-Size="Small"></asp:Label></div>
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="DeptCoordinationMaster" HorizontalColumnsCount="10" RemoteName="sDeptCoordinationMaster.DeptCoordinationMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" ShowApplyButton="False" OnApply="MasterOnApply" OnLoadSuccess="DataformLoadSucess" ValidateStyle="Dialog" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="連絡單號" Editor="text" FieldName="DeptCoordinationNO" Format="" ReadOnly="True" Span="2" Width="100" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核日期" Editor="datebox" FieldName="ApplyDate" Format="" ReadOnly="True" Span="2" Width="100" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sDeptCoordinationMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployeeID,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" ReadOnly="True" Span="2" Width="100" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="承辦者" Editor="text" FieldName="ApplyEmpName" Format="" maxlength="0" Visible="False" Width="100" Span="2" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sDeptCoordinationMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" Format="" maxlength="0" ReadOnly="True" Span="2" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保密等級" Editor="infocombobox" FieldName="FileLevel" Span="2" Width="100" EditorOptions="valueField:'Code',textField:'CodeNmae',remoteName:'sDeptCoordinationMaster.GradeCode',tableName:'GradeCode',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" Visible="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="事由" Editor="textarea" FieldName="Subject" Format="" maxlength="256" Width="800" EditorOptions="height:25" Span="10" PlaceHolder="最多可填寫256字元!" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="內容" Editor="textarea" FieldName="Description" Format="" maxlength="2048" Width="800" EditorOptions="height:50" OnBlur="" PlaceHolder="最多可填寫2048字元!" Span="10" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建議" Editor="textarea" EditorOptions="height:30" FieldName="ActionPlan" maxlength="1024" Span="10" Visible="True" Width="800" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主管說明" Editor="textarea" FieldName="MangAdditional" maxlength="256" Width="800" Visible="False" PlaceHolder="最多可填寫256字元!" Span="10" EditorOptions="height:25" />
                        <JQTools:JQFormColumn Alignment="left" Caption="意見綜合" Editor="textarea" EditorOptions="height:40" FieldName="Summary" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" Span="10" Visible="True" Width="800" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主管決行" Editor="textarea" EditorOptions="height:30" FieldName="MangCarryOut" MaxLength="256" PlaceHolder="最多可填寫256字元!" ReadOnly="False" Span="10" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" 附檔1" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/DeptCoordination',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment1" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" 附檔3" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/DeptCoordination',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" 附檔2" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/DeptCoordination',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="5" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="補充說明" Editor="textarea" FieldName="TempAdditional" MaxLength="256" NewRow="False" ReadOnly="False" RowSpan="1" Span="10" Visible="True" Width="800" EditorOptions="height:30" PlaceHolder="最多可填寫256字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Width="180" Visible="False" MaxLength="0" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" Format="" Width="180" Visible="False" MaxLength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" maxlength="0" ReadOnly="False" Span="1" Visible="False" Width="80" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="增補會簽" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'是',value:'true'},{text:'否',value:'false'}]" FieldName="Supplement" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="10" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案狀態" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'未結案',value:'0'},{text:'結案',value:'1'}]" FieldName="Closed" maxlength="0" ReadOnly="False" Span="10" Visible="True" Width="80" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AddCountersignEmps" Editor="text" FieldName="AddCountersignEmps" maxlength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="DeptCoordinationCountersign" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sDeptCoordinationMaster.DeptCoordinationMaster" Title="明細資料" OnDelete="GridDetail_OnDelete" OnUpdate="GridDetail_OnUpdate" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="GridDetail_OnInsert" >

                    <Columns>
                          <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" FormatScript="" Visible="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="DeptCoordinationNO" Editor="text" FieldName="DeptCoordinationNO" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="會簽職稱" Editor="infocombobox" FieldName="CountersignRole" Format="" Width="120" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sDeptCoordinationMaster.PetitionPosition',tableName:'PetitionPosition',columns:[],columnMatches:[],whereItems:[],valueField:'GroupID',textField:'GROUPNAME',valueFieldCaption:'GroupID',textFieldCaption:'GROUPNAME',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Visible="True" />
                          <JQTools:JQGridColumn Alignment="left" Caption="會簽 人員" Editor="infocombobox" FieldName="CountersignEmp" Width="60" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sDeptCoordinationMaster.Employee',tableName:'Employee',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',valueFieldCaption:'GroupID',textFieldCaption:'GROUPNAME',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Format="" Visible="True" />
                          <JQTools:JQGridColumn Alignment="left" Caption="詢問建議/意見" Editor="text" FieldName="Description" Format="" Width="235" FormatScript="ShowDetailGrid" Visible="True" />
                          <JQTools:JQGridColumn Alignment="left" Caption="會簽者回覆" Editor="text" FieldName="MangReply" FormatScript="ShowDetailGrid" Width="235" Visible="True" ReadOnly="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="會簽日期" Editor="text" FieldName="CountersignDate" Format="yyyy/mm/dd" Visible="True" Width="65" ReadOnly="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="附檔" Editor="text" FieldName="Attachment" Format="" Visible="True" Width="80" FormatScript="GriddownloadScript">
                          </JQTools:JQGridColumn>
                          <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="AddCountersigns" Editor="text" FieldName="AddCountersigns" Visible="False" Width="80" />
                          <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Visible="False" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                          <JQTools:JQGridColumn Alignment="left" Caption="增補" Editor="text" FieldName="AddCountersigns" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="DeptCoordinationNO" ParentFieldName="DeptCoordinationNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem ID="DetailAdd" Icon="icon-add"  ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="930px" Title="會簽明細" DialogLeft="10px" DialogTop="150px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="DeptCoordinationCountersign" HorizontalColumnsCount="4" RemoteName="sDeptCoordinationMaster.DeptCoordinationMaster" OnLoadSuccess="DetailformLoadSucess" OnApply="dataFormDetail_OnApply" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" ReadOnly="True" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="DeptCoordinationNO" Format="" Span="1" Visible="False" Width="120" MaxLength="0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽職稱" Editor="inforefval" EditorOptions="title:'選取職稱',panelWidth:350,remoteName:'sDeptCoordinationMaster.PetitionPosition',tableName:'PetitionPosition',columns:[{field:'GroupID',title:'職稱編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'GROUPNAME',title:'職稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'GroupID',textField:'GROUPNAME',valueFieldCaption:'職稱編號',textFieldCaption:'職稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignRole" Span="2" Width="200" ReadOnly="False" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽人員" Editor="inforefval" EditorOptions="title:'選取會簽人員',panelWidth:350,remoteName:'sDeptCoordinationMaster.PetitionList',tableName:'PetitionList',columns:[{field:'USERID',title:'人員編號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'USERNAME',title:'會簽人員',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[{field:'GroupID',value:'row[CountersignRole]'}],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'會簽人員編號',textFieldCaption:'會簽人員',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignEmp" Format="" Span="2" Width="200" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="承辦詢問建議" Editor="textarea" EditorOptions="height:40" FieldName="Description" Format="" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" Span="4" Width="780" Visible="True" />                            
                             <JQTools:JQFormColumn Alignment="left" Caption="會簽者回覆" Editor="textarea" EditorOptions="height:60" FieldName="MangReply" MaxLength="1024" NewRow="False" PlaceHolder="最多可填寫1024字元!" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="780" />
                            <JQTools:JQFormColumn Alignment="left" Caption="附件檔名" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/DeptCoordination',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" FieldName="Attachment" Format="" Span="4" Visible="True" Width="300" />
                             <JQTools:JQFormColumn Alignment="left" Caption="增補會簽" Editor="text" FieldName="AddCountersigns" ReadOnly="False" Visible="False" Width="80" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽日期" Editor="text" FieldName="CountersignDate" Format="" Visible="False" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                             <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="DeptCoordinationNO" ParentFieldName="DeptCoordinationNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue=" 自動編號" FieldName="DeptCoordinationNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="ApplyEmpName" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Subject" RemoteMethod="True" ValidateMessage="請填寫主旨!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Description" RemoteMethod="True" ValidateMessage="請填寫內容!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignRole" RemoteMethod="True" ValidateMessage="會簽職稱不可空白!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignEmp" RemoteMethod="True" ValidateMessage="會簽人員不可空白!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <asp:Panel ID="dgpanel" runat="server">                    
                    <br />
                    <br />
                    <JQTools:JQDataGrid ID="PlusApproveList" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="PlusApprove" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sDeptCoordinationMaster.DeptCoordinationMaster" RowNumbers="True" Title="加簽意見" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="加簽者" Editor="text" FieldName="USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="加簽意見" Editor="text" FieldName="REMARK" FormatScript="ShowDetailGrid" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="680">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="附檔" Editor="text" FieldName="ATTACHMENTS" FormatScript="FlowdownloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="DeptCoordinationNO" Editor="text" FieldName="DeptCoordinationNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="LISTID" Editor="text" FieldName="LISTID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="DeptCoordinationNO" ParentFieldName="DeptCoordinationNO" />
                        </RelationColumns>
                    </JQTools:JQDataGrid>
                    <br />
                </asp:Panel>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
