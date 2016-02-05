namespace Constellation.Sitecore.Presentation.Mvc.Models
{
	using Constellation.Sitecore.Items;
	using Constellation.Sitecore.Items.FieldProperties;

	/// <summary>
	/// Identifies that an Item has fields for analytics scripts in the CMS. 
	/// This contract is used by the PageAnalyticsController.
	/// </summary>
	/// <remarks>
	/// Requires that you are using Constellation.Sitecore.Items.
	/// To implement, all page-based Data Templates for your site must be created with the following fields:
	/// 
	/// Body End Script - multiline text
	/// Body Start Script - multiline text
	/// Inherit Scripts - checkbox
	/// Page Header Script - multiline text
	/// 
	/// Assign this Interface to your page interfaces & page classses.
	/// </remarks>
	public interface IPageAnalytics : IStandardTemplate
	{
		TextProperty BodyEndScript { get; }
		TextProperty BodyStartScript { get; }
		CheckboxProperty InheritScripts { get; }
		TextProperty PageHeaderScript { get; }
	}
}
