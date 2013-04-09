<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.master" AutoEventWireup="true"
    Inherits="Site.Kids.bmi.ir.AdminCP.WishesAdmin.WishesAdmin_Admin" CodeBehind="WishesAdmin_Admin.aspx.cs"
    ValidateRequest="false" Theme="Admin" %>

<%@ Import Namespace="Kids.Common" %>
<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        function Confirm() {
            return confirm('آیا مطمئن هستید؟');
        }
    </script>
    <table width="100%">
        <tr>
            <td>
                <fieldset id="pnlSearch" runat="server">
                    <legend style="font-weight: bold">جستجو</legend>
                    <br />
                    <table style="width: 100%; background-color: #EBEBEB;">
                        <tr>
                            <td align="right" width="100">
                                عنوان آرزو :
                            </td>
                            <td style="text-align: left; width: 123px;">
                                <asp:TextBox ID="txtSearchWishName" runat="server" CssClass="Textbox"></asp:TextBox>
                            </td>
                            <td style="text-align: left">
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
                <asp:Button ID="btnNewWishe" runat="server" Text="تعریف آرزوی جدید" OnClick="btnNewPlan_Click"
                    CssClass="btn" Width="100px" CausesValidation="False" />&nbsp; &nbsp;<asp:Button
                        ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="جستجو  و مشاهده  "
                        CssClass="btn" Width="130px" ValidationGroup="View" CausesValidation="False" />
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
                    <asp:GridView ID="dgPlans" runat="server" AutoGenerateColumns="False" BorderWidth="2px"
                        CellPadding="4" ForeColor="#333333" OnRowDeleting="dgMagezinText_RowDeleting"
                        Width="100%" AllowPaging="True" OnPageIndexChanging="dgPlans_PageIndexChanging"
                        OnSelectedIndexChanging="dgPlans_SelectedIndexChanging" DataKeyNames="WishId"
                        OnRowDataBound="dgPlans_RowDataBound" BorderColor="#0918A7" PageSize="5">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="WishName" HeaderText="نام صفحه" />
                            <asp:BoundField DataField="WishAmount" HeaderText="تیتر صفحه" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# SystemConfigs.UrlWishFilesPath + Eval("WishPicSmall")%>' />
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
                    <br />
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                &nbsp;عنوان آرزو :
                            </td>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                <asp:TextBox ID="txtWishName" runat="server" CssClass="Textbox"></asp:TextBox>
                                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtWishName"
                                    ErrorMessage="اجباری" ValidationGroup="Save" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                مبلغ آرزو :
                            </td>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                <asp:TextBox ID="txtWishAmount" runat="server" CssClass="Textbox"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtWishAmount"
                                    Display="Dynamic" ErrorMessage="اجباری" SetFocusOnError="True" ValidationGroup="Save"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                تصویر کوچک :
                            </td>
                            <td align="right">
                                <asp:FileUpload runat="server" ID="fupSmallPicAddress" type="file" />&nbsp;
                                <asp:LinkButton ID="SmallPicDeleteLnk" runat="server"  Visible="False"
                                    OnCommand="SmallPicDeleteLnk_Command">حذف فایل</asp:LinkButton>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                تصویر بزرگ :
                            </td>
                            <td align="right">
                                <asp:FileUpload runat="server" ID="fupPicAddress" type="file" name="PicAddress" />&nbsp;&nbsp;
                                <asp:LinkButton ID="LargePicDeleteLnk" runat="server"  Visible="False"
                                    OnCommand="LargePicDeleteLnk_Command">حذف فایل</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                توضیحات آرزو :
                            </td>
                            <td style="text-align: right; background-color: #EBEBEB;">
                                <FTB:FreeTextBox ID="txtWishDescription" runat="Server" ImageGalleryPath="~/AdminCP/Files/"
                                    TextDirection="RightToLeft" ImageGalleryUrl="/AdminCP/ftb.imagegallery.aspx?"
                                    ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                                    Width="700px" AllowHtmlMode="True" Language="fa-IR" 
                                   DesignModeCss="/App_Themes/Admin/ftb.css" 
                                    DesignModeBodyTagCssClass="main" 
                                   
                                    
                                    ButtonOverImage="True" >
                                </FTB:FreeTextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp; &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btn" OnClick="btnSave_Click"
                                    Text="ذخیره " UseSubmitBehavior="true" ValidationGroup="Save" Width="100px" />
                                &nbsp;<asp:Button ID="btnCancelPlan" runat="server" CssClass="btn" OnClick="btnCancelPlan_Click"
                                    Text="لغو " Width="100px" CausesValidation="False" />
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
    </table>
   
</asp:Content>
