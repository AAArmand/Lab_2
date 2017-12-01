using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI {
    public class CommandsInit {
        public List<ICommand> Options { get; }

        public static bool ValidateObject(Type objectType) {
            foreach (Type obj in objectType.GetInterfaces()) {
                if (obj == typeof(ICommand)) {
                    if (!objectType.IsAbstract)
                        return true;
                }
            }
            return false;
        }

        public CommandsInit() {
            Options = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(ValidateObject)
                .Select(Activator.CreateInstance)
                .Cast<ICommand>()
                .ToList();
        }
    }
}
