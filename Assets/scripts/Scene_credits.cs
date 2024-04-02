using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Scene_credits : MonoBehaviour
{
    public string credits;
    public AudioClip sonClic;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void allerSurCredits()
    {
        audioSource.PlayOneShot(sonClic, 1.2f);
        Invoke("DelaiChargementScene", 1f);
    }

    void DelaiChargementScene()
    {
        SceneManager.LoadScene(credits);
    }
}
