using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Providers.BCRA.Data
{
    /// <summary>
    /// Respuesta a la solicitud sobre una de las principales variables
    /// durante un periodo de tiempo indicado
    /// </summary>
    public class DatosVariableResponse
    {
        /// <summary>
        /// Indica si hubo error o no. 200 => OK, 500 o 404 o 400 => Error
        /// </summary>
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("errorMessages")]
        public List<string> ErrorMessages { get; set; }

        [JsonPropertyName("results")]
        public List<DatosVariableDto> DatosVariable { get; set; }

        public bool IsSuccess()
        {
            return Status == 200;
        }

        public override string ToString()
        {
            string msg = string.Empty;
            if (Status == 200)
                msg = string.Format("Status: {0} - Variables leidas: {1}", Status, DatosVariable.Count);
            else
                msg = string.Format("Status: {0} - 1st Message: {1}", Status, ErrorMessages[0]);
            return msg;
        }
    }
}
