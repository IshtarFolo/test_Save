using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class control_ui : MonoBehaviour
{
    public GameObject interfacejeu;
    public GameObject journal;
    public GameObject menuPause;

    [Header("AudioSource")]
    public AudioSource gameManager;

    [Header("AudioClip")]
    public AudioClip ouvrirJournal;
    public AudioClip fermerJournal;

    // La variable est appelé pour que lorsque le menu pause
    // est ouvert, on ne peut pas ouvrir le journal.
    private bool pasOuvrirJournal;


    void Update()
    {

        // LORSQUE LE JOUEUR APPUIE SUR J, ON OUVRE L'INVENTAIRE, L'INTERFACE DE JEU EST INACTIVE
        if (Input.GetKeyDown(KeyCode.J) && journal.activeSelf == false && pasOuvrirJournal == false && _collision_kirie.finTuto == true)
        {
            journal.SetActive(true);
            interfacejeu.SetActive(false);
            //Debug.Log("J est actif");
            gameManager.PlayOneShot(ouvrirJournal, 1.8f);
        }
        else if (Input.GetKeyDown(KeyCode.J) && journal.activeSelf == true && pasOuvrirJournal == false)
        {
            journal.SetActive(false);
            interfacejeu.SetActive(true);
            gameManager.PlayOneShot(fermerJournal, 0.8f);
        }

        // LORSQUE LE JOUEUR APPUIE SUR ESCAPE, ON OUVRE LE MENU PAUSE, L'INTERFACE DE JEU EST INACTIVE
        if (Input.GetKeyDown(KeyCode.Escape) && menuPause.activeSelf == false)
        {
            menuPause.SetActive(true);
            interfacejeu.SetActive(false);
            journal.SetActive(false);
            //Debug.Log("Esc est actif");
            Time.timeScale = 0f;
            pasOuvrirJournal = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menuPause.activeSelf == true)
        {
            menuPause.SetActive(false);
            interfacejeu.SetActive(true);
            // On arrete la pause
            Time.timeScale = 1f;
            pasOuvrirJournal = false;
        }


    }

    public void arretJeuOk()
    {
        Time.timeScale = 1f;
    }
}
