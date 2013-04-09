<%@ Page Language="C#" AutoEventWireup="true" Inherits="Site.Kids.bmi.ir.Payment.Payment_Commit"
    CodeBehind="Payment_Commit.aspx.cs" Theme="Default" MasterPageFile="~/Masters/FA.Master" Title="بررسی وضعیت پرداخت" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label>
    <table id="pnlResult" style="width: 100%;" runat="server" enableviewstate="false"
        visible="false">
        <tr>
            <td>
                <br />
                <hr />
                <br />
            </td>
        </tr>
        <tr>
            <td align="center">
                <table style="width: 100%;">
                    <tr>
                        <td width="55%" align="center" valign="top" style="width: 100%">
                            <fieldset style="direction: rtl; width: 60%; border: 1px solid black">
                                <legend>اطلاعات تراکنش خرید بانکی</legend>
                                <table style="width: 80%; direction: ltr; line-height: 20px;">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblOrderId" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td align="right" width="100">
                                            : شماره سفارش
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTraceNo" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td align="right">
                                            : شماره پیگیری
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblRefrenceNo" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td align="right">
                                            : شماره مرجع
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblAmount" runat="server" ForeColor="Blue"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblAmount_Letter" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td align="right">
                                            : مبلغ تراکنش
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblTranDate" runat="server" ForeColor="Blue"></asp:Label>
                                        </td>
                                        <td align="right">
                                            : تاریخ تراکنش
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <hr />
            </td>
        </tr>
    </table>
</asp:Content>
