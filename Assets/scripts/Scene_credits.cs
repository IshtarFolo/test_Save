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
                Debug.Log(Click);
            }
        }

        StartCoroutine(DelaiChargementScene());
    }

    IEnumerator DelaiChargementScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Credits");
    }
}
