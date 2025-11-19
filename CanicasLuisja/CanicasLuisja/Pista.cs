using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CanicasLuisja
{
    class Pista
    {
        // Ya no es Main; se llama desde Padre para ejecutar la lógica de la pista en el mismo proceso.
        public static List<ResultadoCanica> Ejecutar(int numPista, out double tiempoProceso)
        {
            Stopwatch swProceso = Stopwatch.StartNew();

            List<Canica> canicas = new List<Canica>();
            for (int i = 1; i <= 2; i++) // 2 canicas mínimo
                canicas.Add(new Canica(numPista, i));

            List<Thread> hilos = new List<Thread>();

            foreach (var c in canicas)
            {
                Thread t = new Thread(c.Correr);
                hilos.Add(t);
                t.Start();
            }

            foreach (var t in hilos)
                t.Join();

            swProceso.Stop();
            tiempoProceso = swProceso.Elapsed.TotalSeconds;

            var resultados = new List<ResultadoCanica>();
            foreach (var c in canicas)
            {
                resultados.Add(new ResultadoCanica
                {
                    Pista = c.Pista,
                    Numero = c.Numero,
                    Tiempo = c.Tiempo
                });
            }

            return resultados;
        }
    }
}