using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EmissorController : MonoBehaviour
{
    public float IntervaloEmissao = 3f;
    private float Temp_IntervaloEmissao = 0f;
    public float IntervaloEmissaoBonus = 3f;
    private float Temp_IntervaloEmissaoBonus = 0f;

    [Header("Obstaculos Mapas")]
    public List<GameObject> ObjetosEmissao_Floresta;
    public List<GameObject> ObjetosEmissao_Mar;
    public List<GameObject> ObjetosEmissao_Espaco;
    public List<GameObject> ObjetosEmissao_Deserto;
    private List<GameObject> ObjetosEmissao_Atual;

    [Header("Bonus Mapas")]
    public List<GameObject> ObjetosEmissaoBonus_Base;
    private List<GameObject> ObjetosEmissaoBonus_Atual;

    [Header("Variáveis")]
    [HideInInspector]
    public float Velocidade = 1;
    public bool Ativo = false;
    public bool ehPai = false;


    //Internas
    private bool Emitir = false;
    private bool EmitirBonus = false;
    private Transform[] PontosEmissao;
    private List<EmissorController> EmissoresFilhos = new List<EmissorController>();

    private void Start()
    {
        Temp_IntervaloEmissao = IntervaloEmissao;
        Temp_IntervaloEmissaoBonus = IntervaloEmissaoBonus;
        Velocidade = GameControlador.Self.VelocidadeGameUniversal;
        PontosEmissao = gameObject.GetComponentsInChildren<Transform>();
        this.ObjetosEmissao_Atual = this.ObjetosEmissao_Floresta;
        this.ObjetosEmissaoBonus_Atual = this.ObjetosEmissaoBonus_Base;

        if (ehPai)
        {
            EmissoresFilhos = GetComponentsInChildren<EmissorController>().Where(p => p.ehPai == false).ToList();
        }
    }

    public void AlteracaoDeCenario(string cenario)
    {
        switch (cenario)
        {
            case "Floresta":
                ObjetosEmissao_Atual = ObjetosEmissao_Floresta;
                break;
            case "Mar":
                ObjetosEmissao_Atual = ObjetosEmissao_Mar;
                break;
            case "Deserto":
                ObjetosEmissao_Atual = ObjetosEmissao_Deserto;
                break;
            case "Espaco":
                ObjetosEmissao_Atual = ObjetosEmissao_Espaco;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (Ativo)
        {
            GerenciaIntervalo();
            if (Emitir) EmiteObjeto();
            if (EmitirBonus) EmiteObjetoBonus();
        }
    }

    private void GerenciaIntervalo()
    {
        var delta = Time.fixedDeltaTime;

        if (Temp_IntervaloEmissao > 0)
        {
            Temp_IntervaloEmissao -= delta;
        }

        if (Temp_IntervaloEmissao < 0) Emitir = true;

        if (Temp_IntervaloEmissaoBonus > 0)
        {
            Temp_IntervaloEmissaoBonus -= delta;
        }

        if (Temp_IntervaloEmissaoBonus < 0) EmitirBonus = true;
    }

    private void EmiteObjeto()
    {
        var objIndex = Random.Range(0, ObjetosEmissao_Atual.Count() -1);
        Debug.Log($"Index emissao " + objIndex);
        var objEmitido = Instantiate(ObjetosEmissao_Atual[objIndex], this.transform);
        objEmitido.transform.SetParent(null);
        AplicaBulletBehaivor(objEmitido);

        if (IntervaloEmissao > 0.8)
            IntervaloEmissao = IntervaloEmissao - (IntervaloEmissao / 100);

        Temp_IntervaloEmissao = IntervaloEmissao;
        Emitir = false;
    }

    private void EmiteObjetoBonus()
    {
        var objIndex = Random.Range(0, ObjetosEmissaoBonus_Atual.Count());

        var indexEmissao = Random.Range(0, PontosEmissao.Count());

        var objEmitido = Instantiate(ObjetosEmissaoBonus_Atual[objIndex], this.PontosEmissao[indexEmissao]);
        objEmitido.transform.SetParent(null);
        AplicaBulletBehaivor(objEmitido);

        if (IntervaloEmissaoBonus > 5)
            IntervaloEmissaoBonus = IntervaloEmissaoBonus - (IntervaloEmissaoBonus / 100);

        Temp_IntervaloEmissaoBonus = IntervaloEmissaoBonus;
        EmitirBonus = false;
    }

    private void AplicaBulletBehaivor(GameObject objEmitido)
    {
        var rb = objEmitido.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-150f * GameControlador.Self.VelocidadeGameUniversal, 0f));
    }
}
