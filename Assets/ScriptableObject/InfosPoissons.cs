using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script qui va cr�er des objets scriptables et une liste de poissons - Par Camilia El Moustarih
[CreateAssetMenu(fileName = "InfosPoissons", menuName = "InfosPoissons", order = 1)]
public class InfosPoissons : ScriptableObject
{
    //Nom du poisson
    public string nomPoisson;
    
    //public GameObject objetInventaire;
   
    //Probabilit� d'attrapper un poisson X
    public int probabiliteDattraper;
}
