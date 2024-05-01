using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace Providers.BCRA.Data
{
    /// <summary>
    /// Respuesta a la solicitud de obtener la descripcion detallada de
    /// la metadata de las principales variables que meneja el BCRA
    /// </summary>
    public class PrincipalesVariablesResponse
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("errorMessages")]
        public List<string> ErrorMessages { get; set; }

        [JsonPropertyName("results")]
        public List<PrincipalesVariablesDto> PricipalesVariables { get; set; }

        public override string ToString()
        {
            string msg = string.Empty;
            if (Status == 200)
                msg = string.Format("Status: {0} - Variables leidas: {1}", Status, PricipalesVariables.Count);
            else
                msg = string.Format("Status: {0} - 1st Message: {1}", Status, ErrorMessages[0]);
            return msg;
        }
    }
}
