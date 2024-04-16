using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.Playables;
using Scene = UnityEngine.SceneManagement.Scene;
using UnityEngine.SceneManagement;

public class _organigramme_jeu : MonoBehaviour
{
    [Header("Booléennes")]
    public bool notification;

    [Header("Gameobjects")]
    public GameObject Kirie;

    [Header("Gameobjects des UI")]
    public GameObject UIMaisonKirie;
    public GameObject UIJournalKirie;
    public GameObject UInotification; //Regarder si peut gérer avec Canvas

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


    public void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene.name);

        // Si on est dans le niveau tutoriel...
        if (scene.name == "Niveau1_Maison-Int")
        {
            Invoke("tutoriel", 0.1f);
        }
    }

    public void Update()
    {

    }

    private void tutoriel()
    {
        Debug.Log("Le script tutoriel roule");

        UIJournalKirie.SetActive(false);
        UIblabla.SetActive(false);
        UItutoriel.SetActive(true);
        UIcle.SetActive(true);
    }
}
