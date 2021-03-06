﻿using System;
using System.IO;
using System.ComponentModel.Composition;

using Interfaces;

namespace CLI
{
    [Export(typeof(IFileSelector))]
    class CLIFileSelector : IFileSelector
    {
        public string FileToOpen(string filter = null)
        {
            string filePath;

            Console.Clear();

            do
            {
                Console.Write(":: TYPE PATH TO FILE ::\n::> ");
                filePath = Console.ReadLine();
                Console.Clear();
            } while (string.IsNullOrEmpty(filePath) || !File.Exists(filePath));

            return filePath;
        }

        public string FileToSave(string filter = null)
        {
            string filePath;

            Console.Clear();

            do
            {
                Console.Write(":: TYPE WHERE TO SAVE ::\n::> ");
                filePath = Console.ReadLine();
                filePath += ".xml";
                Console.Clear();
            } while (string.IsNullOrEmpty(filePath));

            return filePath;
        }
    }
}
