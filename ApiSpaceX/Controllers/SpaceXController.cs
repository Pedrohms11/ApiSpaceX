using ApiSpaceX.Models;
using ApiSpaceX.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiSpaceX.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar dados da SpaceX
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SpaceXController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;

        public SpaceXController()
        {
            _firebaseService = new FirebaseService();
        }

        // ==================== ENDPOINTS DE LANÇAMENTOS ====================

        /// <summary>
        /// Obtém a lista completa de todos os lançamentos armazenados
        /// </summary>
        /// <returns>Lista de objetos Launch</returns>
        /// <response code="200">Retorna a lista de lançamentos</response>
        /// <response code="500">Erro interno ao buscar os dados</response>
        [HttpGet("launches")]
        [ProducesResponseType(typeof(List<Launch>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllLaunches()
        {
            try
            {
                var launches = await _firebaseService.GetAllLaunchesAsync();
                return Ok(launches);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        /// <summary>
        /// Obtém os detalhes de um lançamento específico pelo seu ID
        /// </summary>
        /// <param name="id">ID do lançamento (ex: 5eb87d42ffd86e000604b384)</param>
        /// <returns>Objeto Launch com os detalhes do lançamento</returns>
        /// <response code="200">Retorna o lançamento encontrado</response>
        /// <response code="404">Lançamento não encontrado</response>
        /// <response code="500">Erro interno ao buscar os dados</response>
        [HttpGet("launches/{id}")]
        [ProducesResponseType(typeof(Launch), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetLaunchById(string id)
        {
            try
            {
                var launch = await _firebaseService.GetLaunchByIdAsync(id);

                if (launch == null)
                {
                    return NotFound(new { error = $"Lançamento com ID {id} não foi encontrado." });
                }

                return Ok(launch);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        /// <summary>
        /// Recebe um novo lançamento e persiste no Firebase
        /// </summary>
        /// <param name="launch">Objeto Launch enviado pelo WPF de coleta</param>
        /// <returns>Confirmação de salvamento</returns>
        /// <response code="201">Lançamento salvo com sucesso</response>
        /// <response code="400">Dados do lançamento inválidos</response>
        /// <response code="500">Erro interno ao salvar os dados</response>
        [HttpPost("launches")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateLaunch([FromBody] Launch launch)
        {
            try
            {
                if (launch == null)
                {
                    return BadRequest(new { error = "Dados do lançamento não podem ser nulos." });
                }

                if (string.IsNullOrEmpty(launch.Id))
                {
                    return BadRequest(new { error = "O ID do lançamento é obrigatório." });
                }

                if (string.IsNullOrEmpty(launch.Name))
                {
                    return BadRequest(new { error = "O nome do lançamento é obrigatório." });
                }

                var saved = await _firebaseService.SaveLaunchAsync(launch);

                if (!saved)
                {
                    return StatusCode(500, new { error = "Não foi possível salvar o lançamento no Firebase." });
                }

                return CreatedAtAction(nameof(GetLaunchById), new { id = launch.Id }, launch);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        /// <summary>
        /// Recebe múltiplos lançamentos em lote (para sincronização inicial)
        /// </summary>
        /// <param name="launches">Lista de objetos Launch</param>
        /// <returns>Resumo da operação</returns>
        /// <response code="200">Lançamentos processados com sucesso</response>
        /// <response code="500">Erro interno ao salvar os dados</response>
        [HttpPost("launches/batch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBatchLaunches([FromBody] List<Launch> launches)
        {
            try
            {
                if (launches == null || launches.Count == 0)
                {
                    return BadRequest(new { error = "Lista de lançamentos não pode ser vazia." });
                }

                int savedCount = 0;

                foreach (var launch in launches)
                {
                    var saved = await _firebaseService.SaveLaunchAsync(launch);
                    if (saved) savedCount++;
                }

                return Ok(new
                {
                    message = $"Processados {savedCount} de {launches.Count} lançamentos.",
                    saved = savedCount,
                    total = launches.Count
                });
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        // ==================== ENDPOINTS DE FOGUETES ====================

        /// <summary>
        /// Obtém a lista completa de todos os foguetes armazenados
        /// </summary>
        /// <returns>Lista de objetos Rocket</returns>
        /// <response code="200">Retorna a lista de foguetes</response>
        /// <response code="500">Erro interno ao buscar os dados</response>
        [HttpGet("rockets")]
        [ProducesResponseType(typeof(List<Rocket>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllRockets()
        {
            try
            {
                var rockets = await _firebaseService.GetAllRocketsAsync();
                return Ok(rockets);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        /// <summary>
        /// Recebe um novo foguete e persiste no Firebase
        /// </summary>
        /// <param name="rocket">Objeto Rocket enviado pelo WPF de coleta</param>
        /// <returns>Confirmação de salvamento</returns>
        /// <response code="201">Foguete salvo com sucesso</response>
        /// <response code="400">Dados do foguete inválidos</response>
        /// <response code="500">Erro interno ao salvar os dados</response>
        [HttpPost("rockets")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateRocket([FromBody] Rocket rocket)
        {
            try
            {
                if (rocket == null)
                {
                    return BadRequest(new { error = "Dados do foguete não podem ser nulos." });
                }

                if (string.IsNullOrEmpty(rocket.Id))
                {
                    return BadRequest(new { error = "O ID do foguete é obrigatório." });
                }

                if (string.IsNullOrEmpty(rocket.Name))
                {
                    return BadRequest(new { error = "O nome do foguete é obrigatório." });
                }

                var saved = await _firebaseService.SaveRocketAsync(rocket);

                if (!saved)
                {
                    return StatusCode(500, new { error = "Não foi possível salvar o foguete no Firebase." });
                }

                return CreatedAtAction(nameof(GetAllRockets), new { id = rocket.Id }, rocket);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        /// <summary>
        /// Recebe múltiplos foguetes em lote
        /// </summary>
        /// <param name="rockets">Lista de objetos Rocket</param>
        /// <returns>Resumo da operação</returns>
        [HttpPost("rockets/batch")]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateBatchRockets([FromBody] List<Rocket> rockets)
        {
            try
            {
                if (rockets == null || rockets.Count == 0)
                {
                    return BadRequest(new { error = "Lista de foguetes não pode ser vazia." });
                }

                int savedCount = 0;

                foreach (var rocket in rockets)
                {
                    var saved = await _firebaseService.SaveRocketAsync(rocket);
                    if (saved) savedCount++;
                }

                return Ok(new
                {
                    message = $"Processados {savedCount} de {rockets.Count} foguetes.",
                    saved = savedCount,
                    total = rockets.Count
                });
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }

        // ==================== ENDPOINTS DE ESTATÍSTICAS ====================

        /// <summary>
        /// Obtém estatísticas gerais calculadas a partir dos lançamentos
        /// </summary>
        /// <returns>Objeto Stats com total, sucessos, falhas e taxa de sucesso</returns>
        /// <response code="200">Retorna as estatísticas calculadas</response>
        /// <response code="500">Erro interno ao calcular os dados</response>
        [HttpGet("stats")]
        [ProducesResponseType(typeof(Stats), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStats()
        {
            try
            {
                var stats = await _firebaseService.GetStatsAsync();
                return Ok(stats);
            }
            catch
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno ao processar sua requisição." });
            }
        }
    }
}
