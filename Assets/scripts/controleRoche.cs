using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controleRoche : MonoBehaviour
{
    private float temps = 10f; // Timer pour remonter la roche

    Animator animateur;

    void Start()
    {
        animateur = GetComponent<Animator>();
    }

    void Update()
    {
        if (temps <= 0f)
        {
            temps = 10f;
            animateur.SetBool("remonte", false);
        }
        else if (temps < 5f)
        {
            temps -= Time.deltaTime;
            animateur.SetBool("remonte", false);
        }
        else if (temps > 5f)
        {
            temps -= Time.deltaTime;
            animateur.SetBool("remonte", true);
            return;
        }
    }
}
