using System.Text;

using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    class VMFieldMetadata : TreeViewItem
    {
        private FieldMetadata fieldMetadata;
        public override string Name => ToString();

        public VMFieldMetadata(FieldMetadata field, ITracer tracer)
        {
            base.tracer = tracer;
            fieldMetadata = field;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            Children.Add(new VMTypeMetadata(fieldMetadata.TypeMetadata, tracer));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Field: " + TransformModifiers() + fieldMetadata.TypeMetadata.Name + " " + fieldMetadata.Name + TransformGenericArguments();
        }

        private string TransformModifiers()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(fieldMetadata.Modifiers.Item1.ToString().Substring(2).ToLower() + " ");
            builder.Append(fieldMetadata.Modifiers.Item2.Equals(StaticEnum.Static) ? "static " : "");

            return builder.ToString();
        }

        private string TransformGenericArguments()
        {
            if (fieldMetadata.TypeMetadata.GenericArguments.IsNullOrEmpty())
                return "";

            StringBuilder builder = new StringBuilder();
            builder.Append("<");

            foreach (TypeMetadata arg in fieldMetadata.TypeMetadata.GenericArguments)
                builder.Append(arg.Name + ",");

            builder.Remove(builder.Length - 1, 1);
            builder.Append(">");

            return builder.ToString();
        }
    }
}
