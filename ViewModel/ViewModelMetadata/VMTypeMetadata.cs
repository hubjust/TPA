using System.Text;

using Model;

namespace ViewModel.ViewModelMetadata
{
    public class VMTypeMetadata : TreeViewItem
    {
        private TypeMetadata typeMetadata;

        public VMTypeMetadata(TypeMetadata typeMetadata)
        {
            this.typeMetadata = typeMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (TypeMetadata attribute in typeMetadata.Attributes.OrEmptyIfNull())
                Children.Add(new VMAttributeMetadata(attribute));
            foreach (PropertyMetadata propertyMetadata in typeMetadata.Properties.OrEmptyIfNull())
                Children.Add(new VMPropertyMetadata(propertyMetadata));
            foreach (TypeMetadata typeMetadata in typeMetadata.NestedTypes.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(typeMetadata));
            foreach (MethodMetadata methodMetadata in typeMetadata.Methods.OrEmptyIfNull())
                Children.Add(new VMMethodMetadata(methodMetadata));
            foreach (MethodMetadata methodMetadata in typeMetadata.Constructors.OrEmptyIfNull())
                Children.Add(new VMMethodMetadata(methodMetadata));
            foreach (FieldMetadata fieldMetadata in typeMetadata.Fields.OrEmptyIfNull())
                Children.Add(new VMFieldMetadata(fieldMetadata));
            if (typeMetadata.BaseType != null)
                Children.Add(new VMTypeMetadata(typeMetadata.BaseType));
            foreach (TypeMetadata implementedInterface in typeMetadata.ImplementedInterfaces.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(implementedInterface));
            base.FinishedLoadingChildren();
        }

        public override string ToString()
        {
            string modifiers = TransformModifiers();
            return TransformAccurateType() + modifiers + typeMetadata.Name + TransformGenericArguments() + TransformExtendsAndImplements();
        }

        private string TransformAccurateType()
        {
            if (typeMetadata.Type == TypeKind.Interface) return "Interface: ";
            else return "Type: ";
        }

        private string TransformModifiers()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(typeMetadata.AccessLevel.ToString().Substring(2).ToLower() + " ");
            builder.Append(typeMetadata.SealedEnum.Equals(SealedEnum.Sealed) ? "sealed " : "");
            builder.Append(typeMetadata.AbstractEnum.Equals(AbstractEnum.Abstract) ? "abstract " : "");

            return builder.ToString();
        }

        private string TransformExtendsAndImplements()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(typeMetadata.BaseType != null ? " : " + typeMetadata.BaseType.Name : "");
            if (!typeMetadata.ImplementedInterfaces.IsNullOrEmpty())
            {
                builder.Append(typeMetadata.BaseType == null ? " : " : ", ");
                foreach (TypeMetadata implementedInterface in typeMetadata.ImplementedInterfaces)
                {
                    builder.Append(implementedInterface.Name);
                }
            }
            return builder.ToString();
        }

        private string TransformGenericArguments()
        {
            if (typeMetadata.GenericArguments.IsNullOrEmpty())
                return "";
            StringBuilder builder = new StringBuilder();
            builder.Append("<");
            foreach (TypeMetadata arg in typeMetadata.GenericArguments)
                builder.Append(arg.Name + ",");
            builder.Remove(builder.Length - 1, 1);
            builder.Append(">");
            return builder.ToString();
        }

        protected override bool CanLoadChildren()
        {
            return !(typeMetadata.Attributes.IsNullOrEmpty() && typeMetadata.Properties.IsNullOrEmpty()
                && typeMetadata.NestedTypes.IsNullOrEmpty() && typeMetadata.Methods.IsNullOrEmpty() &&
                typeMetadata.Constructors.IsNullOrEmpty() && typeMetadata.Fields.IsNullOrEmpty() &&
                typeMetadata.BaseType == null);
        }
    }
}
