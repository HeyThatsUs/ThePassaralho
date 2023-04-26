using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cenario :  MonoBehaviour
{
    public int Id;
    [HideInInspector]
    public int Index;
    public string Nome;
    public bool FluxoHistoria;
    public bool Inicio;
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

