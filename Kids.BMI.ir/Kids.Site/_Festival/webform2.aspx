<%@ Page Title="" Language="C#" MasterPageFile="~/Masters/Fa.Master" AutoEventWireup="True"
    CodeBehind="WebForm2.aspx.cs" Theme="Default" Inherits="Site.Kids.bmi.ir.WebForm2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   
    <asp:DataList ID="galleryDataList" RepeatColumns="5" runat="server"> 
      <ItemTemplate> 
       <a href='<%# Eval("Name","/AdminCP/Files/Festival/{0}")%>' rel="prettyPhoto[pp_gal]" title="You can add caption to pictures.">                                
        <img src='<%# Eval("Name","/AdminCP/Files/Festival/{0}")%>' width="60" height="60" alt='<%# Eval("Name") %>' /> 
       </a> 
      </ItemTemplate> 
    </asp:DataList> 
    
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {
            $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'facebook', slideshow: 5000, autoplay_slideshow: true });
        });
	</script>
</asp:Content>
