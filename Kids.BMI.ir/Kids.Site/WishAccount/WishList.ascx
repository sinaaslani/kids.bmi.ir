<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WishList.ascx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.WishList" %>
<table style="width: 100%">
    <tr>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:DataList ID="dgWish" runat="server" RepeatColumns="2" 
                OnItemCommand="dgWish_ItemCommand" Width="70%">
                <ItemTemplate>
                    <asp:LinkButton ID="btnSelectWish" runat="server"
                        ToolTip='<%# Eval("WishDescription") %>' CommandArgument='<%# Eval("WishId") %>'>
                        <asp:Image ID="Image2" runat="server" ImageUrl='<%#"~/AdminCP/Files/Wish/"+ Eval("WishPicSmall") %>' /><br />

                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("WishName") %>'></asp:Label>
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr runat="server" id="pnlWishDetails" visible="false">
        <td>
            <fieldset style="width:98%">
                <legend>انتخاب آررزو</legend>
            <table style="width: 100%;">
                <tr>
                    <td align="right">
                        <asp:HiddenField runat="Server" ID="hdnWishId"/>
                        <asp:Label ID="lblWishName" runat="server"></asp:Label>
                        <asp:Image ID="imgWishPicSmall" runat="server"/>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Image ID="imgWishPic" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                      
                        مبلغ آرزوی شما:
                          <asp:Label ID="lblWishAmount" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblWishDescription" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSelectWish" runat="server" Text="میخواهم این آرزو به عنوان آرزوی اصلی من در سیستم ثبت شود"
                            OnClick="btnSelectWish_Click" />
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
</table>
