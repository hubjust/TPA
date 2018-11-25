using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Model
{
    public class FieldMetadata : BaseMetadata
    {
        public TypeMetadata TypeMetadata;
        public ICollection<TypeMetadata> AttributesMetadata { get; set; }

        public TupleTwo<AccessLevel, StaticEnum> Modifiers { get; set; }

        public FieldMetadata(FieldInfo field)
            : base(field.Name)
        {
            TypeMetadata = TypeMetadata.EmitReference(field.FieldType);
            AttributesMetadata = TypeMetadata.EmitAttributes(field.GetCustomAttributes());

            Modifiers = EmitModifiers(field);
        }

        public FieldMetadata() { }

        internal static ICollection<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fields)
        {
            return (from field in fields
                    where field.GetVisible()
                    select new FieldMetadata(field)).ToList();
        }

        private static TupleTwo<AccessLevel, StaticEnum> EmitModifiers(FieldInfo field)
        {
            AccessLevel _access = AccessLevel.IsPrivate;
            if (field.IsPublic)
                _access = AccessLevel.IsPublic;
            else if (field.IsFamily)
                _access = AccessLevel.IsProtected;
            else if (field.IsFamilyAndAssembly)
                _access = AccessLevel.IsProtectedInternal;
            StaticEnum _static = StaticEnum.NotStatic;
            if (field.IsStatic)
                _static = StaticEnum.Static;
            return new Tuple<AccessLevel, StaticEnum>(_access, _static);
        }
    }
}
