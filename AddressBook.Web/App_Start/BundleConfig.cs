using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace AddressBook.Web
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkID=303951
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/jquery-ui-{version}.js")
                .Include("~/Scripts/jquery.validate*"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryui"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                       "~/Scripts/moment/moment-with-locales.js"));

            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // Order is very important for these files to work, they have explicit dependencies
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include("~/Scripts/angular.js",
                "~/Scripts/angular-route.js",
                    "~/Scripts/angular-sanitize.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/angular-clipboard.js",
                    "~/Scripts/toastr.js",
                    "~/Scripts/angular-toastr.js",
                    "~/Scripts/angular-toastr.tpls.js",
                    "~/Scripts/ng-tags-input.js",
                    "~/Scripts/angular-dropdowns.js",
                    "~/Scripts/ng-infinite-scroll.js"
                    ).Include("~/Scripts/angular-bootstrap-datetimepicker/js/*.js"));

            bundles.Add(new ScriptBundle("~/bundles/TinyMCE").Include("~/Scripts/tinymce/tinymce.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-fileupload").Include("~/Scripts/ng-file-upload-all.min.js",
                    "~/Scripts/ng-file-upload.min.js",
                    "~/Scripts/angular-file-upload.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular-app").Include("~/app/*.js"));

            // Use the Development version of Modernizr to develop with and learn from. Then, when you’re
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            ScriptManager.ScriptResourceMapping.AddDefinition(
                "respond",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/respond.min.js",
                    DebugPath = "~/Scripts/respond.js",
                });
        }
    }
}