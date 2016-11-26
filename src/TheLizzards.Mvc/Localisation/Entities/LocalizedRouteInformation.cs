namespace TheLizzards.Mvc.Localisation.Entities
{
	public sealed class LocalizedRouteInformation
	{
		public LocalizedRouteInformation(string culture, string template)
		{
			Culture = culture;
			Template = template;
		}

		public string Culture { get; }

		public string Template { get; }

		public string GetTempalteWithChangeCulture() => Template.Replace("{culture}", Culture);
	}
}