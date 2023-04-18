using Assets.Scripts.Aplicacao._3___Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public DirecaoMovimentoBala Direcao;
    public float Velocidade = 5f;

    private void FixedUpdate()
    {
        switch (Direcao)
        {
            case DirecaoMovimentoBala.Esquerda:
                this.transform.Translate(new Vector3((Time.fixedDeltaTime * Velocidade) * -1, 0f, 0f));
                break;
            case DirecaoMovimentoBala.Direita:
                this.transform.Translate(new Vector3(Time.fixedDeltaTime * Velocidade, 0f, 0f));
                break;
        }
    }
}
