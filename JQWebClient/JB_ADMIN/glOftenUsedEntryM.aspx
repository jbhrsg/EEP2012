<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glOftenUsedEntryM.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
    <script> 
        //========================================= ready ====================================================================================

        var sCompanyID = "";   
        var waitA = false;

        $(document).ready(function () {            
            //傳回登入者目前設定的公司別
            var UserID=getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherMaster', //連接的Server端，command
                data: "mode=method&method=" + "getglVoucherSet" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        sCompanyID=rows[0].CompanyID;                       
                    }
                }
            });

                       
        });               

        //========================================= 常用分錄列表 ====================================================================================
        function OnLoadSuccessGV() {
            var dgid = $(this);
            //第一次載入
            if (!dgid.data('firstLoad') && dgid.data('firstLoad', true)) {

                //panel寬度調整
                var dgid = $('#dataGridView');
                var queryPanel = getInfolightOption(dgid).queryDialog;
                if (queryPanel)
                    $(queryPanel).panel('resize', { width: 500 });              

                //設定傳回目前的公司別              
                $("#CompanyID_Query").combobox('setValue', sCompanyID);

                query('#dataGridView');

            }

            //ControlGrid();//Grid 匯出顯示方式的顯示或隱藏


        }
        //========================================= 公司別 & 科目 連動Combobox ====================================================================================   
        //主檔的 公司別 有變動時      
        //---------------------------------------選公司別觸發---------------------------------
        var CompanyID_OnSelect = function (rowdata) {
            //影響
            GetAcno("");//科目
            RunGetSubAcno();//明細           
        }

        function RunGetSubAcno() {
            //若DataFormDetails不為viewed狀態,則重跑
            if (getEditMode($("#dataFormDetail")) != 'viewed') {
                var Acno = $("#dataFormDetailAcno").combobox('getValue');
                GetSubAcno(Acno, "");
            }
        }
        //========================================= 科目 1.明細 2.摘碼代號 連動Combobox ====================================================================================   

        //---------------------------------------選取科目觸發---------------------------------
        var Acno_OnSelect = function (rowdata) {
            $("#dataFormDetailDescribe").val("");
            ClearAcnoCombo();
            //1.明細
            GetSubAcno(rowdata.value, "");
            //2.摘碼代號
            GetDescribeID(rowdata.value, "");
        }

        //========================================= 摘碼代號 => 得到內容 ====================================================================================   
        //摘碼代號 => 得到內容
        function GetDescribeText(CompanyID, DetailAcno, DescribeID) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherDetails', //連接的Server端，command
                data: "mode=method&method=" + "GetDescribeText" + "&parameters=" + CompanyID + "," + DetailAcno + "," + DescribeID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#dataFormDetailDescribe').val(rows[0].Describe);
                    }
                }
            });
        }

        var OnLoadSuccessDFDetail = function (rowdata) {

            //DataFormDetails 資料編輯時
            if (getEditMode($("#dataFormDetail")) == 'updated') {
                GetAcno(rowdata.Acno);
                GetSubAcno(rowdata.Acno, rowdata.SubAcno);
                GetDescribeID(rowdata.Acno, rowdata.DescribeID);
            }
            $("#dataFormDetailBorrowLendType").combo('textbox').focus();//焦點

            //================================== combo blur 事件 ====================================       

            //combo blur 事件  =>   科目
            $("#dataFormDetailAcno").combo('textbox').blur(function () {
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                //1.得到明細
                GetSubAcno(DetailAcno, "");
                //2.摘碼代號
                GetDescribeID(DetailAcno, "");
            });

            //combo blur 事件  =>   摘碼代號
            $("#dataFormDetailDescribeID").combo('textbox').blur(function () {

                var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                var DescribeID = $("#dataFormDetailDescribeID").combobox('getValue');//摘碼代號

                //得到內容
                GetDescribeText(CompanyID, DetailAcno, DescribeID);
            });            

        }
        function ClearAcnoCombo() {
                      
            //2.明細 清空
            $("#dataFormDetailSubAcno").combobox('loadData', []).combobox('clear');
            //3.摘碼代號 清空
            $("#dataFormDetailDescribeID").combobox('loadData', []).combobox('clear');
        }
        //得到科目資料
        var GetAcno = function (Acno) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetAcno', { Company_ID: CompanyID, Ac_no: Acno});
            if (CodeList != null) {
                $("#dataFormMasterAcno").combobox('loadData', CodeList);//Master
                $("#dataFormDetailAcno").combobox('loadData', CodeList);//Detail
            }
        }
        //得到明細資料
        var GetSubAcno = function (Acno, SubAcno) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetSubAcno', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (CodeList != null) $("#dataFormDetailSubAcno").combobox('loadData', CodeList);
        }
        //得到摘碼代號
        var GetDescribeID = function (Acno, DescribeID) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetDescribeID', { Company_ID: CompanyID, Ac_no: Acno, Describe_ID: DescribeID });
            if (CodeList != null) $("#dataFormDetailDescribeID").combobox('loadData', CodeList);
        }
        //---------------------------------------呼叫Method---------------------------------------
        var GetDataFromMethod = function (methodName, data) {
            var returnValue = null;
            $.ajax({
                url: '../handler/JqDataHandle.ashx?RemoteName=sglVoucherMaster',
                data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                type: 'POST',
                async: false,
                success: function (data) { returnValue = $.parseJSON(data); },
                error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
            });
            return returnValue;
        };
      
        //========================================= DataFormMaster ====================================================================================        
        function OnLoadSuccessDFMaster() {
           
            setTimeout(function () {
                insertItem('#dataGridDetail');
            }, 500);

            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                
                //設定傳回目前的公司別               
                $("#dataFormMasterCompanyID").combobox('setValue', sCompanyID);
               
                GetSubAcno("0", "");//新增時預設
            } 
            var Acno = $("#dataFormMasterAcno").combobox('getValue');
            GetAcno(Acno);

            //$("#JQType").options('setValue', "1");
            //$('#dataGridDetail').datagrid('getPanel').show();
            //$('#dataGridDetail2').datagrid('getPanel').hide();
           

        }                          

        function OnAppliedDFMaster() {
            $("#dataGridView").datagrid("reload");
        }

        //存檔前檢查 OnSubmited
        function OnApplyDFMaster() {
            //檢查 dataGridDetail 的 公司別 是否有跑掉
            //公司別 
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var rows = $('#dataGridDetail').datagrid('getRows');            
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].CompanyID != CompanyID) {
                    alert("公司別不一致！");
                    return false;
                }
            }
            //檢查明細
            var data = $("#dataGridDetail").datagrid("getRows");
            if (data.length == 0) {
                alert('無分錄明細。');
                return false;
            }
            queryGrid('#dataGridView');//按查詢

        }
        //========================================= Grid Master刪除事件 ====================================================================================              
        //判斷
        function OnDeleteGV() {
            //var VoucherNo = $("#dataGridView").datagrid('getSelected').VoucherNo;//取得當前主檔中選中的那個Data

            ////檢查若沒有glVoucherDetails,則可以刪除
            //var cnt = GetDataFromMethod('GetglVoucherDetailsCount', { Voucher_No: VoucherNo});
            //if ((cnt == "0") || (cnt == "undefined")) {
            //    return true;
            //}
            //else {
            //    alert('還有科目明細,無法刪除!!');
            //    return false;
            //}
        }
        //========================================= GridDetails ====================================================================================

        //新增前的檢查
        function OnInsertDetail() {
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            if (CompanyID == "") {
                alert('請選擇公司別!');
                return false;
            }
            //var Acno = $("#dataFormMasterAcno").combobox('getValue');
            //if (Acno == "") {
            //    alert('請選擇科目!');
            //    return false;
            //}
            //var UsedName = $("#dataFormMasterUsedName").val();
            //if (UsedName == "") {
            //    alert('分類名稱不可空白!');
            //    return false;
            //}
        }
        //========================================= DataFormDetails ====================================================================================              
        //將摘碼代號所選帶入摘碼內容
        function OnSelectDescribeID(rowData) {
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');//公司別
            var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
            var DescribeID = $("#dataFormDetailDescribeID").combobox('getValue');//摘碼代號
            //得到內容
            GetDescribeText(CompanyID, DetailAcno, DescribeID);
        }
        //明細	驗證 => 金額需>0
        function CheckMethodAmt(val) {
            if (val<=0) {
                return false;
            } else return true;//通過
        }
        //明細	驗證 =>可能selected Value 為空白=> 判斷文字
        function CheckSubAcno() {
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getText');
            if (SubAcno == "" || SubAcno == "---請選擇---") {
                alert('請選擇明細!');
                return false;
            } else return true;//通過
        }
        //DataFormDetails存檔前檢查
        function OnApplyDFDetail() {
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getText');
            //選取時將文字帶入DataForm的文字欄位=>之後Grid用
            //明細
            $("#dataFormDetailSubAcnoText").val(SubAcno);           

            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            $("#dataFormDetailCompanyID").val(CompanyID);
                        
            var Acno = $("#dataFormDetailAcno").combobox('getValue');
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');
            if (Acno != "") {
                //1.是否要成本中心=>由Acno,SubAcno推 
                var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
                var CostCenterID = $("#dataFormDetailCostCenterID").combobox('getValue');

                if (bCostCenterID == "True" && CostCenterID == "") {
                    alert('此科目需成本中心-請選擇成本中心!');
                    return false;
                }
                //2.新增明細時檢查  => 科目+明細檢查       
                //公司別
                var iCount = GetDataFromMethod('GetDetailData', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
                if (iCount == 0) {
                    alert("科目或明細資料不存在！");
                    return false;
                }
            }
        }
        //複制分錄
        function CopyDetail() {
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有可選取分錄資料,本功能無法使用');
                return false;
            }
            var aNewObj = {};
            var row = $('#dataGridDetail').datagrid('getSelected');            
            $.extend(aNewObj, row);//複製結構與資料

            //取目前編號
            var Item = 0;
            var iAutoKey = 0;
            var rows = $('#dataGridDetail').datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                if (parseInt(rows[i].Item) > Item) {
                    Item = rows[i].Item;
                }
                if (parseInt(rows[i].iAutoKey) > iAutoKey) {
                    iAutoKey = rows[i].iAutoKey;
                }
            }
            aNewObj.iAutoKey = iAutoKey + 1;
            aNewObj.Item = parseInt(Item) + 1;           
            
            $('#dataGridDetail').datagrid('appendRow', aNewObj);
           
        }
        
      


    </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglOftenUsedEntryM.glOftenUsedEntryM" runat="server" AutoApply="True"
                DataMember="glOftenUsedEntryM" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="常用分錄列表" QueryMode="Panel" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDelete="OnDeleteGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="numberbox" FieldName="CompanyName" Format="" Visible="true" Width="150" />
                    <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" FieldName="Acno" MaxLength="0" Visible="true" Width="80" />
                    <JQTools:JQGridColumn Alignment="center" Caption="科目名稱" Editor="text" FieldName="AcnoName" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="分類名稱 " Editor="text" FieldName="UsedName" Format="" Visible="true" Width="250" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="UserID" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />                   
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="150" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="科目" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'Acno',textField:'Acno',remoteName:'sglVoucherMaster.infoAcno',tableName:'infoAcno',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Acno" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="分類名稱" Condition="%" DataType="string" Editor="text" FieldName="UsedName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="2" Width="150" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="常用分錄維護" DialogLeft="50px" DialogTop="30px" Width="850px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glOftenUsedEntryM" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglOftenUsedEntryM.glOftenUsedEntryM" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFMaster" OnApply="OnApplyDFMaster" OnApplied="OnAppliedDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Format="" Width="100" ReadOnly="True" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" Format="" Width="150" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" FieldName="Acno" Format="" Width="100" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" MaxLength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分類名稱 " Editor="text" EditorOptions="" FieldName="UsedName" Width="290" Format="" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <div id="tt" class="easyui-tabs" style="width:auto;height:auto;">
                <div title="一般" style="padding:20px;">
                        <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="glOftenUsedEntryD" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" Height="240px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnInsert="OnInsertDetail" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglOftenUsedEntryM.glOftenUsedEntryM" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="iAutoKey" Editor="text" FieldName="iAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" EditorOptions="" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="text" EditorOptions="" FieldName="SubAcnoText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="text" EditorOptions="" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="255">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyDetail" Text="複製" Visible="True" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                </div>
                <div title="匯出" data-options="closable:true" style="overflow:auto;padding:20px;">
                        <JQTools:JQDataGrid ID="dataGridDetail2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="glOftenUsedEntryD" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="240px" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglOftenUsedEntryM.glOftenUsedEntryM" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="left" Caption="iAutoKey" Editor="text" FieldName="iAutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="text" FieldName="VoucherDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="76">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="text" EditorOptions="" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" EditorOptions="" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="text" EditorOptions="" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="成本中心" Editor="text" EditorOptions="" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="330">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="text" FieldName="Amt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                                </JQTools:JQGridColumn>
                            </Columns>
                            <RelationColumns>
                                <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                            </RelationColumns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-print" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出EXCEL" />
                            </TooItems>
                        </JQTools:JQDataGrid>
                </div>
                </div>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Continue" Width="750px">
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="3" />
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="glOftenUsedEntryD" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="OnApplyDFDetail" OnLoadSuccess="OnLoadSuccessDFDetail" ParentObjectID="dataFormMaster" RemoteName="sglOftenUsedEntryM.glOftenUsedEntryM" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="iAutoKey" Editor="text" FieldName="iAutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListID',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Acno_OnSelect,panelHeight:200" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SubAcnoText" Editor="text" FieldName="SubAcnoText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                            <JQTools:JQFormColumn Alignment="left" Caption="摘碼代號" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDescribeID,panelHeight:200" FieldName="DescribeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" />
                            <JQTools:JQFormColumn Alignment="left" Caption="內容" Editor="text" FieldName="Describe" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="270" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="AutoKey" ParentFieldName="AutoKey" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <%--<JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="dataFormDetail" FieldName="iAutoKey" NumDig="0" />--%>
                    <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="dataFormDetail" FieldName="iAutoKey" NumDig="0" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="UsedName" RemoteMethod="True" ValidateMessage="分類名稱不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
<%--                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="iAutoKey" RemoteMethod="True" />--%>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="BorrowLendType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="SubAcno" RemoteMethod="False" ValidateMessage="請選擇明細！" ValidateType="None" CheckMethod="CheckSubAcno" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
