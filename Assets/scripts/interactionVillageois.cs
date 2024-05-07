using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class interactionVillageois : MonoBehaviour
{
    public GameObject lettreE;
    public bool veutParler = false;

    public TextMeshPro dialogueVillageois; // Référence au texte du villageois actuel
    public GameObject bulle; // Référence à la bulle de dialogue
    public static int DialogueActuelleIndex = 0; // Index du dialogue actuel

    public bool aParle = false; // Variable pour vérifier si le joueur a parlé
    public bool dialoguesTermines = false; // Variable pour vérifier si tous les dialogues ont été affichés


    public string[] dialogues; // Tableau de textes pour les dialogues

    private Coroutine dialogueCoroutine; // Coroutine pour afficher les dialogues lettre par lettre


    // Start is called before the first frame update
    void Start()
    {
        dialogueVillageois.enabled = true;
        bulle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (dialogueCoroutine != null)
                {
                    StopCoroutine(dialogueCoroutine);
                }

                AfficherDialogueSuivant();
                // Si tous les dialogues ont été affichés, marquer que le joueur a parlé au villageois
                if (dialoguesTermines)
                {
                    aParle = true;
                }
            }
        }

        //Debug.Log(DialogueActuelleIndex);
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if(infoCollision.gameObject.tag == "Player")
        {
        lettreE.SetActive(true);
        veutParler = true;

        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player")
        {
            lettreE.SetActive(false);
            veutParler = false;

        }

    }

    // Méthode pour afficher le dialogue suivant
    public void AfficherDialogueSuivant()
    {
        if (DialogueActuelleIndex < dialogues.Length)
        {
            // Vérifier si la coroutine est déjà en cours d'exécution
            if (dialogueCoroutine != null)
            {
                StopCoroutine(dialogueCoroutine);
            }
                dialogueCoroutine = StartCoroutine(AfficherDialogueCoroutine(dialogues[DialogueActuelleIndex]));
        }
        else
        {
            // Si tous les dialogues ont été affichés, désactiver le dialogue
            dialogueVillageois.enabled = false;
            bulle.SetActive(false);
            lettreE.SetActive(false);
            veutParler = false;
            dialoguesTermines = true; // Marquer que tous les dialogues ont été affichés
            interactionPerso.villageois.GetComponent<BoxCollider>().enabled = false;
        }
    }

    // Coroutine pour afficher les dialogues lettre par lettre
    IEnumerator AfficherDialogueCoroutine(string dialogue)
    {
        dialogueVillageois.text = "";
        bulle.SetActive(true);

        foreach (char lettre in dialogue)
        {
            dialogueVillageois.text += lettre;
            yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
        }

        DialogueActuelleIndex++;
    }
    
    //Moyen pour que le dialogue recommence après avoir fini son itération
    public void RecommencerDialogue()
    {
        DialogueActuelleIndex = 0;
        dialogueVillageois.enabled = true;
        bulle.SetActive(true);
        dialoguesTermines = false;
        AfficherDialogueSuivant();
    }

}
