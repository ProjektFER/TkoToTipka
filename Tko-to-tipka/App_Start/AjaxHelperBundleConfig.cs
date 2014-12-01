using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Tko_to_tipka.App_Start.AjaxHelperBundleConfig), "RegisterBundles")]

namespace Tko_to_tipka.App_Start
{
	public class AjaxHelperBundleConfig
	{
		public static void RegisterBundles()
		{
			BundleTable.Bundles.Add(new ScriptBundle("~/bundles/ajaxhelper").Include("~/Scripts/jquery.ajaxhelper.js"));
		}
	}
}