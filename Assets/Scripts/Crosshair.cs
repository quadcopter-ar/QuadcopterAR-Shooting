using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform Reticle;
    private float Speed;
    private float CeaseFireSize;
    private float OpenFireSize;
    private float CurrentFireSize;
    
    void Awake()
    {
        this.Reticle = this.gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
        {
            this.CurrentFireSize = Mathf.Lerp(CurrentFireSize, OpenFireSize, Time.deltaTime * speed);
        }
        else
        {
            this.CurrentFireSize = Mathf.Lerp(CurrentFireSize, CeaseFireSize, Time.deltaTime * speed);
        }

        this.Reticle.sizeDelta = new Vector2(this.CurrentFireSize,this.CurrentFireSize);
    }

}