namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	public abstract class RenderingController<TItem> : ItemController<TItem, ItemRepository<TItem>>
		where TItem : class, IStandardTemplate
	{
		public ActionResult Render()
		{
			if (DatasourceItem == null)
			{
				if (global::Sitecore.Context.PageMode.IsExperienceEditorEditing)
				{
					return View("NoDatasource");
				}

				return new EmptyResult();
			}

			return DoRender();
		}

		protected abstract ActionResult DoRender();
	}
}