using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script pour le syst�me de p�che du niveau 2 de lac - Le joueur devra p�cher 3 poissons pour continuer la qu�te
//Par Camilia El Moustarih

//Liste d'�num�ration au cas o� il y aurait plusieurs sources d'eau offrant diff�rents poissons
//Exemples oc�an, rivi�re, mar�cage, etc.
//Dans ce jeu, il n'y aura qu'un lac
public enum SourceDeau{
    Lac
}

public class SystemePeche : MonoBehaviour
{
    public static SystemePeche Instance { get; set; }

    private void Awake()
    {
        //S'assurer qu'il n'y a pas d'autre syst�me de p�che dupliqu� dans le jeu. Si oui, le supprimer.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Dans le cas o� nous voudrions diff�rentes sources d'eau qui offrent diff�rents poissons
    public List<InfosPoissons> poissonsDeLac;
    //Les poissons offerts ici sont
    //le saumon
    //la morue
    //la truite
    //le AucunPoisson

    //V�rifier s'il y a contact et terminer la session de p�che
    public bool siContactPoisson;
    private bool estEnTrainDeTirer;

    public GameObject minijeu;

    public static event Action OnPecheTerminee;

    internal void CommencerPeche(SourceDeau sourceDeau)
    {
        //Looper � travers la liste de poissons et leurs probabilit�s de p�che
        StartCoroutine(PecheCoroutine(sourceDeau));
    }

    IEnumerator PecheCoroutine(SourceDeau sourceDeau)
    {
        //D�lai de 3 secondes avant contact avec poisson
        yield return new WaitForSeconds(3f);

        InfosPoissons poisson = CalculerProbPoisson(sourceDeau);

        //S'il y a contact avec des poissons
        //Si le poisson touch� s'appelle AucunPoisson, le jeu s'arr�te. Sinon, continuer avec les autres probabilit�s
        if (poisson.nomPoisson == "AucunPoisson")
        {
            Debug.Log("Aucun contact");
            //La session de p�che se termine
            PecheTerminee();
        }
        else
        {
            Debug.Log(poisson.nomPoisson + " a mordu !");
            StartCoroutine(CommencerResistancePoisson(poisson));
        }
    }

    internal void TerminerMiniJeu(bool reussite)
    {
        minijeu.gameObject.SetActive(false);

        //Terminer la session de p�che
        if (reussite)
        {
            PecheTerminee();

            //Ajouter � l'inventaire
        }
        else
        {
            PecheTerminee();
        }
    }

    IEnumerator CommencerResistancePoisson(InfosPoissons poisson)
    {
        siContactPoisson = true;

        //Attendre que le joueur clique sur le bouton droit de la souris pour tirer
        while(estEnTrainDeTirer == false)
        {
            yield return null;
        }

        Debug.LogWarning("Commencer mini-jeu");

        //Commencer le mini-jeu de p�che avec le ui
        CommencerMiniJeuPeche();
    }

    private void CommencerMiniJeuPeche()
    {
        minijeu.SetActive(true);
    }

    public void SetEstEnTrainDeTirer()
    {
        estEnTrainDeTirer = true;
    }

    private InfosPoissons CalculerProbPoisson(SourceDeau sourceDeau)
    {
        List<InfosPoissons> poissonDispo = CapturerPoissonDispo(sourceDeau);

        //Calculer la probabilit�
        float probabiliteTotale = 0f;
        foreach (InfosPoissons poisson in poissonDispo) 
        // Truite = 5% (0-4) Aucun poisson = 10% (5-9) Saumon = 20% (10-19) Morue = 40% (20-39) = probabilit� totale de : 75%
        {
            probabiliteTotale += poisson.probabiliteDattraper;
        }

        //G�n�rer un nombre al�atoire entre 0 et toutes les probabilit�s (75%)
        int nombreAleatoire = UnityEngine.Random.Range(0, Mathf.FloorToInt(probabiliteTotale) + 1); //entre 0 et 75
        Debug.Log("Le nombre pig� est de " + nombreAleatoire);

        //Traverser la boucle des poissons et v�firier les probabilit�s
        float probabilitePigee = 0f;
        foreach (InfosPoissons poisson in poissonDispo)
        {
            probabilitePigee += poisson.probabiliteDattraper;
            if (nombreAleatoire <= probabilitePigee)
            {
                //Un poisson X a mordu
                return poisson;
            }
        }

        //En cas d'erreur de nombre al�atoire pig�
        return null;
    }

    private List<InfosPoissons> CapturerPoissonDispo(SourceDeau sourceDeau)
    {
        //S'il y a plusieurs sources d'eau offrant diff�rents poissons
        switch (sourceDeau)
        {
            case SourceDeau.Lac:
                return poissonsDeLac;

            default:
                return null;
        }
    }

    //Terminer le jeu s'il n'y plus de contact et que le joueur ne tire plus
    private void PecheTerminee()
    {
        siContactPoisson = false;
        estEnTrainDeTirer = false;

        //Trigger pour terminer la manipulation de la canne � p�che
        OnPecheTerminee?.Invoke();
        //� FAIRE D�SACTIVER LA CANNE � P�CHE ET AFFICHER UN MESSAGE LORSQUE LES 3 POISSONS POUR GATITO SONT RAMASS�S
    }
}
