using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSave : MonoBehaviour
{
    /*
     * Activation de la sauvegarde par Xavier Arbour:
     * 
     * Le joueur passe sur un trigger possedant le tag "save" et on lance la fonction de sauvegarde du script "savePosition".
     *
     */
    public GameObject manager; // Reference au game manager
    public GameObject joueur; // Reference au joueur

    // Detection du trigger pour la sauvegarde
    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "save")
        {
            // On lance la sauvegarde du script "savePosition"
            manager.GetComponent<savePosition>().Save();
        }
    }
}
