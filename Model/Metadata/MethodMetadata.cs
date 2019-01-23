
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

        public AccessLevel AccessLevel { get; set; }

        public AbstractEnum AbstractEnum { get; set; }

        public StaticEnum StaticEnum { get; set; }

        public VirtualEnum VirtualEnum { get; set; }

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

            EmitModifiers(method);
            Extension = EmitExtension(method);
        }

        public MethodMetadata(DBCore.Model.MethodBase baseMethod)
        {
            this.Name = baseMethod.Name;
            this.AbstractEnum = baseMethod.AbstractEnum.ToLogicEnum();
            this.AccessLevel = baseMethod.AccessLevel.ToLogicEnum();
            this.Extension = baseMethod.Extension;
            this.ReturnType = TypeMetadata.GetOrAdd(baseMethod.ReturnType);
            this.StaticEnum = baseMethod.StaticEnum.ToLogicEnum();
            this.VirtualEnum = baseMethod.VirtualEnum.ToLogicEnum();

            GenericArguments = baseMethod.GenericArguments?.Select(TypeMetadata.GetOrAdd).ToList();

            Parameters = baseMethod.Parameters?.Select(t => new ParameterMetadata(t)).ToList();

        }

        public MethodMetadata() { }



        #region privateMethods
        public static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return (from MethodBase _currentMethod in methods
                    //where _currentMethod.GetVisible()
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

        private void EmitModifiers(MethodBase method)
        {
            AccessLevel = method.IsPublic ? AccessLevel.IsPublic :
                method.IsFamily ? AccessLevel.IsProtected :
                method.IsAssembly ? AccessLevel.Internal : AccessLevel.IsPrivate;

            AbstractEnum = method.IsAbstract ? AbstractEnum.Abstract : AbstractEnum.NotAbstract;

            StaticEnum = method.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;

            VirtualEnum = method.IsVirtual ? VirtualEnum.Virtual : VirtualEnum.NotVirtual;
        }

        #endregion
    }
}
