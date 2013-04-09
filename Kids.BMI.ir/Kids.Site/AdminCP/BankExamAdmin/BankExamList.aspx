<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="BankExamList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.BankExamAdmin.BankExamList"
    Theme="Admin" %>

<%@ Import Namespace="Kids.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 26px" align="right" width="100%" colspan="2" >
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">نظر
                    سنجي هاي موجود
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 26px" align="right" width="100%" colspan="2" >
                <div align="center">
                    <asp:GridView ID="dgExamList" runat="server" Height="30px" Width="100%" CellSpacing="1"
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2"
                        AutoGenerateColumns="False" AllowPaging="True">
                        <AlternatingRowStyle Font-Names="Tahoma" Wrap="False" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF"></AlternatingRowStyle>
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF">
                        </RowStyle>
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="ExamName">
                                <HeaderStyle HorizontalAlign="Right" Width="300px"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#PersianDateTime.MiladiToPersian(Convert.ToDateTime( Eval("IsActiveFromDate"))).ToShortDateTimeString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Width="300px" />
                                <ItemStyle BackColor="#E6F1FF" HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#PersianDateTime.MiladiToPersian(Convert.ToDateTime( Eval("IsActiveToDate"))).ToShortDateTimeString() %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" Width="300px" />
                                <ItemStyle BackColor="#E6F1FF" HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:HyperLinkField Text="ويرايش" DataNavigateUrlFields="ExamId" DataNavigateUrlFormatString="~/AdminCP/BankExamAdmin/AddEditBankExam.aspx?act=edit&amp;pid={0}">
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="ExamId" DataNavigateUrlFormatString="~/AdminCP/BankExamAdmin/AddEditBankExam.aspx?act=del&amp;pid={0}">
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
                <asp:HyperLink ID="AddNewQuestionLnk" runat="server"  NavigateUrl="~/AdminCP/BankExamAdmin/AddEditBankExam.aspx?act=new">اضافه کردن آزمون جديد</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
