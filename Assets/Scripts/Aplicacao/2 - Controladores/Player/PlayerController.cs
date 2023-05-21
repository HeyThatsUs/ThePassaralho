using Assets.Models;
using System;
using System.Collections.Generic;
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
        public int VidaFoguete = 300;

        //Internas
        private int VidaAtual;
        [HideInInspector]
        public int Temp_VidaFoguete;
        private bool GameplayNave = false;
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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Obstaculo":
                    if (!PlayerVantagens.AtivoEscudoProtecao)
                        this.RecebeDano(collision.gameObject.GetComponent<ObstaculoControlador>().ValorDano);
                    else
                        PlayerVantagens.DesativaVantagem(TipoVantagem.EscudoProtecao);
                    break;
                case "Disparo":
                    this.RecebeDano(25);
                    Destroy(collision.gameObject);
                    break;
                case "BonusItem":
                    AudioControlador.Self.Play("Vantagem_Recolhida");
                    var bonus = collision.gameObject.GetComponent<Bonus>();
                    this.VidaAtual += bonus.Vida;
                    GameControlador.Self.Saves.Geral.Moedas += bonus.Passacoins;
                    MenusControlador.Self.AtualizaDadosHudGameplay(this.VidaAtual);

                    if (bonus.EhVantagem)
                    {
                        GerenciaVantagens(bonus.TipoVantagem);
                    }
                    Destroy(bonus.gameObject);
                    break;
            }
        }

        public void GerenciaVantagens(TipoVantagem tipoVantagem)
        {
            switch (tipoVantagem)
            {
                case TipoVantagem.VidaExtra:
                    break;
                case TipoVantagem.EscudoProtecao:
                    this.ReferenciasPrefab.EscudoProtecao.SetActive(true);
                    PlayerVantagens.AtivaVantagem(TipoVantagem.EscudoProtecao);
                    break;
                case TipoVantagem.Foguete:
                    PlayerVantagens.AtivaVantagem(TipoVantagem.Foguete);
                    if (GameplayNave == false)
                        this.AtivaGameplayNave();
                    break;
            }
        }

        public void RecebeDano(int valor)
        {

            if (GameplayNave == false)
            {
                this.VidaAtual -= valor;

                var variacao = UnityEngine.Random.Range(1, 10);

                if (variacao % 2 == 0)
                {
                    this.Animator.Play("RecebendoDano", 0, 0f);
                }
                else
                    this.Animator.Play("RecebendoDano_Reverse", 0, 0f);

                if (VidaAtual <= 0)
                {
                    this.Rb.gravityScale = 1;
                    this.MovimentoControlador.Habilitado = false;
                    GameControlador.Self.GameOver();
                }

            }
            else
            {
                this.Temp_VidaFoguete -= valor;

                if (Temp_VidaFoguete > 0)
                {
                    this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("ReceberDano", -1, 0);
                    var teste = (Temp_VidaFoguete * 100) / VidaFoguete;
                    this.ReferenciasPrefab.Foguete_VidaDisplay.transform.localScale = new Vector3(teste / 10, 1f, 1f);
                }
                else
                {
                    this.ReferenciasPrefab.Foguete_VidaDisplay.transform.localScale = new Vector3(0f, 1f, 1f);
                    this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("Destroi");
                }
            }

            this.MenusControlador.LblVida.text = VidaAtual.ToString();
        }

        public void AtivaGameplayNave()
        {
            this.ReferenciasPrefab.Foguete.SetActive(true);
            this.ReferenciasPrefab.Foguete.GetComponent<Animator>().Play("Entrada", -1, 0);
            this.GameplayNave = true;
            this.Temp_VidaFoguete = this.VidaFoguete;
        }

        public void DesativaGameplayNave()
        {
            this.ReferenciasPrefab.Foguete.SetActive(false);
            this.GameplayNave = false;
            this.PlayerVantagens.DesativaVantagem(TipoVantagem.Foguete);
        }
    }
}
