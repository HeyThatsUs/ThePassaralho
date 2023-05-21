using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    [Serializable]
    public class SaveFile : Arquivos
    {
        public int Moedas;

        public string NomeUsuario;

        public int QuantidadeMortes;

        public List<int> ItensAdquiridosLoja;

        public string PassaralhoSelecionado;

        public int Pontuacao;

        public float DistanciaPercorrida;

        

        public SaveFile(bool criptografar) : base("Geral.game", criptografar)
        {
            ItensAdquiridosLoja = new List<int>();
            ItensAdquiridosLoja.Add(1);
            PassaralhoSelecionado = "Passaralho";
            NomeUsuario = "";
            Moedas = 0;
        }
    }

    [Serializable]
    public class Rankings
    {
        public string Nome { get; set; }
        public int LevelRecord { get; set; }
    }
}
