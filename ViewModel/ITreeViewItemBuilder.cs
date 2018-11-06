using System.Collections.ObjectModel;

namespace ViewModel
{
    public interface ITreeViewItemBuilder
    {
        void Build(ObservableCollection<ITreeViewItem> children);
    }
}
