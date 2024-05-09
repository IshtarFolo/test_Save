using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script pour le système de pêche du niveau 2 de lac - Le joueur devra pêcher 3 poissons pour continuer la quête
//Par Camilia El Moustarih

//Liste d'énumération au cas où il y aurait plusieurs sources d'eau offrant différents poissons
//Exemples océan, rivière, marécage, etc.
//Dans ce jeu, il n'y aura qu'un lac
public enum SourceDeau
{
    Lac
}
public class SystemePeche : MonoBehaviour
{
    public static SystemePeche Instance { get; set; }
    public static Action OnPecheTerminee { get; internal set; }

    private void Awake()
    {
        //S'assurer qu'il n'y a pas d'autre système de pêche dupliqué dans le jeu. Si oui, le supprimer.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    //Dans le cas où nous voudrions différentes sources d'eau qui offrent différents poissons
    //Les poissons offerts ici sont
    //le saumon
    //la morue
    //la truite
    //le AucunPoisson
    public List<InfosPoissons> poissonsDeLac;

    //Vérifier s'il y a contact et terminer la session de pêche
    public bool siContactPoisson;
    private bool estEnTrainDeTirer;

    //public static event Action OnPecheTerminee;
 

    IEnumerator PecheCoroutine(SourceDeau sourceDeau)
    {
        //Délai de 5 secondes avant contact avec poisson
        yield return new WaitForSeconds(5f);

        //Récupérer la liste des poissons de cette source d'eau
        InfosPoissons poisson = CalculerProbPoisson(sourceDeau);

        //S'il y a contact avec des poissons
        //Si le poisson touché s'appelle AucunPoisson, le jeu s'arrête. Sinon, continuer avec les autres probabilités
        if (poisson.nomPoisson == "AucunPoisson")
        {
            Debug.Log("Aucun contact, la session de pêche est terminée");
            //La session de pêche se termine
            PecheTerminee();
        }
        else
        {
            Debug.Log(poisson.nomPoisson + " a mordu !");
            StartCoroutine(CommencerResistancePoisson(poisson));
        }
    }

    internal void CommencerPeche(SourceDeau sourceDeau)
    {
        //Looper à travers la liste de poissons et leurs probabilités de pêche
        StartCoroutine(PecheCoroutine(sourceDeau));
        Debug.Log("peche commencée");
    }

    IEnumerator CommencerResistancePoisson(InfosPoissons poisson)
    {
        siContactPoisson = true;

        //Attendre que le joueur clique sur le bouton droit de la souris pour tirer
        while (estEnTrainDeTirer == false)
        {
            yield return null;
        }

        Debug.LogWarning("Commencer mini-jeu");
        Debug.Log("Afficher maintenant le UI pour le miniJeu");

        //Commencer le mini-jeu de pêche avec le ui
        //CommencerMiniJeuPeche();
    }

    public void SetEstEnTrainDeTirer()
    {
        estEnTrainDeTirer = true;
    }
    

    private InfosPoissons CalculerProbPoisson(SourceDeau sourceDeau)
    {
        List<InfosPoissons> poissonDispo = CapturerPoissonDispo(sourceDeau);

        //Calculer la probabilité
        float probabiliteTotale = 0f;
        foreach (InfosPoissons poisson in poissonDispo)
        // Truite = 5% (0-4) Aucun poisson = 10% (5-9) Saumon = 20% (10-19) Morue = 40% (20-39) = probabilité totale de : 75%
        {
            probabiliteTotale += poisson.probabiliteDattraper;
        }

        //Générer un nombre aléatoire entre 0 et toutes les probabilités (75%)
        int nombreAleatoire = UnityEngine.Random.Range(0, Mathf.FloorToInt(probabiliteTotale) + 1); //entre 0 et 75
        Debug.Log("Le nombre pigé est de " + nombreAleatoire);

        //Traverser la boucle des poissons et véfirier les probabilités
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

        //En cas d'erreur de nombre aléatoire pigé
        return null;
    }
    private void PecheTerminee()
    {
        siContactPoisson = false;
        estEnTrainDeTirer = false;

        //Trigger pour terminer la manipulation de la canne à pêche
        OnPecheTerminee?.Invoke();
    }
    private List<InfosPoissons> CapturerPoissonDispo(SourceDeau sourceDeau)
    {
        //S'il y a plusieurs sources d'eau offrant différents poissons
        switch (sourceDeau)
        {
            case SourceDeau.Lac:
                return poissonsDeLac;

            default:
                return null;
        }
    }
}
