// Copyright (c) Cedita Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.

using System;

namespace Coda.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get a DisplayName attribute's name value from an Enum Value
        /// </summary>
        /// <param name="value">Enum Value</param>
        /// <returns>Display Name for Enum Value</returns>
        public static string GetDisplayName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var fieldInfo = value.GetType().GetField(value.ToString());
            return fieldInfo.GetDisplayName();
        }
    }
}
