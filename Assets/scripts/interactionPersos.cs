using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionPersos : MonoBehaviour
{
    public GameObject lettreE;
    public bool veutParler = false;

    // Start is called before the first frame update
    void Start()
    {
        lettreE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        lettreE.SetActive(true);
        veutParler = true;

    }

    private void OnTriggerExit(Collider other)
    {
        lettreE.SetActive(false);
        veutParler = false;
    }
}
