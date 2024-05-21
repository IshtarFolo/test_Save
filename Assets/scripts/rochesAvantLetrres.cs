using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rochesAvantLetrres : MonoBehaviour
{
    /*============
     * VARIABLES *
     ============*/
   public GameObject kirie; // Reference a Kirie

    public Camera[] cam; // Tableau des cameras
    bool aChange = false; // Bool qui determine si les cameras changent

    // References aux composantes 
    Animator animateur;

    void Start()
    {
        // Association des composants aux variables appropriees
        animateur = GetComponent<Animator>();

        // Activation/ desactivation des cameras au depart
        cam[0].enabled = true;
        cam[1].enabled = false;
    }


    void Update()
    {
        // Si la cam n'a pas changee et que kirie a ramassee les lettres...
        if (!aChange && kirie.GetComponent<_collision_kirie>().lettreRamassee == 5)
        {
            // On anime La montee des roches de la cascade
            animateur.SetBool("monte", true);
            // La cam a changee
            aChange = true;
            // Kirie ne peut pas bouger pendant la cutscene
            kirie.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            // On lance la coroutine de changement de camera
            StartCoroutine(changementCam());
        }
    }


    /*=============
     * COROUTINES *
     =============*/

    /* La camera montrant les roches monter est activee et la camera de jeu est desactivee, 
     * puis on fait l'inverse apres 2 secondes et on redonne la possibilite de bouger au joueur */
    IEnumerator changementCam()
    {
        cam[1].enabled = true;
        cam[0].enabled = false;
        yield return new WaitForSeconds(2f);


        cam[1].enabled = false;
        cam[0].enabled = true;
        kirie.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
