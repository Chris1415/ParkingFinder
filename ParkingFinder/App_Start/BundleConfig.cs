﻿using System.Web;
using System.Web.Optimization;

namespace ParkingFinder
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.matchHeight-min.js",
                        "~/Scripts/bootstrap-datepicker.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/BaseScripts").Include(
                        "~/Scripts/Custom/Custom.js",
                        "~/Scripts/Custom/Cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/MapScripts").Include(
                        "~/Scripts/Custom/Map.js"));

            bundles.Add(new ScriptBundle("~/bundles/Analytics").Include(
                       "~/Scripts/Custom/Analytics.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/Custom/ParkingFinder.css",
                      "~/Content/Custom/CustomMap.css",
                      "~/Content/bootstrap-datepicker.min.css"));
        }
    }
}
