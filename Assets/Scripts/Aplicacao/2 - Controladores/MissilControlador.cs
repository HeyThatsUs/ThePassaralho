using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MissilControlador : MonoBehaviour
{

    private void OnDestroy()
    {
        AudioControlador.Self.Play("Missil_Explosao");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.GetComponent<Animator>().Play("Explode", -1, 0f);
    }

    public void ExplosaoFinalizada()
    {
        Destroy(this.gameObject);
    }
}
