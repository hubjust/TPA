using System;
using System.Collections.Generic;
using System.Reflection;
using ViewModel.ViewModelMetadata;
using Model;
using Logger;
using System.Diagnostics;

namespace ConsoleView
{
    class Program
    {
        private static ITracer tracer = new FileTracer("TextUserInterface.log", TraceLevel.Verbose);

        private static string path = @"..\..\..\View\bin\Debug\ViewModel.dll";
        private static AssemblyMetadata assemblyMetadata;
        private static bool isNamespaceSelected;

        private static Stack<TypeMetadata> stack = new Stack<TypeMetadata>();
        private static Dictionary<string, NamespaceMetadata> namespaces = new Dictionary<string, NamespaceMetadata>();
        private static Dictionary<string, TypeMetadata> expandableTypes = new Dictionary<string, TypeMetadata>();

        static void Main(string[] args)
        {
            tracer.TracerLog(TraceLevel.Info, "Tracer started");
            tracer.TracerLog(TraceLevel.Verbose, "Loading dll");

            Assembly assembly = Assembly.LoadFrom(path);
            assemblyMetadata = new AssemblyMetadata(assembly);

            tracer.TracerLog(TraceLevel.Verbose, "Succesful Loading dll");

            string input = "";
            string message = "";

            while (true)
            {
                ListNamespaces(message);
                input = Read();

                if (namespaces.ContainsKey(input))
                {
                    message = "";
                    isNamespaceSelected = true;
                    ListTypes(input, message);
                }
                else
                {
                    tracer.TracerLog(TraceLevel.Warning, "Invalid namespace");
                    message = "ERROR: Invalid namespace";
                    isNamespaceSelected = false;
                }

                while (isNamespaceSelected == true)
                {
                    string command = Read();

                    switch (command)
                    {
                        case "exit":
                            tracer.TracerLog(TraceLevel.Info, "User closed app");
                            return;

                        case "back":
                            message = "";
                            Back(input);
                            break;

                        default:
                            if (expandableTypes.ContainsKey(command))
                            {
                                message = "";
                                ExpandType(command);
                            }
                            else
                            {
                                tracer.TracerLog(TraceLevel.Warning, "Invalid command or type name entered by user");
                                Console.Write("ERROR: Invalid command / Invalid type name");
                            }
                            break;
                    }
                }
            }
        }

        private static string Read()
        {
            Console.Write("\n::> ");
            return Console.ReadLine();
        }

        private static void SetUpConsole(bool showCommands, string message)
        {
            Console.Clear();
            expandableTypes.Clear();

            if (message != "")
                Console.WriteLine(message + "\n");

            if (showCommands == true)
                Console.WriteLine(":: TYPE COMMAND ::\nAVALIABLE COMMANDS:\n\t[type] - specify type to expand,\n\t{back},\n\t{exit}.\n");
        }

        private static void Back(string input)
        {
            if (stack.Count == 0)
            {
                isNamespaceSelected = false;
            }
            else if (stack.Count == 1)
            {
                stack.Clear();
                ListTypes(input, "");
            }
            else
            {
                stack.Pop();
                ExpandType(stack.Pop().Name);
            }
        }

        private static void ListNamespaces(string message)
        {
            tracer.TracerLog(TraceLevel.Info, "Namespace selection");

            if (message != "")
                message += "\n\n";

            SetUpConsole(false, message + "::CHOOSE NAMESPACE::");

            foreach (NamespaceMetadata namespac in assemblyMetadata.Namespaces)
            {
                if (!namespaces.ContainsKey(namespac.Name))
                    namespaces.Add(namespac.Name, namespac);
                Console.WriteLine(namespac.Name);
            }
        }

        private static void ListTypes(string namespac, string message)
        {
            tracer.TracerLog(TraceLevel.Info, namespac);

            SetUpConsole(true, message);

            Console.WriteLine("Stored types: " + TypeMetadata.DictionaryType.Count);
            foreach (TypeMetadata storedType in namespaces[namespac].Types)
            {
                Console.WriteLine(new VMTypeMetadata(storedType));
                expandableTypes.Add(storedType.Name, storedType);
            }
        }

        private static void ExpandType(string typeName)
        {
            tracer.TracerLog(TraceLevel.Info, typeName);

            TypeMetadata type = TypeMetadata.DictionaryType[typeName];
            stack.Push(type);

            SetUpConsole(true, "");

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
