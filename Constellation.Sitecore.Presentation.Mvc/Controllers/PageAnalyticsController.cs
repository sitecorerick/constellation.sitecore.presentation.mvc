namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	/// <summary>
	/// Renders analytics & tracking javascript stored in the CMS. Supports inheriting scripts from parent
	/// pages. Don't use this to store all inline javascript, just 3rd party analytics kits, such as 
	/// Google Analytics.
	/// </summary>
	/// <remarks>
	/// All methods will check for empty fields and attempt to navigate up the tree looking for a non-empty
	/// script field as long as the "inherit scripts" field is true, which should be the default.
	/// </remarks>
	public class PageAnalyticsController : ItemController<IPageAnalytics, ItemRepository<IPageAnalytics>>
	{
		/// <summary>
		/// Renders scripts that should appear in the HTML head tag.
		/// Supports inheriting scripts from ancestor pages.
		/// </summary>
		/// <returns>The stored scripts, exactly as they appear in the CMS.</returns>
		public ActionResult RenderHeaderScripts()
		{
			var script = string.Empty;
			var page = DatasourceItem;

			while (string.IsNullOrEmpty(script) && page != null && page.InheritScripts.Checked)
			{
				script = page.PageHeaderScript.Value;

				page = page.InnerItem.Parent.As<IPageAnalytics>();
			}

			return Content(script);
		}

		/// <summary>
		/// Renders scripts that should appear immediately after the HTML body tag.
		/// Supports inheriting scripts from ancestor pages.
		/// </summary>
		/// <returns>The stored scripts, exactly as they appear in the CMS.</returns>
		public ActionResult RenderBodyStartScripts()
		{
			var script = string.Empty;
			var page = DatasourceItem;

			while (string.IsNullOrEmpty(script) && page != null && page.InheritScripts.Checked)
			{
				script = page.BodyStartScript.Value;

				page = page.InnerItem.Parent.As<IPageAnalytics>();
			}

			return Content(script);
		}

		/// <summary>
		/// Renders scripts that should appear immediately before the closing HTML body tag.
		/// Supports inheriting scripts from ancestor pages.
		/// </summary>
		/// <returns>The stored scripts, exactly as they appear in the CMS.</returns>
		public ActionResult RenderBodyEndScripts()
		{
			var script = string.Empty;
			var page = DatasourceItem;

			while (string.IsNullOrEmpty(script) && page != null && page.InheritScripts.Checked)
			{
				script = page.BodyEndScript.Value;

				page = page.InnerItem.Parent.As<IPageAnalytics>();
			}

			return Content(script);
		}
	}
}
