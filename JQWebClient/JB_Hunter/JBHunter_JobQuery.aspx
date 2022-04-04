<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_JobQuery.aspx.cs" Inherits="Template_JQueryQuery1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%-- <script src="../js/jquery.jbjob.js"></script>--%>
        <script>      
                       
            $(document).ready(function () {
                //刪除dialog下面存檔,關閉按鈕
                $("#dataGridDetail-SubmitDiv").remove();                
                //隱藏搜尋結果Grid
                //$("#divResult").hide();
                //根據職缺的條件來搜尋該範圍內的資料
                $("#bnSelect").click(function () {               
                    UpdateGrid($("#JQDataResult"));                   
                });                                               
                //dataForm
                $("#dataFormMaster").form({
                    onLoadSuccess: function () {
                        if (getEditMode($(this)) == "updated") {
                            //過濾語文搜尋條件
                            var JobID = $("#dataFormMasterJobID").val();
                            $("#dataGridDetail").datagrid('setWhere', " JobID=" + JobID);
                            //職缺餐盤條件過濾
                            $("#JQDataAssignNew").datagrid('setWhere', " n.JobID=" + JobID);
                            //預設推薦時間
                            var date = new Date();
                            var year = date.getYear() + 1900;
                            var month = date.getMonth() + 1;
                            var day = date.getDate();
                            var hour = date.getHours();
                            var minute = date.getMinutes();
                            var second = date.getSeconds();
                            var now = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
                            $('#JQDateBox1').datebox('setValue', now);
                            //新增推薦紀錄--給事件
                            $("#bnAssign").click(function () {
                                AddAssign($("#JQDataAssignNew"));
                            });
                           
                        }                        
                    }
                });              
            });
            //-------------------------------------------------Grid Search--------------------------------------------------------------
            //defaultMaster=>求得JobLang 的JobID
            function SetJobID() {
                return $("#dataFormMasterJobID").val();
            }
            // 根據輸入的條件更新 Grid
            function UpdateGrid(dg) {               
                //$("#divResult").show();
                if ($(dg).attr('id') == 'JQDataResult') {
                    var JobID = $("#dataFormMasterJobID").val();
                    var where = $(dg).datagrid('getWhere');
                    if (where.length == 0) {//--過濾已在職缺名單餐盤內人員--
                        where = where + " u.UserID not in (Select UserID from HUT_JobAssignNew where JobID=" + JobID + ")";
                    }
                    var Age1 = $("#dataFormMasterJobAge1").val();//起始年齡
                    if (Age1!='') {
                        where = where + " and DateDiff(Year,u.BirthDay,GetDate())>=" + Age1;
                    }
                    var Age2 = $("#dataFormMasterJobAge2").val();//終止年齡
                    if (Age2 != '') {
                        where = where + " and DateDiff(Year,u.BirthDay,GetDate())<=" + Age2;
                    }
                    var Gender = $("#dataFormMasterJobGender").options('getValue');//性別
                    if (Gender != '2') {
                        where = where + " and ( u.Gender=" + Gender + ")";
                    }
                    var EduID = $("#dataFormMasterEduLevelID").combobox('getValue', EduID);//最高教育程度
                    if (EduID != '') {
                        where = where + " and ( u.EduID1>=" + EduID + ")";
                    }
                    var EduSubject = $("#dataFormMasterEduSubject").val();//需求學類
                    if (EduSubject != '') {
                        where = where + " and ( u.EduSubject1 like '%"+EduSubject+"%' or u.EduSubject2 like '%"+EduSubject+"%' or u.EduSubject3 like '%"+EduSubject+"%')";
                    }
                    var EduDepart = $("#dataFormMasterEduDepart").val();//需求科系
                    if (EduDepart != '') {
                        where = where + " and ( u.Department1 like '%" + EduDepart + "%' or u.Department2 like '%" + EduDepart + "%' or u.Department3 like '%" + EduDepart + "%')";
                    }
                    //語文條件grid
                    var rows = $('#dataGridDetail').datagrid('getRows');  // Return the current page rows
                    //for (var i = 0; i < rows.length; i++) {
                    //    var LangID = rows[i].LangID;                        
                    //    var ListenLevel = rows[i].ListenLevel;
                    //    var SayLevel =rows[i].SayLevel;
                    //    var ReadLevel = rows[i].ReadLevel;
                    //    var WriteLevel = rows[i].WriteLevel;
                    //    where = where + " and (l.LangID=" + LangID + " and l.ListenLevel<=" + ListenLevel + " and l.SayLevel<=" + SayLevel + " and l.ReadLevel<=" + ReadLevel + " and l.WriteLevel<=" + WriteLevel+")";
                    //}
                    if (rows.length > 0) {
                        var JobID = $("#dataFormMasterJobID").val();
                        where = where + " and u.UserID in (select u.UserID from HUT_JobLang l" +
                            " inner join HUT_UserLang u on l.LangID=u.LangID " +
                            " where l.JobID=" + JobID + " and l.ListenLevel>=u.ListenLevel and l.SayLevel>=u.SayLevel and l.ReadLevel>=u.ReadLevel and l.WriteLevel>=u.WriteLevel " +
                            " group by u.UserID having count(u.UserID)>=" + rows.length + ")";
                    }

                    $(dg).datagrid('setWhere', where);                                        

                }                               
            }
            //-------------------------------------------------Grid Result--------------------------------------------------------------
            //Grid開啟履歷連結
            function OpenUser(val, row) {
                return $('<a>', { href: '#', onclick: 'self.parent.addTab("履歷資料維護", "JB_Hunter/JBHunter_UserCareer.aspx");', theData: row.UserID }).linkbutton({ text: "<b><div style=\"color:Blue\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
            //control顯示加入餐盤按鈕
            function ShowAddMenu() {
                if ($("#JQDataResult").datagrid('getSelections').length == 0) {
                    $("#toolItemJQDataResult加入餐盤").hide();
                } else {
                    $("#toolItemJQDataResult加入餐盤").show();
                    //$("#toolItemJQDataResult加入餐盤").linkbutton({ plain: false });
                }
            }                     
           
            function GridResultReload() {
                $('#JQDataResult').datagrid('reload');
            }
            function GridMenuReload() {
                $('#JQDataAssignNew').datagrid('reload');
            }

            // 加入餐盤 Call Method
            function AddMenu() {
                var pre = confirm("確定加入餐盤?");
               
                if (pre == true) {
                    var UserID = $("#JQDataResult").datagrid('getSelected').UserID;                                       
                    var JobID = $("#dataFormMasterJobID").val();
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.QueryResult', //連接的Server端，command
                        data: "mode=method&method=" + "AddMenu" + "&parameters=1," + UserID + "," + JobID+",1", //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != false) {
                                AssignNO = $.parseJSON(data);
                            }
                        }
                    });
                    if (AssignNO != "0" && AssignNO != "undefined") {
                        GridResultReload();
                        GridMenuReload();
                        alert("已加入職缺餐盤！");                        
                    }
                    else {
                        alert("資料有誤~~");
                    }
                }
            }
            //-------------------------------------------------Grid Menu--------------------------------------------------------------
            //新增推薦紀錄
            function AddAssign(dg) {
                var AssignID = "";
                if (dg.datagrid('getSelections').length != 0) {
                    var AssignID = $("#OptionsStep").options('getValue');
                    //var AssignID = $("select#OptionsStep").val();
                    if(AssignID!="")
                    {
                        var pre = confirm("確定新增推薦紀錄?");
                        if (pre == true) {
                            var UserID = dg.datagrid('getSelected').UserID;
                            var JobID = $("#dataFormMasterJobID").val();
                            var AssignTime = $("#JQDateBox1").datebox("getValue");
                            $.ajax({
                                type: "POST",
                                url: '../handler/jqDataHandle.ashx?RemoteName=sJobQuery.QueryResult', //連接的Server端，command
                                data: "mode=method&method=" + "AddMenu" + "&parameters=2," + UserID + "," + JobID + "," + AssignID + "," + AssignTime, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                                cache: false,
                                async: false,
                                success: function (data) {
                                    if (data != false) {
                                        AssignNO = $.parseJSON(data);
                                    }
                                }
                            });
                            if (AssignNO != "0" && AssignNO != "undefined") {
                                GridMenuReload();
                                alert("推薦紀錄新增成功！");
                                //$("#OptionsStep").options('setValue') = 0;
                            }
                            else {
                                alert("資料有誤~~");
                            }
                        }
                    }else alert("請選擇要推薦的狀態~");
                } else alert("餐盤目前無資料~");
            }
            //-------------------------------------------------推薦紀錄歷程-------------------------------------------------------------
            function aBtnOnRow(val, row) {
                return $('<a>', { href: '#', onclick: 'aBtnOnRowFunction(this)', theData: row.JobID, theData2: row.UserID }).linkbutton({ text: "<b><div style=\"color:Red\">" + val + "</div></b>", plain: true })[0].outerHTML;
            }
            var aBtnOnRowFunction = function (Target) {                            
                $("#JQDialog4").dialog("open");
                var JobID = $(Target).attr('theData');
                var UserID = $(Target).attr('theData2');
                $("#JQJobAssignLogs").datagrid('setWhere', "l.JobID='" + JobID + "' and l.UserID='"+UserID+"'");
            }
            function ControlButton2() {
                //推薦紀錄歷程-----刪除dialog下面存檔,關閉按鈕              
                $("#JQJobAssignLogs-SubmitDiv").remove();
            }
            
        </script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sJobQuery.JobQuery" runat="server" AutoApply="True"
                DataMember="JobQuery" Pagination="True" QueryTitle="條件查詢"
                Title="職缺清單" AllowDelete="False" AllowInsert="False" AllowUpdate="True" QueryMode="Panel" AlwaysClose="true" EditDialogID="JQDialog1" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="職務名稱" Editor="text" FieldName="JobName" MaxLength="128" Width="160" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需求起始日" Editor="datebox" FieldName="JobDateStd" Width="110" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需求終止日" Editor="datebox" FieldName="JobDateEnd" Width="110" Format="yyyy/mm/dd" />
                    <JQTools:JQGridColumn Alignment="center" Caption="需求人數" Editor="text" FieldName="JobNeedCount" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="JobContName" MaxLength="50" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="職稱" Editor="text" FieldName="JobContTitle" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人電話" Editor="text" FieldName="JobContTel" MaxLength="20" Width="120" />
                </Columns>
                <QueryColumns>
                    <JQTools:JQQueryColumn Caption="獵才顧問" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'HunterName',remoteName:'sHunter.HUT_Hunter',tableName:'HUT_Hunter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="HunterID" NewLine="True" RemoteMethod="False" Width="180" DefaultValue="" />
                    <JQTools:JQQueryColumn Caption="客戶名稱" Condition="=" DataType="string" Editor="inforefval" EditorOptions="title:'請選擇客戶',panelWidth:290,remoteName:'sCustomersJobs.HUT_Customer',tableName:'HUT_Customer',columns:[{field:'CustomerTel',title:'客戶編號',width:100,align:'left',table:''},{field:'CustShortName',title:'客戶簡稱',width:150,align:'left',table:''}],columnMatches:[],whereItems:[],valueField:'CustID',textField:'CustShortName',valueFieldCaption:'CustID',textFieldCaption:'CustShortName',cacheRelationText:false,checkData:false,showValueAndText:false,selectOnly:false" FieldName="j.CustID" NewLine="True" RemoteMethod="False" Width="180" />
                    <JQTools:JQQueryColumn Caption="是否有效" Condition="&gt;=" DataType="datetime" Editor="datebox" EditorOptions="" NewLine="True" RemoteMethod="False" Width="125" DefaultMethod="" FieldName="JobDateEnd" DefaultValue="_today" />
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>

     <JQTools:JQDialog ID="JQDialog1" runat="server" Title="職缺推薦作業" BindingObjectID="dataFormMaster" EditMode="Dialog" Width="920px" DialogLeft="10px" DialogTop="10px" Closed="False" >
        <div id="tt" class="easyui-tabs">
            <div id="tab1" title="職缺條件搜尋">                
            <fieldset>
            <legend>職缺條件</legend>
    
                  <table style="width:100%;">
                      <tr>
                          <td class="auto-style1">
                              <JQTools:JQDataForm ID="dataFormMaster"  runat="server" Closed="False" ContinueAdd="False" DataMember="JobQuery" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="4" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" RemoteName="sJobQuery.JobQuery" ShowApplyButton="False" title="Tab1" ValidateStyle="Hint" ParentObjectID="">
                                  <Columns>
                                      <JQTools:JQFormColumn Alignment="left" Caption="職務代號" Editor="numberbox" FieldName="JobID" Visible="False" Width="120" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="職務名稱" Editor="text" FieldName="JobName" ReadOnly="False" Span="1" Width="150" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="需求起始年齡" Editor="numberbox" FieldName="JobAge1" Span="1" Width="60" Visible="True" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="需求終止年齡" Editor="numberbox" FieldName="JobAge2" Span="1" Width="60" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="需求性別" Editor="infooptions" FieldName="JobGender" Span="1" Width="130" EditorOptions="title:'JQOptions',panelWidth:0,remoteName:'sReferences.HUT_ZGENType',tableName:'HUT_ZGENType',valueField:'ID',textField:'Gender',columnCount:3,multiSelect:false,openDialog:false,selectOnly:false,items:[]" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="教育程度" Editor="infocombobox" FieldName="EduLevelID" Width="120" EditorOptions="valueField:'EduID',textField:'EduName',remoteName:'sReferences.HUT_ZEduLevel',tableName:'HUT_ZEduLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="需求學類" Editor="text" FieldName="EduSubject" Span="1" Width="100" />
                                      <JQTools:JQFormColumn Alignment="left" Caption="需求科系" Editor="text" FieldName="EduDepart" Width="100" />
                                  </Columns>
                              </JQTools:JQDataForm>                            

                              <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" DataMember="HUT_JobLang" DeleteCommandVisible="True" DuplicateCheck="True" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RemoteName="sJobLang.HUT_JobLang" Title="語文條件" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" ColumnsHibeable="False" RecordLockMode="None" Width="550px">
                                  <Columns>
                                      <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Visible="False" Width="90" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="語文種類" Editor="infocombobox" FieldName="LangID" Width="90" EditorOptions="valueField:'LangID',textField:'LangName',remoteName:'sJobLang.HUT_ZLangType',tableName:'HUT_ZLangType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="聽" Editor="infocombobox" FieldName="ListenLevel" Width="90" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="說" Editor="infocombobox" FieldName="SayLevel" Width="90" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="讀" Editor="infocombobox" FieldName="ReadLevel" Width="90" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="寫" Editor="infocombobox" FieldName="WriteLevel" Width="90" EditorOptions="valueField:'ID',textField:'Level',remoteName:'sJobLang.HUT_ZLangLevel',tableName:'HUT_ZLangLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                                      <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="90" />
                                  </Columns>
                                  <TooItems>
                                      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" />
                                      <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="Update" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="Delete" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="True" />
                                      <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                                      <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="Query" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="Export" Visible="False" />
                                  </TooItems>
                              </JQTools:JQDataGrid>
                              <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                                  <Columns>
                                      <JQTools:JQDefaultColumn DefaultValue="" FieldName="JobID" RemoteMethod="False" DefaultMethod="SetJobID" />
                                      <JQTools:JQDefaultColumn DefaultValue="_usercode" FieldName="CreateBy" RemoteMethod="True" />
                                      <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                  </Columns>
                              </JQTools:JQDefault>
                              <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                                  <Columns>
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="LangID" RemoteMethod="True" ValidateMessage="請選擇語文種類" ValidateType="None" />
                                      <JQTools:JQValidateColumn CheckNull="True" FieldName="ListenLevel" RemoteMethod="True" ValidateMessage="聽程度?" ValidateType="None" />
                                  </Columns>
                              </JQTools:JQValidate>
                          </td>
                          <td valign="bottom">
                              <input id="bnSelect" type="button" value="搜尋" />
                          </td>
                      </tr>
                </table>
                
                 </fieldset><div id="divResult">
                    <JQTools:JQDataGrid ID="JQDataResult" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="True" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="QueryResult" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.QueryResult" Title="搜尋結果" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="100%" OnLoadSuccess="ShowAddMenu" EditDialogID="JQDialog2">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="130" FormatScript="OpenUser" />
                            <JQTools:JQGridColumn Alignment="left" Caption="最新履歷狀態" Editor="text" FieldName="AssignName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="90" />
                            <JQTools:JQGridColumn Alignment="left" Caption="最新推薦資訊" Editor="text" FieldName="LastStatus" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="280" />
                            <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                            <JQTools:JQGridColumn Alignment="left" Caption="eMail" Editor="text" FieldName="eMail1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="230" />
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem ItemType="easyui-linkbutton" OnClick="AddMenu" Text="加入餐盤" Visible="True" Icon="icon-ok" />
                        </TooItems>
                    </JQTools:JQDataGrid>
                </div>
</div>
            <div id="tab2" title="推薦餐盤" style="padding:20px;" >		        
                <table style="width:100%;">
                    <tr>
                        <td>
                            <JQTools:JQOptions ID="OptionsStep" runat="server" ColumnCount="0" DialogWidth="610" DisplayMember="AssignName" EnableTheming="True" OpenDialog="False" RemoteName="sJobQuery.AssignStep" ValueMember="AssignID">
                            </JQTools:JQOptions>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <JQTools:JQDateBox ID="JQDateBox1" runat="server" ShowTimeSpinner="True" Width="70px" />
                            <input id="bnAssign" type="button" value="新增推薦紀錄" />
                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            

                            <JQTools:JQDataGrid ID="JQDataAssignNew" data-options="pagination:true,view:commandview" RemoteName="sJobQuery.JobAssignNew" runat="server" AutoApply="True"
                DataMember="JobAssignNew" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog2" Title="" DeleteCommandVisible="True" ViewCommandVisible="False" OnDeleted="GridMenuReload">
                <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="推薦狀態" Editor="text" FieldName="AssignName" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="95" FormatScript="aBtnOnRow" />
                        <JQTools:JQGridColumn Alignment="left" Caption="推薦時間" Editor="text" FieldName="AssignTime" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="133" Format="yyyy/mm/dd HH:MM:SS" />
                        <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Name" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="行動電話" Editor="text" FieldName="MobileNo1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="eMail" Editor="text" FieldName="eMail1" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="230" />
                </Columns>
            </JQTools:JQDataGrid>

                            <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMaster1" Title="推薦備註維護" DialogLeft="150px" DialogTop="150px" Width="600px" Height="300px">
                                <JQTools:JQDataForm ID="dataFormMaster1" runat="server" DataMember="JobAssignNew" HorizontalColumnsCount="2" RemoteName="sJobQuery.JobAssignNew">
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="AssignNO" Editor="text" FieldName="AssignNO" maxlength="0" Width="180" Visible="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" maxlength="0" Width="180" Visible="False" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="JobID" Editor="numberbox" FieldName="JobID" Width="180" Visible="False" />
                                    </Columns>
                                </JQTools:JQDataForm>
                                <JQTools:JQDataGrid ID="dataGridDetail1" runat="server" AutoApply="False" DataMember="AssignNotes" EditDialogID="" Pagination="False" ParentObjectID="dataFormMaster1" RemoteName="sJobQuery.JobAssignNew" Title="" ViewCommandVisible="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="True" Height="200px" Width="520px">
                                    <Columns>
                                        <JQTools:JQGridColumn Alignment="left" Caption="推薦編號" Editor="text" FieldName="AssignNO" Width="120" Visible="False" />
                                        <JQTools:JQGridColumn Alignment="right" Caption="職務代號" Editor="numberbox" FieldName="JobID" Width="120" Visible="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="推薦備註" Editor="textarea" FieldName="AssignNotes" Width="450" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Width="90" Visible="False" />
                                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Width="90" Format="yyyy/mm/dd" Visible="False" />
                                    </Columns>
                                    <RelationColumns>
                                        <JQTools:JQRelationColumn FieldName="AssignNO" ParentFieldName="AssignNO" />
                                    </RelationColumns>
                                    <TooItems>
                                      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="Insert" />                                     
                                      <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="Apply" Visible="False" />
                                      <JQTools:JQToolItem Icon="icon-cancel" ItemType="easyui-linkbutton" OnClick="cancel" Text="Cancel" Visible="True" />
                                    </TooItems>
                                </JQTools:JQDataGrid>
                                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormDetail" EditMode="Switch">
                                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="AssignNotes" DuplicateCheck="False" HorizontalColumnsCount="2" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsShowFlowIcon="False" ParentObjectID="dataFormMaster" RemoteName="sJobQuery.JobAssignNew" ShowApplyButton="False" ValidateStyle="Hint">
                                        <Columns>
                                            <JQTools:JQFormColumn Alignment="left" Caption="推薦編號" Editor="text" FieldName="AssignNO" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="職務代號" Editor="numberbox" FieldName="JobID" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="推薦備註" Editor="text" FieldName="AssignNotes" Width="280" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Visible="False" Width="120" />
                                            <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Visible="False" Width="120" />
                                        </Columns>
                                        <RelationColumns>
                                            <JQTools:JQRelationColumn FieldName="AssignNO" ParentFieldName="AssignNO" />
                                            <JQTools:JQRelationColumn FieldName="JobID" ParentFieldName="JobID" />
                                        </RelationColumns>
                                    </JQTools:JQDataForm>
                                </JQTools:JQDialog>
                                <JQTools:JQDefault ID="defaultMaster1" runat="server" BindingObjectID="dataFormMaster1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="validateMaster1" runat="server" BindingObjectID="dataFormMaster1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                </JQTools:JQValidate>
                                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail1" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                    <Columns>
                                        <JQTools:JQDefaultColumn DefaultMethod="SetJobID" FieldName="JobID" RemoteMethod="False" />
                                        <JQTools:JQDefaultColumn FieldName="CreateBy" RemoteMethod="True" DefaultValue="_usercode" />
                                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                        <JQTools:JQDefaultColumn DefaultMethod="" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                    </Columns>
                                </JQTools:JQDefault>
                                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                                </JQTools:JQValidate>
                              <%--  <div id="JQDialog4">--%>
                                    <JQTools:JQDialog ID="JQDialog4" runat="server" BindingObjectID="" EditMode="Dialog" Title="推薦紀錄歷程" HorizontalAlign="Justify" DialogLeft="10px" DialogTop="10px" Width="400px" Height="330px">
                                    <JQTools:JQDataGrid ID="JQJobAssignLogs" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="AssignLogs" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="7,10,20,30,40,50" PageSize="7" Pagination="True" ParentObjectID="" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJobQuery.AssignLogs" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="ControlButton2">
                                        <Columns>
                                            <JQTools:JQGridColumn Alignment="left" Caption="AssignNO" Editor="text" FieldName="AssignNO" Visible="False" Width="90" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="JobName" Editor="text" FieldName="JobName" Visible="False" Width="90" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="推薦時間" Editor="text" FieldName="AssignTime" Frozen="False" MaxLength="0" ReadOnly="False" Sortable="False" Visible="True" Width="177" Format="yyyy/mm/dd HH:MM:SS" />
                                            <JQTools:JQGridColumn Alignment="left" Caption="推薦狀態" Editor="text" FieldName="AssignName" Width="120" />
                                        </Columns>
                                    </JQTools:JQDataGrid>
                                    </JQTools:JQDialog>
                              <%--  </div>--%>

                            </JQTools:JQDialog>






























                        </td>
                    </tr>
                </table>               
            </div>               
        </div>
    </JQTools:JQDialog>        
</form>
</body>
</html>
