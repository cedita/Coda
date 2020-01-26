// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Coda.WebCore.TagHelpers;

namespace Coda.WebCore
{
    public class WebCoreTagHelperOptions
    {
        public WebCoreTagHelperOptions()
        {
            DefaultOperatorMode = IfOperatorMode.Or;
            DefaultComparisonMode = IfComparisonMode.Match;
        }

        public IfOperatorMode DefaultOperatorMode { get; set; }

        public IfComparisonMode DefaultComparisonMode { get; set; }

        public string DefaultClassIfClass { get; set; }
    }
}
