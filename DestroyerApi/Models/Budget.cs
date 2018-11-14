using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///   This is primary budget for the house.
    ///   Can have multiple Budgets.
    /// </summary>
    public class Budget
    {
        /// <summary>
        ///   Primary Id for the budget
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///   This is the household Id for which this budget it associated with
        /// </summary>
        int HouseholdId { get; set; }

        /// <summary>
        ///   This is the Title or Name of this budget
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///   If you need to make any notes about this budget, add here.
        /// </summary>
        string Descriptions { get; set; }
    }
}