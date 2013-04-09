<%@ Page Language="C#" AutoEventWireup="true" Inherits="Site.Kids.bmi.ir.JpegImage" CodeBehind="JpegImage.aspx.cs" %>

<%@ OutputCache Location="None" NoStore="true" %>
<%
    Response.Buffer = true;
    Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
    Response.Expires = 0;
    Response.CacheControl = "no-cache";
    Response.Cache.SetCacheability(HttpCacheability.NoCache);
    Response.Cache.SetExpires(DateTime.Now); 
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Content-Language" content="UTF-8" />
    <title>Image Generator</title>

    <script type="text/javascript" language="JavaScript">
<!--
         window.history.forward(1);
//-->
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    </form>
</body>
</html>
