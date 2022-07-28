<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_ContinueEmploy.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script type="text/javascript">
        $(function () {
            $("#JQDataForm1LetterClass").closest("td").prev("td").css({"color":"red"});
        });
        //外籍勞工姓名→入境日,期滿日
        function OnSelectLaborName(rowdata) {
            $("#dataFormDetailGender").val(rowdata.sex);
            $("#dataFormDetailCountry").val(rowdata.nat_name);
            if (rowdata.lab_idate !== null && rowdata.lab_idate.trim() !== "" && rowdata.lab_idate !== undefined) {
                var s = parseInt(rowdata.lab_idate.substr(0, 3), 10);
                var m = parseInt(rowdata.lab_idate.substr(3, 2), 10);
                var d = parseInt(rowdata.lab_idate.substr(5, 2), 10);
                var year = s + 1911;
                var smd = year + "/" + m + "/" + d;
                $("#dataFormDetailImmigrationDate").datebox('setValue', smd);
            }
            if (rowdata.lab_edate !== null && rowdata.lab_edate.trim() !== "" && rowdata.lab_edate !== undefined) {
                var s1 = parseInt(rowdata.lab_edate.substr(0, 3), 10);
                var m1 = parseInt(rowdata.lab_edate.substr(3, 2), 10);
                var d1 = parseInt(rowdata.lab_edate.substr(5, 2), 10);
                var year1 = s1 + 1911;
                var smd1 = year1 + "/" + m1 + "/" + d1;
                $("#dataFormDetailDueDate").datebox('setValue', smd1);
            }
        }
        //雇主→外籍勞工姓名之名單
        function OnSelectEmployer(rowdata) {
            var where = "cus_no ='"+rowdata.cus_no+"'";
            $("#dataFormDetailLaborName").refval('setWhere', where);
        }
        function XFlowFlag() {
            var ContinueEmployNO = $("#dataFormMasterContinueEmployNO").val();
            if (ContinueEmployNO != "") {
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sERPContinueEmploy.ERPContinueEmployMaster', //連接的Server端，command
                    data: "mode=method&method=" + "XFlowFlag" + "&parameters=" + ContinueEmployNO,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != "False") {
                            if (data != "True") {//有執行完
                                $("#XFlowFlag").attr('disabled', 'disabled');
                                alert("作廢成功");
                            } else { alert("作廢失敗"); }
                        } else {//執行發生錯誤
                            alert('執行發生錯誤');
                        }
                    }
                });
            }
        }

        function OnLoaddataFormMaster() {
            var param = Request.getQueryStringByName("p1");
            if (param == '') { param = Request.getQueryStringByName2("p1"); }
            //div1(dataGridDetail)業務用來編輯，JQDialog3(JQDataForm1)用來給業務編輯
            //div2(JQDataGrid1)有checkbox，用來註記勞工變更意願，JQDialog2(dataFormDetail)用來檢視
            //div0(JQDataGrid2)只是用來審核和列印通知單，JQDialog2(dataFormDetail)用來檢視
            if (param == 'apply') {
                $("#div1").hide();
                $("#div2").hide();
                $("#div0").show();
                //getEditMode($('#dataFormMaster'));getClientInfo("userid")
                //申請人審核結案
            } else if (param == 'end') {//&& (getClientInfo('_username') == $("#dataFormMasterCreateBy").val())
                $("#div0").show();
                $("#div1").hide();
                $("#div2").hide();
                var PrintLink = $('<a>', { href: 'javascript:void(0)', name: 'OrdersPrintLink', onclick: 'OpeneReportOrders.call(this,1)' }).linkbutton({ plain: false, text: '續聘通知單列印' })[0].outerHTML
                var PrintLink1 = $('<a>', { href: 'javascript:void(0)', name: 'OrdersPrintLink1', onclick: 'OpeneReportOrders.call(this,0)' }).linkbutton({ plain: false, text: '不續聘通知單列印' })[0].outerHTML
                $("#toolbarJQDataGrid2").append(PrintLink);
                $("#toolbarJQDataGrid2").append(PrintLink1);
                //結案
            } else if ($("#dataFormMasterFlowFlag").val() == 'Z' && (getClientInfo('_username') == $("#dataFormMasterCreateBy").val() || ($("#dataFormMasterCreateBy").val() == '王純純' && getClientInfo('_username') == '王圻馨')) && getEditMode($('#dataFormMaster')) == "viewed") {
                $("#div2").show();
                $("#div1").hide();
                $("#div0").hide();
                var PrintLink = $('<a>', { href: 'javascript:void(0)', name: 'OrdersPrintLink', onclick: 'OpeneReportOrders.call(this,1)' }).linkbutton({ plain: false, text: '續聘通知單列印' })[0].outerHTML
                var PrintLink1 = $('<a>', { href: 'javascript:void(0)', name: 'OrdersPrintLink1', onclick: 'OpeneReportOrders.call(this,0)' }).linkbutton({ plain: false, text: '不續聘通知單列印' })[0].outerHTML
                var DeleteDetail = $('<a>', { href: 'javascript:void(0)', id: 'DeleteDetail', onclick: 'DeleteDetail.call(this)' }).linkbutton({ plain: false, text: '變更勞工意願' })[0].outerHTML
                //var XFlowFlag;//作廢此單
                //if ($('#dataFormMasterFlowFlag').val() != 'X') {
                //    XFlowFlag = $('<a>', { href: 'javascript:void(0)', id: 'XFlowFlag', onclick: 'XFlowFlag.call(this)' }).linkbutton({ plain: false, text: '作廢此單' })[0].outerHTML
                //} else {
                //    XFlowFlag = $('<a>', { href: 'javascript:void(0)', id: 'XFlowFlag' }).linkbutton({ plain: false, text: '作廢此單', disabled: 'disabled' })[0].outerHTML
                //}
                //var tdOrderNo = $('#dataFormMasterContinueEmployNO').closest('td');
                //tdOrderNo.append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + PrintLink);
                $("#toolbarJQDataGrid1").append(PrintLink);
                $("#toolbarJQDataGrid1").append(PrintLink1);
                $("#toolbarJQDataGrid1").append(DeleteDetail);
                //業務審核
            } else if (param == 'sales') {
                $("#div1").show();
                $("#div2").hide();
                $("#div0").hide();
            } else {//其他只能看和審核
                $("#div1").hide();
                $("#div2").hide();
                $("#div0").show();
            }
        }
        //按"列印通知單"
        function OpeneReportOrders(isRecontract) {
            var ContinueEmployNO = $('#dataFormMasterContinueEmployNO').val();
            var url = "../JB_ADMIN/REPORT/FWCRM/CEReportView.aspx?ContinueEmployNO=" + ContinueEmployNO +"&IsRecontract="+isRecontract;
            var title = (isRecontract = 1) ? "外籍勞工期滿續聘通知單" : "外籍勞工期滿不續聘通知單";
            var height = $(window).height() - 50;
            var width = $(window).width() - 200;
            var dialog = $('<div/>')
            .dialog({
                draggable: false,
                modal: true,
                height: height,
                width: width,
                title: title,
                //maximizable: true                              
            });
            $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
            dialog.dialog('open');
        }
        function FormatScript_CheckBox(val) {
            return (val == "true" || val == true) ? "V" : "";
        }
        //按"變更勞工意願"
        function DeleteDetail() {
            if (confirm("確定要變更嗎?，變更後須重新送通知單。")) {
                var ContinueEmployNO = $("#dataFormMasterContinueEmployNO").val();
                var rows = $("#JQDataGrid1").datagrid('getChecked');
                for (var i = 0; i < rows.length; i++) {//卡有變更過的
                    if (rows[i].DeleteFlag) { alert(rows[i].LaborName + "已變更過"); return false; }
                }
                var aCEConfirmNO = [];
                var sCEConfirmNO;
                for (var i = 0; i < rows.length; i++) {
                    aCEConfirmNO.push("'" + rows[i].CEConfirmNO + "'")
                }
                sCEConfirmNO = aCEConfirmNO.join(",");
                if (rows.length != 0) {
                    $.ajax({
                        type: "POST",
                        url: '../handler/jqDataHandle.ashx?RemoteName=sERPContinueEmploy.ERPContinueEmployMaster', //連接的Server端，command
                        data: "mode=method&method=" + "DeleteDetail" + "&parameters=" + sCEConfirmNO + "*" + ContinueEmployNO,
                        cache: false,
                        async: false,
                        success: function (data) {
                            if (data != "False") {//有執行完
                                if (data == "True") {//沒資料
                                    alert("變更失敗");
                                } else {//有資料
                                    $("#JQDataGrid1").datagrid('load');
                                    alert("變更成功");
                                }
                            } else {//執行發生錯誤
                                alert('執行發生錯誤');
                            }
                        }
                    });
                }
            }
        }
        function OnLoadSuccess_JQDataGrid1() {
            $('#JQDataGrid1').datagrid('uncheckAll');
        }
        //function OnLoad_JQDataForm1() {
        //    if ($("#JQDataForm1LetterNO").val() == "") { $("#JQDataForm1LetterNO").val("資料審核中"); }
        //}
        function OnUpdate_dataGridDetail(row) {
            if (row.IsRecontract == '1' || row.IsRecontract =="true") { return true; } else { return false; }
        }
        //卡函文類別(沒使用)
        function OnApply_JQDataForm1() {
            var v1 = $("#JQDataForm1LetterClass").combobox("getText");
            if (v1 == "---請選擇---") {
                alert("函文類別為必填"); return false;
            }
        }
        //卡函文類別(沒使用)
        function OnApply_dataFormMaster() {
            var rows = $("#dataGridDetail").datagrid("getRows");
            for (var i = 0; i < rows.length ; i++) {
                if (rows[i].IsRecontract == true && rows[i].LetterClass == null) {
                    alert(rows[i].LaborName +" 未選擇「函文類別」"); return false;
                }
            }
        }
        function FormatScript_JQDataGrid1_DeleteFlag(val) {
            if (val == true) { return "V"; } else { return ""; }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" runat="server" AutoApply="True"
                DataMember="ERPContinueEmployMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title=" " ReportFileName="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="ContinueEmployNO" Editor="text" FieldName="ContinueEmployNO" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="續聘通知單總單" Width="1020px" DialogTop="10px" DialogLeft="30px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="ERPContinueEmployMaster" HorizontalColumnsCount="3" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoaddataFormMaster" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="180" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="承辦人" Editor="text" FieldName="CreateBy" Format="" Width="180" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="timespinner" FieldName="CreateDate" Format="" Width="183" ReadOnly="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="FlowFlag" Editor="text" FieldName="FlowFlag" ReadOnly="False" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="續聘函文類別" Editor="infocombobox" EditorOptions="items:[{value:'初招',text:'初招',selected:'false'},{value:'重招',text:'重招',selected:'false'},{value:'遞補',text:'遞補',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="LetterClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="183" />
                        <JQTools:JQFormColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" maxlength="0" ReadOnly="False" Width="180" NewRow="False" RowSpan="1" Span="1" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>

                <div id="div1">
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="ERPContinueEmployDetail" EditDialogID="JQDialog3" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" Title="名單" AllowAdd="False" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" ReportFileName="" OnUpdate="OnUpdate_dataGridDetail" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="30" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="120" Visible="False" ReadOnly="True" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工人姓名" Editor="infocombobox" FieldName="LaborName" Format="" Width="60" EditorOptions="valueField:'lab_no',textField:'lab_cname',remoteName:'sERPContinueEmploy.lab',tableName:'lab',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Width="30" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="50" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="" Width="70" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="" Width="70" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" Format="" Width="130" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="text" FieldName="IsRecontract" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="text" FieldName="Transfer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="text" FieldName="ReturnHome" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="text" FieldName="BackPot" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="text" FieldName="TransferAg" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘函文類別" Editor="text" EditorOptions="" FieldName="LetterClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ContinueEmployNO" ParentFieldName="ContinueEmployNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" Visible="False" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" Visible="False" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
                <div id="div2">
                <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AutoApply="False" DataMember="ERPContinueEmployDetail" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" Title="名單" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" ReportFileName="" OnLoadSuccess="OnLoadSuccess_JQDataGrid1" >
                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="60" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工人姓名" Editor="infocombobox" FieldName="LaborName" Format="" Width="60" EditorOptions="valueField:'lab_no',textField:'lab_cname',remoteName:'sERPContinueEmploy.lab',tableName:'lab',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Width="30" />
                        <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="50" />
                        <JQTools:JQGridColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" Format="" Width="130" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="text" FieldName="IsRecontract" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="text" FieldName="Transfer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="text" FieldName="ReturnHome" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="text" FieldName="BackPot" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="text" FieldName="TransferAg" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘函文類別" Editor="text" FieldName="LetterClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="變更意願" Editor="text" FieldName="DeleteFlag" ReadOnly="False" Width="60" FormatScript="FormatScript_JQDataGrid1_DeleteFlag" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ContinueEmployNO" ParentFieldName="ContinueEmployNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>
                <div id="div0">
                <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AutoApply="False" DataMember="ERPContinueEmployDetail" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" Title="名單" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" ReportFileName="" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="30" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="120" Visible="False" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="工人姓名" Editor="infocombobox" FieldName="LaborName" Format="" Width="60" EditorOptions="valueField:'lab_no',textField:'lab_cname',remoteName:'sERPContinueEmploy.lab',tableName:'lab',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="52">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Width="30" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="50" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="" Width="70" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="" Width="70" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" Format="" Width="130" ReadOnly="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘" Editor="text" FieldName="IsRecontract" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉雇" Editor="text" FieldName="Transfer" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="返國" Editor="text" FieldName="ReturnHome" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30" FormatScript="FormatScript_CheckBox">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="回鍋" Editor="text" FieldName="BackPot" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="轉仲" Editor="text" FieldName="TransferAg" FormatScript="FormatScript_CheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="True" Sortable="False" Visible="True" Width="30">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="續聘函文類別" Editor="infocombobox" EditorOptions="items:[{value:'初招',text:'初招',selected:'false'},{value:'重招',text:'重招',selected:'false'},{value:'遞補',text:'遞補',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="LetterClass" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="120">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="ContinueEmployNO" ParentFieldName="ContinueEmployNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>
                </div>


                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" Width="550px" Title="詳細" EditMode="Dialog">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPContinueEmployDetail" HorizontalColumnsCount="2" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" ReadOnly="False" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工人姓名" Editor="text" FieldName="LaborName" Format="" Width="120" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="160" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="160" />
                            <JQTools:JQFormColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="" Width="124" />
                            <JQTools:JQFormColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="" Width="164" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" Format="" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘" Editor="checkbox" FieldName="IsRecontract" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" EditorOptions="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="轉雇" Editor="checkbox" EditorOptions="" FieldName="Transfer" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="返國" Editor="checkbox" EditorOptions="" FieldName="ReturnHome" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="回鍋" Editor="checkbox" FieldName="BackPot" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="轉仲" Editor="checkbox" FieldName="TransferAg" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘函文類別" Editor="text" FieldName="LetterClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="ContinueEmployNO" ParentFieldName="ContinueEmployNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="AutoKey" />
                </JQTools:JQDialog>
                <JQTools:JQDialog ID="JQDialog3" runat="server" BindingObjectID="JQDataForm1" Width="550px" Title="詳細" EditMode="Dialog">
                    <JQTools:JQDataForm ID="JQDataForm1" runat="server" ParentObjectID="dataFormMaster" DataMember="ERPContinueEmployDetail" HorizontalColumnsCount="2" RemoteName="sERPContinueEmploy.ERPContinueEmployMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="編號" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" ReadOnly="False" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="通知單總單編號" Editor="text" FieldName="ContinueEmployNO" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘" Editor="checkbox" FieldName="IsRecontract" Width="80" EditorOptions="" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="工人姓名" Editor="text" FieldName="LaborName" ReadOnly="True" Visible="True" Width="120" EditorOptions="" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="Employer" Width="160" ReadOnly="True" Visible="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="性別" Editor="text" FieldName="Gender" Format="" Width="120" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="國別" Editor="text" FieldName="Country" Format="" Width="160" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="入境日" Editor="datebox" FieldName="ImmigrationDate" Format="" Width="124" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="期滿日" Editor="datebox" FieldName="DueDate" Format="" Width="164" ReadOnly="True" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘確認書編號" Editor="text" FieldName="CEConfirmNO" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="120" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="轉雇" Editor="checkbox" EditorOptions="" FieldName="Transfer" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="返國" Editor="checkbox" EditorOptions="" FieldName="ReturnHome" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="回鍋" Editor="checkbox" FieldName="BackPot" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="轉仲" Editor="checkbox" FieldName="TransferAg" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                            <JQTools:JQFormColumn Alignment="left" Caption="續聘函文類別" Editor="infocombobox" FieldName="LetterClass" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="123" EditorOptions="items:[{value:'初招',text:'初招',selected:'false'},{value:'重招',text:'重招',selected:'false'},{value:'遞補',text:'遞補',selected:'false'}],checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="文號" Editor="text" FieldName="LetterNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="120" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="ContinueEmployNO" ParentFieldName="ContinueEmployNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="" DefaultValue="自動編號" FieldName="ContinueEmployNO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
