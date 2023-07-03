namespace TheCocktail.Domain.Entities
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public string DrinkId { get; set; }
        public string DrinkImage { get; set; }
        public string DrinkName { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
