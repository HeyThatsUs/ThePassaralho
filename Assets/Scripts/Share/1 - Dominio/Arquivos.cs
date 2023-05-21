using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Arquivos
{

    public bool Criptografar { get; }

    [HideInInspector]
    public string Nome { get; }
    [HideInInspector]
    public string Diretorio;
    [HideInInspector]
    public string DiretorioCompleto;


    public Arquivos(string nomeArquivo, bool criptografar)
    {
        Nome = nomeArquivo;
        Diretorio = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ThePassaralho";
        DiretorioCompleto = $"{Diretorio}/{Nome}";
        Criptografar = criptografar;
    }

    public void SetDiretorio(string diretorioNovo)
    {
        Diretorio = diretorioNovo + "/ThePassaralho";
        DiretorioCompleto = $"{Diretorio}/{Nome}";
    }

    
}
