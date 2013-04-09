<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Admin.Master" ValidateRequest="false"
    AutoEventWireup="True" CodeBehind="PostalCardAdmin.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.PostalCardAdmin.PostalCardAdmin"
    Theme="Admin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" dir="ltr">
        <tr>
            <td align="right" colspan="2">
                &nbsp;
                <img src="/App_Themes/Admin/Images/admin_bullet.gif">
                <asp:Literal ID="lblHead" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtPostalCardTitle" runat="server" Height="31px" Width="390px"></asp:TextBox><br>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPostalCardTitle"
                    ErrorMessage="خطا : عنوان خبر الزامی است" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td align="right">
                عنوان کارت پستال :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:DropDownList ID="drpScoreTypeId" runat="server">
                </asp:DropDownList>
            </td>
            <td valign="top" align="right" width="100">
                نوع امتیاز کارت پستال :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtCardScore" runat="server"></asp:TextBox>
            </td>
            <td valign="top" align="right" width="100">
                امتیاز استفاده از کارت
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtPostalCardDescription" runat="server" Height="57px" TextMode="MultiLine"
                    Width="390px"></asp:TextBox>
            </td>
            <td valign="top" align="right" width="100">
                توضیحات:
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="fupPostalCardPicAddress" type="file" />&nbsp;
                <asp:LinkButton ID="PostalCardPicDeleteLnk" runat="server" 
                    Visible="False" OnCommand="SmallPicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td height="31" align="right" width="100">
                تصویر کارت پستال کوچک :
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:FileUpload runat="server" ID="fupPostalCardFileAddress" type="file" name="PicAddress" />&nbsp;&nbsp;
                <asp:LinkButton ID="PostalCardFileDeleteLnk" runat="server" 
                    Visible="False" OnCommand="LargePicDeleteLnk_Command">حذف فایل</asp:LinkButton>
            </td>
            <td align="right" width="100">
                تصویر کارت پستال بزرگ :
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnCancel" runat="server"  Font-Names="Tahoma"
                    Text="  انصراف  " OnClick="btnCancel_Click"></asp:Button>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSave" runat="server"  Font-Names="Tahoma"
                    OnClick="btnSave_Click"></asp:Button>
            </td>
        </tr>
        <tr>
            <td width="100%" colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
