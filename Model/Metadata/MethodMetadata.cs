
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Model
{
  public class MethodMetadata : Metadata
  {
        public List<TypeMetadata> GenericArguments { get; set; }
        public TupleFour<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public TypeMetadata ReturnType { get; set; }
        public bool Extension { get; set; }
        public List<ParameterMetadata> Parameters { get; set; }

        public MethodMetadata() { }

        public MethodMetadata(MethodBase method) : base(method.Name)
        {
            //sprawdzic czy to na pewno jest dobrze
            if(!method.IsGenericMethodDefinition)
            {
                GenericArguments = null;
            }
            else 
            {
                GenericArguments = TypeMetadata.EmitGenericArguments(method.GetGenericArguments()).ToList();
            }

            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters()).ToList();
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase _currentMethod in methods
                    where _currentMethod.GetVisible()
                    select new MethodMetadata(_currentMethod);
        }

        private static IEnumerable<ParameterMetadata> EmitParameters(IEnumerable<ParameterInfo> parms)
        {
            return from parm in parms
                   select new ParameterMetadata(parm.Name, TypeMetadata.EmitReference(parm.ParameterType));
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
