<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_CashTakeBackApply.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var _BalanceAmount = 0;
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterIsUrgentPay', '#dataFormMasterIsNotPayDate'];
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
        })
        function dataFormMaster_OnLoadSuccess() {
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                GetUserOrgNOs();
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterApplyEmpID').combobox('setWhere', whereStr);
            }
        }
        function dataFormDetail_OnLoadSuccess(){
                var FormName = '#dataFormDetail';
                var HideFieldName = ['ShortTermNO'];
                var ShowFieldName = ['ShortTermNO'];
                var AgainBillType = $('#dataFormMasterAgainBillType').combobox('getValue')
                //人工沖銷時,隱藏暫借單號
                if (AgainBillType == 5) {
                    $.each(HideFieldName, function (index, fieldName) {
                        $(FormName + fieldName).closest('td').prev('td').hide();
                        $(FormName + fieldName).closest('td').hide();
                    });
                }
                else {
                    $.each(ShowFieldName, function (index, fieldName) {
                        $(FormName + fieldName).closest('td').prev('td').show();
                        $(FormName + fieldName).closest('td').show();
                    });
                }
                $("#dataFormDetailShortTermGist").focus();

            }

        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCashTakeBackMaster.CashTakeBackMaster',
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
            })
        };
        function GetEmpFlowAgentList() {
            var UserID = getClientInfo("UserID");
            var Flow = "現金繳回申請";
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sCashTakeBackMaster.CashTakeBackMaster', //連接的Server端，command
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
        function ShortTermNOOnSelect(rowData) {
            _BalanceAmount = rowData.BalanceAmount
            $("#dataFormMasterCompanyID").val(rowData.CompanyID);
            $("#dataFormDetailShortTermGist").val(rowData.SHORTTERMGIST);
            $("#dataFormDetailAmount").numberbox('setValue', _BalanceAmount);
            
        }
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        function dataGridDetailOnInsert() {
            var ApplyNotes = $("#dataFormMasterApplyNotes").val();
            if (ApplyNotes == "" || ApplyNotes == undefined) {
                alert('注意!!,申請事由不可為空白,請填入!!');
                return false;
            }
        }
        function dataFormMasterOnApply() {
            var TotalAmount = $("#dataFormMasterTotalAmount").val();
            if (TotalAmount == 0 || TotalAmount == undefined) {
                alert('注意!!,未新增明細資料,無法存檔送出,請新增!!');
                return false;
            }

        }
        function dataFormDetailOnApply() {
            var AgainBillType = $('#dataFormMasterAgainBillType').combobox('getValue');
            var ShortermNO = $('#dataFormDetailShortTermNO').refval('getValue');
            if (AgainBillType == 1) {
                if (ShortermNO == '' || ShortermNO == undefined) {
                    alert('注意!!請選擇填入借款單號!!');
                    return false;
                }
                var Amount = $("#dataFormDetailAmount").val();
                if (Amount > _BalanceAmount) {
                    alert('注意!!還款金額大於暫借餘額' + _BalanceAmount + ', 請修正');
                    return false;
                }
            }
            var CashTakeBackType = $('#dataFormDetailCashTakeBackType').combobox('getValue');
            if (CashTakeBackType == '' || CashTakeBackType == undefined) {
                alert('注意!!請選擇填入現金來源!!');
                return false;
            }

        }
        function SumTotalAmount(rowData) {
            $("#dataFormMasterTotalAmount").val(rowData.Amount);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sCashTakeBackMaster.CashTakeBackMaster" runat="server" AutoApply="True"
                DataMember="CashTakeBackMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="CashTakeBackNO" Editor="text" FieldName="CashTakeBackNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyNotes" Editor="text" FieldName="ApplyNotes" Format="" MaxLength="0" Visible="true" Width="115" />
                    <JQTools:JQGridColumn Alignment="right" Caption="對沖表單" Editor="infocombobox" FieldName="AgainBillType" Format="" Visible="true" Width="120" EditorOptions="valueField:'AgainBillType',textField:'AgainBillName',remoteName:'sCashTakeBackMaster.CashAgainBillType',tableName:'CashAgainBillType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CashType" Editor="text" FieldName="CashType" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="現金匯款繳回申請" DialogLeft="10px" DialogTop="10px" Width="770px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="CashTakeBackMaster" HorizontalColumnsCount="3" RemoteName="sCashTakeBackMaster.CashTakeBackMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnCancel="CloseDataForm" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnApply="dataFormMasterOnApply" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="繳回單號" Editor="text" FieldName="CashTakeBackNO" Format="" maxlength="0" Width="90" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="沖銷表單" Editor="infocombobox" EditorOptions="valueField:'AgainBillType',textField:'AgainBillName',remoteName:'sCashTakeBackMaster.CashAgainBillType',tableName:'CashAgainBillType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AgainBillType" Format="" ReadOnly="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="right" Caption="繳回總金額" Editor="text" FieldName="TotalAmount" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="115" Format="N0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請員工" Editor="infocombobox" FieldName="ApplyEmpID" Format="" maxlength="0" Width="120" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sCashTakeBackMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="ApplyOrg_NO" Format="" Width="120" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sCashTakeBackMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" maxlength="0" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="textarea" FieldName="ApplyNotes" Format="" Width="480" EditorOptions="height:24" Span="3" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Visible="False" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" ReadOnly="False" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="CashTakeBackDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sCashTakeBackMaster.CashTakeBackMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="總金額:" UpdateCommandVisible="True" ViewCommandVisible="True" Width="700px" OnInsert="dataGridDetailOnInsert" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="35" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="暫借單號" Editor="text" FieldName="ShortTermNO" Format="" Width="70" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="暫借事由" Editor="text" FieldName="ShortTermGist" Visible="True" Width="220" />
                        <JQTools:JQGridColumn Alignment="left" Caption="現金來源" Editor="infocombobox" EditorOptions="valueField:'CashTakeBackType',textField:'CashTakeBackName',remoteName:'sCashTakeBackMaster.CashTakeBackType',tableName:'CashTakeBackType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CashTakeBackType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="幣別" Editor="infocombobox" EditorOptions="valueField:'CurrencyType',textField:'CurrencyType',remoteName:'sCashTakeBackMaster.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Currency" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                        <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="Amount" Format="N0" Width="65" Total="sum" OnTotal="SumTotalAmount" />
                        <JQTools:JQGridColumn Alignment="left" Caption="現金匯款" Editor="infocombobox" EditorOptions="items:[{value:'現金',text:'現金',selected:'false'},{value:'匯款',text:'匯款',selected:'false'},{value:'授扣',text:'授扣',selected:'false'},{value:'支票',text:'支票',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CashType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="70" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="100" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CashTakeBackNO" Editor="text" FieldName="CashTakeBackNO" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="CashTakeBackNO" ParentFieldName="CashTakeBackNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="10px" DialogTop="50px" Title="還款明細" Width="630px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="CashTakeBackDetails" HorizontalColumnsCount="1" RemoteName="sCashTakeBackMaster.CashTakeBackMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormDetailOnApply" OnLoadSuccess="dataFormDetail_OnLoadSuccess" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="30" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="暫借單號" Editor="inforefval" FieldName="ShortTermNO" Format="" Width="95" EditorOptions="title:'單據查詢',panelWidth:620,remoteName:'sCashTakeBackMaster.AgainstBill',tableName:'AgainstBill',columns:[{field:'SHORTTERMNO',title:'單據號碼',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'SHORTTERMGIST',title:'申請事由',width:260,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'SHORTTERMAMOUNT',title:'暫借金額',width:60,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AgainstAmount',title:'已冲金額',width:60,align:'right',table:'',isNvarChar:false,queryCondition:''},{field:'BalanceAmount',title:'暫借餘額',width:60,align:'right',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'SHORTTERMNO',textField:'SHORTTERMNO',valueFieldCaption:'SHORTTERMNO',textFieldCaption:'SHORTTERMNO',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,onSelect:ShortTermNOOnSelect,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                            <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="ShortTermGist" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="355" />
                            <JQTools:JQFormColumn Alignment="left" Caption="現金來源" Editor="infocombobox" EditorOptions="valueField:'CashTakeBackType',textField:'CashTakeBackName',remoteName:'sCashTakeBackMaster.CashTakeBackType',tableName:'CashTakeBackType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CashTakeBackType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="現金/匯款/支票" Editor="infocombobox" EditorOptions="items:[{value:'現金',text:'現金',selected:'false'},{value:'匯款',text:'匯款',selected:'false'},{value:'授扣',text:'授扣',selected:'false'},{value:'支票',text:'支票',selected:'false'},{value:'應收折讓',text:'應收折讓',selected:'false'},{value:'呆帳',text:'呆帳',selected:'false'},{value:'請款單',text:'請款單',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CashType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="幣別" Editor="infocombobox" EditorOptions="valueField:'CurrencyType',textField:'CurrencyType',remoteName:'sCashTakeBackMaster.Currency',tableName:'Currency',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Currency" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="Amount" Format="" Width="77" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Width="60" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="100" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CashTakeBackNO" Editor="text" FieldName="CashTakeBackNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="CashTakeBackNO" ParentFieldName="CashTakeBackNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="CashTakeBackNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="現金" FieldName="CashType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AgainBillType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="TotalAmount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="9" FieldName="CompanyID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyNotes" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Amount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="TWD" FieldName="Currency" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="現金" FieldName="CashType" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Amount" RangeFrom="1" RangeTo="9999999" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemNO" NumDig="3" />
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
