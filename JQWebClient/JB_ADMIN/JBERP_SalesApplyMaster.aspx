<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesApplyMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
      var backcolor = "#cbf1de"
      $(document).ready(function () {
           //設定欄位Caption 變顏色
           var flagIDs = ['#dataFormMasterDispatchAreaID', '#dataFormMasterDispatchAreaManager'];
           $(flagIDs.toString()).each(function () {
               var captionTd = $(this).closest('td').prev('td');
               captionTd.css({color: '#8A2BE2' });
           });
           //將Focus 欄位背景顏色改為黃色
           $(function () {
               $("input, select, textarea").focus(function () {
                   $(this).css("background-color", "yellow");
               });
               $("input, select, textarea").blur(function () {
                   $(this).css("background-color", backcolor);
               });
           });
           //var memo = $('#dataFormMasterDispatchAreaManager').closest('td');
           //memo.append('  ＊派遣區域,簽核主管當銷售類別[派遣]時專用選取填入')
           $("#dataGridDetail").datagrid({
                   onAfterEdit: function (rowIndex, rowData, changes) {
                   rowData.InvoiceAmt = rowData.InvoiceAmt * 1;
                   rowData.SalesAmt = rowData.SalesAmt * 1;
                   //rowData.SalesTotal = rowData.SalesAmt * 1;
                   $(this).datagrid('refreshRow', rowIndex);
               }
           });
       })
       function DataformMasterLoadSucess() {
           var parameters = Request.getQueryStringByName("P1");
           if (getEditMode($("#dataFormMaster")) == 'inserted') {
               var FormName = '#dataFormMaster';
               var HideFieldName = ['CustNOP'];
               $.each(HideFieldName, function (index, fieldName) {
                   $(FormName + fieldName).closest('td').prev('td').hide();
                   $(FormName + fieldName).closest('td').hide();
               });
               GetUserOrgNOs();
               var EmpFlowAgentList = GetEmpFlowAgentList();
               var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
               $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
               $('#divJQDataGrid1').hide();
           }
        
           $('#dataFormMasterApplyDate').datebox('textbox').attr('title', '請款月份');
           $('#dataFormMasterSalesYM').combobox('textbox').attr('title', '實際開立月份');
           $('#dataFormMasterContact').focus();
       }
       function dataFormMasterOnApply() {
           var InvoiceAmt = $("#dataFormMasterInvoiceAmt").val();
           if (InvoiceAmt == 0 || InvoiceAmt == undefined) {
               alert('注意!!,未新增明細資料,無法存檔,請新增!!');
               return false;
           }
           //將明細"銷售總額"放入主檔"銷售金額"
           var footerRows = $("#dataGridDetail").datagrid('getFooterRows');
           $("#dataFormMasterInvoiceAmt").val(footerRows[0].SalesTotal);
       }
       function dataGridDetailOnInsert() {
           var dataFormMasterCustNO = $("#dataFormMasterCustNO").refval('getValue');
           if (dataFormMasterCustNO == "" || dataFormMasterCustNO == undefined) {
               alert('注意!!,未選取銷貨客戶,請選取!!');
               $("#dataFormMasterCustNO").focus();
               return false;
            }
           var cc = $('#dataFormMasterCustNO').refval('selectItem').text;
           if (cc.indexOf("?") != -1) {
               alert('注意!!,銷貨客戶資料有誤,原因：此客戶未指定業務人員或指定的業務人員已失效');
               return false;
           }
           var SalesTypeID = $('#dataFormMasterSalesTypeID').combobox('getValue');
           var SalesTypeName = $('#dataFormMasterSalesTypeID').combobox('getText');
           var cc = $('#dataFormMasterPayKindList').val();
           if (cc.indexOf(SalesTypeID) == -1) {
               alert('注意!!,銷貨客戶資料有誤,原因：已選取的銷售類別「' + SalesTypeName+'」未設定收款方式,請到「客戶資料維護」設定');
               return false;
           }
           var dataFormMasterSalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
           if (dataFormMasterSalesTypeID == "" || dataFormMasterSalesTypeID == undefined) {
               alert('注意!!,未選取銷貨類別,請選取!!');
               $("#dataFormMasterSalesTypeID").focus();
               return false;
           }
           var dataFormMasterSalesYM = $("#dataFormMasterSalesYM").combobox('getValue');
           if (dataFormMasterSalesYM == "" || dataFormMasterSalesYM == undefined) {
               alert('注意!!,未選取發票年月,請選取!!');
               $("#dataFormMasterSalesYM").focus();
               return false;
           }
           //var dataFormMasterSalesOutLine = $("#dataFormMasterSalesOutLine").combobox('getValue');
           //if (dataFormMasterSalesOutLine == "" || dataFormMasterSalesOutLine == undefined) {
           //    alert('注意!!,未登錄或選取銷貨說明,請登錄或選取!!');
           //    $("#dataFormMasterSalesOutLine").focus();
           //    return false;
           //}
       }
       function SalesItemIDOnSelect(rowData) {
           $("#dataFormMasterSalesItemType").val(rowData.SalesItemType);
           $("#dataFormMasterSalesItemName").val(rowData.SalesItemName);
       }
       function SalesQtyOnBlur() {
           CalTotalFee();
       }
       function SalesPriceOnBlur() {
           CalTotalFee();
       }
       function SalesAmtOnBlur() {
           CalTotalFee();
       }
       function SalesTaxOnBlur() {
               var amt = $("#dataFormDetailSalesAmt").val();
               var tax = $("#dataFormDetailSalesTax").val();
               var tot = parseInt(amt) + parseInt(tax);
               $("#dataFormDetailSalesTotal").numberbox('setValue',tot);
               //$("#dataFormDetailSalesTotal").val(tot);
       }
       function CloseDataForm() {
           self.parent.closeCurrentTab();
           return false;
       }
       function CalTotalFee() {
           var qty =   $("#dataFormDetailSalesQty").val();
           var price = $("#dataFormDetailSalesPrice").val();
           var amt = (parseInt(qty) * parseFloat(price))
           var tax = Math.round(amt * 0.05);
           var tot = parseInt(amt) + parseInt(tax)
           $("#dataFormDetailSalesAmt").numberbox('setValue', amt);
           $("#dataFormDetailSalesTax").numberbox('setValue', tax);
           $("#dataFormDetailSalesTotal").numberbox('setValue', tot);
       }
       function SumInvoiceAmt(rowData) {
           $("#dataFormMasterInvoiceAmt").val(rowData.SalesTotal);
       }
       function OnSelectEmployeeID(rowData) {
           $("#dataFormMasterApplyOrg_NO").combobox('setValue',rowData.OrgNO);
           $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
       }
       //取得此表單設登入者為有效代理人人員清單
       function GetEmpFlowAgentList() {
           var UserID = getClientInfo("UserID");
           var Flow = "銷貨收入申請";
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
       function dataFormDetailLoadSucess() {
       
           var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
           if (SalesTypeID != 7) {
               HideField();
           }
       }
       function dataGridDetailLoadSucess() {
           var parameters = Request.getQueryStringByName("P1");
           if (parameters == "Apply") {
               var rowData = $("#dataFormMasterApplyEmpID").combobox('getSelectItem');
               $("#dataFormMasterApplyOrg_NO").val(rowData.OrgNO);
           }
       }
       function SalesTypeIDOnSelect(rowData) {
           //$("#dataFormMasterSalesOutLine").combobox('setValue', '');
           //$("#dataFormMasterSalesOutLine").combobox('setWhere', 'SalesTypeID='+rowData.SalesTypeID);
           if (rowData.SalesTypeID == '7') {
               $("#dataFormMasterDispatchAreaID").combobox('setWhere', '');
               $("#dataFormMasterDispatchAreaManager").combobox('setWhere', '');
           }
       }
       function HideField() {
           var FormName = '#dataFormDetail';
           var HideFieldName = ['EmpLaAmt', 'EmpCount'];
           $.each(HideFieldName, function (index, fieldName) {
               $(FormName + fieldName).closest('td').prev('td').hide();
               $(FormName + fieldName).closest('td').hide();
           });
       }
       function DispatchAreaIDOnSelect(rowData) {
           $("#dataFormMasterDispatchAreaManager").combobox('setValue',rowData.DispatchAreaManager);
       }
       function GetSalesYM() {
           var Dt = new Date();
           var MonthStr = '00' + (Dt.getMonth() + 1).toString();
           var RR = (Dt.getFullYear() - 1911).toString().trim() + "/" + MonthStr.substring(MonthStr.length - 2);
           return RR;
       }
       function GetInvoiceYM() {
           var rr = $("#dataFormMasterSalesYM").combobox('getValue');
           return rr
       }
       // //客戶代號選取時
       //function GetSignCount() {
       //    var SalesApplyNO = $("#dataFormMasterSalesApplyNO").val();
       //    $.ajax({
       //        type: "POST",
       //        url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesApplyMaster.ERPSalesApplyMaster',
       //        data: "mode=method&method=" + "GetSignCount" + "&parameters=" + SalesApplyNO,
       //        cache: false,
       //        async: false,
       //        success: function (data) {
       //            cnt = $.parseJSON(data);
       //        }
       //    });
       //    return cnt;
       //}
       function CustNOOnSelect(rowData) {
           $("#dataFormMasterPayKindList").val(rowData.PayKindList);
       }
       //取得USER的部門代號
       function GetUserOrgNOs() {
           var UserID = getClientInfo("UserID");
           $.ajax({
               type: "POST",
               url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalesApplyMaster.ERPSalesApplyMaster', //連接的Server端，command
               data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" +  UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
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
       //function dataGridDetailOnUpdate() {
       //    var footerRows = $("#dataGridDetail").datagrid('getFooterRows');
       //    alert(footerRows[0].Quantity);
       //}
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
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesOutLine" Editor="text" FieldName="SalesOutLine" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNotes" Editor="text" FieldName="SalesNotes" Format="" MaxLength="0" Width="360" />
                    <JQTools:JQGridColumn Alignment="right" Caption="InsGroupID" Editor="numberbox" FieldName="InsGroupID" Format="" Width="360" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨申請" DialogLeft="10px" DialogTop="10px" Width="750px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesApplyMaster" HorizontalColumnsCount="4" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" OnCancel="CloseDataForm" OnApply="dataFormMasterOnApply" OnLoadSuccess="DataformMasterLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesApplyNO" Format="" Width="87" ReadOnly="True" Span="1" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨屬性" Editor="infocombobox" EditorOptions="valueField:'SalesItemID',textField:'SalesItemName',remoteName:'sERPSalesApplyMaster.SalesItem',tableName:'SalesItem',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:SalesItemIDOnSelect,panelHeight:200" FieldName="SalesItemID" MaxLength="0" ReadOnly="False" Span="1" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" MaxLength="0" ReadOnly="True" Span="1" Width="95" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERPSalesApplyMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="right" Caption="銷貨金額" Editor="text" FieldName="InvoiceAmt" MaxLength="0" ReadOnly="True" Span="1" Visible="True" Width="90" Format="N" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPSalesApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployeeID,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" ReadOnly="False" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨日期" Editor="datebox" FieldName="ApplyDate" Format="" maxlength="0" OnBlur="" Span="1" Visible="True" Width="100" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPSalesApplyMaster.InvoiceYM',tableName:'InvoiceYM',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesYM" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupShortName',remoteName:'sERPSalesApplyMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InsGroupID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨客戶" Editor="inforefval" EditorOptions="title:'銷貨客戶選取',panelWidth:350,remoteName:'sERPSalesApplyMaster.Customers',tableName:'Customers',columns:[{field:'CustNO',title:'客戶代號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'CustShortName',title:'客戶簡稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'Contact',value:'ContactA'},{field:'SalesID',value:'SalesID'},{field:'TaxNO',value:'TaxNO'}],whereItems:[],valueField:'CustNO',textField:'CustShortName',valueFieldCaption:'CustNO',textFieldCaption:'CustShortName',cacheRelationText:true,checkData:true,showValueAndText:false,onSelect:CustNOOnSelect,selectOnly:true" FieldName="CustNO" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Contact" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="96" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" Span="1" Visible="True" Width="90" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨業務" Editor="infocombobox" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesApplyMaster.Sales',tableName:'Sales',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesID" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="144" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷售類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesApplyMaster.SalesType',tableName:'SalesType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:SalesTypeIDOnSelect,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="95" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨憑證" Editor="infocombobox" EditorOptions="valueField:'PROOFTYPEID',textField:'PROOFTYPENAME',remoteName:'sERPSalesApplyMaster.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨說明" Editor="text" EditorOptions="" FieldName="SalesOutLine" Format="" MaxLength="250" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨備註" Editor="text" FieldName="SalesNotes" Format="" MaxLength="0" ReadOnly="False" Span="4" Visible="True" Width="613" />
                        <JQTools:JQFormColumn Alignment="left" Caption="派遣區域" Editor="infocombobox" EditorOptions="valueField:'DispatchAreaID',textField:'DispatchAreaName',remoteName:'sERPSalesApplyMaster.DispatchArea',tableName:'DispatchArea',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:DispatchAreaIDOnSelect,panelHeight:200" FieldName="DispatchAreaID" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核主管" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sERPSalesApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DispatchAreaManager" MaxLength="0" ReadOnly="False" Span="3" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesItemType" Editor="text" FieldName="SalesItemType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷項名稱" Editor="text" FieldName="SalesItemName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PayKindList" Editor="text" FieldName="PayKindList" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNOP" ReadOnly="False" Width="96" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" ReadOnly="False" Span="1" Width="80" MaxLength="0" NewRow="False" RowSpan="1" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPSalesApplyDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="dataGridDetailOnInsert" Width="670px" OnLoadSuccess="dataGridDetailLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="ItemSeq" Format="" Width="45" />
                        <JQTools:JQGridColumn Alignment="left" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="60" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="SalesQty" Format="" Width="60" />
                        <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="text" FieldName="SalesPrice" Width="70" />
                        <JQTools:JQGridColumn Alignment="right" Caption="銷售金額" Editor="numberbox" FieldName="SalesAmt" Format="N" Width="75" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="稅額" Editor="numberbox" FieldName="SalesTax" Format="N" Width="75" Total="sum" />
                        <JQTools:JQGridColumn Alignment="right" Caption="銷售總額" Editor="numberbox" FieldName="SalesTotal" Format="N" Width="80" Total="sum" OnTotal="SumInvoiceAmt" />
                        <JQTools:JQGridColumn Alignment="right" Caption="員工勞保" Editor="numberbox" FieldName="EmpLaAmt" Format="" Width="70" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="員工人數" Editor="numberbox" FieldName="EmpCount" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="SalesApplyNO" Editor="text" FieldName="SalesApplyNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="ItemOutLine" Editor="text" FieldName="ItemOutLine" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
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
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="770px" DialogLeft="30px" DialogTop="240px" Title="明細維護">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPSalesApplyDetails" HorizontalColumnsCount="5" RemoteName="sERPSalesApplyMaster.ERPSalesApplyMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormDetailLoadSucess" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="SalesApplyNO" Editor="text" FieldName="SalesApplyNO" Format="" Visible="False" Width="120" ReadOnly="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="ItemSeq" Format="" ReadOnly="True" Width="30" Span="5" />
                            <JQTools:JQFormColumn Alignment="left" Caption="發票年月" Editor="infocombobox" EditorOptions="valueField:'InvoiceYM',textField:'InvoiceYM',remoteName:'sERPSalesApplyMaster.InvoiceYM',tableName:'InvoiceYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="InvoiceYM" Format="" Span="4" Width="83" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="False" />
                            <JQTools:JQFormColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="SalesQty" Format="" Width="60" Visible="True" />
                            <JQTools:JQFormColumn Alignment="right" Caption="單價" Editor="numberbox" FieldName="SalesPrice" Width="70" OnBlur="SalesPriceOnBlur" EditorOptions="precision:2" ReadOnly="False" Span="1" />
                            <JQTools:JQFormColumn Alignment="right" Caption="銷售金額" Editor="numberbox" FieldName="SalesAmt" Format="N" Width="70" OnBlur="SalesAmtOnBlur" Span="1" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="right" Caption="銷售稅額" Editor="numberbox" FieldName="SalesTax" Format="N" Width="70" OnBlur="SalesTaxOnBlur" Span="1" ReadOnly="False" Visible="True" />
                            <JQTools:JQFormColumn Alignment="right" Caption="銷售總額" Editor="numberbox" FieldName="SalesTotal" Format="N" Width="70" Span="1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="銷售備註" Editor="text" FieldName="ItemOutLine" Format="" Span="5" Width="590" ReadOnly="False" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="員工勞保" Editor="numberbox" FieldName="EmpLaAmt" Span="1" Width="70" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="員工人數" Editor="numberbox" FieldName="EmpCount" Span="4" Width="70" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
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
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesItemID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="+" FieldName="SalesItemType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="InvoiceAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesYM" FieldName="SalesYM" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ProofTypeID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesOutLine" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TaxNO" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SalesAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SalesTax" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SalesTotal" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="EmpLaAmt" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="SalesPrice" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="EmpCount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="False" DefaultMethod="GetInvoiceYM" FieldName="InvoiceYM" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemSeq" NumDig="2" />
            </JQTools:JQDialog>
        </div>
          <script type="text/javascript">
              $(":input").css("background", backcolor);
         </script>
    </form>
   
</body>
</html>
