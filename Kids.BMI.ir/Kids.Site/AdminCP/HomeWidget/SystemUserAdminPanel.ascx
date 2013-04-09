<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="SystemUserAdminPanel.ascx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.SystemUserAdminPanel"  %>
<table width="100%" style="width: 100%; height: 118px">
    <tr>
        <th align="right" height="25" class="TableHeaderText" style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);
            font-family: Tahoma,Verdana,Arial,Helvetica" width="100%">
           
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">&nbsp;مديريت 
            مسولان و راهبران سایت
        </th>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="addPollLnk" runat="server"  NavigateUrl="~/AdminCP/SystemUserAdmin/AdminUsers.aspx?act=new">مدیریت راهبران</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%" valign="top" style="height: 21px">
            نمایش لیست راهبران
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="pollListLnk" runat="server"  NavigateUrl="~/AdminCP/SystemUserAdmin/AdminRoles.aspx">مدیریت نقشها و مسئولیتها</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" style="height: 0px" width="100%" valign="top">
            نمايش ليست نقشها
        </td>
    </tr>
</table>
