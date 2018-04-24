using System.Web;
using System.Web.Optimization;

namespace ProjektIO
{
    public class BundleConfig
    {
        // Aby uzyskać więcej informacji o grupowaniu, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/js").Include(
                     "~/Scripts/jquery.js",
                     "~/Scripts/popper.js",
                     "~/Scripts/boostrap.js"));
        }
    }
}
