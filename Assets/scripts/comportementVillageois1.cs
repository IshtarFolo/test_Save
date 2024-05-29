using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class comportementVillageois1 : MonoBehaviour
{
    /*
     * Comportement du premier villageois (rouge) par Xavier Arbour: 
     * 
     * Le villageois se deplace entre le marche et le tableau des affiches des enfants perdus du village.
     * Devant le tableau, on lance son animation de "villageois triste" Egalement, lorsque Kirie (le joueur) lui parle,
     * il s'arrete et se tourne vers Kirie pour lui parler (Pour plus d'info sur cette partie regarder le script "interactionVillageois".
     * 
     */
    /*============
     * VARIABLES *
     ============*/
    int indexDestinations = 0;
    public Transform[] destinations;

    /* References aux composants */
    NavMeshAgent agent;
    Animator animateur;

    void Start()
    {
        // Associationdes variables des GetCompoenent
        agent = GetComponent<NavMeshAgent>();
        animateur = GetComponent<Animator>();

        if (!agent.isStopped)
        {
            // Declenchement de la coroutine
            StartCoroutine(ChangerDestination());
        }
    }

    void Update()
    {
        // On regarde si le NPC a atteint sa destination, et si c'est le cas, on relance sa "patrouille"
        if (!agent.pathPending && agent.remainingDistance < 0.5f && agent.enabled && !agent.isStopped)
        {
            StartCoroutine(ChangerDestination());
        }

        // Declenchement de la marche du personnage
        switch (agent.isStopped)
        {
            case true:
                animateur.SetBool("marche", false);
                break;
            case false:
                animateur.SetBool("marche", true);
                break;
        }


        // On declenche l'animation du villageois triste
        if (agent.remainingDistance >= agent.stoppingDistance && agent.isStopped && indexDestinations == 0)
        {
            animateur.SetBool("triste", true);
        }
        else
        {
            animateur.SetBool("triste", false);
        }
    }

    /*
     * COROUTINES
     -----------------------------------------------------------------------------------------------------------*/
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
        // Arrete le NavMeshAgent
        agent.isStopped = true;

        // On attend un peu avant de relancer le mouvement du NPC
        yield return new WaitForSeconds(10f);
        // On arrete le personnage
        agent.isStopped = false;
    }
}
