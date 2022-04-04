<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_SalesDetails2.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
         $(document).ready(function () {
             //寬度調整
             $("#cbSalesEmployeeID").combobox('resize','100');
             $("#cbCustNO").combobox('resize', '100');
             //條件參數添加
             refObj = new Object();
             refObj.SalesEmployeeID = getClientInfo("UserID");
             $("div").data(refObj);//把數據添加到div元素  

             //跳登日期註冊
             $("#dataFormMasterJumpDate").jbDateBoxMultiple({});
             //$("#dataFormMasterJumpDate").jbDateBoxMultiple('setData');

             //查詢條件預設值
             var UserID = getClientInfo("UserID");             
             $("#cbSalesEmployeeID").combobox('setValue', UserID);
             $("#cbCustNO").combobox('setValue', "==不拘==");

             var dt = new Date();
             var FirstDate = new Date($.jbGetFirstDate(dt));             
             $("#JQDate1").datebox('setValue', $.jbjob.Date.DateFormat(FirstDate, 'yyyy/MM/dd'));
             var LastDate = new Date($.jbGetLastDate(dt));
             $("#JQDate2").datebox('setValue', $.jbjob.Date.DateFormat(LastDate, 'yyyy/MM/dd'));             
             
             //查詢欄位更新主檔
             $('#JQDate1').datebox({
                 width: 100,
                 onSelect: function (date) {
                     $('#dataGridDetail').datagrid('setWhere', '1=0'); //可以有清空資料的效果
                     RefreshGrid();
                 }
             }).combo('textbox').blur(function () {
                 RefreshGrid();
             });
             $('#JQDate2').datebox({
                 width: 100,
                 onSelect: function (date) {
                     $('#dataGridDetail').datagrid('setWhere', '1=0'); //可以有清空資料的效果
                     RefreshGrid();
                 }
             }).combo('textbox').blur(function () {
                 RefreshGrid();
             });
             //新增主檔銷貨排版
             AddSalesMasterLoad();            
             //代辦事項筆數
             ShowToDoCount();
             ////新增銷貨明細排版and 註冊日曆
             AddSalesDetailsLoad();             
         });
         //代辦事項筆數呈現
         function ShowToDoCount() {
             var cnt;
             var SalesEmployeeID = $("div").data("SalesEmployeeID");
             //var UserID = getClientInfo("UserID");                        
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesMaster', //連接的Server端，command
                 data: "mode=method&method=" + "ShowToDoCount" + "&parameters=" + SalesEmployeeID, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                 cache: false,
                 async: false,
                 success: function (data) {
                     if (data != false) {
                         cnt = data;
                     }                    
                 },
                 error: function (xhr, ajaxOptions, thrownError) {
                     alert(xhr.status);
                     alert(thrownError);
                 }
             });
             //代辦事項呈現
             if (cnt != undefined) {
                 $('#TextAreaToDo').show();                 
                 $('#TextAreaToDo').html('提醒您！目前有 ' + cnt + ' 筆待辦事項。').css("background-color", "pink");
                 //"<a href='javascript: void(0)' onclick='LinkLog(" + index + ");'>" + value + "</a>";
                 //var EconmicLink = $("<a>").attr({ 'href': 'http://www.gcis.nat.gov.tw/pub/cmpy/cmpyInfoListAction.do'}).attr({'target':'_blank'}).text("    經濟部商業司");
             } else {
                 $('#TextAreaToDo').hide();
             }
         }
         //業務人員combo更新時的事件
         function cbSalesEmployeeIDRefresh() {
             $('#dataGridDetail').datagrid('setWhere', '1=0'); //可以有清空資料的效果
             $("div").data("SalesEmployeeID",$("#cbSalesEmployeeID").combobox('getValue'));
             ShowToDoCount();//代辦事項筆數呈現
             RefreshGrid();             
         }        
         //-------------------------------------新增主檔Dialog----------------------------------------------------------------------
         //呼叫視窗銷貨明細新增
         function OpenInsertSalesDetails() {
             openForm('#Dialog_Master', {}, "inserted", 'dialog');//
         }
         function OnAppliedSalesDetails() {
             RefreshGrid();
         }
         function AddSalesMasterLoad() {                        
             //新增銷貨畫面選單預設
             ControlShowItem();
             ControlSalesType();
         }
         //新增銷貨時取得SalesEmployeeID
         function GetSalesEmployeeID() {
             return $("#cbSalesEmployeeID").combobox('getValue');
         }
         //依據交易別顯示隱藏 求才或報紙 選項
         function ControlShowItem() {
             var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
             //報,版,發,段,社單價,社行,客行,繳社價,客總價
             var HideFieldName = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID', 'Sections', 'OfficePrice', 'OfficeLines', 'CustLines', 'OfficeAmt', 'CustAmt'];
             var FormName = '#dataFormMaster';
             //報紙6=>顯示
             if (SalesTypeID.trim() == "6") {
                 $.each(HideFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').css("color", "red");
                     $(FormName + fieldName).closest('td').prev('td').css("color", "red");
                     $(FormName + fieldName).closest('td').prev('td').show();                     
                     $(FormName + fieldName).closest('td').show();
                 });
                 $("#dataFormMasterDMTypeID").closest('td').prev('td').hide();
                 $("#dataFormMasterDMTypeID").closest('td').hide();
             } else {//求才1=>隱藏
                 $.each(HideFieldName, function (index, fieldName) {
                     $(FormName + fieldName).closest('td').prev('td').hide();
                     $(FormName + fieldName).closest('td').hide();
                 });
                 $("#dataFormMasterDMTypeID").closest('td').prev('td').css("color", "red");

                 $("#dataFormMasterDMTypeID").closest('td').prev('td').show();
                 $("#dataFormMasterDMTypeID").closest('td').show();                
             }
         }
         //主檔OnLoadSuccess
         function MasterOnLoadSuccess() {            
             ControlSalesType();
         }
         //刊登方式顯示隱藏 連登或跳登 選項
         function ControlSalesType() {
                 var PublishType = $("#dataFormMasterPublishType").options('getValue');
                 //連登1,跳登2
                 if (PublishType.trim() == "1") {
                     //連登日期預設明天
                     var dt = new Date();
                     var TDate = new Date($.jbDateAdd('days', 1, dt));
                     $("#dataFormMasterContinueDate").datebox('setValue', $.jbjob.Date.DateFormat(TDate, 'yyyy/MM/dd'));
                     $("#dataFormMasterContinueDate").closest('td').prev('td').css("color", "red");
                     $("#dataFormMasterContinueDate").closest('td').prev('td').show();
                     $("#dataFormMasterContinueDate").closest('td').show();
                     $("#dataFormMasterJumpDate").closest('td').prev('td').hide();
                     $("#dataFormMasterJumpDate").closest('td').hide();
                 } else {//求才1=>隱藏                
                     $("#dataFormMasterContinueDate").closest('td').prev('td').hide();
                     $("#dataFormMasterContinueDate").closest('td').hide();
                     $("#dataFormMasterJumpDate").closest('td').prev('td').css("color", "red");
                     $("#dataFormMasterJumpDate").closest('td').prev('td').show();
                     $("#dataFormMasterJumpDate").closest('td').show();
                 }
         }
         //新增銷貨時檢查必填
         function checkItemNull() {
             var dt = new Date();
             var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
             //連登1,跳登2
             var PublishType = $("#dataFormMasterPublishType").options('getValue');
             if (PublishType.trim() == "1") {                

                 //連登日期
                 var Check = $("#dataFormMasterContinueDate").datebox('getValue');
                 if (Check == "" || Check == undefined) {
                     alert('連登日期未選擇！');
                     return false;
                 }
                 //日期需大於今天日期
                 if (Check <= today) {
                     alert('連登日期需大於今天！');
                     return false;
                 }
                 var TotalSalesQty=parseInt($("#dataFormMasterTotalSalesQty").val(), 0);
                 var Sum = parseInt($("#dataFormMasterPublishCount").val(), 0) + parseInt($("#dataFormMasterPresentCount").val(), 0);
                 if (TotalSalesQty < Sum) {
                     alert('期數錯誤！');
                     return false;
                 }
             } else {
                 //跳登日期
                 var Check = $("#dataFormMasterJumpDate").val();
                 if (Check == "" || Check == undefined) {
                     alert('跳登日期未選擇！');
                     return false;
                 }
                 //跳登起始日期需大於今天日期                 
                 var sJumpDate = $("#dataFormMasterJumpDate").val();
                 var arr = sJumpDate.split("\n");                 
                 if (arr[0] <= today) {
                     alert('跳登起始日期需大於今天！');
                     return false;
                 }
                 var arrCount = arr.length-1;//去掉\n空白
                 var Sum = parseInt($("#dataFormMasterPublishCount").val(),0) + parseInt($("#dataFormMasterPresentCount").val(),0);
                 if (arrCount != Sum) {
                     alert('跳登日期個數錯誤！');
                     return false;
                 }

             }
             var SalesTypeID = $("#dataFormMasterSalesTypeID").combobox('getValue');
             ////報紙6
             if (SalesTypeID.trim() == "6") {
                 //客行,社單價,社行,段
                 var sCheck = ['CustLines', 'OfficePrice', 'OfficeLines', 'Sections'];
                 var sError = "";
                 $.each(sCheck, function (index, fieldName) {
                     var Check = $("#dataFormMaster" + fieldName).val();
                     if (Check == "" || Check == undefined) {
                         sError = "尚有未填寫項目！";
                     }
                 });
                 //報,版,發
                 var sCheck2 = ['NewsTypeID', 'NewsAreaID', 'NewsPublishID'];
                 $.each(sCheck2, function (index, fieldName) {
                     var sCheck2 = $("#dataFormMaster" + fieldName).combobox('getValue');
                     if (sCheck2 == "" || sCheck2 == undefined) {
                         sError = "尚有未選擇項目！";
                     }
                 });
                 if (sError != "") {
                     alert(sError);
                     return false;
                 } else return true;
             } else {
                 //版別
                 var Check = $("#dataFormMasterDMTypeID").combobox('getValue');
                 if (Check == "" || Check == undefined) {
                     alert('版別未選擇！');
                     return false;
                 }
             }
            

         }
         //繳社價=社行*社單價
         function OnBlur_ChangeOfficeAmt() {
             var OfficeLines = $("#dataFormMasterOfficeLines").val();
             var OfficePrice = $("#dataFormMasterOfficePrice").val();
             $("#dataFormMasterOfficeAmt").val(OfficeLines * OfficePrice);
         }
         //客總價=客行*客單價
         function OnBlur_ChangeCustAmt() {
             var CustLines = $("#dataFormMasterCustLines").val();
             var CustPrice = $("#dataFormMasterCustPrice").val();
             $("#dataFormMasterCustAmt").val(CustLines * CustPrice);
         }
         
         //天數提醒(主檔),是否失效(明細)CheckBox
         function genCheckBox(val) {
             if (val != "0")
                 return "<input  type='checkbox' checked='true' />";
             else
                 return "<input  type='checkbox' />";
         }
         //更新近期客戶清單
         function RefreshGrid() {             
                var SalesEmployeeID = $("#cbSalesEmployeeID").combobox('getValue');
                var CustNO = $("#cbCustNO").combobox('getValue');
                var JQDate1 = $("#JQDate1").combo('textbox').val();//datebox("getBindingValue");//datebox("getValue");                
                var JQDate2 = $("#JQDate2").combo('textbox').val();//datebox("getBindingValue");
                var where = $("#dataGridView").datagrid('getWhere');
                where = where + " s.SalesEmployeeID='" + SalesEmployeeID + "' and d.SalesDate between '" + JQDate1 + "' and '" + JQDate2 + "'";
                if (CustNO != "==不拘==") {
                    where = where + " and m.CustNO='" + CustNO + "'";
                }
                $("#dataGridView").datagrid('setWhere', where);                
                
         }
         //主檔過濾條件
         function MastersetWhere() {
             //$('#dataGridView').datagrid("unselectAll");
             //第一次載入
             if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                 RefreshGrid();                 
             }
         }
         //依據選取的客戶更新Detail明細,顯示或隱藏DataGrid Item
         function dataGridViewSelect(rowindex, rowdata) {
             if (rowdata != null && rowdata != undefined) {
                 var SalesMasterNO = rowdata.SalesMasterNO;
                 var where = $("#dataGridDetail").datagrid('getWhere');
                 where = where + "SalesMasterNO=" + SalesMasterNO;
                 $("#dataGridDetail").datagrid('setWhere', where);                                

                 var SalesTypeID = rowdata.SalesTypeID;//交易別
                 ControlGridItem(SalesTypeID.trim());                 
             }
         }
         //顯示或隱藏DataGrid Item
         function ControlGridItem(SalesTypeID) {
             if (SalesTypeID == "6")//報紙
             {
                 $("#dataGridDetail").datagrid('hideColumn', 'DMTypeID'); //版別
                 $("#dataGridDetail").datagrid('hideColumn', 'GrantTypeID'); //贈期
                
                 $("#dataGridDetail").datagrid('showColumn', 'NewsTypeID'); //報
                 $("#dataGridDetail").datagrid('showColumn', 'NewsAreaID'); //版
                 $("#dataGridDetail").datagrid('showColumn', 'NewsPublishID'); //發
                 $("#dataGridDetail").datagrid('showColumn', 'Sections'); //段
                 $("#dataGridDetail").datagrid('showColumn', 'OfficePrice'); //社單價
                 $("#dataGridDetail").datagrid('showColumn', 'OfficeLines'); //社行
                 $("#dataGridDetail").datagrid('showColumn', 'OfficeAmt'); //繳社價
                 $("#dataGridDetail").datagrid('showColumn', 'CustLines'); //客行                

             } else {                 
                 $("#dataGridDetail").datagrid('showColumn', 'DMTypeID'); //版別
                 $("#dataGridDetail").datagrid('showColumn', 'GrantTypeID'); //贈期

                 $("#dataGridDetail").datagrid('hideColumn', 'NewsTypeID'); //報
                 $("#dataGridDetail").datagrid('hideColumn', 'NewsAreaID'); //版
                 $("#dataGridDetail").datagrid('hideColumn', 'NewsPublishID'); //發
                 $("#dataGridDetail").datagrid('hideColumn', 'Sections'); //段
                 $("#dataGridDetail").datagrid('hideColumn', 'OfficePrice'); //社單價
                 $("#dataGridDetail").datagrid('hideColumn', 'OfficeLines'); //社行
                 $("#dataGridDetail").datagrid('hideColumn', 'OfficeAmt'); //繳社價
                 $("#dataGridDetail").datagrid('hideColumn', 'CustLines'); //客行 
             }
         }
         //-------------------------------------明細1----------------------------------------------------------------------

         //新增明細時取得CustNO
         function GetCustNO() {
             var row = $('#dataGridView').datagrid('getSelected');
             return row.CustNO;
         }
        //新增明細時取得SalesMasterNO
        function GetSalesMasterNO() {
            var row = $('#dataGridView').datagrid('getSelected');
            return row.SalesMasterNO;
        }
        
         //更新銷貨明細
        function RefreshDetail() {
            var row = $('#dataGridView').datagrid('getSelected');
            if (row != null) {
                var where = $("#dataGridDetail").datagrid('getWhere');
                where = where + "SalesMasterNO=" + row.SalesMasterNO;//"SalesMasterNO=" + row.SalesMasterNO + "and
                $("#dataGridDetail").datagrid('setWhere', where);
            } else {
                $('#dataGridDetail').datagrid('setWhere', '1=0'); //可以有清空資料的效果
            }
        }
        //明細過濾條件
        function DetailsetWhere() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                RefreshDetail();
            }
        }                                    
         //銷貨明細修改後更新
        function DetailSaveRefresh() {             
            RefreshGrid();
            RefreshDetail();
            //代辦事項筆數
            ShowToDoCount();
        }                                       
    //-------------------------------------銷貨備註維護----------------------------------------------------------------------
        function UpdateSalesDescr() {
            //呼叫視窗修改
            openForm('#Dialog_SalesDescr', $('#dataGridDetail').datagrid('getSelected'), "updated", 'dialog');
        }
        //銷貨備註新增完成
        function OnAppliedDFSalesDescr() {
            RefreshDetail();
            //代辦事項筆數
            ShowToDoCount();
        }
         //-------------------------------------(補單)銷貨明細新增----------------------------------------------------------------------
        //呼叫視窗新增
        function OpenInsertSalesDetailsLast() {
            openForm('#Dialog_SalesDetails', "", "inserted", 'dialog');//
        }
         //
        function AddSalesDetailsLoad() {
            //跳登日期註冊            
            $("#dataFormSalesDetailJumpDate").jbDateBoxMultiple({});            
        }
        function OnLoadSuccessSalesDetail() {            
            //清空                              
            $("#dataFormSalesDetailJumpDate").jbDateBoxMultiple("setData");
        }
                 
         //新增銷貨明細(補單)     
        function InsertSalesDetailsLast() {
            //版別
            var Check = $("#dataFormSalesDetailDMTypeID").combobox('getValue');
            //跳登起始日期需大於今天日期                 
            var sJumpDate = $("#dataFormSalesDetailJumpDate").val();
            var dt = new Date();
            var today = $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')
            var arr = sJumpDate.split("\n");
            if (Check == "" || Check == undefined) {
                alert('版別未選擇！');                
            } else if (sJumpDate == "" || sJumpDate == undefined) {
                alert('日期未選擇！');                
            } else if (arr[0] <= today) {
                alert('日期需大於今天！');
            }else {                            
                var SalesMasterNO = $('#dataGridView').datagrid('getSelected').SalesMasterNO;
                var CustNO = $('#dataGridView').datagrid('getSelected').CustNO;
                var DMTypeID = $("#dataFormSalesDetailDMTypeID").combobox('getValue');
                var sJumpDate = $("#dataFormSalesDetailJumpDate").val();
                //檢查新增銷貨明細(補單)
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                    data: "mode=method&method=" + "CheckERPSalseDetailsLast" + "&parameters=" + SalesMasterNO + "," + sJumpDate,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                    cache: false,
                    async: false,
                    success: function (data) {
                        var rows = $.parseJSON(data);   //將JSon轉會到Object類型提供給Grid顯示
                        if (rows.length > 0) {
                            if (rows[0].checkCount < 0) {
                                alert('日期個數錯誤！');
                            } else {
                                if (rows[0].sAlertDate !="") {
                                    alert('提醒您,銷貨日期:' + rows[0].sAlertDate + "目前已存在！");
                                }
                                //新增銷貨明細(補單)
                                $.ajax({
                                    type: "POST",
                                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPSalseDetails.ERPSalesDetails', //連接的Server端，command
                                    data: "mode=method&method=" + "InsertERPSalseDetailsLast" + "&parameters=" + SalesMasterNO + "," + CustNO + "," + DMTypeID + "," + sJumpDate,  //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                                    cache: false,
                                    async: false,
                                    success: function (data) {
                                        RefreshGrid();
                                        RefreshDetail();
                                        //清空                              
                                        $("#dataFormSalesDetailJumpDate").jbDateBoxMultiple("");
                                        closeForm('#Dialog_SalesDetails');
                                    }
                                });
                            }
                        }
                    }
                });
               
            }
        }
        //新增明細時檢查銷貨日期
        function checkSalesDate(val, row) {
            return false;

            //var row = $('#dataGridDetail').datagrid('getSelected');
            //var SalesDate = row.SalesDate;
            //var dt = new Date();

            //if (SalesDate > $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd')) {
            //    return false;
            //}
            //else {
            //    return true;
            //}
        }

             
     </script> 
    </head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="業務:"></asp:Label>
            <JQTools:JQComboBox ID="cbSalesEmployeeID" runat="server" DisplayMember="SalesName" PanelHeight="150" RemoteName="sERPSalseDetails.infoSalesMan" ValueMember="SalesEmployeeID" Width="50px" OnSelect="cbSalesEmployeeIDRefresh">
            </JQTools:JQComboBox>
            &nbsp;<asp:Label ID="Label3" runat="server" Font-Size="Small" Text="客戶代號:"></asp:Label>
            <JQTools:JQComboBox ID="cbCustNO" runat="server" DisplayMember="CustNO" RemoteName="sERPSalseDetails.infoCustomersAll" ValueMember="CustNO" OnSelect="RefreshGrid">
            </JQTools:JQComboBox>
&nbsp;<asp:Label ID="Label2" runat="server" Font-Size="Small" Text="起訖日期:"></asp:Label>
            <JQTools:JQDateBox ID="JQDate1" runat="server" Width="100px" />
            〜<JQTools:JQDateBox ID="JQDate2" runat="server" />
                             <JQTools:JQTextArea ID="TextAreaToDo" runat="server" Height="20px" Width="220px" />
            <br />
        </div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />

            <script src="../js/jquery.calendar_jb.js"></script>
           

            <JQTools:JQDialog ID="Dialog_Master" runat="server" BindingObjectID="dataFormMaster" Title="新增銷貨" EditMode="Dialog" DialogLeft="30px" DialogTop="10px" Width="700px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPSalesMaster" HorizontalColumnsCount="4" RemoteName="sERPSalseDetails.ERPSalesMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" OnLoadSuccess="MasterOnLoadSuccess" OnApply="checkItemNull" OnApplied="OnAppliedSalesDetails" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Width="80" Visible="False" maxlength="0" Span="1" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客戶代號" Editor="infocombobox" FieldName="CustNO" Width="140" EditorOptions="valueField:'CustNO',textField:'CustShortName',remoteName:'sERPSalseDetails.infoCustomers',tableName:'infoCustomers',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="2" NewRow="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易別" Editor="infocombobox" FieldName="SalesTypeID" Width="140" Visible="True" EditorOptions="valueField:'SalesTypeID',textField:'SalesTypeName',remoteName:'sERPSalseDetails.infoERPSalesType',tableName:'infoERPSalesType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ControlShowItem,panelHeight:200" NewRow="True" Span="4" MaxLength="0" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" FieldName="DMTypeID" Width="140" Visible="True" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位數" Editor="numberbox" FieldName="SalesQty" NewRow="True" ReadOnly="False" Visible="True" Width="50" MaxLength="0" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總單位數" Editor="numberbox" FieldName="TotalSalesQty" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保留天數" Editor="numberbox" FieldName="KeepDays" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="佣金" Editor="numberbox" FieldName="Commission" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刊" Editor="text" FieldName="PublishCount" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="贈" Editor="text" FieldName="PresentCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="贈週報" Editor="text" FieldName="PresentWNewsCount" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="刊登方式" Editor="infooptions" FieldName="PublishType" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="100" EditorOptions="title:'JQOptions',panelWidth:120,remoteName:'',tableName:'',valueField:'',textField:'',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:ControlSalesType,selectOnly:false,items:[{text:'連登',value:'1'},{text:'跳登',value:'2'}]" />
                        <JQTools:JQFormColumn Alignment="left" Caption="連登日期" Editor="datebox" FieldName="ContinueDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="跳登日期" Editor="textarea" FieldName="JumpDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="報" Editor="infocombobox" FieldName="NewsTypeID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsTypeID',textField:'NewsTypeName',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="版" Editor="infocombobox" FieldName="NewsAreaID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsAreaID',textField:'NewsAreaName',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="發" Editor="infocombobox" FieldName="NewsPublishID" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="125" EditorOptions="valueField:'NewsPublishID',textField:'NewsPublishName',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="段" Editor="text" FieldName="Sections" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="社行" Editor="numberbox" FieldName="OfficeLines" MaxLength="0" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_ChangeOfficeAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="社單價" Editor="numberbox" FieldName="OfficePrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" OnBlur="OnBlur_ChangeOfficeAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="繳社價" Editor="numberbox" FieldName="OfficeAmt" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" OnBlur="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客行" Editor="numberbox" FieldName="CustLines" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="50" OnBlur="OnBlur_ChangeCustAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客單價" Editor="numberbox" FieldName="CustPrice" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" OnBlur="OnBlur_ChangeCustAmt" />
                        <JQTools:JQFormColumn Alignment="left" Caption="客總價" Editor="numberbox" FieldName="CustAmt" MaxLength="0" RowSpan="1" Span="1" Visible="True" Width="60" EditorOptions="precision:3" NewRow="False" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨備註" Editor="textarea" FieldName="SalesDescr" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="500" EditorOptions="height:120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustShortName" Editor="text" FieldName="CustShortName" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SalesEmployeeID" Editor="text" FieldName="SalesEmployeeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="天數提醒" Editor="text" FieldName="KeepDaysAlert" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="30" EditorOptions="" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesMasterNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesEmployeeID" DefaultValue="" FieldName="SalesEmployeeID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="SalesDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="PublishType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="PresentWNewsCount" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Commission" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="KeepDaysAlert" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="30" FieldName="KeepDays" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustNO" RemoteMethod="True" ValidateMessage="請選擇客戶" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateMessage="請選擇交易別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PublishCount" RemoteMethod="True" ValidateMessage="請填寫刊登天數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PresentCount" RemoteMethod="True" ValidateMessage="請填寫贈送天數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PresentWNewsCount" RemoteMethod="True" ValidateMessage="請填寫贈週報天數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CustPrice" RemoteMethod="True" ValidateMessage="單價不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="單位數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="TotalSalesQty" RemoteMethod="True" ValidateMessage="總單位數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="KeepDays" RemoteMethod="True" ValidateMessage="保留天數不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Commission" RemoteMethod="True" ValidateMessage="佣金不可空白" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="PublishType" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>

            </JQTools:JQDialog>

            <div class="easyui-layout" style="height: 450px;">
                <div data-options="region:'west',split:true" style="width: 305px; height: 16px;">
                     <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPSalseDetails.ERPSalesMaster" runat="server" AutoApply="True"
                DataMember="ERPSalesMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="近期銷貨清單" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Height="448px" OnLoadSuccess="MastersetWhere" ParentObjectID="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" MaxLength="0" Width="60" ReadOnly="True" Frozen="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="客戶簡稱" Editor="text" FieldName="CustShortName" Format="" MaxLength="0" Width="60" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="text" FieldName="SalesTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="38" />
                    <JQTools:JQGridColumn Alignment="center" Caption="剩餘數" Editor="text" FieldName="UseQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="43" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="text" FieldName="CreateDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="65" />
                    <JQTools:JQGridColumn Alignment="right" Caption="金額" Editor="text" FieldName="SalesAmt" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="43" />
                    <JQTools:JQGridColumn Alignment="center" Caption="總單位數" Editor="text" FieldName="TotalSalesQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="50" />
                    <JQTools:JQGridColumn Alignment="center" Caption="保留天數" Editor="text" FieldName="KeepDays" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="53" />
                    <JQTools:JQGridColumn Alignment="center" Caption="天數提醒" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="KeepDaysAlert" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="53" FormatScript="genCheckBox" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesID" Editor="text" FieldName="SalesID" Format="" MaxLength="0" Width="120" Visible="False" ReadOnly="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80" />
                </Columns>
                <TooItems>                  
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OpenInsertSalesDetails" Text="新增銷貨" />
                </TooItems>
            </JQTools:JQDataGrid>
                </div>
                <div data-options="region:'center'" style="height: 450px;">                                                                      
          <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster0" Title="新增銷貨" EditMode="Continue" DialogLeft="30px" DialogTop="10px" Width="500px">
              <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" data-options="pagination:true,view:commandview" DataMember="ERPSalesDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="" EditMode="Dialog" EditOnEnter="True" Height="447px" InsertCommandVisible="True" MultiSelect="False" OnUpdated="DetailSaveRefresh" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster0" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sERPSalseDetails.ERPSalesMaster" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                  <Columns>
                      <JQTools:JQGridColumn Alignment="right" Caption="SalesMasterNO" Editor="numberbox" FieldName="SalesMasterNO" Format="" Visible="False" Width="50" />
                      <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CustNO" Format="" Visible="False" Width="60" />
                      <JQTools:JQGridColumn Alignment="center" Caption="項次" Editor="text" FieldName="ItemSeq" Format="" ReadOnly="True" Visible="False" Width="59" />
                      <JQTools:JQGridColumn Alignment="center" Caption="交易別" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'SalesTypeID',textField:'SalesTypeID',remoteName:'sERPSalseDetails.infoERPSalesType',tableName:'infoERPSalesType',valueFieldCaption:'SalesTypeID',textFieldCaption:'SalesTypeID',selectOnly:false,checkData:false,columns:[{field:'SalesTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'SalesTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false,onSelect:ControlShowItem" FieldName="SalesTypeID" Format="" Frozen="True" Visible="False" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="版別" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'DMTypeID',textField:'DMTypeID',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',valueFieldCaption:'DMTypeID',textFieldCaption:'DMTypeID',selectOnly:false,checkData:false,columns:[{field:'DMTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'DMTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" FieldName="DMTypeID" Format="" Frozen="True" Width="35" />
                      <JQTools:JQGridColumn Alignment="center" Caption="銷貨日期" Editor="datebox" FieldName="SalesDate" Format="" FormatScript="" Frozen="True" Sortable="True" Width="65" />
                      <JQTools:JQGridColumn Alignment="left" Caption="有效" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsActive" FormatScript="genCheckBox" Frozen="True" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="33" />
                      <JQTools:JQGridColumn Alignment="center" Caption="發票年月" Editor="text" FieldName="InvoiceYM" Format="" Width="57" />
                      <JQTools:JQGridColumn Alignment="center" Caption="贈期" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'GrantTypeID',textField:'GrantTypeID',remoteName:'sERPSalseDetails.infoERPGrantType',tableName:'infoERPGrantType',valueFieldCaption:'GrantTypeID',textFieldCaption:'GrantTypeID',selectOnly:false,checkData:false,columns:[{field:'GrantTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'GrantTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" FieldName="GrantTypeID" Format="" Sortable="True" Width="35" />
                      <JQTools:JQGridColumn Alignment="center" Caption="單位數" Editor="numberbox" FieldName="SalesQty" Format="" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="見刊" Editor="text" FieldName="SalesQtyView" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="33" />
                      <JQTools:JQGridColumn Alignment="center" Caption="報" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'NewsTypeID',textField:'NewsTypeID',remoteName:'sERPSalseDetails.infoERPNewsType',tableName:'infoERPNewsType',valueFieldCaption:'NewsTypeID',textFieldCaption:'NewsTypeID',selectOnly:false,checkData:false,columns:[{field:'NewsTypeID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsTypeName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" FieldName="NewsTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="版" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'NewsAreaID',textField:'NewsAreaID',remoteName:'sERPSalseDetails.infoERPNewsArea',tableName:'infoERPNewsArea',valueFieldCaption:'NewsAreaID',textFieldCaption:'NewsAreaID',selectOnly:false,checkData:false,columns:[{field:'NewsAreaID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsAreaName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" FieldName="NewsAreaID" Format="" Visible="True" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="發" Editor="infocombogrid" EditorOptions="panelWidth:165,valueField:'NewsPublishID',textField:'NewsPublishID',remoteName:'sERPSalseDetails.infoERPNewsPublish',tableName:'infoERPNewsPublish',valueFieldCaption:'NewsPublishID',textFieldCaption:'NewsPublishID',selectOnly:false,checkData:false,columns:[{field:'NewsPublishID',title:'代號',width:40,align:'left',sortable:false},{field:'NewsPublishName',title:'名稱',width:105,align:'left',sortable:false}],cacheRelationText:false" FieldName="NewsPublishID" Format="" Visible="True" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="段" Editor="numberbox" FieldName="Sections" Format="" Visible="True" Width="26" />
                      <JQTools:JQGridColumn Alignment="right" Caption="社單價" Editor="numberbox" FieldName="OfficePrice" Format="" Visible="True" Width="40" />
                      <JQTools:JQGridColumn Alignment="right" Caption="社行" Editor="numberbox" FieldName="OfficeLines" Format="" Visible="True" Width="30" />
                      <JQTools:JQGridColumn Alignment="right" Caption="繳社價" Editor="numberbox" FieldName="OfficeAmt" Format="" Visible="True" Width="40" />
                      <JQTools:JQGridColumn Alignment="center" Caption="客行" Editor="numberbox" FieldName="CustLines" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" />
                      <JQTools:JQGridColumn Alignment="right" Caption="單價" Editor="numberbox" FieldName="CustPrice" Format="" Width="33" />
                      <JQTools:JQGridColumn Alignment="right" Caption="總額" Editor="numberbox" FieldName="CustAmt" Format="" FormatScript="" ReadOnly="False" Width="45" />
                      <JQTools:JQGridColumn Alignment="right" Caption="佣金" Editor="numberbox" FieldName="Commission" Format="" Width="40" />
                      <JQTools:JQGridColumn Alignment="left" Caption="業務代碼" Editor="text" FieldName="SalesEmployeeID" Format="" Visible="False" Width="60" />
                      <JQTools:JQGridColumn Alignment="center" Caption="已收日期" Editor="datebox" FieldName="AcceptDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="62" />
                      <JQTools:JQGridColumn Alignment="center" Caption="提醒日期" Editor="datebox" FieldName="SalesDescrDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65" />
                      <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="CreateBy" Format="" ReadOnly="True" Width="48" />
                      <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM" ReadOnly="True" Width="94" />
                      <JQTools:JQGridColumn Alignment="left" Caption="IsSetInvoice" Editor="text" FieldName="IsSetInvoice" Format="" Visible="False" Width="60" />
                      <JQTools:JQGridColumn Alignment="left" Caption="SalesOutLine" Editor="text" FieldName="SalesOutLine" Format="" Visible="False" Width="60" />
                      <JQTools:JQGridColumn Alignment="left" Caption="IsImport" Editor="text" FieldName="IsImport" Format="" Visible="False" Width="60" />
                  </Columns>
                  <RelationColumns>
                      <JQTools:JQRelationColumn FieldName="SalesMasterNO" ParentFieldName="SalesMasterNO" />
                  </RelationColumns>
                  <TooItems>
                      <JQTools:JQToolItem Enabled="True" Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                      <JQTools:JQToolItem Enabled="True" Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                      <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="OpenInsertSalesDetailsLast" Text="新增銷貨明細" />
                      <JQTools:JQToolItem Enabled="True" Icon="icon-ok" ItemType="easyui-linkbutton" OnClick="UpdateSalesDescr" Text="銷貨備註維護" Visible="True" />
                  </TooItems>
              </JQTools:JQDataGrid>
              <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataGridDetail" FieldName="ItemSeq" />
              <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                  <Columns>
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesTypeID" RemoteMethod="True" ValidateMessage="請選擇交易別" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckMethod="checkSalesDate" CheckNull="False" FieldName="SalesDate" RemoteMethod="False" ValidateMessage="請選擇銷貨日期" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="InvoiceYM" RemoteMethod="True" ValidateMessage="請填寫發票年月" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="CustPrice" RemoteMethod="True" ValidateMessage="請填寫單價" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="CustAmt" RemoteMethod="True" ValidateMessage="請填寫總額" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQty" RemoteMethod="True" ValidateMessage="請填寫單位數" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="SalesQtyView" RemoteMethod="True" ValidateMessage="請填寫見刊" ValidateType="None" />
                      <JQTools:JQValidateColumn CheckNull="True" FieldName="Commission" RemoteMethod="True" ValidateMessage="請填寫佣金" ValidateType="None" />
                  </Columns>
              </JQTools:JQValidate>
              <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" EnableTheming="True">
                  <Columns>
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesMasterNO" DefaultValue="" FieldName="SalesMasterNO" RemoteMethod="False" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetCustNO" FieldName="CustNO" RemoteMethod="False" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQty" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="SalesQtyView" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Commission" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetSalesEmployeeID" FieldName="SalesEmployeeID" RemoteMethod="False" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="CustLines" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficeLines" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Sections" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficeAmt" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="OfficePrice" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsSetInvoice" RemoteMethod="True" />
                      <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsImport" RemoteMethod="True" />
                  </Columns>
              </JQTools:JQDefault>
              <JQTools:JQDataForm ID="dataFormMaster0" runat="server" DataMember="ERPSalesMaster" HorizontalColumnsCount="2" RemoteName="sERPSalseDetails.ERPSalesMaster">
                  <Columns>
                  </Columns>
              </JQTools:JQDataForm>
            </JQTools:JQDialog>

                            <JQTools:JQDialog ID="Dialog_SalesDescr" runat="server" BindingObjectID="dataFormSalesDescr" EditMode="Dialog" Title="銷貨備註維護" DialogLeft="150px" DialogTop="50px" Width="500px">
                                <JQTools:JQDataForm runat="server" ID="dataFormSalesDescr" RemoteName="sERPSalseDetails.ERPSalesDetails" DataMember="ERPSalesDetails" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="1" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="OnAppliedDFSalesDescr" >
                                    <Columns>
                                        <JQTools:JQFormColumn Alignment="left" Caption="ItemSeq" Editor="text" FieldName="ItemSeq" ReadOnly="True" Visible="False" Width="120" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="SalesMasterNO" Editor="text" FieldName="SalesMasterNO" ReadOnly="False" Visible="False" Width="80" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="銷貨備註" Editor="textarea" FieldName="SalesDescr" Visible="True" Width="350" EditorOptions="height:200" />
                                        <JQTools:JQFormColumn Alignment="left" Caption="提醒日期" Editor="datebox" FieldName="SalesDescrDate" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                                    </Columns>
                                </JQTools:JQDataForm>                               
                             </JQTools:JQDialog>
                            <JQTools:JQDialog ID="Dialog_SalesDetails" runat="server" BindingObjectID="dataFormSalesDetail" EditMode="Dialog" Title="新增銷貨明細" DialogLeft="250px" DialogTop="50px" Width="350px" ShowSubmitDiv="False">
                                <table style="width:100%;">
                                    <tr>
                                        <td>
                                            <JQTools:JQDataForm ID="dataFormSalesDetail" runat="server" Closed="False" ContinueAdd="False" DataMember="ERPSalesDetails" disapply="False" DuplicateCheck="False" HorizontalColumnsCount="3" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedDFSalesDescr" OnLoadSuccess="OnLoadSuccessSalesDetail" RemoteName="sERPSalseDetails.ERPSalesDetails" ShowApplyButton="False" ValidateStyle="Hint" ParentObjectID="">
                                                <Columns>
                                                    <JQTools:JQFormColumn Alignment="left" Caption="版別" Editor="infocombobox" EditorOptions="valueField:'DMTypeID',textField:'DMTypeName',remoteName:'sERPSalseDetails.infoERPDMType',tableName:'infoERPDMType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="DMTypeID" NewRow="True" ReadOnly="False" Span="1" Visible="True" Width="140" />
                                                    <JQTools:JQFormColumn Alignment="left" Caption="日期" Editor="textarea" FieldName="JumpDate" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="100" />
                                                </Columns>
                                            </JQTools:JQDataForm>
                                        </td>
                                        <td style="vertical-align: bottom">
                                            <input id="bnAssign" type="button" value="新增銷貨" onclick="InsertSalesDetailsLast()"/>
                                        </td>
                                    </tr>
                                </table>
                             </JQTools:JQDialog>                           
                </div>
             </div>
               
           
    </form>
</body>
</html>
