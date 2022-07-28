<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMServiceRecord.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>  

         $(document).ready(function () {                       
           
             $("#CompanyID_Query").combobox('setValue', 1);
             GetQEmployer();//雇主

             //Grid選取單選,checkbox多選
             $("#dataGridDetail").datagrid({
                 singleSelect: true,
                 selectOnCheck: false,
                 checkOnSelect: false
             });

         });

         //---------------------------------------呼叫Method---------------------------------------
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = null;
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sFWCRMServiceRecord',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };

         //---------------------------------------dataGrid Query---------------------------------
         //---------------------------------------選公司別觸發---------------------------------
         var QCompanyID_OnSelect = function (rowdata) {
             GetQEmployer();//雇主
             $('#EmployerID_Query').combobox('setValue', "");

         }

         //得到雇主
         var GetQEmployer = function (CompanyID) {
             //公司別
             var CompanyID = $("#CompanyID_Query").combobox('getValue');
             var EmployerID = $("#EmployerID_Query").combobox('getValue');//雇主	

             var CodeList = GetDataFromMethod('GetEmployer', { Company_ID: CompanyID, Employer_ID: EmployerID });
             if (CodeList != null) {
                 $("#EmployerID_Query").combobox('loadData', CodeList);//雇主
             }
         }
         //---------------------------------------DataForm---------------------------------

         //---------------------------------------選報表種類觸發---------------------------------
         var OnSelect_RecordType = function (rowdata) {
             $('#dataGridDetail').datagrid('loadData', []); //清空資料 
             $('#dataFormMasterNationalityID').combobox('setValue', "");
             $('#dataFormMasterEmployerID').combobox('setValue', "");

         }  
        //---------------------------------------選公司別觸發---------------------------------
         var CompanyID_OnSelect = function (rowdata) {
             GetEmployer();//雇主
             $('#dataGridDetail').datagrid('loadData', []); //清空資料 
             $('#dataFormMasterNationalityID').combobox('setValue', "");
        }
      
         //得到雇主
        var GetEmployer = function (CompanyID) {
            //公司別
            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');
            var EmployerID = $("#dataFormMasterEmployerID").combobox('getValue');//雇主	

            var CodeList = GetDataFromMethod('GetEmployer', { Company_ID: CompanyID, Employer_ID: EmployerID });
            if (CodeList != null) {
                $("#dataFormMasterEmployerID").combobox('loadData', CodeList);//雇主
            }
        }
         //---------------------------------------選雇主觸發---------------------------------
        var EmployerID_OnSelect = function (rowdata) {
            $('#dataGridDetail').datagrid('loadData', []); //清空資料 
            $('#dataFormMasterNationalityID').combobox('setValue', "");
            GetNational();//國籍
        }

         //得到國籍
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

         //載入外勞名單
        function RunEmployee() {

            var CompanyID = $("#dataFormMasterCompanyID").combobox('getValue');//公司別 1傑報人力 ,2傑信管理, 3傑信家服
            var EmployerID = $("#dataFormMasterEmployerID").combobox('getValue');//雇主	
            var NationalityID = $("#dataFormMasterNationalityID").combobox('getValue');//國籍		
            var iMonth = $("#dataFormMasteriMonth").val();//離職月數		

            if (EmployerID == "0") {
                alert("請選擇雇主！");
                return false;
            }
            var dataGrid = $('#dataGridDetail');
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMServiceRecord.FWCRMServiceRecordMaster',  //連接的Server端，command
                data: "mode=method&method=" + "getEmployeeData" + "&parameters=" + CompanyID + "," + EmployerID + "," + NationalityID + "," + iMonth,
                cache: false,
                async: true,
                success: function (data) {
                    var rows = $.parseJSON(data);//將JSon轉會到Object類型提供給Grid顯示                            
                    //$('#dataGridDetail').datagrid('loadData', rows);//通過loadData方法清除掉原有Grid中的舊有資料並填補新資料
                    if (rows != null && rows.length > 0) {
                        dataGrid.datagrid('loadData', { "total": 0, "rows": [] });
                        data = eval('(' + data + ')');
                        var appandRows = [];
                        var UserID = getClientInfo("UserID");
                        var UserName = getClientInfo("UserName");
                        var today = getClientInfo('_today')
                        for (var j = 0; j < data.length; j++) {
                            appandRows.push({ iAutokey: j, RecordNo: "0", EmployeeID: data[j].EmployeeID, EmployeeTcName: data[j].EmployeeTcName, EffectDate: data[j].EffectDate, EffectDate2: data[j].EffectDate2, CreateBy: UserName, CreateDate: today, LastUpdateBy: UserName, LastUpdateDate: today });
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
        //---------------------------------------外勞批次刪除-----------------------------------------------------
        function deleteMore() {
            var dataGrid = $('#dataGridDetail');

            if (dataGrid.datagrid('getChecked').length == 0) {
                alert('請勾選外勞。');
            } else {

                var pre = confirm("確定刪除?");
                if (pre == true) {
                    var allRows = dataGrid.datagrid('getRows');
                    var chRows = dataGrid.datagrid("getChecked");

                    for (var i = 0; i < chRows.length; i++) {
                        for (var j = 0; j < allRows.length; j++) {
                            if (allRows[j].EmployeeID == chRows[i].EmployeeID) {
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
                alert('無外勞。');
                return false;
            }
        }
        function OnLoadFormMaster() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#bRunEmployee").show();//顯示查詢紐
                //属性删除
                $("#dataFormMasterRecordType").options('enable');
                $("#dataFormMasterCompanyID").combobox().removeAttr("disabled");
                $("#dataFormMasterEmployerID").combobox().removeAttr("disabled");
                $("#dataFormMasterNationalityID").combobox().removeAttr("disabled");
                $("#dataFormMasteriMonth").numberbox().removeAttr("disabled");

            } else {
                //属性不可編輯
                $("#dataFormMasterRecordType").options('disable');
                $("#dataFormMasterCompanyID").combobox('disable');
                $("#dataFormMasterEmployerID").combobox('disable');
                $("#dataFormMasterNationalityID").combobox('disable');
                $("#dataFormMasteriMonth").numberbox('disable');

                GetEmployer();//雇主
                GetNational();//國籍
                $("#bRunEmployee").hide();//隱藏查詢紐
            }
        }

        function OnLoadDetail() {
            //選擇外勞...
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

        function OnAppliedFormMaster() {
            queryGrid($('#dataGridView'));

            if (getEditMode($("#dataFormMaster")) == "inserted") {
                AddISODocument();//--到ISO 表單取號新增資料
            }
        }
        function queryGrid(dg) {//查詢後添加固定條件
            if ($(dg).attr('id') == 'dataGridView') {
                //查詢條件
                var result = [];
                var CompanyID = $('#CompanyID_Query').combobox('getValue');//公司別
                var RecordType = $('#RecordType_Query').combobox('getValue');//報表種類
                var EmployerID = $('#EmployerID_Query').combobox('getValue');//雇主  
                var NationalityID = $('#NationalityID_Query').combobox('getValue');//國籍                             

                var RecordNoShow = $('#RecordNoShow_Query').val();//表單編號
                var CreateDateS = $('#CreateDateS_Query').datebox('getValue');//建立日期
                var CreateDateE = $('#CreateDateE_Query').datebox('getValue');//建立日期

                if (CompanyID != '') result.push("y.CompanyID = '" + CompanyID + "'");
                if (RecordType != '') result.push("[FWCRMServiceRecordMaster].RecordType = '" + RecordType + "'");
                if (EmployerID != '' && EmployerID != '0') result.push("r.EmployerID = '" + EmployerID + "'");
                if (NationalityID != '') result.push("[FWCRMServiceRecordMaster].NationalityID = " + NationalityID );

                if (RecordNoShow != '') result.push("[FWCRMServiceRecordMaster].RecordNoShow like '%" + RecordNoShow + "%'");
                if (CreateDateS != '') result.push("Convert(nvarchar(10),[FWCRMServiceRecordMaster].CreateDate,111) between '" + CreateDateS + "' and '" + CreateDateE + "'");

                $(dg).datagrid('setWhere', result.join(' and '));
            }
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

         //-------------到ISO 表單取號新增資料----------------------------------------
        function AddISODocument() {
            var CompanyID = $('#dataFormMasterCompanyID').combobox('getValue');//公司別
            var EmployerID = $('#dataFormMasterEmployerID').combobox('getValue');//雇主	
            var NationalityID = $('#dataFormMasterNationalityID').combobox('getValue');//國籍	
            var Remark = $('#dataFormMasterRemark').val();//處理事項	

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sFWCRMServiceRecord.FWCRMServiceRecordMaster', //連接的Server端，command
                data: "mode=method&method=" + "AddISODocument" + "&parameters=" + CompanyID + "*" + EmployerID + "*" + NationalityID + "*" + Remark, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                cache: false,
                async: false,
                success: function (data) {
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    alert(xhr.status);
                    alert(thrownError);
                }
            });

        }
        //-------------Report ---------------------------------------------------------------------------------------------------       
        //搜尋結果Grid => 開啟報表連結       
        function LinkReport(val, row, index) {
            var FileName = row.EmployerName;
            var RecordType = row.RecordType;
            var RecordNo = row.RecordNo;
            var CompanyID = row.CompanyID;
            return $('<a>', { href: "#", onclick: "OpenReport('" + FileName + "','" + RecordType + "','" + RecordNo + "','" + CompanyID + "')", theData: row.RecordNo }).linkbutton({ text: "<img src=img/Record.png></a><b><div style=\"color:Red\"></div></b>", plain: true })[0].outerHTML;
        }
        function OpenReport(FileName, RecordType, RecordNo, CompanyID) {
            var url = "../JB_ADMIN/REPORT/FWCRM/ServiceRecordReport.aspx?FileName=" + FileName + "&RecordType=" + RecordType + "&RecordNo=" + RecordNo + "&CompanyID=" + CompanyID;

            var height = $(window).height() - 50;
            var height2 = $(window).height() - 90;
            var width = $(window).width() - 200;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                //top:0,
                width: width,
                title: "服務紀錄表",
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="98%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');

        }

     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMServiceRecord.FWCRMServiceRecordMaster" runat="server" AutoApply="True"
                DataMember="FWCRMServiceRecordMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="服務紀錄單" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnUpdate="RecordUpdateRow">
                <Columns>
                     <JQTools:JQGridColumn Alignment="center" Caption="報表" Editor="text" FieldName="LinkReport" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="50" FormatScript="LinkReport"></JQTools:JQGridColumn>
                     <JQTools:JQGridColumn Alignment="center" Caption="報表種類" Editor="text" FieldName="RecordTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="表單編號" Editor="text" FieldName="RecordNoShow" Format="" Visible="true" Width="70" Sortable="True" />
                     <JQTools:JQGridColumn Alignment="left" Caption="RecordNo" Editor="text" FieldName="RecordNo" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="text" FieldName="CompName" Format="" Visible="true" Width="80" DrillObjectID="" EditorOptions="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="雇主" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Visible="true" Width="160" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="國籍" Editor="text" FieldName="NationalityText" Format="" Visible="true" Width="65" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="取號備註" Editor="text" FieldName="Remark" Format="" MaxLength="0" Visible="True" Width="170" />
                     <JQTools:JQGridColumn Alignment="center" Caption="ISO編號" Editor="text" FieldName="FormNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="65">
                     </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="有效?" Editor="checkbox" FieldName="IsActive" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="sCheckBox">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="建立者" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Visible="true" Width="65" />
                    <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="80" />
                     <JQTools:JQGridColumn Alignment="left" Caption="RecordType" Editor="text" FieldName="RecordType" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                     </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增服務紀錄" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
<%--                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="公司別" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompName',remoteName:'sFWCRMServiceRecord.infoCompanyID',tableName:'infoCompanyID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:QCompanyID_OnSelect,panelHeight:120" FieldName="CompanyID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="雇主" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:EmployerID_OnSelect,panelHeight:150" FieldName="EmployerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="190" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="報表種類" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'RecordTypeID',textField:'RecordTypeName',remoteName:'sFWCRMServiceRecord.infoFWCRMRecordType',tableName:'infoFWCRMRecordType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:120" FieldName="RecordType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="國籍" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ListID',textField:'ListContent',remoteName:'sFWCRMServiceRecord.infoNationalityID',tableName:'infoNationalityID',pageSize:'-1',checkData:true,selectOnly:true,cacheRelationText:false,panelHeight:150" FieldName="NationalityID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="表單編號" Condition="%" DataType="string" Editor="text" FieldName="RecordNoShow" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="建立日期" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="～" Condition="%" DataType="string" Editor="datebox" FieldName="CreateDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="85" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="新增服務紀錄" Width="900px" DialogLeft="50px" DialogTop="5px" Height="480px">
                <table style="width:100%;">
                    <tr>
                        <td>
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMServiceRecordMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApplied="OnAppliedFormMaster" OnApply="checkApplyFormMaster" OnLoadSuccess="OnLoadFormMaster" RemoteName="sFWCRMServiceRecord.FWCRMServiceRecordMaster" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="表單編號" Editor="text" FieldName="RecordNo" Format="" ReadOnly="True" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="RecordNoShow" Editor="text" FieldName="RecordNoShow" ReadOnly="False" Span="1" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="報表種類" Editor="infooptions" EditorOptions="title:'JQOptions',panelWidth:150,remoteName:'sFWCRMServiceRecord.infoFWCRMRecordType',tableName:'infoFWCRMRecordType',valueField:'RecordTypeID',textField:'RecordTypeName',columnCount:2,multiSelect:false,openDialog:false,selectAll:false,onSelect:OnSelect_RecordType,selectOnly:false,items:[]" FieldName="RecordType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompName',remoteName:'sFWCRMServiceRecord.infoCompanyID',tableName:'infoCompanyID',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:CompanyID_OnSelect,panelHeight:200" FieldName="CompanyID" maxlength="0" Span="1" Visible="True" Width="95" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="雇主" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,onSelect:EmployerID_OnSelect,panelHeight:150" FieldName="EmployerID" Format="" maxlength="0" ReadOnly="False" Width="200" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="離職月數" Editor="numberbox" FieldName="iMonth" MaxLength="0" Span="1" Visible="True" Width="50" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="國籍" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:150" FieldName="NationalityID" Format="" Span="1" Visible="True" Width="90" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="有效?" Editor="checkbox" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="60" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="取號備註" Editor="text" FieldName="Remark" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="250" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="處理事項" Editor="textarea" EditorOptions="height:40" FieldName="ProcessItem" Format="" maxlength="0" Span="4" Visible="True" Width="560" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" ReadOnly="False" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                </Columns>
                            </JQTools:JQDataForm>
                        </td>
                        <td style="vertical-align: bottom; text-align: left;"><a ID="bRunEmployee" class="easyui-linkbutton" href="#" onclick="RunEmployee()">查詢</a></td>
                    </tr>
                </table>
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="FWCRMServiceRecordDetails" DeleteCommandVisible="False" DuplicateCheck="True" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" Height="280px" InsertCommandVisible="True" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sFWCRMServiceRecord.FWCRMServiceRecordMaster" RowNumbers="True" Title="外勞設定→( 不要的請勾選後刪除 )" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="570px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="RecordNo" Editor="text" FieldName="RecordNo" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="center" Caption="外勞編號" Editor="text" FieldName="EmployeeID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="110" />
                        <JQTools:JQGridColumn Alignment="center" Caption="外勞姓名" Editor="text" FieldName="EmployeeTcName" Width="130" />
                        <JQTools:JQGridColumn Alignment="center" Caption="到職日期" Editor="datebox" FieldName="EffectDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="center" Caption="離職日期" Editor="text" FieldName="EffectDate2" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="iAutokey" Editor="text" FieldName="iAutokey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="RecordNo" ParentFieldName="RecordNo" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Enabled="True" Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteMore" Text="刪除外勞" Visible="True" />
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增外勞" />
                        <%--                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />--%>
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="150px" DialogTop="50px" Title="新增外勞" Width="300px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="FWCRMServiceRecordDetails" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnLoadSuccess="OnLoadDetail" ParentObjectID="dataFormMaster" RemoteName="sFWCRMServiceRecord.FWCRMServiceRecordMaster" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="選擇外勞" Editor="inforefval" EditorOptions="title:'請選擇',panelWidth:350,remoteName:'sFWCRMServiceRecord.infoEmployeeID',tableName:'infoEmployeeID',columns:[],columnMatches:[],whereItems:[],valueField:'EmployeeID',textField:'EmployeeTcName',valueFieldCaption:'外勞編號',textFieldCaption:'外勞姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" FieldName="EmployeeID" Format="" Width="120" />
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
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployeeID" RemoteMethod="True" ValidateMessage="請選擇外勞" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCode" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERNAME" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="RecordNo" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="IsActive" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="12" FieldName="iMonth" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerID" RemoteMethod="True" ValidateMessage="請選擇雇主" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="NationalityID" RemoteMethod="True" ValidateMessage="請選擇國籍" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="iMonth" RemoteMethod="True" ValidateMessage="請填寫離職月數" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RecordType" RemoteMethod="True" ValidateMessage="請選擇報表種類" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Remark" RemoteMethod="True" ValidateMessage="取號備註不可空白！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
