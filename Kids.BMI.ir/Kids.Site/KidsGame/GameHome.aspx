<%@ Page Title="بازی و سرگرمی" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" Theme="Default" CodeBehind="GameHome.aspx.cs" Inherits="Site.Kids.bmi.ir.KidsGame.GameHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataList ID="dgGames" runat="server" RepeatColumns="2" Width="80%" CellPadding="10"
        CellSpacing="10" OnItemDataBound="dgGames_ItemDataBound" 
    BackColor="Transparent" Font-Bold="False" Font-Italic="False" 
    Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" 
    Font-Underline="False">
        <AlternatingItemStyle Font-Bold="False" Font-Italic="False" 
            Font-Overline="False" Font-Size="Medium" Font-Strikeout="False" 
            Font-Underline="False" />
        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="False" 
            Font-Italic="False" Font-Overline="False" Font-Size="Medium" 
            Font-Strikeout="False" Font-Underline="False" />
        <ItemTemplate>
            <asp:HyperLink ID="lnkGame" runat="server" BorderWidth="0" ToolTip='<%# Eval("Description") %>' Target="_blank">
                <asp:Image ID="Image2" Width="572px" Height="373" runat="server" ImageUrl='<%#string.Format("~/AdminCP/Files/Game/{0}", Eval("ThumbnailAddress")) %>' />
                <br />
                <asp:Label ID="lblGameName" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' Font-Size="XX-Small"></asp:Label>
            </asp:HyperLink>
        </ItemTemplate>
        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
        <SeparatorStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Size="Medium" Font-Strikeout="False" Font-Underline="False" />
    </asp:DataList>
</asp:Content>
