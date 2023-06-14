using Assets.Models;
using Assets.Scripts.Share._1___Dominio.Models;
using Assets.Scripts.Share._3___Enums;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UpgradesControlador : MonoBehaviour
{
    public int ValorUpgrades;

    private List<Upgrade> Upgrades;
    private void Awake()
    {
        Upgrades = GetComponentsInChildren<Upgrade>().ToList();
        CarregaUpgradesSaveFile();
    }

    public void AdquirirUpgrade(GameObject upgrade)
    {
        var upgradeAtual = Upgrades.Where(p => p == upgrade.GetComponent<Upgrade>()).FirstOrDefault();
        if (upgradeAtual.LevelAtual < upgradeAtual.QuantidadeLeveis)
        {
            upgradeAtual.LevelAtual++;
            SetLevelUpgrade(upgradeAtual);
        }
    }

    private void CarregaUpgradesSaveFile(SaveFile saveUpgrades = null)
    {
        foreach (var upgrade in Upgrades)
        {
            Upgrade upgradeSalvo = null;

            if(upgradeSalvo == null)
                SetLevelUpgrade(upgrade);
            else
                SetLevelUpgrade(upgradeSalvo);
        }
    }

    private void SetLevelUpgrade(Upgrade upgrade)
    {
        upgrade.Level.text = $"{upgrade.LevelAtual}/{upgrade.QuantidadeLeveis}";
    }
}
