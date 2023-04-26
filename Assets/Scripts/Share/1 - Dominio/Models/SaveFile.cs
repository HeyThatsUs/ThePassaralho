using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class SaveFile : Arquivos
    {
        public List<int> ItensAdquiridosLoja;

        public int PassaralhoAtualId;

        public int QtdPassacoins;

        public int Pontuacao;

        public float DistanciaPercorrida;

        public string UserName;

        public SaveFile() : base("SaveBug.bug")
        {
            ItensAdquiridosLoja = new List<int>();
            ItensAdquiridosLoja.Add(1);
            PassaralhoAtualId = 0;
            QtdPassacoins = 0;
            UserName = "Teste";
        }
    }

    [Serializable]
    public class Rankings
    {
        public string Nome { get; set; }
        public int LevelRecord { get; set; }
    }
}
