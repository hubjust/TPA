using System.Text;

using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    public class VMTypeMetadata : TreeViewItem
    {
        private TypeMetadata typeMetadata;
        public override string Name => this.ToString();

        public VMTypeMetadata(TypeMetadata typeMetadata, ITracer tracer)
        {
            this.tracer = tracer;
            this.typeMetadata = typeMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (TypeMetadata attribute in typeMetadata.Attributes.OrEmptyIfNull())
                Children.Add(new VMAttributeMetadata(attribute, tracer));
            foreach (PropertyMetadata propertyMetadata in typeMetadata.Properties.OrEmptyIfNull())
                Children.Add(new VMPropertyMetadata(propertyMetadata, tracer));
            foreach (TypeMetadata typeMetadata in typeMetadata.NestedTypes.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(typeMetadata, tracer));
            foreach (MethodMetadata methodMetadata in typeMetadata.Methods.OrEmptyIfNull())
                Children.Add(new VMMethodMetadata(methodMetadata, tracer));
            foreach (MethodMetadata methodMetadata in typeMetadata.Constructors.OrEmptyIfNull())
                Children.Add(new VMMethodMetadata(methodMetadata, tracer));
            foreach (FieldMetadata fieldMetadata in typeMetadata.Fields.OrEmptyIfNull())
                Children.Add(new VMFieldMetadata(fieldMetadata, tracer));
            if (typeMetadata.BaseType != null)
                Children.Add(new VMTypeMetadata(typeMetadata.BaseType, tracer));
            foreach (TypeMetadata implementedInterface in typeMetadata.ImplementedInterfaces.OrEmptyIfNull())
                Children.Add(new VMTypeMetadata(implementedInterface, tracer));
            base.FinishedLoadingChildren();
        }

        public override string ToString()
        {
            string modifiers = TransformModifiers();
            return TransformAccurateType() + modifiers + typeMetadata.Name + TransformGenericArguments() + TransformExtendsAndImplements();
        }

        private string TransformAccurateType()
        {
            if (typeMetadata.TypeKindProperty == TypeKind.Interface) return "Interface: ";
            else return "Type: ";
        }

        private string TransformModifiers()
        {
            StringBuilder builder = new StringBuilder();

            if (typeMetadata.Modifiers != null)
            {
                builder.Append(typeMetadata.Modifiers.Item1.ToString().Substring(2).ToLower() + " ");
                builder.Append(typeMetadata.Modifiers.Item2.Equals(SealedEnum.Sealed) ? "sealed " : "");
                builder.Append(typeMetadata.Modifiers.Item3.Equals(AbstractEnum.Abstract) ? "abstract " : "");
            }

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
