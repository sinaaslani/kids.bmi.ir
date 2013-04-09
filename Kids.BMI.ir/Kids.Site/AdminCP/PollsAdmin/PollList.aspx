<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="PollList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.PollsAdmin.PollList"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 26px" align="right" width="100%" colspan="2" >
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;نظر سنجي هاي
                    موجود&nbsp;
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 26px" align="right" width="100%" colspan="2" >
                <div align="center">
                    <asp:GridView ID="PollListGrid" runat="server" Height="30px" Width="100%" CellSpacing="1"
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2"
                        AutoGenerateColumns="False" AllowPaging="True">
                        <AlternatingRowStyle Font-Names="Tahoma" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF">
                        </RowStyle>
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="Title">
                                <HeaderStyle HorizontalAlign="Right" Width="300px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:HyperLinkField Text="مشاهده نتايج" DataNavigateUrlFields="QuestionId" DataNavigateUrlFormatString="~/AdminCP/PollsAdmin/pollItemsList.aspx?pid={0}">
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="ويرايش" DataNavigateUrlFields="QuestionId" DataNavigateUrlFormatString="~/AdminCP/PollsAdmin/AddEditPoll.aspx?act=edit&amp;pid={0}">
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="QuestionId" DataNavigateUrlFormatString="~/AdminCP/PollsAdmin/AddEditPoll.aspx?act=del&amp;pid={0}">
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                        <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="AddNewQuestionLnk" runat="server"  NavigateUrl="~/AdminCP/PollsAdmin/AddEditPoll.aspx?act=new">اضافه کردن سوال جديد</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
