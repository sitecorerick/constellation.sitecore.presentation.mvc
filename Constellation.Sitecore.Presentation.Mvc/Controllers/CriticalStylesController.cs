namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using System.Web.Mvc;

	public class CriticalStylesController : CriticalAssetController
	{
		/// <summary>
		/// Specify the Action to run on first request.
		/// </summary>
		/// <returns>The ActionResult</returns>
		protected override ActionResult RenderFirstRequest()
		{
			string path = this.Parameters.CriticalFile;

			if (string.IsNullOrEmpty(path))
			{
				return new EmptyResult();
			}

			var filePath = Server.MapPath(path);

			if (!System.IO.File.Exists(filePath))
			{
				return new EmptyResult();
			}

			var styles = System.IO.File.ReadAllText(filePath);

			if (string.IsNullOrEmpty(styles))
			{
				return Content("<!-- no styles in file path: " + filePath + " -->");
			}

			if (this.Parameters.MinifyContent)
			{
				styles = Minify(styles);
			}

			if (string.IsNullOrEmpty(styles))
			{
				return Content("<!-- no styles after minification -->");
			}

			return Content("<style>" + styles + "</style>");
		}

		/// <summary>
		/// Specify the action to run when the session has already made a request.
		/// </summary>
		/// <returns>The ActionResult</returns>
		protected override ActionResult RenderNormal()
		{
			var path = this.Parameters.NormalFile;

			if (string.IsNullOrEmpty(path))
			{
				return Content("<!-- no normalfile parameter supplied by rendering -->");
			}

			var url = path;

			if (this.Parameters.BustCache)
			{
				url = url + "?ver=" + HttpContext.ApplicationInstance.GetType().Assembly.GetName().Version;
			}

			return Content(string.Format("<link rel=\"stylesheet\" href=\"{0}\" />", url));
		}
	}
}
