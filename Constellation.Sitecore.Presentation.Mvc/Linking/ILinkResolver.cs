namespace Constellation.Sitecore.Presentation.Mvc.Linking
{
	using Constellation.Sitecore.Items;
	using global::Sitecore.Data.Items;
	using global::Sitecore.Links;

	public interface ILinkResolver
	{
		string GetItemUrl(Item item);

		string GetItemUrl(Item item, UrlOptions options);

		string GetItemUrl(IStandardTemplate item);

		string GetItemUrl(IStandardTemplate item, UrlOptions options);

		string GetMediaUrl(MediaItem item);
	}
}
