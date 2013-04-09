<%@ Page Title="صندوقچه اطلاعات" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="true"
    CodeBehind="NewList.aspx.cs" Inherits="Site.Kids.bmi.ir.InfoBox.NewList" Theme="Default" %>

<%@ Import Namespace="Kids.Common" %>
<%@ Import Namespace="Kids.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td valign="bottom" align="right">
                <asp:Label ID="lblTitle" runat="server" Font-Names="Mj_Two">آخرين اخبار</asp:Label>
            </td>
            <td valign="bottom" class="LangUnAlign" align="left">
                <font size="1">
                    <br>
                </font>
                <asp:DropDownList ID="TopCatNews" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TopCatNews_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" width="100%" background="line.gif" colspan="2" height="2">
            </td>
        </tr>
        <tr>
            <td style="height: 1px" align="center" width="100%" colspan="2">
                <div align="center">
                    <asp:GridView ID="newsGrid" runat="server" ShowHeader="False" PageSize="5" CellPadding="0"
                        GridLines="None" ForeColor="Black" Font-Names="Mj_Two" CellSpacing="1" Width="100%"
                        Height="30px" EnableViewState="False" AutoGenerateColumns="False">
                        <AlternatingRowStyle Font-Names="Mj_Two" HorizontalAlign="Center" VerticalAlign="Middle">
                        </AlternatingRowStyle>
                        <RowStyle Font-Names="Mj_Two" HorizontalAlign="Center" VerticalAlign="Middle"></RowStyle>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table width='100%' style="background-image: url('/App_Themes/Default/images/News/news-back.png'); width: 650px; height: 188px;  background-repeat: no-repeat; background-position: center; background-size: 650px; padding: 50px;" align="center">
                                        <tr>
                                            <td width='100%' style='width: 100%' valign="top">
                                                <asp:Label runat="server" ForeColor="gray" Text='<%#PersianDateTime.MiladiToPersian((DateTime)Eval("CreateDateTime")).ToLongDateTimeString() %>'></asp:Label>
                                                <br>
                                                    <asp:HyperLink runat="server" class='LinkTitleNews' NavigateUrl='<%# "/News.aspx?id="+Eval("NewsId")%>'
                                                        Text='<%#Eval("Title") %>'></asp:HyperLink><br />
                                                    <asp:Label runat="server" class='normalTextSmall' Text='<%#Eval("Summary").ToString().Length < 256?Eval("Summary"):Eval("Summary").ToString().Substring(0, 256) %>'></asp:Label>
                                                    <br></br>
                                                </br>
                                            </td>
                                            <td valign="top">
                                                <asp:Image runat="server" border="0" Width='120' ImageUrl='<%# SystemConfigs.UrlNewsFilesPath + Eval("SmallPicAddress")%>' />
                                                <asp:HyperLink runat="server" NavigateUrl='<%# "/News.aspx?id="+Eval("NewsId")%>'>ادامه >> </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <hr />
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle Font-Size="9pt" Font-Names="Mj_Two" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate"></HeaderStyle>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                        <PagerSettings Position="Top" Mode="NumericFirstLast" />
                    </asp:GridView>
                </div>
                <div id="subNewsDiv" runat="server">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table style="width: 100%">
                    <tr>
                        <td colspan="2">
                            &nbsp;<asp:HyperLink ID="lnkPrevMonth" class="linkCadetBlue" runat="server"></asp:HyperLink>
                        </td>
                        <td align="left">
                            <asp:HyperLink ID="lnkNextMonth" class="linkCadetBlue" runat="server"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="1">
                <asp:Label ID="lblContinue" runat="server" Visible="False">ادامه</asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
