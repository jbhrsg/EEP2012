<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FWCRMGetNo.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>   
         $(document).ready(function () {
             //$('#tabs1').tabs({
             //    border:false,
             //    onSelect: function (title) {
             //        if (title == "入境確認單列表")//選取
             //        {
             //            //panel寬度調整
             //            var dgid = $('#dataGridView');
             //            var queryPanel = getInfolightOption(dgid).queryDialog;
             //            $(queryPanel).dialog('close');
             //        }
             //    }
             //});

         });

         function queryGrid(dg) {
             var result = [];
             //聘工表號碼列表
             if ($(dg).attr('id') == 'dataGridView') {
                 var EmployerID = $('#EmployerID_Query').val();//雇主名稱
                 var WorkNo = $('#WorkNo_Query').val();//聘工表號碼
                 var CreateBy = $('#CreateBy_Query').val();//建立人員

                 if (EmployerID != '') result.push("r.EmployerName like '%" + EmployerID + "%'");
                 if (WorkNo != '') result.push("f.WorkNo like '%" + WorkNo + "%'");
                 if (CreateBy != '') result.push("f.CreateBy like '%" + CreateBy + "%'");
                 $(dg).datagrid('setWhere', result.join(' and '));
             }

             //入境確認單列表
             if ($(dg).attr('id') == 'dataGridView2') {
                 var EmployerID = $('#EmployerName_Query').val();//雇主名稱
                 var OrderNo = $('#OrderNo_Query').val();//訂單編號
                 var IndateNo = $('#IndateNo_Query').val();//入境單號碼
                 var CreateBy = $('#CreateBy_Query').val();//建立人員

                 if (EmployerID != '') result.push("r.EmployerName like '%" + EmployerID + "%'");
                 if (OrderNo != '') result.push("f.OrderNo like '%" + OrderNo + "%'");
                 if (IndateNo != '') result.push("f.IndateNo like '%" + IndateNo + "%'");
                 if (CreateBy != '') result.push("f.CreateBy like '%" + CreateBy + "%'");
                 $(dg).datagrid('setWhere', result.join(' and '));
             }
         }
         //聘工表列印
         function WorkNoLink(value, row, index) {
             return $('<a>', { href: 'javascript:void(0)', name: 'WorkNoLink', onclick: ' OpenWorkNo.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '列印' })[0].outerHTML
         }
         //呼叫聘工表Report視窗
         function OpenWorkNo() {
             var index = $(this).attr('rowIndex');
             $("#dataGridView").datagrid('selectRow', index);
             var rows = $("#dataGridView").datagrid('getSelected');
             var WorkNo = rows.WorkNo;
             var url = "../JB_ADMIN/REPORT/FWCRM/WorkNoReport.aspx?WorkNo=" + WorkNo;

             var height = $(window).height() - 50;
             var width = $(window).width() - 200;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 width: width,
                 title: "聘工表列印",
                 //maximizable: true                              
             });
             $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
             dialog.dialog('open');
         }
         
         //入境單列印
         function IndateNoLink(value, row, index) {
             return $('<a>', { href: 'javascript:void(0)', name: 'IndateNoLink', onclick: ' OpenIndateNo.call(this)', rowIndex: index }).linkbutton({ plain: false, text: '列印' })[0].outerHTML
         }
         //呼叫入境單Report視窗
         function OpenIndateNo() {
             var index = $(this).attr('rowIndex');
             $("#dataGridView2").datagrid('selectRow', index);
             var rows = $("#dataGridView2").datagrid('getSelected');
             var IndateNo = rows.IndateNo;
             var url = "../JB_ADMIN/REPORT/FWCRM/IndateNoReport.aspx?IndateNo=" + IndateNo;

             var height = $(window).height() - 50;
             var width = $(window).width() - 200;
             var dialog = $('<div/>')
             .dialog({
                 draggable: false,
                 modal: true,
                 height: height,
                 width: width,
                 title: "入境確認單列印",
                 //maximizable: true                              
             });
             $('<iframe style="border: 0px;" src="' + url + '" width="100%" height="100%"></iframe>').appendTo(dialog.find('.panel-body'));
             dialog.dialog('open');
         }
     </script>       
</head>
<body>
    <form id="form1" runat="server">
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
             <div class="easyui-tabs" style="width:780px;height100%" id="tabs1">
             <div title="聘工表號碼列表" style="padding:10px">
                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sFWCRMGetNo.FWCRMWorkNo" runat="server" AutoApply="True"
                    DataMember="FWCRMWorkNo" Pagination="True" QueryTitle="聘工表查詢" EditDialogID="JQDialog1"
                    Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="250px" QueryMode="Panel" QueryTop="150px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="聘工表列印" Editor="text" FieldName="WorkNoLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="75" FormatScript="WorkNoLink">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="聘工表號碼" Editor="text" FieldName="WorkNo" Format="" MaxLength="0" Visible="true" Width="100" FormatScript="" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Visible="true" Width="300" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="75" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM:SS" Visible="true" Width="125" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="insertItem" Text="新增聘工表單號" />                   
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                            OnClick="openQuery" Text="查詢" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱" Condition="=" DataType="string" Editor="text" EditorOptions="" FieldName="EmployerID" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="聘工表號碼 " Condition="%%" DataType="string" Editor="text" FieldName="WorkNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="建立人員 " Condition="%%" DataType="string" Editor="text" FieldName="CreateBy" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="新增聘工表編號" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="FWCRMWorkNo" HorizontalColumnsCount="2" RemoteName="sFWCRMGetNo.FWCRMWorkNo" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="WorkNo" Editor="text" FieldName="WorkNo" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="雇主名稱" Editor="infocombobox" FieldName="EmployerID" Format="" maxlength="0" Width="180" EditorOptions="valueField:'EmployerID',textField:'EmployerName',remoteName:'sFWCRMGetNo.infoEmployerID',tableName:'infoEmployerID',pageSize:'-1',checkData:true,selectOnly:false,cacheRelationText:false,panelHeight:200" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" maxlength="0" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="WorkNo" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="EmployerID" RemoteMethod="True" ValidateMessage="請選擇雇主" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            </div>

            <div title="入境確認單列表" style="padding:10px">
                <JQTools:JQDataGrid ID="dataGridView2" data-options="pagination:true,view:commandview" RemoteName="sFWCRMGetNo.FWCRMIndateNo" runat="server" AutoApply="True"
                    DataMember="FWCRMIndateNo" Pagination="True" QueryTitle="聘工表查詢" EditDialogID="JQDialog2"
                    Title="" AllowAdd="True" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="250px" QueryMode="Panel" QueryTop="150px" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="left" Caption="入境單列印" Editor="text" FieldName="IndateNoLink" FormatScript="IndateNoLink" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="入境單號碼" Editor="text" FieldName="IndateNo" Format="" MaxLength="0" Visible="true" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="訂單編號" Editor="text" FieldName="OrderNo" Format="" MaxLength="0" Visible="true" Width="100" EditorOptions="" />
                        <JQTools:JQGridColumn Alignment="left" Caption="雇主名稱" Editor="text" FieldName="EmployerName" Format="" MaxLength="0" Visible="true" Width="200" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Visible="true" Width="75" />
                        <JQTools:JQGridColumn Alignment="left" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="yyyy-mm-dd HH:MM:SS" Visible="true" Width="125" />
                    </Columns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                            OnClick="insertItem" Text="新增入境單號" />                   
                        <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                            OnClick="openQuery" Text="查詢" />
                    </TooItems>
                    <QueryColumns>
                        <JQTools:JQQueryColumn AndOr="and" Caption="雇主名稱" Condition="%" DataType="string" Editor="text" FieldName="EmployerName" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="訂單編號" Condition="=" DataType="string" Editor="text" EditorOptions="" FieldName="OrderNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="入境單號碼 " Condition="%%" DataType="string" Editor="text" FieldName="IndateNo" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                        <JQTools:JQQueryColumn AndOr="and" Caption="建立人員 " Condition="%%" DataType="string" Editor="text" FieldName="CreateBy" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="100" />
                    </QueryColumns>
                </JQTools:JQDataGrid>
                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormMaster2" Title="新增入境確認單編號" DialogLeft="10px" DialogTop="10px">
                <JQTools:JQDataForm ID="dataFormMaster2" runat="server" DataMember="FWCRMIndateNo" HorizontalColumnsCount="2" RemoteName="sFWCRMGetNo.FWCRMIndateNo" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="False" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="IndateNo" Editor="text" FieldName="IndateNo" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="訂單編號" Editor="inforefval" FieldName="OrderNo" Format="" maxlength="0" Width="280" EditorOptions="title:'訂單選擇',panelWidth:400,panelHeight:350,remoteName:'sFWCRMGetNo.infoOrderNo',tableName:'infoOrderNo',columns:[{field:'OrderNo',title:'訂單編號',width:120,align:'left',table:'',isNvarChar:false,queryCondition:''},{field:'EmployerName',title:'雇主名稱 ',width:220,align:'left',table:'',isNvarChar:false,queryCondition:''}],columnMatches:[],whereItems:[],valueField:'OrderNo',textField:'OrderNo',valueFieldCaption:'OrderNo',textFieldCaption:'OrderNo',cacheRelationText:false,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" NewRow="True" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateBy" Editor="text" FieldName="CreateBy" Format="" maxlength="0" Width="180" NewRow="False" ReadOnly="False" RowSpan="1" Span="1" Visible="False" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CreateDate" Editor="datebox" FieldName="CreateDate" Format="" Width="180" maxlength="0" Visible="False" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="自動編號" FieldName="IndateNo" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                    </Columns>
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster2" runat="server" BindingObjectID="dataFormMaster2" EnableTheming="True">
                    <Columns>
                        <JQTools:JQValidateColumn CheckNull="True" FieldName="OrderNo" RemoteMethod="True" ValidateMessage="請選擇訂單編號" ValidateType="None" />
                    </Columns>
                </JQTools:JQValidate>
            </JQTools:JQDialog>
            </div>
        </div>
    </form>
</body>
</html>
