using System.Collections;
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

    [Header("Variables pour animation des lettres")]
    float delaiEntreLettres = 0.05f;
    bool ecrit = false;

    void Start()
    {
        //bulleKirie.SetActive(false);
        //bulleThays.SetActive(false);

        //Le dialogue Choix Diplomate

        //Choix diplomate  de Kirie
        choixDiplomateKirie[0] = "Thays ! Enfin je te trouve. Je suis venue libérer mon frère et tous les enfants que tu as enlevés pour ton bon plaisir !";
        choixDiplomateKirie[1] = "Oh…. Un certain détective m’a parlé de cette entrée de cave. La lettre que j’ai en ma possession est la preuve de tes méfaits (Un mensonge pas si loin de la vérité)";
        choixDiplomateKirie[2] = "Donc, c’était vrai ! Il travaille pour vous…. (Moi qui lui faisais confiance) Peu importe. Rends-moi mon frère Aasha ! Je ne repartirai pas sans lui !";
        choixDiplomateKirie[3] = "C’est moi sa seule famille ! Pourquoi fais-tu tout cela Thays ? Tu sembles malheureuse…";
        choixDiplomateKirie[4] = "Il n’est pas trop tard ! Relâche tous les enfants et viens avec nous.";
        choixDiplomateKirie[5] = "Je n’étais pas présente lors de ton bannissement, mais je te promets sur la tête d’Aasha que je ferais tout pour qu’on te pardonne. FORMONS UNE NOUVELLE FAMILLE ENSEMBLE !";


        //Choix diplomate  de Thays
        choixDiplomateThays[0] = "Comment es-tu rentrée chez moi ? Personne n’a trouvé ma cachette ! Impossible ! (Devrais-je déménager ?) Je ne sais pas de quoi tu parles. Aucun enfant ne vit ici contre son gré. Pour qui me prenez-vous ?";
        choixDiplomateThays[1] = "Argh ! Gatito, ce petit chat de gouttière qui n’en fait qu’à sa tête ! La prochaine fois, il va le regretter quand j’aurais sa langue dans mes mains HAHAH ! ";
        choixDiplomateThays[2] = "Aasha… Il m’appartient désormais : Il s’est jeté dans la gueule du loup ! Haha ! Ils m’appartiennent tous. C’est la famille que je n’ai jamais eue ! ";
        choixDiplomateThays[3] = "On m’a tout pris, les tiens m’ont abandonnée. D’une enfance joyeuse à une vie recluse. ILS DOIVENT COMPRENDRE MA DOULEUR DEPUIS 40 ANS ! ";
        choixDiplomateThays[4] = "Vraiment !?! Hors de question !";
        choixDiplomateThays[5] = "Si tu le dis. Gare à toi si tu me mens. Rusée comme un renard, il faut se méfier….";
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
        lesOptions[0].GetComponent<Button>().interactable = false; // Désactive le composant Button du bouton
        lesOptions[1].SetActive(false);
        StartCoroutine(CommencerConversation());
    }

    public void ChoixColere()
    {
        choixColere = true;
        lesOptions[1].GetComponent<Button>().interactable = false; // Désactive le composant Button du bouton
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
