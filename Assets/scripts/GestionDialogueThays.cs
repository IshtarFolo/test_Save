using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionDialogueThays : MonoBehaviour
{

    [SerializeField]
    private string[] phrase1;
    [SerializeField]
    private string[] phrase2;
    public GameObject[] reponses;

    public TextMeshProUGUI dialogue;
    void Start()
    {
        DialogueOption1();
       for (int i=0; i< reponses.Length; i++)
        {
            reponses[i].SetActive(true);
        } 
    }

    public void DialogueOption1()
    {
        for (int i = 0; i < reponses.Length; i++)
        {
            reponses[i].SetActive(false);
        }
        dialogue.gameObject.SetActive(true);
        dialogue.text = (phrase1[0]);
    }

    public void DialogueOption2()
    {
        for (int i = 0; i < reponses.Length; i++)
        {
            reponses[i].SetActive(false);
        }
        dialogue.gameObject.SetActive(true);
        dialogue.text = (phrase2[0]);
    }
}
