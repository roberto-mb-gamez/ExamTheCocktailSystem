namespace TheCocktail.Application.DTOs.Cocktails
{
    public class SearchCocktailsDTO
    {
        private string _search;
        public string Search
        {
            get => _search ?? string.Empty;
            set => _search = value;
        }
        public TipoBusqueda TipoBusqueda { get; set; }
    }

    public enum TipoBusqueda
    {
        Nombre = 1,
        Ingrediente
    }
}
