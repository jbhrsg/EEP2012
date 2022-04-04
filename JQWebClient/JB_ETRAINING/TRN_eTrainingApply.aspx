<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_eTrainingApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
            var TermStartDate = $('#dataFormMasterTermStartDate').closest('td');
            var TermEndDate = $('#dataFormMasterTermEndDate').closest('td').children();
            TermStartDate.append(' - ').append(TermEndDate);
            var CourseStartHour = $('#dataFormMasterCourseStartHour').closest('td');
            var CourseEndHour = $('#dataFormMasterCourseEndHour').closest('td').children();
            CourseStartHour.append(' - ').append(CourseEndHour);
            var TotalHours = $('#dataFormMasterTotalHours').closest('td');
            TotalHours.append(' 小時')
            var Tuition = $('#dataFormMasterTuition').closest('td');
            Tuition.append(' 元')
            var IsDeclaration = $('#dataFormMasterIsDeclaration').closest('td');
            IsDeclaration.append(' 我已閱讀並接受上述內容')
            $('<br/><span id="t1" style=\"color:Red\" >[個資使用同意聲明]：本人同意公司使用本人之個人資料(身份證字號、出生年月日)<br/>作為對外訓練課程之報名表及製作相關證照等使用。</span>').insertAfter($('#dataFormMasterDescription', '#dataFormMaster'));
        })
        function dataFormMasterOnLoadSucess() {
            var StudentID = getClientInfo("UserID");
            var parameters = Request.getQueryStringByName("P1");
            if (parameters == "Apply") {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['ApplyID'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " StudentID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterStudentID').combobox('setWhere', whereStr);
                GetApplyStudentInfo(StudentID);
            }
            //$('#dataFormMasterIsDeclaration').hide()
        }
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //取得申請員工資訊
        function GetApplyStudentInfo(StudentID) {
            var ApplyID = 1;
            var Type = 2;   
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sTrainingApply.CourseApply',
                data: "mode=method&method=" + "GetApplyStudentInfo" + "&parameters=" + StudentID + "," + ApplyID + "," + Type, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#dataFormMasterArea').val(rows[0].DeptName);
                        $('#dataFormMasterCCID').val(rows[0].CCID);
                        $('#dataFormMasterStudentTitleID').combobox('setValue',rows[0].StudentTitleID);
                        //alert('ok');
                        //$('#JQDataForm1ExamID').combobox('loadData', rows).combobox('clear');
                    }
                }
            }
            );
        }
        function CourseIDOnSelect() {
            var CourseName = $('#dataFormMasterCourseName').val();
            if (CourseName != null && CourseName != undefined) {
                $('#dataFormMasterCourseName').val($('#dataFormMasterCourseID').combobox('getText'));
            }
            return true;
        }
        function TrainingInstitueIDOnSelect() {
            var TrainingInstitute = $('#dataFormMasterTrainingInstitute').val();
            //alert(TrainingInstitute);
            if (TrainingInstitute != null && TrainingInstitute != undefined) {
                $('#dataFormMasterTrainingInstitute').val($('#dataFormMasterTrainingInstituteID').combobox('getText'));
            }
            return true;
        }
        function dataFormMasterOnApply() {
            
            var ApplyType = $('#dataFormMasterApplyType').combobox('getValue');
            if (ApplyType == '' || ApplyType == undefined) {
                alert('注意!!申請方式未選取,請選取');
                return false;
            }
            var CourseID = $('#dataFormMasterCourseID').combobox('getValue');
            var CourseName = $('#dataFormMasterCourseName').val();
            if (CourseID == '' && CourseName == '') {
                alert('注意!!課程名稱未選取或填入,請選取課程名稱或登錄新增課程');
                return false;
            }
            var CourseMethod = $('#dataFormMasterCourseMethod').combobox('getValue');
            if (CourseMethod == '') {
                alert('注意!!上課方式未選取,請選取');
                return false;
            }
            var TrainingInstitueID = $('#dataFormMasterTrainingInstituteID').combobox('getValue');
            var TrainingInstitute = $('#dataFormMasterTrainingInstitute').val();;
            if (TrainingInstitueID == 42 && TrainingInstitute == '') {
                alert('注意!!訓練機構未選取或填入,請選取訓練機構或登錄新增機構');
                return false;
            }
            var CourseStartHour = $('#dataFormMasterCourseStartHour').val();
            var CourseEndHour = $('#dataFormMasterCourseEndHour').val();
            if (CourseStartHour == '00:00' || CourseEndHour == '00:00') {
                alert('注意!!訓練機構未選取或填入,請選取訓練機構或登錄新增機構');
                return false;
            }
            var TotalHours = $('#dataFormMasterTotalHours').val();
            if (TotalHours == 0) {
                alert('注意!!課程時數不得為0,請修正');
                return false;
            }
            
            var flag = $('#dataFormMasterIsDeclaration').is(':checked');
            if (flag == false) {
                alert('注意!!閱讀同意宣告未選取,請選取');
                return false;
            }
            return true
        }
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var today = new Date();
            var UserID = getClientInfo("UserID");
            var Flow = "外訓申請單";
            var Today = $.jbjob.Date.DateFormat(today, 'yyyy/MM/dd');
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sTrainingApply.CourseApply', 
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow + "," + Today,
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sTrainingApply.CourseApply" runat="server" AutoApply="True"
                DataMember="CourseApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="外訓申請" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyID" Editor="numberbox" FieldName="ApplyID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CCID" Editor="text" FieldName="CCID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PhoneExt" Editor="text" FieldName="PhoneExt" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Area" Editor="text" FieldName="Area" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="StudentTitleID" Editor="numberbox" FieldName="StudentTitleID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseID" Editor="text" FieldName="CourseID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseName" Editor="text" FieldName="CourseName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainingPurpose" Editor="text" FieldName="TrainingPurpose" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Description" Editor="text" FieldName="Description" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CourseMethod" Editor="numberbox" FieldName="CourseMethod" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseStartHour" Editor="text" FieldName="CourseStartHour" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CourseEndHour" Editor="text" FieldName="CourseEndHour" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyType" Editor="numberbox" FieldName="ApplyType" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TrainingInstituteID" Editor="numberbox" FieldName="TrainingInstituteID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TrainingInstitute" Editor="text" FieldName="TrainingInstitute" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InstituteAddress" Editor="text" FieldName="InstituteAddress" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InstitutePhone" Editor="text" FieldName="InstitutePhone" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsCertifiedCourse" Editor="text" FieldName="IsCertifiedCourse" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TermStartDate" Editor="datebox" FieldName="TermStartDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TermEndDate" Editor="datebox" FieldName="TermEndDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="TotalHours" Editor="numberbox" FieldName="TotalHours" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="Tuition" Editor="numberbox" FieldName="Tuition" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsOverseaCourse" Editor="text" FieldName="IsOverseaCourse" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsEnrollBySelf" Editor="text" FieldName="IsEnrollBySelf" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SelfTuition" Editor="numberbox" FieldName="SelfTuition" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsDeclaration" Editor="text" FieldName="IsDeclaration" Format="" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="外訓申請" DialogLeft="10px" DialogTop="45px" Width="615px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CourseApply" HorizontalColumnsCount="2" RemoteName="sTrainingApply.CourseApply" IsAutoSubmit="True" IsShowFlowIcon="True" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterOnLoadSucess" OnCancel="CloseDataForm" OnApply="dataFormMasterOnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請單號" Editor="numberbox" FieldName="ApplyID" Format="" Width="88" Span="2" MaxLength="0" ReadOnly="True" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" maxlength="0" Span="2" Width="90" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請學員" Editor="infocombobox" FieldName="StudentID" maxlength="0" Width="240" Span="1" EditorOptions="valueField:'StudentID',textField:'StudentName',remoteName:'sTrainingApply.Student',tableName:'Student',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門名稱" Editor="text" FieldName="Area" Format="" MaxLength="0" Span="2" Width="237" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本代號" Editor="text" FieldName="CCID" Format="" Span="2" Visible="True" Width="88" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="PhoneExt" Format="" maxlength="0" Width="88" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人職稱" Editor="infocombobox" FieldName="StudentTitleID" Format="" maxlength="0" Width="240" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sTrainingApply.StudentTitle',tableName:'StudentTitle',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請方式" Editor="infocombobox" FieldName="ApplyType" Format="" Width="90" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sTrainingApply.ApplyType',tableName:'ApplyType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="infocombobox" FieldName="CourseID" Format="" maxlength="0" Width="420" Span="2" EditorOptions="valueField:'CourseID',textField:'CourseName',remoteName:'sTrainingApply.Course',tableName:'Course',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="新增課程" Editor="text" FieldName="CourseName" Format="" Width="417" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受訓目的" Editor="textarea" FieldName="TrainingPurpose" Format="" maxlength="0" Width="417" EditorOptions="height:45" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上課方式" Editor="infocombobox" FieldName="CourseMethod" Format="" maxlength="0" Width="240" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sTrainingApply.CourseMethod',tableName:'CourseMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訓練機構" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sTrainingApply.TrainingInstitute',tableName:'TrainingInstitute',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TrainingInstituteID" Format="" Span="2" Width="420" />
                        <JQTools:JQFormColumn Alignment="left" Caption="新增機構" Editor="text" FieldName="TrainingInstitute" Format="" Span="2" Width="417" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡地址" Editor="text" FieldName="InstituteAddress" Format="" maxlength="0" Span="2" Width="417" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡電話" Editor="text" FieldName="InstitutePhone" Format="" maxlength="0" Span="2" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程日期" Editor="datebox" FieldName="TermStartDate" Format="yyyy/mm/dd" Width="93" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="TermEndDate" Format="yyyy/mm/dd" Width="93" />
                        <JQTools:JQFormColumn Alignment="left" Caption="時間起迄" Editor="text" FieldName="CourseStartHour" Format="" maxlength="0" Width="90" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CourseEndHour" Format="" maxlength="0" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程時數" Editor="numberbox" FieldName="TotalHours" Format="" Span="2" Width="90" EditorOptions="precision:1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="課程學費" Editor="numberbox" FieldName="Tuition" Format="" Span="2" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="認證課程" Editor="checkbox" FieldName="IsCertifiedCourse" Format="" Width="60" EditorOptions="on:1,off:0" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="海外訓" Editor="checkbox" FieldName="IsOverseaCourse" Format="" maxlength="0" Width="60" EditorOptions="on:1,off:0" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="自行註冊" Editor="checkbox" FieldName="IsEnrollBySelf" Format="" Width="60" EditorOptions="on:1,off:0" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="自費課程" Editor="checkbox" FieldName="SelfTuition" Format="" Width="60" EditorOptions="on:1,off:1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Description" Format="" Width="400" EditorOptions="height:30" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsDeclaration" maxlength="0" Span="2" Width="20" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ApplyID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="StudentID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="TotalHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsDeclaration" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="42" FieldName="TrainingInstituteID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="TotalHours" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="00:00" FieldName="CourseStartHour" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="00:00" FieldName="CourseEndHour" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="TermStartDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="TermEndDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Tuition" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
