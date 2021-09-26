using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class JoystickControl : MonoBehaviour
{

    // oculus setup
    public bool debugMode = false;
    private XRNode leftControllerNode = XRNode.LeftHand;
    private List<UnityEngine.XR.InputDevice> leftInputDevices = new List<UnityEngine.XR.InputDevice>();
    private UnityEngine.XR.InputDevice leftController;
    private XRNode rightControllerNode = XRNode.RightHand;
    private List<UnityEngine.XR.InputDevice> rightInputDevices = new List<UnityEngine.XR.InputDevice>();
    private UnityEngine.XR.InputDevice rightController;

    // joystick control setup
    public float xAngle, yAngle, zAngle;
    [RequireComponent(typeof(bool))]
    public bool leftStickTrueRightStickFalse;
    public string KeyUp, KeyDown, KeyLeft, KeyRight; 
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
        this.stepSize = 8.0f;
        this.lerpTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // oculus joystick connection
        if(!debugMode){
            if (leftController == null) {
                GetControllerDevices(leftControllerNode, ref leftController, ref leftInputDevices);
            }

            if (rightController == null) {
                GetControllerDevices(rightControllerNode, ref rightController, ref rightInputDevices);
            }
            // content for joystick motion detection
            CheckForChanges();
        }



        // if(Input.GetKey(this.KeyUp))
        // {
        //     this.RollToFront();
        // }

        // if(Input.GetKey(this.KeyDown))
        // {
        //     this.RollToRear();
        // }

        // if(Input.GetKey(this.KeyLeft))
        // {
        //     this.RollToLeft();
        // }

        // if(Input.GetKey(this.KeyRight))
        // {
        //     this.RollToRight();
        // }

        if(!Input.anyKey)
        {
            this.XAngle = 0.0f;
            this.YAngle = 0.0f;
            this.ZAngle = 0.0f;
        }

        this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, Quaternion.Euler(this.XAngle,this.YAngle,this.ZAngle), this.stepSize * Time.deltaTime);
    }


    void CheckForChanges() {
        Vector2 leftTouchCoords;
        Vector2 rightTouchCoords;
        bool triggerVal;
        if(leftStickTrueRightStickFalse)
        {
            if (leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out leftTouchCoords) && leftTouchCoords != Vector2.zero)
            {

                if(leftTouchCoords.x < -deadZoneAmount){
                //   MoveLeft(leftTouchCoords.x);
                    this.RollToLeft();
                }else if(leftTouchCoords.x > deadZoneAmount){
                //   MoveRight(leftTouchCoords.x);
                    this.RollToRight();
                }

                if (leftTouchCoords.y < -deadZoneAmount) {
                    // MoveBackward(leftTouchCoords.y);
                    this.RollToRear();
                } else if (leftTouchCoords.y > deadZoneAmount) {
                    // MoveForward(leftTouchCoords.y);
                    this.RollToFront();
                }
            }
        }
        else
        {
            if (rightController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out rightTouchCoords) && rightTouchCoords != Vector2.zero)
            {
                if(rightTouchCoords.x < -deadZoneAmount){
                //   RotateLeft(rightTouchCoords.x);
                    this.RollToLeft();
                }else if(rightTouchCoords.x > deadZoneAmount){
                //   RotateRight(rightTouchCoords.x);
                    this.RollToRight();
                }

                if (rightTouchCoords.y < -deadZoneAmount) {
                //   MoveDown(rightTouchCoords.y);
                    this.RollToRear();
                } else if (rightTouchCoords.y > deadZoneAmount) {
                //   MoveUp(rightTouchCoords.y);
                    this.RollToFront();
                }
            }
 
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
