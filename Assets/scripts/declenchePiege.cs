using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class declenchePiege : MonoBehaviour
{
    public GameObject piege;

    private void OnCollisionEnter(Collision infoCollision)
    {
        if (infoCollision.gameObject.tag == "piege")
        {
            piege.GetComponent<Animator>().SetBool("touche", true);
            StartCoroutine(Tombe());
            Invoke("Retour", 15f);
        }
    }

    void Retour()
    {
        piege.GetComponent<Animator>().SetBool("retourne", true);
        piege.GetComponent<Animator>().SetBool("tombe", false);
        piege.GetComponent<Animator>().SetBool("touche", false);
        return;
    }

    IEnumerator Tombe()
    {
        yield return new WaitForSeconds(2f);
        piege.GetComponent<Animator>().SetBool("tombe", true);
        piege.GetComponent<Animator>().SetBool("retourne", false);
    }

}
