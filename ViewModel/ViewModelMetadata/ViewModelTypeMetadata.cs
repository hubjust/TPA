using Model;
using System.Collections.ObjectModel;

namespace ViewModel.ViewModelMetadata
{
    public class ViewModelTypeMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public TypeMetadata TypeData { get; set; }

        public ViewModelTypeMetadata(TypeMetadata typeMetadata) : base(typeMetadata.Name)
        {
            TypeData = typeMetadata;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (TypeData.BaseType != null)
                Add(TypeData.BaseType, children);

            if (TypeData.DeclaringType != null)
                Add(TypeData.DeclaringType, children);

            if (TypeData.Properties != null)
                Add(TypeData.Properties, children);

            if (TypeData.Fields != null)
                Add(TypeData.Fields, children);

            if (TypeData.GenericArguments != null)
                Add(TypeData.GenericArguments, children);
            
            if (TypeData.ImplementedInterfaces != null)
                Add(TypeData.ImplementedInterfaces, children);
            
            if (TypeData.NestedTypes != null)
                Add(TypeData.NestedTypes, children);
            
            if (TypeData.Methods != null)
                Add(TypeData.Methods, children);
            
            if (TypeData.Constructors != null)
                Add(TypeData.Constructors, children);
            
        }

        private string GetTypeKindString(TypeMetadata.TypeKind typeKind)
        {
            return typeKind.ToString().ToLower();
        }

        public override string ToString()
        {
            string fullName = "";

            if (TypeData.Modifiers != null)
            {
                fullName = GetAccessLevelString(TypeData.Modifiers.Item1);

                fullName += " " + GetAbstractString(TypeData.Modifiers.Item3);
                fullName.Trim();

                fullName += " " + GetSealedString(TypeData.Modifiers.Item2);
                fullName.Trim();
            }

            fullName += " " + GetTypeKindString(TypeData.Type);
            fullName.Trim();

            fullName += " " + TypeData.Name;

            return fullName;
        }
    }
}
