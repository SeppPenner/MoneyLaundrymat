namespace MoneyLaundrymat
{
    using System;

    public static class RandomExtensions
    {
        public static decimal NextDecimal(this Random random)
        {
            return Convert.ToDecimal(random.NextDouble());
        }

        public static decimal NextDecimal(this Random random, decimal minimum, decimal maximum)
        {
            return (random.NextDecimal() * (maximum - minimum)) + minimum;
        }
    }
}