// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankTransaction.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The bank transaction model class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneyLaundrymat
{
    using System;

    /// <summary>
    /// The bank transaction model class.
    /// </summary>
    public class BankTransaction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount"/> class.
        /// </summary>
        /// <param name="fromAccountId">The from account identifier.</param>
        /// <param name="toAccountId">The to account identifier.</param>
        /// <param name="amount">The amount.</param>
        public BankTransaction(long fromAccountId, long toAccountId, decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(nameof(amount));
            }

            this.FromAccountId = fromAccountId;
            this.ToAccountId = toAccountId;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets the from account identifier.
        /// </summary>
        public long FromAccountId { get; }

        /// <summary>
        /// Gets the to account identifier.
        /// </summary>
        public long ToAccountId { get; }

        /// <summary>
        /// Gets the amount.
        /// </summary>
        public decimal Amount { get; }
    }
}