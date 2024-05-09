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
        if (veutParler && Input.GetKeyDown(KeyCode.E) && !aParle)
        {
            if (dialogueCoroutine == null)
            {
                dialogueCoroutine = StartCoroutine(AfficherDialoguesAuto());
            }
        }

        //Debug.Log(DialogueActuelleIndex);
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
    IEnumerator AfficherDialoguesAuto()
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
        }

        // Si tous les dialogues ont été affichés, désactiver le dialogue
        dialogueVillageois.enabled = false;
        bulle.SetActive(false);
        lettreE.SetActive(false);
        veutParler = false;
        dialoguesTermines = true; // Marquer que tous les dialogues ont été affichés
        aParle = true; // Marquer que le joueur a parlé au villageois
    }

}
