using System.Threading.Tasks;
using TheCocktail.Application.DTOs.CocktailDBAPI;

namespace TheCocktail.Application.Contracts.ExternalServices
{
    public interface ICocktailDBService
    {
        Task<CocktailDbDTO> GetCocktailsByName(string name);
        Task<CocktailDbDTO> GetCocktailsByIngredient(string ingredient);
        Task<CocktailDetailDbDTO> GetCocktailById(string id);
    }
}
