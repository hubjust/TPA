using System;
using System.Collections.Generic;
using System.Linq;

using Core.Enum;

namespace ViewModel
{
    public static class ExtensionMethods
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || !enumerable.Any();
            // if (enumerable == null)
            //     return true;
            // return enumerable.ToList().Count == 0;
        }

        public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
            //return enumerable ?? new List<T>();
        }

        #region enums

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

        internal static AccessLevel ToLogicEnum(this AccessLevel baseEnum)
        {
            switch (baseEnum)
            {
                case AccessLevel.IsPrivate:
                    return AccessLevel.IsPrivate;

                case AccessLevel.IsProtected:
                    return AccessLevel.IsProtected;

                case AccessLevel.IsProtectedInternal:
                    return AccessLevel.IsProtectedInternal;

                case AccessLevel.IsPublic:
                    return AccessLevel.IsPublic;

                default:
                    throw new Exception();
            }
        }

        internal static SealedEnum ToLogicEnum(this SealedEnum baseEnum)
        {
            switch (baseEnum)
            {
                case SealedEnum.NotSealed:
                    return SealedEnum.NotSealed;

                case SealedEnum.Sealed:
                    return SealedEnum.Sealed;
                default:
                    throw new Exception();
            }
        }

        internal static StaticEnum ToLogicEnum(this StaticEnum baseEnum)
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

        internal static TypeKind ToLogicEnum(this TypeKind baseEnum)
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
        internal static VirtualEnum ToLogicEnum(this VirtualEnum baseEnum)
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
    }
}
