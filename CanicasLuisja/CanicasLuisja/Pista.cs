// Pista.cs
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace CanicasLuisja
{
    class Pista
    {
        // Método que ejecuta la carrera de todas las canicas en la pista indicada
        public static List<ResultadoCanica> Ejecutar(int numPista, out double tiempoProceso)
        {
            // Cronómetro que mide el tiempo total de la "carrera" de esta pista
            Stopwatch swProceso = Stopwatch.StartNew();

            // Creamos las canicas de la pista
            List<Canica> canicas = new List<Canica>();
            for (int i = 1; i <= 2; i++) // Se crean 2 canicas mínimo
                canicas.Add(new Canica(numPista, i));

            // Lista de hilos para ejecutar las canicas en paralelo
            List<Thread> hilos = new List<Thread>();

            // Cada canica se ejecuta en un hilo independiente
            foreach (var c in canicas)
            {
                Thread t = new Thread(c.Correr); // Asociamos el método Correr de la canica al hilo
                hilos.Add(t);
                t.Start(); // Iniciamos el hilo
            }

            // Esperamos a que todos los hilos terminen antes de continuar
            foreach (var t in hilos)
                t.Join();

            swProceso.Stop();
            tiempoProceso = swProceso.Elapsed.TotalSeconds; // Tiempo total de la pista

            // Convertimos los resultados de las canicas a la estructura de ResultadoCanica
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
