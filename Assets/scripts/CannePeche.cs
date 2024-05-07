using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

//Script pour le mini-jeu de p�che par Camilia El Moustarih
//Inspir� du code de Mike's Code - 3D Survival Game Tutorial sur YouTube
public class CannePeche : MonoBehaviour
{
    //D�claration des variables
    //Des animations pour chaque �tat sont disponibles ****** � AJOUTER ******
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
        //R�cup�rer l'animator pour la canne � p�che
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
        //D�truire l'app�t
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
            //Detecter la zone de p�che
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Lorsque la souris pointe sur le lac, il est possible de p�cher
                if (hit.collider.CompareTag("zonePeche"))
                {
                    peutPecher = true;

                    //Afficher le mini jeu UI
                    SystemePeche.Instance.minijeu.SetActive(true);

                    //Ajouter un bouton pour commencer la p�che


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

        //Si la corde est utilis�e
        // � AJOUTER DIFF�RENTS �L�MENTS DE LA CANNE � P�CHE
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
                //Debug.Log("La canne � p�che est lanc�e");
            }
        }

        //Clic droit de la souris seulement si un poisson est d�tect�
        if (estLance && Input.GetMouseButtonDown(1) && SystemePeche.Instance.siContactPoisson) //Se d�clenche seulement s'il y a contact
        {
            PullRod();
            Debug.Log("Je tire");
        }
    }


    IEnumerator CastRod(Vector3 targetPosition, SourceDeau source)
    {
        //Ajouter une animation de lancer de canne � p�che
        estLance = true;
        animator.SetTrigger("Cast");

        //D�lai entre l'animation et l'apparition de l'app�t sur la zone de p�che
        yield return new WaitForSeconds(1f);

        //Cr�e un app�t qui flotte sur l'eau
        GameObject appatInstance = Instantiate(appatPrefab);
        appatInstance.transform.position = targetPosition;

        positionAppat = appatInstance.transform;

        appatRef = appatInstance;

        //D�but des conditions des poissons
        //R�f�rence au script de p�che. Dans ce cas, il ira chercher les poissons trouv�s dans le lac
        SystemePeche.Instance.CommencerPeche(SourceDeau.Lac);
    }

    private void PullRod()
    {
        animator.SetTrigger("Pull");
        estLance = false;
        tire = true;

        //D�but du mini jeu
        SystemePeche.Instance.SetEstEnTrainDeTirer();
    }
}
