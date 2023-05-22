using Assets.Scripts.Share._1___Dominio;
using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace Assets.Scripts.Aplicacao._2___Controladores
{
    public class ObstaculoControlador : MonoBehaviour
    {
        public int ValorDano = 50;
        public bool Atira = false;
        public GameObject Disparo;
        private float TimerDisparo;

        [Header("Particulas")]
        public bool EmiteParticulaOnDestroi = false;
        public int QuantidadeParticulas;
        public GameObject Particula;
        public string AudioQuebra;

        private void Start()
        {
            TimerDisparo = GetVariacaoTempoDisparo();
        } 

        private void FixedUpdate()
        {
            if(TimerDisparo > 0)
                TimerDisparo -= Time.fixedDeltaTime;

            

            if (Atira )
            {
                if (TimerDisparo <= 0)
                    EmitiDisparo();
            }
        }

        private void EmitiDisparo()
        {
            AudioControlador.Self.Play("Laser_Disparo");
            var objEmitido = Instantiate(Disparo, this.transform);
            objEmitido.transform.parent= null;
            objEmitido.SetActive(true);
            objEmitido.GetComponent<Rigidbody2D>().AddForce( new Vector2(-20, 0f), ForceMode2D.Impulse);
            TimerDisparo = GetVariacaoTempoDisparo();
        }

        private float GetVariacaoTempoDisparo()
        {
            return UnityEngine.Random.Range(1, 5);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Missil"))
            {
                if (EmiteParticulaOnDestroi)
                {
                    AplicaDestruicao();
                }

                Destroy(this.gameObject);
                GameControlador.Self.AdicionaLevelGame();
                if(GameControlador.Self.GameplayEspaco)
                    MenusControlador.Self.LblInimigosRestantes.text = "" + GameControlador.Self.Temp_ContadorTrocaDeCenarioEspaco;
            }
        }

        private void AplicaDestruicao()
        {
            for (int i = 0; i < QuantidadeParticulas; i++)
            {
                var item = Instantiate(Particula);
                item.gameObject.layer = LayerMask.NameToLayer("Particulas");
                item.transform.position = this.transform.position;
                var rb = item.GetComponent<Rigidbody2D>();
                rb.AddForce(new Vector2(UtilitarioRandom.GerarNumeroAleatorio(5, 10), UtilitarioRandom.GerarNumeroAleatorio(5, 10)), ForceMode2D.Impulse);
                rb.AddTorque(UtilitarioRandom.GerarNumeroAleatorio(50, 200));
                AudioControlador.Self.Play(AudioQuebra);
            }
        }
    }
}
