using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Scene = UnityEngine.SceneManagement.Scene;

public class Scene_credits : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Click;

    bool sonJoue = false;

    public float nbSecondes;
    private Scene scene;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        scene = SceneManager.GetActiveScene();
        //Debug.Log("Name: " + scene.name);

        if (scene.name == "CinematiqueIntro")
        {
            Invoke("cinematiqueDebutFini", nbSecondes);
        }
    }

    public void Update()
    {
        if(scene.name == "CinematiqueIntro" && Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("cinematiqueDebutFini", 1f);
        }
    }

    // Fonction pour aller sur la scène des crédits
    public void allerSurCredits()
    {
        if (!sonJoue)
        {
            sonJoue = true;
            audioSource.PlayOneShot(Click);

            if (audioSource.isPlaying)
            {
                //Debug.Log(Click);
            }
        }

        StartCoroutine(DelaiChargementCredits());
    }

    IEnumerator DelaiChargementCredits()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Credits");
    }

// Fonction pour aller sur la scène des réglages
    public void allerSurReglages()
    {
        if (!sonJoue)
        {
            sonJoue = true;
            audioSource.PlayOneShot(Click);

            if (audioSource.isPlaying)
            {
                //Debug.Log(Click);
            }
        }

        StartCoroutine(DelaiChargementReglages());
    }

    IEnumerator DelaiChargementReglages()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Reglages");
    }

// Fonction pour aller sur le menu principal
    public void allerMenuPrincipal()
    {
        if (!sonJoue)
        {
            sonJoue = true;
            audioSource.PlayOneShot(Click);

            if (audioSource.isPlaying)
            {
                //Debug.Log(Click);
            }
        }

        StartCoroutine(DelaiChargementMenu());
    }

    IEnumerator DelaiChargementMenu()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("EcranTitre");
    }

// Fonction pour aller sur la scène du jeu
    public void allerJeu()
    {
        if (!sonJoue)
        {
            sonJoue = true;
            audioSource.PlayOneShot(Click);

            if (audioSource.isPlaying)
            {
                //Debug.Log(Click);
            }
        }

        StartCoroutine(DelaiChargementJeu());
    }

    IEnumerator DelaiChargementJeu()
    {
        yield return new WaitForSeconds(1);
        // ICI, il faut programmer le changement de scènes selon
        // la progression du joueur.
        SceneManager.LoadScene("EcranTitre");
    }

    private void cinematiqueDebutFini()
    {
        SceneManager.LoadScene("Niveau1_Maison-Int");
    }
}