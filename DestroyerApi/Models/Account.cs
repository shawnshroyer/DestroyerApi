using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///   This is the Bank Account or pool of money that transaction will pull from
    /// </summary>
    public class Account
    {
        /// <summary>
        ///   Primary Id for the Account being used
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///   The account must be associate with a house.
        /// </summary>
        public int HouseholdId { get; set; }

        /// <summary>
        ///   This is the name of the Account (ex. Checking)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   This is the amount availabe within the account.
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        ///   This will be bank balance based on the Reconciled Amount
        /// </summary>
        public decimal ReconBalance { get; set; }

        /// <summary>
        ///   This will allow us to delete an account, but keep for safety.
        ///   The database entery for account will not be removed.
        /// </summary>
        public bool DeleteAccount { get; set; }
    }
}