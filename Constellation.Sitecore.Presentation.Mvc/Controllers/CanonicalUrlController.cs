namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using global::Sitecore.Links;
	using System;
	using System.Web.Mvc;

	/// <summary>
	/// Renders the rel=canonical link tag based upon the current page, request protocol, and the Sitecore Site's definition.
	/// </summary>
	/// <remarks>
	/// Renders an absolute URL that uses the same protocol used to request the context item (http/https). Includes all the
	/// default LinkManager options for the site, (so it supports custom LinkProviders) but it does force "AlwaysIncludeServerUrl"
	/// to provide the absolute URL required by Google or whoever else wants to use the canonical tag.
	/// </remarks>
	public class CanonicalUrlController : RenderingController<IPage>
	{
		/// <summary>
		/// Renders the link rel=canonical tag.
		/// </summary>
		/// <returns>The complete HTML link tag.</returns>
		protected override ActionResult DoRender()
		{
			var options = LinkManager.GetDefaultUrlOptions();
			options.AlwaysIncludeServerUrl = true;

			var basicUrl = LinkManager.GetItemUrl(DatasourceItem.InnerItem, options);

			var builder = new UriBuilder(basicUrl);

			if (builder.Port == 443 || builder.Port == 80)
			{
				builder.Port = -1; // removes port number from obvious URLs.
			}

			var canonicalUrl = builder.Uri.AbsoluteUri;

			return Content("<link href=\"" + canonicalUrl + "\" rel=\"canonical\" />");
		}
	}
}
