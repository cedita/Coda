// Copyright (c) Cedita Digital Ltd. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the solution root for license information.
using System;
using System.Linq;
using System.Reflection;

namespace Coda.Reflection
{
    public static class PropertyHelpers
    {
        public static PropertyInfo GetProperty(object obj, string propName)
        {
#if NETSTANDARD1_4
            return obj.GetType().GetTypeInfo().DeclaredProperties.Where(m => m.Name == propName).FirstOrDefault();
#else
            return obj.GetType().GetProperty(propName);
#endif
        }

        public static Type GetPropertyType(object obj, string propName)
        {
            return GetProperty(obj, propName).PropertyType;
        }

        public static object GetPropertyValue(object obj, string propName)
        {
            return GetProperty(obj, propName).GetValue(obj);
        }

        public static T GetPropertyValue<T>(object obj, string propName)
        {
            return (T)GetPropertyValue(obj, propName);
        }

        public static void SetPropertyValue(object obj, string propName, object val)
        {
            GetProperty(obj, propName).SetValue(obj, val);
        }
    }
}
