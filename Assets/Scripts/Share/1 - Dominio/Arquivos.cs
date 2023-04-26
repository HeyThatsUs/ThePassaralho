using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Arquivos
{
    public bool Criptografar { get; }

    public string Nome { get; }
    public string Diretorio { get; }
    public string DiretorioCompleto { get; }


    public Arquivos(string nomeArquivo)
    {
        Nome = nomeArquivo;
        Diretorio = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/ThePassaralho";
        DiretorioCompleto = $"{Diretorio}/{Nome}";
        Criptografar = false;
    }

    
}
