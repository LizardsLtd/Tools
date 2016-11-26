using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace TheLizzards.Mvc.Stratup
{
	public sealed class RouteConfigurator
	{
		private readonly IList<RouteTemplate> routeTemplates;

		internal RouteConfigurator()
		{
			this.routeTemplates = new List<RouteTemplate>();
		}

		public RouteConfigurator AddRoute(
			string name
			, string controller
			, string action
			, string parameters = "")
		{
			this.routeTemplates.Add(new RouteTemplate(name, controller, action, parameters));
			return this;
		}

		internal void BuildRoutes(IRouteBuilder routes)
		{
			foreach (var template in this.routeTemplates)
			{
				routes.MapRoute(
					name: template.Name
					, template: template.GetTemplate());
			}
		}

		private sealed class RouteTemplate
		{
			private readonly string controller;
			private readonly string action;
			private readonly string parameters;

			public RouteTemplate(
				string name
				, string controller
				, string action
				, string parameters = "")
			{
				this.Name = name;
				this.controller = controller;
				this.action = action;
				this.parameters = parameters;
			}

			public string Name { get; }

			public string GetTemplate()
			{
				var template = $"{{controller={this.controller}}}/{{action={this.action}}}";

				if (!string.IsNullOrEmpty(this.parameters))
				{
					template = $"{template}/{parameters}";
				}
				return template;
			}
		}
	}
}