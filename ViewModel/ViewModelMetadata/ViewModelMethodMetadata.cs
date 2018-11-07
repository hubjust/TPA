using Model;
using System.Linq;
using System.Collections.ObjectModel;


namespace ViewModel.ViewModelMetadata
{
    public class ViewModelMethodMetadata : ViewModelMetadata, ITreeViewItemBuilder
    {
        public MethodMetadata Method { get; set; }

        public ViewModelMethodMetadata(MethodMetadata methodMetadata) : base(methodMetadata.Name)
        {
            Method = methodMetadata;
        }

        public void Build(ObservableCollection<ITreeViewItem> children)
        {
            if (Method.GenericArguments != null)
                Add(Method.GenericArguments, children);

            if (Method.Parameters != null)
                Add(Method.Parameters, children);

            if (Method.ReturnType != null)
                Add(Method.ReturnType, children);
        }

        public override string ToString()
        {
            string fullName = "";

            if (Method.Modifiers != null)
            {
                fullName = GetAccessLevelString(Method.Modifiers.Item1);

                fullName = fullName.Trim();
                fullName += " " + GetStaticString(Method.Modifiers.Item3);

                fullName = fullName.Trim();
                fullName += " " + GetVirtualString(Method.Modifiers.Item4);

                fullName = fullName.Trim();
                fullName += " " + GetAbstractString(Method.Modifiers.Item2);
            }

            if (Method.ReturnType != null)
            {
                fullName = fullName.Trim();
                fullName += " " + Method.ReturnType.Name;
            }

            fullName = fullName.Trim();
            fullName += " " + Method.Name;

            fullName += "(";
            foreach (ParameterMetadata parameterMetadata in Method.Parameters)
            {
                fullName += parameterMetadata.Type.Name + " " + parameterMetadata.Name;

                if (parameterMetadata != Method.Parameters.Last())
                {
                    fullName += ", ";
                }
            }

            fullName = fullName.TrimEnd(new char[] { ',', ' ' });
            fullName += ")";

            return fullName;
        }
    }
}
