using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform Reticle; // UI component in unity
    private float CrosshairExpandTime; // defines the cross fire expanding time
    private float CeaseFireSize; // defines the size of the cross fire when fire is cease or no fire
    private float OpenFireSize;  // defines the max size of the cross fire when firing
    private float CurrentFireSize; // for tracking the current fire size
    
    void Awake()
    {
        this.Reticle = this.GetComponent<RectTransform>(); // get RectTransForm component from unity
        // setup default sizes and times
        this.CeaseFireSize = 100.0f;
        this.CurrentFireSize = 100.0f;
        this.OpenFireSize = 200.0f;
        this.CrosshairExpandTime = 1.0f;

    }

    void Update()
    {
        if((Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)) // detecting joystick control to see if it moves up/down(vertical) or left/right (horizontal)
        {
            this.CurrentFireSize = Mathf.Lerp(this.CurrentFireSize, this.OpenFireSize, Time.deltaTime * this.CrosshairExpandTime); //lerp the cross fire expansion while firing
        }
        else
        {
            this.CurrentFireSize = Mathf.Lerp(this.CurrentFireSize, this.CeaseFireSize, Time.deltaTime * this.CrosshairExpandTime); //lerp the cross fire expansion while ceasing or still
        }

        this.Reticle.sizeDelta = new Vector2(this.CurrentFireSize,this.CurrentFireSize); // set the cross fire expansion size
        // Debug.Log(this.CurrentFireSize);
        // Debug.Log(this.OpenFireSize);
    }

}