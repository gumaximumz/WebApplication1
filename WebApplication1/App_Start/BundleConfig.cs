﻿using System.Web;
using System.Web.Optimization;

namespace WebApplication1
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/jqgrid/css").Include(
                //"~/Content/jqgrid/ui.jqgrid.css",
                "~/Content/jqgrid/ui.jqgrid-bootstrap-ui.css",
                "~/Content/jqgrid/ui.jqgrid-bootstrap.css"           
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                "~/Scripts/jqgrid/jquery.jqGrid.js",
                "~/Scripts/i18n/grid.locale-th.js",
                "~/Scripts/jquery.form.js",
                "~/Scripts/helper/formHelper.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/product").Include(
                "~/Scripts/product/grid.js"
                ));

            //bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
            //    "~/Content/themes/base/base.css",
            //    "~/Content/themes/base/core.css",
            //    "~/Content/themes/base/theme.css"
            //    ));

            bundles.Add(new ScriptBundle("~/bundles/productType").Include(
                "~/Scripts/producttype/grid.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/supplier").Include(
                "~/Scripts/supplier/grid.js"
                ));

        }
    }
}
