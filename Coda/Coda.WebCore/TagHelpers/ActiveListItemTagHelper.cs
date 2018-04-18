// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Coda.WebCore.TagHelpers
{
    [HtmlTargetElement("li", Attributes = TagAttribute)]
    public class ActiveListItemTagHelper : RouteBasedTagHelper
    {
        private const string TagAttribute = "active-if-*";

        public ActiveListItemTagHelper(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            AddRouteMatch(RouteOption.Area, () => !string.IsNullOrWhiteSpace(ActiveIfArea), () => ActiveIfArea);
            AddRouteMatch(RouteOption.Controller, () => !string.IsNullOrWhiteSpace(ActiveIfController), () => ActiveIfController);
            AddRouteMatch(RouteOption.Action, () => !string.IsNullOrWhiteSpace(ActiveIfAction), () => ActiveIfAction);
            AddRouteMatch(RouteOption.Page, () => !string.IsNullOrWhiteSpace(ActiveIfPage), () => ActiveIfPage);
        }

        public IfOperatorMode ActiveIfOperator { get; set; } = IfOperatorMode.Or;

        public IfComparisonMode ActiveIfMode { get; set; } = IfComparisonMode.Match;

        public string ActiveIfController { get; set; }

        public string ActiveIfAction { get; set; }

        public string ActiveIfArea { get; set; }

        public string ActiveIfPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var match = GetComparisonResult(ActiveIfOperator, ActiveIfMode);

            if (match)
            {
                var classes = output.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
                output.Attributes.SetAttribute("class", $"active {classes}");
            }
        }
    }
}
