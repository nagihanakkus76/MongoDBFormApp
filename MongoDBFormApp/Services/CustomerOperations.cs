using MongoDB.Bson;
using MongoDBFormApp.Entities;

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
    }
}
