<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_Vendors.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var VendorItemTypeIDs = '';
        var VendorItemTypeNames = '';
        $(document).ready(function () {
            $("#VendName_Query").attr("placeholder", "廠商名稱/聯絡人名稱或電話/聯絡人手機/銀行名稱");
            var Btn = $('<a>', { href: "javascript:void(0)" }).bind('click', function () {
                var nodes = $('#JQTreeItemType').tree('getChecked');
                $.each(nodes, function (index, node) {
                    $('#JQTreeItemType').tree('uncheck', node.target);
                    $(node.target).removeClass('.tree-node-clicked');
                });
                var root = $('#JQTreeItemType').tree('getRoot');
                $('#JQTreeItemType').tree('uncheck', root.target);
                VendorItemTypeIDs = $('#dataFormMasterVendorItemTypeIDs').val();
                JQTreeItemTypeSetChecked(VendorItemTypeIDs);
                openForm('#JQDialog2', {}, "", 'dialog');
            }).linkbutton({ text: '選取物品類別' });
            $('#dataFormMasterVendorItemTypeNames').after(Btn);
            //客戶聯絡人1 區號+電話+分機 合併為同TD顯示
            var area = $('#dataFormMasterContactTelArea').closest('td');
            var code = $('#dataFormMasterContactTel').closest('td').children();
            var ext  = $('#dataFormMasterContactTelExt').closest('td').children();
            area.append('-').append(code).append('分機').append(ext);
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "lightyellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
           //新增時將廠商名稱帶給廠商簡稱
            $('#dataFormMasterVendName').blur(function () {
                var cc = $('#dataFormMasterVendShortName').val();
                if (cc == "") {
                    var VendShortName = ($('#dataFormMasterVendName').val()).substring(0, 5);
                    $('#dataFormMasterVendShortName').val(VendShortName);
                }
                var cc = $('#dataFormMasterVendAccountName').val();
                if (cc == "") {
                    $('#dataFormMasterVendAccountName').val($('#dataFormMasterVendName').val());
                }
            });
            $("#VendName_Query").focus(function () {
                var field = $(this);
                if (field.val() == field.attr('value')) {
                    field.val('');
                }
            });
        });
        var NumStr_FormatScript = function (value, row, index) {
            if (value != null) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "NumStr_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
            }
        }
        var NumStr_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            $("#Label1").text('交期狀況說明:'+rowData.NumStrDesc);
            return true;
        }
        var QtyStr_FormatScript = function (value, row, index) {
            if (value != null) {
                var fieldName = this.field;
                return $('<a>', { href: 'javascript: void(0)', title: '', onclick: "QtyStr_CommandTrigger.call(this,'')" }).linkbutton({ iconCls: '', plain: true, text: value })[0].outerHTML;
            }
        }
        var QtyStr_CommandTrigger = function (command) {
            var rowIndex = parseInt($(this).closest('tr').attr('datagrid-row-index'));
            var rowData = $("#dataGridView").datagrid('selectRow', rowIndex).datagrid('getSelected');
            $("#Label1").text('驗收狀況說明:' + rowData.QtyStrDesc);
            return true;
        }
        function dataGridViewOnSelect(rowIndex, rowData) {
            if (rowData.NumStr == null || rowData.NumStr == undefined) {
                $("#Label1").text('');
            }
        }
        function filterData() {
            var astr = getClientInfo('_groupid');
            var bstr = astr.substring(0, 7);
            if ((bstr != '1030043') && (bstr != '1030051') && (bstr != '1030052') && (bstr != '1010052') && (bstr != '1010062') && (bstr != '1010063')) {
                $("#dataGridView").datagrid("setWhere", "UserID='" + _usercode + "'");
            }
        }
        function dataFormMasterOnLoadSuccess() {
            //設定帳戶檔下載
            $("#downloadFilePath").remove();
            var FilePath = $('.info-fileUpload-value', $("#dataFormMasterFilePath").next()).val();
            if (FilePath != '') {
                var link = $("<a download>").attr({ 'id': 'downloadFilePath', 'href': '../JB_ADMIN/VendorAccount/' + FilePath }).html('[下載]');
                $('#dataFormMasterFilePath').closest('td').append(link);
            }
            //設定證照檔下載
            $("#downloadVendLicense").remove();
            var VendLicense = $('.info-fileUpload-value', $("#dataFormMasterVendLicense").next()).val();
            if (VendLicense != '') {
                var link = $("<a download>").attr({ 'id': 'downloadVendLicense', 'href': '../JB_ADMIN/VendorLicense/' + VendLicense }).html('[下載]');
                $('#dataFormMasterVendLicense').closest('td').append(link);
            }

        }
        //更新Master Grid
        function dataFormMasterOnApplied() {
            //
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
               
                if (VendorItemTypeIDs != '') {
                    SaveVendorItemType(VendorItemTypeIDs);
                }
            }
            $("#dataGridView").datagrid('reload');
        }
        function dataFormMasterOnApply() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $('#dataFormMasterVendID').val(GetMaxVendorID());
            }
            var IDs = $('#dataFormMasterVendorItemTypeIDs').val();
            SaveVendorItemType(IDs);
        }
        function VendNameOnBlur() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var VendName = $('#dataFormMasterVendName').val();
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sVendors.Vendors',
                    data: "mode=method&method=" + "CheckVendNameIsExist" + "&parameters=" + VendName,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {

                    return true;
                }
                else {
                    alert('此廠商名稱[' + VendName + ']已存在,無法新增!!');
                    $('#dataFormMasterVendName').val('');
                    $('#dataFormMasterVendName').focus();
                    return false;
                }
            }
        }
        //檢查廠商資料是否刪除
        function CheckDelVendors() {
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var row = $('#dataGridView').datagrid('getSelected');
                var cnt;
                $.ajax({
                    type: "POST",
                    url: '../handler/jqDataHandle.ashx?RemoteName=sVendors.Vendors',
                    data: "mode=method&method=" + "CheckDelVendors" + "&parameters=" + row.VendID,
                    cache: false,
                    async: false,
                    success: function (data) {
                        if (data != false) {
                            cnt = $.parseJSON(data);
                        }
                    }
                });
                if ((cnt == "0") || (cnt == "undefined")) {

                    return true;
                }
                else {
                    alert('提示此廠商已有請款單使用,無法刪除!!');
                    return false;
                }
            }
      
        }
        //當選行庫時,重新設定相關Data
        function GetBankData(rowData) {
            $("#dataFormMasterIsRemit").checkbox('setValue',rowData.IsRemit);
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        //產生下載檔案連結
        function FilePathLink(value) {
            if (value == "" || value == null) {
                return "";
            } else {
                value = "VendorAccount/" + value
                return "<a href='" + value + "' target='_blank' >上傳檔</a>";
            }
        }
        function dataGridViewOnLoadSucess() {
           // alert('ok');
           // $('#VendGradeID_Query').combobox('setValue', undefined);
           // $('#VendName_Query').focus();
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var Str = $('#VendName_Query').val();
                var QStr = String(Str).trim();
                var FiltStr = "((VendName LIKE '%" + QStr + "%')" + " OR (VendShortName Like '%" + QStr + "%') OR (ContactName Like '%" + QStr + "%')  OR  (ContactTel Like '%" + QStr + "%') OR (ContactMobile Like '%" + QStr + "%'))";
                var Str = $('#VendLevelID_Query').combobox('getValue');
                if (Str != '' && Str != undefined) {
                    FiltStr = FiltStr + " And (VendLevelID = '" + Str + "')";
                }
                //廠商屬性
                var Str = $('#VendPropertyID_Query').combobox('getValue');
                if (Str != '' && Str != undefined) {
                    FiltStr = FiltStr + " And (VendPropertyID = '" + Str + "')";
                }
                //當選擇廠商類別時,篩選已設定供應該類別的廠商
                var QStr = $("#ItemTypeID_Query").combobox('getValue');
                if (QStr != '' && QStr != undefined) {
                    FiltStr = FiltStr + " AND dbo.Vendors.VendID IN (Select VendID From VENDORITEMTYPE WHERE ItemTypeID = '" + QStr + "')"
                }
                var Str = $('#VendGradeID_Query').combobox('getValue');
                if (Str != '' && Str != undefined) {
                    FiltStr = FiltStr + " And (VendGradeID = '" + Str + "')";
                }
                $(dg).datagrid('setWhere', FiltStr);
            }
        }
        function JQTreeItemTypeSetChecked(IDstr) {
            if (IDstr != '') {
                $.each(IDstr.split(","), function (i, id) {
                    var node = $('#JQTreeItemType').tree('find', id);
                    if (node != null) {
                        $(node.target).addClass('tree-node-clicked');
                        $('#JQTreeItemType').tree('check', node.target);
                    }
                });
            }
        }
        //取得最新廠商代号
        function GetMaxVendorID() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sVendors.Vendors',
                data: "mode=method&method=" + "GetMaxVendorID" + "&parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data != false) {
                        ReturnStr = data;
                    }
                }
            });
            return ReturnStr;
        }
        function TreeItemTypeOnSubmited() {
            var nodes = $('#JQTreeItemType').tree('getChecked');
                VendorItemTypeIDs = '';
                VendorItemTypeNames = '';
            var i = 1;
            $.each(nodes, function (index, node) {
                if (i > 1) {
                    VendorItemTypeIDs = VendorItemTypeIDs + ',' + node.id;
                    VendorItemTypeNames = VendorItemTypeNames + ',' + node.text;
                }
                else {
                    VendorItemTypeIDs = VendorItemTypeIDs + node.id;
                    VendorItemTypeNames = VendorItemTypeNames + node.text;
                }
                i = i + 1;
            });
            $('#dataFormMasterVendorItemTypeIDs').val(VendorItemTypeIDs);
            $('#dataFormMasterVendorItemTypeNames').val(VendorItemTypeNames);
            return true;
        }
        //將選擇的物品類別代號存入VendorItemType
        function SaveVendorItemType(VendorItemTypeIDs) {
            var UserID = getClientInfo("UserName");
            var VendID = $('#dataFormMasterVendID').val();
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sVendors.Vendors', 
                data: "mode=method&method=" + "SaveVendorItemType" + " &parameters=" + VendID + "*" + VendorItemTypeIDs + "*" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {

                    }
                    else {
                        alert("提示!!存入失敗")
                    }
                }
            });
        }
        function RamitOnBlur() {
            var Ramit = $('#dataFormMasterRemit').val();
            if (Ramit == "") {
                $('#dataFormMasterRemit').val(0);
            }
            if (Ramit > 0) {
                $("#dataFormMasterIsRemit").checkbox('setValue', true);
            }
            else {
               $("#dataFormMasterIsRemit").checkbox('setValue', false);
            }
        }
        //廠商評鑑
        function PutVendorEval() {
            var YN = confirm("提示!!系統將依365天內的交期與驗收紀錄評鑑廠商等級,按[確定]開始評鑑、[取消]取消評鑑");
            if (YN == false) {
                return false;
            }
            var UserID = getClientInfo("UserName");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sVendors.Vendors',
                data: "mode=method&method=" + "PutVendorEval" + " &parameters=" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                        alert('提示!!廠商評鑑成功');
                   }
                    else {
                        alert("提示!!廠商評鑑失敗,請洽資訊中心")
                    }
                }
            });
        }
     </script>
 
</head>
<body>
    <asp:Label ID="Label1" runat="server" ForeColor="#6600FF" BackColor="#CCFF33" Font-Size="Medium"></asp:Label>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sVendors.Vendors" runat="server" AutoApply="True"
                DataMember="Vendors" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="廠商維護" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="15,30,45" PageSize="15" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" OnDelete="CheckDelVendors" OnLoadSuccess="dataGridViewOnLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True" OnSelect="dataGridViewOnSelect"  >
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="廠商代號" Editor="numberbox" FieldName="VendID" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="廠商名稱" Editor="text" FieldName="VendName" Format="" Width="180" />
                    <JQTools:JQGridColumn Alignment="center" Caption="廠商屬性" Editor="text" FieldName="VendProperty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" Format="" MaxLength="0" Width="70" Visible="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="聯絡電話" Editor="text" FieldName="Tel" MaxLength="0" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="傳真號碼" Editor="text" FieldName="VendFax" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="手機號碼" Editor="text" FieldName="ContactMobile" MaxLength="0" Width="75" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelArea" Frozen="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContactTel" Editor="text" FieldName="ContactTel" Format="" MaxLength="0" Width="100" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContactTelExt" Editor="text" FieldName="ContactTelExt" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="PayTermDays" Editor="numberbox" FieldName="PayTermDays" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ContactEmail" Editor="text" FieldName="ContactEmail" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VendBank" Editor="text" FieldName="VendBank" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="VendAccount" Editor="text" FieldName="VendAccount" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="廠商類別" Editor="numberbox" FieldName="VendTypeID" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="帳戶上傳" Editor="text" FieldName="FilePath" Format="Image,Folder:JB_ADMIN/Files,Height:30" MaxLength="0" Width="60" Visible="False" FormatScript="FilePathLink" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="廠商簡稱" Editor="text" FieldName="VendShortName" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ItemTypeID" Editor="text" FieldName="ItemTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="最近交易日" Editor="timespinner" FieldName="DealDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="70">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="交期狀況" Editor="text" FieldName="NumStr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="NumStr_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="準交率%" Editor="text" FieldName="NumRate" Format="N1" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="驗收狀況" Editor="text" FieldName="QtyStr" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60" FormatScript="QtyStr_FormatScript">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="合格率%" Editor="text" FieldName="QtyRate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="評鑑等級" Editor="text" FieldName="VendGrade" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="55">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="廠商類別" Editor="text" FieldName="VendorItemTypeNames" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="250">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="備註" Editor="text" FieldName="VendorNotes" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="NumStrDesc" Editor="text" FieldName="NumStrDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="QtyStrDesc" Editor="text" FieldName="QtyStrDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                   <%-- <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton" OnClick="PutVendorEval" Text="廠商評鑑" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn  Caption="廠商搜尋" Condition="=" DataType="string" Editor="text" FieldName="VendName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="320" Span="0" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="廠商類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ItemTypeID',textField:'ItemTypeName',remoteName:'sVendors.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ItemTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False"  Span="0" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="等級" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'VendLevelID',textField:'VendLevelNum',remoteName:'sVendors.VendLevel',tableName:'VendLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendLevelID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="廠商屬性" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Content',remoteName:'sVendors.VendProperty',tableName:'VendProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendPropertyID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="評鑑等級" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Content',remoteName:'sVendors.VendGrade',tableName:'VendGrade',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendGradeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="70" />
                </QueryColumns>
            </JQTools:JQDataGrid>
            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="廠商維護" Width="1060px" DialogLeft="30px" DialogTop="50px" >
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="Vendors" HorizontalColumnsCount="4" RemoteName="sVendors.Vendors" Width="1080px" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApplied="dataFormMasterOnApplied" IsAutoPause="False" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnApply="dataFormMasterOnApply" OnLoadSuccess="dataFormMasterOnLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商代號" Editor="text" FieldName="VendID" Format="" Width="80" ReadOnly="True" Span="1" maxlength="0" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商名稱" Editor="text" FieldName="VendName" Format="" Width="240" Span="1" OnBlur="VendNameOnBlur" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商簡稱" Editor="text" FieldName="VendShortName" Format="" Width="130" Span="2" maxlength="5" EditorOptions="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡人" Editor="text" FieldName="ContactName" Format="" maxlength="0" Span="1" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聯絡電話" Editor="text" FieldName="ContactTelArea" Format="" maxlength="10" Span="1" Width="25" />
                        <JQTools:JQFormColumn Alignment="left" Caption="手機號碼" Editor="text" FieldName="ContactMobile" Format="" maxlength="0" Span="0" Width="130" />
                        <JQTools:JQFormColumn Alignment="left" Caption="郵件信箱" Editor="text" FieldName="ContactEmail" Format="" maxlength="0" Span="0" Width="177" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTel" Format="" maxlength="20" Width="90" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption=" " Editor="text" FieldName="ContactTelExt" Format="" maxlength="10" Span="1" Width="25" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="傳真" Editor="text" FieldName="VendFax" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="地址" Editor="text" FieldName="VendAddress" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="240" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款戶名" Editor="text" FieldName="VendAccountName" Span="2" Width="240" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款天數" Editor="infocombobox" EditorOptions="valueField:'PayTermID',textField:'PayTermName',remoteName:'sVendors.PayTerm',tableName:'PayTerm',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="PayTermID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款方式" Editor="infocombobox" EditorOptions="valueField:'PayTypeID',textField:'PayTypeName',remoteName:'sReferencesADM.PayType',tableName:'PayType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="PayTypeID" ReadOnly="False" Span="1" Width="87" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="付款銀行" Editor="inforefval" EditorOptions="title:'銀行搜尋',panelWidth:500,remoteName:'sVendors.Bank',tableName:'Bank',columns:[{field:'BankNO',title:'銀行代號',width:120,align:'left',table:'',queryCondition:''},{field:'BankBranchNO',title:'分行代號',width:120,align:'left',table:'',queryCondition:''},{field:'BankName',title:'銀行名稱',width:300,align:'left',table:'',queryCondition:''}],columnMatches:[],whereItems:[],valueField:'BankID',textField:'BankName',valueFieldCaption:'BankID',textFieldCaption:'BankName',cacheRelationText:false,checkData:false,showValueAndText:false,onSelect:GetBankData,selectOnly:true" FieldName="VendBank" Format="" Span="2" Width="240" maxlength="0" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯款帳號" Editor="text" FieldName="VendAccount" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="155" />
                        <JQTools:JQFormColumn Alignment="left" Caption="匯費金額" Editor="text" FieldName="Remit" maxlength="0" OnBlur="RamitOnBlur" Width="80" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="檔案上傳" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/VendorAccount',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="FilePath" maxlength="0" Span="2" Visible="True" Width="120" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商屬性" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Content',remoteName:'sVendors.VendProperty',tableName:'VendProperty',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendPropertyID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商級別" Editor="infocombobox" EditorOptions="valueField:'VendLevelID',textField:'VendLevelName',remoteName:'sVendors.VendLevel',tableName:'VendLevel',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendLevelID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="87" />
                        <JQTools:JQFormColumn Alignment="left" Caption="評鑑等級" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Content',remoteName:'sVendors.VendGrade',tableName:'VendGrade',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="VendGradeID" Width="80" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="評鑑日期" Editor="datebox" FieldName="LastEvalDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="85" />
                        <JQTools:JQFormColumn Alignment="left" Caption="最近交易日" Editor="datebox" FieldName="DealDate" Format="yyyy/mm/dd" ReadOnly="True" Width="85" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="交易筆數" Editor="text" FieldName="DealQty" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="50" />
                        <JQTools:JQFormColumn Alignment="left" Caption="評鑑證照" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/VendorLicense',showButton:true,showLocalFile:false,fileSizeLimited:'1000'" FieldName="VendLicense" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="80" PlaceHolder="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="廠商類別" Editor="text" FieldName="VendorItemTypeNames" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="4" Visible="True" Width="650" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" EditorOptions="height:35" FieldName="VendorNotes" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="6" Visible="True" Width="650" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="180" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正人員" Editor="text" FieldName="LastUpdateBy" Format="" maxlength="0" Visible="False" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修正日期" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="VendorItemTypeIDs" Editor="text" FieldName="VendorItemTypeIDs" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="需付匯費" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsRemit" maxlength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="VendID" RemoteMethod="False" DefaultMethod="" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="2" FieldName="PayTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="LastUpdateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Ramit" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="VendLevelID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="VendPropertyID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="4" FieldName="VendGradeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="VendLevelID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="ContactEmail" RemoteMethod="True" ValidateType="EMail" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VendName" RemoteMethod="True" ValidateType="None" />
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="VendShortName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
         <JQTools:JQDialog ID="JQDialog2" runat="server" DialogLeft="260px" DialogTop="120px" Title="選取物品類別" Width="450px" OnSubmited="TreeItemTypeOnSubmited">
                  <div class="easyui-layout" style="height: 480px;">
                    <div data-options="region:'center',title:'',border:false" title="">
                 <JQTools:JQTreeView ID="JQTreeItemType" runat="server" DataMember="ItemTypeTree" idField="ID" parentField="ParentID" RemoteName="sVendors.ItemTypeTree" textField="Name" Checkbox="True"></JQTools:JQTreeView>
                    </div>
                    </div>
                 </JQTools:JQDialog>
        
    </form>
</body>
</html>
