using Assets.Scripts.Share._3___Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Share._1___Dominio.Models
{
    [Serializable]
    public class Upgrade : MonoBehaviour
    {
        public TipoUpgrade TipoUpgrade;
        public TextMeshProUGUI Titutlo;
        public TextMeshProUGUI Level;
        public int QuantidadeLeveis;
        public int LevelAtual;
    }
}
