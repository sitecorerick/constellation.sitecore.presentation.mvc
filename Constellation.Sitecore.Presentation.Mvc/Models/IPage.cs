namespace Constellation.Sitecore.Presentation.Mvc.Models
{
	using Constellation.Sitecore.Items;

	/// <summary>
	/// Specifies that the inheriting Custom Item has presentation details and therefore would be
	/// the context item of a given Sitecore request. Assign this to any Constellation.Items object
	/// that should be designated a Page.
	/// </summary>
	public interface IPage : IStandardTemplate
	{

	}
}
