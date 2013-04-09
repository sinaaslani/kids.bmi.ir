<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="NewsAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.NewsAdmin.NewsAdmin"
    Theme="Admin" %>

<%@ Register Src="~/UserControls/ucDatePicker.ascx" TagName="ucDatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" dir="ltr">
        <tr>
            <td align="right" width="100%" colspan="2">
                &nbsp;
                <img src="/App_Themes/Admin/Images/admin_bullet.gif" alt="">
                <asp:Literal ID="lblHead" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtTitle" runat="server"  TextMode="MultiLine" Width="390px"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Css ControlToValidate="txtTitle"
                    ErrorMessage="RequiredFieldValidator">خطا : عنوان خبر الزامی است</asp:RequiredFieldValidator>
            </td>
            <td>
                عنوان خبر :
            </td>
        </tr>
        <tr>
            <td align="right">
                <table>
                    <tr>
                        <td align="right">
                            <img src="/App_Themes/Admin/Images/admin_bullet.gif">
                            موضوعات&nbsp;مورد نظر را تیک بزنید
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="NewCatsGrid" Width="390px" PageSize="20" GridLines="None" CellSpacing="1"
                                Font-Size="9pt" Font-Names="Tahoma" runat="server" AutoGenerateColumns="False"
                                CellPadding="2" DataKeyNames="NewsCategoryId">
                                <AlternatingRowStyle BackColor="#E6F1FF"></AlternatingRowStyle>
                                <RowStyle BackColor="#E6F1FF" />
                                <HeaderStyle ForeColor="White" BackColor="#0D66BA"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField HeaderText="موضوع">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%# Bind("NewsCategoryId") %>'>
                                            </asp:HiddenField>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("NewsCategoryName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" Width="100%" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderStyle Width="10px"></HeaderStyle>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkCategory" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <asp:Label ID="lblSubjectValidator" runat="server" Font-Names="Tahoma" Font-Size="X-Small"
                                Visible="False" ForeColor="Red">انتخاب موضوع الزامی می باشد</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                موضوع خبر :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="summaryCtrl" runat="server" Height="57px" TextMode="MultiLine" Width="390px"></asp:TextBox>
            </td>
            <td valign="top">
                خلاصه خبر :
            </td>
        </tr>
        <tr>
            <td align="right" width="100%">
                <FTB:FreeTextBox ID="txtHTMLNewsbody" runat="Server" ImageGalleryPath="~/AdminCP/Files/"
                    TextDirection="RightToLeft" ImageGalleryUrl="/AdminCP/ftb.imagegallery.aspx?"
                    Width="700px" AllowHtmlMode="True" Language="fa-IR" ToolbarLayout="ParagraphMenu,FontFacesMenu,FontSizesMenu,FontForeColorsMenu,FontForeColorPicker,FontBackColorsMenu,FontBackColorPicker|Bold,Italic,Underline,Strikethrough,Superscript,Subscript,RemoveFormat|JustifyLeft,JustifyRight,JustifyCenter,JustifyFull;BulletedList,NumberedList,Indent,Outdent;CreateLink,Unlink,InsertImage|Cut,Copy,Paste,Delete;Undo,Redo,Print,Save|SymbolsMenu,StylesMenu,InsertHtmlMenu|InsertRule,InsertDate,InsertTime|InsertTable,EditTable;InsertTableRowAfter,InsertTableRowBefore,DeleteTableRow;InsertTableColumnAfter,InsertTableColumnBefore,DeleteTableColumn|InsertForm,InsertTextBox,InsertTextArea,InsertRadioButton,InsertCheckBox,InsertDropDownList,InsertButton|InsertDiv,EditStyle,InsertImageFromGallery,Preview,SelectAll,WordClean,NetSpell"
                    DesignModeCss="/App_Themes/Admin/ftb.css" DesignModeBodyTagCssClass="main">
                </FTB:FreeTextBox>
            </td>
            <td valign="top">
                متن خبر :
            </td>
        </tr>
        <tr>
            <td dir="ltr" align="right">
                <uc1:ucDatePicker ID="ucNewsDateDime" runat="server" ShowTime="true" />
            </td>
            <td height="30">
                تاریخ خبر :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="SmallPicAddress" type="file" />&nbsp;
                <asp:LinkButton ID="SmallPicDeleteLnk" runat="server" Visible="False" OnCommand="SmallPicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td height="31">
                تصویر کوچک :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="PicAddress" type="file" name="PicAddress" />&nbsp;&nbsp;
                <asp:LinkButton ID="LargePicDeleteLnk" runat="server" Visible="False" OnCommand="LargePicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td>
                تصویر بزرگ :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="bodyFile" type="file" name="bodyFile" />&nbsp;&nbsp;
                <asp:LinkButton ID="BodyFileDeleteLnk" runat="server" Visible="False" OnCommand="BodyFileDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td>
                فایل متن اخبار :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="realFile" type="file" name="realFile" />&nbsp;&nbsp;
                <asp:LinkButton ID="RealFileDeleteLnk" runat="server" Visible="False" OnCommand="RealFileDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td height="19">
                فایل real player
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="mediaFile" type="file" name="mediaFile" />&nbsp;&nbsp;
                <asp:LinkButton ID="MediaFileDeleteLnk" runat="server" Visible="False" OnCommand="MediaFileDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td>
                فایل media player
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="NewsStatus" runat="server" Css></asp:Label>
            </td>
            <td>
                وضعیت خبر :
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Font-Names="Tahoma" OnClick="btnSave_Click">
                </asp:Button>&nbsp;<asp:Button ID="commitBtn" runat="server" Font-Names="Tahoma"
                    Text="ذخیره و تایید خبر" OnClick="commitBtn_Click"></asp:Button>
                <asp:Button ID="DiscardBtn" runat="server" Font-Names="Tahoma" Text=" رد خبر  " OnClick="DiscardBtn_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="btnCancel" runat="server" Font-Names="Tahoma" Text="  انصراف  " OnClick="btnCancel_Click">
                </asp:Button>
            </td>
            <td>
                <img style="width: 118px; height: 1px" height="1" src="/App_Themes/Default/images/blnk.gif"
                    width="118">
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="2" height="30">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
