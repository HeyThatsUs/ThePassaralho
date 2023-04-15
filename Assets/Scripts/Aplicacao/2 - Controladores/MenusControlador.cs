using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using static UnityEditor.Experimental.GraphView.GraphView;

public class MenusControlador : MonoBehaviour
{

    [Header("Referencias Menus")]
    public GameObject Hud;

    [Header("Referencia Animators")]
    public Animator MenuConfiguracoes_Animator;
    public Animator Notificador;
    [HideInInspector] 
    public Animator MenusGeral_Animator;

    [Header("Referencias Labels")]
    public TextMeshProUGUI LblEquiparComprar;
    public TextMeshProUGUI LblValorItem;
    public TextMeshProUGUI LblSaldoPassacoins;
    public TextMeshProUGUI LblNotificador;

    private void Start()
    {
        MenusGeral_Animator = GetComponent<Animator>();
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

    public void AtualizarSaldoPassaCoins(int Saldo)
    {
        this.LblSaldoPassacoins.text = Saldo.ToString();
    }

    public void Notificar(string mensagem)
    {
        this.Notificador.Play("Noticacao", -1, 0f);

        this.LblNotificador.text = mensagem;
    }
}
