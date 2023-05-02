using Assets.Models;
using Assets.Scripts.Aplicacao._2___Controladores;
using Assets.Scripts.Share._2___Controladores;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlador : MonoBehaviour
{
    private ArquivoSaveControlador _arquivoSaveControlador;
    private SaveFile _saveFile;

    private Animator GameAnimator;
    public GameObject MenuPrimeiroAcesso;
    public LojaControlador Loja_Controlador;

    [HideInInspector]
    public static GameControlador Self;

    [HideInInspector]
    public ArquivoSaveControlador ArquivoSave
        { 
        get 
        { 
            if (_arquivoSaveControlador == null)
            {
                _arquivoSaveControlador = new ArquivoSaveControlador();
            }
            return _arquivoSaveControlador;
        } 
    }

    [HideInInspector]
    public SaveFile Save
    {
        get
        {
            if (_saveFile == null)
            {
                _saveFile = ArquivoSave.Carregar();

            }
            return _saveFile;
        }
    }

    [HideInInspector]
    public bool PodeIniciar = false;

    [Header("Referencias")]
    public PlayerController Player_Controlador;
    public CenariosControlador CenariosControlador;
    public EmissorController Emissor;


    //Referencias Internas
    private Animator MenusGeralAnimator;
    private bool PrimeiroAcesso = false;
    public bool JogoIniciado = false;
    private int DistanciaPercorrida = 0;

    [Header("Variáveis")]
    public float Temporizador_Distancia_Percorrida = 5f;
    private float Temp_Temporizador_Distancia;
    [Range(1, 10)]
    public float VelocidadeGameUniversal = 3f;
    public float VelocidadeGameEspaco = 0.5f;
    [HideInInspector]
    public int LevelAtual = 0;
    public int ContadorTrocaDeCenario = 50;
    [HideInInspector]
    public int Temp_ContadorTrocaDeCenario = 0;


    private void Awake()
    {
        Temp_Temporizador_Distancia = Temporizador_Distancia_Percorrida;
        Temp_ContadorTrocaDeCenario = ContadorTrocaDeCenario;

        this.Loja_Controlador.GameControlador = this;
        GameControlador.Self = this;

        Save.SetDiretorio(Application.persistentDataPath);
        this._arquivoSaveControlador.Arquivo = Save;

        //LoadGame
        this.CarregaInformacoesSaveFile();
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

            if (Temp_ContadorTrocaDeCenario <= 0)
            {
                var nomeCenarioAtual = this.CenariosControlador.AlteraCenarioAtual();
                AplicaModificacoesCenario(nomeCenarioAtual);
            }
        }
    }

    private void AplicaModificacoesCenario(string cenario)
    {
        //alterar Musica, Obstaculos e Bonus;
        switch (cenario)
        {
            case "Espaco":
                HabilitaGameplayEspaco();
                break;
        }

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
    }

    private void AddReferencias()
    {
        MenusGeralAnimator= MenusControlador.Self.MenusGeral_Animator;
    }

    private void CarregaInformacoesSaveFile()
    {
        if(this.Save.PassaralhoAtualId == 0) this.Save.PassaralhoAtualId= 1;

        AtualizaDadosMenu();

    }

    private void AtualizaDadosMenu()
    {
        MenusControlador.Self.AtualizarSaldoPassaCoins(this.Save.QtdPassacoins);
    }

    public void IniciaGame()
    {
        if (this.PodeIniciar)
        {
            var passaralhoPrefab = this.Loja_Controlador.ItensLoja.Where(p => p.Id == this.Save.PassaralhoAtualId).FirstOrDefault().ObjectPreview;
            var passaralho = Instantiate(passaralhoPrefab, this.Player_Controlador.Passaralho.transform);
            this.Player_Controlador.Passaralho.transform.SetParent(passaralho.transform);
            this.Player_Controlador.Animator =  passaralho.GetComponent<Animator>();

            this.Emissor.Ativo = true;

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
        this.Save.UserName = "James";
        ArquivoSave.Salvar(); 
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
    }

    public void ReiniciaGame()
    {
        SceneManager.LoadScene("MainMenuEModoInfinito", LoadSceneMode.Single);
    }

    public void AumentaVelocidadeUniversal()
    {
        if(VelocidadeGameUniversal < 10)
        {
            VelocidadeGameUniversal += 0.5f;
        }
    }

    public void HabilitaGameplayEspaco()
    {
        this.Player_Controlador.AtivaGameplayNave();
        this.Emissor.AlteracaoDeCenario("Espaco");
    }
}
