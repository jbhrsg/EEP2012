<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_Teacher.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>講師資料維護</title>
    <style>
        .tree-node-clicked {
            background: #ff7f50;            
        }
    </style>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var Btn1 = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var nodes = $('#JQTreeViewTeacher').tree('getChecked');
                $.each(nodes, function (index, node) {
                    $('#JQTreeViewTeacher').tree('uncheck', node.target);
                    $(node.target).removeClass('.tree-node-clicked');
                });
                var TeacherGroupID = $('#dataFormMasterTeacherGroupID').combobox('getValue');
                if (TeacherGroupID != 3) {
                    alert('注意!!講師群組為「課程講師群」才需設定選取講師');
                    return false;
                }
                var root = $('#JQTreeViewTeacher').tree('getRoot');
                $('#JQTreeViewTeacher').tree('uncheck', root.target);
                var TeacherIDS = $('#dataFormMasterTeacherIDs').val();
                JQTreeViewTeacherSetChecked(TeacherIDS);
                openForm('#JQDialog3', {}, "", 'dialog');
            }).linkbutton({ text: '選取講師' });
            $('#dataFormMasterTeacherIDsName').after(Btn1);
            $('#JQTreeViewTeacher').tree({
                onlyLeafCheck: true,
                onSelect: function (node) {
                },
                onCheck: function (node, checked) {
                    if (checked) $(node.target).addClass('tree-node-clicked');
                    else $(node.target).removeClass('tree-node-clicked');
                },
                onLoadSuccess: function () {
                //$(this).tree('collapseAll');
                }
            });
            var Btn2 = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var nodes = $('#JQTreeViewCourse').tree('getChecked');
                $.each(nodes, function (index, node) {
                    $('#JQTreeViewCourse').tree('uncheck', node.target);
                    $(node.target).removeClass('.tree-node-clicked');
                });
                openForm('#JQDialog2', {}, "", 'dialog');
            }).linkbutton({ text: '選取課程'});
            $('#JQTreeViewCourse').tree({
                onlyLeafCheck: false,
                onSelect: function (node) {
                },
                onCheck: function (node, checked) {
                    if (checked) $(node.target).addClass('tree-node-clicked');
                    else $(node.target).removeClass('tree-node-clicked');
                },		
                onLoadSuccess: function () {
                    //$(this).tree('collapseAll');
                }
            });
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterDispatchAreaID', '#dataFormMasterDispatchAreaManager'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //設定 Grid QueryColunm Windows width=480px
            var dgid = $('#dataGridView');
            var queryPanel = getInfolightOption(dgid).queryDialog;
            if (queryPanel)
                $(queryPanel).panel('resize', { width: 480 });
            $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //將QUERY PANEL 按鈕置中
            $('.infosysbutton-q', '#dataGridView').closest('td').attr('align', 'middle');
            //
        })
        //選取講師後存檔
        function TreeViewTeacherOnSubmited() {
            var nodes = $('#JQTreeViewTeacher').tree('getChecked');
            var TeacherIDs = '';
            var TeacherIDsName = '';
            var i = 1
            $.each(nodes, function (index, node) {
                if (i >1) {
                    TeacherIDs = TeacherIDs + ',' + node.id;
                    TeacherIDsName = TeacherIDsName + ',' + node.text;
                }
                else {
                    TeacherIDs = TeacherIDs + node.id;
                    TeacherIDsName = TeacherIDsName + node.text;
                }
                i = i + 1;
            });
            SetTeacherGroupIDs(TeacherIDs);
            $('#dataFormMasterTeacherIDs').val(TeacherIDs)
            $('#dataFormMasterTeacherIDsName').val(TeacherIDsName)
            return true;
        }
        //選取課程後存檔
        function TreeViewCourseOnSubmited() {
            var nodes = $('#JQTreeViewCourse').tree('getChecked');
            var CourseIDs = '';
            var CourseIDsName = '';
            var i = 1
            $.each(nodes, function (index, node) {
                if (i > 1) {
                    CourseIDs = CourseIDs + ',' + node.id;
                }
                else {
                    CourseIDs = CourseIDs + node.id;
                }
                i = i + 1;
            });
            SetTeacherCanCourseIDs(CourseIDs);
            $('#dataGridView').datagrid('reload');
            return true;
        }
        function JQTreeViewTeacherSetChecked(IDstr) {
            if (IDstr != '') {
                $.each(IDstr.split(","), function (i, id) {
                    var node = $('#JQTreeViewTeacher').tree('find', id);
                    $(node.target).addClass('tree-node-clicked');
                    if (node != null) $('#JQTreeViewTeacher').tree('check', node.target);
                });
            }
        }
        function JQTreeViewCourseSetChecked(IDstr) {
            if (IDstr != '') {
                $.each(IDstr.split(","), function (i, id) {
                    var node = $('#JQTreeViewCourse').tree('find', id);
                    $(node.target).addClass('tree-node-clicked');
                    if (node !=null) $('#JQTreeViewCourse').tree('check', node.target);
                });
            }
        }
        function SetTeacherCourse() {
            var nodes = $('#JQTreeViewCourse').tree('getChecked');
            $.each(nodes, function (index, node) {
               $('#JQTreeViewCourse').tree('uncheck', node.target);
               $(node.target).removeClass('.tree-node-clicked');
            });
            var root = $('#JQTreeViewCourse').tree('getRoot');
            $('#JQTreeViewCourse').tree('uncheck', root.target);
            var RowData = $("#dataGridView").datagrid('getSelected');
            var CanTeachCourseIDs = RowData.CanTeachCourseIDs;
            JQTreeViewCourseSetChecked(CanTeachCourseIDs);
            openForm('#JQDialog2', {}, "", 'dialog');
       }
       //將選擇的課程代號存入Teacher與StudentsCourses
        function SetTeacherCanCourseIDs(CourseIDs) {
           var UserID = getClientInfo("UserID");
           var RowData = $("#dataGridView").datagrid('getSelected');
           var TeacherID = RowData.TeacherID;
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sTeacher.Teacher', //連接的Server端，command
               data: "mode=method&method=" + "SetTeacherCanCourseIDs" + " &parameters=" + TeacherID + "*" + CourseIDs + "*" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   if (data == "True") {
                      
                   }
                   else {
                       alert("存入失敗")
                   }
              }
           });
       }
       //將選擇的講師代號存入TeachersGroups
       function SetTeacherGroupIDs(TeacherIDs) {
           var UserID = getClientInfo("UserID");
           var TeacherID = $('#dataFormMasterTeacherID').val();
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sTeacher.Teacher', //連接的Server端，command
               data: "mode=method&method=" + "SetTeacherGroupIDs" + " &parameters=" + TeacherID + "*" + TeacherIDs + "*" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   if (data == "True") {

                   }
                   else {
                       alert("存入失敗")
                   }
               }
           });
       }
       function TeacherGroupIDOnSelect(rowData) {
           var TeacherID = GetTeacherID(rowData.TeacherGroupID);
           $('#dataFormMasterTeacherID').val(TeacherID);
       }
       function GetTeacherID(TeacherGroupID) {
           var UserID = getClientInfo("UserID");
           var ReturnStr = "";
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sTeacher.Teacher', //連接的Server端，command
               data: "mode=method&method=" + "GetTeacherID" + "&parameters=" + TeacherGroupID+","+UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
       function dataFormMasterLoadSucess() {
             GetSetCourseIDsNames();
       }
       function GetSetCourseIDsNames() {
            var TeacherID = $('#dataFormMasterTeacherID').val()
            $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sTeacher.Teacher', //連接的Server端，command
               data: "mode=method&method=" + "GetCourseIDsNames" + "&parameters=" + TeacherID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
               cache: false,
               async: false,
               success: function (data) {
                   var rows = $.parseJSON(data);
                   if (rows.length > 0) {
                       $("#dataFormMasterCanTeachCourseIDs").val(rows[0].CanTeachCourseIDs);
                       $("#dataFormMasterCourseIDsName").val(rows[0].CourseIDsName);
                   }
               }
           }
           );
       }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sTeacher.Teacher" runat="server" AutoApply="True"
                DataMember="Teacher" Pagination="True" QueryTitle="講師查詢" EditDialogID="JQDialog1"
                Title="講師維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Window" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="900px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="講師群組" Editor="infocombobox" FieldName="TeacherGroupID" Format="" Width="80" EditorOptions="valueField:'TeacherGroupID',textField:'TeacherGroupName',remoteName:'sTeacher.TeacherGroup',tableName:'TeacherGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="講師代號" Editor="text" FieldName="TeacherID" Format="" MaxLength="0" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="講師姓名" Editor="text" FieldName="TeacherName" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="center" Caption="課程數" Editor="text" FieldName="CourseNum" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="45" />
                    <JQTools:JQGridColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="任職公司" Editor="text" FieldName="TeacherCompany" Format="" MaxLength="0" Width="240" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="TeacherTitle" Format="" MaxLength="0" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WorkingHistory" Editor="text" FieldName="WorkingHistory" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProSkill" Editor="text" FieldName="ProSkill" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Licence" Editor="text" FieldName="Licence" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CanTeachCourseIDs" Editor="text" FieldName="CanTeachCourseIDs" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Desciption" Editor="text" FieldName="Desciption" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"
                        OnClick="SetTeacherCourse" Text="設定授課課程"/>

                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="講師群組" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'TeacherGroupID',textField:'TeacherGroupName',remoteName:'sTeacher.TeacherGroup',tableName:'TeacherGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="TeacherGroupID" IsNvarChar="False" NewLine="True" RemoteMethod="False" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="講師姓名" Condition="%%" DataType="string" Editor="text" FieldName="TeacherName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="講師維護" DialogLeft="15px" DialogTop="50px" Width="600px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Teacher" HorizontalColumnsCount="1" RemoteName="sTeacher.Teacher" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterLoadSucess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="講師群組" Editor="infocombobox" FieldName="TeacherGroupID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'TeacherGroupID',textField:'TeacherGroupName',remoteName:'sTeacher.TeacherGroup',tableName:'TeacherGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:TeacherGroupIDOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="講師代號" Editor="text" FieldName="TeacherID" Format="" Width="117" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="講師名稱" Editor="text" FieldName="TeacherName" Format="" Width="240" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="講代明細" Editor="text" FieldName="TeacherIDs" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="360" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="講師明細" Editor="textarea" FieldName="TeacherIDsName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="360" EditorOptions="height:45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="授課課程" Editor="text" FieldName="CanTeachCourseIDs" Format="" maxlength="0" Width="360" EditorOptions="" Visible="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="教授課程" Editor="textarea" EditorOptions="height:75" FieldName="CourseIDsName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職公司" Editor="text" FieldName="TeacherCompany" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="職稱" Editor="text" FieldName="TeacherTitle" Format="" maxlength="0" Width="360" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作資歷" Editor="textarea" FieldName="WorkingHistory" Format="" maxlength="0" Width="360" EditorOptions="height:45" />
                        <JQTools:JQFormColumn Alignment="left" Caption="專業技能" Editor="textarea" FieldName="ProSkill" Format="" maxlength="0" Width="360" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="擁有證照" Editor="textarea" FieldName="Licence" Format="" maxlength="0" Width="360" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Desciption" Format="" maxlength="0" Width="360" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="StudentID" Editor="text" FieldName="StudentID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="TeacherID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
              <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="15px" DialogTop="50px" Title="選取講師" Width="450px" OnSubmited="TreeViewTeacherOnSubmited" Height="500px">
                  <div class="easyui-layout" style="height: 500px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQTreeViewTeacher" runat="server" DataMember="TeacherTree" idField="ID" parentField="ParentID" RemoteName="sTeacher.TeacherTree" textField="Name" Checkbox="True" Width="500px">
                     <Columns>
                         <JQTools:JQTreeViewColumn Caption="IsClass" FieldName="IsClass" NewLine="True" />
                     </Columns>
                        </JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
                 <JQTools:JQDialog ID="JQDialog2" runat="server" DialogLeft="15px" DialogTop="50px" Title="選取課程" Width="450px" OnSubmited="TreeViewCourseOnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQTreeViewCourse" runat="server" DataMember="CourseTree" idField="CourseID" parentField="CourseParentID" RemoteName="sTeacher.CourseTree" textField="CourseNAME" Checkbox="True">
                         </JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
