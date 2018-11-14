using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///   This is the categories for each budget
    /// </summary>
    public class BudgetItem
    {
        /// <summary>
        ///   Primary Id for each Budget Item
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///   Each budget item is associated with a budget.
        /// </summary>
        int BudgetId { get; set; }

        /// <summary>
        ///   How much money is available for transaction to use from this budget item
        /// </summary>
        decimal Amount { get; set; }

        /// <summary>
        ///   Name of this Budget (ex. Groceries)
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///   If you need to have more descriptive information available.
        /// </summary>
        string Description { get; set; }
    }
}