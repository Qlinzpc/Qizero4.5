using System.Web;
using System.Web.Optimization;

namespace Qz.GPS.Web.App_Start
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region JS
            bundles.Add(new ScriptBundle("~/Qz/js").Include(
                "~/Module/QzUI/base/js/base.js",
                "~/Module/QzUI/common/js/common.js",
                "~/Module/QzUI/lib/Store/js/store.js",
                "~/Module/QzUI/lib/Layout/js/layout.js",
                "~/Module/QzUI/lib/Box/js/box.js",
                "~/Module/QzUI/lib/Menu/js/menu.js",
                "~/Module/QzUI/lib/Tab/js/tab.js",
                "~/Module/QzUI/lib/Ajax/js/ajax.js",
                "~/Module/QzUI/lib/Dialog/js/dialog.js",
                "~/Module/QzUI/lib/Tree/js/tree.js",
                "~/Module/QzUI/lib/Form/js/form.js",
                "~/Module/QzUI/lib/Drag/js/drag.js"
                ));

            bundles.Add(new ScriptBundle("~/Qz/jQuery").Include("~/Scripts/jquery-1.10.2.js"));

            //bundles.Add(new ScriptBundle("~/Qz/data/home").Include("~/Scripts/data/home.js"));
            //bundles.Add(new ScriptBundle("~/Qz/common").Include("~/Scripts/common.js"));
            //bundles.Add(new ScriptBundle("~/Qz/api").Include("~/Scripts/api.js"));
            //bundles.Add(new ScriptBundle("~/Qz/ajax").Include("~/Scripts/ajax.js"));
            //bundles.Add(new ScriptBundle("~/Qz/main").
            //    Include(
            //        "~/Scripts/api.js",
            //        "~/Scripts/ajax.js",
            //        "~/Scripts/common.js"
            //    ));
            #endregion

            #region CSS
            bundles.Add(new StyleBundle("~/Qz/css").Include(
                "~/Module/QzUI/base/css/skin/default.css",
                "~/Module/QzUI/common/css/common.css",
                "~/Module/QzUI/common/css/animate.css",
                "~/Module/QzUI/common/css/fonts.css",
                "~/Module/QzUI/lib/ContextMenu/css/contextMenu.css",
                "~/Module/QzUI/lib/Layout/css/layout.css",
                "~/Module/QzUI/lib/Box/css/box.css",
                "~/Module/QzUI/lib/Menu/css/menu.css",
                "~/Module/QzUI/lib/Tab/css/tab.css",
                "~/Module/QzUI/lib/Button/css/button.css",
                "~/Module/QzUI/lib/Dialog/css/dialog.css",
                "~/Module/QzUI/lib/Tree/css/tree.css",
                "~/Module/QzUI/lib/Form/css/form.css",
                "~/Module/QzUI/lib/Table/css/table.css"
                ));

            //bundles.Add(new StyleBundle("~/Content/common").Include("~/Content/css/common.css"));
            //bundles.Add(new StyleBundle("~/Content/font").Include("~/Content/css/fonts.css"));
            //bundles.Add(new StyleBundle("~/Content/main").
            //    Include(
            //        "~/Content/css/common.css",
            //        "~/Content/css/fonts.css",
            //        "~/Content/css/animate.css"
            //    ));
            #endregion

        }
    }
}