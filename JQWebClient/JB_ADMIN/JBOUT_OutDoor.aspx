<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBOUT_OutDoor.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
       $(document).ready(function () {
            //將費用項目Caption顏色改為紫色
            var flagIDs = ['#dataFormMasterMileFee', '#dataFormMasterTrafficFee', '#dataFormMasterParkingFee', '#dataFormMasterHighWayFee', '#dataFormMasterTotalFee', '#dataFormMasterOilFee'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "lightyellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
           //調整QUERYPANEL
            var dgid = $('#JQDataGridOutDoorList');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            //if (queryPanel)
            //    $(queryPanel).panel('resize', { width: 480 });
            $('.infosysbutton-q', '#JQDataGridOutDoorList').closest('td').attr({ 'align': 'middle' });
            var StaTime = $('#dataFormMasterStaTime').closest('td');
            var EndTime = $('#dataFormMasterEndTime').closest('td').children();
            StaTime.append(' - ').append(EndTime)
            var StaMile = $('#dataFormMasterStaMile').closest('td');
            var EndMile = $('#dataFormMasterEndMile').closest('td').children();
            StaMile.append(' - ').append(EndMile)
            $("#dataFormMasterFromHYGate1").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate1").combobox('setWhere', "HYID=0");
            $("#dataFormMasterFromHYGate2").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate2").combobox('setWhere', "HYID=0");
            $("#dataFormMasterFromHYGate3").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate3").combobox('setWhere', "HYID=0");
            $("#dataFormMasterFromHYGate4").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate4").combobox('setWhere', "HYID=0");
            $("#dataFormMasterFromHYGate5").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate5").combobox('setWhere', "HYID=0");
            $("#dataFormMasterFromHYGate6").combobox('setWhere', "HYID=0");
            $("#dataFormMasterToHYGate6").combobox('setWhere', "HYID=0");
            //外出日期選擇時,
            $("#dataFormMasterOutDate").datebox({
                onSelect: function () {
                    CheckdaysBefore(); //檢查最晚申請日期
                    CheckLeadDays();   //檢查借車預借天數
                    GetOilRateByOutDate();
                    CalTotaLFee();
                }
            });
            var Goal = $('#dataFormMasterGoal').closest('td');
            Goal.append('  ＊請輸入縣市,鄉鎮名稱如台北市,中壢...')
            var OutNotes = $('#dataFormMasterOutNotes').closest('td');
            OutNotes.append('  ＊當實際行程有變動時,請在此說明')
            var EmpTogether = $('#dataFormMasterEmpTogether').closest('td');
            EmpTogether.append('  ＊請輸入隨行人員姓名')
            //增加外出記錄與
            var dfApplyEmpID = $('#dataFormMasterApplyDate').closest('td');
            dfApplyEmpID.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                var UserID = getClientInfo("UserID");
                var FiltStr = "ApplyEmpID = '" + UserID+"'";
                $("#JQDataGridOutDoorList").datagrid('setWhere', FiltStr);
                openForm('#JQDialogOutDoorList', {}, "", 'dialog');
                return true;
            }).linkbutton({ text: '外出記錄' }));
         });
        //檢查是否超過時限
        function CheckdaysBefore() {
            var parameters = Request.getQueryStringByName("P1");
            var daysBefore = $("#dataFormMasterdaysBefore").val();
            var Dt = new Date();
            var d1 = $("#dataFormMasterOutDate").datebox('getValue');
            var d2 = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate()
            var Wdays = GetPeriodWorkdays(d1, d2);
            if ($('#dataFormMasterTotalFee').val() > 0) {
                if (daysBefore - Wdays < 0) {
                    alert('注意!!,申請日期超過申請期限,無法申請,日期回設為今日');
                    if (parameters != 'Fee') {
                        $('#dataFormMasterOutDate').datebox('setValue', dd);
                    }
                    return false;
                }
            }
            return true;
        }
        function DataformLoadSucess() {
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var IsOverlap = $('#dataFormMasterIsOverlap').val();
            var UserID = getClientInfo("UserID");
            //申請
            if (getEditMode($("#dataFormMaster")) == 'inserted' || parameters == "ADM") {
                $("#dataFormMasterTargets").combobox('setWhere', '1=1');
                HideFieldApply();
                GetUserOrgNOs();
                var sGroupID = '1071061'; //高專/外勞客服組
                $('#dataFormMasterIsEmpGroupID').checkbox('setValue', CheckApplyEmpIsGroupID(sGroupID));
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EMPLOYEEID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
                //過濾可使用的設備(公務車)
                var Org_NO = $("#dataFormMasterApplyOrg_NO").combobox("getValue");
                var FiltStr = "(LimitDepts='' or LimitDepts like '%" + Org_NO + "%' OR LimitDepts is null)";
                $("#dataFormMasterDeviceItemsID").combobox('setWhere', FiltStr);
                var UserOrgNO = GetUserOrgNO(UserID);
                $("#dataFormMasterOrg_NOParent").val(UserOrgNO);
            }
            else {
                //申請人Combobox 設為不可選取
                $("#dataFormMasterApplyEmpID").combobox('disable');
            }
            //填寫費用
            if (parameters == "Fee") {
                //取得部門對應的成本中心-----------------------------------------------------
                var rowData = $("#dataFormMasterApplyOrg_NO").combobox('getSelectItem');
                var filter = "CostCenterID in (" + rowData.CostCenterID + ")";
                $("#dataFormMasterCostCenterID").combobox("setWhere", filter);
                //取得部門對應的成本中心-----------------------------------------------------
                disableFieldApply();
                $("#dataFormMasterOutDate").datebox('disable');
                var IsMileage = $('#dataFormMasterIsMileage').val();
                var IsEtag = $('#dataFormMasterIsEtag').val();
                if (IsEtag == "false") {
                    var FormName = '#dataFormMaster';
                    var HideFieldName = ['HYID1', 'FromHYGate1', 'ToHYGate1', 'Kms1', 'HYID2', 'FromHYGate2', 'ToHYGate2', 'Kms2', 'HYID3', 'FromHYGate3', 'ToHYGate3',
                    'Kms3', 'HYID4', 'FromHYGate4', 'ToHYGate4', 'Kms4', 'HYID5', 'FromHYGate5', 'ToHYGate5', 'Kms5', 'HYID6', 'FromHYGate6', 'ToHYGate6', 'Kms6'];
                    $.each(HideFieldName, function (index, fieldName) {
                        $(FormName + fieldName).closest('td').prev('td').hide();
                        $(FormName + fieldName).closest('td').hide();
                    });
                }
                if (IsMileage == "true") {
                    $("#dataFormMasterStaMile").attr('disabled', false);
                    $("#dataFormMasterEndMile").attr('disabled', false);
                    $('#dataFormMasterMileage').attr('disabled', true);
                    $('#dataFormMasterMileage').val(0);
                    $('#dataFormMasterMileFee').val(0);
                }
                else {
                    $("#dataFormMasterStaMile").attr('disabled', true);
                    $("#dataFormMasterEndMile").attr('disabled', true);
                    $('#dataFormMasterMileage').attr('disabled', false);
                }
            }
            if (parameters == "MG") {
                HideETagFields();
            }
            if (parameters == "GM") {
                HideETagFields();
            }
            GetYMData();
            GetOilRateByOutDate();
            GetdaysBefore();
            var Org_NO = $('#dataFormMasterOrg_NO').val().toString();
            var Org = $('#dataFormMasterORG').val()
         }
        //當
        function HideETagFields() {
                 var vHY1 = $("#dataFormMasterHYID1").combobox('getValue');
                 if (vHY1 == "" || vHY1 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID1', 'FromHYGate1', 'ToHYGate1', 'Kms1'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
                 var vHY2 = $("#dataFormMasterHYID2").combobox('getValue');
                 if (vHY2 == "" || vHY2 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID2', 'FromHYGate2', 'ToHYGate2', 'Kms2'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
                 var vHY3 = $("#dataFormMasterHYID3").combobox('getValue');
                 if (vHY3 == "" || vHY3 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID3', 'FromHYGate3', 'ToHYGate3', 'Kms3'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
                 var vHY4 = $("#dataFormMasterHYID4").combobox('getValue');
                 if (vHY4 == "" || vHY4 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID4', 'FromHYGate4', 'ToHYGate4', 'Kms4'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
                 var vHY5 = $("#dataFormMasterHYID5").combobox('getValue');
                 if (vHY5 == "" || vHY5 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID5', 'FromHYGate5', 'ToHYGate5', 'Kms5'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
                 var vHY6 = $("#dataFormMasterHYID6").combobox('getValue');
                 if (vHY6 == "" || vHY6 == undefined) {
                     var FormName = '#dataFormMaster';
                     var HideFieldName = ['HYID6', 'FromHYGate6', 'ToHYGate6', 'Kms6'];
                     $.each(HideFieldName, function (index, fieldName) {
                         $(FormName + fieldName).closest('td').prev('td').hide();
                         $(FormName + fieldName).closest('td').hide();
                     });
                 }
        }
        function CheckApplyEmpIsGroupID(GroupID) {
            var ApplyEmpID = $("#dataFormMasterApplyEmpID").combobox('getValue');
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',
                data: "mode=method&method=" + "CheckApplyEmpIsGroupID" + "&parameters=" + ApplyEmpID + "," + GroupID,
                cache: false,
                async: false,
                success: function (data) {
                    cnt = data;
                }
            });
            if ((cnt == "Y")) {
                return true;
            }
            else {
                return false;
            }
        }
        function HideFieldApply() {
            $("#dataFormMasterTargets").combobox('enable');
            var FormName = '#dataFormMaster';
            var HideFieldName = ['StaMile', 'EndMile', 'Mileage', 'MileFee', 'TrafficFee', 'ParkingFee','OilFee', 'HighWayFee', 'TotalFee','CostCenterID',
            'HYID1', 'FromHYGate1', 'ToHYGate1','HYID1', 'FromHYGate1', 'ToHYGate1', 'Kms1', 'Kms1', 'HYID2', 'FromHYGate2', 'ToHYGate2', 'Kms2', 'HYID3', 'FromHYGate3', 'ToHYGate3',
            'Kms3', 'HYID4', 'FromHYGate4', 'ToHYGate4', 'Kms4', 'HYID5', 'FromHYGate5', 'ToHYGate5', 'Kms5', 'HYID6', 'FromHYGate6', 'ToHYGate6', 'Kms6', 'OutNotes'];
            $.each(HideFieldName, function (index, fieldName) {
                $(FormName + fieldName).closest('td').prev('td').hide();
                $(FormName + fieldName).closest('td').hide();
            });
        }
        function disableFieldApply() {
            //$("#dataFormMasterOutDate").datebox("disable");
            $('#dataFormMasterMileage').attr('disabled', true);
            $('#dataFormMasterStaTime').attr('disabled', false);
            $('#dataFormMasterEndTime').attr('disabled', false);
            $('#dataFormMasterOutLine').attr('disabled', false);
            $('#dataFormMasterGoal').attr('disabled', false);
        }
        function SetFilt() {
            var rowData = $("#dataFormMasterApplyEmpID").combobox('getSelectItem');
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        function dataFormMasterOnApply() {
            var parameters = Request.getQueryStringByName("P1");
            var dataFormMasterDeviceItemsID = $("#dataFormMasterDeviceItemsID").combobox('getValue');
            var IsOverlap = $('#dataFormMasterIsOverlap').val();
            if (dataFormMasterDeviceItemsID == "" || dataFormMasterDeviceItemsID == undefined) {
                alert('注意!!,未選取交通工具,請選取!!');
                $("#dataFormMasterDeviceItemsID").focus();
                return false;
            }
            var dataFormMasterTargets = $("#dataFormMasterTargets").combobox('getValue');
            if (dataFormMasterTargets == "" || dataFormMasterTargets == undefined) {
                alert('注意!!,未選取目標客戶,請選取!!');
                $("#dataFormMasterTargets").focus();
                return false;
            }
            if ((getEditMode($("#dataFormMaster")) != 'inserted') && (parameters == 'Fee')) {
                var CostCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
                if (CostCenterID == "" || CostCenterID == undefined) {
                    alert('注意!!,未選取成本中心,請選取!!');
                    $("#dataFormMasterCostCenterID").focus();
                    return false;
                }
                else {
                    var Departure = $("#dataFormMasterDeparture").combobox('getValue');
                    if  (Departure == "" || Departure == undefined) {
                        alert('注意!!,未選取出發地點,請選取!!');
                        $("#dataFormMasterDeparture").focus();
                        return false;
                    }
                 }
           }
           if (parameters == 'Fee') {
                var OutDate = $("#dataFormMasterOutDate").datebox('getValue');
                var DtOutDate=Date.parse((OutDate));
                var DtCurrDate = Date.parse((new Date()).toDateString());
                if (DtCurrDate < DtOutDate) {
                    alert('注意!!,外出日期必需小於等於今天日期才能申請里程費用送出!!');
                    $("#dataFormMasterOutDate").focus();
                    return false
                };
                var StaMile = $("#dataFormMasterStaMile").val();
                var EndMile = $("#dataFormMasterEndMile").val();
                var IsMileage = $('#dataFormMasterIsMileage').val();
                if (IsMileage == 'true' && (StaMile == 0 || EndMile == 0 || (EndMile-StaMile)<1)) {
                    alert('注意!!,起迄公里數輸入有誤,請修正');
                    return false;
                }
                return true;
                //return CheckdaysBefore();
            }
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var kk = CheckEmpOutOverlap();
                if (kk ==false) {
                    return false;
                }
            }
            //當設備需檢查時間重疊時,檢查時間重疊
            if (IsOverlap == 'true' && getEditMode($("#dataFormMaster")) == 'inserted') {
                var kk = CheckDeviceOverlap();
                if (kk == false) {
                   return false;
                }
            }
            return CheckLeadDays();
          }
       function CalMileage() {
            var StaMile = $("#dataFormMasterStaMile").val();
            var EndMile = $("#dataFormMasterEndMile").val();
            if (EndMile < StaMile && EndMile!=0) {
                alert('注意!!輸入錯誤,終止里程數不可小於起始里程數!!')
                var EndMile = $("#dataFormMasterEndMile").val(0);
                return false;
            }
        }
        //HYID 選擇改變時連動HYGate
        function GetHyGate(rowData) {
            var idstr = this.id;
            var len = idstr.length;
            var num = idstr.substr(len - 1, 1);
            var fromstr = "#" + "dataFormMasterFromHYGate" + num
            var tostr = "#" + "dataFormMasterToHYGate" + num
            $(fromstr).combobox('setValue', "");
            $(fromstr).combobox('setWhere', "HYID=" + rowData.HYID);
            $(tostr).combobox('setValue', "");
            $(tostr).combobox('setWhere', "HYID=" + rowData.HYID);
        }
        function OnBlurMileage() {
            var DeviceItemsID = $("#dataFormMasterDeviceItemsID").combobox('getValue');
            if (DeviceItemsID.length == 0) {
                alert('請選取交通工具');
                return false;
            }
            var Mileage = $("#dataFormMasterMileage").val();
            var CarRate = $("#dataFormMasterOilRateCar").val();
            var MotoRate = $("#dataFormMasterOilRateMoto").val();
            if (DeviceItemsID == 1)
                var Fee = Math.round((Mileage * MotoRate));
            else {
                if (DeviceItemsID == 2)
                    var Fee = Math.round((Mileage * CarRate));
                else
                    var Fee = 0;
            }
            var MileFee = $("#dataFormMasterMileFee").val(Fee);
            CalTotaLFee();
        }
        //check 時間格式如 : 0800 或 0830
        function checkTimeFormat(val) {
            return $.jbIsTimeFormat(val);
        }
        //check 計薪年月格式如 : 201401 或 201412
        function checkYearMonthFormat(val) {
            return $.jbIsYearMonthStr(val)
        }
        //取得申請控管天數
        function GetdaysBefore() {
            GetYMData();
        }
        function GetYMData() {
            var outdate = $('#dataFormMasterOutDate').combo('textbox').val()
            var UserID=getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', 
                data: "mode=method&method=" + "GetOilRateByOutDate" + "&parameters=" + outdate +","+UserID , 
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterOilRateCar").val(rows[0].NCar);
                        $("#dataFormMasterOilRateMoto").val(rows[0].NMoto);
                        $("#dataFormMasterdaysBefore").val(rows[0].daysBefore);
                        $("#dataFormMasterEtagRate").val(rows[0].eTagRate);
                        $("#dataFormMasterOrg_NO").val(rows[0].ORG_NO);
                        
                    }
                }
            }
            );
        }
        //取得期間工作天數
        function GetPeriodWorkdays(sdate,edate) {
            var Wdays = 0 ;
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', 
                data: "mode=method&method=" + "GetPeriodWorkdays" + "&parameters=" + sdate + "," + edate + "," + UserID,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        Wdays = rows[0].Workdays;
                     }
                }
            }
            )
            return Wdays;
        }
        function IsTimeFormat(Str) {
            var r = Str.match(/(?!0000)^(?:([01]\d|2[0-3])([0-5]\d)|2400)$/);
            if (r == null) return false;
            else return true;
        }
        //依外出日期取得最新油資補助
        function GetOilRateByOutDate() {
            GetYMData();
            var NCar = $("#dataFormMasterOilRateCar").val();
            var NMoto = $("#dataFormMasterOilRateMoto").val();
            var NeTag = $("#dataFormMasterEtagRate").val();
            var oilDesc = "小客車每公里:" + NCar + '    機車每公里：' + NMoto + '    eTag每公里：' + NeTag;
            $("#dataFormMasterOilDescr").val(oilDesc);
        }
        //傳入 起迄匣道取得公里數
        function GetFromToFee() {
            var cnt;
            var idstr = this.id;
            var len = idstr.length;
            var num = idstr.substr(len - 1, 1);
            var hyy = "#" + "dataFormMasterHYID" + num
            var from = "#" + "dataFormMasterFromHYGate" + num
            var to = "#" + "dataFormMasterToHYGate" + num
            var kms = "#" + "dataFormMasterKms" + num
            var hy = $(hyy).combobox('getValue');
            var fromgate = $(from).combobox('getValue');
            var togate = $(to).combobox('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', //連接的Server端，command
                data: "mode=method&method=" + "GetFromToFee" + "&parameters=" + hy + "," + fromgate + "," + togate, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    cnt = $.parseJSON(data);
                }
            });
            $(kms).val(cnt);
            if  (cnt> 0) {
            CalHighWayFee();
            }
        }
        function CalHighWayFee() {
             var ETagRate = $("#dataFormMasterEtagRate").val();
             Kms1 = parseFloat($("#dataFormMasterKms1").val());
             Kms2 = parseFloat($("#dataFormMasterKms2").val());
             Kms3 = parseFloat($("#dataFormMasterKms3").val());
             Kms4 = parseFloat($("#dataFormMasterKms4").val());
             Kms5 = parseFloat($("#dataFormMasterKms5").val());
             Kms6 = parseFloat($("#dataFormMasterKms6").val());
             HYFee = Math.round(((Kms1 + Kms2 + Kms3 + Kms4 + Kms5) * ETagRate));
             $("#dataFormMasterHighWayFee").val(HYFee);
             CalTotaLFee();
        }
        function CalTotaLFee() {
            var MileFee = $("#dataFormMasterMileFee").val();
            var TrafficFee = $("#dataFormMasterTrafficFee").val();
            var ParkingFee = $("#dataFormMasterParkingFee").val();
            var OilFee = $("#dataFormMasterOilFee").val();
            var HighWayFee = $("#dataFormMasterHighWayFee").val();
            if (MileFee.length == 0)
                MileFee = 0;
            else
                MileFee = parseInt(MileFee);
            if (TrafficFee.length == 0)
                TrafficFee = 0;
            else
                TrafficFee = parseInt(TrafficFee);

            if (ParkingFee.length == 0)
                ParkingFee = 0;
            else
                ParkingFee = parseInt(ParkingFee);

            if (OilFee.length == 0)
                OilFee = 0;
            else
                OilFee = parseInt(OilFee);
            if (HighWayFee.length == 0)
                HighWayFee = 0;
            else
                HighWayFee = parseInt(HighWayFee);
            TotalFee = MileFee + TrafficFee + ParkingFee + OilFee + HighWayFee;
            $("#dataFormMasterTotalFee").val(TotalFee);
        }
        //檢查車輛預借天數
        function CheckLeadDays() {
            var Leaddays = $('#dataFormMasterLeadDays').val();
            var DeviceItemsName = $('#dataFormMasterDeviceItemsName').val();
            if (Leaddays > 0) {
                var d1 = $("#dataFormMasterApplyDate").datebox('getValue');
                var d2 = $("#dataFormMasterOutDate").datebox('getValue');
                var Wdays = GetPeriodWorkdays(d1, d2);
                if ((Leaddays - Wdays) < 0) {
                      alert('注意,(' + DeviceItemsName + ')預借天數為' + Leaddays + '天,(週休二日不計)已超過期限,無法借用');
                      $("#dataFormMasterDeviceItemsID").combobox('setValue', "");
                      return false;
                }
            }
            return true;
        }
        //當選取DeviceItems時執行此檢查
        function DeviceItemsCheck(rowData) {
            $('#dataFormMasterIsMileage').val(rowData.IsMileage);
            $('#dataFormMasterIsEtag').val(rowData.IsEtag);
            $('#dataFormMasterLeadDays').val(rowData.Leaddays);
            $('#dataFormMasterDeviceItemsName').val(rowData.DeviceItemsName);
            $('#dataFormMasterIsOverlap').val(rowData.IsOverlap);
            CheckLeadDays();
            if (rowData.IsOverlap == 1) {
               CheckDeviceOverlap();
            }
        }
        //檢查設備日期時間是否重疊
        function CheckDeviceOverlap() {
            var DeviceItemID = $("#dataFormMasterDeviceItemsID").combobox('getValue');//取得當前主檔中選中的那個Data
            var Date =  $("#dataFormMasterOutDate").datebox('getValue');
            var Stime = $('#dataFormMasterStaTime').val();
            var Etime = $('#dataFormMasterEndTime').val();
            var UserID = getClientInfo("UserID");
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', //連接的Server端，command
                data: "mode=method&method=" + "CheckDeviceOverlap" + "&parameters=" + DeviceItemID + "," + Date+","+Stime+","+Etime+","+UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {                   
                    cnt = data;
                }
            });
            if ((cnt == "N")) {
                return true;
            }
            else {
                alert('此公務車起迄時間已被 ' + cnt + ' 行程所佔用');
                $("#dataFormMasterDeviceItemsID").combobox('setValue', '');
                return false;
            }
        }
        function CheckEmpOutOverlap() {
            var ApplyEmpID = $("#dataFormMasterApplyEmpID").combobox('getValue');
            var Date = $("#dataFormMasterOutDate").datebox('getValue');
            var Stime = $('#dataFormMasterStaTime').val();
            var Etime = $('#dataFormMasterEndTime').val();
            var UserID = getClientInfo("UserID");
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', 
                data: "mode=method&method=" + "CheckEmpOutOverlap" + "&parameters=" + ApplyEmpID + "," + Date + "," + Stime + "," + Etime+","+ UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    cnt = data;
                }
            });
            if ((cnt == "N")) {
                return true;
            }
            else {
                alert('您此行程時間區間('+Stime+'-'+Etime+')與 ' + cnt + ' 行程重疊！！');
                return false;
            }
        }
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "外出申請單";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', 
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, 
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'JQDataGridOutDoorList') {
                var result = [];
                var aVal = '';
                var bVal = '';
                aVal = $("#dataFormMasterApplyEmpID").combobox('getValue');
                if (aVal != '')
                    result.push("ApplyEmpID = '" + aVal + "'");
                aVal = $('#OutDateS_Query').datebox('getValue');
                bVal = $('#OutDateE_Query').datebox('getValue');
                if (aVal != '' && bVal != '')
                    result.push("OutDate between '" + aVal + "' and '" + bVal + "'");
                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }
        function GetUserOrgNO(UserID) {
            var _return = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sOutDoor.OutDoor',
                data: "mode=method&method=" + "GetUserOrgNO" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length == 1) {
                        _return = rows[0].UserOrgNO;
                    }
                }
            })
            return _return;
        }
      
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sOutDoor.OutDoor" runat="server" AutoApply="True"
                DataMember="OutDoor" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="OutDoorID" Editor="text" FieldName="OutDoorID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="DeviceItemsID" Editor="numberbox" FieldName="DeviceItemsID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OutDateSta" Editor="datebox" FieldName="OutDateSta" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OutDateEnd" Editor="datebox" FieldName="OutDateEnd" Format="" Width="120" />
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
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="外出申請" Width="840px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="OutDoor" HorizontalColumnsCount="6" RemoteName="sOutDoor.OutDoor" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="DataformLoadSucess" OnApply="dataFormMasterOnApply" OnCancel="CloseDataForm" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="text" FieldName="OutDoorID" Format="" Width="97" ReadOnly="True" Span="1" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sOutDoor.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" ReadOnly="False" Span="1" Visible="True" Width="157" />
                        <JQTools:JQFormColumn Alignment="left" Caption="油資補助" Editor="text" FieldName="OilDescr" ReadOnly="True" Span="6" Width="383" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人" Editor="infocombobox" FieldName="ApplyEmpID" Format="" Width="100" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sOutDoor.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" Span="1" ReadOnly="False" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Width="100" ReadOnly="False" Span="5" Visible="True" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="外出日期" Editor="datebox" FieldName="OutDate" Span="1" Width="100" OnBlur="" ReadOnly="False" Visible="True" Format="yyyy/mm/dd" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起迄時間" Editor="text" FieldName="StaTime" Format="" Width="41" Span="1" ReadOnly="False" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="EndTime" Format="" Span="5" Width="41" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="外出主旨" Editor="textarea" EditorOptions="height:40" FieldName="OutLine" Format="" Span="6" Width="300" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="出發地點" Editor="infocombobox" EditorOptions="items:[{value:'自家點',text:'自家點',selected:'false'},{value:'桃園總公司',text:'桃園總公司',selected:'false'},{value:'台北分公司',text:'台北分公司',selected:'false'},{value:'新竹分公司',text:'新竹分公司',selected:'false'},{value:'中壢辦公室',text:'中壢辦公室',selected:'false'},{value:'大園辦事處',text:'大園辦事處',selected:'false'},{value:'平鎮幸福村',text:'平鎮幸福村',selected:'false'},{value:'傑誠宿舍',text:'傑誠宿舍',selected:'false'},{value:'長安宿舍',text:'長安宿舍',selected:'false'},{value:'台茂宿舍',text:'台茂宿舍',selected:'false'},{value:'國瑞宿舍',text:'國瑞宿舍',selected:'false'},{value:'新榮宿舍',text:'新榮宿舍',selected:'false'},{value:'吉祥宿舍',text:'吉祥宿舍',selected:'false'},{value:'碁富彰化廠',text:'碁富彰化廠',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Departure" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="目標客戶" Editor="infocombobox" FieldName="Targets" Format="" Span="6" Width="304" EditorOptions="valueField:'Targets',textField:'Targets',remoteName:'sOutDoor.targets',tableName:'targets',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:true,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="外出地點" Editor="text" FieldName="Goal" Format="" Span="6" Width="300" ReadOnly="False" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交通工具" Editor="infocombobox" FieldName="DeviceItemsID" Format="" Width="304" EditorOptions="valueField:'DeviceItemsID',textField:'DeviceNameLoc',remoteName:'sOutDoor.Device',tableName:'Device',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:DeviceItemsCheck,panelHeight:200" Span="6" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="隨行人員" Editor="text" FieldName="EmpTogether" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程異動" Editor="textarea" EditorOptions="height:30" FieldName="OutNotes" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="425" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起迄公里數" Editor="numberbox" FieldName="StaMile" Format="" Width="70" Span="1" ReadOnly="True" OnBlur="CalMileage" MaxLength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="里程數" Editor="numberbox" FieldName="Mileage" Format="" Span="1" Width="77" OnBlur="OnBlurMileage" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="numberbox" FieldName="EndMile" Format="" Width="70" Span="4" ReadOnly="True" OnBlur="CalMileage" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OutCome" Editor="text" FieldName="OutCome" Format="" ReadOnly="False" Span="0" Visible="False" Width="180" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="油資補助" Editor="text" FieldName="MileFee" Width="70" OnBlur="" ReadOnly="True" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交通費" Editor="numberbox" FieldName="TrafficFee" Format="" Width="77" Span="1" OnBlur="CalTotaLFee" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="停車費" Editor="numberbox" FieldName="ParkingFee" ReadOnly="False" Width="77" Format="" OnBlur="CalTotaLFee" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="eTag" Editor="numberbox" FieldName="HighWayFee" Format="" Width="77" ReadOnly="True" Span="2" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="加油油資" Editor="numberbox" FieldName="OilFee" Width="50" ReadOnly="False" Span="2" OnBlur="CalTotaLFee" Visible="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總費用" Editor="numberbox" FieldName="TotalFee" ReadOnly="True" Span="1" Width="77" MaxLength="0" Format="" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sOutDoor.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" ReadOnly="False" Span="6" Width="170" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程1國道" Editor="infocombobox" FieldName="HYID1" Format="" Width="80" ReadOnly="False" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate1" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate1" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="text" FieldName="Kms1" Format="" Width="50" ReadOnly="True" Span="3" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程2國道" Editor="infocombobox" FieldName="HYID2" Format="" Width="80" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate2" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate2" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="Kms2" Format="" Width="50" ReadOnly="True" Span="3" MaxLength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程3國道" Editor="infocombobox" FieldName="HYID3" Format="" Width="80" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" ReadOnly="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate3" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate3" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" MaxLength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="Kms3" Format="" Width="50" ReadOnly="True" Span="3" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程4國道" Editor="infocombobox" FieldName="HYID4" Format="" Width="80" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" ReadOnly="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate4" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate4" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="Kms4" Format="" Width="50" ReadOnly="True" Span="3" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程5國道" Editor="infocombobox" FieldName="HYID5" Format="" Width="80" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" ReadOnly="False" Span="1" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate5" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate5" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" ReadOnly="False" Span="1" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="Kms5" Format="" Width="50" ReadOnly="True" Span="3" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="行程6國道" Editor="infocombobox" FieldName="HYID6" Format="" Width="80" EditorOptions="valueField:'HYID',textField:'HYName',remoteName:'sOutDoor.HY',tableName:'HY',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetHyGate,panelHeight:200" Visible="True" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起始閘道" Editor="infocombobox" FieldName="FromHYGate6" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" Visible="True" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="終止閘道" Editor="infocombobox" FieldName="ToHYGate6" Format="" Width="80" EditorOptions="valueField:'HYGate',textField:'HYGate',remoteName:'sOutDoor.HYGate',tableName:'HYGate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetFromToFee,panelHeight:200" Visible="True" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公里數" Editor="numberbox" FieldName="Kms6" Format="" MaxLength="0" Width="50" ReadOnly="True" Span="3" Visible="True" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="汽車費率" Editor="numberbox" FieldName="OilRateCar" Format="" Width="40" Visible="False" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="機車費率" Editor="text" FieldName="OilRateMoto" Width="40" Span="1" ReadOnly="False" Visible="False" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EtagRate" Editor="text" FieldName="EtagRate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Visible="False" Width="180" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Width="180" Format="" Visible="False" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsMileage" Editor="text" FieldName="IsMileage" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsEtag" Editor="text" FieldName="IsEtag" ReadOnly="False" Span="6" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="daysBefore" Editor="text" FieldName="daysBefore" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LeadDays" Editor="text" FieldName="LeadDays" ReadOnly="False" Span="6" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DeviceItemsName" Editor="text" FieldName="DeviceItemsName" ReadOnly="False" Span="6" Width="80" Visible="False" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsOverlap" Editor="numberbox" FieldName="IsOverlap" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="False" Width="80" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" ReadOnly="False" Span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NO" Editor="text" FieldName="Org_NO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ORG" Editor="text" FieldName="ORG" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsEmpGroupID" Editor="checkbox" FieldName="IsEmpGroupID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" EditorOptions="on:1,off:0" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="OutDoorID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="OutDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0830" FieldName="StaTime" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1730" FieldName="EndTime" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Kms1" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Kms2" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Kms3" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Kms4" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Kms5" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="Kms6" RemoteMethod="True" DefaultValue="0" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="TrafficFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ParkingFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="桃園總公司" FieldName="Departure" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="HighWayFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="TotalFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="MileFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Mileage" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="StaMile" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="EndMile" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OilFee" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsEmpGroupID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="IsTimeFormat" CheckNull="True" FieldName="StaTime" RemoteMethod="False" ValidateType="None" ValidateMessage="輸入格式24小時制:時分(1010)" />
                        <JQTools:JQValidateColumn CheckMethod="IsTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateType="None" ValidateMessage="輸入格式24小時制:時分(1010)" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OutLine" RemoteMethod="True" ValidateType="None" />                      
                        
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Departure" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Targets" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Goal" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DeviceItemsID" RemoteMethod="True" ValidateType="None" />
                        
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            <div style="display: none;">
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="oilrate" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sOutDoor.oilrate" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="200px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="NCar" Editor="text" FieldName="NCar" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="NMoto" Editor="text" FieldName="NMoto" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataGrid>
            </div>
        </div>
         <JQTools:JQDialog ID="JQDialogOutDoorList" runat="server" DialogLeft="10px" DialogTop="30px" Title="外出紀錄列表" Width="980px" OnSubmited="" Closed="True" ShowSubmitDiv="False" Height="560px">
                 <JQTools:JQDataGrid ID="JQDataGridOutDoorList" runat="server" AlwaysClose="True" DataMember="OutDoorList" RemoteName="sOutDoor.OutDoorList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" OnUpdate="" Height="480px" Width="910px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="工號" Editor="text" FieldName="ApplyEmpID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外出日期" Editor="datebox" FieldName="OutDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外出時間" Editor="text" FieldName="OutTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="交通工具" Editor="text" FieldName="DeviceName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外出主旨" Editor="text" FieldName="OUTLine" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="里程數" Editor="text" FieldName="Mileage" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="里程金額" Editor="text" FieldName="MileFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="交通費" Editor="text" FieldName="TrafficFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="停車費" Editor="text" FieldName="ParkingFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="ETC" Editor="text" FieldName="HighWayFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="加油費" Editor="text" FieldName="OilFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="總費用" Editor="text" FieldName="TotalFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" Total="sum">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="狀態" Editor="text" FieldName="STATUS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                     <QueryColumns>
                         <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="OutDateS" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                         <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="OutDateE" Format="yyyy/mm/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="_today" />
                     </QueryColumns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
    </form>
</body>
</html>
