namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using global::Sitecore.Diagnostics;
	using global::Sitecore.Mvc.Presentation;
	using System;
	using System.Web;
	using System.Web.Mvc;
	using WebMarkupMin.Core.Minifiers;
	using WebMarkupMin.Core.Settings;

	/// <summary>
	/// Base class for controllers that manage the rendering of critical
	/// styles and scripts to the page.
	/// </summary>
	public abstract class CriticalAssetController : Controller
	{
		private const string Key = "criticalAssetsLoaded";

		private CriticalAssetParameters parameters = null;

		protected CriticalAssetParameters Parameters
		{
			get
			{
				if (parameters == null)
				{
					parameters = new CriticalAssetParameters(RenderingContext.Current.Rendering.Parameters);
				}

				return parameters;
			}
		}

		public ActionResult Render()
		{
			if (IsFirstRequest() && IsNormalMode())
			{
				return RenderFirstRequest();
			}

			return RenderNormal();
		}

		/// <summary>
		/// Specify the Action to run on first request.
		/// </summary>
		/// <returns>The ActionResult</returns>
		protected abstract ActionResult RenderFirstRequest();

		/// <summary>
		/// Specify the action to run when the session has already made a request.
		/// </summary>
		/// <returns>The ActionResult</returns>
		protected abstract ActionResult RenderNormal();

		protected string Minify(string content)
		{
			var settings = new HtmlMinificationSettings();
			var cssMinifier = new KristensenCssMinifier();
			var jsMinifier = new CrockfordJsMinifier();
			var minifier = new HtmlMinifier(settings, cssMinifier, jsMinifier);

			MarkupMinificationResult result = minifier.Minify(content);

			if (result.Errors.Count != 0)
			{
				Log.Warn("Attempt to minify content failed", this);
				return content;
			}

			return result.MinifiedContent;
		}

		/// <summary>
		/// Checks the current HttpContext and the Cookie to determine if the current
		/// request is the first request of the session.
		/// </summary>
		/// <returns></returns>
		private bool IsFirstRequest()
		{
			var requestStatus = HttpContext.Items[Key];

			if (requestStatus != null)
			{
				// We've already checked the cookie on this request. Here's the result:
				return (bool)requestStatus;
			}

			var cookie = Request.Cookies[Key] != null;

			if (cookie)
			{
				// We have a cookie, this is not the first request.
				HttpContext.Items.Add(Key, false); // not the first request.
				return false; // not the first request.
			}

			// First request. Add the cookie to the response
			var newCookie = new HttpCookie(Key, "1") { Expires = DateTime.Now.AddMinutes(30) };
			Response.Cookies.Add(newCookie);
			HttpContext.Items.Add(Key, true); // this is the first request
			return true; // this is the first request
		}

		/// <summary>
		/// Verifies we're not in the Sitecore Editor.
		/// </summary>
		/// <returns></returns>
		private bool IsNormalMode()
		{
			return global::Sitecore.Context.PageMode.IsNormal;
		}
	}
}
