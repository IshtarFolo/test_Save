using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Playables;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

public class notific : MonoBehaviour
{
    [Header("Gameobjects des UI")]
    public GameObject UInotifArmoire;
    public GameObject UInotifPorte;
    public GameObject UInotifCle;
    public GameObject UInotification;

    [Header("Booléennes")]
    public bool notification;
    public bool cleRamasse;

    // Le numero de l'index de la scene a charger 
    public static int noScene;

    // Ce script remplace le script de l'organigramme. (Plus facile collision et scènes)
    public void Start()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //Debug.Log(scene.name);

        //// Si on est dans le niveau tutoriel...
        //if (scene.name == "Niveau1_Maison-Int")
        //{
        //    Invoke("tutoriel", 0.1f);
        //    Invoke("enleverNoirFadeOut", 1f);
        //}

        //// Si on est dans le niveau tutoriel...
        //if (scene.name == "Niveau1_Village")
        //{
        //    Invoke("village", 0.1f);
        //    Invoke("enleverNoirFadeOut", 1f);
        //}

        // au debut du jeu, on trouve l'index de la scene a activer
        //noScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
