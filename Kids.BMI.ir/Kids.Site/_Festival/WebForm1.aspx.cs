using System;
using System.Linq;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;
using Site.Kids.bmi.ir.Classes;

namespace Site.Kids.bmi.ir
{
    public partial class WebForm1 : KidsSecureFormBaseClass
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageMaster.ScriptManager.EnablePageMethods = true;
        }
        [WebMethod]
        [ScriptMethod]
        public static Slide[] GetSlides()
        {

            Slide[] imgSlide = new Slide[4];

            imgSlide[0] = new Slide("App_Themes/Default/images/BirthDay/ballon.png", "Autumn", "Autumn Leaves");
            imgSlide[1] = new Slide("App_Themes/Default/images/BirthDay/cake.png", "Creek", "Creek");
            imgSlide[2] = new Slide("App_Themes/Default/images/BirthDay/favorites.png", "Landscape", "Landscape");
            imgSlide[3] = new Slide("App_Themes/Default/images/BirthDay/Dock.jpg", "Dock", "Dock");

            return (imgSlide);
        }

        [WebMethod, ScriptMethod]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            // Create array of movies  
            string[] movies = { "Star Wars", "Star Trek", "Superman", "Memento", "Shrek", "Shrek II" };

            // Return matching movies  
            return (from m in movies where m.StartsWith(prefixText, StringComparison.CurrentCultureIgnoreCase) select m).Take(count).ToArray();
        }  

        protected override void CheckKidsUser()
        {

        }
    }
}