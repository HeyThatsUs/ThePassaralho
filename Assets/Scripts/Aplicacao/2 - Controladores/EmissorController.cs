using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmissorController : MonoBehaviour
{
    public float IntervaloEmissao = 3f;
    public float Temp_IntervaloEmissao = 0f;
    public List<GameObject> ObjetosEmissao;
    public float MultiplicadorVelocidade = 1;
    public bool Ativo = false;
    public bool ehPai = false;

    private bool Emitir = false;
    private List<EmissorController> EmissoresFilhos =  new List<EmissorController>();

    private void Start()
    {
        Temp_IntervaloEmissao = IntervaloEmissao;

        if (ehPai)
        {
            EmissoresFilhos = GetComponentsInChildren<EmissorController>().Where(p => p.ehPai == false).ToList();
        }
    }

    public void AtivaTodosEmissores()
    {
        foreach (var item in EmissoresFilhos)
        {
            item.Ativo= true;   
        }
    }

    private void FixedUpdate()
    {
        if (Ativo)
        {
            GerenciaIntervalo();
            if (Emitir) EmiteObjeto();
        }
    }

    private void GerenciaIntervalo()
    {
        if(Temp_IntervaloEmissao > 0)
        {
            Temp_IntervaloEmissao -= Time.fixedDeltaTime;
        }

        if (Temp_IntervaloEmissao < 0) Emitir = true;
    }

    private void EmiteObjeto()
    {
        var objIndex = Random.Range(0, ObjetosEmissao.Count() - 1);
        var objEmitido = Instantiate(ObjetosEmissao[objIndex], this.transform);
        objEmitido.transform.SetParent(null);

        Temp_IntervaloEmissao = IntervaloEmissao;
        Emitir = false;
    }
}
