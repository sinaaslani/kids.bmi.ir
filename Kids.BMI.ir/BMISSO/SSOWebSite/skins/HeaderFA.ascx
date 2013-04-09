<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderFA.ascx.cs" Inherits="SSOWebSite.skins.HeaderFA" %>
  <script src="Scripts/AC_RunActiveContent.js" type="text/javascript"></script>

  <table border="0" style="width: 100%; height: 77px" cellspacing="0" cellpadding="0"
    dir="ltr">
    <tr>
        <td style="height: 86px; background-image: url('App_Themes/fa/Images/BannerFALine.gif');"
            valign="top" dir="rtl">
            <div id="divBanner" align="center" style="width: 100%">

                <script type="text/javascript">
                
                var FlashVersion=new String();
                FlashVersion=GetSwfVer();
                                
                var MajorVersion
                if(FlashVersion!=-1)
                {
                    if(FlashVersion.indexOf(",")>=0)
                     MajorVersion=FlashVersion.split(" ")[1].split(",")[0];
                    else if(FlashVersion.indexOf(".")>=0)
                     MajorVersion=FlashVersion.split(".")[0];
                }
                                
                if(FlashVersion==-1)
                {
                 //var msgInstallFlash = "<BR>›·‘ »— —ÊÌ œ” ê«Â ‘„« ‰’» ‰„Ì»«‘œ.<a style='text-decoration:none' href='http://http://get.adobe.com/flashplayer/'>œ—Ì«›  ¬Œ—Ì‰ ‰”ŒÂ «“ ”«Ì  adobe</a>";
                 //msgInstallFlash += "<br><a href='http://www.bmi.ir/Fa/uploadedFiles/ArchiveFiles/2009_2_17/install_flash_player_10_active_x__a198127a53.zip'>œ—Ì«›  ‰”ŒÂ 10 ÊÌéÂ IE</a>&nbsp;&nbsp;|&nbsp;&nbsp;<a href='http://www.bmi.ir/Fa/uploadedFiles/ArchiveFiles/2009_2_17/install_flash_player_10__92b4456401.zip'>œ—Ì«›  ‰”ŒÂ 10 ÊÌéÂ Firefox</a>";
                 //document.getElementById('divBanner').innerHTML = msgInstallFlash;
                }
                else if(MajorVersion>=9)
                {
                 AC_FL_RunContent('codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','100%','height','86','src','App_Themes/fa/Images/bmi.swf','quality','high','wmode','transparent','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','App_Themes/fa/Images/bmi.swf','divBanner','divBanner' ,'menu','false'); //end AC code
                }
                else
                {
                    AC_FL_RunContent('codebase','http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0','width','100%','height','86','src','App_Themes/fa/Images/bmi.swf','quality','high','wmode','transparent','pluginspage','http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash','movie','App_Themes/fa/Images/bmi.swf','divBanner','divBanner' ,'menu','false'); //end AC code
                    //var UpdateLink="‰”ŒÂ ›·‘ ‘„« ﬁœÌ„Ì „Ì»«‘œ.<a style='text-decoration:none' href='http://www.macromedia.com/go/getflashplayer'>·ÿ›« «“ «Ì‰ „”Ì— ¬Œ—Ì‰ ‰”ŒÂ —« œ—Ì«›  ‰„«ÌÌœ</a>";
                    //document.getElementById('divBanner').innerHTML=document.getElementById('divBanner').innerHTML+'<BR><BR>'+UpdateLink+'<BR><BR>';
                }
                </script>

                <noscript>
                    <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0"
                        width="120" height="240">
                        <param name="movie" value="" />
                        <param name="quality" value="high" />
                        <embed src="" quality="high" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash"
                            type="application/x-shockwave-flash" width="120" height="240"></embed>
                    </object>
                </noscript>
            </div>
        </td>
    </tr>
</table>
