using Assets.Models;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private VersaoControlador _Versao;
    public SaveFile Geral;

    private ArquivosControlador<SaveFile> arquivoSaveFile;

    public void Carregar()
    {
        if (arquivoSaveFile == null)
        {
            if (Geral == null)
            {
                _Versao = new VersaoControlador();
                this.Geral = new SaveFile(!_Versao.VersaoTeste());
            }
                this.arquivoSaveFile = new ArquivosControlador<SaveFile>(Geral);
        }
        this.Geral = arquivoSaveFile.Carregar();
    }

    public void Salvar()
    {
        arquivoSaveFile.Salvar();
    }

    private void OnApplicationQuit()
    {
        Salvar();
    }
}
