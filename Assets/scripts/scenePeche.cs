using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenePeche : MonoBehaviour
{
    /*
     * Court script de changmenent de scene pour passer a la scene de peche par Xavier Arbour: 
     * 
     * Si le personnage entre en contact avec le trigger de la planche possedant le tag "zonePeche", on charge la scene de peche avec
     * son index(4). Ce script n'est actif que dans la scene du village.
     * 
     */
    private void OnTriggerEnter(Collider infoTrigger)
    {
        if (infoTrigger.gameObject.tag == "zonePeche")
        {
            SceneManager.LoadScene(4);
        }
    }
}
