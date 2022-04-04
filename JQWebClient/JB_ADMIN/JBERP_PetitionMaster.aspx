<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_PetitionMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js">    </script>   
    <script type="text/javascript">       
        //$(document).ready(function () {
        //    //將Focus 欄位背景顏色改為黃色
        //    $(function () {
        //        $("input, select, textarea").focus(function () {
        //            $(this).css("resize","none");
        //        });
        //        $("input, select, textarea").blur(function () {
        //            $(this).css("background-color", "white");
        //        });
        //    });
        //})
      
        $(document).ready(function () {
            //$("textarea").css("background-color", "red");
            //$('.changetext').css("background-color", "red");
            //$(function () {
            //    $("textarea").keydown(function () {
            //        $("textarea").css("overflow", "hidden").bind("keydown keyup", function () {
            //            $(this).height('0px').height($(this).prop("scrollHeight") + 'px');
            //        });
            //    });

            //});
        })

        var AddDetailform = 0;//明細新增筆數AutoKey欄位預設值       
        var _LogUserId = "";//登入使用者
        var FlomParameters = "";//表單參數
        var DispPlusApprove = true;
        var dispcnt = 0;

        function DataformLoadSucess() {
            _LogUserId = getClientInfo("UserID");
            FlomParameters = Request.getQueryStringByName("P1");            
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
           
            var UserNmae = getClientInfo('_username');
            var mode1 = getEditMode($("#dataFormMaster"));
            //$("#dataFormMasterSubject").css({ "resize": "none"});
            //alert('FlomParameters:' + FlomParameters + 'NAVIGATOR_MODE:' + mode+" mode1:"+mode1);

            //顯示加簽者意見
            var FlowListid = $("#dataFormMasterFlowListid").val();            
            if (FlowListid == '' || mode1 == "inserted" || FlomParameters == "Mang") {                
                $("#dgpanel").remove();
            }
            else {
                //GetPlusApprove();
                //$("#LabGrid").css({ "border-color": "red", "border-style": "solid", "padding": "2px" });
                $("#PlusAppoveList").datagrid('getPanel').panel('collapse');
            }
            
            //主旨判斷資料調整欄高
            var Subjectlen = $("#dataFormMasterSubject").val().length;
            var lineHeight = Subjectlen / 70;
            if (Subjectlen >= 70) {
                $("#dataFormMasterSubject").css("height", (lineHeight * 25)  + "px");
            }
            //說明判斷資料調整欄高
            Subjectlen = $("#dataFormMasterDescription").val().length;            
            if (Subjectlen >= 70) {
                lineHeight = Subjectlen / 70;
                $("#dataFormMasterDescription").css("height", (lineHeight * 25)  + "px");
            }
            //擬案判斷資料調整欄高
            Subjectlen = $("#dataFormMasterActionPlan").val().length;            
            if (Subjectlen >= 70) {
                lineHeight = Subjectlen / 70;               
                $("#dataFormMasterActionPlan").css("height", (lineHeight * 25) + "px");
            }
            //總經理決行判斷資料調整欄高
            Subjectlen = $("#dataFormMasterCarryOut").val().length;            
            if (Subjectlen >= 70) {
                lineHeight = Subjectlen / 70;
                $("#dataFormMasterCarryOut").css("height", (lineHeight * 25)  + "px");
            }            
            
            //簽呈表新增時關閉綜合意見、會簽回覆、總經理決行
            if (mode1 == "inserted"  || (FlomParameters=="Apply" && mode1 == "updated")) {
                $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterCarryOut').closest('td').hide();
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();               
                $("#dataFormDetailMangReply").closest("td").prev("td").hide()
                $('#dataFormDetailMangReply').closest('td').hide();
                $("#dataFormMasterCreateBy").val(UserNmae);
                GetUserOrgNOs();                
                var EmpFlowAgentList = GetEmpFlowAgentList();
                var whereStr = " EmployeeID in (" + EmpFlowAgentList + ")";
                $('#dataFormMasterEmployeeID').combobox('setWhere', whereStr);
                $('#div1').css({ 'display': 'none' });
                
            }
            if (mode1 == "updated") {
                //隱藏明細新增功能
                $('#DetailAdd').hide();
                $('#dataFormMasterFileLevel').combobox('disable', true);                
                $('#dataFormMasterAttachment').attr('readonly', true);
                $('#dataFormMasterAttachment').attr('disabled', true);
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormMasterAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
                $('#dataFormMasterSubject').attr('disabled', true);
                $('#dataFormMasterSubject').attr('readonly', true);
                $('#dataFormMasterDescription').attr('disabled', true);
                $('#dataFormMasterDescription').attr('readonly', true);
                $('#dataFormMasterActionPlan').attr('disabled', true);
                $('#dataFormMasterActionPlan').attr('readonly', true);
                var _empid = $('#dataFormMasterApplyEmpID').combobox('getValue');                
                if (FlomParameters == "Countersigns" || FlomParameters == "Applicant")
                {
                    $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                    $('#dataFormMasterCarryOut').closest('td').hide();
                    $('#dataFormDetailDescription').attr('disabled', true);
                    $('#dataFormDetailDescription').attr('readonly', true);
                }
                if (FlomParameters == "Countersigns")//隱藏彙總欄位
                {
                    $("#dataFormMasterSummary").closest("td").prev("td").hide()
                    $('#dataFormMasterSummary').closest('td').hide();
                }
                if (FlomParameters == "Applicant")
                {
                    $('#dataFormDetailMangReply').attr('disabled', true);
                    $('#dataFormDetailMangReply').attr('readonly', true);
                    //$("#dataFormMasterSummary").css({ "border-color": "red", "border-style": "solid", "padding": "2px" });
                    $('#dataFormMasterSummary').focus().css({ "background-color": "yellow" });;
                }
                if (FlomParameters == "CarryOut") {
                    $('#dataFormMasterSummary').attr('disabled', true);
                    $('#dataFormMasterSummary').attr('readonly', true);                    
                    $('#dataFormMasterCarryOut').focus().css({"background-color": "yellow"});
                }
            }
            if (FlomParameters == "Mang")
            {
                $("#dataFormMasterCarryOut").closest("td").prev("td").hide()
                $('#dataFormMasterCarryOut').closest('td').hide();
                $("#dataFormMasterSummary").closest("td").prev("td").hide()
                $('#dataFormMasterSummary').closest('td').hide();
                $("#dataFormDetailMangReply").closest("td").prev("td").hide()
                $('#dataFormDetailMangReply').closest('td').hide();
            }
        }

        function mygetQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return '';
        }

        function OnSelectEmployeeID(rowData) {
            alert('empid編號:' + rowdata.applyempid);
            $("#dataFormMasterApplyEmpID").combobox('setValue', rowData.ApplyEmpID);
            //$("#dataFormMasterApplyOrg_NO").combobox('setValue', rowData.OrgNOt);
        }

        function CkCountersignRole() {
            var ApplyEmpID = $("#dataFormDetailCountersignRole").val();
            alert("職稱值:" + ApplyEmpID);
            if (ApplyEmpID == "") {
                $("#dataFormDetailCountersignRole").focus();
                //$("#dataFormDetailCountersignRole").data("inforefval").refval.find("input.refval-text").focus();
            }
        }

        //取得USER的部門代號
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";

            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sPetitionMaster.PetitionMaster',
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + _LogUserId,
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        //$("#dataFormMasterCreateBy").val(rows[0].ORG_DESC);
                        //$("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);CreateBy                        
                        ReturnStr = data;
                    }
                }
            }
            );           
        }

        function DetailformLoadSucess()
        {
            //會簽回覆判斷資料調整欄高
            var Subjectlen = $("#dataFormDetailMangReply").val().length;
            var lineHeight = Subjectlen / 70;
            if (Subjectlen > 70)
                $("#dataFormDetailMangReply").css("height", (lineHeight * 25)  + "px");
            //承辦詢問意見判斷資料調整欄高
            Subjectlen = $("#dataFormDetailDescription").val().length;
            
            if (Subjectlen > 70) {
                lineHeight = Subjectlen / 70;
                $("#dataFormDetailDescription").css("height", (lineHeight * 25)  + "px");
            }
        }
        //明細AutoKey欄位預設值
        function dataFormDetail_OnApply()
        {
            if (getEditMode($("#dataFormDetail")) == "inserted") {
                $("#dataFormDetailAutoKey").val(AddDetailform);
            }
            //var _DetailAutoKey = $("#dataFormDetailAutoKey").val();
            //alert('明細筆數' + _DetailAutoKey);
            AddDetailform += 1; 
        }

        function GridDetail_OnDelete()
        {
            //會簽、承辦會總、總經理決行不可刪除
            if (FlomParameters == "Countersigns" || FlomParameters == "Applicant" ||FlomParameters == "CarryOut") {
                alert('會簽明細不可刪除!');
                return false;
            }            
        }

        //明細修改資料判斷
        function GridDetail_OnUpdate()
        {
            if (FlomParameters == "Countersigns") {
                var _selectrow = $('#dataGridDetail').datagrid('getSelected');
                var rowIndex = $('#dataGridDetail').datagrid('getRowIndex', _selectrow)
                var _UpdateRows = $('#dataGridDetail').datagrid('getRows');
                var up_value = _UpdateRows[rowIndex].CountersignEmp;
                if (_LogUserId != up_value) {
                    alert('非此會簽人員無法編輯!');
                    return false;
                }
                else {
                    $('#dataFormDetailDescription').attr('readonly', true);
                    $('#dataFormDetailDescription').attr('disabled', true);
                }
            }
            if (FlomParameters == "Applicant" || FlomParameters == "CarryOut") {
                //確認功能關閉
                $("#jqdialog2").find(".infosysbutton-s").hide();
                $('#dataFormDetailDescription').attr('readonly', true);
                $('#dataFormDetailDescription').attr('disabled', true);
                $('#dataFormDetailMangReply').attr('readonly', true);
                $('#dataFormDetailMangReply').attr('disabled', true);
                $('#dataFormDetailAttachment').attr('readonly', true);
                $('#dataFormDetailAttachment').attr('disabled', true);
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-value').attr('disabled', true); //前面Textbox
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-file').attr('disabled', true); //後面瀏覽鈕
                $('#dataFormDetailAttachment').next('.info-fileUpload-span').find('.info-fileUpload-button').hide(); //後面上傳鈕
            }            
        }

            //彙總會簽人員名單至主檔
        function MasterOnApply() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {                
                var rows = $('#dataGridDetail').datagrid('getRows');
                var _emps = "";

                for (var i = 0; i < rows.length; i++) {
                    var chkemp = _emps.indexOf(rows[i]["CountersignEmp"]);
                    //alert('此會簽名單如下:' + _emps + '目前判斷人員:' + _CountersignEmp1 + '判斷結果:' + chkemp); 
                    if (rows.length - 1 == i)
                        _emps = _emps + rows[i]["CountersignEmp"]; //人員rows[i]["CountersignEmp"]
                    else
                        _emps = _emps + rows[i]["CountersignEmp"] + ",";
                    if (i > 0 && Number(chkemp) >= 0) {
                        alert('此會簽名單重覆如下:' + _emps);
                        return false;
                    }
                }
                $("#dataFormMasterCountersignEmps").val(_emps);
                //var dd = $('#dataFormDetailCountersignRole').refval('selectItem').text;
                //alert('此會簽職稱' + dd);
                //var dd1 = $('#dataFormDetailCountersignRoled').val();
            }
            
            //判斷Textarea欄位有特殊字元
            $('textarea', this).each(function () {
                var value1 = $(this).val();
                value = value1.replace(/\v/g, '');//replace \v
                $(this).val(value);
            })
        }

        //完整顯示明細Grid資料
        function ShowDetailGrid(value) {            
            return "<p style='margin:0px;word-wrap:break-word;white-space: normal'>" + value + "</p>";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div style="display: none;"> <%--display: none;--%>
                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPetitionMaster.PetitionMaster" runat="server" AutoApply="True"
                DataMember="PetitionMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False"  EditOnEnter="True"  MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True"  EditMode="Dialog" InsertCommandVisible="True" Width="600px">
                <Columns>
                    <%--<JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" />--%>
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="呈核人員編號" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="呈核人員" Editor="text" FieldName="ApplyEmpName" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="申請者部門" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="right" Caption="保密等級" Editor="numberbox" FieldName="FileLevel" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="主旨" Editor="text" FieldName="Subject" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Description" Editor="text" FieldName="Description" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ActionPlan" Editor="text" FieldName="ActionPlan" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Attachment" Editor="text" FieldName="Attachment" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Summary" Editor="text" FieldName="Summary" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CarryOut" Editor="text" FieldName="CarryOut" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="True" />
                </Columns>
               <%-- <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>--%>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
            </div>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="簽呈申請" Width="930px"  DialogTop="10px" DialogLeft="10px">
               <div id="titledes"><asp:Label ID="LabGrid0" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="文字敍述顯示不完整時,請點此欄位最右邊捲軸" BorderStyle="Solid" Font-Size="Medium"></asp:Label></div>
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PetitionMaster" HorizontalColumnsCount="12" RemoteName="sPetitionMaster.PetitionMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" 
                     OnLoadSuccess="DataformLoadSucess" OnApply="MasterOnApply" ParentObjectID="">                     
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" ReadOnly="True" Span="2" Width="100" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="簽呈日期" Editor="datebox" FieldName="PetitionDate" Format="" maxlength="0" ReadOnly="True" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員" Editor="infocombobox" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployeeID,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" ReadOnly="True" Span="2" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPetitionMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" Format="" maxlength="0" ReadOnly="True" Span="4" Width="120" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保密等級" Editor="infocombobox" EditorOptions="valueField:'Code',textField:'CodeNmae',remoteName:'sPetitionMaster.GradeCode',tableName:'GradeCode',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="FileLevel" Format="" maxlength="0" ReadOnly="False" Span="2" Width="100" />
                        <JQTools:JQFormColumn Alignment="left" Caption="主旨" Editor="textarea" EditorOptions="height:30" FieldName="Subject" maxlength="256" PlaceHolder="最多可填寫256字元!" Span="12" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="說明" Editor="textarea" EditorOptions="height:120" FieldName="Description" Format="" MaxLength="2048" NewRow="False" PlaceHolder="最多可填寫2048字元!" ReadOnly="False" RowSpan="1" Span="12" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="擬案" Editor="textarea" EditorOptions="height:60" FieldName="ActionPlan" Format="" MaxLength="1024" NewRow="False" PlaceHolder="最多可填寫1024字元!" ReadOnly="False" RowSpan="1" Span="12" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="附檔" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'Files/FWCRM/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'1024'" FieldName="Attachment" Format="" maxlength="0" Span="12" Width="120" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="意見綜合" Editor="textarea" EditorOptions="height:45" FieldName="Summary" Format="" maxlength="1024" PlaceHolder="最多可填寫1024字元!" Span="12" Width="800" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="總經理決行" Editor="textarea" EditorOptions="height:30" FieldName="CarryOut" Format="" maxlength="1024" PlaceHolder="最多可填寫1024字元!" Span="12" Visible="True" Width="800" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CountersignEmps" Editor="text" FieldName="CountersignEmps" Format="" Span="6" Visible="False" Width="450" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" Visible="False" Width="180" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" ReadOnly="False" Span="1" Visible="False" Width="180" MaxLength="0" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="呈核人員姓名" Editor="text" FieldName="ApplyEmpName" Format="" ReadOnly="True" Span="2" Visible="False" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowListid" Editor="text" FieldName="FlowListid" ReadOnly="False" Span="1" Visible="False" Width="80" MaxLength="0" NewRow="False" RowSpan="1" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PetitionCountersign" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sPetitionMaster.PetitionMaster" Title="會簽明細" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="Reload" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="GridDetail_OnUpdate" OnDelete="GridDetail_OnDelete" >
                   
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽職稱" Editor="infocombobox" FieldName="CountersignRole" Format="" Width="120" EditorOptions="valueField:'GroupID',textField:'GROUPNAME',remoteName:'sPetitionMaster.PetitionList',tableName:'PetitionList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽人員" Editor="infocombobox" FieldName="CountersignEmp" Format="" Width="60" EditorOptions="valueField:'EMPLOYEEID',textField:'EMPLOYEENAME',remoteName:'sPetitionMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="承辦詢問建議" Editor="text" FieldName="Description" Visible="True" Width="250" Format="" FormatScript="ShowDetailGrid">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽者回覆" Editor="text" FieldName="MangReply" Width="400" Visible="True" FormatScript="ShowDetailGrid" />
                        <JQTools:JQGridColumn Alignment="left" Caption="會簽日期" Editor="datebox" FieldName="CountersignDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="附件檔案" Editor="text" FieldName="Attachment" Format="" Width="80" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Width="80" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem ID="DetailAdd" Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="930px" EditMode="Dialog" DialogLeft="10px" DialogTop="150px" Title="會簽明細">
                    
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="PetitionCountersign" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="4" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" OnApply="dataFormDetail_OnApply" ParentObjectID="dataFormMaster" RemoteName="sPetitionMaster.PetitionMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="DetailformLoadSucess">
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="簽呈編號" Editor="text" FieldName="PetitionNO" Format="" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽職稱" Editor="inforefval" EditorOptions="title:'選取職稱',panelWidth:350,remoteName:'sPetitionMaster.PetitionPosition',tableName:'PetitionPosition',columns:[{field:'GroupID',title:'職稱編號',width:100,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'GROUPNAME',title:'職稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'GroupID',textField:'GROUPNAME',valueFieldCaption:'職稱編號',textFieldCaption:'職稱',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignRole" Span="2" Width="200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽人員" Editor="inforefval" EditorOptions="title:'選取會簽人員',panelWidth:350,remoteName:'sPetitionMaster.PetitionList',tableName:'PetitionList',columns:[{field:'USERID',title:'人員編號',width:80,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'USERNAME',title:'會簽人員',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[{field:'GroupID',value:'row[CountersignRole]'}],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'會簽人員編號',textFieldCaption:'會簽人員',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" FieldName="CountersignEmp" Format="" Span="2" Width="200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="承辦詢問建議" Editor="text" EditorOptions="" FieldName="Description" Format="" MaxLength="1024" PlaceHolder="最多可填寫1024字元!" Span="4" Width="750" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽者回覆" Editor="textarea" EditorOptions="height:35" FieldName="MangReply" MaxLength="1024" OnBlur="" PlaceHolder="最多可填寫1024字元!" Span="4" Visible="True" Width="780" />
                            <JQTools:JQFormColumn Alignment="left" Caption="附件檔名" Editor="infofileupload" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt|pdf',isAutoNum:true,upLoadFolder:'Files/FWCRM/Petition',showButton:true,showLocalFile:false,fileSizeLimited:'1024'" FieldName="Attachment" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="300" />
                            <JQTools:JQFormColumn Alignment="left" Caption="會簽日期" Editor="text" FieldName="CountersignDate" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="text" FieldName="CreateDate" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                     

                </JQTools:JQDialog>

                
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">                    
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="PetitionDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="PetitionNO" RemoteMethod="True" DefaultValue=" 自動編號" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="ApplyEmpName" RemoteMethod="True" DefaultValue="_username" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                    </Columns>
                    
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="FileLevel" RemoteMethod="True" ValidateMessage="請選擇密件等級!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Subject" RemoteMethod="True" ValidateMessage="主旨不可空白!" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Description" RemoteMethod="True" ValidateMessage="說明不可空白!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignRole" RemoteMethod="True" ValidateType="None" ValidateMessage="會簽職稱不可空白!" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="CountersignEmp" RemoteMethod="True" ValidateMessage="會簽人員不可空白!" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <br />
                <br />
                <asp:Panel ID="dgpanel" runat="server">
                    <asp:Label ID="LabGrid" runat="server" Font-Bold="True" Font-Overline="False" ForeColor="Red" Text="顯示加簽意見請點選右邊▼展開資料" BorderStyle="Solid"></asp:Label>
                    <br />
                    <br />
                    <JQTools:JQDataGrid ID="PlusAppoveList" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="False" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="PlusApproveList" DeleteCommandVisible="False" DuplicateCheck="False" EditDialogID="JQDialog2" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="False" ParentObjectID="dataFormMaster" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sPetitionMaster.PetitionMaster" RowNumbers="True" Title="加簽意見" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="left" Caption="PetitionNO" Editor="text" FieldName="PetitionNO" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="left" Caption="LISTID" Editor="text" FieldName="LISTID" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="left" Caption="加簽者" Editor="text" FieldName="USERNAME" Format="" FormatScript="" Width="80" />
                            <JQTools:JQGridColumn Alignment="left" Caption="批示意見" Editor="text" FieldName="REMARK" Format="" FormatScript="ShowDetailGrid" Width="615" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="PetitionNO" ParentFieldName="PetitionNO" />
                        </RelationColumns>
                    </JQTools:JQDataGrid>
                    <br />
                </asp:Panel>
                <br />
            </JQTools:JQDialog>
      
        </div>
       
    </form>
</body>
   
</html>
