using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class CenariosControlador : MonoBehaviour
{
    [Header("Referencias")]
    public List<Cenario> Cenarios;
    public Animator GlitchAnimator;

    private List<Cenario> CenariosFluxoAleatorio;
    private List<Cenario> CenariosFluxoHistoria_Inicio;
    private List<Cenario> CenariosFluxoHistoria_Fim;

    public Cenario CenarioAtual;
    private List<int> CenariosJaUtilizados;

    private bool FluxoHistoria = false;
    private bool TrocaInicial = true;

    private void Awake()
    {
        CenariosJaUtilizados = new List<int>();
        TrocaInicial = true;

        CenariosFluxoHistoria_Inicio = Cenarios.Where(p => p.FluxoHistoria == true && p.Inicio == true).ToList();
        CenariosFluxoHistoria_Fim = Cenarios.Where(p => p.FluxoHistoria == true && p.Inicio == false).ToList();
        CenariosFluxoAleatorio = Cenarios.Where(p => p.FluxoHistoria == false).ToList();
    }

    public string AlteraCenarioAtual()
    {
        GlitchAnimator.Play("TrocaCenario", -1, 0f);

        Task.Delay(55).GetAwaiter().GetResult();

        CenarioAtual.gameObject.SetActive(false);

        switch (CenarioAtual.GetComponent<Cenario>().Nome)
        {
            case "Espaco":
                MenusControlador.Self.Notificar("Aliens: Bater em Retirada!!", true);
                break;
        }


        if (FluxoHistoria)
        {
            AlteraCenarioFluxoHistoria();
        }
        else
        {
            TrocaInicial = false;
            var cenariosValidos = CenariosFluxoAleatorio
                .Where(p => !CenariosJaUtilizados.Any(x => x == p.Id))
                .ToList();

            if (cenariosValidos.Count() > 0)
            {
                var index = Random.Range(0, cenariosValidos.Count());
                Debug.Log($"Index cenario " + index);
                var cenario = cenariosValidos[index];

                cenario.gameObject.SetActive(true);
                CenariosJaUtilizados.Add(cenario.Id);
                CenarioAtual = cenario;
            }
            else
            {
                FluxoHistoria = true;
                AlteraCenarioFluxoHistoria();
            }
        }

        MenusControlador.Self.Notificar($"Novo Cenário: {CenarioAtual.Nome}", true);

        return CenarioAtual.Nome;
    }

    private void AlteraCenarioFluxoHistoria()
    {
        var cenarioFinal = CenariosFluxoHistoria_Fim.Where(p => !CenariosJaUtilizados.Any(x => x == p.Id)).FirstOrDefault();

        if (cenarioFinal == null)
        {
            ////Reinicia Fluxo
            var cenarioInicial = CenariosFluxoHistoria_Inicio.FirstOrDefault();
            CenarioAtual = cenarioInicial;
            CenarioAtual.gameObject.SetActive(true);
            FluxoHistoria = false;
            CenariosJaUtilizados = new List<int>();
            GameControlador.Self.AumentaVelocidadeUniversal();
        }
        else
        {
            CenarioAtual = cenarioFinal;
            CenarioAtual.gameObject.SetActive(true);
            TrocaInicial = true;
            CenariosJaUtilizados.Add(cenarioFinal.Id);
        }

    }


}
