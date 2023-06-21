using Assets.Models;
using Assets.Scripts.Aplicacao._2___Controladores;
using Assets.Scripts.Share._1___Dominio.Models;
using Assets.Scripts.Share._3___Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlador : MonoBehaviour
{

    private Animator GameAnimator;
    public SaveManager Saves;
    public GameObject MenuPrimeiroAcesso;
    public LojaControlador Loja_Controlador;

    [HideInInspector]
    public static GameControlador Self;

    [HideInInspector]
    public bool PodeIniciar = false;

    [Header("Referencias")]
    public PlayerController Player_Controlador;
    public CenariosControlador CenariosControlador;
    public GameObject LimitadorCentral;
    public List<Emissor> Emissores;

    [Header("Configurações Game")]
    public float Global_IntevaloEmissao;
    public float Global_IntevaloEmissaoBonus;
    [Range(3, 10)] public float Global_VelocidadeGame;
    public float VelocidadeGameEspaco;
    public int ContadorTrocaDeCenario;
    public int ContadorTrocaDeCenarioEspaco;
    public bool ConverterMetrosEmPassacoins;

    //Variaveis Ocultas
    [HideInInspector] public int LevelAtual = 0;
    [HideInInspector] public bool GameplayEspaco = false;
    [HideInInspector] public bool JogoIniciado = false;

    //Referencias Internas
    private Emissor EmissorAtual;
    private float Temporizador_Distancia_Percorrida = 5f;
    private float Temp_Temporizador_Distancia;
    private Animator MenusGeralAnimator;
    private bool PrimeiroAcesso = false;
    private int DistanciaPercorrida = 0;
    private int Temp_ContadorTrocaDeCenario = 0;
    [HideInInspector]
    public int Temp_ContadorTrocaDeCenarioEspaco = 5;
    private float Timer_inicioTransferencia = 1f;

    private void Awake()
    {
        Saves.Carregar();

        Temp_Temporizador_Distancia = Temporizador_Distancia_Percorrida;
        Temp_ContadorTrocaDeCenario = ContadorTrocaDeCenario;
        Temp_ContadorTrocaDeCenarioEspaco = ContadorTrocaDeCenarioEspaco;

        Emissores = Emissores.Where(p => p.Ativo).ToList();

        this.Loja_Controlador.GameControlador = this;
        GameControlador.Self = this;
        AtualizaDadosMenu();
    }

    private void Start()
    {
        GameAnimator = this.GetComponent<Animator>();

        if (PrimeiroAcesso)
        {
            AbreMenuPrimeiroAcesso();
            PrimeiroAcesso = false;
        }

        AddReferencias();

        AudioControlador.Self.Play("Menu");
    }


    private void FixedUpdate()
    {
        if (JogoIniciado)
        {
            Temp_Temporizador_Distancia -= Time.fixedDeltaTime;
            if (Temp_Temporizador_Distancia <= 0)
            {
                DistanciaPercorrida += 1;
                MenusControlador.Self.AtualizaDistanciaPercorrida(DistanciaPercorrida);
                Temp_Temporizador_Distancia = Temporizador_Distancia_Percorrida;
            }

            if (Temp_ContadorTrocaDeCenario <= 0 || Temp_ContadorTrocaDeCenarioEspaco <= 0)
            {
                this.EmissorAtual.GameObject.GetComponent<EmissorController>().DestroiObstaculosEmTela();
                var nomeCenarioAtual = this.CenariosControlador.AlteraCenarioAtual();
                Temp_ContadorTrocaDeCenario = ContadorTrocaDeCenario;
                Temp_ContadorTrocaDeCenarioEspaco = ContadorTrocaDeCenarioEspaco + 2;
                AplicaModificacoesCenario(nomeCenarioAtual);
            }
        }

        ConversorMetragem();
    }

    private void ConversorMetragem()
    {
        if (ConverterMetrosEmPassacoins)
        {
            if (Timer_inicioTransferencia > 0) Timer_inicioTransferencia -= Time.fixedDeltaTime;

            if (Timer_inicioTransferencia <= 0)
            {
                if (DistanciaPercorrida > 0)
                {
                    DistanciaPercorrida--;
                    MenusControlador.Self.LblDistandiaConversor.text = "" + DistanciaPercorrida;
                    var valor = Convert.ToInt32(MenusControlador.Self.LblPassacoinsConversor.text) + 1;
                    MenusControlador.Self.LblPassacoinsConversor.text = "" + valor;
                }
                else
                {
                    ConverterMetrosEmPassacoins = false;
                }
            }
        }
    }

    private void AplicaModificacoesCenario(string cenario)
    {
        //alterar Musica, Obstaculos e Bonus;
        LimitadorCentral.SetActive(false);
        this.GameplayEspaco = false;
        if (this.Global_IntevaloEmissao > 1)
        {
            this.Global_IntevaloEmissao -= 0.2f;
            MenusControlador.Self.Notificar("Quantidade de Obstáculos aumentada!", true);
        }

        EmissorAtual.GameObject.GetComponent<EmissorController>().EmissaoAtiva = false;

        //ResetPadroes
        PlayerController.Self.ReferenciasPrefab.Submarino.SetActive(false);

        switch (cenario)
        {
            case "Espaco":
                HabilitaGameplayEspaco();
                MenusControlador.Self.LblInimigosRestantes.text = "" + Temp_ContadorTrocaDeCenarioEspaco;
                EmissorAtual = Emissores.Where(p => p.Nome == "Espaco").FirstOrDefault();
                this.Player_Controlador.ReferenciasPrefab.Foguete.GetComponent<FogueteControlador>().CoolDown = 1.5f;
                break;
            case "Mar":
                PlayerController.Self.PlayerVantagens.DesativaVantagem(TipoVantagem.Foguete);
                PlayerController.Self.ReferenciasPrefab.Submarino.SetActive(true);
                PlayerController.Self.TipoGameplay = GameplayTipo.Submarino;
                break;
            default:
                PlayerController.Self.TipoGameplay = GameplayTipo.Padrao;
                this.Player_Controlador.ReferenciasPrefab.Foguete.GetComponent<FogueteControlador>().CoolDown = 5;
                DesabilitaGameplayEspaco();
                break;
        }

        EmissorAtual = Emissores.Where(p => p.Nome == cenario).FirstOrDefault();

        if (EmissorAtual == null)
            EmissorAtual = Emissores.Where(p => p.Nome == "Floresta").FirstOrDefault();

        if (EmissorAtual != null)
            EmissorAtual.GameObject.GetComponent<EmissorController>().EmissaoAtiva = true;
    }


    private void AddReferencias()
    {
        MenusGeralAnimator = MenusControlador.Self.MenusGeral_Animator;
    }

    private void AtualizaDadosMenu()
    {
        var saveGeral = this.Saves.Geral;
        MenusControlador.Self.AtualizarSaldoPassaCoins(saveGeral.Moedas);
    }

    public void IniciaGame()
    {
        if (this.PodeIniciar)
        {
            var passaralhoPrefab = this.Loja_Controlador.ItensLoja.Where(p => p.Nome == Saves.Geral.PassaralhoSelecionado).FirstOrDefault().ObjectPreview;
            var passaralho = Instantiate(passaralhoPrefab, this.Player_Controlador.Passaralho.transform);
            this.Player_Controlador.Passaralho.transform.SetParent(passaralho.transform);
            this.Player_Controlador.Animator = passaralho.GetComponent<Animator>();

            EmissorAtual = this.Emissores.Where(p => p.Nome == "Floresta").FirstOrDefault();
            EmissorAtual.GameObject.GetComponent<EmissorController>().EmissaoAtiva = true;

            GameAnimator.Play("IniciaGame");
            MenusGeralAnimator.Play("Menu_Main_Esconder");

            MenusControlador.Self.HudGameplay.SetActive(true);

            JogoIniciado = true;
        }
        else
            MenusControlador.Self.Notificar("Selecione um passaralho adquirido para iniciar!");
    }

    public void AbreMenuPrimeiroAcesso()
    {
        MenuPrimeiroAcesso.SetActive(true);
    }

    public void AbreMenuModoGame()
    {
        MenusControlador.Self.MenuModoGame.SetActive(true);
    }

    public void SalvarDadosPrimeiroAcesso()
    {
        this.MenuPrimeiroAcesso.SetActive(false);
    }

    public void Animacao_IniciaGame_Finalizada()
    {
        this.Loja_Controlador.gameObject.SetActive(false);
        this.Player_Controlador.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        MenusControlador.Self.HudGameplay.SetActive(false);
        MenusControlador.Self.MenuGameOver.SetActive(true);
        this.JogoIniciado = false;
        Saves.Geral.DistanciaPercorrida += DistanciaPercorrida;
        ConverterMetrosEmPassacoins = true;
        Timer_inicioTransferencia = 3f;
        Saves.Geral.Moedas = DistanciaPercorrida;
        MenusControlador.Self.LblDistandiaConversor.text = "" + DistanciaPercorrida;
        Saves.Salvar(Saves.Geral);
    }

    public void ReiniciaGame()
    {
        SceneManager.LoadScene("MainMenuEModoInfinito", LoadSceneMode.Single);
    }

    public void AumentaVelocidadeUniversal()
    {

        if (Global_VelocidadeGame < 10)
        {
            Global_VelocidadeGame += 0.5f;
            MenusControlador.Self.Notificar("Velocidade aumentada!", true);
        }
    }

    public void HabilitaGameplayEspaco()
    {
        this.GameplayEspaco = true;
        this.Player_Controlador.AtivaGameplayNave();
        LimitadorCentral.SetActive(true);
        PlayerController.Self.TipoGameplay = GameplayTipo.Nave;
    }

    public void DesabilitaGameplayEspaco()
    {
        this.GameplayEspaco = false;
        this.Player_Controlador.DesativaGameplayNave();
        LimitadorCentral.SetActive(false);
        PlayerController.Self.TipoGameplay = GameplayTipo.Padrao;
    }

    public void AdicionaLevelGame()
    {
        if (JogoIniciado)
        {
            LevelAtual += 1;
            MenusControlador.Self.LblLevelAtual.text = "" + LevelAtual;
            if (!GameplayEspaco)
                Temp_ContadorTrocaDeCenario--;
            else
                Temp_ContadorTrocaDeCenarioEspaco--;
        }
    }
}
