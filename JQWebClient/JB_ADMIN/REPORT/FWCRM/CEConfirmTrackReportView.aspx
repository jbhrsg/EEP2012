<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CEConfirmTrackReportView.aspx.cs" Inherits="MyPage_Customers" %>

<%@ Register Assembly="EFClientTools" Namespace="EFClientTools" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server" />
            <cc1:WebDataSource ID="DataSource" runat="server" RemoteName="" PreviewSolution="SOLUTION1"
                PreviewDatabase="SDERPS" PacketRecords="-1" /><%--此為EEP元件--%>
            
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="800px"
                 Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt"><%--此為.NET 元件--%>
                <LocalReport EnableExternalImages="True" OnSubreportProcessing="SubreportProcessing" ReportPath="">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DataSource" Name="TestDataSet" />
                    </DataSources> 
                </LocalReport>
            </rsweb:ReportViewer>
            <%--OnLoad="ReportViewer_Load"--%>
        </div>
    </form>
</body>
</html>