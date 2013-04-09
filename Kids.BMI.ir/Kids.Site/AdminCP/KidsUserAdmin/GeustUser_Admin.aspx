<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="True"
    Inherits="Site.Kids.bmi.ir.AdminCP.KidsUserAdmin.GeustUser_Admin" CodeBehind="GeustUser_Admin.aspx.cs"
    Theme="Admin" %>

<%@ Import Namespace="Kids.Utility" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            return confirm('آیا مطمئن هستید؟');
        }
    </script>
    <table style="width: 100%;">
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                            <fieldset id="pnlSearch" runat="server">
                                <legend style="font-weight: bold">جستجو</legend>
                                <br />
                                <table style="width: 100%; background-color: #EBEBEB;">
                                    <tr>
                                        <td style="text-align: left">
                                            نام :
                                        </td>
                                        <td style="text-align: left; width: 123px;">
                                            <asp:TextBox ID="txtSearchName" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            نام خانوتدگی :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchFamily" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            کد ملی :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchMelliCode" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            شماره موبایل :
                                        </td>
                                        <td style="text-align: left; width: 123px;">
                                            <asp:TextBox ID="txtSearchMobileNumber" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            آدرس ایمیل :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchEmailAddress" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            &nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: center">
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="جستجو  و مشاهده  "
                                CssClass="btn" Width="130px" ValidationGroup="View" CausesValidation="False" />
                            &nbsp;&nbsp; &nbsp;<asp:ImageButton ID="ImgExportToExcell" runat="server" ImageUrl="~/App_Themes/Default/images/exel.PNG"
                                OnClick="ImgExportToExcell_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: center">
                            <asp:Label ID="lblMessage" runat="server" EnableViewState="False" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <fieldset id="pnlResult" runat="server" visible="false">
                                <legend style="font-weight: bold">نتایج</legend>
                                <br />
                                <asp:GridView ID="dgGeustUser" runat="server" AutoGenerateColumns="False" BorderWidth="2px"
                                    CellPadding="4" ForeColor="#333333"
                                    Width="800px" AllowPaging="True" OnPageIndexChanging="dgGeustUser_PageIndexChanging"
                                   
                                     BorderColor="#0918A7" PageSize="20">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="نام" />
                                        <asp:BoundField DataField="Family" HeaderText="نام خانوادگی" />
                                        <asp:BoundField DataField="MelliCode" HeaderText="کد ملی"></asp:BoundField>
                                        <asp:BoundField DataField="MobileNumber" HeaderText="شماره همراه" />
                                        <asp:BoundField DataField="EmailAddress" HeaderText="آدرس ایمیل" />
                                        <asp:TemplateField HeaderText="تاریخ و زمان">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%#PersianDateTime.MiladiToPersian(Convert.ToDateTime( Eval("CreateDateTime"))) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="btn" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
