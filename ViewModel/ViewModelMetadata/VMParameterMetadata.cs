using Model;

namespace ViewModel.ViewModelMetadata
{
    public class VMParameterMetadata : TreeViewItem
    {
        ParameterMetadata parameterMetadata;
        public override string Name => ToString();

        public VMParameterMetadata(ParameterMetadata parameterMetadata)
        {
            this.parameterMetadata = parameterMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            Children.Add(new VMTypeMetadata(parameterMetadata.Type));
            foreach (TypeMetadata attribute in parameterMetadata.Attributes.OrEmptyIfNull())
                Children.Add(new VMAttributeMetadata(attribute));
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Parameter: " + parameterMetadata.Type.Name + " " + parameterMetadata.Name;
        }
    }
}
