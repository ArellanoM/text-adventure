using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VolumeValue : MonoBehaviour {

     public Slider volumeSlider;

    // A reference to the enhanced Text object
    TextMeshProUGUI textMeshObject;

    void Awake()
    {
        textMeshObject = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update ()
    {
        float volValue;
        volValue = volumeSlider.value*100;
        textMeshObject.text = ((int) volValue).ToString();
	}
}
