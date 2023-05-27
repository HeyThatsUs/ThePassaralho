using Assets.Scripts.Share._1___Dominio;
using Assets.Scripts.Share._1___Dominio.Models;
using Assets.Scripts.Share._1___Dominio.ViewModels;
using Assets.Scripts.Share._3___Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmissorController : MonoBehaviour
{

    [Header("Variáveis")]
    public List<Obstaculo> Obstaculos;
    public List<ItemBonusListaViewModel> ListaItensBonus;
    public bool EmissaoAtiva = false;

    //Internas
    [HideInInspector] public List<Bonus> Bonus;
    [HideInInspector] public float VelocidadeObstaculos = 1;
    private List<PontoEmissao> PontosEmissao;
    private bool Emitir = false;
    private bool EmitirBonus = false;
    private List<GameObject> ObstaculosEmitidos = new List<GameObject>();
    private float Temp_IntervaloEmissao;
    private float Temp_IntervaloEmissaoBonus;

    private void Start()
    {
        Temp_IntervaloEmissao = GameControlador.Self.Global_IntevaloEmissao;
        Temp_IntervaloEmissaoBonus = GameControlador.Self.Global_IntevaloEmissaoBonus;

        VelocidadeObstaculos = GameControlador.Self.Global_VelocidadeGame;

        Obstaculos = Obstaculos.Where(p => p.Ativo).ToList();
        Bonus = ListaItensBonus.Where(p => p.Ativo).Select(p => p.Bonus).ToList();

        PontosEmissao = PontosEmissaoController.Self.PontosEmissao;
    }

    private void FixedUpdate()
    {
        if (EmissaoAtiva)
        {
            GerenciaIntervalo();
            if (Emitir) EmiteObjeto();
            if (EmitirBonus) EmiteObjetoBonus();
        }
    }

    private void GerenciaIntervalo()
    {
        var delta = Time.fixedDeltaTime;

        if (Temp_IntervaloEmissao > 0)
        {
            Temp_IntervaloEmissao -= delta;
        }

        if (Temp_IntervaloEmissao < 0) Emitir = true;

        if (Temp_IntervaloEmissaoBonus > 0)
        {
            Temp_IntervaloEmissaoBonus -= delta;
        }

        if (Temp_IntervaloEmissaoBonus < 0) EmitirBonus = true;
    }

    private void EmiteObjeto()
    {
        if(Obstaculos != null && Obstaculos.Count() > 0)
        {
            var itensDisponiveis = Obstaculos.Where(p => p.Ativo == true).ToList();
            var indexSorteio = UtilitarioRandom.GerarNumeroAleatorio(1, Obstaculos.Count());
            var obstaculo = Obstaculos[indexSorteio -1];
            var transformEmissao = this.transform;

            if (obstaculo.UtilizaPontosEmissao)
            {
                var pontosDisponiveis = PontosEmissao.Where(p => p.TipoPontoEmissao == obstaculo.TipoPontoEmissao).ToList();
                var indexSorteioEmissao = UtilitarioRandom.GerarNumeroAleatorio(1 , pontosDisponiveis.Count());
                var pontoEmissao = pontosDisponiveis[indexSorteioEmissao -1];
                transformEmissao = pontoEmissao.GameObject.transform;
            }

            var objEmitido = Instantiate(obstaculo.GameObject, transformEmissao);
            objEmitido.transform.SetParent(null);
            ObstaculosEmitidos.Add(objEmitido);
            AplicaBulletBehaivor(objEmitido, obstaculo.AplicaTorque);
        }

        Temp_IntervaloEmissao = GameControlador.Self.Global_IntevaloEmissao;
        Emitir = false;
    }

    private void EmiteObjetoBonus()
    {
        if (ListaItensBonus != null && ListaItensBonus.Count() > 0)
        {
            var indexSorteio = UnityEngine.Random.Range(1, ListaItensBonus.Count()) - 1;
            var bonus = Bonus[indexSorteio];
            var transformEmissao = this.transform;

            if (bonus.UtilizaPontosEmissao)
            {
                var pontosDisponiveis = PontosEmissao.Where(p => p.TipoPontoEmissao == TipoPontoEmissao.Frontal).ToList();
                var indexSorteioEmissao = UtilitarioRandom.GerarNumeroAleatorio(1, pontosDisponiveis.Count());
                var pontoEmissao = pontosDisponiveis[indexSorteioEmissao - 1];
                transformEmissao = pontoEmissao.GameObject.transform;
            }

            var objEmitido = Instantiate(bonus, transformEmissao); 
            objEmitido.transform.SetParent(null);
            ObstaculosEmitidos.Add(objEmitido.gameObject);
            AplicaBulletBehaivor(objEmitido.gameObject);
        }

        Temp_IntervaloEmissaoBonus = GameControlador.Self.Global_IntevaloEmissaoBonus;
        EmitirBonus = false; 
    }

    private void AplicaBulletBehaivor(GameObject objEmitido, bool torque = false)
    {
        var rb = objEmitido.GetComponent<Rigidbody2D>();
        var velocidade = -150f * GameControlador.Self.Global_VelocidadeGame;
        rb.AddForce(new Vector2(-150f * GameControlador.Self.Global_VelocidadeGame, 0f));

        if(torque) rb.AddTorque(UtilitarioRandom.GerarNumeroAleatorio(50, 200)); ;
    }

    public void DestroiObstaculosEmTela()
    {
        foreach (var item in ObstaculosEmitidos)
        {
            Destroy(item);
        }
    }
}
