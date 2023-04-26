using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ArquivosControlador<T> where T: Arquivos
{
    private T _arquivo;


    public ArquivosControlador(T arquivo)
    {
        _arquivo = arquivo;
    }

    private void CriarDiretorio()
    {
        if (!Directory.Exists(_arquivo.Diretorio))
        {
            Directory.CreateDirectory(_arquivo.Diretorio);
        }

    }

    public bool ArquivoExiste()
    {
        return File.Exists(_arquivo.DiretorioCompleto);
    }

    public T Carregar()
    {
        if (ArquivoExiste())
        {

            var sConteudo = File.ReadAllText(_arquivo.DiretorioCompleto);

            if (_arquivo.Criptografar)
            {
                var textoBytes = Convert.FromBase64String(sConteudo);
                sConteudo = CriptografiaControlador.Descriptografar(textoBytes);             
            }

            return JsonConvert.DeserializeObject<T>(sConteudo);
        }
        return _arquivo;
    }

    public void Salvar()
    {
        CriarDiretorio();

        var sConteudo = JsonConvert.SerializeObject(_arquivo);

        if (_arquivo.Criptografar)
        {
            byte[] encryptedBytes = CriptografiaControlador.Criptografar(sConteudo);
            sConteudo = Convert.ToBase64String(encryptedBytes);
        }
        File.WriteAllText(_arquivo.DiretorioCompleto, sConteudo);
    }
}
