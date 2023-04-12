using Assets.Models;
using Assets.Scripts.Share._2___Controladores;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlador : MonoBehaviour
{
    public Animator MenusAnimator;
    private Animator GameAnimator;


    public GameObject Loja;
    public GameObject Player;
    private SaveAndLoadController SaveController;
    private SaveFile Save;

    private void Start()
    {
        GameAnimator = this.GetComponent<Animator>();

        //LoadGame
        this.CarregaInformacoesSaveFile();
    }

    private void CarregaInformacoesSaveFile()
    {
        SaveController = new SaveAndLoadController();
        Save = SaveController.Load();
        if (Save == null) Save = new SaveFile();


    }

    public void IniciaGame()
    {
        GameAnimator.Play("IniciaGame");
        MenusAnimator.Play("Menu_Main_Esconder");
    }

    public void Animacao_IniciaGame_Finalizada()
    {
        this.Loja.SetActive(false);
        this.Player.SetActive(true);
    }
}
