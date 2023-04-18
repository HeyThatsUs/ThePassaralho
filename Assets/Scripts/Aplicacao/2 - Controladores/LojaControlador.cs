using Assets.Models;
using Assets.Scripts.Aplicacao._2___Controladores;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    private Item ItemAtual;
    private GameObject PreviewAtual;
    private int indexGerador = 1;

    //Botões e labels
    public Button BtnComprarEquipar;

    //ProprieadesInternas
    private bool PossuiItemAtual = false;


    private void Start()
    {
        foreach (var item in ItensLoja)
        {
            item.Index = this.indexGerador;
            this.indexGerador++;
        }
        this.ItemAtual = new Item(ItensLoja.FirstOrDefault());

        AddReferencias();
        IniciarLoja();
        AtualizaItemDisplay();
    }

    public void IniciarLoja()
    {
        var passaralhoInicial = this.ItensLoja.Where(p => p.Index == this.GameControlador.Save.PassaralhoAtualId).FirstOrDefault();
        this.ItemAtual = new Item(passaralhoInicial);
    }

    public void AddReferencias()
    {
        this.Menus_Controlador = this.GameControlador.Menus_Controlador;
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

        var save = this.GameControlador.Save;

        this.Menus_Controlador.LblValorItem.text = $"{ItemAtual.Nome}";

        PreviewAtual = Instantiate(ItemAtual.ObjectPreview, this.Display.transform);
        this.Display.transform.SetParent(PreviewAtual.transform);

        if (save.ItensAdquiridosLoja.Any(p => p == ItemAtual.Id))
        {
            this.PossuiItemAtual = true;

            if (ItemAtual.Id == this.GameControlador.Save.PassaralhoAtualId)
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
        var saveFile = this.GameControlador.Save;

        if (saveFile.QtdPassacoins >= this.ItemAtual.Valor)
        {
            saveFile.QtdPassacoins -= this.ItemAtual.Valor;
            saveFile.ItensAdquiridosLoja.Add(ItemAtual.Id);
            saveFile.PassaralhoAtualId = ItemAtual.Id;

            this.GameControlador.SaveController.Save(saveFile);

            this.Menus_Controlador.AtualizarSaldoPassaCoins(saveFile.QtdPassacoins);
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

        this.GameControlador.Save.PassaralhoAtualId = this.ItemAtual.Id;
        this.GameControlador.SaveController.Save(this.GameControlador.Save);

        if(Atualizar || PossuiItemAtual ==  false) this.AtualizaItemDisplay();
    }
}
