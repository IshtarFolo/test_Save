using System.Collections;
using UnityEngine;

public class CannePeche : MonoBehaviour
{
    // Déclaration des variables
    // Des animations pour chaque état sont disponibles **À AJOUTER**
    public bool estEquipe;
    public bool peutPecher;
    public bool estLance;
    public bool tire;

    // Pour les animations de la canne 
    Animator animator;
    public GameObject appatPrefab;
    public GameObject finCorde;
    public GameObject debutCorde;
    public GameObject debutCanne;

    Transform positionAppat;

    public void Start()
    {
        // Récupérer l'animator de la canne peche
        animator = GetComponent<Animator>();
        estEquipe = true;
        // Always set peutPecher to true at start
        peutPecher = true;
        // Start fishing automatically
        StartCoroutine(StartFishing());
    }

    // Coroutine pour débuter la peche après un court délai
    IEnumerator StartFishing()
    {
        //Attendre 1 seconde avant de lancer la canne a peche
        yield return new WaitForSeconds(1f);

        //Commencer la peche automatiquement lorsque les conditions sont atteintes
        if (estEquipe && peutPecher && !estLance && !tire)
        {
            Debug.Log("Je peche");
            StartCoroutine(CastRod(transform.position));
        }
    }

    void Update()
    {
        //Tirer la canne a peche sur le clic droit de la souris
        if (estLance && Input.GetMouseButtonDown(1))
        {
            PullRod();
        }
    }

    // Coroutine pour lancer la canne 
    IEnumerator CastRod(Vector3 targetPosition)
    {
        estLance = true;
        animator.SetTrigger("LancerCanne");
        yield return new WaitForSeconds(1f);

        GameObject instanceAppat = Instantiate(appatPrefab);
        instanceAppat.transform.position = targetPosition;

        positionAppat = instanceAppat.transform;

        // Start Fish Bite Logic
    }

    // Methode pour tirer la canne peche
    private void PullRod()
    {
        animator.SetTrigger("TirerCanne");
        Debug.Log("je tire");
        estLance = false;
        tire = true;

        // Start Minigame Logic
    }
}
