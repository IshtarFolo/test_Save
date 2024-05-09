using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script pour le mini-jeu de pêche géré par des barres UI - Par Camilia El Moustarih
public class MiniJeuPeche : MonoBehaviour
{
    //Variables pour les barres ui images
    public RectTransform poissonTransform;
    public RectTransform attrapePoissonTransform;

    //Vérifier s'il y a chevauchement
    public bool siOverlap;

    //Variables pour les calculs du mini-jeu
    public Slider reussiteSlider; //montrer au joueur s'il est sur le point de réussir ou de perdre
    float reussiteIncrement = 15; //tant que le poisson et l'attrape poisson sont en overlap, incrémenter les chances de réussites de 15
    float echecIncrement = 12; //s'il n'y PAS overlap, décrémenter de 12 les chances de réussite
    float reussiteLimite = 100; //le joueur gagne le mini jeu
    float echecLimite = -100; //le joueur perd, bye
    float compteurReussite = 0; //compteur qui va déterminer si gagne ou perd



    public Animator animatorPoisson; 
      
    
    //Pour calculer le chevauchement entre les deux éléments du UI (le poisson et l'attrape poisson)
    private void Update()
    {
        if (TesterOverlap(poissonTransform, attrapePoissonTransform))
        {
            siOverlap = true;
        }
        else
        {
            siOverlap = false;
        }

        //Pour le calcul de la victoire ou l'échec du mini-jeu
        OverlapCalcul();
    }

    private void OverlapCalcul()
    {
        if (siOverlap)
        {
            //incrémenter le compteur lorsqu'il y a overlap
            compteurReussite += reussiteIncrement * Time.deltaTime;
        }
        else
        {
            //décrémenter le compteur lorsqu'il n'y PAS de overlap
            compteurReussite -= echecIncrement * Time.deltaTime;
        }

        //Avec limites
        compteurReussite = Mathf.Clamp(compteurReussite, echecLimite, reussiteLimite);

        //Mettre à jour le slider
        reussiteSlider.value = compteurReussite;

        //Vérifier si les limites sont atteintes
        if (compteurReussite >= reussiteLimite)
        {
            Debug.Log("Bravo! 1 poisson ajouté à l'inventaire !");

          

            //Jouer une animation d'un poisson sorti de l'eau
            animatorPoisson.SetTrigger("PoissonSorti");

            /////////////////////////Ajouter le poisson à l'inventaire!/////////////////////////////

            //Remettre le compteur à 0
            compteurReussite = 0;
            reussiteSlider.value = 0;

            //Terminer le jeu
            SystemePeche.Instance.TerminerMiniJeu(true);

        }
        else if(compteurReussite <= echecLimite)
        {
            Debug.Log("Échec... Gatito commence à avoir faim là...");

           

            //Remettre le compteur à 0
            compteurReussite = 0;
            reussiteSlider.value = 0;

            //Terminer le jeu
            SystemePeche.Instance.TerminerMiniJeu(false);
        }
    }

    //Transforms et méthode overlaps pour vérifier le chevauchement et retournera vrai ou faux
    private bool TesterOverlap(RectTransform rect1, RectTransform rect2)
    {
        Rect r1 = new Rect(rect1.position.x, rect1.position.y, rect1.rect.width / 5, rect1.rect.height);
        Rect r2 = new Rect(rect2.position.x, rect2.position.y, rect2.rect.width / 5, rect2.rect.height);
        return r1.Overlaps(r2);
    }


}
