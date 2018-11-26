using Model;
using System.Text;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    public class VMPropertyMetadata : TreeViewItem
    {
        private PropertyMetadata propertyMetadata;
        public override string Name => ToString();

        public VMPropertyMetadata(PropertyMetadata propertyMetadata, ITracer tracer)
        {
            this.tracer = tracer;
            this.propertyMetadata = propertyMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            foreach (TypeMetadata attribute in propertyMetadata.Attributes.OrEmptyIfNull())
                Children.Add(new VMAttributeMetadata(attribute, tracer));
            base.Children.Add(new VMTypeMetadata(propertyMetadata.Type, tracer));
            base.FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Property: " + propertyMetadata.Type.Name + TransformGenericArguments() + " " + propertyMetadata.Name;
        }

        private string TransformGenericArguments()
        {
            if (propertyMetadata.Type.GenericArguments.IsNullOrEmpty())
                return "";
            StringBuilder builder = new StringBuilder();
            builder.Append("<");
            foreach (TypeMetadata arg in propertyMetadata.Type.GenericArguments)
                builder.Append(arg.Name + ",");
            builder.Remove(builder.Length - 1, 1);
            builder.Append(">");
            return builder.ToString();
        }
    }
}
