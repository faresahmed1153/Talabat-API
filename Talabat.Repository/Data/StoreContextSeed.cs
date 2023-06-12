﻿using System.Text.Json;
using Talabat.Core.Models;
using Talabat.Core.Models.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if (brands?.Count() > 0)
                {
                    foreach (var brand in brands)
                        await dbContext.Set<ProductBrand>().AddAsync(brand);
                    await dbContext.SaveChangesAsync();
                }

            }

            if (!dbContext.ProductTypes.Any())
            {
                var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types?.Count() > 0)
                {
                    foreach (var type in types)
                        await dbContext.Set<ProductType>().AddAsync(type);
                    await dbContext.SaveChangesAsync();
                }

            }


            if (!dbContext.Products.Any())
            {
                var ProductsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                        await dbContext.Set<Product>().AddAsync(product);
                    await dbContext.SaveChangesAsync();
                }

            }


            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if (deliveryMethods?.Count() > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                        await dbContext.Set<DeliveryMethod>().AddAsync(deliveryMethod);
                    await dbContext.SaveChangesAsync();
                }

            }
        }
    }
}
