namespace TheCocktail.Application.Exceptions
{
    public class ExceptionResponse
    {
        public string ErrorMessage { get; set; }

        public string CorrelationIdentifier { get; set; }

        public string InnerException { get; set; }
    }
}
