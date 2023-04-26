using TMPro;
using UnityEngine;

public class MenusControlador : MonoBehaviour
{

    [Header("Referencias Menus")]
    public GameObject Hud;
    public GameObject MenuGameOver;
    public GameObject MenuModoGame;
    public GameObject HudGameplay;

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

    //Internios
    [HideInInspector]
    public static MenusControlador Self;

    private void Awake()
    {
        Self = this;
    }

    private void Start()
    {
        MenusGeral_Animator = GetComponent<Animator>();
        LblVida.text = "" + 100;
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

    private void Update()
    {

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

        if (!inGame)
        {
            this.LblNotificador.text = mensagem;
            this.Notificador.Play("Noticacao", -1, 0f);
        }
        else
        {
            this.LblNotificadorInGame.text = mensagem;
            this.NotificadorInGame.Play("Noticacao", -1, 0f);
        }
    }
}
