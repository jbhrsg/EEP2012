<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_CourseOpen.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
       <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var tempRowIndex = 0;
        $(document).ready(function () {
            var Today = new Date();
            var yyyy = Today.getFullYear();
            $("#cbYEAR").combobox('resize', '80');
            $("#cbCoursePlan").combobox('resize', '360');
            GetYearAndPlan();
            var dgid = $('#JQDataGrid1');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 250 });
            $('.infosysbutton-q', '#JQDataGrid1').closest('td').attr('align', 'middle');
            CoursePlanFilt();
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
               $(queryPanel).panel('resize', { width: 450 });
            $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
            var Startdate = $('#JQDataForm1CourseStartDate').closest('td');
            var Enddate = $('#JQDataForm1CourseEndDate').closest('td').children();
            Startdate.append(' -').append(Enddate);
            var Starthr = $('#JQDataForm1CourseStartHour').closest('td');
            var Endhr = $('#JQDataForm1CourseEndHour').closest('td').children();
            Starthr.append(' -').append(Endhr);
            var ExamStd = $('#JQDataForm4ExamStartDate').closest('td');
            var ExamEnd = $('#JQDataForm4ExamEndDate').closest('td').children();
            ExamStd.append(' -').append(ExamEnd);
            $('#JQTreeViewStudent').tree({
                onlyLeafCheck: true,
            });
            var CourseHours = $('#JQDataForm1CourseHours').closest('td');
            CourseHours.append(' 小時')
            var TrainingFee = $('#JQDataForm1TrainingFee').closest('td');
            TrainingFee.append(' 元')
            //GetOpenCourseDataID('edit', CourseOpenID);
        });
        var DataGridView_FormatScript = function (value, row, index) {
            var fieldName = this.field;
            switch (fieldName) {
                case 'TRANSLOG':
                    return $('<a>', { href: 'javascript: void(0)', onclick: "DataGridView_CommandTrigger.call(this,'DataLog')" }).html('異動資料記錄')[0].outerHTML;
                    break;
                case 'Toolbar':
                    return $('<a>', { href: 'javascript:void(0)', title: '課程開課', onclick: "DataGridView_CommandTrigger.call(this,'Base')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '課程開課' })[0].outerHTML
                    // + $('<a>', { href: 'javascript:void(0)', title: '出勤班表設定', onclick: "DataGridView_CommandTrigger.call(this,'Attend')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '出勤班表設定' })[0].outerHTML
                    //+ $('<a>', { href: 'javascript:void(0)', title: '薪資群組設定', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'SalaryGroup')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '薪資群組設定' })[0].outerHTML
                    //+ $('<a>', { href: 'javascript:void(0)', title: '核定薪資作業', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'SalaryBase')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '核定薪資作業' })[0].outerHTML
                    //+ $('<a>', { href: 'javascript:void(0)', title: '勞健保投保', onclick: "DataGrid_BaseIO_CommandTrigger.call(this,'Insurance')" }).linkbutton({ iconCls: 'icon-tip', plain: false, text: '勞健保投保' })[0].outerHTML;
                    break;
                default:
                    return '';
                    break;
            }
        }
        //---------------------------------------Grid欄位CommandTrigger---------------------------
        var DataGridView_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var RowData = $("#dataGridView").datagrid('getSelected');
            //var RowData = $(DataGrid_BaseIO_ID).datagrid('selectRow', rowIndex).datagrid('getSelected');
            switch (command) {
                case 'DataLog':
                case 'Base':
                    PlanCourseOpen();
                    break;
                case 'Attend':
                case 'SalaryGroup':
                case 'SalaryBase':
                case 'Insurance':
                    break;
                default:
                    break;
            }
        }
        function queryGrid(dg) {
           var rowData = $("#dataGridView").datagrid('getSelected');
           var PlanID = rowData.PlanID;
           if ($(dg).attr('id') == 'JQDataGrid1') {
                var CourseID = rowData.CourseID;
                var wherestr = [];
                var val = '';
                val = $('#CourseOpenStatus_Query').combobox("getValue");
                if (val != '') {
                    wherestr.push("CourseOpenStatus = " + val);
                }
                wherestr.push("CourseID=" + "'" + CourseID + "'");
                wherestr.push("CoursePlanID=" + "'" + PlanID + "'");
                $(dg).datagrid('setWhere', wherestr.join(' and '));
                $("#JQDataGrid2").datagrid('setWhere', '1=2');
           }
           if ($(dg).attr('id') == 'dataGridView') {
               var wherestr = [];
               var val = '';
               val = $('#ClassName_Query').val();
               if (val != '') {
                   wherestr.push("ClassName like '%" + val + "%'");
               }
               val = $('#OpenSeason_Query').combobox("getValue");
               if (val != '') {
                   wherestr.push("OpenSeason = '" + val + "'");
               }
               wherestr.push("PlanID=" + "'" + PlanID + "'");
               $(dg).datagrid('setWhere', wherestr.join(' and '));
               $("#JQDataGrid1").datagrid('setWhere', '1=2');
               $("#JQDataGrid2").datagrid('setWhere', '1=2');
           }
        }
        function cbYearOnSelect(rowdata) {
            $("#dataGridView").datagrid('setWhere', '1=2');
            $("#JQDataGrid1").datagrid('setWhere', '1=2');
            $("#JQDataGrid2").datagrid('setWhere', '1=2');
            $("#cbCoursePlan").combobox('setValue', "");
            $("#cbCoursePlan").combobox('setWhere', "Year=" + rowdata.YEAR);
        }
        //計畫課程開課
        function PlanCourseOpen() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var PlanID = RowData.PlanID;
            var CourseID = RowData.CourseID;
            if (CheckIsOpenCourse(CourseID, PlanID) == false) {
                alert('注意!!,此課程有未結案課程,無法開課或加開!!');
                return false;
            }
            var Year = $("#cbYEAR").combobox("getValue");
            var PlanID = $("#cbCoursePlan").combobox("getValue");
            var SpreadType = $("#cbCoursePlan").combobox("getValue");
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
                data: "mode=method&method=" + "procCoursePlanOpen" + " &parameters=" + Year + "," + PlanID + "," + CourseID + "," + SpreadType + "," + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                       // $.messager.confirm('提示訊息', '計畫課程開課成功,按下「確定」離開', function (r) {
                       //     if (r) {
                                $("#JQDataGrid1").datagrid('reload');
                       //     }
                       // })
                    }
                    else {
                        alert("計畫課程開課失敗")
                    }

                }

            });
        }
        //開課課程加開
        function PlanCourseOpenAdd() {
            var RowData = $("#dataGridView").datagrid('getSelected');
            var PlanID = RowData.PlanID;
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            if (RowData == null) {
                alert('注意!! 此課程未開課,無法加開課程');
                return false;
            }
            var CourseID = RowData.CourseID;
            var CourseOpenID = RowData.CourseOpenID;
            var UserID = getClientInfo("UserID");
            if (CheckIsOpenCourse(CourseID, PlanID) == false) {
                alert('注意!!,此課程有未結案課程,無法開課或加開!!');
                return false;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
                data: "mode=method&method=" + "procCoursePlanOpenAdd" + " &parameters=" +CourseOpenID+"," + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                             $("#JQDataGrid1").datagrid('reload');
                    }
                    else {
                        alert("開課課程加開失敗")
                    }
                }
            });
        }        
        //確認開課
        function PlanCourseOpenConfirm() {
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            var row = $("#JQDataGrid1").datagrid('getSelected');
            var rowIndex = $("#JQDataGrid1").datagrid('getRowIndex', row);
            tempRowIndex = rowIndex;
            if (RowData == null) {
                alert('注意!! 此課程未開課,無法確認開課');
                return false;
            }
            if (RowData.CourseOpenStatus != 0) {
                alert('注意!! 此課程'+RowData.TextCourseOpenStatus+',無法確認開課');
                return false;
            }
            if ((RowData.CourseStartHour == null) || (RowData.CourseEndHour == null) || (RowData.MainHost == null) || (RowData.CourseLocationID == null) || (RowData.TeacherID1 == null) || (RowData.CourseDataID == ''))
            {
                alert('注意!! 開課資料未設定完成,無法確認開課,請設定');
                return false;
            }
            var CourseOpenID = RowData.CourseOpenID;
            var UserID = getClientInfo("UserID");
            $.ajax({
                  type: "POST",
                  url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
                  data: "mode=method&method=" + "procCoursePlanOpenBookBill" + " &parameters=" + CourseOpenID + "," + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                  cache: false,
                  async: false,
                  success: function (data) {
                      if (data == "True") {
                          // $.messager.confirm('提示訊息', '確認開課成功,按下「確定」離開', function (r) {
                          //     if (r) {
                          $("#JQDataGrid1").datagrid('reload');
                          $("#JQDataGrid2").datagrid('reload');
                          //     }
                        // })
                     }
                     else {
                        alert("注意!! 確認開課失敗")
                     }
                }
            });
        }
        //因故停課
        function PlanCourseOpenStop() {
            var RowData = $('#JQDataGrid1').datagrid('getSelected');
            var rowIndex = $("#JQDataGrid1").datagrid('getRowIndex', RowData);
            tempRowIndex = rowIndex;
            if ((RowData.CourseOpenStatus == 2) || (RowData.CourseOpenStatus == 3) || (RowData.CourseOpenStatus == 4)) {
                alert('注意!!當開課狀態在準備中與已開課時,才可因故停課');
                return false;
            }
            openForm('#JQDialog5', $('#JQDataGrid1').datagrid('getSelected'), "updated", 'dialog');
        }
        //設定測驗
        function PlanCourseOpenExam() {
            var RowData = $('#JQDataGrid1').datagrid('getSelected');
            var rowIndex = $("#JQDataGrid1").datagrid('getRowIndex', RowData);
            tempRowIndex = rowIndex;
            var CourseOpenID = RowData.CourseOpenID;
            var CourseTestMethod = RowData.CourseTestMethod;
            if (RowData.CourseOpenStatus !=1) {
                alert('注意!!當開課狀態在開課中時,才可設定測驗');
                return false;
            }
            if (RowData.CourseTestMethod != 2) {
                alert('注意!!當課程測驗方式為線上測驗時,才需設定測驗,請編輯開課資訊');
                return false;
            }
            //GetOpenCourseExamID(CourseOpenID, CourseTestMethod);
            openForm('#JQDialog6', RowData, "updated", 'dialog');
        }
        //內部開課結案
        function PlanCourseOpenClose() {
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            var rowIndex = $("#JQDataGrid1").datagrid('getRowIndex', RowData);
            tempRowIndex = rowIndex;
            var CourseOpenID = RowData.CourseOpenID;
            var UserID = getClientInfo("UserID");
            if (RowData == null) {
                alert('注意!!無開課紀錄或未選取開課紀錄,無法結案');
                return false;
            }
            if ((RowData.CourseOpenStatus == 0) || (RowData.CourseOpenStatus == 2) || (RowData.CourseOpenStatus == 3)) {
                alert('注意!!開課狀態不是已開課或測驗中,無法結案');
                return false;
            }
            if (CourseOpenClose(CourseOpenID, UserID) == 0) {
                alert('注意!!,開課結案失敗,請洽IT人員');
                return false;
            }
            else {
                $.messager.confirm('提示訊息', '課程結案成功,按下「確定」離開', function (r) {
                    if (r) {
                        $("#JQDataGrid1").datagrid('reload');
                        return true;
                    }
                });
            }
        }
        //列印空白簽到表,暫不使用
        function PrintRegisterBlank() {
            alert('Task To do')
        }
        function cbCoursePlanOnSelect(rowdata) {
            $("#JQDataGrid1").datagrid('setWhere', '1=2');
            $("#JQDataGrid2").datagrid('setWhere', '1=2');
            CoursePlanFilt();
        }
        function JQDataForm1LoadSuccess() {
            var CourseID = $('#JQDataForm1CourseID').val();
            var whereStr = " CourseID = "+"'" + CourseID+"'";
            $('#JQDataForm1TeacherID1').combobox('setWhere', whereStr);
            var RowData = $("#JQDataGrid1").datagrid('getSelected');
            var CourseOpenID = RowData.CourseOpenID;
            GetOpenCourseDataID('edit',CourseOpenID);
        }
        function CoursePlanFilt() {
            var PlanID = $("#cbCoursePlan").combobox("getValue");
            var FiltStr = "PlanID=" + "'" + PlanID + "'" ;
            $("#dataGridView").datagrid('setWhere', FiltStr);
        }
        function dataGridViewOnSelect(rowindex, rowdata) {
            var ClassName = rowdata.ClassName + ' 課程開課列表';
            $("#JQDataGrid1").datagrid('getPanel').panel('setTitle', ClassName);
            $("#JQDataGrid1").datagrid('options').title = ClassName;
            var ClassName = rowdata.ClassName + ' 學員名單列表';
            $("#JQDataGrid2").datagrid('getPanel').panel('setTitle', ClassName);
            $("#JQDataGrid2").datagrid('options').title = ClassName;
            if (rowdata != null && rowdata != undefined) {
                $("#JQDataGrid2").datagrid('setWhere', "1=2");
                var PlanID = rowdata.PlanID;
                var CourseID = rowdata.CourseID;
                var CourseNO = rowdata.CourseNO;
                var FiltStr = "CoursePlanID=" + "'" + PlanID + "'" + " and CourseID=" + "'" + CourseID + "'";
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
            }
        }
        function JQDataGrid1OnUpdate(rowdata) {
            if ((rowdata.CourseOpenStatus == 3) || (rowdata.CourseOpenStatus == 4)) {
                openForm('#JQDialog2', $('#JQDataGrid1').datagrid('getSelected'), "viewed", 'dialog');
                return false;
            }
        }
        function JQDataGrid1OnSelect(rowindex, rowdata) {
           
                if (rowdata != null && rowdata != undefined) {
                    var CourseOpenID = rowdata.CourseOpenID;
                    var FiltStr = "CourseOpenID=" + "'" + CourseOpenID + "'";
                    $("#JQDataGrid2").datagrid('setWhere', FiltStr);
                }
        }
       function JQDataGrid2OnUpdated() {
                $("#JQDataGrid2").datagrid('reload');
        }
        //
       function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;'  />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
       //
       function GetYearAndPlan() {
            var para = '';
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.LastPlanYear', 
                data: "mode=method&method=" + "GetYearAndPlan" + "&parameters=" + para, 
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#cbYEAR").combobox('setValue', rows[0].Year);
                        $("#cbCoursePlan").combobox('setValue', rows[0].PlanID);
                        $("#cbCoursePlan").combobox('setWhere', "Year=" + rows[0].Year);
                    }
                }
            }
            );
       }
        //選擇測驗方式
       function CourseTestMethodOnSelect() {
           var today = new Date();
           var nextday = new Date();
           nextday.setDate(nextday.getDate() + 15);
               var RowData = $("#JQDataGrid1").datagrid('getSelected');
               var CourseOpenID = RowData.CourseOpenID;
               var TestMethod = $("#JQDataForm4CourseTestMethod").combobox("getValue");
               GetOpenCourseExamID(CourseOpenID, TestMethod);
               if (TestMethod == 2) {
                   $("#JQDataForm4CanExamTime").val(9999);
                   $('#JQDataForm4ExamStartDate').datebox('setValue', $.jbjob.Date.DateFormat(today, 'yyyy/MM/dd'));
                   $('#JQDataForm4ExamEndDate').datebox('setValue', $.jbjob.Date.DateFormat(nextday, 'yyyy/MM/dd'));
               }
       }
       //取得開課課程測驗卷
       function GetOpenCourseExamID(CourseOpenID,TestMethod) {
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.LastPlanYear', 
               data: "mode=method&method=" + "GetOpenCourseExamID" + "&parameters=" + CourseOpenID + "," + TestMethod,
               cache: false,
               async: false,
               success: function (data) {
                   var rows = $.parseJSON(data);
                   if (rows.length > 0) {
                       $('#JQDataForm4ExamID').combobox('loadData', rows).combobox('clear');
                  }
               }
           }
           );
        }
       //取得開課課程教材
       function GetOpenCourseDataID(Type,CourseOpenID) {
          $.ajax({
              type: "POST",
              url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.LastPlanYear', 
              data: "mode=method&method=" + "GetOpenCourseDataID" + "&parameters=" + Type + "," + CourseOpenID, 
              cache: false,
              async: false,
              success: function (data) {
                  var rows = $.parseJSON(data);
                  if (rows.length > 0) {
                      $('#JQDataForm1CourseDataID').combobox('loadData', rows);
                  }
              }
          }
          );
        }
        //簽到簿報表
       function CallReport() {
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           var CourseOpenID = RowData.CourseOpenID;
           var url = "../JB_ETRAINING/report/TRN_CourseBookBillReportView.aspx?CourseOpenID=" + CourseOpenID;
           var height = $(window).height() - 20;
           var height2 = $(window).height() + 150;
           var width = $(window).width() - 100;
           var dialog = $('<div/>')
           .dialog({
               draggable: false,
               modal: true,
               height: height,
               width: width,
               title: "簽到簿",
               //maximizable: true                              
           });
           $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="' + height2 + '"></iframe>').appendTo(dialog.find('.panel-body'));
           dialog.dialog('open');
       }
       //開課課程資訊更新確認後
       function JQDataForm4OnApplied() {
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           var CourseOpenID = RowData.CourseOpenID;
           var TestMethod = $("#JQDataForm4CourseTestMethod").combobox("getValue");
           var CourseOpenStatus = '4';
           var IsSendMail = '1'
           //當設定測驗且測驗方式=線上測驗
           if ((TestMethod == 2)) {
               SetOpenCourseExam(CourseOpenID, CourseOpenStatus, IsSendMail);
           }
           $("#JQDataGrid1").datagrid('reload');
        }
        //開課課程資訊更新確認
        function JQDataForm4OnApply() {
            if (getEditMode($("#JQDataForm4")) == 'updated') {
                var TestMethod = $("#JQDataForm4CourseTestMethod").combobox("getValue");
                if (TestMethod == 2) {
                    var ExamID = $("#JQDataForm4ExamID").combobox("getValue");
                    if ((ExamID == null) || (ExamID == '')) {
                        alert('注意!!未選取測驗試卷,請選取');
                        return false;
                    }
                }
            }
        }
        //選取學員確認
        function TreeViewStudentOnSubmited() {
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           var nodes = $('#JQTreeViewStudent').tree('getChecked');
           var StudentIDs = '';
           var i = 1;
           $.each(nodes, function (index, node) {
               if (i > 1) {
                   StudentIDs = StudentIDs + ',' + node.id;
               }
               else {
                   StudentIDs = StudentIDs + node.id;
               }
               i = i + 1;
           });
           //alert(StudentIDs);
           AddCourseOpenBookBill(RowData.CourseOpenID,StudentIDs);
           return true;
       }
        //開啟加入學員視窗
        function AddCourseStudents() {
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           if (RowData == null) {
               alert('注意!!未開課,無法新增學員');
               return false;
           }
           var CourseStatus = RowData.CourseOpenStatus;
           if (CourseStatus != 0 && CourseStatus != 1) {
               alert('注意!!,課程在準備中才可新增學員');
               return false;
           }
           
           var StudentsStr = GetPlanOpenCourseStudents(RowData.CoursePlanID, RowData.CourseID);
           if (StudentsStr != '') {
               $.each(StudentsStr.split(","), function (i, id) {
                   var node = $('#JQTreeViewStudent').tree('find', id);
                   if (node != null) {
                      $('#JQTreeViewStudent').tree('remove', node.target);
                   }
               });
           }
           var nodes = $('#JQTreeViewStudent').tree('getChecked');
           $.each(nodes, function (index, node) {
               $('#JQTreeViewStudent').tree('uncheck', node.target);
           });
           var root = $('#JQTreeViewStudent').tree('getRoot');
           $('#JQTreeViewCourse').tree('uncheck', root.target);
           openForm('#JQDialog4', {}, "", 'dialog');
       }
        //取得傳入計畫課程已開課學員名單
        function GetPlanOpenCourseStudents(PlanID, CourseID) {
           var UserID = getClientInfo("UserID");
           var ReturnStr = "";
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CoursePlan', //連接的Server端，command
               data: "mode=method&method=" + "GetPlanOpenCourseStudents" + "&parameters=" + PlanID+","+CourseID+","+UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
        //將加入的學員名單寫入課程上課名單CourseBookBill
        function AddCourseOpenBookBill(CourseOpenID,StudentIDs) {
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           if (RowData.CourseOpenStatus == 3) {
               alert('注意！！開課狀態已結案,無法加入學員!!');
               return false;
           }
           var UserID = getClientInfo("UserID");
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
               data: "mode=method&method=" + "procAddCourseOpenBookBill" + " &parameters=" + CourseOpenID + "*" + StudentIDs + "*" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   if (data == "True") {
                       var row = $("#JQDataGrid1").datagrid('getSelected');
                       var rowIndex = $("#JQDataGrid1").datagrid('getRowIndex', row);
                       tempRowIndex = rowIndex;
                       $("#JQDataGrid1").datagrid('reload');
                   }
                   else {
                       alert("加入學員失敗")
                   }
               }
           });
       }
        //執行開課課程測驗
        function SetOpenCourseExam(CourseOpenID,CourseOpenStatus,IsSendMail) {
           var UserID = getClientInfo("UserID");
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
               data: "mode=method&method=" + "SetOpenCourseExam" + " &parameters=" + CourseOpenID + "," + CourseOpenStatus + "," + IsSendMail + "," + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   if (data == "True") {

                   }
                   else {
                       alert("注意!! 更改測驗狀態失敗")
                   }
               }
           });
       }
        //開課紀錄刪除
        function JQDataGrid1OnDelete() {
           var today = new Date();
           var UserID = getClientInfo("UserID");
           var RowData = $("#JQDataGrid1").datagrid('getSelected');
           var Dt = $.jbjob.Date.DateFormat(today, 'yyyy/MM/dd');
           if (UserID != RowData.CreateBy) {
               alert('注意!!,僅開課人員可執行開課紀錄刪除');
               return false;
           }
           if (RowData.CourseOpenStatus != 0 && RowData.CourseOpenStatus != 1) {
               alert('注意!!,僅開課狀態在準備中或開課中可刪除開課紀錄');
               return false;
           }
           if (DeleteCourseBookBill(RowData.CourseOpenID) == 0) {
               alert('注意!!,發生資料錯誤,刪除失敗,請洽IT人員');
               return false;
           }
       }
        //刪除傳入開課紀錄的上課名單
        function DeleteCourseBookBill(CourseOpenID) {
           var flag=0
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
               data: "mode=method&method=" + "DeleteCourseBookBill" + " &parameters=" + CourseOpenID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   if (data == 'True') {
                       flag = 1;
                   }
                   else {
                       alert("注意!! 刪除開課學員名單失敗");
                   }
               }
           });
           return flag;
       }
        //因故停課
       function CourseOpenStop(CourseOpenID, UserID, IsSendMail) {
           var flag = 0
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', //連接的Server端，command
               data: "mode=method&method=" + "PlanCourseOpenStop" + " &parameters=" + CourseOpenID + "," + UserID + "," + IsSendMail,
               cache: false,
               async: false,
               success: function (data) {
                   if (data == 'True') {
                       flag = 1;
                   }
                   else {
                       alert("注意!! 因故停課失敗");
                   }
               }
           });
           return flag;
        }
        //課程結案
        function CourseOpenClose(CourseOpenID, UserID) {
            var flag = 0;
            var CourseOpenStatus = 3; //結案
            var IsSendMail = 0;  //是否發送eMail 否
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord',
                data: "mode=method&method=" + "procCourseOpenClose" + " &parameters=" + CourseOpenID + "," + CourseOpenStatus+","+IsSendMail+","+UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == 'True') {
                        flag = 1;
                    }
                    else {
                        alert("注意!! 課程結案失敗");
                    }
                }
            });
            return flag;
        }
        function JQDataForm3OnApplied() {
            var UserID = getClientInfo("UserID");
            var IsSendMail = $('#JQDataForm3IsSendMail').checkbox('getValue');
            var RowData = $('#JQDataGrid1').datagrid('getSelected');
            if (CourseOpenStop(RowData.CourseOpenID, UserID, IsSendMail) == 0) {
                alert('注意!!,因故停課作業失敗,請洽IT人員');
                return false;
            }
            $("#JQDataGrid1").datagrid('reload');
        }
        function JQDataGrid1OnLoadSucess() {
            if (tempRowIndex != 0) {
                $('#JQDataGrid1').datagrid('selectRow', tempRowIndex);
                tempRowIndex = 0;
            }
        }
        function JQDataGrid2OnUpdate() {
            var rowdata = $("#JQDataGrid1").datagrid('getSelected');
            if (rowdata.CourseOpenStatus == 3) {
                var row = $("#JQDataGrid2").datagrid('getSelected');
                var index = $("#JQDataGrid2").datagrid('getRowIndex', row);
                if (index != undefined) {
                    $("#JQDataGrid2").datagrid('selectRow', index).datagrid('beginEdit', index);
                    var cells = $("#JQDataGrid2").datagrid('getEditors', index);
                    $.each(cells, function (index, obj) {
                        if (obj.field != "DEPTNAME" && obj.field != "StudentName" && obj.field != "Status" && obj.field != "ExamTime") {
                            switch (obj.type) {
                                case "text": $(obj.target).attr("disabled", "disabled");
                                    break;
                                case "validatebox": $(obj.target).attr("disabled", "disabled");
                                    break;
                                case "datebox": $(obj.target).datebox("disable");
                                    break;
                                case "infocombogrid": $(obj.target).combogrid('disable');
                                    break;
                                case "numberbox": $(obj.target).numberbox('disable');
                                    break;
                                case "checkbox": $(obj.target).attr('disabled', "disabled");
                                    break;
                                default:
                                    break;
                            }
                        }
                    });
                }
            }
        }
        //檢查是否可課程開課或加開課程
        function CheckIsOpenCourse(CourseID,PlanID) {
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCourseOpen.CourseOpenRecord', 
                data: "mode=method&method=" + "CheckIsOpenCourse" + "&parameters=" + CourseID + "," + PlanID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                //alert('注意!!,此課程有未結案開課課程,無法開課或加開！！');
                return false;
            }
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <br />
         <div>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="計畫年度:"></asp:Label>
            <JQTools:JQComboBox ID="cbYEAR" runat="server" DisplayMember="YEAR" PanelHeight="150" RemoteName="sCourseOpen.YearNO" ValueMember="YEAR" Width="30px" OnSelect="cbYearOnSelect">
            </JQTools:JQComboBox>
            &nbsp;&nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="Small" Text="課程計畫:"></asp:Label>
            <JQTools:JQComboBox ID="cbCoursePlan" runat="server" DisplayMember="PlanName" PanelHeight="150" RemoteName="sCourseOpen.CoursePlan" ValueMember="PlanID" Width="30px" OnSelect="cbCoursePlanOnSelect">
            </JQTools:JQComboBox>
          </div>
        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
        <br />
        <div class="easyui-layout" style="width: 1100px;height: 720px;">
            <div data-options="region:'west',split:true" style="width: 450px; height: 300px;">

                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCourseOpen.CoursePlanDetails" runat="server" AutoApply="True"
                    DataMember="CoursePlanDetails" Pagination="True" QueryTitle="計畫查詢" EditDialogID="JQDialog1"
                    Title="計畫課程" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="10px" QueryMode="Window" QueryTop="65px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" Width="440px" OnSelect="dataGridViewOnSelect">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="ClassName" Format="" MaxLength="0" Width="220" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="達成率" Editor="text" FieldName="CloseRate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" />
                        <JQTools:JQGridColumn Alignment="center" Caption="季別" Editor="text" FieldName="OpenSeason" Format="" MaxLength="0" Width="35" Frozen="True" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="text" FieldName="CourseOpenCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35" />
                        <JQTools:JQGridColumn Alignment="left" Caption="開啟" Editor="text" FieldName="IsActive" Format="" Width="30" FormatScript="genCheckBox" />
                        <JQTools:JQGridColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" Format="" MaxLength="0" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="Toolbar" Editor="text" FieldName="Toolbar" FormatScript="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100" />
                        <JQTools:JQGridColumn Alignment="right" Caption="CourseNO" Editor="numberbox" FieldName="CourseNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="計畫代號" Editor="numberbox" FieldName="PlanID" Format="" Width="40" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsClass" Editor="text" FieldName="IsClass" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsUserDefine" Editor="text" FieldName="IsUserDefine" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="DeptID" Editor="text" FieldName="DeptID" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PlanDeptIDs" Editor="text" FieldName="PlanDeptIDs" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="StartDate1" Editor="datebox" FieldName="StartDate1" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="StartDate2" Editor="datebox" FieldName="StartDate2" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SpreadType" Editor="numberbox" FieldName="SpreadType" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PlanType" Editor="text" FieldName="PlanType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                            OnClick="openQuery" Text="查詢" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpen" Text="課程開課" />
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />


                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="課程名稱" Condition="%%" DataType="string" Editor="text" FieldName="ClassName" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="開課季別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Season',textField:'Season',remoteName:'sCoursePlan.OpenSeason',tableName:'OpenSeason',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OpenSeason" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="60" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="課程開課">
                    <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CoursePlanDetails" HorizontalColumnsCount="1" RemoteName="sCourseOpen.CoursePlanDetails" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="計畫代號" Editor="numberbox" FieldName="PlanID" Format="" Width="60" />
                            <JQTools:JQFormColumn Alignment="left" Caption="展開方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourse.SpreadType',tableName:'SpreadType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SpreadType" Format="" Width="110" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" Format="" MaxLength="0" Width="117" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="ClassName" Format="" MaxLength="0" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="季別" Editor="text" FieldName="OpenSeason" Format="" MaxLength="0" Width="117" />
                            <JQTools:JQFormColumn Alignment="left" Caption="堂次" Editor="numberbox" FieldName="CourseNO" Format="" Width="60" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="StartDate1" Format="yyyy/mm/dd" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="StartDate2" Format="yyyy/mm/dd" Width="100" />
                            <JQTools:JQFormColumn Alignment="left" Caption="開啟" Editor="checkbox" FieldName="IsActive" Format="" Width="60" EditorOptions="on:1,off:0" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IsClass" Editor="text" FieldName="IsClass" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="IsUserDefine" Editor="text" FieldName="IsUserDefine" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Format="" MaxLength="0" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Format="" MaxLength="0" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="DeptID" Editor="text" FieldName="DeptID" Format="" MaxLength="0" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PlanDeptIDs" Editor="text" FieldName="PlanDeptIDs" Format="" MaxLength="0" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" MaxLength="0" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
            </div>
            <div data-options="region:'center'" style="width: 780px; height: 300px;">
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True"
                    CheckOnSelect="True" ColumnsHibeable="False" DataMember="CourseOpenRecord" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog"
                    EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False"
                    QueryLeft="465px" QueryMode="Window" QueryTitle="查詢" QueryTop="100px" RecordLock="False" RecordLockMode="None"
                    RemoteName="sCourseOpen.CourseOpenRecord" Title="課程開課列表" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="JQDialog2" Width="650px" OnSelect="JQDataGrid1OnSelect" OnDelete="JQDataGrid1OnDelete" OnLoadSuccess="JQDataGrid1OnLoadSucess" OnUpdate="JQDataGrid1OnUpdate">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="代號" Editor="text" FieldName="CourseOpenID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="開課類型" Editor="text" FieldName="TextCourseOpenType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="堂次" Editor="text" FieldName="CourseNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="CourseStartDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="CourseEndDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="center" Caption="開課狀態" Editor="text" FieldName="TextCourseOpenStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="狀態日期" Editor="datebox" FieldName="CourseCloseDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="left" Caption="開課人員" Editor="text" FieldName="OpenUser" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseMaxCount" Editor="text" FieldName="CourseMaxCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CoursePlanID" Editor="text" FieldName="CoursePlanID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="Year" Editor="text" FieldName="Year" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseOpenType" Editor="text" FieldName="CourseOpenType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseID" Editor="text" FieldName="CourseID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseName" Editor="text" FieldName="CourseName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseType" Editor="text" FieldName="CourseType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseDataID" Editor="text" FieldName="CourseDataID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseStartHour" Editor="text" FieldName="CourseStartHour" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseEndHour" Editor="text" FieldName="CourseEndHour" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseMethod" Editor="text" FieldName="CourseMethod" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseLocationID" Editor="text" FieldName="CourseLocationID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="開課狀態" Editor="infocombobox" FieldName="CourseOpenStatus" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.OpenStatus',tableName:'OpenStatus',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseTestMethod" Editor="text" FieldName="CourseTestMethod" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ExamStartDate" Editor="text" FieldName="ExamStartDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ExamEndDate" Editor="text" FieldName="ExamEndDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseHours" Editor="text" FieldName="CourseHours" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ExamID" Editor="text" FieldName="ExamID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment=" " Caption="MainHost" Editor="text" FieldName="MainHost" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="SubHost" Editor="text" FieldName="SubHost" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="TeacherID1" Editor="text" FieldName="TeacherID1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="TrainingFee" Editor="text" FieldName="TrainingFee" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="TeacherID2" Editor="text" FieldName="TeacherID2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsOverseaCourse" Editor="text" FieldName="IsOverseaCourse" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsAllowBooking" Editor="text" FieldName="IsAllowBooking" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsMustBeEval" Editor="text" FieldName="IsMustBeEval" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsOpenToLook" Editor="text" FieldName="IsOpenToLook" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="IsCert" Editor="text" FieldName="IsCert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="Description" Editor="text" FieldName="Description" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CanExamTime" Editor="text" FieldName="CanExamTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CourseOpenProperty" Editor="text" FieldName="CourseOpenProperty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ParentCourseOpenID" Editor="text" FieldName="ParentCourseOpenID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                            OnClick="openQuery" Text="查詢" />
                        <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpenAdd" Text="加開課程" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpenConfirm" Text="確認開課" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpenStop" Text="因故停課" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpenExam" Text="設定測驗" />
                        <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"
                            OnClick="PlanCourseOpenClose" Text="開課結案" />
                        <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="CallReport" Text="簽到簿" />
                      
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="開課狀態" Condition="=" DataType="string" Editor="infocombobox" FieldName="CourseOpenStatus" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.OpenStatus',tableName:'OpenStatus',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
                <br />
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="JQDataForm1" Title="課程計畫維護" Width="550px" DialogTop="40px" EnableViewState="False" DialogLeft="10px">
                    <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="CourseOpenRecord" HorizontalColumnsCount="1" RemoteName="sCourseOpen.CourseOpenRecord" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="JQDataForm1LoadSuccess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="ExamFlag" Editor="text" FieldName="ExamFlag" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="開課代號" Editor="text" FieldName="CourseOpenID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="390" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="開課方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.CourseOpenType',tableName:'CourseOpenType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseOpenType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程類型" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.CourseType',tableName:'CourseType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.CourseMethod',tableName:'CourseMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseMethod" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起迄日期" Editor="datebox" FieldName="CourseStartDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="CourseEndDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起迄時間" Editor="text" FieldName="CourseStartHour" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="CourseEndHour" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程時數" Editor="text" FieldName="CourseHours" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課程費用" Editor="text" FieldName="TrainingFee" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                            <JQTools:JQFormColumn Alignment="left" Caption="主辦單位" Editor="infocombobox" FieldName="MainHost" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.TRCompany',tableName:'TRCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="協辦單位" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.TRCompany',tableName:'TRCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SubHost" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課地點" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.TRLocation',tableName:'TRLocation',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseLocationID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課講師" Editor="infocombobox" EditorOptions="valueField:'STUDENTID',textField:'TEACHERNAME',remoteName:'sCourseOpen.Teacher',tableName:'Teacher',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TeacherID1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課助教" Editor="infocombobox" EditorOptions="valueField:'TeacherID',textField:'TeacherName',remoteName:'sCourseOpen.Teacher',tableName:'Teacher',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TeacherID2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="上課教材" Editor="infocombobox" EditorOptions="valueField:'CourseDataID',textField:'CourseDataName',remoteName:'sCourseOpen.CourseData',tableName:'CourseData',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseDataID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="海外訓練" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOverseaCourse" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="允許報名" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAllowBooking" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="課後評估" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsMustBeEval" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="公開公告" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOpenToLook" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="完訓證明" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsCert" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.TestMethod',tableName:'TestMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CourseTestMethod" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CoursePlanID" Editor="text" FieldName="CoursePlanID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗試卷" Editor="infocombobox" FieldName="ExamID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="185" EditorOptions="valueField:'ExamPaperID',textField:'Name',remoteName:'sCourseOpen.ExamPaper',tableName:'ExamPaper',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗次數" Editor="text" FieldName="CanExamTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="測驗起迄" Editor="datebox" FieldName="ExamStartDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="90" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="ExamEndDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="90" Format="yyyy/mm/dd" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQDialog>

                <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True"
                    CheckOnSelect="True" ColumnsHibeable="False" DataMember="CourseBookBill" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog"
                    EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False"
                    QueryLeft="30px" QueryMode="Window" QueryTitle="" QueryTop="150px" RecordLock="False" RecordLockMode="None"
                    RemoteName="sCourseOpen.CourseBookBill" Title="學員名單列表" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" EditDialogID="" Width="650px" OnUpdated="JQDataGrid2OnUpdated" ReportFileName="" OnUpdate="JQDataGrid2OnUpdate">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="text" FieldName="DEPTNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="170" />
                        <JQTools:JQGridColumn Alignment="left" Caption="學員姓名" Editor="text" FieldName="StudentName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="True" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="初複訓" Editor="text" FieldName="Status" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="45" />
                        <JQTools:JQGridColumn Alignment="left" Caption="上課" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="出席" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsAttend" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="合格" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPass" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                        <JQTools:JQGridColumn Alignment="right" Caption="分數" Editor="text" FieldName="ExamPoint" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="center" Caption="測驗次數" Editor="text" FieldName="ExamTime" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"    OnClick  ="openQuery" Text="查詢" />
                        <JQTools:JQToolItem Icon="icon-excel"  ItemType="easyui-linkbutton"    OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-add"    ItemType="easyui-linkbutton"    OnClick="AddCourseStudents" Text="新增學員" />
                        <JQTools:JQToolItem Icon="icon-save"   ItemType ="easyui-linkbutton"   OnClick="apply" Text="存檔" />
                        <JQTools:JQToolItem Icon="icon-undo"   ItemType ="easyui-linkbutton"   OnClick="cancel" Text="取消"  />
                     </TooItems>
                </JQTools:JQDataGrid>
                
            </div>
            <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="10px" DialogTop="60px" Title="選取學員" Width="450px" OnSubmited="TreeViewStudentOnSubmited">
                  <div class="easyui-layout" style="height: 530px;">
                  <div data-options="region:'center',title:'',border:false" title="">
                  <JQTools:JQTreeView ID="JQTreeViewStudent" runat="server" DataMember="StudentTree" idField="ID" parentField="ParentID" RemoteName="sCourseOpen.StudentTree" textField="Name" Checkbox="True">
                         </JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog5"  BindingObjectID="JQDataForm3" runat="server" DialogLeft="10px" DialogTop="65px" Title="因故停課" Width="520px" Closed="False">
                  <JQTools:JQDataForm ID="JQDataForm3" runat="server" DataMember="CourseOpenRecord" HorizontalColumnsCount="1" RemoteName="sCourseOpen.CourseOpenRecord" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="JQDataForm3OnApplied">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption="開課代號" Editor="text" FieldName="CourseOpenID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                             <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="87" />
                             <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="390" EditorOptions="" />
                             <JQTools:JQFormColumn Alignment="left" Caption="Mail通知" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSendMail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault3" runat="server" BindingObjectID="JQDataForm3" EnableTheming="True">
                        <Columns>
                        </Columns>
                    </JQTools:JQDefault>
            </JQTools:JQDialog>
            <JQTools:JQDialog ID="JQDialog6"  BindingObjectID="JQDataForm4" runat="server" DialogLeft="10px" DialogTop="65px" Title="設定測驗" Width="520px" Closed="False">
                  <JQTools:JQDataForm ID="JQDataForm4" runat="server" DataMember="CourseOpenRecord" HorizontalColumnsCount="1" RemoteName="sCourseOpen.CourseOpenRecord" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="JQDataForm4OnApplied">
                        <Columns>
                             <JQTools:JQFormColumn Alignment="left" Caption="開課代號" Editor="text" FieldName="CourseOpenID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                             <JQTools:JQFormColumn Alignment="left" Caption="課程代號" Editor="text" FieldName="CourseID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                             <JQTools:JQFormColumn Alignment="left" Caption="課程名稱" Editor="text" FieldName="CourseName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="390" EditorOptions="" />
                             <JQTools:JQFormColumn Alignment="left" Caption="測驗方式" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'NAME',remoteName:'sCourseOpen.TestMethod',tableName:'TestMethod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:CourseTestMethodOnSelect,panelHeight:200" FieldName="CourseTestMethod" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                             <JQTools:JQFormColumn Alignment="left" Caption="測驗試卷" Editor="infocombobox" EditorOptions="valueField:'ExamPaperID',textField:'Name',remoteName:'sCourseOpen.ExamPaper',tableName:'ExamPaper',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ExamID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="185" />
                             <JQTools:JQFormColumn Alignment="left" Caption="測驗次數" Editor="text" FieldName="CanExamTime" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                             <JQTools:JQFormColumn Alignment="left" Caption="測驗起迄" Editor="datebox" FieldName="ExamStartDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                             <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="datebox" FieldName="ExamEndDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        </Columns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="JQDefault4" runat="server" BindingObjectID="JQDataForm4" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="9999" FieldName="CanExamTime" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
            </JQTools:JQDialog>
        </div>
      </form>
</body>
</html>
