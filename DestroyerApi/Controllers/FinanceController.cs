using DestroyerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace DestroyerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Finance")]
    public class FinanceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        ///   Returns household, primary building block
        /// </summary>
        /// <param name="hhId">Needed to Retrieve Household info</param>
        /// <returns>Houshold int ID, string Name</returns>
        [Route("GetHousehold")]
        public async Task<Household> GetHoushold (int hhId)
        {
            return await db.GetHousehold(hhId);
        }

        /// <summary>
        ///   Gets List of Accounts for household
        /// </summary>
        /// <param name="hhId">Needed to get appropriate accounts</param>
        /// <returns>List Accounts int ID, int hhID, string name, double balance</returns>
        [Route("GetAccounts")]
        public async Task<List<Account>> GetAccounts(int hhId)
        {
            return await db.GetAccounts(hhId);
        }

        /// <summary>
        ///   Gets List of Transaction for a specific account
        /// </summary>
        /// <param name="acctId">The Id from the account to retrieve transactions</param>
        /// <returns>List Transactions int acctId, int tranType, int budgetItem, string UserId, date date, decimal amount, string name, string description, bool Reconciled, decimal ReconAmount, bool Void</returns>
        [Route("GetAccountTransactions")]
        public async Task<List<Transaction>> GetAccountTransactions(int acctId)
        {
            return await db.GetAccountTransactions(acctId);
        }

        /// <summary>
        ///   Gets all transactions for a specific households
        /// </summary>
        /// <param name="hhId">Household Id to pull transactions</param>
        /// <returns>List Transactions int acctId, int tranType, int budgetItem, string UserId, date date, decimal amount, string name, string description, bool Reconciled, decimal ReconAmount, bool Void</returns>
        [Route("GetAllTransactions")]
        public async Task<List<Transaction>> GetAllTransactions(int hhId)
        {
            return await db.GetAllTransactions(hhId);
        }

        /// <summary>
        ///   Gets all budgets for the Household
        /// </summary>
        /// <param name="hhId">Household ID needed to associate Budgets</param>
        /// <returns>List Budgets int ID, int hhId, string Name, String Dexcription</returns>
        [Route("GetBudgets")]
        public async Task<List<Budget>> GetBudgets(int hhId)
        {
            return await db.GetBudgets(hhId);
        }

        /// <summary>
        ///   Gets All subcatagories of Budgets (ex. Fuel, groceries, etc.)
        ///   Includes amount allocated for each subcatagory.
        /// </summary>
        /// <param name="budgetId">Id of the budget for which these items belong</param>
        /// <returns>int Id, int budgetID, decimal amount, string description, string name</returns>
        [Route("GetBudgetItems")]
        public async Task<List<BudgetItem>> GetBudgetItems(int budgetId)
        {
            return await db.GetBudgetItems(budgetId);
        }

        /// <summary>
        ///   Add a new household, this is the building block for all components.
        /// </summary>
        /// <param name="name">This will be the name by which this household will be represented</param>
        /// <returns>200</returns>
        [Route("AddHousehold")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddHousehold(string name)
        {
            try
            {
                await db.AddHousehold(name);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Adds the accounts that budgets and transactions are meant to run off of.
        /// </summary>
        /// <param name="hhId">The Id for the house to which to add this account</param>
        /// <param name="name">The name that is unique to this account (ex. WellsFargo)</param>
        /// <param name="balance">This is the amount of money in the account</param>
        /// <returns>200</returns>
        [Route("AddAccount")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddAccount(int hhId, string name, decimal balance)
        {
            try
            {
                await db.AddAccount(hhId, name, balance);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Add a budget for the household.
        ///   budget may be impacted by multiple accounts
        /// </summary>
        /// <param name="hhId">The household Id for which the budget belongs</param>
        /// <param name="name">Name of the budget (ex. Jan Budget or temp Budget)</param>
        /// <param name="description">If you need to add any additional information</param>
        /// <returns>200</returns>
        [Route("AddBudget")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddBudget(int hhId, string name, string description)
        {
            try
            {
                await db.AddBudget(hhId, name, description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Add subcatagories to a budget
        /// </summary>
        /// <param name="budgetId">The budget Id for which this will be associated</param>
        /// <param name="amount">How much funds do we want to dedicate to this catagory</param>
        /// <param name="name">The name of this subcatagory (ex. Utilites, entertainment, Food)</param>
        /// <param name="description">Any additional details if needed</param>
        /// <returns>200</returns>
        [Route("AddBudgetItem")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddBudgetItem(int budgetId, decimal amount, string name, string description)
        {
            try
            {
                await db.AddBudgetItem(budgetId, amount, name, description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
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
        [Route("AddTransaction")]
        [AcceptVerbs("POST")]
        public async Task<IHttpActionResult> AddTransaction(int acctId, int tranType, int budgetItem, string UserId, decimal amount, string name, string description)
        {
            try
            {
                await db.AddTransaction(acctId, tranType, budgetItem, UserId, amount, name, description);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Delete household
        ///   Currently not auto removing all associated items
        /// </summary>
        /// <param name="hhId">The ID of the household we wish to remove</param>
        /// <returns>200</returns>
        [Route("DeleteHousehold")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteHousehold(int hhId)
        {
            try
            {
                await db.DeleteHousehold(hhId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Remove Specified Account
        /// </summary>
        /// <param name="acctId">Account Id to remove</param>
        /// <returns>200</returns>
        [Route("DeleteAccount")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteAccount(int acctId)
        {
            try
            {
                await db.DeleteAccount(acctId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Remove the Budget
        ///   Does not remove associated Items
        /// </summary>
        /// <param name="budgetId">Id of budget to be removed</param>
        /// <returns>200</returns>
        [Route("DeleteBudget")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteBudget(int budgetId)
        {
            try
            {
                await db.DeleteBudget(budgetId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Removes a Budget Item
        /// </summary>
        /// <param name="budgetItemId">Budget Item Id of Item to be removed.</param>
        /// <returns>200</returns>
        [Route("DeleteBudgetItem")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteBudgetItem(int budgetItemId)
        {
            try
            {
                await db.DeleteBudgetItem(budgetItemId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///   Remove transaction
        /// </summary>
        /// <param name="tranId">Id of the transaction to remove</param>
        /// <returns>200</returns>
        [Route("DeleteTransaction")]
        [AcceptVerbs("DELETE")]
        public async Task<IHttpActionResult> DeleteTransaction(int tranId)
        {
            try
            {
                await db.DeleteTransaction(tranId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
