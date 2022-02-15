// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BankAccount.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The bank account model class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneyLaundrymat;

/// <summary>
/// The bank account model class.
/// </summary>
public class BankAccount
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BankAccount"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="accountType">The account type.</param>
    public BankAccount(long id, AccountType accountType)
    {
        this.Id = id;
        this.AccountType = accountType;
    }

    /// <summary>
    /// Gets the identifier.
    /// </summary>
    public long Id { get; }

    /// <summary>
    /// Gets the amount.
    /// </summary>
    public decimal Amount { get; private set; }

    /// <summary>
    /// Gets the account type.
    /// </summary>
    public AccountType AccountType { get; }

    /// <summary>
    /// Adds money to the account.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void AddMoney(decimal amount)
    {
        if (amount < 0)
        {
            return;
        }

        this.Amount += amount;
    }

    /// <summary>
    /// Subtracts money from the account.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void SubtractMoney(decimal amount)
    {
        if (amount < 0)
        {
            return;
        }

        if (amount > this.Amount)
        {
            return;
        }

        this.Amount -= amount;
    }
}
