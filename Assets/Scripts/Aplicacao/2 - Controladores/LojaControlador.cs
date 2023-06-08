using Assets.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LojaControlador : MonoBehaviour
{
    [SerializeField]
    public List<ItemLoja> ItensLoja;
    public GameObject Display;

    [HideInInspector]
    public GameControlador GameControlador;

    //Referencias Internas
    private MenusControlador Menus_Controlador;
    private PlataformaControlador Plataforma_Controlador;

    private Item ItemAtual;
    private GameObject PreviewAtual;
    private int indexGerador = 1;

    //Botões e labels
    public Button BtnComprarEquipar;

    //ProprieadesInternas
    private bool PossuiItemAtual = false;


    private void Start()
    {
        Plataforma_Controlador = new PlataformaControlador();
        foreach (var item in ItensLoja)
        {
            if (item.Plataformas.Contains(Plataforma_Controlador.ObterPlataforma()))
            {
                item.Index = this.indexGerador;
                this.indexGerador++;
            }
        }
        this.ItemAtual = new Item(ItensLoja.FirstOrDefault());

        AddReferencias();
        IniciarLoja();
        AtualizaItemDisplay();
    }

    public void IniciarLoja()
    {
       var passaralhoInicial = this.ItensLoja.Where(p => p.Nome == this.GameControlador.Saves.Geral.PassaralhoSelecionado).FirstOrDefault();
       this.ItemAtual = new Item(passaralhoInicial);
    }

    public void AddReferencias()
    {
        this.Menus_Controlador = MenusControlador.Self;
    }

    public void ProximoItem()
    {
        var proximoItem = ItensLoja
            .Where(p => p.Index == this.ItemAtual.Index + 1)
            .FirstOrDefault();

        if (proximoItem == null)
            this.ItemAtual = new Item(ItensLoja.First());
        else
            this.ItemAtual = new Item(proximoItem);


        AtualizaItemDisplay();
    }

    public void ItemAnterior()
    {
        var proximoItem = ItensLoja
            .Where(p => p.Index == this.ItemAtual.Index - 1)
            .FirstOrDefault();

        if (proximoItem == null)
            this.ItemAtual = new Item(ItensLoja.Last());
        else
            this.ItemAtual = new Item(proximoItem);

        AtualizaItemDisplay();
    }

    public void AtualizaItemDisplay()
    {
        if (PreviewAtual != null)
        {
            Destroy(PreviewAtual);
        }

        this.Menus_Controlador.LblValorItem.text = $"{ItemAtual.Nome}";

        PreviewAtual = Instantiate(ItemAtual.ObjectPreview, this.Display.transform);
        this.Display.transform.SetParent(PreviewAtual.transform);

        if (this.GameControlador.Saves.Geral.ItensAdquiridosLoja.Any(p => p == ItemAtual.Id))
        {
           this.PossuiItemAtual = true;

            if (ItemAtual.Nome == this.GameControlador.Saves.Geral.PassaralhoSelecionado)
                this.Menus_Controlador.LblEquiparComprar.text = "Equipado";
            else
                this.Menus_Controlador.LblEquiparComprar.text = "Equipar";

            EquiparItem(false);

            this.BtnComprarEquipar.gameObject.SetActive(false);
            this.GameControlador.PodeIniciar = true;
        }
        else
        {
            this.PossuiItemAtual = false;
            this.Menus_Controlador.LblEquiparComprar.text = "Comprar";
            this.Menus_Controlador.LblValorItem.text = $"{ItemAtual.Nome}: ${ItemAtual.Valor}";
            this.BtnComprarEquipar.gameObject.SetActive(true);
            this.GameControlador.PodeIniciar = false;
}
    }

    public void ComprarItem()
    {
        if (GameControlador.Saves.Geral.Moedas >= this.ItemAtual.Valor)
        {
            GameControlador.Saves.Geral.Moedas -= this.ItemAtual.Valor;
            GameControlador.Saves.Geral.ItensAdquiridosLoja.Add(ItemAtual.Id);
            GameControlador.Saves.Geral.PassaralhoSelecionado = ItemAtual.Nome;

            this.GameControlador.Saves.Salvar();

            this.Menus_Controlador.AtualizarSaldoPassaCoins(GameControlador.Saves.Geral.Moedas);
        }
        else
           this.Menus_Controlador.Notificar("Saldo insuficiente!");
    }

    public void EquiparItem(bool Atualizar = true)
    {
        if (PossuiItemAtual == false)
        {
            ComprarItem();
        }

        GameControlador.Saves.Geral.PassaralhoSelecionado = this.ItemAtual.Nome;
        GameControlador.Saves.Salvar();

        if (Atualizar || PossuiItemAtual ==  false) this.AtualizaItemDisplay();
    }
}
