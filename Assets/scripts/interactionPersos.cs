using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionPersos : MonoBehaviour
{
    public TextMeshProUGUI lettreE;
    public bool veutParler = false;

    public TextMeshProUGUI dialogueVilleagois; // Référence au texte du villageois actuel
    private int currentDialogueIndex = 0; // Index du dialogue actuel

    public string[] dialogues; // Tableau de textes pour les dialogues

    // Start is called before the first frame update
    void Start()
    {
        dialogueVilleagois = dialogueVilleagois.GetComponent<TextMeshProUGUI>();
        if (dialogues.Length > 0)
            dialogueVilleagois.text = dialogues[currentDialogueIndex];
            dialogueVilleagois.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Afficher le prochain dialogue s'il y en a un
                if (currentDialogueIndex < dialogues.Length - 1)
                {
                    currentDialogueIndex++;
                    dialogueVilleagois.text = dialogues[currentDialogueIndex];
                    dialogueVilleagois.enabled = true;
                }
                else
                {
                    // Cacher le texte s'il n'y a plus de dialogues
                    dialogueVilleagois.text = "";
                    veutParler = false;
                    lettreE.enabled = false;
                    dialogueVilleagois.enabled = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        lettreE.enabled = true;
        veutParler = true;

    }

    private void OnTriggerExit(Collider other)
    {
        lettreE.enabled = false;
        veutParler = false;
        dialogueVilleagois.enabled = false;
    }
}
