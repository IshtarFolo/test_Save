using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueGatitoVillage : MonoBehaviour
{
    public TextMeshProUGUI lettreE;
    public bool veutParler = false;
    public bool peutActiverAction = false; // Indique si Gatito peut activer l'action du joueur

    public TextMeshPro dialogueGatito; // Référence au texte de Gatito
    public GameObject bulle; // Référence à la bulle de dialogue
    private int dialogueActuelIndex = 0; // Index du dialogue actuel

    public bool aParle = false; // Variable pour vérifier si le joueur a parlé
    public bool dialoguesTermines = false; // Variable pour vérifier si tous les dialogues ont été affichés

    public string[] dialoguesSansAction; // Dialogues lorsque l'action préalable n'a pas été effectuée
    public string[] dialoguesAvecAction; // Dialogues lorsque l'action préalable a été effectuée

    private Coroutine dialogueCoroutine; // Coroutine pour afficher les dialogues lettre par lettre

    public interactionVillageois scriptVillageois;

    // Start is called before the first frame update
    void Start()
    {
        dialogueGatito.enabled = true;
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

                if (peutActiverAction) // Si Gatito peut activer l'action du joueur
                {
                    ExecuteActionDuJoueur();
                }
                else // Sinon, gérer le dialogue avec Gatito normalement
                {
                    if (aParle) // Si le joueur a déjà parlé, afficher les dialogues avec action préalable
                    {
                        AfficherDialogueSuivant(dialoguesAvecAction);
                    }
                    else // Sinon, afficher les dialogues sans action préalable
                    {
                        AfficherDialogueSuivant(dialoguesSansAction);
                    }
                }
            }
        }
    }

    // Méthode pour exécuter l'action spécifique du joueur
    private void ExecuteActionDuJoueur()
    {
        // Code pour activer l'action spécifique du joueur
        Debug.Log("Action spécifique du joueur activée !");
    }

    // Méthode pour afficher le dialogue suivant
    public void AfficherDialogueSuivant(string[] dialogues)
    {
        scriptVillageois.AfficherDialogueSuivant();
    }

    // Coroutine pour afficher les dialogues lettre par lettre
    IEnumerator AfficherDialogueCoroutine(string dialogue)
    {
        dialogueGatito.text = "";
        bulle.SetActive(true);

        foreach (char lettre in dialogue)
        {
            dialogueGatito.text += lettre;
            yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
        }

        dialogueActuelIndex++;
    }

    // Méthode pour réinitialiser le dialogue
    public void RecommencerDialogue()
    {
        scriptVillageois.RecommencerDialogue();
    }
}
