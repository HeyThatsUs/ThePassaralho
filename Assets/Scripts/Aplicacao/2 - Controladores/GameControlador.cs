using Assets.Models;
using Assets.Scripts.Aplicacao._2___Controladores;
using Assets.Scripts.Share._2___Controladores;
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
    public MenusControlador Menus_Controlador;
    public PlayerController Player_Controlador;
    public EmissorController EmissorPai_Controlador;


    //Referencias Internas
    private Animator MenusGeralAnimator;

    private bool PrimeiroAcesso = false;
    private bool JogoIniciado = false;
    private int DistanciaPercorrida = 0;

    [Header("Variáveis")]
    public float Temporizador_Distancia = 5f;
    private float Temp_Temporizador_Distancia;
    [HideInInspector]
    public int LevelAtual = 0;

    private void Awake()
    {
        Temp_Temporizador_Distancia = Temporizador_Distancia;

        this.Loja_Controlador.GameControlador = this;
        GameControlador.Self = this;

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
                this.Menus_Controlador.AtualizaDistanciaPercorrida(DistanciaPercorrida);
                Temp_Temporizador_Distancia = Temporizador_Distancia;
            }
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
        MenusGeralAnimator= this.Menus_Controlador.MenusGeral_Animator;
    }

    private void CarregaInformacoesSaveFile()
    {
        var ArquivoSaveControlador = new ArquivoSaveControlador();        

        if(this.Save.PassaralhoAtualId == 0) this.Save.PassaralhoAtualId= 1;

        AtualizaDadosMenu();

    }

    private void AtualizaDadosMenu()
    {
        this.Menus_Controlador.AtualizarSaldoPassaCoins(this.Save.QtdPassacoins);
    }

    public void IniciaGame()
    {
        if (this.PodeIniciar)
        {
            var passaralhoPrefab = this.Loja_Controlador.ItensLoja.Where(p => p.Id == this.Save.PassaralhoAtualId).FirstOrDefault().ObjectPreview;
            var passaralho = Instantiate(passaralhoPrefab, this.Player_Controlador.Passaralho.transform);
            this.Player_Controlador.Passaralho.transform.SetParent(passaralho.transform);
            this.Player_Controlador.Animator =  passaralho.GetComponent<Animator>();

            this.EmissorPai_Controlador.AtivaTodosEmissores();

            GameAnimator.Play("IniciaGame");
            MenusGeralAnimator.Play("Menu_Main_Esconder");

            this.Menus_Controlador.HudGameplay.SetActive(true);

            JogoIniciado = true;
        }
        else
            this.Menus_Controlador.Notificar("Selecione um passaralho adquirido para iniciar!");
    }

    public void AbreMenuPrimeiroAcesso()
    {
        MenuPrimeiroAcesso.SetActive(true);
    }

    public void AbreMenuModoGame()
    {
        this.Menus_Controlador.MenuModoGame.SetActive(true);
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
        ArquivoSave.Salvar(); 
        this.Menus_Controlador.MenuGameOver.SetActive(true);
    }

    public void ReiniciaGame()
    {
        SceneManager.LoadScene("MainMenuEModoInfinito", LoadSceneMode.Single);
    }
}
