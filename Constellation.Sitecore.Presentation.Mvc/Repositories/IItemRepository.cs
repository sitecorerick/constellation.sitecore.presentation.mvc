namespace Constellation.Sitecore.Presentation.Mvc.Repositories
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Models;
	using global::Sitecore.Data;
	using System.Collections.Generic;

	/// <summary>
	/// Contract that allows separation of Sitecore API calls and context classes from
	/// implementing controllers, allowing for unit testing.
	/// </summary>
	/// <typeparam name="TDatasource">The Constellation.Sitecore.Item type</typeparam>
	public interface IItemRepository<out TDatasource>
		where TDatasource : class, IStandardTemplate
	{
		/// <summary>
		/// Gets a strongly-typed item based on the Context Rendering's datasource Item.
		/// </summary>
		TDatasource DatasourceItem { get; }

		/// <summary>
		/// Gets a strongly-typed item based on the Sitecore.Context.Item
		/// </summary>
		IPage ContextItem { get; }

		/// <summary>
		/// Gets a strongly-typed Item based on the request context and the ID provided.
		/// </summary>
		/// <typeparam name="TItem">The Constellation.Sitecore.Item type</typeparam>
		/// <param name="id">The ID of the Item to return.</param>
		/// <returns>The strongly-typed Item or null if the Item cannot be cast to the current type.</returns>
		TItem GetItem<TItem>(ID id) where TItem : class, IStandardTemplate;

		/// <summary>
		/// For the supplied Item, attempts to cast children to the supplied Type. Only successful casts are returned.
		/// </summary>
		/// <typeparam name="TItem">The Constellation.Sitecore.Item type</typeparam>
		/// <param name="item">The parent Item</param>
		/// <returns>A list of child items that were successfully cast to the provided TItem type</returns>
		IEnumerable<TItem> GetChildren<TItem>(IStandardTemplate item) where TItem : class, IStandardTemplate;

		/// <summary>
		/// Determines if the supplied Ancestor candidate is actually an ancestor of the supplied descendant.
		/// </summary>
		/// <param name="ancestor">The ancestor candidate.</param>
		/// <param name="descendant">The descendant item.</param>
		/// <returns>True if the ancestor is an ancestor of the supplied descendant.</returns>
		bool IsAncestor(IStandardTemplate ancestor, IStandardTemplate descendant);
	}
}
