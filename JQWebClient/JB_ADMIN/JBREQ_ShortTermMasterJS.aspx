<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_ShortTermMasterJS.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        //'NR':正常
        var FlowStatus = 'NR';
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
            var Btn = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
            var EmployerID = $("#dataFormDetailEmployerID").combobox('getValue');
            if ((EmployerID == "" || EmployerID == undefined)) {
                alert('注意!!請先選取雇主');
                return false;
            }
            openForm('#JQDialog3', {}, "", 'dialog');
            }).linkbutton({ text: '選取外勞' });
            $('#dataFormDetailDormID').closest("td").append(Btn);
            //
            var Btn = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var EmployerID = $("#dataFormDetailEmployerID").combobox('getValue');
                if ((EmployerID == "" || EmployerID == undefined)) {
                    alert('注意!!請先選取雇主');
                    return false;
                }
                var DormID = $("#dataFormDetailDormID").combobox('getValue');
                if ((DormID == "" || DormID == undefined)) {
                    alert('注意!!請先選取宿舍');
                    return false;
                }
                var FeeID = $("#dataFormDetailFeeID").combobox('getValue');
                if ((FeeID == "" || FeeID == undefined)) {
                    alert('注意!!請先選費用項目');
                    return false;
                }
                var FeeSDate = $("#dataFormDetailFeeSDate").datebox('getValue');
                if ((FeeSDate == "" || FeeSDate == undefined)) {
                    alert('注意!!請先填入起始日期');
                    return false;
                }
                var whereStr = "Dorm = '" + DormID + "' and EmployerID = '" + EmployerID + "' and FeeID = '" + FeeID + "' and outdate >= '" + FeeSDate + "'"
                $("#JQDataGrid3").datagrid('setWhere', whereStr);
                openForm('#JQDialog4', {}, "", 'dialog');
            }).linkbutton({ text: '檢視費用' });
            $('#dataFormDetailRequisitionAmount').closest("td").append(Btn);
        });
        function dataFormMaster_OnLoadSuccess() {
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                GetUserOrgNOs();
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterEmployeeID').combobox('setWhere', whereStr);
            }
            else {
                //當外勞會計立帳時,執行暫借款立帳
                if (parameters == 'SETBILL') {
                    $('#dataFormMasterShortTermNO').after($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                       var YearMonth = $("#dataFormMasterYearMonth").combobox('getValue');
                       var IsSetUpFee = CheckSetUpFeeStatusMonth(YearMonth);
                       if (IsSetUpFee == true) {
                           alert('注意!!' + YearMonth + '已結帳,無法立帳');
                           return false;
                       }
                       var ShortTermNO = $("#dataFormMasterShortTermNO").val();
                       if (CheckSetUpFeeStatusBill(ShortTermNO) == false) {
                           alert('注意!!本暫借單已立帳');
                           return false;
                       }
                       var IsAllSucess = 0;
                       var rows = $("#dataGridDetail").datagrid('getRows');
                       var cnt = rows.length;
                       for (var i = 0 ; i < cnt ; i++) {
                           var FeeID = rows[i].FeeID;
                           var FeeSDate = rows[i].FeeSDate;
                           var FeeEDate = rows[i].FeeEDate;
                           var ShortTermAmount = rows[i].ShortTermAmount;
                           var ShortTermNO = rows[i].ShortTermNO + rows[i].ItemNO;
                           var DormID = rows[i].DormID;
                           var UserID = getClientInfo("UserName")
                           $.ajax({
                               type: "POST",
                               url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                               data: "mode=method&method=" + "procInsertFeeSetUpMbyEEP" + " &parameters=" + FeeID + "," + YearMonth + "," + FeeSDate + "," + FeeEDate + ',' + ShortTermAmount + ',' + ShortTermNO + ',' + DormID + ',' + UserID,
                               cache: false,
                               async: false,
                               success: function (data) {
                                   if (data == "True") {
                                       IsAllSucess = 1;
                                   }
                                   else {
                                       IsAllSucess = 0;
                                   }
                               }
                           });
                       }
                       if (IsAllSucess == 1) {
                           alert('立帳成功');
                       }
                       else {
                           alert('注意!!,立帳失敗');
                       }
                       $("#dataFormMasterSetUpStatus").checkbox('setValue', IsAllSucess);
                    }).linkbutton({ text: '暫借款立帳' }));
                    var dfEmployeeID = $('#dataFormMasterEmployeeID').closest('td');
                    dfEmployeeID.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                        var ShortTermNO = $("#dataFormMasterShortTermNO").val();
                        var FiltStr = "LEFT(ShortTermNO,8) = '" + ShortTermNO + "'";
                        $("#JQDataGridSetUpList").datagrid('setWhere', FiltStr);
                        $("#JQDataGridFeeSetUpPrePay").datagrid('setWhere', FiltStr);
                        openForm('#JQDialogSetUpList', {}, "", 'dialog');
                        return true;
                    }).linkbutton({ text: '立帳查詢' }));
                }
            }
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
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
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "暫借款申請單多筆";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
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
        function EmployerIDOnSelect(rowdata) {
            $("#dataFormDetailDormID").combobox('setValue', "");
            $("#dataFormDetailDormID").combobox('setWhere', "l.EmployerID=" + "'"+ rowdata.EmployerID+"'");
            $("#dataFormDetailOrg_NOPar").val(rowdata.OrgNOParent);
            $("#dataFormDetailEmployerName").val(rowdata.EmployerName);
            var whereStr = "";
            var EmployerID = $("#dataFormDetailEmployerID").combobox("getValue");
            var whereStr = " l.EmployerID = " + "'" + EmployerID + "'";
            $("#JQDataGrid2").datagrid('setWhere', whereStr);
        }
        function DormOnSelect(rowdata) {
            var EmployerName = $("#dataFormDetailEmployerName").val();
            var dataGridTitle = EmployerName+'/'+rowdata.DormName + '-外勞名單';
            $("#JQDataGrid2").datagrid('getPanel').panel('setTitle', dataGridTitle);
            $("#JQDataGrid2").datagrid('options').title = dataGridTitle;
            var whereStr = "";
            var EmployerID = $("#dataFormDetailEmployerID").combobox("getValue");
            var whereStr = " l.EmployerID = " + "'" + EmployerID + "'" + " AND Dorm = " + "'" + rowdata.DORMID + "'";
            $("#JQDataGrid2").datagrid('setWhere', whereStr);
        }
        function JQDialog3OnSubmited() {
            var rows = $('#JQDataGrid2').datagrid("getChecked");
            var count = rows.length;
            if (count == 0) {
                alert('注意!!未選取任何外勞,請選取');
                return false;
            }
            var EmployeeIDs = '';
            var EmployeeNames = '';
            var EmployerName = $('#dataFormDetailEmployerID').combobox('getText');
            for (var i = 0; i <= rows.length - 1; i++) {
                if (i > 0) {
                    EmployeeIDs = EmployeeIDs + ',' + rows[i].EmployeeID;
                    EmployeeNames = EmployeeNames + ',' + rows[i].EmployeeTcName;
                }
                else{
                    EmployeeIDs = EmployeeIDs + rows[0].EmployeeID;
                    EmployeeNames = EmployeeNames + rows[0].EmployeeTcName;
                    }
            }
            $("#dataFormDetailShortTermDescr").val($("#dataFormDetailShortTermDescr").val() + ' ' + EmployerName+'-'+EmployeeNames);
            $("#dataFormDetailEmployeeIDs").val(EmployeeIDs);
            return true;
        }
        function dataFormMasterOnApply() {
            var parameters = Request.getQueryStringByName("P1");
            var dataFormMasterCompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            if (dataFormMasterCompanyID == "" || dataFormMasterCompanyID == undefined) {
                alert('注意!!,未選取公司別,請選取');
                $("#dataFormMasterCompanyID").focus();
                return false;
            }
            if (dataFormMasterYearMonth == "" || dataFormMasterYearMonth == undefined) {
                alert('注意!!,未選立帳年月,請選取');
                $("#dataFormMasterYearMonth").focus();
                return false;
            }
            var TotalAmount = $("#dataFormMasterTotalAmount").val();
            if (TotalAmount <= 0) {
                alert('注意!!,未輸入明細,無法存檔送出');
                return false;
            }
            if (parameters == 'SETBILL') {
                var ShortTermNO = $("#dataFormMasterShortTermNO").val();
                if (CheckSetUpFeeStatusBill(ShortTermNO) == true) {
                    alert('注意!!未執行暫借款立帳,無法存檔送出');
                    return false;
                }
            }
        }
        function CheckEmployeeIDsIsNull() {
            var _return = false;
            var rows = $("#dataGridDetail").datagrid('getRows');
            var cnt = rows.length;
            for (var i = 0 ; i < cnt ; i++) {
                if (rows[i].EmployeeIDs == '' || rows[i].EmployeeIDs == undefined) {
                    
                    _return = true;
                    break;
                }
                return _return;
            }
            
        }
        function JQDialog4OnSubmited() {
            var rows = $('#JQDataGrid3').datagrid('getRows');
            var count = rows.length;
            if (count == 0) {
                alert('注意!!無任何資料');
                return false;
            }
            var tot = 0;
            for (var i = 0 ; i <= count - 1; i++) {
                tot = tot + rows[i].FeeAmount;
            }
            $("#dataFormDetailRequisitionAmount").val(tot);
            var OAmt = $("#dataFormDetailOriginalAmount").val();
            if (OAmt > 0) {
                $("#dataFormDetailShortTermAmount").val(OAmt - tot)
            }
            return true;
        }
        function dataFormDetailOnApply() {
            var parameters = Request.getQueryStringByName("P1");
            var dataFormDetailFeeID = $("#dataFormDetailFeeID").combobox('getValue');
            if (dataFormDetailFeeID == "" || dataFormDetailFeeID == undefined) {
                alert('注意!!,未選取費用項目,請選取');
                $("#dataFormDetailFeeID").focus();
                return false;
            }
            var dataFormDetailPayTypeID = $("#dataFormDetailPayTypeID").combobox('getValue');
            if (dataFormDetailPayTypeID == "" || dataFormDetailPayTypeID == undefined) {
                alert('注意!!,未選取支付方式,請選取');
                $("#dataFormDetailPayTypeID").focus();
                return false;
            }
            var dataFormDetailEmployerID = $("#dataFormDetailEmployerID").combobox('getValue');
            if (dataFormDetailEmployerID == "" || dataFormDetailEmployerID == undefined) {
                alert('注意!!,未選取雇主,請選取');
                $("#dataFormDetailEmployerID").focus();
                return false;
            }
            if ($("#dataFormDetailPayTypeID").combobox('getValue') == '') {
                alert('注意!!,未選取支付方式,請選取');
                return false;
            }
            if ($("#dataFormDetailEmployeeIDs").val() == '' || $("#dataFormDetailEmployeeIDs").val() == 'undefined') {
                alert('注意!!,未選取外勞,請選取');
                return false;
            }
            if ($("#dataFormDetailShortTermAmount").val() == 0) {
                alert('注意!!,請款金額不可為0');
                return false;
            }
        }
        function SumShortTermAmount(rowData) {
            $("#dataFormMasterTotalAmount").val(rowData.ShortTermAmount);
        }
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        function JQDataGrid2OnLoadSucess() {
        
        }
        function OnSelectCompanyID(rowData) {
            $("#dataFormDetailEmployerID").combobox('setWhere', "CompanyID = " + rowData.CompanyID);
        }
        function GetSYS_TODOLISTStatus() {
            var BILLNO = $('#dataFormMasterShortTermNO').val();
            var UserID = getClientInfo("UserID");
            var ReturnStr = '';
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                data: "mode=method&method=" + "GetSYS_TODOLISTStatus" + "&parameters=" + BILLNO + "," + UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        ReturnStr = rows[0].FlowStatus;
                    }
                 }
            }
            );
            return ReturnStr;
        }
        function DataGridDetailOnDelete(rowData) {
            var IsSetUpFeeStatus = CheckSetUpFeeStatus(rowData.ShortTermNO,rowData.ItemNO);
            if (! IsSetUpFeeStatus) {
                alert('注意!!此筆暫借單已立帳,無法刪除');
                return false;
            }
            return true;
        }
        function DataGridDetailOnInsert(rowData) {
            var YearMonth = $("#dataFormMasterYearMonth").combobox('getValue');
            if (YearMonth == "" || YearMonth == undefined) {
                alert('注意!!未選取立帳年月,請先選取');
                return false;
            }
            return true;
        }
        function dataFormDetailOnLoadSucess() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var YearMonth = $("#dataFormMasterYearMonth").combobox('getValue');
                var yy = YearMonth.substr(0, 4);
                var mm = YearMonth.substr(4, 2);
                var LDay = getLastDay(yy, mm);
                var FirstDay = yy + "/" + mm + "/" + "01";
                var LastDay = yy + "/" + mm + "/" + LDay;
                $("#dataFormDetailFeeSDate").datebox('setValue', FirstDay);
                $("#dataFormDetailFeeEDate").datebox('setValue', LastDay);
                var RQDate = $("#dataFormMasterRequestDate").datebox('getValue');
                $("#dataFormDetailRequestDate").datebox('setValue', RQDate);
            }
            return true;
        }
        function getLastDay(year, month) {
            var new_year = year;        //取前的年份         
            var new_month = month++;    //取下一個月的第一天，方便計算         
            if (month > 12) {
                new_month -= 12;        //月份减         
                new_year++;             //年份增         
            }
            var new_date = new Date(new_year, new_month, 1);                //取當年當月中的第一天         
            return (new Date(new_date.getTime() - 1000 * 60 * 60 * 24)).getDate();//取得當月最後一天日期         
        }
        //檢查費用年月是否已立帳 
        function CheckSetUpFeeStatusMonth(sYearMonth) {
            var cnt = 0;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                data: "mode=method&method=" + "CheckSetUpFeeStatusMonth" + "&parameters=" + sYearMonth,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        cnt = $.parseJSON(data);
                    }
                }
            });
            if ((cnt == "0") || (cnt == "undefined")) {

                return false;
            }
            else {
                return true;
            }
        }
        //檢查暫借單號明細是否已立帳
        function CheckSetUpFeeStatus(ShortTermNO, ItemNO) {
            sShortTermNO = ShortTermNO + ItemNO;
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                data: "mode=method&method=" + "CheckSetUpFeeStatus" + "&parameters=" + sShortTermNO,
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
                return false;
            }
        }
        //檢查暫借單號是否已立帳
        function CheckSetUpFeeStatusBill(ShortTermNO) {
            sShortTermNO = ShortTermNO;
            var cnt;
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sShortTermMasterApplyJS.ShortTermMaster',
                data: "mode=method&method=" + "CheckSetUpFeeStatusBill" + "&parameters=" + sShortTermNO,
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
                return false;
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sShortTermMasterApplyJS.ShortTermMaster" runat="server" AutoApply="True"
                DataMember="ShortTermMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermDate" Editor="datebox" FieldName="ShortTermDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="EmployeeID" Editor="text" FieldName="EmployeeID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ShortTermTypeID" Editor="numberbox" FieldName="ShortTermTypeID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ShortTermGist" Editor="text" FieldName="ShortTermGist" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ApplyType" Editor="numberbox" FieldName="ApplyType" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyType" Editor="text" FieldName="ApplyType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title=" 多筆暫借款申請-傑信" DialogLeft="10px" DialogTop="10px" Width="880px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ShortTermMaster" HorizontalColumnsCount="2" RemoteName="sShortTermMasterApplyJS.ShortTermMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess=" dataFormMaster_OnLoadSuccess" OnCancel="CloseDataForm" OnApply="dataFormMasterOnApply" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借單編號" Editor="text" FieldName="ShortTermNO" Format="" maxlength="0" Width="140" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ShortTermDate" Format="yyyy/mm/dd" Width="145" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="EmployeeID" Format="" Width="144" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sShortTermMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" Width="144" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sShortTermMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" Width="144" Visible="False" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借款類別" Editor="infocombobox" FieldName="ShortTermTypeID" Format="" Width="144" EditorOptions="valueField:'ShortTermTypeID',textField:'ShortTermTypeName',remoteName:'sShortTermMaster.ShortTermType',tableName:'ShortTermType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" FieldName="CompanyID" Format="" Width="144" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sShortTermMaster.Company',tableName:'Company',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectCompanyID,panelHeight:200" ReadOnly="False" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="年月" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sShortTermMasterApplyJS.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="YearMonth" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="144" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" MaxLength="0" ReadOnly="False" Width="145" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" Format="" Width="480" ReadOnly="False" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="right" Caption="暫借總金額" Editor="text" FieldName="TotalAmount" ReadOnly="True" Width="140" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyType" Editor="text" FieldName="ApplyType" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowStatus" Editor="text" FieldName="FlowStatus" maxlength="0" ReadOnly="False" Width="80" Visible="False" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已立帳" Editor="checkbox" FieldName="SetUpStatus" MaxLength="0" ReadOnly="False" Visible="False" Width="80" EditorOptions="on:1,off:0" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ShortTermDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sShortTermMasterApplyJS.ShortTermMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="True" OnDelete="DataGridDetailOnDelete" OnInsert="DataGridDetailOnInsert" >
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="事由說明" Editor="text" FieldName="ShortTermDescr" Width="150" />
                        <JQTools:JQGridColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="yyyy/mm/dd" FormatScript="" Width="66" />
                        <JQTools:JQGridColumn Alignment="right" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="N" Width="60" Total="sum" OnTotal="SumShortTermAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="支付方式" Editor="infocombobox" FieldName="PayTypeID" Format="" Width="60" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTermMaster.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="yyyy/mm/dd" Width="66" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="受款人" Editor="infocombobox" FieldName="PayTo" Format="" Width="70" EditorOptions="valueField:'VendID',textField:'VendShortName',remoteName:'sShortTermMasterApplyJS.Vendors',tableName:'Vendors',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="EmployerName" Width="70" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTermMaster.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EmployerID" Format="" Visible="False" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="成本備註" Editor="text" FieldName="CostNotes" Format="" Width="200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EmployeeIDs" Editor="text" FieldName="EmployeeIDs" Visible="False" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="FeeID" Editor="text" FieldName="FeeID" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="DormID" Editor="text" FieldName="DormID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="FeeSDate" Editor="text" FieldName="FeeSDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="FeeEDate" Editor="text" FieldName="FeeEDate" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ShortTermNO" ParentFieldName="ShortTermNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="15px" DialogTop="40px" Width="620px" Title="暫借款明細資料">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ShortTermDetails" HorizontalColumnsCount="2" RemoteName="sShortTermMasterApplyJS.ShortTermDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormDetailOnApply" OnLoadSuccess="dataFormDetailOnLoadSucess" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" ReadOnly="True" Span="2" Width="20" />
                            <JQTools:JQFormColumn Alignment="left" Caption="事由說明" Editor="textarea" EditorOptions="height:60" FieldName="ShortTermDescr" Span="2" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" FieldName="EmployerID" Format="" Span="1" Width="120" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTermMasterApplyJS.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:EmployerIDOnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="宿舍" Editor="infocombobox" EditorOptions="valueField:'DORMID',textField:'DormName',remoteName:'sShortTermMasterApplyJS.Dorm',tableName:'Dorm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DormOnSelect,panelHeight:200" FieldName="DormID" ReadOnly="False" Span="1" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="費用項目" Editor="infocombobox" EditorOptions="valueField:'FeeID',textField:'FeeName',remoteName:'sShortTermMasterApplyJS.FeeItem',tableName:'FeeItem',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="FeeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始日期" Editor="datebox" FieldName="FeeSDate" Span="1" Visible="True" Width="118" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption="終止日期" Editor="datebox" FieldName="FeeEDate" Span="1" Visible="True" Width="118" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption="成本備註" Editor="text" FieldName="CostNotes" Format="" Width="315" Span="2" Visible="True" />
                            <JQTools:JQFormColumn Alignment="right" Caption="原始金額" Editor="numberbox" FieldName="OriginalAmount" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" Format="" />
                            <JQTools:JQFormColumn Alignment="right" Caption="請款金額" Editor="numberbox" FieldName="RequisitionAmount" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="113" Format="" />
                            <JQTools:JQFormColumn Alignment="right" Caption="暫借金額" Editor="numberbox" FieldName="ShortTermAmount" Format="" Width="120" Span="1" Visible="True" ReadOnly="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="支付方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTermMaster.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" Format="" Span="1" Width="120" Visible="True" ReadOnly="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" FieldName="PayTo" Format="" Width="120" Span="2" Visible="True" EditorOptions="title:'受款人搜尋',panelWidth:450,remoteName:'sShortTermMasterApply.Vendors',tableName:'Vendors',columns:[{field:'VendID',title:'廠商代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'廠商簡稱',width:240,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Employee_ID',title:'員工代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'受款人名稱',textFieldCaption:'受款人代號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="需求日期" Editor="datebox" FieldName="RequestDate" Format="" Width="121" Span="1" Visible="True" ReadOnly="True" RowSpan="1" MaxLength="0" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="預計歸還日" Editor="datebox" FieldName="PlanPayDate" Format="" Width="121" Span="1" Visible="True" ReadOnly="False" RowSpan="1" MaxLength="0" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" ReadOnly="False" RowSpan="1" Span="1" MaxLength="0" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" ReadOnly="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EmployeeIDs" Editor="text" FieldName="EmployeeIDs" ReadOnly="False" Span="2" Width="180" Visible="False" MaxLength="0" NewRow="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EmployerName" Editor="text" FieldName="EmployerName" ReadOnly="False" Span="1" Width="80" Visible="False" MaxLength="0" NewRow="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ShortTermNO" Editor="text" FieldName="ShortTermNO" Format="" ReadOnly="False" Span="1" Visible="False" Width="120" MaxLength="0" NewRow="False" RowSpan="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請款受款人" Editor="inforefval" EditorOptions="title:'請款受款人搜尋',panelWidth:450,remoteName:'sShortTermMasterApply.Vendors',tableName:'Vendors',columns:[{field:'VendID',title:'廠商代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'廠商名稱',width:240,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Employee_ID',title:'員工代號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'廠商代號',textFieldCaption:'廠商名稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="ReqPayTo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請款支付方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sShortTermMasterApply.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ReqPayTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="請款預付日期" Editor="datebox" FieldName="ReqPlanPayDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="121" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="ShortTermNO" ParentFieldName="ShortTermNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemNO" NumDig="2" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ShortTermNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ShortTermDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="EmployeeID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="ShortTermTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="CompanyID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="TotalAmount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="M" FieldName="ApplyType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="NR" FieldName="FlowStatus" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SetUpStatus" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RequestDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShortTermGist" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PlanPayDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="RequestDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="ShortTermAmount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OriginalAmount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RequisitionAmount" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ShortTermDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
         <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="30px" DialogTop="100px" Title="選取外勞" Width="450px" OnSubmited="JQDialog3OnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                    </div>
                       <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AlwaysClose="True" DataMember="LaborList" RemoteName="sShortTermMasterApplyJS.LaborList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" PageList="10,20,30,40,50" PageSize="30" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="課程職能應上課名單test" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="380px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="EmployerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="EmployerID" Editor="infocombobox" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sShortTermMaster.Employer',tableName:'Employer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="l.EmployerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞代號" Editor="text" FieldName="EmployeeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="Dorm" Editor="text" FieldName="Dorm" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                    <%-- <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />--%>
                </TooItems>
                </JQTools:JQDataGrid>
                      <br />
                    </div>
                </JQTools:JQDialog>
                   <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="30px" DialogTop="100px" Title="費用檢視" Width="450px" OnSubmited="JQDialog4OnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                    </div>
                       <JQTools:JQDataGrid ID="JQDataGrid3" runat="server" AlwaysClose="True" DataMember="FeeRequisitionList" RemoteName="sShortTermMasterApplyJS.FeeRequisitionList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="30" Pagination="True" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="課程職能應上課名單test" TotalCaption="合計l:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="450px" Width="380px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="費用名稱" Editor="text" FieldName="FeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="FeeAmount" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Total="sum" OnTotal="">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
                      <br />
                    </div>
         </JQTools:JQDialog>
        <JQTools:JQDialog ID="JQDialogSetUpList" runat="server" DialogLeft="10px" DialogTop="30px" Title="立帳資料列表" Width="860px" OnSubmited="" Closed="True" ShowSubmitDiv="False" Height="680px">
                 <JQTools:JQDataGrid ID="JQDataGridSetUpList" runat="server" AlwaysClose="True" DataMember="BillSetUpList" RemoteName="sShortTermMasterApplyJS.BillSetUpList" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="20" Pagination="False" QueryAutoColumn="False" QueryLeft="15px" QueryMode="Window" QueryTitle="設備查詢" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="合計:" UpdateCommandVisible="False" ViewCommandVisible="False" OnUpdate="" Height="480px" Width="780px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞中文姓名" Editor="text" FieldName="EmployeeTcName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="外勞英文姓名" Editor="text" FieldName="EmployeeEnName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="130">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="科目名稱" Editor="text" FieldName="FeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="110">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="立帳金額" Editor="text" FieldName="FeeAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" Total="sum" Format="N">
                        </JQTools:JQGridColumn>
                      </Columns>
                     <TooItems>
                     <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                </JQTools:JQDataGrid>
                 <br />
                 <JQTools:JQDataGrid ID="JQDataGridFeeSetUpPrePay" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="FeeSetUpPrePay" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sShortTermMasterApplyJS.FeeSetUpPrePay" RowNumbers="True" Title="離境預收清單" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="780px">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="EmployeeTcName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="EmployeeEnName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="年月" Editor="text" FieldName="YearMonth" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="費用科目" Editor="text" FieldName="FeeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="FeeAmount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="N">
                         </JQTools:JQGridColumn>
                     </Columns>
                 </JQTools:JQDataGrid>
            </JQTools:JQDialog>
            <script type="text/javascript">
                $(":input").css("background", backcolor);
         </script>
    </form>
</body>
</html>
