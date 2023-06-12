using Talabat.Core.Models;

namespace Talabat.Core.Repositories
{
    public interface ICartRepository
    {
        Task<CustomerCart?> GetCartAsync(string cartId);

        Task<CustomerCart?> UpdateCartAsync(CustomerCart cart);

        Task<bool> DeleteCartAsync(string cartId);

    }
}
