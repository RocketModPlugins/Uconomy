using System;
using System.Collections.Generic;
using System.Globalization;
using EnvyGarage.DataStorage;
using System.Security.Principal;

using Rocket.Core.Logging;
using Steamworks;
using fr34kyn01535.Uconomy.Models;
using System.Linq;

namespace fr34kyn01535.Uconomy
{
    public class DatabaseManager
    {

        private List<Account> Data;
        private DataStorage<List<Account>> AccountStorage { get; set; }

        internal DatabaseManager()
        {
            System.IO.Directory.CreateDirectory($"{Environment.CurrentDirectory}/Plugins/Uconomy/Database");
            this.AccountStorage = new DataStorage<List<Account>>(Uconomy.Instance.Directory + "/Database", "Accounts.json");
            Logger.Log("Loading json... OK ");
            CheckSchema();
        }



        /// <summary>
        /// Retrieves the current balance of a specific account.
        /// </summary>
        /// <param name="id">The Steam 64 ID of the account to retrieve the balance from.</param>
        /// <returns>The balance of the account.</returns>
        public decimal GetBalance(string id)
        {
           
            var exist = this.Data.FirstOrDefault(x => x.Id == id);
            if (exist != null)
            {
                return exist.Balance;
            }
            else
            {
                return 0;
            }

        
        }

        /// <summary>
        /// Increases the account balance of the specific ID with IncreaseBy.
        /// </summary>
        /// <param name="id">Steam 64 ID of the account.</param>
        /// <param name="increaseBy">The amount that the account should be changed with (can be negative).</param>
        /// <returns>The new balance of the account.</returns>
        public decimal IncreaseBalance(string id, decimal increaseBy)
        {
            var exist = this.Data.FirstOrDefault(x => x.Id == id);
            if(exist != null)
            {
                exist.Balance += increaseBy;
            }
            AccountStorage.Save(this.Data);
            Uconomy.Instance.BalanceUpdated(id, increaseBy);
            return exist.Balance;
        }

        /// <summary>
        /// Ensures that the account exists in the database and creates it if it isn't.
        /// </summary>
        /// <param name="id">Steam 64 ID of the account to ensure its existence.</param>
        public void CheckSetupAccount(CSteamID id)
        {
            var exist =  this.Data.FirstOrDefault(x => x.Id == id.ToString());
            if(exist == null)
            {
                Account account = new Account()
                {
                    Balance = Uconomy.Instance.Configuration.Instance.InitialBalance,
                    Id = id.ToString(),
                    LastUpdated = DateTime.Now
                };
                this.Data.Add(account);
                AccountStorage.Save(this.Data);
            }

        }

        internal void CheckSchema()
        {
            Data = AccountStorage.Read();
            if (Data == null)
            {
                Data = new List<Account>();
                AccountStorage.Save(Data);
            }
        }


    }
}