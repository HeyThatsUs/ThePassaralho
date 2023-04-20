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

        [Header("Variaveis")]
        public int Vida = 100;

        //Internas
        private int VidaAtual;
        private Rigidbody2D Rb;
        private PassaralhoMovimentoControlador MovimentoControlador;

        private void Awake()
        {
            VidaAtual = Vida;
            this.Rb = GetComponent<Rigidbody2D>();
            this.Rb.gravityScale = 0;
            this.MovimentoControlador = GetComponent<PassaralhoMovimentoControlador>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Obstaculo":
                this.RecebeDano(collision.gameObject.GetComponent<ObstaculoControlador>().ValorDano);
                    break;
                case "BonusItem":
                    var bonus = collision.gameObject.GetComponent<BonusItem>();
                    this.VidaAtual += bonus.Vida;
                    GameControlador.Self.Save.QtdPassacoins += bonus.Passacoins;
                    MenusControlador.Self.AtualizaDadosHudGameplay(this.VidaAtual);
                    break;
            }
        }

        public void RecebeDano(int valor)
        {
            this.VidaAtual -= valor; 
            var variacao = UnityEngine.Random.Range(1, 10);

            if (variacao % 2 == 0)
            {
                this.Animator.Play("RecebendoDano", 0, 0f);
            }else
                this.Animator.Play("RecebendoDano_Reverse", 0, 0f);

            if (VidaAtual <= 0)
            {
                this.Rb.gravityScale = 1;
                this.MovimentoControlador.Habilitado = false;
                GameControlador.Self.GameOver();
            }

            this.MenusControlador.LblVida.text = VidaAtual.ToString();
        }
    }
}
