using ConsoleUI;
using Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_2 {
    class Program {
        static void Main(string[] args) {
            
            Console.OutputEncoding = Encoding.UTF8;
            App app = new App();

            app.RunCommand();

            /*
            Rational number = new Rational(9, 8);
            int x = (int)number;
            Console.WriteLine(x);

            number = x;
            Console.WriteLine(number);
            */
        }
    }
}
