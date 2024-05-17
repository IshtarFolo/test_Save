using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tombeDansEau : MonoBehaviour
{
    public GameObject manager;


    Animator animateur;
    Rigidbody rb;

    private void Start()
    {
        animateur = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "lac")
        {
            animateur.SetTrigger("noyade");
            rb.isKinematic = true;
            StartCoroutine(Mort());
        }
    }

    IEnumerator Mort()
    {
        yield return new WaitForSeconds(2f);
        manager.GetComponent<savePosition>().Load();
    }
}
