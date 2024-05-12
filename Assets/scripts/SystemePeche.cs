using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script pour le syst�me de p�che du niveau 2 de lac - Le joueur devra p�cher 3 poissons pour continuer la qu�te
//Par Camilia El Moustarih

//Liste d'�num�ration au cas o� il y aurait plusieurs sources d'eau offrant diff�rents poissons
//Exemples oc�an, rivi�re, mar�cage, etc.
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
        audioSource = GetComponent<AudioSource>();

        //S'assurer qu'il n'y a pas d'autre syst�me de p�che dupliqu� dans le jeu. Si oui, le supprimer.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        //Le canvas du mini jeu est d�sactiv� au d�but de la p�che, il sera r�activ� seulement lorsqu'il y a contact avec un poisson autre que "Aucun poisson"
        minijeu.SetActive(false);
    }

    //Dans le cas o� nous voudrions diff�rentes sources d'eau qui offrent diff�rents poissons
    //Les poissons offerts ici sont
    //le saumon
    //la morue
    //la truite
    //le AucunPoisson
    public List<InfosPoissons> poissonsDeLac;

    //V�rifier s'il y a contact et terminer la session de p�che
    public bool siContactPoisson;
    private bool estEnTrainDeTirer;

    //public static event Action OnPecheTerminee;

    //Variables pour le mini jeu UI
    public GameObject canvas;
    public GameObject minijeu;

    //Variables de son
    [SerializeField] AudioClip poissonCapture, poissonPerdu;
    private AudioSource audioSource;    

    IEnumerator PecheCoroutine(SourceDeau sourceDeau)
    {
        //D�lai de 5 secondes avant contact avec poisson
        yield return new WaitForSeconds(5f);

        //R�cup�rer la liste des poissons de cette source d'eau
        InfosPoissons poisson = CalculerProbPoisson(sourceDeau);

        //S'il y a contact avec des poissons
        //Si le poisson touch� s'appelle AucunPoisson, le jeu s'arr�te. Sinon, continuer avec les autres probabilit�s
        if((poisson.nomPoisson == "Saumon") || (poisson.nomPoisson == "Truite") || (poisson.nomPoisson == "Morue"))
        {
            Debug.Log(poisson.nomPoisson + " a mordu !");
            StartCoroutine(CommencerResistancePoisson(poisson));

            //afficher le canvas du mini jeu
            canvas.SetActive(true);
        }
    }

    internal void CommencerPeche(SourceDeau sourceDeau)
    {
        //Looper � travers la liste de poissons et leurs probabilit�s de p�che
        StartCoroutine(PecheCoroutine(sourceDeau));
        Debug.Log("peche commenc�e");
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

        //Commencer le mini-jeu de p�che avec le ui
        CommencerMiniJeuPeche();
    }

    internal void TerminerMiniJeu(bool reussite)
    {
        //D�sactiver le mini jeu et son canvas
        minijeu.SetActive(false);

        //Terminer la session de p�che
        if (reussite)
        {
            PecheTerminee();
            Debug.Log("poisson attrap�");
            //Jouer le son de poisson captur�
            audioSource.PlayOneShot(poissonCapture);

            

            //Ajouter � l'inventaire

            //CommencerMiniJeuPeche();
        }
        else
        {
            PecheTerminee();
            Debug.Log("poisson perdu");
            //Jouer le son de poisson captur�
            audioSource.PlayOneShot(poissonPerdu);

            //Terminer la p�che
            SystemePeche.Instance.PecheTerminee();
            estEnTrainDeTirer = false;

            //CommencerMiniJeuPeche();

            //Recharger la sc�ne en cas d'�chec
            SceneManager.LoadScene("Niveau1_MiniJeuPeche");
        }
    }

    private void CommencerMiniJeuPeche()
    {
        //activer le mini jeu de p�che
        minijeu.gameObject.SetActive(true);

    }

    public void SetEstEnTrainDeTirer()
    {
        estEnTrainDeTirer = true;
        //Afficher le canvas
        canvas.gameObject.SetActive(true);
    }

    public void ResetEstEnTrainDeTirer()
    {
        estEnTrainDeTirer = false;
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
    public void PecheTerminee()
    {
        siContactPoisson = false;
        estEnTrainDeTirer = false;

        Debug.Log("Peche termin�e");

        

        //Trigger pour terminer la manipulation de la canne � p�che
        //OnPecheTerminee?.Invoke();

        //CommencerPeche(SourceDeau.Lac);
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
}
