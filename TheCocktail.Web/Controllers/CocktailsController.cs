using Microsoft.AspNetCore.Mvc;
using TheCocktail.Application.DTOs.Cocktails;
using TheCocktail.Application.Features.Cocktails;

namespace TheCocktail.Web.Controllers
{
    public class CocktailsController : Controller
    {
        private readonly ICocktailService _cocktailService;

        private const int USER_TEST = 1;

        public CocktailsController(
            ICocktailService cocktailService
        )
        {
            _cocktailService = cocktailService ?? throw new ArgumentNullException(nameof(cocktailService));
        }

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SearchCocktails(SearchCocktailsDTO searchViewModel)
            =>  View(await _cocktailService.GetCocktailsByNameOrIngredient(searchViewModel));

        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Detail(string cocktailId)
            => View(await _cocktailService.GetCocktailDetail(USER_TEST, cocktailId));
    }
}
