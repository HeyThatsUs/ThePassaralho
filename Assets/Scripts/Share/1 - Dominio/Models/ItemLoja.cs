﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    [Serializable]
    public class ItemLoja
    {
        [SerializeField] public int Id;
        [SerializeField] [HideInInspector] public int Index;
        [SerializeField] public string Nome; 
        [SerializeField] public string Descricao;
        [SerializeField] public int Valor;
        [SerializeField] public GameObject ObjectPreview;
    }

    [Serializable]
    public class Item : ItemLoja
    {
        [SerializeField] public bool Adquirido = false;

        public Item(ItemLoja obj)
        {
            Id = obj.Id;
            Index= obj.Index;
            Nome = obj.Nome;
            Descricao = obj.Descricao;
            Valor = obj.Valor;
            ObjectPreview = obj.ObjectPreview;
        }
    }
}
