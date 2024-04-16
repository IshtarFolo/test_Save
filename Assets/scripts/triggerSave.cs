using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSave : MonoBehaviour
{
    public GameObject manager; // Reference au game manager

    // Detection du trigger pour la sauvegarde
    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "save")
        {
            manager.GetComponent<savePosition>().Save();
        }
    }
}
