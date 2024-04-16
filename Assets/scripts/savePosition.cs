using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class savePosition : MonoBehaviour
{
    /*============
     * VARIABLES *
     ============*/
    public GameObject joueur; // Le joueur
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

    // Passer les valeurs de la save
    public void Start()
    {
        if (positionX != 0)
        {
            // On regarde si le personnage a boug� pr�c�demment
            transform.position = new Vector3(positionX, positionY, positionZ);
            transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

            // Enregistrement du numero de la scene au depart
            scene = _collision_kirie.noScene;
        }
    }

    private void Update()
    {
        scene = _collision_kirie.noScene;
    }

    // Sauvegarder les valeurs de positions 
    public void Save()
    {
        PlayerPrefs.SetFloat("laPositionX", transform.position.x);
        PlayerPrefs.SetFloat("laPositionY", transform.position.y);
        PlayerPrefs.SetFloat("laPositionZ", transform.position.z);

        PlayerPrefs.SetFloat("laRotationX", transform.eulerAngles.x);
        PlayerPrefs.SetFloat("laRotationY", transform.eulerAngles.y);
        PlayerPrefs.SetFloat("laRotationZ", transform.eulerAngles.z);

        // On cree une sauvegarde du numero de la scene
        PlayerPrefs.SetInt("laScene", scene);
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
        Time.timeScale = 0f;
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

        PlayerPrefs.GetInt("laScene", scene);


        // On charge la bonne scene 
        SceneManager.LoadScene(scene);

        // On regarde si le personnage a boug� pr�c�demment
        transform.position = new Vector3(positionX, positionY, positionZ);
        transform.rotation = Quaternion.Euler(rotationX, rotationY, rotationZ);

        // On d�sactive l'�cran de chargement
        loadingScreen.SetActive(false); 
        // On arrete la pause
        Time.timeScale = 1f;
    }
}
