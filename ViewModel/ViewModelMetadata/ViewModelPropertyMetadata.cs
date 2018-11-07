using Model;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    public class ViewModelPropertyMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public PropertyMetadata PropertyData { get; set; }

        public ViewModelPropertyMetadata(PropertyMetadata propertyMetadata) : base(propertyMetadata.Name)
        {
            PropertyData = propertyMetadata;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (PropertyData.Type != null)
                Add(PropertyData.Type, children);
        }

        public override string ToString()
        {
            string fullName = "";

            if (PropertyData.Type != null)
                fullName += PropertyData.Type.Name;

            fullName = fullName.Trim();
            fullName += " " + PropertyData.Name;

            return fullName;
        }
    }
}
