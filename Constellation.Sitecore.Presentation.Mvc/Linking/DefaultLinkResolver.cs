namespace Constellation.Sitecore.Presentation.Mvc.Linking
{
	using Constellation.Sitecore.Items;
	using global::Sitecore.Data.Items;
	using global::Sitecore.Links;
	using global::Sitecore.Resources.Media;

	public class DefaultLinkResolver : ILinkResolver
	{
		public string GetItemUrl(Item item)
		{


			var options = LinkManager.GetDefaultUrlOptions();

			return GetItemUrl(item, options);
		}

		public string GetItemUrl(Item item, UrlOptions options)
		{
			if (item == null)
			{
				return string.Empty;
			}

			return LinkManager.GetItemUrl(item, options);
		}

		public string GetItemUrl(IStandardTemplate item)
		{
			if (item == null)
			{
				return string.Empty;
			}

			return GetItemUrl(item.InnerItem);
		}

		public string GetItemUrl(IStandardTemplate item, UrlOptions options)
		{
			return GetItemUrl(item.InnerItem, options);
		}

		public string GetMediaUrl(MediaItem item)
		{
			if (item == null)
			{
				return string.Empty;
			}

			return MediaManager.GetMediaUrl(item);
		}
	}
}