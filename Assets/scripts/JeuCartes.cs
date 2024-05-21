using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


//Script pour le jeu de cartes mémoires pour le niveau 2 - puzzle pour entrer dans la cave de l'antagoniste - Par Camilia El Moustarih
//Inspiré du tutoriel de Unity pour les nuls sur YouTube - Série Memory Unity
public class JeuCartes : MonoBehaviour
{
    ///////// Déclaration des variables /////////
    //Liste contenant tous les sprites sur les cartes
    [SerializeField] List<Sprite> listeItems = new List<Sprite>();

    //Variables de son
    [SerializeField] AudioClip sonMatchCarte, sonTourneCarte, sonCarteBombe, sonReussite;
    private AudioSource audioSource;

    //Variables pour compter les cartes retournées en paires
    private int retournerCarte = 0;
    private GameObject premiereCarte, secondeCarte;

    //Liste qui va stocker des chaînes de caractères pour utiliser le nom des sprites
    private List<string> listeCartesTrouvees = new List<string>(); 

    //Récupérer les slots de chaque carte
    private GameObject[] slot;

    //Variables de texte pour le compteur
    public TextMeshProUGUI txtCompteur;
    private int valeurCompteur = 30; //Le joueur aura 60 secondes pour trouver les bonnes combinaisons

    //TextMeshPro
    public TextMeshProUGUI notifEchec;
    public TextMeshProUGUI texteInstructions;
    public TextMeshProUGUI texteBombe;

    //Activer un écran noir quand deux cartes de bombes sont agencées
    public GameObject ecranNoirBombe;

    public void Awake()
    {
        //Assigner dans le tableau slot, toutes les slots de la liste qui ont le tag "slot"
        slot = GameObject.FindGameObjectsWithTag("Slot");
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Mélanger et afficher les cartes
        Shuffle();

        //Initialiser le compteur
        InvokeRepeating("Compteur", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //Détecter quand le joueur clique avec le bouton gauche de la souris sur une carte
        if (Input.GetMouseButtonDown(0))
        {
            //Pour faire en sorte que le joueur ne vera que 2 cartes à la fois, lorsqu'il clique sur la troisième,
            //les deux autres précédemment affichées se retournent
            if(retournerCarte == 2)
            {
                retournerCarte = 0;

                //Cacher les cartes à nouveau
                SpriteRenderer spritePremiereCarte = premiereCarte.GetComponentInChildren<SpriteRenderer>();
                SpriteRenderer spriteSecondeCarte = secondeCarte.GetComponentInChildren<SpriteRenderer>();

                if (!listeCartesTrouvees.Contains(spritePremiereCarte.sprite.name))
                {
                    spritePremiereCarte.enabled = false;
                    spriteSecondeCarte.enabled = false;
                }                
            }

            //pour la position de la souris sur l'écran
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Afficher les sprites
                SpriteRenderer spritesImagesCartes = hit.transform.Find("ImgCarte").GetComponent<SpriteRenderer>();
                spritesImagesCartes.enabled = !spritesImagesCartes.enabled; //inverser le sprite de true à false

                //Incrémenter les cartes retournées
                retournerCarte++;
                if (retournerCarte == 1)
                {
                    premiereCarte = hit.collider.gameObject;
                }

                if (retournerCarte == 2)
                {
                    secondeCarte = hit.collider.gameObject;

                    //Si les cartes sont identiques de par le nom de leurs sprites
                    if (premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name)
                        PaireTrouvee(premiereCarte, secondeCarte);

                    //Si le joueur combine des cartes de bombes -- La partie est perdue et elle recommencera
                    if ((premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "bombeSprite") && (secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "bombeSprite"))
                    {
                        audioSource.PlayOneShot(sonCarteBombe);

                        //Désactiver les instructions
                        texteInstructions.gameObject.SetActive(false);

                        //Activer le panel noir
                        ecranNoirBombe.gameObject.SetActive(true);

                        //Activer la notification de bombe
                        texteBombe.gameObject.SetActive(true);

                        Debug.Log("Partie terminée");

                        //Recharger la scène après un délai de 5 secondes
                        Invoke("DelaiChargementScene", 5f);
                        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }

                    //Si le joueur combine deux cartes de Aasha et deux cartes de clés - Il accèdera à la scene finale
                    if ((premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "aashaSprite") && (secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "aashaSprite") 
                        && 
                        (premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "cleSprite") && (secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "cleSprite"))
                    {
                        //Jouer le son de réussite
                        audioSource.PlayOneShot(sonReussite);

                        //Charger la scène de la cave de Thays - Boss final - Niveau3_Delivrance
                        Invoke("ChargerSceneFinale", 5f);
                    }
                }
                
                //Jouer le son de carte tournée
                audioSource.PlayOneShot(sonTourneCarte);
            }
        }
    }

    //Fonction qui va générer dynamiquement les cartes avec leurs sprites à des positions différentes
    private void Shuffle()
    {
        //Liste temporaire
        List<Sprite> listeTemporaire = listeItems;
        listeItems.AddRange(listeItems);

        for (int i = 0; i < slot.Length; i++) 
        {
            int rnd = Random.Range(0, listeItems.Count);

            //Assigner le sprite correspondant au slot et boucler chaque slot
            SpriteRenderer target = slot[i].transform.Find("ImgCarte").GetComponent<SpriteRenderer>();
            target.sprite = listeTemporaire[rnd];

            //Retirer de la liste une fois choisi
            listeTemporaire.RemoveAt(rnd);
            //Les sprites sont cachés au démarrage du jeu puisqu'on aura à cliquer pour retourner les cartes
            target.enabled = false;
        }
    }

    private void PaireTrouvee(GameObject obj1, GameObject obj2)
    {
        //Désactiver les boxColliders pour ne plus pouvoir cliquer dessus et le raycast ne fonctionnera plus
        obj1.GetComponentInChildren<BoxCollider>().enabled = false;
        obj2.GetComponentInChildren<BoxCollider>().enabled = false;
        listeCartesTrouvees.Add(obj1.GetComponentInChildren<SpriteRenderer>().sprite.name);
        audioSource.PlayOneShot(sonMatchCarte);
    }

    public void Replay()
    {
        //Recharger la scène courante
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Le joueur aura 30 secondes pour trouver les bonnes combinaisons
    void Compteur()
    {
        valeurCompteur -= 1;
        txtCompteur.text = valeurCompteur.ToString();

        //Arrêter le compteur lorsqu'il est rendu à 0
        if (valeurCompteur <= 0)
        {            
            //Annuler la fonction "Compteur"
            CancelInvoke("Compteur");
            Debug.Log("trop lent");

            //Désactiver les instructions
            texteInstructions.gameObject.SetActive(false);

            //Activer la notification d'échec
            notifEchec.gameObject.SetActive(true);


            //Recharger la scène
            Invoke("DelaiChargementScene", 2f);
            //SceneManager.GetActiveScene();
        }
    }

    //Invoke pour le chargement de scène
    void DelaiChargementScene() 
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Charger la scène de confrontation finale
    void ChargerSceneFinale()
    {
        SceneManager.LoadScene("Niveau3_Delivrance");
    }
}
