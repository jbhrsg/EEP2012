<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBREC_EmpOverDutyQuery.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <script>
          $(document).ready(function () {
              $(function () {
                  $("input, select, textarea").focus(function () {
                      $(this).css("background-color", "yellow");
                  });
                  $("input, select, textarea").blur(function () {
                      $(this).css("background-color", "white");
                  });
              });
              $('#YearMonth_Query').combobox('clear');
          });
          function queryGrid(dg) {
              var result = [];
              var aVal = '';
              var aVal = $('#YearMonth_Query').combobox('getValue');
              if (aVal != '')
                  result.push("YearMonth = '" + aVal + "'");
              var aVal = $('#DutyQty_Query').val();
              if (aVal != '')
                  result.push("DutyQty > '" + aVal + "'");
              $(dg).datagrid('setWhere', result.join(' and '));
          }
      </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sJBRecruitOverDuty.JBRecruitOverDuty" runat="server" AutoApply="True"
                DataMember="JBRecruitOverDuty" Pagination="True" QueryTitle="派遣加班時數查詢" EditDialogID="JQDialog1"
                Title="" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="20,40,60,120" PageSize="20" QueryAutoColumn="False" QueryLeft="" QueryMode="Panel" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶代號" Editor="text" FieldName="CUSTOMERID" Format="" MaxLength="0" Width="80" Sortable="True" />
                    <JQTools:JQGridColumn Alignment="left" Caption="客戶名稱" Editor="text" FieldName="CustomerShortName" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="業務區域" Editor="text" EditorOptions="" FieldName="SalesDepartment" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="True" Visible="True" Width="120">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="員工代號" Editor="text" FieldName="EMPID" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="姓名" Editor="text" FieldName="Namec" Format="" MaxLength="0" Width="80" />
                    <JQTools:JQGridColumn Alignment="left" Caption="身分證字號" Editor="text" FieldName="IDNumber" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="80">
                    </JQTools:JQGridColumn>
                    <JQTools:JQGridColumn Alignment="left" Caption="YearMonth" Editor="text" FieldName="YearMonth" Format="" MaxLength="0" Width="120" Visible="False" />
                    <JQTools:JQGridColumn Alignment="right" Caption="加班總時數" Editor="numberbox" FieldName="DutyQty" Format="N1" Width="80" />
                </Columns>
                <TooItems>
                  <JQTools:JQToolItem Icon="icon-excel" ItemType="easyui-linkbutton" OnClick="exportGrid" Text="匯出Excel" Visible="True" />
                  <%--  <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton"
                        OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply"
                        Text="存檔" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel"
                        Text="取消"  />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton"
                        OnClick="openQuery" Text="查詢" />--%>
                </TooItems>
                <QueryColumns>
                    <JQTools:JQQueryColumn AndOr="and" Caption="發薪年月" Condition="=" DataType="string" Editor="infocombobox" EditorOptions="valueField:'YearMonth',textField:'YearMonth',remoteName:'sJBRecruitOverDuty.YearMonth',tableName:'YearMonth',pageSize:'-1',checkData:false,selectOnly:true,cacheRelationText:false,panelHeight:200" FieldName="YearMonth" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" />
                    <JQTools:JQQueryColumn AndOr="and" Caption="加班總時數大於" Condition="=" DataType="number" Editor="text" FieldName="DutyQty" IsNvarChar="False" NewLine="False" RemoteMethod="False" RowSpan="0" Span="0" Width="125" DefaultValue="46.0" />
                </QueryColumns>
            </JQTools:JQDataGrid>

            <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="派遣加班時數查詢">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="JBRecruitOverDuty" HorizontalColumnsCount="2" RemoteName="sJBRecruitOverDuty.JBRecruitOverDuty" >
                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="CUSTOMERID" Editor="text" FieldName="CUSTOMERID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CustomerShortName" Editor="text" FieldName="CustomerShortName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="EMPID" Editor="text" FieldName="EMPID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="Namec" Editor="text" FieldName="Namec" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="YearMonth" Editor="text" FieldName="YearMonth" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="DutyQty" Editor="numberbox" FieldName="DutyQty" Format="" Width="180" />
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
