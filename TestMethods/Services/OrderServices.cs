using LinqToDB;
using LinqToDB.EntityFrameworkCore; 
using TestMethods.Models;

namespace TestMethods.Services
{
    public class OrderService
    {
        public List<CustomerOrderInfo> GetOrderCounts(int limit)
        {
            using (var db = new BikeStoresContext())
            {

                var query = db.Orders.ToLinqToDBTable()
                    .GroupBy(o => o.CustomerId)
                    .Select(g => new CustomerOrderInfo
                    {
                        CustomerId = g.Key,
                        TotalOrders = g.Count()
                    }).OrderByDescending(x => x.TotalOrders)
                    .Take(limit);

                return query.ToList();
            }
        }
    }
}