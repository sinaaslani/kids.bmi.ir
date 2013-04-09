<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ConfigAdminPanel.ascx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.ConfigAdminPanel" %>
<table width="100%">
    <tr>
        <th align="right" height="25" class="TableHeaderText" style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);
            font-family: Tahoma,Verdana,Arial,Helvetica" width="100%">
            <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مديريت تنظیمات
        </th>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="addPollLnk" runat="server"  NavigateUrl="~/AdminCP/_Config/ConfigsAdmin.aspx">مدیریت تنظیمات سایت</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="Hyperlink1" runat="server"  NavigateUrl="~/AdminCP/Logging/ViewLogs.aspx">مشاهده خطاها</asp:HyperLink>
        </td>
    </tr>
</table>
