using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script de gestion UI pour le mini jeu d'attrape poisson - Par Camilia El Moustarih
public class MouvementPoissonUI : MonoBehaviour
{
    //Variables pour la barre UI pour attraper le poisson
    //Limites du sprite de poisson sur la barre UI
    public float maxGaucheUI = -250f;
    public float maxDroiteUI = 250f;

    //Gestion de la vitesse du poisson sur la barre UI
    public float vitessePoisson = 250f;
    public float changerFrequence = 0.005f; //le poisson changera son mouvement à cette fréquence

    //Gestion de la position du poisson
    public float poissonPosition;
    public bool mouvementGaucheDroite = true;

    // Start is called before the first frame update
    void Start()
    {
        //Lui donner une position aléatoire entre le haut et le bas de la barre
        poissonPosition = Random.Range(maxGaucheUI, maxDroiteUI);
    }

    // Update is called once per frame
    void Update()
    {
        //Bouger le sprite du poisson à sa position aléatoire générée
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(poissonPosition, transform.localPosition.y, transform.localPosition.z), vitessePoisson * Time.deltaTime);

        //Vérifier si le sprite du poisson a atteint la position qui lui a été générée
        if (Mathf.Approximately(transform.localPosition.x, poissonPosition))
        {
            //Générer une nouvelle position
            poissonPosition = Random.Range(maxGaucheUI, maxDroiteUI);
        }

        //Changer de direction aléatoirement pour plus de challenge!
        if (Random.value < changerFrequence)
        {
            mouvementGaucheDroite = !mouvementGaucheDroite;
            poissonPosition = mouvementGaucheDroite ? maxDroiteUI : maxGaucheUI;
        }
    }
}
