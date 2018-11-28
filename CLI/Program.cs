using System;
using System.Collections.Generic;
using System.Linq;

using ViewModel;
using Serializers;

namespace CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ISerializer serializer;

            VMViewModel dataContext = new VMViewModel(new CLIFileSelector(), serializer);
            dataContext.OpenDLL.Execute(null);

        Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
            TreeViewItem current = dataContext.HierarchicalAreas[0];
            current.IsExpanded = true;

            string command;
            string errorMessage = null;
            bool showHelp = true;

            while (true)
            {
                SetUpConsole(current.Name, stack.Count, showHelp, errorMessage);
                errorMessage = null;
                showHelp = false;
                foreach (TreeViewItem item in current.Children)
                    Console.WriteLine("\t" + item);
                Console.Write("\n:: TYPE COMMAND ::\n:> ");

                command = Console.ReadLine();

                switch (command)
                {
                    case "help":
                        showHelp = true;
                        break;

                    case "exit":
                        return;

                    case "back":
                        if (stack.Count == 0)
                            errorMessage = "You are on root!";
                        else
                            current = stack.Pop();
                        break;

                    default:
                        stack.Push(current);
                        try
                        {
                            current = current.Children.First(i => i.Name.Equals(command));
                        }
                        catch (InvalidOperationException)
                        { errorMessage = "There is no such type!"; }
                        break;
                }

                current.IsExpanded = true;
            }

        }

        private static void SetUpConsole(string currentName, int size, bool help, string errorMessage)
        {
            Console.Clear();

            if(!errorMessage.IsNullOrEmpty())
            {
                Console.WriteLine(":# ERROR: " + errorMessage + "\n");
                help = true;
            }

            if(help)
            {
                string back = "\n   ~  back  - u can go back to preveius branch {" + size + "} times";
                if (size == 0)
                    back = "\n   ~  back  - u can't go back now, you are on root";

                Console.WriteLine(":: AVALIABLE COMMANDS ::" +
                                  "\n   ~ [type] - specify type to expand" +
                                  back + "\n   ~  exit  - close program" +
                                  "\n   ~  help  - show help\n" );
            }

            Console.WriteLine(":: CURRENT TREE ::\n" + currentName);
       */ }
    }
}