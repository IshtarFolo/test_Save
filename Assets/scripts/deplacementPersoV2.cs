using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementPersoV2 : MonoBehaviour
{
    // Raccourcis GetComponent
    Rigidbody rb;
    Animator animateur;

    public float vitesse = 30f; // Rapidite du personnage
    bool toucheSol; // Booleen pour detecter si le perso touche le sol
    public float forceSaut = 50f; // Force du saut
    private float multiplicateurDescente = 15f; // La force de descente du personnage lorsqu'il est en l'air
    bool peutBouger = true; // Verification si le personnage peut bouger
    private bool aSaute = false; // Verifie si le personnage a saute
    private bool retoucheSol = false;
    bool peutCourir = true;
    bool jumpInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Rigidbody
        animateur = GetComponent<Animator>(); // Animator

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Pour detecter les collisions de manière permanente et
                                                                       // eviter que le pero ne colle trop au murs

    }

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpInput = true;
        }
    }

    void FixedUpdate()
    {
        /*===========
        * MOUVEMENT *
        ============*/
        if (peutBouger)
        {
            float moveHorizontal = -Input.GetAxis("Vertical");
            float moveVertical = Input.GetAxis("Horizontal");

            // Raccourcis du mouvement et passation de ses valeurs
            Vector3 mouvement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

            rb.AddForce(mouvement * vitesse);

            // On passe les valeurs des variables aux Floats de l'animator
            animateur.SetFloat("VelociteX", moveHorizontal);
            animateur.SetFloat("VelociteZ", moveVertical);

            /* 
            * Gestion des angles dans l'animation idle
            --------------------------------------------------------------------------------------------------------------------------- */
            // On verifie dans quel angle le personnage se dirige et on active l'animation idle correspondante � son mouvement
            // Si la velocite du rigidbody est egale a 0...
            if (mouvement != Vector3.zero)
            {
                float angle = Vector3.SignedAngle(Vector3.forward, mouvement, Vector3.up);

                // On lance la bonne animation dependemment de l'angle du perso
                if (angle > 0)
                {
                    animateur.SetTrigger("idleGauche");
                }
                else if (angle <= 0)
                {
                    animateur.SetTrigger("idleDroite");
                }
            }

            // On regarde si les param�tres de l'animator sont �gaux � 0...
            if (animateur.GetFloat("VelociteX") == 0 && animateur.GetFloat("VelociteZ") == 0)
            {
                // Si oui, l'animation idle est true
                animateur.SetBool("idle", true);
                // l'animation de course se désactive
                animateur.SetBool("cours", false);
            }
            else if(peutCourir)
            {
                // Si non, l'animation idle est false
                animateur.SetBool("idle", false);
                // L'animation de course s'active
                animateur.SetBool("cours", true);
            }
        }

                // Raccourci pour la velocite du saut
        float velociteY = (rb.velocity.y);

        // Si le personnage touche le sol, sa velocite Y ne change pas sinon elle change
        if (!toucheSol)
        {
            animateur.SetFloat("VelociteY", velociteY);
        }
        else
        {
            animateur.SetFloat("VelociteY", 0);
        }

        /*=======
         * SAUT *
         =======*/
        RaycastHit infoCollision;
        // Cast des spheres vers bas perso + variable infoCollision prends valeurs
        toucheSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.3f, 1f), 0f, -transform.up, out infoCollision, 1.5f);

        // Si le jouer appuie sur espace la velocitee Y augmente et le personnage saute  
        if (jumpInput && toucheSol && peutBouger)
        {
            rb.AddForce(new Vector3(0, forceSaut, 0), ForceMode.Impulse);
            animateur.SetBool("saute", true); 
            animateur.SetBool("retombe", false); 
            aSaute = true;
            jumpInput = false;
        }
        // Si la velocite Y est plus petite que 2...
        else if (rb.velocity.y < 0 && !toucheSol)
        {
            // On applique une force de descente au personnage pour le forcer au sol
            rb.velocity += Vector3.up * Physics.gravity.y * (multiplicateurDescente - 1) * Time.deltaTime;
            animateur.SetBool("retombe", true); 
        }
        else if (toucheSol && Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            animateur.SetBool("saute", false);
            animateur.SetBool("retombe", false);
            aSaute = false;
        }
        else if (rb.velocity.x == 0 && rb.velocity.z == 0 && animateur.GetBool("saute") == true)
        {
            animateur.Play("MilieuSaut");
        }

        // On verifie si le personnage touche le sol ou non
        switch (toucheSol)
        {
            case false:
                animateur.SetBool("saute", true);
                break;
            case true:
                StartCoroutine(RecupSaut());
                if (aSaute)
                {
                    if (retoucheSol && Mathf.Abs(rb.velocity.y) < 0.01f)
                    {
                        animateur.SetBool("retombe", true);
                        aSaute = false;
                    }
                    else
                    {
                        animateur.SetBool("retombe", false);
                    }
                }
                animateur.SetBool("saute", false);
                break;
        }
        Debug.Log("aSaute: " + aSaute);
        Debug.Log("peutBouger: " + peutBouger);
        Debug.Log("peutCourir: " + peutCourir);
    }


    private void OnDrawGizmos()
    {
        // On dessine la sph�re sous la capsule (perso), l� o� le sphereCast se fait
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 0.3f, 0f), 1f);
    } 

    /*--------------
     * IENUMERATOR *
     --------------*/
    // La couroutine qui permet de donner un effet de r�cup�ration du saut
    IEnumerator RecupSaut()
    {
        peutBouger = false;
        peutCourir = false;
        yield return new WaitForSeconds(0.5f);
        peutBouger = true;
        peutCourir = true;
        retoucheSol = false;
    }
}