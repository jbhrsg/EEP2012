<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_PetitionQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script type="text/javascript">

         $(document).ready(function () {
             //建立日期	串聯
             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));
             $("#PetitionDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#ExpirationDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             var Date1 = $('#PetitionDate_Query').closest('td');
             var Date2 = $('#ExpirationDate_Query').closest('td').children();
             Date1.append(' - ').append(Date2);
         });

         var DetailAttachment = true;//明細檔案讀取
         function DataformLoadSucess() {
             //清空檔案下載
             $('h7').empty();
             var RawExcel = $('.info-fileUpload-value', $("#dataFormMasterAttachment").next()).val();
             if (RawExcel != '') {
                 var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/Petition/' + RawExcel }).html('<h7>[檔案下載]<h7>');
                 $('#dataFormMasterAttachment').closest('td').append(link);
             }
         }

         function queryGrid(dg) {
             var _LogUserId = getClientInfo("UserID");//登入使用者
             var FileLevel = $("#FileLevel_Query").combobox('getValue');//保密等級
             if (FileLevel == "") {
                 alert("請選擇保密等級!");
                 return false;
             }
             
             var tt = $(dg).attr('id');
             
             
             if ($(dg).attr('id') == 'dataGridView') {
                 var FileLevel = $("#FileLevel_Query").combobox('getValue');//保密等級
                 var PetitionDateB = $('#PetitionDate_Query').datebox('getValue');//申請日起                     
                 var PetitionDateE = $('#ExpirationDate_Query').datebox('getValue');//申請日迄
                 var showdatalen = 0;
                 if (FileLevel == 1) {//一般文件查詢
                     $.ajax({                         
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',  //連接的Server端，command
                         data: "mode=method&method=" + "GetNormalPetition" + "&parameters=" + _LogUserId + "," + FileLevel + "," + PetitionDateB + "," + PetitionDateE,
                         cache: false,
                         async: true,
                         success: function (data) {
                             var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                             if (rows.length > 10) {
                                 //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                                 $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                             } else {
                                 $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                             }
                         }
                     });                     
                 }
                 else {//限閱及密件流程及限閱人員查詢
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',  //連接的Server端，command
                         data: "mode=method&method=" + "GetReadPetition" + "&parameters=" + _LogUserId + "," + FileLevel + "," + PetitionDateB + "," + PetitionDateE,
                         cache: false,
                         async: true,
                         success: function (data) {
                             var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示  
                             if (rows.length > 10) {
                                 //通過loadData方法清除掉原有Grid中的舊有資料並填補分頁資料
                                 $('#dataGridView').datagrid({ loadFilter: pagerFilter, url: "" }).datagrid('loadData', rows);
                             } else {
                                 $('#dataGridView').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                             }
                         }
                     });
                 }
             }
         }
         
         function DetailformLoadSucess() {
             var RawExcel = $('.info-fileUpload-value', $("#dataFormDetailAttachment").next()).val();
             if (RawExcel != '' ) { //&& DetailAttachment
                 var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/Petition/' + RawExcel }).html('<h7>[檔案下載]</h7>');
                 $('#dataFormDetailAttachment').closest('td').append(link);
                 DetailAttachment = false;
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
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPetitionMaster.PetitionMaster" runat="server" AutoApply="True"
                DataMember="PetitionMaster" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="簽呈簽核" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" QueryMode="Panel" AllowAdd="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" MaxLength="20" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者編號" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="20" Width="80" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="100" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" MaxLength="20" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPetitionMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="FileLevel" Editor="numberbox" FieldName="FileLevel" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="主旨" Editor="text" FieldName="Subject" Format="" MaxLength="256" Width="200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="說明" Editor="text" FieldName="Description" Format="" MaxLength="2048" Width="500" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ActionPlan" Editor="text" FieldName="ActionPlan" Format="" MaxLength="1024" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="MangAdditional" Editor="text" FieldName="MangAdditional" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment" Editor="text" FieldName="Attachment" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Summary" Editor="text" FieldName="Summary" Format="" MaxLength="1024" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CarryOut" Editor="text" FieldName="CarryOut" Format="" MaxLength="1024" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" MaxLength="100" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TempAdditional" Editor="text" FieldName="TempAdditional" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Closed" Editor="text" FieldName="Closed" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExpirationDate" Editor="datebox" FieldName="ExpirationDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ReadDataEmpID" Editor="text" FieldName="ReadDataEmpID" Format="" MaxLength="0" Width="120" Visible="False" />
                </Columns>
                <%--<TooItems>
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="保密等級" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'Code',textField:'CodeNmae',remoteName:'sPetitionMaster.GradeCode',tableName:'GradeCode',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FileLevel" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" DefaultValue="1" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="簽呈日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="PetitionDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" Format="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="=" DataType="datetime" Editor="datebox" FieldName="ExpirationDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="簽呈簽核" Width="930px" DialogLeft="0px" DialogTop="30px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PetitionMaster" HorizontalColumnsCount="12" RemoteName="sPetitionMaster.PetitionMaster" IsAutoPageClose="True" IsShowFlowIcon="True" ValidateStyle="Dialog" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" VerticalGap="0" OnLoadSuccess="DataformLoadSucess" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" maxlength="20" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="20" Width="120" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" maxlength="100" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="20" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPetitionMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保密等級" Editor="infocombobox" FieldName="FileLevel" Format="" Width="120" EditorOptions="valueField:'Code',textField:'CodeNmae',remoteName:'sPetitionMaster.GradeCode',tableName:'GradeCode',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主旨" Editor="textarea" FieldName="Subject" Format="" maxlength="256" Width="800" EditorOptions="height:20" Span="12" PlaceHolder="最多可填寫256字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="說明" Editor="textarea" FieldName="Description" Format="" maxlength="2048" Width="800" EditorOptions="height:60" Span="12" PlaceHolder="最多可填寫2048字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建議" Editor="textarea" FieldName="ActionPlan" Format="" maxlength="1024" Width="800" EditorOptions="height:40" Span="12" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保留期限" Editor="datebox" FieldName="ExpirationDate" Format="" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔" Editor="infofileupload" FieldName="Attachment" Format="" maxlength="50" Width="120" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'JB_ADMIN/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="10" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核主管補充說明" Editor="textarea" FieldName="MangAdditional" Format="" maxlength="256" Width="800" EditorOptions="height:25" Span="12" PlaceHolder="最多可填寫256字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="彙總承報" Editor="textarea" FieldName="Summary" Format="" maxlength="2048" Width="800" EditorOptions="height:45" Span="12" PlaceHolder="最多可填寫204824字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總經理決行" Editor="textarea" FieldName="CarryOut" Format="" maxlength="1024" Width="800" EditorOptions="height:30" Span="12" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案狀態" Editor="infooptions" FieldName="Closed" Format="" maxlength="0" Width="80" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'未結案',value:'0'},{text:'結案',value:'1'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文件讀取名單" Editor="infocombogrid" FieldName="ReadDataEmpID" Format="" maxlength="0" Width="800" EditorOptions="panelWidth:350,valueField:'USERNAME',textField:'USERID',remoteName:'sPetitionMaster.ReaderList',tableName:'ReaderList',valueFieldCaption:'人員編號',textFieldCaption:'人員姓名',selectOnly:true,checkData:false,columns:[],cacheRelationText:false,multiple:true" Span="12" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" Width="180" maxlength="100" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" maxlength="10" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" maxlength="20" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" Format="" Width="180" maxlength="0" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="TempAdditional" Editor="text" FieldName="TempAdditional" Format="" maxlength="0" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PetitionCountersign" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sPetitionMaster.PetitionMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽職稱" Editor="infocombobox" FieldName="CountersignRole" Format="" Width="120" EditorOptions="valueField:'GroupID',textField:'GROUPNAME',remoteName:'sPetitionMaster.PetitionPosition',tableName:'PetitionPosition',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽人員" Editor="infocombobox" FieldName="CountersignEmp" Format="" Width="60" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="承辦詢問建議" Editor="text" FieldName="Description" Format="" FormatScript="ShowDetailGrid" Width="270" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽回覆" Editor="text" FieldName="MangReply" Format="" Width="270" FormatScript="ShowDetailGrid" />
                        <JQTools:JQGridColumn Alignment="left" Caption="附件檔案" Editor="text" FieldName="Attachment" Format="" Width="80" FormatScript="GriddownloadScript" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽日期" Editor="datebox" FieldName="CountersignDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                    </RelationColumns>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="930px" Title="會簽明細" DialogLeft="0px" DialogTop="20px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="PetitionCountersign" HorizontalColumnsCount="4" RemoteName="sPetitionMaster.PetitionMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="DetailformLoadSucess" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽職稱" Editor="inforefval" FieldName="CountersignRole" Format="" Width="200" EditorOptions="title:'選取職稱',panelWidth:350,remoteName:'sPetitionMaster.PetitionPosition',tableName:'PetitionPosition',columns:[{field:'GroupID',title:'職稱編號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'GROUPNAME',title:'職稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'GroupID',textField:'GROUPNAME',valueFieldCaption:'職稱編號',textFieldCaption:'職稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" Span="2" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽人員" Editor="inforefval" FieldName="CountersignEmp" Format="" Width="200" EditorOptions="title:'選取會簽人員',panelWidth:350,remoteName:'sPetitionMaster.PetitionList',tableName:'PetitionList',columns:[{field:'USERID',title:'會簽人員編號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'USERNAME',title:'會簽人員',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'會簽人員編號',textFieldCaption:'會簽人員',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" Span="2" />
                            <JQTools:JQFormColumn Alignment="left" Caption="承辦詢問建議" Editor="textarea" FieldName="MangReply" Format="" Width="780" EditorOptions="height:40" Span="4" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽者回覆" Editor="textarea" FieldName="Description" Format="" Width="780" EditorOptions="height:40" Span="4" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CountersignDate" Editor="datebox" FieldName="CountersignDate" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="Attachment" Editor="infofileupload" FieldName="Attachment" Format="" Width="120" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:false,upLoadFolder:'JB_ADMIN/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'1048'" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <br />
                <br />
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="PlusApproveList" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPetitionMaster.PetitionMaster" RowNumbers="True" Title="加簽意見" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="加籨者" Editor="text" FieldName="USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="批示意見" Editor="text" FieldName="REMARK" FormatScript="ShowDetailGrid" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="680">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="附檔" Editor="text" FieldName="ATTACHMENTS" FormatScript="FlowdownloadScript" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                    </RelationColumns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
