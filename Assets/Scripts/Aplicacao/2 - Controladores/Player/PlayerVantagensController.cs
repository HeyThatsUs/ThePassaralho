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

    public TipoVantagem? VantagemAtual;

    public void GerenciaVantagens(Bonus bonus)
    {

        switch (bonus.TipoVantagem)
        {
            case TipoVantagem.Passacoins:
                GameControlador.Self.Saves.Geral.Moedas += bonus.Passacoins;
                break;
            case TipoVantagem.Vida:
                PlayerControlador.VidaAtual += bonus.Vida;
                break;
            case TipoVantagem.VidaExtra:
                PlayerControlador.QtdVidas++;
                break;
            case TipoVantagem.EscudoProtecao:
                if (VantagemAtual.HasValue) DesativaVantagem(VantagemAtual.Value);
                PlayerControlador.ReferenciasPrefab.EscudoProtecao.SetActive(true);
                AtivaVantagem(TipoVantagem.EscudoProtecao);
                break;
            case TipoVantagem.Foguete:
                if (VantagemAtual.HasValue) DesativaVantagem(VantagemAtual.Value);
                AtivaVantagem(TipoVantagem.Foguete);
                if (PlayerControlador.TipoGameplay != Assets.Scripts.Share._3___Enums.GameplayTipo.Nave)
                    PlayerControlador.AtivaGameplayNave();
                break;
        }
    }

    private void AtivaVantagem(TipoVantagem tipoVantagem)
    {
        switch (tipoVantagem)
        {
            case TipoVantagem.EscudoProtecao:
                if (VantagemAtual == TipoVantagem.EscudoProtecao)
                {
                    AtivoEscudoProtecao = true;
                    PlayerControlador.ReferenciasPrefab.EscudoProtecao.SetActive(true);
                }
                break;
            case TipoVantagem.Foguete:
                if (VantagemAtual == TipoVantagem.Foguete)
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

        VantagemAtual = tipoVantagem;
    }


    public void DesativaVantagem(TipoVantagem tipoVantagem)
    {
        switch (tipoVantagem)
        {
            case TipoVantagem.EscudoProtecao:
                PlayerControlador.ReferenciasPrefab.EscudoProtecao.SetActive(false);
                break;
            case TipoVantagem.Foguete:
                PlayerControlador.ReferenciasPrefab.Foguete.SetActive(false);
                break;
        }
    }


}
