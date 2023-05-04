using Assets.Scripts.Aplicacao._2___Controladores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVantagensController : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerController PlayerControlador;

    //Internas
    [HideInInspector]
    public bool AtivoEscudoProtecao { get; internal set; }
    [HideInInspector]
    public bool AtivoFoguete { get; internal set; }

    public void AtivaVantagem(TipoVantagem tipoVantagem)
    {
        switch (tipoVantagem)
        {
            case TipoVantagem.EscudoProtecao:
                if (!AtivoEscudoProtecao)
                {
                    AtivoEscudoProtecao = true;
                    PlayerControlador.ReferenciasPrefab.EscudoProtecao.SetActive(true);
                }
                break;
            case TipoVantagem.Foguete:
                if (!AtivoFoguete)
                {
                    AtivoFoguete = true;
                    PlayerControlador.ReferenciasPrefab.Foguete.SetActive(true);
                }
                else
                {
                    if (PlayerControlador.Temp_VidaFoguete < PlayerControlador.VidaFoguete)
                        PlayerControlador.Temp_VidaFoguete += PlayerControlador.VidaFoguete;
                    if (PlayerControlador.Temp_VidaFoguete > PlayerControlador.VidaFoguete + 50)
                        PlayerControlador.Temp_VidaFoguete = PlayerControlador.VidaFoguete + 50;

                    var teste = (PlayerControlador.Temp_VidaFoguete * 100) / PlayerControlador.VidaFoguete;
                    PlayerControlador.ReferenciasPrefab.Foguete_VidaDisplay.transform.localScale = new Vector3(teste / 10, 1f, 1f);
                }
                break;
        }
    }


    public void DesativaVantagem(TipoVantagem tipoVantagem)
    {
        switch (tipoVantagem)
        {
            case TipoVantagem.EscudoProtecao:
                AtivoEscudoProtecao = false;
                PlayerControlador.ReferenciasPrefab.EscudoProtecao.SetActive(false);
                break;
            case TipoVantagem.Foguete:
                AtivoFoguete = false;
                PlayerControlador.ReferenciasPrefab.Foguete.SetActive(false);
                break;
        }
    }


}
