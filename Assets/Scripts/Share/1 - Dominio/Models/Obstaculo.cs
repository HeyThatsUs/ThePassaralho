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
    public class Obstaculo
    {
        [SerializeField] public bool Ativo;
        [SerializeField] public bool UtilizaPontosEmissao;
        [SerializeField] public GameObject GameObject;
        [SerializeField] public bool AplicaTorque = false;
        [SerializeField] public TipoPontoEmissao TipoPontoEmissao;
    }
}
