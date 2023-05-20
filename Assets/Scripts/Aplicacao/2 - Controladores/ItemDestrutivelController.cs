using Assets.Scripts.Share._1___Dominio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ItemDestrutivelController : MonoBehaviour
{

    public GameObject particula;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
            case "Missil":
                var particulas = new List<GameObject>
                {
                    Instantiate(particula),
                    Instantiate(particula),
                    Instantiate(particula),
                    Instantiate(particula),
                    Instantiate(particula)
                };

                foreach (var item in particulas)
                {
                    item.transform.localPosition = this.transform.localPosition;
                    var rb = item.GetComponent<Rigidbody2D>();
                    rb.AddForce(new Vector2(UtilitarioRandom.GerarNumeroAleatorio(5, 10), UtilitarioRandom.GerarNumeroAleatorio(5, 10)), ForceMode2D.Impulse);
                    rb.AddTorque(UtilitarioRandom.GerarNumeroAleatorio(50, 200));
                    AudioControlador.Self.Play("Madeira_Quebrando");
                }

                Destroy(this.gameObject);
                break;
        }
    }

    public void AnimcaoDestruicaoFinalizada()
    {
        Destroy(gameObject);
    }
}
