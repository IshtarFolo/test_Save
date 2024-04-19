using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionDialogue : MonoBehaviour
{
    // Variable pour la cible vers laquelle s'orienter
    public Transform laCible;

    // Référence au texte associé à la bulle de dialogue
    public GameObject texteDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotation vers la cible
        transform.LookAt(laCible.position);
        // Faire en sorte que le texte reste face au joueur en gardant la même rotation que la caméra
        texteDialogue.transform.rotation = Quaternion.LookRotation(texteDialogue.transform.position - Camera.main.transform.position);
    }
}
