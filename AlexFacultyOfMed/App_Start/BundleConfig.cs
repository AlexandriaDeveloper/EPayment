﻿using System.Web.Optimization;

namespace AlexFacultyOfMed
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {



            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //    "~/Scripts/jquery-2.1.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //    "~/Scripts/modernizr-2.6.2.js"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //    "~/Scripts/bootstrap.rtl.min.js",
            //    "~/Scripts/respond.js"));


            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                 "~/Scripts/jquery-2.1.1.min.js",

               "~/Scripts/bootstrap.rtl.min.js",
               "~/Scripts/respond.js"));


            //bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
            //    "~/Scripts/compressedscript.js"));
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //    "~/Content/bootstrap2.rtl.min.css",
            //    "~/Content/bootstrap.flatly.min.css",

            //    "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(        
                "~/Content/MainCSS.css"));


            BundleTable.EnableOptimizations = true;
        }
    }
}