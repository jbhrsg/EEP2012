<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_TrainingForm.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            $('#dataFormMasterPlanPayDate').datebox({
                onSelect: function (date) {
                    ChkException();
                }
            }).combo('textbox').blur(function () {
                ChkException();
            });
            ////預計完成日&驗收日期合併為同TD顯示
            //var EstimationDate = $('#dataFormMasterEstimationDate').closest('td');
            //var CheckDate = $('#dataFormMasterCheckDate').closest('td').children();
            //EstimationDate.append(' 驗收日期').append(CheckDate);
        });
        //檢查是否非期限內付款,緊急付款
        function ChkException() {
            var mess = "";
            var bdt = $('#dataFormMasterPlanPayDate').combo('textbox').val();
            var dt = new Date(bdt);
            var days = dt.getDate()
            if ((days != 5) && (days != 25)) {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 1);
            }
            else {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 0);
            }
            var dd = new Date();
            var dc = (dt - dd) / 86400000;
            if (dc.toPrecision() <= 5) {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 1);
            }
            else {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 0);
            }
        }
        //當選取客戶時,重新設定付款條件與付款方式並改變預付日期
        function GetPayTerm(rowData) {
            $("#dataFormMasterPayTermID").combobox('setValue', rowData.PayTermID);
            $("#dataFormMasterPayTypeID").combobox('setValue', rowData.PayTypeID);
            var pp = $("#dataFormMasterPayTypeID").combobox('getValue'); //combobox 取值
            if (pp == 2) {
                $("#dataFormMasterIsRemit").checkbox('setValue', rowData.IsRemit);
                $("#dataFormMasterRemitType").combobox('setValue', '廠商付');
                $("#dataFormMasterRemit").val(30); //text給值
            }
            else {
                $("#dataFormMasterIsRemit").checkbox('setValue', 0);
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
            }
            var PayTerm = $("#dataFormMasterPayTermID").combobox('getText');
            var WorkDays = 5; //作業最短日期
            var tdate = GetPlanPayDate(PayTerm, WorkDays, $("#dataFormMasterApplyDate").datebox('getValue'));
            $('#dataFormMasterPlanPayDate').datebox('setValue', tdate);
        }
        //需匯款費與匯款費付款關係
        function CheckRemitType() {
            var kk = $("#dataFormMasterIsRemit").checkbox('getValue')
            if (kk != 1) {
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
            }
            else {
                $("#dataFormMasterRemitType").combobox('setValue', '廠商付');
                $("#dataFormMasterRemit").val(30); //text給值
            }
        }
        //當選取付款方式時,重新設定付款條件與付款方式並改變預付日期
        function GetPayType(rowData) {
            var pp = $("#dataFormMasterPayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2) {
                $("#dataFormMasterIsRemit").checkbox('setValue', 0);
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
            }
        }
        //當選取付款條件時,依付款條件改變預付日期
        function GetPayDateByTerm() {
            var PayTerm = $("#dataFormMasterPayTermID").combobox('getText');
            var tdate = GetPlanPayDate(PayTerm, 5, $("#dataFormMasterApplyDate").datebox('getValue'));
            $('#dataFormMasterPlanPayDate').datebox('setValue', tdate);
        }
        //傳入付款條件,作業天數,申請日期求得預付日期
        function GetPlanPayDate(PayTerm, WorkDays, PlanPayDate) {
            var now = new Date(PlanPayDate);
            if (PayTerm == "當月") {
                var newDate = DateAdd("d ", WorkDays, now).toLocaleDateString();
            }
            else {
                var days = parseInt(PayTerm, 10);
                var newDate = DateAdd("d ", days, now).toLocaleDateString();
            }
            var ttDate = GetPayDate(newDate);
            return ttDate;
        }
        //計算付款日
        function GetPayDate(PayDate) {
            var Dt = new Date(PayDate);
            var year = Dt.getFullYear();
            var month = Dt.getMonth() + 1;
            var Bt = Dt.toLocaleDateString();
            var Dt10 = new Date(year + '/' + month + '/' + '10').toLocaleDateString();
            var Dt25 = new Date(year + '/' + month + '/' + '25').toLocaleDateString();
            if (Bt <= Dt10) {
                return Dt10;
            }
            else
                if (Bt <= Dt25) {
                    return Dt25;
                }
                else {
                    return Dt25;
                }
        }
        function DateAdd(interval, number, date) {
            switch (interval) {
                case "y ": {
                    date.setFullYear(date.getFullYear() + number);
                    return date;
                    break;
                }
                case "q ": {
                    date.setMonth(date.getMonth() + number * 3);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMonth(date.getMonth() + number);
                    return date;
                    break;
                }
                case "w ": {
                    date.setDate(date.getDate() + number * 7);
                    return date;
                    break;
                }
                case "d ": {
                    date.setDate(date.getDate() + number);
                    return date;
                    break;
                }
                case "h ": {
                    date.setHours(date.getHours() + number);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMinutes(date.getMinutes() + number);
                    return date;
                    break;
                }
                case "s ": {
                    date.setSeconds(date.getSeconds() + number);
                    return date;
                    break;
                }
                default: {
                    date.setDate(d.getDate() + number);
                    return date;
                    break;
                }
            }
        }
        //檢查憑證號碼
        function CheckProofNO() {
            var ProofType = $("#dataFormMasterProofTypeID").combobox("getValue");
            var ProofNO = $("#dataFormMasterProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;

        }


        //檢查Combo要選擇
        function checkCombo() {
            var parameter = Request.getQueryStringByName("D");
            if (parameter == "Modify1") {
                //訓練類別,訓練類型,公司別,成本中心
                var sCheck = ['TrainTypeID', 'TrainKindID', 'CompanyID', 'CostCenterID'];           
                var sError="";
                $.each(sCheck, function (index, fieldName) {
                    var Check = $("#dataFormMaster" + fieldName).combobox('getValue');
                    if (Check == "" || Check == undefined) {                    
                        sError = "尚有未選擇項目！";
                    }
                });
                if (sError != "") {
                    alert(sError);
                    return false;
                } else return true;
            } else if (parameter == "Modify2") {
                var RequisitAmt = $("#dataFormMasterRequisitAmt").val();
                if (RequisitAmt <=0 || RequisitAmt == undefined) {
                    alert('請款金額不可小於0！');
                    $("#dataFormMasterRequisitAmt").focus();
                    return false;
                }
                var RequisitionNotes = $("#dataFormMasterRequisitionNotes").val();
                if (RequisitionNotes == "" || RequisitionNotes == undefined) {
                    alert('請款備註不可空白！');
                    $("#dataFormMasterRequisitionNotes").focus();
                    return false;
                }
                var PayTo = $("#dataFormMasterPayTo").refval("getValue");               
                if (PayTo == "" || PayTo == undefined) {
                    alert('請選擇受款人！');                   
                    return false;
                }                
            } else if (parameter == "Modify3") {
                var CloseDate = $("#dataFormMasterCloseDate").combo('textbox').val()
                if (CloseDate == "") {
                    alert('請選擇結案日期！');
                    return false;
                }
            }
        }
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //控制出現承辦的資料
        function ControlModify() {
            //流程圖定義的參數
            var parameter = Request.getQueryStringByName("D");
            //mode=2(新增申請),mode=0(審核,已結案)
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var FormName = '#dataFormMaster';

            //預期效益前加文字
            $("#dataFormMasterExpectBenefit").closest('td').prepend($('<div>', { style: 'color:red;' }).html('★預期效益內容請提及實際具體可達成的課後績效'));


            //新增申請或承辦初審 把承辦人員資料(請款欄位)隱藏
            if ((parameter == "") || parameter == "Modify1" ) {
                //已結案日期       
                var CloseDate = $("#dataFormMasterCloseDate").combo('textbox').val()
                //未結案不顯示,已結案則顯示
                if (CloseDate == "") {
                    //申請補助,請款金額,憑據號碼,報名人數,報名備註,請款依據,檢附憑證,請款備註,受款人,付款方式,預付日期,付款條件,緊急付款,非付款日付款,付款備註,需匯款費,匯款費付款,匯費金額,結案日期
                    var HideFieldName = ['IsApplyHelp', 'RequisitAmt', 'ProofNO', 'TrainFinalCount', 'SignupNotes', 'RequisitionTypeID', 'ProofTypeID', 'RequisitionNotes', 'PayTo', 'PayTypeID', 'PlanPayDate', 'PayTermID', 'IsUrgentPay', 'IsNotPayDate', 'PayToNotes', 'IsRemit', 'RemitType', 'Remit', 'CloseDate'];

                    $.each(HideFieldName, function (index, fieldName) {
                        $(FormName + fieldName).closest('td').prev('td').hide();
                        $(FormName + fieldName).closest('td').hide();
                    });
                    //審核時顯示申請補助
                    if (mode == "0") {
                        //申請補助disable属性顯示                     
                        $("#dataFormMasterIsApplyHelp").closest('td').prev('td').show();
                        $("#dataFormMasterIsApplyHelp").closest('td').show();
                    }
                }
               
            } 
            if (parameter != "") {
                //鎖textarea型態欄位
                $('textarea', "#dataFormMaster").each(function () {//input,select,
                    this.disabled = 'disabled';
                });
                //訓練類別不可選取
                $("#dataFormMasterTrainTypeID").datebox("disable");
                //訓練類型不可選取
                $("#dataFormMasterTrainKindID").combobox("disable");
                //公司別不可選取
                $("#dataFormMasterCompanyID").combobox("disable");
                //成本中心不可選取
                $("#dataFormMasterCostCenterID").combobox("disable");
                //申請日期不可選取
                $("#dataFormMasterApplyDate").datebox("disable");

                //課程名稱,上課人數,訓練單位,課程費用,上課地點,課程時數,上課日期
                var HideFieldName = ['CourseName', 'TrainHeadCount', 'TrainOrg', 'TrainFee', 'TrainLocation', 'TrainHours', 'TrainDate'];
                //申請人員資料不可編輯
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).attr("disabled", "disabled");
                });

                //承辦人員初審
                if (parameter == "Modify1") {
                    //申請補助disable属性顯示                      
                    $("#dataFormMasterIsApplyHelp").closest('td').prev('td').show();
                    $("#dataFormMasterIsApplyHelp").closest('td').show();
                    alert("提醒您，請注意此申請是否需要申請補助。");
                } else if (parameter == "Modify2") {//承辦請款報名      
                    //申請補助不可編輯
                    $("#dataFormMasterIsApplyHelp").attr("disabled", "disabled");
                    //報名備註disable属性删除
                    $("#dataFormMasterSignupNotes").removeAttr("disabled");
                    //請款備註disable属性删除
                    $("#dataFormMasterRequisitionNotes").removeAttr("disabled");
                    //付款備註disable属性删除
                    $("#dataFormMasterPayToNotes").removeAttr("disabled");
                    //結案日期隱藏
                    $("#dataFormMasterCloseDate").closest('td').prev('td').hide();
                    $("#dataFormMasterCloseDate").closest('td').hide();
                } else if (parameter == "Modify3") {//承辦結案(輸日結案日期)
                    //申請補助不可編輯
                    $("#dataFormMasterIsApplyHelp").attr("disabled", "disabled");
                    //請款依據不可選取
                    $("#dataFormMasterRequisitionTypeID").combobox("disable");
                    //檢附憑證不可選取
                    $("#dataFormMasterProofTypeID").combobox("disable");                   
                    //付款方式不可選取
                    $("#dataFormMasterPayTypeID").combobox("disable");
                    //預付日期不可選取
                    $("#dataFormMasterPlanPayDate").datebox("disable");
                    //付款條件不可選取
                    $("#dataFormMasterPayTermID").datebox("disable");

                    $("#dataFormMasterPayTo").data("inforefval").refval.find("span.icon-view").hide(); //受款人refval放大鏡鈕隱藏 

                    //請款金額,憑據號碼,報名人數
                    var HideFieldName = ['RequisitAmt', 'ProofNO', 'TrainFinalCount'];
                    //請款報名資料不可編輯
                    $.each(HideFieldName, function (index, fieldName) {
                        $(FormName + fieldName).attr("disabled", "disabled");
                    });
                }
            }

        }
                                
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sTrainingForm.TrainingForm" runat="server" AutoApply="True"
                DataMember="TrainingForm" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="JBERP_TrainingForm">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainingFormID" Editor="text" FieldName="TrainingFormID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseName" Editor="text" FieldName="CourseName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainTypeID" Editor="numberbox" FieldName="TrainTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainKindID" Editor="numberbox" FieldName="TrainKindID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainEmpList" Editor="text" FieldName="TrainEmpList" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainOrg" Editor="text" FieldName="TrainOrg" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainLocation" Editor="text" FieldName="TrainLocation" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainStdDate" Editor="datebox" FieldName="TrainStdDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainEndDate" Editor="datebox" FieldName="TrainEndDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainHours" Editor="numberbox" FieldName="TrainHours" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainFee" Editor="numberbox" FieldName="TrainFee" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainHeadCount" Editor="numberbox" FieldName="TrainHeadCount" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OutLine" Editor="text" FieldName="OutLine" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyNotes" Editor="text" FieldName="ApplyNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsApplyHelp" Editor="text" FieldName="IsApplyHelp" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="教育訓練申請單" DialogLeft="30px" DialogTop="10px" Width="710px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="TrainingForm" HorizontalColumnsCount="3" RemoteName="sTrainingForm.TrainingForm" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" OnApply="checkCombo" OnCancel="CloseDataForm" OnLoadSuccess="ControlModify" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="表單編號" Editor="text" FieldName="TrainingFormID" Format="" Width="120" ReadOnly="True" Visible="False" NewRow="False"  />
                        <JQTools:JQFormColumn Alignment="left" Caption="訓練類別" Editor="infocombobox" FieldName="TrainTypeID" Format="" Width="150" NewRow="True" EditorOptions="valueField:'TrainTypeID',textField:'TrainTypeName',remoteName:'sTrainingForm.infoTrainType',tableName:'infoTrainType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訓練類型" Editor="infocombobox" FieldName="TrainKindID" Format="" Width="150" EditorOptions="valueField:'TrainKindID',textField:'TrainKindName',remoteName:'sTrainingForm.infoTrainKind',tableName:'infoTrainKind',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請補助" Editor="checkbox" FieldName="IsApplyHelp" Format="" maxlength="0" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" NewRow="False" ReadOnly="False" Visible="True" Width="150" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:250" FieldName="CostCenterID" Format="" maxlength="0" Width="150" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="85" NewRow="False" Span="1"  />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" Format="" Width="380" NewRow="True" Span="2" Visible="True"/>                       
                         <JQTools:JQFormColumn Alignment="left" Caption="上課人數" Editor="numberbox" FieldName="TrainHeadCount" Format="" Width="85" maxlength="0" NewRow="False" Span="1" />    
                        <JQTools:JQFormColumn Alignment="left" Caption="參訓學員" Editor="textarea" FieldName="TrainEmpList" Format="" Width="540" NewRow="True" Span="3" />                    
                        <JQTools:JQFormColumn Alignment="left" Caption="訓練單位" Editor="text" FieldName="TrainOrg" Format="" Width="380" NewRow="True" Span="2" maxlength="0"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="課程總費用" Editor="text" FieldName="TrainFee" Format="" Width="85" NewRow="False" Span="1"  />
                        
                        <JQTools:JQFormColumn Alignment="left" Caption="上課地點" Editor="text" FieldName="TrainLocation" Format="" Width="380" NewRow="True" Span="2" maxlength="0"/>                        
                       <JQTools:JQFormColumn Alignment="left" Caption="課程時數" Editor="numberbox" FieldName="TrainHours" Format="" Width="85" EditorOptions="precision:1" maxlength="0" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上課日期" Editor="text" FieldName="TrainDate" Format="" Width="540" NewRow="True" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程大綱" Editor="textarea" FieldName="OutLine" Format="" Width="540" EditorOptions="height:60" NewRow="True" Span="3" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訓練目的" Editor="textarea" EditorOptions="height:60" FieldName="TrainPurpose" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="540" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預期效益" Editor="textarea" EditorOptions="height:60" FieldName="ExpectBenefit" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="540" />
                        <JQTools:JQFormColumn Alignment="left" Caption="其他備註" Editor="textarea" FieldName="ApplyNotes" Format="" Width="540" NewRow="True" Span="3" EditorOptions="height:40" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />                        
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" Span="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" Format="" Span="1" Width="130" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報名人數" Editor="numberbox" FieldName="TrainFinalCount" Width="80" NewRow="False" Span="1" Visible="True" MaxLength="0" ReadOnly="False" RowSpan="1" />                        

                        <JQTools:JQFormColumn Alignment="left" Caption="報名備註" Editor="textarea" FieldName="SignupNotes" NewRow="True" Width="540" Span="3" />

                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" Format="" Width="133" OnBlur="" maxlength="0" NewRow="False" span="1" />

                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" Format="" maxlength="0" span="3" Width="540" NewRow="True" ReadOnly="False" />

                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'受款人篩選',panelWidth:580,remoteName:'sRequisition.Vendor',tableName:'Vendor',columns:[{field:'VendTypeName',title:'廠商類別',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'供應商簡稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'選取',cacheRelationText:false,checkData:false,showValueAndText:false,onSelect:GetPayTerm,selectOnly:true" FieldName="PayTo" Format="" maxlength="0" Width="130" ReadOnly="False" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisition.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayType,panelHeight:200" FieldName="PayTypeID" Format="" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" FieldName="PlanPayDate" Width="85" Format="yyyy/mm/dd" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:false"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisition.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:GetPayDateByTerm,panelHeight:200" FieldName="PayTermID" Format="" Width="133" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" EditorOptions="height:40" FieldName="PayToNotes" Format="" maxlength="0" Span="3" Width="540" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需匯款費" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRemit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" OnBlur="CheckRemitType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款費付款" Editor="infocombobox" EditorOptions="items:[{value:'廠商付',text:'廠商付',selected:'false'},{value:'公司付',text:'公司付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RemitType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />

                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" FieldName="CloseDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />

                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="TrainingFormID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
<%--                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CloseDate" RemoteMethod="True" />--%>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TrainTypeID" RemoteMethod="True" ValidateMessage="請選擇訓練類別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TrainKindID" RemoteMethod="True" ValidateMessage="請選擇訓練類型" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyDate" RemoteMethod="True" ValidateMessage="請選擇申請日期" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CostCenterID" RemoteMethod="True" ValidateMessage="請選擇成本中心" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CourseName" RemoteMethod="True" ValidateMessage="請填寫課程名稱" ValidateType="None" />
                     

                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ExpectBenefit" RemoteMethod="True" ValidateMessage="請填謝預期效益" ValidateType="None" />
                     

                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
