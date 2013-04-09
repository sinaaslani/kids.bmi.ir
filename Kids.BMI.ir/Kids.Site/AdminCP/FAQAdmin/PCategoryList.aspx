<%@ Page Language="c#" AutoEventWireup="True" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    CodeBehind="PCategoryList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.FAQAdmin.PCategoryList"
     %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td  style="height: 26px" align="right" width="100%" colspan="2" >
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;
                    &nbsp;گروه های&nbsp;پرسش های متداول&nbsp;
            </td>
        </tr>
        <tr>
            <td >
            </td>
        </tr>
        <tr>
            <td  style="height: 26px" align="right" width="100%" colspan="2" >
                <div align="center">
                    <asp:GridView ID="ListGrid" runat="server" Height="30px" Width="100%" CellSpacing="1"
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2"
                        AutoGenerateColumns="False" AllowPaging="True" 
                        onpageindexchanging="ListGrid_PageIndexChanging">
                        <AlternatingRowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF">
                        </RowStyle>
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="عنوان">
                                <HeaderStyle HorizontalAlign="Right" Width="300px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:HyperLinkField Text="پرسش ها" DataNavigateUrlFields="CategoryId" DataNavigateUrlFormatString="FAQlist.aspx?pcid={0}">
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="ویرایش" DataNavigateUrlFields="categoryId" DataNavigateUrlFormatString="pCategoryAdmin.aspx?act=edit&amp;pcid={0}">
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="categoryId" DataNavigateUrlFormatString="pCategoryAdmin.aspx?act=del&amp;pcid={0}">
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue"  Wrap="False">
                        </PagerStyle>
                        <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td >
                <asp:HyperLink ID="AddNewLnk" runat="server"  NavigateUrl="~/AdminCP/FAQAdmin/PCategoryAdmin.aspx?act=new"> ایجاد گروه جدید</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
