using Model;
using Logger;

namespace ViewModel.ViewModelMetadata
{
    class VMAttributeMetadata : TreeViewItem
    {
        TypeMetadata attributeMetadata;
        VMTypeMetadata attributeViewModel;
        public override string Name => ToString();

        public VMAttributeMetadata(TypeMetadata attributeMetadata, Tracer tracer)
        {
            base.tracer = tracer;
            this.attributeMetadata = attributeMetadata;
            if (CanLoadChildren())
                Children.Add(null);
        }

        protected override void LoadChildren()
        {
            base.LoadChildren();
            attributeViewModel = new VMTypeMetadata(attributeMetadata, tracer);
            Children.Add(attributeViewModel);
            FinishedLoadingChildren();
        }

        public override string ToString()
        {
            return "Attribute: " + attributeMetadata.Name;
        }

    }
}
