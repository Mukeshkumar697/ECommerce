
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Project.Infrastructure.Implementation
{
    public class Order_ItemReposetory : IOrder_Item
    {
        private readonly ProjDbContext context;
     
        public Order_ItemReposetory(ProjDbContext context)
        {
            this.context = context;
        }
        public  List<Order_Item> GetOrderedItems()
        {
            return context.Order_Items.ToList();
        }

        public void AddOrderBillsItem(int userId, Guid orderId)
        {
            var result = context.Carts.Where(t => t.UserId == userId).ToList();
        
            foreach(var item in result)
            {
                var orderitem = new Order_Item()
                {
                    
                    ProductId = item.ProductId,
                    OrderId = orderId,
                    ProductName = context.Products.First(c=> c.ProductId == item.ProductId ).ProductName,
                    UserId = userId,
                    Quantity = item.Quantity,
                    Price = item.UnitPrice,
                    TotalAmount = item.Quantity * item.UnitPrice


                };
                context.Order_Items.Add(orderitem);
                context.SaveChanges();
            }
            

           
           

        }

    }
}
