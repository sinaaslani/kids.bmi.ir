<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PaymentFactorWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Payment.PaymentFactorWidget" %>

<fieldset>
    <legend>شعبه افتتاح حساب</legend>
    <table style="width: 100%;">
        <tr>
            <td align="left" width="120">
                کد شعبه :
            </td>
            <td align="right">
                
                <asp:TextBox ID="txtChildBranchCode"  runat="server" OnTextChanged="txtChildBranchCode_TextChanged"
                    AutoPostBack="True" onkeyup="return OnKeyup(this);"></asp:TextBox>
                <asp:HyperLink ID="lnkSearchBMIUnits" runat="server" Target="_blank" NavigateUrl="http://units.bmi.ir/">جستجوی شعبه</asp:HyperLink>
                &nbsp;&nbsp;
                <asp:Label ID="lblUnitInfo" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
    </table>
</fieldset>
<fieldset>
    <legend>پیش فاکتور پرداخت</legend>
    <table style="width: 80%;">
        <tr>
            <td align="left" width="120">
                هزینه تمبر&nbsp; :
            </td>
            <td align="right">
                10،000 ریال
            </td>
        </tr>
        <tr>
            <td align="left">
                هزینه صدور کارت&nbsp; :
            </td>
            <td align="right">
                30،000 ریال
            </td>
        </tr>
        <tr>
            <td align="left">
                مبلغ افتتاح حساب&nbsp; :
            </td>
            <td align="right">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtChildBranchCode"
                    ErrorMessage="!" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtChildBranchCode"
                    Display="Dynamic" ErrorMessage="مقدار نامعتبر است" ForeColor="Red" Operator="DataTypeCheck"
                    SetFocusOnError="True" Type="Integer"></asp:CompareValidator>
                <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                (حداقل 100،000 ریال)
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:Button ID="btnPayment" runat="server" OnClick="btnPayment_Click" Text="پرداخت" />
            </td>
        </tr>
    </table>
</fieldset>
