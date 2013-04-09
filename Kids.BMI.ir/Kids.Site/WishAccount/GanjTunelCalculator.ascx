<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GanjTunelCalculator.ascx.cs"
    Inherits="Site.Kids.bmi.ir.WishAccount.GanjTunelCalculator" %>
<table style="width: 100%;" >
    <tr>
        <td align="right" valign="middle">
            &nbsp;مبلغ پس انداز :&nbsp;
            <asp:DropDownList ID="drpDuration" runat="server">
                <asp:ListItem Selected="True" Value="0">مدت زمان پس انداز</asp:ListItem>
                <asp:ListItem Value="1">روزانه</asp:ListItem>
                <asp:ListItem Value="7">هفتگی</asp:ListItem>
                <asp:ListItem Value="30">ماهانه</asp:ListItem>
                <asp:ListItem Value="365">سالانه</asp:ListItem>
            </asp:DropDownList>
            &nbsp;*
            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
            &nbsp;<asp:ImageButton ID="btnCalculate" runat="server" AlternateText="محاسبه" OnClick="btnCalculate_Click"
                ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/images/Calculate.png" Width="32px"
                CausesValidation="False" />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="مبلغ نامعتبر است "
                ForeColor="Red" Operator="DataTypeCheck" SetFocusOnError="True" Type="Currency"
                ControlToValidate="txtAmount"></asp:CompareValidator>
            &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="drpDuration"
                ErrorMessage="!" ForeColor="Red" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td align="right">
            <asp:Label ID="lblResult" runat="server" ForeColor="Blue"></asp:Label>
        </td>
    </tr>
</table>
