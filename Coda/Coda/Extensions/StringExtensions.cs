// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

namespace Coda.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Returns true if the string is null or whitespace
        /// </summary>
        /// <param name="str">String to work with</param>
        /// <returns>True if the string is null or whitespace</returns>
        public static bool IsNullOrWhitespace(this string str) => string.IsNullOrWhiteSpace(str);

        /// <summary>
        /// Returns true if the string is null or empty
        /// </summary>
        /// <param name="str">String to work with</param>
        /// <returns>True if the string is null or empty</returns>
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        /// <summary>
        /// Return a string truncated down to <paramref name="maxLength"/>, with optional trailing content from <paramref name="trailer"/>
        /// </summary>
        /// <param name="str">String to operate on</param>
        /// <param name="maxLength">Maximum length of string</param>
        /// <param name="trailer">Optional Trailer</param>
        /// <returns>Truncated string</returns>
        public static string MaxLength(this string str, int maxLength, string trailer = null)
        {
            if (str.Length <= maxLength)
            {
                return str;
            }

            if (trailer != null)
            {
                return str.Substring(0, maxLength - trailer.Length) + trailer;
            }

            return str.Substring(0, maxLength);
        }
    }
}
