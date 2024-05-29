using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleRoche : MonoBehaviour
{
    /*
    * Controle des roches pieges dans l'eau par Xavier Arbour:
    * 
    * Les roches montent et descendent dans l'eau pour creer un petit challenge pour le joueur lors du puzzle de saut. 
    * 
    */
    private float temps = 10f; // Timer pour remonter la roche

    /* Reference a l'animator */
    Animator animateur;

    void Start()
    {
        animateur = GetComponent<Animator>(); // L'animator est associe a la variable animateur
    }

    void Update()
    {
        // Ici on regarde si le temps ecoule est plus petit ou egal a 0, si c'est le cas, le booleen de l'animator "remonte" est 
        // mis a false et le timer est remis a 10...
        if (temps <= 0f)
        {
            temps = 10f;
            animateur.SetBool("remonte", false);
        }
        // Si le timer descend en bas de 5, on fait diminuer le compteur avec le temps reel et les roches ne remontent pas...
        else if (temps < 5f)
        {
            temps -= Time.deltaTime;
            animateur.SetBool("remonte", false);
        }
        // Si le timer est plus grand que 5, le timer descend avec le temps et le booleen de l'animator "remonte est true"
        else if (temps > 5f)
        {
            temps -= Time.deltaTime;
            animateur.SetBool("remonte", true);
            return;
        }
    }
}
