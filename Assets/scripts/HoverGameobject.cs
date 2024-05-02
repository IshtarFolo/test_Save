using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoverGameobject : MonoBehaviour
{
    public Vector3 mousePos; //To store mouse position

    public Transform incrementElement; //The UI element I'm instantiating
    public Transform parentObj; //The UI Canvas

    public TMPro.TMP_Text kiriePense;

    void OnMouseOver()
    {
        if(gameObject.name == "garde_robe_ferme")
        {
            //Debug.Log(" Ceci est le test de la roche 1");
            kiriePense.text = "L'armoire est barré...";
        }

        if (gameObject.name == "PorteSortie")
        {
            //Debug.Log(" Ceci est le test de la roche 2");
            kiriePense.text = "Je ne peux pas partir tout de suite! J'ai besoin de mon journal.";
        }

        if(gameObject.name == "cle")
        {
            kiriePense.text = "La clé est là!";
        }

        if(gameObject.name == "MaisonKirie")
        {
            kiriePense.text = "Je dois retrouver mon frère!";
        }

        if(gameObject.name == "Villageois2" || gameObject.name == "villageois")
        {
            kiriePense.text = "Peut-être qu'il peut m'aider?";
        }

        if(gameObject.name == "hover_sauvegarde")
        {
            kiriePense.text = "Si je me perds, je pourrai revenir ici.";
        }

        if(gameObject.name == "HoverGatito")
        {
            kiriePense.text = "Qui est-ce?";
        }

        //If your mouse hovers over the GameObject with the script attached, output this message
        //Debug.Log("Mouse is over GameObject.");

        //mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition); //Gets mouse position
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        //Debug.Log("Mouse is no longer on GameObject.");
        kiriePense.text = "";
    }
}
