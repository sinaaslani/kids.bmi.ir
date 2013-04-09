<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>JQuery Photo Slideshow Example</title>
    <link rel="stylesheet" type="text/css" href="styles/prettyPhoto.css"  charset="utf-8" media="screen" />        
    <!-- Arquivos utilizados pelo jQuery lightBox plugin -->
    <script type="text/javascript" src="javascripts/jquery-1.3.2.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="javascripts/jquery-1.4.4.min.js" charset="utf-8"></script>
    <script type="text/javascript" src="javascripts/jquery.prettyPhoto.js" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h3>Using IMG HTML Control</h3>
    <a href="images/fullscreen/1.jpg" rel="prettyPhoto[pp_gal]" title="You can add caption to pictures."><img src="images/thumbnails/t_1.jpg" width="60" height="60" alt="Red round shape" /></a>
    <a href="images/fullscreen/2.jpg" rel="prettyPhoto[pp_gal]"><img src="images/thumbnails/t_2.jpg" width="60" height="60" alt="Nice building" /></a>
    <a href="images/fullscreen/3.jpg" rel="prettyPhoto[pp_gal]"><img src="images/thumbnails/t_3.jpg" width="60" height="60" alt="Fire!" /></a>
    <a href="images/fullscreen/4.jpg" rel="prettyPhoto[pp_gal]"><img src="images/thumbnails/t_4.jpg" width="60" height="60" alt="Rock climbing" /></a>
    <a href="images/fullscreen/5.jpg" rel="prettyPhoto[pp_gal]"><img src="images/thumbnails/t_5.jpg" width="60" height="60" alt="Fly kite, fly!" /></a>
    </div>    
    <div>
    <h3>Using DataList</h3>
    <asp:DataList ID="galleryDataList" RepeatColumns="5" runat="server"> 
      <ItemTemplate> 
       <a href='<%# Eval("Name","images/fullscreen/{0}")%>' rel="prettyPhoto[pp_gal]" title="You can add caption to pictures.">                                
        <img src='<%# Eval("Name","images/fullscreen/{0}")%>' width="60" height="60" alt='<%# Eval("Name") %>' /> 
       </a> 
      </ItemTemplate> 
    </asp:DataList> 
    </div>
    
    </form>
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function() {
            $("a[rel^='prettyPhoto']").prettyPhoto({ theme: 'facebook', slideshow: 5000, autoplay_slideshow: true });
        });
	</script>
</body>
</html>
