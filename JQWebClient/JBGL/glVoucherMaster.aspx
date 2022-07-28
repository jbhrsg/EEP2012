<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glVoucherMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
    <script> 
        //========================================= ready ====================================================================================

        var sCompanyID = "";
        var sVoucherID = "";

        $(document).ready(function () {
            
            //傳回登入者目前設定的公司別、傳票類別
            var UserID = getClientInfo("UserID");
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
                        sVoucherID = rows[0].VoucherID;
                    }
                }
            });
                          

        });
        //========================================= 傳票列表 ====================================================================================
        function OnLoadSuccessGV() {
            var dgid = $(this);
            //第一次載入
            if (!dgid.data('firstLoad') && dgid.data('firstLoad', true)) {

                //panel寬度調整
                var dgid = $('#dataGridView');
                var queryPanel = getInfolightOption(dgid).queryDialog;
                if (queryPanel)
                    $(queryPanel).panel('resize', { width: 500 });

                var dt = new Date();
                var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                $("#VoucherDate_Query").datebox('setValue', today);//預設傳票日期	 

                //設定傳回目前的公司別、傳票類別               
                $("#CompanyID_Query").combobox('setValue', sCompanyID);
                $("#VoucherID_Query").options('setValue', sVoucherID);

                query('#dataGridView');

            }
        }
        
        //========================================= GridDetails ====================================================================================

        //GridDetails選擇
        function OnSelectDetail() {
            ////Detail dataform 設定為 修改模式
            //setTimeout(function () {
            //    updateItem('#dataGridDetail');
            //}, 400);
        }

       //新增前的檢查
        function OnInsertDetail() {
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            if (CompanyID == "") {
                alert('請選擇公司別!');
                return false;
            }
            var VoucherID = $("#dataFormMasterVoucherID").options('getCheckedValue');
            if (VoucherID == "") {
                alert('請選擇傳票類別!');
                return false;
            }
        }
        //========================================= 公司別 & 科目 連動Combobox ====================================================================================   
        //主檔的 公司別 有變動時      
        //---------------------------------------選公司別觸發---------------------------------
        var CompanyID_OnSelect = function (rowdata) {
            //影響
            GetAcno("");//科目
            RunGetSubAcno();//明細           
        }
        //得到科目資料
        var GetAcno = function (Acno) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetAcno', { Company_ID: CompanyID, Ac_no: Acno });
            if (CodeList != null) {
                $("#dataFormDetailAcno").combobox('loadData', CodeList);//Detail
            }
        }
        function RunGetSubAcno() {
            //若DataFormDetails不為viewed狀態,則重跑
            if (getEditMode($("#dataFormDetail")) != 'viewed') {
                var Acno = $("#dataFormDetailAcno").combobox('getValue');
                GetSubAcno(Acno, "");
            }
        }
        //得到明細資料
        var GetSubAcno = function (Acno, SubAcno) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetSubAcno', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (CodeList != null) $("#dataFormDetailSubAcno").combobox('loadData', CodeList);
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
      
        function ClearAcnoCombo() {
            //1.明細 清空
            $("#dataFormDetailSubAcno").combobox('loadData', []).combobox('clear');
            //2.摘碼代號 清空
            $("#dataFormDetailDescribeID").combobox('loadData', []).combobox('clear');
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

        //========================================= 公司別,傳票類別 => 常用分錄 連動Combobox ====================================================================================   

        //---------------------------------------選取分錄科目觸發---------------------------------
        var UsedAcno_OnSelect = function (rowdata) {
            ClearOftenUsedCombo();
            GetrOftenUsed(rowdata.value, "");
        }
        function ClearOftenUsedCombo() {
            //常用分錄 清空
            $("#dataFormMasterOftenUsedEntryID").combobox('loadData', []).combobox('clear');
        }
        //得到常用分錄資料
        var GetrOftenUsed = function (Acno, OftenUsedEntryID) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetrOftenUsed', { Company_ID: CompanyID, Ac_no: Acno, OftenUsedEntry_ID: OftenUsedEntryID });
            if (CodeList != null) $("#dataFormMasterOftenUsedEntryID").combobox('loadData', CodeList);
        }        
        //========================================= DataFormMaster ====================================================================================        
        function OnLoadSuccessDFMaster() {
          
            if (getEditMode($("#dataFormMaster")) == 'viewed') {
                $('#dataFormDetail').hide();
            } else $('#dataFormDetail').show();

            //Detail dataform 設定為 新增模式
            setTimeout(function () {
                insertItem('#dataGridDetail');
            }, 500);
            

            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //傳票日期disable属性删除
                $("#dataFormMasterVoucherDate").combo('textbox').removeAttr("disabled");
                $("#dataFormMasterVoucherDate").datebox().removeAttr("disabled");
                //預設傳票日期
                var dt = new Date();
                var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                $("#dataFormMasterVoucherDate").datebox('setValue', today);

                //設定傳回目前的公司別、傳票類別               
                $("#dataFormMasterCompanyID").combobox('setValue', sCompanyID);
                $("#dataFormMasterVoucherID").options('setValue', sVoucherID);

                //得到常用分錄資料
                GetOftenAcno("0");
                GetrOftenUsed("0", "");

                //Detail dataform
                GetAcno("");
                GetSubAcno("0", "");//新增時預設
            } else {
                //傳票日期不可編輯
                $("#dataFormMasterVoucherDate").datebox("disable");

                //帶出常用分錄
                //分錄科目
                var UsedAcno = $("#dataFormMasterOftenUsedAcno").combobox('getValue');
                GetOftenAcno(UsedAcno);
                //常用分錄
                var UsedEntryID = $("#dataFormMasterOftenUsedEntryID").combobox('getValue');                
                GetrOftenUsed(UsedAcno, UsedEntryID);
            }
            
           
        }       
        //得到分錄科目=>包含請選擇
        var GetOftenAcno = function (Acno) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var CodeList = GetDataFromMethod('GetAcnoAll', { Company_ID: CompanyID, Ac_no: Acno });
            if (CodeList != null) {
                $("#dataFormMasterOftenUsedAcno").combobox('loadData', CodeList);
            }
        }
        //主檔的 公司別 或 傳票類別 有變動時
        function OnSelectCompanyID(rowdata) {
            RunGetSubAcno();//科目            
            GetrOftenUsed(rowdata.OftenUsedAcno, rowdata.OftenUsedEntryID);//常用分錄

        }

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
        
        function OnAppliedDFMaster() {
            $("#dataGridView").datagrid("reload");
        }
        //========================================= 選擇常用分錄 =>帶出傳票資訊====================================================================================              

        function OnSelectOftenUsedEntryID(rowData) {
            //傳票類別	
            var VoucherID = $("#dataFormMasterVoucherID").options('getCheckedValue');

            var OftenUsedEntryID = rowData.value;
            var dataGrid = $('#dataGridDetail');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherDetails', //連接的Server端，command
                data: "mode=method&method=" + "BindOftenUsedEntry" + "&parameters=" + OftenUsedEntryID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows!=null && rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];
                        var UserName = getClientInfo("UserName");
                        var today = getClientInfo('_today')
                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ VoucherNo: '自動編號', CompanyID: data[j].CompanyID, VoucherID: VoucherID, Item: data[j].Item, BorrowLendType: data[j].BorrowLendType, Acno: data[j].Acno, SubAcnoText: data[j].SubAcnoText, SubAcno: data[j].SubAcno, CostCenterID: data[j].CostCenterID, DescribeID: data[j].DescribeID, Describe: data[j].Describe, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today });
                        }
                        for (var i = 0; i < appandRows.length; i++) {
                            dataGrid.datagrid("appendRow", appandRows[i]);
                        }
                        //griddetail的footer強制更新
                        setFooter(dataGrid);
                    } else dataGrid.datagrid('loadData', []);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });
        }

        //========================================= DataFormDetails ====================================================================================              
        var OnLoadSuccessDFDetail = function (rowdata) {
            //DataFormDetails 資料編輯時
            if (getEditMode($("#dataFormDetail")) == 'updated') {
                GetAcno(rowdata.Acno);
                GetSubAcno(rowdata.Acno, rowdata.SubAcno);
                GetDescribeID(rowdata.Acno, rowdata.DescribeID);
            }
            if (getEditMode($("#dataFormDetail")) == 'inserted') {
                $("#dataFormDetailBorrowLendType").combo('textbox').focus();//焦點
                $("#dataFormDetailBorrowLendType").combobox('setValue', "");
            }
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
        function OnSelectSubAcno(rowData) {
            //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用            
            $("#dataFormDetailSubAcnoText").val(rowData.text);
        }

        //DataFormDetails存檔前檢查
        function OnApplyDFDetail() {
            //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用
            var SubAcnoValue = $("#dataFormDetailSubAcno").combobox('getValue');
            var SubAcnoText = $("#dataFormDetailSubAcno").combobox('getText');
           

            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            $("#dataFormDetailCompanyID").val(CompanyID);
            //傳票類別	
            var VoucherID = $("#dataFormMasterVoucherID").options('getCheckedValue');
            $("#dataFormDetailVoucherID").val(VoucherID);
            //1.明細必選檢查
            //if (SubAcno == "" || SubAcno == "---請選擇---") {
            //    alert('請選擇明細!');
            //    return false;
            //}
            //2.是否要成本中心=>由Acno,SubAcno推 
            var Acno = $("#dataFormDetailAcno").combobox('getValue');
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');
            var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            var CostCenterID = $("#dataFormDetailCostCenterID").combobox('getValue');

            if (bCostCenterID == "True" && CostCenterID == "") {
                alert('此科目需成本中心-請選擇成本中心!');
                return false;
            }
            //3.傳票日期必選檢查            
            var VoucherDate=$("#dataFormMasterVoucherDate").datebox('getValue');
            if (VoucherDate == "") {
                alert('請選擇傳票日期!');
                return false;
            }
            //4.新增明細時檢查  => 科目+明細檢查       
            //公司別
            var iCount = GetDataFromMethod('GetDetailData', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (iCount == 0) {
                alert("科目或明細資料不存在！");
                return false;
            }
            
        }


        //========================================= Grid Master刪除事件 ====================================================================================              
        //判斷是否已鎖檔
        function OnDeleteGV() {
            var VoucherDate = $("#dataGridView").datagrid('getSelected').VoucherDate;//取得當前主檔中選中的那個Data
            var CompanyID = $("#dataGridView").datagrid('getSelected').CompanyID;

            //檢查若沒有glVoucherDetails,則可以刪除
            var cnt = GetDataFromMethod('CheckDeleteglVoucherMaster', { Company_ID: CompanyID, Voucher_Date: VoucherDate });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                alert('此年月已鎖檔,無法動作!!');
                return false;
            }
        }
        //========================================= 存檔前檢查 ====================================================================================              
        //存檔前檢查 OnSubmited
        function OnApplyDFMaster() {
            //1.檢查 dataGridDetail 的 公司別 是否有跑掉
            //公司別 
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');

            //2.借貸方金額要平衡 借+貸=0 BorrowLendType 1=>借 , 2=>貸           
            var borrow = 0;//借金額
            var lend = 0;//貸金額
            var rows = $('#dataGridDetail').datagrid('getRows');            
            for (var i = 0; i < rows.length; i++) {                
                if (rows[i].CompanyID != CompanyID) {
                    alert("傳票資料有誤:公司別不一致！");
                    return false;
                }
                if (rows[i].BorrowLendType == 1) {
                    borrow = parseInt(borrow) + parseInt(rows[i].AmtShow);
                } else {
                    lend = parseInt(lend) + parseInt(rows[i].AmtShow);
                }
            }
            if (rows.length> 0 && borrow == 0) {
                alert("借:" + borrow + ",貸:" + lend + " 借貸有問題！");
                return false;
            }
            if (borrow != lend) {
                alert("借:"+borrow +",貸:"+lend+" 總金額不平衡！");
                return false;
            }
           
            //3.傳票日期檢查=>是否已鎖檔
            var VoucherDate = $('#dataFormMasterVoucherDate').datebox('getValue')
            //檢查若沒有glVoucherDetails,則可以刪除
            var cnt = GetDataFromMethod('CheckDeleteglVoucherMaster', { Company_ID: CompanyID, Voucher_Date: VoucherDate });
            if ((cnt == "0") || (cnt == "undefined")) {
                return true;
            }
            else {
                alert('此年月已鎖檔,無法新增!!');
                return false;
            }
            ////更新
            //$('#dataGridView').datagrid('reload');
            queryGrid('#dataGridView');//按查詢

        }
      
        //========================================= 傳票列印 ====================================================================================              
        function VoucherPrint() {
            var CompanyID = $("#dataGridView").datagrid('getSelected').CompanyID;
            var VoucherID = "";
            var JQDate1 = $("#dataGridView").datagrid('getSelected').VoucherDate;
            var JQDate2 = $("#dataGridView").datagrid('getSelected').VoucherDate;
            var Acno1 = "";
            var Acno2 = "";
            var SubAcno1 = "";
            var SubAcno2 = "";
            var VoucherNo = $("#dataGridView").datagrid('getSelected').VoucherNo;
            var CostCenterID = "";
            var iType = "0";//呈現種類	0轉帳傳票 1傳票清單 2日記帳

            //報表用參數
            var TypeText = "轉帳傳票";//呈現種類	1傳票清單 
            var CompanyText = "1";

            var url = "../JBGL/REPORT/RglVoucherListReportView.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
                "&VoucherNo=" + VoucherNo + "&CostCenterID=" + CostCenterID + "&Acno1=" + Acno1 + "&Acno2=" + Acno2 + "&SubAcno1=" + SubAcno1 + "&SubAcno2=" + SubAcno2 +
                "&iType=" + iType + "&TypeText=" + TypeText + "&CompanyText=" + CompanyText;

            var height = $(window).height() - 20;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 20;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "Report",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');

        }
        



    </script> 
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglVoucherMaster.glVoucherMaster" runat="server" AutoApply="True"
                DataMember="glVoucherMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="傳票列表" QueryMode="Panel" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="OnDeleteGV" OnUpdate="OnDeleteGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="numberbox" FieldName="CompanyName" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票類別" Editor="text" FieldName="VoucherTypeName" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票編號" Editor="text" FieldName="VoucherNo" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="借方總額" Editor="numberbox" FieldName="SumBorrow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="貸方總額" Editor="numberbox" FieldName="SumLend" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />                   
                    <JQTools:JQToolItem Enabled="True" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="VoucherPrint" Text="列印" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票類別" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="VoucherID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="VoucherDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票編號" Condition="%" DataType="string" Editor="text" FieldName="VoucherNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="傳票維護" DialogLeft="20px" DialogTop="5px" Width="870px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherMaster.glVoucherMaster" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFMaster" OnApply="OnApplyDFMaster" OnApplied="OnAppliedDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票編號" Editor="text" FieldName="VoucherNo" Format="" Width="100" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" MaxLength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" Format="" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infooptions" FieldName="VoucherID" Format="" Width="180" EditorOptions="title:'JQOptions',panelWidth:260,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" MaxLength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分錄科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:UsedAcno_OnSelect,panelHeight:200" FieldName="OftenUsedAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="常用分錄" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectOftenUsedEntryID,panelHeight:200" FieldName="OftenUsedEntryID" MaxLength="0" Width="290" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="glVoucherDetails" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglVoucherMaster.glVoucherMaster" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" EditDialogID="JQDialog2" OnInsert="OnInsertDetail" ParentObjectID="dataFormMaster" OnSelect="OnSelectDetail" Height="240px" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="infocombobox" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="text" EditorOptions="" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="text" EditorOptions="" FieldName="SubAcnoText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="190">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="85">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="text" EditorOptions="" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="197">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="numberbox" FieldName="AmtShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SourseType" Editor="text" FieldName="SourseType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="SubAcno" Editor="text" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                     <RelationColumns>
                         <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                    </RelationColumns>
                     <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />                                       
                </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Continue" Width="750px" Closed="True" Title="">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="glVoucherDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherMaster.glVoucherDetails" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnLoadSuccess="OnLoadSuccessDFDetail" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListID',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="BorrowLendType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Acno_OnSelect,panelHeight:150" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectSubAcno,panelHeight:200" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" OnBlur="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SubAcnoText" Editor="text" FieldName="SubAcnoText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="110" />
                            <JQTools:JQFormColumn Alignment="left" Caption="摘碼代號" Editor="infocombobox" FieldName="DescribeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectDescribeID,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="內容" Editor="text" FieldName="Describe" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="270" />
                            <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="numberbox" FieldName="AmtShow" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="70" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SourseType" Editor="text" FieldName="SourseType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="text" FieldName="LastUpdateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="3" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="VoucherNo" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="VoucherDate" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateMessage="請選擇傳票類別！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherDate" RemoteMethod="True" ValidateMessage="傳票日期	不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="BorrowLendType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowLendType" RemoteMethod="True" ValidateMessage="請選擇借貸！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="SubAcno" RemoteMethod="False" ValidateMessage="請選擇明細！" ValidateType="None" CheckMethod="CheckSubAcno" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AmtShow" RemoteMethod="True" ValidateMessage="請填寫金額！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
