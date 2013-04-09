<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True"
    CodeBehind="PostalCardList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.PostalCardAdmin.PostalCardList"
    Theme="Admin" %>

<%@ Import Namespace="Kids.Common" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td width="100%" align="right">
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;
                    لیست بازی&nbsp;
            </td>
        </tr>
        <tr>
            <td style="height: 103px" width="100%">
                <table width="100%">
                    <tr>
                        <td align="right" width="50">
                            نام:
                        </td>
                        <td align="right">
                            <asp:TextBox ID="searchKeyTxt" runat="server" Width="207px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;<img style="width: 57px; height: 2px" height="2" src="/App_Themes/Admin/Images/blankImg.gif"
                                width="57">
                        </td>
                        <td style="width: 538px">
                            <asp:Button ID="btnSearch" runat="server"  Text="   جستجو   "
                                OnClick="btnSearch_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" width="100%" style="height: 21px">
                <font face="Tahoma" size="2">تعداد نتایج یافت شده&nbsp;:</font>
                <asp:Label ID="ResultNumberLbl"  runat="server"></asp:Label>&nbsp;<font
                    face="Tahoma" size="2">مورد</font>
            </td>
        </tr>
        <tr>
            <td align="center" width="100%">
                <div align="center">
                    <asp:GridView ID="dgPostalCards" Height="30px" runat="server" 
                        AllowPaging="True" PageSize="20"
                        AutoGenerateColumns="False" CellPadding="2" Width="100%" GridLines="None" ForeColor="Black"
                        Font-Size="9pt" Font-Names="Tahoma" CellSpacing="1" 
                        OnPageIndexChanging="dgPostalCards_PageIndexChanging">
                        <AlternatingRowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF">
                        </RowStyle>
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="تصویر" >
                                <ItemTemplate>
                                    <asp:Image ID="Label1" Width=100 runat="server" ImageUrl='<%#SystemConfigs.UrlPostalCardFilesPath+ Eval("CardPostalSmallPic") %>'>
                                    </asp:Image>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CardName" HeaderText="نام کارت" HtmlEncode="False" HtmlEncodeFormatString="False">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                    </asp:GridView>
                </div>
                 
            </td>
        </tr>
        <tr>
            <td align="right" width="100%">
                <asp:HyperLink ID="lnkAdd" Text="ایجاد مورد جدید" runat="server" 
                    NavigateUrl="~/AdminCP/PostalCardAdmin/PostalCardAdmin.aspx?act=new"></asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
