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
        internal static Model.AbstractEnum ToLogicEnum(this DBCore.Enum.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.AbstractEnum.Abstract:
                    return AbstractEnum.Abstract;

                case DBCore.Enum.AbstractEnum.NotAbstract:
                    return AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static Model.AccessLevel ToLogicEnum(this DBCore.Enum.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.AccessLevel.IsPrivate:
                    return AccessLevel.IsPrivate;

                case DBCore.Enum.AccessLevel.IsProtected:
                    return AccessLevel.IsProtected;

                case DBCore.Enum.AccessLevel.IsProtectedInternal:
                    return AccessLevel.IsProtectedInternal;

                case DBCore.Enum.AccessLevel.IsPublic:
                    return AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        public static Model.SealedEnum ToLogicEnum(this DBCore.Enum.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.SealedEnum.NotSealed:
                    return SealedEnum.NotSealed;

                case DBCore.Enum.SealedEnum.Sealed:
                    return SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Model.StaticEnum ToLogicEnum(this DBCore.Enum.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.StaticEnum.Static:
                    return StaticEnum.Static;

                case DBCore.Enum.StaticEnum.NotStatic:
                    return StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        public static Model.TypeKind ToLogicEnum(this DBCore.Enum.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.TypeKind.Class:
                    return TypeKind.Class;
                case DBCore.Enum.TypeKind.Enum:
                    return TypeKind.Enum;
                case DBCore.Enum.TypeKind.Interface:
                    return TypeKind.Interface;
                case DBCore.Enum.TypeKind.Struct:
                    return TypeKind.Struct;

                default:
                    throw new Exception();
            }
        }

        public static Model.VirtualEnum ToLogicEnum(this DBCore.Enum.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case DBCore.Enum.VirtualEnum.NotVirtual:
                    return VirtualEnum.NotVirtual;

                case DBCore.Enum.VirtualEnum.Virtual:
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
