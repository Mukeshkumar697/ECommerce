using Project.Model;

namespace Project.Infrastructure.Service
{
    public interface IProduct
    {
        List<Product> GetAllProducts();

        Product GetProductById(int id);

        List<Product> GetProductsByCategoryId(int categoryId);

        bool AddProduct(Product product);

        Product UpdateProduct(int id, Product product);

        Product DeleteProduct(int id);

        bool ProductExists(int id);

        List<Product> SearchProduct(string productString);
        Category GetCategoriesByCategoryId(int categoryId);

        List<Category> GetAllCategories();

       // Category AddCategory(Category category);

        Task<Category> AddCategory(Category category);

        Category UpdateCategory(int id, Category category);

        Task<Category> DeleteCategory(int id);

        bool CategoryExists(int id);
    }
}
