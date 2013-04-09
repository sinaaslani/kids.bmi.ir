<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master"
    AutoEventWireup="true" CodeBehind="AddEditRoles.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.SystemUserAdmin.AddEditRoles"
    Title="مدیریت کاربران" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" style="border-collapse: collapse;" dir="ltr">
        <tr>
            <td colspan="2"  align="right">
                <img runat="server" id="img1" src="~/App_Themes/Admin/Images/admin_bullet.gif" />
                <asp:Label ID="HeaderLbl" runat="server" Css></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 13px" align="right" height="13" >
                براي نام نقش تنها از حروف انگليسي استفاده نماييد.
            </td>
            <td style="height: 13px" align="right" height="13">
            </td>
        </tr>
        <tr>
            <td height="28" width="90%" align="right" dir="ltr">
                <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator4" CssClass="validationWarningSmall"
                    runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="roleNameTxt">الزامي است</asp:RequiredFieldValidator>
                <asp:TextBox ID="roleNameTxt" runat="server" Width="232px"></asp:TextBox>
            </td>
            <td  align="right" width="100">
                نام&nbsp;نقش مسووليتي&nbsp;:
            </td>
        </tr>
        <tr>
            <td align="right" height="31" dir="ltr">
                &nbsp;<asp:TextBox ID="descriptionTxt" runat="server" Width="231px" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td align="right" height="31" >
                توضيحات&nbsp;:
            </td>
        </tr>
        <tr>
            <td height="64"  align="right" style="height: 64px">
                &nbsp;
                <asp:Button ID="createEditBtn" runat="server"  OnClick="CreateEditBtn_Click">
                </asp:Button>&nbsp;
                <asp:Button ID="cancelBtn" runat="server" Text="انصراف"  CausesValidation="False"
                    OnClick="CancelBtn_Click"></asp:Button>
            </td>
            <td height="64"  align="right" style="height: 64px">
                &nbsp;<img style="width: 135px; height: 1px" height="1" src="/App_Themes/FaAdmin/images/blankImg.gif"
                    width="135" />
            </td>
        </tr>
    </table>
</asp:Content>
