using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//Script pour le mini-jeu de peche par Camilia El Moustarih
//Inspire du code de Mike's Code - 3D Survival Game Tutorial sur YouTube
public class CannePeche : MonoBehaviour
{
    //Declaration des variables
    //Des animations pour chaque état sont disponibles **À AJOUTER**
    public bool estEquipe;
    public bool peutPecher;
    public bool estLance;
    public bool tire;

    //Pour les animations de la canne 
    Animator animator;
    public GameObject appatPrefab;
    public GameObject finCorde;
    public GameObject debutCorde;
    public GameObject debutCanne;

    //Animations de Kirie
    //public GameObject kiriePerso;
    //Animator animatorKirie;

    Transform positionAppat;
    GameObject appatRef;

    public void Start()
    {
        // Récupérer l'animator de la canne peche
        animator = GetComponent<Animator>();
        estEquipe = true;
        peutPecher = true;
        //Commencer la peche automatiquement
        StartCoroutine(DebutPeche());

        //Activer l'animation de pêche de Kirie
        //animatorKirie = kiriePerso.GetComponent<Animator>();
           
    }

    private void OnEnable()
    {
        SystemePeche.OnPecheTerminee += GestionFinPeche;
    }

    private void GestionFinPeche()
    {
        //Detruire l'appat
        Destroy(appatRef);
    }

    private void OnDestroy()
    {
        SystemePeche.OnPecheTerminee -= GestionFinPeche;
    }

    //Coroutine pour débuter la peche après un court délai
    IEnumerator DebutPeche()
    {
        yield return null;

        while (true) 
        {
            //Sur clic gauche, commencer la peche
            if (Input.GetMouseButtonDown(0) && estEquipe && peutPecher && !estLance && !tire)
            {
                Debug.Log("Je peche - clic gauche");
                //animatorKirie.SetTrigger("Cast");
                SystemePeche.Instance.CommencerPeche(SourceDeau.Lac);
                StartCoroutine(LancerCannePeche(transform.position));
                yield break;
            }

            
            yield return null;
        }
    }


    void Update()
    {
        //Tirer la canne a peche sur le clic droit de la souris
        if (estLance && Input.GetMouseButtonDown(1) && SystemePeche.Instance.siContactPoisson) //Se declenche seulement s'il y a contact
        {
            TirerCanne();
            Debug.Log("Appel de la fonction TirerCanne - clic droit");
        }
    }

    //Coroutine pour lancer la canne 
    IEnumerator LancerCannePeche(Vector3 targetPosition)
    {
            estLance = true;
            animator.SetTrigger("LancerCanne");
            yield return new WaitForSeconds(1f);

            GameObject instanceAppat = Instantiate(appatPrefab);
            instanceAppat.transform.position = targetPosition;

            positionAppat = instanceAppat.transform;

            appatRef = instanceAppat;

            //Start Fish Bite Logic 
                   
    }

    //Methode pour tirer la canne peche
    private void TirerCanne()
    {
        animator.SetTrigger("ResistanceCanne");
        Debug.Log("je tire");
        estLance = false;
        tire = true;

        //Commencer le miniJeu
        SystemePeche.Instance.SetEstEnTrainDeTirer();
    }
}
