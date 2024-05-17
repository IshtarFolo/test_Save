using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class tombeDansEau : MonoBehaviour
{
    public GameObject manager;

    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "lac")
        {
            manager.GetComponent<savePosition>().Load();
        }
    }
}
