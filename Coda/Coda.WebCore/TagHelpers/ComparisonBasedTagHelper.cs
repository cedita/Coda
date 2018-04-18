// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Coda.WebCore.TagHelpers
{
    public abstract class ComparisonBasedTagHelper : TagHelper
    {
        protected List<(Func<bool> qualifier, Func<bool> comparison)> Comparisons = new List<(Func<bool>, Func<bool>)>();

        protected ComparisonBasedTagHelper()
            : base()
        {
        }

        protected void AddComparison(Func<bool> qualifier, Func<bool> result)
        {
            Comparisons.Add((qualifier, result));
        }

        protected IEnumerable<bool> MakeComparisons()
        {
            var results = Comparisons
                .Where(m => m.qualifier())
                .Select(m => m.comparison());
            return results;
        }

        protected bool GetComparisonResult(IfOperatorMode operatorMode, IfComparisonMode comparisonMode)
        {
            var results = MakeComparisons();
            var match = operatorMode == IfOperatorMode.And ? results.All(m => m) : results.Any(m => m);
            if (comparisonMode == IfComparisonMode.Negated)
            {
                match = !match;
            }

            return match;
        }
    }
}
