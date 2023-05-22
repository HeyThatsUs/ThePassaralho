using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bonus : MonoBehaviour
{
    public int Passacoins = 0;
    public int Vida = 0;
    public bool UtilizaPontosEmissao = false;
    public TipoVantagem TipoVantagem;
}
public enum TipoVantagem
{
    VidaExtra,
    Vida,
    Passacoins,
    EscudoProtecao,
    Foguete,
}