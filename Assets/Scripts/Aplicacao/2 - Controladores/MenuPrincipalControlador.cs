using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPrincipalControlador : MonoBehaviour
{
    public Animator AnimatorMenuConfiguracoes;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void AbreMenuConfig()
    {
        if (!AnimatorMenuConfiguracoes.GetBool("Aberto"))
        {
            AnimatorMenuConfiguracoes.Play("Abrir");
            AnimatorMenuConfiguracoes.SetBool("Aberto", true);
        }
        else
        {
            AnimatorMenuConfiguracoes.Play("Fechar");
            AnimatorMenuConfiguracoes.SetBool("Aberto", false);
        }
    }

    public void ProximoPassaralho() { }

    public void AnteriorPassaralho() { }
}
