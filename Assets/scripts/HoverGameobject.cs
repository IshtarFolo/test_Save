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
        if(gameObject.name == "Test_roche_1")
        {
            //Debug.Log(" Ceci est le test de la roche 1");
            kiriePense.text = "Cette roche a l'air suspecte...";
        }

        if (gameObject.name == "Test_roche_2")
        {
            //Debug.Log(" Ceci est le test de la roche 2");
            kiriePense.text = "Mais c'est une jolie roche ça!";
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
