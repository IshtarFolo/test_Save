using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class comportementVillageois2 : MonoBehaviour
{
    /*
     * Comportement du deuxieme villageois (bleu) par Xavier Arbour: 
     * 
     * Le villageois se deplace entre le marche du village et le bord du lac. Lorsqu'il atteint sa destination,
     * on l'arrete pour 20 secondes puis on le laisse prendre une nouvelle destination et recommencer sa "patrouille".
     * 
     */
    /*============
     * VARIABLES *
     ============*/
    int indexDestinations = 0; // L'index actuel de la destination a atteindre
    public Transform[] destinations; // Tableau des destinations a atteindre 

    /* References aux composants */
    NavMeshAgent agent;
    Animator animateur;

    void Start()
    {
        // Associationdes variables des GetCompoenent
        agent = GetComponent<NavMeshAgent>();
        animateur = GetComponent<Animator>();

        // Si l'agent n'est pas arrete, on lance la coroutine de deplacement
        if (!agent.isStopped)
        {
            // Declenchement de la coroutine
            StartCoroutine(ChangerDestination());
        }
    }

    void Update()
    {
        // On regarde si le NPC a atteint sa destination et s'il n'est pas arrete avant de relancer le changement de destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f && agent.enabled && !agent.isStopped)
        {
            StartCoroutine(ChangerDestination());
        }

        // Declenchement de la marche du personnage si l'agent est arrete
        switch (agent.isStopped)
        {
            case true:
                animateur.SetBool("marche", false);
                break;
            case false:
                animateur.SetBool("marche", true);
                break;
        }
    }


    /*
     * COROUTINES 
     ----------------------------------------------------------------------------------------------------------*/
    /*-----------------------------------------
     * COROUTINE DE CHANGEMENT DE DESTINATION *
     -----------------------------------------*/
    IEnumerator ChangerDestination()
    {
        // Retourne if si il n'y a pas de destinations
        if (destinations.Length == 0)
            yield break;
        // On envoie le NPC a la destination
        agent.destination = destinations[indexDestinations].position;

        // On choisi une nouvelle destination
        indexDestinations = (indexDestinations + 1) % destinations.Length;

        // On arrete le mouvement pendant 20 secondes
        agent.isStopped = true;

        // On attend un peu avant de relancer le mouvement du NPC
        yield return new WaitForSeconds(20);

        // On repart l'agent et, donc, on retablit le mouvement du personnage
        agent.isStopped = false;
    }
}
