
using Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleUI {
    public class App {
        CommandsInit commandsInit;
        const string POINTER = " >>> ";
        
        public App() {
            commandsInit = new CommandsInit();
        }

        public ICommand FindCommand(string commandName) {
            ICommand returnObject = null;

            foreach (ICommand obj in commandsInit.Options) {
                if (obj.Name == commandName) {
                    returnObject = obj;
                }
            }
            return returnObject;
        }

        public void RunCommand() {
            while (true) {
                Console.Write(POINTER);
                string key = Console.ReadLine();
                string[] cmdInput = key.Split(
                            new char[] { ' ', '\t' },
                            StringSplitOptions.RemoveEmptyEntries
                        );

                try {
                    string option = cmdInput[0];
                    ICommand command = FindCommand(option);


                    if (command == null) {
                        throw new InvalidOperationException("Вы ввели неверную команду, пожалуйста, повторите ввод");
                    }

                    if (cmdInput.Length < 3) {
                        throw new NullReferenceException("Вы ввели не все числа, пожалуйста, повторите ввод");
                    }

                    if (cmdInput.Length > 3) {
                        throw new NullReferenceException("Вы ввели что-то лишнее, пожалуйста, повторите ввод");
                    }

                    command.Execute(cmdInput[1], cmdInput[2]);

                } catch (InvalidOperationException error) {
                    Console.WriteLine(error.Message);
                } catch (NullReferenceException error) {
                    Console.WriteLine(error.Message);
                } catch (FormatException error) {
                    Console.WriteLine(error.Message);
                } catch (IndexOutOfRangeException) {
                    Console.WriteLine("Вы ввели пустую строку, не надо так");
                }
            }
        }

    }
}
