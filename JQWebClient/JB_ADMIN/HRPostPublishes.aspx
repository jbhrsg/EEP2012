<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HRPostPublishes.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>                                
        
         $(document).ready(function () {
             //panel寬度調整
             var dgid = $('#dataGridView');
             var queryPanel = getInfolightOption(dgid).queryDialog;
             if (queryPanel)
                 $(queryPanel).panel('resize', { width: 250 });

             //查詢條件預設值
             var dt = new Date();
             $("#Published_Query").datebox('setValue', $.jbjob.Date.DateFormat(dt, 'yyyy/MM/dd'));

         });

         function OnLoadSuccessGV() {
            
             //控制顯示複製按鈕
             var data = $("#dataGridView").datagrid('getData');
             if (data.total > 0) {                
                 $("#toolItemdataGridView複製出刊日").show();
                 $("#toolItemdataGridView刪除出刊日").show();
             } else {
                 $("#toolItemdataGridView複製出刊日").hide();
                 $("#toolItemdataGridView刪除出刊日").hide();
             }

         }

         //查詢條件出刊日期預設加一天
         function GetPublished() {
             var d = $("#Published_Query").datebox('getValue');
             var dt = new Date(d);
             var aDate = new Date($.jbDateAdd('days', 1, dt));
             return $.jbjob.Date.DateFormat(aDate, 'yyyy/MM/dd');
         }

         //呼叫視窗 複製出刊日
         function OpenHRPostPublishes() {            
             openForm('#JQDialog1', {}, "inserted", 'dialog');
         }

         //複製作業
         function CopyHRPostPublishes() {
             var Published = $("#Published_Query").datebox('getValue');
             var NewPublished = $('#dataFormMasterPublished').datebox('getValue');
             if (Published != "" && NewPublished != "" && Published!=NewPublished) {
                 var pre = confirm("確定從 " + Published + " 複製到 " + NewPublished + " ?");
                 var cnt;
                 if (pre == true) {                     
                     $.ajax({
                         type: "POST",
                         url: '../handler/jqDataHandle.ashx?RemoteName=sHRPostPublishes.HRPostPublishes', //連接的Server端，command
                         data: "mode=method&method=" + "CopyHRPostPublishes" + "&parameters=" + Published + "," + NewPublished, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                         cache: false,
                         async: false,
                         success: function (data) {
                             cnt = data;                             
                         },
                         error: function (xhr, ajaxOptions, thrownError) {
                             alert(xhr.status);
                             alert(thrownError);
                         }
                     });
                     if (cnt != "0") {
                         alert('複製成功！');
                         //把新的複製日期當作查詢條件
                         $("#Published_Query").datebox('setValue', NewPublished);
                         
                         var setstr = "Deleted=0 and Convert(nvarchar(10),Published,111)='" + NewPublished + "'";
                         $("#dataGridView").datagrid('setWhere', setstr);

                         closeForm('#JQDialog1');
                     } else alert('資料筆數為0！');
                 }
             } else {
                 alert('請填入出刊日期！');
                 return false;
             }
         }
         function DelHRPostPublishes() {
             var Published = $("#Published_Query").datebox('getValue');
             var pre = confirm("確定刪除 " + Published + "資料 ?");
             if (pre == true) {
                 $.ajax({
                     type: "POST",
                     url: '../handler/jqDataHandle.ashx?RemoteName=sHRPostPublishes.HRPostPublishes', //連接的Server端，command
                     data: "mode=method&method=" + "DelHRPostPublishes" + "&parameters=" + Published, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的UserID欄位
                     cache: false,
                     async: false,
                     success: function (data) {
                         cnt = data;
                     },
                     error: function (xhr, ajaxOptions, thrownError) {
                         alert(xhr.status);
                         alert(thrownError);
                     }
                 });
                 if (cnt == "0") {
                     alert('刪除成功！');                     

                     var setstr = " Convert(nvarchar(10),Published,111)='" + Published + "'";
                     $("#dataGridView").datagrid('setWhere', setstr);

                 } else alert('error！');
             }
         }

   </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sHRPostPublishes.HRPostPublishes" runat="server" AutoApply="True"
                DataMember="HRPostPublishes" Pagination="True" QueryTitle="查詢條件" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnLoadSuccess="OnLoadSuccessGV" Width="960px">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代碼" Editor="text" FieldName="Code" Format="" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="公司名稱" Editor="text" FieldName="Company" Format="" MaxLength="0" Visible="true" Width="100" />
                    <JQTools:JQGridColumn Alignment="left" Caption="顯示名稱" Editor="text" FieldName="DisplayName" Format="" MaxLength="0" Visible="true" Width="180" />
                    <JQTools:JQGridColumn Alignment="left" Caption="連絡電話" Editor="text" FieldName="Tel1" Format="" Visible="true" Width="170" />
                    <JQTools:JQGridColumn Alignment="left" Caption="地址" Editor="text" FieldName="Address1" Format="" Visible="true" Width="250" />
                    <JQTools:JQGridColumn Alignment="center" Caption="出刊日期" Editor="datebox" FieldName="Published" Format="" Visible="true" Width="100" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="OpenHRPostPublishes" Text="複製出刊日" />    
                     <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="DelHRPostPublishes" Text="刪除出刊日" />                 
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="出刊日期" Condition="=" DataType="datetime" Editor="datebox" FieldName="Published" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="105" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="複製作業" DialogLeft="200px" DialogTop="150px" ShowSubmitDiv="False" Width="350px">
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultMethod="GetPublished" FieldName="Published" RemoteMethod="False" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="Published" RemoteMethod="True" ValidateMessage="請填入出刊日期！" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
                <table style="width:100%;">
                    <tr>
                        <td align="center">
                            <JQTools:JQDataForm ID="dataFormMaster" runat="server" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" DataMember="HRPostPublishes" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalColumnsCount="2" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" RemoteName="sHRPostPublishes.HRPostPublishes" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0">
                                <Columns>
                                    <JQTools:JQFormColumn Alignment="left" Caption="出刊日期" Editor="datebox" FieldName="Published" Width="100" />
                                </Columns>
                            </JQTools:JQDataForm>
                        </td>
                        <td align="center"><a id="CoppyLink" class="easyui-linkbutton" data-options="plain:false" href="#" onclick="CopyHRPostPublishes.call(this)">複製</a></td>
                    </tr>
                </table>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
