namespace ApiSpaceX.Models
{
    /// <summary>
    /// Modelo que representa um lançamento da SpaceX
    /// </summary>
    public class Launch
    {
        /// <summary>
        /// Identificador único do lançamento (mesmo ID da API pública)
        /// </summary>
        /// <example>5eb87d42ffd86e000604b384</example>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Nome da missão
        /// </summary>
        /// <example>Crew-5</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o lançamento foi bem-sucedido
        /// </summary>
        /// <example>true</example>
        public bool Success { get; set; }

        /// <summary>
        /// Detalhes adicionais sobre o lançamento
        /// </summary>
        /// <example>Successfully launched astronauts to the ISS</example>
        public string Details { get; set; } = string.Empty;
    }
}
