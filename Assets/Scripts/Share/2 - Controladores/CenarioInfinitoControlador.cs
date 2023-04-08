using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioInfinitoControlador : MonoBehaviour
{
    public float Velocidade;

    void Update()
    {
        Movimentar();
    }

    private void Movimentar()
    {
        Vector2 deslocamento = new Vector2(Time.time * Velocidade, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}
