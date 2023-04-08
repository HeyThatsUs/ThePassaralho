using Assets.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LojaControlador : MonoBehaviour
{
    [SerializeField]
    public List<ItemLoja> ItensLoja;
    public GameObject Display;

    private ItemAtual ItemAtual;
    private GameObject PreviewAtual;
    private int indexGerador = 1;

    private void Start()
    {
        foreach (var item in ItensLoja)
        {
            item.Index = this.indexGerador;
            this.indexGerador++;
        }

        this.ItemAtual = new ItemAtual(ItensLoja.FirstOrDefault());

        AtualizaItemDisplay();
    }

    public void ProximoItem()
    {
        var proximoItem = ItensLoja
            .Where(p => p.Index == this.ItemAtual.Index + 1)
            .FirstOrDefault();

        if (proximoItem == null)
            this.ItemAtual = new ItemAtual(ItensLoja.First());
        else
            this.ItemAtual = new ItemAtual(proximoItem);

        AtualizaItemDisplay();
    }

    public void ItemAnterior()
    {
        var proximoItem = ItensLoja
            .Where(p => p.Index == this.ItemAtual.Index - 1)
            .FirstOrDefault();

        if (proximoItem == null)
            this.ItemAtual = new ItemAtual(ItensLoja.Last());
        else
            this.ItemAtual = new ItemAtual(proximoItem);

        AtualizaItemDisplay();
    }

    public void AtualizaItemDisplay()
    {
        if (PreviewAtual != null)
        {
            Destroy(PreviewAtual);
        }

        PreviewAtual = Instantiate(ItemAtual.ObjectPreview, this.transform);
    }
}
