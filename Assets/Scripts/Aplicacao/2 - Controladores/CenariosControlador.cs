using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CenariosControlador : MonoBehaviour
{

    public List<Cenario> Cenarios;
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

        var index = 0;
        foreach (var item in Cenarios)
        {
            item.Index = index + 1;
            index++;
        }

        CenariosFluxoHistoria_Inicio = Cenarios.Where(p => p.FluxoHistoria == true && p.Inicio == true).ToList();
        CenariosFluxoHistoria_Fim = Cenarios.Where(p => p.FluxoHistoria == true && p.Inicio == false).ToList();
        CenariosFluxoAleatorio = Cenarios.Where(p => p.FluxoHistoria == false).ToList();
    }

    public void AlteraCenarioAtual()
    {
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
                var index = Random.Range(0, CenariosFluxoAleatorio.Count() - 1);
                var cenario = cenariosValidos[index];

                //cenario.gameObject.SetActive(true);
                GameControlador.Self.Temp_ContadorTrocaDeCenario = GameControlador.Self.ContadorTrocaDeCenario;
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
    }

    private void AlteraCenarioFluxoHistoria()
    {
        var cenarioFinal = CenariosFluxoHistoria_Fim.Where(p => CenariosJaUtilizados.Any(x => x == p.Id)).FirstOrDefault();

        if (cenarioFinal == null)
        {
            //Reinicia Fluxo
            var cenarioInicial = CenariosFluxoHistoria_Inicio.FirstOrDefault();
            CenarioAtual.gameObject.SetActive(false);
            CenarioAtual = cenarioInicial;
            CenarioAtual.gameObject.SetActive(true);
            FluxoHistoria = false;
            GameControlador.Self.Temp_ContadorTrocaDeCenario = GameControlador.Self.ContadorTrocaDeCenario;
        }
        else
        {
            CenarioAtual.gameObject.SetActive(false);
            CenarioAtual = cenarioFinal;
            CenarioAtual.gameObject.SetActive(true);
            GameControlador.Self.Temp_ContadorTrocaDeCenario = GameControlador.Self.ContadorTrocaDeCenario;
            TrocaInicial = true;
        }

    }
}
