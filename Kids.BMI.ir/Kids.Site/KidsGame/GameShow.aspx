<%@ Page Title="بازی و سرگرمی" Theme="Default" Language="C#" 
    AutoEventWireup="true" CodeBehind="GameShow.aspx.cs" Inherits="Site.Kids.bmi.ir.KidsGame.GameShow" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
    <head  id="Head2" >
             <title>سرزمین آرزوها</title>
    <link rel="SHORTCUT ICON" href="<%=ResolveUrl("~/App_Themes/Default/images/Web/bmi.ico") %>" />
    <script type="text/javascript" src="<%=ResolveUrl("~/Scripts/AC_RunActiveContent.js") %>"></script>
</head>
<head  id="Head1" runat=server >

   
</head>
<body>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center" style="text-align: center">
        <tr runat="server" id="pnlGame" visible="false">
            <td bgcolor="Transparent">
               
                
                <div id="divBanner" align="center">
                    <asp:Literal ID="lblSWF" runat="server"></asp:Literal>
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
