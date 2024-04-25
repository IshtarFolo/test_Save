using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffetVent : MonoBehaviour
{
    Rigidbody rb;
    public WindZone windZone;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (windZone != null)
        {
            // Récupérer la direction et la force du vent de la Wind Zone
            Vector3 windDirection = windZone.transform.forward;
            float windStrength = windZone.windMain;

            // Appliquer la force du vent au Rigidbody de l'arbre
            rb.AddForce(windDirection * windStrength, ForceMode.Force);
        }
    }
}
