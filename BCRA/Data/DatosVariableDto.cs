using System.Text.Json.Serialization;

namespace Providers.BCRA.Data
{
    /// <summary>
    /// Informacion del valor de la variable solicitada
    /// </summary>
    public class DatosVariableDto
    {
        [JsonPropertyName("idVariable")]
        public int IdVariable { get; set; }

        [JsonPropertyName("fecha")]
        public string Fecha { get; set; }

        [JsonPropertyName("valor")]
        public string Valor { get; set; }

        public override string ToString()
        {
            string msg = string.Format("[{0}] {1}", Fecha, Valor);

            return msg;
        }
    }
}
