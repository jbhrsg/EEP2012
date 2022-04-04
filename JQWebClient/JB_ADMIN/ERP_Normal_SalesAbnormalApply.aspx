<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Normal_SalesAbnormalApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            //紅字
            //RedFields('#dataFormMaster', ['SalesNO','AbnormalType']);
        });

        function SalesNO_OnSelect(row) {
            $("#dataFormMasterSalesTypeID").combobox('setValue',row.SalesTypeID);
            $("#dataFormMasterInvoiceNO").val(row.InvoiceNO);
            $("#dataFormMasterCustomerID").combobox('setWhere', "CustomerID='"+row.CustomerID+"'");
            $("#dataFormMasterCustomerID").combobox('setValue', row.CustomerID);
            $("#dataFormMasterTaxNO").val(row.TaxNO);
            $("#dataFormMasterSalesID").combobox('setValue', row.SalesID);
            $("#dataFormMasterSumAmount").val(row.SumAmount);
            $("#dataFormMasterSalesAmount").val(row.SalesAmount);
            $("#dataFormMasterSalesTax").val(row.SalesTax);
            $("#dataFormMasterSalesTotal").val(row.SalesTotal);
        }

        function dataFormMaster_OnLoadSuccess() {

            //設值ApplyOrg_NO、Org_NOParent
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var myArr = GetUserOrgNOs();//得組織編號和隸屬組織編號
                var myCostCenterID;
                if (myArr.length > 0) {
                    $("#dataFormMasterApplyOrg_NO").combobox('setValue', myArr[0]);
                    $("#dataFormMasterOrg_NOParent").val(myArr[1]);
                }
            }


            //欄位 顯示或隱藏 停用或啟用
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            if (parameter == "Apply") {//申請
                HideFields('#dataFormMaster', ['IsWarrant','WarrantField']);
            } else if (parameter=="MApprove"){//主管
                HideFields('#dataFormMaster', ['IsWarrant', 'WarrantField']);
            } else if (parameter == "AApprove") {//會計
                ShowFields('#dataFormMaster', ['IsWarrant', 'WarrantField']);

                var DisabledFieldArr = ['Contact','AbnormalReason','DealType','WarrantAmount'];
                var DisabledComboboxArr = ['AbnormalType', 'AbnormalReasonID', 'DealTypeID'];
                var DisabledRefvalArr = ['SalesNO'];
                DisableFields("#dataFormMaster", DisabledFieldArr, DisabledComboboxArr, DisabledRefvalArr);
            }

        }

        //當異常屬性被選取時
        function SalesItemIDOnSelect(rowData) {
            //$("#dataFormMasterSalesItemType").val(rowData.SalesItemType);
            //$("#dataFormMasterSalesItemName").val(rowData.SalesItemName);
            $("#dataFormMasterAbnormalReasonID").combobox('setValue', "");
            $("#dataFormMasterAbnormalReasonID").combobox('setWhere', "SalesItemID=" + rowData.SalesItemID);
        }

        //當異常原因被選取時
        function SalesExceptionIDOnSelect(rowData) {
            $("#dataFormMasterAbnormalReason").val(rowData.SalesExceptionName);
        }

        //當處理方式被選取時
        function ExceptDealTypeIDOnSelect(rowData) {
            $("#dataFormMasterDealType").val(rowData.ExceptDealTypeName);
            //$("#dataFormMasterDealItemID").combobox('setValue', "");
            //$("#dataFormMasterDealItemID").combobox('setWhere', "ExceptDealTypeID=" + rowData.ExceptDealTypeID);
        }

        function dataFormMaster_OnApply() {
            
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }
            if (parameter == "Apply") {//申請
                if ($("#dataFormMasterAbnormalType").combobox('getValue') == '') {
                    alert("異常屬性未填");
                    $("#dataFormMasterAbnormalType").focus();
                    return false;
                }
            } else if (parameter == "AApprove") {//會計
                if ($("#dataFormMasterIsWarrant").checkbox('getValue')=='1' && $("#dataFormMasterWarrantField").combobox('getValue')=="") {
                    alert("沖銷欄位未填");
                    $("#dataFormMasterWarrantField").focus();
                    return false;
                }
            }
            
        }

        //----工具---
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var myArr = [];
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_SalesAbnormalApply.SalesAbnormalApply', //連接的Server端，command
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        myArr[0] = rows[0].OrgNO;
                        myArr[1] = rows[0].OrgNOParent;
                    }
                }
            }
            );
            return myArr;
        }

        function HideFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
            });
        }
        function ShowFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames, DisabledRefvalNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
            $.each(DisabledRefvalNames, function (index, value) {
                $(FormName + value).refval('disable');
            });
        }
        function EnableFields(FormName, EnabledFieldNames, EnabledComboboxNames, EnabledRefvalNames) {
            $.each(EnabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', false);
            });
            $.each(EnabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('enable');
            });
            $.each(EnabledRefvalNames, function (index, value) {
                $(FormName + value).refval('enable');
            });
        }
        function RedFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' })
            });
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Normal_SalesAbnormalApply.SalesAbnormalApply" runat="server" AutoApply="True"
                DataMember="SalesAbnormalApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyNO" Editor="text" FieldName="ApplyNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="AbnormalType" Editor="numberbox" FieldName="AbnormalType" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesTypeID" Editor="text" FieldName="SalesTypeID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Contact" Editor="text" FieldName="Contact" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TaxNO" Editor="text" FieldName="TaxNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesTax" Editor="numberbox" FieldName="SalesTax" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesTotal" Editor="numberbox" FieldName="SalesTotal" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesAmount" Editor="numberbox" FieldName="SalesAmount" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AbnormalReasonID" Editor="text" FieldName="AbnormalReasonID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="AbnormalReason" Editor="text" FieldName="AbnormalReason" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="DealTypeID" Editor="numberbox" FieldName="DealTypeID" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DealType" Editor="text" FieldName="DealType" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="WarrantAmount" Editor="numberbox" FieldName="WarrantAmount" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="IsWarrant" Editor="text" FieldName="IsWarrant" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="銷貨異常單" Width="800px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="SalesAbnormalApply" HorizontalColumnsCount="4" RemoteName="sERP_Normal_SalesAbnormalApply.SalesAbnormalApply" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ParentObjectID="" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dataFormMaster_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="異常單號" Editor="text" FieldName="ApplyNO" Format="" maxlength="0" Width="120" ReadOnly="True" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="inforefval" FieldName="SalesNO" Format="" maxlength="0" Width="120" EditorOptions="title:'銷貨單號',panelWidth:350,remoteName:'sERP_Normal_SalesAbnormalApply.SalesNO',tableName:'SalesNO',columns:[],columnMatches:[],whereItems:[],valueField:'SalesNO',textField:'SalesNO',valueFieldCaption:'銷貨單號',textFieldCaption:'銷貨單號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:SalesNO_OnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" ReadOnly="False" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERP_Normal_SalesAbnormalApply.Users',tableName:'Users',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERP_Normal_SalesAbnormalApply.SYS_ORG',tableName:'SYS_ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異常屬性" Editor="infocombobox" FieldName="AbnormalType" Format="" Width="120" EditorOptions="valueField:'SalesItemID',textField:'SalesItemName',remoteName:'sERP_Normal_SalesAbnormalApply.SalesItem1',tableName:'SalesItem1',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:SalesItemIDOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalesInvoices.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶" Editor="infocombobox" FieldName="CustomerID" Format="" maxlength="0" Width="120" ReadOnly="True" EditorOptions="valueField:'CustomerID',textField:'ShortName',remoteName:'sERP_Normal_SalesAbnormalApply.Customer',tableName:'Customer',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據號碼" Editor="text" FieldName="InvoiceNO" Format="" maxlength="0" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="Contact" Format="" maxlength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="統一編號" Editor="text" FieldName="TaxNO" Format="" maxlength="0" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨業務" Editor="infocombobox" FieldName="SalesID" Format="" maxlength="0" Width="120" ReadOnly="True" EditorOptions="valueField:'SalesID',textField:'SalesName',remoteName:'sERPSalesInvoices.SalesPerson',tableName:'SalesPerson',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額" Editor="text" FieldName="SumAmount" Width="120" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據金額" Editor="text" FieldName="SalesAmount" Format="" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據稅額" Editor="text" FieldName="SalesTax" Format="" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據總額" Editor="text" FieldName="SalesTotal" Format="" Width="120" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異常原因" Editor="infocombobox" FieldName="AbnormalReasonID" Format="" maxlength="0" Width="662" EditorOptions="valueField:'SalesExceptionID',textField:'SalesExceptionName',remoteName:'sERP_Normal_SalesAbnormalApply.SalesException',tableName:'SalesException',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:SalesExceptionIDOnSelect,panelHeight:200" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="原因說明" Editor="textarea" FieldName="AbnormalReason" Format="" Width="660" maxlength="0" Span="4" EditorOptions="height:60" />
                        <JQTools:JQFormColumn Alignment="left" Caption="處理方式" Editor="infocombobox" FieldName="DealTypeID" Format="" Width="120" EditorOptions="valueField:'ExceptDealTypeID',textField:'ExceptDealTypeName',remoteName:'sERP_Normal_SalesAbnormalApply.SalesExceptDealType',tableName:'SalesExceptDealType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:ExceptDealTypeIDOnSelect,panelHeight:200" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="方式說明" Editor="text" FieldName="DealType" Format="" maxlength="0" Width="480" NewRow="False" Span="3" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖銷金額" Editor="numberbox" FieldName="WarrantAmount" Format="" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="自動沖銷" Editor="checkbox" FieldName="IsWarrant" Format="" Width="120" EditorOptions="on:1,off:0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖銷欄位" Editor="infocombobox" EditorOptions="items:[{value:'',text:'--請選擇--',selected:'false'},{value:'Oth',text:'其他',selected:'false'},{value:'Reb',text:'折讓',selected:'false'},{value:'Ret',text:'退貨',selected:'false'},{value:'Bad',text:'呆帳',selected:'false'}],checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="WarrantField" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="ApplyNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsWarrant" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesNO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AbnormalType" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
