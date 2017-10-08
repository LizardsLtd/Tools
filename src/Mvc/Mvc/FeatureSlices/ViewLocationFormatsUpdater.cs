using Microsoft.AspNetCore.Mvc.Razor;

namespace Picums.Mvc.FeatureSlices
{
	internal sealed class ViewLocationFormatsUpdater
	{
		private readonly RazorViewEngineOptions options;

		public ViewLocationFormatsUpdater(RazorViewEngineOptions options)
		{
			this.options = options;
		}

		public ViewLocationFormatsUpdater UpdateViewLocations()
		{
			// {0} - Action Name
			// {1} - Controller Name
			// {2} - Area Name
			// {3} - Feature Name
			// Replace normal view location entirely
			options.ViewLocationFormats.Clear();
			options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
			options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
			options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

			return this;
		}

		public ViewLocationFormatsUpdater AddExtender()
		{
			options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
			return this;
		}
	}
}