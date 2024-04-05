using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionPersos : MonoBehaviour
{
    public TextMeshProUGUI lettreE;
    public bool veutParler = false;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lettreE.color = new Color(255, 255, 255);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lettreE.enabled = true;
        veutParler = true;

    }

    private void OnTriggerExit(Collider other)
    {
        lettreE.enabled = false;
        veutParler = false;
    }
}
