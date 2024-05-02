using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleRoche : MonoBehaviour
{
    Animator animateur;

    void Start()
    {
        animateur = GetComponent<Animator>();
    }

    void Update()
    {
        StartCoroutine(remonterRoche());
    }

    IEnumerator remonterRoche()
    {
        yield return new WaitForSeconds(5f);
        animateur.SetBool("remonte", true);
        yield return new WaitForSeconds(10f);
        animateur.SetBool("remonte", false);
    }
}
