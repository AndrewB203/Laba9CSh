using NorthwindClient.Models;
using NorthwindClient.Services;
using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using (HttpClient client = new HttpClient())
        {
            var productService = new ProductService(client);

            // GET: Получение всех продуктов
            var products = await productService.GetProductsAsync();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.product_id}, Name: {product.ProductName}");
            }

            // GET: Получение продукта по ID
            var productById = await productService.GetProductAsync(1);
            Console.WriteLine($"Product by ID: {productById.ProductName}");

            // POST: Создание нового продукта
            var newProduct = new Product { ProductName = "New Product", UnitPrice = 10.99m };
            var response = await productService.CreateProductAsync(newProduct);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product created successfully.");
            }

            // PUT: Обновление продукта
            var updatedProduct = new Product { product_id = 1, ProductName = "Updated Product" };
            response = await productService.UpdateProductAsync(1, updatedProduct);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product updated successfully.");
            }

            // DELETE: Удаление продукта
            response = await productService.DeleteProductAsync(1);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Product deleted successfully.");
            }
        }
    }
}