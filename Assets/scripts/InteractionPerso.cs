using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactionPerso: MonoBehaviour
{
    public TextMeshProUGUI lettreE; // Texte "Appuyez sur E" pour interagir
    public bool peutInteragir = false; // Indique si le joueur peut interagir avec un villageois
    public GameObject villageois; // Référence au villageois avec lequel le joueur interagit

    private interactionVillageois scriptVillageois; // Script du villageois avec lequel le joueur interagit

    // Start is called before the first frame update
    void Start()
    {
        lettreE.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (peutInteragir)
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
            peutInteragir = true;
            villageois = infoCollision.gameObject;
            scriptVillageois = villageois.GetComponent<interactionVillageois>();
        }
    }

    private void OnTriggerExit(Collider infoCollision)
    {
        if (infoCollision.gameObject.tag == "villageois")
        {
            lettreE.enabled = false;
            peutInteragir = false;
            villageois = null;
            scriptVillageois = null;
        }
    }

    // Méthode pour interagir avec le villageois
    private void InteragirAvecVillageois()
    {
        if (scriptVillageois != null)
        {
            scriptVillageois.AfficherDialogueSuivant();
            scriptVillageois.dialogueVilleagois.enabled = true;
        }
    }
}
