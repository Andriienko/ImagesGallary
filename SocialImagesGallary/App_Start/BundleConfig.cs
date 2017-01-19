using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SocialImagesGallary
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/Style.css"));

            bundles.Add(new StyleBundle("~/Content/css/profile").Include("~/Content/profile.css"));
            bundles.Add(new StyleBundle("~/Content/css/gallery").Include("~/Content/photoGallery.css"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/knockout-3.4.1.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/scripts/home").Include("~/Scripts/app/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/profile").Include("~/Scripts/app/profile.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/gallery").Include("~/Scripts/app/gallery.js"));
        }
    }
}