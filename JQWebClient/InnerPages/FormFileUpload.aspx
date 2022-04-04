<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormFileUpload.aspx.cs" Inherits="InnerPage_FormFileUpload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="formFileUpload" runat="server">
        <div>
            <JQTools:JQFileUpload ID="JQFileUpload1" runat="server" Width="400px" Filter="" UpLoadFolder="WorkflowFiles" ShowButton="false" ShowLocalFile="True" IsAutoNum="True" FileSizeLimited="860" />
            <br />
            <JQTools:JQFileUpload ID="JQFileUpload2" runat="server" Width="400px" Filter="" UpLoadFolder="WorkflowFiles" ShowButton="false" ShowLocalFile="True" IsAutoNum="True" FileSizeLimited="860" />
            <br />
            <JQTools:JQFileUpload ID="JQFileUpload3" runat="server" Width="400px" Filter="" UpLoadFolder="WorkflowFiles" ShowButton="false" ShowLocalFile="True" IsAutoNum="True" FileSizeLimited="860" />
            <br />
            <JQTools:JQFileUpload ID="JQFileUpload4" runat="server" Width="400px" Filter="" UpLoadFolder="WorkflowFiles" ShowButton="false" ShowLocalFile="True" IsAutoNum="True" FileSizeLimited="860" />
            <br />
            <JQTools:JQFileUpload ID="JQFileUpload5" runat="server" Width="400px" Filter="" UpLoadFolder="WorkflowFiles" ShowButton="false" ShowLocalFile="True" IsAutoNum="True" FileSizeLimited="860" />
            <ul id="ulFiles_FormFileUpload"></ul>
        </div>
        <div>
            <a href="#" class="easyui-linkbutton" iconcls="icon-upload" id="btnUpload_FormFileUpload">Upload</a>
            <a id="btnCancel_FileUpload" href="#" class="easyui-linkbutton">Close</a>
        </div>
    </form>
</body>
</html>
