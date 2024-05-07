using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class deplacementPerso : MonoBehaviour
{
    /*----------------
     *** VARIABLES ***
     -----------------*/
    private float vitesseDeplacement = 15f; // Vitesse de d�placement du personnage
    private float forceSaut = 500f; // Force du saut
    private float multiplicateurDescente = 15f; // La force de descente du personnage lorsqu'il est en l'air
    bool toucheSol; // Booleen pour detecter si le perso touche le sol
    bool peutBouger = true; // Verification si le personnage peut bouger
    Vector3 dernierMouvement; // Enregistrement du dernier mouvement du joueur

    // Raccourcis GetComponent
    Rigidbody rb;
    Animator animateur;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody
        animateur = GetComponent<Animator>(); // Animator
    }

    void Update()
    {
        /*--------------
         ** MOUVEMENT **
         ---------------*/
        // Deplacement horizontal perso
        var vDeplacement = Input.GetAxis("Horizontal") * vitesseDeplacement * Time.deltaTime;

        // Deplacement vertical perso
        var vMonte = Input.GetAxis("Vertical") * vitesseDeplacement * Time.deltaTime;

        // Raccourci pour la velocite du saut
        float velociteY = rb.velocity.y;

        // Normalize pour le vecteur de direction
        Vector3 direction = new Vector3(-vMonte, 0, vDeplacement).normalized;

        // Multiplier la velocite par le temps
        direction *= vitesseDeplacement * Time.deltaTime;

        // Controles pour faire avancer le perso avec les touches Horizontales (A and D) et Verticales (W and S)
        if (peutBouger)
        {
            // On regarde avec le raycasting si un obstacle est sur le chemin
            if (!Physics.Raycast(rb.position, direction, direction.magnitude))
            {
                // Si il n'y a pas d'obstacle, le personnage bouge
                rb.AddForce(direction * vitesseDeplacement, ForceMode.VelocityChange);
            }
        }

        /*-------------
         * ANIMATIONS *
         -------------*/
        // On regarde si les param�tres de l'animator sont �gaux � 0...
        if (animateur.GetFloat("VelocityX") == 0 && animateur.GetFloat("VelocityZ") == 0)
        {
            // Si oui, l'animation idle est true
            animateur.SetBool("idle", true);
        }
        else
        {
            // Si non, l'animation idle est false
            animateur.SetBool("idle", false);
        }

        // Association des param�tres de l'animator avec les directions de d�placement du personnage
        animateur.SetFloat("VelocityX", vDeplacement);
        animateur.SetFloat("VelocityZ", vMonte);
        animateur.SetFloat("VelocityY", velociteY);

        // Si le joueur a tourn� � gauche, le personnage va faire face � gauche et il fera de m�me pour la droite
        if (vDeplacement > 0 || vDeplacement < 0)
        {
            dernierMouvement = new Vector3(vDeplacement, 0f, vMonte).normalized;
        }

        /*---------
         ** SAUT **
         ----------*/
        // Si on est dans le tutoriel, Kirie saute moins haut que dans les autres scènes
    /*    switch (_collision_kirie.journalRamasse)
        {
            case false:
                forceSaut = 500f;
                vitesseDeplacement = 14f;
                break;
            case true:
                forceSaut = 1000f;
                vitesseDeplacement = 15f;
                break;
        }*/

        RaycastHit infoCollision;
        // Cast des spheres vers bas perso + variable infoCollision prends valeurs
        toucheSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.1f, 1f), 0f, -transform.up, out infoCollision, 1f);

        // Si le jouer appuie sur espace la velocitee Y augmente et le personnage saute  
        if (Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            rb.AddForce(new Vector3(0, forceSaut, 0), ForceMode.Impulse);
        }
        // Si la velocite Y est plus petite que 2...
        else if (rb.velocity.y < 0 && !toucheSol)
        {
            // On applique une force de descente au personnage pour le forcer au sol
             rb.velocity += Vector3.up * Physics.gravity.y * (multiplicateurDescente - 1) * Time.deltaTime;
        }
        // On v�rifie si le personnage touche le sol ou non
        switch (toucheSol)
        {
            case false:
                animateur.SetBool("saute", true);
                break;
            case true:
                animateur.SetBool("saute", false);
                break;
        }

        // Fin du Saut Droite
        if (animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut") && toucheSol)
        {
            animateur.Play("FinSaut");
            StartCoroutine(RecupSaut());
        }

        // Fin Saut gauche
        if (animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_Gauche") && toucheSol)
        {
            animateur.Play("FinSaut_Gauche");
            StartCoroutine(RecupSaut());
        }

        /*
        * Gestion des animations de saut dans differents angles
        -------------------------------------------------------------------------------------------------------------------------------------------*/
        /* A Droite en Haut */
        // On regarde si le joueur bouge vers le haut a droite et appuie sur espace
        if (animateur.GetFloat("VelocityX") > 0 && animateur.GetFloat("VelocityZ") > 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_DiagoHDroite");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_DiagoHDroite") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_DiagoHDroite");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut());
        }
        /* A Droite en Bas */
        // On regarde si le joueur bouge vers le bas a droite et appuie sur espace
        if (animateur.GetFloat("VelocityX") > 0 && animateur.GetFloat("VelocityZ") < 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_DiagoBDroite");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_DiagoBDroite") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_DiagoBDroite");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut());
        }
        /* A Gauche en Haut */
        // On regarde si le joueur bouge vers le haut a gauche et appuie sur espace
        if (animateur.GetFloat("VelocityX") < 0 && animateur.GetFloat("VelocityZ") > 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_DiagoHGauche");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_DiagoHGauche") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_DiagoHGauche");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut());
        }
        /* A Gauche en Bas */
        // On regarde si le joueur bouge vers le bas a gauche et appuie sur espace
        if (animateur.GetFloat("VelocityX") < 0 && animateur.GetFloat("VelocityZ") < 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_DiagoBGauche");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_DiagoBGauche") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_DiagoBGauche");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut());
        }
        /* Vers le Bas */
        // On regarde si le joueur bouge vers le bas
        if (animateur.GetFloat("VelocityX") == 0 && animateur.GetFloat("VelocityZ") < 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_Bas");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_Bas") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_Bas");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut()); 
        }
        /* Vers le Haut */ 
        // On regarde si le joueur bouge vers le bas
        if (animateur.GetFloat("VelocityX") == 0 && animateur.GetFloat("VelocityZ") > 0 && Input.GetKeyDown(KeyCode.Space) && toucheSol && peutBouger)
        {
            animateur.Play("MilieuSaut_Haut");
            animateur.SetBool("tombe", false);
        }
        // On regarde si l'animation a fini de jouer et si la velocite Y est plus petite que 0
        if (animateur.GetFloat("VelocityY") < 0 && animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_Haut") && toucheSol && peutBouger)
        {
            animateur.Play("FinSaut_Haut");
            animateur.SetBool("tombe", true);
            StartCoroutine(RecupSaut());
        }
      
        // On verifie, ici, si le saut est active a partir de la gauche ou de la droite 
        // on regarde si la velocite X est plus grande que 0...
        if (animateur.GetFloat("VelocityX") > 0)
        {
            // Si oui, on confirme que le mouvement ne se fait pas a gauche
            animateur.SetBool("aGauche", false);
        }
        // Sinon, on confirme que le mouvement se fait a gauche
        else if (animateur.GetFloat("VelocityX") < 0)
        {
            animateur.SetBool("aGauche", true);
        }
        /*
        * Fin de la gestion des angles de saut
        ------------------------------------------------------------------------------------------------------------------------------------------------*/

        /* 
        * Gestion des angles dans l'animation idle
        --------------------------------------------------------------------------------------------------------------------------- */
        // On verifie dans quel angle le personnage se dirige et on active l'animation idle correspondante � son mouvement
        // Si la velocite du rigidbody est egale a 0...
        if (direction != Vector3.zero)
        {
            float angle = Vector3.SignedAngle(Vector3.forward, direction, Vector3.up);

            // On lance la bonne animation dependemment de l'angle du perso
            if (angle > 0)
            {
                animateur.SetTrigger("idleGauche");
            }
            else
            {
                animateur.SetTrigger("idleDroite");
            }
        }
    }

    /* Pour voir le spherecast 
     ---------------------------------------------------------------------------*/
    private void OnDrawGizmos()
    {
        // On dessine la sph�re sous la capsule (perso), l� o� le sphereCast se fait
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 0.1f, 0f), 1f);
    } 

    /*--------------
     * IENUMERATOR *
     --------------*/
    // La couroutine qui permet de donner un effet de r�cup�ration du saut
    IEnumerator RecupSaut()
    {
        peutBouger = false;
        yield return new WaitForSeconds(0.8f);
        peutBouger = true;
    }
}