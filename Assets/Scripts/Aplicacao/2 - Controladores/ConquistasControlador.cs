using Assets.Scripts.Share._3___Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConquistasControlador : MonoBehaviour
{
    public List<int> Conquistas;

    private List<int> ConquistasDisponíveis;

    private void Awake()
    {
        ConquistasDisponíveis = Conquistas.Where(p => !GameControlador.Self.Saves.Geral.ConquistasObtidas.Any(x => x == p)).ToList();
    }

    public void VerificaConquistas(TipoConquista tipoConquista)
    {
        switch (tipoConquista)
        {
            case TipoConquista.Distancia:
                VerificaDistancia();
                break;
            case TipoConquista.Obstaculos:
                VerificaDistancia();
                break;
        }
    }

    private void VerificaDistancia()
    {
    }

    public void AdicionarConquistaPlayer() 
    { 
        
    }

    public void NotificarConquista()
    {

    }
}
