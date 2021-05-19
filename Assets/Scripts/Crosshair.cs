using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform Reticle;
    private float CrosshairExpandTime;
    private float CeaseFireSize;
    private float OpenFireSize;
    private float CurrentFireSize;
    
    void Awake()
    {
        this.Reticle = this.GetComponent<RectTransform>();
        this.CeaseFireSize = 100.0f;
        this.CurrentFireSize = 100.0f;
        this.OpenFireSize = 200.0f;
        this.CrosshairExpandTime = 1.0f;

    }

    void Update()
    {
        if((Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f))
        {
            this.CurrentFireSize = Mathf.Lerp(this.CurrentFireSize, this.OpenFireSize, Time.deltaTime * this.CrosshairExpandTime);
        }
        else
        {
            this.CurrentFireSize = Mathf.Lerp(this.CurrentFireSize, this.CeaseFireSize, Time.deltaTime * this.CrosshairExpandTime);
        }

        this.Reticle.sizeDelta = new Vector2(this.CurrentFireSize,this.CurrentFireSize);
        // Debug.Log(this.CurrentFireSize);
        // Debug.Log(this.OpenFireSize);
    }

}