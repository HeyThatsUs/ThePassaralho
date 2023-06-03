using Assets.Models;
using Assets.Scripts.Share._1___Dominio;
using Assets.Scripts.Share._3___Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Aplicacao._2___Controladores
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Referencias")]
        public GameObject Passaralho;
        public MenusControlador MenusControlador;
        public Animator Animator;
        [HideInInspector]
        public PlayerPrefabReferenciasControlador ReferenciasPrefab;


        [Header("Variaveis")]
        public int Vida = 100;
        public int VidaMaxima = 100;
        public int VidaFoguete = 300;

        //Internas
        [HideInInspector]
        public int VidaAtual;
        [HideInInspector]
        public static PlayerController Self;
        [HideInInspector]
        public int QtdVidas = 0;
        [HideInInspector]
        public int Temp_VidaFoguete;
        [HideInInspector]
        public GameplayTipo TipoGameplay = GameplayTipo.Padrao;
        private Rigidbody2D Rb;
        private PassaralhoMovimentoControlador MovimentoControlador;
        private GameObject Nave;
        [HideInInspector]
        public PlayerVantagensController PlayerVantagens;

        private void Awake()
        {
            VidaAtual = Vida;
            Temp_VidaFoguete = VidaFoguete;
            this.Rb = GetComponent<Rigidbody2D>();
            this.Rb.gravityScale = 0;
            this.MovimentoControlador = GetComponent<PassaralhoMovimentoControlador>();
            this.ReferenciasPrefab = GetComponentInChildren<PlayerPrefabReferenciasControlador>();
            this.PlayerVantagens = GetComponentInChildren<PlayerVantagensController>();
            Self = this;
            this.TipoGameplay = GameplayTipo.Padrao;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Obstaculo":
                    this.RecebeDano(collision.gameObject.GetComponent<ObstaculoControlador>().ValorDano);
                    break;
                case "Disparo":
                    this.RecebeDano(25);
                    Destroy(collision.gameObject);
                    break;
                case "BonusItem":
                    var bonus = collision.gameObject.GetComponent<Bonus>();
                    PlayerVantagens.GerenciaVantagens(bonus);
                    Destroy(bonus.gameObject);

                    AudioControlador.Self.Play("Vantagem_Recolhida");
                    MenusControlador.Self.AtualizaDadosHudGameplay(this.VidaAtual);
                    break;
            }
        }

        public void RecebeDano(int valor)
        {
            if (!PlayerVantagens.AtivoEscudoProtecao)
            {
                GerenciaDano(valor);
            }
            else
                PlayerVantagens.DesativaVantagem(TipoVantagem.EscudoProtecao);

            this.MenusControlador.LblVida.text = VidaAtual.ToString();
        }

        private void GerenciaDano(int valor)
        {
            switch (TipoGameplay)
            {
                case GameplayTipo.Padrao:
                case GameplayTipo.Submarino:
                    VidaAtual -= valor;
                    if (VidaAtual <= 0)
                    {
                        this.Rb.gravityScale = 1;
                        this.MovimentoControlador.Habilitado = false;
                        GameControlador.Self.GameOver();
                    }
                    if (TipoGameplay == GameplayTipo.Padrao)
                        AplicaAnimacaoDano();
                    else
                        this.ReferenciasPrefab.Submarino.gameObject.GetComponent<Animator>().Play("ReceberDano", -1, 0f);
                    break;
                case GameplayTipo.Nave:
                    Temp_VidaFoguete -= valor;

                    if (Temp_VidaFoguete > 0)
                    {
                        this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("ReceberDano", -1, 0);
                        var valorVidaDisplay = (Temp_VidaFoguete * 100) / VidaFoguete;
                        this.ReferenciasPrefab.Foguete_VidaDisplay.transform.localScale = new Vector3(valorVidaDisplay / 10, 1f, 1f);
                    }
                    else
                    {
                        this.ReferenciasPrefab.Foguete_VidaDisplay.transform.localScale = new Vector3(0f, 1f, 1f);
                        this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("Destroi");
                        this.DesativaGameplayNave();
                        PlayerVantagens.DesativaVantagem(TipoVantagem.Foguete);
                    }
                    break;
            }
        }

        public void AtivaGameplayNave()
        {
            this.ReferenciasPrefab.Foguete.SetActive(true);
            this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("Entrada", -1, 0);
            this.TipoGameplay = GameplayTipo.Nave;
            this.Temp_VidaFoguete = this.VidaFoguete;
        }

        public void DesativaGameplayNave()
        {
            this.ReferenciasPrefab.Foguete.SetActive(false);
            this.TipoGameplay = GameplayTipo.Padrao;
            this.PlayerVantagens.DesativaVantagem(TipoVantagem.Foguete);
        }
        public void AplicaAnimacaoDano()
        {
            var variacao = UtilitarioRandom.GerarNumeroAleatorio(0, 1);

            if(variacao == 1) 
                this.Animator.Play("RecebendoDano");
            else
                this.Animator.Play("RecebendoDano_Reverse");
        }
    }
}
