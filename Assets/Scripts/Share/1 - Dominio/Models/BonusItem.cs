using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BonusItem 
{
    [SerializeField] public int Passacoins = 0;
    [SerializeField] public int Vida = 0;
    [SerializeField] public bool EhVantagem = false;
    [SerializeField] public bool Ativo = false;
    [SerializeField] public TipoVantagem TipoVantagem;
    [SerializeField] public GameObject GameObject;
}
public enum TipoVantagem
{
    EscudoProtecao = 0,
    VidaExtra = 1,
    Foguete = 2,
}