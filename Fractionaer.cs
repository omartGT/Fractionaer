using System;
using System.Linq;

namespace OStools
{
    /// <summary>
    /// Converts decimal number to a tuple representing each part of a fractional number
    /// </summary>
    public static class Fractionaer
    {
        private static int GCD(int a, int b)
        {
            if (b == 0) return a;
            return GCD(b, a % b);
        }

        /// <summary>
        /// Converts decimal number to a fractonal representation
        /// </summary>
        /// <param name="number">Decimal number to be converted to fractonal representation</param>
        /// <param name="simplify">Specify true to reduce fraction, other wise denominator will be a power of 10</param>
        /// <returns>Tuple consisting of whole number, numerator, and denominator</returns>
        public static Commands Convert(
            double number, Boolean simplify = true)
        {
            DecimalOriginal = Math.Round(number, Decimal_Precision);

            Fraction = number == 0 ? (0, 0, 0) : (0, 1, 1);

            number = Math.Round(number * fractionPrecision) / fractionPrecision;
            number = Math.Round(number, AfterDecimalCOunt(1 / (float)Fraction_Precision));//AfterDecimalCOunt(DecimalOriginal));

            if (!number.ToString().Contains('.'))
            {
                Fraction.WholeNumber = (int)number;
                return new Commands(Fraction);
            }

            string s = number.ToString();

            Fraction.WholeNumber = int.Parse(s.Substring(0, number.ToString().IndexOf(".")));
            Fraction.Numerator = DecimalToNumerator(Fraction.WholeNumber, number);
            Fraction.Denominator = DecimalToDenominator(number);

            int gcd = GCD(Fraction.Numerator, Fraction.Denominator);

            if (simplify)
            {
                Fraction.Numerator /= gcd;
                Fraction.Denominator /= gcd;
            }

            (FractionWhole, FractionNumerator, FractionDenominator) = Fraction;

            DecimalResult = Math.Round((double)Fraction.WholeNumber +
                ((double)Fraction.Numerator / (double)Fraction.Denominator),
                AfterDecimalCOunt(DecimalOriginal));
            Command = new Commands(Fraction);

            return (Command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">Decimal number to be converted</param>
        /// <param name="precision">Fractional precision, integer representing 1/nth</param>
        /// <param name="simplify">reduce fraction if possible</param>
        /// <returns></returns>
        public static Commands Convert(double number, int precision, Boolean simplify = true)
        {
            Fraction_Precision = precision;
            return Convert(number);
        }

        /// <summary>
        /// Gets a number as a power of 10, based on the number of digits after the decimal
        /// </summary>
        /// <param name="number">original decimal number</param>
        /// <returns></returns>
        private static int DecimalToDenominator(double number)
        {
            return (int)(1 * Math.Pow(10, AfterDecimalCOunt(number)));
        }

        /// <summary>
        /// Converts decimal portion of the number to a whole number
        /// </summary>
        /// <param name="whole">whole number portion of a decimal</param>
        /// <param name="number">Original decimal number</param>
        /// <returns></returns>
        private static int DecimalToNumerator(int whole, double number)
        {
            if (number <= 0)
                return 0;
            return int.Parse(number.ToString().Replace($"{whole}.", ""));
        }

        public static Commands Command = new Commands(Fraction);

        /// <summary>
        /// Gets number of decimal places after the dcimal
        /// </summary>
        /// <param name="number">Original decimal number</param>
        /// <returns></returns>
        private static int AfterDecimalCOunt(double number)
        {
            string s = number.ToString();
            if (s.Contains("."))
                return s.Substring(s.IndexOf(".")).Count() - 1;
            return 0;
        }

        ///// <summary>
        ///// Returns the string representation of a fraction
        ///// </summary>
        ///// <returns>String representation of a fraction</returns>
        //public static string ToText()
        //{
        //    return $"{Fraction.WholeNumber} {Fraction.Numerator}/{Fraction.Denominator}";
        //}

        /// <summary>
        /// FractionNumerator of a fraction
        /// </summary>
        public static int FractionNumerator { get; set; }

        /// <summary>
        /// FractionDenominator of a fraction
        /// </summary>
        public static int FractionDenominator { get; set; }

        /// <summary>
        /// Original Decimal number, that is to be converted
        /// </summary>
        public static double DecimalOriginal { get; private set; }

        /// <summary>
        /// Number after it has been converted to fraction
        /// </summary>
        public static double DecimalResult { get; private set; }

        /// <summary>
        /// Whole number of a fraction
        /// </summary>
        public static int FractionWhole { get; set; }

        /// <summary>
        /// Precision for rounded decimal number, encase of repeating to avoid overflow, minimum is 1, default is 5
        /// </summary>
        public static int Decimal_Precision
        {
            get => decimalPrecision;
            set => decimalPrecision = value == 0 ? 1 : value;
        }

        /// <summary>
        /// Precision to round number to the closest 1/nth fraction, Ex 16 represents 1/16
        /// </summary>
        public static int Fraction_Precision
        {
            get => fractionPrecision;
            set => fractionPrecision = value == 0 ? 1 : value;
        }

        /// <summary>
        /// Tuple containing all parts of the fraction`
        /// </summary>
        public static (int WholeNumber, int Numerator, int Denominator) Fraction;
        private static int decimalPrecision = 5;
        private static int fractionPrecision = 16;
    }
}
