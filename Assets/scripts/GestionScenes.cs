using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GestionScenes : MonoBehaviour
{
    //Variable de son une fois un bouton cliqu�
    public AudioClip sonClic;

    //Pour ins�rer la sc�ne vis�e
    public string nomScene;

    //Fonction pour changer les sc�nes au clic d'un button
    //Couroutine pour avoir un d�lai selon l'information fournie dans le WaitForSeconds
    public void DelaiScene()
    {
        StartCoroutine (ChangerScene());
    }

    public IEnumerator ChangerScene()
    {
        //Charger la sc�ne apr�s 1 seconde pour donner au son le temps de jouer
        yield return new WaitForSeconds(1);
        //Charger la sc�ne indiqu�e dans l'inspecteur
        SceneManager.LoadScene(nomScene);
        //Jouer un son d�fini dans l'inspecteur
        //GetComponent<AudioSource>().PlayOneShot(sonClic);
    }
}
