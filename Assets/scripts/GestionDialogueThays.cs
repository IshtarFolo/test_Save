using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionDialogueThays : MonoBehaviour
{
    [Header("Les différents dialogues des personnages selon l'option choisi")]
    [SerializeField]
    private string[] choixDiplomateKirie;
    [SerializeField]
    private string[] choixDiplomateThays;
    [SerializeField]
    private string[] choixColereKirie;
    [SerializeField]
    private string[] choixColereThays;

    [Header("Choix proposés")]
    public GameObject[] lesOptions;

    [Header("Les dialogues des personnages")]
    public TextMeshProUGUI dialogueKirie;
    public TextMeshProUGUI dialogueThays;

    [Header("Les bulles des dialogues des personnages")]
    public GameObject bulleKirie;
    public GameObject bulleThays;

    [Header("booleen des type des choix")]
    public bool choixDiplomate = false;
    public bool choixColere = false;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void ChoixDiplomate()
    {
        choixDiplomate = true;
        lesOptions[1].SetActive(false);
    }

    public void ChoixColere()
    {
        choixColere = true;
        lesOptions[0].SetActive(false);
    }
}
