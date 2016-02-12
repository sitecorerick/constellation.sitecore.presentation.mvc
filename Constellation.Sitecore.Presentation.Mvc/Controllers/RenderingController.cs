namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Presentation.Mvc.Repositories;
	using System.Web.Mvc;

	/// <summary>
	/// A basic Controller rendering with a strongly-typed datasource.
	/// </summary>
	/// <remarks>
	/// Supports the concept of "render if the Datasource is not null" which is very handy for optional content. This
	/// mode will render regardless in Page Editor mode, so you can click on it & add an appropriate datasource.
	/// 
	/// If a Datasource is not required, you can simply call "Render" and handle the null value yourself.
	/// </remarks>
	/// <typeparam name="TDatasource">The strongly typed Item class or interface.</typeparam>
	public abstract class RenderingController<TDatasource> : ItemController<TDatasource, ItemRepository<TDatasource>>
		where TDatasource : class, IStandardTemplate
	{
		/// <summary>
		/// Renders the result if the Datasource can be cast to TDatasource.
		/// </summary>
		/// <remarks>
		/// Returns an empty result if the Datasource is null. When in Page Editor, returns a simple HTML container
		/// with a message allowing the user to see that the rendering is malfunctioning. This message can be styled by
		/// applying styles to the following CSS marker: ".constellation .no-datasource".
		/// </remarks>
		/// <returns>The results of DoRender() if the Datasource is not null else an Empty Result.</returns>
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

			return DoRender();
		}

		/// <summary>
		/// Attempts to return the ActionResult of DoRender regardless of the status of the Datasource object.
		/// </summary>
		/// <returns>The results of DoRender()</returns>
		public ActionResult Render()
		{
			return DoRender();
		}

		/// <summary>
		/// Programmer's step-off point for controller logic and determining the ActionResult.
		/// </summary>
		/// <returns>The ActionResult</returns>
		protected abstract ActionResult DoRender();
	}
}