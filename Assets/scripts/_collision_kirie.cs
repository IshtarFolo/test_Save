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

    [Header("Gameobjects des UI")]
    public GameObject UIMaisonKirie;
    public GameObject UIJournalKirie;
    public GameObject UInotification;

    [Header("Gameobjects des quêtes")]
    public GameObject UIblabla;

    public GameObject UItutoriel;
    public GameObject UIcle;
    public GameObject UIbarreCle;
    public GameObject UIfiniTuto;
    public GameObject UIfiniTuto2;

    public GameObject UIvillage;
    public GameObject UIbarreCanne;
    public GameObject UIbarrePoisson;

    public GameObject UIforet;
    public GameObject UIbarreLettre;

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
        }
    }

    public void Update()
    {

    }

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
            Debug.Log("touché la clé");
            cle.SetActive(false);
            cleRamasse = true;
        }
    }

    public void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.name == "garde_robe_ferme")
        {
            if(cleRamasse == false)
            {
                Debug.Log("Armoire barré");
            }
            else if(cleRamasse == true)
            {
                armoireFerme.SetActive(false);
                armoireOuverte.SetActive(true);

                changement_scene.tutorielTermine = true;

                Invoke("accesALinventaire", 0.5f);
            }
        }
    }

    private void tutoriel()
    {
        //Debug.Log("Le script tutoriel roule");

        UIJournalKirie.SetActive(false);
        UIblabla.SetActive(false);
        UItutoriel.SetActive(true);
        UIcle.SetActive(true);
    }

    public void accesALinventaire()
    {
        UIJournalKirie.SetActive(true);
        Debug.Log("Le journal apparait...");
    }
}
