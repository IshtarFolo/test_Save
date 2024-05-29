using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacementPersoV2 : MonoBehaviour
{
    /**
     * Deplacement perso V2 par Xavier Arbour: 
     * 
     * Version amelioree de Deplacement perso, ce script permet le mouvement du personnage joueur (Kirie).
     * On applique une force au mouvement lateral, horizontal et sur l,axe des Y (pour le saut) a l'aide de la methode AddForce().
     * On applique egalement un temps de recuperation entre les saut pour faire jouer l'animation du personnage qui retombe au complet
     * en evitant de permettre au personnage de bouger pendant ce court delai. Aussi, on regarde dans quel angle le personnage se trouve
     * pour qu'il fasse face dans la direction desiree sans revenir automatiquement a sa position idle a droite. Ainsi, on garde la 
     * position idle a gauche ou a droite dependemment de la direction dans laquelle le joueur allait auparavant.
     * 
     */
    /*----------------
    *** VARIABLES ***
    -----------------*/
    public float vitesse = 30f; // Rapidite du personnage
    public float forceSaut = 50f; // Force du saut
    private float multiplicateurDescente = 15f; // La force de descente du personnage lorsqu'il est en l'air
    bool peutBouger = true; // Verification si le personnage peut bouger

    bool toucheSol; // Booleen pour detecter si le perso touche le sol
    bool saute = false; // Variable qui determine si le perso saute
    float derniereFoisAuSol; // variable qui calcule le temps de la derniere fois que le perso eatit au sol
    bool peutSauter = true; // Variable qui determine si le perso peut sauter 

    // Raccourcis GetComponent
    Rigidbody rb;
    Animator animateur;

    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Rigidbody
        animateur = GetComponent<Animator>(); // Animator

        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Pour detecter les collisions de manière permanente et
                                                                       // eviter que le pero ne colle trop au murs
    }
    private void Update()
    {
        /*
         * Gestion du Saut *
         ----------------------------------------------------------------------------------------------------------------------------*/
        RaycastHit infoCollision;

        // Variable qui regarde si le perso etait precedemment au sol
        bool etaitAuSol = toucheSol;

        // Cast des spheres vers bas perso + variable infoCollision prends valeurs
        toucheSol = Physics.SphereCast(transform.position + new Vector3(0f, 0.3f, 1f), 0f, -transform.up, out infoCollision, 1.5f);

        // Si le perso peut sauter et qu'on appuie sur espace et que le perso touche le sol, on lance la fonction de saut
        if (peutSauter)
        {
            if (Input.GetKey(KeyCode.Space) && toucheSol)
            {
                FonctionSaut();
            }
        }

        // Si le perso n'a pas saute precedemment et touche le sol
        if (!etaitAuSol && toucheSol)
        {
            // On calcule le temps que le perso a passe en l'air avant de retoucher le sol
            derniereFoisAuSol = Time.time;
        }

        // Puis on enleve au temps present le temps passe en l'air pour determiner si le personnage retombe
        // et on lance l'animation de retour au sol
        if (Time.time - derniereFoisAuSol < 1f)
        {
            animateur.SetBool("auSol", true);
            StartCoroutine(RecupSaut());
            saute = false;
        }
        else
        {
            animateur.SetBool("auSol", false);
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
                else if (angle < 0)
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
            else
            {
                // Si non, l'animation idle est false
                animateur.SetBool("idle", false);
                // L'animation de course s'active
                animateur.SetBool("cours", true);
            }

            if (rb.velocity.y < 0 && !toucheSol)
            {
                // On applique une force de descente au personnage pour le forcer au sol
                rb.velocity += Vector3.up * Physics.gravity.y * (multiplicateurDescente - 1) * Time.deltaTime;
            }

            // Si le perso ne saute pas et tombe d'un rebord on declenche l'animation ou il tombe
            if (!saute && !toucheSol && rb.velocity.y < 0)
            {
                animateur.SetBool("tombe", true);
            }
            else
            {
                animateur.SetBool("tombe", false);
            }
        }
    }
    /*
     * FONCTIONS SUPPLEMENTAIRES
     ------------------------------------------------------------------------------------------------------------*/
    /*=======
     * SAUT *
     =======*/
    private void FonctionSaut()
    {
        // On ajoute une force pour faire sauter le perso
        rb.AddForce(new Vector3(0, forceSaut, 0), ForceMode.Impulse);
        // On active la variable de saut de l'animator
        animateur.SetBool("saute", true);
        // Le perso peut sauter
        saute = true;
        derniereFoisAuSol = 0;
    }

    /*
     * COROUTINES
     -----------------------------------------------------------------------------------------------------------*/
    /*----------------------
     * RECUPERAION DU SAUT *
     ----------------------*/
    // La couroutine qui permet de donner un effet de r�cup�ration du saut
    IEnumerator RecupSaut()
    {
        peutBouger = false;
        peutSauter = false;
        animateur.SetBool("saute", false);
        yield return new WaitForSeconds(0.5f);
        peutBouger = true;
        peutSauter = true;
    }
}