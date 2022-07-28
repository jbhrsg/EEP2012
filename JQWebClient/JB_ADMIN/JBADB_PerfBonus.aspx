<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBADB_PerfBonus.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script src="../js/jquery.jbjob.js"></script>
     <script type="text/javascript">
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
                 
             });
             //設定轉入資料
             var dfRawExcel = $('#dataFormMasterRawExcel').closest('td');
             dfRawExcel.append($('<a>', { href: 'javascript:void(0)' }).on('click', function () {
                 //var UserID = getClientInfo("UserID");
                 //var FiltStr = "ApplyEmpID = '" + UserID + "'";
                 //$("#JQDataGridOutDoorList").datagrid('setWhere', FiltStr);
                 //openForm('#JQDialogOutDoorList', {}, "", 'dialog');
                 return true;
             }).linkbutton({ text: '轉入資料' }));
         });
         function PerfBonusYMOnSelect(rowData) {
             $("#dataFormMasterSalaryYM").combobox('setValue', rowData.SalaryYM);
         }
         function dataFormMaster_OnLoadSuccess() {
             if (getEditMode($("#dataFormMaster")) == 'inserted') {
                 var orgno = GetUserOrgNOs();
                 alert(oragno);
                 $("#dataFormMasterOrg_NOParent").combobox('setValue',orgno);
             }
         }
         function GetUserOrgNOs() {
             var UserID = getClientInfo("UserID");
             var _return = '';
             $.ajax({
                 type: "POST",
                 url: '../handler/jqDataHandle.ashx?RemoteName=sPerfBonusMaster.PerfBonusMaster',
                 data: "mode=method&method=" + "GetUserOrgNOs" + "&parameters=" + UserID,
                 cache: false,
                 async: false,
                 success: function (data) {
                     var rows = $.parseJSON(data);
                     if (rows.length > 0) {
                         //alert(rows[0].OrgNO);
                         //$("#dataFormMasterOrg_NOParent").combobox('setValue', rows[0].OrgNO);
                         _return = rows[0].OrgNO;
                     }
                  }
             }
             );
             return _return;
         }
     </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sPerfBonusMaster.PerfBonusMaster" runat="server" AutoApply="True"
                DataMember="PerfBonusMaster" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="績效獎金簽核">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusNO" Editor="text" FieldName="PerfBonusNO" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusYM" Editor="text" FieldName="PerfBonusYM" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyEmpID" Editor="text" FieldName="ApplyEmpID" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ApplyDate" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Org_NOParent" Editor="text" FieldName="Org_NOParent" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="SalaryYM" Editor="text" FieldName="SalaryYM" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="績效獎金簽核" Width="840px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="PerfBonusMaster" HorizontalColumnsCount="4" RemoteName="sPerfBonusMaster.PerfBonusMaster" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="dataFormMaster_OnLoadSuccess" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="簽核單號" Editor="text" FieldName="PerfBonusNO" Format="" maxlength="0" Width="112" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請人員" Editor="infocombobox" FieldName="ApplyEmpID" Format="" Width="120" EditorOptions="valueField:'USERID',textField:'USERNAME',remoteName:'sPerfBonusMaster.EmpList',tableName:'EmpList',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請日期" Editor="datebox" FieldName="ApplyDate" Format="" Width="120" maxlength="0" />
                        <JQTools:JQFormColumn Alignment="left" Caption="申請部門" Editor="infocombobox" FieldName="Org_NOParent" Format="" Width="120" maxlength="0" EditorOptions="valueField:'ORG_NO',textField:'ORG_DESC',remoteName:'sPerfBonusMaster.ORG',tableName:'ORG',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="績效年月" Editor="infocombobox" FieldName="PerfBonusYM" Format="" maxlength="0" Width="120" EditorOptions="valueField:'PerfBonusYM',textField:'PerfBonusYM',remoteName:'sPerfBonusMaster.PerfBonusYM',tableName:'PerfBonusYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:PerfBonusYMOnSelect,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="薪資年月" Editor="infocombobox" FieldName="SalaryYM" Format="" maxlength="0" Width="120" EditorOptions="valueField:'SalaryYM',textField:'SalaryYM',remoteName:'sPerfBonusMaster.SalaryYM',tableName:'SalaryYM',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" Visible="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" EditorOptions="filter:'',isAutoNum:false,upLoadFolder:'',showButton:true,showLocalFile:false,fileSizeLimited:'500'" FieldName="RawExcel" maxlength="0" ReadOnly="False" Span="2" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Flowflag" Editor="text" FieldName="Flowflag" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="False" DataMember="PerfBonusDetails" Pagination="False" ParentObjectID="dataFormMaster" RemoteName="sPerfBonusMaster.PerfBonusMaster" Title="明細資料" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True" >
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="PerfBonusNO" Editor="text" FieldName="PerfBonusNO" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工工號" Editor="text" FieldName="EmpID" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="員工姓名" Editor="text" FieldName="EmpName" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="績效金額" Editor="numberbox" FieldName="BonusAmt" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="調整金額" Editor="numberbox" FieldName="AdjustAmt" Format="" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="120" Visible="False" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="PerfBonusNO" ParentFieldName="PerfBonusNO" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    </TooItems>
                </JQTools:JQDataGrid>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="PerfBonusNO" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="ApplyEmpID" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="ApplyDate" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
                <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataGridDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataGridDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
