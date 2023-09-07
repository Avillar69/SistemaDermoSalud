using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SistemaDermoSalud.View.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/basics").Include(
                     "~/app-assets/vendors/js/extensions/jquery.knob.min.js",
                     "~/app-assets/vendors/js/charts/raphael-min.js",
                     "~/app-assets/vendors/js/charts/morris.min.js",
                     "~/app-assets/vendors/js/charts/jquery.sparkline.min.js",
                     "~/app-assets/js/core/app-menu.js",
                     "~/app-assets/js/core/app.js",
                     "~/app-assets/js/clusterize.min.js",
                     "~/app-assets/vendors/js/extensions/fullcalendar.min.js",
                     "~/app-assets/vendors/js/extensions/locale-all.js",
                     "~/app-assets/swal/sweetalert.min.js",
                     "~/app-assets/swal/jquery.sweet-alert.custom.js",
                     "~/app-assets/js/bootstrap-colorpicker.min.js",
                     "~/app-assets/js/jspdf.min.js",
                     "~/app-assets/js/jspdf.plugin.autotable.min.js",
                     "~/app-assets/js/scripts/extensions/dropzone.js",
                     "~/app-assets/js/scripts/jquery.timers.min.js",
                     "~/Scripts/app.js",
                     "~/Scripts/vst.js"));
        }
    }
}