<%@ Page Language="c#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="True"
    CodeBehind="ScoreTypeList.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.ScoreAdmin.ScoreTypeList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%">
        <tr>
            <td  style="height: 26px" align="right" width="100%" colspan="2" >
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">&nbsp;
                    فهرست انواع امتیازات
            </td>
        </tr>
        <tr>
            <td >
            </td>
        </tr>
        <tr>
            <td  style="height: 26px" align="right" width="100%" colspan="2" >
                <div align="center">
                    <asp:GridView ID="ListGrid" runat="server"  Width="100%" CellSpacing="1"
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="Black" GridLines="None" CellPadding="2"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="30" OnPageIndexChanging="ListGrid_PageIndexChanging">
                        <AlternatingRowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle"
                            BackColor="#F0F7FF" />
                        <RowStyle Font-Names="Tahoma" HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#F0F7FF" />
                        <HeaderStyle Font-Size="9pt" Font-Names="Tahoma" HorizontalAlign="Center" ForeColor="White"
                            CssClass="forumHeaderBackgroundAlternate" BackColor="#0D66BA"></HeaderStyle>
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="شماره سیستمی">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ScoreFAName" HeaderText="عنوان">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="ScoreEnName" HeaderText="عنوان سیستمی">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CoefficentValue" HeaderText="ضریب">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaxPerMonth" HeaderText="ماکزیمم در ماه">
                                <HeaderStyle HorizontalAlign="Right"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="MaxPerDay" HeaderText="ماکزیمم در روز">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" BackColor="#E6F1FF"></ItemStyle>
                            </asp:BoundField>
                            <asp:HyperLinkField Text="ویرایش" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/AdminCP/ScoreAdmin/ScoreTypeAdmin.aspx?act=edit&amp;pid={0}">
                                <ItemStyle Width="30px" />
                            </asp:HyperLinkField>
                            <asp:HyperLinkField Text="حذف" DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/AdminCP/ScoreAdmin/ScoreTypeAdmin.aspx?act=del&amp;pid={0}">
                                <ItemStyle Width="30px" />
                            </asp:HyperLinkField>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="False"></PagerStyle>
                        <PagerSettings Position="TopAndBottom" Mode="NextPreviousFirstLast" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
        <tr>
            <td >
                <asp:HyperLink ID="AddNewLnk" runat="server"  NavigateUrl="~/AdminCP/ScoreAdmin/ScoreTypeAdmin.aspx?act=new"> ایجاد نوع امتیاز جدید</asp:HyperLink>
            </td>
        </tr>
    </table>
</asp:Content>
