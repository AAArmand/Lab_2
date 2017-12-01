using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Operations {
    public class Rational {
        /// <summary>
        /// Числитель дроби
        /// </summary>
        public int Numerator { get; set; }

        /// <summary>
        /// Знаменатель дроби
        /// </summary>
        public int Denominator { get; set; }

        /// <summary>
        /// Целая часть числа Z.N:D, Z получается делением числителя на знаменатель и
        /// отбрасыванием остатка
        /// </summary>
        public int Base { get {
                return this.Numerator / this.Denominator;
            }
        }

        /// <summary>
        /// Дробная часть числа Z.N:D, N:D
        /// </summary>
        public Rational Fraction { get {
                return new Rational (
                    this.Numerator % this.Denominator,
                    this.Denominator
                );
            }
        }

        public Rational(int N = 0, int D = 1) {
            Numerator = N;
            Denominator = D;
        }

        /// <summary>
        /// Нахождение НОД двух целых чисел
        /// </summary>
        /// <returns>
        /// НОД чисел a и b
        /// </returns>
        private int GreatestCommonDivisor(int a, int b) {

            while (a != b) {
                if (a > b) {
                    a = a - b;
                } else {
                    b = b - a;
                }
            }
            return a;
        }

        private int LeastCommonMultiple(int a, int b) {
            return (a * b / GreatestCommonDivisor(a, b));
        }

        /// <summary>
        /// Находит дополнительный множитель для числителя той дроби,
        /// чей знаменатель передан в качестве первого параметра
        /// </summary>
        /// <param name="firstSumDenominator">
        /// Знаменатель первой дроби
        /// </param>
        /// <param name="secondSumDenominator">
        /// Знаменатель второй дроби
        /// </param>
        /// <returns>
        /// Дополнительный множитель первой для первой дроби
        /// </returns>
        private int AdditionalFactor(int firstSumDenominator, int secondSumDenominator) {
            return (LeastCommonMultiple(firstSumDenominator, secondSumDenominator) 
                / firstSumDenominator);
        }

/// <summary>
/// Возвращает числитель результирующей дроби
/// после операции сложения
/// </summary>
/// <param name="a">
/// Первое слагаемое
/// </param>
/// <param name="b">
/// Второе слагаемое
/// </param>
/// <returns>
/// Числитель полученной после сложения дроби 
/// </returns>
private int GetSummNumerator(Rational a, Rational b) {
            return a.Numerator * 
                AdditionalFactor(a.Denominator, b.Denominator) + 
                b.Numerator *
                AdditionalFactor(b.Denominator, a.Denominator);
        }

        /// <summary>
        /// Операция сложения, возвращает новый объект - рациональное число,
        /// которое является суммой чисел c и this
        /// </summary>
        public Rational Add(Rational c) {

            Rational SummValue = new Rational(
                GetSummNumerator(this, c),
                LeastCommonMultiple(this.Denominator, c.Denominator)
                );
            
            SummValue.Even();

            return SummValue;
        }

        /// <summary>
        /// Операция смены знака, возвращает новый объект - рациональное число,
        /// которое являтеся разностью числа 0 и this
        /// </summary>
        public Rational Negate() {
            this.Numerator = - this.Numerator;
            return this;
        }

        /// <summary>
        /// Операция умножения, возвращает новый объект - рациональное число,
        /// которое является результатом умножения чисел x и this
        /// </summary>
        public Rational Multiply(Rational x) {
            Rational MultValue = new Rational (
                this.Numerator * x.Numerator,
                this.Denominator * x.Denominator
                );
            
            MultValue.Even();

            return MultValue;
        }

        /// <summary>
        /// Операция деления, возвращает новый объект - рациональное число,
        /// которое является результатом деления this на x
        /// </summary>
        public Rational DivideBy(Rational x) {
            int temp = x.Denominator;
            if (x.Numerator < 0) {
                x.Denominator = Math.Abs(x.Numerator);
                x.Numerator = - temp;
            } else {
                x.Denominator = x.Numerator;
                x.Numerator = temp;
            }

            Rational DivValue = this.Multiply(x);

            DivValue.Even();
            return DivValue;
        }

        /// <summary>
        /// Вовзращает строковое представление числа в виде Z.N:D, где
        /// Z — целая часть, N и D — целые числа, числитель и знаменатель, N < D
        /// '.' — символ, отличающий целую часть от дробной,
        /// ':' — символ, обозначающий знак деления
        /// если числитель нацело делится на знаменатель, то
        /// строковое представление не отличается от целого числа
        /// (исчезает точка и всё, что после точки)
        /// Если Z = 0, опускается часть представления до точки включительно
        /// </summary>
        public override string ToString() {
            
            string number;
            
            if (this.Base == 0) {
                if (this.Numerator != 0) {
                    number = String.Format("{0}:{1}", this.Numerator, this.Denominator);
                } else {
                    number = "0";
                }
            } else {
                if (this.Fraction.Numerator == 0) {
                    number = String.Format("{0}", this.Base);
                } else {
                    number = String.Format("{0}.{1}:{2}", this.Base, Math.Abs(this.Fraction.Numerator), this.Denominator);
                }
            }

            return number;
        }

        /// <summary>
        /// Создание экземпляра рационального числа из строкового представления Z.N:D
        /// допускается N > D, также допускается
        /// Строковое представления рационального числа
        /// Результат конвертации строкового представления в рациональное
        /// число
        /// true, если конвертация из строки в число была успешной,
        /// false если строка не соответствует формату
        /// </summary>
        
        
        public static bool TryParse(string input, out Rational result) {
            result = null;
            int[] number;
            string[] strNumber;

            try {
                if (input.Split('.',':').Length > 0) {
                    strNumber = input.Split('.', ':');
                    number = new int[]{
                        Int32.Parse(strNumber[0]),
                        Int32.Parse(strNumber[1]),
                        Int32.Parse(strNumber[2])
                    };

                    if (number[2] == 0) {
                        throw new DivideByZeroException();
                    }

                    if ((number[1] < 0) || (number[2] < 0)) {
                        throw new FormatException();
                    }

                    if (number[0] < 0) {
                        result = new Rational(number[2] * number[0] - number[1], number[2]);
                    } else {
                        result = new Rational(number[2] * number[0] + number[1], number[2]);
                    }
                    
                    
                } else if (input.Split(':').Length > 0){
                    strNumber = input.Split(':');

                    number = new int[]{
                        Int32.Parse(strNumber[0]),
                        Int32.Parse(strNumber[1])
                    };

                    if (number[1] == 0) {
                        throw new DivideByZeroException();
                    }

                    if (number[1] < 0) {
                        throw new FormatException();
                    }

                    result = new Rational(number[0], number[1]);

                } else {
                    result = new Rational(Math.Abs(Int32.Parse(input)), 1);
                }
            } catch (ArgumentException) {
                return false;
            } catch (FormatException) {
                return false;
            } catch (OverflowException) {
                return false;
            } catch (DivideByZeroException) {
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// Приведение дроби - сокращаем дробь на общие делители числителя
        /// и знаменателя. 
        /// Вызывается реализацией после каждой арифметической операции
        /// </summary>
        private void Even() {
            if (this.Numerator != 0) {
                int GCD = GreatestCommonDivisor(Math.Abs(this.Numerator), Math.Abs(this.Denominator));
                this.Numerator /= GCD;
                this.Denominator /= GCD;
            }   
        }

        public static Rational operator +(Rational a, Rational b) {
            return a.Add(b);
        }

        public static Rational operator *(Rational a, Rational b) {
            return a.Multiply(b);
        }

        public static Rational operator -(Rational a, Rational b) {
            return a.Add(b.Negate());
        }

        public static Rational operator /(Rational a, Rational b) {
            return a.DivideBy(b);
        }

        public static implicit operator Rational(int a) {
            return new Rational(a,1);
        }

        public static explicit operator int(Rational c) {
            return c.Base;
        }
        
    }
}
