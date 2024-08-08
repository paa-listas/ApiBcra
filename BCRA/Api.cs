using Providers.BCRA.Data;
using Providers.Common;

using RestSharp;

using System;
using System.Net;
using System.Threading.Tasks;

/// Ref.: https://www.bcra.gob.ar/BCRAyVos/catalogo-de-APIs-banco-central.asp
/// 


namespace Providers.BCRA
{
    /// <summary>
    /// Implementacion de la API v2.0 publicada por el Banco Central de la Republica Argentina (BCRA)
    /// El header "Accept-Language" esta fijo en "es-AR" (no se da la opcion de pedir en el otro)
    /// Ref.: https://www.bcra.gob.ar/BCRAyVos/catalogo-de-APIs-banco-central.asp
    /// </summary>
    public class Api : BaseApi, IDisposable
    {
        #region Constantes

        const string MI_NAME = "PROVIDER.BCRA";

        /// <summary>
        /// API que implementa esta clase
        /// </summary>
        public override ApiProviders YoProveoLaApi { get => ApiProviders.BCRA; set => throw new InvalidOperationException("YoProveoLaApi es de solo lectura"); }

        /// <summary>
        /// Cadena a utilizar en el pedido al proveedor
        /// </summary>
        public const string DefaultUserAgentHeader = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:105.0) Gecko/20100101 Firefox/105.0"; // "Provider.InvertirOnline/1.0";

        /// <summary>
        /// URI de la API del BCRA. Todas las solicitudes tiene
        /// como base esta URI.
        /// </summary>
        public const string BaseApiUri = "https://api.bcra.gob.ar/";

        #endregion Constantes

        #region Variables de Clase

        /// <summary>
        /// URL base a la que haremos todas las llamadas REST.
        /// </summary>
        private static Uri m_baseUri;
        private int info;
        readonly RestClient m_restClient;

        #endregion  Variables de Clase

        #region Propiedades y variables Publicas

        /// <summary>
        /// Indica si estamos logueados a la plataforma
        /// </summary>
        /// <remarks>No se usa</remarks>
        public bool IsLogged { get; private set; }

        #endregion

        #region Constructores


        /// <summary>
        /// Crea una nueva instancia de la API de Nasdaq
        /// </summary>
        /// <param name="baseUri">URI base donde dirigir los pedidos a la API de Nasdaq,
        /// <seealso cref="BaseApiUri"/> </param>
        public Api(Uri baseUri, string userAgentHeader = DefaultUserAgentHeader, WebProxy proxy = null)
        {
            m_baseUri = baseUri;

            // Opciones
            RestClientOptions options = new RestClientOptions(m_baseUri);
            options.UserAgent = userAgentHeader;
            options.Proxy = proxy;

            // RestSharp
            m_restClient = new RestClient(options);
            Log.Information("Iniciando Api BCRA");
        }

        public Api(WebProxy proxy, string userAgentHeader = DefaultUserAgentHeader) : this(new Uri(BaseApiUri), userAgentHeader, proxy)
        {

        }

        ~Api()
        {
        }
        /// <summary>
        /// De la pagina de RestSharp. Ref.: https://restsharp.dev/usage.html#api-client
        /// "The code above includes a couple of things that go beyond the "basics", and so we won't cover them here:"
        /// "    - The API client class needs to be disposable, so that it can dispose of the wrapped HttpClient instance"
        /// 
        /// </summary>
        public void Dispose()
        {
            m_restClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion Constructores

        #region Login and Logout

        /// <summary>
        /// Permite loguearse a la plataforma.
        /// No se usa, son consultas 'anonimas'
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Siempre TRUE</returns>
        public async Task<string> Login(string username, string password)
        {
            // Realmente no es necesario loguearse ni desloguearse, son consultas 'anonimas'
            IsLogged = true;

            return await Task.FromResult<string>("true");
        }

        /// <summary>
        /// Permite desloguearse de la plataforma.
        /// No se usa, son consultas 'anonimas'
        /// </summary>
        public void Logout()
        {
            IsLogged = false;
        }


        #endregion Login and Logout

        /// <summary>
        /// Devuelve una estructura que incluye una lista con las
        /// principales variables relevadas y puestas a disposicion
        /// por el BCRA
        /// </summary>
        /// <returns></returns>
        public async Task<PrincipalesVariablesResponse> PrincipalesVariables()
        {
            string remoteResource = "estadisticas/v2.0/principalesvariables";
            PrincipalesVariablesResponse pvr = null;

            RestRequest request = new RestRequest(remoteResource, Method.Get);
            request.AddHeader("Accept-Language", "es-AR");

            RestResponse<PrincipalesVariablesResponse> nResponse = await m_restClient.ExecuteAsync<PrincipalesVariablesResponse>(request);

            if (nResponse != null)
            {
                if (nResponse.IsSuccessful == false)
                {
                    Log.Error("No pudimos obtener informacion sobre las principales variables del BCRA. Mensaje: {0}", nResponse.ErrorMessage);
                }
                else
                {
                    if (nResponse.StatusCode == HttpStatusCode.OK)
                    {
                        if (nResponse.Data.IsSuccess())
                            pvr = nResponse.Data;
                    }
                }
            }

            return pvr;
        }

        /// <summary>
        /// Para una variable determinada, esta funcion permite obtener los valores
        /// historicos entre un par de fechas dado
        /// </summary>
        /// <param name="idVariable"></param>
        /// <param name="desde">Corresponde a la fecha de inicio del rango a consultar, la misma deberá tener el formato yyyy-mm-dd.</param>
        /// <param name="hasta">Corresponde a la fecha de fin del rango a consultar, la misma deberá tener el formato yyyy-mm-dd.</param>
        /// <returns></returns>
        public async Task<DatosVariableResponse> GetVariable(int idVariable, DateTime desde, DateTime hasta)
        {
            // Full URL: /estadisticas/v1/DatosVariable/{idVariable}/{desde}/{hasta}
            // https://api.bcra.gob.ar/estadisticas/v1/DatosVariable/1/2024-04-20/2024-04-24
            string remoteResource = string.Format("/estadisticas/v2.0/datosvariable/{0}/{1}/{2}", idVariable, desde.Date.ToString("yyyy-MM-dd"), hasta.Date.ToString("yyyy-MM-dd"));

            DatosVariableResponse dvr = null;
            RestRequest request = new RestRequest(remoteResource, Method.Get);
            request.AddHeader("Accept-Language", "es-AR");

            RestResponse<DatosVariableResponse> nResponse = await m_restClient.ExecuteAsync<DatosVariableResponse>(request);

            if (nResponse != null)
            {
                if (nResponse.IsSuccessful == false)
                {
                    Log.Error("No pudimos obtener informacion sobre la variables del BCRA. Mensaje: {0}", nResponse.ErrorMessage);
                }
                else
                {
                    if (nResponse.StatusCode == HttpStatusCode.OK)
                    {
                        //if(nResponse.Data.)
                        dvr = nResponse.Data;
                    }
                }
            }

            return dvr;
        }
    }
}
