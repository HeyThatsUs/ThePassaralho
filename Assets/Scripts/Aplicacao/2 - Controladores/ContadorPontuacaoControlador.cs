using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContadorPontuacaoControlador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstaculo"))
        {
            GameControlador.Self.Save.Pontuacao++;
        }
    }
}
