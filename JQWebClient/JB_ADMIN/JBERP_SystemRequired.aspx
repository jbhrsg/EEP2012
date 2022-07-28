<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SystemRequired.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
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

        function CompareDate(ApplyDate, EstimatedDate) {
            var arr = ApplyDate.split("/");
            var sdaten = new Date(arr[0], arr[1], arr[2]);
            var sdate = sdaten.getTime();

            var arrs = EstimatedDate.split("/");
            var edaten = new Date(arrs[0], arrs[1], arrs[2]);
            var edate = edaten.getTime();
            if (sdate >= edate) {
                return false;
            }
            else
                return true;
        }

        var FlomParameters = "";
        var GetCheckIdList = "";
        var _RequiredType = "";
        var AttachmentFlag = true;
        function DataformLoadSucess() {
            $('#JQDialog1').dialog('setTitle', '維運功能需求單');
            var _LogUserId = getClientInfo("UserID");
            FlomParameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var mode1 = getEditMode($("#dataFormMaster"));
            //alert("FlomParameters:" + FlomParameters + " mode:" + mode + " mode1:" + mode1);
            _RequiredType = $('#dataFormMasterRequiredType').options('getValue');
            //alert("需求頪別:" + _RequiredType);
            
            if (mode1 == "inserted" || FlomParameters == "Apply" || FlomParameters == "Mang" || FlomParameters == "President" || FlomParameters == "InforMang") {
                if (FlomParameters == "") {
                    GetUserOrgNOs();
                }
                if (FlomParameters != "InforMang"){
                    $("#dataFormMasterProjectLeader").closest("td").prev("td").hide();
                    $("#dataFormMasterProjectLeader").closest("td").hide();
                }
                
                ////系統需求類別為修改工程師不必填寫開發模組評估
                //if (_RequiredType == "P" || _RequiredType == "") {
                    
                //}
                $("#dataFormMasterDevelopTechnology").closest("td").prev("td").hide();
                $("#dataFormMasterDevelopTechnology").closest("td").hide();
                $("#dataFormMasterConfidential").closest("td").prev("td").hide();
                $("#dataFormMasterConfidential").closest("td").hide();
                $("#dataFormMasterIntegrity").closest("td").prev("td").hide();
                $("#dataFormMasterIntegrity").closest("td").hide();
                $("#dataFormMasterAvailability").closest("td").prev("td").hide();
                $("#dataFormMasterAvailability").closest("td").hide();
                $("#dataFormMasterEvaluationResult").closest("td").prev("td").hide();
                $("#dataFormMasterEvaluationResult").closest("td").hide();
                //$("#dataFormMasterPGEvaluateDescr").closest("td").prev("td").hide();
                //$("#dataFormMasterPGEvaluateDescr").closest("td").hide();

                //工程師預估時間
                $("#dataFormMasterRequiredCase").closest("td").prev("td").hide();
                $("#dataFormMasterRequiredCase").closest("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").prev("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").hide();
                $("#dataFormMasterCompledDate").closest("td").prev("td").hide();
                $("#dataFormMasterCompledDate").closest("td").hide();
                

                if (mode1 == "inserted" || FlomParameters == "Apply") {
                    $("#dataFormMasterChecker").closest("td").prev("td").hide();
                    $("#dataFormMasterChecker").closest("td").hide();
                }
                
                $("#dataFormMasterProjectLeaderDescr").closest("td").prev("td").hide();
                $("#dataFormMasterProjectLeaderDescr").closest("td").hide();
                //工程師系統測試
                $("#dataFormMasterPGTestTarget").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").hide();
                //驗收確認
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                //工程師上線發佈
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }
            
            if (FlomParameters == "Mang" || FlomParameters == "PGTest") {
                GetCheckId();
                //alert("人員名單");
                $("#dataFormMasterChecker").combobox('setWhere', "USERID IN (" + GetCheckIdList + ")");
            }
            if (FlomParameters == "Mang" || FlomParameters == "InforMang") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-需求部門主管');
                if (FlomParameters == "InforMang") {
                    $("#dataFormMasterChecker").combobox('disable', true);
                }
                $("#dataFormMasterRequiredCase").closest("td").prev("td").hide();
                $("#dataFormMasterRequiredCase").closest("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").prev("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").hide();
                $("#dataFormMasterCompledDate").closest("td").prev("td").hide();
                $("#dataFormMasterCompledDate").closest("td").hide();
                $("#dataFormMasterProjectLeaderDescr").closest("td").prev("td").hide();
                $("#dataFormMasterProjectLeaderDescr").closest("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }
            if (FlomParameters == "President") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-總經理簽核');
            }
            if (FlomParameters == "PGEvaluate") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-系統功能審查');
                $('#dataFormMasterProjectLeaderDescr').focus().css({ "background-color": "yellow" });
                $("#dataFormMasterChecker").combobox('disable', true);
                $("#dataFormMasterProjectLeader").combobox('disable', true);
                $("#dataFormMasterEstimatedDate").closest("td").prev("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").hide();               
                $("#dataFormMasterCompledDate").closest("td").prev("td").hide();
                $("#dataFormMasterCompledDate").closest("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }

            if (FlomParameters == "InforAudit") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-資訊中心主管審核');
                $("#dataFormMasterChecker").attr('readonly', true);
                $('#dataFormMasterChecker').attr('disabled', true);
                $('#dataFormMasterRequiredCase').next('span').find('input').attr('disabled', true);
                //$("#dataFormMasterProjectLeader").closest("td").prev("td").hide();
                //$("#dataFormMasterProjectLeader").closest("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").prev("td").hide();
                $("#dataFormMasterEstimatedDate").closest("td").hide();
                //系統需求類別為新專案
                if (_RequiredType == "A") {
                    $('#dataFormMasterDevelopTechnology').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterConfidential').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterIntegrity').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterAvailability').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterEvaluationResult').next('span').find('input').attr('disabled', true);
                    //$("#dataFormMasterPGEvaluateDescr").attr('readonly', true);
                    //$('#dataFormMasterPGEvaluateDescr').attr('disabled', true);
                }
                $("#dataFormMasterCompledDate").closest("td").prev("td").hide();
                $("#dataFormMasterCompledDate").closest("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }
            if (FlomParameters == "PG") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-工程師預計日期');
                $("#dataFormMasterChecker").combobox('disable', true);
                $('#dataFormMasterProjectLeader').combobox('disable', true);
                $('#dataFormMasterRequiredCase').next('span').find('input').attr('disabled', true);
                //系統需求類別為新專案
                if (_RequiredType == "A") {
                    $('#dataFormMasterDevelopTechnology').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterConfidential').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterIntegrity').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterAvailability').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterEvaluationResult').next('span').find('input').attr('disabled', true);
                    //$("#dataFormMasterPGEvaluateDescr").attr('readonly', true);
                    //$('#dataFormMasterPGEvaluateDescr').attr('disabled', true);
                }
                $("#dataFormMasterCompledDate").closest("td").prev("td").hide();
                $("#dataFormMasterCompledDate").closest("td").hide();
                $("#dataFormMasterProjectLeaderDescr").attr('readonly', true);
                $("#dataFormMasterProjectLeaderDescr").attr('disabled', true);
                $("#dataFormMasterPGTestTarget").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestTarget").closest("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").prev("td").hide();
                $("#dataFormMasterPGTestItems").closest("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment1').closest("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").prev("td").hide();
                $('#dataFormMasterPGTestAttachment2').closest("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }

            
            //人員異動時工程師可重新指定測試人員
            if (FlomParameters == "PGTest") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-工程師測試報告');
                $('#dataFormMasterPGTestTarget').focus().css({ "background-color": "yellow" });
                //$("#dataFormMasterChecker").combobox('disable', true);
                $('#dataFormMasterProjectLeader').combobox('disable', true);
                $('#dataFormMasterRequiredCase').next('span').find('input').attr('disabled', true);
                $("#dataFormMasterEstimatedDate").datebox('disable');
                //系統需求類別為新專案
                if (_RequiredType == "A") {
                    $('#dataFormMasterDevelopTechnology').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterConfidential').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterIntegrity').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterAvailability').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterEvaluationResult').next('span').find('input').attr('disabled', true);
                    //$("#dataFormMasterPGEvaluateDescr").attr('readonly', true);
                    //$('#dataFormMasterPGEvaluateDescr').attr('disabled', true);
                }
                $("#dataFormMasterProjectLeaderDescr").attr('readonly', true);
                $('#dataFormMasterProjectLeaderDescr').attr('disabled', true);
                $("#dataFormMasterExpectedBenefits").attr('readonly', true);
                $("#dataFormMasterExpectedBenefits").attr('disabled', true);
                $("#dataFormMasterCheckDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDescr").closest("td").hide();
                $("#dataFormMasterCheckDate").closest("td").prev("td").hide();
                $("#dataFormMasterCheckDate").closest("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterCheckAttachment").closest("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();

            }
            if (FlomParameters == "Checker") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-驗收確認');
                $('#dataFormMasterCheckDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                $('#dataFormMasterProjectLeader').combobox('disable', true);
                $('#dataFormMasterRequiredCase').next('span').find('input').attr('disabled', true);
                $("#dataFormMasterChecker").combobox('disable', true);
                $("#dataFormMasterEstimatedDate").datebox('disable');
                $("#dataFormMasterCompledDate").datebox('disable');
                //系統需求類別為新專案
                if (_RequiredType == "A") {
                    $('#dataFormMasterDevelopTechnology').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterConfidential').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterIntegrity').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterAvailability').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterEvaluationResult').next('span').find('input').attr('disabled', true);
                    //$("#dataFormMasterPGEvaluateDescr").attr('readonly', true);
                    //$('#dataFormMasterPGEvaluateDescr').attr('disabled', true);
                }
                $("#dataFormMasterProjectLeaderDescr").attr('readonly', true);
                $('#dataFormMasterProjectLeaderDescr').attr('disabled', true);
                $("#dataFormMasterExpectedBenefits").attr('readonly', true);
                $("#dataFormMasterExpectedBenefits").attr('disabled', true);
                $("#dataFormMasterPGTestTarget").attr('readonly', true);
                $("#dataFormMasterPGTestTarget").attr('disabled', true);
                $("#dataFormMasterPGTestItems").attr('readonly', true);
                $("#dataFormMasterPGTestItems").attr('disabled', true);
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").prev("td").hide();
                $('#dataFormMasterOnlineAttachment').closest("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").prev("td").hide();
                $("#dataFormMasterSysUpDate").closest("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").prev("td").hide();
                $("#dataFormMasterCloseDescr").closest("td").hide();
            }

            if (FlomParameters == "PGComplete") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-工程師完成');
                $('#dataFormMasterProjectLeader').combobox('disable', true);
                $('#dataFormMasterRequiredCase').next('span').find('input').attr('disabled', true);
                $("#dataFormMasterEstimatedDate").datebox('disable');
                //系統需求類別為新專案
                if (_RequiredType == "A") {
                    $('#dataFormMasterDevelopTechnology').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterConfidential').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterIntegrity').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterAvailability').next('span').find('input').attr('disabled', true);
                    $('#dataFormMasterEvaluationResult').next('span').find('input').attr('disabled', true);
                    //$("#dataFormMasterPGEvaluateDescr").attr('readonly', true);
                    //$('#dataFormMasterPGEvaluateDescr').attr('disabled', true);
                }
                $("#dataFormMasterProjectLeaderDescr").attr('readonly', true);
                $('#dataFormMasterProjectLeaderDescr').attr('disabled', true);
                $("#dataFormMasterPGTestTarget").attr('readonly', true);
                $("#dataFormMasterPGTestTarget").attr('disabled', true);
                $("#dataFormMasterPGTestItems").attr('readonly', true);
                $("#dataFormMasterPGTestItems").attr('disabled', true);
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterPGTestAttachment1').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterPGTestAttachment2').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $('#dataFormMasterChecker').combobox('disable', true);
                $("#dataFormMasterCheckDescr").attr('readonly', true);
                $("#dataFormMasterCheckDescr").attr('disabled', true);
                $("#dataFormMasterCheckDate").datebox('disable');
                $('#dataFormMasterCheckAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterCheckAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterCheckAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕                
                //if (_RequiredType == "P") {
                //    $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                //    $("#dataFormMasterOnlineDate").closest("td").hide();
                //    $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                //    $("#dataFormMasterBackupDescr").closest("td").hide();
                //    $("#dataFormMasterOnlineAttachment").closest("td").prev("td").hide();
                //    $("#dataFormMasterOnlineAttachment").closest("td").hide();
                //}
            }

            //相關模組資料不顯示
            if (_RequiredType == "P" || _RequiredType=="") {//mode1 == 'viewed' &&
                //模組上線不顯示
                $("#dataFormMasterOnlineDate").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineDate").closest("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").prev("td").hide();
                $("#dataFormMasterBackupDescr").closest("td").hide();
                $("#dataFormMasterOnlineAttachment").closest("td").prev("td").hide();
                $("#dataFormMasterOnlineAttachment").closest("td").hide();

                //系統需求類別為修改工程師不必填寫開發模組評估
                $("#dataFormMasterDevelopTechnology").closest("td").prev("td").hide();
                $("#dataFormMasterDevelopTechnology").closest("td").hide();
                $("#dataFormMasterConfidential").closest("td").prev("td").hide();
                $("#dataFormMasterConfidential").closest("td").hide();
                $("#dataFormMasterIntegrity").closest("td").prev("td").hide();
                $("#dataFormMasterIntegrity").closest("td").hide();
                $("#dataFormMasterAvailability").closest("td").prev("td").hide();
                $("#dataFormMasterAvailability").closest("td").hide();
                $("#dataFormMasterEvaluationResult").closest("td").prev("td").hide();
                $("#dataFormMasterEvaluationResult").closest("td").hide();
                //$("#dataFormMasterPGEvaluateDescr").closest("td").prev("td").hide();
                //$("#dataFormMasterPGEvaluateDescr").closest("td").hide();
            }

            if (FlomParameters == "InforOk") {
                $('#JQDialog1').dialog('setTitle', '維運功能需求單-資訊中心主確認');
            }

            var FormName = '#dataFormMasterAttachment';
            if (FlomParameters != "" && FlomParameters != "Apply") {
                $("#dataFormMasterApplyDate").datebox('disable');
                $('#dataFormMasterRequiredType').next('span').find('input').attr('disabled', true);
                $("#dataFormMasterSystemName").attr('readonly', true);
                $('#dataFormMasterSystemName').attr('disabled', true);
                $("#dataFormMasterDescription").attr('readonly', true);
                $('#dataFormMasterDescription').attr('disabled', true);
                $("#dataFormMasterExpectedBenefits").attr('readonly', true);
                $("#dataFormMasterExpectedBenefits").attr('disabled', true);
                for (var i = 0; i < 3; i++) {
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                    $(FormName + String(i + 1)).next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                }
            }

            if (AttachmentFlag) {
                //新增需求檔案下載
                for (var i = 0; i < 3; i++) {
                    var RawExcel = $('.info-fileUpload-value', $(FormName + String(i + 1)).next()).val();
                    if (RawExcel != '') {
                        var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + RawExcel }).html('[檔案下載]');
                        $(FormName + String(i + 1)).closest('td').append(link);
                    }
                }

                ////工程師驗收檔案下載
                for (var i = 0; i < 2; i++) {
                    var RawExcel = $('.info-fileUpload-value', $("#dataFormMasterPGTestAttachment" + String(i + 1)).next()).val();
                    if (RawExcel != '') {
                        var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + RawExcel }).html('[檔案下載]');
                        $("#dataFormMasterPGTestAttachment" + String(i + 1)).closest('td').append(link);
                    }
                }

                //線上系統上線完成畫面檔案下載
                var OnlineRawExcel = $('.info-fileUpload-value', $("#dataFormMasterOnlineAttachment").next()).val();
                if (OnlineRawExcel != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + OnlineRawExcel }).html('[檔案下載]');
                    $("#dataFormMasterOnlineAttachment").closest('td').append(link);
                }

                //驗收人員驗數檔案下載
                var CloseRawExcel = $('.info-fileUpload-value', $("#dataFormMasterCheckAttachment").next()).val();
                if (CloseRawExcel != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + CloseRawExcel }).html('[檔案下載]');
                    $("#dataFormMasterCheckAttachment").closest('td').append(link);
                }
                AttachmentFlag = false;
            }
        }

        //取得USER的部門代號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sSystemRequired.SystemRequired',
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

        //取得驗證人員
        function GetCheckId() {
            var _Org_NO = $('#dataFormMasterApplyOrg_NO').combobox('getValue');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sSystemRequired.SystemRequired',
                data: "mode=method&method=" + "GetCheckId" + "&parameters=" + _Org_NO,
                cache: false,
                async: false,
                success: function (data) {
                    //var rows = $.parseJSON(data);
                    if (data != false) {
                        GetCheckIdList = data;
                    }                    
                }
            }
            );
        }

        function MasterOnApply() {
            if (FlomParameters == "" || FlomParameters == "Apply") {
                var _RequiredTypea = $("#dataFormMasterRequiredType").options('getValue');
                if (_RequiredTypea == "") {
                    alert("請點選需求類別!");
                    return false;
                }
            }
            if (FlomParameters == "Mang") {
                var _ProjectLeader = $('#dataFormMasterChecker').combobox('getValue');
                if (_ProjectLeader == "") {
                    alert("請選擇驗收人員!");
                    $('#dataFormMasterChecker').next(".combo").find(".combo-text").focus().css({ "background-color": "yellow" });
                    return false;
                }
            }
            if (FlomParameters == "InforMang") {
                var _ProjectLeader = $('#dataFormMasterProjectLeader').combobox('getValue');
                if (_ProjectLeader == "") {
                    alert("請選擇工程師!");
                    $('#dataFormMasterProjectLeader').next(".combo").find(".combo-text").focus().css({ "background-color": "yellow" });
                    return false;
                }
            }
            
            if (FlomParameters == "PGEvaluate")  {
                if (_RequiredTypea == "A") {
                    var _DevelopTechnology = $('#dataFormMasterDevelopTechnology').options('getValue');
                    var _Confidential = $('#dataFormMasterConfidential').data("infooptions").text.val();
                    var _Integrity = $('#dataFormMasterIntegrity').data("infooptions").text.val();
                    var _Availability = $('#dataFormMasterAvailability').data("infooptions").text.val();
                    var _EvaluationResult = $('#dataFormMasterEvaluationResult').options('getValue');
                    if (_DevelopTechnology == "") {
                        alert("請點選開發技術審查!");
                        return false;
                    }
                    if (_Confidential == "") {
                        alert("請點選審查機敏性!");
                        return false;
                    }
                    if (_Integrity == "") {
                        alert("請點選審查完整性!");
                        return false;
                    }
                    if (_Availability == "") {
                        alert("請點選審查可行性!");
                        return false;
                    }
                    if (_EvaluationResult == "") {
                        alert("請點選評估審查總結!");
                        return false;
                    }
                }
                var _Case = $('#dataFormMasterRequiredCase').options('getValue');
                if (_Case == "") {
                    alert("請點選需求是否成案!");
                    return false;
                }
            }

            if (FlomParameters == "PG") {
                var _EstimatedDate = $('#dataFormMasterEstimatedDate').datebox('getValue');
                if (_EstimatedDate == "") {
                    alert("請填寫預計完成日!");
                    $('#dataFormMasterEstimatedDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                    return false;
                }
                var _ApplyDate = $('#dataFormMasterApplyDate').datebox('getValue');
                if (_ApplyDate > _EstimatedDate) {
                    alert('預計完成日小於申請日期，請確認!');
                    $('#dataFormMasterEstimatedDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                    return false;
                }
            }
            if (FlomParameters == "PGTest") {
                    var _CompledDate = $('#dataFormMasterCompledDate').datebox('getValue');
                    var _Checker = $("#dataFormMasterChecker").combobox('getValue');
                    if (_CompledDate == "") {
                        alert("請填寫實際完成日!");
                        $('#dataFormMasterCompledDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                        return false;
                    }
                    var _ApplyDate = $('#dataFormMasterApplyDate').datebox('getValue');
                    if (_ApplyDate > _CompledDate) {
                        alert('實際完成日小於申請日期，請確認!');
                        $('#dataFormMasterCompledDate').next(".combo").find(".combo-text").focus().css({ "background-color": "yellow" });
                        return false;
                    }
            }
            if (FlomParameters == "Checker") {
                var _CheckDate = $('#dataFormMasterCheckDate').datebox('getValue');
                if (_CheckDate == "") {
                    alert("請選擇驗收日!");
                    $('#dataFormMasterCheckDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                    return false;
                }
                var _CompledDate = $('#dataFormMasterCompledDate').datebox('getValue');
                if (_CompledDate > _CheckDate) {
                    alert('驗收日小於實際完成日，請確認!');
                    $('#dataFormMasterCheckDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                    return false;
                }
            }
            if (FlomParameters == "PGComplete" ) {
                var _OnlineDate = $('#dataFormMasterOnlineDate').datebox('getValue');
                if (_RequiredTypea == "A") {
                    if (_OnlineDate == "") {
                        alert("請填寫上線日期!");
                        $('#dataFormMasterOnlineDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                        return false;
                    }
                    var _CheckDate = $('#dataFormMasterCheckDate').datebox('getValue');
                    if (_CheckDate > _OnlineDate) {
                        alert('上線日期小於驗收日，請確認!');
                        $('#dataFormMasterOnlineDate').next(".combo").find(".combo-text").focus().css({ "background-color": "yellow" });
                        return false;
                    }
                }
                var _SysUpDate = $('#dataFormMasterSysUpDate').datebox('getValue');
                if (_SysUpDate == "") {
                    alert("請填寫更版日期!");
                    $('#dataFormMasterSysUpDate').next('span').find('input').focus().css({ "background-color": "yellow" });
                    return false;
                }
                if (_OnlineDate > _SysUpDate) {
                    alert('更版日期小於上線日期，請確認!');
                    $('#dataFormMasterSysUpDate').next(".combo").find(".combo-text").focus().css({ "background-color": "yellow" });
                    return false;
                }
                
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSystemRequired.SystemRequired" runat="server" AutoApply="True"
                DataMember="SystemRequired" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="系統需求編號" Editor="text" FieldName="SysRequiredNo" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者編號" Editor="infocombobox" FieldName="ApplyEmpID" Format="" MaxLength="20" Width="120" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="系統名稱" Editor="text" FieldName="SystemName" Format="" MaxLength="256" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求說明" Editor="text" FieldName="Description" Format="" MaxLength="2048" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExpectedBenefits" Editor="text" FieldName="ExpectedBenefits" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequiredType" Editor="text" FieldName="RequiredType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment1" Editor="text" FieldName="Attachment1" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment2" Editor="text" FieldName="Attachment2" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment3" Editor="text" FieldName="Attachment3" Format="" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProjectLeader" Editor="text" FieldName="ProjectLeader" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProjectLeaderDescr" Editor="text" FieldName="ProjectLeaderDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EstimatedDate" Editor="datebox" FieldName="EstimatedDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestTarget" Editor="text" FieldName="PGTestTarget" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestItems" Editor="text" FieldName="PGTestItems" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DevelopTechnology" Editor="text" FieldName="DevelopTechnology" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Confidential" Editor="text" FieldName="Confidential" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Integrity" Editor="text" FieldName="Integrity" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Availability" Editor="text" FieldName="Availability" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EvaluationResult" Editor="text" FieldName="EvaluationResult" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestAttachment1" Editor="text" FieldName="PGTestAttachment1" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestAttachment2" Editor="text" FieldName="PGTestAttachment2" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CompledDate" Editor="datebox" FieldName="CompledDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Checker" Editor="text" FieldName="Checker" Format="" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDescr" Editor="text" FieldName="CheckDescr" Format="" MaxLength="1024" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDate" Editor="datebox" FieldName="CheckDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckAttachment" Editor="text" FieldName="CheckAttachment" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OnlineDate" Editor="datebox" FieldName="OnlineDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BackupDescr" Editor="text" FieldName="BackupDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OnlineAttachment" Editor="text" FieldName="OnlineAttachment" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SysUpDate" Editor="datebox" FieldName="SysUpDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CloseDescr" Editor="text" FieldName="CloseDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" MaxLength="10" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="1" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="維運功能需求單" DialogLeft="10px" DialogTop="10px" Width="930px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SystemRequired" HorizontalColumnsCount="8" RemoteName="sSystemRequired.SystemRequired" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="MasterOnApply" OnLoadSuccess="DataformLoadSucess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="系統需求編號" Editor="text" FieldName="SysRequiredNo" Format="" maxlength="20" Width="100" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者編號" Editor="infocombobox" FieldName="ApplyEmpID" Format="" Width="100" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="20" ReadOnly="True" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" maxlength="20" Width="180" Visible="False" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="20" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sSystemRequired.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" maxlength="0" Width="90" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" maxlength="0" Width="180" Visible="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求類別" Editor="infooptions" FieldName="RequiredType" Format="" maxlength="0" Width="120" EditorOptions="title:'JQOptions',panelWidth:200,remoteName:'sSystemRequired.SystemType',tableName:'SystemType',valueField:'Code',textField:'CodeNmae',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統名稱" Editor="textarea" FieldName="SystemName" Format="" maxlength="256" Width="780" EditorOptions="height:20" PlaceHolder="最多可填寫256字元!" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求說明" Editor="textarea" FieldName="Description" Format="" maxlength="2048" Width="780" EditorOptions="height:45" PlaceHolder="最多可填寫2048字元!" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預期效益" Editor="textarea" FieldName="ExpectedBenefits" Format="" maxlength="0" Width="780" EditorOptions="height:30" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="infofileupload" FieldName="Attachment1" Format="" maxlength="50" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="infofileupload" FieldName="Attachment2" Format="" maxlength="50" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="infofileupload" FieldName="Attachment3" Format="" maxlength="50" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程師" Editor="infocombobox" FieldName="ProjectLeader" Format="" maxlength="20" Width="100" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.ProjectLeader',tableName:'ProjectLeader',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求成案" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'成案',value:'true'},{text:'不成案',value:'false'}]" FieldName="RequiredCase" MaxLength="0" ReadOnly="False" Span="2" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimatedDate" Format="" maxlength="0" Span="2" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際完成日" Editor="datebox" FieldName="CompledDate" Format="" maxlength="0" Span="2" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程師描述" Editor="textarea" FieldName="ProjectLeaderDescr" Format="" maxlength="1024" Width="780" EditorOptions="height:50" Span="8" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開發技術審查" Editor="infooptions" FieldName="DevelopTechnology" Format="" Width="500" EditorOptions="title:'JQOptions',panelWidth:500,remoteName:'sSystemRequired.DevelopTechnology',tableName:'DevelopTechnology',valueField:'Code',textField:'CodeNmae',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" Span="8" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查機敏性(C)" Editor="infooptions" FieldName="Confidential" Format="" maxlength="0" Width="60" EditorOptions="title:'機敏性(C) 模組數據涉及機敏程度可能遭受侵害威脅程度',panelWidth:350,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查完整性(I)" Editor="infooptions" FieldName="Integrity" Format="" maxlength="0" Width="60" EditorOptions="title:'完整性(I)模組功能其遭受算改成或人為疏失可能遭受侵害威脅程度',panelWidth:400,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查可行性(A)" Editor="infooptions" FieldName="Availability" Format="" maxlength="0" Width="60" EditorOptions="title:'可行性(A)模組因系統異常、故障等造成可用性威脅程度',panelWidth:350,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="評估審查總結" Editor="infooptions" FieldName="EvaluationResult" Format="" maxlength="0" Width="300" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'sSystemRequired.EvaluationResult',tableName:'EvaluationResult',valueField:'Code',textField:'CodeNmae',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:true,items:[]" Span="8" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試目標" Editor="textarea" EditorOptions="height:30" FieldName="PGTestTarget" MaxLength="1024" NewRow="False" ReadOnly="False" RowSpan="1" Span="8" Visible="True" Width="780" Format="" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試項目與結果" Editor="textarea" FieldName="PGTestItems" Format="" maxlength="1024" Width="780" EditorOptions="height:50" PlaceHolder="最多可填寫1024字元!" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試附件1" Editor="infofileupload" FieldName="PGTestAttachment1" Format="" maxlength="0" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試附件2" Editor="infofileupload" FieldName="PGTestAttachment2" Format="" maxlength="0" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收人員" Editor="infocombobox" FieldName="Checker" Format="" Width="90" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sSystemRequired.UsersGROUPS',tableName:'UsersGROUPS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" MaxLength="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收日" Editor="datebox" FieldName="CheckDate" Format="" maxlength="0" Width="90" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收說明" Editor="textarea" FieldName="CheckDescr" Format="" maxlength="1024" Width="780" Span="8" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收附件" Editor="infofileupload" FieldName="CheckAttachment" Format="" Width="280" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="8" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上線日期" Editor="datebox" FieldName="OnlineDate" Format="" maxlength="0" Width="90" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統上線完成畫面" Editor="infofileupload" FieldName="OnlineAttachment" Format="" Width="280" Span="4" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:true,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備份說明" Editor="textarea" FieldName="BackupDescr" Format="" maxlength="0" Width="780" EditorOptions="height:30" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="更版日期" Editor="datebox" FieldName="SysUpDate" Format="" maxlength="0" Width="90" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案說明" Editor="textarea" FieldName="CloseDescr" Format="" Width="780" Span="8" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" maxlength="10" Width="180" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Width="180" Visible="False" MaxLength="1" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="ApplyEmpName" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="SysRequiredNo" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SystemName" RemoteMethod="True" ValidateMessage="系統名稱不可空白!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Description" RemoteMethod="True" ValidateMessage="需求說明不可空白!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyDate" RemoteMethod="True" ValidateMessage="請填寫申請日期" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
