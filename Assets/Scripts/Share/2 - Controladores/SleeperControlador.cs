using System.Collections;
using UnityEngine;

public class SleeperControlador : MonoBehaviour
{
    public bool passouTempo;

    public IEnumerator Aguardar(int SegundosReais)
    {
        passouTempo = false;
        yield return new WaitForSeconds(SegundosReais);
        passouTempo = true;
    }
}
