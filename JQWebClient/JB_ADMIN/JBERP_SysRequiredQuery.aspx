<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SysRequiredQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

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
             $("#ApplyDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#CreateDate_Query").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));
             var Date1 = $('#ApplyDate_Query').closest('td');
             var Date2 = $('#CreateDate_Query').closest('td').children();
             Date1.append(' - ').append(Date2);
         });

         function queryGrid(dg) {
             if ($(dg).attr('id') == 'dataGridView') {
                 var DateB = $('#ApplyDate_Query').datebox('getValue');//申請日起
                 var DateE = $('#CreateDate_Query').datebox('getValue');//申請日迄
                 var ApplyEmpID = $("#ApplyEmpID_Query").combobox('getValue');//申請人
                 var ApplyOrg_NO = $('#ApplyOrg_NO_Query').combobox('getValue');//申請部門                 
                 var RequiredType = $('#RequiredType_Query').options('getValue');//需求類別
                 var FlowType = $('#flowflag_Query').options('getValue');
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sSystemRequired.SystemRequired',  //連接的Server端，command
                     data: "mode=method&method=" + "GetSystemRequired" + "&parameters=" + DateB + "," + DateE + "," + ApplyEmpID + "," + ApplyOrg_NO + "," + RequiredType+","+FlowType,
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
         
         function DataformLoadSucess() {
             var _RequiredType = $('#dataFormMasterRequiredType').options('getValue');
             if (_RequiredType != "A") {
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
                 }
             //清空檔案下載
             $('h7').empty();
             var FormName = '#dataFormMasterAttachment';
             //新增需求檔案下載
             for (var i = 0; i < 3; i++) {
                 var RawExcel = $('.info-fileUpload-value', $(FormName + String(i + 1)).next()).val();
                 if (RawExcel != '') {
                     var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + RawExcel }).html('<h7>[檔案下載]</h7>');
                     $(FormName + String(i + 1)).closest('td').append(link);
                 }
             }

             ////工程師驗收檔案下載
             for (var i = 0; i < 2; i++) {
                 var RawExcel = $('.info-fileUpload-value', $("#dataFormMasterPGTestAttachment" + String(i + 1)).next()).val();
                 if (RawExcel != '') {
                     var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + RawExcel }).html('<h7>[檔案下載]</h7>');
                     $("#dataFormMasterPGTestAttachment" + String(i + 1)).closest('td').append(link);
                 }
             }

             //線上系統上線完成畫面檔案下載
             var OnlineRawExcel = $('.info-fileUpload-value', $("#dataFormMasterOnlineAttachment").next()).val();
             if (OnlineRawExcel != '') {
                 var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + OnlineRawExcel }).html('<h7>[檔案下載]</h7>');
                 $("#dataFormMasterOnlineAttachment").closest('td').append(link);
             }

             //驗收人員驗數檔案下載
             var CloseRawExcel = $('.info-fileUpload-value', $("#dataFormMasterCheckAttachment").next()).val();
             if (CloseRawExcel != '') {
                 var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/SystemRequired/' + CloseRawExcel }).html('<h7>[檔案下載]</h7>');
                 $("#dataFormMasterCheckAttachment").closest('td').append(link);
             }

             
         }

         //Grid資料自動換行
         function ShowDetailGrid(value) {
             return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
         }

         function ShowDetailGrid(value) {
             if (value == "Z")
                 value = "流程已完成";
             if (value == "P")
                 value = "流程中";            
             return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
         }

         function onLoadSuccess_Grid() {

         }
         </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sSystemRequired.SystemRequired" runat="server" AutoApply="True"
                DataMember="SystemRequired" Pagination="True" QueryTitle="維運功能需求單查詢" EditDialogID="JQDialog1"
                Title="" AllowDelete="False" AllowUpdate="False" QueryMode="Panel" AlwaysClose="True" AllowAdd="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="系統需求編號" Editor="text" FieldName="SysRequiredNo" Format="" MaxLength="20" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="65" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者編號" Editor="infocombobox" FieldName="ApplyEmpID" Format="" MaxLength="20" Width="80" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" MaxLength="20" Width="100" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sSystemRequired.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="系統名稱" Editor="text" FieldName="SystemName" Format="" MaxLength="256" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求說明" Editor="text" FieldName="Description" Format="" MaxLength="2048" Width="500" FormatScript="ShowDetailGrid" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExpectedBenefits" Editor="text" FieldName="ExpectedBenefits" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="需求類別" Editor="text" FieldName="RequiredType" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment1" Editor="text" FieldName="Attachment1" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment2" Editor="text" FieldName="Attachment2" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment3" Editor="text" FieldName="Attachment3" Format="" MaxLength="50" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="指派工程師" Editor="infocombobox" FieldName="ProjectLeader" Format="" MaxLength="20" Width="80" Visible="True" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ProjectLeaderDescr" Editor="text" FieldName="ProjectLeaderDescr" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimatedDate" Format="" Width="65" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestTarget" Editor="text" FieldName="PGTestTarget" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestItems" Editor="text" FieldName="PGTestItems" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DevelopTechnology" Editor="text" FieldName="DevelopTechnology" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Confidential" Editor="text" FieldName="Confidential" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Integrity" Editor="text" FieldName="Integrity" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Availability" Editor="text" FieldName="Availability" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EvaluationResult" Editor="text" FieldName="EvaluationResult" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGEvaluateDescr" Editor="text" FieldName="PGEvaluateDescr" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestAttachment1" Editor="text" FieldName="PGTestAttachment1" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PGTestAttachment2" Editor="text" FieldName="PGTestAttachment2" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="實際完成日" Editor="datebox" FieldName="CompledDate" Format="" Width="65" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Checker" Editor="text" FieldName="Checker" Format="" MaxLength="20" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDescr" Editor="text" FieldName="CheckDescr" Format="" MaxLength="1024" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckDate" Editor="datebox" FieldName="CheckDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckAttachment" Editor="text" FieldName="CheckAttachment" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OnlineDate" Editor="datebox" FieldName="OnlineDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BackupDescr" Editor="text" FieldName="BackupDescr" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="OnlineAttachment" Editor="text" FieldName="OnlineAttachment" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SysUpDate" Editor="datebox" FieldName="SysUpDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CloseDescr" Editor="text" FieldName="CloseDescr" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" MaxLength="10" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="flowflag" Format="" MaxLength="10" Width="120" Visible="False" FormatScript="ShowDetailGrid" />
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
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請日期" Condition="&gt;=" DataType="datetime" Editor="datebox" FieldName="ApplyDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" TableName="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="&lt;" DataType="datetime" Editor="datebox" FieldName="CreateDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請者編號" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="申請部門" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sSystemRequired.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="需求類別	" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'sSystemRequired.SystemType',tableName:'SystemType',valueField:'Code',textField:'CodeNmae',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="RequiredType" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單狀態" Condition="%" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:200,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'流程中',value:'P'},{text:'流程已完成',value:'Z'}]" FieldName="flowflag" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="維運功能需求單" Width="930px" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SystemRequired" HorizontalColumnsCount="8" RemoteName="sSystemRequired.SystemRequired" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="DataformLoadSucess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="系統需求編號" Editor="text" FieldName="SysRequiredNo" Format="" maxlength="20" Width="100" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者編號" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="20" Span="2" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者姓名" Editor="text" FieldName="ApplyEmpName" Format="" maxlength="20" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sSystemRequired.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" Format="" maxlength="20" Span="2" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="90" Span="2" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgNOParent" Editor="text" FieldName="OrgNOParent" Format="" maxlength="0" Width="180" Visible="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求類別" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'sSystemRequired.SystemType',tableName:'SystemType',valueField:'Code',textField:'CodeNmae',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="RequiredType" Format="" maxlength="0" Span="8" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統名稱" Editor="textarea" FieldName="SystemName" Format="" maxlength="256" Width="780" EditorOptions="height:20" Span="8" PlaceHolder="最多可填寫256字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求說明" Editor="textarea" FieldName="Description" Format="" maxlength="2048" Width="780" EditorOptions="height:50" Span="8" PlaceHolder="最多可填寫2048字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預期效果" Editor="textarea" FieldName="ExpectedBenefits" Format="" maxlength="0" Width="780" EditorOptions="height:30" Span="8" PlaceHolder="最多可填寫1024字元!" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="infofileupload" FieldName="Attachment1" Format="" maxlength="50" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="infofileupload" FieldName="Attachment2" Format="" maxlength="50" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="infofileupload" FieldName="Attachment3" Format="" maxlength="50" Width="280" Visible="True" Span="8" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程師" Editor="infocombobox" FieldName="ProjectLeader" Format="" maxlength="20" Width="100" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求成案" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[{text:'成案',value:'true'},{text:'未成案',value:'false'}]" FieldName="RequiredCase" MaxLength="0" Span="2" Width="150" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計完成日" Editor="datebox" FieldName="EstimatedDate" Format="" maxlength="0" Width="90" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實際完成日" Editor="datebox" FieldName="CompledDate" Format="" Width="90" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工程師描述" Editor="textarea" FieldName="ProjectLeaderDescr" Format="" maxlength="0" Width="780" EditorOptions="height:50" Span="8" PlaceHolder="最多可填寫1024字元!" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="開發技術審查" Editor="infooptions" FieldName="DevelopTechnology" Format="" maxlength="0" Width="500" Visible="True" EditorOptions="title:'JQOptions',panelWidth:500,remoteName:'sSystemRequired.DevelopTechnology',tableName:'DevelopTechnology',valueField:'Code',textField:'CodeNmae',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查機敏性(C)" Editor="infooptions" FieldName="Confidential" Format="" maxlength="0" Width="60" Visible="True" EditorOptions="title:'機敏性(C) 模組數據涉及機敏程度可能遭受侵害威脅程度',panelWidth:350,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查完整性(I)" Editor="infooptions" FieldName="Integrity" Format="" maxlength="0" Width="60" Visible="True" EditorOptions="title:'完整性(I)模組功能其遭受算改成或人為疏失可能遭受侵害威脅程度',panelWidth:400,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="審查可行性(A)" Editor="infooptions" FieldName="Availability" Format="" maxlength="0" Width="60" Visible="True" EditorOptions="title:'可行性(A)模組因系統異常、故障等造成可用性威脅程度',panelWidth:350,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:3,multiSelect:false,openDialog:true,selectAll:false,selectOnly:true,items:[{text:'高',value:'高'},{text:'中',value:'中'},{text:'低',value:'低'}]" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="評估審查總結" Editor="infooptions" FieldName="EvaluationResult" Format="" maxlength="0" Width="300" Visible="True" EditorOptions="title:'JQOptions',panelWidth:300,remoteName:'sSystemRequired.EvaluationResult',tableName:'EvaluationResult',valueField:'Code',textField:'CodeNmae',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試目標" Editor="textarea" FieldName="PGTestTarget" Format="" maxlength="1024" Width="780" Visible="True" EditorOptions="height:30" PlaceHolder="最多可填寫1024字元!" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試項目與結果" Editor="textarea" FieldName="PGTestItems" Format="" maxlength="1024" Width="780" Visible="True" EditorOptions="height:50" PlaceHolder="最多可填寫1024字元!" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試附件1" Editor="infofileupload" FieldName="PGTestAttachment1" Format="" maxlength="0" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'500'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="測試附件2" Editor="infofileupload" FieldName="PGTestAttachment2" Format="" maxlength="0" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收人" Editor="infocombobox" FieldName="Checker" Format="" maxlength="20" Width="100" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sSystemRequired.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收日期" Editor="datebox" FieldName="CheckDate" Format="" maxlength="0" Width="90" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收說明" Editor="textarea" FieldName="CheckDescr" Format="" Width="780" MaxLength="1024" Span="8" EditorOptions="height:30" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="驗收附件" Editor="infofileupload" FieldName="CheckAttachment" Format="" maxlength="0" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上線日期" Editor="datebox" FieldName="OnlineDate" Format="" Width="90" Visible="True" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="系統上線完成畫面" Editor="infofileupload" FieldName="OnlineAttachment" Format="" maxlength="0" Width="280" Visible="True" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf|zip',isAutoNum:false,upLoadFolder:'JB_ADMIN/SystemRequired',showButton:true,showLocalFile:false,fileSizeLimited:'2048'" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備份說明" Editor="textarea" FieldName="BackupDescr" Format="" maxlength="0" Width="780" Visible="True" Span="8" />
                        <JQTools:JQFormColumn Alignment="left" Caption="更版日期" Editor="datebox" FieldName="SysUpDate" Format="" Width="90" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案說明" Editor="textarea" FieldName="CloseDescr" Format="" maxlength="0" Width="780" EditorOptions="height:25" Span="8" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷假員工" Editor="text" FieldName="CreateBy" Format="" maxlength="10" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" maxlength="1" Width="180" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
