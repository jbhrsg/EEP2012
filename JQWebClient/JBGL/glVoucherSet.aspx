<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glVoucherSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script src="../js/jquery.jbjob.js"></script>
    <script type="text/javascript">
        var flag = true; //定義一個全域變數，只有第一次執行

        //=================================================================參數設定==============================================================
        function OnLoadSuccessGV() { //執行setwehre方法，過濾自己的條件
            if (flag) {
                UserName = getClientInfo("UserName");
                var WhereString = "";
                WhereString = WhereString + "CreateBy = '" + UserName + "'";
                $("#dataGridView").datagrid('setWhere', WhereString);
                flag = false;
            }
            var data = $("#dataGridView").datagrid('getData');
            if (data.total > 0) {
                $("#toolItemdataGridView新增").hide();
            } else {
                $("#toolItemdataGridView新增").show();
            }
        }
         function OnInsertedGV() {
             $("#dataGridView").datagrid("reload");
         }

        //當選取 公司別 時,重新設定 傳票類別      
         function GetVoucherID(rowData) {
             //$("#dataFormMasterViewAreaID").combobox('setValue', "");
             $("#dataFormMasterVoucherID").combobox('setWhere', "CompanyID='" + rowData.CompanyID + "'");
             setTimeout(function () {
                 var data = $("#dataFormMasterVoucherID").combobox('getData');
                 var setvalue = data[0].VoucherID;
                 if (data.length > 0) {
                     $("#dataFormMasterVoucherID").combobox('setValue', setvalue);
                 }
             }, 500);
         }
        
        //=================================================================鎖檔==============================================================
         //---------------------------------------呼叫Method---------------------------------------
         var GetDataFromMethod = function (methodName, data) {
             var returnValue = null;
             $.ajax({
                 url: '../handler/JqDataHandle.ashx?RemoteName=sglVoucherSet',
                 data: { mode: 'method', method: methodName, parameters: $.toJSONString(data) },
                 type: 'POST',
                 async: false,
                 success: function (data) { returnValue = $.parseJSON(data); },
                 error: function (xhr, ajaxOptions, thrownError) { returnValue = null; }
             });
             return returnValue;
         };

        //TypeID 1=> 鎖檔年月 ,2 => 年轉
         function GetLockYM() {
             return GetDataFromMethod('GetLockYM', { TypeID: 1 });
         }

         function CallApply() {
             var pre = confirm("確認鎖檔?");
             if (pre == true) {
                 apply('#dataGridView2');
             }
         }
        //=================================================================年轉==============================================================
     
     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />          
                <div title="參數設定" style="padding:20px;">
                        <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglVoucherSet.glVoucherSet" runat="server" AutoApply="True"
                        DataMember="glVoucherSet" Pagination="False" QueryTitle="Query" EditDialogID="JQDialog1"
                        Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="False" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" Width="590px" OnInserted="OnInsertedGV" OnLoadSuccess="OnLoadSuccessGV">
                        <Columns>
                            <JQTools:JQGridColumn Alignment="right" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Visible="False" Width="120" />
                            <JQTools:JQGridColumn Alignment="center" Caption="公司別" Editor="infocombobox" FieldName="CompanyID" Format="" Visible="true" Width="160" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherMaster.glCompany',tableName:'glCompany',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQGridColumn Alignment="center" Caption="傳票類別" Editor="infocombobox" FieldName="VoucherID" Format="" MaxLength="0" Visible="true" Width="120" EditorOptions="valueField:'VoucherID',textField:'VoucherTypeName',remoteName:'sglVoucherMaster.infoglVoucherType',tableName:'infoglVoucherType',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" />
                            <JQTools:JQGridColumn Alignment="center" Caption="設定人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="center" Caption="設定日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM" Visible="true" Width="120" ReadOnly="True" />
                            <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="False" Width="80">
                            </JQTools:JQGridColumn>
                        </Columns>
                        <TooItems>
                             <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                                        OnClick="insertItem" Text="新增" />  
                            <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                                Text="存檔" />
                            <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                                Text="取消"  />
                        </TooItems>
                        <QueryColumns>
                        </QueryColumns>
                    </JQTools:JQDataGrid>
                        <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="設定">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="glVoucherSet" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sglVoucherSet.glVoucherSet" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="公司別" Editor="infocombobox" EditorOptions="valueField:'CompanyID',textField:'CompanyName',remoteName:'sglVoucherSet.glCompany',tableName:'glCompany',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,onSelect:GetVoucherID,panelHeight:200" FieldName="CompanyID" Format="" ReadOnly="False" Span="1" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="傳票類別" Editor="infocombobox" EditorOptions="title:'JQOptions',panelWidth:300,panelHeight:200,remoteName:'sglVoucherSet.glCompanyVoucher',tableName:'glCompanyVoucher',valueField:'VoucherID',textField:'VoucherTypeName',columnCount:3,multiSelect:false,openDialog:false,selectAll:false,selectOnly:false,items:[]" FieldName="VoucherID" Format="" maxlength="0" Span="1" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="text" FieldName="AutoKey" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="2" Visible="False" Width="80" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Visible="False" Width="180" />
                                    <JQTools:JQFormColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Visible="False" Width="180" />
                                </Columns>
                            </JQTools:JQDataForm>
                            <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                                    <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_usercode" FieldName="UserID" RemoteMethod="True" />
                                </Columns>
                            </JQTools:JQDefault>
                            <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                                <Columns>
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="CompanyID" RemoteMethod="True" ValidateMessage="請選擇公司別！" ValidateType="None" />
                                    <JQTools:JQValidateColumn CheckNull="True" FieldName="VoucherID" RemoteMethod="True" ValidateMessage="請選擇傳票類別！" ValidateType="None" />
                                </Columns>
                            </JQTools:JQValidate>
                        </JQTools:JQDialog>
                </div>                
        </div>
    </form>
</body>
</html>
