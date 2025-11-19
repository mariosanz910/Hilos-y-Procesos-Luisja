using System;
using System.Diagnostics;
using System.Threading;

namespace CanicasLuisja
{
    public class Canica
    {
        public int Pista { get; set; }
        public int Numero { get; set; }
        public double Tiempo { get; set; }
        private Random rand;

        public Canica(int pista, int numero)
        {
            Pista = pista;
            Numero = numero;
            rand = new Random(Environment.TickCount + pista * 100 + numero);
        }

        public void Correr()
        {
            Stopwatch sw = Stopwatch.StartNew();
            int avance = 0;
            int meta = 100;

            while (avance < meta)
            {
                avance += rand.Next(1, 6);
                Thread.Sleep(rand.Next(20, 50));
            }

            sw.Stop();
            Tiempo = sw.Elapsed.TotalSeconds;
        }
    }
}
