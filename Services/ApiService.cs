using System.Net.Http.Json;
using System.Text.Json;
using RaceDayMVC.Models;

namespace RaceDayMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserSessionViewModel?> LoginAsync(
            LoginViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/useraccounts/login", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<UserSessionViewModel>();
        }

        public async Task<UserSessionViewModel?> RegisterAsync(
            RegisterViewModel model)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/useraccounts", model);

            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content
                .ReadFromJsonAsync<UserSessionViewModel>();
        }

        public async Task<List<EventViewModel>> GetEventsAsync()
        {
            var response = await _httpClient.GetAsync("api/events");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Unable to retrieve events from the API.");
            }

            var events = await response.Content.ReadFromJsonAsync<List<EventViewModel>>();

            return events ?? new List<EventViewModel>();
        }
    }
}
