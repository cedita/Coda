// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Reflection;

namespace Coda.Extensions
{
    public static class TypeExtensions
    {
        public static FieldInfo GetField(this Type type, string fieldName)
        {
            if (type == null || string.IsNullOrEmpty(fieldName)) return default(FieldInfo);

            var currentType = type;
            do
            {
                var typeInfo = currentType.GetTypeInfo();
                var declaredField = typeInfo.GetDeclaredField(fieldName);
                if (declaredField != null)
                {
                    return declaredField;
                }
                currentType = typeInfo.BaseType;
            } while (currentType != null);

            return default(FieldInfo);
        }
    }
}
