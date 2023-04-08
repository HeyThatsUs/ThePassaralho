using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlador : MonoBehaviour
{
    public Animator MenusAnimator;
    private Animator GameAnimator;

    private void Start()
    {
        GameAnimator = this.GetComponent<Animator>();
    }

    public void IniciaGame()
    {
        GameAnimator.Play("IniciaGame");
        MenusAnimator.Play("Menu_Main_Esconder");
    }
}
