using Assets.Models;
using Assets.Scripts.Aplicacao._2___Controladores;
using Assets.Scripts.Share._2___Controladores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControlador : MonoBehaviour
{
    private Animator GameAnimator;
    public GameObject MenuPrimeiroAcesso;
    public LojaControlador Loja_Controlador;

    [HideInInspector]
    public static GameControlador Self;

    [HideInInspector]
    public SaveFile Save;

    [HideInInspector]
    public SaveAndLoadController SaveController;

    [HideInInspector]
    public bool PodeIniciar = false;

    [Header("Referencias")]
    public MenusControlador Menus_Controlador;
    public PlayerController Player_Controlador;
    public EmissorController EmissorPai_Controlador;

    //Referencias Internas
    private Animator MenusGeralAnimator;

    private bool PrimeiroAcesso = false;

    private void Awake()
    {
        this.Loja_Controlador.GameControlador = this;
        GameControlador.Self = this;

        //LoadGame
        this.CarregaInformacoesSaveFile();
    }

    private void Start()
    {
        GameAnimator = this.GetComponent<Animator>();

        if (PrimeiroAcesso)
        {
            AbreMenuPrimeiroAcesso();
            PrimeiroAcesso = false;
        }

        AddReferencias();
    }

    private void AddReferencias()
    {
        MenusGeralAnimator= this.Menus_Controlador.MenusGeral_Animator;
    }

    private void CarregaInformacoesSaveFile()
    {
        SaveController = new SaveAndLoadController();
        Save = SaveController.Load();
        if (Save == null) 
        {
            //remover a quantidade 500
            Save = new SaveFile()
            {
                QtdPassacoins = 500
            }; 
            PrimeiroAcesso = true; 
        }

        if(this.Save.PassaralhoAtualId == 0) this.Save.PassaralhoAtualId= 1;

        AtualizaDadosMenu();

    }

    private void AtualizaDadosMenu()
    {
        this.Menus_Controlador.AtualizarSaldoPassaCoins(this.Save.QtdPassacoins);
    }

    public void IniciaGame()
    {
        if (this.PodeIniciar)
        {
            var passaralhoPrefab = this.Loja_Controlador.ItensLoja.Where(p => p.Id == this.Save.PassaralhoAtualId).FirstOrDefault().ObjectPreview;
            var passaralho = Instantiate(passaralhoPrefab, this.Player_Controlador.Passaralho.transform);
            this.Player_Controlador.Passaralho.transform.SetParent(passaralho.transform);
            this.Player_Controlador.Animator =  passaralho.GetComponent<Animator>();
            this.EmissorPai_Controlador.AtivaTodosEmissores();
            GameAnimator.Play("IniciaGame");
            MenusGeralAnimator.Play("Menu_Main_Esconder");
        }
        else
            this.Menus_Controlador.Notificar("Selecione um passaralho adquirido para iniciar!");
    }

    public void AbreMenuPrimeiroAcesso()
    {
        MenuPrimeiroAcesso.SetActive(true);
    }

    public void SalvarDadosPrimeiroAcesso()
    {
        this.Save.UserName = "James";
        SaveController.Save(this.Save);
        this.MenuPrimeiroAcesso.SetActive(false);
    }

    public void Animacao_IniciaGame_Finalizada()
    {
        this.Loja_Controlador.gameObject.SetActive(false);
        this.Player_Controlador.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        this.Menus_Controlador.MenuGameOver.SetActive(true);
    }

    public void ReiniciaGame()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
