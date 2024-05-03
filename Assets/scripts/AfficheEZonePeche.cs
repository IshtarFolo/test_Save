using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetVent : MonoBehaviour
{
    public GameObject lettreE; // L'interaction avec la letttre E
    //Affichage du E

    private void OnTriggerEnter(Collider infoCollision)
    {

        //Si le joueur veut jouer au mini jeu de pêche afficher la lettre E
        if (infoCollision.gameObject.tag == "zonePeche")
        {
            lettreE.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {


        if (infoCollision.gameObject.tag == "zonePeche")
        {
            lettreE.SetActive(false);
        }
    }

}