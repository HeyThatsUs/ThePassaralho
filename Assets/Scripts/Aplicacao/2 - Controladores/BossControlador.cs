using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControlador : MonoBehaviour
{
    public Transform Jogador;
    public GameObject Projetil;
    public float VelocidadeMovimento = 3f;
    public float VelocidadeProjetil = 5f;
    public float Rate = 2f;
    private float ProximoLancamentoProjetil;
    private bool EmMovimento = false;

    private void Start()
    {
        StartCoroutine(MovimentoAleatorio());
    }

    private void Update()
    {
        MovimentoAleatorio();

        if (Time.time >= ProximoLancamentoProjetil)
        {
            LancarProjetil();
            ProximoLancamentoProjetil = Time.time + 1f / Rate;
        }
    }

    private void Movimentar(float tempoMovimento)
    {
        if (!EmMovimento)
        {
            EmMovimento = true;
            StartCoroutine(Mover(tempoMovimento));
        }
    }

    private IEnumerator MovimentoAleatorio()
    {

        while (true)
        {
            float TempoMovimento = Random.Range(1f, 5f);
            float TempoEsperaMovimento = Random.Range(1f, 5f);

            yield return new WaitForSeconds(TempoEsperaMovimento);

            Movimentar(TempoMovimento);

            yield return new WaitForSeconds(TempoMovimento);

            Parar();
        }
    }


    private IEnumerator Mover(float tempoMovimento)
    {
        float tempoDecorrido = 0f;
        Vector3 posicaoInicial = transform.position;
        Vector3 posicaoAlvo = posicaoInicial + Vector3.up * VelocidadeMovimento * tempoMovimento;

        while (tempoDecorrido < tempoMovimento)
        {
            transform.position = Vector3.Lerp(posicaoInicial, posicaoAlvo, tempoDecorrido / tempoMovimento);
            tempoDecorrido += Time.deltaTime;
            yield return null;
        }

        transform.position = posicaoAlvo;
    }


    private void Parar()
    {
        EmMovimento = false;
    }


    private void LancarProjetil()
    {
        Vector2 direction = (Jogador.position - transform.position).normalized;
        GameObject projectile = Instantiate(Projetil, transform.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * VelocidadeProjetil;
    }
}
