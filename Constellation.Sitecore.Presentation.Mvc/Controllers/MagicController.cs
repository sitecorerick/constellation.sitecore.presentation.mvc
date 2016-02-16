namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using global::Sitecore.Mvc.Presentation;
	using System.Web.Mvc;

	/// <summary>
	/// A Controller designed to automatically launch a View based on the name & tree location of the Rendering Item.
	/// </summary>
	/// <remarks>
	/// This controller is designed to work with Constellation.Sitecore.Data.Items, and assumes that the resulting
	/// view implements a Model that is compatible with IStandardTemplate.
	/// 
	/// Best used on Views where the datasource is the only model value required.
	/// In order to successfully use this controller on more than one rendering on a given page (highly likely) you also
	/// need to configure the Sitecore MVC RenderRendering pipeline to utilize the custom GenerateCacheKey handler included
	/// in this library, otherwise all renderings that use this controller may return the same markup.
	/// </remarks>
	public class MagicController : Controller
	{
		/// <summary>
		/// Fallback action based on known Sitecore defaults.
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return Render();
		}

		/// <summary>
		/// Renders a view whose location on disk matches the location of the Rendering Item in Sitecore, starting from
		/// the root of their type-specific Layout folder. Views should be placed in ~/Views/[Layouts | Sublayouts | Renderings] in order
		/// for their paths to resolve correctly. Spaces in folder/file names will be omitted when converting a Sitecore path to a file
		/// path.
		/// </summary>
		/// <returns>The View, or if the datasource is null, an empty result, unless the page is open for editing.</returns>
		public virtual ActionResult Render()
		{
			var item = RenderingContext.Current.Rendering.Item.AsStronglyTyped();

			if (item == null)
			{
				if (global::Sitecore.Context.PageMode.IsExperienceEditorEditing)
				{
					return Content("<div class=\"constellation no-datasource\">Incompatible Datasource.</div>");
				}

				return new EmptyResult();
			}

			return View(ResolveViewPath(), item);
		}

		protected string ResolveViewPath()
		{
			var path = RenderingContext.Current.Rendering.RenderingItem.InnerItem.Paths.FullPath;
			var modified = path.ToLower().Replace("/sitecore/layout", string.Empty).Replace(" ", string.Empty);
			var viewLocation = "~/Views" + modified + ".cshtml";

			return viewLocation;
		}
	}
}
