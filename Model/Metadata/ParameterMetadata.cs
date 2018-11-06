namespace Model
{
    public class ParameterMetadata : Metadata
    {
        public TypeMetadata m_Type { get; set; }

        public ParameterMetadata() { }

        public ParameterMetadata(string name, TypeMetadata typeMetadata) : base(name)
        {
            m_Type = typeMetadata;
        }
    }
}