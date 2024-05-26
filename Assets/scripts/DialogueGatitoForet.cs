using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueGatitoForet : MonoBehaviour
{
    public GameObject lettreE;
    public bool veutParler = false;
    public static bool poissonMange = false; // Indique si Gatito peut activer l'action du joueur

    public TextMeshPro dialogueGatito; // Référence au texte de Gatito
    public GameObject bulle; // Référence à la bulle de dialogue

    public bool aParle = false; // Variable pour vérifier si le joueur a parlé
    public bool dialoguesTermines = false; // Variable pour vérifier si tous les dialogues ont été affichés

    public string[] dialoguesForet; // Dialogues de Gatito

    private Coroutine dialogueCoroutine; // Coroutine pour afficher les dialogues lettre par lettre


    // Start is called before the first frame update
    void Start()
    {
        dialogueGatito.enabled = true;
        bulle.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (veutParler && (Input.GetKeyDown(KeyCode.E)) && !aParle)
        {
            if (dialogueCoroutine == null)
            {
                    dialogueCoroutine = StartCoroutine(AfficherDialoguesAuto(dialoguesForet));
            }
        }
    }

    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "Player" && !aParle)
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


    // Méthode coroutine pour faire défiler le dialogue automatiquement
    public IEnumerator AfficherDialoguesAuto(string[] dialogues)
    {
        // Désactiver la lettre E après avoir appuyé une seule fois pour démarrer la conversation
        lettreE.SetActive(false);

        foreach (string dialogue in dialogues)
        {
            dialogueGatito.text = "";
            bulle.SetActive(true);

            foreach (char lettre in dialogue)
            {
                dialogueGatito.text += lettre;
                yield return new WaitForSeconds(0.10f); // Attendre un court laps de temps entre chaque lettre
            }

            yield return new WaitForSeconds(1f); // Attendre un court laps de temps avant d'afficher le prochain dialogue
        }

        // Si tous les dialogues ont été affichés, désactiver le dialogue
        dialogueGatito.enabled = false;
        bulle.SetActive(false);
        lettreE.SetActive(false);
        veutParler = false;
        dialoguesTermines = true; // Marquer que tous les dialogues ont été affichés
        aParle = true;
    }

}
