<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_WorkRecordApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script>
        $(document).ready(function () {
            $('.infosysbutton-q', '#querydataGridView').closest('td').attr({ 'align': 'middle' });
            $('#querydataGridView').find(".infosysbutton-cl").hide();
            $('#querydataGridView').find(".infosysbutton-q").hide();
            
            //刪除按鈕
            //var clearBtn1 = $('<a>', { href: 'javascript:void(0)', onclick: 'clearBtn1_OnClick()' }).linkbutton({ text: '清除', width: '250px' })[0].outerHTML;
            //$('#querydataGridView').find(".infosysbutton-q").closest('td').append('&nbsp;').append(clearBtn1);

        });
        function clearBtn1_OnClick() {
            
            //$('#WorkDateS_Query').combobox('setValue', '');
            //$('#WorkDateE_Query').combobox('setValue', '');

            //var data1 = $('#USERID_Query').combobox('getData');
            //if (data1.length > 1) {
            //    $('#USERID_Query').combobox('setValue', '');
            //}
        }

        function DGV_OnLoad() {
            if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                var userid = getClientInfo("UserID");
                var WhereString = "";
                var WhereString1 = "";
                WhereString = WhereString + "V.USERID = '" + userid + "';";  //11 = 11 C.DEPT_MANAGER
                WhereString = WhereString + "B.USERID <> '" + userid + "';";   //12 = 12 M.DEPT_MANAGER
                WhereString = WhereString + "u.USERID = '" + userid + "';";  //13 = 13 A.EMPLOYEE_CODE
                WhereString1 = WhereString + "u.USERID = '" + userid + "'"; //14=14

                setTimeout(function () {
                    //篩選
                    $('#ORG_NO_Query').combobox('setWhere', WhereString);//不含14
                    $("#USERID_Query").combobox('setWhere', WhereString1);
                    //篩選後預設狀態
                    setTimeout(function () {
                        var data = $('#ORG_NO_Query').combobox('getData');
                        var data1 = $('#USERID_Query').combobox('getData');
                        //部門有資料，設值第一個
                        if (data.length > 0) {
                            $('#ORG_NO_Query').combobox('setValue', data[0].ORG_NO);
                        }
                        //員工有一筆資料(代表沒管人)
                        if (data1.length == 1 || data1.length <= 0) {
                            if (data1.length == 1) {
                                //設值第一個
                                $('#USERID_Query').combobox('setValue', data1[0].USERID);
                            }
                            $('#ORG_NO_Query').combobox('disable');
                            $('#USERID_Query').combobox('disable');
                        }
                        //查詢按鈕
                        $('#querydataGridView').find(".infosysbutton-q").show();
                    }, 1500);

                }, 1000);

                //查詢(預設一進來只能看到自己)
                $("#dataGridView").datagrid('setWhere', "USERID='" + userid + "'");
            }
        }

        function DFM_OnLoad() {

            var UserID = $("#dataFormMasterUSERID").combobox('getValue');
            var WorkDate = $("#dataFormMasterWorkDate").datebox('getValue');
            //GetUserOrg(UserID);
            var parameter = Request.getQueryStringByName2("P1");//沒加密
            if (parameter == "") {
                parameter = Request.getQueryStringByName("P1");//有加密
            }

            //申請關卡時
            //if (parameter == "" && (getEditMode($("#dataFormMaster")) == 'updated')) {//getEditMode($("#dataFormMaster")) == 'inserted' ||
                //if (getEditMode($("#dataFormMaster")) == 'inserted') {
                    //[設值]員工
                    //$("#dataFormMasterUSERID").combobox('setValue', UserID);
                    //[設值]ORG_NO、ORG_NOParent
                    //var arr =[];
                    //arr = GetInfoCommandValue($("#dataFormMasterUSERID"), "EmployeeID='" + UserID + "'","getORG");
                    //$("#dataFormMasterORG_NO").combobox('setValue', arr[0]);
                    //$("#dataFormMasterORG_NOParent").val(arr[1]);
                //}

                //[設值]本日計畫(抓此員工最新的NextDayPlan)
                //$("#dataFormMasterTodayPlan").val(WorkDate+'：'+GetNextDayPlan(UserID, WorkDate));
            //}

            //[設值]本日計畫(抓此員工最新的NextDayPlan)
            $("#dataFormMasterTodayPlan").val(WorkDate + '：' + GetNextDayPlan(UserID, WorkDate));

            //審核或檢視時
            if (parameter == "Manager" || getEditMode($("#dataFormMaster")) == 'viewed') {
                if (parameter != "") { $("#querydataGridView").hide(); }//審核時，query隱藏
                $("#dataGridDetailDiv").hide();//明細資料隱藏
                ShowFields('#dataFormMaster', ['RecordText']);//明細內文顯示

                //加下載連結
                for (i = 1; i <= 3; i++) {
                    $("#download" + i).remove();
                    var file = $('.info-fileUpload-value', $("#dataFormMasterFile" + i).next()).val();
                    if (file != '') {
                        var link = $("<a download>").attr({ 'id':'download'+i,'href': '../JB_ADMIN/JBBERP_WorkRecordApply/File' + i + '/' + file }).html('下載');
                        $('#dataFormMasterFile' + i).closest('td').append(link);
                    }
                }

            } else {//申請關卡時
                if (parameter == "" || parameter == "Apply") { $("#querydataGridView").show(); }//query顯示
                $("#dataGridDetailDiv").show();//明細資料顯示
                HideFields('#dataFormMaster', ['RecordText']);//明細內文隱藏
                //移除下載連結
                for (i = 1; i <= 3; i++) {
                    $("#download" + i).remove();
                }
            }

            //if ($("#dataFormMasterFlowflag").val() == 'N') {
                //alert(parameter);
            //DisableFields("#dataFormMaster", ['NextDayPlan'], ['WorkDate'],[]);//  
            //$("#dataGridDetailDiv").hide();
            //$('.info-fileUpload-file[name="File1"]').attr('disabled', true);//選擇檔案
            //$('.info-fileUpload-file[name="File1"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            //$('.info-fileUpload-file[name="File2"]').attr('disabled', true);//選擇檔案
            //$('.info-fileUpload-file[name="File2"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            //$('.info-fileUpload-file[name="File3"]').attr('disabled', true);//選擇檔案
            //$('.info-fileUpload-file[name="File3"]').next().attr('class', 'info-fileUpload-button href= l-btn l-btn-plain l-btn-disabled');//上傳
            //$('input[name="File1"]').attr('disabled', true);
            //$('input[name="File2"]').attr('disabled', true);
            //$('input[name="File3"]').attr('disabled', true);
            //$("#dataGridDetail-SubmitDiv").hide();
            //}
            
        }
       
        //dataFormMasterEmployee_OnSelect
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterORG_NO").combobox('setValue', rowData.OrgNO);
            $("#dataFormMasterORG_NOParent").val(rowData.OrgNOParent);
        }

        //存檔起單
        function DFM_OnApplied() {
            
            if (getEditMode($("#dataFormMaster")) == 'updated' && $("#dataFormMasterFlowflag").val() == '') {
                var cf = confirm("請注意!!按下確定即送出審核，無法再修改");
                if (cf == true) {
                    var userid = $("#dataFormMasterUSERID").combobox('getValue');
                    var WRNO = $("#dataFormMasterWRNO").val();
                    //起單
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sJBERP_WorkRecordApply.WRMaster',
                        data: "mode=method&method=" + "FlowStartUp" + "&parameters=" + userid + "," + WRNO, //method后的參數為server的Method名稱  parameters后為端的到后端的參數這裡傳入選中資料的CustomerID欄位
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != "False") {
                                alert('工作紀錄單申請成功');
                            } else {
                                alert('工作紀錄單申請失敗');
                            }
                        }
                    });
                    $("#dataGridView").datagrid("load");
                } else {
                    return false;
                }


            } else {
                return true;
            }
        }

        function DFD_OnApply() {
            var beginTime = $('#dataFormDetailBeginTime').val();
            var endTime = $('#dataFormDetailEndTime').val();
            var beginTimeValidate = $.fn.datebox('parseDate', beginTime.replace(/\//g, '-'));
            var endTimeValidate = $.fn.datebox('parseDate', endTime.replace(/\//g, '-'));
            if (parseInt(beginTime) >= parseInt(endTime)) {
                alert('起始時間 : ' + beginTime + ' 需小於迄止時間 : ' + endTime);
                return false;
            }
        }

        //check 時間格式如 : 0800 或 0830
        function checkTimeFormat(val) {
            return $.jbIsTimeFormat(val);
        }

        
        //沒用到
        function GetUserOrg(UserID) {
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJBERP_WorkRecordApply.WRMaster',
                data: "mode=method&method=" + "GetUserOrg" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $('#ORG_NO_Query').combobox("loadData", rows);
                        $('#ORG_NO_Query').combobox('setValue', rows[0].ORG_NO);
                    }
                }
            }
        );
            return $('#ORG_NO_Query').combobox('getValue');
        }
        //查詢部門OnSelect
        function ORG_NOOnSelect() {
            var ORG_NO = $('#ORG_NO_Query').combobox('getValue');
            var userid = getClientInfo("UserID");
            var WhereString = "";
            var WhereString1 = "";
            WhereString = WhereString + "V.USERID = '" + userid + "';";  //11 = 11 C.DEPT_MANAGER
            WhereString = WhereString + "B.USERID <> '" + userid + "';";   //12 = 12 M.DEPT_MANAGER
            WhereString = WhereString + "u.USERID = '" + userid + "';";  //13 = 13 A.EMPLOYEE_CODE
            WhereString = WhereString + "u.USERID = '" + userid + "';"; //14=14
            WhereString = WhereString + "ORG_NO = '" + ORG_NO + "'"; //15=15

            //setTimeout(function () {
                $("#USERID_Query").combobox('setValue', '');
                $("#USERID_Query").combobox('setWhere', WhereString);
            //}, 800);
         }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var userid = getClientInfo("userid");
                var result = [];
                var aVal = '';
                var bVal = '';
                aVal = $('#ORG_NO_Query').combobox('getValue');
                if  (aVal != '')
                    result.push("ORG_NO = '" + aVal + "'");

                aVal = $("#USERID_Query").combobox('getValue');
                if (aVal != '' && aVal != userid) {//不是自己
                    result.push("USERID = '" + aVal + "' and Flowflag='Z'");
                } else if (aVal != '' && aVal == userid) {//是自己
                    result.push("USERID = '" + aVal + "'");
                } else {//沒選
                    result.push("Flowflag='Z'");
                }
                
                aVal = $('#WorkDateS_Query').datebox('getValue');
                bVal = $('#WorkDateE_Query').datebox('getValue');
                if (aVal != '' && bVal != '')
                    result.push("WorkDate between '" + aVal + "' and '" + bVal + "'");
                

                aVal = $("#Flowflag_Query").combobox('getValue');
                if (aVal =='NULL') {
                    result.push("Flowflag is null");
                } else if (aVal == 'N' || aVal == 'P' || aVal == 'Z' || aVal == 'X') {
                    result.push("Flowflag = '" + aVal + "'");
                }


                var FiltStr = result.join(' and ')
                $("#dataGridView").datagrid('setWhere', FiltStr);
                //$("#dataGridView").datagrid('reload');
            }

          
        }

        function DFM_OnApply() {
            
            if ($("#dataGridDetail").datagrid('getRows').length == 0) {
                alert("請新增明細資料");
                return false;
            } else {
                return true;
            }

            
            
        }

        function DGV_OnUpdate(row) {
            var userid = getClientInfo("UserID");
            if (row.USERID!=userid) {
                alert('非申請人無法編輯');
                return false;
            } else if (row.D_STEP_ID=='工作紀錄單申請') {//為了退回到申請者，還能編輯
                return true;
            }
            else if (row.Flowflag != '' && row.Flowflag !=null) {
                alert('已起單，無法編輯');
                return false;
            }
        }

        function GetNextDayPlan(UserID,WorkDate) {
            var returnValue = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sJBERP_WorkRecordApply.WRMaster',
                data: "mode=method&method=" + "GetNextDayPlan" + "&parameters=" + UserID+","+WorkDate,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        returnValue = rows[0].NextDayPlan;
                    }
                }
            }
        );
            return returnValue;
        }

        //---工具
        function HideFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').hide();
            });
        }
        function ShowFields(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $(FormName + value).closest('td').show();
            });
        }
        //呼叫dll指定的infoCommand
        function GetInfoCommandValue(controller, where,getWhat) {
            var remoteName = getInfolightOption(controller).remoteName;
            var tableName = getInfolightOption(controller).tableName;
            // var valueField = getInfolightOption(infoRefval).valueField;
            var returnValue =[];
            $.ajax({
                type: "POST",
                dataType: 'json',
                url: getRemoteUrl(remoteName, tableName, false) + "&whereString=" + encodeURIComponent(where),
                data: { rows: 1 },
                cache: false,
                async: false,
                success: function (data) {
                    if (data.length > 0) {
                        if (getWhat == "getORG") {
                            returnValue[0] = data[0]["OrgNO"];
                            returnValue[1] = data[0]["OrgNOParent"];
                        } 
                    }
                },
                error: function (data) { }
            });
            return returnValue;
        }

        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames, DisabledRefvalNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
            $.each(DisabledRefvalNames, function (index, value) {
                $(FormName + value).refval('disable');
            });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBERP_WorkRecordApply.WRMaster" runat="server" AutoApply="True"
                DataMember="WRMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="15,30,45,60,75" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnLoadSuccess="DGV_OnLoad" OnUpdate="DGV_OnUpdate">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="工作紀錄單號" Editor="text" FieldName="WRNO" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="員工" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sJBERP_WorkRecordApply.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="USERID" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="工作日期" Editor="text" FieldName="WorkDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" Format="yyyy-mm-dd">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="隸屬部門" Editor="infocombobox" FieldName="ORG_NOParent" Format="" Visible="False" Width="100" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sJBERP_WorkRecordApply.OrganizationParent',tableName:'OrganizationParent',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="ORG_NO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sJBERP_WorkRecordApply.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="紀錄內容" Editor="textarea" FieldName="RecordText" MaxLength="0" Visible="true" Width="300" />
                    <JQTools:JQGridColumn Alignment="left" Caption="流程作業" Editor="text" FieldName="D_STEP_ID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="經辦者" Editor="text" FieldName="SENDTO_NAME" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="流程狀態" Editor="infocombobox" FieldName="Flowflag" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70" EditorOptions="items:[{value:'',text:'未起單',selected:'false'},{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'結案',selected:'false'},{value:'X',text:'作廢',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="False" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="部門" Condition="=" DataType="string" Editor="infocombobox" FieldName="ORG_NO" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="220" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sJBERP_WorkRecordApply.ORG_NO_Query',tableName:'ORG_NO_Query',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:ORG_NOOnSelect,panelHeight:200" TableName="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工" Condition="=" DataType="string" Editor="infocombobox" FieldName="USERID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" TableName="" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sJBERP_WorkRecordApply.USERID_Query',tableName:'USERID_Query',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="string" Editor="datebox" FieldName="WorkDateS" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="迄止日期" Condition="&lt;=" DataType="string" Editor="datebox" FieldName="WorkDateE" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="流程狀態" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="items:[{value:'',text:'',selected:'false'},{value:'NULL',text:'未起單',selected:'false'},{value:'N',text:'新申請',selected:'false'},{value:'P',text:'流程中',selected:'false'},{value:'Z',text:'結案',selected:'false'},{value:'X',text:'作廢',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="Flowflag" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="工作紀錄單" Width="800px" DialogTop="10px" DialogLeft="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="WRMaster" HorizontalColumnsCount="4" RemoteName="sJBERP_WorkRecordApply.WRMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="DFM_OnLoad" OnApplied="DFM_OnApplied" OnApply="DFM_OnApply" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="工作紀錄單號" Editor="text" FieldName="WRNO" Format="" Width="130" ReadOnly="True" maxlength="0" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="工作日期" Editor="datebox" FieldName="WorkDate" Format="" maxlength="0" ReadOnly="False" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="員工" Editor="infocombobox" FieldName="USERID" Format="" Width="130" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sJBERP_WorkRecordApply.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" ReadOnly="True" maxlength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門" Editor="infocombobox" FieldName="ORG_NO" Format="" maxlength="0" Width="130" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sJBERP_WorkRecordApply.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="隸屬部門" Editor="text" FieldName="ORG_NOParent" MaxLength="0" ReadOnly="True" Visible="False" Width="225" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sJBERP_WorkRecordApply.OrganizationParent',tableName:'OrganizationParent',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="本日計畫" Editor="textarea" FieldName="TodayPlan" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="4" Visible="True" Width="630" EditorOptions="height:50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下次計畫" Editor="textarea" FieldName="NextDayPlan" maxlength="0" ReadOnly="False" Span="4" Width="630" EditorOptions="height:50" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="紀錄內容" Editor="textarea" EditorOptions="height:280" FieldName="RecordText" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="630" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔1" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/JBBERP_WorkRecordApply/File1',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="File1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔2" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/JBBERP_WorkRecordApply/File2',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="File2" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔3" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/JBBERP_WorkRecordApply/File3',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="File3" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核旗標" Editor="text" FieldName="Flowflag" ReadOnly="False" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>

                <div id="dataGridDetailDiv">
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="WRDetail" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sJBERP_WorkRecordApply.WRMaster" Title="工作區段紀錄" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="WRNO" Editor="text" FieldName="WRNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="40" />
                        <JQTools:JQGridColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="迄止時間" Editor="text" FieldName="EndTime" Format="" Width="60" />
                        <JQTools:JQGridColumn Alignment="left" Caption="紀錄內容" Editor="text" FieldName="RecordText" Format="" Width="310" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="WRNO" ParentFieldName="WRNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Title="明細資料" DialogTop="155px" Width="800px" DialogLeft="10px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="WRDetail" HorizontalColumnsCount="3" RemoteName="sJBERP_WorkRecordApply.WRMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="DFD_OnApply" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="WRNO" Editor="text" FieldName="WRNO" Format="" Width="120" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemNO" Format="" Width="120" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="起始時間" Editor="text" FieldName="BeginTime" Format="" Width="120" NewRow="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="迄止時間" Editor="text" FieldName="EndTime" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="紀錄內容" Editor="textarea" FieldName="RecordText" Format="" Width="630" EditorOptions="height:300" NewRow="True" Span="3" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="WRNO" ParentFieldName="WRNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemNO" />
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="WRNO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="WorkDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="WorkDate" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0830" FieldName="BeginTime" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1730" FieldName="EndTime" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="BeginTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckMethod="checkTimeFormat" CheckNull="True" FieldName="EndTime" RemoteMethod="False" ValidateMessage="請輸入正確的時間格式如 : 0800 或 0830" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="RecordText" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
