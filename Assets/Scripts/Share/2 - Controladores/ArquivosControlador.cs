using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class ArquivosControlador<T> where T: Arquivos
{
    public T Arquivo;


    public ArquivosControlador(T arquivo)
    {
        Arquivo = arquivo;
    }

    private void CriarDiretorio()
    {
        if (!Directory.Exists(Arquivo.Diretorio))
        {
            Directory.CreateDirectory(Arquivo.Diretorio);
        }

    }

    public bool ArquivoExiste()
    {
        return File.Exists(Arquivo.DiretorioCompleto);
    }

    public T Carregar()
    {
        if (ArquivoExiste())
        {

            var sConteudo = File.ReadAllText(Arquivo.DiretorioCompleto);

            if (Arquivo.Criptografar)
            {
                var textoBytes = Convert.FromBase64String(sConteudo);
                sConteudo = CriptografiaControlador.Descriptografar(textoBytes);             
            }

            return JsonConvert.DeserializeObject<T>(sConteudo);
        }
        return Arquivo;
    }

    public void Salvar()
    {
        CriarDiretorio();

        var sConteudo = JsonConvert.SerializeObject(Arquivo);

        if (Arquivo.Criptografar)
        {
            byte[] encryptedBytes = CriptografiaControlador.Criptografar(sConteudo);
            sConteudo = Convert.ToBase64String(encryptedBytes);
        }
        File.WriteAllText(Arquivo.DiretorioCompleto, sConteudo);
    }
}
