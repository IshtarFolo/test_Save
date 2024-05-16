using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasseDesPartiesLettres : MonoBehaviour
{
    [Header("La position du joueur")]
    public Transform kirie;
    [Header("Distance où la le morceau de lettre court")]
    public float distanceAlerte = 5f;
    [Header("Nombre maximum de fuites")]
    public int fuiteMax = 3;
    [Header("distance de déplacement par rapport au joueur")]
    public float distanceDeplacement = 15f;
    [Header("Nombre de fuites restantes")]
    public int fuitesRestantes;
    [Header("Position d'origine de la lettre")]
    private Vector3 originalPosition;

    [Header("Conditioni si la lettre est en train de bouger ou pas")]
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        fuitesRestantes = fuiteMax;
    }

    // Update is called once per frame
    void Update()
    {
        // Calcul de la distance entre la lettre et le joueur
        // Déplacement aléatoire
        float distanceJoueurEtLettre = Vector3.Distance(transform.position, kirie.position);

        // Si le joueur est à une certaine distance de la lettre
        if (distanceJoueurEtLettre <= distanceAlerte && fuitesRestantes > 0 && !isMoving)
        {
            // Déplacement aléatoire
            StartCoroutine(Fuite());
        }

    }

    IEnumerator Fuite()
    {
        isMoving = true;

        // Animation de fuite en translation
        for (int i = 0; i < 20 && fuitesRestantes > 0; i++) // Répéter l'animation 20 fois (vous pouvez ajuster ce nombre selon vos besoins)
        {
            // Génère une direction aléatoire pour la fuite
            Vector3 directionAleatoire = originalPosition + new Vector3(Random.Range(-distanceDeplacement, distanceDeplacement),
                                                                    0f,
                                                                    Random.Range(-distanceDeplacement, distanceDeplacement));

            // Enregistre le temps de début du déplacement
            float DebutDeplacement = Time.time;

            // Calcule la distance totale à parcourir pour le déplacement
            float fuiteTotale = Vector3.Distance(transform.position, directionAleatoire);

            // Tant que la durée du déplacement n'a pas atteint 1 seconde
            while (Time.time - DebutDeplacement < 1f) // Durée du déplacement (1 seconde)
            {
                // Calcule la distance couverte jusqu'à présent
                float distanceParcourie= (Time.time - DebutDeplacement) * 1f; // Vitesse du déplacement (1 unité par seconde)

                // Calcule la fraction du déplacement parcourue jusqu'à présent
                float fuiteActuelle = distanceParcourie / fuiteTotale;

                // Déplace progressivement la lettre vers la direction aléatoire
                transform.position = Vector3.Lerp(transform.position, directionAleatoire, fuiteActuelle);

                yield return null; // Attend la prochaine frame
            }
            // Décrémente le nombre de fuites restantes
            fuitesRestantes--; 
            yield return new WaitForSeconds(1.5f); // Attente de 3 secondes entre chaque déplacement
        }

        
        isMoving = false;
    }


}
