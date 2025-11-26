using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimbleBank.Data
{
    public enum TrimbleBankDbType
    {
        SqlServer,
        SQlite
    }

    public class TrimbleBankDb
    {
        private TrimbleBankDbPlugin _dataBasePlugIn = null;

        public TrimbleBankDb(TrimbleBankDbType dbType)
        {
            switch (dbType)
            {
                case TrimbleBankDbType.SqlServer:
                    _dataBasePlugIn = new TrimbleBankDbPluginSqlServer();
                    break;
                case TrimbleBankDbType.SQlite:
                    _dataBasePlugIn = new TrimbleBankDbPluginSQLite();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Load complete database
        /// </summary>        
        public void Load()
        {
            _dataBasePlugIn.Load();
        }

        #region Customers

        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>DataTable "Customers" containing all customers</returns>
        public DataTable GetCustomers()
        {
            return _dataBasePlugIn.GetCustomers();
        }

        /// <summary>
        /// Write modified custómer DataRows to database
        /// </summary>
        public void UpdateCustomers()
        {
            _dataBasePlugIn.UpdateCustomers();
        }

        /// <summary>
        /// Add customer to database
        /// </summary>
        /// <param name="customerRow">DataRow containing new customer</param>
        public void AddCustomer(DataRow customerRow)
        {
            _dataBasePlugIn.AddCustomer(customerRow);
        }

        /// <summary>
        /// Delete customer from database
        /// </summary>
        /// <param name="customerRow">DataRow containing customer to delete</param>
        public void DeleteCustomer(DataRow customerRow)
        {
            _dataBasePlugIn.DeleteCustomer(customerRow);
        }

        /// <summary>
        /// Created new customer DataRow 
        /// </summary>
        /// <returns>DataRow from table customer</returns>
        public DataRow CreateCustomerRow()
        {
            return _dataBasePlugIn.CreateCustomerRow();
        }
        #endregion

        #region Accounts
        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>DataTable "Accounts" containing all accounts</returns>
        public DataTable GetAccounts()
        {
            return _dataBasePlugIn.GetAccounts();
        }

        /// <summary>
        /// Get account with given database id
        /// </summary>
        /// <param name="id">database id of account</param>
        /// <returns>DataRow containing account</returns>
        public DataRow GetAccount(int id)
        {
            return _dataBasePlugIn.GetAccount(id);
        }

        /// <summary>
        /// Write modified account DataRows to database
        /// </summary>
        public void UpdateAccounts()
        {
            _dataBasePlugIn.UpdateAccounts();
        }
        /// <summary>
        /// Add account to database
        /// </summary>
        /// <param name="accountRow">DataRow containing new account</param>
        public void AddAccount(DataRow accountRow)
        {
            _dataBasePlugIn.AddAccount(accountRow);   
        }
        /// <summary>
        /// Delete account from database
        /// </summary>
        /// <param name="accountRow">DataRow containing customer to delete</param>
        public void DeleteAccount(DataRow accountRow)
        {
            _dataBasePlugIn.DeleteAccount(accountRow);
        }

        /// <summary>
        /// Created new account DataRow 
        /// </summary>
        /// <returns>DataRow from table account</returns>
        public virtual DataRow CreateAccountRow()
        {
            return _dataBasePlugIn.CreateAccountRow();
        }
        #endregion    

        #region Transactions
        /// <summary>
        /// Get transaction with given database id
        /// </summary>
        /// <param name="id">database id of transaction</param>
        /// <returns>DataRow containing transaction</returns>
        public DataRow GetTransaction(int id)
        {
            return _dataBasePlugIn.GetTransaction(id);
        }

        /// <summary>
        /// Get all transactions for given account 
        /// </summary>
        /// <param name="accountId">Database id if account</param>
        /// <returns></returns>
        public DataRow[] GetAccountTransactions(int accountId)
        {
            return _dataBasePlugIn.GetAccountTransactions(accountId);
        }

        /// <summary>
        /// Created new transaction DataRow 
        /// </summary>
        /// <returns>DataRow from table transaction</returns>
        public DataRow CreateTransactionRow()
        {
            return _dataBasePlugIn.CreateTransactionRow();
        }

        /// <summary>
        /// Add transaction to database
        /// </summary>
        /// <param name="transactionRow">DataRow containing the transaction</param>
        /// <param name="accountRow">DataRow with account for creating transaction</param>
        /// <returns></returns>
        public DataRow AddTransaction(DataRow transactionRow, DataRow accountRow)
        {
            return _dataBasePlugIn.AddTransaction(transactionRow, accountRow);
        }

        /// <summary>
        /// Delete transaction from database
        /// </summary>
        /// <param name="id">database id of transaction</param>
        public virtual void DeleteTransaction(int id)
        {
            _dataBasePlugIn.DeleteTransaction(id);
        }
        #endregion






    }
}
