using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image ImageHealthBar;
    
    [SerializeField]
    private Text SuccessFailMessage;
    private float currentHealth;
    private float TimeElapse;
    public float CurrentHealth{
        get
        {
            return currentHealth;
        }
        set
        {
            if(value<=1 && value>=0)
            {
                this.currentHealth = value;
                if(Math.Round(value*100.0f, 0) == 0)
                {
                    this.SuccessFailMessage.text = "Fail!";
                    Application.Quit();
                }
                else
                {
                    this.SuccessFailMessage.text = "Power: " + Math.Round(value*100.0f, 0).ToString() + "%";
                    // Debug.Log(value);
                }
            }
        }
    }

    public void SetHealth(float health)
    {
        this.ImageHealthBar.fillAmount = health;
    }

    void Start()
    {
        this.CurrentHealth = 1.0f;
        this.TimeElapse = 0.0f;
        this.SetHealth(this.CurrentHealth);

    }

    void Update()
    {

        if(this.TimeElapse<1)
        {
            this.TimeElapse += Time.deltaTime;
        }
        else
        {
            this.CurrentHealth -= 0.01f;
            this.TimeElapse = 0.0f;
        }

        this.SetHealth(this.CurrentHealth);
        //Debug.Log(this.CurrentHealth);
    }
    
}