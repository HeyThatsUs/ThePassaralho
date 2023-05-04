using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilControlador : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioControlador.Self.Play("Missil_Explosao");

        //instancia explosao
    }
}
