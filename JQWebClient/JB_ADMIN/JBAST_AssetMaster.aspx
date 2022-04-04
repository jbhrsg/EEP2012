<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBAST_AssetMaster.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#AssetName_Query").attr("placeholder", "資產名稱,規格,備註,存放位置.....");
            //$('.infosysbutton-q', '#querydataGridView').closest('td').attr('align', 'middle');
            var flagIDs = ['#IsPublicView'];
            $(flagIDs.toString()).each(function () {
                var captionTd = $(this).closest('td').prev('td');
                captionTd.css({ color: '#8A2BE2' });
            });
            //取得日期變更
            $("#dataFormMasterAssetGetDate").datebox({
                width:120,
                onSelect: function (date) {
                    AssetGetDateOnBlur();
                 }
            }).combo('textbox').blur(function () {
                    AssetGetDateOnBlur();
            });
            //將Focus 欄位背景顏色改為黃色
            $(function () {
                $("input,textarea").focus(function () {
                    $(this).css("background-color", "lightyellow");
                });
                $("input,textarea").blur(function () {
                    $(this).css("background-color", "white");
                });
            });
            //---呼叫維護保養紀錄------------------------------------------------------------------------
            var dfMaintFreqID = $('#dataFormMasterMaintFreqID').closest('td');
            dfMaintFreqID.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                var AssetID = $("#dataFormMasterAssetID").val();
                var FiltStr = "AssetID = '" + AssetID + "'";
                $("#JQDataGridMD").datagrid('setWhere', FiltStr);
                openForm('#JQDialog4', {}, "", 'dialog');
                return true;
            }).linkbutton({ text: '保養紀錄' }));
            //-----------------------------------------------------------------------------------------
        });
        function dataFormMasterLoadSucess() {
           if (getEditMode($("#dataFormMaster")) == 'inserted') {
                $("#dataFormMasterAssetCompID").combobox('enable');
                $("#dataFormMasterAssetOwnerID").combobox('enable');
                $("#dataFormMasterAssetLocaID").combobox('enable');
                var IsAudit = returnIsAssetAuditor();
                if (IsAudit == -1) {
                    $('#div_dataGridDetail').hide();
                }
                document.getElementById("cc").style.display = "none";
           }
           if (getEditMode($("#dataFormMaster")) == 'updated') {
                 $('#div_dataGridDetail').show();
                 $("#dataFormMasterAssetCompID").combobox('disable');
                 $("#dataFormMasterAssetOwnerID").combobox('disable');
                 $("#dataFormMasterAssetLocaID").combobox('disable');
            }
            var idx = $('#dataGridView').datagrid('getRowIndex', $('#dataGridView').datagrid('getSelected'));
            var hh = parseInt(idx);
            $("#dataFormMasterRecNO").val(hh);
        }
        function queryGrid(dg) {
            if ($(dg).attr('id') == 'dataGridView') {
                var FiltStr = '';
                var QueryStr = $('#AssetName_Query').val();
                if (QueryStr != '' && QueryStr != undefined) {
                    var QStr = String(QueryStr).trim();
                    //LIKE '%_%'時,需使用'[_]'置換之
                    QueryStr = QStr.replace('_', "[_]");
                    FiltStr = "((AssetID LIKE '%" + QueryStr + "%')" + " OR (AssetName Like '%" + QueryStr + "%')  OR  (AssetSpecs Like '%" + QueryStr + "%')  OR  (AssetNotes Like '%" + QueryStr + "%') OR (AssetPlace Like '%" + QueryStr + "%')  )";
                }
                var OwnerID = $('#OwnerID_Query').combobox('getValue');
                if (OwnerID != '' && OwnerID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + " dbo.funReturnAssetOwnerID(A.AssetID)="+"'" + OwnerID + "'";
                }
                var LocalID = $('#LocalID_Query').combobox('getValue');
                if (LocalID != '' && LocalID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + " dbo.funReturnAssetLocaID(A.AssetID)=" + LocalID;
                }
                var MaintType = $('#MaintType_Query').combobox('getValue');
                if (MaintType != '' && MaintType != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + " B.MaintTypeID = " + "'" + MaintType + "'";
                }
                var IsNotLabel = $('#IsNotLabel_Query').checkbox('getValue');
                if (IsNotLabel == 1) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    FiltStr = FiltStr + " A.IsLabel = 0";
                }
                var IsCatalogue = $('#IsCatalogue_Query').checkbox('getValue');
                if (IsCatalogue != '' && IsCatalogue != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and ";
                    }
                    FiltStr = FiltStr + " A.IsCatalogue = " + "'" + IsCatalogue + "'";
                }
                var StatusID = $('#StatusID_Query').combobox('getValue');
                if (StatusID != '' && StatusID != undefined) {
                    if (FiltStr != '' && FiltStr != undefined) {
                        FiltStr = FiltStr + " and "
                    }
                    if (StatusID == 1) {
                        FiltStr = FiltStr + " dbo.funReturnAssetTranType(A.AssetID) != 2";
                    }
                    else {
                        FiltStr = FiltStr + "Case A.IsInventory  when 1 then 3 else dbo.funReturnAssetTranType(A.AssetID) end = " + StatusID;
                    }
                }
                $(dg).datagrid('setWhere', FiltStr);
                var IsLabelList = $('#IsLabelList_Query').checkbox('getValue');
                var DGVColumns = ['GroupName', 'QRCode', 'AssetID', 'AssetName', 'AssetSpecs', 'TranTypeID', 'AssetQty', 'AssetUnit', 'AssetGetType', 'AssetGetDate', 'AssetGetYM', 'UsefulYears', 'IsCatalogue', 'CompID', 'OwnerID', 'LocalID', 'AssetPlace', 'ItemTypeID', 'AssetPhotoPath','IsLabel'];
                HideGridColumns("#dataGridView", DGVColumns);
                if (IsLabelList == 1) {
                    var DGVColumns = ['GroupName', 'QRCode', 'AssetID', 'AssetName', 'AssetGetDate', 'AssetPlace'];
                    ShowGridColumns("#dataGridView", DGVColumns);
                }
                else {
                    var DGVColumns = ['AssetID', 'AssetName', 'AssetSpecs', 'TranTypeID', 'AssetQty', 'AssetUnit', 'AssetGetType', 'AssetGetDate', 'AssetGetYM', 'UsefulYears', 'IsCatalogue', 'CompID', 'OwnerID', 'LocalID', 'AssetPlace', 'ItemTypeID', 'AssetPhotoPath', 'IsLabel'];
                    ShowGridColumns("#dataGridView", DGVColumns);
                }
            }
        }
        function dataFormMasterOnApply() {
            var AssetGetType = $('#dataFormMasterAssetGetType').combobox('getValue');
            if (AssetGetType == "" || AssetGetType == undefined) {
                alert('注意!!,未選取得方式,請選取!!');
                $("#dataFormMasterAssetGetType").focus();
                return false;
            }
            var AssetUnit = $('#dataFormMasterAssetUnit').combobox('getValue');
            if (AssetUnit == "" || AssetUnit == undefined) {
                alert('注意!!,未選取或輸入單位,請輸入或選取!!');
                $("#dataFormMasterAssetUnit").focus();
                return false;
            }
            if (getEditMode($("#dataFormMaster")) == 'Inserted') {
                var AssetCompID = $('#dataFormMasterAssetCompID').combobox('getValue');
                if (AssetCompID == "" || AssetCompID == undefined) {
                    alert('注意!!,未選取帳務歸屬,請選取!!');
                    $("#dataFormMasterAssetCompID").focus();
                    return false;
                }
                var AssetOwnerID = $('#dataFormMasterAssetOwnerID').combobox('getValue');
                if (AssetOwnerID == "" || AssetOwnerID == undefined) {
                    alert('注意!!,未選取保管人員,請選取!!');
                    $("#dataFormMasterAssetOwnerID").focus();
                    return false;
                }
                var AssetLocaID = $('#dataFormMasterAssetLocaID').combobox('getValue');
                if (AssetLocaID == "" || AssetLocaID == undefined) {
                    alert('注意!!,未選取區域位置,請選取!!');
                    $("#dataFormMasterAssetLocaID").focus();
                    return false;
                }
            }
        }
        function dataFormMasterOnApplied() {
            if (getEditMode($("#dataFormMaster")) == 'updated') {
                var AssetID = $('#dataFormMasterAssetID').val();
                //alert(AssetID);
                var IsInventory = $('#dataFormMasterIsInventory').checkbox('getValue');
                //alert(IsInventory);
                var ok = procPutAssetInventoryLogs(AssetID, IsInventory);
            }
   
            var idx = $('#dataGridView').datagrid('getRowIndex', $('#dataGridView').datagrid('getSelected'));
            var hh = parseInt(idx);
            $('#dataGridView').datagrid('reload');
            $('#dataGridView').datagrid('selectRow', hh);
           
        }
        //寫入資產庫存狀態
        function procPutAssetInventoryLogs(AssetID, IsInventory) {
            var Username = getClientInfo('username');
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetMaster.AssetMaster',
                data: "mode=method&method=" + "procPutAssetInventoryLogs" + "&parameters=" + AssetID + ',' + IsInventory + ',' + Username,
                cache: false,
                async: false,
                success: function (data) {
                }
            });
        }
        function dataGridViewOnUpdate() {
            var RowData = $('#dataGridView').datagrid('getSelected');
            var TranTypeID = RowData.TranTypeID;
            if (TranTypeID == 4) {
                alert('注意!!,設備狀態「報廢」,無法修改');
                return false;
            }
            var VL = returnIsAssetAuditor();
            if (VL == -1) {
                alert('注意!!,你無權限修改資料,請改用檢視');
                return false;
            }
            return true;
        }
        function dataGridViewOnLoadSucess()
        {
            var RecNO = $("#dataFormMasterRecNO").val();
            if ( (RecNO != "") ) {
                $('#dataGridView').datagrid('selectRow', RecNO);
            }
            var where = "DESCRIPTION='JB'";
            $('#OwnerID_Query').combobox('setWhere', where);
            var where = "AssetLocaID IN (Select AssetLocaID From AssetBelongLogs Group By AssetLocaID)";
            $('#LocalID_Query').combobox('setWhere', where);
            //$('#dataFormDetailAssetOwnerID').combobox('setWhere', where);
            //var DGVColumns = ['AssetID', 'AssetName', 'AssetSpecs', 'TranTypeID', 'AssetQty', 'AssetUnit', 'AssetGetType', 'AssetGetYM', 'UsefulYears', 'IsCatalogue', 'CompID', 'OwnerID', 'LocalID', 'AssetPlace', 'ItemTypeID', 'AssetPhotoPath'];
            //ShowGridColumns("#dataGridView", DGVColumns);
            
        }

        function ShowGridColumns(GridName, ColumnNames) {
            $.each(ColumnNames, function (index, value) {
                $(GridName).datagrid('showColumn', value);//closest上一層selector,prev下一個selector
            });
        }
        function HideGridColumns(GridName, ColumnNames) {
            $.each(ColumnNames, function (index, value) {
                $(GridName).datagrid('hideColumn', value);//closest上一層selector,prev下一個selector
            });
        }
       function DeleteGridAssetMaster() {
            var con = confirm("確定刪除嗎?");
            if (con == true) {
                var VL = returnIsAssetAuditor();
                if (VL == -1) {
                    alert('注意!!,你無權限刪除資料');
                    return false;
                }
                var RowData = $('#dataGridView').datagrid('getSelected');
                if (RowData.TranTypeID == 4) {
                    alert('注意!!,設備狀態「報廢」,無法刪除');
                    return false;
                }
                procDeleteAssetMaster();
                $('#dataGridView').datagrid('reload');
            }
            return true;
        }
        function undeleteGridAssetMaster() {
            var VL = returnIsAssetAuditor();
            if (VL == -1) {
                alert('注意!!,你無權限復原資料');
                return false;
            }
            var RowData = $('#dataGridView').datagrid('getSelected');
            var UseStatusID = RowData.UseStatusID;
            if (UseStatusID != -1) {
                alert('注意!!,設備狀態不是「刪除」,無法復原');
                return false;
            }
            procUNDeleteAssetMaster();
            $('#dataGridView').datagrid('reload');
            return true;
        }
        function dataGridViewOnInsert() {
            var VL = returnIsAssetAuditor();
            if (VL == -1) {
                alert('注意!!,你無權限新增資料,請改以資產設備異動單申請');
                return false;
            }
            return true;
        }
        function GetMaxAssetID() {
            var UserID = getClientInfo("UserID");
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetMaster.AssetMaster',
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
       //傳回登入者是否為資料維護者,傳回值=-1 不是系統維護者
        function returnIsAssetAuditor() {
            var UserID = getClientInfo("UserID");
            var row = $('#JQDataGrid2').datagrid('getSelected');
            var ViewList = row.AssetAuditor;
            var UserRight = ViewList.indexOf(UserID);
            return UserRight;
        }
        function dataFormDetailOnLoadSuccess() {
            if (getEditMode($("#dataFormDetail")) == 'inserted') {
                var row = $('#dataGridDetail').datagrid('getSelected');
                var where = "DESCRIPTION='JB'";
                $('#dataFormDetailAssetOwnerID').combobox('setWhere',where);
            }
        }
        function procDeleteAssetMaster() {
            var RowData = $('#dataGridView').datagrid('getSelected');
            var AssetID = RowData.AssetID;
            var Username = getClientInfo('username');
            var ReturnStr = "";
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetMaster.AssetMaster',
                data: "mode=method&method=" + "procDeleteAssetMaster" + "&parameters=" + AssetID + ',' + Username,
                cache: false,
                async: false,
                success: function (data) {
                }
            });
        }
        //選擇物品名
        function ItemIDOnSelect(rowData) {
            
            $("#dataFormMasterMaintTypeID").combobox('setValue', rowData.MaintTypeID);
            $("#dataFormMasterMaintFreqID").combobox('setValue', rowData.MaintFreqID);
        }
        //
        function ItemTypeOnSelect(rowData) {
             
             $('#dataFormMasterItemID').combobox('setWhere', "ItemTypeID = '" + rowData.ITEMTYPEID + "'");
        }
        function TranTypeOnSelect(rowData) {
            $("#dataFormDetailTranType").val(rowData.TranTypeName);
        }
        function AssetLocateOnSelect(rowData) {
             $("#dataFormDetailAssetOwnerID").combobox('setValue', rowData.AssetOwnerID);
        }
       function CopyAssetApplyDetails() {
            var rowcount = $('#dataGridDetail').datagrid('getData').total;
            if (rowcount <= 0) {
                alert('注意!! 沒有異動資料,本功能無法使用');
                return false;
            }
            var row = $('#dataGridDetail').datagrid('getSelected');           
            $('#dataGridDetail').datagrid('appendRow', row);
            row.AssetBelongLogsNO = row.AssetBelongLogsNO+1;
            openForm('#JQDialog2', row, "updated", 'dialog');
        }
        function genCheckBox(val) {
            if (val)
                return "<input  type='checkbox' checked='true' checked onclick='return false;' />";
            else
                return "<input  type='checkbox' onclick='return false;' />";
        }
        function dataGridDetailOnInsert() {
            var dMItemTypeID = $("#dataFormMasterItemTypeID").combobox('getValue');
            if (dMItemTypeID == "" || dMItemTypeID == undefined) {
                alert('注意!!,未選取設備類別,請選取!!');
                $("#dataFormMasterItemTypeID").focus();
                return false;
            }
        }
        function AddLabel() {
            var rows = $('#dataGridView').datagrid("getChecked");
            if (rows.length <= 0) {
                alert('注意!!未選物品資產,請選取');
                return false;
            }
            var count = rows.length;
            var UserID = getClientInfo("UserID");
            var AssetIDStr = '';
            for (var i = 0; i <= rows.length - 1; i++) {
                if (i > 0)
                    AssetIDStr = AssetIDStr + ',' + rows[i].AssetID;
                else
                    AssetIDStr = AssetIDStr + rows[0].AssetID;
            }
            $.ajax({
                type: "POST",
                url: '../handler/jqDataHandle.ashx?RemoteName=sAssetMaster.AssetMaster',
                data: "mode=method&method=" + "procAddAssetIDLabel" + " &parameters=" + AssetIDStr + "*" + UserID,
                cache: false,
                async: false,
                success: function (data) {
                    if (data == "True") {
                    }
                    else {
                        alert("注意!! 聯絡人加入標籤失敗")
                    }
                    $('#dataGridView').datagrid('reload');
                }
            });
          
        }
        function JQDataForm1OnLoadSucess() {
            var AssetID = $("#dataFormMasterAssetID").val();
            var AssetName = $("#dataFormMasterAssetName").val();
            $("#JQDataForm1AssetID").val(AssetID);
            $("#JQDataForm1AssetName").val(AssetName);
            
        }
       function  getAssetGetYM() {
        }
        function AssetGetDateOnBlur() {
            //var aa = $("#dataFormMasterAssetGetDate").datebox('getValue');
            //$("#dataFormMasterAssetGetYM").val(aa.substr(0, 4) + '/' + aa.substr(5, 2))
         }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sAssetMaster.AssetMaster" runat="server" AutoApply="True"
                DataMember="AssetMaster" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="True" AlwaysClose="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="True" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="30px" QueryMode="Panel" QueryTop="50px" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" OnUpdate="dataGridViewOnUpdate" OnInsert="dataGridViewOnInsert" OnLoadSuccess="dataGridViewOnLoadSucess" BufferView="False" NotInitGrid="False" RowNumbers="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="GroupName" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="140">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="QRCode" Editor="text" FieldName="QRCode" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="510">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="False" Width="55" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="設備名稱" Editor="text" FieldName="AssetName" Format="" MaxLength="0" Width="180" Sortable="True" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="規格" Editor="text" FieldName="AssetSpecs" MaxLength="0" Width="300" Format="" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="維修" Editor="text" FieldName="MaintCou" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="35">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="狀態" Editor="infocombobox" FieldName="TranTypeID" MaxLength="0" Width="40" EditorOptions="valueField:'UseStatusID',textField:'UseStatusDisp',remoteName:'sAssetMaster.AssetUseStatus',tableName:'AssetUseStatus',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FormatScript="" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="數量" Editor="numberbox" FieldName="AssetQty" Format="" Width="30" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="單位" Editor="text" FieldName="AssetUnit" Format="" MaxLength="0" Width="35" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="標籤" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsLabel" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="40">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="廠牌" Editor="text" FieldName="BrandSpecs" MaxLength="0" Width="160" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="取得方式" Editor="text" FieldName="AssetGetType" Format="" MaxLength="0" Width="55" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="取得年月" Editor="text" FieldName="AssetGetYM" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="55" />
                    <JQTools:JQGridColumn Alignment="center" Caption="使用年限" Editor="numberbox" FieldName="UsefulYears" Format="" Width="55" EditorOptions="" MaxLength="0" Visible="False" />
                    <JQTools:JQGridColumn Alignment="center" Caption="財產目錄" Editor="checkbox" FieldName="IsCatalogue" FormatScript="genCheckBox" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="60">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="所屬公司" Editor="infocombobox" FieldName="CompID" Format="" Width="70" Visible="False" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="保管人" Editor="infocombobox" FieldName="OwnerID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="70" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                    <JQTools:JQGridColumn Alignment="left" Caption="區域位置" Editor="infocombobox" FieldName="LocalID" Format="" MaxLength="0" Width="100" Visible="False" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                    <JQTools:JQGridColumn Alignment="left" Caption="存放位置" Editor="text" FieldName="AssetPlace" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="請購類別" Editor="infocombobox" EditorOptions="valueField:'ITEMTYPEID',textField:'ITEMTYPENAME',remoteName:'sAssetMaster.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="ItemTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="100">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="center" Caption="實物照片" Editor="text" FieldName="AssetPhotoPath" Format="Image,Folder:JB_ADMIN/Images,Height:45" MaxLength="0" Width="60" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PurchaseNO" Editor="text" FieldName="PurchaseNO" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="系統編號" Editor="numberbox" FieldName="AssetMasterNO" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"     OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-cut" ItemType="easyui-linkbutton"  OnClick="DeleteGridAssetMaster" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportXlsx" Text="匯出Excel" Visible="True" />
                    <JQTools:JQToolItem Icon="icon-sum" ItemType="easyui-linkbutton"   OnClick="AddLabel" Text="已貼標簽" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="請輸入關鍵字" Condition="%%" DataType="string" Editor="text" FieldName="AssetName" IsNvarChar="False" NewLine="False" RemoteMethod="False" Width="183" Span="1" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="資產保管人" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OwnerID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="90" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="保養屬性" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sAssetMaster.ItemMaintType',tableName:'ItemMaintType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MaintType" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="80" DefaultValue="" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="資產狀態" Condition="=" DataType="number" Editor="infocombobox" EditorOptions="valueField:'UseStatusID',textField:'UseStatus',remoteName:'sAssetMaster.AssetUseStatus',tableName:'AssetUseStatus',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="StatusID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="100" DefaultValue="1" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="所在區域" Condition="%%" DataType="string" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LocalID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="1" Width="190" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="未列印標籤" Condition="%" DataType="string" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsNotLabel" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="10" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="財產目錄" Condition="%" DataType="string" DefaultValue="" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsCatalogue" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="10" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="標籤列表" Condition="=" DataType="number" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsLabelList" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="1" Width="10" DefaultValue="" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="資產設備維護" DialogLeft="10px" DialogTop="30px" Width="1080px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="AssetMaster" HorizontalColumnsCount="4" RemoteName="sAssetMaster.AssetMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" OnApply="dataFormMasterOnApply" OnLoadSuccess="dataFormMasterLoadSucess" OnApplied="dataFormMasterOnApplied" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" Width="1020px" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="資產編號" Editor="text" FieldName="AssetID" Format="" Width="60" ReadOnly="True" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備名稱" Editor="text" FieldName="AssetName" Format="" Width="240" ReadOnly="False" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資產類別" Editor="infocombobox" FieldName="ItemTypeID" maxlength="0" Width="120" EditorOptions="valueField:'ITEMTYPEID',textField:'ITEMTYPENAME',remoteName:'sAssetMaster.ItemType',tableName:'ItemType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ItemTypeOnSelect,panelHeight:200" Span="1" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="物品品名" Editor="infocombobox" EditorOptions="valueField:'ITEMID',textField:'ITEMNAME',remoteName:'sAssetMaster.Items',tableName:'Items',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:ItemIDOnSelect,panelHeight:200" FieldName="ItemID" MaxLength="0" Width="105" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="品牌規格" Editor="text" FieldName="AssetSpecs" Span="2" Width="470" Format="" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="取得方式" Editor="infocombobox" FieldName="AssetGetType" Format="" Width="120" EditorOptions="valueField:'AssetGetType',textField:'AssetGetType',remoteName:'sAssetMaster.AssetGetType',tableName:'AssetGetType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="right" Caption="使用年限" Editor="numberbox" EditorOptions="precision:1" FieldName="UsefulYears" Format="" Width="98" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="right" Caption="數量" Editor="numberbox" FieldName="AssetQty" Format="" Width="40" maxlength="0" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="單位" Editor="infocombobox" FieldName="AssetUnit" Format="" Width="45" EditorOptions="valueField:'ASSETUNIT',textField:'ASSETUNIT',remoteName:'sAssetMaster.AssetUnit',tableName:'AssetUnit',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="取得日期" Editor="datebox" FieldName="AssetGetDate" Format="" Span="1" Width="150" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="採購單號" Editor="text" FieldName="PONO" Format="" Span="1" Width="98" Visible="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="存放位置" Editor="infocombobox" FieldName="AssetPlace" Width="175" EditorOptions="valueField:'AssetPlace',textField:'AssetPlace',remoteName:'sAssetMaster.AssetPlace',tableName:'AssetPlace',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Span="1" Visible="True" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="已印標籤" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsLabel" MaxLength="0" Span="1" Visible="True" Width="20" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保養屬性" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sAssetMaster.ItemMaintType',tableName:'ItemMaintType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MaintTypeID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="120" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保養頻度" Editor="infocombobox" EditorOptions="valueField:'ID',textField:'Contents',remoteName:'sAssetMaster.ItemMaintFreq',tableName:'ItemMaintFreq',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="MaintFreqID" MaxLength="0" ReadOnly="True" Span="1" Visible="True" Width="105" />
                        <JQTools:JQFormColumn Alignment="left" Caption="實物照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Images',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="AssetPhotoPath" Format="" Visible="True" Width="50" Span="2" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="標簽照片" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'JB_ADMIN/Images',showButton:true,showLocalFile:false,fileSizeLimited:'5000'" FieldName="AssetPhotoPath1" Span="2" Width="50" Visible="True" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="備註" Editor="textarea" FieldName="AssetNotes" Span="4" Width="470" MaxLength="0" EditorOptions="height:30" />
                        <JQTools:JQFormColumn Alignment="left" Caption="存入庫存" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsInventory" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="4" Visible="True" Width="20" />
                        <JQTools:JQFormColumn Alignment="left" Caption="財產目錄" Editor="checkbox" EditorOptions="on:1,off:0" FieldName="IsCatalogue" MaxLength="0" Span="1" Width="20" ReadOnly="False" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="區域位置" Editor="infocombobox" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="LocalID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="280" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="保管人員" Editor="infocombobox" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="OwnerID" maxlength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="140" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="帳務歸屬" Editor="infocombobox" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="CompID" Visible="True" Width="120" Format="" ReadOnly="True" MaxLength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetMasterNO" Editor="numberbox" FieldName="AssetMasterNO" Span="1" Visible="False" Width="60" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" MaxLength="0" ReadOnly="False" Span="1" Visible="False" Width="180" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改人員" Editor="text" FieldName="LastUpdateBy" MaxLength="0" ReadOnly="False" Visible="False" Width="180" Format="" Span="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="設備類別" Editor="infocombobox" FieldName="AssetType" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="140" EditorOptions="valueField:'ASSETTYPE',textField:'ASSETTYPE',remoteName:'sAssetMaster.AssetType',tableName:'AssetType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Format="" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" maxlength="0" ReadOnly="False" Visible="False" Width="180" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="RecNO" Editor="text" FieldName="RecNO" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UseStatusID" Editor="text" FieldName="UseStatusID" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" MaxLength="0" Visible="False" Width="80" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AssetGetYM" Editor="text" FieldName="AssetGetYM" Width="80" Visible="False" MaxLength="0" ReadOnly="False" Span="1" NewRow="False" RowSpan="1" />
                        <JQTools:JQFormColumn Alignment="left" Caption="IsActive" Editor="text" FieldName="IsActive" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="MaintCou" Editor="text" FieldName="MaintCou" maxlength="0" ReadOnly="False" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>
                <div id="div_dataGridDetail">
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="AssetBelongLogs" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sAssetMaster.AssetMaster" Title="異動狀態" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" OnInsert="dataGridDetailOnInsert" RowNumbers="True" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="right" Caption="AssetBelongLogsNO" Editor="numberbox" FieldName="AssetBelongLogsNO" Format="" Width="120" Visible="False" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AssetID" Editor="text" FieldName="AssetID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動別" Editor="infocombobox" FieldName="TranType" Format="" Width="60" EditorOptions="valueField:'TranTypeName',textField:'TranTypeName',remoteName:'sAssetMaster.AssetTranType',tableName:'AssetTranType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="異動編號" Editor="text" FieldName="TranNO" Width="80" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Format="yyyy/mm/dd" Width="100" Sortable="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="區域位置" Editor="infocombobox" FieldName="AssetLocaID" Format="" Width="280" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="帳目歸屬" Editor="infocombobox" FieldName="AssetCompID" Format="" Width="100" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="保管人員" Editor="infocombobox" FieldName="AssetOwnerID" Format="" Visible="True" Width="80" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy/mm/dd HH:MM:SS" Width="120" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="TranTypeID" Editor="text" FieldName="TranTypeID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                        </JQTools:JQGridColumn>
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="AssetID" ParentFieldName="AssetID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    </TooItems>
                   <%-- <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="CopyAssetApplyDetails" Text="新增異動" />
                    </TooItems>--%>
                </JQTools:JQDataGrid>
                </div>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="40px" DialogTop="220px" Title="異動維護" Width="960px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="dataFormMaster" DataMember="AssetBelongLogs" HorizontalColumnsCount="7" RemoteName="sAssetMaster.AssetMaster" Closed="False" ContinueAdd="False" disapply="False" DuplicateCheck="False" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" AlwaysReadOnly="False" DivFramed="False" HorizontalGap="0" VerticalGap="0" OnLoadSuccess="dataFormDetailOnLoadSuccess" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="紀錄編號" Editor="numberbox" FieldName="AssetBelongLogsNO" Format="" Width="77" Visible="False" ReadOnly="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="AssetID" Editor="text" FieldName="AssetID" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="異動別" Editor="infocombobox" FieldName="TranTypeID" Format="" Width="80" EditorOptions="valueField:'TranTypeID',textField:'TranTypeName',remoteName:'sAssetMaster.AssetTranType',tableName:'AssetTranType',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:TranTypeOnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="位置區域" Editor="infocombobox" FieldName="AssetLocaID" Format="" Width="220" EditorOptions="valueField:'AssetLocaID',textField:'AssetLocaName',remoteName:'sAssetMaster.AssetLocate',tableName:'AssetLocate',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:AssetLocateOnSelect,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="帳務歸屬" Editor="infocombobox" FieldName="AssetCompID" Format="" Width="130" EditorOptions="valueField:'InsGroupID',textField:'InsGroupName',remoteName:'sAssetMaster.InsGroup',tableName:'InsGroup',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQFormColumn Alignment="left" Caption="保管者" Editor="infocombobox" FieldName="AssetOwnerID" Format="" Width="90" EditorOptions="valueField:'EmployeeID',textField:'EmployeeName',remoteName:'sAssetMaster.Employee',tableName:'Employee',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="生效日期" Editor="datebox" FieldName="EffectDate" Format="yyyy/mm/dd" Width="90" EditorOptions="" Visible="True" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                            <JQTools:JQFormColumn Alignment="left" Caption="TranType" Editor="text" FieldName="TranType" ReadOnly="False" Visible="False" Width="80" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="AssetID" ParentFieldName="AssetID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                </JQTools:JQDialog>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AssetQty" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AssetMasterNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="AssetGetDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="AssetID" RemoteMethod="False" DefaultMethod="GetMaxAssetID" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="購置" FieldName="AssetGetType" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0.0" FieldName="UsefulYears" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="UseStatusID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="台" FieldName="AssetUnit" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="3" FieldName="AssetCompID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="AssetOwnerID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="Z  " FieldName="Flowflag" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="1" FieldName="AssetGroupTypeID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="getAssetGetYM" FieldName="AssetGetYM" RemoteMethod="False" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="True" FieldName="IsActive" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsLabel" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="IsInventory" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="AssetName" RemoteMethod="True" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AssetBelongLogsNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="EffectDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="AssetOwnerID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="AssetCompID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" FieldName="AssetLocaID" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
          <div style="display:none;">
            <JQTools:JQDataGrid ID="JQDataGrid2" runat="server" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" AutoApply="True" CheckOnSelect="True" ColumnsHibeable="False" DataMember="SYSVar" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,30,40,50" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RemoteName="sAssetMaster.SYSVar" Title="JQDataGrid" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" BufferView="False" NotInitGrid="False" RowNumbers="True">
               <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="AssetAuditor" Editor="text" FieldName="AssetAuditor" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80" />
                </Columns>
            </JQTools:JQDataGrid>
          </div>
         <JQTools:JQDialog ID="JQDialog4" runat="server" DialogLeft="90px" DialogTop="75px" Title="維修紀錄" Width="720px" Closed="True" ShowSubmitDiv="False" DialogCenter="False" EditMode="Dialog" EnableTheming="True" ScrollBars="Vertical">
                 <JQTools:JQDataGrid ID="JQDataGridMD" runat="server" EditDialogID="JQDialog5" AlwaysClose="True" DataMember="ItemMaintDetails" RemoteName="sAssetMaster.ItemMaintDetails" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AutoApply="True" CheckOnSelect="False" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" PageList="10,20,40,60,80" PageSize="10" Pagination="True" QueryAutoColumn="False" QueryLeft="120px" QueryMode="Window" QueryTitle="" QueryTop="50px" RecordLock="False" RecordLockMode="None" Title="" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" Height="370px" Width="645px" BufferView="False" NotInitGrid="False" RowNumbers="True">
                     <Columns>
                         <JQTools:JQGridColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="AssetID" Editor="text" FieldName="AssetID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="維修日期" Editor="datebox" FieldName="MaintDate" Format="yyyy/mm/dd" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="65">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="維修內容" Editor="text" FieldName="MaintDesc" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="300">
                         </JQTools:JQGridColumn>
                         <JQTools:JQGridColumn Alignment="left" Caption="維修紀錄檔" Editor="infofileupload" FieldName="MaintDocFile" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="160" Format="download,folder:files/MaintFiles">
                         </JQTools:JQGridColumn>
                     </Columns>
                     <TooItems>
                         <JQTools:JQToolItem Enabled="True" Icon="icon-add"  ItemType ="easyui-linkbutton"  OnClick="insertItem"    Text="新增" Visible="True" />
                         <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton"   OnClick="exportXlsx" Text="匯出Excel" Visible="True" />
                     </TooItems>
                </JQTools:JQDataGrid>
          </JQTools:JQDialog>
              <JQTools:JQDialog ID="JQDialog5" runat="server" BindingObjectID="JQDataForm1" Title="維修紀錄" Width="450px" DialogLeft="220px" DialogTop="180px">
                <JQTools:JQDataForm ID="JQDataForm1" runat="server" DataMember="ItemMaintDetails" HorizontalColumnsCount="1" RemoteName="sAssetMaster.ItemMaintDetails" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="JQDataForm1OnLoadSucess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="False" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetID" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="資產名稱" Editor="text" FieldName="AssetName" MaxLength="0" NewRow="False" ReadOnly="True" RowSpan="1" Span="1" Visible="True" Width="230" />
                        <JQTools:JQFormColumn Alignment="left" Caption="維修日期" Editor="datebox" FieldName="MaintDate" Format="yyyy/mm/dd" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="90" />
                        <JQTools:JQFormColumn Alignment="left" Caption="維修內容" Editor="textarea" EditorOptions="height:60" FieldName="MaintDesc" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="230" />
                        <JQTools:JQFormColumn Alignment="left" Caption="維修單上傳" Editor="infofileupload" FieldName="MaintDocFile" MaxLength="0" ReadOnly="False" Span="1" Visible="True" Width="60" NewRow="False" RowSpan="1" EditorOptions="filter:'',isAutoNum:true,upLoadFolder:'Files/MaintFiles',showButton:true,showLocalFile:false,fileSizeLimited:'500'" />
                    </Columns>
                </JQTools:JQDataForm>
                 <JQTools:JQDefault ID="default2" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="MaintDate" RemoteMethod="True" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                     </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="JQValidate1" runat="server" BindingObjectID="JQDataForm1" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="ContactDescr" RemoteMethod="True" ValidateMessage="不可空白" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
    </form>
</body>
</html>
