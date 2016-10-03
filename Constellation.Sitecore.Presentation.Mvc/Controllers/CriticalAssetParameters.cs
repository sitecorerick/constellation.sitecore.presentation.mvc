namespace Constellation.Sitecore.Presentation.Mvc.Controllers
{
	using global::Sitecore.Mvc.Presentation;

	public class CriticalAssetParameters
	{
		private readonly RenderingParameters renderingParameters;

		public CriticalAssetParameters(RenderingParameters renderingParameters)
		{
			this.renderingParameters = renderingParameters;
		}

		public string CriticalFile
		{
			get
			{
				return renderingParameters["criticalFile"];
			}
		}

		public string NormalFile
		{
			get
			{
				return renderingParameters["normalFile"];
			}
		}

		public bool BustCache
		{
			get
			{
				bool output;

				if (bool.TryParse(renderingParameters["bustCache"], out output))
				{
					return output;
				}

				return true;
			}
		}

		public bool MinifyContent
		{
			get
			{
				bool output;

				if (bool.TryParse(renderingParameters["minifyContent"], out output))
				{
					return output;
				}

				return true;
			}
		}
	}
}
