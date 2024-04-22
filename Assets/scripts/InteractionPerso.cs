using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class interactionPerso: MonoBehaviour
{
    public GameObject lettreE; // L'interaction avec la letttre E
    public bool veutParler = false; // Indique si le joueur peut interagir avec un villageois
    public static GameObject villageois; // Référence au villageois avec lequel le joueur interagit
    public GameObject gatito; // Référence au villageois avec lequel le joueur interagit

    private interactionVillageois scriptVillageois; // Script du villageois avec lequel le joueur interagit
    public DialogueGatitoVillage scriptGatito; // Script du villageois avec lequel le joueur interagit

    //public GameObject bulle;

    // Start is called before the first frame update
    void Start()
    {
        lettreE.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                scriptVillageois = villageois.GetComponent<interactionVillageois>();
                InteragirAvecVillageois();
            }
        }
    }
    //Affichage du E
    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "villageois")
        {
            lettreE.SetActive(true);
            veutParler = true;
            villageois = infoCollision.gameObject;
        }
        // Si le joueur a déjà parlé au villageois, réactiver la bulle de dialogue
        if (scriptVillageois.aParle)
        {
            scriptVillageois.dialogueVillageois.enabled = true;
            scriptVillageois.bulle.SetActive(true);
        }
        //Si le joueur parle à Gatito
        if (infoCollision.gameObject.tag == "Gatito")
        {
            lettreE.SetActive(true);
            veutParler = true;
            gatito = infoCollision.gameObject;
            scriptGatito = gatito.GetComponent<DialogueGatitoVillage>();
            scriptVillageois.veutParler = true;
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "villageois")
        {
            lettreE.SetActive(false);
            veutParler = false;
            villageois = null;
            
        }

        if (infoCollision.gameObject.tag == "Gatito")
        {
            lettreE.SetActive(false);
            veutParler = false;
            gatito= null;

        }
    }

    // Méthode pour interagir avec le villageois
    private void InteragirAvecVillageois()
    {
        if (scriptVillageois != null)
        {
            scriptVillageois.AfficherDialogueSuivant();
            scriptVillageois.dialogueVillageois.enabled = true;
        }
    }
}
