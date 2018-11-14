using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///     Responsible for keeping track of individual transactions.
    ///     There should already be a house account for this transaction to belong to.
    ///     It is assumed that there is a budget Item this can be tied to.
    /// </summary>
    public class Transaction
    {
        /// <summary>
        ///   This is the Primary Key for the Transaction
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///   This is the house or bank account this transaction will be tied to
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        ///   This type of transaction taking place
        ///      3. Deposit
        ///      4. Withrdraw
        /// </summary>
        public int TransactionTypeId { get; set; }

        /// <summary>
        ///   This is the category of the Budget in which this transaction will belong.
        /// </summary>
        public int BudgetItemId { get; set; }

        /// <summary>
        ///   This is a GUID for the currently loged in user posting the transaction.
        /// </summary>
        public string EnteredById { get; set; }

        /// <summary>
        ///   This Auto Generated DateTime when transaction is inserted.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        ///   How much money was involved with this transaction.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///   When the Transaction has been reonciled with the bank, this will be true
        /// </summary>
        public bool IsReconciled { get; set; }

        /// <summary>
        ///   This will reflect a number reconciled by the bank.
        /// </summary>
        public decimal ReconciledAmount { get; set; }

        /// <summary>
        ///   If the transaction is voided this will be true.
        ///   Use case of user being able to see this, but know it's voided.
        /// </summary>
        public bool VoidTransaction { get; set; }

        /// <summary>
        ///   This is a title or name of the transaction.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   If you wish to go into further details about this transaction.
        /// </summary>
        public string Description { get; set; }
    }
}