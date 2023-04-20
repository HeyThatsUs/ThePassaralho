using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public int Passacoins = 0;
    public int Vida = 0;
    //public TipoVantagem Vantagem = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}