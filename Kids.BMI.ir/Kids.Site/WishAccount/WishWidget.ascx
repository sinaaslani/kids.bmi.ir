<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WishWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.WishAccount.WishWidget" %>
<asp:HyperLink runat="server" ID="lnkWish" NavigateUrl="~/WishAccount/WishAccHome.aspx">
    <table style="width: 100%; direction: rtl; text-align:center">
        <tr>
            <td align="right" valign="top">
                <asp:Label ID="lblWishName" runat="server"></asp:Label>
                <asp:Image ID="imgWishPicSmall" runat="server" ImageAlign="AbsMiddle" Height="65px" />
            </td>
            <td align="right" valign="middle">
                مبلغ آرزو:
                <asp:Label ID="lblWishAmount" runat="server"></asp:Label>
            </td>
            <td align="right" valign="middle">
                <asp:Label ID="lblWishDescription" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:HyperLink>
<span runat="server" id="pnlMessage" visible="false">شما هنوز آرزوی خود را انتخاب نکرده
    اید.برای انتخاب آرزوی خود <a href="/WishAcc.aspx">اینجا</a> کلیک کنید. </span>
