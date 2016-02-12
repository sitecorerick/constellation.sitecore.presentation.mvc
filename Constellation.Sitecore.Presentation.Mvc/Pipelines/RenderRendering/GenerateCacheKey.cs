namespace Constellation.Sitecore.Presentation.Mvc.Pipelines.RenderRendering
{
	using global::Sitecore.Globalization;
	using global::Sitecore.Mvc.Extensions;
	using global::Sitecore.Mvc.Pipelines.Response.RenderRendering;
	using global::Sitecore.Mvc.Presentation;

	public class GenerateCacheKey : global::Sitecore.Mvc.Pipelines.Response.RenderRendering.GenerateCacheKey
	{
		protected override string GenerateKey(Rendering rendering, RenderRenderingArgs args)
		{
			var str1 = rendering.Caching.CacheKey.OrIfEmpty(args.Rendering.Renderer.ValueOrDefault(renderer => renderer.CacheKey));
			if (str1.IsEmptyOrNull())
				return null;
			var str2 = str1 + "_#lang:" + Language.Current.Name.ToUpper() + this.GetAreaPart(args);

			str2 += rendering.RenderingItem.InnerItem.Paths.FullPath;

			RenderingCachingDefinition caching = rendering.Caching;
			if (caching.VaryByData)
				str2 += this.GetDataPart(rendering);
			if (caching.VaryByDevice)
				str2 += this.GetDevicePart(rendering);
			if (caching.VaryByLogin)
				str2 += this.GetLoginPart(rendering);
			if (caching.VaryByUser)
				str2 += this.GetUserPart(rendering);
			if (caching.VaryByParameters)
				str2 += this.GetParametersPart(rendering);
			if (caching.VaryByQueryString)
				str2 += this.GetQueryStringPart(rendering);
			return str2;
		}
	}
}
