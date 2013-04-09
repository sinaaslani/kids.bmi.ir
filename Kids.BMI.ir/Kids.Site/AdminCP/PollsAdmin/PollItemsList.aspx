<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="PollItemsList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.PollsAdmin.PollItemsList"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 26px" align="right" width="100%" colspan="2" >
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">
               
                    <asp:Label ID="pollTitleLbl" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 8px" align="right" width="100%" colspan="2" height="8">
                <font face="Tahoma" size="1">&nbsp;&nbsp;ليست گزينه ها</font>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="PollitemsGrid" runat="server" Height="30px" Width="100%" CellSpacing="1"
                    Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2"
                    AutoGenerateColumns="False" ShowHeader="False">
                    <AlternatingRowStyle Font-Names="Tahoma" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"
                        BackColor="#F0F7FF"></AlternatingRowStyle>
                    <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
                    <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                        CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                    <Columns>
                        <asp:BoundField DataField="ItemText">
                            <HeaderStyle HorizontalAlign="Right" Width="300px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF" />
                        </asp:BoundField>
                        <asp:HyperLinkField Text="ويرايش" DataNavigateUrlFields="ItemId" DataNavigateUrlFormatString="AddEditPollItem.aspx?act=edit&amp;itmid={0}">
                        </asp:HyperLinkField>
                        <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="ItemId" DataNavigateUrlFormatString="AddEditPollItem.aspx?act=del&amp;itmid={0}">
                        </asp:HyperLinkField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                    <asp:PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="AddNewItemLnk" runat="server" >اضافه کردن گزينه جديد</asp:HyperLink>
                &nbsp;&nbsp; <a href="pollList.aspx" class="LinkCadetBlue">ليست نظر سنجي ها </a>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="tblrseult" runat="server" EnableViewState="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
