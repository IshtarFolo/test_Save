using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionPerso: MonoBehaviour
{
    public TextMeshProUGUI lettreE; // L'interaction avec la letttre E
    public bool veutParler = false; // Indique si le joueur peut interagir avec un villageois
    public GameObject villageois; // Référence au villageois avec lequel le joueur interagit
    public GameObject gatito; // Référence au villageois avec lequel le joueur interagit

    private interactionVillageois scriptVillageois; // Script du villageois avec lequel le joueur interagit
    private DialogueGatitoVillage scriptGatito; // Script du villageois avec lequel le joueur interagit

    //public GameObject bulle;

    // Start is called before the first frame update
    void Start()
    {
        lettreE.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (veutParler)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InteragirAvecVillageois();
            }
        }
    }
    //Affichage du E
    private void OnTriggerEnter(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "villageois")
        {
            lettreE.enabled = true;
            veutParler = true;
            villageois = infoCollision.gameObject;
            scriptVillageois = villageois.GetComponent<interactionVillageois>();
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
            lettreE.enabled = true;
            veutParler = true;
            gatito = infoCollision.gameObject;
            scriptGatito = gatito.GetComponent<DialogueGatitoVillage>();
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "villageois")
        {
            lettreE.enabled = false;
            veutParler = false;
            villageois = null;
            
        }

        if (infoCollision.gameObject.tag == "Gatito")
        {
            lettreE.enabled = false;
            veutParler = false;
            villageois = null;

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
