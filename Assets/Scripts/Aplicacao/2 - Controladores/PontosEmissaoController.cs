using Assets.Scripts.Share._1___Dominio.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PontosEmissaoController : MonoBehaviour
{
    public List<PontoEmissao> PontosEmissao;
    public static PontosEmissaoController Self;

    private void Awake()
    {
        Self = this;
        PontosEmissao = PontosEmissao.Where(p => p.Ativo == true).ToList();
    }
}
