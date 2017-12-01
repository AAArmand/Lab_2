using Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI {
    public class DivCommand : ICommand {

        public string Name { get { return "div"; } }

        private Rational[] GetRationals(string[] numbers) {
            try {
                Rational[] rationals = new Rational[2];
                int i = 0;
                foreach (string number in numbers) {
                    bool format = Rational.TryParse(number, out rationals[i]);
                    i++;
                    if (!format) {
                        throw new FormatException("Число " + number + " не соответствует формату, пожалуйста введите его заново");
                    }
                }

                if ((rationals[1].Numerator == 0) && (rationals[1].Base == 0)) {
                    throw new DivideByZeroException("Вы пытаетесь разделить на ноль, не надо так");
                }
                return rationals;

            } catch (FormatException error) {
                Console.WriteLine(error.Message);
            } catch (DivideByZeroException error) {
                Console.WriteLine(error.Message);
            }

            return null;
        }

        public void Execute(params string[] inputParametrs) {
            string[] numbers = { inputParametrs[0], inputParametrs[1] };

            try {
                Rational[] rationals = GetRationals(numbers);
                Console.WriteLine(rationals[0] / rationals[1]);
            } catch (NullReferenceException) { }
        }
    }
}
