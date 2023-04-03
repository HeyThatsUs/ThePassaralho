using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    public Animator AnimatorConfiguracoes;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AbreMenuConfig()
    {
        if (!AnimatorConfiguracoes.GetBool("Aberto"))
        {
            AnimatorConfiguracoes.Play("Abrir");
            AnimatorConfiguracoes.SetBool("Aberto", true);
        }
        else
        {
            AnimatorConfiguracoes.Play("Fechar");
            AnimatorConfiguracoes.SetBool("Aberto", false);
        }

        
    }
}
