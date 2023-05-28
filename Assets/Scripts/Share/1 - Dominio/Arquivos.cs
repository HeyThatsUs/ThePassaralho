using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using Assets.Scripts.Share.Aplicacao;


[Serializable]
public class Arquivos
{
    public bool Criptografar { get; }

    [JsonIgnore]
    public string Nome { get; }
    [JsonIgnore]
    public string Diretorio
    {
        get
        {
            string _diretorio = ObterDiretorioFormatado(Application.persistentDataPath);

            switch (Plataforma)
            {
                case TPlataformas.Windows:
                    _diretorio = ObterDiretorioFormatado(Path.GetDirectoryName(Application.dataPath));
                    break;

            }

            return _diretorio;
        }
    }

    [JsonIgnore]
    public TPlataformas Plataforma
    {
        get
        {

            var _plataforma = TPlataformas.Windows;

            switch (Application.platform)
            {

                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    _plataforma = TPlataformas.Mac;
                    break;

                case RuntimePlatform.Android:
                    _plataforma = TPlataformas.Android;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    _plataforma = TPlataformas.IOS;
                    break;
            }

            return _plataforma;
        }
    }

    [JsonIgnore]
    public string DiretorioCompleto
    {
        get { return Path.Combine(Diretorio, Nome); }
    }


    public Arquivos(string nomeArquivo, bool criptografar)
    {
        Nome = nomeArquivo;
        Criptografar = criptografar;
    }

    private string ObterDiretorioFormatado(string _diretorio)
    {
        return Path.Combine(_diretorio, "files");
    }

}
