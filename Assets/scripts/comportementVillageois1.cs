using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class comportementVillageois1 : MonoBehaviour
{
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
        agent = GetComponent<NavMeshAgent>();
        animateur = GetComponent<Animator>();
        
        StartCoroutine(ChangerDestination());
    }

    void Update()
    {
        // On regarde si le NPC a atteint sa destination
        if (!agent.pathPending && agent.remainingDistance < 0.5f && agent.enabled)
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

        if (agent.remainingDistance >= agent.stoppingDistance && agent.isStopped && indexDestinations == 0)
        {
            animateur.SetBool("triste", true);
        }
        else
        {
            animateur.SetBool("triste", false);
        }
    }

    IEnumerator ChangerDestination()
    {
        // Retourne if si il n'y a pas de destinations
        if (destinations.Length == 0)
            yield break;
        // On envoie le NPC a la destination
        agent.destination = destinations[indexDestinations].position;

        // On choisi une nouvelle destination
        indexDestinations = (indexDestinations + 1) % destinations.Length;

        agent.isStopped = true;

        // On attend un peu avant de relancer le mouvement du NPC
        yield return new WaitForSeconds(10);

        agent.isStopped = false;
    }
}
