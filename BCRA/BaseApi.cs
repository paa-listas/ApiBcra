namespace Providers.Common
{
    /// <summary>
    /// Enum con el proveedor que soporta
    /// </summary>
    public enum ApiProviders { BCRA = 5 }
    
    /// <summary>
    /// Balse base
    /// </summary>
    public abstract class BaseApi
    {
        /// <summary>
        /// Devuelve el tipo de API que provee esta implementacion.
        /// </summary>
        public abstract ApiProviders YoProveoLaApi { get; set; }
    }
}
