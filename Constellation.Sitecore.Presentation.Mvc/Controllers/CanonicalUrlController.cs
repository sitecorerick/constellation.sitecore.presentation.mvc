namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using global::Sitecore.Links;
	using System;
	using System.Web.Mvc;

	public class CanonicalUrlController : RenderingController<IPage>
	{
		protected override ActionResult Render()
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
