using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image ImageHealthBar;
    
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