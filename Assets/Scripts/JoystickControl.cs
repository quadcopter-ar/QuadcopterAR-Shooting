using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;
    private float lerpTimer;
    public float stepSize;
    private float XAngle
    {
        get
        {
                return xAngle;     
        }
        set
        {
            if(value > 90.0f)
            {
                xAngle = 90.0f;
            }
            else if(value < -90.0f)
            {
                xAngle = -90.0f;
            }
            else
            {
                xAngle = value;
            }

            
        }
    }

    private float YAngle
    {
        get
        {
                return yAngle;     
        }
        set
        {
            if(value > 90.0f)
            {
                yAngle = 90.0f;
            }
            else if(value < -90.0f)
            {
                yAngle = -90.0f;
            }
            else
            {
                yAngle = value;
            }

            
        }
    }

    private float ZAngle
    {
        get
        {
                return zAngle;     
        }
        set
        {
            if(value > 90.0f)
            {
                zAngle = 90.0f;
            }
            else if(value < -90.0f)
            {
                zAngle = -90.0f;
            }
            else
            {
                zAngle = value;
            }

            
        }
    }


    private Vector3 JoystickEulerAngles;
    void Awake()
    {
        this.JoystickEulerAngles = this.transform.rotation.eulerAngles;

    }
    // Start is called before the first frame update
    void Start()
    {
        this.XAngle = 0.0f;
        this.YAngle = 0.0f;
        this.ZAngle = 0.0f;
        this.stepSize = 1.0f;
        this.lerpTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.RollToFront();
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.RollToRear();
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.RollToLeft();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.RollToRight();
        }    

    }

    void RollToLeft()
    {
        this.ZAngle += this.stepSize;
    }

    void RollToRight()
    {
        this.ZAngle -= this.stepSize;
    }

    void RollToFront()
    {
        this.XAngle += this.stepSize;
    }

    void RollToRear()
    {
        this.XAngle -= this.stepSize;
    }
}
