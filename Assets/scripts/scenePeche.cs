using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenePeche : MonoBehaviour
{
    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "zonePeche")
        {
            SceneManager.LoadScene(4);
        }
    }
}
