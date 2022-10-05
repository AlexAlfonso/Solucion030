using System.Web;
using System.Web.Optimization;

namespace _00_Mvc
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
          
            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquerymin").Include(
                      "~/Scripts/plugins/jquery/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapmin").Include(
                      "~/Scripts/plugins/bootstrap/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick").Include(
                      "~/Scripts/plugins/slick-carousel/slick.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/filterzr").Include(
                      "~/Scripts/plugins/filterzr/jquery.filterizr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/magnific").Include(
                      "~/Scripts/plugins/magnific-popup/dist/jquery.magnific-popup.js"));

            bundles.Add(new ScriptBundle("~/bundles/wow").Include(
                      "~/Scripts/plugins/wow/wow.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/script").Include(
                      "~/Scripts/js/script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/plugins/themefisher-font/style.css",
                      "~/Content/plugins/bootstrap/bootstrap.min.css",
                      "~/Content/plugins/animate-css/animate.css",
                      "~/Content/plugins/magnific-popup/dist/magnific-popup.css",
                      "~/Content/plugins/slick-carousel/slick.css",
                      "~/Content/plugins/slick-carousel/slick-theme.css",
                      "~/Content/css/style.css"));

        }
    }
}
