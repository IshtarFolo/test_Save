using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionVillageois : MonoBehaviour
{
    public TextMeshProUGUI lettreE;
    public bool veutParler = false;

    public TextMeshPro dialogueVillageois; // Référence au texte du villageois actuel
    public GameObject bulle; // Référence à la bulle de dialogue
    private int DialogueActuelleIndex = 0; // Index du dialogue actuel

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
            dialogueVillageois.enabled = false;
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
        dialogueVillageois.text = "";
        bulle.SetActive(true);

        foreach (char lettre in dialogue)
        {
            dialogueVillageois.text += lettre;
            yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
        }

        DialogueActuelleIndex++;
        // Ajuster dynamiquement la taille du texte et de la boîte de dialogue
        AdjustTextSize(dialogue);
        AdjustDialogueBoxSize(dialogue);
    }

    // Méthode pour ajuster la taille du texte en fonction de la longueur du dialogue
    private void AdjustTextSize(string dialogue)
    {
        float baseFontSize = 1f; // Taille de police de base
        float scaleFactor = 1f; // Facteur d'échelle initial
        if (dialogue.Length > 50)
        {
            scaleFactor = 50f / dialogue.Length; // Ajuster le facteur d'échelle en fonction de la longueur du dialogue
        }
        dialogueVillageois.fontSize = baseFontSize * scaleFactor; // Ajuster la taille de la police
    }

    // Méthode pour ajuster la taille de la boîte de dialogue en fonction de la longueur du dialogue
    public void AdjustDialogueBoxSize(string dialogue)
    {
        // Calculer la taille du texte
        Vector2 textSize = dialogueVillageois.GetPreferredValues(dialogue);
        // Ajuster la taille de la boîte de dialogue en fonction de la taille du texte
        Vector2 boxSize = new Vector2(textSize.x + 20f, textSize.y + 20f); // Ajouter un padding
        bulle.GetComponent<RectTransform>().sizeDelta = boxSize;
    }
}
