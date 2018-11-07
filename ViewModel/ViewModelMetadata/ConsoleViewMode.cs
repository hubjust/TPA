using Model;

namespace ViewModel.ViewModelMetadata
{
    public class ConsoleViewModel
    {
        private TypeMetadata Type;

        public ConsoleViewModel(TypeMetadata typeMetadata)
        {
            Type = typeMetadata;
        }

        public override string ToString()
        {
            string fullName = "";

            foreach (ParameterMetadata field in Type.Fields)
            {
                fullName += new ViewModelParameterMetadata(field);
                fullName += "\n";
            }

            foreach (PropertyMetadata property in Type.Properties)
            {
                fullName += new ViewModelPropertyMetadata(property);
                fullName += "\n";
            }

            foreach (MethodMetadata method in Type.Methods)
            {
                fullName += new ViewModelMethodMetadata(method);
                fullName += "\n";
            }

            return fullName;
        }
    }
}
