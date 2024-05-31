using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class savePosition : MonoBehaviour
{
    /*
     * Systeme de sauvegarde par Xavier Arbour:
     * 
     * Le joueur passe sur un des nombreux points de sauvegarde dujeu dissemines dans chaque niveaux.
     * Le systeme sauvegarde la positionet rotation du personnage joueur (Kirie) et certains booleens
     * confirmant la fin de quetes importantes au progres du joueur. Lorsque le joueur appuie sur "charger une partie"
     * on recharge a la derniere sauvegarde, s'il appuie sur "Nouvelle partie" la derniere sauvegarde est effacee et on
     * recommence le jeu a 0.
     * 
     */
    /*============
     * VARIABLES *
     ============*/
    public GameObject joueur; // Le joueur
    public Button newG; // Bouton de nouvelle partie
    public Button load; // Bouton de chargement
    public GameObject loadingScreen; // L'�cran de chargement

    // Les positions et rotations a enregistrer
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public static int scene; // Numero de la scene a charger

    // Les Quetes et leur objectifs en bool
    public bool tutoFini; // Le journal est en la possession du joueur
    public static bool finPeche; // La fin du jeu de peche
    public static bool cannePeche; // L'obtention de la canne a peche
    public static bool villageoisAParle; // La discussion avec le bon villageois
    public bool queteLettresFinie; // La fin de la quete de la lettre
    public static bool parleGatito; // Si Gatito a parle au joueur dans le village
    public static bool retrouveGatito; // Si on retrouve Gatito apres la quete de peche 

    // Passer les valeurs de la save
    public void Start()
    {
        // On charge les positions et rotations du joueur si les cles existent
        if (PlayerPrefs.HasKey("laPositionX") && PlayerPrefs.HasKey("laPositionY") && PlayerPrefs.HasKey("laPositionZ") &&
        PlayerPrefs.HasKey("laRotationX") && PlayerPrefs.HasKey("laRotationY") && PlayerPrefs.HasKey("laRotationZ"))
        {
            positionX = PlayerPrefs.GetFloat("laPositionX", 0);
            positionY = PlayerPrefs.GetFloat("laPositionY", 0);
            positionZ = PlayerPrefs.GetFloat("laPositionZ", 0);
            rotationX = PlayerPrefs.GetFloat("laRotationX", 0);
            rotationY = PlayerPrefs.GetFloat("laRotationY", 0);
            rotationZ = PlayerPrefs.GetFloat("laRotationZ", 0);

            // On regarde si le personnage a bougé précédemment
            joueur.transform.position = new Vector3(positionX, positionY, positionZ);
            joueur.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

            // Enregistrement du numero de la scene au depart
            scene = _collision_kirie.noScene;

            // Charger les variables de quête
            tutoFini = PlayerPrefsX.GetBool("tutoFini");
            finPeche = PlayerPrefsX.GetBool("finPeche");
            cannePeche = PlayerPrefsX.GetBool("CanneRamassee");
            villageoisAParle = PlayerPrefsX.GetBool("vilParle");
            queteLettresFinie = PlayerPrefsX.GetBool("finLettres");
            parleGatito = PlayerPrefsX.GetBool("finiGatito");
            retrouveGatito = PlayerPrefsX.GetBool("retrouveGatito");

            Load();
        }
    }

    private void Update()
    {
        scene = _collision_kirie.noScene;
        tutoFini = _collision_kirie.journalRamasse;
        queteLettresFinie = _collision_kirie.finQueteLettres;
        villageoisAParle = interactionVillageois.aParleVillageois1;
        cannePeche = _collision_kirie.cannePecheRamasse;
        parleGatito = DialogueGatitoVillage.finiParler;
        finPeche = _collision_kirie.finJeuPeche;
        retrouveGatito = _collision_kirie.trouveGatito;
    }

    public void NouvellePartie()
    {
        // Supprime toutes les sauvegardes
        PlayerPrefs.DeleteAll();
        DeleteAllPlayerPrefsX();

        // Remettre les variables de quête à false
        tutoFini = false;
        finPeche = false;
        cannePeche = false;
        villageoisAParle = false;
        queteLettresFinie = false;
        retrouveGatito = false;

        // Sauvegarder les états initiaux
        PlayerPrefsX.SetBool("tutoFini", tutoFini);
        PlayerPrefsX.SetBool("finPeche", finPeche);
        PlayerPrefsX.SetBool("CanneRamassee", cannePeche);
        PlayerPrefsX.SetBool("vilParle", villageoisAParle);
        PlayerPrefsX.SetBool("finLettres", queteLettresFinie);
        PlayerPrefsX.SetBool("finiGatito", parleGatito);
        PlayerPrefsX.SetBool("retrouveGatito", retrouveGatito);

        // Load la premiere scene
        SceneManager.LoadScene(1);
    }

    // Sauvegarder les valeurs de positions 
    public void Save()
    {

        finPeche = _collision_kirie.finJeuPeche;
        cannePeche = _collision_kirie.cannePecheRamasse;
        villageoisAParle = interactionVillageois.aParleVillageois1;
        queteLettresFinie = _collision_kirie.finQueteLettres;
        parleGatito = DialogueGatitoVillage.finiParler;
        retrouveGatito = _collision_kirie.trouveGatito;

        string sceneKey = "Scene" + scene;

        PlayerPrefs.SetFloat(sceneKey + "laPositionX", joueur.transform.position.x);
        PlayerPrefs.SetFloat(sceneKey + "laPositionY", joueur.transform.position.y);
        PlayerPrefs.SetFloat(sceneKey + "laPositionZ", joueur.transform.position.z);

        PlayerPrefs.SetFloat(sceneKey + "laRotationX", joueur.transform.eulerAngles.x);
        PlayerPrefs.SetFloat(sceneKey + "laRotationY", joueur.transform.eulerAngles.y);
        PlayerPrefs.SetFloat(sceneKey + "laRotationZ", joueur.transform.eulerAngles.z);

        // On cree une sauvegarde du numero de la scene
        scene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("laScene", scene);

        // Sauvegarder les états des quêtes
        PlayerPrefsX.SetBool("tutoFini", tutoFini);
        PlayerPrefsX.SetBool("vilParle", villageoisAParle);
        PlayerPrefsX.SetBool("CanneRamassee", cannePeche);
        PlayerPrefsX.SetBool("finPeche", finPeche);
        PlayerPrefsX.SetBool("finLettres", queteLettresFinie);
        PlayerPrefsX.SetBool("finiGatito", parleGatito);
        PlayerPrefsX.SetBool("retrouveGatito", retrouveGatito);

        PlayerPrefs.Save();
    }

    // Avoir les valeurs pour la position de base 
    public void Load()
    {
        // On lance la coroutine pour emuler un temps de chargement pour le jeu
        StartCoroutine(LoadDelai());
    }

    IEnumerator LoadDelai()
    {
        // On arrete la pause
        Time.timeScale = 1f;
        // l'�cran de chargement est activ�
        loadingScreen.SetActive(true);
        // On attend 1 seconde...
        yield return new WaitForSecondsRealtime(1f);
        // Les positions et rotations du joueur sont recharg�es
        // Les postions
        positionX = PlayerPrefs.GetFloat("laPositionX");
        positionY = PlayerPrefs.GetFloat("laPositionY");
        positionZ = PlayerPrefs.GetFloat("laPositionZ");
        // Les rotations
        rotationX = PlayerPrefs.GetFloat("laRotationX");
        rotationY = PlayerPrefs.GetFloat("laRotationY");
        rotationZ = PlayerPrefs.GetFloat("laRotationZ");

        // On charge l'index de la scene enregistre dans PlayerPrefs
        scene = PlayerPrefs.GetInt("laScene", scene);

        // Les quetes dans l'ordre:
        tutoFini = PlayerPrefsX.GetBool("tutoFini");
        villageoisAParle = PlayerPrefsX.GetBool("vilParle");
        cannePeche = PlayerPrefsX.GetBool("CanneRamassee");
        finPeche = PlayerPrefsX.GetBool("finPeche");
        queteLettresFinie = PlayerPrefsX.GetBool("finLettres");
        parleGatito = PlayerPrefsX.GetBool("finiGatito");
        retrouveGatito = PlayerPrefsX.GetBool("retrouveGatito");

        // On charge la scene
        SceneManager.LoadScene(scene);

        Debug.Log("Load tutoFini: " + tutoFini);
        Debug.Log("Load finPeche: " + finPeche);
        Debug.Log("Load cannePeche: " + cannePeche);
        Debug.Log("Load villageoisAParle: " + villageoisAParle);
        Debug.Log("Load queteLettresFinie: " + queteLettresFinie);
        Debug.Log("Load finiGatito: " + parleGatito);
        Debug.Log("Load trouveGatito: " + retrouveGatito);

        _collision_kirie.finTuto = tutoFini;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    // Load la position et rotation specifiques a la scene chargee
    string sceneKey = "Scene" + scene.buildIndex;

    if (PlayerPrefs.HasKey(sceneKey + "laPositionX") && PlayerPrefs.HasKey(sceneKey + "laPositionY") && PlayerPrefs.HasKey(sceneKey + "laPositionZ"))
    {
        float positionX = PlayerPrefs.GetFloat(sceneKey + "laPositionX");
        float positionY = PlayerPrefs.GetFloat(sceneKey + "laPositionY");
        float positionZ = PlayerPrefs.GetFloat(sceneKey + "laPositionZ");

        joueur.transform.position = new Vector3(positionX, positionY, positionZ);
    }

    if (PlayerPrefs.HasKey(sceneKey + "laRotationX") && PlayerPrefs.HasKey(sceneKey + "laRotationY") && PlayerPrefs.HasKey(sceneKey + "laRotationZ"))
    {
        float rotationX = PlayerPrefs.GetFloat(sceneKey + "laRotationX");
        float rotationY = PlayerPrefs.GetFloat(sceneKey + "laRotationY");
        float rotationZ = PlayerPrefs.GetFloat(sceneKey + "laRotationZ");

        joueur.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);
    }

        tutoFini = PlayerPrefsX.GetBool("tutoFini");
        villageoisAParle = PlayerPrefsX.GetBool("vilParle");
        cannePeche = PlayerPrefsX.GetBool("CanneRamassee");
        finPeche = PlayerPrefsX.GetBool("finPeche");
        queteLettresFinie = PlayerPrefsX.GetBool("finLettres");
        parleGatito = PlayerPrefsX.GetBool("finiGatito");
        retrouveGatito = PlayerPrefsX.GetBool("retrouveGatito");

        // On d�sactive l'�cran de chargement
        loadingScreen.SetActive(false); 
    }

    // Efface les variables sauvegardees dans PlayerPrefsX
    public static void DeleteAllPlayerPrefsX()
    {
        string[] keysToDelete = new string[]
        {
        "tutoFini",
        "finPeche",
        "CanneRamassee",
        "vilParle",
        "finLettres",
        "finiGatito",
        "retrouveGatito",
        };

        foreach (string key in keysToDelete)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
