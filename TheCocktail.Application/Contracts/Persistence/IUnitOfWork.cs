using System;
using System.Threading.Tasks;

namespace TheCocktail.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IFavoriteRepository FavoriteRepository { get; }
        Task Save();
    }
}
