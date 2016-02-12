namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Linking;
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using global::Sitecore.Mvc.Presentation;
	using System.Web.Mvc;

	/// <summary>
	/// Base controller class that attempts to abstract out Sitecore context features into
	/// a repository, supplied via injection.
	/// </summary>
	/// <typeparam name="TDatasource">The expected type of the rendering's datasource.</typeparam>
	/// <typeparam name="TRepository">The type of repository required by the controller.</typeparam>
	public abstract class ItemController<TDatasource, TRepository> : Controller
		where TDatasource : class, IStandardTemplate
		where TRepository : class, IItemRepository<TDatasource>, new()
	{
		/// <summary>
		/// The repository for accessing Sitecore objects. 
		/// </summary>
		public TRepository Repository { get; private set; }

		/// <summary>
		/// The Datasource of the rendering, cast to the desired type. If the cast failed, 
		/// this value will be null.
		/// </summary>
		public TDatasource DatasourceItem
		{
			get
			{
				return Repository.DatasourceItem;
			}
		}

		public ILinkResolver LinkResolver { get; private set; }

		/// <summary>
		/// The Context item of the request. 
		/// </summary>
		public IPage ContextItem
		{
			get
			{
				return Repository.ContextItem;
			}
		}

		/// <summary>
		/// Creates a new instance of ItemController. Will eventually be replaced by the injection constructor.
		/// </summary>
		protected ItemController()
		{
			Repository = new TRepository();
			LinkResolver = new DefaultLinkResolver();
		}

		/// <summary>
		/// Creates a new instance of ItemController
		/// </summary>
		/// <param name="repository">An instance of the repository needed by the controller.</param>
		/// <param name="linkResolver">An instance of LinkResolver</param>
		protected ItemController(TRepository repository, ILinkResolver linkResolver)
		{
			Repository = repository;
			LinkResolver = linkResolver;
		}

		#region Helper Methods

		protected string ResolveViewPath()
		{
			var path = RenderingContext.Current.Rendering.RenderingItem.InnerItem.Paths.FullPath;
			var modified = path.ToLower().Replace("/sitecore/layout", string.Empty).Replace(" ", string.Empty);
			var viewLocation = "~/Views" + modified + ".cshtml";

			return viewLocation;
		}
		#endregion
	}
}