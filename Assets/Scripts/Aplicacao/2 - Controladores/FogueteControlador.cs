using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class FogueteControlador : MonoBehaviour
{
    public GameObject Missil;
    public Transform PontoDisparoTransform;
    public float VelocidadeMissil = 1.0f;

    public float CoolDown = 5f;
    private float Temp_CoolDown = 0f;


    private void FixedUpdate()
    {
        Temp_CoolDown -= Time.fixedDeltaTime;

        if(Temp_CoolDown <= 0)
        {
            IntanciaMissil();
            Temp_CoolDown = CoolDown;
        }
    }

    public void OnDisparo(InputValue value)
    {

    }

    private void IntanciaMissil()
    {
        var missil = Instantiate(Missil, PontoDisparoTransform);
        missil.SetActive(true);
        missil.transform.parent = null;
        missil.GetComponent<Rigidbody2D>().AddForce(new Vector2(VelocidadeMissil, 0f), ForceMode2D.Impulse);
        missil.transform.SetPositionAndRotation(PontoDisparoTransform.position, new Quaternion(0f,0f,0f,0f));
    }

    private void AnimacaoDestroiFinalizada()
    {
        GameControlador.Self.Player_Controlador.DesativaGameplayNave();
    }

}
