using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlador : MonoBehaviour
{
    public Animator MenusAnimator;
    private Animator GameAnimator;


    public GameObject Loja;
    public GameObject Player;

    private void Start()
    {
        GameAnimator = this.GetComponent<Animator>();
    }

    public void IniciaGame()
    {
        GameAnimator.Play("IniciaGame");
        MenusAnimator.Play("Menu_Main_Esconder");
    }

    public void Animacao_IniciaGame_Finalizada()
    {
        this.Loja.SetActive(false);
        this.Player.SetActive(true);
    }
}
