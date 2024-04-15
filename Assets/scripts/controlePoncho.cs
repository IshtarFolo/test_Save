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
  // Make the poncho follow the kirie with a delay
    poncho.transform.position = Vector3.Lerp(poncho.transform.position, kirie.transform.position, Time.deltaTime * speed);
    }
}
