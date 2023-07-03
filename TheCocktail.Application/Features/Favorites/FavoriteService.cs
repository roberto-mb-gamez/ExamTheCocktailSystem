using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheCocktail.Application.Contracts.Persistence;
using TheCocktail.Application.Features.Favorites.Responses;
using TheCocktail.Domain.Entities;

namespace TheCocktail.Application.Features.Favorites
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetFavoritesByUserId(int userId);
        Task<Favorite?> GetFavoriteByUserAndDrunk(int userId, string drinkId);
        Task<AddRemoveFavoriteResponse> AddFavorite(Favorite favorite);
        Task<AddRemoveFavoriteResponse> RemoveFavorite(Favorite favorite);

    }

    public class FavoriteService : IFavoriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteService(IUnitOfWork favoriteRepository)
        {
            _unitOfWork = favoriteRepository ?? throw new ArgumentNullException(nameof(favoriteRepository));
        }

        public async Task<List<Favorite>> GetFavoritesByUserId(int userId)
        {
            return await _unitOfWork.FavoriteRepository.GetFavoritesByUserId(userId);
        }

        public async Task<Favorite?> GetFavoriteByUserAndDrunk(int userId, string drinkId)
        {
            return await _unitOfWork.FavoriteRepository.GetFavoriteByUserIdAndDrinkId(userId, drinkId);
        }

        public async Task<AddRemoveFavoriteResponse> AddFavorite(Favorite favoriteToAdd)
        {
            var response = new AddRemoveFavoriteResponse();

            var favorite = await _unitOfWork.FavoriteRepository.Add(favoriteToAdd);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Se agrego a favoritos exitosamente";
            response.Id = favorite.FavoriteId;
            response.IsAdded = true;

            return response;
        }

        public async Task<AddRemoveFavoriteResponse> RemoveFavorite(Favorite favoriteToDelete)
        {
            var response = new AddRemoveFavoriteResponse();

            var favoriteInDb = await _unitOfWork.FavoriteRepository.Get(favoriteToDelete.FavoriteId);
            response.Id = favoriteToDelete.FavoriteId;

            if (favoriteInDb == null)
            {
                response.Success = false;
                response.Message = "No se encontró información de favoritos registrada";
                response.Total = await _unitOfWork.FavoriteRepository.Count();
            }
            else
            {
                await _unitOfWork.FavoriteRepository.Delete(favoriteInDb);
                await _unitOfWork.Save();

                response.Success = true;
                response.IsAdded = false;
                response.Message = "Se elimino de favoritos exitosamente";
                response.Total = await _unitOfWork.FavoriteRepository.Count();
            }

            return response;
        }
    }
}
