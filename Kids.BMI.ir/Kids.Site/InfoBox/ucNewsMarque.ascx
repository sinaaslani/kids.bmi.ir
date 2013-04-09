<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="ucNewsMarque.ascx.cs"
    Inherits="Site.Kids.bmi.ir.InfoBox.ucNewsMarque"  %>
<asp:Literal ID="litMsg" runat="server"></asp:Literal>
<script language="JavaScript" type="text/javascript">
    var theSummaries = new Array(<%=Titles %>);
var theSiteLinks = new Array(<%=Links %>);

</script>
<div style="padding-right: 25px; padding-top: 10px; width: 694px; height: 28px; background-image: url(/App_Themes/Default/images/news-bar.png);
                            background-repeat: no-repeat;  background-position: center; font-family: Mj_Two;  ">
    <span id="theTicker"> <a class="siteNavigation" id="newsbar"></a>
        <script language="JavaScript" type="text/javascript" src="/Scripts/ticker_engine.js"></script>
    </span>
</div>
