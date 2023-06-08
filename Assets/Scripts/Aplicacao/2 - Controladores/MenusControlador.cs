using Assets.Scripts.Share._1___Dominio.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;

public class MenusControlador : MonoBehaviour
{

    [Header("Referencias Menus")]
    public GameObject Hud;
    public GameObject MenuGameOver;
    public GameObject MenuModoGame;
    public GameObject HudGameplay;
    public GameObject MenuPontuacoes;
    public GameObject MenuUpgrades;
    public GameObject MenuRanking;
    public GameObject MenuConfiguracoes;

    [Header("Menus Refatoracao")]
    public List<Menu> Menus;

    [Header("Referencia Animators")]
    public Animator MenuConfiguracoes_Animator;
    public Animator Notificador;
    public Animator NotificadorInGame;
    [HideInInspector]
    public Animator MenusGeral_Animator;

    [Header("Referencias Labels")]
    public TextMeshProUGUI LblEquiparComprar;
    public TextMeshProUGUI LblValorItem;
    public TextMeshProUGUI LblSaldoPassacoins;
    public TextMeshProUGUI LblNotificador;
    public TextMeshProUGUI LblNotificadorInGame;
    public TextMeshProUGUI LblDistancia;
    public TextMeshProUGUI LblVida;
    public TextMeshProUGUI LblLevelAtual;
    public TextMeshProUGUI LblDistandiaConversor;
    public TextMeshProUGUI LblPassacoinsConversor;
    public TextMeshProUGUI LblInimigosRestantes;

    [Header("Variaveis")]

    public float TimerNotificador = 5;
    public float Temp_TimerNotificador;

    //Internios
    [HideInInspector]
    public static MenusControlador Self;
    private List<Notificacao> Notificacoes =  new List<Notificacao>();

    private void Awake()
    {
        Self = this;
    }

    private void Start()
    {
        MenusGeral_Animator = GetComponent<Animator>();
        LblVida.text = "" + 100;
        Temp_TimerNotificador = TimerNotificador;
    }

    public void AbreMenuConfig()
    {
        if (!MenuConfiguracoes_Animator.GetBool("Aberto"))
        {
            MenuConfiguracoes_Animator.Play("Abrir");
            MenuConfiguracoes_Animator.SetBool("Aberto", true);
        }
        else
        {
            MenuConfiguracoes_Animator.Play("Fechar");
            MenuConfiguracoes_Animator.SetBool("Aberto", false);
        }
    }

    private void FixedUpdate()
    {
        if(Temp_TimerNotificador > 0)
            Temp_TimerNotificador = Temp_TimerNotificador - Time.fixedDeltaTime;

        ExibeNotificacoes();
    }

    public void AbreMenu(GameObject menu)
    {
        menu.SetActive(true);
        
        var menuAnimator = menu.GetComponentInChildren<Animator>();
        if (menuAnimator != null) menuAnimator.Play("Abrir", -1, 0f);
    }

    public void FechaMenu(GameObject menu)
    {
        menu.SetActive(false);
    }


    public void AtualizaDistanciaPercorrida(int distancia)
    {
        this.LblDistancia.text = distancia + "M";
    }

    public void AtualizarSaldoPassaCoins(int Saldo)
    {
        this.LblSaldoPassacoins.text = Saldo.ToString();
    }

    public void AtualizaDadosHudGameplay(int vida)
    {
        this.LblVida.text = "" + vida;
    }

    public void Notificar(string mensagem, bool inGame = false)
    {
        Notificacoes.Add(new Notificacao
        {
            Msg= mensagem,
            InGame=inGame,
        });
    }

    private void ExibeNotificacoes()
    {

        if(Temp_TimerNotificador <= 0 && Notificacoes.Count() > 0)
        {
            var notificacao = Notificacoes.FirstOrDefault();

            Notificacoes.Remove(notificacao);
            if (!notificacao.InGame)
            {
                this.LblNotificador.text = notificacao.Msg;
                this.Notificador.Play("Noticacao", -1, 0f);
            }
            else
            {
                this.LblNotificadorInGame.text = notificacao.Msg;
                this.NotificadorInGame.Play("Noticacao", -1, 0f);
            }

            Temp_TimerNotificador = TimerNotificador;
        }
    }
}

public class Notificacao{
    public string Msg { get; set; }
    public bool InGame { get; set; }
}
