using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TrimbleBank.Classes;

namespace TrimbleBank.Data
{
    public class TableNames
    {
        public const string CUSTOMERS    = "Customers";
        public const string ACCOUNTS     = "Accounts";
        public const string TRANSACTIONS = "Transactions";
    }

    public class TransactionType
    {
        public const int WITHDRAWAL  = 0;
        public const int DEPOSIT     = 1;
        public const int TRANSFER    = 2;
        public const int INCOMING    = 3;
    }


    public class TrimbleBankDbPlugin
    {
        protected DataSet _dbDataSet = new DataSet();


        /// <summary>
        /// Load complete database
        /// </summary>        
        public virtual void Load()
        {
            throw new NotImplementedException("Load() call not implemented");        
        }

        #region Customers
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns>DataTable "Customers" containing all customers</returns>
        public DataTable GetCustomers()
        {
            return _dbDataSet.Tables[TableNames.CUSTOMERS];
        }

        /// <summary>
        /// Created new customer DataRow 
        /// </summary>
        /// <returns>DataRow from table customer</returns>
        public DataRow CreateCustomerRow()
        {
            DataRow newRow = _dbDataSet.Tables[TableNames.CUSTOMERS].NewRow();
            newRow["Id"] = 0;
            return newRow;
        }

        /// <summary>
        /// Add customer to database
        /// </summary>
        /// <param name="customerRow">DataRow containing new customer</param>
        public virtual void AddCustomer(DataRow customerRow)
        {
            throw new NotImplementedException("AddCustomer() call not implemented");        
        }

        /// <summary>
        /// Write modified custómer DataRows to database
        /// </summary>
        public virtual void UpdateCustomers()
        {
            throw new NotImplementedException("UpdateCustomers() call not implemented");        
        }

        /// <summary>
        /// Delete customer from database
        /// </summary>
        /// <param name="customerRow">DataRow containing customer to delete</param>
        public virtual void DeleteCustomer(DataRow customerRow)
        {
            throw new NotImplementedException("DeleteCustomers() call not implemented");        
        }
        #endregion


        #region Accounts
        /// <summary>
        /// Get all accounts
        /// </summary>
        /// <returns>DataTable "Accounts" containing all accounts</returns>
        public DataTable GetAccounts()
        {
            return _dbDataSet.Tables[TableNames.ACCOUNTS];
        }

        /// <summary>
        /// Get account with given database id
        /// </summary>
        /// <param name="id">database id of account</param>
        /// <returns>DataRow containing account</returns>
        public DataRow GetAccount(int id)
        {
            return _dbDataSet.Tables[TableNames.ACCOUNTS].Rows.Find(id);
        }

        /// <summary>
        /// Created new account DataRow 
        /// </summary>
        /// <returns>DataRow from table account</returns>
        public DataRow CreateAccountRow()
        {
            DataRow newRow = _dbDataSet.Tables[TableNames.ACCOUNTS].NewRow();
            newRow["Id"] = 0;
            return newRow;
        }

        /// <summary>
        /// Add account to database
        /// </summary>
        /// <param name="accountRow">DataRow containing new account</param>
        public virtual void AddAccount(DataRow accountRow)
        {
            throw new NotImplementedException("AddAccount() call not implemented");        
        }

        /// <summary>
        /// Delete account from database
        /// </summary>
        /// <param name="accountRow">DataRow containing customer to delete</param>
        public virtual void DeleteAccount(DataRow accountRow)
        {
            throw new NotImplementedException("DeleteAccount() call not implemented");        
        }

        /// <summary>
        /// Write modified account DataRows to database
        /// </summary>
        public virtual void UpdateAccounts()
        {
            throw new NotImplementedException("UpdateAccounts() call not implemented");        
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
            return _dbDataSet.Tables[TableNames.TRANSACTIONS].Rows.Find(id);
        }

        /// <summary>
        /// Created new transaction DataRow 
        /// </summary>
        /// <returns>DataRow from table transaction</returns>
        public DataRow CreateTransactionRow()
        {
            DataRow newRow = _dbDataSet.Tables[TableNames.TRANSACTIONS].NewRow();
            newRow["Id"] = 0;
            return newRow;
        }

        /// <summary>
        /// Get all transactions for given account 
        /// </summary>
        /// <param name="accountId">Database id if account</param>
        /// <returns></returns>
        public virtual DataRow[] GetAccountTransactions(int accountId)
        {
            throw new NotImplementedException("GetAccountTransactions() call not implemented");        
        }

        /// <summary>
        /// Add transaction to database
        /// </summary>
        /// <param name="transactionRow">DataRow containing the transaction</param>
        /// <param name="accountRow">DataRow with account for creating transaction</param>
        /// <returns></returns>
        public virtual DataRow AddTransaction(DataRow transactionRow, DataRow accountRow)
        {
            throw new NotImplementedException("AddTransaction() call not implemented");        
        }

        /// <summary>
        /// Delete transaction from database
        /// </summary>
        /// <param name="id">database id of transaction</param>
        public virtual void DeleteTransaction(int id)
        {
            throw new NotImplementedException("DeleteTransaction() call not implemented");        
        }

        /// <summary>
        /// Calculate new account balance and balance signature for given transaction
        /// </summary>
        /// <param name="transactionRow">DataRow containing the transaction</param>
        /// <param name="accountRow">DataRow containing the account, balance and balanceCheck will be updated</param>
        protected void CalcTransactionBalance(DataRow transactionRow, DataRow accountRow)
        {
            // --- current account balance ---
            double balance = Convert.ToDouble(accountRow["Balance"]);    
            //
            // --- transaction amount ---
            double amount = Convert.ToDouble(transactionRow["Amount"]);
            //
            // --- transaction type ---
            int typeId = Convert.ToInt32(transactionRow["Type"]);

            // --- depends on type, calc new balance ---
            double newBalance = 0;
            switch (typeId)
            {
                case TransactionType.WITHDRAWAL:
                case TransactionType.TRANSFER:
                    newBalance = balance - amount;
                    break;
                case TransactionType.DEPOSIT:
                case TransactionType.INCOMING:
                    newBalance  = balance + amount;
                    break;
                default:
                    throw new Exception($"unknown transaction type id [{typeId}]"); 
            }
            // --- set new balance ---
            accountRow["Balance"] = newBalance;

            // --- set new balance signature ---
            // => include account number to signature to ensure signature cannot be used for other accounts
            accountRow["BalanceCheck"] = SignatureManager.Create(newBalance, accountRow);
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Initialize dataset, be setting PKs/needed relations and adding columns for account viewing
        /// </summary>
        protected void InitDataSet()
        {
            // --- set needed primary keys ---
            _dbDataSet.Tables[TableNames.ACCOUNTS].PrimaryKey     = new DataColumn[] { _dbDataSet.Tables[TableNames.ACCOUNTS    ].Columns["Id"] };
            _dbDataSet.Tables[TableNames.TRANSACTIONS].PrimaryKey = new DataColumn[] { _dbDataSet.Tables[TableNames.TRANSACTIONS].Columns["Id"] };

            // --- foreign key Customer -> Accounts ---
            DataRelation fkCustomerAccount = new DataRelation("FK_Customers_Accounts", _dbDataSet.Tables[TableNames.CUSTOMERS].Columns["Id"], _dbDataSet.Tables[TableNames.ACCOUNTS].Columns["CustomerID"]);
            _dbDataSet.Relations.Add(fkCustomerAccount);

            // --- add some customer address props to account table ---
            // => First Name
            AddDataColumn("FirstName", "FK_Customers_Accounts", "Accounts");
            // => Last Name
            AddDataColumn("LastName" , "FK_Customers_Accounts", "Accounts");
        }

        /// <summary>
        /// Add datacolumn to table
        /// </summary>
        /// <param name="columnName">Name if new column</param>
        /// <param name="dataRelation">Name of used DataRelation</param>
        /// <param name="tableName">Name of table</param>
        protected void AddDataColumn(string columnName, string dataRelation, string tableName)
        {
            DataColumn col = new DataColumn(columnName, typeof(string));
            col.Expression = $"Parent({dataRelation}).{columnName}";
            _dbDataSet.Tables[tableName].Columns.Add(col);
        }
        #endregion 


    }
}
