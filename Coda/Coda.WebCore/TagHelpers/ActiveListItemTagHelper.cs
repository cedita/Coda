using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace Coda.WebCore
{
    [HtmlTargetElement("li", Attributes = _tagAttribute)]
    public class ActiveListItemTagHelper : TagHelper
    {
        const string _tagAttribute = "active-if-*";

        readonly IHttpContextAccessor httpContextAccessor;

        public string ActiveIfController { get; set; }
        public string ActiveIfAction { get; set; }
        public string ActiveIfArea { get; set; }
        public string ActiveIfPage { get; set; }

        public ActiveListItemTagHelper(IHttpContextAccessor httpContextAccessor) : base()
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var area = httpContextAccessor.HttpContext.GetRouteValue("area") as string;
            var action = httpContextAccessor.HttpContext.GetRouteValue("action") as string;
            var controller = httpContextAccessor.HttpContext.GetRouteValue("controller") as string;
            var page = httpContextAccessor.HttpContext.GetRouteValue("page") as string;

            var match = true;

            if (!string.IsNullOrWhiteSpace(ActiveIfController) && !string.Equals(controller, ActiveIfController, StringComparison.InvariantCultureIgnoreCase))
            {
                match = false;
            }
            if (!string.IsNullOrWhiteSpace(ActiveIfAction) && !string.Equals(action, ActiveIfAction, StringComparison.InvariantCultureIgnoreCase))
            {
                match = false;
            }
            if (!string.IsNullOrWhiteSpace(ActiveIfArea) && !string.Equals(area, ActiveIfArea, StringComparison.InvariantCultureIgnoreCase))
            {
                match = false;
            }
            if (!string.IsNullOrWhiteSpace(ActiveIfPage) && !string.Equals(page, ActiveIfPage, StringComparison.InvariantCultureIgnoreCase))
            {
                match = false;
            }

            if (match)
            {
                var classes = output.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
                output.Attributes.SetAttribute("class", $"active {classes}");
            }
        }
    }
}
