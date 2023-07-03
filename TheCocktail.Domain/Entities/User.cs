using System.Collections.Generic;

namespace TheCocktail.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<Favorite> Favorites { get; set; }
    }
}
