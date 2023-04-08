using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorControlador : MonoBehaviour
{
    private List<TCores> _conjuntoCores;

    public List<TCores> ConjuntoCores
    {
        get
        {
            if ((_conjuntoCores == null) || (_conjuntoCores.Count == 0))
            {
                _conjuntoCores = SortearConjuntoCores();
            }
            return _conjuntoCores;
        }
    }

    public Color32 ObterCorPorTipo(TCores tipoCor)
    {
        var cor = new Cor();

        switch (tipoCor)
        {
            case TCores.Azul:
                cor.DefinirCor(15, 98, 230);
                break;

            case TCores.Vermelho:
                cor.DefinirCor(253, 68, 68);
                break;

            case TCores.Preto:
                cor.DefinirCor(0, 0, 0);
                break;

            case TCores.Roxo:
                cor.DefinirCor(170, 39, 231);
                break;

            case TCores.VerdeLimao:
                cor.DefinirCor(55, 210, 10);
                break;

            case TCores.AzulCalcinha:
                cor.DefinirCor(47, 222, 252);
                break;
            case TCores.Laranja:
                cor.DefinirCor(243, 72, 41);
                break;

            case TCores.Rosa:
                cor.DefinirCor(255, 50, 227);
                break;

            case TCores.Transaparente:
                cor.DefinirCor(0, 0, 0, 0);
                break;
        }
        return new Color32(cor.Vermelho, cor.Verde, cor.Azul, cor.Transparencia);
    }

    private List<TCores> SortearConjuntoCores()
    {
        var ListaCoresSorteadas = new List<TCores>();
        // var random = new System.Random();
        // var CorSorteada;
        // var bCorJaInserida = false;
        // var iContador = 0;

        // var possiveisCoresPorDificuldade = new TCores[] { };

        // switch (
        //     GameObject
        //         .Find("ControladorGeralJogo")
        //         .GetComponent<ControladorGeralJogo>()
        //         .dificuldadeJogo
        // )
        // {
        //     case ControladorGeralJogo.TiposDificuldade.normal:
        //     {
        //         possiveisCoresPorDificuldade = new TCores[]
        //         {
        //             TCores.AzulCalcinha,
        //             TCores.Rosa,
        //             TCores.Roxo,
        //             TCores.Laranja,
        //             TCores.Rosa
        //         };
        //         break;
        //     }
        //     case ControladorGeralJogo.TiposDificuldade.dificil:
        //     {
        //         possiveisCoresPorDificuldade = new TCores[] { TCores.Vermelho };

        //         break;
        //     }
        //     default:

        //         {
        //             possiveisCoresPorDificuldade = new TCores[]
        //             {
        //                 TCores.VerdeLimao,
        //                 TCores.Vermelho
        //             };
        //         }
        //         break;
        // }

        // do
        // {
        //     bCorJaInserida = false;
        //     var index = random.Next(possiveisCoresPorDificuldade.Length);
        //     CorSorteada = (TCores)possiveisCoresPorDificuldade.GetValue(index);

        //     for (int y = 0; y < ListaCoresSorteadas.Count; y++)
        //     {
        //         if (
        //             CorSorteada == ListaCoresSorteadas[y]
        //             && GameObject
        //                 .Find("ControladorGeralJogo")
        //                 .GetComponent<ControladorGeralJogo>()
        //                 .dificuldadeJogo != ControladorGeralJogo.TiposDificuldade.dificil
        //         )
        //         {
        //             bCorJaInserida = true;
        //         }
        //     }

        //     if (!bCorJaInserida)
        //     {
        //         ListaCoresSorteadas.Add(CorSorteada);
        //         iContador = iContador + 1;
        //     }
        // } while (iContador < 2);

        return ListaCoresSorteadas;
    }
}
