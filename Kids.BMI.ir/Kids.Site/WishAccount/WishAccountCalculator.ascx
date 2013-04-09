<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WishAccountCalculator.ascx.cs"
    Inherits="Site.Kids.bmi.ir.WishAccount.WishAccountCalculatorascx" %>
<%@ Register Src="WishWidget.ascx" TagName="WishWidget" TagPrefix="uc1" %>

<table style="width: 100%;" runat="server" visible="false" id="pnlMain" 
    dir="ltr" >
    <tr>
        <td colspan="2" align="right">
            <uc1:WishWidget ID="WishWidget1" runat="server" />
        </td>
    </tr>
    <tr>
        <td align="right">
              ریال
            &nbsp;
         <asp:Label ID="lblCurrnrRemain" runat="server"></asp:Label> 
         
        </td>
        <td align="right" width="100">
            : موجودی فعلی
        </td>
    </tr>
    <tr>
        <td align="right">
          
          
                 &nbsp;
            ریال
            &nbsp;
             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ErrorMessage="اجباری" ControlToValidate="txtPeymentPerMonth" 
                Display="Dynamic" ForeColor="Red" SetFocusOnError="True" 
                ValidationGroup="btnCalculate"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtPeymentPerMonth" runat="server"></asp:TextBox>
        </td>
        <td align="right">
            : مقدار واریز ماهیانه
        </td>
    </tr>
    <tr>
        <td align="right" class="style1" colspan="2">
        </td>
    </tr>
    <tr>
        <td colspan="2" align="center">
            <asp:Button ID="btnCalculateWishDuration" runat="server" OnClick="btnCalculateWishDuration_Click"
                Text="محاسبه مدت زمان لازم جهت دستیابی به آرزو" 
                ValidationGroup="btnCalculate" />
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right">
            <asp:Label ID="lblResult" runat="server"></asp:Label>
        </td>
    </tr>
</table>
