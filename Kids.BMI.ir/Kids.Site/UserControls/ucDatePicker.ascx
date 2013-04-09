<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucDatePicker.ascx.cs"
    Inherits="Site.Kids.bmi.ir.UserControls.ucDatePicker" %>
<div dir="ltr">
    &nbsp;<asp:RequiredFieldValidator ID="Reqval1" 
        runat="server" ControlToValidate="drpDay"
        ErrorMessage="نامعتبر" InitialValue="-1"  Display="Dynamic"
        SetFocusOnError="True" ForeColor="Red" Width="35px"></asp:RequiredFieldValidator>
    &nbsp;<asp:RequiredFieldValidator ID="Reqval2" runat="server" ControlToValidate="drpMonth"
        ErrorMessage="نامعتبر" InitialValue="-1"  Display="Dynamic"
        SetFocusOnError="True" ForeColor="Red" Width="35px"></asp:RequiredFieldValidator>
    &nbsp;<asp:RequiredFieldValidator ID="Reqval3" runat="server" ControlToValidate="drpYear"
        ErrorMessage="نامعتبر" InitialValue="-1"  Display="Dynamic"
        SetFocusOnError="True" ForeColor="Red" Width="40px"></asp:RequiredFieldValidator>
    <br />
    <asp:DropDownList ID="drpYear" runat="server" CssClass="time" Width="65px">
      
    </asp:DropDownList>
    &nbsp;/
    <asp:DropDownList ID="drpMonth" runat="server" CssClass="time" Width="65px">
        <asp:ListItem Selected="True" Value="-1">----</asp:ListItem>
        <asp:ListItem Value="01">فروردین</asp:ListItem>
        <asp:ListItem Value="02">اردیبهشت</asp:ListItem>
        <asp:ListItem Value="03">خرداد</asp:ListItem>
        <asp:ListItem Value="04">تیر</asp:ListItem>
        <asp:ListItem Value="05">مرداد</asp:ListItem>
        <asp:ListItem Value="06">شهریور</asp:ListItem>
        <asp:ListItem Value="07">مهر</asp:ListItem>
        <asp:ListItem Value="08">آبان</asp:ListItem>
        <asp:ListItem Value="09">آذر</asp:ListItem>
        <asp:ListItem Value="10">دی</asp:ListItem>
        <asp:ListItem Value="11">بهمن</asp:ListItem>
        <asp:ListItem Value="12">اسفند</asp:ListItem>
    </asp:DropDownList>
    &nbsp;/
    <asp:DropDownList ID="drpDay" runat="server" CssClass="time" Width="40px">
    </asp:DropDownList>
    <div runat="server" id="pnlTime" visible="false">
        &nbsp; &nbsp; &nbsp; &nbsp;
        <asp:DropDownList ID="drpHour" runat="server">
           
        </asp:DropDownList>
        &nbsp; : &nbsp;
        <asp:DropDownList ID="drpMinute" runat="server">
           
        </asp:DropDownList>
    </div>
</div>
