using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Arquivos
{
    public bool Criptografar { get; }

    public string Nome { get; }
    public string Diretorio;
    public string DiretorioCompleto;


    public Arquivos(string nomeArquivo)
    {
        Nome = nomeArquivo;
        Diretorio = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ThePassaralho";
        DiretorioCompleto = $"{Diretorio}/{Nome}";
        Criptografar = false;
    }

    public void SetDiretorio(string diretorioNovo)
    {
        Diretorio = diretorioNovo + "/ThePassaralho";
        DiretorioCompleto = $"{Diretorio}/{Nome}";
    }

    
}
