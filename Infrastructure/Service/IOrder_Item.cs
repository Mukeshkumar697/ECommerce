using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IOrder_Item
    {
        List<Order_Item> GetOrderedItems();

        
        void AddOrderBillsItem(int userId, Guid orderId);


    }
}
