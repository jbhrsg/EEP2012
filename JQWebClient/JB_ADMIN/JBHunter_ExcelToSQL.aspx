<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JBHunter_ExcelToSQL.aspx.cs" Inherits="exceltosql" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>jQuery UI Button - Checkboxes</title>
  <link rel="stylesheet" href="/../code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">  
 
  <script src="../js/jquery.hoverpulse.js"></script>
   
    <script type="text/javascript">
        $('.datagrid-view1').css
        {
            visible: false;
        }
        function rowformater(value) { 
            if(value==""||value==null){
            return "未知";
        }else{
                return  "<a  href='" + value + "'>個人履歷</a>";
        }
        }
        function works(value) {
            if (value == " " || value == null || value == "") {
                return "無";
            } else {
                return "<a  href='" + value + "'>個人作品</a>";
            }
        }
        $(document).ready(function () {
          
           

        });
        function img(value) {
            if (value == " " || value == null || value == "" ) {
                return "無";
            } else {
                return "<input type='image' onclick='return false;' class='info-image' height='100px' src='" + value + "' onmouseover='infogridimageformatterset(this,'" + value + "');' onmouseout='infoimageonmouseover();'> ";
            }
          
        }
      
          
 </script>
</head>
   
<body>
  
    <form id="form1" runat="server">

    <ul>個人履歷上傳<asp:FileUpload ID="FileUpload1" runat="server" />
    個人照片上傳<asp:FileUpload ID="FileUpload2" runat="server" /></ul><p/>
   <h5 style="border: thin dashed #808080; margin: 0px; padding: 0px; font-family: 細明體; font-size: 16px; font-style:normal; font-variant: normal; text-transform: none; ">  
    作品集
    <ul>個人作品上傳<asp:FileUpload ID="FileUpload3" runat="server" /></ul>
    <ul>個人作品上傳<asp:FileUpload ID="FileUpload4" runat="server" /></ul>
    <ul>個人作品上傳<asp:FileUpload ID="FileUpload5" runat="server" /></ul></h5>

       
        
       
    <p style="text-align: right; width: 743px;">
      
        <input id="Button2" type="button" value="button" /><asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="上傳"  />
    </p>
    

        <div>
            <JQTools:JQScriptManager ID="JQScriptManager1" runat="server" />
            <JQTools:JQDataGrid ID="dataGridMaster" data-options="pagination:true,view:commandview" RemoteName="sUser.HUT_User" runat="server" AutoApply="True"
                DataMember="HUT_User" Pagination="False" QueryTitle="Query"
                Title="Test" ColumnsHibeable="False" DeleteCommandVisible="False" MultiSelect="False" AllowAdd="True" AllowDelete="True" AllowUpdate="True" AlwaysClose="False" CheckOnSelect="False" DuplicateCheck="False" EditMode="Dialog" EditOnEnter="False" InsertCommandVisible="True" PageList="10,20,30,40,50" PageSize="10" QueryAutoColumn="False" QueryLeft="" QueryMode="Window" QueryTop="" RecordLock="False" TotalCaption="Total:" UpdateCommandVisible="False" ViewCommandVisible="False">
                <Columns>
                    <JQTools:JQGridColumn Alignment="left" Caption="人才編號" Editor="text" FieldName="UserID" MaxLength="20" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="中文姓名" Editor="text" FieldName="NameC" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="英文姓名" Editor="text" FieldName="NameE" MaxLength="50" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="出生日期" Editor="datebox" FieldName="Birthday" Width="120" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DocFile1" Editor="text" FieldName="DocFile1" MaxLength="100" Width="120" FormatScript="works" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DocFile2" Editor="text" FieldName="DocFile2" MaxLength="100" Width="120" FormatScript="works" />
                    <JQTools:JQGridColumn Alignment="left" Caption="DocFile3" Editor="text" FieldName="DocFile3" MaxLength="100" Width="120" FormatScript="works" />
                    <JQTools:JQGridColumn Alignment="left" Caption="PhotoFile" Editor="text" FieldName="PhotoFile" MaxLength="100" Width="120" Format="image,Folder:JB_Hunter,height=100" FormatScript="img" />
                    <JQTools:JQGridColumn Alignment="left" Caption="ExcelFile" Editor="text" FieldName="ExcelFile" MaxLength="100" Width="120" Format="下載履歷"   FormatScript="rowformater" />
                    <JQTools:JQGridColumn Alignment="left" Caption="LastUpdateDate" Editor="datebox" FieldName="LastUpdateDate" Width="120" />
                </Columns>
                <TooItems>
                    <JQTools:JQToolItem Icon="icon-add" ItemType="easyui-linkbutton" OnClick="insertItem" Text="新增" />
                    <JQTools:JQToolItem Icon="icon-edit" ItemType="easyui-linkbutton" OnClick="updateItem" Text="更改" />
                    <JQTools:JQToolItem Icon="icon-remove" ItemType="easyui-linkbutton" OnClick="deleteItem" Text="刪除" />
                    <JQTools:JQToolItem Icon="icon-save" ItemType="easyui-linkbutton" OnClick="apply" Text="確定" />
                    <JQTools:JQToolItem Icon="icon-undo" ItemType="easyui-linkbutton" OnClick="cancel" Text="取消" />
                    <JQTools:JQToolItem Icon="icon-search" ItemType="easyui-linkbutton" OnClick="openQuery" Text="查詢" />
                </TooItems>
                <QueryColumns>
                </QueryColumns>
            </JQTools:JQDataGrid>
        </div>
    <JQTools:JQDefault runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="defaultMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQDefault>
<JQTools:JQValidate runat="server" BindingObjectID="dataGridMaster" BorderStyle="NotSet" Enabled="True" EnableTheming="True" ClientIDMode="Inherit" ID="validateMaster" EnableViewState="True" ViewStateMode="Inherit" >
</JQTools:JQValidate>
        
   <div>
        <input id="Hidden1" name="Hidden1" value="abcd" type="hidden" />
   </div>
       
        
 <script type="text/javascript">
     var b = getClientInfo('_username');
     $('#Hidden1').val(b);
    
        
  
 </script>
       
       <div>
        <%--<JQTools:JQImageContainer ID="JQImageContainer1" runat="server" Height="300px" OnDataBinding="JQImageContainer1_DataBinding"></JQTools:JQImageContainer>--%>
    </div>     
       
</form>
</body>
    
</html>
