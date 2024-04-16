using Project.Infrastructure.Implementation;
using Project.Model;
using static Project.Infrastructure.Implementation.CartRepository;

namespace Project.Infrastructure.Service
{
    public interface ICart
    {
        Task<Cart> AddToCart(int productId, int userId);
        List<Cart> GetAllCartItems(int userId);
        //void UpdateCartItemDB(int userId, ShoppingCartUpdate[] CartItemsUpdate);
        int GetToTalPrice(int userId);
        bool RemoveItem(int productId, int userId);

        string UpdateItem(int productId, int userId, int quantity);

        void EmptyCart(int userId);

        int GetCount(int userId);
    }
}
