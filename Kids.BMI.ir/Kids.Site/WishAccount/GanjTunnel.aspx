<%@ Page Title="تونل گنج" Theme="Default" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="GanjTunnel.aspx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.GanjTunnel" %>

<%@ Register Src="GanjTunelCalculator.ascx" TagName="GanjTunelCalculator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%; font-family: Mj_Two; font-size: 15px; line-height: 30px;"
        runat="server" id="pnlMain" visible="false">
        <tr>
            <td align="right">
                <strong>تونل گنج</strong><br />
                &nbsp;در اين تونل به شما ياد مي دهيم كه چطور مي توانيد پول دربياوريد و يا از محل
                پول توجيبي هاي خودتان كم كم به يك گنج بزرگ دست بيابيد. اينها روشهاي طلايي هستند
                كه فقط به شما ساكنين جزيره آرزوها گفته مي شود.<br />
                <br />
                <strong>روش پيشنهادي تونل گنج:
                    <br />
                </strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:DataList ID="DataList1" runat="server" Width="100%">
                    <ItemTemplate>
                        <fieldset style="width: 95%;">
                            <legend>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Default/images/bullet_green.png"
                                    ImageAlign="Middle" />
                                <asp:Literal ID="lblTitle" Text='<%#Eval("Title") %>' runat="server"></asp:Literal>
                            </legend>
                            <table style="width: 100%;">
                                <tr>
                                    <td align="right">
                                        <asp:Literal ID="lblBody" Text='<%#Eval("Body") %>' runat="server"></asp:Literal>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <uc1:GanjTunelCalculator ID="ctrl_ucGanjTunelCalculator"
                                            ValidationGrp='<%# "ucGanjTunelCalculator_"+Eval("PageId")%>' runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </ItemTemplate>
                </asp:DataList>
            </td>
        </tr>
    </table>
</asp:Content>
