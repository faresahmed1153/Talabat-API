using Talabat.Core.Models;
using Talabat.Core.Repositories;

namespace Talabat.Core
{
    public interface IUnitOfWork : IAsyncDisposable
    {

        IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : BaseModel;


        Task<int> Complete();
    }
}
