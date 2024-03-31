using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_credits : MonoBehaviour
{
    public string credits;
    public AudioClip sonClic;


    public void allerSurCredits()
    {
        SceneManager.LoadScene(credits);
        GetComponent<AudioSource>().PlayOneShot(sonClic);
    }
}
