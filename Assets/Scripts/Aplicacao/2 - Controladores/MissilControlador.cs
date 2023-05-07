using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MissilControlador : MonoBehaviour
{

    private void OnDestroy()
    {
        AudioControlador.Self.Play("Missil_Explosao");
 
        //instancia explosao
    }
}
