using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Service;
using Project.Model;
using System.Security.Policy;

namespace Project.Infrastructure.Implementation
{
    
        public class ProductRepository : IProduct
        {
            private readonly ProjDbContext context;
            public ProductRepository(ProjDbContext context)
            {
                this.context = context;
            }

        

            public List<Product> GetAllProducts()
            {
                return context.Products.ToList();
            }
            public Product GetProductById(int id)
            {
                return context.Products.FirstOrDefault(t => t.ProductId == id);
            }
            public List<Product> GetProductsByCategoryId(int categoryId)
            {
                return context.Products.Where(t => t.CategoryId == categoryId).ToList();
            }
            public bool AddProduct(Product product)
            {
                var res = context.Categories.FirstOrDefault(c => c.CategoryId == product.CategoryId);
                if (product.CategoryId == 0 ||  res == null) 
                {
                    return false;
                }
                var result = context.Products.AddAsync(product);
                context.SaveChangesAsync();
                return true;
            }

            public Product UpdateProduct(int id, Product product)
            {
                var result = context.Products.FirstOrDefault(t => t.ProductId == id);
                if (result != null)
                {
                    result.ProductName = product.ProductName;
                    result.Description = product.Description;
                    result.Price = product.Price;
                    result.Stock = product.Stock;
                    result.CategoryId = product.CategoryId;

                    context.SaveChanges();
                    return result;
                }
                return null;
            }

            public bool ProductExists(int id)
            {
                return context.Products.Any(t => t.ProductId == id);
            }

            public List<Product> SearchProduct(string productString)
            {
                var result = context.Products.Where(p => p.ProductName.Contains(productString) || p.Description.Contains(productString)).ToList();
                if (result != null)
                {
                    return result;
                }
                return null;
            }

            public Product DeleteProduct(int id)
            {
                var result = context.Products.FirstOrDefault(t => t.ProductId == id);
                if (result != null)
                {
                    context.Products.Remove(result);
                    context.SaveChanges();
                    return result;
                }
                return null;
            }

            public Category GetCategoriesByCategoryId(int categoryId)
            {
                return context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
            }

            public List<Category> GetAllCategories()
            {
                return context.Categories.ToList();
            }

            public async Task<Category> AddCategory(Category category)
            {
                var result = await context.Categories.AddAsync(category);
                await context.SaveChangesAsync();
                return result.Entity;
            }

            public Category UpdateCategory(int id, Category category)
            {
                var result = context.Categories.FirstOrDefault(t => t.CategoryId == id);
                if (result != null)
                {
                    result.CategoryName = category.CategoryName;

                    context.SaveChanges();
                    return result;
                }
                return null;
            }

            public async Task<Category> DeleteCategory(int id)
            {
                var result = await context.Categories.FirstOrDefaultAsync(t => t.CategoryId == id);
                if (result != null)
                {
                    context.Categories.Remove(result);
                    await context.SaveChangesAsync();
                    return result;
                }
                return null;
            }

            public bool CategoryExists(int id)
            {
                return context.Categories.Any(t => t.CategoryId == id);
            }
        }
    }

   
    

