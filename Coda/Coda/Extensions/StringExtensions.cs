// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
namespace Coda.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhitespace(this string str) => string.IsNullOrWhiteSpace(str);
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static string MaxLength(this string str, int maxLength, string trailer = null)
        {
            if (str.Length <= maxLength)
                return str;

            if (trailer != null)
                return str.Substring(0, maxLength - trailer.Length) + trailer;

            return str.Substring(0, maxLength);
        }
    }
}
