using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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
    public class TrimbleBankDbPluginSqlServer : TrimbleBankDbPlugin
    {

        private string        _connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TrimbleBankDb;Integrated Security=True;Connect Timeout=30;Encrypt=False";
        private SqlConnection _sqlConnection    = null;

        private SqlDataAdapter _dataAdapterCustomer     = null;
        private SqlDataAdapter _dataAdapterAccounts     = null;
        private SqlDataAdapter _dataAdapterTransactions = null;
        
        public TrimbleBankDbPluginSqlServer()
        {
            _sqlConnection = new SqlConnection(_connectionString);
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
            _dataAdapterCustomer.InsertCommand = new SqlCommand(SqlStatement.InsertCustomer, _sqlConnection);
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@FirstName"   , SqlDbType.NVarChar, 256, "FirstName");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@LastName"    , SqlDbType.NVarChar, 256, "LastName");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@Street"      , SqlDbType.NVarChar, 256, "Street");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@HouseNumber" , SqlDbType.NVarChar, 256, "HouseNumber");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@ZipCode"     , SqlDbType.NVarChar, 256, "ZipCode");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@City"        , SqlDbType.NVarChar, 256, "City");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@PhoneNumber" , SqlDbType.NVarChar, 256, "PhoneNumber");
            _dataAdapterCustomer.InsertCommand.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 256, "EmailAddress");

            _dataAdapterAccounts.InsertCommand = new SqlCommand(SqlStatement.InsertAccount, _sqlConnection);
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@CustomerId"  , SqlDbType.Int     ,   4, "CustomerId");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@Number"      , SqlDbType.NVarChar, 256, "Number");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@Balance"     , SqlDbType.Float   ,   8, "Balance");
            _dataAdapterAccounts.InsertCommand.Parameters.Add("@BalanceCheck", SqlDbType.NVarChar, 256, "BalanceCheck");

            _dataAdapterTransactions.InsertCommand = new SqlCommand(SqlStatement.InsertTransaction, _sqlConnection);
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@AccountId", SqlDbType.Int      ,  4, "AccountId");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Date"     , SqlDbType.DateTime2,  8, "Date");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Amount"   , SqlDbType.Int      ,  4, "Amount");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Purpose"  , SqlDbType.NVarChar , 256, "Purpose");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@IBAN"     , SqlDbType.NVarChar , 256, "IBAN");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Number"   , SqlDbType.NVarChar , 256, "Number");
            _dataAdapterTransactions.InsertCommand.Parameters.Add("@Type"     , SqlDbType.Int      ,   4, "Type");
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
        private SqlDataAdapter CreateDataAdapter(string sqlSelect, SqlConnection sqlConnection)
        {
            SqlCommand        cmdSelect     = new SqlCommand(sqlSelect, sqlConnection);
            SqlDataAdapter    dbDataAdapter = new SqlDataAdapter(cmdSelect);
            SqlCommandBuilder cmb           = new SqlCommandBuilder(dbDataAdapter);

            return dbDataAdapter;
        }
   }
}
