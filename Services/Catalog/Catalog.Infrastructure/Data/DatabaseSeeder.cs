using Catalog.Core.Entities;
using Catalog.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Text.Json;

namespace Catalog.Infrastructure.Data;

public class DatabaseSeeder
{
    public static async Task SeedAsync(IOptions<DatabaseSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        var db = client.GetDatabase(settings.DatabaseName);
        var brands = db.GetCollection<ProductBrand>(settings.BrandCollectionName);
        var types = db.GetCollection<ProductType>(settings.TypeCollectionName);
        var products = db.GetCollection<Product>(settings.ProductCollectionName);

        var SeedBasePath = Path.Combine("Data", "SeedData");

        // Seed Brands
        List<ProductBrand> brandList = new ();
        if((await brands.CountDocumentsAsync(_ => true)) == 0)
        {
            var brandData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "brands.json"));
            brandList = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
            await brands.InsertManyAsync(brandList);
        }
        else
        {
            // If brands already exist, load them to map with products later
            brandList = await brands.Find(_ => true).ToListAsync();
        }

        // Seed Types
        List<ProductType> typeList = new ();
        if((await types.CountDocumentsAsync(_ => true)) == 0)
        {
            var typeData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "types.json"));
            typeList = JsonSerializer.Deserialize<List<ProductType>>(typeData);
            await types.InsertManyAsync(typeList);
        }
        else
        {
            // If types already exist, load them to map with products later
            typeList = await types.Find(_ => true).ToListAsync();
        }

        // Seed Products
        if ((await products.CountDocumentsAsync(_ => true)) == 0)
        {
            var productData = await File.ReadAllTextAsync(Path.Combine(SeedBasePath, "products.json"));
            var productList = JsonSerializer.Deserialize<List<Product>>(productData);
            // Map the brand and type names to their respective IDs
            foreach (var product in productList)
            {
                // Reset Id to let MongoDB generate a new one
                product.Id = null;
                // Default CreatedDate to now if not set
                if(product.CreatedDate == default)
                    product.CreatedDate = DateTime.UtcNow;
                
            }
            await products.InsertManyAsync(productList);
        }
    }
}