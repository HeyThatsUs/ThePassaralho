using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Share._1___Dominio
{
    public static class UtilitarioRandom
    {
        public static int GerarNumeroAleatorio(int valorInicial, int valorFinal)
        {
            Random rand = new Random();
            return rand.Next(valorInicial, valorFinal + 1);
        }
    }
}
