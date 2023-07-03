using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TheCocktail.Application.Contracts.ExternalServices;
using TheCocktail.Application.DTOs.CocktailDBAPI;
using TheCocktail.Application.Exceptions;
using TheCocktail.Common.Options;
using TheCocktail.Common.Validators;

namespace TheCocktail.ExternalServices.CocktailDBAPI.Services
{
    public class CocktailDBService : ICocktailDBService
    {
        // Tipo de contenido para serializar en solicitudes tipo POST, PUT, PATCH
        private const string ContentType = "application/json";

        private readonly IOptions<ApplicationSettings> _applicationSettings;

        private readonly HttpClient _httpClient;

        public CocktailDBService(
            IHttpClientFactory httpClientFactory,
            IOptions<ApplicationSettings> applicationSettings
        )
        {
            _httpClient = httpClientFactory.CreateClient();
            _applicationSettings = applicationSettings;
        }

        public async Task<CocktailDbDTO> GetCocktailsByName(string name)
        {
            NotNullValidator.ThrowIfNull(name, nameof(name));

            using var dataRequest = new HttpRequestMessage(HttpMethod.Get, $"{_applicationSettings.Value.TheCocktailDBAPIEndpoint}search.php?s={name}");
            var cocktailsResponse = await _httpClient.SendAsync(dataRequest).ConfigureAwait(false);

            if(!cocktailsResponse.IsSuccessStatusCode)
            {
                await ThrowServiceToServiceErrors(cocktailsResponse);
            }

            if(cocktailsResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                CocktailDbDTO? cocktails = new CocktailDbDTO();

                try
                {
                    cocktails = await cocktailsResponse.Content
                        .ReadFromJsonAsync<CocktailDbDTO>()
                        .ConfigureAwait(false);
                }
                catch
                {
                }

                if(cocktails != null && cocktails.drinks == null) cocktails.drinks = new List<DrinkDTO>();  
                
                return cocktails ?? new CocktailDbDTO();
            }
            else
            {
                return new CocktailDbDTO();
            }
        }

        public async Task<CocktailDbDTO> GetCocktailsByIngredient(string ingredient)
        {
            NotNullValidator.ThrowIfNull(ingredient, nameof(ingredient));

            if (ingredient == string.Empty) return new CocktailDbDTO();

            using var dataRequest = new HttpRequestMessage(HttpMethod.Get, $"{_applicationSettings.Value.TheCocktailDBAPIEndpoint}filter.php?i={ingredient}");
            var cocktailsResponse = await _httpClient.SendAsync(dataRequest).ConfigureAwait(false);

            if (!cocktailsResponse.IsSuccessStatusCode)
            {
                await ThrowServiceToServiceErrors(cocktailsResponse);
            }

            if (cocktailsResponse.StatusCode != System.Net.HttpStatusCode.NoContent 
                && cocktailsResponse.IsSuccessStatusCode)
            {
                CocktailDbDTO? cocktails = new CocktailDbDTO();

                try
                {
                    cocktails = await cocktailsResponse.Content
                    .ReadFromJsonAsync<CocktailDbDTO>()
                    .ConfigureAwait(false);
                }
                catch
                {   
                }

                return cocktails ?? new CocktailDbDTO();
            }
            else
                return new CocktailDbDTO();
        }

        public async Task<CocktailDetailDbDTO> GetCocktailById(string id)
        {
            using var dataRequest = new HttpRequestMessage(HttpMethod.Get, $"{_applicationSettings.Value.TheCocktailDBAPIEndpoint}lookup.php?i={id}");
            var cocktailsResponse = await _httpClient.SendAsync(dataRequest).ConfigureAwait(false);

            if (!cocktailsResponse.IsSuccessStatusCode)
            {
                await ThrowServiceToServiceErrors(cocktailsResponse);
            }

            if (cocktailsResponse.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                CocktailDetailDbDTO? cocktail = new CocktailDetailDbDTO();

                try
                {
                    cocktail = await cocktailsResponse.Content
                        .ReadFromJsonAsync<CocktailDetailDbDTO?>()
                        .ConfigureAwait(false);
                }
                catch
                {
                }

                if (cocktail != null && cocktail.drinks == null) cocktail.drinks = new List<DrinkDetailDTO>();

                return cocktail ?? new CocktailDetailDbDTO();
            }
            else
            {
                return new CocktailDetailDbDTO();
            }
        }

        private async Task ThrowServiceToServiceErrors(HttpResponseMessage response)
        {
            //var exceptionReponse = await response.Content.ReadFromJsonAsync<ExceptionResponse>().ConfigureAwait(false);
            var exceptionReponse = new ExceptionResponse
            {
                ErrorMessage = response.RequestMessage.ToString(),
            };

            throw new Exception(exceptionReponse?.InnerException);
        }
    }
}
