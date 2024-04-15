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

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // le poncho suit la rotation de kirie 
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

        // mouvement lateral sur les X
        if (kirie.GetComponent<Animator>().GetFloat("VelocityX") > 0)
        {
            ponchoY = kirie.transform.position.y + 1.8f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.1f);
        }
        else if (kirie.GetComponent<Animator>().GetFloat("VelocityX") < 0)
        {
            ponchoY = kirie.transform.position.y + 1.8f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.5f);
        }
        else if (kirie.GetComponent<Animator>().GetFloat("VelocityX") > 0 && kirie.GetComponent<Animator>().GetFloat("VelocityZ") > 0)
        {
            ponchoY = kirie.transform.position.y + 1.8f;
            poncho.transform.position = new Vector3(poncho.transform.position.x, ponchoY, kirie.transform.position.z - 0.1f);
            Quaternion rotation = Quaternion.Euler(0, 50, 0);
            transform.rotation = rotation;
        }
    }
}
