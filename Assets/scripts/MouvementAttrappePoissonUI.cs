using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouvementAttrappePoissonUI : MonoBehaviour
{

    //Variables pour la barre UI pour attraper le poisson
    //Limites de la barre rouge qui va attraper le poisson sur la barre UI
    public float maxGaucheUI = -250f;
    public float maxDroiteUI = 250f;

    //Gestion de la vitesse de la barre rouge Attrape Poisson sur la barre UI
    public float vitesseBarre = 250f;


    // Update is called once per frame
    void Update()
    {
        //Controller la barre attrape poisson à l'aide des flèches gauche et droite
        float moveInput = Input.GetAxis("Horizontal");

        //La barre rouge Attrape Poisson sera gérée par le joueur.
        //Éxécuter ce code lorsque le joueur manipule la barre rouge attrape poisson
        if (moveInput != 0)
        {
            MouvementBarre(moveInput);
        }
    }

    private void MouvementBarre(float moveInput)
    {
        Vector3 mouvementJoueur = Vector3.right * moveInput * vitesseBarre * Time.deltaTime;
        Vector3 nouvellePosition = transform.localPosition + mouvementJoueur;
        nouvellePosition.x = Mathf.Clamp(nouvellePosition.x, maxGaucheUI, maxDroiteUI);
        transform.localPosition = nouvellePosition;
    }
}
