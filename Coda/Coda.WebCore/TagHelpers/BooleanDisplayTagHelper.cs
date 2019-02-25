using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Coda.WebCore.TagHelpers
{
    [HtmlTargetElement("*", Attributes = TagAttribute)]
    public class BooleanDisplayTagHelper : ComparisonBasedTagHelper
    {
        private const string TagAttribute = "render-if-matches";

        public BooleanDisplayTagHelper()
        {
            AddComparison(() => RenderIfMatches != null, () => RenderIfMatches.Value);
        }

        public IfOperatorMode RenderIfOperator { get; set; } = IfOperatorMode.Or;

        public IfComparisonMode RenderIfMode { get; set; } = IfComparisonMode.Match;

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