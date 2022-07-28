<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBRecruitUserDayCollar.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>  

         $(document).ready(function () {                       
           
             $("#div_DG_Export").hide();
             //$("#CompanyID_Query").combobox('setValue', 1);
             //GetQEmployer();//雇主

             ////Grid選取單選,checkbox多選
             //$("#dataGridDetail").datagrid({
             //    singleSelect: true,
             //    selectOnCheck: false,
             //    checkOnSelect: false
             //});

         });
         function queryGrid(dg) {//查詢後添加固定條件
             if ($(dg).attr('id') == 'dataGridView') {
                 //查詢條件
                 var result = [];
                 var CollarType = $('#CollarType_Query').combobox('getValue');//發薪種類
                 var RecruitID = $('#RecruitID_Query').combobox('getValue');//招募人員 

                 var CreateDateS = $('#CreateDateS_Query').datebox('getValue');//付款日期
                 var CreateDateE = $('#CreateDateE_Query').datebox('getValue');//付款日期

                 if (CollarType != '') result.push("m.CollarType = " + CollarType );
                 if (RecruitID != '') result.push("m.RecruitID = " + RecruitID);
                 if (CreateDateS != '') result.push("Convert(nvarchar(10),m.PayDate,111) between '" + CreateDateS + "' and '" + CreateDateE + "'");

                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }

         //寫入人才資料列表
         function RunEmployee() {

             var CollarType = $("#dataFormMasterCollarType").combobox('getValue');
             if (CollarType == "") {
                 alert("請選擇薪轉種類！");
                 return false;
             }
             var RecruitID = $("#dataFormMasterRecruitID").combobox('getValue');//招募		
             if (RecruitID == "") {
                 alert("請選擇招募人員！");
                 return false;
             }
             var iAmt = $("#dataFormMasterAmt").numberbox('getValue');//預設金額

             var dataGrid = $('#dataGridDetail');
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruit.UserDayCollarMaster',  //連接的Server端，command
                     data: "mode=method&method=" + "InsertUserDayCollarUser" + "&parameters=" + CollarType + "," + RecruitID + "," + iAmt,
                 cache: false,
                 async: true,
                 success: function (data) {
                     var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                     //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                     if (rows != null && rows.length > 0) {
                         dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                         data = eval('(' + data + ')');
                         var appandRows = [];
                         //var UserID = getClientInfo("UserID");
                         var UserName = getClientInfo("UserName");
                         var today = getClientInfo('_today')
                         for (var j = 0; j < data.length; j++) {
                             appandRows.push({
                                 Autokey: j, UserID: data[j].UserID, CustomerID: data[j].CustomerID, JobID: data[j].JobID, BankID: data[j].BankID, BankName: data[j].BankName,
                                 BankBranchID: data[j].BankBranchID, AccountID: data[j].AccountID, AccountNo: data[j].AccountNo, AccountName: data[j].AccountName,
                                 NameC: data[j].NameC, JobName: data[j].JobName, Amt: iAmt, CreateBy: UserName,
                                 CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today
                             });
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

             //$.ajax({
             //    type: "POST",
             //    url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruit.UserDayCollarMaster',  //連接的Server端，command
             //    data: "mode=method&method=" + "InsertUserDayCollarUser" + "&parameters=" + CollarType + "," + RecruitID + "," + Amt,
             //    cache: false,
             //    async: true,
             //    success: function (data) {
             //        $('#dataGridDetail').datagrid('reload');
             //        alert("載入完成！");
             //    },
             //    error: function (xhr, ajaxOptions, thrownError) {
             //        alert(xhr.status);
             //        alert(thrownError);
             //    }
             //});
         }
         //新增完資料
         function OnAppliedFormMaster() {

             queryGrid($('#dataGridView'));

             ////再打開一次網頁---------------------------------------------------------------------------------------
             //var Autokey = $("#dataFormMasterAutokey").val();
             //if (getEditMode($("#dataFormMaster")) == 'updated') {

             //    $("#dataGridView").datagrid('setWhere', "Autokey = " + Autokey);
             //    setTimeout(function () {
             //        openForm('#JQDialog1', $('#dataGridView').datagrid('getSelected'), "updated", 'dialog');
             //    }, 500);
             //}
         }

         //---------------------------------------呼叫Method---------------------------------------
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = null;
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sJBRecruit.UserDayCollarMaster',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };

         //---------------------------------------dataGrid Query---------------------------------
         //---------------------------------------選客戶觸發---------------------------------
         var QCustomerID_OnSelect = function (rowdata) {
             GetQJob();//職缺
             $('#JobID_Query').combobox('setValue', "");

         }

         //得到職缺
         var GetQJob = function (CompanyID) {
             //客戶
             var CustomerID = $("#CustomerID_Query").combobox('getValue');
             var JobID = $("#JobID_Query").combobox('getValue');//職缺	

             var CodeList = GetDataFromMethod('GetEmployer', { Customer_ID: CustomerID, Job_ID: JobID });
             if (CodeList != null) {
                 $("#JobID_Query").combobox('loadData', CodeList);//職缺
             }
         }
         //---------------------------------------DataForm---------------------------------

         //---------------------------------------選發薪種類觸發---------------------------------
         var OnSelect_CollarType = function (rowdata) {
             $('#dataGridDetail').datagrid('loadData', []); //清空資料 
             $('#dataFormMasterNationalityID').combobox('setValue', "");
             $('#dataFormMasterEmployerID').combobox('setValue', "");

         }  
        //---------------------------------------選客戶觸發---------------------------------
         var CustomerID_OnSelect= function (rowdata) {
             GetJob();//職缺
             $('#dataGridDetail').datagrid('loadData', []); //清空資料 
             $('#dataFormMasterNationalityID').combobox('setValue', "");
        }
      
         //得到職缺
        var GetJob = function (CompanyID) {
            //客戶
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var EmployerID = $("#dataFormMasterEmployerID").combobox('getValue');//雇主	

            var CodeList = GetDataFromMethod('GetJob', { Company_ID: CompanyID, Employer_ID: EmployerID });
            if (CodeList != null) {
                $("#dataFormMasterEmployerID").combobox('loadData', CodeList);//雇主
            }
        }
         //---------------------------------------選職缺觸發---------------------------------
        var EmployerID_OnSelect = function (rowdata) {
            $('#dataGridDetail').datagrid('loadData', []); //清空資料 
            $('#dataFormMasterNationalityID').combobox('setValue', "");
            GetNational();//國籍
        }

         //得到招募
        var GetNational = function () {
           
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue'); //公司別
            var EmployerID = $("#dataFormMasterEmployerID").combobox('getValue');//雇主	
            var NationalityID = $("#dataFormMasterNationalityID").combobox('getValue');//國籍	
            var RecordType = $("#dataFormMasterRecordType").options('getValue');//報表種類

            var CodeList = GetDataFromMethod('GetNational', { Company_ID: CompanyID, Employer_ID: EmployerID, Nationality_ID: NationalityID, RecordType_ID: RecordType });
            if (CodeList != null) {
                $("#dataFormMasterNationalityID").combobox('loadData', CodeList);//國籍
            }
        }
         //---------------------------------------選國籍觸發---------------------------------
        var NationalityID_OnSelect = function (rowdata) {
            //OnSelectEmployee();//帶出外勞名單
        }

         
        //---------------------------------------人才批次刪除-----------------------------------------------------
        function deleteMore() {
            var dataGrid = $('#dataGridDetail');

            if (dataGrid.datagrid('getChecked').length == 0) {
                alert('請勾選人才。');
            } else {

                var pre = confirm("確定刪除?");
                if (pre == true) {
                    var allRows = dataGrid.datagrid('getRows');
                    var chRows = dataGrid.datagrid("getChecked");

                    for (var i = 0; i < chRows.length; i++) {
                        for (var j = 0; j < allRows.length; j++) {
                            if (allRows[j].Autokey == chRows[i].Autokey) {
                                dataGrid.datagrid("deleteRow", j);
                            }
                        }
                    }

                    //dataGrid.datagrid("reload");

                }
            }
        }
        //------------------存檔前檢查 dataForm-----------------------
        function checkApplyFormMaster() {
            //1.檢查訂單明細
            var data = $("#dataGridDetail").datagrid("getRows");
            if (data.length == 0) {
                alert('無人才。');
                return false;
            }
            //2.若已轉出檔案=>則不可編輯
            if ($("#dataFormMasterIsOutPut").val() == "true") {           
                alert('已轉出檔案,則不可編輯。');
                return false;
            }
            //3.若已轉出檔案=>則不可編輯
            if ($("#dataFormMasterIsOutPutSmall").val() == "true") {
                alert('已轉出檔案,則不可編輯。');
                return false;
            }
        }
        function OnLoadFormMaster() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#bRunEmployee").show();//顯示查詢紐
                //属性删除
                $("#dataFormMasterCollarType").combobox().removeAttr("disabled");
                $("#dataFormMasterRecruitID").combobox().removeAttr("disabled");
                $("#dataFormMasterAmt").numberbox().removeAttr("disabled");

            } else {
                //属性不可編輯
                $("#bRunEmployee").hide();//隱藏查詢紐
                $("#dataFormMasterCollarType").combobox('disable');
                $("#dataFormMasterRecruitID").combobox('disable');
                $("#dataFormMasterAmt").numberbox('disable');

            }
        }

        function OnLoadDetail() {
            //選擇人才...
            if (getEditMode($("#dataFormMaster")) == 'updated') 
            {
                var RecordNo = $("#dataFormMasterRecordNo").val();
                var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
                var CustID = $("#dataFormMasterEmployerID").combobox('getValue');
                $('#dataFormDetailEmployeeID').refval('setWhere', "CompanyID=" + CompanyID + " and left(EmployeeID,5)='" + CustID + "' and  EmployeeID not in (select EmployeeID from FWCRMServiceRecordDetails where RecordNo ='" + RecordNo + "')");
            }
        }
        function sCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }

       

        function AddEmployee() {
            var dataGrid = $('#dataGridDetail');

            var UserID = getClientInfo("UserID");
            var UserName = getClientInfo("UserName");
            var today = getClientInfo('_today')

            var sRecordNo = $("#dataFormMasterRecordNo").val();
            var sEmployeeID = $("#dataFormDetailEmployeeID").refval('getValue');
            var sEmployeeTcName = $("#dataFormDetailEmployeeID").refval('selectItem').text;//取refval文字
            $(dataGrid).datagrid('updateRow', {
                index: 0,
                row: { iAutokey: 0, RecordNo: sRecordNo, EmployeeID: sEmployeeID, EmployeeTcName: sEmployeeTcName }//可以多欄位 用','隔開
            });
            //var appandRows = [];
            //appandRows.push({ iAutokey: 0, RecordNo: sRecordNo, EmployeeID: sEmployeeID, EmployeeTcName: sEmployeeTcName, EffectDate: "", EffectDate2: "", CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today });
            //dataGrid.datagrid("appendRow", appandRows);
            ////griddetail的footer強制更新
            //setFooter(dataGrid);
        }
        //---------------------------------------報表編輯權限控制-----------------------------------------------------
        //控制是否可以修改
        function RecordUpdateRow(rowData) {
            var username = getClientInfo("username");
            if (rowData.LastUpdateBy != username) {
                alert('無編輯權限！');
                return false; //取消編輯的動作 
            }
        }

        //-------------------CheckBox顯示---------------------------------------
        function genCheckBox(val) {
            if (val == null) {
                return "";
            }
            else if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox'  onclick='return false;'  />";
        }

        // 顯示人才資料不完整的部分
        function FormatNameCString(val, rowData) {
            if (rowData.Autokey == null) {
                return "";
            }
            else if (rowData.BankBranchID == "" || rowData.AccountID == "" || rowData.AccountName == "" || rowData.AccountNo == "") {//分行代號 ,身分證, 匯款戶名,帳號
                return "<div style='font-weight:bold;color:red;' title='資料不完整'> " + val + "</div>";
             } else {
                 return "<div style='color:black;'> " + val + "</div>";
             }
         }

        //-------------轉出文字檔(整批) ---------------------------------------------------------------------------------------------------       
        function LinkText(val, row, index) {
            var Autokey = row.Autokey;
            if (Autokey == null) {
                return "";
            }
            else
            return $('<a>', { href: "#", onclick: "OpenText(" + Autokey + ")", theData: row.RecordNo }).linkbutton({ text: "<img src=img/document.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
        }
        function OpenText(Autokey) {
            
            var pre = confirm("確定輸出檔案?");
            if (pre == true) {

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruit.UserDayCollarMaster', //連接的Server端，command
                    data: "mode=method&method=" + "TxtUserDayCollarData" + "&parameters=" + Autokey,
                    cache: false,
                    async: true,
                    beforeSend: function () { $.messager.progress({ title: '轉出文字檔', msg: '轉出文字檔開始, 請稍後 ...' }); },
                    complete: function () { $.messager.progress('close'); },
                    success: function (jsonStr) {
                        var json = $.parseJSON(jsonStr);
                        if (!json.IsOK) $.messager.alert('產生失敗', '執行錯誤', 'error');
                        else {
                            if (json.Result.length == 0) { $.messager.alert("無轉出資料"); }
                            else {
                                //通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                                $("#DG_Export").datagrid('loadData', json.Result);//將結果塞入
                                exportExcel($("#DG_Export"), "1");//轉出文字檔
                                queryGrid($('#dataGridView'));

                            }
                        }
                    }
                });
            }

        }
        //-------------轉出文字檔(小額) ---------------------------------------------------------------------------------------------------       
        function LinkTextSmall(val, row, index) {
            var Autokey = row.Autokey;
            if (Autokey == null) {
                return "";
            }
            else
            return $('<a>', { href: "#", onclick: "OpenTextSmall(" + Autokey + ")", theData: row.RecordNo }).linkbutton({ text: "<img src=img/document.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
        }
        function OpenTextSmall(Autokey) {

            var pre = confirm("確定輸出檔案?");
            if (pre == true) {

                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sJBRecruit.UserDayCollarMaster', //連接的Server端，command
                    data: "mode=method&method=" + "TxtUserDayCollarDataSmall" + "&parameters=" + Autokey,
                    cache: false,
                    async: true,
                    beforeSend: function () { $.messager.progress({ title: '轉出文字檔', msg: '轉出文字檔開始, 請稍後 ...' }); },
                    complete: function () { $.messager.progress('close'); },
                    success: function (jsonStr) {
                        var json = $.parseJSON(jsonStr);
                        if (!json.IsOK) $.messager.alert('產生失敗', '執行錯誤', 'error');
                        else {
                            if (json.Result.length == 0) { $.messager.alert("無轉出資料"); }
                            else {
                                //通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                                $("#DG_Export").datagrid('loadData', json.Result);//將結果塞入
                                exportExcel($("#DG_Export"), "1");//轉出文字檔
                                queryGrid($('#dataGridView'));

                            }
                        }
                    }
                });
            }

        }
        function exportExcel(DG_SalaryExport, flag) {
            var userid = getClientInfo("UserID");
            var exportFields = [];
            var fields = DG_SalaryExport.datagrid('getColumnFields', true);
            for (var i = 0; i < fields.length; i++) {
                exportFields.push({ field: fields[i], title: DG_SalaryExport.datagrid('getColumnOption', fields[i]).title });
            }
            fields = DG_SalaryExport.datagrid('getColumnFields');
            for (var i = 0; i < fields.length; i++) {
                exportFields.push({ field: fields[i], title: DG_SalaryExport.datagrid('getColumnOption', fields[i]).title });
            }

            //$.each(rows[0], function (key, value) {
            //    exportFields.push(key);
            //});

            var rows = DG_SalaryExport.datagrid('getRows');

            //userid 使用者登入帳號;
            //flag == "0" 轉 Excel ; 
            //flag == "1" 轉 txt ; 
            //isBlank == "Y" 欄位與欄位間需要加一個空白字串 ; 
            //isBlank == "N" 欄位與欄位間不需要加一個空白字串; 
            //isColumnName == "Y" 需要加欄位表頭名稱 ; 
            //isColumnName == "N" 不需要加欄位表頭名稱; 
            $.ajax({
                type: "POST",
                url: "../handler/JBHRISHandler.ashx?ExportExcel=" + flag,//連接當前網頁的cs程式
                data: { mode: 'ExportExcel', userid: userid, flag: flag, isBlank: 'N', isColumnName: 'N', reportName: '薪轉', fields: $.toJSONString(exportFields), rows: $.toJSONString(rows) },
                cache: false,
                async: false,
                success: function (data) {
                    $.messager.progress('close'); //進度條結束
                    window.open('../handler/JqFileHandler.ashx?File=' + data, 'download');
                }
            });
        }

     </script>
    <style type="text/css">
        .auto-style1 {
            height: 28px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruit.UserDayCollarMaster" runat="server" AutoApply="True"
                DataMember="UserDayCollarMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="上海銀行薪轉列表" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="RecordUpdateRow" Width="980px">
                <Columns>
                     <JQTools:JQGridColumn Alignment="center" Caption="整批輸出" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOutPut" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="genCheckBox">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="整批轉出" Editor="text" FieldName="LinkText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" FormatScript="LinkText"></JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="小額輸出" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsOutPutSmall" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="genCheckBox">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="小額轉出" Editor="text" FieldName="LinkTextSmall" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" FormatScript="LinkTextSmall"></JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="薪轉種類" Editor="text" FieldName="sCollarType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="招募人員" Editor="text" FieldName="RecruitIDText" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100" Format="">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="付款日期" Editor="datebox" FieldName="PayDate" Format="yyyy/mm/dd" MaxLength="0" Visible="True" Width="80" />
                     <JQTools:JQGridColumn Alignment="right" Caption="筆數" Editor="numberbox" FieldName="iCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="right" Caption="總金額" Editor="numberbox" FieldName="iAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Total="sum" Visible="True" Width="70" Format="N">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="yyyy/mm/dd HH:MM:SS" Visible="true" Width="120" />
                     <JQTools:JQGridColumn Alignment="center" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                     </JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="left" Caption="RecordType" Editor="text" FieldName="CollarType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="編號" Editor="text" FieldName="Autokey" Format="" Visible="False" Width="70" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RecruitID" Editor="text" FieldName="RecruitID" Visible="False" Width="80" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增薪轉資料" />
<%--                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />--%>
<%--                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
<%--                    <JQTools:JQQueryColumn AndOr="and" Caption="客戶" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CustomerID',textField:'CustomerShortName',remoteName:'sJBRecruit.infoCustomerID',tableName:'infoCustomerID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QCustomerID_OnSelect,panelHeight:120" FieldName="CustomerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="職缺" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="JobID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="190" />--%>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發薪種類" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sJBRecruit.infoCollarType',tableName:'infoCollarType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" FieldName="CollarType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="62" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="招募人員" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit.infoRecruitID',tableName:'infoRecruitID',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="RecruitID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="120" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="付款日期" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="～" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                </QueryColumns>
            </JQTools:JQDataGrid>

             <div id="div_DG_Export">
            <JQTools:JQDataGrid ID="DG_Export" data-options="pagination:true,view:commandview" RemoteName="sJBRecruit.UserDayCollarMaster" runat="server" AutoApply="True"
                DataMember="UserDayCollarMaster" Pagination="False" QueryTitle="" EditDialogID=""
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="750px">
                <Columns>
                     <JQTools:JQGridColumn Alignment="left" Caption="sTxt" Editor="text" FieldName="sTxt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="400"></JQTools:JQGridColumn>
                </Columns>
            </JQTools:JQDataGrid>
            </div>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="新增薪轉紀錄" Width="1050px" DialogLeft="5px" DialogTop="5px" Height="480px" ShowSubmitDiv="True">
                <table style="width:100%;">
                    <tr>
                        <td class="auto-style1" style="width: 620px">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="UserDayCollarMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedFormMaster" OnApply="checkApplyFormMaster" OnLoadSuccess="OnLoadFormMaster" RemoteName="sJBRecruit.UserDayCollarMaster" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" ParentObjectID="dataGridView">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="text" FieldName="Autokey" Format="" ReadOnly="True" Visible="False" Width="80" maxlength="0" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="薪轉種類" Editor="infocombobox" FieldName="CollarType" ReadOnly="False" Span="1" Visible="True" Width="80" EditorOptions="valueField:'ID',textField:'Name',remoteName:'sJBRecruit.infoCollarType',tableName:'infoCollarType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" MaxLength="0" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="招募人員" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sJBRecruit.infoRecruitID',tableName:'infoRecruitID',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="RecruitID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="150" Format="" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="付款日期" Editor="datebox" FieldName="PayDate" Visible="True" Width="95" ReadOnly="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="預設金額" Editor="numberbox" FieldName="Amt" Visible="True" Width="50" ReadOnly="False" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Visible="False" Width="180" Format="" ReadOnly="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" ReadOnly="False" Width="180" Visible="False" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" ReadOnly="False" Visible="False" Width="180" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" ReadOnly="False" Visible="False" Width="180" Span="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="IsOutPut" Editor="text" FieldName="IsOutPut" ReadOnly="False" Visible="False" Width="80" Span="1" MaxLength="0" NewRow="False" RowSpan="1" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="IsOutPutSmall" Editor="text" FieldName="IsOutPutSmall" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                                </Columns>
                                <RelationColumns>
                                    <JQTools:JQRelationColumn FieldName="Autokey" ParentFieldName="Autokey" />
                                </RelationColumns>
                            </JQTools:JQDataForm>
                        </td>
                        <td style="vertical-align: bottom; text-align: left;" class="auto-style1"><a ID="bRunEmployee" class="easyui-linkbutton" href="#" onclick="RunEmployee()">載入</a></td>
                    </tr>
                </table>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="UserDayCollarDetail" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="330px" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sJBRecruit.UserDayCollarMaster" RowNumbers="True" Title="人才匯款金額設定" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="850px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="Autokey" Editor="text" FieldName="Autokey" Format="" ReadOnly="False" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="客戶-職缺" Editor="text" FieldName="JobName" ReadOnly="True" Visible="True" Width="140" />
                        <JQTools:JQGridColumn Alignment="left" Caption="人才姓名" Editor="text" FieldName="NameC" ReadOnly="True" Visible="True" Width="60" FormatScript="FormatNameCString">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="日領金額" Editor="numberbox" FieldName="Amt" ReadOnly="False" Visible="True" Width="55" Total="sum" Format="N">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉帳費" Editor="numberbox" FieldName="TransferAmt" ReadOnly="False" Visible="True" Width="45" Total="sum" Format="N">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="行政費" Editor="numberbox" FieldName="AdministrativeAmt" ReadOnly="False" Visible="True" Width="45" Total="sum" Format="N">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="履歷編號" Editor="text" FieldName="UserID" Visible="True" Width="70" ReadOnly="True" TableName="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="收款銀行" Editor="text" FieldName="BankName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="匯款代碼" Editor="text" FieldName="BankBranchID" Visible="True" Width="65" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="身分證" Editor="text" FieldName="AccountID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="收款戶名" Editor="text" FieldName="AccountName" Visible="True" Width="65" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="收款帳號" Editor="text" FieldName="AccountNo" Visible="True" Width="100" ReadOnly="True" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" Sortable="False">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="銀行代號" Editor="text" FieldName="BankID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="JobID" Editor="text" FieldName="JobID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="RecruitID" Editor="text" FieldName="RecruitID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="120" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="MasterAutokey" Editor="text" FieldName="MasterAutokey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="MasterAutokey" ParentFieldName="Autokey" />
                    </RelationColumns>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="150px" DialogTop="50px" Title="新增人才" Width="300px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="UserDayCollar" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="OnLoadDetail" ParentObjectID="dataFormMaster" RemoteName="sJBRecruit.UserDayCollar" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="選擇人才" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:350,remoteName:'sFWCRMServiceRecord.infoEmployeeID',tableName:'infoEmployeeID',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeTcName',valueFieldCaption:'人才編號',textFieldCaption:'人才姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="EmployeeID" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="Autokey" Editor="numberbox" FieldName="iAutokey" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="RecordNo" Editor="text" FieldName="RecordNo" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="RecordNo" ParentFieldName="RecordNo" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <a class="easyui-linkbutton" href="#" onclick="AddEmployee()">新增</a>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="iAutokey" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCode" FieldName="CreateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployeeID" RemoteMethod="True" ValidateMessage="請選擇人才" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Autokey" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PayDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CollarType" RemoteMethod="True" ValidateMessage="請選擇發薪種類" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RecruitID" RemoteMethod="True" ValidateMessage="請選擇招募人員" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PayDate" RemoteMethod="True" ValidateMessage="請選擇付款日期" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
