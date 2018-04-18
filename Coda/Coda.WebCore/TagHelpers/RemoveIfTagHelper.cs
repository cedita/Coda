// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Coda.WebCore.TagHelpers
{
    [HtmlTargetElement("*", Attributes = TagAttribute)]
    public class RemoveIfTagHelper : RouteBasedTagHelper
    {
        private const string TagAttribute = "remove-if-*";

        public RemoveIfTagHelper(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
            AddRouteMatch(RouteOption.Area, () => !string.IsNullOrWhiteSpace(RemoveIfArea), () => RemoveIfArea);
            AddRouteMatch(RouteOption.Controller, () => !string.IsNullOrWhiteSpace(RemoveIfController), () => RemoveIfController);
            AddRouteMatch(RouteOption.Action, () => !string.IsNullOrWhiteSpace(RemoveIfAction), () => RemoveIfAction);
            AddRouteMatch(RouteOption.Page, () => !string.IsNullOrWhiteSpace(RemoveIfPage), () => RemoveIfPage);
        }

        public IfOperatorMode RemoveIfOperator { get; set; } = IfOperatorMode.Or;

        public IfComparisonMode RemoveIfMode { get; set; } = IfComparisonMode.Match;

        public string RemoveIfController { get; set; }

        public string RemoveIfAction { get; set; }

        public string RemoveIfArea { get; set; }

        public string RemoveIfPage { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var match = GetComparisonResult(RemoveIfOperator, RemoveIfMode);

            if (match)
            {
                output.TagName = null;
                output.Content.SetContent(string.Empty);
            }
        }
    }
}
