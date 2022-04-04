<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glVoucherMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../css/JBGL/label.css" rel="stylesheet" />
    <title></title>
    <script> 
        var sCompanyID = "";
        var sVoucherID = "";

        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';

        //========================================= ready ====================================================================================

        $(document).ready(function () {
            
            //傳票編號串聯
            var VoucherNo1 = $('#VoucherNoShow_Query').closest('td');
            var VoucherNo2 = $('#VoucherNo_Query').closest('td').children();
            VoucherNo1.append(' - ').append(VoucherNo2)        

            //combo blur 事件  =>   傳票編號起帶入訖
            $("#VoucherNoShow_Query").blur(function () {
                if ($("#VoucherNo_Query").val() == "") {
                    $("#VoucherNo_Query").val($("#VoucherNoShow_Query").val());
                }
            });

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

            var UserID = getClientInfo("UserID");
            setTimeout(function () {
                var data = $("#UserID_Query").combobox('getData');
                for (var i = 0; i < data.length; i++) {
                    if (data[i].UserID == UserID) {
                        $("#UserID_Query").combobox('setValue', data[i].UserID);
                    }
                }
            }, 200);

          
            //-----------------------------------讀取ExcelJquery----------------------------------
            $('#Dialog_Import').jbExcelFileImport({
                OnFileUploadSuccess: function () {
                    //開啟配對視窗
                    openForm('#Dialog_ImportMain', {}, 'inserted', 'Dialog');
                    $(this).jbExcelFileImport('changeSheetByName', 'Sheet1');
                },
                OnGetTitleSuccess: function (SheetArray, TitleArray) {

                    //SheetChangge
                    $('#DataForm_SheetImportMainSHEET').combobox('clear').combobox('loadData', SheetArray);

                    //載入選項以及預設
                    $('#DataForm_ImportMain').find('.info-combobox').each(function () {
                        $(this).combobox('loadData', TitleArray).combobox('clear');
                        $(this).combobox('selectExistsForText', $(this).closest('td').prev('td').html());
                    });
                },
                OnImportSuccess: function (jsonStr) {
                    var json = $.parseJSON(jsonStr);
                    if (!json.IsOK) {
                        var showMessage = json.ErrorMsg;
                        if (json.Result) {
                            showMessage += $('<a>', { href: '../handler/JbExcelHandler.ashx?' + $.param({ mode: 'FileDownload', DownloadName: '錯誤資料.xls', FilePathName: json.Result }), target: '_blank' })
                                            .html('檔案下載')[0].outerHTML;
                        }
                        $.messager.alert('匯入失敗', showMessage, 'error');
                    }
                    else {
                        $.messager.alert(' ', "匯入成功");
                        $('#Dialog_Import').dialog('close');
                        $('#Dialog_ImportMain').dialog('close');
                        $('#dataGridView').datagrid('reload');
                    }
                }
            });
            //-----------------------------------欄位配對視窗送出按鈕----------------------------
            $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                if (!$('#DataForm_ImportMain').form('validateForm')) return;            //驗證
                //選擇檢查
                var CompanyID = $("#DataForm_VoucherMasterCompanyID").combobox('getValue');
                var VoucherID = $("#DataForm_VoucherMasterVoucherID").combobox('getValue');

                if (CompanyID != "" && VoucherID != "") {
                    var pre = confirm("確定匯入?");
                    if (pre == true) {

                        var voucherObject = $('#DataForm_VoucherMaster').jbDataFormGetAFormData();   //取資料
                        var titleObject = $('#DataForm_ImportMain').jbDataFormGetAFormData();   //取資料

                        $('#Dialog_Import').jbExcelFileImport('importFile', {
                            remoteName: 'sglVoucherImport',
                            method: 'ExcelFileImport',
                            sheetIndex: $('#DataForm_SheetImportMainSHEET').combobox('getValue'),
                            titleObject: titleObject,
                            parameters: $.toJSONString(voucherObject)
                        });
                    }

                }

            });
            //-------------------------------------------------------------------------------------


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
                    $(queryPanel).panel('resize', { width: 580 });

                //var dt = new Date();
                //var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
                //$("#VoucherDate_Query").datebox('setValue', today);//預設傳票日期	 

                ////設定傳回目前的公司別、傳票類別               
                //$("#CompanyID_Query").combobox('setValue', sCompanyID);
                //$("#VoucherID_Query").options('setValue', sVoucherID);

                //預設建立人員為登入者
                //var sUserID = getClientInfo("UserID");             
                //$("#UserID_Query").combobox('setValue', sUserID);                

                //setTimeout(function () {
                //    query('#dataGridView');
                //}, 200);

            }
        }
        
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var CompanyID = $('#CompanyID_Query').combobox('getValue');//公司別
                var VoucherID = $('#VoucherID_Query').options('getValue');//傳票類別
                var VoucherNoShow = $('#VoucherNoShow_Query').val();//傳票編號1
                var VoucherNoShow2 = $('#VoucherNo_Query').val();//傳票編號2
                var UserID = $('#UserID_Query').combobox('getValue');//	建立人員    
                var VoucherDate = $('#VoucherDate_Query').datebox('getValue');//傳票日期                           
                var CreateDate = $('#CreateDate_Query').datebox('getValue');//建立日期                           

                if (CompanyID != '') result.push("CompanyID = '" + CompanyID + "'");
                if (VoucherID != '') result.push("VoucherID = '" + VoucherID + "'");
                if (VoucherNoShow != '') result.push("VoucherNoShow between '" + VoucherNoShow + "' and '" + VoucherNoShow2+"'");
                if (UserID != '') result.push("UserID = '" + UserID + "'");
                if (VoucherDate != '') result.push("VoucherDate = '" + VoucherDate + "'");
                if (CreateDate != '') result.push("Convert(nvarchar(10),CreateDate,111) = '" + CreateDate + "'");
                $(dg).datagrid('setWhere', result.join(' and '));
            }
        }

        //========================================= GridDetails ====================================================================================

        //GridDetails選擇
        function OnSelectGrid2() {
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
            //$("#dataFormDetailDescribe").val("");
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
            ControlGrid();//Grid key in 顯示方式的顯示或隱藏
        }

        //========================================= Grid key in 顯示方式的顯示或隱藏 ====================================================================================   
        function ControlGrid() {
            ////分錄科目是否選擇 => 0 無選擇 =>Grid Continue
            //var OftenUsedAcno = $("#dataFormMasterOftenUsedAcno").combobox('getValue');
            //if (OftenUsedAcno == "0" || OftenUsedAcno == "") {
            //    //Grid隱藏
            //    $('#dataGridDetail').datagrid('getPanel').show();
            //    $('#JQDialog2').show();

            //    if (getEditMode($("#dataFormMaster")) == 'inserted') {
            //        //Detail dataform 設定為 新增模式
            //        setTimeout(function () {
            //            insertItem('#dataGridDetail');
            //        }, 500);
            //    }

            //    $('#dataGridDetail2').datagrid('getPanel').hide();
            //} else {
            //    $('#dataGridDetail').datagrid('getPanel').hide();
            //    $('#JQDialog2').hide();
            //    $('#dataGridDetail2').datagrid('getPanel').show();
            //}

            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                //Detail dataform 設定為 新增模式
                setTimeout(function () {
                    insertItem('#dataGridDetail');
                }, 500);
                //Grid隱藏
                $('#dataGridDetail').datagrid('getPanel').show();
                $('#JQDialog2').show();
                $('#dataGridDetail2').datagrid('getPanel').hide();
            } else {
                $('#dataGridDetail').datagrid('getPanel').hide();
                $('#JQDialog2').hide();
                $('#dataGridDetail2').datagrid('getPanel').show();
            }
        }

        //========================================= DataFormMaster ====================================================================================        
        function OnLoadSuccessDFMaster() {
          
            if (getEditMode($("#dataFormMaster")) == 'viewed') {
                $('#dataFormDetail').hide();
            } else $('#dataFormDetail').show();              

            if (getEditMode($("#dataFormMaster")) == 'inserted') {

                //combo blur 事件  =>   分錄科目 => 常用分錄
                $("#dataFormMasterOftenUsedAcno").combo('textbox').blur(function () {
                    var OftenUsedAcno = $("#dataFormMasterOftenUsedAcno").combobox('getValue');//科目
                    ClearOftenUsedCombo();
                    GetrOftenUsed(OftenUsedAcno, "");
                });

                //傳票日期disable属性删除
                $("#dataFormMasterVoucherDate").combo('textbox').removeAttr("disabled");
                $("#dataFormMasterVoucherDate").datebox().removeAttr("disabled");
                //常用分錄disable属性删除
                $("#dataFormMasterOftenUsedAcno").combobox().removeAttr("disabled");
                $("#dataFormMasterOftenUsedEntryID").combobox().removeAttr("disabled");
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

                //分錄科目預設清空
                $("#dataFormMasterOftenUsedAcno").combobox('setValue', "");

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

                //常用分錄不可編輯
                $("#dataFormMasterOftenUsedAcno").combobox('disable');
                $("#dataFormMasterOftenUsedEntryID").combobox('disable');

                ////編輯狀態下=> 用Grid Continue模式
                //$('#dataGridDetail').datagrid('getPanel').show();
                //$('#JQDialog2').show();               
                //$('#dataGridDetail2').datagrid('getPanel').hide();
            }

            ControlGrid();//控制Grid key in 顯示方式的顯示或隱藏

           
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
        
        //明細代號 => 得到內容 (顯示在Grid中)
        function GetAcnoNameText(CompanyID, Acno, SubAcno) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sglVoucherMaster.glVoucherDetails', //連接的Server端，command
                data: "mode=method&method=" + "GetAcnoNameText" + "&parameters=" + CompanyID + "," + Acno + "," + SubAcno,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用            
                        $('#dataFormDetailSubAcnoText').val(rows[0].AcnoName);
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
                            appandRows.push({ VoucherNo: '自動編號', CompanyID: data[j].CompanyID, VoucherID: VoucherID, Item: data[j].Item, BorrowLendType: data[j].BorrowLendType, Acno: data[j].Acno, SubAcnoText: data[j].SubAcnoText, SubAcno: data[j].SubAcno, CostCenterID: data[j].CostCenterID, DescribeID: data[j].DescribeID, Describe: data[j].Describe, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today, Amt: 0, AmtShow: 0 });
                            //appandRows.push({ VoucherNo: '自動編號', CompanyID: data[j].CompanyID, VoucherID: VoucherID, Item: data[j].Item, BorrowLendType: data[j].BorrowLendType, SubAcnoText: data[j].SubAcnoText, CostCenterID: data[j].CostCenterID, DescribeID: data[j].DescribeID, Describe: data[j].Describe, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today, Amt: 0, AmtShow: 0 });

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

           

            //combo blur 事件  =>   明細
            $("#dataFormDetailSubAcno").combo('textbox').blur(function () {
                //明細  => 選取時將文字帶入DataForm的文字欄位=>之後Grid用      
                var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');//公司別
                var DetailAcno = $("#dataFormDetailAcno").combobox('getValue');//科目
                var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');//明細
                //得到內容
                GetAcnoNameText(CompanyID, DetailAcno, SubAcno);
                
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
           
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            $("#dataFormDetailCompanyID").val(CompanyID);
            //傳票類別	
            var VoucherID = $("#dataFormMasterVoucherID").options('getCheckedValue');
            $("#dataFormDetailVoucherID").val(VoucherID);

            var Acno = $("#dataFormDetailAcno").combobox('getValue');
            var SubAcno = $("#dataFormDetailSubAcno").combobox('getValue');
            var BorrowLendType = $("#dataFormDetailBorrowLendType").combobox('getValue');
            var Describe=$("#dataFormDetailDescribe").val();
            //1.必選檢查
            if (BorrowLendType == "") {
                alert('請選擇借貸!');
                return false;
            }
            if (SubAcno == "---請選擇---") {
                alert('請選擇明細!');
                return false;
            }
            if (Describe == "") {
                alert('內容不可為空白!');
                return false;
            }
            //2.傳票日期必選檢查            
            var VoucherDate=$("#dataFormMasterVoucherDate").datebox('getValue');
            if (VoucherDate == "") {
                alert('請選擇傳票日期!');
                return false;
            }
            //3.新增明細時檢查  => 科目+明細檢查       
            //公司別
            var iCount = GetDataFromMethod('GetDetailData', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            if (iCount == 0) {
                alert("科目或明細資料不存在！");
                return false;
            }
            //4.是否要成本中心=>由Acno,SubAcno推 
            var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
            var CostCenterID = $("#dataFormDetailCostCenterID").combobox('getValue');
            if (bCostCenterID == "True" && CostCenterID == "") {
                alert('此科目需成本中心-請選擇成本中心!');
                return false;
            }
            
        }


        //========================================= Grid Master刪除事件 ====================================================================================              
        function OnDeleteGV() {
            var VoucherDate = $("#dataGridView").datagrid('getSelected').VoucherDate;//取得當前主檔中選中的那個Data
            var CompanyID = $("#dataGridView").datagrid('getSelected').CompanyID;
            var VoucherNo = $("#dataGridView").datagrid('getSelected').VoucherNo;

            //1.判斷是否已鎖檔
            var cnt = GetDataFromMethod('CheckDeleteglVoucherMaster', { Company_ID: CompanyID, Voucher_Date: VoucherDate });
            if ((cnt == "0") || (cnt == "undefined")) {                
                return true;                
            }
            else {
                alert('此年月已鎖檔,無法動作!!');
                return false;
            }
            
        }
        function OnUpdateGV() {
            var VoucherDate = $("#dataGridView").datagrid('getSelected').VoucherDate;//取得當前主檔中選中的那個Data
            var CompanyID = $("#dataGridView").datagrid('getSelected').CompanyID;
            var VoucherNo = $("#dataGridView").datagrid('getSelected').VoucherNo;

            //1.判斷是否已鎖檔
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

            var GridName = '#dataGridDetail';
            //var OftenUsedEntryID = $("#dataFormMasterOftenUsedEntryID").combobox('getValue');
            //if (OftenUsedEntryID != "" ) {
            //    GridName = '#dataGridDetail2';
            //    //裡面使用beginEdit/endEdit將值存回dataGridDeatil中
            //    endEdit($("#dataGridDetail2"));
            //}
            if (getEditMode($("#dataFormMaster")) != 'inserted') {
                GridName = '#dataGridDetail2';
                //裡面使用beginEdit/endEdit將值存回dataGridDeatil中
                endEdit($("#dataGridDetail2"));
            }


            //1.檢查 dataGridDetail 的 公司別 是否有跑掉
            //公司別 
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            //2.傳票內容檢查
            var rows = $(GridName).datagrid('getRows');

            if (rows.length == 0) {
                alert("無傳票內容！");
                return false;
            }
            //3.借貸方金額要平衡 借+貸=0 BorrowLendType 1=>借 , 2=>貸           
            var borrow = 0;//借金額
            var lend = 0;//貸金額
           
            for (var i = 0; i < rows.length; i++) {                
                if (rows[i].CompanyID != CompanyID) {
                    alert("傳票資料有誤:公司別不一致！");
                    return false;
                }
                //3.1是否要成本中心=>由Acno,SubAcno推 
                var Acno = rows[i].Acno;
                var SubAcno = rows[i].SubAcno;
                var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
                var CostCenterID = rows[i].CostCenterID;
                if (bCostCenterID == "True" && CostCenterID == "") {
                    alert('此科目' + Acno + ' ' + SubAcno + '需成本中心-請選擇成本中心!');
                    return false;
                }

                if (rows[i].BorrowLendType == 1) {
                    borrow = parseInt(borrow) + parseInt(rows[i].AmtShow);
                } else {
                    lend = parseInt(lend) + parseInt(rows[i].AmtShow);
                }
            }
            //if (rows.length> 0 && borrow == 0) {
            //    alert("借:" + borrow + ",貸:" + lend + " 借貸有問題！");
            //    return false;
            //}
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


            ////4.傳票日期檢查 => 不同年份提醒檢查
            //var dt = new Date();
            //var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
            //return confirm("提醒您,傳票日期。");

            ////更新
            //$('#dataGridView').datagrid('reload');
            queryGrid('#dataGridView');//按查詢

        }
      
        //========================================= 傳票列印 ====================================================================================              
        function OpenPrint(val, row) {
            var sVoucherNoShow = row.VoucherNoShow;
            var sCompanyID = row.CompanyID;
            var VoucherID = row.VoucherID;
            //傳票類別	1稅務+會計帳A, B ; 2稅務帳A ; 3會計帳B=>選1 只跑A開頭傳票編號
            if (VoucherID == 3) {
                VoucherID = "B";
            } else VoucherID = "A";

            if (row.VoucherNoShow == undefined) ""
            else 
                return $('<a>', { href: '#', onclick: 'window.open("http://nt2.jbjob.com.tw/FWCRM/EEPVoucherPrint.aspx?sCompanyID=' + sCompanyID + '&sVoucherNoShow=' + sVoucherNoShow + '&sVoucherID=' + VoucherID + '","轉帳傳票列印");', theData: row.UserID }).linkbutton({ text: "<img src=img/printer.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
        }
        //========================================= 傳票列印(號碼起訖) ====================================================================================              
        function OpenPrint2() {
            var CompanyID = $('#CompanyID_Query').combobox('getValue');//公司別
            var VoucherID = $('#VoucherID_Query').options('getValue');//傳票類別
            var VoucherNoShow = $('#VoucherNoShow_Query').val();//傳票編號1
            var VoucherNoShow2 = $('#VoucherNo_Query').val();//傳票編號2
            var UserID = $('#UserID_Query').combobox('getValue');//	建立人員    
            var VoucherDate = $('#VoucherDate_Query').datebox('getValue');//傳票日期                           
            var CreateDate = $('#CreateDate_Query').datebox('getValue');//建立日期 
          
            //傳票類別	1稅務+會計帳A, B ; 2稅務帳A ; 3會計帳B=>選1 只跑A開頭傳票編號
            if (VoucherID == 3) {
                VoucherID = "B";
            } else VoucherID = "A";

            window.open("http://nt2.jbjob.com.tw/FWCRM/EEPVoucherPrint2.aspx?sCompanyID=" + CompanyID + "&sVoucherID=" + VoucherID + "&sVoucherNoShow=" + VoucherNoShow + "&sVoucherNoShow2=" + VoucherNoShow2 + "&sUserID=" + UserID + "&sVoucherDate=" + VoucherDate + "&sCreateDate=" + CreateDate ,"轉帳傳票列印");
        }
        //參考
        function VoucherPrint() {
            var CompanyID = $("#dataGridView").datagrid('getSelected').CompanyID;
            var VoucherID = $("#dataGridView").datagrid('getSelected').VoucherID;
            var JQDate1 = $("#dataGridView").datagrid('getSelected').VoucherDate;
            var JQDate2 = $("#dataGridView").datagrid('getSelected').VoucherDate;
            var Acno1 = "";
            var Acno2 = "";
            var SubAcno1 = "";
            var SubAcno2 = "";
            var VoucherNo = $("#dataGridView").datagrid('getSelected').VoucherNoShow;
            var CostCenterID = "";
            var iType = "0";//呈現種類	0轉帳傳票 1傳票清單 2日記帳

            //報表用參數
            var TypeText = "轉帳傳票";//呈現種類	1傳票清單 
            var CompanyText = $("#dataGridView").datagrid('getSelected').CompanyName;           

            //傳票類別	1稅務+會計帳A, B ; 2稅務帳A ; 3會計帳B=>選1 只跑A開頭傳票編號
            if (VoucherID == 3) {
                VoucherID = "B";
            } else VoucherID = "A";
            var url = "../JB_ADMIN/REPORT/JBGL/RglVoucherListReportView2.aspx?SDate=" + JQDate1 + "&EDate=" + JQDate2 + "&CompanyID=" + CompanyID + "&VoucherID=" + VoucherID +
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
        
        //複制分錄
        function CopyDetail() {
            var GridName = '#dataGridDetail';
            //var OftenUsedAcno = $("#dataFormMasterOftenUsedAcno").combobox('getValue');
            //if (OftenUsedAcno != "0") {
            //    GridName = '#dataGridDetail2';
            //}
            if (getEditMode($("#dataFormMaster")) != 'inserted') {
                GridName = '#dataGridDetail2';
            }

            var rowcount = $(GridName).datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有可選取明細資料,本功能無法使用');
                return false;
            }
            var aNewObj = {};
            var row = $(GridName).datagrid('getSelected');
            $.extend(aNewObj, row);//複製結構與資料

            //取目前編號
            var Item = 0;
            var AutoKey = 0;
            var rows = $(GridName).datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                if (parseInt(rows[i].Item) > Item) {
                    Item = rows[i].Item;
                }
                if (parseInt(rows[i].AutoKey) > AutoKey) {
                    AutoKey = rows[i].AutoKey;
                }
            }
            aNewObj.AutoKey = parseInt(AutoKey) + 1;
            aNewObj.Item = padLeft(parseInt(Item) + 1,3);

            $(GridName).datagrid('appendRow', aNewObj);

        }
        //補左邊字串
        function padLeft(str, len) {
            str = '' + str;
            return str.length >= len ? str : new Array(len - str.length + 1).join("0") + str;
        }
        //========================================Grid內 科目,明細,內容 之連動=============================================================================
        //一載入時=>明細 隨 公司別、科目 連動
        function OnBeforeLoadSubAcno(param) {
           
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
                
            var row = $('#dataGridDetail2').datagrid('getSelected');
            var rowIndex = $('#dataGridDetail2').datagrid('getRowIndex', row);//找到被編輯的grid的rowindex
            var editor = $('#dataGridDetail2').datagrid('getEditor', { index: rowIndex, field: 'Acno' });//找到Acno的編輯元件
            var Acno = $(editor.target).combobox('getValue');
            if (Acno == "") {
                var rowData = $("#dataGridDetail2").datagrid("getSelected");
                Acno = rowData.Acno;
            }
            var queryWord = new Object();
            queryWord.whereString = "CompanyID=" + CompanyID + " and acno='" + Acno + "'";
            param.queryWord = $.toJSONString(queryWord);                
        }
        function OnBeforeLoadDescribeID(param) {

            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');

            var row = $('#dataGridDetail2').datagrid('getSelected');
            var rowIndex = $('#dataGridDetail2').datagrid('getRowIndex', row);//找到被編輯的grid的rowindex
            var editor = $('#dataGridDetail2').datagrid('getEditor', { index: rowIndex, field: 'Acno' });//找到Acno的編輯元件
            var Acno = $(editor.target).combobox('getValue');
            if (Acno == "") {
                var rowData = $("#dataGridDetail2").datagrid("getSelected");
                Acno = rowData.Acno;
            }
            var queryWord = new Object();
            queryWord.whereString = "CompanyID=" + CompanyID + " and acno='" + Acno + "'";
            param.queryWord = $.toJSONString(queryWord);
        }
        //Grid AutoApply 連動
        function GridAcnoOnSelect(selectedRow) {
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var Acno = selectedRow.Acno;
            var index = $('#dataGridDetail2').datagrid('getRowIndex', $('#dataGridDetail2').datagrid('getSelected'));
            //var editor = $('#dataGridDetail2').datagrid('getEditor', { index: editingRowIndex, field: 'SubAcno' });
            //$(editor.target).combobox('setValue', "");
            //$(editor.target).combobox('setWhere', "CompanyID=" + CompanyID + " and acno='" + Acno + "'");

            if (index != undefined) {
                $("#dataGridDetail2").datagrid('selectRow', index).datagrid('beginEdit', index);
                var cells = $("#dataGridDetail2").datagrid('getEditors', index);
                $.each(cells, function (index, obj) {
                    //科目、明細、內容  連動問題
                    if (obj.field == "SubAcno") {
                        $(obj.target).combobox('setValue', "");
                        $(obj.target).combobox('setWhere', "CompanyID=" + CompanyID + " and Acno='" + Acno + "'");
                    }
                    ////內容清空
                    //if (obj.field == "SubAcnoText") {
                    //    $(obj.target).val("");
                    //}
                    //摘碼代號帶出摘碼內容
                    if (obj.field == "DescribeID") {
                        $(obj.target).combobox('setValue', "");
                        $(obj.target).combobox('setWhere', "CompanyID=" + CompanyID + " and Acno='" + Acno + "'");

                    }
                });
            }
        }
        //========================================Grid2中新增資料=>取得公司別================================================================================
        function GetGrid2CompanyID() {
            return $("#dataFormMasterCompanyID").combobox('getValue');
        }
        //========================================Grid2中新增資料=>取得公司別================================================================================
        function GetGrid2VoucherID() {
            return $("#dataFormMasterVoucherID").options('getCheckedValue');
        }
        //========================================Grid2 內 明細目帶出之內容================================================================================
        function GridSubAcnoOnSelect(selectedRow) { 
            var editingRowIndex = $('#dataGridDetail2').datagrid('getRowIndex', $('#dataGridDetail2').datagrid('getSelected'));
            var editor = $('#dataGridDetail2').datagrid('getEditor', { index: editingRowIndex, field: 'SubAcnoText' });
            $(editor.target).val(selectedRow.AcnoName);
        }
        //========================================Grid2 內 摘碼代號帶出摘碼內容 =============================================================================
        function DescribeIDOnSelect(selectedRow) {
            var editingRowIndex = $('#dataGridDetail2').datagrid('getRowIndex', $('#dataGridDetail2').datagrid('getSelected'));
            var editor = $('#dataGridDetail2').datagrid('getEditor', { index: editingRowIndex, field: 'Describe' });
            $(editor.target).val(selectedRow.Describe);
        }
        //存檔前檢查
        function OnInsertGrid2() {
            var GridName = '#dataGridDetail2';
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var rows = $(GridName).datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {                
                //3.1是否要成本中心=>由Acno,SubAcno推 
                var Acno = rows[i].Acno;
                var SubAcno = rows[i].SubAcno;
                var bCostCenterID = GetDataFromMethod('GetCostCenterID', { Company_ID: CompanyID, Ac_no: Acno, Sub_Acno: SubAcno });
                var CostCenterID = rows[i].CostCenterID;
                if (bCostCenterID == "True" && CostCenterID == "") {
                    alert('此科目' + Acno + ' ' + SubAcno + '需成本中心-請選擇成本中心!');
                    return false;
                }
            }
        }
        //=========================================================================================            
        //-----------------------------------輸入檢查-----------------------------------------
        //var DataForm_VoucherMaster_OnApply = function () {
        //    var data = $(this).jbDataFormGetAFormData();
        //    var methodName = getEditMode($(this)) == 'inserted' ? 'DataValidate_Insert' : 'DataValidate_Update';
        //    $.ajaxSetup({ async: false });
        //    var Ans = false;
        //    //●判斷輸入資料是否合格在職
        //    //●是否輸入重複
        //    $.post('../handler/JqDataHandle.ashx?RemoteName=_HRM_Attend_Normal_ShiftManagement', { mode: 'method', method: methodName, parameters: $.toJSONString(data) }
        //          ).done(function (data) {
        //              var Json = $.parseJSON(data);
        //              if (Json.IsOK) Ans = true;
        //              else alert(Json.ErrorMsg);
        //          }).fail(function (xhr, textStatus, errorThrown) {
        //              alert('error');
        //          });
        //    return Ans;
        //}
        
        //========================================匯入Excel========================================
        var openImportExcel = function () {
            $(Dialog_Import_ID).dialog('open');
        }
        //==========================================================================================

        //---------------------------------------匯入Excel Sheet切換------------------------------
        var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
            $('#Dialog_Import').jbExcelFileImport('changeSheetByIndex', rowData.value);
        }
    </script> 
</head>
<body>

    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglVoucherMaster.glVoucherMaster" runat="server" AutoApply="True"
                DataMember="glVoucherMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="傳票列表" QueryMode="Panel" OnLoadSuccess="OnLoadSuccessGV" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnDelete="OnDeleteGV" OnUpdate="OnUpdateGV">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="numberbox" FieldName="CompanyName" Format="" Visible="true" Width="150" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票類別" Editor="text" FieldName="VoucherTypeName" MaxLength="0" Visible="true" Width="100" Sortable="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票編號" Editor="text" FieldName="VoucherNoShow" Format="" MaxLength="0" Visible="true" Width="100" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="列印" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FieldName="OpenPrint" FormatScript="OpenPrint">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Format="" MaxLength="0" Visible="False" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="借方總額" Editor="numberbox" FieldName="SumBorrow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="right" Caption="貸方總額" Editor="numberbox" FieldName="SumLend" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" Total="sum">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="True" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="建立時間" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Visible="True" Width="120" FormatScript="" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增傳票" />                   
                    <JQTools:JQToolItem ID="Import" Icon="icon-importExcel" ItemType="easyui-linkbutton" OnClick="openImportExcel" Text="匯入傳票"  />
                    <JQTools:JQToolItem ID="JQToolItem1" Icon="icon-print" ItemType="easyui-linkbutton" OnClick="OpenPrint2" Text="傳票列印"  />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="155" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票類別" Condition="=" DataType="string" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="VoucherID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建立人員" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'UserID',textField:'CreateBy',remoteName:'sglVoucherMaster.infoCreateBy',tableName:'infoCreateBy',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="UserID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票編號" Condition="%" DataType="string" Editor="text" FieldName="VoucherNoShow" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption=" " Condition="%" DataType="string" Editor="text" FieldName="VoucherNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="傳票日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="VoucherDate" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="90" />
<JQTools:JQQueryColumn FieldName="CreateDate" IsNvarChar="False" Caption="建立日期" Width="90" Condition="=" Editor="datebox" RemoteMethod="False" AndOr="and" DataType="datetime" NewLine="False" Span="0" RowSpan="0"></JQTools:JQQueryColumn>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="傳票維護" DialogLeft="20px" DialogTop="5px" Width="870px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherMaster.glVoucherMaster" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnLoadSuccess="OnLoadSuccessDFMaster" OnApply="OnApplyDFMaster" OnApplied="OnAppliedDFMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票編號" Editor="text" FieldName="VoucherNoShow" Format="" Width="100" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="datebox" FieldName="VoucherDate" Format="" MaxLength="0" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" Format="" Width="160" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infooptions" FieldName="VoucherID" Format="" Width="180" EditorOptions="title:'JQOptions',panelWidth:260,remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" MaxLength="0" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分錄科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:UsedAcno_OnSelect,panelHeight:200" FieldName="OftenUsedAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="常用分錄" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectOftenUsedEntryID,panelHeight:200" FieldName="OftenUsedEntryID" MaxLength="0" Width="290" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="glVoucherDetails" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglVoucherMaster.glVoucherMaster" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" EditDialogID="JQDialog2" OnInsert="OnInsertDetail" ParentObjectID="dataFormMaster" Height="240px" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNoShow" Editor="text" FieldName="VoucherNoShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
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
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyDetail" Text="複製" Visible="True" />                                                             
                </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDataGrid ID="dataGridDetail2" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DataMember="glVoucherDetails" DeleteCommandVisible="True" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="240px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" OnSelect="OnSelectGrid2" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sglVoucherMaster.glVoucherMaster" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNo" Editor="text" FieldName="VoucherNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherNoShow" Editor="text" FieldName="VoucherNoShow" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="序號" Editor="text" FieldName="Item" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="科目" Editor="infocombobox" EditorOptions="valueField:'Acno',textField:'Acno',remoteName:'sglVoucherMaster.infoAcno',tableName:'infoAcno',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:GridAcnoOnSelect,panelHeight:200" FieldName="Acno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="valueField:'SubAcno',textField:'SubAcno',remoteName:'sglVoucherMaster.infoSubAcno',tableName:'infoSubAcno',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:GridSubAcnoOnSelect,onBeforeLoad:OnBeforeLoadSubAcno,panelHeight:200" FieldName="SubAcno" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="內容" Editor="text" FieldName="SubAcnoText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="160">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="valueField:'CostCenterID',textField:'CostCenterName',remoteName:'sglVoucherMaster.infoglCostCenter',tableName:'infoglCostCenter',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="99">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="摘碼代號" Editor="infocombobox" EditorOptions="valueField:'DescribeID',textField:'DescribeID',remoteName:'sglVoucherMaster.infoglDescribe',tableName:'infoglDescribe',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:DescribeIDOnSelect,onBeforeLoad:OnBeforeLoadDescribeID,panelHeight:200" FieldName="DescribeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="摘碼內容" Editor="text" FieldName="Describe" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="206">
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
                        <JQTools:JQGridColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton" OnClick="CopyDetail" Text="複製" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQAutoSeq ID="JQAutoSeq2" runat="server" BindingObjectID="dataGridDetail2" FieldName="Item" NumDig="3" />
                <JQTools:JQAutoSeq ID="JQAutoSeq12" runat="server" BindingObjectID="dataGridDetail2" FieldName="AutoKey" NumDig="3" />

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" EditMode="Continue" Width="750px" Closed="True" Title="">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="True" DataMember="glVoucherDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherMaster.glVoucherDetails" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApply="OnApplyDFDetail" OnLoadSuccess="OnLoadSuccessDFDetail" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="序號" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="借貸" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListID',remoteName:'sglVoucherMaster.infoglBorrowLendType',tableName:'infoglBorrowLendType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:100" FieldName="BorrowLendType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="40" />
                            <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:Acno_OnSelect,panelHeight:150" FieldName="Acno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectSubAcno,panelHeight:200" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="200" OnBlur="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="SubAcnoText" Editor="text" FieldName="SubAcnoText" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
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
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherNoShow" Editor="text" FieldName="VoucherNoShow" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CompanyID" Editor="text" FieldName="CompanyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="VoucherID" Editor="text" FieldName="VoucherID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="VoucherNo" ParentFieldName="VoucherNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="Item" NumDig="3" />
                    <JQTools:JQAutoSeq ID="JQAutoSeq11" runat="server" BindingObjectID="dataFormDetail" FieldName="AutoKey" NumDig="3" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="VoucherNoShow" RemoteMethod="True" />
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
                <JQTools:JQValidate ID="validateGridDetail2" runat="server" BindingObjectID="dataGridDetail2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="BorrowLendType" RemoteMethod="True" ValidateMessage="請選擇借貸！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Acno" RemoteMethod="True" ValidateMessage="請選擇科目！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="" CheckNull="True" FieldName="SubAcnoText" RemoteMethod="True" ValidateMessage="請選擇明細！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Describe" RemoteMethod="True" ValidateMessage="請填寫摘碼內容！" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AmtShow" RemoteMethod="True" ValidateMessage="請填寫金額！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail2" runat="server" BindingObjectID="dataGridDetail2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AutoKey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="1" FieldName="BorrowLendType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" CarryOn="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetGrid2CompanyID" FieldName="CompanyID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetGrid2VoucherID" FieldName="VoucherID" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
            </JQTools:JQDialog>
        </div>

         <!-- 匯入對話框內容的 DIV -->
        <div id="Dialog_Import"></div>

        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="" Width="750px">
            <JQTools:JQDataForm ID="DataForm_VoucherMaster" runat="server" Closed="False" ContinueAdd="False" DataMember="glVoucherDetails" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="3" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherImport.glVoucherDetails" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompanyID" Width="180" Format="" MaxLength="50"  />
                    <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infocombobox" EditorOptions="valueField:'VoucherID',textField:'VoucherTypeName',remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:true,panelHeight:200" FieldName="VoucherID" MaxLength="50" Width="180" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_VoucherMaster" runat="server" BindingObjectID="DataForm_VoucherMaster" EnableTheming="True">
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>
            <JQTools:JQDefault ID="Default_Schedule" runat="server" BindingObjectID="DataForm_VoucherMaster">
            </JQTools:JQDefault>
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />

                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherImport.glVoucherDetails" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="傳票日期" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="VoucherDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="借貸" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="BorrowLendType" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="科目" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Acno" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="明細" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="SubAcno" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="成本中心" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="CostCenterID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="摘碼內容" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Describe" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="金額" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Amt" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
            </JQTools:JQValidate>
        </JQTools:JQDialog>

    </form>
</body>
</html>
