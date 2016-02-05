namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	public abstract class RenderingController<TDatasource> : ItemController<TDatasource, ItemRepository<TDatasource>>
		where TDatasource : class, IStandardTemplate
	{
		public ActionResult RenderWithMandatoryDatasource()
		{
			if (DatasourceItem == null)
			{
				if (global::Sitecore.Context.PageMode.IsExperienceEditorEditing)
				{
					return Content("<div class=\"constellation no-datasource\">No Datasource set</div>");
				}

				return new EmptyResult();
			}

			return Render();
		}

		protected abstract ActionResult Render();
	}
}