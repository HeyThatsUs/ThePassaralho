using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class SaveFile
    {
        public List<int> ItensAdquiridosLoja;

        public int PassaralhoAtualId;

        public int QtdPassacoins;

        public string UserName;

        public SaveFile()
        {
            ItensAdquiridosLoja= new List<int>();
            PassaralhoAtualId=0;
            QtdPassacoins=0;
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
