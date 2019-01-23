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
            VMViewModel viewModel = new VMViewModel();
            viewModel.OpenButton.Execute(null);

            Stack<TreeViewItem> stack = new Stack<TreeViewItem>();
            TreeViewItem current = viewModel.HierarchicalAreas[0];
            current.IsExpanded = true;

            string command;
            string errorMessage = null;
            bool showHelp = true;

            while (true)
            {
                SetUpConsole(current.Name, stack.Count, showHelp, errorMessage);
                errorMessage = null;
                foreach (TreeViewItem item in current.Children)
                    Console.WriteLine("\t" + item);
                Console.Write("\n:: TYPE COMMAND ::\n:> ");

                command = Console.ReadLine();

                switch (command)
                {
                    case "back":
                        if (stack.Count == 0)
                            errorMessage = "You are on root!";
                        else
                            current = stack.Pop();
                        break;

                    case "exit":
                        return;

                    case "help":
                        showHelp = !showHelp;
                        break;

                    case "load":
                        viewModel.OpenButton.Execute(null);
                        break;

                    case "save":
                        viewModel.SaveButton.Execute(null);
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
            }

            if(help)
            {
                string back = "\n   ~  back  - u can go back to preveius branch {" + size + "} times";
                if (size == 0)
                    back = "\n   ~  back  - u can't go back now, you are on root";

                Console.WriteLine(":: AVALIABLE COMMANDS ::" +
                                  "\n   ~ [type] - specify type to expand" +
                                  back + "\n   ~  exit  - close program" +
                                  "\n   ~  help  - hide help (or show when it is hidden)" +
                                  "\n   ~  load  - load another file" +
                                  "\n   ~  save  - save file to XML\n" );
            }

            Console.WriteLine(":: CURRENT TREE ::\n" + currentName);
        }
    }
}