using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruidorControlador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            GameControlador.Self.AdicionaLevelGame();
        }
    }
}
