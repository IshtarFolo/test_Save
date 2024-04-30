using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDialogue : MonoBehaviour
{
    // Variable pour la cible vers laquelle s'orienter
    public Transform laCible;

    // Référence au texte associé à la bulle de dialogue
    public GameObject texteDialogue;

    //
    public GameObject bulleDialogue;

    public Vector3 offset = new Vector3(0, 2, 0);

    // Update is called once per frame
    void Update()
    {
        // Rotation vers la cible
        transform.position = laCible.position + offset;

        // Faire en sorte que le texte reste face au joueur en gardant la même rotation que la caméra
        texteDialogue.transform.rotation = Quaternion.LookRotation(texteDialogue.transform.position - Camera.main.transform.position);

        bulleDialogue.transform.rotation = Quaternion.LookRotation(bulleDialogue.transform.position - Camera.main.transform.position);
    }
}