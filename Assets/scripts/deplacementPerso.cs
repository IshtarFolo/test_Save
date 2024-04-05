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
    private float vitesseDeplacement = 15f; // Vitesse de déplacement du personnage
    private float forceSaut = 15f; // Force du saut
    bool toucheSol; // Booleen pour detecter si le perso touche le sol
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
        var vDeplacement = Input.GetAxis("Horizontal") * vitesseDeplacement;

        // Deplacement vertical perso
        var vMonte = Input.GetAxis("Vertical") * vitesseDeplacement;

        // Raccourci pour la velocite du saut
        float velociteY = rb.velocity.y;

        // Controles pour faire avancer le perso sur l'axe des X avec les touches Horizontales (W et S)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position += new Vector3(-vMonte * Time.deltaTime, 0, 0);
        }

        /*-------------
         * ANIMATIONS *
         -------------*/
        // On regarde si les paramètres de l'animator sont égaux à 0...
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

        // Association des paramètres de l'animator avec les directions de déplacement du personnage
        animateur.SetFloat("VelocityX", vDeplacement);
        animateur.SetFloat("VelocityZ", vMonte);
        animateur.SetFloat("VelocityY", Mathf.Clamp(velociteY, -1 ,1));

        // Si le joueur a tourné à gauche, le personnage va faire face à gauche et il fera de même pour la droite
        if (vDeplacement > 0 || vDeplacement < 0)
        {
            dernierMouvement = new Vector3(vDeplacement, 0f, vMonte).normalized;
        }

        /*---------
         ** SAUT **
         ----------*/
        RaycastHit infoCollision;
        // Cast des spheres vers bas perso + variable infoCollision prends valeurs
        toucheSol = Physics.SphereCast(transform.position + new Vector3(0f, 1f, 0f), 1f, -transform.up, out infoCollision, 1f);

        // Si le jouer appuie sur espace la velocitee Y augmente et le personnage saute  
        if (Input.GetKeyDown(KeyCode.Space) && toucheSol)
        {
            velociteY += forceSaut;
        }

        // Les velocitees se font passer les valeurs des variables vDeplacement et velociteY
        rb.velocity = transform.forward * vDeplacement + new Vector3(0, velociteY, 0);

        // pour voir si le joueur touche le sol
        // Debug.Log(toucheSol == true);
        Debug.Log(Mathf.Clamp(velociteY, -1, 1));

        // Saut
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
        }

        // Fin Saut gauche
        if (animateur.GetCurrentAnimatorStateInfo(0).IsName("MilieuSaut_Gauche") && toucheSol)
        {
            animateur.Play("FinSaut_Gauche");
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
    }

    void LateUpdate()
    {
        // Si la velocite du rigidbody est égale à 0...
        if (rb.velocity.magnitude == 0)
        {
            // On vérifie dans quel angle il se dirige et on active l'animation idle correspondante à son mouvement
            float angle = Vector3.SignedAngle(Vector3.forward, dernierMouvement, Vector3.up);
            if (angle < 0)
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
        // On dessine la sphère sous la capsule (perso), là où le sphereCast se fait
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, 1f, 0f), 1f);
    }
}