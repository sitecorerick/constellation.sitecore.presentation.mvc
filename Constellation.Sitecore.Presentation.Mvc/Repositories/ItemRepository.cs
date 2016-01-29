namespace Constellation.Sitecore.Presentation.Mvc.Repositories
{
	using Constellation.Sitecore.Items;

	using global::Sitecore.Data;
	using global::Sitecore.Mvc.Presentation;

	using System.Collections.Generic;

	public class ItemRepository<TDatasource> : IItemRepository<TDatasource>
		where TDatasource : class, IStandardTemplate
	{


		public TDatasource DatasourceItem
		{
			get
			{
				return RenderingContext.Current.Rendering.Item.As<TDatasource>();
			}
		}

		public IStandardTemplate ContextItem
		{
			get
			{
				return RenderingContext.Current.PageContext.Item.AsStronglyTyped();
			}
		}

		public TItem GetItem<TItem>(ID id) where TItem : class, IStandardTemplate
		{
			return RenderingContext.Current.PageContext.Database.GetItem(id).As<TItem>();
		}

		public IEnumerable<TItem> GetChildren<TItem>(IStandardTemplate item) where TItem : class, IStandardTemplate
		{
			return item.InnerItem.GetChildren().As<TItem>();
		}

		public bool IsAncestor(IStandardTemplate ancestor, IStandardTemplate descendant)
		{
			return ancestor.InnerItem.Axes.IsAncestorOf(descendant.InnerItem);
		}
	}
}
