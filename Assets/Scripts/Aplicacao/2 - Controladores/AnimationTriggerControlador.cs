using Assets.Scripts.Aplicacao._2___Controladores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerControlador : MonoBehaviour
{
    public string NomeAnimacao;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                if (PlayerController.Self.TipoGameplay == Assets.Scripts.Share._3___Enums.GameplayTipo.Padrao)
                {
                    var animator = collision.gameObject.GetComponentInChildren<Animator>();
                    animator.Play(NomeAnimacao, -1, 0f);
                }
                break;
        }
    }
}
