<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KidsUserSateWidget.ascx.cs"
    Inherits="Site.Kids.bmi.ir.Registration.KidsUserSateWidget" %>
    
    <script type="text/javascript">
        $(function () {
             <%=TooltipScript %>

        });
</script>
<table style="width: 100%;">
    <tr>
        <td>
            <asp:PlaceHolder ID="StatePlaceHolder" runat="server"></asp:PlaceHolder>
            &nbsp;(<asp:Label ID="lblLastStatus" runat="server" Font-Size="X-Small"></asp:Label>)
        </td>
    </tr>
</table>
