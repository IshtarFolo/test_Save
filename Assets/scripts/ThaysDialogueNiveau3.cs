using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThaysDialogueNiveau3 : MonoBehaviour
{
    public string[] dialogues;
    public string[] reponses;

    public string GetDialogue(int index)
    {
        return dialogues[index];
    }

    public string GetReponse(int index)
    {
        return reponses[index];
    }
}
