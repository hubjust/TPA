﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
    public static class ExtensionMethods
    {
        internal static bool GetVisible(this Type type)
        {
            return type.IsPublic || type.IsNestedPublic || type.IsNestedFamily || type.IsNestedFamANDAssem;
        }

        internal static bool GetVisible(this MethodBase method)
        {
            return method != null && (method.IsPublic || method.IsFamily || method.IsFamilyAndAssembly);
        }

        internal static bool GetVisible(this FieldInfo field)
        {
            return field != null && (field.IsPublic || field.IsFamily || field.IsFamilyAndAssembly);
        }

        internal static string GetNamespace(this Type type)
        {
            string ns = type.Namespace;
            return ns != null ? ns : string.Empty;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
        }
    }
}
