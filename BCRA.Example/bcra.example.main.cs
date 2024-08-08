using Providers.BCRA;
using Providers.BCRA.Data;

using System.Net;

namespace BCRA.Example
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            WebProxy proxy = new WebProxy("127.0.0.1", 8000);
            Providers.BCRA.Api bApi = new Api((WebProxy)null!);

            await MostrarPrincipalesVariables(bApi);

            string? numero = string.Empty;
            Console.WriteLine();
            Console.Write("Indique el numero de variable a obtener a continuacion: ");
            while ((numero = Console.ReadLine()) != string.Empty)
            {
                if (numero == "0")
                {
                    await MostrarPrincipalesVariables(bApi);
                }
                else
                {
                    /// Obtenemos el detalle para una de las principales variables
                    /// 
                    DatosVariableResponse? dvr = await bApi.GetVariable(Convert.ToInt32(numero), DateTime.Now.Date.AddDays(-30), DateTime.Now.Date.AddDays(-1));
                    if (dvr != null)
                    {
                        if (dvr.Status == 200)
                        {
                            foreach (var item in dvr.DatosVariable)
                                Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Ocurrio un error");
                    }

                    Console.WriteLine();
                    Console.Write("Indique el numero de variable a obtener a continuacion: ");
                }
            }
        }

        private static async Task MostrarPrincipalesVariables(Api bApi)
        {
            /// Obtenemos un listado completo sobre la metadata de las principales variables
            PrincipalesVariablesResponse? pv = await bApi.PrincipalesVariables();
            if (pv != null)
            {
                if (pv.Status == 200)
                {
                    foreach (var item in pv.PricipalesVariables)
                    {
                        Console.WriteLine(item);
                    }
                }
                else
                {
                    Console.WriteLine($"Ocurrio un error");
                }
            }
        }
    }
}
