using System;
using System.Data.SqlClient;
using System.IO;

using Dapper;

namespace CodeCatalog.DDD.Data
{
    internal class RepositoryBase
    {
        protected SqlConnection Connection { get; }

        public RepositoryBase()
        {
            var databaseFile = Environment.CurrentDirectory + "\\DDDOrderSampleDb.sqlite";

            Connection = new SqlConnection("Data Source=" + databaseFile);

            CreateDatabaseIfNew(databaseFile);
        }

        private void CreateDatabaseIfNew(string databaseFile)
        {
            if (!File.Exists(databaseFile))
                CreateAndSeed();

            void CreateAndSeed()
            {
                using (Connection)
                {
                    Connection.Open();

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
                Connection.Execute("create table Customer "
                                   + "( "
                                   + "Id integer identity primary key autoincrement, "
                                   + "IsPrivilegeCustomer char not null, "
                                   + "FirstName varchar(100) not null);");
            }

            void CreateProductTable()
            {
                Connection.Execute("create table Product "
                                   + "( "
                                   + "Id integer identity primary key autoincrement, "
                                   + "ProductName varchar(100));");
            }

            void CreateOrderLineTable()
            {
                Connection.Execute("create table OrderLine "
                                   + "( "
                                   + "Id integer identity primary key autoincrement, "
                                   + "OrderId text not null, "
                                   + "ProductId integer not null, "
                                   + "Quantity integer not null ,"
                                   + "Price double not null, "
                                   + "Discount double not null, "
                                   + "FOREIGN KEY (OrderId) REFERNCES Order(Id), "
                                   + "FOREIGN KEY (ProductId) REFERENCES Product(Id));");
            }

            void CreateOrderTable()
            {
                Connection.Execute("create table Order "
                                   + "( "
                                   + "Id  text primary key, "
                                   + "CustomerId integer not null, "
                                   + "PaymentProcessed char not null"
                                   + "FOREIGN KEY (CustomerId) REFERENCES Customer(Id));");
            }
        }

        private void SeedData()
        {
            PopulateProductTable();
            PopulateCustomerTable();

            void PopulateProductTable()
            {
                Connection.Execute("insert into Product (ProductName) values('Nexus 6P'); "
                                   + "insert into Product (ProductName) values('Nexus 5X'); "
                                   + "insert into Product (ProductName) values('Nexus 6'); "
                                   + "insert into Product (ProductName) values('Nexus 5');");
            }

            void PopulateCustomerTable()
            {
                Connection.Execute("insert into Customer (IsPrivilegeCustomer, FirstName) values('Y','Test User'); ");
            }
        }
    }
}