using System;
using System.IO;

using ViewModel;

namespace CLI
{
    class CLIFileSelector : IFileSelector
    {
        public string FileToOpen()
        {
            string filePath;

            do
            {
                Console.Write("Type the path to DLL: ");
                filePath = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(filePath) || !File.Exists(filePath));

            return filePath;
        }

        public string FileToSave()
        {
            string filePath;

            do
            {
                Console.Write("Type where to save: ");
                filePath = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(filePath));

            return filePath;
        }
    }
}
