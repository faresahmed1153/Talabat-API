using StackExchange.Redis;
using System.Text.Json;
using Talabat.Core.Models;

namespace Talabat.Core.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly IDatabase _database;

        public CartRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string cartId)
        {
            return await _database.KeyDeleteAsync(cartId);
        }

        public async Task<CustomerCart?> GetCartAsync(string cartId)
        {
            var cart = await _database.StringGetAsync(cartId);
            return cart.IsNull ? null : JsonSerializer.Deserialize<CustomerCart>(cart);
        }

        public async Task<CustomerCart?> UpdateCartAsync(CustomerCart cart)
        {
            var updatedOrCreatedCart = await _database.StringSetAsync(cart.Id, JsonSerializer.Serialize(cart), TimeSpan.FromDays(1));
            if (updatedOrCreatedCart is false) return null;
            return await GetCartAsync(cart.Id);
        }
    }
}
