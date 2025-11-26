using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SQLite;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using TrimbleBank.Classes;
using TrimbleBank.UserControls;



namespace TrimbleBank.Data
{
    public class TrimbleBankDbPluginSQLite : TrimbleBankDbPlugin
    {
        private string           _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrimbleBankDb;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private SQLiteConnection _sqlConnection    = null;

        private SQLiteDataAdapter _dataAdapterCustomer     = null;
        private SQLiteDataAdapter _dataAdapterAccounts     = null;
        private SQLiteDataAdapter _dataAdapterTransactions = null;
        
        public TrimbleBankDbPluginSQLite()
        {
            _sqlConnection = new SQLiteConnection(_connectionString);
        }

        /// <summary>
        /// Load complete database
        /// </summary>
        public override void Load()
        {
            // --- create data adapter for each table ---
            _dataAdapterCustomer     = CreateDataAdapter(SqlStatement.GetCustomer    , _sqlConnection);
            _dataAdapterAccounts     = CreateDataAdapter(SqlStatement.GetAccounts    , _sqlConnection);
            _dataAdapterTransactions = CreateDataAdapter(SqlStatement.GetTransactions, _sqlConnection);

            // --- fill data set ---
            _dataAdapterCustomer.FillSchema(_dbDataSet, SchemaType.Source);
            _dataAdapterCustomer.Fill(_dbDataSet, TableNames.CUSTOMERS);
            //
            _dataAdapterAccounts.FillSchema(_dbDataSet, SchemaType.Source);
            _dataAdapterAccounts.Fill(_dbDataSet, TableNames.ACCOUNTS);
            //
            _dataAdapterTransactions.FillSchema(_dbDataSet, SchemaType.Source);
            _dataAdapterTransactions.Fill(_dbDataSet, TableNames.TRANSACTIONS);

            // --- add PKs, ... ---
            InitDataSet();

            // --- for better handling when creating new data rows, redefine insert command   ---
            // --- for each table. This ensures that, after writing a new row to the dataset, --- 
            // --- the created id will be set within the row                                  ---
            _dataAdapterCustomer.InsertCommand = new SQLiteCommand(SqlStatement.InsertCustomer, _sqlConnection);
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@FirstName"   , DbType.String, 256, "FirstName");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@LastName"    , DbType.String, 256, "LastName");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@Street"      , DbType.String, 256, "Street");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@HouseNumber" , DbType.String, 256, "HouseNumber");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@ZipCode"     , DbType.String, 256, "ZipCode");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@City"        , DbType.String, 256, "City");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@PhoneNumber" , DbType.String, 256, "PhoneNumber");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@EmailAddress", DbType.String, 256, "EmailAddress");

            _dataAdapterAccounts.InsertCommand = new SQLiteCommand(SqlStatement.InsertAccount, _sqlConnection);
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@CustomerId"  , DbType.Int32 ,   4, "CustomerId");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@Number"      , DbType.String, 256, "Number");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@Balance"     , DbType.Double,   8, "Balance");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@BalanceCheck", DbType.String, 256, "BalanceCheck");

            _dataAdapterTransactions.InsertCommand = new SQLiteCommand(SqlStatement.InsertTransaction, _sqlConnection);
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@AccountId", DbType.Int32    ,  4, "AccountId");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Date"     , DbType.DateTime2,  8, "Date");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Amount"   , DbType.Int32    ,  4, "Amount");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Purpose"  , DbType.String   , 256, "Purpose");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@IBAN"     , DbType.String   , 256, "IBAN");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Number"   , DbType.String   , 256, "Number");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Type"     , DbType.Int32    ,   4, "Type");
        }

        /// <summary>
        /// Add customer to database
        /// </summary>
        /// <param name="customerRow">DataRow containing new customer</param>
        public override void AddCustomer(DataRow customerRow)
        {
            _dbDataSet.Tables[TableNames.CUSTOMERS].Rows.Add(customerRow);
            _dataAdapterCustomer.Update(_dbDataSet, TableNames.CUSTOMERS);
        }
        /// <summary>
        /// Write modified custómer DataRows to database
        /// </summary>
        public override void UpdateCustomers()
        {
            _dataAdapterCustomer.Update(_dbDataSet, TableNames.CUSTOMERS);
        }
        /// <summary>
        /// Delete customer from database
        /// </summary>
        /// <param name="customerRow">DataRow containing customer to delete</param>
        public override void DeleteCustomer(DataRow customerRow)
        {
            customerRow.Delete();
            _dataAdapterCustomer.Update(_dbDataSet, TableNames.CUSTOMERS);
        }

        /// <summary>
        /// Write modified account DataRows to database
        /// </summary>
        public override void UpdateAccounts()
        {
            _dataAdapterAccounts.Update(_dbDataSet, TableNames.ACCOUNTS);
        }

        /// <summary>
        /// Add account to database
        /// </summary>
        /// <param name="accountRow">DataRow containing new account</param>
        public override void AddAccount(DataRow accountRow)
        {
            _dbDataSet.Tables[TableNames.ACCOUNTS].Rows.Add(accountRow);
            _dataAdapterAccounts.Update(_dbDataSet, TableNames.ACCOUNTS);
        }

        /// <summary>
        /// Delete account from database
        /// </summary>
        /// <param name="accountRow">DataRow containing customer to delete</param>
        public override void DeleteAccount(DataRow accountRow)
        {
            accountRow.Delete();
            _dataAdapterAccounts.Update(_dbDataSet, TableNames.ACCOUNTS);
        }

        /// <summary>
        /// Delete transaction from database
        /// </summary>
        /// <param name="id">database id of transaction</param>
        public override void DeleteTransaction(int id)
        {
            DataRow deleteRow = _dbDataSet.Tables[TableNames.TRANSACTIONS].Rows.Find(id);
            if (deleteRow != null)
            {
                deleteRow.Delete();
                _dataAdapterTransactions.Update(_dbDataSet, TableNames.TRANSACTIONS);
            }        
        }

        /// <summary>
        /// Get all transactions for given account 
        /// </summary>
        /// <param name="accountId">Database id if account</param>
        /// <returns></returns>
        public override DataRow[] GetAccountTransactions(int accountId)
        {
            return _dbDataSet.Tables[TableNames.TRANSACTIONS].Select($"AccountId = {accountId}");
        }
        
        /// <summary>
        /// Add transaction to database
        /// </summary>
        /// <param name="transactionRow">DataRow containing the transaction</param>
        /// <param name="accountRow">DataRow with account for creating transaction</param>
        /// <returns></returns>
        public override DataRow AddTransaction(DataRow transactionRow, DataRow accountRow)
        {
            // --- write new balance and balance security checksum to accountRow ---         
            CalcTransactionBalance(transactionRow, accountRow);
            //
            // --- write updated accountRow to DB ---
            _dataAdapterAccounts.Update(_dbDataSet, TableNames.ACCOUNTS);

            // --- write transaction to DB ---
            _dbDataSet.Tables[TableNames.TRANSACTIONS].Rows.Add(transactionRow);
            _dataAdapterTransactions.Update(_dbDataSet, TableNames.TRANSACTIONS);

            // --- return updated accountRow ---
            return accountRow;
        }


        /// <summary>
        /// Create DataApapter with passed SQL select statement
        /// </summary>
        /// <param name="sqlSelect">SQL select statement for filling DataSet table</param>
        /// <param name="sqlConnection">SQL connection for DataAdapter</param>
        /// <returns>initialized DataAdapter</returns>
        private SQLiteDataAdapter CreateDataAdapter(string sqlSelect, SQLiteConnection sqlConnection)
        {
            SQLiteCommand        cmdSelect     = new SQLiteCommand(sqlSelect, sqlConnection);
            SQLiteDataAdapter    dbDataAdapter = new SQLiteDataAdapter(cmdSelect);
            SQLiteCommandBuilder cmb           = new SQLiteCommandBuilder(dbDataAdapter);

            return dbDataAdapter;
        }
   }
}
