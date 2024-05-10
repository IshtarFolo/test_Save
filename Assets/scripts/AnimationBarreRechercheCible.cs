using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationBarreRechercheCible : MonoBehaviour
{
    public Transform joueur;
    public Transform cible;
    public Scrollbar scrollbar;
    public float ditanceProche = 10f;
    public float distanceLoin = 50f;

    private void Update()
    {
        //Calcul de la distance entre le joueur et la cible
        float distance = Vector3.Distance(joueur.position, cible.position);

        // Calculer la valeur de la scrollbar en fonction de la distance
        float scrollbarValue = 0f;
        if (distance <= ditanceProche)
        {
            scrollbarValue = 1f; // Rouge
        }
        else if (distance >= distanceLoin)
        {
            scrollbarValue = 0f; // Bleu
        }
        else
        {
            // Interpoler la valeur entre 1 et 0 en fonction de la distance
            scrollbarValue = Mathf.SmoothStep(0f, 1f, Mathf.InverseLerp(distanceLoin, ditanceProche, distance));
        }

        // Mettre à jour la valeur de la scrollbar
        scrollbar.value = scrollbarValue;
    }
}
