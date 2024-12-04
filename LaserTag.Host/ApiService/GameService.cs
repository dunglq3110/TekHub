using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using TekHub.Host.Models;

namespace TekHub.Host.ApiService
{
    public static class GameService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7204/api/")
        };

        // Get all game configurations
        public static async Task<List<Config>> GetAllConfigsAsync()
        {
            try
            {
                var configs = await _httpClient.GetFromJsonAsync<List<Config>>("Config");
                return configs ?? new List<Config>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching configs: {ex.Message}");
                return new List<Config>();
            }
        }
        public static async Task<List<GameAttribute>> GetAllAttributeAsync()
        {
            try
            {
                var configs = await _httpClient.GetFromJsonAsync<List<GameAttribute>>("Attribute");
                return configs ?? new List<GameAttribute>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching configs: {ex.Message}");
                return new List<GameAttribute>();
            }
        }

    }
}
