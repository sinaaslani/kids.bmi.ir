<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DataListPager.ascx.cs"
    Inherits="Site.Kids.bmi.ir.UserControls.DataListPager" %>
<asp:DataList ID="lstPager" runat="server" style="direction:rtl"
    OnItemCommand="DataList1_ItemCommand" RepeatDirection="Horizontal" 
    OnItemDataBound="DataList1_ItemDataBound">
    <ItemTemplate>
        <asp:LinkButton ID="lnkPager" runat="server"></asp:LinkButton>
    </ItemTemplate>
</asp:DataList>
