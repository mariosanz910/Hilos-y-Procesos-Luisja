// Padre.cs
using System;
using System.Collections.Generic;

namespace CanicasLuisja
{
    class Padre
    {
        static void Main()
        {
            Console.WriteLine("=== PROCESO PADRE: Carrera de Canicas ===\n");

            // Lista global que almacenará los resultados de todas las pistas
            List<ResultadoCanica> resultadosGlobales = new List<ResultadoCanica>();

            // Ejecutamos cada pista "como si fueran procesos hijos" (aquí se hace todo en un mismo proceso)
            for (int pista = 1; pista <= 2; pista++)
            {
                double tiempoProceso;

                // Ejecuta la lógica de la pista y devuelve los resultados de las canicas de esa pista
                var resultadosLocal = Pista.Ejecutar(pista, out tiempoProceso);

                // Reconstruimos la salida como si viniera de un proceso hijo externo
                var salidaLines = new List<string>();
                foreach (var c in resultadosLocal)
                    salidaLines.Add($"CANICA;{c.Pista};{c.Numero};{c.Tiempo:F3}");
                salidaLines.Add($"TIEMPO_PROCESO;{pista};{tiempoProceso:F3}");
                string salidaCompleta = string.Join(Environment.NewLine, salidaLines);

                // Mostramos la salida del "proceso hijo"
                Console.WriteLine($"[Padre] Salida del proceso hijo (Pista {pista}):\n{salidaCompleta}");

                // Agregamos los resultados de esta pista a la clasificación global
                resultadosGlobales.AddRange(resultadosLocal);
            }

            // Ordenamos todas las canicas globalmente por tiempo
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

    // Clase para almacenar los resultados de cada canica
    class ResultadoCanica
    {
        public int Pista { get; set; }
        public int Numero { get; set; }
        public double Tiempo { get; set; }
    }
}
