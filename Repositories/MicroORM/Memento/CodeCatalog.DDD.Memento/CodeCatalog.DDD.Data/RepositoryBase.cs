using System;
using System.Data.SqlClient;
using System.IO;

using Dapper;

using Microsoft.Data.Sqlite;

namespace CodeCatalog.DDD.Data
{
    public class RepositoryBase
    {
        protected SqliteConnection Connection { get; }

        public RepositoryBase(string databaseFileName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            var databaseFilePath = Path.Combine(appData, databaseFileName);

            Connection = new SqliteConnection("Data Source=" + databaseFilePath );

            CreateDatabaseIfNew(databaseFilePath);
        }

        private void CreateDatabaseIfNew(string databaseFile)
        {
            if (!File.Exists(databaseFile))
                CreateAndSeed();

            void CreateAndSeed()
            {
                using (Connection)
                {
                    CreateTables();
                    SeedData();
                }
            }
        }

        private void CreateTables()
        {
            CreateCustomerTable();
            CreateProductTable();
            CreateOrderLineTable();
            CreateOrderTable();

            void CreateCustomerTable()
            {
                Connection.Execute("create table Customers "
                                   + "( "
                                   + "Id integer primary key autoincrement, "
                                   + "IsPrivilegeCustomer char not null, "
                                   + "FirstName varchar(100) not null);");
            }

            void CreateProductTable()
            {
                Connection.Execute("create table Products "
                                   + "( "
                                   + "Id integer primary key autoincrement, "
                                   + "ProductName varchar(100));");
            }

            void CreateOrderLineTable()
            {
                Connection.Execute("create table OrderLines "
                                   + "( "
                                   + "Id integer primary key autoincrement, "
                                   + "OrderId text not null, "
                                   + "ProductId integer not null, "
                                   + "Quantity integer not null ,"
                                   + "Price double not null, "
                                   + "Discount double not null, "
                                   + "FOREIGN KEY (OrderId) REFERENCES Orders(Id), "
                                   + "FOREIGN KEY (ProductId) REFERENCES Products(Id));");
            }

            void CreateOrderTable()
            {
                Connection.Execute("create table Orders "
                                   + "( "
                                   + "Id text primary key, "
                                   + "CustomerId integer not null, "
                                   + "PaymentProcessed char not null, "
                                   + "FOREIGN KEY (CustomerId) REFERENCES Customers(Id));");
            }
        }

        private void SeedData()
        {
            PopulateProductTable();
            PopulateCustomerTable();

            void PopulateProductTable()
            {
                Connection.Execute("insert into Products (ProductName) values('Nexus 6P'); "
                                   + "insert into Products (ProductName) values('Nexus 5X'); "
                                   + "insert into Products (ProductName) values('Nexus 6'); "
                                   + "insert into Products (ProductName) values('Nexus 5');");
            }

            void PopulateCustomerTable()
            {
                Connection.Execute("insert into Customers (IsPrivilegeCustomer, FirstName) values('Y','Test User'); ");
            }
        }
    }
}