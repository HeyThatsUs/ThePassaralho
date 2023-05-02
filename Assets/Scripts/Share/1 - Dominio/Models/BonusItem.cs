using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{
    public int Passacoins = 0;
    public int Vida = 0;
    public bool EhVantagem = false;
    public TipoVantagem TipoVantagem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

}
public enum TipoVantagem
{
    EscudoProtecao = 0,
    VidaExtra = 1,
    Foguete = 2,
}