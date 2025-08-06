using TodoWebApp.Models;

namespace TodoWebApp.Services
{
    public class TMDbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public TMDbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["TMDb:ApiKey"] ?? "";
        }

        public async Task<TMDbResponse?> GetPopularMoviesAsync()
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return null;
            }
            return await _httpClient.GetFromJsonAsync<TMDbResponse>($"movie/popular?api_key={_apiKey}");
        }
    }
}
