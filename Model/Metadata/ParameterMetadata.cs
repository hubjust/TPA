namespace Model
{
    public class ParameterMetadata : Metadata
    {
        public TypeMetadata Type { get; set; }

        public ParameterMetadata() { }

        public ParameterMetadata(string name, TypeMetadata typeMetadata) : base(name)
        {
            Type = typeMetadata;
        }
    }
}