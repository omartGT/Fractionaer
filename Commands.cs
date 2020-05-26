namespace OStools
{
    /// <summary>
    /// 
    /// </summary>
    public class Commands
    {
        private (int WholeNumber, int Numerator, int Denominator) Fraction;

        public Commands((int WholeNumber, int Numerator, int Denominator) f)
        {
            Fraction = f;
        }

        public string ToText()
        {
            return $"{Fraction.WholeNumber} {Fraction.Numerator}/{Fraction.Denominator}";

        }
    }
}
