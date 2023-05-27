using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenario :  MonoBehaviour
{
    [HideInInspector]
    public int Id;
    public string Nome;
    public bool FluxoHistoria;
    public bool Inicio;
    public bool Ativo;
    public Animator Animator;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    public void FinalizadaAnimacaoFimCenario()
    {
        this.gameObject.SetActive(false);
    }
}

