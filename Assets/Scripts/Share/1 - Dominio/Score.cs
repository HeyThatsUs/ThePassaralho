using System;
using UnityEngine;
 
[Serializable]
public class Score
{
    public string Nome { get; }
    public int Pontucacao { get; }


    public Score(string nome, int pontuacao) {
        this.Nome = nome;
        this.Pontucacao = pontuacao;
        }
}
