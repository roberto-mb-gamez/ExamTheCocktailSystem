using System.Collections.Generic;

namespace TheCocktail.Application.DTOs.CocktailDBAPI
{
    public class CocktailDbDTO
    {
        public IEnumerable<DrinkDTO> drinks { get; set; }

        public CocktailDbDTO()
        {
            drinks = new List<DrinkDTO>();
        }
    }

    public class DrinkDTO
    {
        public string idDrink { get; set; }
        public string strDrink { get; set; }
        public string strDrinkThumb { get; set; }
    }

}
