<%@ Page Title="سوالات متداول" Language="C#" MasterPageFile="~/Masters/Fa.Master"
    AutoEventWireup="true" CodeBehind="FAQList.aspx.cs" Inherits="Site.Kids.bmi.ir.Contact._FAQ.FAQList"
    Theme="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showBody(obj, state) {
            obj = document.getElementById(obj);
            //obj.style.display = state;
            if (obj.style.display == 'none')
                obj.style.display = 'inline';
            else
                obj.style.display = 'none';
        }
    </script>
    <table width="100%">
        <tr>
            <td valign="bottom" align="right">
                <asp:Label ID="lblTitle" runat="server" Css>پرسش و پاسخ :</asp:Label>
                &nbsp;
                <asp:DropDownList ID="TopCats" runat="server" AutoPostBack="True" OnSelectedIndexChanged="TopCats_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" width="100%" height="2" background="line.gif">
            </td>
        </tr>
        <tr>
            <td style="height: 7px" valign="top" align="center" width="100%">
                <div align="center">
                    <asp:DataGrid ID="itemsGrid" CellSpacing="1" runat="server" Font-Names="Tahoma" Font-Size="9pt"
                        ForeColor="Black" GridLines="None" Width="100%" CellPadding="2" AutoGenerateColumns="False"
                        PageSize="1" ShowHeader="False" OnPageIndexChanged="TopCats_SelectedIndexChanged">
                        <AlternatingItemStyle Font-Names="Tahoma" VerticalAlign="Middle"></AlternatingItemStyle>
                        <ItemStyle Font-Names="Tahoma" VerticalAlign="Middle"></ItemStyle>
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" ForeColor="White" CssClass="forumHeaderBackgroundAlternate"
                            BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <ItemStyle></ItemStyle>
                                <ItemTemplate>
                                    <table style="width: 100%">
                                        <tr>
                                            <td align="right">
                                                <div style="padding-top: 10px;">
                                                    <img src="/App_Themes/Default/images/bullet.jpg" align="absmiddle" />
                                                    <a onclick="showBody('<%#"Body"+Eval("FAQId") %>','inline')" style="cursor: pointer"
                                                        href="#">
                                                        <%# Eval( "Title") %></a>
                                                </div>
                                                <div id='<%#"Body"+Eval("FAQId") %>' style="display: none">
                                                    <fieldset>
                                                        <legend>
                                                            <%# Eval( "Title") %></legend>
                                                        <%# Eval( "Body") %>
                                                        <br />
                                                        <a onclick="showBody('<%#"Body"+Eval("FAQId") %>','none')" style="cursor: pointer"
                                                            href="#">عدم نمایش</a>
                                                    </fieldset>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Position="TopAndBottom" Wrap="False"
                            Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></div>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
