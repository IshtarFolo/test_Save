using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class GererMusique : MonoBehaviour
{
    public Slider mainSlider;
    public static float ValeurSon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Invoked when a submit button is clicked.
    public void SubmitSliderSetting()
    {
        //Displays the value of the slider in the console.
        Debug.Log(mainSlider.value);
        ValeurSon = mainSlider.value;
    }
}
