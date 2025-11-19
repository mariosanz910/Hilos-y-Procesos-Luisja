// Canica.cs
using System;
using System.Diagnostics;
using System.Threading;

namespace CanicasLuisja
{
    public class Canica
    {
        // Número de pista en la que está la canica
        public int Pista { get; set; }

        // Número de la canica (identificador)
        public int Numero { get; set; }

        // Tiempo que tarda en completar la carrera
        public double Tiempo { get; set; }

        // Generador de números aleatorios para la carrera
        private Random rand;

        // Constructor de la canica: inicializa pista, número y el generador de números aleatorios
        public Canica(int pista, int numero)
        {
            Pista = pista;
            Numero = numero;

            // Semilla aleatoria basada en tiempo + datos de la canica para evitar que todas generen la misma secuencia
            rand = new Random(Environment.TickCount + pista * 100 + numero);
        }

        // Método que simula la carrera de la canica
        public void Correr()
        {
            // Stopwatch para medir el tiempo que tarda la canica en llegar a la meta
            Stopwatch sw = Stopwatch.StartNew();

            int avance = 0;     // Posición actual de la canica en la pista
            int meta = 100;     // Distancia final de la pista

            // Mientras la canica no llegue a la meta
            while (avance < meta)
            {
                // Avanza un valor aleatorio entre 1 y 5
                avance += rand.Next(1, 6);

                // Pausa el hilo entre 20 y 50 ms para simular el tiempo de movimiento
                Thread.Sleep(rand.Next(20, 50));
            }

            sw.Stop();
            // Guardamos el tiempo total de la carrera en segundos
            Tiempo = sw.Elapsed.TotalSeconds;
        }
    }
}
