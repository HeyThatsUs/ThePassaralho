using Assets.Scripts.Share._1___Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Models
{
    public class SaveFile_Upgrades : Arquivos
    {
        
         List<Upgrade> UpgradesAdquiridos;

        public SaveFile_Upgrades(bool criptografar) : base("Upgrades.game", criptografar)
        {
            
        }
    }
}
