<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADM_PerformBonus.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var Dialog_Import_ID = '#Dialog_Import';
        var Dialog_ImportMain_ID = '#Dialog_ImportMain';
        var DataForm_ImportMain_ID = '#DataForm_ImportMain';
        $(document).ready(function () {
            var backcolor = "#ffffff"
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", backcolor);
                });
               
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
                            //$.messager.alert(' ', "匯入成功");
                            $("#dataGridDetail").datagrid('setWhere', FiltStr);
                            $('#Dialog_Import').dialog('close');
                            $('#Dialog_ImportMain').dialog('close');
                            var PerfBonusNO = $("#dataFormMasterPerfBonusNO").val();
                            var FiltStr = " PerfBonusNO = '" + PerfBonusNO + "'";
                            setTimeout(function () {
                                $("#dataGridDetail").datagrid('setWhere', FiltStr);
                                $('#divPerfBonusDetails').show();
                            }, 1000);
                            
                           
                        }
                    }
                });
                //-----------------------------------欄位配對視窗送出按鈕----------------------------
                $('#DialogSubmit', '#Dialog_ImportMain').removeAttr('onclick').on('click', function () {
                    if (!$('#DataForm_ImportMain').form('validateForm')) return;            
                    //設定申請單號
                    var PerfBonusNO = $("#DataForm_BonusMasterPerfBonusNO").val();
                    if (PerfBonusNO != "") {
                        var pre = confirm("確定匯入?");
                        if (pre == true) {
                            var voucherObject = $('#DataForm_BonusMaster').jbDataFormGetAFormData();
                            var titleObject = $('#DataForm_ImportMain').jbDataFormGetAFormData();
                            $('#Dialog_Import').jbExcelFileImport('importFile', {
                                remoteName: 'sPerformBonus', //
                                method: 'ExcelFileImport',
                                sheetIndex: $('#DataForm_SheetImportMainSHEET').combobox('getValue'),
                                titleObject: titleObject,
                                parameters: $.toJSONString(voucherObject)
                            });
                        }
                    }
                });
            });
            //設定Excel匯入按鈕
            //var dfRawExcel = $('#dataFormMasterRawExcel').closest('td');
            //dfRawExcel.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
            //    if (IsNullPerfBonusYM() == true) {
            //        alert('提示!!績效年月未選取');
            //        $("##dataFormMasterPerfBonusYM").focus();
            //        return false;
            //    }
            //    if (IsNullRawExcel()==true) {
            //        alert('提示!!上傳檔案未選取');
            //        $("#dataFormMasterRawExcel").focus();
            //        return false;
            //    }
            //    //開啟Excel匯入畫面
            //    openImportExcel();
            //    return true;
            //}).linkbutton({ text: '匯入EXCEL' }));
        });
        //檢查績效年月是否未填
        function IsNullPerfBonusYM() {
            var _return = false;
            var PerfBonusYM = $("#dataFormMasterPerfBonusYM").combobox('getValue');
            if ((PerfBonusYM == '') || (PerfBonusYM == 'undefined')) {
                _return = true;
            }
            return _return;
        }
        //檢查是否上傳檔案
        function IsNullRawExcel() {
            var _return = false;
            var RawExcel = $("#infoFileUploaddataFormMasterRawExcel").val();
            if ((RawExcel == '') || (RawExcel == 'undefined')) {
                _return = true;
            }
            return _return;
        }
        function PerfBonusYMOnSelect(rowData) {
            var Org_NOParent = $("#dataFormMasterOrg_NOParent").combobox('getValue');
            var Org_NOParentName = $("#dataFormMasterOrg_NOParent").combobox('getText');
            var PerfBonusYM = $("#dataFormMasterPerfBonusYM").combobox('getValue');
            if (CheckIsDuplicateApply(Org_NOParent,PerfBonusYM)){
                alert('注意!!' + Org_NOParentName + ':' + PerfBonusYM + '績效獎金已申請過,請確認')
                setTimeout(function () {
                   $("#dataFormMasterPerfBonusYM").combobox('setValue', '');
                }, 500);
                $("#dataFormMasterPerfBonusYM").focus();
                return false;
            }
            $("#dataFormMasterSalaryYM").val(rowData.SalaryYM);
        }
        function dataFormMaster_OnLoadSuccess() {
            $('#divPerfBonusDetails').hide();
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                
                var orgno = GetUserOrgNOs();
                var OrgParentName = "";
                $("#dataFormMasterOrg_NOParent").combobox('setValue', orgno);
                setTimeout(function () {
                OrgParentName = $("#dataFormMasterOrg_NOParent").combobox('getText');
                    $("#dataFormMasterOrgParentName").val(OrgParentName);
                }, 500);
                var PerfBonusNO = GetPerfBonusNO();
                $("#dataFormMasterPerfBonusNO").val(PerfBonusNO);
                setTimeout(function () {
                var PBY = GetPerfBonusYM();
                $("#dataFormMasterPerfBonusYM").combobox('setWhere', "PerfBonusYM = '" + PBY + "'");
                }, 1000);
                //將申請單號給轉EXECEL
                $("#DataForm_BonusMasterPerfBonusNO").val(PerfBonusNO);
                $('#toolItemdataGridDetail存檔').hide();
             }
            else {
                //顯示下載連結
                $("#downloadRawExcel").remove();
                var RawExcel = $('.info-fileUpload-value', $("#dataFormMasterRawExcel").next()).val();
                if (RawExcel != '') {
                    var link = $("<a download>").attr({ 'id': 'downloadRawExcel', 'href': '../JB_ADMIN/PerformBonus/' + RawExcel }).html('[下載]');
                    $('#dataFormMasterRawExcel').closest('td').append(link);
                }
                setTimeout(function () {
                var PerfBonusNO = $("#dataFormMasterPerfBonusNO").val();
                var FiltStr = " PerfBonusNO = '" + PerfBonusNO + "'";
                $("#dataGridDetail").datagrid('setWhere', FiltStr);
                }, 500);
                $("#dataGridDetail").datagrid('hideColumn', 'ErrorMsg');
                $('#divPerfBonusDetails').show();
                $('#toolItemdataGridDetail存檔').show();
            }
        }
        //
        function dataFormMaster_OnApply() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var OrgParentName = $("#dataFormMasterOrg_NOParent").combobox('getText');
                $("#dataFormMasterOrgParentName").val(OrgParentName);
                var rows = $("#dataGridDetail").datagrid('getRows').length;
                if (rows < 1) {
                    alert('注意!!未轉入任何績效紀錄,請重新轉入');
                    return false;
                }
                var PerfBonusYM = $("#dataFormMasterPerfBonusYM").combobox('getValue');
                if (PerfBonusYM == '' || PerfBonusYM == 'undefined') {
                    alert('注意!!績效年月未選取,請選取');
                    return false;
                }
                var cou = GetErrCount();
                if (cou > 0) {
                    alert('提示!!有' + cou + '筆資料工號或姓名有誤,請重新轉入');
                    return false;
                }

            }
        }
       //檢查績效是否重複簽核
         function CheckIsDuplicateApply(Org_NOParent,PerfBonusYM) {
                    var UserID = getClientInfo("UserID");
                    var cnt;
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sPerformBonus.PerfBonusMaster',
                        data: "mode=method&method=" + "CheckIsDuplicateApply" + "&parameters=" + Org_NOParent + "," + PerfBonusYM + "," + UserID, 
                        cache: false,
                        async: false,
                        success: function (data) {                   
                            cnt = data;
                        }
                    });
                    if ((cnt == "Y")) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
        function GetErrCount() {
            var errcou = 0;
            var count = $("#dataGridDetail").datagrid('getRows').length-1;
            var rows = $("#dataGridDetail").datagrid('getRows');
            for (i = 0; i <= count; i++) {
                if (rows[i].IsActive == false) {
                    errcou = errcou + 1;
                }
            }
            return errcou;
        }
        function GetPerfBonusYM() {
            var dt = new Date();
            var year = dt.getFullYear();
            var month = dt.getMonth();
            if (month == 0)
            {
                year=(dt.getFullYear()-1);
                month = 12;
            }
            if (month < 10) {
                month = "0" + month;
            }
            return year.toString() + month.toString();
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var _return = '';
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPerformBonus.PerfBonusMaster',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        _return = rows[0].OrgNO;
                    }
                }
            }
            );
            return _return;
        }
            //取得PerfBonusNO
            function GetPerfBonusNO() {
                var ReturnStr = "";
                var dts = dateToStr();
                var ReturnStr = "PB" + dts;
                return ReturnStr;
            }
            //========================================匯入Excel=======================================
            var openImportExcel = function () {
                $(Dialog_Import_ID).dialog('open');
            }
            //---------------------------------------匯入Excel Sheet切換------------------------------
            var DataForm_SheetImportMainSHEET_OnSelect = function (rowData) {
                $('#Dialog_Import').jbExcelFileImport('changeSheetByIndex', rowData.value);
            }
            //將日期
            function dateToStr() {
                var dateTime = new Date();
                var year = dateTime.getFullYear();
                var month = dateTime.getMonth() + 1;//js從0開始取
                var date = dateTime.getDate();
                var hour = dateTime.getHours();
                var minutes = dateTime.getMinutes();
                var second = dateTime.getSeconds();
                year = "0" + year;
                if (month < 10) {
                    month = "0" + month;
                }
                if (date < 10) {
                    date = "0" + date;
                }
                if (hour < 10) {
                    hour = "0" + hour;
                }
                if (minutes < 10) {
                    minutes = "0" + minutes;
                }
                if (second < 10) {
                    second = "0" + second;
                }
                return year.trim().substr(3, 2) + month + date + hour + minutes + second;
            }
            function RawExcelOnSuccess() {
                setTimeout(function () {
                    openImportExcel();
                }, 500);
                return true;
            }
     
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPerformBonus.PerfBonusMaster" runat="server" AutoApply="True"
                DataMember="PerfBonusMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusNO" Editor="text" FieldName="PerfBonusNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusYM" Editor="text" FieldName="PerfBonusYM" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalaryYM" Editor="text" FieldName="SalaryYM" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="RawExcel" Editor="text" FieldName="RawExcel" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="月部門績效獎金簽核" Width="900px" DialogLeft="20px" DialogTop="20px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PerfBonusMaster" HorizontalColumnsCount="4" RemoteName="sPerformBonus.PerfBonusMaster" OnLoadSuccess="dataFormMaster_OnLoadSuccess" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="dataFormMaster_OnApply" >
                   <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核單號" Editor="text" FieldName="PerfBonusNO" Format="" Width="110" ReadOnly="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人員" Editor="infocombobox" FieldName="ApplyEmpID" Format="" Width="90" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sPerformBonus.EmpList',tableName:'EmpList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="90" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="Org_NOParent" Format="" Width="125" maxlength="0" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPerformBonus.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="績效年月" Editor="infocombobox" FieldName="PerfBonusYM" MaxLength="0" Width="115" EditorOptions="valueField:'PerfBonusYM',textField:'PerfBonusYM',remoteName:'sPerformBonus.PerfBonusYM',tableName:'PerfBonusYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:PerfBonusYMOnSelect,panelHeight:200" Format="" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪資年月" Editor="text" FieldName="SalaryYM" Format="" maxlength="0" Width="83" EditorOptions="" ReadOnly="True" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" FieldName="RawExcel" Width="120" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/PerformBonus/',showButton:true,showLocalFile:false,onSuccess:RawExcelOnSuccess,fileSizeLimited:'2048'" Span="2" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="OrgParentName" Editor="text" FieldName="OrgParentName" maxlength="0" Span="1" Width="80" Visible="False" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="BonusAmt" Editor="text" FieldName="BonusAmt" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>  
                </JQTools:JQDataForm>
               <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="BonusAmt" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <div id="divPerfBonusDetails">
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PerfBonusDetails" Pagination="True" ParentObjectID="" RemoteName="sPerformBonus.PerfBonusDetails" Title="績效獎金明細" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="20,40,60" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="合計:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="810px" >
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusNO" Editor="text" FieldName="PerfBonusNO" Format="" Width="120" Visible="False" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="訊息" Editor="text" FieldName="ErrorMsg" ReadOnly="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="員工工號" Editor="text" FieldName="EmpID" Format="" Width="55" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="EmpName" Format="" Width="70" ReadOnly="True" Visible="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="績效金額" Editor="numberbox" FieldName="BonusAmt" Format="N0" Width="70" Visible="True" Total="sum" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="right" Caption="調整金額" Editor="numberbox" FieldName="AdjustAmt" Format="N0" Width="70" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="調整事由" Editor="text" FieldName="AdjustNote" Visible="True" Width="320" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PerfBonusNO" ParentFieldName="PerfBonusNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="存檔" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
            </JQTools:JQDialog>
        </div>
        <div id="Dialog_Import"></div>
        <JQTools:JQDialog ID="Dialog_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" Title="欄位配對" ShowModal="True" EditMode="Dialog" DialogLeft="" DialogTop="" Width="780px">
            <JQTools:JQDataForm ID="DataForm_BonusMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="PerfBonusMaster" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sPerformBonus.PerfBonusMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="PerfBonusNO" Editor="text" FieldName="PerfBonusNO" MaxLength="0" Width="80" NewRow="False" Visible="False" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_SheetImportMain" runat="server" DataMember=" " HorizontalColumnsCount="3" RemoteName=" " Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="工作表" Editor="infocombobox" FieldName="SHEET" Width="120" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,onSelect:DataForm_SheetImportMainSHEET_OnSelect,panelHeight:200" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQDataForm ID="DataForm_ImportMain" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="PerfBonusImport" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="3" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sPerformBonus.PerfBonusImport" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                <Columns>
                    <JQTools:JQFormColumn Alignment="left" Caption="員工工號" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="EmpID" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="員工姓名" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="EmpName" Width="100" />
                    <JQTools:JQFormColumn Alignment="left" Caption="獎金金額" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="BonusAmt" Width="100" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" />
                </Columns>
            </JQTools:JQDataForm>
            <JQTools:JQValidate ID="Validate_ImportMain" runat="server" BindingObjectID="DataForm_ImportMain" EnableTheming="True">
            </JQTools:JQValidate>
        </JQTools:JQDialog>
      <%--  <JQTools:JQDialog ID="JQDialogImportInfo" runat="server" DialogLeft="200px" DialogTop="100px" Title="匯入紀錄查詢" ShowSubmitDiv="False" Width="400px">
                <JQTools:JQDataGrid ID="JQGridImportInfo" runat="server" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="infoImportInfo" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTitle="" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sHumanImport.infoImportInfo" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" Width="320px" BufferView="False" NotInitGrid="False" RowNumbers="True" Height="300px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="匯入日期" Editor="datebox" FieldName="cDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="95" Format="yyyy/mm/dd HH:MM" />
                        <JQTools:JQGridColumn Alignment="center" Caption="匯入人員" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="90" />
                        <JQTools:JQGridColumn Alignment="right" Caption="匯入筆數" Editor="numberbox" FieldName="iCount" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Total="sum" Visible="True" Width="77" />
                    </Columns>
                    
                </JQTools:JQDataGrid>
        </JQTools:JQDialog>--%>
    </form>
</body>
</html>
