<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True"
    CodeBehind="~/AdminCP/NewsAdmin/NewsCatList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.NewsAdmin.NewsCatList"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="450">
        <tr>
            <td width="100%">
                <img src="/App_Themes/Default/Admin/admin_bullet.gif">&nbsp;
                    لیست&nbsp;موضوعات خبری&nbsp;
            </td>
        </tr>
        <tr>
            <td width="100%" align="center">
                <div align="center">
                    <asp:GridView ID="newsCatGrid" Height="30px" PageSize="20" AutoGenerateColumns="False"
                        CellPadding="2" Width="100%" GridLines="None" ForeColor="Black" Font-Size="9pt"
                        Font-Names="Tahoma" runat="server" CellSpacing="1">
                        <AlternatingRowStyle Font-Names="Tahoma" VerticalAlign="Middle"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" VerticalAlign="Middle" />
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" ForeColor="White" CssClass="forumHeaderBackgroundAlternate"
                            BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="NewsCategoryName" HeaderText="موضوع">
                                <HeaderStyle HorizontalAlign="Right" Width="150px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Middle" BackColor="#E6F1FF" />
                            </asp:BoundField>
                            <asp:HyperLinkField Text="ویرایش"   DataNavigateUrlFields="NewsCategoryId" DataNavigateUrlFormatString="/AdminCp/NewsAdmin/NewsCatAdmin.aspx?act=edit&amp;nwscid={0}"
                                HeaderText="ویرایش">
                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="NewsCategoryId" DataNavigateUrlFormatString="/AdminCp/NewsAdmin/NewsCatAdmin.aspx?act=del&amp;nwscid={0}"
                                HeaderText="حذف">
                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue"  Wrap="False"></PagerStyle>
                        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                    </asp:GridView></div>
                 
            </td>
        </tr>
        <tr>
            <td align="right" width="100%">
            </td>
        </tr>
    </table>
</asp:Content>
