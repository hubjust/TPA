using System;
using System.Collections.Generic;
using System.Reflection;
using ViewModel.ViewModelMetadata;
using Model;

namespace ConsoleView
{
    class Program
    {
        private static AssemblyMetadata assemblyMetadata;
        private static string selectedNamespace;

        private static Stack<TypeMetadata> stack = new Stack<TypeMetadata>();
        private static Dictionary<string, TypeMetadata> expandableTypes = new Dictionary<string, TypeMetadata>();
        private static Dictionary<string, NamespaceMetadata> namespaces = new Dictionary<string, NamespaceMetadata>();

        static void Main(string[] args)
        {
            string path = @"..\..\..\View\bin\Debug\ViewModel.dll";
            Assembly assembly = Assembly.LoadFrom(path);
            assemblyMetadata = new AssemblyMetadata(assembly);

            Console.WriteLine(":: CHOOSE NAMESPACE ::\n");
            foreach (NamespaceMetadata namespac in assemblyMetadata.Namespaces)
            {
                namespaces.Add(namespac.Name, namespac);
                Console.WriteLine(namespac.Name);
            }

            Console.Write("\n:: > ");

            string input = Console.ReadLine();

            if (input == null)
                Console.WriteLine("No input from user!");

            else
                selectedNamespace = input;

            ListTypes(input);

            while(true)
            {
                Console.Write("\n:: > ");

                input = Console.ReadLine();

                if (input == null)
                {
                    Console.WriteLine("Input empty!");
                    continue;
                }

                string command = input.Split(' ')[0];

                switch (command)
                {
                    case "1":
                        if (input.Split(' ').Length == 2)
                        {
                            string selectedType = input.Split(' ')[1];

                            if (expandableTypes.ContainsKey(selectedType))
                                ExpandType(selectedType);
                            else
                                Console.WriteLine("Invalid type name");
                        }
                        else
                            Console.WriteLine("Specify type.");
                        break;

                    case "2":
                        GoBack();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }
            }
        }

        private static void SetUpConsole()
        {
            Console.Clear();
            expandableTypes.Clear();

            Console.WriteLine("AVALIABLE COMMANDS: 1 [type] - list, 2 - back, 3 - exit\n");
        }

        private static void ListTypes(string namespac)
        {
            SetUpConsole();

            Console.WriteLine("Stored types: " + TypeMetadata.DictionaryType.Count);
            foreach (TypeMetadata storedType in namespaces[namespac].Types)
            {
                Console.WriteLine(new ViewModelTypeMetadata(storedType));
                expandableTypes.Add(storedType.Name, storedType);
            }
        }

        private static void GoBack()
        {
            if (stack.Count > 0)
            {
                if (stack.Count == 1)
                {
                    stack.Clear();
                    ListTypes(selectedNamespace);
                }
                else
                {
                    stack.Pop();
                    ExpandType(stack.Pop().Name);
                }
            }
        }

        private static void ExpandType(string typeName)
        {
            TypeMetadata type = TypeMetadata.DictionaryType[typeName];
            stack.Push(type);

            SetUpConsole();

            foreach (ParameterMetadata field in type.Fields)
            {
                string fieldTypeName = field.Type.Name;
                if (!expandableTypes.ContainsKey(fieldTypeName))
                {
                    expandableTypes.Add(fieldTypeName, field.Type);
                }
            }

            foreach (PropertyMetadata property in type.Properties)
            {
                string propertyTypeName = property.Type.Name;
                if (!expandableTypes.ContainsKey(propertyTypeName))
                {
                    expandableTypes.Add(propertyTypeName, property.Type);
                }
            }


            foreach (MethodMetadata method in type.Methods)
            {
                string returnTypeName = method.ReturnType.Name;
                if (!expandableTypes.ContainsKey(returnTypeName))
                {
                    expandableTypes.Add(returnTypeName, method.ReturnType);
                }

                foreach (ParameterMetadata parameter in method.Parameters)
                {
                    string parameterTypeName = parameter.Type.Name;
                    if (!expandableTypes.ContainsKey(parameterTypeName))
                    {
                        expandableTypes.Add(parameterTypeName, parameter.Type);
                    }
                }
            }

            Console.WriteLine("Stored types: " + TypeMetadata.DictionaryType.Count);
            Console.WriteLine(new ConsoleViewModel(type));
        }
    }
}
