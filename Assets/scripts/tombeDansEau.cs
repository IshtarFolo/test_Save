using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tombeDansEau : MonoBehaviour
{
    /*
     * Script detectant si le joueur tombe dans l'eau par Xavier Arbour:
     * 
     * On regarde si le joueur touche au trigger du game object ayant le tag "lac". Si c'est le cas on declenche l'animation de "noyade".
     * Le personnage ne peux plus bouger a ce moment la et on declenche la "mort", soit le rechargement de la derniere scene.
     * 
     */
    public GameObject manager; // Le game manager

    /* Reference aux composants */
    Animator animateur;
    Rigidbody rb;

    private void Start()
    {
        // On associe l'animator et le rigidbody aux variables precedemment declarees
        animateur = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Si le joueur touche au trigger du game object avec le tag "lac"...
    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "lac")
        {
            // l'animation est lancee
            animateur.SetTrigger("noyade");
            // le rigidbody ne peut plus bouger
            rb.isKinematic = true;
            // la mort est activee
            StartCoroutine(Mort());
        }
    }

    /*
     * COROUTINE *
     -----------------------------------------------------------------------------------------------------*/
    /*----------
     * LA MORT *
     ----------*/
    IEnumerator Mort()
    {
        // Apres 2 secondes on recharge la partie selon la derniere sauvegarde
        yield return new WaitForSeconds(2f);
        manager.GetComponent<savePosition>().Load();
    }
}
