namespace Interfaces
{
    public interface ISerializer<T>
    {
        void Serialize(IFileSelector filePath, T target);
        T Deserialize(IFileSelector filePath);
    }
}
