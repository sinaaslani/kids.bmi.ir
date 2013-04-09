<%@ Page Title="بازی و سرگرمی" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" Theme="Default" CodeBehind="PostalCardList.aspx.cs" Inherits="Site.Kids.bmi.ir.WishAccount.PostalCardList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>
            انتخاب کارت پستال
        </legend>
    <asp:DataList ID="dgPostalCards" runat="server" RepeatColumns="2" Width="80%" CellPadding="10"
        CellSpacing="10" OnItemDataBound="dgPostalCards_ItemDataBound" 
    BackColor="Transparent">
        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        <ItemTemplate>
            <asp:HyperLink ID="lnkPostalCard" runat="server" BorderWidth="0" ToolTip='<%# Eval("CardPostalDescription") %>'>
                <asp:Image ID="Image2" Width="60px" runat="server" ImageUrl='<%#string.Format("~/AdminCP/Files/PostalCard/{0}", Eval("CardPostalSmallPic")) %>' />
                <br />
                <asp:Label ID="lblPostalCardName" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("CardPostalDescription") %>' Font-Size="XX-Small"></asp:Label>
            </asp:HyperLink>
        </ItemTemplate>
    </asp:DataList>
    </fieldset>
</asp:Content>
