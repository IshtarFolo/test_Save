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
    public static bool tutoFini; // Le journal est en la possession du joueur
    public static int poissons; // Les poissons ramasses

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
            if (PlayerPrefs.HasKey("tutoFini"))
            {
                tutoFini = PlayerPrefsX.GetBool("tutoFini");
            }
        }
    }

    private void Update()
    {
        scene = _collision_kirie.noScene;
    }

    public void NouvellePartie()
    {
        // Supprime toutes les sauvegardes
        PlayerPrefs.DeleteAll();
        // tutoFini redevient false
        _collision_kirie.journalRamasse = false;
        // Supprime les données de PlayerPrefsX
        PlayerPrefsX.SetBool("tutoFini", tutoFini);
        // Load la premiere scene
        SceneManager.LoadScene(1);
    }

    // Sauvegarder les valeurs de positions 
    public void Save()
    {
        PlayerPrefs.SetFloat("laPositionX", joueur.transform.position.x);
        PlayerPrefs.SetFloat("laPositionY", joueur.transform.position.y);
        PlayerPrefs.SetFloat("laPositionZ", joueur.transform.position.z);

        PlayerPrefs.SetFloat("laRotationX", joueur.transform.eulerAngles.x);
        PlayerPrefs.SetFloat("laRotationY", joueur.transform.eulerAngles.y);
        PlayerPrefs.SetFloat("laRotationZ", joueur.transform.eulerAngles.z);

        // On cree une sauvegarde du numero de la scene
        scene = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("laScene", scene);

        // Sauvegarde de l,acquisition du journal
        PlayerPrefsX.SetBool("Journal", tutoFini);

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
        PlayerPrefsX.GetBool("Journal", tutoFini);

        // On charge la scene
        SceneManager.LoadScene(scene);
    }

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    if (joueur != null)
    {
        // On passe les valeurs de position et de rotation au joueur
        joueur.transform.position = new Vector3(positionX, positionY, positionZ);
        joueur.transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

        tutoFini = _collision_kirie.journalRamasse;
    }

    // On d�sactive l'�cran de chargement
    loadingScreen.SetActive(false); 
    // On arrete la pause
   // Time.timeScale = 1f;

    // On unsubscribe de l'evenement de loadScene pour prevenir les leaks de memoire
    SceneManager.sceneLoaded -= OnSceneLoaded;
}
}
