namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	public abstract class SublayoutController<TDatasource> : ItemController<TDatasource, ItemRepository<TDatasource>>
		where TDatasource : class, IStandardTemplate
	{
		public ActionResult Render()
		{
			return DoRender();
		}

		protected abstract ActionResult DoRender();
	}
}