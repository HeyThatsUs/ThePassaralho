using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Share._1___Dominio.Models
{
    [Serializable]
    public class Emissor
    {
        [SerializeField] public string Nome;
        [SerializeField] public bool Ativo;
        [SerializeField] public GameObject GameObject;
    }
}
