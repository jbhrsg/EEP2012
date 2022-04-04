<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREQ_Requisition.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        function dataFormMaster_OnLoadSuccess() {
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            if (parameters == "SERVICE" && mode == "0") {
                $('#dataFormMasterIsRemit').removeAttr("disabled");
                $("#dataFormMasterRemitType").combobox('enable');  //combobox 設為可用
                $('#dataFormMasterRemit').removeAttr("disabled");
            }
         }
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
            $('#dataFormMasterPlanPayDate').datebox({
                onSelect: function (date) {
                   ChkException();
                }
            }).combo('textbox').blur(function () {
                ChkException();
            });

            //在客戶負責人加入經濟部商業司超連結
            var RequisitKindLink = $("<a>").attr({ 'href': '../JB_ADMIN/Files/委任權限表.pdf' }).attr({ 'target': '_blank' }).text("     請款性質說明");
            var RequistKind = $('#dataFormMasterRequistKindID').closest('td');
            RequistKind.append(RequisitKindLink);

        });
        function checkCombo() {
            var dataFormMasterApplyOrg_NO = $("#dataFormMasterApplyOrg_NO").combobox('getValue');
            if (dataFormMasterApplyOrg_NO == "" || dataFormMasterApplyOrg_NO == undefined) {
                alert('注意!!,未選取申請部門,請選取');
                $("#dataFormMasterApplyOrg_NO").focus();
                return false;
            }
            var dataFormMasterCostCenterID = $("#dataFormMasterCostCenterID").combobox('getValue');
            if (dataFormMasterCostCenterID == "" || dataFormMasterCostCenterID == undefined) {
                alert('注意!!,未選取成本中心,請選取');
                $("#dataFormMasterCostCenterID").focus();
                return false;
            }
            var dataFormMasterRequisitionTypeID = $("#dataFormMasterRequisitionTypeID").combobox('getValue');
            if (dataFormMasterRequisitionTypeID == "" || dataFormMasterRequisitionTypeID == undefined) {
                alert('注意!!,未選取請款依據,請選取');
                $("#dataFormMasterRequisitionTypeID").focus();
                return false;
            }
            var dataFormMasterPayTermID = $("#dataFormMasterPayTermID").combobox('getValue');
            if (dataFormMasterPayTermID == "" || dataFormMasterPayTermID == undefined) {
                alert('注意!!,未選取付款條件,請選取');
                $("#dataFormMasterPayTermID").focus();
                return false;
            }
            var dataFormMasterPayTypeID = $("#dataFormMasterPayTypeID").combobox('getValue');
            if (dataFormMasterPayTypeID == "" || dataFormMasterPayTypeID == undefined) {
                alert('注意!!,未選取付款方式,請選取');
                $("#dataFormMasterPayTypeID").focus();
                return false;
            }
        };
        //檢查是否非期限內付款,緊急付款
        function ChkException() {
            var mess = "";
            var bdt = $('#dataFormMasterPlanPayDate').combo('textbox').val();
            var dt = new Date(bdt);
            var days = dt.getDate()
            if ((days != 5) && (days != 25)) {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 1);
            }
            else {
                $('#dataFormMasterIsNotPayDate').checkbox('setValue', 0);
            }
            var dd = new Date();
            var dc = (dt - dd) / 86400000;
            if (dc.toPrecision() <= 5) {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 1);
               }
            else {
                $('#dataFormMasterIsUrgentPay').checkbox('setValue', 0);
            }
        }
        //當選取客戶時,重新設定付款條件與付款方式並改變預付日期
        function GetPayTerm(rowData) {
            $("#dataFormMasterPayTermID").combobox('setValue', rowData.PayTermID);
            $("#dataFormMasterPayTypeID").combobox('setValue', rowData.PayTypeID);
            var pp = $("#dataFormMasterPayTypeID").combobox('getValue'); //combobox 取值
            if (pp == 2) {
                $("#dataFormMasterIsRemit").checkbox('setValue', rowData.IsRemit);
                $("#dataFormMasterRemitType").combobox('setValue', '廠商付');
                $("#dataFormMasterRemit").val(30); //text給值
               }
              else
               {
                    $("#dataFormMasterIsRemit").checkbox('setValue',0);
                    $("#dataFormMasterRemitType").combobox('setValue', "");
                    $("#dataFormMasterRemit").val(0); //text給值
              }
            var PayTerm = $("#dataFormMasterPayTermID").combobox('getText');
            var WorkDays = 5; //作業最短日期
            var tdate = GetPlanPayDate(PayTerm, WorkDays, $("#dataFormMasterApplyDate").datebox('getValue'));
            $('#dataFormMasterPlanPayDate').datebox('setValue', tdate);
        }
        function CheckRemitType() {
            var kk = $("#dataFormMasterIsRemit").checkbox('getValue')
            if (kk != 1) {
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
            }
            else {
                $("#dataFormMasterRemitType").combobox('setValue', '廠商付');
                $("#dataFormMasterRemit").val(30); //text給值
            }
        }
        //當選取付款方式時,重新設定付款條件與付款方式並改變預付日期
        function GetPayType(rowData) {
            var pp = $("#dataFormMasterPayTypeID").combobox('getValue'); //combobox 取值
            if (pp != 2)
             {
                $("#dataFormMasterIsRemit").checkbox('setValue',0);
                $("#dataFormMasterRemitType").combobox('setValue', "");
                $("#dataFormMasterRemit").val(0); //text給值
              }
        }
        //當選取付款條件時,依付款條件改變預付日期
        function GetPayDateByTerm() {
            var PayTerm = $("#dataFormMasterPayTermID").combobox('getText');
            var tdate = GetPlanPayDate(PayTerm, 5, $("#dataFormMasterApplyDate").datebox('getValue'));
            $('#dataFormMasterPlanPayDate').datebox('setValue', tdate);
        }
       //傳入付款條件,作業天數,申請日期求得預付日期
        function GetPlanPayDate(PayTerm, WorkDays, PlanPayDate) {
            var now = new Date(PlanPayDate);
            if (PayTerm == "當月") {
                var newDate = DateAdd("d ", WorkDays, now).toLocaleDateString();
             }
            else {
                var days = parseInt(PayTerm, 10);
                var newDate = DateAdd("d ", days, now).toLocaleDateString();
             }
            var ttDate = GetPayDate(newDate);
            return ttDate;
        }
        //計算付款日
        function GetPayDate(PayDate) {
            var Dt = new Date(PayDate);
            var year = Dt.getFullYear();
            var month = Dt.getMonth()+1;
            var Bt = Dt.toLocaleDateString();
            var Dt10 = new Date(year + '/' + month + '/' + '10').toLocaleDateString();
            var Dt25 = new Date(year + '/' + month + '/' + '25').toLocaleDateString();
            if (Bt <= Dt10) {
                return Dt10;
            }
            else
                if (Bt <= Dt25) {
                    return Dt25;
                }
                else {
                   return Dt25;
                }
        }
        function DateAdd(interval, number, date) {
            switch (interval) {
                case "y ": {
                    date.setFullYear(date.getFullYear() + number);
                    return date;
                    break;
                }
                case "q ": {
                    date.setMonth(date.getMonth() + number * 3);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMonth(date.getMonth() + number);
                    return date;
                    break;
                }
                case "w ": {
                    date.setDate(date.getDate() + number * 7);
                    return date;
                    break;
                }
                case "d ": {
                    date.setDate(date.getDate() + number);
                    return date;
                    break;
                }
                case "h ": {
                    date.setHours(date.getHours() + number);
                    return date;
                    break;
                }
                case "m ": {
                    date.setMinutes(date.getMinutes() + number);
                    return date;
                    break;
                }
                case "s ": {
                    date.setSeconds(date.getSeconds() + number);
                    return date;
                    break;
                }
                default: {
                    date.setDate(d.getDate() + number);
                    return date;
                    break;
                }
            }
        }
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        //檢查憑證號碼
        function CheckProofNO() {
            var ProofType = $("#dataFormMasterProofTypeID").combobox("getValue");
            var ProofNO = $("#dataFormMasterProofNO").val();
            var ProofNOLen = ProofNO.length;
            if ((ProofType == 1) && (ProofNOLen != 10)) {
                return false;
            }
            return true;
           
        }
     
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sRequisition.Requisition" runat="server" AutoApply="True"
                DataMember="Requisition" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="請款單申請" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNO" Editor="text" FieldName="RequisitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionDescr" Editor="text" FieldName="RequisitionDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="RequisitionTypeID" Editor="numberbox" FieldName="RequisitionTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="ProofTypeID" Editor="numberbox" FieldName="ProofTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTermID" Editor="numberbox" FieldName="PayTermID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTypeID" Editor="numberbox" FieldName="PayTypeID" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RequisitionNotes" Editor="text" FieldName="RequisitionNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayTo" Editor="text" FieldName="PayTo" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PayToNotes" Editor="text" FieldName="PayToNotes" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="numberbox" FieldName="CompanyID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="請款單申請" Width="750px" DialogLeft="30px" DialogTop="50px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Requisition" HorizontalColumnsCount="3" RemoteName="sRequisition.Requisition" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="True" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" Width="720px" OnApply="checkCombo" OnLoadSuccess="dataFormMaster_OnLoadSuccess" OnCancel="CloseDataForm" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="RequisitionNO" Format="" maxlength="0" Width="127"  ReadOnly="True"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" ReadOnly="True" span="1" Visible="True" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款性質" Editor="infocombobox" EditorOptions="valueField:'RequisitKindID',textField:'RequistKindName',remoteName:'sRequisition.RequistKind',tableName:'RequistKind',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="RequistKindID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sRequisition.Company',tableName:'Company',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sRequisition.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sRequisition.CostCenter',tableName:'CostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:250" FieldName="CostCenterID" Format="" maxlength="0" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款事由" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionDescr" Format="" maxlength="0" Span="3" Width="546" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款依據" Editor="infocombobox" EditorOptions="valueField:'RequisitionTypeID',textField:'RequisitionTypeName',remoteName:'sRequisition.RequisitionType',tableName:'RequisitionType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RequisitionTypeID" Format="" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檢附憑證" Editor="infocombobox" EditorOptions="valueField:'ProofTypeID',textField:'ProofTypeName',remoteName:'sRequisition.ProofType',tableName:'ProofType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ProofTypeID" Format="" Width="130" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="憑據號碼" Editor="text" FieldName="ProofNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="130" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款金額" Editor="numberbox" FieldName="RequisitAmt" Format="" Span="1" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="暫借款單" Editor="infocombobox" EditorOptions="valueField:'ShortTermNO',textField:'ShortTermDescr',remoteName:'sRequisition.ShortTerm',tableName:'ShortTerm',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ShortTermNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="344" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請款備註" Editor="textarea" EditorOptions="height:40" FieldName="RequisitionNotes" Format="" maxlength="0" span="3" Width="546" />
                        <JQTools:JQFormColumn Alignment="left" Caption="受款人" Editor="inforefval" EditorOptions="title:'受款人篩選',panelWidth:580,remoteName:'sRequisition.Vendor',tableName:'Vendor',columns:[{field:'VendTypeName',title:'廠商類別',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'VendShortName',title:'供應商簡稱',width:250,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'ContactName',title:'聯絡人',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'VendID',textField:'VendShortName',valueFieldCaption:'VendID',textFieldCaption:'VendShortName',cacheRelationText:false,checkData:false,showValueAndText:false,onSelect:GetPayTerm,selectOnly:true" FieldName="PayTo" Format="" maxlength="0" Width="130" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sRequisition.PayType',tableName:'PayType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:GetPayType,panelHeight:200" FieldName="PayTypeID" Format="" Width="133" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款條件" Editor="infocombobox" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sRequisition.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:GetPayDateByTerm,panelHeight:200" FieldName="PayTermID" Format="" Width="133" />
                        <JQTools:JQFormColumn Alignment="left" Caption="預付日期" Editor="datebox" FieldName="PlanPayDate" Width="133" Format="yyyy/mm/dd"/>
                        <JQTools:JQFormColumn Alignment="left" Caption="緊急付款" Editor="checkbox" FieldName="IsUrgentPay" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="非付款日付款" Editor="checkbox" FieldName="IsNotPayDate" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款備註" Editor="textarea" EditorOptions="height:40" FieldName="PayToNotes" Format="" maxlength="0" Span="3" Width="546" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Visible="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需匯款費" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRemit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" OnBlur="CheckRemitType" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款費付款" Editor="infocombobox" EditorOptions="items:[{value:'廠商付',text:'廠商付',selected:'false'},{value:'公司付',text:'公司付',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RemitType" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="RequisitionNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" FieldName="CreateBy" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="RequistKindID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitAmt" RangeFrom="1" RangeTo="1000000" RemoteMethod="True" ValidateMessage="金額不可小於0" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RequisitionDescr" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayTo" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="CheckProofNO" CheckNull="False" FieldName="ProofNO" RemoteMethod="False" ValidateMessage="發票輸入格式錯誤" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
