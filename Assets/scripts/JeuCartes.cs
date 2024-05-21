using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


//Script pour le jeu de cartes m�moires pour le niveau 2 - puzzle pour entrer dans la cave de l'antagoniste - Par Camilia El Moustarih
//Inspir� du tutoriel de Unity pour les nuls sur YouTube - S�rie Memory Unity
public class JeuCartes : MonoBehaviour
{
    ///////// D�claration des variables /////////
    //Liste contenant tous les sprites sur les cartes
    [SerializeField] List<Sprite> listeItems = new List<Sprite>();

    //Variables de son
    [SerializeField] AudioClip sonMatchCarte, sonTourneCarte, sonCarteBombe, sonReussite;
    private AudioSource audioSource;

    //Variables pour compter les cartes retourn�es en paires
    private int retournerCarte = 0;
    private GameObject premiereCarte, secondeCarte;

    //Liste qui va stocker des cha�nes de caract�res pour utiliser le nom des sprites
    private List<string> listeCartesTrouvees = new List<string>(); 

    //R�cup�rer les slots de chaque carte
    private GameObject[] slot;

    //Variables de texte pour le compteur
    public TextMeshProUGUI txtCompteur;
    private int valeurCompteur = 30; //Le joueur aura 60 secondes pour trouver les bonnes combinaisons

    //TextMeshPro
    public TextMeshProUGUI notifEchec;
    public TextMeshProUGUI texteInstructions;
    public TextMeshProUGUI texteBombe;

    //Activer un �cran noir quand deux cartes de bombes sont agenc�es
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
        //M�langer et afficher les cartes
        Shuffle();

        //Initialiser le compteur
        InvokeRepeating("Compteur", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        //D�tecter quand le joueur clique avec le bouton gauche de la souris sur une carte
        if (Input.GetMouseButtonDown(0))
        {
            //Pour faire en sorte que le joueur ne vera que 2 cartes � la fois, lorsqu'il clique sur la troisi�me,
            //les deux autres pr�c�demment affich�es se retournent
            if(retournerCarte == 2)
            {
                retournerCarte = 0;

                //Cacher les cartes � nouveau
                SpriteRenderer spritePremiereCarte = premiereCarte.GetComponentInChildren<SpriteRenderer>();
                SpriteRenderer spriteSecondeCarte = secondeCarte.GetComponentInChildren<SpriteRenderer>();

                if (!listeCartesTrouvees.Contains(spritePremiereCarte.sprite.name))
                {
                    spritePremiereCarte.enabled = false;
                    spriteSecondeCarte.enabled = false;
                }                
            }

            //pour la position de la souris sur l'�cran
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Afficher les sprites
                SpriteRenderer spritesImagesCartes = hit.transform.Find("ImgCarte").GetComponent<SpriteRenderer>();
                spritesImagesCartes.enabled = !spritesImagesCartes.enabled; //inverser le sprite de true � false

                //Incr�menter les cartes retourn�es
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

                        //D�sactiver les instructions
                        texteInstructions.gameObject.SetActive(false);

                        //Activer le panel noir
                        ecranNoirBombe.gameObject.SetActive(true);

                        //Activer la notification de bombe
                        texteBombe.gameObject.SetActive(true);

                        Debug.Log("Partie termin�e");

                        //Recharger la sc�ne apr�s un d�lai de 5 secondes
                        Invoke("DelaiChargementScene", 5f);
                        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }

                    //Si le joueur combine deux cartes de Aasha et deux cartes de cl�s - Il acc�dera � la scene finale
                    if ((premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "aashaSprite") && (secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "aashaSprite") 
                        && 
                        (premiereCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "cleSprite") && (secondeCarte.GetComponentInChildren<SpriteRenderer>().sprite.name == "cleSprite"))
                    {
                        //Jouer le son de r�ussite
                        audioSource.PlayOneShot(sonReussite);

                        //Charger la sc�ne de la cave de Thays - Boss final - Niveau3_Delivrance
                        Invoke("ChargerSceneFinale", 5f);
                    }
                }
                
                //Jouer le son de carte tourn�e
                audioSource.PlayOneShot(sonTourneCarte);
            }
        }
    }

    //Fonction qui va g�n�rer dynamiquement les cartes avec leurs sprites � des positions diff�rentes
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
            //Les sprites sont cach�s au d�marrage du jeu puisqu'on aura � cliquer pour retourner les cartes
            target.enabled = false;
        }
    }

    private void PaireTrouvee(GameObject obj1, GameObject obj2)
    {
        //D�sactiver les boxColliders pour ne plus pouvoir cliquer dessus et le raycast ne fonctionnera plus
        obj1.GetComponentInChildren<BoxCollider>().enabled = false;
        obj2.GetComponentInChildren<BoxCollider>().enabled = false;
        listeCartesTrouvees.Add(obj1.GetComponentInChildren<SpriteRenderer>().sprite.name);
        audioSource.PlayOneShot(sonMatchCarte);
    }

    public void Replay()
    {
        //Recharger la sc�ne courante
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //Le joueur aura 30 secondes pour trouver les bonnes combinaisons
    void Compteur()
    {
        valeurCompteur -= 1;
        txtCompteur.text = valeurCompteur.ToString();

        //Arr�ter le compteur lorsqu'il est rendu � 0
        if (valeurCompteur <= 0)
        {            
            //Annuler la fonction "Compteur"
            CancelInvoke("Compteur");
            Debug.Log("trop lent");

            //D�sactiver les instructions
            texteInstructions.gameObject.SetActive(false);

            //Activer la notification d'�chec
            notifEchec.gameObject.SetActive(true);


            //Recharger la sc�ne
            Invoke("DelaiChargementScene", 2f);
            //SceneManager.GetActiveScene();
        }
    }

    //Invoke pour le chargement de sc�ne
    void DelaiChargementScene() 
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    //Charger la sc�ne de confrontation finale
    void ChargerSceneFinale()
    {
        SceneManager.LoadScene("Niveau3_Delivrance");
    }
}
