using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class savePosition : MonoBehaviour
{
    /*============
     * VARIABLES *
     ============*/
    public GameObject joueur; // Le joueur
    public Button newG; // Bouton de nouvelle partie
    public Button load; // Bouton de chargement
    public GameObject loadingScreen; // L'�cran de chargement

    // Les positions et rotations � enregistrer
    public float positionX;
    public float positionY;
    public float positionZ;

    public float rotationX;
    public float rotationY;
    public float rotationZ;

    public static int scene; // Numero de la scene a charger

    // Les Quetes et leur objectifs en bool
    public static bool tutoFini; // Le journal est en la possession du joueur
    public static bool finPeche; // La fin du jeu de peche
    public static bool cannePeche; // L'obtention de la canne a peche
    public static bool villageoisAParle; // La discussion avec le bon villageois
    public static bool queteLettresFinie;

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

            // Load la variable tutoFini si tutoFini existe
            tutoFini = PlayerPrefsX.GetBool("tutoFini");
        }
    }

    private void Update()
    {
        scene = _collision_kirie.noScene;
        tutoFini = _collision_kirie.finTuto;

       // Debug.Log();
    }

    public void NouvellePartie()
    {
        // Supprime toutes les sauvegardes
        PlayerPrefs.DeleteAll();
        DeleteAllPlayerPrefsX();

        // tutoFini redevient false
        _collision_kirie.journalRamasse = false;
        // Supprime les données de PlayerPrefsX
        PlayerPrefsX.SetBool("tutoFini", tutoFini);

        PlayerPrefsX.SetBool("finPeche", finPeche);

        PlayerPrefsX.SetBool("CanneRamassee", cannePeche);

        PlayerPrefsX.SetBool("vilParle", villageoisAParle);

        PlayerPrefsX.SetBool("finLettres", queteLettresFinie);

        // Load la premiere scene
        SceneManager.LoadScene(1);
    }

    // Sauvegarder les valeurs de positions 
    public void Save()
    {

        finPeche = SystemePeche.finiPeche;
        cannePeche = _collision_kirie.cannePecheRamasse;
        villageoisAParle = interactionVillageois.aParleVillageois1;
        queteLettresFinie = _collision_kirie.finQueteLettres;

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

        // Sauvegarde de l,acquisition du journal
        PlayerPrefsX.SetBool("Journal", tutoFini);

        // Les quetes dans l'ordre:
        PlayerPrefsX.SetBool("tutoFini", tutoFini);
        PlayerPrefsX.SetBool("vilParle", villageoisAParle);
        PlayerPrefsX.SetBool("CanneRamassee", cannePeche);
        PlayerPrefsX.SetBool("finPeche", finPeche);
        PlayerPrefsX.SetBool("finLettres", queteLettresFinie);

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
        // On pause le jeu
        //Time.timeScale = 0f;
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

        // Chargement de l'acquisition du journal
        tutoFini = PlayerPrefsX.GetBool("Journal");

        // Les quetes dans l'ordre:
        tutoFini = PlayerPrefsX.GetBool("tutoFini");
        villageoisAParle = PlayerPrefsX.GetBool("vilParle");
        cannePeche = PlayerPrefsX.GetBool("CanneRamassee");
        finPeche = PlayerPrefsX.GetBool("finPeche");
        PlayerPrefsX.GetBool("finLettres", queteLettresFinie);

        // On charge la scene
        SceneManager.LoadScene(scene);
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


    tutoFini = PlayerPrefsX.GetBool("Journal");
    villageoisAParle = PlayerPrefsX.GetBool("vilParle", villageoisAParle);
    cannePeche = PlayerPrefsX.GetBool("CanneRamassee", cannePeche);
    finPeche = PlayerPrefsX.GetBool("finPeche", finPeche);
    queteLettresFinie = PlayerPrefsX.GetBool("finLettres", queteLettresFinie);


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
        };

        foreach (string key in keysToDelete)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
