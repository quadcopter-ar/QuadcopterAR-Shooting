using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class JoystickControl : MonoBehaviour
{

    // oculus setup
    private float deadZoneAmount = 0.5f;
    public bool debugMode = false;
    private XRNode leftControllerNode = XRNode.LeftHand;
    private List<UnityEngine.XR.InputDevice> leftInputDevices = new List<UnityEngine.XR.InputDevice>();
    private UnityEngine.XR.InputDevice leftController;
    private XRNode rightControllerNode = XRNode.RightHand;
    private List<UnityEngine.XR.InputDevice> rightInputDevices = new List<UnityEngine.XR.InputDevice>();
    private UnityEngine.XR.InputDevice rightController;

    // joystick control setup
    public float xAngle, yAngle, zAngle;

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
        if (!debugMode){
          GetDevices();
        }
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

    void GetDevices() {
        //Gets the Right Controller Devices
        GetControllerDevices(leftControllerNode, ref leftController, ref leftInputDevices);

        //Gets the Right Controller Devices
        GetControllerDevices(rightControllerNode, ref rightController, ref rightInputDevices);


        Debug.Log(string.Format("Device name '{0}' with characteristics '{1}'", leftController.name, leftController.characteristics));

        Debug.Log(string.Format("Device name '{0}' with characteristics '{1}'", rightController.name, rightController.characteristics));

    }


    void CheckForChanges() {
        Vector2 leftTouchCoords;
        Vector2 rightTouchCoords;
        bool triggerVal;
        if(leftStickTrueRightStickFalse)
        {
            if (leftController.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out leftTouchCoords) && leftTouchCoords != Vector2.zero)
            {
                Debug.Log("We moving with Left controller");

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
                Debug.Log("We moving with right controller");
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

    void GetControllerDevices(XRNode controllerNode, ref UnityEngine.XR.InputDevice controller,ref List<UnityEngine.XR.InputDevice> inputDevices) {
        Debug.Log("Get devices is called");
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(controllerNode, inputDevices);

        if (inputDevices.Count == 1){
            controller = inputDevices[0];
            Debug.Log(string.Format("Device name '{0}' with characteristics '{1}'", controller.name, controller.characteristics));
        }

        if (inputDevices.Count > 1) {
            Debug.LogAssertion("More than one device found with the same input characteristics");
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
