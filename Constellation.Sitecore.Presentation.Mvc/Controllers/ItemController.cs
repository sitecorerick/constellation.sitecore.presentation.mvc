namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	public abstract class ItemController<TDatasource, TRepository> : Controller
		where TDatasource : class, IStandardTemplate
		where TRepository : class, IItemRepository<TDatasource>, new()
	{
		public TRepository Repository { get; private set; }

		public TDatasource DatasourceItem
		{
			get
			{
				return Repository.DatasourceItem;
			}
		}

		public IPage ContextItem
		{
			get
			{
				return Repository.ContextItem;
			}
		}

		protected ItemController()
		{
			Repository = new TRepository();
		}

		protected ItemController(TRepository repository)
		{
			Repository = repository;
		}
	}
}