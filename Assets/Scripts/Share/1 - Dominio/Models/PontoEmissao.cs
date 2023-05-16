using Assets.Scripts.Share._3___Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Share._1___Dominio.Models
{
    [Serializable]
    public class PontoEmissao
    {
        [SerializeField] public bool Ativo;
        [SerializeField] public TipoPontoEmissao TipoPontoEmissao;
        [SerializeField] public GameObject GameObject;
    }
}
