<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fm2.aspx.cs" Inherits="Site.Kids.bmi.ir.AdminCP.fm2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">GetAccBill</asp:LinkButton>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">GetAccByMellicode</asp:LinkButton>&nbsp;<asp:TextBox 
            ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">GetCustInfoByMelliCode</asp:LinkButton>&nbsp;<asp:TextBox 
            ID="TextBox5" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click">GetCustInfoByCuId</asp:LinkButton>
        <asp:TextBox 
            ID="TextBox11" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">GetLoanAccByCUID</asp:LinkButton>&nbsp;<asp:TextBox 
            ID="TextBox6" runat="server"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
&nbsp;<asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
        <br />
        <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">GetLoanBill</asp:LinkButton>&nbsp;<asp:TextBox 
            ID="TextBox7" runat="server"></asp:TextBox>
        <br />

        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
