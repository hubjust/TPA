using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
    public static class ExtensionMethods
    {
        #region Enum
        public static DBCore.Enum.AbstractEnum ToBaseEnum(this Model.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.AbstractEnum.Abstract:
                    return DBCore.Enum.AbstractEnum.Abstract;

                case Model.AbstractEnum.NotAbstract:
                    return DBCore.Enum.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static DBCore.Enum.AccessLevel ToBaseEnum(this Model.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case Model.AccessLevel.IsPrivate:
                    return DBCore.Enum.AccessLevel.IsPrivate;

                case Model.AccessLevel.IsProtected:
                    return DBCore.Enum.AccessLevel.IsProtected;

                case Model.AccessLevel.IsProtectedInternal:
                    return DBCore.Enum.AccessLevel.IsProtectedInternal;

                case Model.AccessLevel.IsPublic:
                    return DBCore.Enum.AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        public static DBCore.Enum.SealedEnum ToBaseEnum(this Model.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.SealedEnum.NotSealed:
                    return DBCore.Enum.SealedEnum.NotSealed;

                case Model.SealedEnum.Sealed:
                    return DBCore.Enum.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static DBCore.Enum.StaticEnum ToBaseEnum(this Model.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.StaticEnum.Static:
                    return DBCore.Enum.StaticEnum.Static;

                case Model.StaticEnum.NotStatic:
                    return DBCore.Enum.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        public static DBCore.Enum.TypeKind ToBaseEnum(this Model.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Model.TypeKind.Class:
                    return DBCore.Enum.TypeKind.Class;
                case Model.TypeKind.Enum:
                    return DBCore.Enum.TypeKind.Enum;
                case Model.TypeKind.Interface:
                    return DBCore.Enum.TypeKind.Interface;
                case Model.TypeKind.Struct:
                    return DBCore.Enum.TypeKind.Struct;

                default:
                    throw new Exception();
            }
        }

        public static DBCore.Enum.VirtualEnum ToBaseEnum(this Model.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.VirtualEnum.NotVirtual:
                    return DBCore.Enum.VirtualEnum.NotVirtual;

                case Model.VirtualEnum.Virtual:
                    return DBCore.Enum.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }

        #endregion


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
