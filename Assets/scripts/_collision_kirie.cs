using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Playables;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.SceneManagement;

public class _collision_kirie : MonoBehaviour
{

    [Header("Gameobjects")]
    public GameObject cle;
    public GameObject armoireFerme;
    public GameObject armoireOuverte;

    [Header("Booléennes")]
    public bool notification;
    public bool cleRamasse;

    [Header("Booléennes des scènes Unity")]
    public static bool tutorielTermine = false;
    public static bool niveau1Termine = false;
    public static bool niveau2Termine = false;
    public static bool niveau3Termine = false;
    public static bool niveau4Termine = false;
    public static bool journalRamasse = false;

    [Header("Gameobjects des UI")]
    public GameObject UIMaisonKirie;
    public GameObject UIJournalKirie;
    public GameObject UInotification;

    [Header("Gameobjects des quêtes")]
    public GameObject UIblabla;
    public GameObject UInoirFadeIn;
    public GameObject UInoirFadeOut;

    public GameObject UItutoriel;
    public GameObject UIcle;
    public GameObject UIbarreCle;
    public GameObject UIarmoire;
    public GameObject UIfiniTuto;
    public GameObject UIfiniTuto2;

    public GameObject UIvillage;
    public GameObject UIvillageois;
    public GameObject UIbarrevillageois;
    public GameObject UIgatito;
    public GameObject UIbarregatito;
    public GameObject UIcanne;
    public GameObject UIbarreCanne;
    public GameObject UIpoisson;
    public GameObject UIbarrePoisson;
    public GameObject UIfiniVillage;

    public GameObject UIforet;
    public GameObject UIbarreLettre;

    [Header("Gameobjects des UI")]
    public GameObject UInotifArmoire;
    public GameObject UInotifPorte;
    public GameObject UInotifCle;
    public GameObject UInotifSauve;

    // Le numero de l'index de la scene a charger 
    public static int noScene;

    [Header("Scenes")]
    public Scene scene;


// Ce script remplace le script de l'organigramme. (Plus facile collision et scènes)
    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name);

        // Si on est dans le niveau tutoriel...
        if (scene.name == "Niveau1_Maison-Int")
        {
            Invoke("tutoriel", 0.1f);
            Invoke("enleverNoirFadeOut", 1f);
        }

        // Si on est dans le niveau tutoriel...
        if (scene.name == "Niveau1_Village")
        {
            Invoke("village", 0.1f);
            Invoke("enleverNoirFadeOut", 1f);
        }

        // au debut du jeu, on trouve l'index de la scene a activer
        noScene = SceneManager.GetActiveScene().buildIndex;
    }


// INFO TRIGGER
// ////////////////////////////////////////////////
    public void OnTriggerEnter(Collider infoTrigger)
    {
        //Partie manquante pour le menu principal? Cinématique?
        //Tant que les cinématiques ne sont pas terminés, le personnage ne peut pas bouger!

        // PARTIE POUR LE TUTORIEL - clé, armoire, etc.
        //D'abord, le joueur fini la cinématique de début lorsqu'il joue pour la première fois. 

        // Notes à Gaby :
        // Menu
        // Nouvelle partie
        // Charger scène de la cinématique
        // lorsque la cinématique fini, on change dans la scène du tutoriel.
        // booléenne pour bouger le perso est set to true
        // terminé le tuto est false
        // on affiche avec Canvas la première quête
        // on joue un son de porte barré
        // on affiche soit du texte avec Canvas de où le personnage est***
        // Personnage nous parle?
        // lorsque le personnage trouve la clé, on désactive celle-ci
        // on active la clé dans l’inventaire
        // on joue le son de la clé ramassé
        // on met la booléenne clé ramassé comme true

        // lorsque le joueur touche l’armoire et que la booléenne de la clé est true,
        // on change le mesh de l’armoire pour celle qui est ouverte, on désactive la clé dans l’inventaire,
        // on active l’inventaire(il est disponible avec J), on met la booléenne de l’inventaire à true,
        // on joue le son de l’armoire qui s’ouvre ou de la clé qui l’ouvre,
        // peut - être un son de la notification avec notification qui dit que l’inventaire est disponible.
        // On change le ui de la quête pour le message du village, on dit que la booléenne du tutoriel est
        // terminé donc true.Le joueur va pouvoir sortir de la maison avec la porte et on joue un son
        // de la porte qui s’ouvre. début du niveau 1.

        if (infoTrigger.gameObject.name == "cle")
        {
            //Debug.Log("touché la clé");
            UIbarreCle.SetActive(true);
            cle.SetActive(false);
            cleRamasse = true;
            UInotification.SetActive(true);
            Invoke("notifCle", 0.1f);
        }

        if (infoTrigger.gameObject.tag == "villageois")
        {
            UIbarrevillageois.SetActive(true);
            UIgatito.SetActive(true);
        }

        if (infoTrigger.gameObject.tag == "Gatito")
        {
            UIbarrevillageois.SetActive(false);
            UIvillageois.SetActive(false);
            UIgatito.SetActive(false);

            UIpoisson.SetActive(true);
            UIcanne.SetActive(true);
        }

        if (infoTrigger.gameObject.tag == "save")
        {
            //Debug.Log("Partie Sauvegardée");
            UInotification.SetActive(true);
            Invoke("notifSauvegarde", 0.1f);
        }
    }


// INFO COLLISION
// ////////////////////////////////////////////////
    public void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.name == "garde_robe_ferme")
        {
            if(cleRamasse == false)
            {
                //Debug.Log("Armoire barré");
                UInotification.SetActive(true);
                Invoke("notifArmoire", 0.1f);
            }
            else if(cleRamasse == true)
            {
                armoireFerme.SetActive(false);
                armoireOuverte.SetActive(true);
                tutorielTermine = true;

                Invoke("accesALinventaire", 0.1f);
            }
        }


        //Lorsque le joueur a TERMINÉ le tutoriel, on lui permet d'aller dans le niveau1
        if (infoCollision.gameObject.tag == "porte" && tutorielTermine == true)
        {
            //Debug.Log("Vous avez terminé le niveau et vous allez être téléporté!");
            UInoirFadeIn.SetActive(true);
            journalRamasse = true;
            Invoke("niveau1", 1f);
        }
        else
        {
            //notificationPasFini.SetActive(true);
            //UInotification.SetActive(true);
            //Invoke("notifPorte", 0.1f);
        }

        //Lorsque le joueur a TERMINÉ le niveau1, on lui permet d'aller dans le niveau2
        if (infoCollision.gameObject.tag == "triggerNiv2" && niveau1Termine == true)
        {
            Invoke("niveau2", 2f);
        }
        else
        {
            //notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }

        //Lorsque le joueur a TERMINÉ le niveau2, on lui permet d'aller dans le niveau3
        if (infoCollision.gameObject.tag == "triggerNiv3" && niveau2Termine == true)
        {
            Invoke("niveau3", 2f);
        }
        else
        {
            //notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }

        // Allons-nous garder le niveau 4?**
        if (infoCollision.gameObject.tag == "triggerNiv4" && niveau3Termine == true)
        {
            Invoke("niveau4", 2f);
        }
        else
        {
            //notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }
    }

// DANS LE TUTORIEL
// ////////////////////////////////////////////////
    private void tutoriel()
    {
        //Debug.Log("Le script tutoriel roule");

        UIJournalKirie.SetActive(false);
        UIblabla.SetActive(false);
        UItutoriel.SetActive(true);
        UIcle.SetActive(true);
        UIarmoire.SetActive(true);
    }

// ACCÈS À L'INVENTAIRE
// ////////////////////////////////////////////////
    public void accesALinventaire()
    {
        UIbarreCle.SetActive(false);
        UIcle.SetActive(false);
        UItutoriel.SetActive(false);
        UIarmoire.SetActive(false);

        UIfiniTuto.SetActive(true);
        UIfiniTuto2.SetActive(true);

        UIJournalKirie.SetActive(true);
        //Debug.Log("Le journal apparait...");
    }

    // DANS LE VILLAGE
    // ////////////////////////////////////////////////
    public void village()
    {
        UIblabla.SetActive(false);

        UIvillage.SetActive(true);
        //UIpoisson.SetActive(true);
        //UIcanne.SetActive(true);
        UIvillageois.SetActive(true);
    }

    void niveau1()
    {
        SceneManager.LoadScene("Niveau1_Village");
        // On prends l'index de la scene et on sauvegarde sa valeur pour le script de sauvegarde
        noScene = SceneManager.GetSceneByName("Niveau1_Village").buildIndex;
    }

    void niveau2()
    {
        SceneManager.LoadScene("MsgFinDemo");
    }

    void niveau3()
    {
        SceneManager.LoadScene("niveau3");
        // On prends l'index de la scene et on sauvegarde sa valeur pour le script de sauvegarde
        noScene = SceneManager.GetSceneByName("niveau3").buildIndex;
    }

    // Allons-nous garder le niveau 4?**
    void niveau4()
    {
        SceneManager.LoadScene("niveau4");
        // On prends l'index de la scene et on sauvegarde sa valeur pour le script de sauvegarde
        noScene = SceneManager.GetSceneByName("niveau4").buildIndex;
    }

    void enleverNoirFadeOut()
    {
        UInoirFadeOut.SetActive(false);
    }

    // PARTIE DÉDIÉ AU NOTIFICATION
    // ////////////////////////////////////////////////

    void notifArmoire()
    {
        if(notification == false)
        {
            UInotifArmoire.SetActive(true);
            Invoke("fermerNotif", 6f);
            notification = true;
        }
    }

    void notifPorte()
    {
        if(notification == false)
        {
            UInotifPorte.SetActive(true);
            Invoke("fermerNotif", 6f);
            notification = true;
        }
    }

    void notifCle()
    {
        if(notification == false)
        {
            UInotifCle.SetActive(true);
            Invoke("fermerNotif", 6f);
            notification = true;
        }
    }

    void notifSauvegarde()
    {
        if(notification == false)
        {
            UInotifSauve.SetActive(true);
            Invoke("fermerNotif", 6f);
            notification = true;
        }
    }

    void fermerNotif()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Niveau1_Maison-Int")
        {
            UInotification.SetActive(false);
            UInotifArmoire.SetActive(false);
            UInotifCle.SetActive(false);
            UInotifPorte.SetActive(false);

            notification = false;
        }

        if(scene.name == "Niveau1_Village")
        {
            UInotification.SetActive(false);
            UInotifSauve.SetActive(false);

            notification = false;
        }
    }


    // ----------------- SAUVEGARDE ET CHARGEMENT ---------------------
    // Lorsque la scene commence
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Lorsque la scene est quittee
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    // Lors du chargement de la scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        noScene = SceneManager.GetActiveScene().buildIndex;
    }
}
