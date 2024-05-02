using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//Script pour le mini-jeu de pêche par Camilia El Moustarih
//Inspiré du code de Mike's Code - 3D Survival Game Tutorial sur YouTube
public class CannePeche : MonoBehaviour
{
    //Déclaration des variables
    //Des animations pour chaque état sont disponibles ****** À AJOUTER ******
    public bool estEquipe;
    public bool peutPecher;
    public bool estLance;
    public bool tire;

    Animator animator;
    public GameObject appatPrefab;
    public GameObject finCorde;  
    public GameObject debutCorde;    
    public GameObject debutCanne;       

    Transform positionAppat;
    GameObject appatRef;

    private void Start()
    {
        //Récupérer l'animator pour la canne à pêche
        //Elle comporte 4 animations 
        animator = GetComponent<Animator>();
        estEquipe = true;
    }

    public void OnEnable()
    {
        SystemePeche.OnPecheTerminee += GestionFinPeche;
    }

    private void GestionFinPeche()
    {
        //Détruire l'appât
        Destroy(appatRef);
    }

    public void OnDestroy()
    {
        SystemePeche.OnPecheTerminee -= GestionFinPeche;
    }

    void Update()
    {
        if (estEquipe)
        {
            //Détecter la zone de pêche
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Lorsque la souris pointe sur le lac, il est possible de pêcher
                if (hit.collider.CompareTag("zonePeche"))
                {
                    peutPecher = true;

                    //Clic du bouton gauche de la souris
                    if (Input.GetMouseButtonDown(0) && !estLance && !tire)
                    {
                        SourceDeau source = hit.collider.gameObject.GetComponent<ZonePeche>().sourceDeau;
                        StartCoroutine(CastRod(hit.point, source));
                    }
                }
                else
                {
                    peutPecher = false;
                }

            }
            else
            {
                peutPecher = false;
            }
        }

        //Si la corde est utilisée
        // À AJOUTER DIFFÉRENTS ÉLÉMENTS DE LA CANNE À PÊCHE
        if (estLance || tire)
        {
            if (debutCorde != null && debutCanne != null && finCorde != null)
            {
                debutCorde.transform.position = debutCanne.transform.position;

                if (positionAppat != null)
                {
                    finCorde.transform.position = positionAppat.position;
                }
            }
            else
            {
                //Debug.Log("La canne à pêche est lancée");
            }
        }

        //Clic droit de la souris seulement si un poisson est détecté
        if (estLance && Input.GetMouseButtonDown(1) && SystemePeche.Instance.siContactPoisson) //Se déclenche seulement s'il y a contact
        {
            PullRod();
            Debug.Log("Je tire");
        }
    }


    IEnumerator CastRod(Vector3 targetPosition, SourceDeau source)
    {
        //Ajouter une animation de lancer de canne à pêche
        estLance = true;
        animator.SetTrigger("Cast");

        //Délai entre l'animation et l'apparition de l'appât sur la zone de pêche
        yield return new WaitForSeconds(1f);

        //Crée un appât qui flotte sur l'eau
        GameObject appatInstance = Instantiate(appatPrefab);
        appatInstance.transform.position = targetPosition;

        positionAppat = appatInstance.transform;

        appatRef = appatInstance;

        //Début des conditions des poissons
        //Référence au script de pêche. Dans ce cas, il ira chercher les poissons trouvés dans le lac
        SystemePeche.Instance.CommencerPeche(SourceDeau.Lac);
    }

    private void PullRod()
    {
        animator.SetTrigger("Pull");
        estLance = false;
        tire = true;

        //Début du mini jeu
        SystemePeche.Instance.SetEstEnTrainDeTirer();
    }
}
