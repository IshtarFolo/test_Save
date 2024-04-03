using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GestionScenes : MonoBehaviour
{
    //Variable de son une fois un bouton cliqué
    public AudioClip sonClic;

    //Pour insérer la scène visée
    public string nomScene;

    //Fonction pour changer les scènes au clic d'un button
    //Couroutine pour avoir un délai selon l'information fournie dans le WaitForSeconds
    public void DelaiScene()
    {
        StartCoroutine (ChangerScene());
    }

    public IEnumerator ChangerScene()
    {
        //Charger la scène après 1 seconde pour donner au son le temps de jouer
        yield return new WaitForSeconds(1);
        //Charger la scène indiquée dans l'inspecteur
        SceneManager.LoadScene(nomScene);
        //Jouer un son défini dans l'inspecteur
        //GetComponent<AudioSource>().PlayOneShot(sonClic);
    }
}
