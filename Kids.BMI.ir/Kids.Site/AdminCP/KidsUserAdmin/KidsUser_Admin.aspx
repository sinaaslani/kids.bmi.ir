<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="True"
    Inherits="Site.Kids.bmi.ir.AdminCP.KidsUserAdmin.KidsUser_Admin" CodeBehind="KidsUser_Admin.aspx.cs"
    Theme="Admin" %>

<%@ Register Src="~/Registration/Editable_UserProfileWidget.ascx" TagName="Editable_UserProfileWidget"
    TagPrefix="uc1" %>
<%@ Register Src="~/Scores/ScoreList.ascx" TagName="ScoreList" TagPrefix="uc2" %>
<%@ Register Src="~/Payment/PaymentList.ascx" TagName="PaymentList" TagPrefix="uc3" %>
<%@ Register src="../../Registration/UserProfileWidget.ascx" tagname="UserProfileWidget" tagprefix="uc4" %>
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
                                            نام کاربری :
                                        </td>
                                        <td style="text-align: left; width: 123px;">
                                            <asp:TextBox ID="txtSearchSSOUserName" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            کد ملی کودک :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchChildMelliCode" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            کد ملی سرپرست :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchParentMelliCode" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            نام :
                                        </td>
                                        <td style="text-align: left; width: 123px;">
                                            <asp:TextBox ID="txtSearchChildName" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: left">
                                            نام خانوادگی :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:TextBox ID="txtSearchChildFamily" runat="server" CssClass="Textbox"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            وضعیت :
                                        </td>
                                        <td style="text-align: right">
                                            <asp:DropDownList ID="drpUserState" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="text-align: center">
                            <asp:Button ID="btnNewKidsUser" runat="server" Text="کاربر جدید" OnClick="btnNewKidsUser_Click"
                                CssClass="btn" Width="80px" CausesValidation="False" />&nbsp;<asp:Button ID="btnSearch"
                                    runat="server" OnClick="btnSearch_Click" Text="جستجو  و مشاهده  " CssClass="btn"
                                    Width="130px" ValidationGroup="View" CausesValidation="False" />
                        &nbsp;
                            <asp:ImageButton ID="ImgExportToExcell" runat="server" 
                                ImageUrl="~/App_Themes/Default/images/exel.PNG" 
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
                                <asp:GridView ID="dgKidsUser" runat="server" AutoGenerateColumns="False" BorderWidth="2px"
                                    CellPadding="4" ForeColor="#333333" OnRowDeleting="dgKidsUser_RowDeleting" Width="800px"
                                    AllowPaging="True" OnPageIndexChanging="dgKidsUser_PageIndexChanging" OnSelectedIndexChanging="dgKidsUser_SelectedIndexChanging"
                                    DataKeyNames="KidsUserId" OnRowDataBound="dgKidsUser_RowDataBound" BorderColor="#0918A7"
                                    PageSize="20">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="KidsUserId" HeaderText="کد عضویت" />
                                        <asp:TemplateField HeaderText="نام">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ChildName") + " " +Eval("ChildFamily") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="وضعیت">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentState" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ChildMelliCode" HeaderText="کد ملی" />
                                        <asp:BoundField DataField="ChildAccNo" HeaderText="شماره حساب"></asp:BoundField>
                                        <asp:TemplateField HeaderText="امتیاز">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCurrentScore" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField SelectText="مشاهده" ShowSelectButton="True" HeaderText="مشاهده">
                                            <HeaderStyle Width="50px" />
                                        </asp:CommandField>
                                        <asp:TemplateField ShowHeader="False" HeaderText="حذف">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CausesValidation="False" CommandName="Delete"
                                                    Text="حذف"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                        <asp:HyperLinkField Target="_blank" Text="چاپ فرم" DataNavigateUrlFields="KidsUserId"
                                            DataNavigateUrlFormatString="PrintAccForm.aspx?uid={0}"></asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerSettings Position="TopAndBottom" Mode="NumericFirstLast" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" CssClass="btn" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
                                <br />
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <fieldset id="pnlDetails" runat="server" visible="false">
                                <legend style="font-weight: bold">جزییات</legend>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <uc2:ScoreList ID="ucScoreList" runat="server" />
                                        </td>
                                    </tr>
                                    
                                     <tr>
                                        <td>
                                    <uc3:PaymentList ID="ucPaymentList" runat="server" />
                                      </td>
                                    </tr>
                                      <tr>
                                        <td>
                                    <uc1:Editable_UserProfileWidget ID="ucEditableUserProfile" runat="server" 
                                                BasicInfoIsEditable="true" Visible="False" />
                                     </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <uc4:UserProfileWidget ID="ucUserProfile" runat="server"  Visible="False" />
                                     </td>
                                    </tr>
                                     <tr>
                                        <td>
                                    <asp:Button ID="btnSave" runat="server" Text="ذخیره" OnClick="btnSave_Click" />
                                    &nbsp; &nbsp;
                                    <asp:Button ID="btnCancel" runat="server" Text="لغو" OnClick="btnCancelPlan_Click" />
                                     </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
