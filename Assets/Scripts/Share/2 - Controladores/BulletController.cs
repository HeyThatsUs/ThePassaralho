using Assets.Scripts.Aplicacao._3___Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject obj;
    public DirecaoMovimentoBala Direcao;
    public float Velocidade = 5f;

 

    void Update()
    {
        switch (Direcao)
        {
            case DirecaoMovimentoBala.Esquerda:
                obj.transform.Translate(new Vector3((Time.deltaTime * Velocidade)* -1, 0f, 0f));
                break;
            case DirecaoMovimentoBala.Direita:
                obj.transform.Translate(new Vector3(Time.deltaTime * Velocidade, 0f, 0f));
                break;
        }
    }
}
