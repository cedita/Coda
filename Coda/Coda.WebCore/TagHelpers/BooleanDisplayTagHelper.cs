// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Coda.WebCore.TagHelpers
{
    [HtmlTargetElement("*", Attributes = TagAttribute)]
    public class BooleanDisplayTagHelper : ComparisonBasedTagHelper
    {
        private const string TagAttribute = "render-if-matches";

        public BooleanDisplayTagHelper(IOptions<WebCoreTagHelperOptions> options)
        {
            RenderIfOperator = options.Value.DefaultOperatorMode;
            RenderIfMode = options.Value.DefaultComparisonMode;

            AddComparison(() => RenderIfMatches != null, () => RenderIfMatches.Value);
        }

        public IfOperatorMode RenderIfOperator { get; set; }

        public IfComparisonMode RenderIfMode { get; set; }

        public bool? RenderIfMatches { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var match = GetComparisonResult(RenderIfOperator, RenderIfMode);
            output.TagName = null;

            if (!match)
            {
                output.SuppressOutput();
            }
        }
    }
}