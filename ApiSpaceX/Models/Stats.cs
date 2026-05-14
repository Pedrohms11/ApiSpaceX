namespace ApiSpaceX.Models
{
    /// <summary>
    /// Modelo que representa estatísticas gerais dos lançamentos
    /// </summary>
    public class Stats
    {
        /// <summary>
        /// Total de lançamentos realizados
        /// </summary>
        /// <example>194</example>
        public int TotalLaunches { get; set; }

        /// <summary>
        /// Número de lançamentos bem-sucedidos
        /// </summary>
        /// <example>187</example>
        public int SuccessfulLaunches { get; set; }

        /// <summary>
        /// Número de lançamentos com falha
        /// </summary>
        /// <example>7</example>
        public int FailedLaunches { get; set; }

        /// <summary>
        /// Taxa de sucesso geral em porcentagem
        /// </summary>
        /// <example>96.39</example>
        public double SuccessRate { get; set; }
    }
}
