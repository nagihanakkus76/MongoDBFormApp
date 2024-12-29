using MongoDB.Bson;
using MongoDB.Driver;
using MongoDBFormApp.Entities;
using System.Collections.Generic;

namespace MongoDBFormApp.Services
{
    public class CustomerOperations
    {
        public void AddCustomer(Customer customer)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var document = new BsonDocument
            {
                { "CustomerName", customer.CustomerName },
                { "CustomerSurname", customer.CustomerSurname },
                { "CustomerCity", customer.CustomerCity },
                { "CustomerBalance", customer.CustomerBalance },
                { "CustomerShoppingCount", customer.CustomerShoppingCount }
            };

            customerCollection.InsertOne(document);
        }

        public List<Customer> GetAllCustomer()
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();

            var customers = customerCollection.Find(new BsonDocument()).ToList();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in customers)
            {
                customerList.Add(new Customer
                {
                    CustomerId = customer["_id"].ToString(),
                    CustomerName = customer["CustomerName"].ToString(),
                    CustomerSurname = customer["CustomerSurname"].ToString(),
                    CustomerCity = customer["CustomerCity"].ToString(),
                    CustomerBalance = decimal.Parse(customer["CustomerBalance"].ToString()),
                    CustomerShoppingCount = int.Parse(customer["CustomerShoppingCount"].ToString())
                });
            }
            return customerList;
        }

        public void DeleteCustomer(string id)
        {
            var connection = new MongoDbConnection();
            var customerCollection = connection.GetCustomersCollection();
            var deletedValue = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            customerCollection.DeleteOne(deletedValue);
        }

       
    }
}  