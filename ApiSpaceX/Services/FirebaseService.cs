using ApiSpaceX.Models;
using System.Text;
using System.Text.Json;

namespace ApiSpaceX.Services
{
    /// <summary>
    /// Serviço responsável pela persistência dos dados no Firebase Realtime Database
    /// </summary>
    public class FirebaseService
    {
        private readonly HttpClient _httpClient;
        private readonly string _firebaseUrl;
        private readonly JsonSerializerOptions _jsonOptions;

        public FirebaseService()
        {
            _httpClient = new HttpClient();
            // IMPORTANTE: Substitua pela URL do seu Firebase!
            // Exemplo: https://spacex-api-1234-default-rtdb.firebaseio.com/
            _firebaseUrl = "https://spacex-api-seu-projeto-default-rtdb.firebaseio.com/";

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        // ==================== LANÇAMENTOS ====================

        /// <summary>
        /// Salva um lançamento no Firebase
        /// </summary>
        /// <param name="launch">Objeto Launch a ser salvo</param>
        /// <returns>True se salvou com sucesso</returns>
        public async Task<bool> SaveLaunchAsync(Launch launch)
        {
            try
            {
                var json = JsonSerializer.Serialize(launch, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Usa o ID como chave no Firebase
                var response = await _httpClient.PutAsync(
                    $"{_firebaseUrl}launches/{launch.Id}.json",
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém todos os lançamentos do Firebase
        /// </summary>
        /// <returns>Lista de lançamentos</returns>
        public async Task<List<Launch>> GetAllLaunchesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_firebaseUrl}launches.json");

                if (!response.IsSuccessStatusCode)
                    return new List<Launch>();

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json) || json == "null")
                    return new List<Launch>();

                var dict = JsonSerializer.Deserialize<Dictionary<string, Launch>>(json, _jsonOptions);

                if (dict == null)
                    return new List<Launch>();

                return new List<Launch>(dict.Values);
            }
            catch
            {
                return new List<Launch>();
            }
        }

        /// <summary>
        /// Obtém um lançamento específico pelo ID
        /// </summary>
        /// <param name="id">ID do lançamento</param>
        /// <returns>Lançamento encontrado ou null</returns>
        public async Task<Launch?> GetLaunchByIdAsync(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_firebaseUrl}launches/{id}.json");

                if (!response.IsSuccessStatusCode)
                    return null;

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json) || json == "null")
                    return null;

                return JsonSerializer.Deserialize<Launch>(json, _jsonOptions);
            }
            catch
            {
                return null;
            }
        }

        // ==================== FOGUETES ====================

        /// <summary>
        /// Salva um foguete no Firebase
        /// </summary>
        /// <param name="rocket">Objeto Rocket a ser salvo</param>
        /// <returns>True se salvou com sucesso</returns>
        public async Task<bool> SaveRocketAsync(Rocket rocket)
        {
            try
            {
                var json = JsonSerializer.Serialize(rocket, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(
                    $"{_firebaseUrl}rockets/{rocket.Id}.json",
                    content);

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Obtém todos os foguetes do Firebase
        /// </summary>
        /// <returns>Lista de foguetes</returns>
        public async Task<List<Rocket>> GetAllRocketsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_firebaseUrl}rockets.json");

                if (!response.IsSuccessStatusCode)
                    return new List<Rocket>();

                var json = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(json) || json == "null")
                    return new List<Rocket>();

                var dict = JsonSerializer.Deserialize<Dictionary<string, Rocket>>(json, _jsonOptions);

                if (dict == null)
                    return new List<Rocket>();

                return new List<Rocket>(dict.Values);
            }
            catch
            {
                return new List<Rocket>();
            }
        }

        // ==================== ESTATÍSTICAS ====================

        /// <summary>
        /// Calcula e retorna as estatísticas gerais baseadas nos lançamentos
        /// </summary>
        /// <returns>Objeto Stats com as estatísticas calculadas</returns>
        public async Task<Stats> GetStatsAsync()
        {
            try
            {
                var launches = await GetAllLaunchesAsync();

                var totalLaunches = launches.Count;
                var successfulLaunches = launches.Count(l => l.Success);
                var failedLaunches = totalLaunches - successfulLaunches;
                var successRate = totalLaunches > 0
                    ? (double)successfulLaunches / totalLaunches * 100
                    : 0;

                return new Stats
                {
                    TotalLaunches = totalLaunches,
                    SuccessfulLaunches = successfulLaunches,
                    FailedLaunches = failedLaunches,
                    SuccessRate = Math.Round(successRate, 2)
                };
            }
            catch
            {
                return new Stats();
            }
        }

        /// <summary>
        /// Salva as estatísticas no Firebase (opcional, para cache)
        /// </summary>
        public async Task<bool> SaveStatsAsync(Stats stats)
        {
            try
            {
                var json = JsonSerializer.Serialize(stats, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync($"{_firebaseUrl}stats.json", content);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
