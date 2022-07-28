<%@ Page Language="C#" AutoEventWireup="true" CodeFile="glCostCenterUser.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
      <script> 

          //=========================================控制Grid1=0都做完才顯示Grid=============================================================
          var waitA = false;
          var waitB = false;

          var myVar = setInterval(function () { myTimer(); }, 100);
          setTimeout(function () { clearTimeout(myVar); }, 6000);//最多6秒結束

          function myTimer() {
              if (waitA == true && waitB == true) {
                  RefreshDetailGrid();
                  clearTimeout(myVar);
              }
          }
          //========================================= ready ====================================================================================
          $(document).ready(function () {
             
          });

          //Grid genCheckBox
          function GridCheckBox(val) {
              if (val != "0")
                  return "<input  type='checkbox' checked='true' onclick='return false;'/>";
              else
                  return "<input  type='checkbox' onclick='return false;'/>";
          }
       
          //============================================================主檔====================================================================        
          function OnLoadMaster() {
              //取消或阻擋抓取GridView第一筆的資料
              $("#dataGridView").datagrid("unselectRow", 0);
              if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                  //1=0做完
                  waitA = true;
              }
          }

          //依據選取的Master更新Detail明細
          function OnSelectMaster(rowindex, rowdata) {
              RefreshDetail(rowdata);                      
          }
        
        
          //============================================================明細====================================================================
          //明細OnLoad
          function OnLoadDetail() {
              if (!$(this).data('firstLoad') && $(this).data('firstLoad', true)) {
                  //1=0做完
                  waitB = true;
              }
          }
          //更新Detail
          function RefreshDetailGrid() {

              $('#dataGridDetail').datagrid('loadData', []); //明細清空資料            
              var result = [];

              //成本中心
              var CostCenterID = $("#dataGridView").datagrid('getSelected').CostCenterID;;//取得當前主檔中選中的那個Data
              if (CostCenterID != '') result.push("g.CostCenterID='" + CostCenterID+"'");

              $("#dataGridDetail").datagrid('setWhere', result.join(' and '));


          }
          //選主檔=>更新明細
          function RefreshDetail(row) {
              if (row != null) {
                  //var where = $(dg).datagrid('getWhere');                              
                  var result = [];

                  //成本中心
                  var CostCenterID = row.CostCenterID;
                  if (CostCenterID != '') result.push("g.CostCenterID='" + CostCenterID+"'");

                  $("#dataGridDetail").datagrid('setWhere', result.join(' and '));
              }
              else {
                  $('#dataGridDetail').datagrid('loadData', []); //明細清空資料                
              }
          }
          //新增人員=>取得 人員代號
          function GetCostCenterID() {
              var row = $('#dataGridView').datagrid('getSelected');
              return row.CostCenterID;//類別
          }

          function queryGrid(dg) {//查詢後添加固定條件
              if ($(dg).attr('id') == 'dataGridDetail') {//GridDetail
                  //查詢條件
                  var result = [];

                  //var row = $('#dataGridView').datagrid('getSelected');
                  //var ClassID = row.ClassID;//類別

                  //var CompanyID = $("#cbCompanyID").combobox('getValue');//公司別                
                  //var Acno = $("#Acno_Query").val();//科目
                  //var AcnoName = $("#AcnoName_Query").val();//科目名稱

                  //if (ClassID != '') result.push("a.ClassID = '" + ClassID + "'");
                  //if (CompanyID != '') result.push("a.CompanyID = " + CompanyID);
                  //if (Acno != '') result.push("a.Acno+Isnull(a.SubAcno,'') like '%" + Acno + "%'");
                  //if (AcnoName != '') result.push("a.AcnoName like '%" + AcnoName + "%'");

                  $(dg).datagrid('setWhere', result.join(' and '));
              }
          }
          function OnAppliedDF() {
              $("#dataGridDetail").datagrid('reload');

          }



  </script> 
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <div class="easyui-layout" style="height: 522px;">
                <div data-options="region:'west',split:true" style="width: 360px;" >
                <JQTools:JQDataGrid ID="dataGridView" data-options="pagination:true,view:commandview" RemoteName="sglCompany.glCostCenter" runat="server" AutoApply="True"
                    DataMember="glCostCenter" Pagination="False" QueryTitle="Query" EditDialogID="JQDialog1"
                    Title="總帳成本中心" AllowAdd="False" AllowDelete="False" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="False" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False" OnSelect="OnSelectMaster" OnLoadSuccess="OnLoadMaster">
                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="流水號" Editor="numberbox" FieldName="AutoKey" Format="" Visible="true" Width="80" />
                        <JQTools:JQGridColumn Alignment="center" Caption="成本代號" Editor="text" FieldName="CostCenterID" Format="" MaxLength="0" Visible="true" Width="90" />
                        <JQTools:JQGridColumn Alignment="left" Caption="成本名稱" Editor="text" FieldName="CostCenterName" Format="" MaxLength="0" Visible="true" Width="120" />
                        <JQTools:JQGridColumn Alignment="left" Caption="AuthorUsers" Editor="text" FieldName="AuthorUsers" Format="" MaxLength="0" Visible="False" Width="120" />
                        <JQTools:JQGridColumn Alignment="right" Caption="SortID" Editor="numberbox" FieldName="SortID" Format="" Visible="False" Width="120" />
                    </Columns>
                    <TooItems>
                        
                    </TooItems>
                    <QueryColumns>
                    </QueryColumns>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog1" runat="server" BindingObjectID="dataFormMaster" Title="glCostCenterUser">
                <JQTools:JQDataForm ID="dataFormMaster" runat="server" DataMember="glCostCenter" HorizontalColumnsCount="2" RemoteName="sglCompany.glCostCenter" >

                    <Columns>
                        <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="CostCenterName" Editor="text" FieldName="CostCenterName" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="AuthorUsers" Editor="text" FieldName="AuthorUsers" Format="" maxlength="0" Width="180" />
                        <JQTools:JQFormColumn Alignment="left" Caption="SortID" Editor="numberbox" FieldName="SortID" Format="" Width="180" />
                    </Columns>
                </JQTools:JQDataForm>

                <JQTools:JQDefault ID="defaultMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQDefault>
                <JQTools:JQValidate ID="validateMaster" runat="server" BindingObjectID="dataFormMaster" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                </JQTools:JQValidate>
            </JQTools:JQDialog>
             </div>
             <div data-options="region:'center'">
                <JQTools:JQDataGrid ID="dataGridDetail" runat="server" AutoApply="True" DataMember="glCostCenterUser" EditDialogID="JQDialog2" Pagination="False" ParentObjectID="" RemoteName="sglCompany.glCostCenterUser" Title="成本中心權限" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="True" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="True" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTitle="Query" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" ViewCommandVisible="False" >

                    <Columns>
                        <JQTools:JQGridColumn Alignment="center" Caption="流水號" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" Width="120" Visible="False" />
                        <JQTools:JQGridColumn Alignment="left" Caption="使用者代號" Editor="text" FieldName="UserID" Format="" Width="100" />
                        <JQTools:JQGridColumn Alignment="left" Caption="使用者名稱" Editor="text" FieldName="USERNAME" Width="120" Visible="True" />
                        <JQTools:JQGridColumn Alignment="left" Caption="修改者" Editor="text" FieldName="LastUpdateBy" Format="" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="100">
                        </JQTools:JQGridColumn>
                        <JQTools:JQGridColumn Alignment="left" Caption="修改日期" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="100" />
                    </Columns>
                    <RelationColumns>
                        <JQTools:JQRelationColumn FieldName="CostCenterID" ParentFieldName="CostCenterID" />
                    </RelationColumns>
                    <TooItems>
                        <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增人員" />
<%--                        <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                        <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />--%>
                    </TooItems>
                </JQTools:JQDataGrid>

                <JQTools:JQDialog ID="JQDialog2" runat="server" BindingObjectID="dataFormDetail" DialogLeft="400px" Title="設定權限" Width="400px">
                    <JQTools:JQDataForm ID="dataFormDetail" runat="server" ParentObjectID="" DataMember="glCostCenterUser" HorizontalColumnsCount="2" RemoteName="sglCompany.glCostCenterUser" AlwaysReadOnly="False" Closed="False" ContinueAdd="False" disapply="False" DivFramed="False" DuplicateCheck="True" HorizontalGap="0" IsAutoPageClose="False" IsAutoPause="False" IsAutoSubmit="False" IsNotifyOFF="False" IsRejectNotify="False" IsRejectON="False" IsShowFlowIcon="False" ShowApplyButton="False" ValidateStyle="Dialog" VerticalGap="0" OnApplied="OnAppliedDF" >
                        <Columns>
                            <JQTools:JQFormColumn Alignment="left" Caption="AutoKey" Editor="numberbox" FieldName="AutoKey" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="CostCenterID" Editor="text" FieldName="CostCenterID" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="選擇人員" Editor="inforefval" FieldName="UserID" Format="" Width="120" EditorOptions="title:'請選擇',panelWidth:350,remoteName:'sglCompany.infoUSERS',tableName:'infoUSERS',columns:[],columnMatches:[],whereItems:[],valueField:'USERID',textField:'USERNAME',valueFieldCaption:'工號',textFieldCaption:'姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:false,capsLock:'none',fixTextbox:'false'" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateBy" Editor="text" FieldName="LastUpdateBy" Format="" Width="120" Visible="False" />
                            <JQTools:JQFormColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Format="" Width="120" Visible="False" />
                        </Columns>
                        <RelationColumns>
                            <JQTools:JQRelationColumn FieldName="CostCenterID" ParentFieldName="CostCenterID" />
                        </RelationColumns>
                    </JQTools:JQDataForm>
                    <JQTools:JQDefault ID="defaultDetail" runat="server" BindingObjectID="dataFormDetail" EnableTheming="True">
                        <Columns>
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="0" FieldName="AutoKey" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_USERCODE" FieldName="LastUpdateBy" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="LastUpdateDate" RemoteMethod="True" />
                            <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="" FieldName="CostCenterID" RemoteMethod="False" DefaultMethod="GetCostCenterID" />
                        </Columns>
                    </JQTools:JQDefault>
                    <JQTools:JQValidate ID="validateDetail" runat="server" BindingObjectID="dataFormDetail" BorderStyle="NotSet" ClientIDMode="Inherit" Enabled="True" EnableTheming="True" EnableViewState="True" ViewStateMode="Inherit">
                        <Columns>
                            <JQTools:JQValidateColumn CheckNull="True" FieldName="UserID" RemoteMethod="True" ValidateMessage="請選擇人員！" ValidateType="None" />
                        </Columns>
                    </JQTools:JQValidate>
                </JQTools:JQDialog>
            </div>
        </div>
    </form>
</body>
</html>
