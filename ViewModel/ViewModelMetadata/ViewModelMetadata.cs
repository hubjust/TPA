using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    public abstract class ViewModelMetadata
    {
        public string Name { get; set; }

        public ViewModelMetadata(string name)
        {
            Name = name;
        }

        public static string GetAbstractString(AbstractEnum abstractEnum)
        {
            return abstractEnum == AbstractEnum.Abstract ? "abstract" : "";
        }

        public static string GetAccessLevelString(AccessLevel accessLevel)
        {
            if (accessLevel == AccessLevel.IsPublic) return "public";
            if (accessLevel == AccessLevel.IsPrivate) return "private";
            if (accessLevel == AccessLevel.IsProtected) return "protected";
            return "internal";
        }

        public static string GetSealedString(SealedEnum sealedEnum)
        {
            return sealedEnum == SealedEnum.Sealed ? "sealed" : "";
        }

        public static string GetStaticString(StaticEnum staticEnum)
        {
            return staticEnum == StaticEnum.Static ? "static" : "";
        }

        public static string GetVirtualString(VirtualEnum virtualEnum)
        {
            return virtualEnum == VirtualEnum.Virtual ? "virtual" : "";
        }

        public void Add(List<PropertyMetadata> origin, ObservableCollection<ITreeViewItem> destination)
        {
            foreach (var property in origin)
            {
                ViewModelPropertyMetadata tmp = new ViewModelPropertyMetadata(property);
                destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
            }
        }

        public void Add(List<TypeMetadata> origin, ObservableCollection<ITreeViewItem> destination)
        {
            foreach (var type in origin)
            {
                ViewModelTypeMetadata tmp = new ViewModelTypeMetadata(TypeMetadata.DictionaryType[type.Name]);
                destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
            }
        }

        public void Add(List<MethodMetadata> origin, ObservableCollection<ITreeViewItem> destination)
        {
            foreach (var method in origin)
            {
                ViewModelMethodMetadata tmp = new ViewModelMethodMetadata(method);
                destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
            }
        }

        public void Add(List<NamespaceMetadata> origin, ObservableCollection<ITreeViewItem> destination)
        {
            foreach (var namspac in origin)
            {
                ViewModelNamespaceMetadata tmp = new ViewModelNamespaceMetadata(namspac);
                destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
            }
        }

        public void Add(List<ParameterMetadata> origin, ObservableCollection<ITreeViewItem> destination)
        {
            foreach (var parameter in origin)
            {
                ViewModelParameterMetadata tmp = new ViewModelParameterMetadata(parameter);
                destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
            }
        }

        public void Add(TypeMetadata origin, ObservableCollection<ITreeViewItem> destination)
        {
            ViewModelTypeMetadata tmp = new ViewModelTypeMetadata(TypeMetadata.DictionaryType[origin.Name]);
            destination.Add(new ITreeViewItem { Name = tmp.ToString(), Hierarchy = tmp });
        }
    }
}
