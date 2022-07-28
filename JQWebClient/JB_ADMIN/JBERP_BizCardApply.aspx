<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_BizCardApply.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(function () {
            //將Focus 欄位背景顏色改為黃色
            $("input, select, textarea").focus(function () {
                $(this).css("background-color", "yellow");
            });
            $("input, select, textarea").blur(function () {
                $(this).css("background-color", "white");
            });
            //名片檔欄位不能打字
            $('input[name="FilePath"]').attr('disabled', true);
            //必填紅字
            RedTd('#dataFormMaster', ['Workplace', 'Title', 'Cname0', 'Ename', 'Email', 'FilePath', 'Quantity']);
            $("#dataFormMasterQuantity").combobox({ "panelHeight": "30px" });
            $("#dataFormMasterIsUrgent").combobox({ "panelHeight": "30px" });

            var IsUrgentDescr = $("<label>").css({ 'color': 'blue'}).text("(急件會列入紀錄)");
            $('#dataFormMasterIsUrgent').closest('td').append(IsUrgentDescr);
            var EnameDescr = $("<label>").css({ 'color': 'blue' }).text("(須加姓氏)");
            $('#dataFormMasterEname').closest('td').append(EnameDescr);
            var TitleDescr = $("<label>").css({ 'color': 'blue' }).text("(範例：管理室/業務專員)");
            $('#dataFormMasterTitle').closest('td').append(TitleDescr);

            $("#dataFormMasterCname0").combobox('textbox').bind('blur', OnSelect_Cname0);

        });
        function OnLoad_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            if (param == '') { param = Request.getQueryStringByName2("p1"); }
            var emode = getEditMode($("#dataFormMaster")).trim();
            //alert(param + ';' + emode);

            //viewed模式
            $("#dataFormMasterFilePath").closest("td").hide();
            $("#dataFormMasterFilePath").closest("td").prev("td").hide();
            $("#dataFormMasterFilePath1").closest("td").show();
            $("#dataFormMasterFilePath1").closest("td").prev("td").show();

            //隱藏Cname0
            $("#dataFormMasterCname0").closest('td').prev('td').hide();//closest上一層selector,prev下一個selector
            $("#dataFormMasterCname0").closest('td').hide();

            if (emode == 'inserted' || (emode=='updated' && param=='')) {//申請時
                var dt = new Date();
                var formatted = dt.getYear() + 1900 + '-' + (parseInt(dt.getMonth()) + parseInt(1)) + '-' + dt.getDate() + ' ' + dt.getHours() + ":" + dt.getMinutes() + ":" + dt.getSeconds();
                $("#dataFormMasterCreateDate").val(formatted);
                $("#dataFormMasterApplyEmpID").combobox('setValue', getClientInfo("userid"));
                $("#dataFormMasterFilePath1").closest("td").hide();
                $("#dataFormMasterFilePath1").closest("td").prev("td").hide();

                var DisabledFieldName = ['Title', 'WorkplaceA', 'Ename','WorkplaceTEL', 'ExtNum', 'FaxNum','PhoneNum', 'Skype', 'LineID', 'Email', 'Remark'];
                var DisabledComboboxName = ['Workplace', 'Quantity', 'IsUrgent'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);

                $("#dataFormMasterCname0").closest('td').prev('td').show();//closest上一層selector,prev下一個selector
                $("#dataFormMasterCname0").closest('td').show();

            } else if (param == "ga") {//流程到總務
                $("#dataFormMasterFilePath1").closest("td").hide();
                $("#dataFormMasterFilePath1").closest("td").prev("td").hide();
            }else if (param == "art") {//流程到美工
                var DisabledFieldName = ['Title', 'WorkplaceA', 'Cname', 'Ename', 'WorkplaceTEL', 'ExtNum','FaxNum', 'PhoneNum', 'Skype', 'LineID', 'Email', 'Remark'];
                var DisabledComboboxName = ['Workplace', 'Quantity', 'IsUrgent'];
                DisableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);
                $("#dataFormMasterFilePath").closest("td").show();
                $("#dataFormMasterFilePath").closest("td").prev("td").show();
                $("#dataFormMasterFilePath1").closest("td").hide();
                $("#dataFormMasterFilePath1").closest("td").prev("td").hide();
            }
        }
        function OnApply_dataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            if (param == '') { param = Request.getQueryStringByName2("p1"); }
            var emode = getEditMode($("#dataFormMaster")).trim();
            if (emode == 'inserted') {//申請時
                if (CheckCombobox('#dataFormMaster', 'Cname0') == false) { return false; }
                if (CheckVal1('#dataFormMaster', 'Ename') == false) { return false; }
                if (CheckVal('#dataFormMaster', "WorkplaceA") == false) { return false; }
                if (CheckVal('#dataFormMaster', 'Title') == false) { return false; }
                if (CheckVal('#dataFormMaster', 'Email') == false) { return false; }
                if (CheckCombobox('#dataFormMaster', 'Quantity') == false) { return false; }

            } else if (param == "art") {//流程到美工
                if (CheckUpload("#dataFormMaster", 'FilePath') == false) { return false; }
            }
        }
        function OnSelect_Cname0() {
            var UserID = $("#dataFormMasterCname0").combobox('getValue');
            var Cname = $("#dataFormMasterCname0").combobox('getText');
            $("#dataFormMasterCname").val(Cname);
            if (Cname != '') {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPBizCardApply.ERPBizCardApply',
                    data: "mode=method&method=GetLastApply&parameters=" + Cname,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                $("#dataFormMasterEname").val(rows[0].Ename);
                                $("#dataFormMasterWorkplace").combobox('setValue', rows[0].Workplace);
                                $("#dataFormMasterWorkplaceA").val(rows[0].WorkplaceA);
                                $("#dataFormMasterWorkplaceTEL").val(rows[0].WorkplaceTEL);
                                $("#dataFormMasterTitle").val(rows[0].Title);
                                $("#dataFormMasterExtNum").val(rows[0].ExtNum);
                                $("#dataFormMasterFaxNum").val(rows[0].FaxNum);
                                $("#dataFormMasterPhoneNum").val(rows[0].PhoneNum);
                                $("#dataFormMasterSkype").val(rows[0].Skype);
                                $("#dataFormMasterLineID").val(rows[0].LineID);
                                $("#dataFormMasterEmail").val(rows[0].Email);
                            } else {
                                $("#dataFormMasterEname").val('');
                                $("#dataFormMasterWorkplace").combobox('setValue', '');
                                $("#dataFormMasterWorkplaceA").val('');
                                $("#dataFormMasterWorkplaceTEL").val('');
                                $("#dataFormMasterTitle").val('');
                                $("#dataFormMasterExtNum").val('');
                                $("#dataFormMasterFaxNum").val('');
                                $("#dataFormMasterPhoneNum").val('');
                                $("#dataFormMasterSkype").val('');
                                $("#dataFormMasterLineID").val('');
                                $("#dataFormMasterEmail").val('');
                            }
                        } else {
                            alert(data + ':' + 'GetLastApply此方法有錯誤');
                        }
                    }
                });
                var DisabledFieldName = ['Title', 'WorkplaceA', 'Ename', 'WorkplaceTEL', 'ExtNum','FaxNum', 'PhoneNum', 'Skype', 'LineID', 'Email', 'Remark'];
                var DisabledComboboxName = ['Workplace', 'Quantity', 'IsUrgent'];
                EnableFields('#dataFormMaster', DisabledFieldName, DisabledComboboxName);


                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPBizCardApply.ERPBizCardApply',
                    data: "mode=method&method=GetUserID&parameters=" + Cname,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            var rows = $.parseJSON(data);
                            if (rows.length > 0) {
                                $("#dataFormMasterUserConfirm").val(rows[0].USERID);
                            } else {
                                $("#dataFormMasterUserConfirm").val($("#dataFormMasterApplyEmpID").combobox('getValue'));
                            }
                        } else {
                            alert(data + ':' + 'GetUserID此方法有錯誤');
                        }
                    }
                    });
            }
        }
        function OnSelect_Workplace(row) {
            $("#dataFormMasterWorkplaceA").val(row.Address);
            $("#dataFormMasterWorkplaceTEL").val(row.TEL);
        }

        //以下是工具function-----------------------
        function CheckUpload(FormName, fieldName) {
            var infofileUpload = $(FormName + fieldName);
            var FieldNameC = infofileUpload.closest('td').prev('td').text();
            var infofileUploadvalue = $('.info-fileUpload-value', infofileUpload.next())
            if (infofileUploadvalue.val() == '' || infofileUploadvalue.val() == undefined) {
                alert('注意!!,未上傳 " ' + FieldNameC + ' " ,請上傳!!');
                infofileUpload.next().find('input').focus();
                return false;
            }
        }
        function DisableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', true);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('disable');
            });
        }
        function EnableFields(FormName, DisabledFieldNames, DisabledComboboxNames) {
            $.each(DisabledFieldNames, function (index, value) {
                $(FormName + value).attr('disabled', false);
            });
            $.each(DisabledComboboxNames, function (index, value) {
                $(FormName + value).combobox('enable');
            });
        }
        function CheckCombobox(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).combobox('getValue');
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                alert('注意!!,未選取 " ' + FieldNameC + ' " ,請選取!!');
                $(FormName + fieldName).next().find('input').focus();
                return false;
            }
        }
        function CheckVal(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).val();
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if ($.trim(FieldValue) == '' || FieldValue == undefined) {
                    alert('注意!!,未填寫 " ' + FieldNameC + ' " ,請填寫!!');
                    $(FormName + fieldName).focus();
                    return false;
            }
        }
        function CheckVal1(FormName, fieldName) {
            var FieldValue = $(FormName + fieldName).val();
            var FieldNameC = $(FormName + fieldName).closest('td').prev('td').text();
            if (FieldValue == '' || FieldValue == undefined) {
                alert('注意!!,未填寫 " ' + FieldNameC + ' " ,請填寫!!');
                $(FormName + fieldName).focus();
                return false;
            }
        }
        function RedTd(FormName, FieldNames) {
            $.each(FieldNames, function (index, value) {
                $(FormName + value).closest('td').prev('td').css({ 'color': 'red' });
            });
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPBizCardApply.ERPBizCardApply" runat="server" AutoApply="True"
                DataMember="ERPBizCardApply" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="名片申請單">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="BizCardNO" Editor="text" FieldName="BizCardNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Workplace" Editor="text" FieldName="Workplace" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Title" Editor="text" FieldName="Title" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Cname" Editor="text" FieldName="Cname" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Ename" Editor="text" FieldName="Ename" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExtNum" Editor="text" FieldName="ExtNum" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PhoneNum" Editor="text" FieldName="PhoneNum" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Skype" Editor="text" FieldName="Skype" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Email" Editor="text" FieldName="Email" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Remark" Editor="text" FieldName="Remark" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="FilePath" Editor="text" FieldName="FilePath" Format="" MaxLength="0" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="名片申請單" DialogTop="10px" Width="580px">
                <asp:Label ID="Label1" runat="server" Text="提醒：名片印刷日為每月10日，請於印刷日4個工作日前提出申請" ForeColor="#0000CC"></asp:Label><br />
                <asp:Label ID="Label2" runat="server" Text="提醒：美工專員編稿後會送到「名片使用者」確認名片稿(若使用者無EEP工號，則由申請者確認)" ForeColor="#0000CC"></asp:Label>
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPBizCardApply" HorizontalColumnsCount="1" RemoteName="sERPBizCardApply.ERPBizCardApply" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" OnLoadSuccess="OnLoad_dataFormMaster" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnApply="OnApply_dataFormMaster" CaptionAlignment="left" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="單號" Editor="text" FieldName="BizCardNO" Format="" Width="300" ReadOnly="True" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請者" Editor="infocombobox" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPBizCardApply.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyEmpID" ReadOnly="True" Width="304" maxlength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請時間" Editor="text" FieldName="CreateDate" Format="" ReadOnly="True" Width="300" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="infocombobox" FieldName="Cname0" Format="" maxlength="0" Width="304" OnBlur="" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sERPBizCardApply.USERS',tableName:'USERS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelect_Cname0,panelHeight:200" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="Cname" maxlength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="Ename" Format="" Width="300" ReadOnly="False" NewRow="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職地點" Editor="infocombobox" FieldName="Workplace" Width="304" EditorOptions="valueField:'Workplace',textField:'Workplace',remoteName:'sERPBizCardApply.WorkPlaceAddress',tableName:'WorkPlaceAddress',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,onSelect:OnSelect_Workplace,panelHeight:200" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="任職地址" Editor="text" EditorOptions="" FieldName="WorkplaceA" Format="" Width="300" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="部門/職稱" Editor="text" FieldName="Title" Format="" maxlength="0" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="公司電話" Editor="text" FieldName="WorkplaceTEL" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="分機" Editor="text" FieldName="ExtNum" Format="" maxlength="0" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="FaxNum" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="300" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機" Editor="text" FieldName="PhoneNum" Format="" maxlength="0" Width="300" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Skype" Editor="text" FieldName="Skype" Format="" maxlength="0" Width="300" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="LineID" Editor="text" FieldName="LineID" Format="" maxlength="0" Width="300" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="E-mail" Editor="text" FieldName="Email" Format="" maxlength="0" Width="300" ReadOnly="False" NewRow="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="盒數" Editor="infocombobox" EditorOptions="items:[{value:'3',text:'3',selected:'false'},{value:'5',text:'5',selected:'false'},{value:'其他',text:'其他--請於備註填寫盒數',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="Quantity" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="304" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="Remark" Format="" maxlength="0" Width="298" EditorOptions="height:30" NewRow="True" RowSpan="1" Span="1" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="速別" Editor="infocombobox" EditorOptions="items:[{value:'一般件',text:'一般件',selected:'false'},{value:'急件',text:'急件',selected:'false'}],checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="IsUrgent" maxlength="0" ReadOnly="False" Width="304" NewRow="False" Span="1" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="名片瀏覽檔" Editor="infofileupload" FieldName="FilePath" Format="" maxlength="0" Width="304" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'JB_ADMIN/JBERP_BizCardApply/origin',showButton:true,showLocalFile:false,fileSizeLimited:'20000'" NewRow="True" Span="1" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="名片瀏覽檔" Editor="text" FieldName="FilePath1" Format="download,folder:JB_ADMIN/JBERP_BizCardApply/origin" MaxLength="0" ReadOnly="False" Width="80" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsPrint" Editor="text" FieldName="IsPrint" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserConfirm" Editor="text" FieldName="UserConfirm" maxlength="0" ReadOnly="False" Width="80" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="BizCardNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_sysdate" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="否" FieldName="IsPrint" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue=" " FieldName="Ename" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="一般件" FieldName="IsUrgent" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
