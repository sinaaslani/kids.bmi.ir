<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="KidsUserAdminPanel.ascx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.KidsUserAdminPanel"  %>
<table width="100%">
    <tr>
        <th align="right" height="25" class="TableHeaderText" style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);
            font-family: Tahoma,Verdana,Arial,Helvetica" width="100%">
           
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;مدیریت کاربران عضو
        </th>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="lnkManageUsers" runat="server"  
                NavigateUrl="~/AdminCP/KidsUserAdmin/KidsUser_Admin.aspx" Visible="False">مدیریت کاربران عضو</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="lnkManageGeustUsers" runat="server"  
                NavigateUrl="~/AdminCP/KidsUserAdmin/GeustUser_Admin.aspx" Visible="False">مدیریت کاربران مهمان</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="lnkPrintAccForm" runat="server"  
                NavigateUrl="~/AdminCP/KidsUserAdmin/KidsUser_Admin_Branch.aspx"> فرم افتتاح حساب</asp:HyperLink>
        </td>
    </tr>
</table>
