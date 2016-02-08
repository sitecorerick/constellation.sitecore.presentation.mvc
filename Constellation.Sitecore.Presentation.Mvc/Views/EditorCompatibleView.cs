namespace Constellation.Sitecore.Presentation.Mvc.Views
{
	using System.Web;

	public abstract class EditorCompatibleView : System.Web.Mvc.WebViewPage
	{
		protected bool InEditor
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditor;
			}
		}

		protected bool IsEditing
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditorEditing;
			}
		}

		protected string Version
		{
			get
			{
				return HttpContext.Current.ApplicationInstance.GetType().Assembly.GetName().Version.ToString();
			}
		}
	}

	public abstract class EditorCompatibleView<TModel> : System.Web.Mvc.WebViewPage<TModel>
	{
		protected bool InEditor
		{
			get
			{
				return !global::Sitecore.Context.PageMode.IsNormal;
			}
		}

		protected bool IsEditing
		{
			get
			{
				return global::Sitecore.Context.PageMode.IsExperienceEditorEditing;
			}
		}
	}
}