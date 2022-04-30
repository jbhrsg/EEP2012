<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBAST_AssetApplyMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //設定欄位Caption 變顏色
            var flagIDs = ['#dataFormMasterDispatchAreaID', '#dataFormMasterDispatchAreaManager'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({color: '#8A2BE2' });
            });
            //設定 Grid QueryColunm Windows width=480px
            //設定 Grid QueryColunm Windows width=480px
            //var dgid = $('#JQDataGrid1');
            //var queryPanel = getInfolightOption(dgid).queryDialog;
            //if (queryPanel)
            //    $(queryPanel).panel('resize', { width: 1020 });
            //將QUERY PANEL 按鈕置中
            $('.infosysbutton-q', '#JQDataGrid1').closest('td').attr('align', 'middle');
            //將Focus 欄位背景顏色改為黃色
            //$("#dataFormDetailAssetGetDate").datebox({
            //    width:210,
            //    onSelect: function (date) {
            //        AssetGetDateOnBlur();
            //    }
            //}).combo('textbox').blur(function () {
            //    AssetGetDateOnBlur();
            //});
            $(function () {
                $("input, select, textarea").focus(function () {
                    $(this).css("background-color", "yellow");
                });
                $("input, select, textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
        })
        function dataFormMasterOnLoadSuccess() {
            var parameters = Request.getQueryStringByName("P1");
            var mode = Request.getQueryStringByName("NAVIGATOR_MODE");
            var FormName = '#dataFormMaster';
            var HideFieldName = ['AssetLocaID', 'AssetOwnerID', 'AssetCompID'];
            if (getEditMode($("#dataFormMaster")) == 'inserted') {
                var where = "DESCRIPTION='JB'";
                $('#dataFormMasterAssetOwnerID').combobox('setWhere', where);
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
                GetUserOrgNOs();
            }
            else {
                //$.each(HideFieldName, function (index, fieldName) {
                //    $(FormName + fieldName).closest('td').prev('td').hide();
                //    $(FormName + fieldName).closest('td').hide();
                //});
            }
            var TranType = $("#dataFormMasterTranType").combobox('getValue');
            if (TranType == '報廢') {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['ScrapReason'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
                //隱藏區域位置/擁有者/帳務歸屬
                var HideFieldName = ['AssetLocaID', 'AssetOwnerID', 'AssetCompID'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });

            }
            else {
                var FormName = '#dataFormMaster';
                var HideFieldName = ['ScrapReason'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
            }
        }
        function dataFormDetailOnLoadSuccess() {
            var parameters = Request.getQueryStringByName("P1");
        }
        function OnSelectEmployee(rowData) {
            $("#dataFormMasterApplyOrg_NO").val(rowData.OrgNO);
            $("#dataFormMasterOrg_NOParent").val(rowData.OrgNOParent);
        }
        function dataGridOnInsert() {
            alert('注意!!,新增功能自2021/09/29起停用,因已由採購系統[採購結帳]後系統自動起單');
            return false;
            var TranType = $("#dataFormMasterTranType").combobox('getValue');
            if (TranType != "立帳") {
                alert("注意!!,新增功能僅限[異動類別=立帳]時使用");
                return false
            }
            //var AssetLocaID = $('#dataFormMasterAssetLocaID').combobox('getValue');
            //if (AssetLocaID == "" || AssetLocaID == undefined) {
            //    alert('注意!!,未選取區域位置,無法新增,請選取!!');
            //    $("#dataFormMasterAssetLocaID").focus();
            //    return false;
            //}
            //var AssetCompID = $('#dataFormMasterAssetCompID').combobox('getValue');
            //if (AssetCompID == "" || AssetCompID == undefined) {
            //    alert('注意!!,未選取帳務歸屬,無法新增,請選取!!');
            //    $("#dataFormMasterAssetCompID").focus();
            //    return false;
            //}
            //var AssetOwnerID = $('#dataFormMasterAssetOwnerID').combobox('getValue');
            //if (AssetOwnerID == "" || AssetOwnerID == undefined) {
            //    alert('注意!!,未選取保管人員,無法新增,請選取!!');
            //    $("#dataFormMasterAssetOwnerID").focus();
            //    return false;
            //}
            return true;
        }
        function dataFormMasterOnApply() {
            var parameters = Request.getQueryStringByName("P1");
            if ($("#dataGridDetail").datagrid('getRows').length == 0) {
                alert('注意!!明細資料不可為空');
                return false;
            }
            if (parameters == 'ACCOUNT') {
                var rows = $('#dataGridDetail').datagrid('getRows');
              
                for (var i = 0; i < rows.length; i++) {
                    if ((rows[i].AssetProperityID !=1) && (rows[i].AssetProperityID !=2))
                    break;
                }
                if (rows.length > i + 1) {
                    alert('注意!!,有立帳資產項目未設定設備屬性,請設定');
                    return false;
                }
             }
        }
        function OpenJQDialog3() {
            var TranType = $("#dataFormMasterTranType").combobox('getValue');
            if (TranType == "") {
                alert("注意!!,未選取異動類別,請選取!!");
                return false
            }
            if (TranType == "立帳" ) {
                alert("注意!!,異動功能不可在[異動類別=立帳]時使用!!");
                return false
            }
            var TranRole = $("#dataFormMasterGROUPID").combobox('getValue');
            if (TranRole == "") {
                alert("注意!!,未選取異動區分,請選取!!");
                return false
            }
            var AssetLocaID = $('#dataFormMasterAssetLocaID').combobox('getValue');
            if ((AssetLocaID == "" || AssetLocaID == undefined) && (TranType!='報廢')) {
                alert('注意!!,未選取區域位置,無法選取設備明細,請選取!!');
                $("#dataFormMasterAssetLocaID").focus();
                return false;
            }
            //var AssetCompID = $('#dataFormMasterAssetCompID').combobox('getValue');
            //if  ((AssetCompID == "" || AssetCompID == undefined) && (TranType!='報廢')) {
            //    alert('注意!!,未選取帳務歸屬,無法選取設備明細,請選取!!');
            //    $("#dataFormMasterAssetCompID").focus();
            //    return false;
            //}
            var AssetOwnerID = $('#dataFormMasterAssetOwnerID').combobox('getValue');
            if ((AssetOwnerID == "" || AssetOwnerID == undefined) && (TranType!='報廢')) {
                alert('注意!!,未選取保管人員,無法選取設備明細,請選取!!');
                $("#dataFormMasterAssetOwnerID").focus();
                return false;
            }
            var Filtstr = GetJQDataGridStr();
            if (Filtstr != '') {
                $('#JQDataGrid1').datagrid('setWhere', "AssetID not in " + Filtstr);
            }
            $("#JQDataGrid1").datagrid('setWhere', '1=2')
            openForm('#JQDialog3', {}, "view", 'dialog');
        }
        //
        function GetJQDataGridStr() {
            var rows = $('#dataGridDetail').datagrid('getRows');
            var AssetDetails = [];
            var AssetID = "";
            for (var i = 0; i < rows.length; i++) {
                AssetID ="'"+ rows[i].AssetID +"'";
                AssetDetails.push(AssetID);
            }
            var str = AssetDetails.join(',');
            if (str != '' || str == undefined) {
                str = '(' + str + ')';
            }
            return str;
        }
        function JQDataGrid1OnUpdate() {
            if ($("#JQDataGrid1").datagrid('getChecked').length == 0) {
                alert('請選取資產設備項目');
                return false;
            }
            var TranNO = $('#dataFormMasterTranNO').val();
            var rowcnt = $($('#dataGridDetail').datagrid('getRows')).length;
            var rows = $("#JQDataGrid1").datagrid('getChecked');
            var AssetIDLists = [];
            for (var i = 0; i < rows.length; i++) {
                var row = new Object();
                var ItemSeq = right(('000' + (rowcnt + (i+1)).toString().trim()), 3);
                row['TranNO'] =  TranNO;
                row['ItemSeq'] = ItemSeq;
                row['AssetID'] = rows[i].AssetID;
                row['AssetName'] = rows[i].AssetName;
                row['AssetType'] = rows[i].AssetType;
                row['AssetUnit'] = rows[i].AssetUnit;
                row['AssetBrand'] = rows[i].AssetBrand;
                row['AssetSpecs'] = rows[i].AssetSpecs;
                row['AssetGetType'] = rows[i].AssetGetType;
                row['AssetGetDate'] = rows[i].AssetGetDate;
                row['AssetGetYM'] = rows[i].AssetGetYM;
                row['UsefulYears'] = rows[i].UsefulYears;
                row['AssetQty'] = rows[i].AssetQty;
                row['AssetPhotoPath'] = rows[i].AssetPhotoPath;
                row['CreateBy'] = rows[i].CreateBy;
                row['CreateDate'] = rows[i].CreateDate;
                row['AssetCompID'] = rows[i].CompID;
                row['AssetOwnerID'] = rows[i].OwnerID;
                row['AssetLocaID'] = rows[i].LocalID;
                $("#dataGridDetail").datagrid('appendRow', row);
            }
             return true;
        }
        //取得最大資產編號
        function GetMaxAssetID() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetApplyMaster.AssetApplyMaster', 
                data: "mode=method&method=" + "GetMaxAssetID" + "&parameters=" + UserID, 
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
        //取得異動單單號
        function GetMaxTranNO() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetApplyMaster.AssetApplyMaster', 
                data: "mode=method&method=" + "GetMaxTranNO" + "&parameters=" + UserID, 
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
        //更新JBADMIN.SYSAUTONUM
        function UpdateSYSAUTONUMAssetID() {
            var AutoID = 'ASSETID';
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetApplyMaster.AssetApplyMaster', 
                data: "mode=method&method=" + "UpdateSYSAUTONUMAssetID" + "&parameters=" + AutoID, 
                cache: false,
                async: false,
                success: function (data) {
                }
            });
         }
        function dataFormDetailOnApply() {
           
            if (getEditMode($("#dataFormDetail")) == 'inserted')
            {
                var AssetGetType = $('#dataFormDetailAssetGetType').combobox('getValue');
                if (AssetGetType == "" || AssetGetType == undefined) {
                    alert('注意!!,未選取設備類別,請選取!!');
                    $("'#dataFormDetailAssetGetType'").focus();
                    return false;
                }
                var AssetUnit = $('#dataFormDetailAssetUnit').combobox('getValue');
                if (AssetUnit == "" || AssetUnit == undefined) {
                    alert('注意!!,未選取設備單位,請選取!!');
                    $("'#dataFormDetailAssetUnit'").focus();
                    return false;
                }
            }
        }
        function GetAssetLoca() {
            var AssetLocalID = $('#dataFormMasterAssetLocaID').combobox('getValue');
            return AssetLocalID;
        }
        function GetAssetCompID() {
            var AssetCompID = $('#dataFormMasterAssetCompID').combobox('getValue');
            return AssetCompID;
        }
        function GetAssetOwnerID() {
            var AssetOwnerID = $('#dataFormMasterAssetOwnerID').combobox('getValue');
            return AssetOwnerID;
        }
        //當點按關閉按鈕時,關閉目前Tab
        function CloseDataForm() {
            self.parent.closeCurrentTab();
            return false;
        }
        function left(str, num) {
            return str.substring(0, num)
        }
        function right(str, num) {
            return str.substring(str.length - num, str.length)
        }
        function GetUserOrgNOs() {
            var UserID = getClientInfo("UserID");
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetApplyMaster.AssetApplyMaster', 
                data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID, 
                cache: false,
                async: false,
                success: function (data) {
                    var rows = $.parseJSON(data);
                    if (rows.length > 0) {
                        $("#dataFormMasterApplyOrg_NO").combobox('setValue', rows[0].OrgNO);
                        $("#dataFormMasterOrg_NOParent").val(rows[0].OrgNOParent);
                    }
                }
            }
            );
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'JQDataGrid1') {
                var result = [];
                var aVal = '';
                var bVal = '';
                aVal = $('#AssetName_Query').val();
                if (aVal != '')
                    result.push("(A.AssetName like '%" + aVal + "%') or  (A.AssetID like '%" + aVal + "%')");
                aVal = $('#ItemTypeID_Query').combobox('getValue');
                if (aVal != '')
                    result.push("A.ItemTypeID = '" + aVal + "'");
                aLocalID = $('#LocalID_Query').combobox('getValue');
                if (aLocalID != '')
                    result.push("dbo.funReturnAssetLocaID(A.AssetID)  = '" + aLocalID + "'");
                aOwnerID = $('#OwnerID_Query').combobox('getValue');
                if (aOwnerID != '')
                    result.push("dbo.funReturnAssetOwnerID(A.AssetID) = '" + aOwnerID + "'");
                //aCompID = $('#CompID_Query').combobox('getValue');
                //if (aCompID != '')
                //    result.push("dbo.funReturnAssetCompID(A.AssetID)  = '" + aCompID + "'");
                //if (('#dataFormMasterTranType') == '異動') {
                //    alert('異動');
                //}
                var Filtstr = GetJQDataGridStr();
                //過濾掉已被選取的資產
                if (Filtstr != '') {
                    result.push("AssetID not in " + Filtstr);
                }
                $(dg).datagrid('setWhere', result.join(' and '));
           }
        }
        function TranTypeNameOnSelect(rowData) {
            var TranTypeName = rowData.TranTypeName
            var FormName = '#dataFormMaster';
            var HideFieldName = ['AssetLocaID', 'AssetOwnerID', 'AssetCompID'];
            var ShowFieldName = ['ScrapReason'];
            if (TranTypeName == "報廢") {
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
                $.each(ShowFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
            }
            else {
                var ShowFieldName = ['AssetLocaID', 'AssetOwnerID', 'AssetCompID','EffectDate'];
                $.each(ShowFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').show();
                    $(FormName + fieldName).closest('td').show();
                });
                var HideFieldName = ['ScrapReason'];
                $.each(HideFieldName, function (index, fieldName) {
                    $(FormName + fieldName).closest('td').prev('td').hide();
                    $(FormName + fieldName).closest('td').hide();
                });
            }
        }
      </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
           
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAssetApplyMaster.AssetApplyMaster" runat="server" AutoApply="True"
                DataMember="AssetApplyMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="right" Caption="AssetApplyMasterNO" Editor="numberbox" FieldName="AssetApplyMasterNO" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TranNO" Editor="text" FieldName="TranNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="TranType" Editor="text" FieldName="TranType" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDescr" Editor="text" FieldName="ApplyDescr" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyOrg_NO" Editor="text" FieldName="ApplyOrg_NO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyTypeID" Editor="text" FieldName="ApplyTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                </Columns>
     
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="設備異動申請" DialogLeft="10px" DialogTop="10px" Width="1060px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AssetApplyMaster" HorizontalColumnsCount="4" RemoteName="sAssetApplyMaster.AssetApplyMaster" IsAutoPageClose="True" IsAutoSubmit="True" IsShowFlowIcon="True" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPause="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" ShowApplyButton="False" ValidateStyle="Hint" OnLoadSuccess="dataFormMasterOnLoadSuccess" OnApply="dataFormMasterOnApply" OnCancel="CloseDataForm" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="異動單號" Editor="text" FieldName="TranNO" Format="" maxlength="0" ReadOnly="True" Span="1" Width="96" NewRow="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人員" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:OnSelectEmployee,panelHeight:200" FieldName="ApplyEmpID" Format="" maxlength="0" Width="120" ReadOnly="False" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sAssetApplyMaster.Organization',tableName:'Organization',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ApplyOrg_NO" Format="" maxlength="0" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="yyyy/mm/dd" Span="1" Width="98" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異動類別" Editor="infocombobox" EditorOptions="valueField:'TranTypeName',textField:'TranTypeName',remoteName:'sAssetApplyMaster.AssetTranType',tableName:'AssetTranType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:TranTypeNameOnSelect,panelHeight:200" FieldName="TranType" Format="" Span="1" Width="103" />
                        <JQTools:JQFormColumn Alignment="left" Caption="管理區分" Editor="infocombobox" EditorOptions="valueField:'GROUPID',textField:'GROUPNAME',remoteName:'sAssetApplyMaster.GROUPS',tableName:'GROUPS',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="GROUPID" maxlength="0" Span="1" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Format="yyyy/mm/dd" Span="1" Width="111" />
                        <JQTools:JQFormColumn Alignment="left" Caption="請購單號" Editor="text" FieldName="PONO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="92" />
                        <JQTools:JQFormColumn Alignment="left" Caption="處理方式" Editor="infocombobox" EditorOptions="valueField:'ScrapReason',textField:'ScrapReason',remoteName:'sAssetApplyMaster.Scrap',tableName:'Scrap',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ScrapReason" maxlength="0" Span="2" Width="260" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請事由" Editor="text" FieldName="ApplyDescr" Format="" maxlength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="473" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異動後區域位置" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetApplyMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetLocaID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="280" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異動後保管人員" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetOwnerID" Format="" Visible="True" Width="110" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="異動後帳務歸屬" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetApplyMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetCompID" Format="" Span="1" Visible="False" Width="110" />
                        <JQTools:JQFormColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" maxlength="0" Span="4" Visible="False" Width="40" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetApplyMasterNO" Editor="numberbox" FieldName="AssetApplyMasterNO" Format="" Width="180" Visible="False" ReadOnly="True" MaxLength="0" NewRow="False" RowSpan="1" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="ApplyTypeID" Editor="text" FieldName="ApplyTypeID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <br />
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="AssetApplyDetails" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sAssetApplyMaster.AssetApplyMaster" Title="明細資料" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnInsert="dataGridOnInsert" BufferView="False" NotInitGrid="False" Width="980px" RowNumbers="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemSeq" Width="35" />
                        <JQTools:JQGridColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Format="" Width="70" />
                        <JQTools:JQGridColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" Format="" Width="100" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="規格" Editor="text" FieldName="AssetSpecs" Format="" Visible="True" Width="240" />
                        <JQTools:JQGridColumn Alignment="left" Caption="區域位置" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetApplyMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetLocaID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="保管人員" Editor="infocombobox" FieldName="AssetOwnerID" Width="80" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="帳務歸屬" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetApplyMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="AssetCompID" Visible="True" Width="80" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="center" Caption="實物照片" Editor="text" FieldName="AssetPhotoPath" Visible="True" Width="90" Format="Image,Folder:JB_ADMIN/Images,Height:45" />
                        <JQTools:JQGridColumn Alignment="left" Caption="標簽照片" Editor="text" FieldName="AssetPhotoPath1" Format="Image,Folder:JB_ADMIN/Images,Height:45" Width="80" />
                        <JQTools:JQGridColumn Alignment="left" Caption="廠牌" Editor="text" FieldName="AssetBrand" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Format="yyyy/mm/dd" Visible="False" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetGetType" Editor="text" FieldName="AssetGetType" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="AssetQty" Editor="numberbox" FieldName="AssetQty" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetType" Editor="text" FieldName="ItemTypeID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetUnit" Editor="text" FieldName="AssetUnit" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="TranNO" Editor="text" FieldName="TranNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="right" Caption="UsefulYears" Editor="numberbox" FieldName="UsefulYears" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetPlace" Editor="text" FieldName="AssetPlace" Width="80" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="PurchaseNO" Editor="text" FieldName="PurchaseNO" Width="80" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetNotes" Editor="text" FieldName="AssetNotes" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="TranNO" ParentFieldName="TranNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="OpenJQDialog3" Text="選取現有設備" />
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="30px" DialogTop="210px" Title="資產明細" Width="1120px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="AssetApplyDetails" HorizontalColumnsCount="4" RemoteName="sAssetApplyMaster.AssetApplyMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="dataFormDetailOnApply" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="dataFormDetailOnLoadSuccess" Width="1120px" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="項次" Editor="text" FieldName="ItemSeq" Format="" Width="60" Span="4" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Format="" Width="60" ReadOnly="True" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" Width="392" Span="1" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="設備類別" Editor="infocombobox" FieldName="ItemTypeID" Width="120" Span="2" EditorOptions="valueField:'ITEMTYPEID',textField:'ITEMTYPENAME',remoteName:'sAssetApplyMaster.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="品名規格" Editor="text" FieldName="AssetSpecs" Format="" Width="523" Span="2" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="取得方式" Editor="infocombobox" FieldName="AssetGetType" Format="" Width="120" Span="1" Visible="True" EditorOptions="valueField:'AssetGetType',textField:'AssetGetType',remoteName:'sAssetApplyMaster.AssetGetType',tableName:'AssetGetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" ReadOnly="False" />
                            <JQTools:JQFormColumn Alignment="right" Caption="使用年限" Editor="numberbox" FieldName="UsefulYears" Span="1" Width="118" ReadOnly="False" Visible="True" Format="" />
                            <JQTools:JQFormColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="AssetQty" Format="" Width="40" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="infocombobox" FieldName="AssetUnit" Format="" Width="45" Span="1" EditorOptions="valueField:'ASSETUNIT',textField:'ASSETUNIT',remoteName:'sAssetApplyMaster.AssetUnit',tableName:'AssetUnit',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Width="120" Visible="True" ReadOnly="False" Span="2" Format="yyyy/mm/dd" />
                            <JQTools:JQFormColumn Alignment="left" Caption="存放位置" Editor="infocombobox" FieldName="AssetPlace" Width="175" Span="4" ReadOnly="False" EditorOptions="valueField:'AssetPlace',textField:'AssetPlace',remoteName:'sAssetApplyMaster.AssetPlace',tableName:'AssetPlace',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="實物照片" Editor="infofileupload" FieldName="AssetPhotoPath" Visible="True" Width="50" Span="2" ReadOnly="False" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/images',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" Format="" />
                            <JQTools:JQFormColumn Alignment="left" Caption="標簽照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/images',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="AssetPhotoPath1" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="True" Width="50" />
                            <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="text" FieldName="AssetNotes" ReadOnly="False" Span="4" Width="970" MaxLength="0" NewRow="False" RowSpan="1" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Visible="False" Width="120" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="TranNO" Editor="text" FieldName="TranNO" Format="" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="120" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="TranNO" ParentFieldName="TranNO" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetMaxTranNO" DefaultValue="" FieldName="TranNO" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="AssetOwnerID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="EffectDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AssetApplyMasterNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="ApplyTypeID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ApplyDescr" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="AssetID" RemoteMethod="False" DefaultMethod="GetMaxAssetID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AssetQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="AssetGetDate" RemoteMethod="True" DefaultValue="_today" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="台" FieldName="AssetUnit" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="購置" FieldName="AssetGetType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetAssetLoca" FieldName="AssetLocaID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetAssetCompID" FieldName="AssetCompID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetAssetOwnerID" FieldName="AssetOwnerID" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="UsefulYears" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="False" FieldName="AssetName" RemoteMethod="False" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQAutoSeq ID="JQAutoSeq1" runat="server" BindingObjectID="dataFormDetail" FieldName="ItemSeq" NumDig="3" />
            </JQTools:JQDialog>
           
            <JQTools:JQDialog ID="JQDialog3" runat="server" DialogLeft="10px" DialogTop="190px" Title="現有設備" Width="1020px" OnSubmited="JQDataGrid1OnUpdate" EditMode="Dialog">
                                 <JQTools:JQDataGrid ID="JQDataGrid1" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" AutoApply="True" BufferView="False" CheckOnSelect="False" ColumnsHibeable="False" DataMember="AssetDetails" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="True" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Panel" QueryTitle="" QueryTop="210px" RecordLock="False" RecordLockMode="None" RemoteName="sAssetApplyMaster.AssetDetails" RowNumbers="True" Title="" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" ParentObjectID="" Width="940px">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Editor="text" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="100" Caption="資產名稱" FieldName="AssetName">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品規格" Editor="text" FieldName="AssetSpecs" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="220">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品類別" Editor="text" EditorOptions="" FieldName="ItemTypeName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="數量" Editor="text" FieldName="AssetQty" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="單位" Editor="text" FieldName="AssetUnit" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="所在位置" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetApplyMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LocalID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetApplyMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="70">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="保管者" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OwnerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="物品相片" Editor="text" FieldName="AssetPhotoPath" Format="Image,Folder:JB_ADMIN/Images,Height:45" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="設備代號/名稱" Condition="=" DataType="string" Editor="text" FieldName="AssetName" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="設備類別" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ITEMTYPEID',textField:'ITEMTYPENAME',remoteName:'sAssetApplyMaster.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ItemTypeID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="110" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="區域" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetApplyMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LocalID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="保管者" Condition="%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetApplyMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OwnerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="80" />
                    </QueryColumns>
                    <TooItems>
                   <%-- <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="現有設備查詢" />--%>
                </TooItems>
                </JQTools:JQDataGrid>
            </JQTools:JQDialog>
        
         </div>
    </form>
</body>
</html>
