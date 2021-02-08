// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hämmer Electronics">
//   Copyright (c) All rights reserved.
// </copyright>
// <summary>
//   The main program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MoneyLaundrymat
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The main program.
    /// </summary>
    public static class Program
    { 
        /// <summary>
        /// The bank accounts.
        /// </summary>
        private static readonly List<BankAccount> Accounts = new List<BankAccount>();

        /// <summary>
        /// The bank transactions.
        /// </summary>
        private static readonly List<BankTransaction> Transactions = new List<BankTransaction>();

        /// <summary>
        /// The random generator.
        /// </summary>
        private static readonly Random Random = new Random();

        /// <summary>
        /// The main method.
        /// </summary>
        private static void Main()
        {
            try
            {
                PrepareAccounts();
                RunSimulation();
                SaveChartToFile();
                End();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        /// <summary>
        /// Prepares the accounts to transfer money.
        /// </summary>
        private static void PrepareAccounts()
        {
            var account1 = new BankAccount(1, AccountType.Start);
            Accounts.Add(account1);

            var account2 = new BankAccount(2, AccountType.Normal);
            Accounts.Add(account2);

            var account3 = new BankAccount(3, AccountType.Normal);
            Accounts.Add(account3);

            var account4 = new BankAccount(4, AccountType.Normal);
            Accounts.Add(account4);

            var account5 = new BankAccount(5, AccountType.Normal);
            Accounts.Add(account5);

            var account6 = new BankAccount(6, AccountType.Normal);
            Accounts.Add(account6);

            var account7 = new BankAccount(7, AccountType.End);
            Accounts.Add(account7);
        }

        /// <summary>
        /// Runs the simulation.
        /// </summary>
        private static void RunSimulation()
        {
            // Get the start account and add some money
            var startAccount = Accounts.First(a => a.AccountType == AccountType.Start);
            startAccount.AddMoney(1000000);

            // Do some transactions to bank accounts while the start account has money
            while (startAccount.Amount > 0)
            {
                // Get all random items
                var amount = Random.NextDecimal(0, startAccount.Amount);
                var toAccounts = Accounts
                    .Where(a => a.AccountType != AccountType.Start && a.AccountType != AccountType.End).ToList();
                var index = Random.Next(toAccounts.Count);
                var toAccount = toAccounts.ElementAt(index);

                // Transfer some money
                if (amount < 0)
                {
                    continue;
                }

                startAccount.SubtractMoney(amount);
                toAccount.AddMoney(amount);
                var transaction = new BankTransaction(startAccount.Id, toAccount.Id, amount);
                Transactions.Add(transaction);
                Console.WriteLine($"{startAccount.Id} --> {toAccount.Id}: {amount} €");
            }

            // Run while we have money on the other accounts and transfer our money around
            while (Accounts.Where(a => a.AccountType != AccountType.Start && a.AccountType != AccountType.End).Any(a => a.Amount > 0))
            {
                // Get all random items
                var fromAccounts = Accounts.Where(
                    a => a.AccountType != AccountType.Start && a.AccountType != AccountType.End).ToList();
                var fromIndex = Random.Next(fromAccounts.Count);
                var fromAccount = fromAccounts.ElementAt(fromIndex);

                var toAccounts = Accounts
                    .Where(a => a.AccountType != AccountType.Start).ToList();
                var toIndex = Random.Next(toAccounts.Count);
                var toAccount = toAccounts.ElementAt(toIndex);

                var amount = Random.NextDecimal(0, fromAccount.Amount);

                // Transfer some money
                if (amount < 0)
                {
                    continue;
                }

                if (fromAccount.Id is toAccount.Id)
                {
                    continue;
                }

                fromAccount.SubtractMoney(amount);
                toAccount.AddMoney(amount);
                var transaction = new BankTransaction(fromAccount.Id, toAccount.Id, amount);
                Transactions.Add(transaction);
                Console.WriteLine($"{fromAccount.Id} --> {toAccount.Id}: {amount} Euro");
            }
        }

        /// <summary>
        /// Saves the chart to a file.
        /// </summary>
        private static void SaveChartToFile()
        {
            var sb = new StringBuilder();
            sb.AppendLine("@startuml");
            sb.AppendLine(string.Empty);
            sb.AppendLine("skinparam componentStyle uml2");

            foreach (var account in Accounts)
            {
                if (account.AccountType is AccountType.Start)
                {
                    sb.AppendLine($"component \"Start\" as Account{account.Id} {{");
                    sb.AppendLine("}");
                }
                else if (account.AccountType is AccountType.End)
                {
                    sb.AppendLine($"component \"End\" as Account{account.Id} {{");
                    sb.AppendLine("}");
                }
                else
                {
                    sb.AppendLine($"component \"Account {account.Id}\" as Account{account.Id} {{");
                    sb.AppendLine("}");
                }
            }

            foreach (var group in Transactions.GroupBy(t => new { From = t.FromAccountId, To = t.ToAccountId }))
            {
                var sum = group.Sum(i => i.Amount);
                sb.AppendLine($"file \"{sum} Euro\" as {group.Key.From}To{group.Key.To}");
                sb.AppendLine($"Account{group.Key.From} --> {group.Key.From}To{group.Key.To}");
                sb.AppendLine($"{group.Key.From}To{group.Key.To} --> Account{group.Key.To}");
            }

            sb.AppendLine("@enduml");

            File.WriteAllText("result.wsd", sb.ToString());
        }

        /// <summary>
        /// Writes the end messages and stops the console from closing.
        /// </summary>
        private static void End()
        {
            Console.WriteLine("The simulation has stopped, see result.wsd in the executing directory for the result.");
            Console.WriteLine("Press any key to close.");
            Console.ReadLine();
        }
    }
}