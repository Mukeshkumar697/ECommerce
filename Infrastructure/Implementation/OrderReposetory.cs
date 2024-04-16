using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Project.Infrastructure.Implementation
{
    public class OrderRepository : IOrder
    {
        private readonly ProjDbContext context;
        private readonly ICart cart;
        private readonly IProduct product;
        public OrderRepository(ProjDbContext context, ICart cart, IProduct product)
        {
            this.context = context;
            this.cart = cart;
            this.product = product;
        }

        public int GetTotalAmount(int userId)
        {
            return cart.GetToTalPrice(userId);
        }

        public int GetNetAmount(int userId)
        {
            var total = context.Orders.FirstOrDefault(t => t.UserId == userId).NetAmount;
            return total;
        }   


        public bool MatchProductId(int userId)
        {
            int cartProductId = context.Carts.FirstOrDefault(c=> c.UserId == userId).ProductId;
            var productProductId = context.Products.FirstOrDefault(c=> c.ProductId ==cartProductId).ProductId;
            
            if( productProductId != null)
            {
                return true;
            }
            return false;
        }

        public bool Minus(int productId, int userId)
        {
            var result = context.Products.FirstOrDefault(c => c.ProductId == productId);
            var qty = context.Carts.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId ).Quantity;
            result.Stock = result.Stock - Convert.ToInt32(qty);

            context.SaveChanges();

            return true;
        }

        public int GetAmountById(int userId,int productId)
        {
            int sum = 0;
            var result = context.Carts.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId).UnitPrice;
            var result1 = context.Carts.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId).Quantity;
            return result * result1;
        }

        public bool CheckQuantity(int userId, int productId)
        {
            var result = context.Products.FirstOrDefault(c => c.ProductId == productId).Stock;
            var qty = context.Carts.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId).Quantity;
            int remain = result - Convert.ToInt32(qty);
            if( remain > 0 )
            {
                return true;
            }
            return false;
        }
        public string BuyNowByOrderId(int userId, int productId)
        {
            
                bool check = CheckQuantity(userId, productId);

                if (check == false)
                {
                    return "Out of stock";
                }

                else
                {
                    var Id = context.Carts.FirstOrDefault(c => c.ProductId == productId).ProductId;
                    bool m = MatchProductId(userId);

                    if (m == true && Id == productId)
                    {

                        var order = new Order
                        {
                            UserId = userId,

                            TotalAmount = GetAmountById(userId, productId),

                            ShippingCharge = 50,

                            NetAmount = GetAmountById(userId, productId) + 50
                        };

                        context.Orders.Add(order);

                        context.SaveChanges();

                        Minus(productId, userId);

                        return "your order is done.\nYour Order id is : " + context.Orders.FirstOrDefault(c => c.UserId == userId).OrderId.ToString();
                    }
                    else
                    {
                        return "Product Id not found";
                    }
                
            }
        }


        public string BuyNow(int userId)
        {
            var cart = context.Carts.FirstOrDefault(c => c.UserId == userId);
            if (cart == null)
            {
                return "Cart is empty.";
            }
            else
            {
                var Id = context.Carts.Where(p => p.UserId == userId).Select(p => p.ProductId).ToList();
                bool[] arr = new bool[Id.Count];
                int count = 0;
                foreach (var id in Id)
                {
                    if (context.Carts.FirstOrDefault(p => p.ProductId == id && p.UserId == userId).Quantity <= context.Products.FirstOrDefault(p => p.ProductId == id).Stock)
                    {
                        arr[count] = true;
                        count++;
                    }
                    else
                    {
                        arr[count] = false;
                    }
                }
                if (arr.Contains(false))
                {
                    return "out of stock.";
                }
                else
                {
                    int total = 0;
                    int p = 0;
                    foreach (var id in Id)
                    {

                        int amount = context.Products.FirstOrDefault(p => p.ProductId == id).Price;
                        int quantity = context.Carts.FirstOrDefault(u => u.ProductId == id).Quantity;
                        p = id;
                        int price = amount * quantity;
                        total += price;
                        Minus(p, userId);
                    }

                    var order = new Order
                    {
                        UserId = userId,

                        TotalAmount = total,

                        ShippingCharge = 50,

                        NetAmount = total + 50
                    };

                    context.Orders.Add(order);

                    context.SaveChanges();



                    return "your order is done.\nYour Order id is : " + context.Orders.FirstOrDefault(c => c.UserId == userId).OrderId.ToString(); ;
                }

            }
        }
    }
}

