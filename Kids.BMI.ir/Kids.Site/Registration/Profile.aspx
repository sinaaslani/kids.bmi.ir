<%@ Page Title="اطلاعات کاربر" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Site.Kids.bmi.ir.Registration.Profile" %>

<%@ Register Src="UserProfileWidget.ascx" TagName="UserProfile" TagPrefix="uc1" %>
<%@ Register Src="../Scores/ScoreList.ascx" TagName="ScoreList" TagPrefix="uc2" %>
<%@ Register Src="../WishAccount/WishWidget.ascx" TagName="WishWidget" TagPrefix="uc3" %>
<%@ Register Src="../Payment/PaymentList.ascx" TagName="PaymentList" TagPrefix="uc4" %>
<%@ Register Src="Editable_UserProfileWidget.ascx" TagName="Editable_UserProfileWidget"
    TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%;">
        <tr>
            <td align="center" width="25%">
                <asp:LinkButton ID="lnkEdit" runat="server" OnClick="btnEdit_Click">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image2" runat="server" BorderStyle="None" ImageUrl="~/App_Themes/Default/images/edit_profile.png"
                                    ImageAlign="Middle" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                ویرایش اطلاعات
                            </td>
                        </tr>
                    </table>
                </asp:LinkButton>
            </td>
            <td align="center" width="25%">
                <asp:LinkButton ID="lnkScores" runat="server" OnClick="lnkScores_Click">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image1" runat="server" BorderStyle="None" ImageUrl="~/App_Themes/Default/images/chart_up.png"
                                    ImageAlign="Middle" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                امتیازات
                            </td>
                        </tr>
                    </table>
                </asp:LinkButton>
            </td>
            <td align="center" width="25%">
                <asp:LinkButton ID="lnkPayments" runat="server" OnClick="lnkPayments_Click">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image3" runat="server" Width="48" BorderStyle="None" ImageUrl="~/App_Themes/Default/images/Bloc Note.png"
                                    ImageAlign="Middle" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                پرداختها
                            </td>
                        </tr>
                    </table>
                </asp:LinkButton>
            </td>
            <td align="center" width="25%">
                <asp:LinkButton ID="lnkWish" runat="server" OnClick="lnkWish_Click">
                    <table>
                        <tr>
                            <td align="center">
                                <asp:Image ID="Image4" runat="server" BorderStyle="None" ImageUrl="~/App_Themes/Default/images/heart.png"
                                    ImageAlign="Middle" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                آرزو
                            </td>
                        </tr>
                    </table>
                </asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlUserInfo" Visible="false">
                    <uc1:UserProfile ID="ucUserProfile" runat="server" BasicInfoIsEditable="false" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlScore" Visible="false">
                    <fieldset>
                        <legend>امتیازات کسب شده</legend>
                        <uc2:ScoreList ID="ucScoreList" runat="server" />
                    </fieldset>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlPayment" Visible="false">
                    <uc4:PaymentList ID="ucPaymentList" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlWish" Visible="false">
                    <uc3:WishWidget ID="ucWishWidget" runat="server" />
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Panel runat="server" ID="pnlEdit" Visible="false">
                    <asp:Button ID="btnSave2" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                        Text="ذخیره اطلاعات" />
                    <uc5:Editable_UserProfileWidget ID="ucEditable_UserProfileWidget" runat="server" />
                    <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" OnClick="btnSave_Click"
                        Text="ذخیره اطلاعات" />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
