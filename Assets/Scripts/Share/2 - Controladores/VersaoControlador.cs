using UnityEngine;

public class VersaoControlador
{
    public string ObterVersaoDescricao()
    {
        return Application.version;
    }

    public bool VersaoTeste()
    {
        return Versao()[3] > 99;
    }

    public int[] Versao()
    {
        var versao = ObterVersaoDescricao().Split('.');

        var ano = 0;
        var mes = 0;
        var dia = 0;
        var build = 0;

        int[] numeros = new int[4];

        if (versao.Length >= 1)
            int.TryParse(versao[0], out ano);
        if (versao.Length >= 2)
            int.TryParse(versao[1], out mes);
        if (versao.Length >= 3)
            int.TryParse(versao[2], out dia);
        if (versao.Length >= 4)
            int.TryParse(versao[3], out build);

        numeros[0] = ano;
        numeros[1] = mes;
        numeros[2] = dia;
        numeros[3] = build;

        return numeros;
    }

    public string Descricao()
    {
        var descricaoVersao = ObterVersaoDescricao();
        var versao = Versao();

        if (versao[3] > 99)
        {
            descricaoVersao = descricaoVersao + " - Versão de testes";
        }

        return descricaoVersao;

    }

}
