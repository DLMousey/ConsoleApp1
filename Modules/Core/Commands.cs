using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ConsoleApp1.Interfaces.Core;

namespace ConsoleApp1.Modules.Core
{
    class Commands
    {
        /* 
         * Calls the Register() function in a Command class
         */
        public static void Load()
        {
            foreach (string item in GetCommands())
            {
                ICommand command = CreateInstance<ICommand>(item);
                command.Register();
            }
        }

        /* 
         * Creates a new instance of a class by its name
         */
        private static I CreateInstance<I>(string item) where I : class
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = assembly.GetTypes()
                .First(t => t.Name == item);
            return Activator.CreateInstance(type) as I;
        }

        /* 
         * Returns a list of classes using the ICommand interface
         */
        private static List<string> GetCommands()
        {
            return AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(ICommand).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(x => x.Name).ToList();
        }
    }
}