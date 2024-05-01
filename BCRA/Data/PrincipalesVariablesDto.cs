using System.Text.Json.Serialization;

namespace Providers.BCRA.Data
{
    /// <summary>
    /// Descripcion del BCRA de cada una de las principales
    /// variables que releva y publica
    /// </summary>
    public class PrincipalesVariablesDto
    {
        [JsonPropertyName("idVariable")]
        public int IdVariable { get; set; }

        [JsonPropertyName("cdSerie")]
        public int CdSerie { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; }

        [JsonPropertyName("fecha")]
        public string Fecha { get; set; }

        [JsonPropertyName("valor")]
        public string Valor { get; set; }

        public override string ToString()
        {
            string msg = string.Format("[{0}]  Id: {1} - Serie: {2} - Valor: {3} - Descr.: {4}", Fecha, IdVariable, CdSerie, Valor, Descripcion);

            return msg;
        }
    }
}
