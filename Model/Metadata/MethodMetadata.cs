﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Model
{
    [DataContract]
    public class MethodMetadata : BaseMetadata
    {
        [DataMember]
        public ICollection<TypeMetadata> GenericArguments { get; set; }
        [DataMember]
        public ICollection<ParameterMetadata> Parameters { get; set; }
        [DataMember]
        public ICollection<TypeMetadata> AttributesMetadata { get; set; }

        [DataMember]
        public TypeMetadata ReturnType { get; set; }
        [DataMember]
        public TypeMetadata ReflectedType { get; set; }

        [DataMember]
        public TupleFour<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        [DataMember]
        public bool Extension { get; set; }

        public MethodMetadata(MethodBase method)
            : base(method.IsConstructor ? method.ReflectedType.Name : method.Name)
        {
            GenericArguments = !method.IsGenericMethodDefinition ? null : TypeMetadata.EmitGenericArguments(method.GetGenericArguments()).ToList();
            Parameters = EmitParameters(method.GetParameters()).ToList();
            AttributesMetadata = TypeMetadata.EmitAttributes(method.GetCustomAttributes());

            ReturnType = EmitReturnType(method);
            ReflectedType = TypeMetadata.EmitReference(method.ReflectedType);

            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata() { }

        public static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return (from MethodBase _currentMethod in methods
                    where _currentMethod.GetVisible()
                    select new MethodMetadata(_currentMethod));
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm);
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            TypeMetadata.StoreType(methodInfo.ReturnType);
            return TypeMetadata.EmitReference(methodInfo.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static TupleFour<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            if (method.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (method.IsFamily)
                _access = AccessLevel.IsProtected;
            else if (method.IsFamilyAndAssembly)
                _access = AccessLevel.IsProtectedInternal;
            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;
            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;
            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;
            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(_access, _abstract, _static, _virtual);
        }
    }
}
