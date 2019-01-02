using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DBCore.Enum;

namespace Model
{
    public static class ExtensionMethods
    {
        #region Enum
        internal static AbstractEnum ToLogicEnum(this AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case AbstractEnum.Abstract:
                    return AbstractEnum.Abstract;

                case AbstractEnum.NotAbstract:
                    return AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static AccessLevel ToLogicEnum(this AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case AccessLevel.IsPrivate:
                    return AccessLevel.IsPrivate;

                case AccessLevel.IsProtected:
                    return AccessLevel.IsProtected;

                case AccessLevel.IsProtectedInternal:
                    return AccessLevel.IsProtectedInternal;

                case Model.AccessLevel.IsPublic:
                    return AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        public static SealedEnum ToLogicEnum(this SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case SealedEnum.NotSealed:
                    return SealedEnum.NotSealed;

                case Model.SealedEnum.Sealed:
                    return SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static StaticEnum ToLogicEnum(this Model.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case StaticEnum.Static:
                    return StaticEnum.Static;

                case StaticEnum.NotStatic:
                    return StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        public static TypeKind ToLogicEnum(this TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case TypeKind.Class:
                    return TypeKind.Class;
                case TypeKind.Enum:
                    return TypeKind.Enum;
                case TypeKind.Interface:
                    return TypeKind.Interface;
                case TypeKind.Struct:
                    return TypeKind.Struct;

                default:
                    throw new Exception();
            }
        }

        public static VirtualEnum ToLogicEnum(this VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case VirtualEnum.NotVirtual:
                    return VirtualEnum.NotVirtual;

                case VirtualEnum.Virtual:
                    return VirtualEnum.Virtual;
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
