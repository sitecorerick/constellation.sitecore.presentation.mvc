﻿namespace Constellation.Sitecore.Presentation.Mvc.Repositories
{
	using Constellation.Sitecore.Items;
	using global::Sitecore.Data;
	using System.Collections.Generic;

	public interface IItemRepository<out TDatasource>
		where TDatasource : class, IStandardTemplate
	{
		TDatasource DatasourceItem { get; }

		IStandardTemplate ContextItem { get; }

		TItem GetItem<TItem>(ID id) where TItem : class, IStandardTemplate;

		IEnumerable<TItem> GetChildren<TItem>(IStandardTemplate item) where TItem : class, IStandardTemplate;

		bool IsAncestor(IStandardTemplate ancestor, IStandardTemplate descendant);
	}
}
