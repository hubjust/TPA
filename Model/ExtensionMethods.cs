using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
    public static class ExtensionMethods
    {
        #region EnumToBase
        public static Core.Enum.AbstractEnum ToBaseEnum(this Model.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.AbstractEnum.Abstract:
                    return Core.Enum.AbstractEnum.Abstract;

                case Model.AbstractEnum.NotAbstract:
                    return Core.Enum.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static Core.Enum.AccessLevel ToBaseEnum(this Model.AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case Model.AccessLevel.IsPrivate:
                    return Core.Enum.AccessLevel.IsPrivate;

                case Model.AccessLevel.IsProtected:
                    return Core.Enum.AccessLevel.IsProtected;

                case Model.AccessLevel.IsProtectedInternal:
                    return Core.Enum.AccessLevel.IsProtectedInternal;

                case Model.AccessLevel.IsPublic:
                    return Core.Enum.AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        public static Core.Enum.SealedEnum ToBaseEnum(this Model.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.SealedEnum.NotSealed:
                    return Core.Enum.SealedEnum.NotSealed;

                case Model.SealedEnum.Sealed:
                    return Core.Enum.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static Core.Enum.StaticEnum ToBaseEnum(this Model.StaticEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.StaticEnum.Static:
                    return Core.Enum.StaticEnum.Static;

                case Model.StaticEnum.NotStatic:
                    return Core.Enum.StaticEnum.NotStatic;
                default:
                    throw new Exception();
            }
        }

        public static Core.Enum.TypeKind ToBaseEnum(this Model.TypeKind baseEnum)
        {
            switch (baseEnum)
            {
                case Model.TypeKind.Class:
                    return Core.Enum.TypeKind.Class;
                case Model.TypeKind.Enum:
                    return Core.Enum.TypeKind.Enum;
                case Model.TypeKind.Interface:
                    return Core.Enum.TypeKind.Interface;
                case Model.TypeKind.Struct:
                    return Core.Enum.TypeKind.Struct;

                default:
                    throw new Exception();
            }
        }

        public static Core.Enum.VirtualEnum ToBaseEnum(this Model.VirtualEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Model.VirtualEnum.NotVirtual:
                    return Core.Enum.VirtualEnum.NotVirtual;

                case Model.VirtualEnum.Virtual:
                    return Core.Enum.VirtualEnum.Virtual;
                default:
                    throw new Exception();
            }
        }

        #endregion

        #region EnumToModel

        public static Model.AbstractEnum ToLogicEnum(this Core.Enum.AbstractEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.AbstractEnum.Abstract:
                    return Model.AbstractEnum.Abstract;

                case Core.Enum.AbstractEnum.NotAbstract:
                    return Model.AbstractEnum.NotAbstract;
                default:
                    throw new Exception();
            }
        }

        public static Model.AccessLevel ToLogicEnum(this Core.Enum.AccessLevel  baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.AccessLevel.IsPrivate:
                    return Model.AccessLevel.IsPrivate;

                case Core.Enum.AccessLevel.IsProtected:
                    return Model.AccessLevel.IsProtected;

                case Core.Enum.AccessLevel.IsProtectedInternal:
                    return Model.AccessLevel.IsProtectedInternal;

                case Core.Enum.AccessLevel.IsPublic:
                    return Model.AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        public static Model.SealedEnum ToLogicEnum(this Core.Enum.SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.SealedEnum.NotSealed:
                    return Model.SealedEnum.NotSealed;

                case Core.Enum.SealedEnum.Sealed:
                    return Model.SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        public static Model.StaticEnum ToLogicEnum(this Core.Enum.StaticEnum  baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.StaticEnum.Static:
                    return Model.StaticEnum.Static;

                case Core.Enum.StaticEnum.NotStatic:
                    return Model.StaticEnum.NotStatic; 
                default:
                    throw new Exception();
            }
        }

        public static Model.TypeKind ToLogicEnum(this Core.Enum.TypeKind  baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.TypeKind.Class :
                    return Model.TypeKind.Class;
                case Core.Enum.TypeKind.Enum:
                    return Model.TypeKind.Enum;
                case Core.Enum.TypeKind.Interface:
                    return Model.TypeKind.Interface;
                case Core.Enum.TypeKind.Struct:
                    return Model.TypeKind.Struct;
                default:
                    throw new Exception();
            }
        }

        public static Model.VirtualEnum ToLogicEnum(this Core.Enum.VirtualEnum  baseEnum)
        {
            switch (baseEnum)
            {
                case Core.Enum.VirtualEnum.NotVirtual :
                    return Model.VirtualEnum.NotVirtual;

                case Core.Enum.VirtualEnum.Virtual:
                    return Model.VirtualEnum.Virtual;
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
