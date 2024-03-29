﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBERP_DelayLunchSet.aspx.cs" Inherits="Template_JQuerySingle1" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
     <script>
           
         ////檢查日期是否重複
         //function validateDateData(Date) {            
         //    var cnt;
         //    var icount;
         //    $.ajax({
         //        type: "POST",
         //        url: '../handler/jqDataHandle.ashx?RemoteName=sERPDelayLunchSet.ERPDelayLunchDate', //連接的Server端，command
         //        data: "mode=method&method=" + "checkDateData" + "&parameters=" + Date, //method後的參數為server的Method名稱  parameters後為端的到後端的參數這裡傳入選中資料的CustomerID欄位
         //        cache: false,
         //        async: false,
         //        success: function (data) {
         //            if (data != false) {
         //                cnt = $.parseJSON(data);
         //            }
         //        }
         //    });
         //    alert(getEditMode($("#dataGridMaster")));
         //    if (getEditMode($("#dataGridMaster")) == 'updated') {
         //        icount = "1";
         //    } else icount = "0";
         //    if (cnt != icount && cnt != "undefined") return false;
         //    else
         //        return true;
             
         //}
        
      </script> 
</head>
<body>
 <form id="form1" runat="server">
    <div id="cc" class="easyui-layout" style="width:970px;height:450px;">
       
         <div data-options="region:'west',title:'設定自動產生名單',split:true" style="width:560px;">  
            <div>
                        <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
                        <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchSet.ERPDelayLunchEmpAuto" runat="server" AutoApply="True"
                            DataMember="ERPDelayLunchEmpAuto" Pagination="False" QueryTitle="Query"
                            Title="" AllowDelete="True" ViewCommandVisible="False" AllowAdd="True" AllowUpdate="True" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="False" Width="552px" Height="420px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="inforefval" FieldName="EmpID" Format="" Width="140" EditorOptions="title:'請選擇員工',panelWidth:340,remoteName:'sERPDelayLunchSet.infoEmpID',tableName:'infoEmpID',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工工號',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                                <JQTools:JQGridColumn Alignment="center" Caption="生效日期" Editor="datebox" FieldName="SDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="失效日期" Editor="datebox" FieldName="EDate" Frozen="False" IsNvarChar="False" MaxLength="0" QueryCondition="" ReadOnly="False" Sortable="False" Visible="True" Width="90">
                                </JQTools:JQGridColumn>
                                <JQTools:JQGridColumn Alignment="center" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="90" ReadOnly="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="90" ReadOnly="True" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                                <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                            </TooItems>
                            <QueryColumns>
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </div>
            <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="defaultMaster" >
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="SDate" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="9999/01/01" FieldName="EDate" RemoteMethod="True" />
                    </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" EnableTheming="True" ID="validateMaster" >
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpID" RemoteMethod="True" ValidateType="None" CheckMethod="" ValidateMessage="請選擇人員" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="SDate" RemoteMethod="True" ValidateMessage="請輸入生效日期" ValidateType="None" />
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EDate" RemoteMethod="True" ValidateMessage="請輸入失效日期" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>

        </div>
        
        <div data-options="region:'center',title:'設定整月不訂人員'" >    
            <div>
                        <JQTools:JQDataGrid ID="dgERPDelayLunchEmp" data-options="pagination:true,view:commandview" RemoteName="sERPDelayLunchSet.ERPDelayLunchEmp" runat="server" AutoApply="True"
                            DataMember="ERPDelayLunchEmp" Pagination="False" QueryTitle="Query"
                            Title="" AllowDelete="True" ViewCommandVisible="False" AllowAdd="True" AllowUpdate="False" AlwaysClose="False" BufferView="False" CheckOnSelect="True" ColumnsHibeable="False" DeleteCommandVisible="False" DuplicateCheck="True" EditMode="Dialog" EditOnEnter="True" InsertCommandVisible="True" MultiSelect="False" NotInitGrid="False" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" RecordLockMode="None" RowNumbers="True" TotalCaption="Total:" UpdateCommandVisible="True" Width="405px" Height="420px">
                            <Columns>
                                <JQTools:JQGridColumn Alignment="center" Caption="員工姓名" Editor="inforefval" FieldName="EmpID" Format="" Width="171" EditorOptions="title:'請選擇員工',panelWidth:340,remoteName:'sERPDelayLunchSet.infoEmpID',tableName:'infoEmpID',columns:[],columnMatches:[],whereItems:[],valueField:'EMPLOYEE_CODE',textField:'NAME_C',valueFieldCaption:'員工工號',textFieldCaption:'員工姓名',cacheRelationText:true,checkData:true,showValueAndText:false,dialogCenter:false,selectOnly:true,capsLock:'none',fixTextbox:'false'" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立人員" Editor="text" FieldName="CreateBy" Format="" MaxLength="0" Width="100" ReadOnly="True" />
                                <JQTools:JQGridColumn Alignment="center" Caption="建立日期" Editor="datebox" FieldName="CreateDate" Format="" Width="100" ReadOnly="True" />
                            </Columns>
                            <TooItems>
                                <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                                <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                                <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                                <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                                <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                            </TooItems>
                            <QueryColumns>
                            </QueryColumns>
                        </JQTools:JQDataGrid>
                    </div>
            <JQTools:JQDefault runat="server" BindingObjectID="dgERPDelayLunchEmp" EnableTheming="True" ID="JQDefault1" >
                    <Columns>
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_username" FieldName="CreateBy" RemoteMethod="True" />
                        <JQTools:JQDefaultColumn CarryOn="False" DefaultValue="_today" FieldName="CreateDate" RemoteMethod="True" />
                    </Columns>
            </JQTools:JQDefault>
            <JQTools:JQValidate runat="server" BindingObjectID="dgERPDelayLunchEmp" EnableTheming="True" ID="JQValidate1" >
                <Columns>
                    <JQTools:JQValidateColumn CheckNull="True" FieldName="EmpID" RemoteMethod="True" ValidateType="None" />
                </Columns>
            </JQTools:JQValidate>

        </div>       
    </div>

</form>
</body>
</html>
