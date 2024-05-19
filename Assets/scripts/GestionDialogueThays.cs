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

    // Déclaration de l'événement pour indiquer la fin des dialogues
    public event System.Action OnDialogueComplete;

    void Start()
    {
        bulleKirie.SetActive(false);
        bulleThays.SetActive(false);

        //Le dialogue Choix Diplomate

        //Choix diplomate de Kirie
        choixDiplomateKirie[0] = "Thays ! Enfin je te trouve. Je suis venue libérer mon frère et tous les enfants que tu as enlevés pour ton bon plaisir !";
        choixDiplomateKirie[1] = "Oh…. Un certain détective m’a parlé de cette entrée de cave. La lettre que j’ai en ma possession est la preuve de tes méfaits. (un mensonge pas si loin de la vérité)";
        choixDiplomateKirie[2] = "Donc, c’était vrai ! Il travaille pour vous…. (moi qui lui faisais confiance) Peu importe. Rends-moi mon frère Aasha ! Je ne repartirai pas sans lui !";
        choixDiplomateKirie[3] = "C’est moi sa seule famille ! Pourquoi fais-tu tout cela Thays ? Tu sembles malheureuse…";
        choixDiplomateKirie[4] = "Il n’est pas trop tard ! Relâche tous les enfants et viens avec nous.";
        choixDiplomateKirie[5] = "Je n’étais pas présente lors de ton bannissement, mais je te promets sur la tête d’Aasha que je ferais tout pour qu’on te pardonne. FORMONS UNE NOUVELLE FAMILLE ENSEMBLE !";


        //Choix diplomate de Thays
        choixDiplomateThays[0] = "Comment es-tu rentrée chez moi ? Personne n’a trouvé ma cachette ! Impossible ! (devrais-je déménager ?) Je ne sais pas de quoi tu parles. Aucun enfant ne vit ici contre son gré. Pour qui me prenez-vous ?";
        choixDiplomateThays[1] = "Argh ! Gatito, ce petit chat de gouttière qui n’en fait qu’à sa tête ! La prochaine fois, il va le regretter quand j’aurais sa langue dans mes mains HAHAH ! ";
        choixDiplomateThays[2] = "Aasha… Il m’appartient désormais : il s’est jeté dans la gueule du loup ! Haha ! Ils m’appartiennent tous. C’est la famille que je n’ai jamais eue ! ";
        choixDiplomateThays[3] = "On m’a tout pris, les tiens m’ont abandonnée. D’une enfance joyeuse à une vie recluse. ILS DOIVENT COMPRENDRE MA DOULEUR DEPUIS 40 ANS ! ";
        choixDiplomateThays[4] = "Vraiment !?! Hors de question !";
        choixDiplomateThays[5] = "Si tu le dis. Gare à toi si tu me mens. Rusée comme un renard, il faut se méfier…";

        //Choix colère de Kirie
        choixColereKirie[0] = "THAYS ! REDONNE-MOI MON FRÈRE SUR-LE-CHAMPS !";
        choixColereKirie[1] = "Moi, j’ai osé ! Et je ne partirai pas sans mon frère. AASHA ! Où est-il ?";
        choixColereKirie[2] = "Oh… Un certain détective m’a parlé de cette entrée de cave. La lettre que j’ai en ma possession est la preuve de tes méfaits.";
        choixColereKirie[3] = "Peu importe tes petits jeux, Thays ! Rends-moi mon frère Aasha ! Je ne repartirai pas sans lui !";
        choixColereKirie[4] = "Tu n'as pas le droit de les retenir ici ! Si tu ne le libères pas maintenant, je te ferai regretter d’avoir croisé mon chemin ! Certaine que plusieurs parents voudront ta tête décapitée entre leurs mains…";
        choixColereKirie[5] = "Les rumeurs au village te concernaient... Cela ne justifie pas tes actes monstrueux ! Relâche-les ou je te ferai comprendre la vraie douleur.";


        //Choix colère de Thays
        choixColereThays[0] = "QUI A OSÉ PÉNÉTRER DANS MON ANTRE SANS PERMISSION ?!?";
        choixColereThays[1] = "SORS D’ICI ! Tu as peut-être trouvé ma cachette, mais tu ne reverras plus jamais la lumière du jour ! Personne n’a trouvé aucune piste menant jusqu’à moi.";
        choixColereThays[2] = "Argh ! Gatito, ce petit chat de gouttière qui n’en fait qu’à sa tête ! La prochaine fois, il va le regretter quand j’aurai sa langue entre mes mains. HAHAH !";
        choixColereThays[3] = "Aasha… Oui… JAMAIS ! Il m’appartient désormais : il s’est jeté dans la gueule du loup ! Haha ! Ils m’appartiennent tous. C’est la famille que je n’ai jamais eue !";
        choixColereThays[4] = "Une sale gamine ose me menacer !?! CES PARENTS M’ONT ACCUSÉ D'AVOIR MANIGANCÉ LA MORT DE MES PARENTS ! ILS DOIVENT COMPRENDRE MA DOULEUR DEPUIS 40 ANS !";
        choixColereThays[5] = "Tu crois me faire peur ? Je suis plus forte que tu ne le penses. Ils m'ont pris ma famille, alors j'ai créé la mienne. Mais très bien, viens, essaye donc de m'arrêter !";

        // Si l'événènement est invoke la fonction DialogueTermine est appelé
        //Connexion des deux éléments (+=)
        OnDialogueComplete += DialogueTermine;
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
                bulleKirie.SetActive(true);
                ecrit = true;
                //WaitUntil - Pour attendre que la variable devienne false pour continuer la conversation
                StartCoroutine(AfficherDialogueParLettre(dialogueKirie, choixDiplomateKirie[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(1.5f);
                bulleKirie.SetActive(false);
                bulleThays.SetActive(true);
                ecrit = true;
                StartCoroutine(AfficherDialogueParLettre(dialogueThays, choixDiplomateThays[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(1.5f);
                bulleThays.SetActive(false);
            }
        }
        else if (choixColere)
        {
            // Afficher les dialogues colériques
            for (int i = 0; i < choixColereKirie.Length; i++)
            {
                dialogueKirie.text = "";
                dialogueThays.text = "";
                bulleKirie.SetActive(true);
                ecrit = true;
                //WaitUntil - Pour attendre que la variable devienne false pour continuer la conversation
                StartCoroutine(AfficherDialogueParLettre(dialogueKirie, choixColereKirie[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(1.5f);
                bulleKirie.SetActive(false);
                bulleThays.SetActive(true);
                ecrit = true;
                StartCoroutine(AfficherDialogueParLettre(dialogueThays, choixColereThays[i]));
                yield return new WaitUntil(() => !ecrit);
                yield return new WaitForSeconds(1.5f);
                bulleThays.SetActive(false);
            }
        }
        // Déclencher l'événement de fin de dialogue
        OnDialogueComplete?.Invoke();
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

    private void DialogueTermine()
    {
        // Code à exécuter lorsque les dialogues sont terminés
        if (choixDiplomate)
        {
            Debug.Log("Tous les dialogues choix diplomate sont terminés.");
        }

        if (choixColere)
        {
            Debug.Log("Tous les dialogues choix colère sont terminés.");
        }
    }
}
