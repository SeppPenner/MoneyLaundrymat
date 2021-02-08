// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RandomExtensions.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   A class that contains extension methods for the <see cref="Random" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneyLaundrymat
{
    using System;

    /// <summary>
    /// A class that contains extension methods for the <see cref="Random" /> class.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Gets the next <see cref="decimal"/> value.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <returns>The next <see cref="decimal"/> value.</returns>
        public static decimal NextDecimal(this Random random)
        {
            return Convert.ToDecimal(random.NextDouble());
        }

        /// <summary>
        /// Gets the next <see cref="decimal"/> value between the bounds.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <param name="minimum">The minimum value.</param>
        /// <param name="maximum">The maximum value.</param>
        /// <param name="decimals">The decimals.</param>
        /// <returns>The next <see cref="decimal"/> value between the bounds.</returns>
        public static decimal NextDecimal(this Random random, decimal minimum, decimal maximum, int decimals = 2)
        {
            var value = (random.NextDecimal() * (maximum - minimum)) + minimum;
            return Math.Round(value, decimals);
        }
    }
}