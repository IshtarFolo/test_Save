using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Scene_credits : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip Click;

    bool sonJoue = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {

    }

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
        SceneManager.LoadScene("EcranTitre");
    }
}
