namespace Constellation.Sitecore.Presentation.Mvc.Caching
{
	using System;

	/// <summary>
	/// Clears the caching of the custom site caching objects
	/// </summary>
	public class CacheClearer
	{
		#region OnPublishEnd
		/// <summary>
		/// On Publish End event to clear the caching from the framework
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		public void OnPublishEnd(object sender, EventArgs args)
		{
			//item saved is called when an item is published (because the item is being saved in the web database)
			//when this happens, we don't want our code to move the item anywhere, so escape out of this function.
			if ((global::Sitecore.Context.Job != null && !global::Sitecore.Context.Job.Category.Equals("publish", StringComparison.OrdinalIgnoreCase)))
			{
				return;
			}

			// complete site list for publish
			System.Xml.XmlNodeList siteList = null;

			// setup default for publish remote
			string eventName = "publish:end:remote";

			// what action was undertaken
			if (!args.GetType().ToString().Equals("Sitecore.Data.Events.PublishEndRemoteEventArgs"))
			{
				eventName = ((global::Sitecore.Events.SitecoreEventArgs)args).EventName;
			}
			else
			{
				// publish end remote event args
				global::Sitecore.Data.Events.PublishEndRemoteEventArgs pargs = (global::Sitecore.Data.Events.PublishEndRemoteEventArgs)args;
			}

			// get the sitelist
			siteList = global::Sitecore.Configuration.Factory.GetConfigNodes(string.Format("/sitecore/events/event[@name='{0}']/handler[@type='Sitecore.Publishing.HtmlCacheClearer, Sitecore.Kernel']/sites/site", eventName));

			// make sure we hav a site list to clean up
			if (siteList != null)
			{
				// cycle through the site lists
				foreach (System.Xml.XmlNode xNode in siteList)
				{
					global::Sitecore.Sites.SiteContext site = global::Sitecore.Configuration.Factory.GetSite(xNode.InnerText);
					if (site != null)
					{
						// clear the caching util
						Cache.ClearSitecoreCache(site.Name, site.Database.Name);
					}
				}
			}
		}
		#endregion
	}

}