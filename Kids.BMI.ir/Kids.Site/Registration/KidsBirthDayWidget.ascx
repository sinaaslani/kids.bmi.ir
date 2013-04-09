<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KidsBirthDayWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Registration.KidsBirthDayWidget" %>
<asp:HyperLink ID="lnkBirthDay" runat="server" NavigateUrl="~/KidsBirthday.aspx">
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblBirthDayInfo" runat="server" Text="فرزند عزیزم : تولدت مبارک!"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="imgBirthDayPic" runat="server" Width="52px" ImageUrl="~/App_Themes/Default/images/BirthDay/ballon.png"
                    Border="0" />
                <asp:Image ID="Image1" runat="server" Width="52px" ImageUrl="~/App_Themes/Default/images/BirthDay/cake.png"
                    Border="0" />
                <asp:Image ID="Image2" runat="server" Width="52px" ImageUrl="~/App_Themes/Default/images/BirthDay/favorites.png"
                    Border="0" />
            </td>
        </tr>
    </table>
</asp:HyperLink>