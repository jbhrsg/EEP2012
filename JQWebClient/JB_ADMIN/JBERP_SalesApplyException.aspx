<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesApplyException.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterSalesItemID', '#dataFormMasterCustNO', '#dataFormMasterCustNO', '#dataFormMasterSalesTypeID'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            $("#dataGridDetail").datagrid({
                onAfterEdit: function (rowIndex, rowData, changes) {
                    rowData.InvoiceAmt= rowData.InvoiceAmt * 1;
                    rowData.SalesAmt = rowData.SalesAmt * 1;
                    rowData.SalesTotal = rowData.SalesAmt * 1;
                    $(this).datagrid('refreshRow', rowIndex);
                }
            });
            var IsPostToNjbExcept = $('#dataFormMasterIsPostToNjbExcept').closest('td');
            IsPostToNjbExcept.append(' 請會計人員確認,當[選取]時將沖銷應收帳款,否則該申異常申請結案!!')
        })
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //點按明細新增時檢查
        function dataGridDetailOnInsert() {
              var dataFormMasterSalesItemID = $("#dataFormMasterSalesItemID").combobox('getValue')
              if (dataFormMasterSalesItemID == "" || dataFormMasterSalesItemID == undefined) {
                  alert('注意!!,未選取[銷貨屬性],請選取後再點選[新增]!!');
                  $("#dataFormMasterSalesItemID").focus();
                  return false;
              }
            var dataFormMasterCustNO = $("#dataFormMasterCustNO").refval('getValue');
            if (dataFormMasterCustNO == "" || dataFormMasterCustNO == undefined) {
                alert('注意!!,未選取[銷貨客戶],請選取後再點選[新增]!!');
                $("#dataFormMasterCustNO").focus();
                return false;
            }
            var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
            if (dataFormMasterSalesTypeID == "" || dataFormMasterSalesTypeID == undefined) {
                alert('注意!!,未選取[銷貨類別],請選取後再點選[新增]!!');
                $("#dataFormMasterSalesTypeID").focus();
                return false;
            }
        }
        //dataGrid載入完成
        function dataGridDetailLoadSucess() {
            //var parameters = Request.getQueryStringByName("P1");
            //if (parameters == "Apply") {
            //    var rowData = $("#dataFormMasterApplyEmpID").combobox('getSelectItem');
            //    $("#dataFormMasterApplyOrg_NO").val(rowData.OrgNO);
            //}
        }
        //當送出存檔時
        function dataFormMasterOnApply() {
            var parameters = Request.getQueryStringByName("P1");
            if (parameters == "Approve") {
                var ExceptDealTypeID = $("#dataFormMasterExceptDealTypeID").combobox('getValue');
                var ExceptDealItemID = $("#dataFormMasterExceptDealItemID").combobox('getValue');
                if  (ExceptDealTypeID==1 &&  (ExceptDealItemID == "" || ExceptDealItemID == undefined)) {
                    alert('注意!!,當主管審核時且處理方式="扣薪"時,處理細項不得為空白,請選取');
                    return false;
                }
            }
            var dataFormMasterSalesItemID = $("#dataFormMasterSalesItemID").combobox('getValue')
            if (dataFormMasterSalesItemID == "" || dataFormMasterSalesItemID == undefined) {
                alert('注意!!,未選取[銷貨屬性],請選取後再點選[新增]!!');
                $("#dataFormMasterSalesItemID").focus();
                return false;
            }
            var dataFormMasterCustNO = $("#dataFormMasterCustNO").refval('getValue');
            if (dataFormMasterCustNO == "" || dataFormMasterCustNO == undefined) {
                alert('注意!!,未選取[銷貨客戶],請選取!!');
                $("#dataFormMasterCustNO").focus();
                return false;
            }
            var ExceptDealTypeID = $("#dataFormMasterExceptDealTypeID").combobox('getValue');
            if (ExceptDealTypeID == "" || ExceptDealTypeID == undefined) {
                alert('注意!!,未選取[處理方式],請選取!!');
                $("#ExceptDealTypeID").focus();
                return false;
            }
            var ExceptAmt = $("#dataFormMasterExceptAmt").val();
            if (ExceptAmt == 0 || ExceptAmt == undefined) {
                alert('注意!!,未新增明細資料,無法存檔送出,請新增!!');
                //$("#dataFormMasterExceptAmt").focus();
                return false;
            }
            var SalesExceptionID = $("#dataFormMasterSalesExceptionID").combobox('getValue');
            if (SalesExceptionID == "" || SalesExceptionID == undefined) {
                alert('注意!!,未選取異常原因,無法存檔送出,請選取!!');
                //$("#dataFormMasterExceptAmt").focus();
                return false;
            }
            var SalesItemName = $("#dataFormMasterSalesItemID").combobox('getText');
            var CustName = $("#dataFormMasterCustNO").data("inforefval").refval.find("input.refval-text").val();
            var SalesTypeName = $("#dataFormMasterSalesTypeID").combobox('getText');
            var SalesAmt = $("#dataFormMasterExceptAmt").val();
            $("#dataFormMasterSalesOutLine").val(CustName.toString().trim()+'/'+SalesTypeName.toString().trim()+'/'+SalesAmt);
            return true;
        }
        //當銷貨屬性被選取時
        function SalesItemIDOnSelect(rowData) {
            $("#dataFormMasterSalesItemType").val(rowData.SalesItemType);
            $("#dataFormMasterSalesItemName").val(rowData.SalesItemName);
            $("#dataFormMasterSalesExceptionID").combobox('setValue', "");
            $("#dataFormMasterSalesExceptionID").combobox('setWhere', "SalesItemID=" + rowData.SalesItemID);
        }
        //當銷貨客戶被選取時
        function CustNOOnSelect(rowData) {
            $("#dataFormDetailInvoiceNO").combobox('setValue', "");
            $("#dataFormDetailInvoiceNO").combobox('setWhere', "CUNO=" + "'" + rowData.CustNO + "'");
        }
        //當銷貨類別被選取時
        function SalesTypeIDOnSelect(rowData) {
            $("#dataFormMasterDispatchAreaID").combobox('setValue', "");
            $("#dataFormMasterDispatchAreaManager").combobox('setValue', "");
        }
        //當派潛區域被選取時
        function DispatchAreaIDOnSelect(rowData) {
            $("#dataFormMasterDispatchAreaManager").combobox('setValue', rowData.DispatchAreaManager);
        }
        //當異常原因被選取時
        function SalesExceptionIDOnSelect(rowData) {
            $("#dataFormMasterSalesExceptionName").val(rowData.SalesExceptionName);
        }
        //當處理方式被選取時
        function ExceptDealTypeIDOnSelect(rowData) {
            $("#dataFormMasterExceptDealTypeName").val(rowData.ExceptDealTypeName);
            $("#dataFormMasterExceptDealItemID").combobox('setValue', "");
            $("#dataFormMasterExceptDealItemID").combobox('setWhere', "ExceptDealTypeID=" + rowData.ExceptDealTypeID);
        }
        //當處理細項被選取時
        function ExceptDealItemIDOnSelect(rowData) {
            $("#dataFormMasterExceptDealItemName").val(rowData.ExceptDealItemName);
        }
        //當選取發票時
        function InvoiceNOOnSelect(rowData) {
            $("#dataFormDetailSalesDate").datebox('setValue',rowData.DLVDATE);;
            $("#dataFormDetailInvoiceAmt").val(rowData.DLVAMT);
        }
        function DataformLoadSucess() {
            var parameters = Request.getQueryStringByName("P1");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['ExceptDealItemID', 'ExceptDealItemName', 'IsPostToNjbExcept'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                GetUserOrgNOs();
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
            }
            else {
                ExceptDealTypeID = $("#dataFormMasterExceptDealTypeID").combobox('getValue');
                $("#dataFormMasterExceptDealItemID").combobox('setWhere', "ExceptDealTypeID=" + ExceptDealTypeID);
            }
           if (parameters == "Apply") {
                var SalesItemID = $("#dataFormMasterSalesItemID").combobox('getValue');
                if (SalesItemID == "" || SalesItemID == undefined) {
                    $("#dataFormMasterSalesExceptionID").combobox('setValue', "");
                    $("#dataFormMasterSalesExceptionID").combobox('setWhere', "1=0");
                    $("#dataFormMasterExceptDealItemID").combobox('setValue', "");
                    $("#dataFormMasterExceptDealItemID").combobox('setWhere', "1=0");
                    $("#dataGridDetailInvoiceNO").combobox('setValue', "");
                    $("#dataGridDetailInvoiceNO").combobox('setWhere', "1=0");
                }
             }
            if (parameters == "Approve") {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['SalesExceptionID'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
            }
        }
        function dataFormDetailOnApply() {
            var InvoiceNO = $("#dataFormDetailInvoiceNO").combobox('getValue');
            if (InvoiceNO == "" || InvoiceNO == undefined) {
                alert('注意!!,銷貨單號不可為空白,請選取!!');
                return false;
            }
        }
        function OnSelectEmployeeID(rowData) {
            $("#dataFormMasterApplyOrg_NO").val(rowData.OrgNO);
        }
            //加總dataGridDetailInvoiceAmt => dataFormMasterInvoiceAmt
        function SumInvoiceAmt(rowData) {
                $("#dataFormMasterInvoiceAmt").val(rowData.InvoiceAmt);
            }
            //加總dataGridDetailSalesAmt => dataFormMasterExceptAmt
        function SumSalesAmt(rowData) {
                $("#dataFormMasterExceptAmt").val(rowData.SalesAmt);
        }
        //取得此表單設登入者為有效代理人人員清單
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "銷貨異常申請";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesApplyMaster.ERPSalesApplyMaster', //連接的Server端，command
                data: "mode=method&method=" + "GetEmpFlowAgentList" + "&parameters=" + UserID + "," + Flow, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesApplyMaster.ERPSalesApplyMaster', //連接的Server端，command
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
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesApplyMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesApplyNO" Editor="text" FieldName="SalesApplyNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustNO" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Contact" Editor="text" FieldName="Contact" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesOutLine" Editor="text" FieldName="SalesOutLine" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNotes" Editor="text" FieldName="SalesNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesTypeID" Editor="numberbox" FieldName="SalesTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesItemID" Editor="numberbox" FieldName="SalesItemID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesItemType" Editor="text" FieldName="SalesItemType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesItemName" Editor="text" FieldName="SalesItemName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesExceptionID" Editor="text" FieldName="SalesExceptionID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesExceptionName" Editor="text" FieldName="SalesExceptionName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ExceptDealTypeID" Editor="numberbox" FieldName="ExceptDealTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExceptDealTypeName" Editor="text" FieldName="ExceptDealTypeName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ExceptDealItemID" Editor="numberbox" FieldName="ExceptDealItemID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExceptDealItemName" Editor="text" FieldName="ExceptDealItemName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsPostToNjbExcept" Editor="text" FieldName="IsPostToNjbExcept" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨異常申請" DialogLeft="10px" DialogTop="10px" Width="880px" ShowModal="True">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesApplyMaster" HorizontalColumnsCount="4" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" OnCancel="CloseDataForm" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="DataformLoadSucess" OnApply="dataFormMasterOnApply" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesApplyNO" Format="" Width="127" Span="1" maxlength="0" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" maxlength="0" Span="1" Width="100" />
                        <JQTools:JQFormColumn Alignment="right" Caption="發票金額" Editor="text" FieldName="InvoiceAmt" maxlength="0" ReadOnly="True" Span="1" Width="119" />
                        <JQTools:JQFormColumn Alignment="right" Caption="異常金額" Editor="text" FieldName="ExceptAmt" ReadOnly="True" Span="1" Width="127" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨屬性" Editor="infocombobox" EditorOptions="valueField:'SalesItemID',textField:'SalesItemName',remoteName:'sERPSalesApplyMaster.SalesItem1',tableName:'SalesItem1',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SalesItemIDOnSelect,panelHeight:200" FieldName="SalesItemID" Format="" Width="130" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPSalesApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployeeID,panelHeight:200" FieldName="ApplyEmpID" Format="" Width="100" Span="1" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPSalesApplyMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="124" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sERPSalesApplyMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" Format="" Span="1" Width="130" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨客戶" Editor="inforefval" EditorOptions="title:'客戶搜尋',panelWidth:350,remoteName:'sERPSalesApplyMaster.Customers',tableName:'Customers',columns:[],columnMatches:[{field:'Contact',value:'ContactA'},{field:'SalesID',value:'SalesID'},{field:'TaxNO',value:'TaxNO'}],whereItems:[],valueField:'CustNO',textField:'CustShortName',valueFieldCaption:'客戶代號',textFieldCaption:'客戶名稱',cacheRelationText:true,checkData:true,showValueAndText:false,onSelect:CustNOOnSelect,selectOnly:true" FieldName="CustNO" Format="" Width="133" Span="1" ReadOnly="False" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Contact" Format="" Width="96" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" EditorOptions="" FieldName="TaxNO" Format="" Width="120" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨業務" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesApplyMaster.Sales',tableName:'Sales',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷售類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesApplyMaster.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:SalesTypeIDOnSelect,panelHeight:200" FieldName="SalesTypeID" Format="" Span="4" Width="130" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="派遣區域" Editor="infocombobox" EditorOptions="valueField:'DispatchAreaID',textField:'DispatchAreaName',remoteName:'sERPSalesApplyMaster.DispatchArea',tableName:'DispatchArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DispatchAreaIDOnSelect,panelHeight:200" FieldName="DispatchAreaID" Span="1" Width="100" ReadOnly="False" Visible="False" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核主管" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPSalesApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DispatchAreaManager" ReadOnly="True" Span="2" Width="97" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異常原因" Editor="infocombobox" FieldName="SalesExceptionID" Span="4" Width="730" EditorOptions="valueField:'SalesExceptionID',textField:'SalesExceptionName',remoteName:'sERPSalesApplyMaster.SalesException',tableName:'SalesException',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:SalesExceptionIDOnSelect,panelHeight:200" Format="" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="原因說明" Editor="textarea" FieldName="SalesExceptionName" Span="4" Width="725" Format="" maxlength="0" EditorOptions="height:36" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="處理方式" Editor="infocombobox" FieldName="ExceptDealTypeID" Format="" Span="1" Width="180" EditorOptions="valueField:'ExceptDealTypeID',textField:'ExceptDealTypeName',remoteName:'sERPSalesApplyMaster.SalesExceptDealType',tableName:'SalesExceptDealType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ExceptDealTypeIDOnSelect,panelHeight:200" MaxLength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="方式說明" Editor="text" FieldName="ExceptDealTypeName" Format="" Span="3" Width="491" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="處理細項" Editor="infocombobox" FieldName="ExceptDealItemID" Format="" Span="1" Width="180" EditorOptions="valueField:'ExceptDealItemNO',textField:'ExceptDealItemName',remoteName:'sERPSalesApplyMaster.SalesExceptDealItem',tableName:'SalesExceptDealItem',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="細項說明" Editor="text" FieldName="ExceptDealItemName" Format="" Span="3" Width="491" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="SalesOutLine" Format="" Width="526" maxlength="0" Span="4" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖銷帳款" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsPostToNjbExcept" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesNotes" Editor="text" FieldName="SalesNotes" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesItemType" Editor="text" FieldName="SalesItemType" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesItemName" Editor="text" FieldName="SalesItemName" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" Span="1" maxlength="0" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Span="1" Width="80" maxlength="0" ReadOnly="False" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPSalesApplyDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="560px" OnInsert="dataGridDetailOnInsert" OnLoadSuccess="dataGridDetailLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="ItemSeq" Format="" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="InvoiceNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="right" Caption="銷貨金額" Editor="numberbox" FieldName="InvoiceAmt" Width="80" OnTotal="SumInvoiceAmt" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="沖銷金額" Editor="numberbox" FieldName="SalesAmt" Format="" Visible="True" Width="120" OnTotal="SumSalesAmt" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SalesTax" Editor="numberbox" FieldName="SalesTax" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SalesTotal" Editor="numberbox" FieldName="SalesTotal" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="EmpLaAmt" Editor="numberbox" FieldName="EmpLaAmt" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesApplyNO" Editor="text" FieldName="SalesApplyNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemOutLine" Editor="text" FieldName="ItemOutLine" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="InvoiceYM" Editor="text" FieldName="InvoiceYM" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SalesQty" Editor="numberbox" FieldName="SalesQty" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="SalesApplyNO" ParentFieldName="SalesApplyNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                  
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="45px" DialogTop="350px" Title="銷貨異常明細" Width="750px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPSalesApplyDetails" HorizontalColumnsCount="3" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="dataFormDetailOnApply" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="ItemSeq" Format="" Width="30" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="infocombobox" FieldName="InvoiceNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="450" EditorOptions="valueField:'DLVNO',textField:'Status',remoteName:'sERPSalesApplyMaster.ARStatus',tableName:'ARStatus',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:InvoiceNOOnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" EditorOptions="" FieldName="SalesDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額" Editor="text" FieldName="InvoiceAmt" Width="80" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="沖銷金額" Editor="numberbox" FieldName="SalesAmt" Format="" Width="80" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesTax" Editor="numberbox" FieldName="SalesTax" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesTotal" Editor="numberbox" FieldName="SalesTotal" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="EmpLaAmt" Editor="numberbox" FieldName="EmpLaAmt" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesApplyNO" Editor="text" FieldName="SalesApplyNO" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="ItemOutLine" Editor="text" FieldName="ItemOutLine" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="InvoiceYM" Editor="text" FieldName="InvoiceYM" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesQty" Editor="numberbox" FieldName="SalesQty" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="SalesApplyNO" ParentFieldName="SalesApplyNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="SalesApplyNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="InsGroupID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="-" FieldName="SalesItemType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="true" FieldName="IsPostToNjbExcept" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SalesAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="InvoiceAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="SalesDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemSeq" NumDig="2" />
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
