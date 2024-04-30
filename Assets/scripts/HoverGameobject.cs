using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverGameobject : MonoBehaviour
{
    public Vector3 mousePos; //To store mouse position

    public Transform incrementElement; //The UI element I'm instantiating
    public Transform parentObj; //The UI Canvas

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");

        //mousePos = Camera.main.WorldToScreenPoint(Input.mousePosition); //Gets mouse position
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
