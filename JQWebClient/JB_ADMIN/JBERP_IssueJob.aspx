<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_IssueJob.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var SoftwareLink;//軟體安裝超連結
        var SoftwareLabel;
        var _ShowHideFields1 = ['BeginDate', 'PeriodDays', 'EndDate']; //當
        var _ShowHideFields2 = ['IsReset', 'ResetDate'];
        var _ShowHideFields3 = ['IsReset', 'ResetDate'];
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "#FFFAB3");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
                $("#dataFormMasterPlusApproveEmployeeID").closest("td").append("&nbsp;<font color='blue'> <<=需知會其他「負責職稱」人員，請選取</font>");
                $("#dataFormMasterIsTransfer").closest("td").append("&nbsp;<font color='blue'>勾選後，請填「負責職稱」和「需求項目」，再存檔(限轉一次)</font>");
                $("#dataFormMasterIsTransfer").click(function () {
                    if ($(this).is(":checked")) {
                        $("#dataFormMasterIssueBelongID").combobox("enable");//負責職稱
                        $("#dataFormMasterEstimationDate").datebox('setValue', '');
                        $("#dataFormMasterCloseDate").datebox('setValue', '');
                        $("#dataFormMasterEstimationDate").datebox({ disabled: true });
                        $("#dataFormMasterCloseDate").datebox({ disabled: true });
                        $("#dataFormMasterCloseDescr").attr("disabled", true)
                        $("#dataFormMasterCost").attr("disabled", true)
                        
                    } else {
                        $("#dataFormMasterIssueBelongID").combobox("disable");//負責職稱
                        $("#dataFormMasterEstimationDate").datebox({ disabled: false });
                        $("#dataFormMasterCloseDate").datebox({ disabled: false });
                        $("#dataFormMasterCloseDescr").attr("disabled", false)
                        $("#dataFormMasterCost").attr("disabled", false)
                    }
                });
                //$("#dataFormMasterORG_NO").closest("td").append("&nbsp;<font color='blue'>緊急程度與申請部門作為統計之用</font>");
                $("#dataFormMasterUrgentLevel").closest("td").prev("td").css({ "color": "red" });
                $("#dataFormMasterORG_NO").closest("td").prev("td").css({ "color": "red" });
                //SoftwareLink = $("<a>").attr({ 'href': '../JB_ADMIN/Files/EEP申請軟體清單與表格.xlsx', 'target': '_blank' }).text("下載格式");
                //SoftwareLabel = $("<font>").attr({'color':'blue'}).text("選擇軟體安裝，請附上檔案。");
                //$('#dataFormMasterIssueJobDate').closest('td').append(SoftwareLabel);
                //$('#dataFormMasterIssueTypeID').closest('td').append(SoftwareLink);
                //SoftwareLink.hide();
                //SoftwareLabel.hide();
                //定義起始日期c
                $("#dataFormMasterBeginDate").datebox({
                    onSelect: function () {
                        OnBlurPeriodsDays();
                    }
                });
                //定義IsReset的Onchange事件方法
                $("#dataFormMasterIsReset:checkbox").change(function () {
                    var IsReset = $("#dataFormMasterIsReset").checkbox('getValue');
                    if (IsReset == 1) {
                        var CurrentDate = getTodayDate();
                        $("#dataFormMasterResetDate").datebox('setValue', CurrentDate);
                    }
                    else {
                        $("#dataFormMasterResetDate").datebox('setValue', '');
                    }
                });
        });
        //當選取負責職稱時,重新設定需求項目(連動)OnSelectIssueBelongID
        function GetIssueType(rowData) {
            $("#dataFormMasterIssueTypeID").combobox('setValue', "");
            $("#dataFormMasterIssueTypeID").combobox('setWhere', "IssueBelongID=" + rowData.GROUPID);
            $("#dataFormMasterIssueTypeID").combobox("enable");
        }
        //需求項目選取時將文字帶給需求描述OnSelectIssueTypeID
        function IssueTypeIDOnSelect(row) {
            var mode1 = getEditMode($("#dataFormMaster"));
            var IssueType = row.IssueTypeID;
            if (mode1 == "inserted") {
                var IssueTypeID = $("#dataFormMasterIssueTypeID").combobox('getText');
                if (IssueTypeID != "") {
                    $('#dataFormMasterIssueDescr').val(row.IssueTypeDescr);
                }
                if (IssueType == 84 || IssueType == 85) {
                    ShowField(_ShowHideFields1);
                }
            }
            //需求項目選到"軟體安裝"
            if (row.IssueTypeName == '軟體安裝') {
                //SoftwareLink.show();//顯示軟體需求格式連結
                //SoftwareLabel.show();
                //加簽設為直屬主管
                GetBossID(getClientInfo("UserID"));
                $("#dataFormMasterPlusApproveEmployeeID").combobox('disable', true);//加簽鎖住
            } else {
                //SoftwareLink.hide();
                //SoftwareLabel.hide();
                $("#dataFormMasterPlusApproveEmployeeID").combobox('enable', true);
                $('#dataFormMasterPlusApproveEmployeeID').combobox('setValue','');
            }
        }
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //檢查Combo要選擇OnApplydataFormMaster
        function dataFormMasterOnApply() {
            var dataFormMasterIssueBelongID = $("#dataFormMasterIssueBelongID").combobox('getValue');
            if (dataFormMasterIssueBelongID == "" || dataFormMasterIssueBelongID == undefined) {
                alert('請選取負責職稱！');
                $("#dataFormMasterIssueBelongID").focus();
                return false;
            }
            var dataFormMasterIssueTypeID = $("#dataFormMasterIssueTypeID").combobox('getValue');
            if (dataFormMasterIssueTypeID == "" || dataFormMasterIssueTypeID == undefined) {
                alert('請選取需求項目！');
                $("#dataFormMasterIssueTypeID").focus();
                return false;
            }
            var IssueType = $("#dataFormMasterIssueTypeID").combobox('getValue');
            if (IssueType == 84 || IssueType == 85) {
                var PeriodDays = $("#dataFormMasterPeriodDays").val();
                var IssueTypeName = $("#dataFormMasterIssueTypeID").combobox('getText');
                var BeginDate = $("#dataFormMasterBeginDate").datebox('getValue');
                if (BeginDate == 0 || BeginDate == undefined) {
                    alert("提示!!當需求項目是[" + IssueTypeName + "]時,啟用日期需選填!!");
                    $("#dataFormMasterBeginDate").focus();
                    return false;
                }
                if (PeriodDays == 0 || PeriodDays == undefined) {
                    alert("提示!!當需求項目是[" + IssueTypeName + "]時,使用天數不得為0!!");
                    $("#dataFormMasterPeriodDays").focus();
                    return false;
                }
            }              
            var parameter = Request.getQueryStringByName("D");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var dataFormMasterIsTransfer = $("#dataFormMasterIsTransfer").checkbox('getValue');
            //檢查預計完成日(有可能只填預計完成日or若填實際完成日期則檢查不可大於今天)
            if (parameter == "EstimationDate" && mode == "2" && dataFormMasterIsTransfer ==false) {//管控"填預計完成日"此步驟的onApply
                var dataFormMasterEstimationDate = $("#dataFormMasterEstimationDate").datebox('getValue');
                if (dataFormMasterEstimationDate == "" || dataFormMasterEstimationDate == undefined) {
                    alert('請選取預計完成日！');
                    $("#dataFormMasterEstimationDate").datebox().focus();
                    return false;
                }
                var dataFormMasterCloseDate = $("#dataFormMasterCloseDate").datebox('getValue');
                var date = new Date();
                var now = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');
                if (dataFormMasterCloseDate != "" && dataFormMasterCloseDate > now) {
                    alert('實際完成日期不可大於今天！');
                    $("#dataFormMasterCloseDate").datebox().focus();
                    return false;
                }
            } else if (parameter == "EstimationDate" && mode == "2" && dataFormMasterIsTransfer == true) {//填轉單直稱
                //if (dataFormMasterEstimationDate != "" || dataFormMasterCloseDate != "") {
                //    alert('預計完成日、實際完成日期不可填！');
                //    return false;
                //}
                $("#dataFormMasterEstimationDate").datebox('setValue', '');
                $("#dataFormMasterCloseDate").datebox('setValue', '');
            }
            //檢查實際完成日期
            if (parameter == "CloseDate" && mode == "2") {//管控"填實際完成日"此步驟的onApply
                var dataFormMasterCloseDate = $("#dataFormMasterCloseDate").datebox('getValue');
                var date = new Date();
                var now = $.jbjob.Date.DateFormat(date, 'yyyy/MM/dd');
                if (dataFormMasterCloseDate == "" || dataFormMasterCloseDate == undefined) {
                    alert('請選取實際完成日期！');
                    $("#dataFormMasterCloseDate").datebox().focus();
                    return false;
                } else if (dataFormMasterCloseDate != "" && dataFormMasterCloseDate > now) {
                    alert('實際完成日期不可大於今天！');
                    $("#dataFormMasterCloseDate").datebox().focus();
                    return false;
                }
            }
            //檢查驗收日期
            if (parameter == "CheckDate" && mode == "2") {//管控"填驗收日期"此步驟的onApply
                var dataFormMasterCheckDate = $("#dataFormMasterCheckDate").datebox('getValue');
                var dataFormMasterCloseDate = $("#dataFormMasterCloseDate").datebox('getValue');
                if (dataFormMasterCheckDate == "" || dataFormMasterCheckDate == undefined) {
                    alert('請選取驗收日期！');
                    $("#dataFormMasterCheckDate").datebox().focus();
                    return false;
                } else if (dataFormMasterCheckDate < dataFormMasterCloseDate) {
                    alert('驗收日期需大於實際完成日期！');
                    $("#dataFormMasterCheckDate").datebox().focus();
                    return false;
                }
            }
           //新增時必填緊急程度
            if(getEditMode($("#dataFormMaster"))=='inserted' && $("#dataFormMasterUrgentLevel").combobox('getValue')==''){
                alert("請選擇緊急程度");
                return false;
            }
            //新增時必填申請部門
            if (getEditMode($("#dataFormMaster")) == 'inserted' && $("#dataFormMasterORG_NO").combobox('getValue') == '') {
                alert("請選擇申請部門");
                return false;
            }
        }
        //控制只有預計完成日,實際完成日期,驗收日期可以填寫(by網管或總務)OnLoadSuccessdataFormMaster
        function dataFormMasterOnLoadSucess() {
            //流程圖定義的參數
            var parameter = Request.getQueryStringByName("D");
            //mode=2(修改),mode=0(通知),mode=''(新增申請)
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var mode1 = getEditMode($("#dataFormMaster"));
            var IssueTypeID = $("#dataFormMasterIssueTypeID").combobox('getValue');
            if ((IssueTypeID == 'undefined' || IssueTypeID == '') ) {
                HiddenField(_ShowHideFields1);
            }
            else if (IssueTypeID == '84' || IssueTypeID == '85') {
                 ShowField(_ShowHideFields1);
            }
            else {
                HiddenField(_ShowHideFields1);
            }
            if (parameter == "Reset") {
                ShowField(_ShowHideFields2);
            }
            else {
                HiddenField(_ShowHideFields2);
            }
            var Table = $('table:first', '#dataFormMaster');//dataForm div 底下的第一個table
            var hideTable = $('#tbData');//隱藏的table
            if ((parameter == "" && mode == "" && mode1 == "inserted") || parameter == "Account" || ((parameter == "Apply" && mode == "2"))) {//申請(inserted),會計加簽,退回到一開始申請
                $("#dataFormMasterEstimationDate").closest('tr').appendTo(hideTable);//預定完成日期
                $("#dataFormMasterCloseDate").closest('tr').appendTo(hideTable);//實際完成日期
                $("#dataFormMasterCloseDescr").closest('tr').appendTo(hideTable);//處理描述
                $("#dataFormMasterCheckDate").closest('tr').appendTo(hideTable);//驗收日期
                $("#dataFormMasterCheckDescr").closest('tr').appendTo(hideTable);//驗收描述
                $("#dataFormMasterCheckScore").closest('tr').appendTo(hideTable);//滿意度
                $("#dataFormMasterCreateBy").closest('tr').appendTo(hideTable);//滿意度
                $("#dataFormMasterIsTransfer").closest('tr').appendTo(hideTable);//轉單
                $("#dataFormMasterIssueTypeID").combobox("disable");//需求項目
                $("#dataFormMasterCost").closest('tr').appendTo(hideTable);//花費時數
            } else if (parameter == "Notify" || mode == "0" || mode1 == "viewed") {//Notify 通知(0) viewed
                
                //$("#dataFormMasterPlusApproveEmployeeID").closest('tr').appendTo(hideTable);
            } else if (parameter != "" && mode == "2") {//修改(2)
                $("#dataFormMasterCheckDate").closest('tr').appendTo(hideTable);//驗收日期
                $("#dataFormMasterCheckDescr").closest('tr').appendTo(hideTable);//驗收描述
                $("#dataFormMasterCheckScore").closest('tr').appendTo(hideTable);//滿意度
                //共同不可選的控制項
                $('textarea', "#dataFormMaster").each(function () {//input,select,
                    this.disabled = 'disabled';
                });
                $("#dataFormMasterIssueJobDate").datebox("disable");//申請日期               
                $("#dataFormMasterIssueBelongID").combobox("disable");//負責職稱
                $("#dataFormMasterIssueTypeID").combobox("disable");//需求項目
                $("#dataFormMasterPlusApproveEmployeeID").combobox("disable");//會計加簽
                $("#dataFormMasterIsTransfer").attr("disabled",true);//轉單
                if (parameter == "EstimationDate" || parameter == "EstimationDate1") {//填預計完成日
                    $("#dataFormMasterCloseDate").datebox("textbox").focus();//實際完成日為焦點
                    $("#dataFormMasterCloseDescr").prop('disabled', false);//處理描述打開  
                    $("#dataFormMasterEstimationDate").datebox('setValue', $.jbjob.Date.DateFormat(new Date(), 'yyyy/MM/dd'));
                    $("#dataFormMasterServeEmployeeID").val(getClientInfo("UserID"));
                    $("#dataFormMasterIssueTypeID").combobox('enable');
                    $("#dataFormMasterIssueTypeID").combobox('setWhere', "IssueBelongID=" + $("#dataFormMasterIssueBelongID").combobox("getValue"));
                    $("#dataFormMasterIssueDescr").prop('disabled', false);
                    if (parameter == "EstimationDate") {
                        $("#dataFormMasterIsTransfer").attr("disabled",false);//轉單
                    }
                } if (parameter == "CloseDate") {//填實際完成日期
                    $("#dataFormMasterCloseDate").datebox("textbox").focus();//實際完成日期為焦點
                    $("#dataFormMasterCloseDescr").prop('disabled', false);//處理描述打開
                    $("#dataFormMasterEstimationDate").datebox("disable");//預計完成日不可選取  
                    $("#dataFormMasterCloseDate").datebox('setValue', $.jbjob.Date.DateFormat(new Date(), 'yyyy/MM/dd'));
                } else if (parameter == "CheckDate") {//填驗收日期
                    $("#dataFormMasterCheckDate").closest('tr').appendTo(Table);
                    $("#dataFormMasterCheckDescr").closest('tr').appendTo(Table);
                    $("#dataFormMasterCreateBy").closest('tr').appendTo(Table);
                    $("#dataFormMasterCheckScore").closest('tr').appendTo(Table);
                    $("#dataFormMasterCheckDate").datebox("textbox").focus();//驗收日期為焦點
                    //prop具有 true 和 false 两个属性的属性，如 checked, selected 或者 disabled 使用prop()，其他的使用 attr()
                    $("#dataFormMasterCheckDescr").prop('disabled', false);//驗收備註打開
                    $("#dataFormMasterCheckDescr").focus();//驗收備註為焦點
                    $("#dataFormMasterEstimationDate").datebox("disable");//預計完成日不可選取            
                    $("#dataFormMasterCloseDate").datebox("disable");//實際完成日期不可選取
                    $("#dataFormMasterCheckDate").datebox('setValue', $.jbjob.Date.DateFormat(new Date(), 'yyyy/MM/dd'));
                    $("#dataFormMasterCheckScore").options('enable');
                    $("#dataFormMasterCheckScore").options('setValue', 5);
                    $("#dataFormMasterCost").closest('tr').appendTo(hideTable);//花費時數
                }
            }
            if (parameter == "Apply" || mode1 == "inserted") {
                $("#dataFormMasterUrgentLevel").combobox('enable');
                $("#dataFormMasterORG_NO").combobox('enable');
                GetUserOrgNOs();
                $("#dataFormMasterIssueBelongID").combobox("setValue", "");
                $("#dataFormMasterIssueTypeID").combobox("setValue", "");
                $("#dataFormMasterPlusApproveEmployeeID").combobox("setValue", "");
            }
            else {
                $("#dataFormMasterUrgentLevel").combobox("disable");
                $("#dataFormMasterORG_NO").combobox("disable");
            }
            //if ($("#dataFormMasterIssueTypeID").combobox('getText') == '軟體安裝') {
            //    //SoftwareLabel.show();
            //} else {
            //    //SoftwareLabel.hide();
            //}
        }
       //顯示傳入fields陣列的欄位
        function ShowField(fields) {
            var FormName = '#dataFormMaster';
            $.each(fields, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').show();
                $(FormName + fieldName).closest('td').show();
            });
        }
        //隱藏傳入fields陣列的欄位
        function HiddenField(fields) {
            var FormName = '#dataFormMaster';
            $.each(fields, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            });
        }
        //申請部門，申請時會帶入使用者的部門
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sIssueJob.IssueJob', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterORG_NO").combobox('setValue', rows[0].OrgNO);
                        //$("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }
        //--工具--    
        function GetBossID(Userid) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sIssueJob.IssueJob', //連接的Server端，command
                data: "mode=method&method=" + "Call_funReturnEmpBossID" + "&parameters=" + Userid, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    $('#dataFormMasterPlusApproveEmployeeID').combobox('setValue',rows[0].BossID);
                }
            });
        }
        function OnBlurPeriodsDays() {
            var days = $("#dataFormMasterPeriodDays").val();
            var BeginDate = $("#dataFormMasterBeginDate").datebox('getValue');
            if (BeginDate != "" && BeginDate != undefined) {
                $("#dataFormMasterEndDate").datebox('setValue', getNewDate(BeginDate, days));
            }
        }
        //日期加天數得新日期
        function getNewDate(dateTemp, days) {
            var dateTemp = dateTemp.split("-");
            var nDate = new Date(dateTemp[1] + '-' + dateTemp[2] + '-' + dateTemp[0]); //轉換為MM-DD-YYYY格式    
            var millSeconds = Math.abs(nDate) + (days * 24 * 60 * 60 * 1000);
            var rDate = new Date(millSeconds);
            var year = rDate.getFullYear();
            var month = rDate.getMonth() + 1;
            if (month < 10) month = "0" + month;
            var date = rDate.getDate();
            if (date < 10) date = "0" + date;
            return (year + "/" + month + "/" + date);
        }
        //取得今天日期 格式:9999/99/99
        function getTodayDate() {
            var fullDate = new Date();
            var yyyy = fullDate.getFullYear();
            var MM = (fullDate.getMonth() + 1) >= 10 ? (fullDate.getMonth() + 1) : ("0" + (fullDate.getMonth() + 1));
            var dd = fullDate.getDate() < 10 ? ("0" + fullDate.getDate()) : fullDate.getDate();
            var today = yyyy + "/" + MM + "/" + dd;
            return today;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div style="display: none;">
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sIssueJob.IssueJob" runat="server" AutoApply="True"
                DataMember="IssueJob" Pagination="False" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="需求單號" Editor="text" FieldName="IssueJobNO" Format="" MaxLength="20" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="IssueJobDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="負責職稱" Editor="numberbox" FieldName="IssueBelongID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="需求項目" Editor="numberbox" FieldName="IssueTypeID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求描述" Editor="text" FieldName="IssueDescr" Format="" MaxLength="1000" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽核旗標" Editor="text" FieldName="Flowflag" Format="" MaxLength="1" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="EmployeeID" Format="" MaxLength="20" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="20" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimationDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDate" Editor="datebox" FieldName="CheckDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDescr" Editor="text" FieldName="CheckDescr" Format="" MaxLength="1000" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CloseDate" Editor="datebox" FieldName="CloseDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CloseDescr" Editor="text" FieldName="CloseDescr" Format="" MaxLength="1000" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="啟用日期" Editor="datebox" FieldName="StartDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="使用天數" Editor="numberbox" FieldName="PeriodDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="到期日期" Editor="datebox" FieldName="EndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
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
            </div>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="工作需求單" DialogLeft="10px" DialogTop="10px" Width="740px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="IssueJob" HorizontalColumnsCount="3" RemoteName="sIssueJob.IssueJob" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" OnApply="dataFormMasterOnApply" OnCancel="CloseDataForm" OnLoadSuccess="dataFormMasterOnLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="需求單號" Editor="text" FieldName="IssueJobNO" Format="" Width="90" ReadOnly="True" NewRow="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="IssueJobDate" Format="" Width="100" NewRow="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sIssueJob.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ORG_NO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="147" />
                        <JQTools:JQFormColumn Alignment="left" Caption="負責職稱" Editor="infocombobox" FieldName="IssueBelongID" Format="" Width="190" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sIssueJob.GROUPS',tableName:'GROUPS',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:GetIssueType,panelHeight:200" NewRow="True" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求項目" Editor="infocombobox" FieldName="IssueTypeID" Format="" Width="318" EditorOptions="valueField:'IssueTypeID',textField:'IssueTypeName',remoteName:'sIssueJob.IssueType',tableName:'IssueType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:IssueTypeIDOnSelect,panelHeight:200" maxlength="0" NewRow="False" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求描述" Editor="textarea" FieldName="IssueDescr" Format="" maxlength="0" Width="560" EditorOptions="height:100" NewRow="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="啟用日期" Editor="datebox" FieldName="BeginDate" NewRow="False" ReadOnly="False" Width="90" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="使用天數" Editor="numberbox" FieldName="PeriodDays" MaxLength="0" NewRow="False" Span="1" Width="70" OnBlur="OnBlurPeriodsDays" />
                        <JQTools:JQFormColumn Alignment="left" Caption="截止日期" Editor="datebox" FieldName="EndDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資安設定還原" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsReset" MaxLength="0" NewRow="False" Span="1" Visible="True" Width="15" OnBlur="IsResetOnBlur" />
                        <JQTools:JQFormColumn Alignment="left" Caption="回復還原日期" Editor="datebox" FieldName="ResetDate" NewRow="False" Width="90" maxlength="0" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急程度" Editor="infocombobox" FieldName="UrgentLevel" Width="90" maxlength="0" Span="1" NewRow="False" EditorOptions="items:[{value:'立即',text:'立即',selected:'false'},{value:'一天',text:'一天',selected:'false'},{value:'三天',text:'三天',selected:'false'},{value:'一週',text:'一週',selected:'false'},{value:'二週',text:'二週',selected:'false'},{value:'三週',text:'三週',selected:'false'},{value:'不限',text:'不限',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" Visible="True" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加簽" Editor="infocombobox" FieldName="PlusApproveEmployeeID" maxlength="0" Span="2" Width="89" NewRow="False" Visible="True" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sIssueJob.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="轉單" Editor="checkbox" FieldName="IsTransfer" Span="3" Width="20" maxlength="0" NewRow="False" ReadOnly="False" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="center" Caption="預計完成日" Editor="datebox" FieldName="EstimationDate" Format="" maxlength="0" NewRow="True" Span="1" Width="90" Visible="True" />
                        <JQTools:JQFormColumn Alignment="center" Caption="實際完成日期" Editor="datebox" FieldName="CloseDate" NewRow="False" Width="90" maxlength="0" Span="2" Visible="True" ReadOnly="False" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="處理描述" Editor="textarea" FieldName="CloseDescr" Format="" maxlength="500" Width="540" Visible="True" NewRow="False" Span="3" ReadOnly="False" EditorOptions="height:80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="花費時數" Editor="numberbox" FieldName="Cost" maxlength="0" Width="80" Visible="True" EditorOptions="precision:2" NewRow="False" Span="2" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="center" Caption="驗收日期" Editor="datebox" FieldName="CheckDate" Format="" Width="90" Visible="True" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收備註" Editor="textarea" FieldName="CheckDescr" Format="" Width="400" Visible="True" MaxLength="0" ReadOnly="False" NewRow="True" RowSpan="1" Span="2" EditorOptions="height:50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" ReadOnly="False" Width="180" Visible="False" Format="" maxlength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" ReadOnly="False" Width="180" Visible="False" maxlength="20" NewRow="False" RowSpan="1" Span="1" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="滿意度" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:5,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[{text:'1分',value:'1'},{text:'2分',value:'2'},{text:'3分',value:'3'},{text:'4分',value:'4'},{text:'5分',value:'5'}]" FieldName="CheckScore" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ServeEmployeeID" Editor="text" FieldName="ServeEmployeeID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="IssueJobNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="IssueJobDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="EmployeeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="不限" FieldName="UrgentLevel" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="BeginDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PeriodDays" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="EndDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="IssueJobDate" RemoteMethod="True" ValidateMessage="請選擇申請日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="IssueBelongID" RemoteMethod="True" ValidateMessage="請選擇負責職稱" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="IssueTypeID" RemoteMethod="True" ValidateMessage="請選擇需求項目" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <table id="tbData" style="width: 100%; display:none;"></table>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
