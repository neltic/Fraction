using System;

namespace Number
{

    /// <summary>
    /// Struct for using fractions
    /// </summary>
    public struct Fraction : IComparable
    {
        private int numerator;
        private int denominator;

        /// <summary>
        /// The value of zero represented as a fraction
        /// </summary>
        public static Fraction Zero => new Fraction(0);
        /// <summary>
        /// Initialize a fraction with an integer
        /// </summary>
        /// <param name="numerator">numerator</param>
        public Fraction(int numerator)
        {
            this.numerator = numerator;
            this.denominator = 1;
        }
        /// <summary>
        /// Initialize a fraction
        /// </summary>
        /// <param name="numerator">numerator</param>
        /// <param name="denominator">denominator</param>
        /// <exception cref="DivideByZeroException"></exception>
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException();
            }
            this.numerator = numerator;
            this.denominator = denominator;
            this.Simplify();
        }
        /// <summary>
        /// Create a matrix with a default value
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <returns></returns>
        public static Fraction[,] GetMatrix(int row, int column)
        {
            return GetMatrix(row, column, Zero);
        }
        public static Fraction[,] GetMatrix(int row, int column, Fraction defaultValue)
        {
            Fraction[,] matrix = new Fraction[row, column];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = defaultValue;
                }
            }
            return matrix;
        }
        /// <summary>
        /// Create an array with a default value
        /// </summary>
        /// <param name="length">number of elements</param>
        /// <returns></returns>
        public static Fraction[] GetArray(int length)
        {
            return GetArray(length, Zero);
        }
        public static Fraction[] GetArray(int length, Fraction defaultValue)
        {
            Fraction[] array = new Fraction[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = defaultValue;
            }
            return array;
        }
        /// <summary>
        /// Implicit conversion from an integer to a fraction
        /// </summary>
        /// <param name="value">value</param>
        public static implicit operator Fraction(int value)
        {
            return new Fraction(value);
        }
        /// <summary>
        /// The positive value
        /// </summary>
        /// <param name="a">Fraction a</param>
        /// <returns>The same fraction</returns>
        public static Fraction operator +(Fraction a) => a;
        /// <summary>
        /// The negative value of a fraction
        /// </summary>
        /// <param name="a">Fraction a</param>
        /// <returns>The negative value of a fraction</returns>
        public static Fraction operator -(Fraction a) => new Fraction(-a.numerator, a.denominator);
        /// <summary>
        /// The sum of two fractions
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns>a+b</returns>
        public static Fraction operator +(Fraction a, Fraction b)
        {
            int mcm = LeastCommonMultiple(a.denominator, b.denominator);
            return new Fraction(a.numerator * (mcm / a.denominator) + b.numerator * (mcm / b.denominator), mcm);
        }
        /// <summary>
        /// The subtraction of two fractions
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns>a-b</returns>
        public static Fraction operator -(Fraction a, Fraction b)
            => a + (-b);
        /// <summary>
        /// The product of two fractions
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns>a*b</returns>
        public static Fraction operator *(Fraction a, Fraction b)
            => new Fraction(a.numerator * b.numerator, a.denominator * b.denominator);
        /// <summary>
        /// The division of two fractions
        /// </summary>
        /// <param name="a">a</param>
        /// <param name="b">b</param>
        /// <returns>a/b</returns>
        public static Fraction operator /(Fraction a, Fraction b)
            => new Fraction(a.numerator * b.denominator, a.denominator * b.numerator);

        /// <summary>
        /// Compare the equality of two fractions
        /// </summary>
        /// <param name="left">a</param>
        /// <param name="right">b</param>
        /// <returns>a==b</returns>
        public static bool operator ==(Fraction left, Fraction right)
        {
            return left.CompareEquality(right, false);
        }

        /// <summary>
        /// Compare the inequality of two fractions
        /// </summary>
        /// <param name="left">a</param>
        /// <param name="right">b</param>
        /// <returns>a!=b</returns>
        public static bool operator !=(Fraction left, Fraction right)
        {
            return left.CompareEquality(right, true);
        }

        /// <summary>
        /// Compare if 'a' is less than 'b'
        /// </summary>
        /// <param name="left">a</param>
        /// <param name="right">b</param>
        /// <returns>'a' less than 'b'</returns>
        public static bool operator <(Fraction left, Fraction right)
        {
            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Compare if 'a' is greater than 'b'
        /// </summary>
        /// <param name="left">a</param>
        /// <param name="right">b</param>
        /// <returns>'a' is greater than 'b'</returns>
        public static bool operator >(Fraction left, Fraction right)
        {
            return left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Compare if 'a' is less than or equal 'b'
        /// </summary>
        /// <param name="left">a</param>
        /// <param name="right">b</param>
        /// <returns>'a' is less than or equal 'b'</returns>
        public static bool operator <=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Compare if 'a' is greater than or equal 'b'
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>'a' is greater than or equal 'b'</returns>
        public static bool operator >=(Fraction left, Fraction right)
        {
            return left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compare an object with the fraction
        /// </summary>
        /// <param name="obj">Object</param>
        /// <returns>object==fraction</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Fraction))
                return false;

            try
            {
                Fraction right = (Fraction)obj;
                return this.CompareEquality(right, false);
            }
            catch
            {
                // can't throw in an Equals!
                return false;
            }
        }

        public override int GetHashCode()
        {
            int numeratorHash = this.numerator.GetHashCode();
            int denominatorHash = this.denominator.GetHashCode();
            return (numeratorHash ^ denominatorHash);
        }
                
        /// <summary>
        /// Convert the fraction as a string
        /// </summary>
        /// <returns>The fraction as a string</returns>
        public override string ToString()
        {
            if (numerator != 0 && numerator != denominator && denominator != 1)
            {
                return $"{numerator}/{denominator}";
            }
            else if (denominator == 1 && numerator != denominator)
            {
                return $"{numerator}";
            }
            else if (numerator == 0)
            {
                return "0";
            }
            else
            {
                return "1";
            }
        }

        /// <summary>
        /// Convert the fraction to double
        /// </summary>
        /// <returns>double</returns>
        public double ToDouble()
        {
            return numerator / (double)denominator;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;   // null is less than anything

            Fraction right;

            if (obj is Fraction)
                right = (Fraction)obj;
            else
                throw new ArgumentException("Must be convertible to Fraction", "obj");

            return this.CompareTo(right);
        }

        public int CompareTo(Fraction right)
        {
            Simplify();
            right.Simplify();

            try
            {
                if (this.denominator == 0 || right.denominator == 0)
                {
                    throw new Exception("Denominator can't be zero");
                }

                int mcm = LeastCommonMultiple(this.denominator, right.denominator);
                int n1 = this.numerator * (mcm / this.denominator);
                int n2 = right.numerator * (mcm / right.denominator);

                checked
                {
                    if (n1 < n2)
                        return -1;
                    else if (n1 > n2)
                        return 1;
                    else
                        return 0;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"CompareTo({this}, {right}) error", e);
            }
        }

        private bool CompareEquality(Fraction right, bool notEqualCheck)
        {
            Simplify();

            right.Simplify();

            if (this.numerator == right.numerator && this.denominator == right.denominator)
            {
                return !notEqualCheck;
            }
            else
            {
                return notEqualCheck;
            }
        }

        private static int GreatestCommonDivisor(int num1, int num2)
        {
            int mcd;
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);
            int a = Math.Max(num1, num2);
            int b = Math.Min(num1, num2);
            do
            {
                mcd = b;
                b = a % b;
                a = mcd;
            } while (b != 0);
            return mcd;
        }

        private static int LeastCommonMultiple(int num1, int num2)
        {
            num1 = Math.Abs(num1);
            num2 = Math.Abs(num2);
            int a = Math.Max(num1, num2);
            int b = Math.Min(num1, num2);
            return (a / GreatestCommonDivisor(a, b)) * b;
        }

        private void Simplify()
        {
            // law of signs
            int sign = 1;
            if ((numerator < 0 && denominator > 0) || (numerator > 0 && denominator < 0))
            {
                sign = -1;
            }
            if (numerator < 0) { numerator *= -1; }
            if (denominator < 0) { denominator *= -1; }
            // reduction if necessary
            if (numerator != 0)
            {
                int mcd = denominator != 0 ? GreatestCommonDivisor(numerator, denominator) : 1;
                numerator /= mcd * sign;
                denominator /= mcd;
            }
            else
            {
                denominator = 1;
            }
        }
    }
}
