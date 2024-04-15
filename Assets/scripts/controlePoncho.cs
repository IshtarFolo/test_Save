using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controlePoncho : MonoBehaviour
{
    public GameObject poncho;
    public GameObject kirie;
    float speed = 1.0f;

    Rigidbody rb;
    Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 
        poncho.transform.position = Vector3.Lerp(poncho.transform.position, kirie.transform.position, Time.deltaTime * speed);

        // 
        if (kirie.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("idleDroite"))
        {
            poncho.transform.position = new Vector3(poncho.transform.position.x, 2.57f, kirie.transform.position.z - 0.4f);
        }
        else
        {
            poncho.transform.position = new Vector3(poncho.transform.position.x, 2.57f, kirie.transform.position.z - 0.2f);
        }
    }
}
