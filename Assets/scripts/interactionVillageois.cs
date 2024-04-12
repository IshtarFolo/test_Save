using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionVillageois : MonoBehaviour
{
    public TextMeshProUGUI lettreE;
    public bool veutParler = false;

    public TextMeshProUGUI dialogueVilleagois; // Référence au texte du villageois actuel
    public GameObject bulle; // Référence à la bulle de dialogue
    private int DialogueActuelleIndex = 0; // Index du dialogue actuel

    public string[] dialogues; // Tableau de textes pour les dialogues

    private Coroutine dialogueCoroutine; // Coroutine pour afficher les dialogues lettre par lettre

    // Start is called before the first frame update
    void Start()
    {
        dialogueVilleagois.enabled = true;
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
            }
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
            dialogueVilleagois.enabled = false;
            bulle.SetActive(false);
            lettreE.enabled = false;
            veutParler = false;
            // Réinitialiser l'index du dialogue
            DialogueActuelleIndex = 0;
        }
    }

    // Coroutine pour afficher les dialogues lettre par lettre
    IEnumerator AfficherDialogueCoroutine(string dialogue)
    {
        dialogueVilleagois.text = "";
        bulle.SetActive(true);

        foreach (char lettre in dialogue)
        {
            dialogueVilleagois.text += lettre;
            yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
        }
            DialogueActuelleIndex++;
    }
}
