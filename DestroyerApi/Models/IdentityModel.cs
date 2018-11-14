using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DestroyerApi.Models
{
    /// <summary>
    ///   This is the Class for connections to the database
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        ///   This is telling us wich WebConfig database we will be utilizing.
        /// </summary>
        public ApplicationDbContext() : base("DefaultConnection")
        { }

        /// <summary>
        ///   This is to allow us to access the DB via code.
        /// </summary>
        /// <returns></returns>
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //How do I call a SQL Stored Procedure
        /// <summary>
        ///   Returns the Name of the household with knowon Id.
        /// </summary>
        /// <param name="hhId">Primary Key</param>
        /// <returns>The Household model (name of household)</returns>
        public async Task<Household> GetHousehold(int hhId)
        {
            return await Database.SqlQuery<Household>("GetHousehold @householdId",
                new SqlParameter("housholdId", hhId)).FirstOrDefaultAsync();
        }

        /// <summary>
        ///   Get's all accounts associated with the household
        /// </summary>
        /// <param name="hhId">The Household Id for which to get the accounts</param>
        /// <returns>A list of all accounts based on the household Id</returns>
        public async Task<List<Account>> GetAccounts(int hhId)
        {
            return await Database.SqlQuery<Account>("GetAccounts @householdId", 
                new SqlParameter("householdId", hhId)).ToListAsync();
        }

        /// <summary>
        ///   Gets all transactions related with a specific account
        /// </summary>
        /// <param name="acctId">The Id for the account to pick transactions</param>
        /// <returns>Returns list of all transactions asscociated with said account</returns>
        public async Task<List<Transaction>> GetAccountTransactions(int acctId)
        {
            return await Database.SqlQuery<Transaction>("GetAccountTransactions @accountId",
                new SqlParameter("accountId", acctId)).ToListAsync();
        }

        /// <summary>
        ///   Gets all Transaction associated with the household
        /// </summary>
        /// <param name="hhId">Household Id to associate with Transactions</param>
        /// <returns>Returns list of transactions</returns>
        public async Task<List<Transaction>> GetAllTransactions(int hhId)
        {
            return await Database.SqlQuery<Transaction>("GetAllTransactions @householdId",
                new SqlParameter("householdId", hhId)).ToListAsync();
        }

        /// <summary>
        ///   Gets all budgets associated with the household
        /// </summary>
        /// <param name="hhId">Household Id needed to know which budgets to retrieve</param>
        /// <returns>List of all budgets for the household</returns>
        public async Task<List<Budget>> GetBudgets(int hhId)
        {
            return await Database.SqlQuery<Budget>("GetBudgets @householdId",
                new SqlParameter("householdId", hhId)).ToListAsync();
        }

        /// <summary>
        ///   Gets all the Subcatagories for the budget
        /// </summary>
        /// <param name="budgetId">Budget Id to get appropriate budget items</param>
        /// <returns>Returns list of Budget Items for said budget</returns>
        public async Task<List<BudgetItem>> GetBudgetItems(int budgetId)
        {
            return await Database.SqlQuery<BudgetItem>("GetBudgetItems @budgetId",
                new SqlParameter("budgetId", budgetId)).ToListAsync();
        }

        /// <summary>
        ///   Allows to add a household wich all other items are under
        /// </summary>
        /// <param name="name">The name of the household</param>
        /// <returns>200</returns>
        public async Task<int> AddHousehold(string name)
        {
            return await Database.ExecuteSqlCommandAsync("AddHousehold @name",
                new SqlParameter("name", name));
        }

        /// <summary>
        ///   This is the accounts in which budgets and transactions will be associated.
        /// </summary>
        /// <param name="hhId">Household to associate</param>
        /// <param name="name">Name the account (ex. Checking Account)</param>
        /// <param name="balance">How much is in this account</param>
        /// <returns>200</returns>
        public async Task<int> AddAccount(int hhId, string name, decimal balance)
        {
            return await Database.ExecuteSqlCommandAsync("AddAccount @householdId, @name, @balance",
                new SqlParameter("householdId", hhId),
                new SqlParameter("name", name),
                new SqlParameter("balance", balance));
        }

        /// <summary>
        ///   This is to add individual transactions.
        ///   Each Transaction must be tied to an account
        /// </summary>
        /// <param name="acctId">Account Id wich is associated with transaction</param>
        /// <param name="tranType">Transaction Type, 3. Deposit, 4. Withdraw </param>
        /// <param name="budgetItem">Which Catagory does this transaction belong</param>
        /// <param name="UserId">GUID of the current user</param>
        /// <param name="amount">Amount of money associated with this transaction</param>
        /// <param name="name">Title for transaction (ex. Walmart)</param>
        /// <param name="description">Any details that may want to be known about this transaction</param>
        /// <returns>200</returns>
        public async Task<int> AddTransaction(int acctId, int tranType, int budgetItem, string UserId, decimal amount, string name, string description)
        {
            return await Database.ExecuteSqlCommandAsync("AddTransaction @houseAccountId, @transactionTypeId, @budgetItemId, @enteredById, @amount, @name, @description",
                new SqlParameter("houseAccountId", acctId),
                new SqlParameter("transactionTypeId", tranType),
                new SqlParameter("budgetItemId", budgetItem),
                new SqlParameter("enteredById", UserId),
                new SqlParameter("amount", amount),
                new SqlParameter("name", name),
                new SqlParameter("description", description));
        }

        /// <summary>
        ///   The main budget of the household.
        ///   May have multiple Budgets.
        /// </summary>
        /// <param name="hhId">Houshold Id, which house owns this budget</param>
        /// <param name="name">The name to title this budget</param>
        /// <param name="description">If you need any additional details for this budget.</param>
        /// <returns></returns>
        public async Task<int> AddBudget(int hhId, string name, string description)
        {
            return await Database.ExecuteSqlCommandAsync("AddBudget @householdId, @name, @description",
                new SqlParameter("householdId", hhId),
                new SqlParameter("name", name),
                new SqlParameter("description", description));
        }

        /// <summary>
        ///   Add a subcatagory to the Budget (ex. Utilities or Groceries)
        /// </summary>
        /// <param name="budgetId">Budget Id that this will be associated with.</param>
        /// <param name="amount">How much money do we want to make avaiable for this portion of the budget</param>
        /// <param name="name">Name of this item as shown above</param>
        /// <param name="description">Any additional details about this item</param>
        /// <returns>200</returns>
        public async Task<int> AddBudgetItem(int budgetId, decimal amount, string name, string description)
        {
            return await Database.ExecuteSqlCommandAsync("AddBudgetItem @budgetId, @amount, @name, @description",
                new SqlParameter("budgetId", budgetId),
                new SqlParameter("amount", amount),
                new SqlParameter("name", name),
                new SqlParameter("description", description));
        }

        /// <summary>
        ///   Delete household
        ///   Currently not auto removing all associated items
        /// </summary>
        /// <param name="hhId">The ID of the household we wish to remove</param>
        /// <returns>200</returns>
        public async Task<int> DeleteHousehold(int hhId)
        {
            return await Database.ExecuteSqlCommandAsync("DeleteHousehold @householdId",
                new SqlParameter("householdId", hhId));
        }

        /// <summary>
        ///   Remove Specified Account
        /// </summary>
        /// <param name="acctId">Account Id to remove</param>
        /// <returns>200</returns>
        public async Task<int> DeleteAccount(int acctId)
        {
            return await Database.ExecuteSqlCommandAsync("DeleteAccount @accountId",
                new SqlParameter("accountId", acctId));
        }

        /// <summary>
        ///   Remove transaction
        /// </summary>
        /// <param name="tranId">Id of the transaction to remove</param>
        /// <returns>200</returns>
        public async Task<int> DeleteTransaction(int tranId)
        {
            return await Database.ExecuteSqlCommandAsync("DeleteTransaction @transactionId",
                new SqlParameter("transactionId", tranId));
        }

        /// <summary>
        ///   Remove the Budget
        ///   Does not remove associated Items
        /// </summary>
        /// <param name="budgetId">Id of budget to be removed</param>
        /// <returns>200</returns>
        public async Task<int> DeleteBudget(int budgetId)
        {
            return await Database.ExecuteSqlCommandAsync("DeleteBudget @budgetId",
                new SqlParameter("budgetId", budgetId));
        }

        /// <summary>
        ///   Removes a Budget Item
        /// </summary>
        /// <param name="budgetItemId">Budget Item Id of Item to be removed.</param>
        /// <returns>200</returns>
        public async Task<int> DeleteBudgetItem(int budgetItemId)
        {
            return await Database.ExecuteSqlCommandAsync("DeleteBudgetItem @budgetItemId",
                new SqlParameter("budgetItemId", budgetItemId));
        }
    }
}