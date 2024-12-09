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
using System.Collections.ObjectModel;
using System.Windows.Documents;

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
        public static async Task<GameAttribute> GetAttributeByIdAsync(int id)
        {
            try
            {
                var attribute = await _httpClient.GetFromJsonAsync<GameAttribute>($"Attribute/{id}");
                return attribute ?? new GameAttribute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching attribute: {ex.Message}");
                return new GameAttribute();
            }
        }
        public static async Task AddNewMatchAsync(Match match)
        {
            try
            {
                // Create an anonymous object to match the required JSON structure
                var matchPayload = new
                {
                    id = match.Id.ToString(), // Convert int to string
                    date = match.StartTime.ToString("O") // Use ISO 8601 format for DateTime
                };

                // Post the payload as JSON
                var response = await _httpClient.PostAsJsonAsync("Match", matchPayload);


            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding match: {ex.Message}");
            }
        }
        public static async Task AddRoundToMatchAsync(Match match)
        {
            try
            {
                foreach (var round in match.Rounds)
                {
                    // Create an anonymous object to match the required JSON structure
                    var payload = new
                    {
                        round_id = round.Id.ToString(), // Convert int to string
                        date = round.StartTime.ToString("O"), // Current date in ISO 8601 format
                        match = new
                        {
                            id = match.Id.ToString(), // Convert match ID to string
                            date = match.StartTime.ToString("O") // Match start date in "yyyy-MM-dd" format
                        }
                    };

                    // Post the payload as JSON
                    var response = await _httpClient.PostAsJsonAsync("Round", payload);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding match: {ex.Message}");
            }
        }
        public static async Task AddHitLog(Collection<HitLog> hitLogs)
        {
            try
            {
                foreach (var hitLog in hitLogs)
                {
                    var payload = new
                    {
                        hit_log_id = hitLog.Id.ToString(),
                        value = hitLog.Damage,
                        source_player = new
                        {
                            id = hitLog.Shooter.Id.ToString(),
                            name = "",
                            mac_gun = "",
                            mac_vest = ""
                        },
                        target_player = new
                        {
                            id = hitLog.Target.Id.ToString(),
                            name = "",
                            mac_gun = "",
                            mac_vest = ""
                        },
                        round = new
                        {
                            round_id = hitLog.Round.Id.ToString(),
                            match = new
                            {
                                id = ""
                            }
                        }

                    };

                    // Post the payload as JSON
                    var response = await _httpClient.PostAsJsonAsync("HitLog", payload);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding match: {ex.Message}");
            }
        }
        public static async Task AddShootLog(Collection<ShootLog> shootLogs)
        {
            try
            {
                foreach (var shootLog in shootLogs)
                {
                    // Create an anonymous object to match the required JSON structure
                    var payload = new
                    {
                        shoot_log_id = shootLog.Id.ToString(),
                        date = shootLog.Time.ToString("O"),
                        player = new
                        {
                            id = shootLog.Shooter.Id.ToString(),
                            name = "", 
                            mac_gun = "", 
                            mac_vest = "" 
                        },
                        round = new
                        {
                            round_id = shootLog.Round.Id.ToString(),
                            match = new
                            {
                                id = ""
                            }
                        }

                    };

                    // Post the payload as JSON
                    var response = await _httpClient.PostAsJsonAsync("ShootLog", payload);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding match: {ex.Message}");
            }
        }


    }
}
