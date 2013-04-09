<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="True" CodeBehind="AdminUsers.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.SystemUserAdmin.AdminUsers"
    Title="مدیریت کاربران" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" dir="ltr">
        <tr>
            <td  width="100%" align="right">
                <img runat="server" id="img1" src="~/App_Themes/Admin/Images/admin_bullet.gif" alt=""/>
               ليست کاربران عضو سايت&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 103px" width="100%" align="center">
                <table width="75%">
                    <tr>
                        <td style="width: 543px; height: 6px"  align="right">
                            <asp:TextBox ID="LNameTxt" runat="server"></asp:TextBox>
                        </td>
                        <td  align="left">
                             : نام خانوادگي
                        </td>
                        <td  align="right">
                            <asp:TextBox ID="FNameTxt" runat="server"></asp:TextBox>
                        </td>
                        <td  align="right" width="150">
                             : نام
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 543px; height: 6px"  align="right" height="5">
                            &nbsp;</td>
                        <td style="width: 543px; height: 6px"  align="left">
                            &nbsp;
                        </td>
                        <td style="width: 543px; height: 6px"  align="right">
                            <asp:TextBox ID="UserNameTxt" runat="server"></asp:TextBox>
                        </td>
                        <td  align="right" width="100">
                            :نام کاربري
                        </td>
                    </tr>
                    <tr>
                        <td  colspan="4" align="center">
                            <asp:Button ID="searchBtn" runat="server"  Text="   جستجو   "
                                OnClick="SearchBtn_Click"></asp:Button>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnNewUser" runat="server"  Text="افزودن کاربر جدید"
                                OnClick="btnNewUser_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td  align="right" width="100%" style="height: 21px">
                <font face="Tahoma" size="2">تعداد نتايج يافت شده&nbsp;:</font>
                <asp:Label ID="ResultNumberLbl"  runat="server"></asp:Label>&nbsp;<font
                    face="Tahoma" size="2">مورد</font>
            </td>
        </tr>
        <tr>
            <td align="center" width="100%" >
                <asp:GridView ID="usersGrid" Height="30px" runat="server" AllowPaging="True" PageSize="30"
                    AutoGenerateColumns="False" CellPadding="2" Width="60%" GridLines="None" ForeColor="Black"
                    Font-Size="9pt" Font-Names="Tahoma" CellSpacing="1" PagerSettings-Position="TopAndBottom"
                    Wrap="False" PagerSettings-Mode="NumericFirstLast" OnPageIndexChanging="UsersGrid_PageIndexChanged">
                    <AlternatingRowStyle Font-Names="Tahoma" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"
                        BackColor="#F0F7FF"></AlternatingRowStyle>
                    <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom"></PagerSettings>
                    <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF">
                    </RowStyle>
                    <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                        CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="AddEditUser.aspx?uid={0}&act=edit"
                            DataTextField="SSOUserName" HeaderText="نام کاربري">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="Name" HeaderText="نام">
                            <HeaderStyle Width="70px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Family" HeaderText="نام خانوادگي">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="SSOUserName" HeaderText="نام کاربری">
                            <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
                        <asp:CheckBoxField DataField="Active" HeaderText="وضعیت ">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:CheckBoxField>
                        
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="Blue"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td  align="right" width="100%">
            </td>
        </tr>
    </table>
</asp:Content>
