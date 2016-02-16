namespace Constellation.Sitecore.Presentation.Mvc.Views
{
	using System.Web;

	/// <summary>
	/// Provides easy access to View-specific Sitecore state conditions without
	/// having to add the tests to every view.
	/// </summary>
	public abstract class EditorCompatibleView : System.Web.Mvc.WebViewPage
	{
		/// <summary>
		/// Gets a value indicating that the page is open in the Experience Editor.
		/// </summary>
		protected bool InEditor
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditor;
			}
		}

		/// <summary>
		/// Gets a value indicating that the page is being edited in the Experience Editor.
		/// </summary>
		protected bool IsEditing
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditorEditing;
			}
		}

		/// <summary>
		/// Gets the version number of the web application's assembly. Handy for client-side cache busting. Set the number in
		/// the AssemblyInfo.cs file.
		/// </summary>
		protected string Version
		{
			get
			{
				return HttpContext.Current.ApplicationInstance.GetType().Assembly.GetName().Version.ToString();
			}
		}
	}

	/// <summary>
	/// Provides easy access to View-specific Sitecore state conditions without
	/// having to add the tests to every view.
	/// </summary>
	public abstract class EditorCompatibleView<TModel> : System.Web.Mvc.WebViewPage<TModel>
	{
		/// <summary>
		/// Gets a value indicating that the page is open in the Experience Editor.
		/// </summary>
		protected bool InEditor
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditor;
			}
		}

		/// <summary>
		/// Gets a value indicating that the page is being edited in the Experience Editor.
		/// </summary>
		protected bool IsEditing
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditorEditing;
			}
		}

		/// <summary>
		/// Gets the version number of the web application's assembly. Handy for client-side cache busting. Set the number in
		/// the AssemblyInfo.cs file.
		/// </summary>
		protected string Version
		{
			get
			{
				return HttpContext.Current.ApplicationInstance.GetType().Assembly.GetName().Version.ToString();
			}
		}
	}
}