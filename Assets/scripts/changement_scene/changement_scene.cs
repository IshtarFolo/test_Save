using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changement_scene : MonoBehaviour
{
    [Header("Booléennes")]
    public static bool tutorielTermine = false;
    public static bool niveau1Termine = false;
    public static bool niveau2Termine = false;
    public static bool niveau3Termine = false;
    public static bool niveau4Termine = false;

    public static int noScenes = 2;

    [Header("Gameobjects")]
    public GameObject notificationPasFini; //Vérifier s'il s'agit d'un gameobject qu'on active et désactive

    // Fonction de trigger qui permet le chan
    public void OnTriggerEnter(Collider other)
    {

        //Lorsque le joueur a TERMINÉ le tutoriel, on lui permet d'aller dans le niveau1
        if (other.gameObject.tag == "porte" || tutorielTermine == true)
        {
            Invoke("niveau1", 2f);
            noScenes++;
        }
        else
        {
            notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }

        //Lorsque le joueur a TERMINÉ le niveau1, on lui permet d'aller dans le niveau2
        if (other.gameObject.tag == "triggerNiv2" || niveau1Termine == true)
        {
            Invoke("niveau2", 2f);
            noScenes++;

        }
        else
        {
            notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }

        //Lorsque le joueur a TERMINÉ le niveau2, on lui permet d'aller dans le niveau3
        if (other.gameObject.tag == "triggerNiv3" || niveau2Termine == true)
        {
            Invoke("niveau3", 2f);
            noScenes++;

        }
        else
        {
            notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }

        // Allons-nous garder le niveau 4?**
        if (other.gameObject.tag == "triggerNiv4" || niveau3Termine == true)
        {
            Invoke("niveau4", 2f);
            noScenes++;

        }
        else
        {
            notificationPasFini.SetActive(true);
            Invoke("fermerNotif", 5f);
        }


    }

    void niveau1()
    {
        SceneManager.LoadScene("niveau1");
    }

    void niveau2()
    {
        SceneManager.LoadScene("niveau2");
    }

    void niveau3()
    {
        SceneManager.LoadScene("niveau3");
    }

    // Allons-nous garder le niveau 4?**
    void niveau4()
    {
        SceneManager.LoadScene("niveau4");
    }

    void fermerNotif()
    {
        notificationPasFini.SetActive(false);
    }
}
