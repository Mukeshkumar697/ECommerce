using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IOrder
    {
        int GetTotalAmount(int userId);
        int GetNetAmount(int userId);

        bool MatchProductId(int userId);
        string BuyNow(int userId);

        string BuyNowByOrderId(int userId, int productId);

        
    }
}
