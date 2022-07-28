<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ERP_Normal_InvoiceVoidApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $('#dataFormMasterInvoiceNO').closest('td').append("&nbsp;<font color='blue'>若找無號碼，表示此發票收據已被沖款，請洽會計室</font>");
                $('#dataFormMasterVoidNotes').closest('td').append("&nbsp;<font color='blue'>限定20個字以內</font>");
                $('#dataFormMasterIsVoidSalesMaster').closest('td').append("&nbsp;<font color='blue'>此欄位由會計室來勾選</font>");
            });
        });
        //取作廢申請單號
        function GetInvoiceVoidNO() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_InvoiceVoidApply.InvoiceVoidApply',
                data: "mode=method&method=" + "GetInvoiceVoidNO" + "&parameters=" + UserID,
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
        //設定申請者組織編號=使用者組織編號，設定Org_NOParent=使用者主管的組織編號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sERP_Normal_InvoiceVoidApply.InvoiceVoidApply',
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
        //取代理人s
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "發票收據作廢申請";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sInvoiceVoidApply.ERPInvoiceVoidApplyMaster',
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

        function dataFormMaster_OnLoadSuccess() {
            var parameters = Request.getQueryStringByName("step");
            //var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //載入時，申請者組織編號=使用者組織編號，Org_NOParent=使用者主管的組織編號，申請者=使用者(defaultMaster裡有設)
                GetUserOrgNOs();
                //載入時，申請者combo過濾為代理人s
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " UserID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);

                
                $("#dataFormMasterInvoiceNO").combobox('setValue', '');
            }

            //控制"作廢銷貨"顯示與否
            if (parameters == "account") {
                $("#dataFormMasterIsVoidSalesMaster").closest('td').prev('td').show();
                $("#dataFormMasterIsVoidSalesMaster").closest('td').show();

                //停用
                var DisabledFieldName = ['VoidNotes'];
                var DisabledComboboxName = ['ApplyDate','ApplyEmpID', 'ApplyOrg_NO', 'InvoiceNO'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
            } else {
                $("#dataFormMasterIsVoidSalesMaster").closest('td').prev('td').hide();
                $("#dataFormMasterIsVoidSalesMaster").closest('td').hide();
            }



            var SalesNO = $("#dataFormMasterSalesNO").val();
            var setWhereStr = "SalesNO=" + "'" + SalesNO + "'";
            setTimeout(function () {
                $("#dataGridSalesDetails").datagrid('setWhere', setWhereStr);
            },1000);
        }

        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }

        //申請者
        function ApplyEmpID_OnSelect(rowdata) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowdata.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowdata.OrgNOParent);
        }
        //單據號碼
        function InVoiceNO_Onselect(rowdata) {
            $("#dataFormMasterCustomerID").val(rowdata.CustomerID);
            $("#dataFormMasterShortName").val(rowdata.ShortName);
            $("#dataFormMasterSalesAmount").val(rowdata.SalesAmount);
            $("#dataFormMasterSalesTax").val(rowdata.SalesTax);
            $("#dataFormMasterSalesTotal").val(rowdata.SalesTotal);
            $("#dataFormMasterSalesNO").val(rowdata.SalesNO);
            $("#dataFormMasterQInvoiceType").combobox('setValue', rowdata.QInvoiceType);

            //帶明細
            var setWhereStr = "SalesNO=" + "'" + rowdata.SalesNO + "'";
            $("#dataGridSalesDetails").datagrid('setWhere', setWhereStr);
        }
        function dataFormMaster_OnApply() {
            if ($("#dataFormMasterVoidNotes").val().length > 20) {
                alert("「作廢事由」限定20個字以內");
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERP_Normal_InvoiceVoidApply.InvoiceVoidApply" runat="server" AutoApply="True"
                DataMember="InvoiceVoidApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceVoidNO" Editor="text" FieldName="InvoiceVoidNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoidNotes" Editor="text" FieldName="VoidNotes" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CustomerID" Editor="text" FieldName="CustomerID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="InvoiceNO" Editor="text" FieldName="InvoiceNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesAmount" Editor="numberbox" FieldName="SalesAmount" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesTax" Editor="numberbox" FieldName="SalesTax" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="SalesTotal" Editor="numberbox" FieldName="SalesTotal" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesNO" Editor="text" FieldName="SalesNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="QInvoiceType" Editor="text" FieldName="QInvoiceType" Format="" MaxLength="0" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="發票收據作廢申請單" Width="800px" DialogLeft="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="InvoiceVoidApply" HorizontalColumnsCount="4" RemoteName="sERP_Normal_InvoiceVoidApply.InvoiceVoidApply" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dataFormMaster_OnApply" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="作廢申請單號" Editor="text" FieldName="InvoiceVoidNO" Format="" maxlength="0" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="0" Width="110" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERP_Normal_InvoiceVoidApply.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:ApplyEmpID_OnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="110" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sERP_Normal_InvoiceVoidApply.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Width="110" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據號碼" Editor="infocombobox" FieldName="InvoiceNO" Format="" maxlength="0" Width="110" EditorOptions="valueField:'InvoiceNO',textField:'InvoiceNO',remoteName:'sERP_Normal_InvoiceVoidApply.InvoiceDetails',tableName:'InvoiceDetails',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:InVoiceNO_Onselect,panelHeight:200" Span="4" />
                        <JQTools:JQFormColumn Alignment="left" Caption="作廢事由" Editor="text" FieldName="VoidNotes" Format="" maxlength="0" NewRow="True" Span="4" Width="420" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustomerID" Format="" maxlength="0" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶簡稱" Editor="text" FieldName="ShortName" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票金額" Editor="numberbox" FieldName="SalesAmount" Format="" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票稅額" Editor="numberbox" FieldName="SalesTax" Format="" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發票總額" Editor="numberbox" FieldName="SalesTotal" Format="" maxlength="0" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨單號" Editor="text" FieldName="SalesNO" Format="" maxlength="0" Width="110" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單據類別" Editor="infocombobox" EditorOptions="valueField:'INVOICETYPEID',textField:'INVOICETYPENAME',remoteName:'sERP_Normal_InvoiceVoidApply.QInvoiceType',tableName:'QInvoiceType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="QInvoiceType" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="作廢銷貨" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsVoidSalesMaster" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                        <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="InvoiceVoidNO" RemoteMethod="False" DefaultMethod="GetInvoiceVoidNO" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoidNotes" RemoteMethod="False" ValidateType="None" CheckMethod="" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyDate" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyEmpID" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyOrg_NO" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="InvoiceNO" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDataGrid ID="dataGridSalesDetails" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SalesDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERP_Normal_InvoiceVoidApply.SalesDetails" RowNumbers="True" Title="銷貨單明細" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" EditDialogID="JQDialog2">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Normal_InvoiceVoidApply.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨品名" Editor="text" FieldName="SalesTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="180">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨數量" Editor="text" FieldName="Quantity" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨單價" Editor="text" FieldName="UnitPrice" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銷貨金額" Editor="text" FieldName="Amount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                        </JQTools:JQGridColumn>
                    </Columns>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        </div>
        <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormSalesDetails" Title="銷貨明細">
            <JQTools:JQDataForm ID="dataFormSalesDetails" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="SalesDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ParentObjectID="" RemoteName="sERP_Normal_InvoiceVoidApply.SalesDetails" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銷貨類別" Editor="infocombobox" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERP_Normal_InvoiceVoidApply.SalesType',tableName:'SalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SalesTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銷貨品名" Editor="text" FieldName="SalesTypeName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="180" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銷貨數量" Editor="text" FieldName="Quantity" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銷貨單價" Editor="text" FieldName="UnitPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                    <JQTools:JQFormColumn Alignment="left" Caption="銷貨金額" Editor="text" FieldName="Amount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                </Columns>
            </JQTools:JQDataForm>
        </JQTools:JQDialog>
    </form>
</body>
</html>
