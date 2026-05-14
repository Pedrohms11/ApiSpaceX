namespace ApiSpaceX.Models
{
    /// <summary>
    /// Modelo que representa um foguete da SpaceX
    /// </summary>
    public class Rocket
    {
        /// <summary>
        /// Identificador único do foguete (mesmo ID da API pública)
        /// </summary>
        /// <example>5e9d0d95eda69973a809d1ec</example>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Nome do foguete
        /// </summary>
        /// <example>Falcon 9</example>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do foguete
        /// </summary>
        /// <example>Falcon 9 is a reusable, two-stage rocket...</example>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Indica se o foguete ainda está ativo
        /// </summary>
        /// <example>true</example>
        public bool Active { get; set; }

        /// <summary>
        /// Taxa de sucesso do foguete em porcentagem
        /// </summary>
        /// <example>98</example>
        public int SuccessRatePct { get; set; }
    }
}
