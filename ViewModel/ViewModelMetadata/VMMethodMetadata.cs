using System.Text;
using System.Collections.Generic;

using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    public class VMMethodMetadata : TreeViewItem
    {
        private MethodMetadata methodMetadata;
        public override string Name => ToString();

        public VMMethodMetadata(MethodMetadata methodMetadata, ITracer tracer)
        {
            base.tracer = tracer;
            this.methodMetadata = methodMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            if (methodMetadata.ReturnType != null)
                base.Children.Add(new VMTypeMetadata(methodMetadata.ReturnType, tracer));
            foreach (TypeMetadata attribute in methodMetadata.AttributesMetadata.OrEmptyIfNull())
                base.Children.Add(new VMAttributeMetadata(attribute, tracer));
            foreach (ParameterMetadata parameter in methodMetadata.Parameters.OrEmptyIfNull())
                base.Children.Add(new VMParameterMetadata(parameter, tracer));
            base.FinishedLoadingChildren();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (methodMetadata.Name.Equals(methodMetadata.ReflectedType.Name))
                builder.Append("Constructor: ");
            else
                builder.Append("Method: ");
            builder.Append(TransformModifiers());
            if (methodMetadata.ReturnType != null)
                builder.Append(methodMetadata.ReturnType.Name + " ");
            else if (methodMetadata.ReflectedType.Name != methodMetadata.Name)
                builder.Append("void ");
            builder.Append(methodMetadata.Name);
            builder.Append(TransformParameters(methodMetadata.Parameters));
            return builder.ToString();
        }

        private string TransformModifiers()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(methodMetadata.AccessLevel.ToString().Substring(2).ToLower() + " ");
            builder.Append(methodMetadata.AbstractEnum.Equals(AbstractEnum.Abstract) ? "abstract " : "");
            builder.Append(methodMetadata.StaticEnum.Equals(StaticEnum.Static) ? "static " : "");
            builder.Append(methodMetadata.VirtualEnum.Equals(VirtualEnum.Virtual) ? "virtual " : "");
            return builder.ToString();
        }

        private string TransformParameters(IEnumerable<ParameterMetadata> parameters)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" (");
            foreach (ParameterMetadata parameter in parameters)
                builder.Append(parameter.Type.Name + " " + parameter.Name + ", ");
            if (builder.Length > 2)
                builder.Remove(builder.Length - 2, 2);
            builder.Append(")");
            return builder.ToString();
        }

        protected override bool CanLoadChildren()
        {
            return !(methodMetadata.ReturnType == null && methodMetadata.AttributesMetadata.IsNullOrEmpty()
                && methodMetadata.Parameters.IsNullOrEmpty());
        }
    }
}
