<%@ Page Language="C#" Theme="Admin" MasterPageFile="~/Masters/Admin.Master" AutoEventWireup="true"
    CodeBehind="AddEditUser.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.SystemUserAdmin.AddEditUser"
    Title="مدیریت کاربران" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table style="width: 100%" dir="ltr">
        <tr>
            <td align="right" colspan="2">
                <img id="img1" src="~/App_Themes/Admin/Images/admin_bullet.gif" runat="server" alt="" />&nbsp;
                <asp:Label ID="Label1" runat="server" Css> پروفايل کاربر</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
            </td>
            <td valign="top" align="right" width="200">
                نام</td>
        </tr>
        <tr>
            <td align="right">
                
                    <asp:TextBox ID="txtLName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td align="right">
                نام خانوادگي
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:TextBox ID="txtUserName" runat="server" Width="200px"></asp:TextBox>
            </td>
            <td height="15" align="right">
                نام کاربري
            </td>
        </tr>
        <tr>
            <td align="right">
            </td>
            <td align="right">
            </td>
        </tr>
        <tr>
            <td align="right" height="32">
                <asp:CheckBox ID="ApprovedChkBox" runat="server"></asp:CheckBox>
            </td>
            <td align="right">
                فعال بودن 
            </td>
        </tr>
        <tr>
            <td  align="right" valign="top">
                <asp:CheckBoxList style="direction:rtl" ID="chkBoxObjectRoles" runat="server" Width="50%" DataValueField="RoleName"
                    DataTextField="Description" RepeatColumns="2" TextAlign="Right" 
                    RepeatDirection="Horizontal">
                </asp:CheckBoxList>
            </td>
            <td valign="top" align="right">
                نقشها و مسووليتهاي کاربر
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="SaveBtn" runat="server"  Font-Names="Tahoma"
                    Text="ذخيره و بهنگام " OnClick="SaveBtn_Click"></asp:Button>&nbsp;
                <asp:Button ID="cancelBtn" runat="server"  Font-Names="Tahoma"
                    Text="  انصراف  " CausesValidation="False" OnClick="CancelBtn_Click"></asp:Button><br>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblError" runat="server" ForeColor="#FF0066"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
