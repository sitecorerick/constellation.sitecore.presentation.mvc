namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using global::Sitecore.Mvc.Presentation;
	using System.Web.Mvc;

	public class MagicController : Controller
	{
		public ActionResult Index()
		{
			return Render();
		}

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
