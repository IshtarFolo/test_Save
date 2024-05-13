using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class interactionVillageois : MonoBehaviour
{
    public GameObject lettreE;
    public bool veutParler = false;

    public TextMeshPro dialogueVillageois; // Référence au texte du villageois actuel
    public GameObject bulle; // Référence à la bulle de dialogue

    public bool aParle = false; // Variable pour vérifier si le joueur a parlé
    public static bool aParleVillageois1 = false;
    public bool dialoguesTermines = false; // Variable pour vérifier si tous les dialogues ont été affichés

    public string[] dialogues; // Tableau de textes pour les dialogues

    private Coroutine dialogueCoroutine; // Coroutine pour afficher les dialogues lettre par lettre

    public GameObject Kirie; // Reference a Kirie

    NavMeshAgent agent; // Variable raccourcis NavMesh


    // Start is called before the first frame update
    void Start()
    {
        dialogueVillageois.enabled = true;
        bulle.SetActive(true);

        // Raccourcis NavMesh
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler && Input.GetKeyDown(KeyCode.E) && !aParle)
        {
            if (dialogueCoroutine == null)
            {
                dialogueCoroutine = StartCoroutine(AfficherDialoguesAuto());
                // Le villageois s'arrete
                this.agent.isStopped = true;
                // Le personnage regarde le joueur
                StartCoroutine(RotationVersJoueur());
                // On relance le mouvement du villageois
                StartCoroutine(ArretDuVillageois());
            }
        }
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if(infoCollision.gameObject.tag == "Player" && !aParle)
        {
            lettreE.SetActive(true);
            veutParler = true;
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player" && !aParle)
        {
            lettreE.SetActive(false);
            veutParler = false;
        }
    }

    //Méthode coroutine pour faire défiler le dialogue automatiquement
    public IEnumerator AfficherDialoguesAuto()
    {
        //Désactiver la lettre E après avoir appuyé une seule fois pour démarrer la conversation
        lettreE.SetActive(false);

        foreach (string dialogue in dialogues)
        {
            dialogueVillageois.text = "";
            bulle.SetActive(true);


            foreach (char lettre in dialogue)
            {
                dialogueVillageois.text += lettre;
                yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
            }

            yield return new WaitForSeconds(0.5f); // Attendre un court laps de temps avant d'afficher le prochain dialogue
            //DialogueActuelleIndex++;
        }

        // Si tous les dialogues ont été affichés, désactiver le dialogue
        dialogueVillageois.enabled = false;
        bulle.SetActive(false);
        lettreE.SetActive(false);
        veutParler = false;
        dialoguesTermines = true; // Marquer que tous les dialogues ont été affichés

        if (gameObject.CompareTag("villageois1") && dialoguesTermines)
        {
            aParleVillageois1 = true;
            //Debug.Log("Test Villageois");
        }

        aParle = true; // Marquer que le joueur a parlé au villageois
    }

    private IEnumerator ArretDuVillageois()
    {
        // Le villageois reprend son deplacement apres un certain temps
        yield return new WaitForSeconds(20f);
        this.agent.isStopped = false;
    }

    IEnumerator RotationVersJoueur()
    {
        while (true)
        {
            // Le villageois regarde dans la direction du joueur
            Vector3 direction = (Kirie.transform.position - this.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            this.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);

            // Si le personnage fait presque face au joueur on arete la coroutine
            if (Quaternion.Angle(this.transform.rotation, lookRotation) < 5f)
            {
                break;
            }
            yield return null;
        }
}

}
