using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class declenchePiege : MonoBehaviour
{
    /*
     * Controle du "piege" avec les roches dans la chute par Xavier Arbour:
     * 
     * Deux roches sont supperposees, une a l'horizontale l'autre a la verticale. Lors que le joueur saute sur le roche du haut, 
     * on declenche l'animation de la roche qui devient chambranlante. Apres 2 secondes la roche qui tient precairement sur le
     * dessus de l'autre roche tombe. Apres 15 secondes la roche reprend sa position originale.
     * 
     */
    public GameObject piege; // Le piege complet

    // Si le joueur touche la roche du dessus possedant le tag "piege"...
    private void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.tag == "piege")
        {
            // L'animation et la coroutine sont lances 
            piege.GetComponent<Animator>().SetBool("touche", true);
            StartCoroutine(Tombe());
            // Apres 15 secondes on fait revenir le piege a sa position originale
            Invoke("Retour", 15f);
        }
    }

    /*
     * FONCTION SUPPLEMENTAIRE
     ----------------------------------------------------------------------------------------------*/
    /*---------------------
     * FONCTION DE RETOUR *
     ---------------------*/
    void Retour()
    {
        piege.GetComponent<Animator>().SetBool("retourne", true);
        piege.GetComponent<Animator>().SetBool("tombe", false);
        piege.GetComponent<Animator>().SetBool("touche", false);
        return;
    }
    /*
     * COROUTINE
     --------------------------------------------------------------------------------------------*/
    /*---------------------------------------
     * COROUTINE QUI LAISSE TOMBER LA ROCHE *
     ---------------------------------------*/
    IEnumerator Tombe()
    {
        yield return new WaitForSeconds(2f);
        piege.GetComponent<Animator>().SetBool("tombe", true);
        piege.GetComponent<Animator>().SetBool("retourne", false);
    }

}
