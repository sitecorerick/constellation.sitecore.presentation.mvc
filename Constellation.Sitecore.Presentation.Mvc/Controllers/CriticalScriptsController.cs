namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using System.Web.Mvc;

	public class CriticalScriptsController : CriticalAssetController
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

			var scripts = System.IO.File.ReadAllText(filePath);

			if (string.IsNullOrEmpty(scripts))
			{
				return Content("<!-- no scripts in file path: " + filePath + " -->");
			}

			if (this.Parameters.MinifyContent)
			{
				scripts = Minify(scripts);
			}

			if (string.IsNullOrEmpty(scripts))
			{
				return Content("<!-- no scripts after minification -->");
			}

			return Content("<script>" + scripts + "</script>");
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
				return new EmptyResult();
			}

			var url = path;

			if (this.Parameters.BustCache)
			{
				url = url + "?ver=" + HttpContext.ApplicationInstance.GetType().Assembly.GetName().Version;
			}

			return Content(string.Format("<script src=\"{0}\"></script>", url));
		}
	}
}
