// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Coda.WebCore.TagHelpers
{
    public abstract class RouteBasedTagHelper : ComparisonBasedTagHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        protected RouteBasedTagHelper(IHttpContextAccessor httpContextAccessor)
            : base()
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        protected enum RouteOption
        {
            Area,
            Controller,
            Action,
            Page,
        }

        protected void AddRouteMatch(RouteOption option, Func<bool> qualifier, Func<string> matchTo)
        {
            AddComparison(qualifier, () =>
            {
                var optionVal = httpContextAccessor.HttpContext.GetRouteValue(option.ToString().ToLower()) as string;

                // Supporting multiple values, split by ;
                var matchVals = matchTo()
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // Sanitise for Razor Page
                if (option == RouteOption.Page)
                {
                    for (int i = 0; i < matchVals.Length; i++)
                    {
                        if (matchVals[i][0] != '/')
                        {
                            matchVals[i] = "/" + matchVals[i];
                        }
                    }
                }

                return matchVals.Any(m => string.Equals(optionVal, m, StringComparison.InvariantCultureIgnoreCase));
            });
        }
    }
}
