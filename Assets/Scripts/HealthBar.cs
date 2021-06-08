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
    private float TotalTimeElapse;
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
                    this.SetFail();
                }
                else
                {
                    this.SuccessFailMessage.text = "Power: " + Math.Round(value*100.0f, 0).ToString() + "%";
                    // Debug.Log(value);
                }
            }
        }
    }

    public void SetSuccess()
    {
        this.SuccessFailMessage.text = "Sucess!";
        Application.Quit();
    }

    public void SetFail()
    {
        this.SuccessFailMessage.text = "Fail!";
        Application.Quit();
    }

    public void SetHealth(float health)
    {
        this.ImageHealthBar.fillAmount = health;
    }

    void Start()
    {
        this.CurrentHealth = 1.0f;
        this.TimeElapse = 0.0f;
        this.TotalTimeElapse = 0.0f;
        this.SetHealth(this.CurrentHealth);

    }

    void Update()
    {
        this.TotalTimeElapse += Time.deltaTime;

        if(this.TotalTimeElapse <= 6.0f)
        {
        
            this.SuccessFailMessage.text = "Captain! Our shield generator is hit and losing power! \n The shields are the only thing between the enemy's weapons and us. \n We have to take them out before the shield depletes completely or it's all over!";

        }
        else
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
    
}