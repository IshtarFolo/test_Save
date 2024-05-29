using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine;

public class camera : MonoBehaviour
{
    /*
     * Gestion de la camera avec cinemachine par Xavier Arbour:
     * 
     * Connexion de la camera "Main" avec le addOn Cinemachine. 
     */
    public GameObject joueur;//Pour trouver le joueur et, eventuellement, ajuster les positions de la camera
    public CinemachineVirtualCamera cam1;//Variable de la camera virtuelle
    private CinemachineComponentBase composants;//Variable pour le "Body" de la camera virtuelle

    private void Update()
    {
        //Si la variable composants est associee au "transposer" de la camera virtuelle 
        if (composants is CinemachineFramingTransposer)
        {
            //On cree une variable qui associe la variable composants au "transposer"
            var framingTransposer = composants as CinemachineFramingTransposer;
        }
    }
}
