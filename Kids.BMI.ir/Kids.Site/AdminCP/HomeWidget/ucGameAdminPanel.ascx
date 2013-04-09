<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ucGameAdminPanel.ascx.cs"
    Inherits="Site.Kids.bmi.ir.AdminCP.HomeWidget.ucGameAdminPanel"  %>
<table width="100%" style="width: 100%;">
    <tr>
        <th align="right" height="25" class="TableHeaderText" style="filter: progid:DXImageTransform.Microsoft.Gradient(startColorStr=#FFFFFF, endColorStr=#0865BD, gradientType=1);
            font-family: Tahoma,Verdana,Arial,Helvetica" width="100%">
           
                <img src="/App_Themes/Default/Admin/admin_bullet.gif" alt="">&nbsp;مدیریت 
            بازیها
        </th>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="AddNewsLnk" runat="server"  NavigateUrl="~/AdminCP/GameAdmin/GameAdmin.aspx?act=new">ایجاد بازی جدید</asp:HyperLink>
        </td>
    </tr>
    <tr>
        <td align="right" width="100%">
            <asp:HyperLink ID="EditNewsLnk" runat="server"  NavigateUrl="~/AdminCP/GameAdmin/GameList.aspx">ویرایش و حذف بازی</asp:HyperLink>
        </td>
    </tr>
</table>
