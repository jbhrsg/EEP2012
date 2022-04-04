<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMWorkNoLogs.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script> 
         $(document).ready(function () {
             //----------------------------------------------------------------------------------------------------
             ////取得流程狀態=>控制顯示項目
             parameter = Request.getQueryStringByName("D");
             //----------------------------------------------------------------------------------------------------

             //--------------------------------------★Need Update-----------------------------------------------
             //parameter = "Status2";//Need Update  ex: Modify
             //----------------------------------------------------------------------------------------------------             
         });
         function OnLoadSuccess() {

             //除了申請時=>顯示下載檔案
             if (parameter == "Status2") {
                 $("#dataFormMasterDownloadWorkImg").closest('td').prev('td').show();
                 $("#dataFormMasterDownloadWorkImg").closest('td').show();
             } else {
                 $("#dataFormMasterDownloadWorkImg").closest('td').prev('td').hide();
                 $("#dataFormMasterDownloadWorkImg").closest('td').hide();
             }
             //申請時=>上傳檔案	顯示紅色
             if (parameter != "Status2") {
                 $("#dataFormMasterWorkImg").closest('td').prev('td').css("color", "red");
             }

         }
         function checkApplyData() {
             //var WorkImg = $('#infoFileUploaddataFormMasterWorkImg').val();
             //if (WorkImg == "" ) {
             //    alert("請上傳檔案！");
             //    return false;
             //}
         }
     </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMWorkNoLogs.FWCRMWorkNoLogs" runat="server" AutoApply="True"
                DataMember="FWCRMWorkNoLogs" Pagination="True" QueryTitle="Query" EditDialogID="JQDialog1"
                Title="" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="True">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="OrderNo" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="WorkNo" Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="Memo" Editor="text" FieldName="Memo" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="flowflag" Editor="text" FieldName="flowflag" Format="" MaxLength="0" Visible="true" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" MaxLength="0" Visible="true" Width="120" />
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

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="聘工表修改申請" DialogTop="10px" DialogLeft="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="FWCRMWorkNoLogs" HorizontalColumnsCount="2" RemoteName="sFWCRMWorkNoLogs.FWCRMWorkNoLogs" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="True" IsAutoPause="False" IsAutoSubmit="True" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="True" ShowApplyButton="False" ValidateStyle="Hint" VerticalGap="0" OnLoadSuccess="OnLoadSuccess" OnApply="checkApplyData" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="inforefval" FieldName="OrderNo" Format="" maxlength="0" Width="130" EditorOptions="title:'選擇訂單',panelWidth:450,remoteName:'sFWCRMWorkNoLogs.infoOrderNo',tableName:'infoOrderNo',columns:[{field:'OrderNo',title:'訂單編號',width:95,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EmployerName',title:'雇主名稱',width:200,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'WorkNo',title:'聘工表號碼',width:95,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[{field:'WorkNo',value:'WorkNo'}],whereItems:[],valueField:'OrderNo',textField:'OrderNo',valueFieldCaption:'OrderNo',textFieldCaption:'訂單編號',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" NewRow="False" ReadOnly="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="聘工表號碼" Editor="text" FieldName="WorkNo" Format="" maxlength="0" Width="130" ReadOnly="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="修改內容" Editor="textarea" EditorOptions="height:80" FieldName="Memo" Format="" maxlength="0" NewRow="True" Width="450" RowSpan="1" Span="2" />
                        <JQTools:JQFormColumn Alignment="left" Caption="上傳檔案" Editor="infofileupload" FieldName="WorkImg" MaxLength="0" NewRow="True" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="290" Format="" EditorOptions="filter:'docx|xlsx|jpg|jpeg|png|bmp|gif|pptx|ppt',isAutoNum:false,upLoadFolder:'Files/FWCRM/Orders',showButton:true,showLocalFile:false,fileSizeLimited:'700'" />
                        <JQTools:JQFormColumn Alignment="left" Caption="下載檔案" Editor="text" FieldName="DownloadWorkImg" Format="download,folder:Files/FWCRM/Orders" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="True" Width="80" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="UserID" Editor="text" FieldName="UserID" Format="" maxlength="0" Width="180" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Item" Editor="text" FieldName="Item" MaxLength="0" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" Width="80" />
                    </Columns>
                </JQTools:JQDataForm>                
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" RemoteMethod="True" DefaultValue="_usercode" FieldName="UserID" />
                         <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="Item" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                     <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OrderNo" RemoteMethod="True" ValidateMessage="請選擇訂單" ValidateType="None" />
                         <JQTools:JQValidateColumn CheckNull="True" FieldName="Memo" RemoteMethod="True" ValidateMessage="修改內容不可空白！" ValidateType="None" />
                    </Columns>

                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
