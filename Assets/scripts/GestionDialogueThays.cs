using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionDialogueThays : MonoBehaviour
{
    [Header("Les différents dialogues des personnages selon l'option choisi")]
    [SerializeField]
    private string[] choixDiplomateKirie;
    [SerializeField]
    private string[] choixDiplomateThays;
    [SerializeField]
    private string[] choixColereKirie;
    [SerializeField]
    private string[] choixColereThays;

    [Header("Choix proposés")]
    public GameObject[] lesOptions;

    [Header("Les dialogues des personnages")]
    public TextMeshProUGUI dialogueKirie;
    public TextMeshProUGUI dialogueThays;

    [Header("Les bulles des dialogues des personnages")]
    public GameObject bulleKirie;
    public GameObject bulleThays;

    [Header("booleen des type des choix")]
    public bool choixDiplomate = false;
    public bool choixColere = false;

    [Header("l'index du dialogue actuelle")]
    int ligneDialogueActuelle = 0;

    float delaiEntreLettres = 0.10f;
    bool ecrit = false;

    void Start()
    {
        //bulleKirie.SetActive(false);
        //bulleThays.SetActive(false);
       
    }


   IEnumerator CommencerConversation()
    {
        if (choixDiplomate)
        {
            // Afficher les dialogues diplomatiques
            for (int i = 0; i < choixDiplomateKirie.Length; i++)
            {
                dialogueKirie.text = "";
                dialogueThays.text = "";
                ecrit = true;
                //WaitUntil - Pour attendre que la variable devienne false pour continuer la conversation
                StartCoroutine(AfficherDialogueParLettre(dialogueKirie, choixDiplomateKirie[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(2f);
                ecrit = true;
                StartCoroutine(AfficherDialogueParLettre(dialogueThays, choixDiplomateThays[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(2f);
            }
        }
        else if (choixColere)
        {
            // Afficher les dialogues colériques
            for (int i = 0; i < choixColereKirie.Length; i++)
            {
                dialogueKirie.text = "";
                dialogueThays.text = "";
                ecrit = true;
                //WaitUntil - Pour attendre que la variable devienne false pour continuer la conversation
                StartCoroutine(AfficherDialogueParLettre(dialogueKirie, choixColereKirie[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(2f);
                ecrit = true;
                StartCoroutine(AfficherDialogueParLettre(dialogueThays, choixColereThays[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void ChoixDiplomate()
    {
        choixDiplomate = true;
        lesOptions[1].SetActive(false);
        StartCoroutine(CommencerConversation());
    }

    public void ChoixColere()
    {
        choixColere = true;
        lesOptions[0].SetActive(false);
        StartCoroutine(CommencerConversation());
    }

    IEnumerator AfficherDialogueParLettre(TextMeshProUGUI textMeshPro, string dialogue)
    {
        foreach (char letter in dialogue)
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(delaiEntreLettres);
        }
        ecrit = false;
    }

}
