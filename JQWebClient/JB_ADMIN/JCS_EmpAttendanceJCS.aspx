<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JCS_EmpAttendanceJCS.aspx.cs" Inherits="Template_JQuerySingle1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <script src="../js/jquery.jbjob.js"></script>
      <script type="text/javascript">
          $(document).ready(function () {
              $("#CalDateS_Query").datebox({
                  onSelect: function () {
                  GetEmpListPeriod();
                  }
              });
              $("#CalDateE_Query").datebox({
                   onSelect: function () {
                   GetEmpListPeriod();
                  }
              });
              var Dt = new Date();
              var D1 = Dt.getFullYear() + "/" + (Dt.getMonth() + 1) + "/" + Dt.getDate();
              //var D2 = Dt.getFullYear() + "/" + right('00' + (Dt.getMonth() + 1), 2) + "/" + right('00' + Dt.getDate(), 2);
              $('#CalDateS_Query').datebox('setValue',D1)
              $('#CalDateE_Query').datebox('setValue',D1)
          });
          function queryGrid(dg) {
              var UserID = getClientInfo("UserID");
              if ($(dg).attr('id') == 'dataGridView') {
                  var result = [];
                  var aVal = '';
                  aVal = $('#CalDateS_Query').datebox('getValue');
                  if (aVal != '')
                      result.push("CalDate >= '" + aVal + "'");
                  aVal = $('#CalDateE_Query').datebox('getValue');
                  if (aVal != '')
                      result.push("CalDate <= '" + aVal + "'");
                  aVal = $('#EMPID_Query').combobox('getValue');
                  if (aVal != '')
                      result.push("A.EmpID = '" + aVal + "'");

                  var filtstr = result.join(' and ');
                  $(dg).datagrid('setWhere', filtstr);
              }
          }
          function dataGridViewOnLoadSucess() {
          }
          function GetEmpListPeriod() {
                var UserID = getClientInfo("UserID");
                var DateS = $('#CalDateS_Query').datebox('getValue');
                var DateE = $('#CalDateE_Query').datebox('getValue');
                $.ajax({
                      type: "POST",
                      url: '../handler/jqDataHandle.ashx?RemoteName=sJCSEmpAttendance.EmpAttendance',
                      data: "mode=method&method=" + "GetEmpListPeriod" + "&parameters=" + DateS + "," + DateE,
                      cache: false,
                      async: false,
                      success: function (data) {
                          var rows = $.parseJSON(data);
                          $('#EMPID_Query').combobox("loadData", rows);
                          //if (rows.length > 0) {
                          //    $('#EMPID_Query').combobox("loadData", rows);
                          //   }
                          //else {
                          //     $("#EMPID_Query").combobox('setWhere', "1=2");
                          //}
                      }
                  }
               );
              }
      
  </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJCSEmpAttendance.EmpAttendance" runat="server" AutoApply="True"
                DataMember="EmpAttendance" Pagination="True" QueryTitle="" EditDialogID="JQDialog1"
                Title="傑誠員工出勤查詢" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,60,60,120,240" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="True" OnLoadSuccess="dataGridViewOnLoadSucess">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="代號" Editor="text" FieldName="EMPID" Format="" MaxLength="0" Width="50" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="NameC" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="日期" Editor="datebox" FieldName="CalDate" Format="" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡1" Editor="text" FieldName="Card1" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡2" Editor="text" FieldName="Card2" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡3" Editor="text" FieldName="Card3" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡4" Editor="text" FieldName="Card4" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡5" Editor="text" FieldName="Card5" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡6" Editor="text" FieldName="Card6" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡7" Editor="text" FieldName="Card7" Format="" MaxLength="0" Width="60" />
                    <JQTools:JQGridColumn Alignment="center" Caption="刷卡8" Editor="text" FieldName="card8" Format="" MaxLength="0" Width="60" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消" Visible="False"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" Visible="False" />
                    <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="起始日期" Condition="&gt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="CalDateS" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="True" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="終止日期" Condition="&lt;=" DataType="datetime" DefaultValue="_today" Editor="datebox" FieldName="CalDateE" Format="yyyy/MM/dd" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="員工" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'EMPID',textField:'NAMEC',remoteName:'sJCSEmpAttendance.EmpListPeriod',tableName:'EmpListPeriod',pageSize:'-1',checkData:false,selectOnly:false,cacheRelationText:false,panelHeight:200" FieldName="EMPID" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="傑誠員工出勤查詢">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="EmpAttendance" HorizontalColumnsCount="2" RemoteName="sJCSEmpAttendance.EmpAttendance" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="EMPID" Editor="text" FieldName="EMPID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="NameC" Editor="text" FieldName="NameC" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CalDate" Editor="datebox" FieldName="CalDate" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card1" Editor="text" FieldName="Card1" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card2" Editor="text" FieldName="Card2" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card3" Editor="text" FieldName="Card3" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card4" Editor="text" FieldName="Card4" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card5" Editor="text" FieldName="Card5" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card6" Editor="text" FieldName="Card6" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Card7" Editor="text" FieldName="Card7" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="card8" Editor="text" FieldName="card8" Format="" maxlength="0" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>
                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
        </div>
    </form>
</body>
</html>
