using System;
using System.Collections.Generic;

namespace CanicasLuisja
{
    class Padre
    {
        static void Main()
        {
            Console.WriteLine("=== PROCESO PADRE: Carrera de Canicas ===\n");

            List<ResultadoCanica> resultadosGlobales = new List<ResultadoCanica>();

            for (int pista = 1; pista <= 2; pista++)
            {
                double tiempoProceso;
                var resultadosLocal = Pista.Ejecutar(pista, out tiempoProceso);

                // Reconstruimos la salida que antes producía el proceso hijo
                var salidaLines = new List<string>();
                foreach (var c in resultadosLocal)
                    salidaLines.Add($"CANICA;{c.Pista};{c.Numero};{c.Tiempo:F3}");
                salidaLines.Add($"TIEMPO_PROCESO;{pista};{tiempoProceso:F3}");
                string salidaCompleta = string.Join(Environment.NewLine, salidaLines);

                Console.WriteLine($"[Padre] Salida del proceso hijo (Pista {pista}):\n{salidaCompleta}");

                // Añadimos resultados a la clasificación global
                resultadosGlobales.AddRange(resultadosLocal);
            }

            resultadosGlobales.Sort((a, b) => a.Tiempo.CompareTo(b.Tiempo));

            Console.WriteLine("\n=== CLASIFICACIÓN FINAL GLOBAL ===");
            int pos = 1;
            foreach (var r in resultadosGlobales)
            {
                Console.WriteLine($"Pos {pos}: Canica {r.Numero} (Pista {r.Pista}) - {r.Tiempo:F2}s");
                pos++;
            }
        }
    }

    class ResultadoCanica
    {
        public int Pista { get; set; }
        public int Numero { get; set; }
        public double Tiempo { get; set; }
    }
}