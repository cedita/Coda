// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using Coda.Reflection;
using System.Linq;
using System.Reflection;

namespace Coda.Extensions
{
    public static class ModelExtensions
    {
        /// <summary>
        /// Type-safe value copy from one object instance to another.
        /// </summary>
        /// <example>
        /// This sample shows how to use <see cref="ValuesFrom{T}(T, T, string[])"/> to copy specific property values specified in
        /// <paramref name="properties"/> from one model to another.
        /// 
        /// <code>
        /// user.ValuesFrom(model,
        ///    nameof(model.Title),
        ///    nameof(model.FirstName),
        ///    nameof(model.Surname)
        /// );
        /// </code>
        /// </example>
        /// <typeparam name="T">Type of object you are copying</typeparam>
        /// <param name="targetObject">Target object for copying values to</param>
        /// <param name="sourceObject">Source object for copying values from</param>
        /// <param name="properties">Properties to copy</param>
        public static void ValuesFrom<T>(this T targetObject, T sourceObject, params string[] properties)
        {
            targetObject.ValuesFrom(sourceObject, false, properties);
        }

        /// <summary>
        /// Copy values from one object to another, as long as they are the same type.
        /// </summary>
        /// <example>
        /// This sample shows how to use <see cref="ValuesFrom{T}(T, T, bool, string[])"/> to copy all values excluding <paramref name="properties"/>
        /// from one model to another.
        /// 
        /// <code>
        /// user.ValuesFrom(model, true,
        ///    nameof(model.DoNotCopyMe)
        /// );
        /// </code>
        /// </example>
        /// <typeparam name="T">Type of object you are copying</typeparam>
        /// <param name="targetObject">Target object for copying values to</param>
        /// <param name="sourceObject">Source object for copying values from</param>
        /// <param name="isExclusionList">Whether <paramref name="properties"/> is a list of properties to exclude or include</param>
        /// <param name="properties">Properties to include or exclude from the copy</param>
        public static void ValuesFrom<T>(this T targetObject, T sourceObject, bool isExclusionList, params string[] properties)
        {
            var type = typeof(T).GetTypeInfo();
            var allProperties = !isExclusionList ? properties : type.DeclaredProperties.Where(m => !properties.Contains(m.Name)).Select(m => m.Name);
            if (allProperties == null || allProperties.Count() == 0) return;
            foreach (var property in properties)
            {
                var newVal = PropertyHelpers.GetProperty(sourceObject, property);
                PropertyHelpers.SetPropertyValue(targetObject, property, newVal);
            }
        }
    }
}
