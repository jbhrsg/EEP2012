<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Contract.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<%--Contract是首約起單用--%>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        function OnLoad_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            //alert(param);
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //GetContractNO();
                //隱藏下載欄位、合約編號
                var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
                HideFields(HiddenFields);
                //下拉選單預設空白，為了打字方便
                $("#dataFormMasterKeeper").combobox('setValue', '');
                $("#dataFormMasterKeeper").combobox('disable');

                $("#dataFormMasterIsForeignDept").val(IsForeignDept());

                setTimeout(function () {
                    $("#dataFormMasterIsGuaranty").combobox('setValue', '否');
                    GuarantyHideShow();//顯現或隱藏履約保證相關欄位
                }, 500);
            } else if (getEditMode($("#dataFormMaster")) == 'viewed' && param == "") {//瀏覽
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                //ShowFields(['ContractNO']);
                HideFields(HiddenFields);
            } else if (getEditMode($("#dataFormMaster")) == 'viewed' && param == "K") {
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                HideFields(HiddenFields);
            } else if (getEditMode($("#dataFormMaster")) == 'updated' && param == 'apply') {//被退回param != "M" && param != "A"
                //隱藏下載欄位
                var HiddenFields = ['Attachment1d', 'Attachment2d', 'Attachment3d', 'Attachment4d', 'Attachment5d'];
                HideFields(HiddenFields);
                //根據保管部門，保管人延遲篩選
                KeeperFilter();
                $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'red' });
            } else if (param == "A") {//會簽會計室
                //除了保管人，其他欄位皆停用
                var DisabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
                var DisabledComboboxName = ['ContractClass', 'BeginDate', 'EndDate', 'ResponsibleDepart', 'IsGuaranty', 'GuarantyEndDate','SignDate'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                $("#dataFormMasterContractB").refval('disable');
                //隱藏上傳欄位
                HideFields(['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5']);
                //根據保管部門，保管人延遲篩選
                KeeperFilter();
                $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'red' });
            }

            if (param == "M" && $('#dataFormMasterContractKey').val() != "") {//主管審核
                //隱藏上傳欄位
                var HiddenFields = ['Attachment1', 'Attachment2', 'Attachment3', 'Attachment4', 'Attachment5'];
                HideFields(HiddenFields);
                //停用全部欄位，為了給合約編號ContractNO設值
                var DisabledFieldName = ['PhysicalContractNO', 'ContractName', 'Amount', 'Remarks', 'Remarks2', 'RemindDays', 'GuarantyAmount', 'GuarantyNO'];
                var DisabledComboboxName = ['ResponsibleDepart','ContractClass', 'BeginDate', 'EndDate', 'Keeper','IsGuaranty','GuarantyEndDate','SignDate'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                $("#dataFormMasterContractB").refval('disable');

                
            } 

            //總經理申請時就取號ContractNO
            //if ($("#dataFormMasterContractNO").val() == "" && getEditMode($("#dataFormMaster")) == 'inserted' && getClientInfo("userid") == '003') {//因總經理申請，後面審核會跳過，故無法取號
            //    $("#dataFormMasterContractNO").closest('td').prev('td').show();
            //    $("#dataFormMasterContractNO").closest('td').show();
            //    var ContractKey = $('#dataFormMasterContractKey').val();
            //    $.ajax({
            //        type: "POST",
            //        url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract', //連接的Server端，command
            //        data: "mode=method&method=" + "GetContractNO" + "&parameters=" + ContractKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
            //        cache: false,
            //        async: false,
            //        success: function (data) {
            //            if (data != "False") {
            //                $('#dataFormMasterContractNO').val(data);
            //            } else {
            //                alert("取號失敗");
            //            }
            //        }
            //    });
            //}

            //新申請以外時，履約保證相關欄位的顯現或隱藏
            if ($("#dataFormMasterIsGuaranty").combobox('getValue') != '' && getEditMode($("#dataFormMaster")) != 'inserted') {
                setTimeout(function () {
                    GuarantyHideShow();
                }, 500);
            }

        }
        
        //function GetContractNO(){
        //    //取號ContractNO
        //    if ($("#dataFormMasterContractNO").val() == "") {
        //        //var ContractKey = $('#dataFormMasterContractKey').val();
        //        $.ajax({
        //            type: "POST",
        //            url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract', //連接的Server端，command
        //            data: "mode=method&method=" + "GetContractNO",// + "&parameters=" + ContractKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
        //            cache: false,
        //            async: false,
        //            success: function (data) {
        //                if (data != "False") {
        //                    $('#dataFormMasterContractNO').val(data);
        //                } else {
        //                    alert("取號失敗");
        //                }
        //            }
        //        });
        //    }
        //}

        function OnApply_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別
            var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
            var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
            var KeepDepart = arr[0];
            var ORG_MAN = arr[1];

            if (getEditMode($("#dataFormMaster")) == 'inserted') {//新申請時
                //無特定保管部門
                if ((KeepDepart == null || KeepDepart == '') && ($("#dataFormMasterKeeper").combobox('getValue') == '' || $("#dataFormMasterKeeper").combobox('getValue') == '---請選擇---')) {
                    alert('保管人必填');
                    return false;
                }
            }else if(param=="A"){//會簽會計
                if (($("#dataFormMasterKeeper").combobox('getValue') == '' || $("#dataFormMasterKeeper").combobox('getValue') == '---請選擇---')) {
                    alert('保管人必填');
                    return false;
                }
            }
        }

        //合約類別
        function OnSelectContractClass() {
            //保管部門設值及保管人立即篩選或鎖定
            KeeperDepart_SetValue();
        }
        //權責部門
        function OnSelectResponsibleDepart() {
            //保管部門設值及保管人立即篩選或鎖定
            KeeperDepart_SetValue();
        }
        //履約保證
        function OnSelectIsGuaranty() {
            //履約保證相關欄位顯現或隱藏
            GuarantyHideShow();
        }
        
        //工具---------------------------------------------------------------------------
        function IsForeignDept() {
            var userid = getClientInfo("userid");
            var counts=0;
            if (userid != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContract.ERPContract',
                    data: "mode=method&method=IsForeignDept&parameters=" + userid,
                    cache: false,
                    async: false,
                    success: function (data) {
                        counts = $.parseJSON(data);
                    }
                });
            }
            return counts;
        }

        function HideFields(FieldNames) {
            var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();
                $(FormName + value).closest('td').hide();
            });
        }

        function ShowFields(FieldNames) {
            var FormName = '#dataFormMaster';
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }

        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }

        //保管人Combobox延時篩選(onloaddataform用)
        function KeeperFilter(){ 
            $("#dataFormMasterKeeper").combobox('disable');
            setTimeout(function () {
                var departText = $("#dataFormMasterKeepDepart").combobox('getValue');
                var where = "org_no='" + departText + "'";
                $("#dataFormMasterKeeper").combobox('setWhere', where);
                $("#dataFormMasterKeeper").combobox('enable');
            }, 1000);
        }

        //保管人Combobox立即篩選
        function KeeperFilter0() {
            $("#dataFormMasterKeeper").combobox('disable');
            var departText = $("#dataFormMasterKeepDepart").combobox('getValue');//保管部門ID
            var where = "org_no='" + departText + "'";
            $("#dataFormMasterKeeper").combobox('setWhere', where);
            $("#dataFormMasterKeeper").combobox('enable');
        }

        //保管部門設值及保管人立即篩選或鎖定
        function KeeperDepart_SetValue() {
            var ContractClass = $("#dataFormMasterContractClass").combobox('getValue');//合約類別ID
            var ResponsibleDepart = $("#dataFormMasterResponsibleDepart").combobox('getValue');//權責部門ID
            //兩個都有填才進行保管部門設值
            if ((ContractClass != '' && ContractClass != '---請選擇---') && (ResponsibleDepart != '---請選擇---' && ResponsibleDepart != '')) {
                //var rowData = $("#dataFormMasterContractClass").combobox('getSelectItem');//合約類別的保管部門
                var ContractClassID = $("#dataFormMasterContractClass").combobox('getValue');
                var arr = GetInfoCommandValue($("#dataFormMasterContractClass"), "ContractClassID='" + ContractClassID + "'");
                var KeepDepart = arr[0];
                var ORG_MAN = arr[1];

                //if (rowData.KeepDepart == null || rowData.KeepDepart == '') {//合約類別無特定保管部門
                if (KeepDepart == null || KeepDepart == '') {//合約類別無特定保管部門
                    //保管部門設值
                    //$("#dataFormMasterKeepDepart").val(ResponsibleDepart);
                    $("#dataFormMasterKeepDepart").combobox('setValue', ResponsibleDepart);
                    //保管人立即篩選
                    KeeperFilter0();
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'red' });
                } else {//合約類別有特定保管部門
                    $("#dataFormMasterKeepDepart").combobox('setValue', KeepDepart);
                    //保管人鎖定
                    $("#dataFormMasterKeeper").combobox('setValue', '');
                    $("#dataFormMasterKeeper").combobox('disable');
                    $("#dataFormMasterKeeper").closest('td').prev('td').css({ 'color': 'black' });
                }

                //特定保管部門主管角色
                $("#dataFormMasterAssignChecker").val(ORG_MAN);
            }
        }
        //履約保證相關欄位顯現或隱藏
        function GuarantyHideShow() {
            if ($("#dataFormMasterIsGuaranty").combobox('getValue') == '是') {
                ShowFields(['GuarantyNO','GuarantyAmount', 'GuarantyEndDate']);
            } else {
                $("#dataFormMasterGuarantyNO").val('');
                $("#dataFormMasterGuarantyAmount").val('');
                $("#dataFormMasterGuarantyEndDate").datebox('setValue', '');
                HideFields(['GuarantyNO','GuarantyAmount', 'GuarantyEndDate']);
            }
        }
        //呼叫dll指定的infoCommand
        function GetInfoCommandValue(controller, where) {
            var remoteName = getInfolightOption(controller).remoteName;
            var tableName = getInfolightOption(controller).tableName;
            // var valueField = getInfolightOption(infoRefval).valueField;
            var returnValue = [];
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: getRemoteUrl(remoteName, tableName, false) + "&whereString=" + encodeURIComponent(where),
                data: { rows: 1 },
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        var valueArr=[]
                        valueArr[0] = data[0]["KeepDepart"];
                        valueArr[1] = data[0]["ORG_MAN"];
                        returnValue = valueArr;
                    }
                },
                error: function (data) { }
            });
            return returnValue;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPContract.ERPContract" runat="server" AutoApply="True"
                DataMember="ERPContract" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="合約首約登錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="ContractKey" Editor="numberbox" FieldName="ContractKey" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractNO" Editor="text" FieldName="ContractNO" Format="" MaxLength="0" Visible="True" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractName" Editor="text" FieldName="ContractName" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractB" Editor="text" FieldName="ContractB" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContractClass" Editor="text" FieldName="ContractClass" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="BeginDate" Editor="datebox" FieldName="BeginDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EndDate" Editor="datebox" FieldName="EndDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Remarks" Editor="text" FieldName="Remarks" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Keeper" Editor="text" FieldName="Keeper" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment1" Editor="text" FieldName="Attachment1" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment2" Editor="text" FieldName="Attachment2" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment3" Editor="text" FieldName="Attachment3" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment4" Editor="text" FieldName="Attachment4" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment5" Editor="text" FieldName="Attachment5" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="FlowFlag" Format="" MaxLength="0" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="True"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="合約首約登錄" DialogTop="50px" Width="600px" Closed="false" Icon="" ShowModal="True" ShowSubmitDiv="True">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPContract" HorizontalColumnsCount="2" RemoteName="sERPContract.ERPContract" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoad_dataFormMaster" OnApply="OnApply_dataFormMaster" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="ContractKey" Editor="numberbox" FieldName="ContractKey" Format="" Width="180" ReadOnly="True" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ParentKey" Editor="numberbox" FieldName="ParentKey" Format="" Width="180" ReadOnly="True" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約編號" Editor="text" FieldName="ContractNO" Format="" Width="180" ReadOnly="True" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="紙本合約編號" Editor="text" FieldName="PhysicalContractNO" Width="180" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約名稱" Editor="text" FieldName="ContractName" Format="" Width="180" maxlength="0" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶/廠商" Editor="inforefval" FieldName="ContractB" Format="" Width="184" maxlength="0" ReadOnly="False" EditorOptions="title:'客戶/廠商',panelWidth:600,remoteName:'sERPContract.VenderCustomer',tableName:'VenderCustomer',columns:[{field:'ID',title:'編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Name',title:'名稱',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Tel',title:'電話',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Addr',title:'地址',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EntityType',title:'類型',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'ID',textField:'Name',valueFieldCaption:'名稱',textFieldCaption:'名稱',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="合約類別" Editor="infocombobox" FieldName="ContractClass" Format="" Width="184" EditorOptions="valueField:'ContractClassID',textField:'ContractClass',remoteName:'sERPContract.ContractClass',tableName:'ContractClass',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectContractClass,panelHeight:200" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="權責部門" Editor="infocombobox" EditorOptions="valueField:'CENTER_CNAME',textField:'CENTER_ENAME',remoteName:'sERPContractGroup.ERPContractGroup',tableName:'ERPContractGroup',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectResponsibleDepart,panelHeight:200" FieldName="ResponsibleDepart" ReadOnly="False" Visible="True" Width="184" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管部門" Editor="infocombobox" FieldName="KeepDepart" ReadOnly="True" Visible="True" Width="180" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPContract.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管人" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPContract.GroupUsers',tableName:'GroupUsers',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Keeper" Format="" ReadOnly="False" Width="184" Visible="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="起日" Editor="datebox" FieldName="BeginDate" Format="" Width="184" maxlength="0" NewRow="False" RowSpan="1" Span="1" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="迄日" Editor="datebox" FieldName="EndDate" Format="" Width="184" ReadOnly="False" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽約日期" Editor="datebox" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" FieldName="SignDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="184" />
                        <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="Amount" ReadOnly="False" Visible="True" Width="180" maxlength="0" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="到期前幾天提醒" Editor="numberbox" FieldName="RemindDays" maxlength="0" Width="180" NewRow="False" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證" Editor="infocombobox" FieldName="IsGuaranty" ReadOnly="False" Width="184" Span="1" EditorOptions="items:[{value:'是',text:'是',selected:'false'},{value:'否',text:'否',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelectIsGuaranty,panelHeight:50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證票號" Editor="text" FieldName="GuarantyNO" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證金額" Editor="numberbox" FieldName="GuarantyAmount" Width="180" Span="1" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="履約保證到期日" Editor="datebox" FieldName="GuarantyEndDate" ReadOnly="False" Width="184" maxlength="0" Visible="True" EditorOptions="dateFormat:'datetime',showTimeSpinner:false,showSeconds:true,editable:false" />
                        <JQTools:JQFormColumn Alignment="left" Caption="流程狀態" Editor="text" FieldName="FlowFlag" Format="" maxlength="0" Width="180" ReadOnly="True" Visible="False" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="注意事項" Editor="textarea" FieldName="Remarks" Format="" Width="453" EditorOptions="height:60" ReadOnly="False" MaxLength="0" Visible="True" NewRow="False" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Remarks2" Width="453" EditorOptions="height:60" ReadOnly="False" MaxLength="0" Visible="True" NewRow="False" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="infofileupload" FieldName="Attachment1" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment1',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="False" maxlength="0" NewRow="True" RowSpan="1" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="infofileupload" FieldName="Attachment2" Format="" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment2',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" ReadOnly="False" maxlength="0" Visible="True" NewRow="True" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="infofileupload" FieldName="Attachment3" Format="" ReadOnly="False" Width="430" maxlength="0" NewRow="True" RowSpan="1" Span="2" Visible="True" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment3',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件4" Editor="infofileupload" FieldName="Attachment4" Format="" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment4',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件5" Editor="infofileupload" FieldName="Attachment5" Format="" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Contract/Attachment5',showButton:true,showLocalFile:false,fileSizeLimited:'10000'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件1" Editor="text" FieldName="Attachment1d" Format="download,folder:JB_ADMIN/Contract/Attachment1" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件2" Editor="text" FieldName="Attachment2d" Format="download,folder:JB_ADMIN/Contract/Attachment2" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件3" Editor="text" FieldName="Attachment3d" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" Format="download,folder:JB_ADMIN/Contract/Attachment3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件4" Editor="text" FieldName="Attachment4d" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" Format="download,folder:JB_ADMIN/Contract/Attachment4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附件5" Editor="text" FieldName="Attachment5d" maxlength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="430" Format="download,folder:JB_ADMIN/Contract/Attachment5" />
                        <JQTools:JQFormColumn Alignment="left" Caption="特定保管部門主管" Editor="text" FieldName="AssignChecker" maxlength="0" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="是外勞部" Editor="text" FieldName="IsForeignDept" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ParentKey" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ContractKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RemindDays" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ContractNO" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractName" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractB" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContractClass" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BeginDate" RemoteMethod="False" ValidateType="None" CheckMethod="" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EndDate" RemoteMethod="False" ValidateType="None" CheckMethod="" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ResponsibleDepart" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="IsGuaranty" RemoteMethod="False" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RemindDays" RemoteMethod="False" ValidateMessage="" ValidateType="None" RangeFrom="0" RangeTo="999" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
