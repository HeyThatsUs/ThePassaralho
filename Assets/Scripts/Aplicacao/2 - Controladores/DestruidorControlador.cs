using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruidorControlador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.CompareTag("Obstaculo"))
        {
            if (GameControlador.Self.JogoIniciado)
            {
                GameControlador.Self.LevelAtual += 1;
                MenusControlador.Self.LblLevelAtual.text = ""+GameControlador.Self.LevelAtual;
                GameControlador.Self.Temp_ContadorTrocaDeCenario--;
            }
        }

    }
}
