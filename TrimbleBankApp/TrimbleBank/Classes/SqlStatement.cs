using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrimbleBank.Classes
{
    /// <summary>
    /// Class for storing needed SQL statements
    /// </summary>
    public class SqlStatement
    {
        public const string GetCustomer = @"
        SELECT [Id]
              ,[FirstName]
              ,[LastName]
              ,[Street]
              ,[HouseNumber]
              ,[ZipCode]
              ,[City]
              ,[PhoneNumber]
              ,[EmailAddress]
        FROM [dbo].[Customers]";

        public const string GetAccounts = @"
        SELECT [Id]
              ,[CustomerId]
              ,[Number]
              ,[Balance]
              ,[BalanceCheck]
        FROM [dbo].[Accounts]";

        public const string GetTransactions = @"
        SELECT [Id]
              ,[AccountId]
              ,[Date]
              ,[Amount]
              ,[Purpose]
              ,[IBAN]
              ,[Number]
              ,[Type]
        FROM [dbo].[Transactions]";

        public const string InsertCustomer = @"
        INSERT INTO [dbo].[Customers]
           (FirstName
           ,LastName
           ,Street
           ,HouseNumber
           ,ZipCode
           ,City
           ,PhoneNumber
           ,EmailAddress)
        OUTPUT INSERTED.Id
        VALUES
           (@FirstName
           ,@LastName
           ,@Street
           ,@HouseNumber
           ,@ZipCode
           ,@City
           ,@PhoneNumber
           ,@EmailAddress)";


        public const string InsertAccount = @"
        INSERT INTO Accounts
            (CustomerId
            ,Number
            ,Balance
            ,BalanceCheck)
        OUTPUT INSERTED.Id
        VALUES
            (@CustomerId
            ,@Number
            ,@Balance
            ,@BalanceCheck)";

        public const string InsertTransaction = @"
        INSERT INTO Transactions
           (AccountId
           ,Date
           ,Amount
           ,Purpose
           ,IBAN
           ,Number
           ,Type)
        OUTPUT INSERTED.Id
        VALUES
           (@AccountId
           ,@Date
           ,@Amount
           ,@Purpose
           ,@IBAN
           ,@Number
           ,@Type)";

    }
}
