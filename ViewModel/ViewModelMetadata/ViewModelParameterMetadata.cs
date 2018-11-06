using Model;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    public class ViewModelParameterMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public ParameterMetadata ParameterData { get; set; }

        public ViewModelParameterMetadata(ParameterMetadata parameterMetadata) : base(parameterMetadata.Name)
        {
            ParameterData = parameterMetadata;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (ParameterData.Type != null)
                Add(ParameterData.Type, children);
        }

        public override string ToString()
        {
            string fullName = "";

            if (ParameterData.Type != null)
                fullName = ParameterData.Type.Name + " ";

            fullName += ParameterData.Name;
            return fullName;
        }
    }
}
