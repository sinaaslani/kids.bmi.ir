<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="true" CodeBehind="AdminRoles.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.SystemUserAdmin.AdminRoles"
    Title="مدیریت کاربران" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td  width="100%" align="right">
                <img runat="server" id="img1" src="~/App_Themes/Admin/Images/admin_bullet.gif" />
               ليست&nbsp;نقش هاي مسووليتي&nbsp;
            </td>
        </tr>
        <tr>
            <td  width="100%" align="right">
                <asp:HyperLink ID="lnlAddRole" runat="server" NavigateUrl="AddEditRoles.aspx?act=new">افزودن نقش جدید</asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td  width="50%" align="center">
                <asp:GridView ID="RolesGrid" Height="30px" PageSize="20" AutoGenerateColumns="False"
                    CellPadding="2" Width="50%" GridLines="None" ForeColor="Black" Font-Size="9pt"
                    Font-Names="Tahoma" runat="server" CellSpacing="1" DataKeyNames="RoleID" OnSelectedIndexChanged="RolesGrid_SelectedIndexChanged">
                    <AlternatingRowStyle BackColor="#DEDFDE" Font-Names="Tahoma" HorizontalAlign="Center"
                        VerticalAlign="Middle" Wrap="False" />
                    <RowStyle BackColor="#DEDFDE" Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                        CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                    <SelectedRowStyle Font-Size="9pt" Font-Names="Tahoma" ForeColor="Black" CssClass="forumHeaderBackgroundAlternate"
                        BackColor="Orange" />
                    <Columns>
                        <asp:BoundField DataField="RoleName" HeaderText="نام نقش">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RoleDescription" HeaderText="توضیحات">
                        <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:HyperLinkField Text="ويرايش" DataNavigateUrlFields="RoleID" DataNavigateUrlFormatString="AddEditRoles.aspx?act=edit&amp;role={0}"
                            HeaderText="ويرايش"></asp:HyperLinkField>
                        <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="RoleID" DataNavigateUrlFormatString="AddEditRoles.aspx?act=delete&amp;role={0}"
                            HeaderText="حذف"></asp:HyperLinkField>
                        <asp:CommandField ShowSelectButton="True" SelectText="مشاهده اعضا" ButtonType="Button">
                        </asp:CommandField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" Wrap="False"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td  width="50%" align="center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td  align="center" width="100%">
                <asp:GridView ID="dgUserInRole" Height="30px" PageSize="20" AutoGenerateColumns="False"
                    CellPadding="2" GridLines="None" ForeColor="Black" Font-Size="9pt" Font-Names="Tahoma"
                    runat="server" CellSpacing="1" onrowdeleting="dgUserInRole_RowDeleting" DataKeyNames="UserID">
                    <AlternatingRowStyle BackColor="#DEDFDE" Font-Names="Tahoma" HorizontalAlign="Center"
                        VerticalAlign="Middle" Wrap="False" />
                    <RowStyle BackColor="#DEDFDE" Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                        CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="نام ">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name")+" "+Eval("Family") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SSoUserName" HeaderText="نام کاربری"></asp:BoundField>
                        <asp:CommandField DeleteText="حذف" ShowDeleteButton="True" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" Wrap="False"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
