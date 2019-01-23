namespace Interfaces
{
    public interface IFileSelector
    {
        string FileToOpen(string filter = null);
        string FileToSave(string filter = null);
    }
}
