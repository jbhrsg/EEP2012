<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_ShortTerm.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var backcolor = "#cbf1de"
        $(document).ready(function () {
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
            });
            $('#dataFormMasterIsSettleAccount').click(function()
            {
                var a = $(this).checkbox('getValue');
                var Dt = new Date();
                var rr = Dt.format("yyyy/MM/dd HH:mm:ss")
                if (a == 1) {
                    $('#dataFormMasterSettleAccountDate').datebox('setValue', rr);
                }
                else
                    $('#dataFormMasterSettleAccountDate').datebox('setValue', '');
            });
            $("#dataFormMasterTargets").combobox('enable');
            var gf = $("#dataFormMasterEmployerID").combobox('getValue');
            $("#dataFormMasterSalesEmployerID").combobox('setValue', "");
        });
       
        function dataFormMaster_OnLoadSuccess() {
            $("#dataFormMasterEmployerID").combobox('setWhere', '1=2');
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var sGroupID = '1071061'; //高專/外勞客服組
                $('#dataFormMasterIsEmpGroupID').checkbox('setValue', CheckApplyEmpIsGroupID(sGroupID));
                GetUserOrgNOs();
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterEmployeeID').combobox('setWhere', whereStr);
                $('#div1').css({ 'display': 'none' });
            }
            else {
                sCompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
                $("#dataFormMasterEmployerID").combobox('setWhere', "CompanyID =" + sCompanyID);
            }
            if (parameters != "SERVICE") {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['IsSettleAccount', 'SettleAccountDate', 'RequisitionDescr', 'RequisitionAmt',''];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                $("#dataFormMasterEmployerID").combobox('setWhere', "1=0");
            }

            var gf = $("#dataFormMasterShortTermTypeID").combobox('getValue');
            if (gf != '2') {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['EmployerID', 'CostNotes'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
            };
            if (parameters == "SERVICE" && mode == "2") {
                $('#dataFormMasterIsSettleAccount').removeAttr("disabled");
            }
            $("#dataFormMasterTargets").combobox('enable');
            //當會計審核且 NAVIGATOR_MODE='0' NORMAL
            if (parameters == "ACCOUNT" && mode == "0") {
               var c = $('#dataFormMasterReqEnd').val();
               if (c == "XZ")
                    alert('本暫借款單有請款單沖銷且金額相符,簽核後將自動結案!!');
            }
            //批示
            setTimeout(function () {
                var CNT = GetSignCount();
                var no = $("#dataFormMasterShortTermNO").val();
                var FiltStr = "FORM_PRESENTATION='ShortTermNO=" + "''" + no + "''" + "'";
                $("#JQDataGrid1").datagrid('setWhere', FiltStr);
                if (CNT == 0) {
                    $('#div1').hide();
                }
             }, 800);
            //$("#dataFormMasterShortTermTypeID").combobox().next('span').find('input').focus();
        }

        //取得有簽核內容簽核數
        function GetSignCount() {
            var ShortTermNO = $("#dataFormMasterShortTermNO").val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTerm.ShortTerm',
                data: "mode=method&method=" + "GetSignCount" + "&parameters=" + ShortTermNO,
                cache: false,
                async: false,
                success: function (data) {
                    cnt = $.parseJSON(data);
                }
            });
            return cnt;
        }

        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' />";
            else
                return "<input  type='checkbox' />";
        }
        function dataFormMasterOnApply() {
            var a = $('#dataFormMasterIsSettleAccount').checkbox('getValue');
            var c = $('#dataFormMasterReqEnd').val();
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (parameters == "SERVICE" && mode == "2" && c=="XX") {
                alert('注意!!本暫借款單有對應的請款單未結案,無法送出...')
                return false;
            }
            if (a == 0 && parameters == "SERVICE" && mode == "2") {
                alert('要確定結案時,請選取結案選取方塊')
                return false;
            }
            var dataFormMasterShortTermTypeID = $("#dataFormMasterShortTermTypeID").combobox('getValue');
            if (dataFormMasterShortTermTypeID == "" || dataFormMasterShortTermTypeID == undefined) {
                alert('注意!!,未選取暫借單類別,請選取');
                $("#dataFormMasterShortTermTypeID").focus();
                return false;
            }
            var dataFormMasterCompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            if (dataFormMasterCompanyID == "" || dataFormMasterCompanyID == undefined) {
                alert('注意!!,未選取公司別,請選取');
                $("#dataFormMasterCompanyID").focus();
                return false;
            }
            
            var title = $("#dataFormMasterShortTermTypeID").combobox('getText')
            var dataFormMasterEmployerID = $("#dataFormMasterEmployerID").combobox('getValue');
            if (dataFormMasterShortTermTypeID == "2" && (dataFormMasterEmployerID == "" || dataFormMasterEmployerID == undefined)) {
                alert('注意!!,已選取暫借類別為[' + title.trim()+ '],但未選取雇主,請選取');
                $("#dataFormMasterEmployerID").focus();
                return false;
            }
            var dataFormMasterPayTypeID = $("#dataFormMasterPayTypeID").combobox('getValue');
            if (dataFormMasterPayTypeID == "" || dataFormMasterPayTypeID == undefined) {
                alert('注意!!,未選取支付方式,請選取');
                $("#dataFormMasterPayTypeID").focus();
                return false;
            }
        };
        function OnSelectShortTermTypeID(rowData) {
            $("#dataFormMasterEmployerID").combobox('setWhere', "1=2");
            var ShortTermTypeID = rowData.ShortTermTypeID;
            if (ShortTermTypeID == 2) {
                var sCompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
                if (sCompanyID != "" &&  sCompanyID != undefined) 
                    $("#dataFormMasterEmployerID").combobox('setWhere', "CompanyID = " + sCompanyID);
                var FormName = '#dataFormMaster';
                var HideFieldName = ['EmployerID', 'CostNotes'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
            }
            else {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['EmployerID', 'CostNotes'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                //$("#dataFormMasterEmployerID").combobox('setWhere', "1=1");
                $("#dataFormMasterEmployerID").combobox('setValue', "");
            }
        }
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        function OnSelectCompanyID(rowData) {
            //alert(rowData.CompanyID);
            $("#dataFormMasterEmployerID").combobox('setWhere', "CompanyID = " + rowData.CompanyID);
        }
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "暫借款申請單";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                //連接的Server端，command
                //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTerm.ShortTerm',
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
        function CheckApplyEmpIsGroupID(GroupID) {
            var EmployeeID = $("#dataFormMasterEmployeeID").combobox('getValue');
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sRequisition.Requisition',
                data: "mode=method&method=" + "CheckApplyEmpIsGroupID" + "&parameters=" + EmployeeID + "," + GroupID,
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
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTerm.ShortTerm', //連接的Server端，command
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
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTerm.ShortTerm" runat="server" AutoApply="True"
                DataMember="ShortTerm" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermDescr" Editor="text" FieldName="ShortTermDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PlanPayDate" Editor="datebox" FieldName="PlanPayDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ShortTermAmount" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CheckDays" Editor="numberbox" FieldName="CheckDays" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CheckTitle" Editor="text" FieldName="CheckTitle" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequestDate" Editor="datebox" FieldName="RequestDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionDescr" Editor="text" FieldName="RequisitionDescr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="是否請款單結案" Editor="text" FieldName="IsEnd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
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
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="暫借款單" DialogLeft="10px" DialogTop="10px" Width="550px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTerm" HorizontalColumnsCount="2" RemoteName="sShortTerm.Employer" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" OnCancel="CloseDataForm" OnApply="dataFormMasterOnApply" IsAutoPause="False" OnLoadSuccess="dataFormMaster_OnLoadSuccess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單編號" Editor="text" FieldName="ShortTermNO" Format="" Width="140" ReadOnly="True" Visible="True" Span="1" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" Width="145" Visible="True" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sShortTerm.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:120" FieldName="EmployeeID" ReadOnly="False" Span="1" Width="144" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTerm.Company',tableName:'Company',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectCompanyID,panelHeight:200" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單類別" Editor="infocombobox" EditorOptions="valueField:'ShortTermTypeID',textField:'ShortTermTypeName',remoteName:'sShortTerm.ShortTermType',tableName:'ShortTermType',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectShortTermTypeID,panelHeight:200" FieldName="ShortTermTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sShortTerm.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTerm.Employer',tableName:'Employer',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployerID" MaxLength="6" ReadOnly="False" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本備註" Editor="text" FieldName="CostNotes" MaxLength="0" ReadOnly="False" Span="2" Visible="True" Width="383" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" MaxLength="0" Span="2" Visible="True" Width="350" />
                        <JQTools:JQFormColumn Alignment="left" Caption="事由說明" Editor="textarea" FieldName="ShortTermDescr" Format="" Width="350" EditorOptions="height:80" Span="2" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="140" Visible="True" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支付方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTerm.PayType',tableName:'PayType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Format="" Width="145" Visible="True" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'JQRefval',panelWidth:350,remoteName:'sShortTerm.Vendors',tableName:'Vendors',columns:[{field:'VendID',title:'廠商代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'員工姓名||廠商名稱',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'選取',cacheRelationText:true,checkData:true,showValueAndText:false,selectOnly:true" FieldName="PayTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="140" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" Width="143" Visible="True" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CheckTitle" Editor="text" FieldName="CheckTitle" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="支票票期" Editor="numberbox" FieldName="CheckDays" Format="" Visible="False" Width="140" ReadOnly="False" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Visible="True" Width="145" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsSettleAccount" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="結案日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true" FieldName="SettleAccountDate" Format="yyyy/mm/dd hh:MM:ss" ReadOnly="True" Visible="True" Width="145" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="對沖請款單" Editor="text" FieldName="RequisitionDescr" ReadOnly="True" Span="2" Visible="True" Width="350" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款單金額" Editor="text" FieldName="RequisitionAmt" ReadOnly="True" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="140" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="狀態" Editor="text" EditorOptions="" FieldName="ReqEnd" ReadOnly="False" Span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsEmpGroupID" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsEmpGroupID" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ShortTermDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ShortTermNO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RequestDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ShortTermAmount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="EmployeeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PlanPayDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsEmpGroupID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ShortTermTypeID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="ShortTermAmount" RangeFrom="1" RangeTo="3000000" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShortTermGist" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <br />
                <div id="div1">
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="Sys_ToDoHis" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sShortTerm.Sys_ToDoHis" RowNumbers="True" Title="簽核紀錄" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="流程" Editor="text" FieldName="S_STEP_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="簽核者" Editor="text" FieldName="USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="S_USERNAME" Editor="text" FieldName="S_USERNAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="簽核內容" Editor="text" FieldName="REMARK" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="簽核日期" Editor="datebox" FieldName="UPDATEDATE" Format="yyyy/mm/dd HH:MM:SS" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
                    </div>
            </JQTools:JQDialog>
        </div>
         <script type="text/javascript">
             $(":input").css("background", backcolor);
         </script>
    </form>
    <p>
&nbsp;</p>
</body>
</html>
