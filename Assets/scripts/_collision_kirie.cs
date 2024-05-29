using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Playables;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.SceneManagement;
using TMPro;

public class _collision_kirie : MonoBehaviour
{

    [Header("Gameobjects")]
    public GameObject cle;
    public GameObject armoireFerme;
    public GameObject armoireOuverte;

    public GameObject planche2Quai; //La planche pour passer au mini jeu de pêche
    public GameObject cannePeche;

    [Header("Booléennes")]
    public bool notification;
    public bool cleRamasse;
    public bool audioJoue;
    public static bool cannePecheRamasse = false;
    public static bool finQueteLettres = false;

    private bool trouveGatito;

    [Header("Booléennes des scènes Unity")]
    public static bool tutorielTermine = false;
    public static bool niveau1Termine = false;
    public static bool niveau2Termine = false;
    public static bool niveau3Termine = false;
    public static bool journalRamasse = false;

    [Header("Gameobjects des UI généraux")]
    //public GameObject UIMaisonKirie;
    public GameObject UIJournalKirie;
    public GameObject UInotification;

    public GameObject UInoirFadeIn;
    public GameObject UInoirFadeOut;

    [Header("Gameobjects des éléments de journal")]
    public GameObject UIcontenu1;
    public GameObject UIcontenu2;
    public GameObject UIcontenu3;
    public GameObject UIcontenu4;
    public GameObject UIcontenu5;

    public GameObject OBJimageGatito;
    public GameObject OBJimageThays;
    public GameObject DESCpointsgatito;
    public GameObject DESCpointsthays;
    public GameObject DESCgatito;
    public GameObject DESCthays;

    public GameObject INVcle;
    public GameObject INVcanne;
    public GameObject INVpoisson;
    public GameObject INVechelle;
    public GameObject INVcarotte;
    public GameObject INVlettre;

    [Header("Gameobjects des quêtes du tutoriel")]
    public GameObject UItutoriel;
    public GameObject UIcle;
    public GameObject UIbarreCle;
    public GameObject UIarmoire;
    public GameObject UIfiniTuto;
    public GameObject UIfiniTuto2;

    public GameObject UIanimationJournal;

    [Header("Gameobjects des quêtes du village")]
    public GameObject UIvillage;
    public GameObject UIvillageois;
    public GameObject UIbarrevillageois;
    public GameObject UIgatito;
    public GameObject UIbarregatito;
    public GameObject UIcanne;
    public GameObject UIbarreCanne;
    public GameObject UIpoisson;
    public GameObject UIbarrePoisson;
    public GameObject UIvoirGatito;
    public GameObject UIfiniVillage;

    public GameObject UIminiJeuChaudFroid;

    [Header("Gameobjects des quêtes de la forêt")]
    public GameObject UIforet;
    public GameObject UIbarreLettre;
    public GameObject UINombreLettre;
    public TextMeshProUGUI UIIndexLettres;
    public int lettreRamassee = 0;
    public GameObject parchemin;
    public GameObject UITrahisonGatito;

    [Header("Gameobjects des Notifications")]
    public GameObject UInotifSauve;

    [Header("AudioSource")]
    public AudioSource gameManager;

    [Header("AudioClip")]
    public AudioClip prendreCle;
    public AudioClip armoireBarre;
    public AudioClip armoireSouvre;
    public AudioClip ouvrirPorte;

    [Header("Scenes")]
    public Scene scene;
    // Le numero de l'index de la scene a charger 
    public static int noScene;

    /*
     * Gestion des quetes avec des variables statiques pour sauvegarder
     * le progres du joueur tout au long du jeu
     */
    public static bool finTuto; // Variable du tutoriel
    public static bool GatitoParle; // variable de la conversation avec Gatito
    public static bool canneRamassee; //

    // Ce script remplace le script de l'organigramme. (Plus facile collision et scènes)
    public void Start()
    {
        audioJoue = false;
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

            //S'assurer que le boxCollider de la planche2 du quai soit désactiver
            planche2Quai.GetComponent<BoxCollider>().enabled = false;
        }

        if (scene.name == "Niveau2_foret")
        {
            //Destroy(UIfiniVillage);
            UIbarreCanne.SetActive(false);
            UIpoisson.SetActive(false);
            UIcanne.SetActive(false);
        }

        if (SystemePeche.finiPeche == true)
        {
            Invoke("derniereEtapeVillage", 0.1f);
        }

        // au debut du jeu, on trouve l'index de la scene a activer
        noScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        /*
         * Dans l'update on regarde si les booleens des quetes sont bel et bien true pour pouvoir
         * sauvegarder et charger la partie avec le progres du joueur et ainsi modifier l'etat du jeu et 
         * des notifications du UI.
         * 
         */
        /*
         * LE TUTORIEL
         -------------------------------------------------------------------------------------------------*/
        // Si le tutoriel est termine...
        if (finTuto)
        {
            //gameManager.PlayOneShot(ouvrirPorte, 1f);
            audioJoue = true;
            finTuto = true;
            journalRamasse = true; // Pour donner acces au journal meme quand on quitte le jeu !MUCHO IMPORTANT!
            Invoke("AudioPeutJouer", 2f);
        }

        /*
         * LE VILLAGE
         -----------------------------------------------------------------------------------------------------*/
        // Si le joueur a parle au bon villageois...
        if (scene.name == "Niveau1_Village")
        {

            if (interactionVillageois.aParleVillageois1 && SystemePeche.finiPeche == false)
            {
                UIvillageois.SetActive(false);
                UIgatito.SetActive(true);

                UIcontenu1.SetActive(false);
                UIcontenu2.SetActive(true);
            }

            // Si le joueur a parle a Gatito...
            if (DialogueGatitoVillage.finiParler == true && SystemePeche.finiPeche == false)
            {
                UIbarrevillageois.SetActive(false);
                UIvillageois.SetActive(false);
                UIgatito.SetActive(false);

                UIpoisson.SetActive(true);
                UIcanne.SetActive(true);
                GatitoParle = true;

                UIminiJeuChaudFroid.SetActive(true);

            if (UIcontenu2.activeInHierarchy == true)
            {
                UIcontenu2.SetActive(false);
                UIcontenu3.SetActive(true);
                OBJimageGatito.SetActive(true);
                DESCpointsgatito.SetActive(false);
                DESCgatito.SetActive(true);
            }
        }
            // Si le joueur ramasse la canne a peche...
            if (cannePecheRamasse && SystemePeche.finiPeche == false)
            {
                UIbarreCanne.SetActive(true);
                cannePeche.SetActive(false);
                UIminiJeuChaudFroid.SetActive(false);
                INVcanne.SetActive(true);
        }
            // Si le joueur fini le jeu de peche...
            if (SystemePeche.finiPeche == true && trouveGatito == false)
            {
                UIvoirGatito.SetActive(false);
                UIgatito.SetActive(false);
                DESCpointsgatito.SetActive(false);
                DESCgatito.SetActive(true);

                UIbarreCanne.SetActive(false);
                UIfiniVillage.SetActive(true);
                niveau1Termine = true;

                INVpoisson.SetActive(false);
                OBJimageGatito.SetActive(true);

                UIcontenu2.SetActive(false);
                UIcontenu4.SetActive(false);
                UIcontenu5.SetActive(true);
        }

        }
        /*
         * LA FORET
         --------------------------------------------------------------------------------------------------*/
        // Si le joueur a rammasse toutes les parties de lettres
        if (lettreRamassee == 5)
        {
            finQueteLettres = true;
        }

        if (finQueteLettres)
        {
            UIforet.SetActive(false);
            UINombreLettre.SetActive(false);
            UIIndexLettres.enabled = false;
            UITrahisonGatito.SetActive(true);
        }
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
            gameManager.PlayOneShot(prendreCle, 0.7f);
        }

        if (infoTrigger.gameObject.name == "trigger_garde_robe_ferme")
        {
            if (cleRamasse == false)
            {
                if (audioJoue == false)
                {
                    //Debug.Log("Armoire barré");
                    gameManager.PlayOneShot(armoireBarre, 0.7f);
                    audioJoue = true;
                    Invoke("AudioPeutJouer", 2f);
                }
            }
        }


        if (!finTuto)
        {
            if (infoTrigger.gameObject.name == "trigger_porte" && tutorielTermine == true)
            {
                //Debug.Log("Vous avez terminé le niveau et vous allez être téléporté!");
                if (audioJoue == false)
                {
                    gameManager.PlayOneShot(ouvrirPorte, 1f);
                    audioJoue = true;
                    finTuto = true;
                    Invoke("AudioPeutJouer", 2f);
                }
            }
        }


        if (infoTrigger.gameObject.tag == "villageois1" && interactionVillageois.aParleVillageois1 == true && SystemePeche.finiPeche == false)
        {
            //Debug.Log("Entrée");
            UIvillageois.SetActive(false);
            UIgatito.SetActive(true);

            UIcontenu1.SetActive(false);
            UIcontenu2.SetActive(true);
        }

        if (infoTrigger.gameObject.tag == "Gatito" && DialogueGatitoVillage.finiParler == true && SystemePeche.finiPeche == false)
        {
            UIbarrevillageois.SetActive(false);
            UIvillageois.SetActive(false);
            UIgatito.SetActive(false);

            UIpoisson.SetActive(true);
            UIcanne.SetActive(true);
            GatitoParle = true;

            UIminiJeuChaudFroid.SetActive(true);
            //Debug.Log("AAAAAAAAAAAAAAAH");

            if (UIcontenu2.activeInHierarchy == false)
            {
                UIbarrevillageois.SetActive(false);
                UIvillageois.SetActive(false);
                UIgatito.SetActive(false);

                UIpoisson.SetActive(true);
                UIcanne.SetActive(true);
                GatitoParle = true;

                UIminiJeuChaudFroid.SetActive(true);
                //Debug.Log("AAAAAAAAAAAAAAAH");

                if (UIcontenu2.activeInHierarchy == true)
                {
                    UIcontenu2.SetActive(false);
                    UIcontenu3.SetActive(true);
                    OBJimageGatito.SetActive(true);
                    DESCpointsgatito.SetActive(false);
                    DESCgatito.SetActive(true);
                }
                UIcontenu2.SetActive(false);
                UIcontenu3.SetActive(true);
                OBJimageGatito.SetActive(true);
                DESCpointsgatito.SetActive(false);
                DESCgatito.SetActive(true);
            }
        }


        if (infoTrigger.gameObject.tag == "Gatito" && SystemePeche.finiPeche == true)
        {
            //Debug.Log("Ceci est la suite des choses");
            trouveGatito = true;
        }

        if (infoTrigger.gameObject.tag == "save")
        {
            //Debug.Log("Partie Sauvegardée");
            UInotification.SetActive(true);
            Invoke("notifSauvegarde", 0.1f);
        }

        //InfoTrigger pour attraper les 5 letres

        if (infoTrigger.gameObject.tag == "lettre")
        {
            Destroy(infoTrigger.gameObject);
            UIforet.SetActive(false);
            UINombreLettre.SetActive(true);

            // Incrémentez lettresRamassee d'une seule unité
            lettreRamassee += 1;
            Debug.Log("Lettre trouvée");

            // Mettez à jour l'index de l'UI avec le nombre de lettres ramassées
            UIIndexLettres.text = lettreRamassee.ToString();

            if (lettreRamassee == 5)
            {
                Debug.Log("La lettre est complète");
                parchemin.SetActive(true);
                Invoke("DesactiverParchemin", 10f);
            }
        }

        //POUR ACTIVER LA PLANCHE2 POUR MINI JEU DE PECHE
        //Si le joueur touche la canne à pêche on peut activer la zone pour passer au niveau de la pêche
        if (infoTrigger.gameObject.tag == "cannePeche" && UIminiJeuChaudFroid.activeInHierarchy == true)
        {
            cannePecheRamasse = true;
            //On va le détruire et changer de cible ou désactiver la scrollbar
            //Destroy(gameObject);
            UIbarreCanne.SetActive(true);
            cannePeche.SetActive(false);
            UIminiJeuChaudFroid.SetActive(false);
            INVcanne.SetActive(true);
        }

        if (cannePecheRamasse)
        {
            //Activer le box collider de la planche2
            planche2Quai.GetComponent<BoxCollider>().enabled = true;
        }

        //Mettre la booléenne finiPeche à false pour enlever le texte des quêtes
        if (infoTrigger.gameObject.tag == "EnleverFiniPeche" && niveau1Termine == true)
        {
            SystemePeche.finiPeche = false;
        }
    }

    void DesactiverParchemin()
    {
        parchemin.SetActive(false);
    }

    void AudioPeutJouer()
    {
        audioJoue = false;
    }


    // INFO COLLISION
    // ////////////////////////////////////////////////
    public void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.name == "garde_robe_ferme")
        {
            if (cleRamasse == false)
            {
                //Debug.Log("Armoire barré");
            }
            else if (cleRamasse == true)
            {
                armoireFerme.SetActive(false);
                armoireOuverte.SetActive(true);
                tutorielTermine = true;
                gameManager.PlayOneShot(armoireSouvre, 0.7f);

                UIanimationJournal.SetActive(true);

                Invoke("accesALinventaire", 0.1f);
                Invoke("enleverAnimation", 5f);
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

        //Lorsque le joueur a TERMINÉ le niveau1, on lui permet d'aller dans le niveau2
        if (infoCollision.gameObject.tag == "triggerNiv2" && niveau1Termine == true)
        {
            UInoirFadeIn.SetActive(true);
            UIfiniVillage.SetActive(false);
            Invoke("niveau2", 1f);
        }

        //Lorsque le joueur a TERMINÉ le niveau2, on lui permet d'aller dans le niveau3
        if (infoCollision.gameObject.tag == "triggerNiv3" && niveau2Termine == true)
        {
            Invoke("niveau3", 1f);
        }

        // Allons-nous garder le niveau 4?**
        if (infoCollision.gameObject.tag == "triggerNiv4" && niveau3Termine == true)
        {
            Invoke("niveau4", 1f);
        }
    }

    // DANS LE TUTORIEL
    // ////////////////////////////////////////////////
    private void tutoriel()
    {
        //Debug.Log("Le script tutoriel roule");

        UIJournalKirie.SetActive(false);
        UItutoriel.SetActive(true);
        UIcle.SetActive(true);
        UIarmoire.SetActive(true);
    }

    // DANS LE TUTORIEL - Animation journal
    // ////////////////////////////////////////////////
    private void enleverAnimation()
    {
        UIanimationJournal.SetActive(false);
    }

    // DERNIÈRE ÉTAPE AVANT LA FIN DU VILLAGE
    // ////////////////////////////////////////////////
    public void derniereEtapeVillage()
    {
        //Debug.Log("Vous avez presque fini! Allez voir Gatito!");
        UIvillageois.SetActive(false);
        UIvoirGatito.SetActive(true);

        UIcontenu1.SetActive(false);
        UIcontenu2.SetActive(false);
        UIcontenu3.SetActive(false);
        UIcontenu4.SetActive(false);

        UIcanne.SetActive(false);
        UIbarreCanne.SetActive(false);
        UIpoisson.SetActive(false);

        INVpoisson.SetActive(true);
        INVcanne.SetActive(true);
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
        SceneManager.LoadScene("Niveau2_Foret");
        noScene = SceneManager.GetSceneByName("Niveau2_Foret").buildIndex;
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

    void notifSauvegarde()
    {
        if (notification == false)
        {
            UInotifSauve.SetActive(true);
            Invoke("fermerNotif", 6f);
            notification = true;
        }
    }

    void fermerNotif()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (scene.name == "Niveau1_Village")
        {
            UInotification.SetActive(false);
            UInotifSauve.SetActive(false);

            notification = false;
        }
        else if (scene.name == "Niveau2_Foret")
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
        // On purge la memoire
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    // Lorsque la scene est quittee
    private void OnDisable()
    {
        // Ici aussi, on purge la memoire
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Lors du chargement de la scene
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Le noScene est egal a l'index de la scene 
        noScene = SceneManager.GetActiveScene().buildIndex;

        // Mettre la booléenne à true
        Debug.Log($"Scene loaded: {scene.name}, SystemePeche.finiPeche: {SystemePeche.finiPeche}");
        if (scene.name == "Niveau1_Village" && SystemePeche.finiPeche == true)
        {
            DialogueGatitoVillage.SetPoissonMange(true);
            Debug.Log("DialogueGatitoVillage.poissonMange set to true");
        }
    }
}
