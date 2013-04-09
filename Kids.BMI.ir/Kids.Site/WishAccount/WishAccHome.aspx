<%@ Page Title="حساب آرزو" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    Theme="Default" CodeBehind="WishAccHome.aspx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.WishAccHome" %>

<%@ Register Src="AccBill.ascx" TagName="AccBill" TagPrefix="uc1" %>
<%@ Register Src="WishAccountCalculator.ascx" TagName="WishAccountCalculator" TagPrefix="uc2" %>
<%@ Register Src="WishList.ascx" TagName="WishList" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 95%;" runat="server">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr runat="server" id="pnlmain" visible="false">
                        <td align="center" width="33%">
                            <asp:LinkButton runat="server" ID="btnBill" OnClick="btnBill_Click" CausesValidation="False">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/Wish/account.png"
                                    ImageAlign="Baseline" BorderStyle="None" /><br />
                                مشاهده صورتحساب آرزو
                            </asp:LinkButton>
                        </td>
                        <td align="center" width="33%">
                            <asp:LinkButton runat="server" ID="btnWish" OnClick="btnWish_Click" CausesValidation="False">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Default/images/Wish/shine_22.png"
                                    ImageAlign="Baseline" BorderStyle="None" /><br />
                                انتخاب آرزو
                            </asp:LinkButton>
                        </td>
                        <td align="center" width="33%">
                            <asp:LinkButton runat="server" ID="btnWishCalculator" OnClick="btnWishCalculator_Click"
                                CausesValidation="False">
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Default/images/Wish/calculator.png"
                                    ImageAlign="Baseline" BorderStyle="None" /><br />
                                محاسبه گرآرزو
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <hr />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <uc1:AccBill ID="ucAccBill" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td align="right" >
                <uc2:WishAccountCalculator ID="ucWishAccountCalculator" runat="server" Visible="false" />
            </td>
        </tr>
        <tr>
            <td align="right" >
                <uc3:WishList ID="ucWishList" runat="server" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>
