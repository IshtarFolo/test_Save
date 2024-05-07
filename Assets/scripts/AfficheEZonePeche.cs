using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script pour débuter le mini-jeu de pêche par Minji et Camilia
public class AfficheEZonePeche : MonoBehaviour
{
    //Variable pour la caméra du jeu de pêche et la main camera
    //public GameObject mainCam;
    //public GameObject camJeuPeche;

    public GameObject lettreE; // L'interaction avec la letttre E
    //Affichage du E

    //private void Start()
    //{
    //    mainCam.SetActive(true);
    //    camJeuPeche.SetActive(false);
    //}

    //private void Update()
    //{
    //    //Lorsque la lettre E s'affiche, le joueur peut commencer le mini-jeu
    //    //if (Input.GetKeyDown(KeyCode.E))
    //    //{
    //        //camJeuPeche.SetActive(true);
    //        //mainCam.SetActive(false);
    //    //}
    //}


    private void OnTriggerEnter(Collider infoCollision)
    {

        //Charger la scène de pêche lorsque le joueur entre en collision avec la planche 2
        if (infoCollision.gameObject.tag == "zonePeche")
        {
            //lettreE.SetActive(true);
            Debug.LogWarning("je touche la planche 2");
            SceneManager.LoadScene("Niveau1_MiniJeuPeche");
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "zonePeche")
        {
            lettreE.SetActive(false);
        }
    }

}