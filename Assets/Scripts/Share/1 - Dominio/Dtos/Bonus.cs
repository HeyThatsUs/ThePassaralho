using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Bonus : MonoBehaviour
{
    public int Passacoins = 0;
    public int Vida = 0;
    public bool EhVantagem = false;
    public bool UtilizaPontosEmissao = false;
    public TipoVantagem TipoVantagem;
}
public enum TipoVantagem
{
    EscudoProtecao = 0,
    VidaExtra = 1,
    Foguete = 2,
}