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
                    foreach (var m in pv.ErrorMessages)
                        Console.WriteLine($"Error message: {m}");
                }
            }

            /// Obtenemos el detalle para una de las principales variables
            /// 
            await Console.Out.WriteLineAsync("\nReservas Internacionales al:");
            DatosVariableResponse? dvr = await bApi.GetVariable(1, new DateTime(2024, 04, 20), new DateTime(2024, 4, 25));
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
                foreach (var m in dvr!.ErrorMessages)
                    Console.WriteLine($"Error message: {m}");
            }

        }
    }
}
