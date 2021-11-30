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
    private Text SuccessFailMessage; // the message prompt for game win or game lose
    private float currentHealth; // track the current health
    private float TimeElapse; // track the time elapse after game start because the health will decreasing periodically over time
    private float TotalTimeElapse; // track the total time elapse
    public float CurrentHealth{ // getter and setter for current health variable
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
        this.SuccessFailMessage.text = "Sucess!"; // set the message prompt for game win
        Application.Quit(); // quit the game
    }

    public void SetFail()
    {
        this.SuccessFailMessage.text = "Fail!"; // set the message prompt for game lose
        Application.Quit(); // quit the game
    }

    public void SetHealth(float health)
    {
        this.ImageHealthBar.fillAmount = health; // set health to the health bar
    }

    void Start()
    {
        //initialization of variables
        this.CurrentHealth = 1.0f;
        this.TimeElapse = 0.0f;
        this.TotalTimeElapse = 0.0f;
        this.SetHealth(this.CurrentHealth);

    }

    void Update()
    {
        
        this.TotalTimeElapse += Time.deltaTime; //update total time elapse

        if(this.TotalTimeElapse <= 6.0f) // if total time elapse < 6, we prompt the background story in the game for the user using text
        {
        
            this.SuccessFailMessage.text = "Captain! Our shield generator is hit and losing power! \n The shields are the only thing between the enemy's weapons and us. \n We have to take them out before the shield depletes completely or it's all over!";

        }
        else
        {
            if(this.TimeElapse<1)
            {
                this.TimeElapse += Time.deltaTime; // update elapse time if it is less than 1 second, otherwise we update health in the else block
            }
            else
            {
                this.CurrentHealth -= 0.01f; // update current health by percentage every 1 second is passed
                this.TimeElapse = 0.0f;
            }

            this.SetHealth(this.CurrentHealth); // call set health function to set health in the health bar
            //Debug.Log(this.CurrentHealth);
        }

    }
    
}