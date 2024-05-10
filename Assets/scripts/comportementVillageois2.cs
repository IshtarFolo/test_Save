using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class comportementVillageois2 : MonoBehaviour
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
        // On regarde si le NPC a atteint sa destination
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
    }

    // Pour changer de destination
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
        yield return new WaitForSeconds(20);

        agent.isStopped = false;
    }
}
