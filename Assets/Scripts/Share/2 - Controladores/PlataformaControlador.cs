using Assets.Scripts.Share.Aplicacao;
using UnityEngine;

public class PlataformaControlador
{
    private TPlataformas _plataforma;


    public TPlataformas ObterPlataforma()
    {
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
            case RuntimePlatform.GameCoreXboxSeries:
            case RuntimePlatform.GameCoreXboxOne:
                _plataforma = TPlataformas.XBOX;
                break;
            case RuntimePlatform.PS4:
            case RuntimePlatform.PS5:
                _plataforma = TPlataformas.PlayStation;
                break;
            case RuntimePlatform.Switch:
                _plataforma = TPlataformas.NintedoSwitch;
                break;


        }

        return _plataforma;

    }

}
