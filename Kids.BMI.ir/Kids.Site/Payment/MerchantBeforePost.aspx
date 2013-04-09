<%@ Page Language="C#" AutoEventWireup="true" Inherits="Site.Kids.bmi.ir.Payment.MerchantBeforePost"
    CodeBehind="MerchantBeforePost.aspx.cs" %>
<%@ Import Namespace="Site.Kids.bmi.ir.Classes" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> در حال ارسال جهت پرداخت</title>
</head>
<body >
    <%=SessionItems.FormBody%>
    <script type="text/javascript">
        document.getElementById('paymentUTLfrm').submit();
    </script>
</body>
</html>
