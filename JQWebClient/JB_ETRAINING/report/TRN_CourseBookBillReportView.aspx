<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TRN_CourseBookBillReportView.aspx.cs" Inherits="HtmlPages_eva_customers" %>

<%@ Register Assembly="EFClientTools" Namespace="EFClientTools" TagPrefix="cc1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>簽到簿</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager" runat="server" />
            <cc1:WebDataSource ID="DataSource" runat="server" RemoteName="" PreviewSolution="SOLUTION1"
                PreviewDatabase="SDERPS" PacketRecords="-1" />

            <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="條件："></asp:Label>
            <asp:RadioButtonList ID="rbGroup" runat="server" Font-Size="Small" RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack="True" OnSelectedIndexChanged="Button1_Click">
                <asp:ListItem Selected="True" Value="0">合併列印</asp:ListItem>
                <asp:ListItem Value="1">依部門群組列印</asp:ListItem>
                <asp:ListItem Value="2">空白簽到表</asp:ListItem>
            </asp:RadioButtonList>

            &nbsp;<rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="808px" Height="480px"
                OnLoad="ReportViewer_Load" Font-Names="Verdana" Font-Size="8pt"
                InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana"
                WaitMessageFont-Size="14pt">
                <LocalReport EnableExternalImages="True" OnSubreportProcessing="SubreportProcessing" ReportPath="">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="DataSource" Name="TestDataSet" />
                    </DataSources> 
                </LocalReport>
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
