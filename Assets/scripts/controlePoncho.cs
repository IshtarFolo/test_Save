using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlePoncho : MonoBehaviour
{
    /*------------
     * VARIABLES *
     ------------*/
    public GameObject poncho; // Reference au poncho
    public GameObject kirie; // Reference a Kirie
    float speed = 1.0f; // Vitesse de suivi du poncho (pas de delai)
    float ponchoY; // La positionY du poncho par rapport a Kirie

    void Update()
    {
        // le poncho suit kirie peu importe sa position
        poncho.transform.position = Vector3.Lerp(poncho.transform.position, kirie.transform.position, Time.deltaTime * speed);

        // si Kirie fait face a une direction ou une autre
        if (kirie.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleDroite"))
        {
            ponchoY = 2.57f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.4f);
        }
        else
        {
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.2f);
        }

        // Si Kirie saute
        if (kirie.GetComponent<Animator>().GetBool("saute") == true && kirie.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleDroite"))
        {
            ponchoY = kirie.transform.position.y + 2.52f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.4f);
        }
        else if (kirie.GetComponent<Animator>().GetBool("saute") == true && kirie.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleGauche"))
        {
            ponchoY = kirie.transform.position.y + 2.52f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.2f);
        }
        else
        {
            ponchoY = 2.57f;
        }
    }
}
