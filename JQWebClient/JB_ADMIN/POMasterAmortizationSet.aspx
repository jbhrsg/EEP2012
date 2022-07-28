<%@ Page Language="C#" AutoEventWireup="true" CodeFile="POMasterAmortizationSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script> 
        //========================================= ready ====================================================================================
        $(document).ready(function () {
          
            //判斷無殘值的checkbox勾選與否
            $("#dataFormMasterbScrapValue").click(function () {                                              
                OnblurScrapValue();
            });

        });

        //-------------------------------------------------------------------------------------
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var CompanyID = $("#CompanyID_Query").combobox('getValue');//公司別
                var AssetName = $("#AssetName_Query").val();//資產名稱,攤銷細項
                var PONO = $("#PONO_Query").refval('getValue');//請購單號	
                var AssetID = $("#AssetID_Query").refval('getValue');//	資產編號	
                var Status = $("#Status_Query").combobox('getValue');//有無攤銷完成=>0未完成,1已完成

                if (CompanyID != '') result.push("c.CompanyID = '" + CompanyID + "'");
                if (AssetName != '') result.push("(AssetName like '%" + AssetName + "%' or AssetDetail like '%" + AssetName + "%')");
                if (PONO != '') result.push("PONO = '" + PONO + "'");
                if (AssetID != '') result.push("AssetID = '" + AssetID + "'");
                if (Status == 2) result.push("RemainAmt>=0");
                if (Status == 0) result.push("RemainAmt!=0");
                if (Status == 1) result.push("RemainAmt=0");
                $(dg).datagrid('setWhere', result.join(' and '));
            }
            
        }
        //計算預留殘值(預留殘值=取得原價/(耐用年限+1))
        function OnblurScrapValue() {
            var AssetGetAmt = $("#dataFormMasterAssetGetAmt").val();//原價
            var UsefulYears = parseInt($("#dataFormMasterUsefulYears").val()) + 1;//耐用年限

            //根據是否有勾選無殘值來判斷預留殘值
            var ScrapValue; //----預留殘值
            if ($("#dataFormMasterbScrapValue").is(":checked") == true) {
                ScrapValue = 0;
            } else {
                ScrapValue = Math.round(AssetGetAmt / UsefulYears);// ...原價/耐用年限+1=預留殘值	
            }
            $("#dataFormMasterScrapValue").numberbox('setValue', ScrapValue);
            $("#dataFormMasterScrapValue").val(ScrapValue);

            //原價減殘值
            var NowValue = AssetGetAmt - ScrapValue;
            $("#dataFormMasterNowValue").numberbox('setValue', NowValue);
            $("#dataFormMasterNowValue").val(NowValue);

            //計算提列數(取得原價-預留殘值/耐用年限/12)
            var MentionAmt = Math.round(NowValue / parseInt($("#dataFormMasterUsefulYears").val()) / 12);
            $("#dataFormMasterMentionAmt").numberbox('setValue', MentionAmt);
            $("#dataFormMasterMentionAmt").val(MentionAmt);
        }
        //傳票總額連結
        function glAmtLink(value, row, index) {
            //if (value != 0)
            return "<a href='javascript: void(0)' onclick='LinkglAmt(" + index + ");'>" + value + "</a>";
            //else return value;
        }

        // open傳票總額 dialog
        function LinkglAmt(index) {
            $("#dataGridView").datagrid('selectRow', index);

            var rows = $("#dataGridView").datagrid('getSelected');
            var AutoKey = rows.AutoKey;
            $("#dataGridDetail").datagrid('setWhere', "v.MAutoKey = " + AutoKey);
            //$("#JQDialog2").dialog("open");

            openForm('#JQDialog2', {}, 'inserted', 'dialog');

        }
        //------------------------------------dataForm-------------------------------------------------
        //新增前檢查
        function OnApplyDFMaster() {
            //檢查借、貸方科目開頭要是公司別
            var CompanyID = $('#dataFormMasterCompanyID').combobox('getValue');//公司別
            var BorrowNo = $("#dataFormMasterBorrowNo").refval('getValue').substring(0, 1);
            var LendNo = $("#dataFormMasterLendNo").refval('getValue').substring(0, 1);
            if (CompanyID != BorrowNo) {
                alert("借方科目有誤");
                return false
            }
            if (CompanyID != LendNo) {
                alert("貸方科目有誤");
                return false
            }
        }
        //修改後更新
        function OnAppliedDFMaster() {
            $('#dataGridView').datagrid('reload');
        }                     
        function GetMAutoKey() {
            var rows = $("#dataGridView").datagrid('getSelected');
            return rows.AutoKey;
        }
        //借方、貸方科目帶入公司別
        function OnSelectCompanyID() {
            var CompanyID = $('#dataFormMasterCompanyID').combobox('getValue');//公司別
            $("#dataFormMasterBorrowNo").refval('setValue', CompanyID);
            $("#dataFormMasterLendNo").refval('setValue', CompanyID);
        }
        


        //-----------------------------------------新增傳票-----------------------------------------------------------
        //  OnLoad DataFormDetail 過濾公司別&已在名單中
        function OnLoadDFDetail() {
            var rows = $("#dataGridView").datagrid('getSelected');
            $('#dataFormDetailVoucherNo').refval('setWhere', "CompanyID=" + rows.CompanyID + " and VoucherNo not in (select VoucherNo from POMasterAmortizationV where MAutoKey=" + rows.AutoKey + " and IsActive=1)");
        }
        // 新增傳票前檢查
        function OnApplyDFDetail() {
            var Check = $("#dataFormDetailVoucherNo").refval('getValue');
            if (Check == "" || Check == undefined) {
                alert('傳票未選擇！');
                return false;
            }
        }
        // 新增傳票後
        function OnAppliedDFDetail() {
            $('#dataGridView').datagrid('reload');
        }
        //-----------------------------------------失效項目-----------------------------------------------------------
        //刪除=>失效
        function OnDeleteDG() {
            var pre = confirm("確認失效此筆?");
            if (pre == true) {
                var AutoKey = $('#dataGridDetail').datagrid('getSelected').AutoKey;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sPOMasterAmortization.POMasterAmortization', //連接的Server端，command
                    data: "mode=method&method=" + "UpdatePOMasterAmortizationVIsActive" + "&parameters=" + AutoKey, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                    cache: false,
                    async: true,
                });
            }
            return false; //取消失效的動作
            $('#dataGridDetail').datagrid('reload');
        }

    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPOMasterAmortization.POMasterAmortization" runat="server" AutoApply="True"
                DataMember="POMasterAmortization" Pagination="True" QueryTitle="查詢" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="110px" QueryMode="Panel" QueryTop="50px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="攤銷編號" Editor="numberbox" FieldName="AutoKey" Format="" Visible="True" Width="55" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" Format="" MaxLength="0" Visible="true" Width="140" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="取得日期" Editor="text" FieldName="AssetGetDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="取得原價A" Editor="numberbox" FieldName="AssetGetAmt" Format="N" Visible="true" Width="70" />
                    <JQTools:JQGridColumn Alignment="right" Caption="預留殘值" Editor="numberbox" FieldName="ScrapValue" Format="N" Visible="true" Width="67" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="原價減殘值" Editor="numberbox" FieldName="NowValue" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="耐用年限" Editor="numberbox" FieldName="UsefulYears" Format="" Visible="true" Width="55" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="right" Caption="已攤金額" Editor="numberbox" FieldName="DeductionAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="57" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="傳票總額" Editor="text" FieldName="glAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="67" FormatScript="glAmtLink" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="提列數" Editor="numberbox" FieldName="MentionAmt" Format="N" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="累計提列數B" Editor="text" FieldName="sumAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="未折減餘額A-B" Editor="numberbox" FieldName="RemainAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="86" EditorOptions="" Format="N">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="成本中心" DrillObjectID="" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterID',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="53">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="借方科目" Editor="text" FieldName="BorrowNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="69">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="貸方科目" Editor="text" FieldName="LendNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="69">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="請購單號" Editor="text" FieldName="PONO" Format="" MaxLength="0" Visible="true" Width="65" Sortable="True" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="資產單號" Editor="text" FieldName="AssetID" Format="" MaxLength="0" Visible="true" Width="65" Sortable="True" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增攤銷項目" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" Enabled="True" />
<%--                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="資產名稱 / 攤銷細項" Condition="%" DataType="string" Editor="text" FieldName="AssetName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="115" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="攤銷完成狀態" Condition="%" DataType="string" Editor="infocombobox" FieldName="Status" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" EditorOptions="items:[{value:'2',text:'不拘',selected:'true'},{value:'0',text:'未完成',selected:'false'},{value:'1',text:'已完成',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="請購單號" Condition="%" DataType="string" Editor="inforefval" EditorOptions="title:'選擇請購單',panelWidth:430,remoteName:'sPOMasterAmortization.infoPOMaster',tableName:'infoPOMaster',columns:[{field:'PONO',title:'請購單號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'Description',title:'請購說明',width:160,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'PurPrice',title:'交貨總額',width:90,align:'right',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'PONO',value:'PONO'}],whereItems:[],valueField:'PONO',textField:'PONO',valueFieldCaption:'PONO',textFieldCaption:'PONO',cacheRelationText:false,checkData:false,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="PONO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="130" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="資產編號" Condition="%" DataType="string" Editor="inforefval" EditorOptions="title:'選擇資產',panelWidth:400,remoteName:'sPOMasterAmortization.infoAssetMaster',tableName:'infoAssetMaster',columns:[{field:'AssetID',title:'資產編號',width:90,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AssetName',title:'資產名稱',width:180,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AssetSpecs',title:'規格',width:110,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'AssetID',value:'AssetID'}],whereItems:[],valueField:'AssetID',textField:'AssetID',valueFieldCaption:'AssetID',textFieldCaption:'AssetID',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="AssetID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="170" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" DialogLeft="100px" DialogTop="40px" Title="攤銷項目維護" Width="850px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="POMasterAmortization" HorizontalColumnsCount="6" RemoteName="sPOMasterAmortization.POMasterAmortization" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApplied="OnAppliedDFMaster" OnApply="OnApplyDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="PONO" Editor="text" FieldName="PONO" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetID" Editor="text" FieldName="AssetID" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sPOMasterAmortization.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:OnSelectCompanyID,panelHeight:200" FieldName="CompanyID" Format="" Width="150" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infocombobox" EditorOptions="valueField:'VoucherID',textField:'VoucherTypeName',remoteName:'sglVoucherMaster.infoglVoucherType2',tableName:'infoglVoucherType2',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VoucherID" MaxLength="0" Width="130" Span="2" Visible="True" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="210" />
                        <JQTools:JQFormColumn Alignment="center" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Format="" NewRow="False" Width="95" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="數量" Editor="numberbox" FieldName="AssetQty" Format="" Width="40" maxlength="0" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="text" FieldName="AssetUnit" Format="" maxlength="0" NewRow="False" Width="40" Visible="True" />

                        <JQTools:JQFormColumn Alignment="right" Caption="取得原價" Editor="numberbox" EditorOptions="" FieldName="AssetGetAmt" Format="" NewRow="False" OnBlur="OnblurScrapValue" Width="80" />
                        <JQTools:JQFormColumn Alignment="right" Caption="耐用年限" Editor="numberbox" FieldName="UsefulYears" Format="" NewRow="False" OnBlur="OnblurScrapValue" Width="52" MaxLength="0" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="right" Caption="已攤金額" Editor="numberbox" EditorOptions="" FieldName="DeductionAmt" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="無殘值" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="bScrapValue" MaxLength="0" NewRow="False" OnBlur="" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="借方科目" Editor="inforefval" EditorOptions="title:'請選擇科目',panelWidth:390,remoteName:'sPOMasterAmortization.infoglAccountItem',tableName:'infoglAccountItem',columns:[{field:'CAcnoSubAcno',title:'科目代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AcnoName',title:'科目名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CAcnoSubAcno',textField:'AcnoName',valueFieldCaption:'AcnoSubAcno',textFieldCaption:'AcnoName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="BorrowNo" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="left" Caption="貸方科目" Editor="inforefval" EditorOptions="title:'請選擇科目',panelWidth:390,remoteName:'sPOMasterAmortization.infoglAccountItem',tableName:'infoglAccountItem',columns:[{field:'CAcnoSubAcno',title:'科目代號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'AcnoName',title:'科目名稱',width:280,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'CAcnoSubAcno',textField:'AcnoName',valueFieldCaption:'AcnoSubAcno',textFieldCaption:'AcnoName',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="LendNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="3" Visible="True" Width="220" />
                        <JQTools:JQFormColumn Alignment="right" Caption="預留殘值" Editor="numberbox" EditorOptions="" FieldName="ScrapValue" Format="" maxlength="0" NewRow="True" ReadOnly="True" RowSpan="1" Span="2" Visible="True" Width="80" OnBlur="OnblurScrapValue" />
                        <JQTools:JQFormColumn Alignment="right" Caption="原價減殘值" Editor="numberbox" EditorOptions="" FieldName="NowValue" maxlength="0" ReadOnly="True" Visible="True" Width="80" Span="2" />
                        <JQTools:JQFormColumn Alignment="right" Caption="提列數" Editor="numberbox" EditorOptions="" FieldName="MentionAmt" ReadOnly="True" Span="2" Visible="True" Width="52" />
                        <JQTools:JQFormColumn Alignment="left" Caption="攤銷細項" Editor="textarea" EditorOptions="height:60" FieldName="AssetDetail" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="650" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetName" RemoteMethod="True" ValidateMessage="資產名稱不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetQty" RemoteMethod="True" ValidateMessage="數量不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetUnit" RemoteMethod="True" ValidateMessage="單位不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetGetDate" RemoteMethod="True" ValidateMessage="取得日期不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetGetAmt" RemoteMethod="True" ValidateMessage="取得原價不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="MentionAmt" RemoteMethod="True" ValidateMessage="提列數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ScrapValue" RemoteMethod="True" ValidateMessage="預留殘值不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="UsefulYears" RemoteMethod="True" ValidateMessage="耐用年限不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="DeductionAmt" RemoteMethod="True" ValidateMessage="已攤金額不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowNo" RemoteMethod="True" ValidateMessage="請選擇借方科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="LendNo" RemoteMethod="True" ValidateMessage="請選擇貸方科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateMessage="請選擇傳票類別！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" CarryOn="False" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" CarryOn="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="1" FieldName="CompanyID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AssetQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="式" FieldName="AssetUnit" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="VoucherID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" FieldName="BorrowNo" RemoteMethod="True" DefaultValue="1" />
                        <JQTools:JQDefaultColumn DefaultMethod="" FieldName="LendNo" RemoteMethod="True" DefaultValue="1" />
                    </Columns>
                </JQTools:JQDefault>
            </JQTools:JQDialog>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Closed="True" DialogLeft="140px" EditMode="Dialog" Title="傳票資訊" Width="375px" ShowSubmitDiv="False">
                    <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="False" AlwaysClose="False" AutoApply="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="POMasterAmortizationV" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog3" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False"  PageList="10,20,30,40,50" PageSize="10" Pagination="True" ParentObjectID="dataGridView" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPOMasterAmortization.POMasterAmortization" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnDelete="OnDeleteDG">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="傳票編號" Editor="text" FieldName="VoucherNoShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="text" FieldName="VoucherDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="95">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="right" Caption="總金額" Editor="numberbox" FieldName="Amt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100" Total="sum">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                            <JQTools:JQGridColumn Alignment="left" Caption="MAutoKey" Editor="text" FieldName="MAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                            <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        </TooItems>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="MAutoKey" ParentFieldName="AutoKey" />
                        </RelationColumns>
                    </JQTools:JQDataGrid>
                    <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="dataFormDetail" Width="390px" DialogLeft="160px" DialogTop="130px" Title="新增傳票" ShowModal="True" ShowSubmitDiv="True">
                        <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="POMasterAmortizationV" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="5" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="OnLoadDFDetail" ParentObjectID="dataGridDetail" RemoteName="sPOMasterAmortization.POMasterAmortization" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnApplied="OnAppliedDFDetail">
                            <Columns>
                                <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="MAutoKey" Editor="numberbox" FieldName="MAutoKey" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                                <JQTools:JQFormColumn Alignment="left" Caption="傳票編號" Editor="inforefval" EditorOptions="title:'選擇傳票',panelWidth:750,remoteName:'sPOMasterAmortization.infoglVoucher',tableName:'infoglVoucher',columns:[{field:'VoucherNoShow',title:'傳票編號',width:110,align:'center',table:'',isNvarChar:false,queryCondition:''},{field:'VoucherDate',title:'傳票日期',width:87,align:'left',table:'',isNvarChar:false,queryCondition:'',formatter:formatValue,format:'yyyy/mm/dd'},{field:'Amt',title:'總金額',width:86,align:'right',table:'',isNvarChar:false,queryCondition:''},{field:'sDescribe',title:'借方摘要',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'sDescribe2',title:'貸方摘要',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'VoucherNo',value:'VoucherNo'}],whereItems:[],valueField:'VoucherNo',textField:'VoucherNo',valueFieldCaption:'VoucherNo',textFieldCaption:'VoucherNo',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="VoucherNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                            </RelationColumns>
                        </JQTools:JQDataForm>
                        <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AutoKey" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" CarryOn="False" />
                                <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                                <JQTools:JQDefaultColumn CarryOn="False" FieldName="MAutoKey" RemoteMethod="False" DefaultMethod="GetMAutoKey" />
                            </Columns>
                        </JQTools:JQDefault>
                        <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                            <Columns>
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowLendType" RemoteMethod="True" ValidateMessage="請選擇借貸！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckMethod="CheckSubAcno" CheckNull="False" FieldName="SubAcno" RemoteMethod="False" ValidateMessage="請選擇明細！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                                <JQTools:JQValidateColumn CheckNull="True" FieldName="AmtShow" RemoteMethod="True" ValidateMessage="請填寫金額！" ValidateType="None" />
                            </Columns>
                        </JQTools:JQValidate>
                    </JQTools:JQDialog>
                </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
