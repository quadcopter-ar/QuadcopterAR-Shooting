using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class JoystickControl : MonoBehaviour
{

    // oculus setup
    private float deadZoneAmount = 0.5f; // deadzone that the oculus joystick can't reach
    public bool debugMode = false; //debug mode trigger
    private XRNode leftControllerNode = XRNode.LeftHand; // left controller node object
    private List<UnityEngine.XR.InputDevice> leftInputDevices = new List<UnityEngine.XR.InputDevice>(); // left input devices object
    private UnityEngine.XR.InputDevice leftController; // left controller
    private XRNode rightControllerNode = XRNode.RightHand; // right controller node object

    private List<UnityEngine.XR.InputDevice> rightInputDevices = new List<UnityEngine.XR.InputDevice>(); // right input devices object
    private UnityEngine.XR.InputDevice rightController; // right controller

    // joystick control setup
    public float xAngle, yAngle, zAngle; // three-dimentional angle variable for rotation of the joystick

    public bool leftStickTrueRightStickFalse; // tell the script the script is attached to joystick prefab for left controller or right controller
    public string KeyUp, KeyDown, KeyLeft, KeyRight;  // define the key to press for moving the joystick up/down/left/right when pressing keyboard
    private float lerpTimer; // a timer for lerping
    public float stepSize; // defines the strength of simulating the joystick rotation. The higher the more rapid joystick movement is shown in the game.
    private float XAngle // XAngle getter-setter
    {
        get
        {
                return xAngle;     
        }
        set
        {
            if(value > 90.0f) // max is 90 degree for X angle rotation
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

    private float YAngle // YAngle getter-setter
    {
        get
        {
                return yAngle;     
        }
        set
        {
            if(value > 90.0f) // max is 90 degree for Y angle rotation
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

    private float ZAngle // Z angle getter-setter
    {
        get
        {
                return zAngle;     
        }
        set
        {
            if(value > 90.0f) // max is 90 degree for Y angle rotation
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


    private Vector3 JoystickEulerAngles; // for rotation, we use Euler system on unity
    void Awake()
    {
        this.JoystickEulerAngles = this.transform.rotation.eulerAngles; // get the eulerAngle component

    }
    // Start is called before the first frame update
    void Start()
    {
        //initialization
        this.XAngle = 0.0f;
        this.YAngle = 0.0f;
        this.ZAngle = 0.0f;
        this.stepSize = 8.0f;
        this.lerpTimer = 0.0f;
        if (!debugMode){
          GetDevices(); //get device for left controller or right controller or both
        }
    }

    // Update is called once per frame
    void Update()
    {
        // oculus joystick connection
        if(!debugMode){ 
            if (leftController == null) {
                GetControllerDevices(leftControllerNode, ref leftController, ref leftInputDevices); // for get left controller
            }

            if (rightController == null) {
                GetControllerDevices(rightControllerNode, ref rightController, ref rightInputDevices); // for get right controller
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

        if(!Input.anyKey) // when joystick is still, we set angles back to 0, so the joystick in the game will be set back to its center
        {
            this.XAngle = 0.0f;
            this.YAngle = 0.0f;
            this.ZAngle = 0.0f;
        }
        //lerp the Euler rotation so that the joystick in the game can be rotated smoothly
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


    void CheckForChanges() { //check for control changes
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
                    this.RollToLeft(); // update angles when joystick is moving to left
                }else if(rightTouchCoords.x > deadZoneAmount){
                //   RotateRight(rightTouchCoords.x);
                    this.RollToRight(); // update angles when joystick is moving to right
                }

                if (rightTouchCoords.y < -deadZoneAmount) {
                //   MoveDown(rightTouchCoords.y);
                    this.RollToRear(); // update angles when joystick is moving to down
                } else if (rightTouchCoords.y > deadZoneAmount) {
                //   MoveUp(rightTouchCoords.y);
                    this.RollToFront(); // update angles when joystick is moving to up
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

    void RollToLeft() // update Z angle when rolling the joystick to left
    {
        this.ZAngle += this.stepSize;
    }

    void RollToRight() // update Z angle when rolling the joystick to right
    {
        this.ZAngle -= this.stepSize;
    }

    void RollToFront() // update X angle when rolling the joystick to up
    {
        this.XAngle += this.stepSize;
    }

    void RollToRear() // update X angle when rolling the joystick to down
    {
        this.XAngle -= this.stepSize;
    }
}
